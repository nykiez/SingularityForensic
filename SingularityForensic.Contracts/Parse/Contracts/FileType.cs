namespace SingularityForensic.Contracts.Parse.Contracts {
    //文件类型;
    public enum FileType {
        Unknown,                //未知文件;
        RegularFile,            //常规文件;
        Directory,              //目录;
        CharacterDeviceFile,    //挂载设备文件;
        BlockDeviceFile,        //块设备文件;
        FIFO,                   //
        Socket,                 //套接字;
        SymbolicLink,            //快捷方式;
        //Partition               //分区;
    }
}
