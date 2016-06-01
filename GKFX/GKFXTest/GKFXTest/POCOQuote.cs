using System;

namespace GKFXTest
{
    class POCOQuote
    {
        public DateTime Time { get; set; }
        public string Sym { get; set; }
        public decimal BidPrice { get; set; }
        public decimal AskPrice { get; set; }
    }
}
