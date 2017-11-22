using System;

namespace Cflab.DataTransport.Modules.Transport.Model
{
	[Serializable]
	public class Package : IInfo
    {
        /// <summary>
        /// 软件名称
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 包名
        /// </summary>
        public string PackageName { get; set; }

        /// <summary>
        /// 用户版本
        /// </summary>
        public string VersionName { get; set; }

        /// <summary>
        /// 构建版本
        /// </summary>
        public int VersionCode { get; set; }

        /// <summary>
        /// 安装包位置
        /// </summary>
        public string SourcePath { get; set; }

	    /// <summary>
	    /// 沙箱位置
	    /// </summary>
	    public string DataPath { get; set; }

		/// <summary>
		/// 申请的权限
		/// </summary>
		public string[] Permissions { get; set; }

        /// <summary>
        /// 安装包大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 是否为系统应用
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public long LastModif { get; set; }
    }
}
