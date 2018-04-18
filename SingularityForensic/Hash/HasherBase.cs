using CDFC.Util.IO;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Hash;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SingularityForensic.Hash {
    public abstract class HasherBase : IHasher {
        public HasherBase(IHashAlgorithmProvider algorithmProvider) {
            this._algorithmProvider = algorithmProvider??throw new ArgumentNullException(nameof(algorithmProvider));
        }

        IHashAlgorithmProvider _algorithmProvider;
        public abstract string HashTypeName { get; }

        public abstract string GUID { get; }

        public abstract int Sort { get; }

        public virtual byte[] ComputeHash(Stream inputStream, IProgressReporter reporter) {
            if(inputStream == null) {
                throw new ArgumentNullException(nameof(inputStream));
            }

            var opStream = new OperatebleStream(inputStream);
            opStream.Position = 0;

            byte[] bts = null;

            if (reporter != null) {
                //订阅取消事件;
                reporter.Canceld += (sender, e) => {
                    opStream.Break();
                };
                //订阅流位置变更事件,通知进度;
                opStream.PositionChanged += (sender, e) => {
                    reporter.ReportProgress((int)(e * 100 / opStream.Length));
                };
            }
            
            //创建哈希算法;
            var algorithm = _algorithmProvider.CreateNew();
            try {
                bts = algorithm.ComputeHash(opStream);
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
            finally {
                //释放哈希算法;
                algorithm.Dispose();
            }

            //如若被取消,则返回空;
            if (opStream.Broken) {
                return null;
            }
            else {
                return bts;
            }

        }
    }
}
