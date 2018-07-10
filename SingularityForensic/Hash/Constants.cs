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

        public const string XmlAttrName_HashSets_Set_IsEnabled = "IsEnabled";

        //默认的哈希存储路径(作用域为程序全局);
        public const string HashDefaultStorageFolder = "HashSets";

        //哈希状态存储路径(作用域为案件内);
        public const string HashValueStatusStorageName = "HashValueStatus.txt";

        //哈希状态文件属性分割符;
        public const char HashValueStatusFormat_Spliter = ' ';

        //哈希状态文件属性包含符号;(")
        public const char HashValueStatusFormat_Container = '"';


        public const string HashSetStatusStorageName = "HashSetStatus.txt";

    }

    /// <summary>
    /// 语言部分;
    /// </summary>
    public static partial class Constants {
        public const string HashTypeName_MD5 = nameof(HashTypeName_MD5);
        public const string HashTypeName_SHA1 = nameof(HashTypeName_SHA1);
        public const string HashTypeName_SHA256 = nameof(HashTypeName_SHA256);
        public const string HashTypeName_SHA512 = nameof(HashTypeName_SHA512);


        public const string WindowTitle_HashSetManagement = nameof(WindowTitle_HashSetManagement);
        
        public const string HashSetProp_Name = nameof(HashSetProp_Name);
        public const string HashSetProp_Desciption = nameof(HashSetProp_Desciption);
        public const string HashSetProp_HashType = nameof(HashSetProp_HashType);
        public const string HashSetProp_IsEnabled = nameof(HashSetProp_IsEnabled);
        public const string HashSetProp_StoragePath = nameof(HashSetProp_StoragePath);

        public const string MsgText_HashSetNameCannotBeEmpty = nameof(MsgText_HashSetNameCannotBeEmpty);

        public const string MsgText_HashSetStoragePathCannotBeCreated = nameof(MsgText_HashSetStoragePathCannotBeCreated);


        public const string MsgText_Index = nameof(MsgText_Index);


        public const string MsgText_InvalidPathChar = nameof(MsgText_InvalidPathChar);


        public const string MsgText_StoragePathAlreadyOccupied = nameof(MsgText_StoragePathAlreadyOccupied);


        public const string HashModuleLoading = nameof(HashModuleLoading);


        public const string WindowTitle_CreateHashSet = nameof(WindowTitle_CreateHashSet);


        public const string BtnWord_CreateHashSet = nameof(BtnWord_CreateHashSet);


        public const string MenuItemText_HashSetManagement = nameof(MenuItemText_HashSetManagement);


        public const string ContextCommandName_DeleteHashSet = nameof(ContextCommandName_DeleteHashSet);


        public const string MsgText_NoHashSetModelBeenSelected = nameof(MsgText_NoHashSetModelBeenSelected);


        public const string ContextCommandName_ImportHash = nameof(ContextCommandName_ImportHash);

        public const string ContextCommandName_ListHashValue = nameof(ContextCommandName_ListHashValue);

        public const string WindowTitle_ImportingHash = nameof(WindowTitle_ImportingHash);


        public const string MsgText_ImportingHashFormat = nameof(MsgText_ImportingHashFormat);


        public const string MsgText_HashImportedFormat = nameof(MsgText_HashImportedFormat);


        public const string WindowTitle_SelectHashSet = nameof(WindowTitle_SelectHashSet);


        public const string Description_SelectHashSet = nameof(Description_SelectHashSet);
        
        public const string WindowTitle_ListHashValueFormat = nameof(WindowTitle_ListHashValueFormat);
        
        public const string HashPairProp_Name = nameof(HashPairProp_Name);
        
        public const string HashPairProp_Value = nameof(HashPairProp_Value);


        public const string WindowTitle_LoadingHashPairs = nameof(WindowTitle_LoadingHashPairs);


    }
}
