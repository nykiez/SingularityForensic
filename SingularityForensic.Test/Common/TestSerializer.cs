using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SingularityForensic.Test.Common {
    [Serializable]
    public class SEntityB : ISerializable {
        public int n1;
        public int N2 => n1;
        public String str;
        public SEntityB() {
            
        }
        protected SEntityB(SerializationInfo info, StreamingContext context) {
            //n1 = info.GetInt32("i");
            //n2 = info.GetInt32("j");
            //str = info.GetString("k");
        }
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context) {
            //info.AddValue("i", n1);
            //info.AddValue("j", n2);
            //info.AddValue("k", str);
            //throw new NotImplementedException();
        }
    }

    [Serializable]
    public class SEntity:SEntityB {
        public SEntity():base() {

        }
        public override void GetObjectData(SerializationInfo si,
StreamingContext context) {
            base.GetObjectData(si, context);
            
        }
        protected SEntity(SerializationInfo info, StreamingContext context):base(info,context) {
        }
    }

    [TestClass]
    public class TestSerializer {
        [TestMethod]
        public void TestSerilizable() {
            var ms = new MemoryStream();
            var en = new SEntity();
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, en);
            
            ms.Position = 0;
            var en2 = formatter.Deserialize(ms);
        }
        [TestMethod]
        public void TestReadLine() {
            var textFile = "E:\\anli\\File Type Categories郭永健 - 无安卓.txt";
            var sr = new StreamReader(textFile,Encoding.GetEncoding("GB2312"));
            sr.ReadLine();
            sr.ReadLine();
            var line = sr.ReadLine();
            
            sr.Dispose();
        }
    }
}
