using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.App {
    /// <summary>
    /// 鼠标服务;
    /// </summary>
    public interface IMouseService {
        /// <summary>
        /// 应用程序当前的Cursor;
        /// </summary>
        Cursor AppCursor { get; set; }
    }

    public class MouseService : GenericServiceStaticInstance<IMouseService> {
        public static Cursor AppCusor {
            get => Current.AppCursor;
            set => Current.AppCursor = value;
        }
    }
    public enum Cursor {
        Normal,
        Loading
    }
}
