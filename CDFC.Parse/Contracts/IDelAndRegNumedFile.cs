namespace CDFC.Parse.Contracts {
    //具有记录删除/线现存特性的功能文件;
    public interface IDelAndRegNumedFile {
        //删除文件数;(仅限下一级)
        long RegFileNum { get;}

        //正常文件数;(仅限下一级)
        long DelFileNum { get;}

        //正常目录数;(仅限下一级)
        long RegDirNum { get; }

        //删除目录数;(仅限下一级)
        long DelDirNum { get;}

        long TotalRegFileNum { get; }
    }
}
