using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    public interface IHaveStoken<out TStoken> where TStoken : ExtensibleObject, new() {
        TStoken GetStoken(string key);
    }

    public abstract class HaveStokenBase<TStoken> where TStoken : ExtensibleObject, new() {
        public HaveStokenBase(string key, TStoken stoken = null) {
            this._key = key;
            _stoken = stoken ?? new TStoken();
        }
        private string _key;
        protected TStoken _stoken;
        public TStoken GetStoken(string key) {
            if (key != _key) {
                throw new AuthenticationException($"{nameof(key)} is not matched.");
            }

            return _stoken;
        }
        
    }

    
}
