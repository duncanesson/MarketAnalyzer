using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MarketAnalyzer.Windows.UI
{
    public class MainViewModel
    {
        public StockCorrelationModel Model { get; set; }

        RelayCommand getStockCorrelation;
   
        public ICommand GetStockCorrelation 
        { 
            get
            {
                return getStockCorrelation ?? (getStockCorrelation = new RelayCommand(param => Model.Calculate(),
                                                            param => Model.CanCalculate()));
            }
        }

        public MainViewModel()
        {
            this.Model = new StockCorrelationModel();
        }
         
    }

}