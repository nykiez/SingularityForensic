using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SingularityForensic.Test.Common {
    [TestClass()]
    public class GenericServiceStaticInstance2Tests {
        [TestMethod()]
        public void GenericServiceStaticInstance2Test() {
            var en1 = new EnGn("test1");
            var en2 = new EnGn2("test2");
            var en3 = new EnGn3("test3");
            Assert.AreNotEqual(en1.KeyName, en2.KeyName);
            Assert.AreNotEqual(en2.KeyName, en3.KeyName);
        }
    }

    public abstract class GenericServiceStaticInstance2<TService> where TService : class {
        public GenericServiceStaticInstance2(string keyName) {
            KeyName = keyName;
        }

        public string KeyName { get; private set; }
        //private static TService _current;
        //public static TService Current => _current ?? (_current = new TService());
    }

    public class EnGn:GenericServiceStaticInstance2<IEntity>{
        public EnGn(string keyName):base(keyName) {

        }
    }

    public class EnGn2 : GenericServiceStaticInstance2<IEntity2> {
        public EnGn2(string keyName) : base(keyName) {

        }
    }

    public class EnGn3 : GenericServiceStaticInstance2<IEntity> {
        public EnGn3(string keyName) : base(keyName) {

        }
    }

    public interface IEntity {

    }
    public interface IEntity2 {

    }
}