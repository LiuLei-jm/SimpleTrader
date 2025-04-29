namespace SimpleTrader.Domain.Models
{
    public class StockIndex
    {
        public string Symbol { get; set; }
        public double Price { get; set; }
        public double Change { get; set; }
        public long Volume { get; set; }
    }
}
