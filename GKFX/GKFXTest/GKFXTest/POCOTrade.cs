using System;

namespace GKFXTest
{
    class POCOTrade
    {
        public DateTime Time { get; set; }
        public string Sym { get; set; }
        public Side Side { get; set; }
        public uint Amount { get; set; }
        public decimal Price { get; set; }
    }
}
