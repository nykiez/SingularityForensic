using CDFC.Util.PInvoke;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SingularityForensic.Ext {
    [Export(typeof(IStreamParsingProvider))]
    public partial class ExtStreamParsingProvider : IStreamParsingProvider {
        public int Order => 33;

        public string GUID => Constants.StreamParserGUID_Ext;

        public bool CheckIsValidStream(Stream stream) {
            if(stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            var unManagedStreamAdapter = new UnmanagedStreamAdapter(stream);

            try {
                var stPartition = ExtX_Init(unManagedStreamAdapter.StreamPtr);
                if(stPartition == IntPtr.Zero) {
                    return false;
                }
                else {
                    ExtX_Exit(stPartition);
                    return true;
                }
            }
            catch(Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
                return false;
            }
            finally {
                unManagedStreamAdapter.Dispose();
            }
        }

        public IFile ParseStream(Stream stream, string name, XElement xElem, IProgressReporter reporter) {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }


            var part = FileFactory.CreatePartition(Constants.PartitionKey_Ext);
            var stoken = part.GetStoken(Constants.PartitionKey_Ext);
            stoken.BaseStream = stream;
            stoken.Name = name;
            stoken.Size = stream.Length;
            stoken.TypeGuids = new string[] {
                Constants.PartitionType_Ext
            };

            var extUnmanagedManager = new ExtUnmanagedManager(stream);
            var partInfo = new ExtPartInfo {
                ExtUnmanagedManager = extUnmanagedManager
            };

#if DEBUG
            //IntPtr stExt4Inode = ExtX_Get_InodeInfo(extUnmanagedManager.ExtManagerPtr, 113880);    //129793
            //var stBlockList = ExtX_Get_BlockList(extUnmanagedManager.ExtManagerPtr, stExt4Inode);
            //var stDirDntry = ExtX_Parse_Dir(extUnmanagedManager.ExtManagerPtr, stBlockList);
            //var dirEntry = stDirDntry.GetStructure<StDirEntry>();
            //var cext4Entry = dirEntry.DirInfo.GetStructure<StExt4DirEntry>();

#endif
            //设定EXT分区详细信息;
            stoken.SetInstance(partInfo, Constants.PartitionStokenTag_ExtPartInfo);
            stoken.PartType = ServiceProvider.GetAllInstances<IPartitionType>().FirstOrDefault(p => p.GUID == Constants.PartitionType_Ext);
            if(partInfo.ExtUnmanagedManager.ExtManagerPtr == IntPtr.Zero) {
                extUnmanagedManager.Dispose();
                throw new InvalidOperationException($"{nameof(extUnmanagedManager.ExtManagerPtr)} can't be nullptr.");
            }
            
            try {
                LoadPartInfo(partInfo);
                LoadPartContent(part, reporter);
            }
            catch(Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
                
            }

            part.Disposing += OnPart_Disposing;

            return part;
        }

        private static void OnPart_Disposing(object sender, EventArgs e) {
            if (!(sender is IPartition part)) {
                return;
            }

            if (part.TypeGuids?.Contains(Constants.PartitionType_Ext) ?? false) {
                return;
            }

            PartitionStoken stoken = null;
            try {
                stoken = part.GetStoken(Constants.PartitionKey_Ext);
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }

            if(stoken == null) {
                return;
            }

            var partInfo = stoken.GetIntance<ExtPartInfo>(Constants.PartitionStokenTag_ExtPartInfo);
            if (partInfo == null) {
                return;
            }

            //释放非托管流;
            try {
                partInfo.ExtUnmanagedManager?.Dispose();
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }

        }

        private static void LoadPartInfo(ExtPartInfo partInfo) {
            if (partInfo == null) {
                throw new ArgumentNullException(nameof(partInfo));
            }

            var stSuperBlockPtr = ExtX_SuperBlock(partInfo.ExtUnmanagedManager.ExtManagerPtr);
            if(stSuperBlockPtr == IntPtr.Zero) {
                LoggerService.WriteCallerLine($"{nameof(stSuperBlockPtr)} can't be nullptr.");
                return;
            }
            else {
                partInfo.StSuperBlock = stSuperBlockPtr.GetStructure<StSuperBlock>();
            }

            var stGroupDescPtr = ExtX_GroupDesc(partInfo.ExtUnmanagedManager.ExtManagerPtr);
            if(stGroupDescPtr == IntPtr.Zero) {
                LoggerService.WriteCallerLine($"{nameof(stGroupDescPtr)} can't be nullptr.");
                return;
            }
            else {
                partInfo.StExt4GroupDescs = stGroupDescPtr.GetStructs<StExtGroupDesc>(p => p.Next)?.ToArray()??null;
            }

            //stExt4INodePtr = ExtX_Get_InodeInfo( 2);                  //加载Inode,BlockList,Dir;
            //stBlockListPtr = Cflabqd_Get_BlockList(stExt4INodePtr);     //加载Inode,BlockList,Dir;
            //stDirEntryPtr = Cflabqd_Parse_Dir(stBlockListPtr);            //加载Inode,BlockList,Dir;
        }

        private static void LoadPartContent(IPartition part,IProgressReporter reporter) {

            var partStoken = part.GetStoken(Constants.PartitionKey_Ext);
            var partInfo = partStoken.GetIntance<ExtPartInfo>(Constants.PartitionStokenTag_ExtPartInfo);
            var partManager = partInfo.ExtUnmanagedManager;
            if (partManager.ExtManagerPtr == IntPtr.Zero) {
                LoggerService.WriteCallerLine($"{nameof(partManager.ExtManagerPtr)} can't be nullptr.");
                return;
            }

#if DEBUG
            //var nodePtr = ExtX_Get_InodeInfo(partManager.ExtManagerPtr, 46);
            //var stBPtr = ExtX_Get_BlockList(partManager.ExtManagerPtr, nodePtr);
            //var ss = ExtX_Parse_Dir(partManager.ExtManagerPtr, stBPtr);
#endif

            var stExt4INodePtr = ExtX_Get_InodeInfo(partManager.ExtManagerPtr,2);                  //加载Inode,BlockList,Dir;
            if(stExt4INodePtr == IntPtr.Zero) {
                LoggerService.WriteCallerLine($"{nameof(stExt4INodePtr)} can't be nullptr.");
                return;
            }

            var stBlockListPtr = ExtX_Get_BlockList(partManager.ExtManagerPtr,stExt4INodePtr);     //加载Inode,BlockList,Dir;
            if(stBlockListPtr == IntPtr.Zero) {
                LoggerService.WriteCallerLine($"{nameof(stBlockListPtr)} can't be nullptr.");
                return;
            }
            
            var stDirEntryPtr = ExtX_Parse_Dir(partManager.ExtManagerPtr, stBlockListPtr);            //加载Inode,BlockList,Dir;
            if (stDirEntryPtr == IntPtr.Zero) {
                LoggerService.WriteCallerLine($"{nameof(stDirEntryPtr)} can't be nullptr.");
                return;
            }

            //当前分区已经被加载的大小;
            long partLoadedSize = 0;

            DealWithFileNode(
                     part,
                     partInfo,
                     stDirEntryPtr,
                     sz => {
                         if (reporter == null) {
                             return;
                         }

                         partLoadedSize += sz;
                         if (partStoken.Size == 0) {
                             return;
                         }
                         var per = (int)(partLoadedSize * 100 / partStoken.Size);
                         if (per > 100) {
                             per = 100;
                         }

                         if (partStoken.Size != 0) {
                             reporter.ReportProgress(per);
                         }
                     },
                     () => reporter?.CancelPending ?? false
                 );

        }

        /// <summary>
        /// 处理子文件/目录/其它加载;
        /// </summary>
        /// <param name="haveFileCollection"></param>
        /// <param name="filePtr">非托管文件节点指针</param>
        /// <param name="ntfSzAct"></param>
        /// <param name="isCancel"></param>
        private static void DealWithFileNode(
            IHaveFileCollection haveFileCollection,
            ExtPartInfo partInfo,
            IntPtr filePtr,
            Action<long> ntfSzAct,
            Func<bool> isCancel) {

            while (filePtr != IntPtr.Zero) {
                IFile file = null;
                FileStokenBase2 fileStoken2 = null;
                var stDirEntry = filePtr.GetStructure<StDirEntry>();

                //压栈;避免使用continue导致死循环以及冗余代码;
                void Push() {
                    if (file != null) {
                        haveFileCollection.Children.Add(file);
                    }
                    filePtr = stDirEntry.Next;
                }
                
                if (stDirEntry.DirInfo == IntPtr.Zero) {
                    Push();
                    continue;
                }

                var stExtDirEntry = stDirEntry.DirInfo.GetStructure<StExt4DirEntry>();
             
                var stExtINodePtr = ExtX_Get_InodeInfo(partInfo.ExtUnmanagedManager.ExtManagerPtr, stExtDirEntry.inode);
                if(stExtINodePtr == IntPtr.Zero) {
                    LoggerService.WriteCallerLine($"{nameof(stExtINodePtr)} can't be nullptr.");
                    Push();
                    continue;
                }
                var stExtINode = stExtINodePtr.GetStructure<StExt4Inode>();

                var stBlockListPtr = ExtX_Get_BlockList(partInfo.ExtUnmanagedManager.ExtManagerPtr, stExtINodePtr);
                if(stBlockListPtr == IntPtr.Zero) {
                    //LoggerService.WriteCallerLine($"{nameof(stBlockListPtr)} can't be nullptr.");
                    Push();
                    continue;
                }

                var extFileInfo = new ExtFileInfo {
                    StDirEntry = stDirEntry,
                    StExt4DirEntry = stExtDirEntry,
                    BlockListPtr = stBlockListPtr,
                    StExt4Inode = stExtINode
                };

                if (stExtDirEntry.file_type == Ext4FileType.Directory) {
                    var dir = FileFactory.CreateDirectory(Constants.DirectoryKey_Ext);

                    var dirStoken = dir.GetStoken(Constants.DirectoryKey_Ext);
                    dirStoken.TypeGuids = new string[] {
                        Constants.DirectoryType_Ext
                    };

                    file = dir;
                    fileStoken2 = dirStoken;

                    EditFileStoken2(fileStoken2, partInfo, extFileInfo);
                    //当目录名为".."或者"."时,跳过加载子文件;
                    //且所加载目录不能被删除;
                    if (stExtDirEntry._name[0] != '.' && !stDirEntry.bDel) {
                        LoadDirectoryContent(dir, partInfo, ntfSzAct, isCancel);
                    }
                    else if(stExtDirEntry._name[1] == '.') {
                        dirStoken.IsBack = true;
                    }
                    else {
                        dirStoken.IsLocalBackUp = true;
                    }
                }
                else if (stExtDirEntry.file_type == Ext4FileType.RegularFile) {
                    var regFile = FileFactory.CreateRegularFile(Constants.RegularFileKey_Ext);
                    var regFileStoken = regFile.GetStoken(Constants.RegularFileKey_Ext);
                    
                    regFileStoken.TypeGuids = new string[] {
                        Constants.RegularFileType_Ext
                    };

                    file = regFile;
                    fileStoken2 = regFileStoken;

                    EditFileStoken2(fileStoken2, partInfo, extFileInfo);
                    ntfSzAct?.Invoke(fileStoken2.Size);
                }

                Push();
                
                if (isCancel?.Invoke() ?? false) {
                    return;
                }

                
            }
        }


        private static void LoadDirectoryContent(IDirectory direct,
            ExtPartInfo partInfo,
            Action<long> ntfSzAct,
            Func<bool> isCancel) {

            var dirStoken = direct.GetStoken(Constants.DirectoryKey_Ext);
            var fatFileInfo = dirStoken.GetIntance<ExtFileInfo>(Constants.FileStokenTag_ExtFileInfo) as ExtFileInfo;

            var filePtr = ExtX_Parse_Dir(partInfo.ExtUnmanagedManager.ExtManagerPtr, fatFileInfo.BlockListPtr);

            DealWithFileNode(direct, partInfo, filePtr, ntfSzAct, isCancel);
        }

        /// <summary>
        /// 编辑目录/文件/其它的时间,簇列表等值;
        /// </summary>
        /// <param name="fileStoken2"></param>
        private static void EditFileStoken2(
            FileStokenBase2 fileStoken2,
            ExtPartInfo partInfo,
            ExtFileInfo extFileInfo) {

            if (fileStoken2 == null) {
                throw new ArgumentNullException(nameof(fileStoken2));
            }

            if (extFileInfo == null) {
                throw new ArgumentNullException(nameof(extFileInfo));
            }

            if (extFileInfo.BlockListPtr == null) {
                throw new InvalidOperationException($"{nameof(extFileInfo.BlockListPtr)} of {nameof(ExtFileInfo)} can't be nullptr.");
            }

            if (partInfo == null) {
                throw new ArgumentNullException(nameof(partInfo));
            }

            fileStoken2.SetInstance(extFileInfo, Constants.FileStokenTag_ExtFileInfo);

            fileStoken2.Name = extFileInfo.StExt4DirEntry?.Name;
            fileStoken2.Size = extFileInfo.StExt4Inode?.Size??0;
            fileStoken2.Deleted = extFileInfo.StDirEntry?.bDel??false;
            
            EditTime(fileStoken2, extFileInfo);
#if DEBUG
            //if (fileStoken2.Name.StartsWith(".journal")) {

            //}
            
#endif
            EditBlockGroups(fileStoken2, partInfo, extFileInfo);

        }

        /// <summary>
        /// 编辑时间;
        /// </summary>
        /// <param name="fileStoken2"></param>
        /// <param name="extFileInfo"></param>
        private static void EditTime(
            FileStokenBase2 fileStoken2,
            ExtFileInfo extFileInfo) {

            if (fileStoken2 == null) {
                throw new ArgumentNullException(nameof(fileStoken2));
            }

            if (extFileInfo == null) {
                throw new ArgumentNullException(nameof(extFileInfo));
            }

            //编辑时间;
            if(extFileInfo.StExt4Inode == null) {
                LoggerService.WriteCallerLine($"{nameof(extFileInfo)} can't be null.");
            }

            extFileInfo.StExt4Inode.Value.GetMacTime(out var modifiedTime, out var accessedTime, out var createTime);
            fileStoken2.ModifiedTime = modifiedTime;
            fileStoken2.AccessedTime = accessedTime;
            fileStoken2.CreateTime = createTime;
        }

        /// <summary>
        /// 编辑块组;
        /// </summary>
        /// <param name="fileStoken2"></param>
        /// <param name="partInfo"></param>
        /// <param name="extFileInfo"></param>
        private static void EditBlockGroups(
            FileStokenBase2 fileStoken2,
            ExtPartInfo partInfo,
            ExtFileInfo extFileInfo) {
            if (fileStoken2 == null) {
                throw new ArgumentNullException(nameof(fileStoken2));
            }

            if (extFileInfo == null) {
                throw new ArgumentNullException(nameof(extFileInfo));
            }

            if (extFileInfo.BlockListPtr == null) {
                throw new InvalidOperationException($"{nameof(extFileInfo.BlockListPtr)} of {nameof(ExtFileInfo)} can't be nullptr.");
            }

            if (partInfo == null) {
                throw new ArgumentNullException(nameof(partInfo));
            }

            if(extFileInfo.BlockList == null) {
                LoggerService.WriteCallerLine($"{nameof(extFileInfo.BlockList)} can't be null.");
                return;
            }
            
            var blocks = extFileInfo.BlockList;
            var blockSize = partInfo.StSuperBlock.BlockSize;

            if(blocks == null) {
                return;
            }
            
            //遍历,粘合,将连续的簇列表并入为一个块组;
            StBlockList? lastBlock = null;
            long firstClusterLBA = 0;

            int blockCount = 0;
            ulong lastBlockNum = 0;

            //重置局部变量;参数为本次(循环)最新的独立头块;
            void Reset(StBlockList block) {
                lastBlock = block;
                blockCount = (int)block.count;
                lastBlockNum = block.address;
                firstClusterLBA = (long)block.address * blockSize;
            }

            //压栈;
            void PushBlockGroup() {
                if (lastBlock == null) {
                    return;
                }

                fileStoken2.BlockGroups.Add(
                    BlockGroupFactory.CreateNewBlockGroup((long)lastBlock.Value.address, blockCount,blockSize, firstClusterLBA)
                );
            }
            
            try {
                foreach (var cluster in blocks) {
                    if (lastBlock == null) {
                        Reset(cluster);
                        continue;
                    }

                    //如果是连续的簇号则+1;
                    if (cluster.address == lastBlockNum + 1) {
                        lastBlockNum = cluster.address;
                        blockCount++;
                    }
                    //否则,构造BlockGroup,插入新建链表,并重置;
                    else {
                        PushBlockGroup();
                        Reset(cluster);
                    }
                }

                if (lastBlock != null) {
                    PushBlockGroup();
                }
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
        }


        
    }
    partial class ExtStreamParsingProvider {
        internal const string EXTAssembly = "ExtRecover.dll";
        [DllImport(EXTAssembly, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ExtX_Init(IntPtr stream);
        [DllImport(EXTAssembly, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ExtX_Get_InodeInfo(IntPtr stPartition, uint N_Inode);
        [DllImport(EXTAssembly, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ExtX_Get_BlockList(IntPtr stPartition, IntPtr ST_Ext4Inode);
        [DllImport(EXTAssembly, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ExtX_Parse_Dir(IntPtr stPartition, IntPtr stBlockList);
        [DllImport(EXTAssembly, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ExtX_InodeInfo_Free(IntPtr stPartition, IntPtr ST_Ext4Inode);
        [DllImport(EXTAssembly, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ExtX_BlockList_Free(IntPtr stPartition, IntPtr ST_BlockList);
        [DllImport(EXTAssembly, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ExtX_Dir_Free(IntPtr stPartition,IntPtr ST_DirDntry);
        [DllImport(EXTAssembly, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ExtX_Exit(IntPtr stPartition);

        //StExt4SuperBlock* ExtX_SuperBlock(void* stPartition);
        [DllImport(EXTAssembly, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ExtX_SuperBlock(IntPtr stPartition);

        //ExtX_GroupDesc(void* stPartition);
        [DllImport(EXTAssembly, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ExtX_GroupDesc(IntPtr stPartition);

       
    }
}
