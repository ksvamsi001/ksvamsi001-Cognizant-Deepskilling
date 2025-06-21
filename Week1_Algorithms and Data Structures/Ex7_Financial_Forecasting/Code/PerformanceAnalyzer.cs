using FinancialForecasting.Models;
using FinancialForecasting.Core;
using System.Diagnostics;
namespace FinancialForecasting.Analysis
{
    public class PerformanceAnalyzer
    {
        public void AnalyzeRecursiveComplexity(List<FinancialData> data)
        {
            Console.WriteLine("=== RECURSIVE ALGORITHM COMPLEXITY ANALYSIS ===\n");
            var forecaster = new RecursiveForecaster();
            var periods = new int[] { 5, 10, 15, 20, 25 };
            Console.WriteLine("Periods\tBasic Recursion\t\tMemoized Recursion");
            Console.WriteLine("\tTime(ms)\tCalls\t\tTime(ms)\tCalls");
            Console.WriteLine(new string('-', 65));
            foreach (int period in periods)
            {
                var basicResult = forecaster.ForecastWithBasicRecursion(data, period);
                var memoizedResult = forecaster.ForecastWithMemoization(data, period);

                Console.WriteLine($"{period}\t{basicResult.ComputationTime.TotalMilliseconds:F2}\t\t{basicResult.RecursiveCalls}\t\t{memoizedResult.ComputationTime.TotalMilliseconds:F2}\t\t{memoizedResult.RecursiveCalls}");
            }
            Console.WriteLine("\n=== TIME COMPLEXITY EXPLANATION ===");
            Console.WriteLine("Basic Recursion: O(n^2) - Each forecast period recalculates all previous periods");
            Console.WriteLine("Memoized Recursion: O(n) - Each calculation is done once and cached");
            Console.WriteLine("The difference becomes significant as the number of forecast periods increases.\n");
        }
        public void CompareAlgorithmPerformance(List<FinancialData> data, int forecastPeriods)
        {
            Console.WriteLine("=== ALGORITHM PERFORMANCE COMPARISON ===\n");
            var forecaster = new RecursiveForecaster();
            Console.WriteLine("Running performance tests...\n");
            var basicResult = MeasurePerformance(() => forecaster.ForecastWithBasicRecursion(data, forecastPeriods), "Basic Recursion");
            var memoizedResult = MeasurePerformance(() => forecaster.ForecastWithMemoization(data, forecastPeriods), "Memoized Recursion");
            var compoundResult = MeasurePerformance(() => forecaster.ForecastWithCompoundGrowth(data, forecastPeriods), "Compound Growth Recursion");
            Console.WriteLine("\n=== PERFORMANCE SUMMARY ===");
            Console.WriteLine($"Basic Recursion - Time: {basicResult.result.ComputationTime.TotalMilliseconds:F2}ms, Calls: {basicResult.result.RecursiveCalls}");
            Console.WriteLine($"Memoized Recursion - Time: {memoizedResult.result.ComputationTime.TotalMilliseconds:F2}ms, Calls: {memoizedResult.result.RecursiveCalls}");
            Console.WriteLine($"Compound Growth - Time: {compoundResult.result.ComputationTime.TotalMilliseconds:F2}ms, Calls: {compoundResult.result.RecursiveCalls}");
            Console.WriteLine("\n=== OPTIMIZATION RECOMMENDATIONS ===");
            Console.WriteLine("1. Use memoization to cache previously computed values");
            Console.WriteLine("2. Consider iterative approaches for simple calculations");
            Console.WriteLine("3. Implement tail recursion optimization where possible");
            Console.WriteLine("4. Use mathematical formulas instead of recursive calculations for compound growth");
        }
        private (ForecastResult result, TimeSpan executionTime) MeasurePerformance(Func<ForecastResult> operation, string operationName)
        {
            Console.WriteLine($"Testing {operationName}...");   
            var stopwatch = Stopwatch.StartNew();
            var result = operation();
            stopwatch.Stop();
            Console.WriteLine($"{operationName} completed in {stopwatch.ElapsedMilliseconds}ms with {result.RecursiveCalls} recursive calls");
            return (result, stopwatch.Elapsed);
        }
        public void AnalyzeAccuracy(List<FinancialData> actualData, List<FinancialData> forecastedData)
        {
            Console.WriteLine("=== FORECAST ACCURACY ANALYSIS ===\n");
            if (actualData.Count != forecastedData.Count)
            {
                Console.WriteLine("Warning: Actual and forecasted data sets have different lengths");
                return;
            }
            decimal totalError = 0;
            decimal totalPercentageError = 0;
            int validComparisons = 0;
            Console.WriteLine("Period\tActual\t\tForecasted\tError\t\tError%");
            Console.WriteLine(new string('-', 60));
            for (int i = 0; i < Math.Min(actualData.Count, forecastedData.Count); i++)
            {
                decimal actual = actualData[i].Value;
                decimal forecasted = forecastedData[i].Value;
                decimal error = Math.Abs(actual - forecasted);
                decimal percentageError = actual != 0 ? (error / Math.Abs(actual)) * 100 : 0;
                Console.WriteLine($"{i + 1}\t${actual:F2}\t\t${forecasted:F2}\t\t${error:F2}\t\t{percentageError:F2}%");
                totalError += error;
                totalPercentageError += percentageError;
                validComparisons++;
            }
            if (validComparisons > 0)
            {
                decimal meanAbsoluteError = totalError / validComparisons;
                decimal meanPercentageError = totalPercentageError / validComparisons;
                Console.WriteLine(new string('-', 60));
                Console.WriteLine($"Mean Absolute Error: ${meanAbsoluteError:F2}");
                Console.WriteLine($"Mean Percentage Error: {meanPercentageError:F2}%");
            }
        }
    }
}