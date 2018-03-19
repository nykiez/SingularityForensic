using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SingularityForensic.Contracts.Imaging {
    //镜像挂载单位契约;
    public interface IImgMounter : IDisposable {
        //原始流;
        Stream RawStream { get; }

        //原镜像路径;
        string ImgPath { get; }
    }

    /// <summary>
    /// 镜像流器挂载器提供者;
    /// </summary>
    public interface IImgMounterProvider {
        /// <summary>
        /// 检查是否能够解析;
        /// </summary>
        /// <param name="imgPath">镜像路径</param>
        /// <param name="fileAccess">打开方式</param>
        /// <param name="fileShare">分享模式</param>
        /// <returns></returns>
        bool CheckIsValidImg(string imgPath);

        /// <summary>
        /// 创建一个挂载器单位;
        /// </summary>
        /// <param name="imgPath">镜像路径</param>
        /// <param name="xElem">为防止跟案件耦合,提供充分的自由度,将案件文件的xElem传到此进行操作</param>
        /// <returns></returns>
        IImgMounter CreateMounter(string imgPath,XElement xElem, FileAccess fileAccess, FileShare fileShare);

        //排序;
        int Sort { get; }

        //格式名;
        string FormatName { get; }

        
    }
}
