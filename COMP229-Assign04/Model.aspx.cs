using COMP229_Assign04.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COMP229_Assign04
{
    public partial class Contact : Page
    {
        string modelName;
        DataTable collection = new DataTable();
        string fileName = HttpContext.Current.Server.MapPath("~/Assets/newJsonFile.json");
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void displayDetail()
        {
            modelName = HttpUtility.UrlDecode(Request.QueryString["Name"]);
            modelDetail.DataSource = getDetails(fileName, "model");
            modelDetail.DataBind();
            actionDetail.DataSource = getDetails(fileName, "action");
            actionDetail.DataBind();
            SADetail.DataSource = getDetails(fileName, "sa");
            SADetail.DataBind();
        }
        public DataTable getDetails(string filePath, string ObjAttr)
        {
            var jsonString = File.ReadAllText(filePath);
            var parsedObject = JObject.Parse(jsonString);
            var modelJson = "";
            CharModel modelObj = new CharModel();
            switch (ObjAttr)
            {
                case "model":
                    modelJson = parsedObject["charName"].ToString();
                    break;                    
                case "action":                
                    modelJson = parsedObject["charName"]["actions"].ToString();
                    break;                    
                case "sa":                    
                    modelJson = parsedObject["charName"]["specialAbilities"].ToString();
                    break;
            }
            modelObj = JsonConvert.DeserializeObject<CharModel>(modelJson);
            collection = objToDataTable(modelObj);
            return collection;
        }
        private DataTable objToDataTable(CharModel obj)
        {
            DataTable dt = new DataTable();
            CharModel objModel = new CharModel();
            dt.Columns.Add("Column_Name");
            foreach (PropertyInfo info in typeof(CharModel).GetProperties())
            {
                dt.Rows.Add(info.Name);
            }
            dt.AcceptChanges();
            return dt;
        }

        protected void updateBtn_Click(object sender, EventArgs e)
        {

        }
    }
}