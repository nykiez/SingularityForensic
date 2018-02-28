namespace SingularityForensic.Controls.ITunes {
    public static class PinKindsDefinitions {
        public const string ForensicClassBasic = nameof(ForensicClassBasic); //基础信息
        public const string ForensicClassInstantTalk = nameof(ForensicClassInstantTalk); //即时通讯;
        public const string ForensicClassBrowser = nameof(ForensicClassBrowser); //浏览器
        public const string ForensicClassMultiMedia = nameof(ForensicClassMultiMedia); //多媒体
        public const string ForensicClassOther = nameof(ForensicClassOther); //其它;

        public static string GetClassLabel(string afc) {
            switch (afc) {
                case ForensicClassBasic:
                    return "基本信息";
                case ForensicClassInstantTalk:
                    return "即时通讯";
                case ForensicClassMultiMedia:
                    return "多媒体文件";
                case ForensicClassOther:
                    return "其他";
                case ForensicClassBrowser:
                    return "浏览器";
            }
            return string.Empty;
        }

        public static string[] ForensicClassTypes => new string[] {
            ForensicClassBasic,
            ForensicClassInstantTalk,
            ForensicClassBrowser,
            ForensicClassMultiMedia,
            ForensicClassOther
        };
    }
}
