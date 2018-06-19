using SingularityForensic.Contracts.Common;
using System.ComponentModel;
using System.Reflection;

namespace SingularityForensic.Ext {
    class ExtPartInfo {
        /// <summary>
        /// 非托管状态;
        /// </summary>
        public ExtUnmanagedManager ExtUnmanagedManager { get; set; }

        /// <summary>
        /// 超级块;
        /// </summary>
        public SuperBlock SuperBlock { get; set; }

        /// <summary>
        /// 块组描述符;
        /// </summary>
        public ExtGroupDesc[] Ext4GroupDescs { get;set;}

        
    }

    public class SuperBlock: StructFieldDecriptorBase<StSuperBlock>,ICustomMemerDecriptor {
        public SuperBlock(StSuperBlock stSuperBlock):base(stSuperBlock) {

        }
    }
    public class ExtGroupDesc : StructFieldDecriptorBase<StExtGroupDesc>, ICustomMemerDecriptor {
        public ExtGroupDesc(StExtGroupDesc desc):base(desc) {

        }

        protected override void OnEditMemberDescriptorOverride(MemberInfo memberInfo, CancelEventArgs arg) {
            if(memberInfo.Name == nameof(StExtGroupDesc.Next) || memberInfo.Name == nameof(StExtGroupDesc.Pre)) {
                arg.Cancel = true;
            }
            base.OnEditMemberDescriptorOverride(memberInfo, arg);
        }
    }
}
