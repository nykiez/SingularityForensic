namespace SingularityForensic.Contracts.FileSystem {
    public static class Constants {
        /// <summary>
        /// 文件案件类型GUID;
        /// </summary>
        public const string ImgCaseEvidence = nameof(ImgCaseEvidence);

        //源文件路径;
        public const string ImgPath = nameof(ImgPath);

        public const string DeviceNodeContextCommand = nameof(DeviceNodeContextCommand);

        //流来源类型;
        public const string StreamSourceCaseEvidenceType = nameof(StreamSourceCaseEvidenceType);

        //镜像流来源类型;
        public const string ImgStreamSourceType = nameof(StreamSourceCaseEvidenceType);

        //硬盘流来源类型;
        public const string HddStreamSourceType = nameof(HddStreamSourceType);

        //分区流来源类型;
        public const string DriveStreamSourceType = nameof(DriveStreamSourceType);
    }
}
