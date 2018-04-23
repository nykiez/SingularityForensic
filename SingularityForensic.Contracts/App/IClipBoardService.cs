using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.Contracts.App {
    /// <summary>
    /// 剪切板服务;
    /// </summary>
    public interface IClipBoardService {
        void Clear();
        string GetText();
        void SetText(string text);
        void SetDataObject(object data);
        object GetDataObject(Type format);
        //public static void Clear();
        //public static bool ContainsAudio();
        //public static bool ContainsData(string format);
        //public static bool ContainsFileDropList();
        //public static bool ContainsImage();
        //public static bool ContainsText();
        //public static bool ContainsText(TextDataFormat format);
        //public static void Flush();
        //public static Stream GetAudioStream();
        //public static object GetData(string format);
        //public static IDataObject GetDataObject();
        //public static StringCollection GetFileDropList();
        //public static BitmapSource GetImage();
        //public static string GetText();
        //public static string GetText(TextDataFormat format);
        //public static bool IsCurrent(IDataObject data);
        //public static void SetAudio(byte[] audioBytes);
        //public static void SetAudio(Stream audioStream);
        //public static void SetData(string format, object data);
        //public static void SetDataObject(object data);
        //public static void SetDataObject(object data, bool copy);
        //public static void SetFileDropList(StringCollection fileDropLis
        //public static void SetImage(BitmapSource image);
        //public static void SetText(string text);
        //public static void SetText(string text, TextDataFormat format);
    }

    public class ClipBoardService:GenericServiceStaticInstance<IClipBoardService> {
        public static string GetText() => Current?.GetText();
        public static void SetText(string text) => Current?.SetText(text);
        public static void SetDataObject(object data) => Current?.SetDataObject(data);
        public static object GetDataObject(Type format) => Current?.GetDataObject(format);
        public static void Clear() => Current?.Clear();
    }
}
