using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Casing {
    /// <summary>
    /// 最近案件管理功能;
    /// </summary>
    public interface IRecentCaseRecordManagementService {
        /// <summary>
        /// 获取最近案件;
        /// </summary>
        /// <returns></returns>
        IEnumerable<IRecentCaseRecord> GetAllRecentRecords();

        /// <summary>
        /// 追加案件到最近打开的案件列表中;
        /// </summary>
        /// <param name="cs"></param>
        void AddCase(ICase cs);

        /// <summary>
        /// 移除最近案件;
        /// </summary>
        /// <param name="recentCaseRecord"></param>
        void RemoveRecord(IRecentCaseRecord recentCaseRecord);

        /// <summary>
        /// 清除最近案件;
        /// </summary>
        void ClearRecords();
    }


    public class RecentCaseRecordManagementService : GenericServiceStaticInstance<IRecentCaseRecordManagementService> {
        public static void AddCase(ICase cs) => Current.AddCase(cs);
        public static IEnumerable<IRecentCaseRecord> GetAllRecentRecords() => Current.GetAllRecentRecords();
        public static void RemoveRecord(IRecentCaseRecord recentCaseRecord) => Current.RemoveRecord(recentCaseRecord);
    }
}
