using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    [AttributeUsage(AttributeTargets.Property)]
    public class PropDescribleAttribute :Attribute{
        public PropDescribleAttribute(int propOffset) {
            this.PropOffset = propOffset;
        }
        /// <summary>
        /// 属性长度;
        /// </summary>
        public int PropLength { get; set; }
        /// <summary>
        /// 属性偏移;
        /// </summary>
        public int PropOffset { get; }
    }
}
