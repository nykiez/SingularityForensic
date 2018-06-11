using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.Common {
    [Serializable]
    public class SEntity : ISerializable {
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            
            //throw new NotImplementedException();
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
    }
}
