using System;
namespace SingletonPatternExample
{
    public sealed class Logger
    {
        private static Logger _instance = null;
        private static readonly object _lock = new object();
        private Logger()
        {
        }
        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new Logger();
                    }
                }
                return _instance;
            }
        }
        public void Log(string message)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
        }
        public void LogError(string errorMessage)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ERROR: {errorMessage}");
        }
        public void LogInfo(string infoMessage)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] INFO: {infoMessage}");
        }
    }
}