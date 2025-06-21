using System;
using System.Threading.Tasks;
namespace SingletonPatternExample
{
    public class SingletonTest
    {
        public static void RunTests()
        {
            Console.WriteLine("=== Running Singleton Tests ===");
            TestSingleInstance();
            TestThreadSafety();
            TestFunctionality();   
            Console.WriteLine("=== All Tests Completed ===\n");
        }
        private static void TestSingleInstance()
        {
            Console.WriteLine("Test 1: Single Instance Test");
            Logger instance1 = Logger.Instance;
            Logger instance2 = Logger.Instance;
            Logger instance3 = Logger.Instance;
            bool allSameInstance = ReferenceEquals(instance1, instance2) && 
                                 ReferenceEquals(instance2, instance3);
            Console.WriteLine($"All instances are the same: {allSameInstance}");
            Console.WriteLine($"Instance1 Hash: {instance1.GetHashCode()}");
            Console.WriteLine($"Instance2 Hash: {instance2.GetHashCode()}");
            Console.WriteLine($"Instance3 Hash: {instance3.GetHashCode()}");
            Console.WriteLine();
        }
        private static void TestThreadSafety()
        {
            Console.WriteLine("Test 2: Thread Safety Test");
            Logger[] loggers = new Logger[10];
            Task[] tasks = new Task[10];
            for (int i = 0; i < 10; i++)
            {
                int index = i;
                tasks[i] = Task.Run(() =>
                {
                    loggers[index] = Logger.Instance;
                });
            }
            Task.WaitAll(tasks);
            bool allSame = true;
            for (int i = 1; i < loggers.Length; i++)
            {
                if (!ReferenceEquals(loggers[0], loggers[i]))
                {
                    allSame = false;
                    break;
                }
            }
            Console.WriteLine($"Thread safety test passed: {allSame}");
            Console.WriteLine();
        }
        private static void TestFunctionality()
        {
            Console.WriteLine("Test 3: Functionality Test");
            Logger logger = Logger.Instance;            
            logger.Log("Testing basic log functionality");
            logger.LogInfo("Testing info log functionality");
            logger.LogError("Testing error log functionality");
            Console.WriteLine("Functionality test completed");
            Console.WriteLine();
        }
    }
}