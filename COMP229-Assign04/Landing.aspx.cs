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
using System.Data;

namespace COMP229_Assign04
{
    public partial class _Default : Page
    {
        DataTable collection;
        string filePath = HttpContext.Current.Server.MapPath("~/Assets/Assign04.json");
        bool showed = false;

        //List<CharModel> collection;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridViewDisplay();
        }
        public DataTable getNames()
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
            listModel.DataSource = getNames();
            listModel.DataBind();
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
            List<CharModel> data = new List<CharModel>();
            CharModel _data = new CharModel();
            try
            {
                if (ValidateField())
                {
                    
                    _data.charName = tbName.Text;
                    _data.faction = tbFaction.Text;
                    _data.rank = int.Parse(tbRank.Text);
                    _data.size = int.Parse(tbSize.Text);
                    _data.deploymentZone = tbDZone.Text;
                    _data._base = int.Parse(tbBase.Text);
                    data.Add(_data);
                    using (StreamWriter writeFile = File.AppendText(filePath))
                    {
                        // deserialize JSON directly from a file
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(writeFile, _data);
                    }
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
    }
}