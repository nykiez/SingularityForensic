using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Cflab.DataTransport.Tools
{
    public class Logger
    {
        /// <summary>
        /// 将文本写入日志文件
        /// </summary>
        /// <param name="msg"></param>
        private static void WriteLine(string msg)
        {
            var dir = Environment.CurrentDirectory + @"\Log";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var path = Path.Combine(dir, DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
            // 以追加的方式写入日志
            var writer = new StreamWriter(path, true);
            writer.WriteLine(msg);
            writer.Close();
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(string msg)
        {
            
        }

        public static void Error(Exception e)
        {
            var sb = new StringBuilder();
            // 加上时间                     
            sb.Append(DateTime.Now.ToString("MM-dd HH:mm:ss    "));
            // 加上异常名称和消息体
            sb.AppendLine($"{e.GetType().FullName}:{e.Message.Trim()}");
            // 加上方法调用栈
            sb.Append(GetStackTrace(2, "                  "));
            // 写入文件
            WriteLine(sb.ToString());
        }

        /// <summary>
        /// 获取方法调用栈
        /// </summary>
        /// <param name="start"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        private static string GetStackTrace(int start, string prefix = null)
        {
            var st = new StackTrace();
            var frams = st.GetFrames();
            if (frams == null)
            {
                return string.Empty;
            }
            var sb = new StringBuilder();
            for (var i = start; i < frams.Length; i++)
            {
                var mb = frams[i].GetMethod();
                // 解析参数信息
                var arg = new StringBuilder();
                var infos = mb.GetParameters();
                for (var index = 0; index < infos.Length; index++)
                {
                    if (index != 0)
                    {
                        arg.Append(",");
                    }
                    arg.Append(infos[index].ParameterType.Name).Append(" ").Append(infos[index].Name);
                }
                sb.Append(prefix).AppendLine($"{mb.DeclaringType?.FullName}.{mb.Name}({arg})");
            }
            return sb.ToString();
        }
    }
}
