using System;
using System.Threading.Tasks;

namespace SingletonPatternExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Running Singleton Tests ===");
            SingletonTest.RunTests();

            Console.WriteLine("=== Basic Logger Demo ===");
            TestBasicLogger();

            Console.WriteLine("\n=== Advanced Logger Demo ===");
            await TestAdvancedLogger();

            Console.WriteLine("\n=== Singleton Pattern Verification ===");
            VerifySingletonPattern();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static void TestBasicLogger()
        {
            Logger logger1 = Logger.Instance;
            Logger logger2 = Logger.Instance;

            logger1.Log("First log message");
            logger2.LogInfo("Second log message");
            logger1.LogError("An error occurred");

            Console.WriteLine($"Are both logger instances the same? {ReferenceEquals(logger1, logger2)}");
            Console.WriteLine($"Logger1 HashCode: {logger1.GetHashCode()}");
            Console.WriteLine($"Logger2 HashCode: {logger2.GetHashCode()}");

            TestSingletonFromDifferentMethods();
        }

        static void TestSingletonFromDifferentMethods()
        {
            Logger loggerFromMethod1 = GetLoggerFromMethod1();
            Logger loggerFromMethod2 = GetLoggerFromMethod2();

            Console.WriteLine($"Loggers from different methods are same instance? {ReferenceEquals(loggerFromMethod1, loggerFromMethod2)}");
        }

        static Logger GetLoggerFromMethod1()
        {
            return Logger.Instance;
        }

        static Logger GetLoggerFromMethod2()
        {
            return Logger.Instance;
        }

        static async Task TestAdvancedLogger()
        {
            AdvancedLogger advancedLogger = AdvancedLogger.Instance;

            Console.WriteLine("Testing different log levels:");
            advancedLogger.Debug("This is a debug message");
            advancedLogger.Info("Application started successfully");
            advancedLogger.Warning("This is a warning message");
            advancedLogger.Error("This is an error message");
            advancedLogger.Critical("This is a critical message");

            Console.WriteLine("\nTesting exception logging:");
            try
            {
                throw new InvalidOperationException("This is a test exception");
            }
            catch (Exception ex)
            {
                advancedLogger.LogException(ex, "TestAdvancedLogger method");
            }

            Console.WriteLine($"\nLog file location: {advancedLogger.GetLogFilePath()}");
            Console.WriteLine($"Log file size: {advancedLogger.GetLogFileSize()} bytes");

            Console.WriteLine("\nTesting concurrent logging...");
            await TestConcurrentLogging();
        }

        static async Task TestConcurrentLogging()
        {
            Task[] tasks = new Task[3];
            
            for (int i = 0; i < 3; i++)
            {
                int taskId = i;
                tasks[i] = Task.Run(() =>
                {
                    AdvancedLogger logger = AdvancedLogger.Instance;
                    logger.Info($"Concurrent message from Task {taskId}");
                });
            }

            await Task.WhenAll(tasks);
            Console.WriteLine("Concurrent logging test completed");
        }

        static void VerifySingletonPattern()
        {
            Logger basicLogger1 = Logger.Instance;
            Logger basicLogger2 = Logger.Instance;
            
            AdvancedLogger advancedLogger1 = AdvancedLogger.Instance;
            AdvancedLogger advancedLogger2 = AdvancedLogger.Instance;

            Console.WriteLine($"Basic Logger - Same instance: {ReferenceEquals(basicLogger1, basicLogger2)}");
            Console.WriteLine($"Basic Logger - Hash codes: {basicLogger1.GetHashCode()} == {basicLogger2.GetHashCode()}");
            
            Console.WriteLine($"Advanced Logger - Same instance: {ReferenceEquals(advancedLogger1, advancedLogger2)}");
            Console.WriteLine($"Advanced Logger - Hash codes: {advancedLogger1.GetHashCode()} == {advancedLogger2.GetHashCode()}");
        }
    }
}