using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileSystem {
    public class SymbolLinkStoken : FileStokenBase2 {

    }
    /// <summary>
    /// 快捷方式;
    /// </summary>
    public class SymbolLink : FileBase<SymbolLinkStoken>,ISymbolLink {
        public SymbolLink(string key):base(key) {
            
        }

        
        
        
    }
}
