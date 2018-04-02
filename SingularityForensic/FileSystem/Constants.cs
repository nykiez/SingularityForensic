namespace SingularityForensic.FileSystem {
    /// <summary>
    /// 模块内部使用的"密码";
    /// </summary>
    public static partial class Constants {
        internal const string DeviceKey_Unknown = "66D12281-5969-4117-B75C-10E56BCBC6F8";
        internal const string DeviceKey_DOS = "2739F2F4-B051-4520-8BF0-7A1511765673";
        internal const string DeviceKey_GPT = "CC4E4F36-0C69-4FD3-919D-BC932AEA0F47";

        internal const string PartEntryKey_Dos = "B96EDB68-2F5C-4711-B7C0-2187C69D9C27";
        internal const string PartEntryKey_GPT = "0EB0D4E8-0313-4849-9803-1E327411525B";

        internal const string PartitionKey_FAT = "D2C19FB9-E98C-498C-B0F2-3AE4EA62BD00";

        internal const string DirectoryKey_FAT = "14A3C48B-6A23-4434-AD2F-492CD1BADE23";
        internal const string RegularFileKey_FAT = "8E74D7D4-7118-4745-9E78-0ABDCAEE8B8F";
    }

    /// <summary>
    /// 语言部分;
    /// </summary>
    public static partial class Constants {
        public const string StreamParser_BaseDevice = nameof(StreamParser_BaseDevice);
        public const string StreamParser_Unknown = nameof(StreamParser_Unknown);
        public const string StreamParser_FAT = nameof(StreamParser_FAT);
        public const string StreamParser_NTFS = nameof(StreamParser_NTFS);

        public const string PartsType_Unknown = nameof(PartsType_Unknown);
        public const string PartsType_DOS = nameof(PartsType_DOS);
        public const string PartsType_GPT = nameof(PartsType_GPT);

        public const string DeviceType_Unknown = nameof(DeviceType_Unknown);
        public const string DeviceType_DOS = nameof(DeviceType_DOS);
        public const string DeviceType_GPT = nameof(DeviceType_GPT);

        /// <summary>
        /// Dos拓展分区表项;
        /// </summary>
        public const string PartEntryType_Dos_Extended = nameof(PartEntryType_Dos_Extended);
        /// <summary>
        /// Dos主分区表项;
        /// </summary>
        public const string PartEntryType_Dos_Main = nameof(PartEntryType_Dos_Main);
        /// <summary>
        /// Dos逻辑分区表项;
        /// </summary>
        public const string PartEntryType_Dos_Logic = nameof(PartEntryType_Dos_Logic);
        /// <summary>
        /// Dos逻辑分区表项;
        /// </summary>
        public const string PartEntryType_Dos_Error = nameof(PartEntryType_Dos_Error);

        /// <summary>
        /// GPT分区表项;
        /// </summary>
        public const string PartEntryType_GPT = nameof(PartEntryType_GPT);

        /// <summary>
        /// FAT分区类型;
        /// </summary>
        public const string PartitionType_FAT32 = nameof(PartitionType_FAT32);

        public const string PartitionType_FAT16 = nameof(PartitionType_FAT16);

        public const string DirectoryType_FAT32 = nameof(DirectoryType_FAT32);

        public const string RegularFileType_FAT32 = nameof(RegularFileType_FAT32);
    }
}
