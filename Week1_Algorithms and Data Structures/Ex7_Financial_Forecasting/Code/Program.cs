using FinancialForecasting.Models;
using FinancialForecasting.Core;
using FinancialForecasting.Utils;
using FinancialForecasting.Analysis;

namespace FinancialForecasting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("FINANCIAL FORECASTING TOOL WITH RECURSIVE ALGORITHMS");
            Console.WriteLine("====================================================\n");

            try
            {
                RunDemonstration();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static void RunDemonstration()
        {
            Console.WriteLine("1. UNDERSTANDING RECURSION IN FINANCIAL FORECASTING");
            Console.WriteLine("===================================================");
            ExplainRecursionConcept();

            Console.WriteLine("\n2. GENERATING SAMPLE FINANCIAL DATA");
            Console.WriteLine("==================================");
            var stockData = DataGenerator.GenerateStockPriceData(12, 100m, 0.05m);
            var revenueData = DataGenerator.GenerateRevenueData(8, 1000000m, 0.08m);
            
            Console.WriteLine($"Generated {stockData.Count} months of stock price data");
            Console.WriteLine($"Generated {revenueData.Count} quarters of revenue data");

            Console.WriteLine("\n3. BASIC RECURSIVE FORECASTING");
            Console.WriteLine("==============================");
            DemonstrateBasicForecasting(stockData);

            Console.WriteLine("\n4. OPTIMIZED RECURSIVE FORECASTING WITH MEMOIZATION");
            Console.WriteLine("===================================================");
            DemonstrateOptimizedForecasting(stockData);

            Console.WriteLine("\n5. COMPOUND GROWTH RECURSIVE FORECASTING");
            Console.WriteLine("========================================");
            DemonstrateCompoundGrowthForecasting(revenueData);

            Console.WriteLine("\n6. PERFORMANCE AND COMPLEXITY ANALYSIS");
            Console.WriteLine("=====================================");
            var analyzer = new PerformanceAnalyzer();
            analyzer.AnalyzeRecursiveComplexity(stockData);
            analyzer.CompareAlgorithmPerformance(stockData, 15);

            Console.WriteLine("\n7. PRACTICAL APPLICATION SCENARIOS");
            Console.WriteLine("==================================");
            DemonstratePracticalScenarios();
        }

        static void ExplainRecursionConcept()
        {
            Console.WriteLine("RECURSION IN FINANCIAL FORECASTING:");
            Console.WriteLine("- Recursion breaks down complex problems into smaller, similar subproblems");
            Console.WriteLine("- In forecasting, each future period depends on the previous period's calculation");
            Console.WriteLine("- Base case: Initial value or first period");
            Console.WriteLine("- Recursive case: Future value = Previous value * (1 + growth rate)");
            Console.WriteLine("- This naturally models compound growth in financial contexts\n");

            Console.WriteLine("ADVANTAGES:");
            Console.WriteLine("- Clean, intuitive code that mirrors mathematical formulas");
            Console.WriteLine("- Easy to understand and maintain");
            Console.WriteLine("- Natural fit for compound interest and growth calculations\n");

            Console.WriteLine("CHALLENGES:");
            Console.WriteLine("- Can be inefficient without optimization (repeated calculations)");
            Console.WriteLine("- Risk of stack overflow with deep recursion");
            Console.WriteLine("- May use more memory than iterative solutions");
        }

        static void DemonstrateBasicForecasting(List<FinancialData> data)
        {
            var forecaster = new RecursiveForecaster();
            var result = forecaster.ForecastWithBasicRecursion(data, 6);

            Console.WriteLine("BASIC RECURSIVE FORECASTING RESULTS:");
            Console.WriteLine("Recent Historical Data (Last 3 periods):");
            for (int i = Math.Max(0, data.Count - 3); i < data.Count; i++)
            {
                Console.WriteLine($"  {data[i]}");
            }

            Console.WriteLine("\nForecasted Data (Next 6 periods):");
            foreach (var forecast in result.ForecastedData)
            {
                Console.WriteLine($"  {forecast}");
            }

            Console.WriteLine($"\nPerformance: {result.ComputationTime.TotalMilliseconds:F2}ms, {result.RecursiveCalls} recursive calls");
        }

        static void DemonstrateOptimizedForecasting(List<FinancialData> data)
        {
            var forecaster = new RecursiveForecaster();
            var result = forecaster.ForecastWithMemoization(data, 6);

            Console.WriteLine("MEMOIZED RECURSIVE FORECASTING RESULTS:");
            Console.WriteLine("Forecasted Data (Next 6 periods):");
            foreach (var forecast in result.ForecastedData)
            {
                Console.WriteLine($"  {forecast}");
            }

            Console.WriteLine($"\nOptimized Performance: {result.ComputationTime.TotalMilliseconds:F2}ms, {result.RecursiveCalls} recursive calls");
            Console.WriteLine("Notice the significant reduction in recursive calls due to caching!");
        }

        static void DemonstrateCompoundGrowthForecasting(List<FinancialData> data)
        {
            var forecaster = new RecursiveForecaster();
            var result = forecaster.ForecastWithCompoundGrowth(data, 4);

            Console.WriteLine("COMPOUND GROWTH RECURSIVE FORECASTING:");
            Console.WriteLine("This method uses compound annual growth rate (CAGR) for more accurate long-term projections");
            
            Console.WriteLine("\nRecent Historical Revenue (Last 2 quarters):");
            for (int i = Math.Max(0, data.Count - 2); i < data.Count; i++)
            {
                Console.WriteLine($"  {data[i]}");
            }

            Console.WriteLine("\nForecasted Revenue (Next 4 quarters):");
            foreach (var forecast in result.ForecastedData)
            {
                Console.WriteLine($"  {forecast}");
            }

            Console.WriteLine($"\nCompound Growth Performance: {result.ComputationTime.TotalMilliseconds:F2}ms, {result.RecursiveCalls} recursive calls");
        }

        static void DemonstratePracticalScenarios()
        {
            Console.WriteLine("SCENARIO 1: Investment Portfolio Growth");
            var investmentData = DataGenerator.GenerateInvestmentData(5, 50000m, 0.12m);
            var forecaster = new RecursiveForecaster();
            var investmentForecast = forecaster.ForecastWithCompoundGrowth(investmentData, 3);
            
            Console.WriteLine($"Portfolio Value Today: ${investmentData.Last().Value:F2}");
            Console.WriteLine($"Projected Value in 3 Years: ${investmentForecast.ForecastedData.Last().Value:F2}");
            Console.WriteLine($"Total Growth: ${investmentForecast.ForecastedData.Last().Value - investmentData.Last().Value:F2}");

            Console.WriteLine("\nSCENARIO 2: Technology Startup Revenue Projection");
            var startupData = DataGenerator.GenerateTrendingData(6, 50000m, 0.15m, true);
            var startupForecast = forecaster.ForecastWithBasicRecursion(startupData, 6);
            
            Console.WriteLine($"Current Monthly Revenue: ${startupData.Last().Value:F2}");
            Console.WriteLine($"Projected Revenue in 6 Months: ${startupForecast.ForecastedData.Last().Value:F2}");

            Console.WriteLine("\nSCENARIO 3: Market Decline Analysis");
            var decliningData = DataGenerator.GenerateTrendingData(8, 10000m, 0.05m, false);
            var declineForecast = forecaster.ForecastWithMemoization(decliningData, 4);
            
            Console.WriteLine($"Current Market Value: ${decliningData.Last().Value:F2}");
            Console.WriteLine($"Projected Value in 4 Periods: ${declineForecast.ForecastedData.Last().Value:F2}");

            Console.WriteLine("\nOPTIMIZATION TECHNIQUES DEMONSTRATED:");
            Console.WriteLine("1. Memoization: Caching results to avoid redundant calculations");
            Console.WriteLine("2. Tail Recursion: Optimizing recursive calls for better performance");
            Console.WriteLine("3. Mathematical Shortcuts: Using compound formulas instead of step-by-step recursion");
            Console.WriteLine("4. Early Termination: Stopping calculations when precision requirements are met");
        }
    }
}