using CDFCUIContracts.Models;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Info {
    //取证服务,具备提取,保存,加载等功能;(针对证据项类型,不唯一);
    public interface IForensicInfoServiceProvider{
        /// <summary>
        /// 开始解析并保存状态;
        /// </summary>
        /// <param name="csEvidence">针对的案件文件</param>
        /// <param name="itemsGuids">取证选择项成员GUID</param>
        /// <param name="extractingHandler">取证中进度回调(需引用System.ValueTuple,下同)</param>
        /// <param name="errHandler">错误回调,返回是否重试;</param>
        /// <param name="isCanceld">是否取消</param>
        void StartForensic(ICaseEvidence csEvidence,IEnumerable<string> itemsGuids,
            Action<(string guid,int percentage,string word)> extractingHandler = null,
            Func<bool> isCanceld = null,
            Func<(string errCode,string errWord, bool needRetry),bool> errHandler = null);

        /// <summary>
        /// 获得取证信息结果节点;
        /// </summary>
        IEnumerable<ForensicTreeUnit> GetInfoesUnit(ICaseEvidence csEvidence);

        //加载案件信息,针对关闭案件后,重新进行加载;
        void Load(ICaseEvidence caseEvidence);

        //清除所有内存中的信息,关闭案件时将会使用;
        void Uninstall();
    }


    ///// <summary>
    ///// 以下为针对安卓镜像示例;
    ///// </summary>
    //[Export(SingularityForensic.Contracts.Casing.Constants.AndroidDeviceImg, typeof(IForensicInfoServiceProvider))]
    //public class AdImgInfoForensicInfoServiceProviderExample : IForensicInfoServiceProvider {
    //    public IEnumerable<ITreeUnit> GetInfoesUnit(CaseEvidence csEvidence) => throw new NotImplementedException();

    //    public void Load(CaseEvidence caseEvidence) => throw new NotImplementedException();

    //    public void StartForensic(CaseEvidence csEvidence, IEnumerable<string> itemsGuids,
    //        Action<(string guid, int percentage, string word)> handler = null,
    //        Func<bool> isCanceld = null,
    //        Func<(string errCode, string errWord,bool needRetry), bool> errHandler = null) {
    //        if(itemsGuids == null) {
    //            throw new ArgumentNullException(nameof(itemsGuids));
    //        }

            

    //        foreach (var guid in itemsGuids) {
    //            for (int i = 0; i < 20; i++) {
    //                handler?.Invoke((guid, i * 5, $"正在提取:{guid}"));   
    //                Thread.Sleep(200);
    //            }
    //            if (isCanceld?.Invoke() == true) {
    //                break;
    //            }
    //        }

    //        //保存结果动作,在使用持久化时需使用案件服务;
    //        var csService = ServiceProvider.Current.GetInstance<ICaseService>();
    //        if (csEvidence != null) {
    //            File.WriteAllText($"{csService.CurrentCase.Path}/{csEvidence.EvidenceGUID}/res.bin", $"这是{DateTime.Now}保存的案件结果");
    //        }
    //    }

    //    public void Uninstall() => throw new NotImplementedException();

    //    IEnumerable<ForensicTreeUnit> IForensicInfoServiceProvider.GetInfoesUnit(CaseEvidence csEvidence) {
    //        throw new NotImplementedException();
    //    }
    //}
}
