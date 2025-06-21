using System;
using System.IO;
using System.Threading;

namespace SingletonPatternExample
{
    public sealed class AdvancedLogger
    {
        private static AdvancedLogger _instance = null;
        private static readonly object _lock = new object();
        private readonly string _logFilePath;
        private readonly object _fileLock = new object();

        public enum LogLevel
        {
            Debug,
            Info,
            Warning,
            Error,
            Critical
        }

        private AdvancedLogger()
        {
            _logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "application.log");
            InitializeLogFile();
        }

        public static AdvancedLogger Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new AdvancedLogger();
                    }
                }
                return _instance;
            }
        }

        private void InitializeLogFile()
        {
            try
            {
                if (!File.Exists(_logFilePath))
                {
                    File.Create(_logFilePath).Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to initialize log file: {ex.Message}");
            }
        }

        public void Log(string message, LogLevel level = LogLevel.Info)
        {
            string formattedMessage = FormatMessage(message, level);
            WriteToConsole(formattedMessage, level);
            WriteToFile(formattedMessage);
        }

        public void Debug(string message)
        {
            Log(message, LogLevel.Debug);
        }

        public void Info(string message)
        {
            Log(message, LogLevel.Info);
        }

        public void Warning(string message)
        {
            Log(message, LogLevel.Warning);
        }

        public void Error(string message)
        {
            Log(message, LogLevel.Error);
        }

        public void Critical(string message)
        {
            Log(message, LogLevel.Critical);
        }

        public void LogException(Exception ex, string context = "")
        {
            string message = string.IsNullOrEmpty(context) 
                ? $"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}"
                : $"Exception in {context}: {ex.Message}\nStackTrace: {ex.StackTrace}";
            
            Log(message, LogLevel.Error);
        }

        private string FormatMessage(string message, LogLevel level)
        {
            return $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [{level.ToString().ToUpper()}] [Thread-{Thread.CurrentThread.ManagedThreadId}] {message}";
        }

        private void WriteToConsole(string message, LogLevel level)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            
            Console.ForegroundColor = level switch
            {
                LogLevel.Debug => ConsoleColor.Gray,
                LogLevel.Info => ConsoleColor.White,
                LogLevel.Warning => ConsoleColor.Yellow,
                LogLevel.Error => ConsoleColor.Red,
                LogLevel.Critical => ConsoleColor.Magenta,
                _ => ConsoleColor.White
            };

            Console.WriteLine(message);
            Console.ForegroundColor = originalColor;
        }

        private void WriteToFile(string message)
        {
            try
            {
                lock (_fileLock)
                {
                    File.AppendAllText(_logFilePath, message + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write to log file: {ex.Message}");
            }
        }

        public void ClearLogFile()
        {
            try
            {
                lock (_fileLock)
                {
                    File.WriteAllText(_logFilePath, string.Empty);
                }
                Log("Log file cleared", LogLevel.Info);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to clear log file: {ex.Message}");
            }
        }

        public string GetLogFilePath()
        {
            return _logFilePath;
        }

        public long GetLogFileSize()
        {
            try
            {
                if (File.Exists(_logFilePath))
                {
                    return new FileInfo(_logFilePath).Length;
                }
                return 0;
            }
            catch
            {
                return -1;
            }
        }
    }
}