using CDFCUIContracts.Models;
using Singularity.Contracts.Common;

namespace Singularity.Contracts.TreeView {
    /// <summary>
    /// 通过字符串作为唯一类型标识的节点类型;
    /// </summary>
    public class PinTreeUnit : TreeUnit, IHavePinKind {
        public PinTreeUnit(string pinKind, ITreeUnit parent) : base(parent) {
            this.ContentId = pinKind;
        }
        //节点类型标识;
        public string ContentId { get; }
        public const string PinSpliter = "\\";
    }



}
