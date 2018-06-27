using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.FileSystem {
    [TestClass()]
    public class FileExtensionsTests {

        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            FileSystemService.Current.Initialize();
        }

        
    }
}