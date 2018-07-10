using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Hash;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer
{

    /// <summary>
    /// 哈希集元数据提供器;
    /// </summary>
    [Export(typeof(IFileMetaDataProvider))]
    class HashSetFileMetaDataProvider : FileMetaDataProvider {
        public override string DisplayName => LanguageService.FindResourceString(Constants.FileMetaDataName_HashSet);

        public override Type MetaDataType => typeof(string);

        public override string GUID => Constants.FileMetaDataGUID_HashSets;

        public override int Order => 16;

        public override object GetMetaData(IFile file) {
            var hashSets = file.ExtensibleTag.GetInstance<IHashSet[]>(Constants.FileTag_HashSets);
            if (hashSets == null) {
                return string.Empty;
            }

            var sb = new StringBuilder();
            foreach (var hashset in hashSets) {
                if (!hashset.IsEnabled) {
                    continue;
                }
                sb.Append($"{hashset.Name};");
            }
            return sb.ToString();

            //一下代码原用于实时查询,匹配显示哈希集所用,但性能不足(滚动卡顿);
            //            var hashSets = HashSetManagementService.HashSets;

            //            foreach (var hashSet in hashSets) {
            //                if (!hashSet.IsEnabled) {
            //                    continue;
            //                }
            //#if DEBUG
            //                if (file.Name == "avformat-56.dll") {

            //                }
            //#endif
            //                var hashValue = file.ExtensibleTag?.GetInstance<string>($"{Constants.FileHashMetaDataProvider_GUIDPrefix}{hashSet.Hasher.GUID}");

            //                if (hashValue == null) {
            //                    continue;
            //                }

            //                if (hashValue.Length != hashSet.Hasher.BytesPerHashValue * 2) {
            //                    continue;
            //                }

            //                try {
            //                    hashSet.BeginOpen();
            //                    var hashPairs = hashSet.FindHashPairs(hashValue);
            //                    if (hashPairs.FirstOrDefault() != null) {
            //                        sb.Append($"{hashSet.Name};");
            //                    }
            //                }
            //                catch (Exception ex) {

            //                }
            //                finally {
            //                    hashSet.EndOpen();
            //                }

            //            }
            //            return sb.ToString();
            //            return string.Empty;


        }
    }
}
