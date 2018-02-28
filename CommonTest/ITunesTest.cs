using Microsoft.VisualStudio.TestTools.UnitTesting;
using CDFC.Parse.Modules.Structs;
using CDFC.Parse.Modules;
using System.IO;
using CDFC.Parse.ITunes;

namespace ITunes.Test {
    [TestClass]
    public class ITunesTest {    
        [TestMethod]
        public void TestParse() {
            //I://IOSB10//2f3ab00fb6eaa3ea5f9f881fef46607a210b4730
            //I://iosb
            string s = "I://IOSB10//2f3ab00fb6eaa3ea5f9f881fef46607a210b4730";
            var parser = new IOSBackUpParser(s);
            //var files = parser.DoParse();
            var bsInfo = parser.GetBasicInfo();
        }
        [TestMethod]
        public void GetBackFields() {
            using (var sw = new StreamWriter("D://1.txt")) {
                foreach (var item in typeof(IOSBasicStruct).GetFields()) {
                    sw.WriteLine($"<sys:String x:Key=\"{item.Name}\"></sys:String>");
                }
            }
                
        }
    }
}
