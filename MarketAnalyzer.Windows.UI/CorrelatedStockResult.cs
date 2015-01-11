using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketAnalyzer.Windows.UI
{
    public class CorrelatedStockResult
    {
        public string Symbol1 { get; set; }

        public string Symbol2 { get; set; }

        public decimal Correlation { get; set; }

        public CorrelatedStockResult()
        {

        }

    }
}
