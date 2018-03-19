using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SingularityForensic.Imaging {
    //DD镜像挂载器;
    class DDImgMounter : IImgMounter {
        public DDImgMounter(string imgPath, FileAccess fileAccess, FileShare fileShare) {
            if (string.IsNullOrEmpty(imgPath)) {
                throw new ArgumentNullException(nameof(imgPath));
            }

            try {
                if (!File.Exists(imgPath)) {
                    throw new FileNotFoundException(imgPath);
                }

                _fs = new FileStream(imgPath, FileMode.Open, fileAccess, fileShare);
                this.ImgPath = imgPath;
            }
            catch (Exception ex) {
                LoggerService.Current?.WriteCallerLine(ex.Message);
                throw;
            }


        }

        public string ImgPath { get; }

        private FileStream _fs;

        //提供描述信息;
        public IEnumerable<(string key, string value)> Infoes {
            get {
                yield return default((string key, string value));
            }
        }

        public Stream RawStream => _fs;

        public string Formart => LanguageService.Current?.FindResourceString(Constants.DDStreamFormat);

        private bool _disposed;
        public void Dispose() {
            if (!_disposed) {
                throw new ObjectDisposedException(nameof(DDImgMounter));
            }

            try {
                _fs.Dispose();
                _disposed = true;
            }
            catch (Exception ex) {
                LoggerService.Current?.WriteCallerLine(ex.Message);
            }
        }
    }

    [Export(typeof(IImgMounterProvider))]
    class DDImgMounterProvider : IImgMounterProvider {
        public int Sort => 128;

        public string FormatName => Constants.DDStreamMounter;

        public bool CheckIsValidImg(string imgPath) {
            if (string.IsNullOrEmpty(imgPath)) {
                throw new ArgumentNullException(nameof(imgPath));
            }

            try {
                return File.Exists(imgPath);
            }
            catch (Exception ex) {
                LoggerService.Current?.WriteLine(ex.Message);
            }

            return false;
        }

        public IImgMounter CreateMounter(string imgPath,XElement xElem, FileAccess fileAccess, FileShare fileShare) {
            if (string.IsNullOrEmpty(imgPath)) {
                throw new ArgumentNullException(nameof(imgPath));
            }

            return new DDImgMounter(imgPath, fileAccess, fileShare);
        }

    }
}
