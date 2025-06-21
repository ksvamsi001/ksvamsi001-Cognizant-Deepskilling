using FinancialForecasting.Models;
using System.Diagnostics;
namespace FinancialForecasting.Core
{
    public class RecursiveForecaster
    {
        private int _recursiveCallCount;
        private Dictionary<string, decimal> _memoizationCache;
        public RecursiveForecaster()
        {
            _memoizationCache = new Dictionary<string, decimal>();
        }
        public ForecastResult ForecastWithBasicRecursion(List<FinancialData> historicalData, int periodsToForecast)
        {
            _recursiveCallCount = 0;
            var stopwatch = Stopwatch.StartNew();
            var result = new ForecastResult();
            result.HistoricalData = new List<FinancialData>(historicalData);
            if (historicalData.Count < 2)
            {
                throw new ArgumentException("At least 2 historical data points are required");
            }
            decimal averageGrowthRate = CalculateAverageGrowthRate(historicalData);
            decimal lastValue = historicalData.Last().Value;
            DateTime lastDate = historicalData.Last().Date;
            for (int i = 1; i <= periodsToForecast; i++)
            {
                decimal forecastedValue = CalculateFutureValueRecursive(lastValue, averageGrowthRate, i);
                DateTime forecastDate = lastDate.AddMonths(i);
                result.ForecastedData.Add(new FinancialData(forecastDate, forecastedValue, averageGrowthRate));
            }
            stopwatch.Stop();
            result.ComputationTime = stopwatch.Elapsed;
            result.RecursiveCalls = _recursiveCallCount;
            return result;
        }
        public ForecastResult ForecastWithMemoization(List<FinancialData> historicalData, int periodsToForecast)
        {
            _recursiveCallCount = 0;
            _memoizationCache.Clear();
            var stopwatch = Stopwatch.StartNew();
            var result = new ForecastResult();
            result.HistoricalData = new List<FinancialData>(historicalData);

            if (historicalData.Count < 2)
            {
                throw new ArgumentException("At least 2 historical data points are required");
            }

            decimal averageGrowthRate = CalculateAverageGrowthRate(historicalData);
            decimal lastValue = historicalData.Last().Value;
            DateTime lastDate = historicalData.Last().Date;

            for (int i = 1; i <= periodsToForecast; i++)
            {
                decimal forecastedValue = CalculateFutureValueWithMemoization(lastValue, averageGrowthRate, i);
                DateTime forecastDate = lastDate.AddMonths(i);
                
                result.ForecastedData.Add(new FinancialData(forecastDate, forecastedValue, averageGrowthRate));
            }

            stopwatch.Stop();
            result.ComputationTime = stopwatch.Elapsed;
            result.RecursiveCalls = _recursiveCallCount;

            return result;
        }

        private decimal CalculateFutureValueRecursive(decimal initialValue, decimal growthRate, int periods)
        {
            _recursiveCallCount++;

            if (periods == 0)
            {
                return initialValue;
            }

            if (periods == 1)
            {
                return initialValue * (1 + growthRate);
            }

            return CalculateFutureValueRecursive(initialValue, growthRate, periods - 1) * (1 + growthRate);
        }

        private decimal CalculateFutureValueWithMemoization(decimal initialValue, decimal growthRate, int periods)
        {
            _recursiveCallCount++;

            string cacheKey = $"{initialValue}_{growthRate}_{periods}";
            
            if (_memoizationCache.ContainsKey(cacheKey))
            {
                return _memoizationCache[cacheKey];
            }

            decimal result;

            if (periods == 0)
            {
                result = initialValue;
            }
            else if (periods == 1)
            {
                result = initialValue * (1 + growthRate);
            }
            else
            {
                result = CalculateFutureValueWithMemoization(initialValue, growthRate, periods - 1) * (1 + growthRate);
            }

            _memoizationCache[cacheKey] = result;
            return result;
        }

        private decimal CalculateAverageGrowthRate(List<FinancialData> data)
        {
            if (data.Count < 2) return 0;

            decimal totalGrowthRate = 0;
            int growthPeriods = 0;

            for (int i = 1; i < data.Count; i++)
            {
                if (data[i - 1].Value != 0)
                {
                    decimal periodGrowthRate = (data[i].Value - data[i - 1].Value) / data[i - 1].Value;
                    totalGrowthRate += periodGrowthRate;
                    growthPeriods++;
                }
            }

            return growthPeriods > 0 ? totalGrowthRate / growthPeriods : 0;
        }

        public ForecastResult ForecastWithCompoundGrowth(List<FinancialData> historicalData, int periodsToForecast)
        {
            _recursiveCallCount = 0;
            var stopwatch = Stopwatch.StartNew();

            var result = new ForecastResult();
            result.HistoricalData = new List<FinancialData>(historicalData);

            if (historicalData.Count < 2)
            {
                throw new ArgumentException("At least 2 historical data points are required");
            }

            decimal compoundGrowthRate = CalculateCompoundAnnualGrowthRate(historicalData);
            decimal lastValue = historicalData.Last().Value;
            DateTime lastDate = historicalData.Last().Date;

            for (int i = 1; i <= periodsToForecast; i++)
            {
                decimal forecastedValue = CalculateCompoundFutureValueRecursive(lastValue, compoundGrowthRate, i);
                DateTime forecastDate = lastDate.AddMonths(i);
                
                result.ForecastedData.Add(new FinancialData(forecastDate, forecastedValue, compoundGrowthRate));
            }

            stopwatch.Stop();
            result.ComputationTime = stopwatch.Elapsed;
            result.RecursiveCalls = _recursiveCallCount;

            return result;
        }

        private decimal CalculateCompoundFutureValueRecursive(decimal initialValue, decimal growthRate, int periods)
        {
            _recursiveCallCount++;

            if (periods == 0)
            {
                return initialValue;
            }

            return initialValue * RecursivePower(1 + growthRate, periods);
        }

        private decimal RecursivePower(decimal baseValue, int exponent)
        {
            _recursiveCallCount++;

            if (exponent == 0)
            {
                return 1;
            }

            if (exponent == 1)
            {
                return baseValue;
            }

            if (exponent % 2 == 0)
            {
                decimal halfPower = RecursivePower(baseValue, exponent / 2);
                return halfPower * halfPower;
            }
            else
            {
                return baseValue * RecursivePower(baseValue, exponent - 1);
            }
        }
        private decimal CalculateCompoundAnnualGrowthRate(List<FinancialData> data)
        {
            if (data.Count < 2) return 0;

            decimal startValue = data.First().Value;
            decimal endValue = data.Last().Value;
            int periods = data.Count - 1;

            if (startValue <= 0) return 0;

            return (decimal)Math.Pow((double)(endValue / startValue), 1.0 / periods) - 1;
        }
    }
}