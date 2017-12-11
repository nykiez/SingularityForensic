using CDFCEntities.Abstracts;
using CDFCStatic.CMethods;
using System;
using System.IO;

namespace CDFCEntities.DeviceObjects {

    public class ImgFile : DefaultObjectDevice {
        private FileStream fs;
        public FileStream Stream {
            get {
                return fs;
            }
        }
        public string Name { get; set; }
        public string Path { get; private set; }
        public bool Exit() {
            try {
                fs.Close();
            }
            catch {
                return false;
            }
            return true;
        }
        public static ImgFile GetImgFile(string path) {
            ImgFile imgFile = new ImgFile();
            imgFile.SectorSize = 512;
            try {
                imgFile.fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                int dotIndex = path.LastIndexOf(@"\");
                imgFile.DriveType = Enums.DriveType.ImgFile;
                imgFile.Handle = imgFile.fs.SafeFileHandle.DangerousGetHandle();
                imgFile.Size = ImgFileMethods.cdfc_common_imagefile_size(imgFile.Handle);
                imgFile.Name = path.Substring(dotIndex + 1);
            }
            catch (Exception ex) {
                EventLogger.Logger.WriteLine("ImgFile->获得镜像文件错误:" + ex.Message);
                throw;
            }
            imgFile.Path = path;
            return imgFile;
        }
        public override int Read(byte[] destination, int offset, int byteCount, ulong pos) {
            fs.Position = (long)pos;
            return Stream.Read(destination, offset, byteCount);
        }
    }
}
