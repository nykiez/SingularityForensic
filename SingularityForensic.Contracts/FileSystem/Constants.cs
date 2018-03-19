namespace SingularityForensic.Contracts.FileSystem {
    public static partial class Constants {
        /// <summary>
        /// 文件案件类型GUID;
        /// </summary>
        public const string ImgCaseEvidence = nameof(ImgCaseEvidence);
        
        public const string DeviceNodeContextCommand = nameof(DeviceNodeContextCommand);

        //流来源类型;
        public const string StreamSourceType = "54F8A966-C881-4BBD-A2C6-F31B36EA8216";
        
        //硬盘流来源类型;
        public const string StreamSourceType_Hdd = "5CE1F69D-6490-412F-AA34-F33229454640";

        //分区流来源类型;
        public const string StreamSourceType_Drive = "EF4589A0-773B-4D42-9120-7E19F5AB7235";
    }

    

    ////文件类别GUID;
    //public static partial class Constants {
    //    //普通文件类别;
    //    public const string FileType_RegFile = nameof(FileType_RegFile);
        
    //    //文件夹类别;
    //    public const string FileType_Directory = nameof(FileType_Directory);
        
    //    //分区类别;
    //    public const string FileType_Partition = nameof(FileType_Partition);
        
    //    //设备类别;
    //    public const string FileType_Device = nameof(FileType_Device);
    //}
}
