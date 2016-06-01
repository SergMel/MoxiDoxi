using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GKFXTest
{
    class CommonReader
    {
        private const string DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffffff";

        public List<POCOQuote> ReadQuotes(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }
            string[] lines;
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format("Error parsing file '{0}'", filePath), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private POCOQuote ParseQuote(string line)
        {
            var fields = line.Split(',');
            if (fields.Length != 4)
            {                
                return null;
            }
            try
            {
                DateTime time = DateTime.ParseExact(fields[0].Trim(), DateTimeFormat, null, System.Globalization.DateTimeStyles.None);
                string instrument = fields[1].Trim().ToUpper();
                Side side = (Side)int.Parse(fields[2].Trim());
                if (!Enum.IsDefined(typeof(Side), side))
                {
                    return null;
                }
                decimal priceBid = decimal.Parse(fields[2], CultureInfo.InvariantCulture);
                decimal priceAsk = decimal.Parse(fields[3], CultureInfo.InvariantCulture);
                POCOQuote ret = new POCOQuote()
                {
                    Time = time,
                    Sym = instrument,
                    BidPrice = priceBid,
                    AskPrice = priceAsk,
                };
                return ret;
            }
            catch
            {
                return null;
            }

        }

        List<POCOTrade> ReadTrades(string file)
        {
             var fields = line.Split(',');
            if (fields.Length != 5)
            {
                return null;
            }
            try
            {
                DateTime time = DateTime.ParseExact(fields[0].Trim(), DateTimeFormat, null, System.Globalization.DateTimeStyles.None);
                string instrument = fields[1].Trim().ToUpper();
                Side side = (Side)int.Parse(fields[2].Trim());
                if (!Enum.IsDefined(typeof(Side), side))
                {
                    return null;
                }
                uint amount = uint.Parse(fields[3].Trim());
                decimal price = decimal.Parse(fields[4], CultureInfo.InvariantCulture);

                POCOTrade ret= new POCOTrade()
                {
                    Time = time,
                    Amount = amount,
                    Price = price,
                    Side = side,
                    Sym = instrument
                };
                return ret;
            }
            catch
            {
                return null;
            }
        }

        private POCOTrade ParseTrade(string line)
        {
            var fields = line.Split(',');
            if (fields.Length != 5)
            {
                return null;
            }
            try
            {
                DateTime time = DateTime.ParseExact(fields[0].Trim(), DateTimeFormat, null, System.Globalization.DateTimeStyles.None);
                string instrument = fields[1].Trim().ToUpper();
                Side side = (Side)int.Parse(fields[2].Trim());
                if (!Enum.IsDefined(typeof(Side), side))
                {
                    return null;
                }
                uint amount = uint.Parse(fields[3].Trim());
                decimal price = decimal.Parse(fields[4], CultureInfo.InvariantCulture);

                POCOTrade ret = new POCOTrade()
                {
                    Time = time,
                    Amount = amount,
                    Price = price,
                    Side = side,
                    Sym = instrument
                };
                return ret;
            }
            catch
            {
                return null;
            }
        }
    }
}
