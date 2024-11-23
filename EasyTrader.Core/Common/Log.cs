using EasyTrader.Core.Configuration;

namespace EasyTrader.Core.Common
{
    public static class Log
    {
        public static Configurations Configurations
        {
            get
            {
                if (TraderGlobals.Configurations is not null)
                { return TraderGlobals.Configurations; }
                else
                { throw  CommonException.ConfigurationNull(); }
            }
        }

        public static void Error(CommonException exception)
        {
            if (Configurations.IsLoggingEnabled)
            { LogToFile(SeverityEnum.Error, exception); }
        }
        public static void Warning(CommonException exception)
        {
            if (Configurations.IsLoggingEnabled)
            { LogToFile(SeverityEnum.Warning, exception); }
        }
        public static void Information(CommonException exception)
        {
            if (Configurations.IsLoggingEnabled)
            { LogToFile(SeverityEnum.Information, exception); }
        }
        public static void Debug(string message)
        {
            if (Configurations.IsDebugEnabled)
            { LogToFile(SeverityEnum.Debug, message); }
        }

        private static void LogToFile(SeverityEnum logEnum, CommonException exception)
        {
            LogToFile(logEnum, exception.FullExceptionDescription);
        }

        private static void LogToFile(SeverityEnum logEnum, string content)
        {
            FileManager.UpdateFile(
                TraderGlobals.Configurations.LogFilePath,
                TraderGlobals.Configurations.LogFileName,
                "\r\n[" + DateTime.Now + "]   " + logEnum.ToString() + ":     " + content);
        }

        public static void Clear()
        {
            FileManager.SaveFile(
                TraderGlobals.Configurations.LogFilePath,
                TraderGlobals.Configurations.LogFileName,
                String.Empty);
        }
    }
}
