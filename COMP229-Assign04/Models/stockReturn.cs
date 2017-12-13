using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comp229_Assign04.Models
{
    public class StockReturn
    {
        [JsonProperty("Meta Data")]
        public MetaData MetaData { get; set; }
        [JsonProperty("Time Series (Daily)")]
        public Dictionary<string, StockData> TimeSeriesDaily { get; set; }
    }

    public class MetaData
    {
        [JsonProperty("1. Information")]
        public string Information { get; set; }
        [JsonProperty("2. Symbol")]
        public string Symbol { get; set; }
        [JsonProperty("3. Last Refreshed")]
        public string LastRefreshed { get; set; }
        [JsonProperty("4. Output Size")]
        public string OutputSize { get; set; }
        [JsonProperty("5. Time Zone")]
        public string TimeZone { get; set; }
    }

    public class StockData
    {
        [JsonProperty("1. open")]
        public string Open { get; set; }
        [JsonProperty("2. high")]
        public string High { get; set; }
        [JsonProperty("3. low")]
        public string Low { get; set; }
        [JsonProperty("4. close")]
        public string Close { get; set; }
        [JsonProperty("5. adjusted close")]
        public string AdjustedClose { get; set; }
        [JsonProperty("6. volume")]
        public string Volume { get; set; }
        [JsonProperty("7. dividend amount")]
        public string DividendAmount { get; set; }
        [JsonProperty("8. split coefficient")]
        public string SplitCoefficient { get; set; }
    }
}