using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http; // TODO: Show reference
using Newtonsoft.Json;
using System.IO;
using COMP229_Assign04.Models;
using System.Linq;
using System.Net.Mail;

namespace COMP229_Assign04
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // DO NOT use local paths. Use relative pathing; ideally to a file in the project.
            var filePath = @"Assets\Assign04.json";
            //if (File.Exists(filePath))
            if (false)
            {
                var jsonString = File.ReadAllText(filePath);
                //TODO: Get json file contents into string
                var collection = JsonConvert.DeserializeObject<List<CharModel>>(jsonString);
                var i = 0;
            }
            else
            {
                Hold();
            }
        }
        public void Hold()
        {
            using (var client = new HttpClient())
            {
                var apiPath = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol=MSFT&apikey=demo";
                var jsonString = client.GetStringAsync(apiPath).Result;
                var stocks = JsonConvert.DeserializeObject<StockReturn>(jsonString);
                var stock = stocks.TimeSeriesDaily.FirstOrDefault(tItem => DateTime.Parse(tItem.Key) < DateTime.Now.AddDays(-1));

                // Note how this variable has a value: FirstOrDefault
                var _default = stocks.TimeSeriesDaily.FirstOrDefault(tItem => DateTime.Parse(tItem.Key) > DateTime.Now);

                // But this variable throws an error: First
                var fail = stocks.TimeSeriesDaily.First(tItem => DateTime.Parse(tItem.Key) > DateTime.Now);

                var i = 0;
            }
        }
    }
}