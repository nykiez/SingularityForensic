using CDFC.Util;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Hash;
using System.ComponentModel.Composition;
using System.Security.Cryptography;

namespace SingularityForensic.Hash {
    [Export(typeof(IHasher))]
    public class SHA1Hasher : HasherBase {
        public SHA1Hasher() : base(SHA1HashAlgorithmProvider.StaticInstance) {

        }
        public override string HashTypeName => LanguageService.FindResourceString(Constants.HashTypeName_SHA1);

        public override string GUID => Constants.HashTypeGUID_SHA1;

        public override int Sort => 4;

        class SHA1HashAlgorithmProvider : GenericStaticInstance<SHA1HashAlgorithmProvider>, IHashAlgorithmProvider {
            public HashAlgorithm CreateNew() => new SHA1CryptoServiceProvider();
        }
    }

    
}
