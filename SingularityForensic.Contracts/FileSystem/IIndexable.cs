using System;
using System.Collections.Generic;

namespace SingularityForensic.Contracts.FileSystem {
    //可索引契约;
    public interface IIndexable {
        //建立索引;
        bool BuildIndexFiles(Action<BuildPeriod, long, long?> notify = null, Func<bool> isCanceld = null);
        //搜索索引;
        List<FileBase > IndexSearchKey(string[] content, string path = null);
        //是否建立了索引;
        bool HasIndexes { get; }
    }

    //生成索引过程;
    public enum BuildPeriod {
        BuildDoc,              //生成Xml中;
        BuildIndexes           //生成索引中;
    }
}
