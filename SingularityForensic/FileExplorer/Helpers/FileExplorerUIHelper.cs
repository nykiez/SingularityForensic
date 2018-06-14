using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Hex;
using System;
using System.Linq;
using SysIO = System.IO;

namespace SingularityForensic.FileExplorer.Helpers {
    public static class FileExplorerUIHelper {
        /// <summary>
        /// 根据流文件创建一个十六进制Tab;
        /// </summary>
        /// <param name="streamFile"></param>
        /// <returns></returns>
        internal static (IDocument doc, IHexDataContext hexDataContext)?
            GetStreamHexDocument(IStreamFile streamFile) {

            var mainDocService = DocumentService.MainDocumentService;
            if (mainDocService == null) {
                LoggerService.WriteCallerLine($"{nameof(mainDocService)} can't be null.");
                return null;
            }

            var hexDoc = mainDocService.CreateNewDocument();
            
            var hexService = ServiceProvider.Current.GetInstance<IHexService>();
            if (hexService == null) {
                LoggerService.WriteCallerLine($"{nameof(hexService)} can't be null.");
                return null;
            }

            var hexDataContext = hexService.CreateNewHexDataContext(streamFile?.BaseStream);
            hexDoc.SetInstance(hexDataContext, Contracts.Hex.Constants.Tag_HexDataContext);
            hexDoc.UIObject = hexDataContext.UIObject;
            hexDataContext.SetInstance<IFile>(streamFile,Contracts.FileExplorer.Constants.HexDataContextTag_File);

            //加载十六进制;
            hexService.LoadHexDataContext(hexDataContext);

            return (hexDoc, hexDataContext);
        }

        /// <summary>
        /// 添加/获取文件(设备/分区)文档;
        /// </summary>
        /// <param name="device"></param>
        internal static IDocumentBase GetOrAddFileDocument(IFile file) {
            //检查文档区域是否已经被添加了相关文件;
            var preDocument = CheckTagAddedToDocument(file);
            if (preDocument != null) {
                return preDocument;
            }

            var mainDocService = DocumentService.MainDocumentService;
            if (mainDocService == null) {
                LoggerService.WriteCallerLine($"{nameof(mainDocService)} can't be null.");
                return null;
            }

            var enumDoc = mainDocService.CreateNewEnumerableDocument();
            enumDoc.SetInstance(file,Contracts.FileExplorer.Constants.DocumentTag_File);
            if (file is IPartition part) {
                enumDoc.Title = part.GetPartFixAndName();
            }
            else {
                enumDoc.Title = file.Name;
            }

            mainDocService.AddDocument(enumDoc);
            mainDocService.SelectedDocument = enumDoc;
            return enumDoc;
        }

        /// <summary>
        /// 查找文档区域是否已经添加了File相关文档;
        /// </summary>
        /// <param name="tag"></param>
        internal static IDocumentBase CheckTagAddedToDocument(IFile file) {
            var mainDocService = DocumentService.MainDocumentService;
            if (mainDocService == null) {
                LoggerService.WriteCallerLine($"{nameof(mainDocService)} can't be null.");
                return null;
            }

            var doc = mainDocService.CurrentDocuments.FirstOrDefault(p =>
            p.GetInstance<IFile>( Contracts.FileExplorer.Constants.DocumentTag_File) == file);

            if (doc != null) {
                mainDocService.SelectedDocument = doc;
                return doc;
            }

            return null;
        }

        /// <summary>
        /// 保存文件到临时目录;
        /// </summary>
        /// <param name="blockFile"></param>
        /// <returns>保存的路径</returns>
        public static string SaveFileToTempPath(IFile blockFile) {
            var inputStream = blockFile.GetInputStream();
            if (inputStream == null) {
                return string.Empty;
            }

            var tempDirectory = $"{Environment.CurrentDirectory}/{Constants.TempDirectoryName}/";
            var tempFileName = tempDirectory + $"{blockFile.Name}";

            try {
                //创建临时文件夹;
                if (!System.IO.Directory.Exists(tempDirectory)) {
                    System.IO.Directory.CreateDirectory(tempDirectory);
                }

                using (var tempFs = SysIO.File.Create(tempFileName)) {
                    inputStream.CopyTo(tempFs);
                }

                return tempFileName;
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
                MsgBoxService.ShowError(ex.Message);
            }
            finally {
                inputStream.Dispose();
            }
            return string.Empty;
        }
    }
}
