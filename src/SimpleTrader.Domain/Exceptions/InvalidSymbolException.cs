using System.Runtime.Serialization;

namespace SimpleTrader.Domain.Exceptions
{
    public class InvalidSymbolException : Exception
    {
        public string Symbol { get; set; }
        public InvalidSymbolException(string symbol)
        {
            Symbol = symbol;
        }

        public InvalidSymbolException(string? message, string symbol) : base(message)
        {
            Symbol = symbol;
        }

        public InvalidSymbolException(string? message, Exception? innerException, string symbol) : base(message, innerException)
        {
            Symbol = symbol;
        }

        protected InvalidSymbolException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
