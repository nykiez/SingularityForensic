namespace SingularityForensic.NTFS {
    /// <summary>
    /// Class representing the $MFT file on disk, including mirror.
    /// </summary>
    /// <remarks>This class only understands basic record structure, and is
    /// ignorant of files that span multiple records.  This class should only
    /// be used by the NtfsFileSystem and File classes.</remarks>
    public partial struct StMFT  {
        
    }

    public partial struct StMFT {
        /// <summary>
            /// MFT index of the MFT file itself.
            /// </summary>
        public const long MftIndex = 0;

        /// <summary>
        /// MFT index of the MFT Mirror file.
        /// </summary>
        public const long MftMirrorIndex = 1;

        /// <summary>
        /// MFT Index of the Log file.
        /// </summary>
        public const long LogFileIndex = 2;

        /// <summary>
        /// MFT Index of the Volume file.
        /// </summary>
        public const long VolumeIndex = 3;

        /// <summary>
        /// MFT Index of the Attribute Definition file.
        /// </summary>
        public const long AttrDefIndex = 4;

        /// <summary>
        /// MFT Index of the Root Directory.
        /// </summary>
        public const long RootDirIndex = 5;

        /// <summary>
        /// MFT Index of the Bitmap file.
        /// </summary>
        public const long BitmapIndex = 6;

        /// <summary>
        /// MFT Index of the Boot sector(s).
        /// </summary>
        public const long BootIndex = 7;

        /// <summary>
        /// MFT Index of the Bad Bluster file.
        /// </summary>
        public const long BadClusIndex = 8;

        /// <summary>
        /// MFT Index of the Security Descriptor file.
        /// </summary>
        public const long SecureIndex = 9;

        /// <summary>
        /// MFT Index of the Uppercase mapping file.
        /// </summary>
        public const long UpCaseIndex = 10;

        /// <summary>
        /// MFT Index of the Optional Extensions directory.
        /// </summary>
        public const long ExtendIndex = 11;

        /// <summary>
        /// First MFT Index available for 'normal' files.
        /// </summary>
        private const uint FirstAvailableMftIndex = 24;
    }
}
