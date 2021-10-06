using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace Utilities
{
    public enum LogLevel
    {
        None,

        [Description("HDR")]
        Heading,

        [Description("ERR")]
        Error,

        [Description("WRN")]
        Warn,

        [Description("INF")]
        Info,

        [Description("DBG")]
        Debug,

        [Description("TRC")]
        Trace,
    }

    public class Logger
    {
        public delegate void GetText(string text);
        public readonly int MaxSize = 2 * 1024 * 1024;
        private static MutexGlobal mutex;

        public GetText GetLogText { get; set; }

        public Logger(string name)
        {
            if (name == null)
            {
                name = GetApplicationName();
            }

            string ext = Path.GetExtension(name);
            if (string.IsNullOrEmpty(ext))
            {
                name += ".log";
            }

            string justname = Path.GetFileNameWithoutExtension(name);
            string logDir = Path.Combine(GetAppDataPath(), "Logs");
            EnsureExists(logDir);

            LogPath = Path.Combine(logDir, name);
            mutex = GetMutex(LogPath);

            string logBackup = Path.Combine(logDir, justname + ".bak");

            using (_ = mutex.GetAwaiter())
            {
                if (File.Exists(LogPath))
                {
                    FileInfo fi = new FileInfo(LogPath);
                    if (fi.Length >= MaxSize)
                    {
                        if (File.Exists(logBackup))
                        {
                            File.Delete(logBackup);
                            File.Move(LogPath, logBackup);
                        }
                    }
                }
                else
                {
                    File.Create(LogPath);
                }

                FileInfo fiLog = new FileInfo(LogPath);
                StartPosition = fiLog.Length;
            }
        }

        private static readonly object loggerLock = new object();

        public static void EnsureExists(string dirpath)
        {
            lock (loggerLock)
            {
                if (!Directory.Exists(dirpath))
                {
                    Directory.CreateDirectory(dirpath);
                }
            }
        }

        private static volatile string dataPath;
        public static string GetAppDataPath()
        {
            if (dataPath != null) return dataPath;
            lock (loggerLock)
            {
                if (dataPath == null)
                {
                    dataPath = Path.Combine(GetLocalAppDataFolder(), GetCompanyName(), Constants.CommonName);
                }
            }

            return dataPath;
        }

        private static volatile string appDataFolder;
        public static string GetLocalAppDataFolder()
        {
            if (appDataFolder != null) return appDataFolder;
            lock (loggerLock)
            {
                if (appDataFolder == null)
                {
                    appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                }
            }

            return appDataFolder;
        }

        private static volatile string applicationName;
        public static string GetApplicationName()
        {
            if (applicationName != null) return applicationName;
            lock (loggerLock)
            {
                if (applicationName == null)
                {
                    string name = null;

                    // System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName
                    using (Process process = Process.GetCurrentProcess())
                    {
                        if (process.MainModule != null)
                        {
                            name = process.MainModule.FileName;
                        }
                    }

                    if (name == null)
                    {
                        //System.Reflection.Assembly.GetEntryAssembly().Location;
                        Assembly assembly = Assembly.GetEntryAssembly();
                        if (assembly != null)
                        {
                            name = Path.GetFileName(assembly.Location);
                        }
                    }

                    applicationName = Path.GetFileNameWithoutExtension(name) ?? throw new NullReferenceException($"{nameof(GetApplicationName)}: Application name cannot be null");
                }
            }

            return applicationName;
        }

        private static volatile string companyName;

        public static string GetCompanyName()
        {
            if (companyName != null) return companyName;
            lock (loggerLock)
            {
                if (companyName == null)
                {
                    Assembly currentAssem = Assembly.GetExecutingAssembly();
                    object[] attribs = currentAssem.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);
                    if (attribs.Length > 0)
                    {
                        companyName = ((AssemblyCompanyAttribute)attribs[0]).Company;
                    }
                }
            }

            return (companyName ?? "Unknown").Replace(" ", "").Replace(".", "");
        }

        private void DoLog(LogLevel level, string text)
        {
            if (LogPath == null) return;
            string dt = DateTime.Now.ToString("yyMMdd-HHmmss.fff");
            using (mutex.GetAwaiter())
            {
                GetLogText?.Invoke(text);
                File.AppendAllText(LogPath, $"{dt}:{level}:{text}\n");
            }
        }

        public void Trace(string format, params object[] objects)
        {
            string text = string.Format(format, objects);
            DoLog(LogLevel.Trace, text);
        }

        public void Debug(string format, params object[] objects)
        {
            string text = string.Format(format, objects);
            DoLog(LogLevel.Debug, text);
        }

        public void Debug(Exception e, string format, params object[] objects)
        {
            string text = string.Format(format, objects);
            DoLog(LogLevel.Debug, $"{text} : {e}");
        }

        public void Debug(Exception e)
        {
            DoLog(LogLevel.Debug, e.ToString());
        }

        public void Info(string format, params object[] objects)
        {
            string text = string.Format(format, objects);
            DoLog(LogLevel.Info, text);
        }

        public void Warn(string format, params object[] objects)
        {
            string text = string.Format(format, objects);
            DoLog(LogLevel.Warn, text);
        }

        public void Warn(Exception e, string format, params object[] objects)
        {
            string text = string.Format(format, objects);
            DoLog(LogLevel.Warn, $"{text} : {e}");
        }

        public void Warn(Exception e)
        {
            DoLog(LogLevel.Warn, e.ToString());
        }

        public void Error(string format, params object[] objects)
        {
            string text = string.Format(format, objects);
            DoLog(LogLevel.Error, text);
        }

        public void Error(Exception e, string format, params object[] objects)
        {
            string text = string.Format(format, objects);
            DoLog(LogLevel.Error, $"{text} : {e}");
        }

        public void Error(Exception e)
        {
            DoLog(LogLevel.Error, e.ToString());
        }

        public void Heading(string format, params object[] objects)
        {
            string text = string.Format(format, objects);
            DoLog(LogLevel.Heading, text);
        }

        public long StartPosition { get; }

        public string LogPath { get; }

        public static MutexGlobal GetMutex(string filePath)
        {
            string mutexName = GetStringHash(filePath) + ".mutex";
            return new MutexGlobal(mutexName);
        }

        public static string GetStringHash(string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(data);
                return new Guid(hash).ToString("n");
            }
        }
    }

    public class MutexGlobal : IDisposable
    {
        public string Name { get; }
        internal Mutex Mutex { get; }
        public int DefaultTimeOut { get; }
        public Func<int, bool> FuncTimeOutRetry { get; }

        public MutexGlobal(string specificName, int defaultTimeOut = Timeout.Infinite)
        {
            if (string.IsNullOrEmpty(specificName))
            {
                throw new ArgumentNullException(nameof(specificName), "Mutex name cannot be null");
            }

            Name = $"Global\\{{{specificName}}}";
            DefaultTimeOut = defaultTimeOut;
            FuncTimeOutRetry = DefaultFuncTimeOutRetry;

            MutexAccessRule allowEveryoneRule = new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), MutexRights.FullControl, AccessControlType.Allow);
            MutexSecurity securitySettings = new MutexSecurity();
            securitySettings.AddAccessRule(allowEveryoneRule);

            Mutex = new Mutex(false, Name, out bool _, securitySettings);
            if (Mutex == null)
            {
                throw new Exception($"Unable to create mutex: {Name}");
            }
        }

        public MutexGlobalAwaiter GetAwaiter(int timeOut)
        {
            return new MutexGlobalAwaiter(this, timeOut);
        }

        public MutexGlobalAwaiter GetAwaiter()
        {
            return new MutexGlobalAwaiter(this, DefaultTimeOut);
        }

        // return true to retry or throw
        private bool DefaultFuncTimeOutRetry(int timeOutUsed)
        {
            //return true;
            throw new TimeoutException($"Mutex timed out. name={Name}, timeused={timeOutUsed}");
        }

        public void Dispose()
        {
            if (Mutex != null)
            {
                Mutex.ReleaseMutex();
                Mutex.Close();
            }
        }
    }

    public class MutexGlobalAwaiter : IDisposable
    {
        private readonly MutexGlobal mutexGlobal;

        public bool HasTimedOut { get; }

        internal MutexGlobalAwaiter(MutexGlobal mutexEx, int timeOut)
        {
            mutexGlobal = mutexEx;

            do
            {
                HasTimedOut = !mutexGlobal.Mutex.WaitOne(timeOut, false);
                if (!HasTimedOut)
                {
                    return;
                }
            }
            while (mutexGlobal.FuncTimeOutRetry(timeOut));
        }

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    mutexGlobal.Mutex.ReleaseMutex();
                }

                disposed = true;
            }
        }

        ~MutexGlobalAwaiter()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
