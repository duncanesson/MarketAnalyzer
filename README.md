# MarketAnalyzer
A demonstration app integrating Yahoo market data, F#, R, C# and WPF.

This project has the following components:

MarketAnalyzer.Core
A F# Library which connects to Yahoo market data to obtain stock prices, and can connect to the local R engine to calculate Correlation between to symbols.

MarketAnalyzer.Windows.UI
A C# WPF application which demostrates the connectivity to the F# Library to calculate correlation for the supplied input parameters, and dispalys the result.