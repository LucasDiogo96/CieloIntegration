using log4net;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Lib.Util
{
    public static class LogUtil
    {
         static readonly ILog logger = LogManager.GetLogger(typeof(LogUtil));

        static LogUtil()
        {
            log4net.Config.XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetEntryAssembly()), new FileInfo("log4net.config"));
        }

        public static void Error(string msg,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            logger.Error(FormatCaller(memberName, sourceFilePath, sourceLineNumber) + msg + Environment.NewLine);
        }

        public static void ErrorTitulo(string titulo, string msg,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (!string.IsNullOrEmpty(msg.Replace("\r", "").Trim()) || !string.IsNullOrEmpty(msg.Replace("\n", "").Trim()))
            {
                msg = titulo + "\r\n" + msg;
                logger.Error(FormatCaller(memberName, sourceFilePath, sourceLineNumber) + msg + Environment.NewLine);
            }
        }

        public static void ErrorLydiansComunicacao(Exception ex, string cliente, string aplicacao,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            GlobalContext.Properties["Cliente"] = cliente;
            GlobalContext.Properties["Aplicacao"] = aplicacao;
            logger.Error(FormatCaller(memberName, sourceFilePath, sourceLineNumber) + ex.ToString() + Environment.NewLine);

        }

        public static void Error(Exception ex,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            logger.Error(FormatCaller(memberName, sourceFilePath, sourceLineNumber) + ex.ToString() + Environment.NewLine);
        }

        public static void Debug(string msg,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            logger.Debug(FormatCaller(memberName, sourceFilePath, sourceLineNumber) + msg + Environment.NewLine);
        }

        public static void DebugTitulo(string titulo, string msg,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (!string.IsNullOrEmpty(msg.Replace("\r", "").Trim()) || !string.IsNullOrEmpty(msg.Replace("\n", "").Trim()))
            {
                msg = titulo + "\r\n" + msg;
                log4net.Config.XmlConfigurator.Configure(logger.Logger.Repository);
                logger.Debug(FormatCaller(memberName, sourceFilePath, sourceLineNumber) + msg + Environment.NewLine);
            }

        }
         static string FormatCaller(string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
        {
            return "[" + sourceFilePath + "][" + sourceLineNumber + "][" + memberName + "]\r\n";
        }
    }
}
