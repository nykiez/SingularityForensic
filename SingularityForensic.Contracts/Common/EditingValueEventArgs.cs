using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {

    public class EditingValueEventArgs<T> : EventArgs {
        public T Value { get; set; }
    }

}
