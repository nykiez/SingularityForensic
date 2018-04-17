using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FAT {
    public static partial class Constants {
        internal const string PartitionKey_FAT = "D2C19FB9-E98C-498C-B0F2-3AE4EA62BD00";

        internal const string DirectoryKey_FAT = "14A3C48B-6A23-4434-AD2F-492CD1BADE23";
        internal const string RegularFileKey_FAT = "8E74D7D4-7118-4745-9E78-0ABDCAEE8B8F";
    }

    public static partial class Constants {
        public const string StreamParser_FAT = nameof(StreamParser_FAT);

        /// <summary>
        /// FAT分区类型;
        /// </summary>
        public const string PartitionType_FAT32 = nameof(PartitionType_FAT32);

        public const string PartitionType_FAT16 = nameof(PartitionType_FAT16);

        public const string DirectoryType_FAT32 = nameof(DirectoryType_FAT32);

        public const string RegularFileType_FAT32 = nameof(RegularFileType_FAT32);
    }
}
