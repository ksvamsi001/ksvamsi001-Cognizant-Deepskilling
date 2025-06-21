using FinancialForecasting.Models;
namespace FinancialForecasting.Utils
{
    public static class DataGenerator
    {
        private static readonly Random _random = new Random();
        public static List<FinancialData> GenerateStockPriceData(int months, decimal initialPrice = 100m, decimal volatility = 0.1m)
        {
            var data = new List<FinancialData>();
            decimal currentPrice = initialPrice;
            DateTime startDate = DateTime.Now.AddMonths(-months);
            for (int i = 0; i < months; i++)
            {
                DateTime date = startDate.AddMonths(i);
                if (i > 0)
                {
                    decimal changePercent = (decimal)(_random.NextDouble() - 0.5) * 2 * volatility;
                    currentPrice *= (1 + changePercent);
                    currentPrice = Math.Max(currentPrice, 1m);
                }
                decimal growthRate = i > 0 ? (currentPrice - data[i - 1].Value) / data[i - 1].Value : 0;
                data.Add(new FinancialData(date, Math.Round(currentPrice, 2), growthRate));
            }
            return data;
        }
        public static List<FinancialData> GenerateRevenueData(int quarters, decimal initialRevenue = 1000000m, decimal growthTrend = 0.05m)
        {
            var data = new List<FinancialData>();
            decimal currentRevenue = initialRevenue;
            DateTime startDate = DateTime.Now.AddMonths(-quarters * 3);
            for (int i = 0; i < quarters; i++)
            {
                DateTime date = startDate.AddMonths(i * 3);
                if (i > 0)
                {
                    decimal seasonalFactor = 1 + (decimal)(0.1 * Math.Sin(i * Math.PI / 2));
                    decimal randomFactor = 1 + (decimal)(_random.NextDouble() - 0.5) * 0.2m;
                    currentRevenue *= (1 + growthTrend) * seasonalFactor * randomFactor;
                }
                decimal growthRate = i > 0 ? (currentRevenue - data[i - 1].Value) / data[i - 1].Value : 0;
                data.Add(new FinancialData(date, Math.Round(currentRevenue, 2), growthRate));
            }
            return data;
        }
        public static List<FinancialData> GenerateInvestmentData(int years, decimal initialInvestment = 10000m, decimal annualReturn = 0.07m)
        {
            var data = new List<FinancialData>();
            decimal currentValue = initialInvestment;
            DateTime startDate = DateTime.Now.AddYears(-years);
            for (int i = 0; i < years; i++)
            {
                DateTime date = startDate.AddYears(i);
                if (i > 0)
                {
                    decimal marketVolatility = (decimal)(_random.NextDouble() - 0.5) * 0.3m;
                    decimal actualReturn = annualReturn + marketVolatility;
                    currentValue *= (1 + actualReturn);
                }
                decimal growthRate = i > 0 ? (currentValue - data[i - 1].Value) / data[i - 1].Value : 0;
                data.Add(new FinancialData(date, Math.Round(currentValue, 2), growthRate));
            }
            return data;
        }
        public static List<FinancialData> GenerateTrendingData(int periods, decimal initialValue = 1000m, decimal trendRate = 0.03m, bool isIncreasing = true)
        {
            var data = new List<FinancialData>();
            decimal currentValue = initialValue;
            DateTime startDate = DateTime.Now.AddMonths(-periods);
            for (int i = 0; i < periods; i++)
            {
                DateTime date = startDate.AddMonths(i);
                if (i > 0)
                {
                    decimal trend = isIncreasing ? trendRate : -trendRate;
                    decimal noise = (decimal)(_random.NextDouble() - 0.5) * 0.1m;
                    currentValue *= (1 + trend + noise);
                    currentValue = Math.Max(currentValue, 1m);
                }
                decimal growthRate = i > 0 ? (currentValue - data[i - 1].Value) / data[i - 1].Value : 0;
                data.Add(new FinancialData(date, Math.Round(currentValue, 2), growthRate));
            }
            return data;
        }
    }
}