using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Imaging {
    public static class Constants {
        //镜像案件文件来源类型;
        public const string EvidenceType_Img = "675300D7-F4D6-46F3-A420-C9E6B99EEDB9";

        //源镜像路径;
        public const string ImgPath = nameof(ImgPath);

        //镜像文件安全访问级别相关;
        public const string ImgFileFileAccess = nameof(ImgFileFileAccess);
        public const string ImgFileFileShare = nameof(ImgFileFileShare);

        //镜像格式;
        public const string ImgFormat = nameof(ImgFormat);
    }
}
