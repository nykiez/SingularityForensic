namespace Singularity.Interfaces {
    /// <summary>
    /// 可拓展类型契约;
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public interface IHaveData<out TData> {
        //数据上下文;
        TData Data { get; }
    }
}
