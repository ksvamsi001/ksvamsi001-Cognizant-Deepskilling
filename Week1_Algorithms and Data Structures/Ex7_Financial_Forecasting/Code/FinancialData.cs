namespace FinancialForecasting.Models
{
    public class FinancialData
    {
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public decimal GrowthRate { get; set; }
        public FinancialData(DateTime date, decimal value, decimal growthRate = 0)
        {
            Date = date;
            Value = value;
            GrowthRate = growthRate;
        }
        public override string ToString()
        {
            return $"Date: {Date:yyyy-MM-dd}, Value: ${Value:F2}, Growth Rate: {GrowthRate:P2}";
        }
    }
    public class ForecastResult
    {
        public List<FinancialData> HistoricalData { get; set; }
        public List<FinancialData> ForecastedData { get; set; }
        public TimeSpan ComputationTime { get; set; }
        public int RecursiveCalls { get; set; }
        public ForecastResult()
        {
            HistoricalData = new List<FinancialData>();
            ForecastedData = new List<FinancialData>();
        }
        public void DisplayResults()
        {
            Console.WriteLine("=== HISTORICAL DATA ===");
            foreach (var data in HistoricalData)
            {
                Console.WriteLine(data);
            }
            Console.WriteLine("\n=== FORECASTED DATA ===");
            foreach (var data in ForecastedData)
            {
                Console.WriteLine(data);
            }
            Console.WriteLine($"\nComputation Time: {ComputationTime.TotalMilliseconds:F2} ms");
            Console.WriteLine($"Recursive Calls Made: {RecursiveCalls}");
        }
    }
}