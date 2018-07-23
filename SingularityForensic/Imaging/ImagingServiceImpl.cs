using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Casing.Events;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Imaging;
using static SingularityForensic.Contracts.Imaging.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using SingularityForensic.Contracts.Shell;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;

namespace SingularityForensic.Imaging {
    [Export(typeof(IImagingService))]
    class ImagingServiceImpl : IImagingService {
        [ImportingConstructor]
        public ImagingServiceImpl([ImportMany]IEnumerable<IImgMounterProvider> mounterProviders) {
            this._mounterProviders = mounterProviders.OrderBy(p => p.Sort);
        }
        
        private readonly IEnumerable<IImgMounterProvider> _mounterProviders;
        
        public void Initialize() {
            RegisterEvents();
        }

        /// <summary>
        /// 注册事件;
        /// </summary>
        private void RegisterEvents() {

            CommonEventHelper.GetEvent<CaseEvidenceLoadingEvent>().Subscribe(OnCaseEvidenceLoading);


            CommonEventHelper.GetEvent<CaseEvidenceRemovedEvent>().Subscribe(OnCaseEvidenceRemovedOnImaging);


            CommonEventHelper.GetEvent<CaseUnloadedEvent>().Subscribe(OnCaseUnloadedOnImaging);
        }

        /// <summary>
        /// 加载案件文件若为镜像,则响应镜像解析;
        /// </summary>
        /// <param name="tuple"></param>
        private void OnCaseEvidenceLoading((ICaseEvidence csEvidence, IProgressReporter reporter) tuple) {
            var csEvidence = tuple.csEvidence;
            var reporter = tuple.reporter;

            if (csEvidence == null) {
                return;
            }

            if (!(csEvidence.EvidenceTypeGuids?.Contains(EvidenceType_Img) ?? false)) {
                return;
            }

            var path = csEvidence[ImgPath];
            if (string.IsNullOrEmpty(path)) {
                return;
            }
            
            try {
                MountImg(csEvidence, reporter);
            }
            catch (Exception ex) {
                LoggerService.Current?.WriteCallerLine(ex.Message);
                MsgBoxService.Current?.ShowError(ex.Message);
            }

        }

        /// <summary>
        /// 移除证据项若为镜像,则进行卸载;
        /// </summary>
        /// <param name="evidence"></param>
        private void OnCaseEvidenceRemovedOnImaging(ICaseEvidence evidence) {
            if (evidence == null) {
                return;
            }

            if (!(evidence.EvidenceTypeGuids?.Contains(EvidenceType_Img) ?? false)) {
                return;
            }

            var fsService = FileSystemService.Current;
            if (fsService == null) {
                LoggerService.Current.WriteCallerLine($"{nameof(fsService)} can't be null.");
                return;
            }

            var tuples = ImgMountUnits.Where(p => p.CaseEvidence == evidence).ToArray();
            foreach (var tuple in tuples) {
                //文件系统卸载文件;
                var files = fsService.MountedUnits.Where(p => tuple.CaseEvidence.EvidenceGUID == p.GUID).ToArray();
                foreach (var fileTuple in files) {
                    fsService.UnMountFile(fileTuple.File);
                }

                tuple.ImgMounter.Dispose();
                _mountUnits.Remove(tuple);
            }

        }

        /// <summary>
        /// 案件卸载时,卸载所有镜像挂载器;
        /// </summary>
        private void OnCaseUnloadedOnImaging(ICase cs) {
            var fsService = FileSystemService.Current;
            if (fsService == null) {
                LoggerService.Current.WriteCallerLine($"{nameof(fsService)} can't be null.");
                return;
            }

            foreach (var tuple in ImgMountUnits) {
                //文件系统卸载镜像文件;
                var files = fsService.MountedUnits.Where(p => tuple.CaseEvidence.EvidenceGUID == p.GUID).ToArray();
                foreach (var file in files) {
                    fsService.UnMountFile(file.File);
                }

                try {
                    tuple.ImgMounter.Dispose();
                }
                catch(Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            }

            _mountUnits.Clear();
        }

        /// <summary>
        /// 挂载新镜像文件到案件中;
        /// </summary>
        /// <param name="path"></param>
        public void AddImg(string path) {
            if (!(CaseService.Current?.ConfirmCaseLoaded() ?? false)) {
                return;
            }

            InternalAddImg(path, FileAccess.ReadWrite, FileShare.Read);
        }

        /// <summary>
        /// 内部使用;
        /// </summary>
        /// <param name="path"></param>
        /// <param name="access"></param>
        /// <param name="fileshare"></param>
        private void InternalAddImg(string path, FileAccess access, FileShare fileshare) {
            if (path == null) {
                throw new ArgumentNullException(nameof(path));
            }

            try {
                if (!File.Exists(path)) {
                    throw new FileNotFoundException($"{nameof(path)}:{path} doesn't exist.");
                }
            }
            catch (Exception ex) {
                LoggerService.Current?.WriteCallerLine(ex.Message);
                throw;
            }

            var csEvidence = CaseService.Current.CreateNewCaseEvidence(new string[] {
                EvidenceType_Img
            }, Path.GetFileName(path), path);

            csEvidence[ImgPath] = Path.GetFullPath(path);
            csEvidence[ImgFileFileAccess] = access.ToString();
            csEvidence[ImgFileFileShare] = fileshare.ToString();

            CaseService.Current.CurrentCase.AddNewCaseEvidence(csEvidence);

            CaseService.Current.CurrentCase.LoadCaseEvidence(csEvidence);

        }

        /// <summary>
        /// 根据案件文件中的信息挂载镜像;
        /// </summary>
        /// <param name="csEvidence">案件文件</param>
        /// <param name="reporter">进度通知报告器</param>
        private void MountImg(ICaseEvidence csEvidence, IProgressReporter reporter) {
            if (csEvidence == null) {
                throw new ArgumentNullException(nameof(csEvidence));
            }

            if (!(csEvidence.EvidenceTypeGuids?.Contains(EvidenceType_Img) ?? false)) {
                throw new InvalidOperationException($"{nameof(ICaseEvidence.EvidenceTypeGuids)} doesn't contain {EvidenceType_Img}");
            }

            try {
                var imgMounter = GetImgMounter(csEvidence);
                if (imgMounter == null) {
                    throw new Exception($"Valid {nameof(imgMounter)} not found.");
                }
                //加入挂载流;
                _mountUnits.Add(new ImgMountUnit(imgMounter,csEvidence));

                //尝试将数据流挂载到文件系统上;
                FileSystemService.Current.MountStream(imgMounter.RawStream,csEvidence.Name,csEvidence.EvidenceGUID, reporter);
            }
            catch (Exception ex) {
                LoggerService.Current?.WriteCallerLine(ex.Message);
                throw;
            }
        }
        
        //获得镜像的可用挂载器;
        private IImgMounter GetImgMounter(ICaseEvidence csEvidence) {
            if (csEvidence == null) {
                throw new ArgumentNullException(nameof(csEvidence));
            }

            if (!(csEvidence.EvidenceTypeGuids?.Contains(EvidenceType_Img) ?? false)) {
                throw new InvalidOperationException($"{nameof(ICaseEvidence.EvidenceTypeGuids)} doesn't contain {EvidenceType_Img}");
            }

            try {
                //获得安全访问级别;
                if (!Enum.TryParse<FileAccess>(csEvidence[ImgFileFileAccess], out var fileAccess)) {
                    fileAccess = FileAccess.Read;
                    LoggerService.Current?.WriteCallerLine($"{nameof(FileAccess)} is not specified.");
                    //throw new FileLoadException($"{nameof(FileAccess)} is not specified.");
                }

                if (!Enum.TryParse<FileShare>(csEvidence[ImgFileFileShare], out var fileShare)) {
                    fileShare = FileShare.Read;
                    LoggerService.Current?.WriteCallerLine($"{nameof(FileShare)} is not specified.");
                    //throw new FileLoadException($"{nameof(FileAccess)} is not specified.");
                }

                var path = csEvidence[ImgPath];
                if (string.IsNullOrEmpty(path)) {
                    throw new InvalidOperationException($"{nameof(path)} can't be null.");
                }
                IImgMounterProvider mounterProvider = null;
                foreach (var mProvider in _mounterProviders) {
                    try {
                        if (mProvider.CheckIsValidImg(path)) {
                            mounterProvider = mProvider;
                            break;
                        }
                    }
                    catch(Exception ex) {
                        LoggerService.WriteCallerLine(ex.Message);
                    }
                }
                 

                //若并未找到,则抛出异常;
                if (mounterProvider == null) {
                    LoggerService.Current.WriteCallerLine($"{nameof(mounterProvider)} could not be found.");
                    return null;
                }

                var group = csEvidence.XElem.GetGroup(Contracts.Common.Constants.EvidenceProperties);
                group[ImgFormat] = mounterProvider.FormatName;
                var parser = mounterProvider.CreateMounter(path,csEvidence.XElem,fileAccess, fileShare);
                return parser;
            }
            catch(Exception ex) {
                LoggerService.Current?.WriteCallerLine(ex.Message);
                throw;
            }
                
            
        }

        //添加镜像;
        public void AddImg() {
            if (!(CaseService.Current?.ConfirmCaseLoaded() ?? false)) {
                return;
            }

            if (ServiceProvider.Current?.GetInstance<ICaseService>()?.CurrentCase == null) {
                LoggerService.Current?.WriteCallerLine("null case entity!", "AddImgCommand");
                return;
            }

            var openPath = ServiceProvider.Current?.GetInstance<IDialogService>()?.OpenFile();
            
            if (string.IsNullOrEmpty(openPath)) {
                return;
            }

            var shellService = ServiceProvider.Current.GetInstance<IShellService>();
            shellService?.ChangeLoadState(true, string.Empty);

            try {
                AddImg(openPath);
            }
            catch (Exception ex) {
                LoggerService.Current?.WriteCallerLine(ex.Message, "AddImgCommand");
            }
            finally {
                shellService?.ChangeLoadState(false, null);
            }
            
        }

        private List<IImgMountUnit> _mountUnits = new List<IImgMountUnit>();

        public IEnumerable<IImgMountUnit> ImgMountUnits => _mountUnits.Select(p => p);
    }
}
