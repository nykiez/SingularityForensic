using CDFC.Parse.Abstracts;
using CDFC.Parse.Python;
using EventLogger;
using Microsoft.Practices.ServiceLocation;
using Singularity.Android.Models;
using Singularity.Contracts.Case;
using Singularity.Contracts.Common;
using System;
using System.IO;
using System.Xml.Linq;

namespace Singularity.UI.Info.Android.Helpers {
    /// <summary>
    /// AndroidDeviceCase以及Python结合帮助类;
    /// </summary>
    public static class AdvPythonHelper {
        //请求文件指令;
        const string request_file = nameof(request_file);

        /// <summary>
        /// //调用py;获得结果参数文件路径;
        /// </summary>
        /// <param name="outPutXDocPath">输出参数文件路径</param>
        /// <param name="pyName"></param>
        /// <returns>输出相对路径以及参数文件的路径</returns>
        public static (string outPutPath, string outDocName) GetProcessOutPut(AndroidDeviceCaseEvidence CaseFile, string pyName) {
            var csService = ServiceProvider.Current?.GetInstance<ICaseService>();
            if(csService == null) {
                throw new InvalidOperationException($"{nameof(ICaseService)} is not registered.");
            }

            var guid = Guid.NewGuid().ToString("N");
            var outPutPath = $"{csService.CurrentCase.Path}/{CaseFile.BasePath}/{guid}";
            var outPutDocName = "output.xml";
            if (!System.IO.Directory.Exists(outPutPath)) {
                System.IO.Directory.CreateDirectory(outPutPath);
            }

            //生成参数文件;
            var xDoc = new XDocument();

            //文件范围相关;
            var rootElem = new XElement("Params");
            var rangesXDoc = CaseFile.BuildFilesDoc(saveAllFiles: false);
            var FileRangesXDoc = $"{outPutPath}/new.xml";
            var outPutXDocPath = $"{outPutPath}/{outPutDocName}";
            rangesXDoc.Save(FileRangesXDoc);

            //其它参数;
            rootElem.Add(new XElement("GUID", guid));
            rootElem.Add(new XElement("type", "SingleImg"));
            rootElem.Add(new XElement("ImgPath", CaseFile.InterLabel));
            rootElem.Add(new XElement(nameof(FileRangesXDoc), FileRangesXDoc));
            rootElem.Add(new XElement("OutPutXDocPath", outPutXDocPath));
            rootElem.Add(new XElement("OutPutPath", outPutPath));
            xDoc.Add(rootElem);

            //流输入控制;
            StreamWriter iw = null;
            try {
                var argPath = $"{outPutPath}/read.xml";
                xDoc.Save(argPath);
                var parser = PythonHelper.GetProcess(pyName, argPath);
                parser.OutputDataReceived += (sender, e) => {
                    //确定是否是请求文件;
                    if (e.Data?.StartsWith(request_file) == true) {
#if DEBUG
                        Console.WriteLine("Request:" + e.Data);
#endif
                        //获得文件相对于设备本身的路径;
                        var requestFile = e.Data.Substring(request_file.Length + 1);
                        var requestPath = requestFile.Substring(0, requestFile.LastIndexOf("/"));
                        var file = CaseFile.Data.GetFileByUrl(requestFile);
                        if (file is RegularFile regFile) {
                            using (var stream = regFile.GetStream()) {
                                Stream targetStream = null;
                                try {
                                    if (!System.IO.Directory.Exists($"{outPutPath}/{requestPath}")) {
                                        System.IO.Directory.CreateDirectory($"{outPutPath}/{requestPath}");
                                    }
                                    targetStream = File.Create($"{outPutPath}/{requestFile}");
                                    stream.CopyTo(targetStream);
                                    targetStream.Close();
                                    iw.WriteLine("True");
                                }
                                catch {
                                    iw.WriteLine("False");
                                }

                            }
                        }
                        else {
                            Logger.WriteCallerLine($"Error:Invalid file type:{e.Data}");
                        }
                    }
                    else {
                        Console.WriteLine("Unknown output:" + e.Data);
                    }
                };
                parser.ErrorDataReceived += (sender, e) => {
                    Logger.WriteLine($"{nameof(AdvPythonHelper)}->{nameof(GetProcessOutPut)}:{e.Data}");
#if DEBUG
                    Console.WriteLine(e.Data);
#endif
                };
                parser.Start();

                //重定向相关;
                parser.BeginOutputReadLine();
                parser.BeginErrorReadLine();

                iw = parser.StandardInput;

                parser.WaitForExit();
                parser.Dispose();

                return (outPutPath, outPutDocName);
            }
            catch (Exception ex) {
                Logger.WriteLine($"{nameof(AdvPythonHelper)}->{nameof(GetProcessOutPut)}:{ex.Message}");
                throw;
            }
        }
    }
}
