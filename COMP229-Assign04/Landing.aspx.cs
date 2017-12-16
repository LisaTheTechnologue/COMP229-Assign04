using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http; // TODO: Show reference
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Data;
using System.Net.Mail;
using Comp229_Assign04.Models;
using COMP229_Assign04.Models;


namespace COMP229_Assign04
{
    public partial class _Default : Page
    {

        List<CharModel> modelCollection;
        string filePath = HttpContext.Current.Server.MapPath("~/Assets/Assign04.json");
        string fileName = HttpContext.Current.Server.MapPath("~/Assets/newJsonFile.json");
        bool showed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridViewDisplay();
            
        }
        /*deserialize the original file and have the collection of Model*/
        public List<CharModel> getCharModel()
        {
            var jsonString = File.ReadAllText(filePath);
            using (StreamReader file = File.OpenText(filePath))
            {
                // deserialize JSON directly from a file
                JsonSerializer serializer = new JsonSerializer();
                modelCollection = JsonConvert.DeserializeObject<List<CharModel>>(jsonString);
            }
            return modelCollection;
        }
        /*change the object to a DataTable for displaying purpose*/
        public DataTable getNames(string fileName)
        {
            var jsonString = File.ReadAllText(fileName);
            List<CharModel> modelCollectionList = JsonConvert.DeserializeObject<List<CharModel>>(jsonString);
            ListToDataTable converter = new ListToDataTable();
            DataTable collection = converter.ToDataTable(modelCollectionList);
            return collection;
        }
        /*check if new file exist and run the table*/
        public void GridViewDisplay()
        {
            if (File.Exists(fileName))
            {
                Console.Title = "File exists.";
                listModel.DataSource = getNames(fileName);
                listModel.DataBind();
            }
            else
            {
                Console.Title = "File does not exist.";
                createJSON();
            }
        }
        /*show the add form*/
        protected void ShowAdd_Click(object sender, EventArgs e)
        {
            if (showed == false)
            {
                addition.Style.Add("display", "block");
                showed = true;
                Console.Write("showed");
            }
            else addition.Style.Add("display", "none");
        }
        /*user add new model*/
        protected void addModel_Click(object sender, EventArgs e)
        {
            try
            {
                var newModelObj = modelObj(tbName.Text, tbFaction.Text, int.Parse(tbRank.Text), int.Parse(tbSize.Text), tbDZone.Text, int.Parse(tbBase.Text), tbActionName.Text, tbSpcAbl.Text);
                modelCollection.Add(newModelObj);
                GridViewDisplay();
                errorMsg.InnerHtml = "Added new Char" + "<br>";
                SendEmail(sender, e);               
            }
            catch (Exception ex)
            {
                errorMsg.InnerHtml += ex.Message + "<br>";
            }
            finally
            {

            }
        }
        /*user cancel the form*/
        protected void cancelModel_Click(object sender, EventArgs e)
        {
            tbName.Text = "";
            tbFaction.Text = "";
            tbRank.Text = "";
            tbSize.Text = "";
            tbDZone.Text = "";
            tbBase.Text = "";
            errorMsg.InnerHtml = "";
        }
        /*user add new model -> create a new CharModel obj*/
        public CharModel modelObj(string charName, string faction, int rank, int size, string deploymentZone, int _base, string actionName, string spcAbiName)
        {

            Models.Action actionsDef = new Models.Action
            {
                name = actionName,
                type = "",
                rating = 0,
                range = ""
            };
            List<Models.Action> actionsCollection = new List<Models.Action> { };
            actionsCollection.Add(actionsDef);
            Models.SpecialAbility specialAbilitiesDef = new Models.SpecialAbility
            {
                name = spcAbiName,
                description = "N/A"
            };
            List<Models.SpecialAbility> specialAbilities = new List<Models.SpecialAbility> { };
            specialAbilities.Add(specialAbilitiesDef);
            CharModel obj = new CharModel
            {
                charName = charName,
                faction = faction,
                rank = rank,
                _base = _base,
                size = size,
                deploymentZone = "C",
                traits = new string[] { "N/A" },
                types = new string[] { "N/A" },
                defenseChart = new string[] { "N/A" },
                mobility = 0,
                willpower = 0,
                resiliance = 0,
                wounds = 0,
                actions = actionsCollection,
                specialAbilities = specialAbilities,
                imageUrl = "N/A"
            };
            return obj;
        }
        /*create the new file*/
        protected void createJSON()
        {
            getCharModel();
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter streamWriter = File.CreateText(fileName))
            {
                using (JsonWriter writer = new JsonTextWriter(streamWriter))
                {
                    writer.Formatting = Formatting.Indented;
                    //serializer.Serialize(streamWriter,);
                    serializer.Serialize(writer, modelCollection);
                }
            }
            Console.WriteLine("created Json file");
        }

        protected void listModel_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = Convert.ToInt32(e.Row.Cells[0].Text);
                HyperLink hl = (HyperLink)e.Row.FindControl("details");
                hl.NavigateUrl += "Model.aspx?Name=" + index;

            }
        }
        /*Email*/
        protected void SendEmail(object sender, EventArgs e)
        {
            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com", 587);
            MailMessage message = new MailMessage();
            //try
            //{
                //These values are probably set by the client.
                message.Subject = "Updated new Json file!";
                message.Body = "I sent you the updated Json file. Enjoy!";
                Attachment attach = new Attachment(Server.MapPath(fileName));
                message.Attachments.Add(attach);

                //These could be static, or dynamic, depending on situation.
                MailAddress toAddress = new MailAddress("cc-comp229f2016@outlook.com", "You");
                MailAddress fromAddress = new MailAddress("cc-comp229f2016@outlook.com", "Comp229");
                message.From = fromAddress;
                message.To.Add(toAddress);
                smtpClient.Host = "smtp-mail.outlook.com";

                //Note that EnableSsl must be true, and we need to turn of default credentials BEFORE adding the new ones
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential("cc-comp229f2016@outlook.com", "password");

                smtpClient.Send(message);
                errorMsg.InnerHtml += "Email sent." +"<br>";
            //}
            //catch (Exception ex)
            //{
            //    errorMsg.InnerHtml += ex.Message;
            //        //"Coudn't send the message!";
            //}
        }

        public void Hold()
        {
            using (var client = new HttpClient())
            {
                var apiPath = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol=MSFT&apikey=demo";
                var jsonString = client.GetStringAsync(apiPath).Result;
                var stocks = JsonConvert.DeserializeObject<StockReturn>(jsonString);
                var stock = stocks.TimeSeriesDaily.FirstOrDefault(tItem => DateTime.Parse(tItem.Key) < DateTime.Now.AddDays(-1));

                //Note how this variable has a value: FirstOrDefault
                var _default = stocks.TimeSeriesDaily.FirstOrDefault(tItem => DateTime.Parse(tItem.Key) > DateTime.Now);

                //But this variable throws an error: First
                var fail = stocks.TimeSeriesDaily.First(tItem => DateTime.Parse(tItem.Key) > DateTime.Now);


            }
        }

    }
}