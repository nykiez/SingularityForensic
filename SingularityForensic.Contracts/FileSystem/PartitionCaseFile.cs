using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using System;
using System.Xml.Linq;

namespace SingularityForensic.Contracts.FileSystem {
    public interface IPartitionCaseFile {

    }

    //分区案件文件;
    //public class PartitionCaseFile : CaseEvidence {
    //    public const string PartitionFolderClass = nameof(CDFC.Parse.Abstracts.Partition) + "s";

    //    //分区案件文件;
    //    public PartitionCaseFile(Partition part,
    //        string interLabel,
    //        DateTime dtAdded, int partID) : base(nameof(CDFC.Parse.Abstracts.Partition), part.Name, interLabel, dtAdded) {
    //        this.PartitionID = partID;
    //        this.Partition = part;
    //    }

    //    public PartitionCaseFile(Partition part, XElement xElem) : base(xElem) {
    //        this.Partition = part;
    //    }

    //    protected override string GetBasePath() => $"{PartitionFolderClass}/{Guid.NewGuid().ToString("N")}-{Name}";

    //    ///内部分区ID;
    //    public int PartitionID {
    //        get => int.TryParse(GetXElemValue(), out var partId) ? partId : 0;
    //        set => SetXElemValue(value.ToString());
    //    }

    //    public Partition Partition { get; }
    //}
}
