using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.App {
    /// <summary>
    /// 线程调用器契约,此契约能够帮助在逻辑中存在进行多线程后台操作时,替换后可以方便地进行单元测试;
    /// </summary>
    public interface IThreadInvoker {
        //后台调用;
        void BackInvoke(Action act);
        //UI调用;
        void UIInvoke(Action act);
    }

    public class ThreadInvoker : GenericServiceStaticInstance<IThreadInvoker> {
        //后台调用;
        public static void BackInvoke(Action act) {
            if(act == null) {
                throw new ArgumentNullException(nameof(act));
            }

            Current.BackInvoke(act);
        }

        //UI调用;
        public static void UIInvoke(Action act) {
            if (act == null) {
                throw new ArgumentNullException(nameof(act));
            }

            Current.UIInvoke(act);
        }
    }
}
