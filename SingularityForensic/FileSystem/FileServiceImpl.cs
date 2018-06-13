using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileSystem
{
    [Export(typeof(IFileService))]
     class FileServiceImpl:IFileService
    {
        public void Initialize() {
            this._fileInputStreamProviders = ServiceProvider.GetAllInstances<IFileInputStreamProvider>().OrderBy(p => p.Sort).ToArray();
            this._fullFileNameProviders = ServiceProvider.GetAllInstances<IFullFileNameProvider>().OrderBy(p => p.Sort).ToArray();
        }
        

        IEnumerable<IFileInputStreamProvider> _fileInputStreamProviders;
        IEnumerable<IFullFileNameProvider> _fullFileNameProviders;

        /// <summary>
        /// 获取输入文件的输入流;
        /// </summary>
        /// <param name="blockGrouped"></param>
        /// <remarks>这将遍历<see cref="IFileInputStreamProvider"/>完成文件流的获取</remarks>
        /// <returns></returns>
        public Stream GetInputStream(IFile file) {
            foreach (var provider in _fileInputStreamProviders) {
                try {
                    var stream = provider.GetInputStream(file);
                    if (stream != null) {
                        return stream;
                    }
                }
                catch (Exception ex) {
                    LoggerService.WriteException(ex);
                }
            }

            return null;
        }
    }
}
