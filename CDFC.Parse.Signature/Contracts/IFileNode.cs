namespace CDFC.Parse.Signature.Contracts {
    public interface IFileNode {
        long StartLBA { get; }
        long EndLBA { get; }
        long FileSize { get; }
        string Name { get; }
        string Data { get; }
        string Type { get; }
    }
}
