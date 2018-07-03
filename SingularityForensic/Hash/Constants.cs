namespace SingularityForensic.Hash {
    public static partial class Constants {
        public const string HashTypeGUID_MD5 = nameof(HashTypeGUID_MD5);
        public const string HashTypeGUID_SHA1 = nameof(HashTypeGUID_SHA1);
        public const string HashTypeGUID_SHA256 = nameof(HashTypeGUID_SHA256);
        public const string HashTypeGUID_SHA512 = nameof(HashTypeGUID_SHA512);

        /// <summary>
        /// 哈希集配置文件相关的元素名称;
        /// </summary>
        public const string HashSetManagement_ConfigFile = "HashSets.xml";

        public const string XmlElemName_HashSets_Root = "HashSets";

        public const string XmlElemName_HashSets_Set = "Set";
        
        public const string XmlAttrName_HashSets_Set_Name = "Name";
        
        public const string XmlAttrName_HashSets_Set_GUID = "GUID";
        
        public const string XmlAttrName_HashSets_Set_StoragePath = "StoragePath";

        public const string XmlAttrName_HashSets_Set_HashTypeGUID = "HashTypeGUID";

        public const string XmlElemName_HashSets_Set_Description = "Description";
    }

    /// <summary>
    /// 语言部分;
    /// </summary>
    public static partial class Constants {
        public const string HashTypeName_MD5 = nameof(HashTypeName_MD5);
        public const string HashTypeName_SHA1 = nameof(HashTypeName_SHA1);
        public const string HashTypeName_SHA256 = nameof(HashTypeName_SHA256);
        public const string HashTypeName_SHA512 = nameof(HashTypeName_SHA512);
    }
}
