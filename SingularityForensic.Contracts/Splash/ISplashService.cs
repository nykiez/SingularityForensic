using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Splash {
    public interface ISplashService {
        void ReportMessage(string msg);
        void ShowSplash();
        void CloseSplash();
    }
}
