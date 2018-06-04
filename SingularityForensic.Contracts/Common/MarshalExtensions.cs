using CDFC.Util.PInvoke;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    public static class MarshalExtensions {
        public static IEnumerable<TStruct> GetStructs<TStruct>(
           this IntPtr ptr, Func<TStruct, IntPtr> getNext)
           where TStruct : struct {
            while (ptr != IntPtr.Zero) {
                var tStruct = ptr.GetStructure<TStruct>();
                ptr = getNext(tStruct);
                yield return tStruct;
            }

        }
    }
}
