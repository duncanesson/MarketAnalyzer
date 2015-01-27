namespace MarketAnalyzer.Core
 
open System
open System.Data
open Deedle
open FSharp.Data
open FSharp.Charting
open FSharp.Charting.ChartTypes
open MathNet.Numerics.Statistics
open RProvider
open RProvider.stats
open RProvider.graphics
open RProvider.``base``
  
/// Module for performing analysis on different stocks and indicies.
module StockAnalysis =
 
    let internal getStockHistorySeries symbol (fromDate:DateTime) (toDate:DateTime) =
        let url = sprintf "http://ichart.finance.yahoo.com/table.csv?s=%s&a=%i&b=%i&c=%i&d=%i&e=%i&f=%i&g=d&ignore=.csv" symbol (fromDate.Month - 1) (fromDate.Day) (fromDate.Year) (toDate.Month - 1) (toDate.Day) (toDate.Year)
        let stockRawData = CsvProvider<IgnoreErrors=true,Sample="Date (Date),Open (float),High (float),Low (float),Close (float),Volume (int64),AdjClose (float)">.Load url
        let stockRows = stockRawData.Rows
        series [ for d in stockRows -> (d.Date, d.Close)]

    /// Returns the correllation between the 2 symbols.
    let getStockCorrellation symbols fromDate toDate =
        
        // Frame has a limited support for mutation - but adding a new column
        // is one of the things that you can easily do - so the following works
        // perfectly fine (and Deedle has been designed to support this)
        let df1 = frame []
        for symbol in symbols do
          df1.AddColumn(symbol, getStockHistorySeries symbol fromDate toDate)

        // The immutable alternative is to create a sequence of key * series pairs
        // that represent individual columns and then use 'Frame.ofColumns'
        let df2 = 
          [ for symbol in symbols ->
              symbol, getStockHistorySeries symbol fromDate toDate ]
          |> Frame.ofColumns

        let result = R.as_data_frame(R.cor(df1))
        result.GetValue<Frame<string, string>>()