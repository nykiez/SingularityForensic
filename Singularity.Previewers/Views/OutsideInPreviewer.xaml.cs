using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;

namespace Singularity.UI.Previewers.Views {
    /// <summary>
    /// Interaction logic for OutsideInPreviewer.xaml
    /// </summary>
    public partial class OutsideInPreviewer : UserControl {
        /// <summary>
        /// DirectOutIn.dll将由该项目通过生成脚本自动生成;
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <param name="dwStyle"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <param name="IntPtrParent"></param>
        /// <param name="hMenu"></param>
        /// <param name="hInstance"></param>
        /// <param name="lpParam"></param>
        /// <returns></returns>
        [DllImport("Entities/DirectOutIn.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr MyCreateWindow(
                                    IntPtr lpClassName,
                                    IntPtr lpWindowName,
                                    uint dwStyle,
                                    int x,
                                    int y,
                                    int nWidth,
                                    int nHeight,
                                    IntPtr IntPtrParent,
                                    IntPtr hMenu,
                                    IntPtr hInstance,
                                    IntPtr lpParam);
        [DllImport("Entities/DirectOutIn.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private extern static void DoOpenFile(IntPtr mainIntPtr, IntPtr szFileName);
        [DllImport("Entities/DirectOutIn.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private extern static void DoCloseFile(IntPtr IntPtr, IntPtr viewerIntPtr);
        [DllImport("Entities/DirectOutIn.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private extern static void DoSize(IntPtr IntPtr, IntPtr viewHND, uint wWidth, uint wHeight);
        public OutsideInPreviewer() {
            InitializeComponent();
            IntPtr sccMainPtr = Marshal.StringToHGlobalAnsi("SCCVIEWER");
            IntPtr sccTiPtr = Marshal.StringToHGlobalAnsi("Me");
            uint style = WS_POPUP | WS_BORDER | WS_VISIBLE | WS_CLIPCHILDREN | WS_VSCROLL;
            try {
                viewerHandle = MyCreateWindow(
               sccMainPtr,                   /* window class    */
               IntPtr.Zero,                /* window name      */
                style,/* window type     */
                0,
                0,
                0,
                0,
                panel.Handle,
                IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero);                                /* additional info */
            }
            catch {
                throw;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            
        }

        IntPtr viewerHandle;
        private void panel_SizeChanged(object sender, EventArgs e) {
            DoSize(panel.Handle, viewerHandle, (uint)panel.Width, (uint)panel.Height);
        }
        public void OpenFile(string fileName) {
            try {
                if (File.Exists(fileName)) {
                    var szFileName = Marshal.StringToHGlobalAnsi(fileName);
                    DoOpenFile(viewerHandle, szFileName);
                    DoSize(panel.Handle, viewerHandle, (uint)panel.Width, (uint)panel.Height);
                }
            }
            catch {

            }
        }
        public void CloseFile() {
            DoCloseFile(panel.Handle, viewerHandle);
        }
    }
    public partial class OutsideInPreviewer {
        #region constants
    public const uint WS_OVERLAPPED = 0x00000000;
    public const uint WS_POPUP = 0x80000000;
    public const uint WS_CHILD = 0x40000000;
    public const uint WS_MINIMIZE = 0x20000000;
    public const uint WS_VISIBLE = 0x10000000;
    public const uint WS_DISABLED = 0x08000000;
    public const uint WS_CLIPSIBLINGS = 0x04000000;
    public const uint WS_CLIPCHILDREN = 0x02000000;
    public const uint WS_MAXIMIZE = 0x01000000;
    public const uint WS_CAPTION = 0x00C00000;    /* WS_BORDER | WS_DLGFRAME  */
    public const uint WS_BORDER = 0x00800000;
    public const uint WS_DLGFRAME = 0x00400000;
    public const uint WS_VSCROLL = 0x00200000;
    public const uint WS_HSCROLL = 0x00100000;
    public const uint WS_SYSMENU = 0x00080000;
    public const uint WS_THICKFRAME = 0x00040000;
    public const uint WS_GROUP = 0x00020000;
    public const uint WS_TABSTOP = 0x00010000;
    public const uint WS_MINIMIZEBOX = 0x00020000;
    public const uint WS_MAXIMIZEBOX = 0x00010000;
    public const uint WS_TILED = WS_OVERLAPPED;
    public const uint WS_ICONIC = WS_MINIMIZE;
    public const uint WS_SIZEBOX = WS_THICKFRAME;
    #endregion
    }
}
