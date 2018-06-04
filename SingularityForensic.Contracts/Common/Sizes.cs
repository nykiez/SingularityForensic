using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    public static class Sizes {
        public const long OneKiB = 1024;
        public const long OneMiB = 1024 * OneKiB;
        public const long OneGiB = 1024 * OneMiB;

        public const int Sector = 512;
    }
}
