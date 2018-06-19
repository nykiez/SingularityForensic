using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System.IO;
using System.Linq;

namespace SingularityForensic.Test.FAT {
    /// <summary>
    /// 为避免在多个测试中编写FAT分区相关的构造(mock)代码,此处提供了一个实体方法;
    /// </summary>
    static class FatMockers {
        /// <summary>
        /// 调用此方法前,需保证容器被正确配置;
        /// </summary>
        /// <returns></returns>
        public static IPartition GetFATPartitition() {
            var provider = ServiceProvider.GetAllInstances<IStreamParsingProvider>().
                FirstOrDefault(p => p.GUID == SingularityForensic.FAT.Constants.StreamParserGUID_FAT);
            Assert.IsNotNull(provider);

            return provider.ParseStream(File.OpenRead("E://anli/FAT32.img"),string.Empty,null,null) as IPartition;
        }
    }
}
