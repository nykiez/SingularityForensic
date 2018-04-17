﻿namespace SingularityForensic.Casing {
    /// <summary>
    /// 树形相关;
    /// </summary>
    public static partial class Constants {
        //案件节点类型;
        public const string CaseEvidenceUnitType = nameof(CaseEvidenceUnitType);
    }

    /// <summary>
    /// 语言相关;
    /// </summary>
    public static partial class Constants {
        //打开案件文件路径;
        public const string OpenCasePathFolder = nameof(OpenCasePathFolder);

        //显示案件属性;
        public const string ShowCaseProperty = nameof(ShowCaseProperty);

        //案件模块正在被加载;
        public const string CaseModuleBeingLoaded = nameof(CaseModuleBeingLoaded);

        //案件创建失败;
        public const string CaseConstructingFailed = nameof(CaseConstructingFailed);

        //支持的案件文件后缀类型;
        public const string SupportedCaseFileType = nameof(SupportedCaseFileType);

        //确认是否关闭案件;
        public const string ConfirmToCloseAndOpen = nameof(ConfirmToCloseAndOpen);

        //打开案件失败;
        public const string FailedToOpenCase = nameof(FailedToOpenCase);

        //打开案件;
        public const string LoadingCase = nameof(LoadingCase);

        //预设案件文件名称;
        public const string DefaultCaseName = nameof(DefaultCaseName);

        //案件文件属性;
        public const string CaseEvidenceProperty = nameof(CaseEvidenceProperty);

        //案件文件物理后缀;
        public const string CaseFileExtention = ".sfproj";
    }
}
