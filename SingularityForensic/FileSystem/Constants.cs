namespace SingularityForensic.FileSystem {

    public static partial class Constants {
        public const string DeviceKey_Unknown = "66D12281-5969-4117-B75C-10E56BCBC6F8";
        public const string DeviceKey_DOS = "2739F2F4-B051-4520-8BF0-7A1511765673";
        public const string DeviceKey_GPT = "CC4E4F36-0C69-4FD3-919D-BC932AEA0F47";

        public const string PartEntryKey_Dos = "B96EDB68 - 2F5C-4711-B7C0-2187C69D9C27";
        public const string PartEntryKey_GPT = "0EB0D4E8-0313-4849-9803-1E327411525B";

    }

    //语言部分;
    public static partial class Constants {
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
    }
}
