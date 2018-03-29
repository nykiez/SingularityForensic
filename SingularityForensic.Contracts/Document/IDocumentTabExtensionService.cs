﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Document {
    /// <summary>
    /// Document拓展服务;
    /// </summary>
    public interface IDocumentTabExtensionService {
        /// <summary>
        /// 创建一个多级的Tab;
        /// </summary>
        /// <returns></returns>
        IEnumerableDocumentTab CreateEnumerableTab();
    }

    
}