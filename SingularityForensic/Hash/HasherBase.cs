using CDFC.Util.IO;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hash;
using System;
using System.IO;

namespace SingularityForensic.Hash {
    public abstract class HasherBase : IHasher {
        public HasherBase(IHashAlgorithmProvider algorithmProvider) {
            this._algorithmProvider = algorithmProvider??throw new ArgumentNullException(nameof(algorithmProvider));
        }

        IHashAlgorithmProvider _algorithmProvider;
        public abstract string HashTypeName { get; }

        public abstract string GUID { get; }

        public abstract int Sort { get; }

        public virtual byte[] ComputeHash(Stream inputStream) {
            if(inputStream == null) {
                throw new ArgumentNullException(nameof(inputStream));
            }

            

            byte[] bts = null;

           
            //创建哈希算法;
            
            var algorithm = _algorithmProvider.CreateNew();
            try {
                bts = algorithm.ComputeHash(inputStream);
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
            finally {
                //释放哈希算法;
                algorithm.Dispose();
            }

            return bts;
        }
    }
}
