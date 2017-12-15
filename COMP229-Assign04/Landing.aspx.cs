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
using COMP229_Assign04.Models;

namespace COMP229_Assign04
{
    public partial class _Default : Page
    {
        DataTable collection;
        List<CharModel> modelCollection;
        string filePath = HttpContext.Current.Server.MapPath("~/Assets/Assign04.json");
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
        public DataTable getNames(string filePath)
        {
            var jsonString = File.ReadAllText(filePath);
            using (StreamReader file = File.OpenText(filePath))
            {
                // deserialize JSON directly from a file
                JsonSerializer serializer = new JsonSerializer();
                collection = JsonConvert.DeserializeObject<DataTable>(jsonString);
            }
            return collection;
        }
        public void GridViewDisplay()
        {
            string fileName = "~/Assets/newJsonFile.json";
            if (File.Exists(fileName))
            {
                Console.WriteLine("File exists.");
                listModel.DataSource = getNames(fileName);
                listModel.DataBind();
            }
            else
                Console.WriteLine("File does not exist.");
            
        }
        protected void showAdd_Click(object sender, EventArgs e)
        {
            if (showed == false)
            {
                addition.Style.Add("display", "block");
                showed = true;
            }
            else addition.Style.Add("display", "none");
        }
        protected void addModel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateField())
                {
                    //CharModel _data = new CharModel
                    //{
                    //charName = tbName.Text,
                    //faction =   tbFaction.Text,
                    //rank =      int.Parse(tbRank.Text),
                    //size =      int.Parse(tbSize.Text),
                    //deploymentZone = tbDZone.Text,
                    //_base =             int.Parse(tbBase.Text)
                    //};
                    modelCollection.Add(modelObj(tbName.Text, tbFaction.Text, int.Parse(tbRank.Text), int.Parse(tbSize.Text), tbDZone.Text, int.Parse(tbBase.Text)));

                    // deserialize JSON directly from a file
                    //JsonSerializer serializer = new JsonSerializer();
                    //serializer.Serialize(writeFile, _data);

                    errorMsg.InnerHtml = "Added new Char";
                }
                else errorMsg.InnerHtml += "Please field all of the fields!";
            }
            catch (Exception ex)
            {
                errorMsg.InnerHtml += ex.Message + "<br>";
            }
            finally
            {

            }

        }
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
        protected bool ValidateField()
        {
            bool validate = true;
            if (
            tbName.Text == "" ||
            tbFaction.Text == "" ||
            tbRank.Text == "" ||
            tbSize.Text == "" ||
            tbDZone.Text == "" ||
            tbBase.Text == "")
            {
                return validate = false;
            }
            else return validate;
        }
        protected CharModel modelObj(string charName, string faction, int rank, int size, string deploymentZone, int _base)
        {
            Models.Action actionsDef = new Models.Action
            {
                name = "Savage Blow",
                type = "Melee",
                rating = 1,
                range = "0"
            };
            List<Models.Action> actionsCollection = new List<Models.Action> { };
            actionsCollection.Add(actionsDef);
            Models.SpecialAbility specialAbilitiesDef = new Models.SpecialAbility
            {
                name = "Pain Fulled",
                description = "While this model has 1 or more damage on it, it gains [+1] Mobility, and its melee attacks gain Unstoppable(1) and [+1] Rating."
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
                traits = ["Union Worker"],
                types = ["Infantry"],
                defenseChart = ["OVERPOWER", "STRIKE", "STRIKE", "STRIKE", "STRIKE", "BLOCK", "BLOCK", "ARMOR", "ARMOR", "ARMOR"],
                mobility = 5,
                willpower = 4,
                resiliance = 1,
                wounds = 2,
                actions = actionsCollection,
                specialAbilities = specialAbilities,
                imageUrl = "http://wrathofkings.com/ks/wp-content/uploads/2013/09/TKworker2_front.jpg"
            };
            return obj;
        }
        protected void createJSON()
        {
            getCharModel();
            using (StreamWriter streamWriter = File.CreateText("~/Assets/newJsonFile.json"))
            {
                foreach (var i in modelCollection)
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(streamWriter, i);
                }
            }

        }
        protected void readJSON()
        {
            //string jrf = System.IO.File.ReadAllText(this.jsonFileLocation);
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //private List jsonAsList = new List();
            //jsonAsList = serializer.Deserialize<List>(jrf.ToString());
        }
        protected void accessDataFromJson()
        {
            /*How to access data stored in the Things Class
        So now that we are able to create, write and read JSON we need to have a mechanism to use the data eg.find values buried in the(manySingleThings) List of(SingleThing) C# classes. This is surprisingly easy.

        We just ask the “jsonAsList” to retrieve the entire C# class which contains a particular variable value that we are interested in.

        SingleThing theNeedle = jsonAsList.Find(x => x.thingName.ToString() == "Trevor");
            Now that we have the new class instantiated we can access its variables*/

            string thingName = theNeedle.thingName;
            //Trevor
            string descriptionOfAThing = theNeedle.thingDescription;
            //Likes cups of tea
        }
        static void Load()
        {
            memberList = JsonConvert.DeserializeObject<List<FacultyMember>>(System.IO.File.ReadAllText(filename));
            AddNew();
            Save();
        }
        protected void checkExistJsonFile()
        {
            string fileName = @"c:\temp\Mahesh.txt";
            if (File.Exists(fileName))
                Console.WriteLine("File exists.");
            else
                Console.WriteLine("File does not exist.");
        }
        protected void SendEmail(object sender, EventArgs e)
        {
            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com", 587);
            MailMessage message = new MailMessage();
            try
            {
                // These values are probably set by the client.
                message.Subject = "Testing!";
                message.Body = "This is the body of a sample message";

                // These could be static, or dynamic, depending on situation.
                MailAddress toAddress = new MailAddress("cc-comp229f2016@outlook.com", "You");
                MailAddress fromAddress = new MailAddress("cc-comp229f2016@outlook.com", "Comp229");
                message.From = fromAddress;
                message.To.Add(toAddress);
                smtpClient.Host = "smtp-mail.outlook.com";

                // Note that EnableSsl must be true, and we need to turn of default credentials BEFORE adding the new ones
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential("cc-comp229f2016@outlook.com", "password");

                smtpClient.Send(message);
                statusLabel.Text = "Email sent.";
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Coudn't send the message!";
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
        protected void add()
        {
            dynamic dynJson = JsonConvert.DeserializeObject(json);
            foreach (var item in dynJson)
            {
                Console.WriteLine("{0} {1} {2} {3}\n", item.id, item.displayName,
                    item.slug, item.imageUrl);
            }
        }
    }
}