using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Imaging {
    /// <summary>
    /// 镜像服务;
    /// </summary>
    public interface IImagingService {
        //初始化;
        void Initialize();

        //添加镜像(到案件上);
        void AddImg(string path);

        //添加镜像;
        void AddImg();

        //案件挂载的组合,mounter为挂载器,csEvidence为案件单元;
        IEnumerable<(IImgMounter mounter, ICaseEvidence csEvidence)> MounterTuples { get; }
    }

    public class ImgService: GenericServiceStaticInstance<IImagingService> {

    }
}
