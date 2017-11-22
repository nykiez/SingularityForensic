namespace CDFC.Parse.Contracts {
    /// <summary>
    /// 分区表类型;
    /// </summary>
    public enum PartsType {

        Unknown,                        //未知分区类型;
        MBR,                             //EFI分区类型;
        GPT                             //GPT分区类型;
    }
}
