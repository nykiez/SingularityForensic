using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer {
    static class FileExtensions {
        private static IEnumerable<IFullFileNameProvider> _fullFileNameProviders;
        public static string GetFullFileName(this IFile file) {
            var providers = _fullFileNameProviders ?? (_fullFileNameProviders = ServiceProvider.GetAllInstances<IFullFileNameProvider>().OrderBy(p => p.Sort).ToArray());
            foreach (var provider in providers) {
                try {
                    var name = provider.GetFullFileName(file,false);
                    if (name != null) {
                        return name;
                    }
                }
                catch (Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            }

            return null;
        }

        /// <summary>
        /// 当分区名为空时,获取一个语言名;(比如分区1);
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public static string GetPartFixAndName(this IPartition part) {
            if (!(part.Parent is IHaveFileCollection haveCollection)) {
                LoggerService.WriteCallerLine($"{nameof(haveCollection)} can't be null.");
                return part.Name;
            }

            var partSb = new StringBuilder($"{LanguageService.FindResourceString(Constants.Prefix_Partition)} {haveCollection.IndexOf(part) + 1}");
            if (!string.IsNullOrEmpty(part.Name)) {
                partSb.Append($"({part.Name})");
            }

            return partSb.ToString();
        }
    }
}
