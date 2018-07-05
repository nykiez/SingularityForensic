using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hash;
using SingularityForensic.Hash.ViewModels;
using System;
using System.Linq;

namespace SingularityForensic.Test.Hash.ViewModels {
    [TestClass()]
    public class HashSetManagementDialogViewModelTests {
        private const string testHashSetName = "TestHashSet";
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            HashSetManagementService.Initialize();
            HashSetManagementService.Current.ClearHashSets();

            var hashSet = HashSetFactory.CreateNew(
                "E:\\anli\\lucene", 
                Guid.NewGuid().ToString("P"),
                GenericServiceStaticInstances<IHasher>.Currents.First(
                    p => p.GUID == SingularityForensic.Hash.Constants.HashTypeGUID_MD5
                )
            );
            hashSet.Name = testHashSetName;
            HashSetManagementService.AddHashSet(hashSet);
            

            _vm = new HashSetManagementDialogViewModel();
            _vm.Initialize();
        }

        private HashSetManagementDialogViewModel _vm;
        [TestMethod()]
        public void HashSetManagementDialogViewModelTest() {
            Assert.AreEqual(_vm.HashSetModels.Count, 1);
            Assert.AreEqual(_vm.HashSetModels[0].HashSet.Hasher.GUID, SingularityForensic.Hash.Constants.HashTypeGUID_MD5);
        }

        [TestMethod]
        public void TestConfirmCommand() {
            HashSetManagementDialogViewModelTest();
            var newHashSetName = "newHashSetName";
            var newDescription = "description Text";
            var newEnabled = true;

            _vm.HashSetModels[0].HashSetName = newHashSetName;
            _vm.HashSetModels[0].HashSetDescription = newDescription;
            _vm.HashSetModels[0].HashSetEnabled = newEnabled;

            _vm.ConfirmCommand.Execute();

            //检查属性是否一致,确认更改生效;
            void CheckPropsMatched() {
                Assert.AreEqual(HashSetManagementService.HashSets.First().Name, newHashSetName);
                Assert.AreEqual(HashSetManagementService.HashSets.First().Description, newDescription);
                Assert.AreEqual(HashSetManagementService.HashSets.First().IsEnabled, newEnabled);
            };

            CheckPropsMatched();
            HashSetManagementService.Initialize();
            //确认更改在重新加载后仍然有效;
            CheckPropsMatched();
            
            
        }

        [TestMethod]
        public void TestCancelCommand() {
            HashSetManagementDialogViewModelTest();
            var newHashSetName = "newHashSetName";
            var newDescription = "description Text";
            var newEnabled = true;

            _vm.HashSetModels[0].HashSetName = newHashSetName;
            _vm.HashSetModels[0].HashSetDescription = newDescription;
            _vm.HashSetModels[0].HashSetEnabled = newEnabled;

            _vm.CancelCommand.Execute();

            //检查属性是否不一致,确认更改未生效;
            void CheckPropsUnMatched() {
                Assert.AreNotEqual(HashSetManagementService.HashSets.First().Name, newHashSetName);
                Assert.AreNotEqual(HashSetManagementService.HashSets.First().Description, newDescription);
                Assert.AreNotEqual(HashSetManagementService.HashSets.First().IsEnabled, newEnabled);
            };

            CheckPropsUnMatched();

            HashSetManagementService.Initialize();

            CheckPropsUnMatched();

            Assert.AreNotEqual(HashSetManagementService.HashSets.First().Name, newHashSetName);
        }
    }
}