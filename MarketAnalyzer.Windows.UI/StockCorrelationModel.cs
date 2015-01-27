using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketAnalyzer.Core;
using Deedle;

namespace MarketAnalyzer.Windows.UI
{
    public class StockCorrelationModel : INotifyPropertyChanged
    {
        private CorrelatedStockResult result;

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string StockSymbol1 { get; set; }

        public string StockSymbol2 { get; set; }

        public CorrelatedStockResult Result 
        { 
            get
            {
                return result;
            }
            set
            {
                result = value;
                this.OnPropertyChanged("Result");
            }
        }

        public StockCorrelationModel()
        {
            this.StockSymbol1 = "YHOO";
            this.StockSymbol2 = "FB";
            this.FromDate = DateTime.Parse("1/1/2013");
            this.ToDate = DateTime.Parse("1/1/2014");
        }
        public void Calculate()
        {
            Deedle.Frame<string, string> correlation = MarketAnalyzer.Core.StockAnalysis.getStockCorrellation(new []{this.StockSymbol1, this.StockSymbol2}, this.FromDate, this.ToDate);
            CorrelatedStockResult result = new CorrelatedStockResult();
            result.Symbol1 = correlation.ColumnKeys.First();
            result.Symbol2 = correlation.ColumnKeys.Last();
            result.Correlation = decimal.Parse(correlation.Rows[result.Symbol1].Values.Last().ToString());
            this.Result = result;
         }

        public bool CanCalculate()
        {
            return true;
        }


        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}