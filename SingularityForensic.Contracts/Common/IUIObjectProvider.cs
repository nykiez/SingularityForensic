﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    /// <summary>
    /// UI元素提供者;
    /// </summary>
    public interface IUIObjectProvider {
        object UIObject { get; }    
    }
}
