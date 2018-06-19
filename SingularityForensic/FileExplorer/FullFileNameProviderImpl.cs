using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.ComponentModel.Composition;
using System.Text;

namespace SingularityForensic.FileExplorer {
    [Export(typeof(IFullFileNameProvider))]
    public class FullFileNameProviderImpl : IFullFileNameProvider {
        public int Sort => 128;

        public string GetFullFileName(IFile file, bool selfIncluded) {
            if (file == null) {
                throw new ArgumentNullException(nameof(file));
            }
            
            var sb = new StringBuilder();
            var fileNode = selfIncluded?file:file.Parent;
            while (fileNode != null) {
                if (fileNode is IPartition part) {
                    sb.Insert(0, $"/{part.GetPartFixAndName()}");
                }
                else {
                    sb.Insert(0, $"/{fileNode.Name}");
                }

                fileNode = fileNode.Parent;
            }
            return sb.ToString();
        }

    }
}
