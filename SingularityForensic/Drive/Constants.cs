namespace SingularityForensic.Drive {
    public static partial class Constants {
        //本地分区证据项类型;
        public const string EvidenceType_LocalVolume = "B29CE37D-5FE1-4AB0-99D9-4F3744623462";

        //本地硬盘证据项类型;
        public const string EvidenceType_LocalHDD = "755A8D8C-3790-4674-9D6E-6437566EAB3A";

        //驱动类型-本地卷;
        public const string DriveType_LocalVolume = "9B355290-4BFA-4602-A252-C99EEF3B0139";

        //驱动类型-本地硬盘;
        public const string DriveType_LocalHDD = "18456299-C255-4020-BE6C-6C638B1508F7";
    }

    //语言部分;
    public static partial class Constants {
        public const string NoHddMatchedFound = nameof(NoHddMatchedFound);
        public const string NoVolumeMatchedFound = nameof(NoVolumeMatchedFound);
    }
}
