using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.Contracts.App {
    public interface IMessageBoxService {
        void ShowError(string error);
        MessageBoxResult Show(string msg);
        MessageBoxResult Show(string msgText, MessageBoxButton button);
        MessageBoxResult Show(string msgText, string caption, MessageBoxButton button);
    }

    /// <summary>
	/// Contains an <see cref="IMessageBoxService"/> instance
	/// </summary>
	public class MsgBoxService : GenericServiceStaticInstance<IMessageBoxService> {
        public static void ShowError(string error) => Current?.ShowError(error);

        public static void Show(string msg) => Current?.Show(msg);
    }
}
