using System;

namespace SingularityForensic.Contracts.Parse.Contracts {
    //有时间属性的契约;
    public interface ITimeable {
        DateTime? ModifiedTime { get; }             //最后修改时间;
        DateTime? AccessedTime { get; }             //最后访问时间;
        DateTime? CreateTime { get; }               //创建时间;
    }
}
