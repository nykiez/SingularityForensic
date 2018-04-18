using CDFC.Util;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Hash;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Hash {
    [Export(typeof(IHasher))]
    public class SHA256Hasher : HasherBase {
        public SHA256Hasher() : base(SHA256HashAlgorithmProvider.StaticInstance) {

        }
        public override string HashTypeName => LanguageService.FindResourceString(Constants.HashTypeName_SHA256);

        public override string GUID => Constants.HashTypeGUID_SHA256;

        public override int Sort => 6;

        class SHA256HashAlgorithmProvider : GenericStaticInstance<SHA256HashAlgorithmProvider>, IHashAlgorithmProvider {
            public HashAlgorithm CreateNew() => new SHA256CryptoServiceProvider();
        }
    }
}
