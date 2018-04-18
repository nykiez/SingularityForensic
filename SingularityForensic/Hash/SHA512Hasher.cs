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
    public class SHA512Hasher : HasherBase {
        public SHA512Hasher() : base(SHA512HashAlgorithmProvider.StaticInstance) {

        }
        public override string HashTypeName => LanguageService.FindResourceString(Constants.HashTypeName_SHA512);

        public override string GUID => Constants.HashTypeGUID_SHA512;

        public override int Sort => 8;

        class SHA512HashAlgorithmProvider : GenericStaticInstance<SHA512HashAlgorithmProvider>, IHashAlgorithmProvider {
            public HashAlgorithm CreateNew() => new SHA512CryptoServiceProvider();
        }
    }
}
