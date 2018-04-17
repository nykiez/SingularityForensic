using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FAT {
    /// <summary>
    /// FAT文件信息;
    /// </summary>
    class FATFileInfo {
        /// <summary>
        /// 文件节点;
        /// </summary>
        public StFatFileNode? StFileNode { get; set; }
        ///// <summary>
        ///// 簇链表;
        ///// </summary>
        //public List<StFatClusterNode> StClusters { get; } = new List<StFatClusterNode>();
    }
}
