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
            displayDetail();
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
            string jsonString = File.ReadAllText(fileName);
            //JArray jsonArr = JArray.Parse(jsonString);
            List<CharModel> obj = JsonConvert.DeserializeObject<List<CharModel>>(jsonString);
            //dynamic modelJson;
            ListToDataTable converter = new ListToDataTable();
            //CharModel modelObj = new CharModel();
            foreach (CharModel o in obj.Where(item => item.charName == modelName))
            {
                switch (ObjAttr)
                {
                    case "model":
                        //CharModel modelJson = o;
                        collection = converter.ClassToDataTable<CharModel>(o);
                        break;
                    case "action":
                        //List<Models.Action> modelJson = o.actions;
                        collection = converter.ToDataTable(o.actions);
                        break;
                    case "sa":
                        //modelJson = o.specialAbilities;
                        collection = converter.ToDataTable(o.specialAbilities);
                        break;
                }
            }
            return collection;
        }
        //private DataTable objToDataTable(dynamic obj)
        //{
        //    CharModel objModel = new CharModel();
        //    Models.Action objAction = new Models.Action();
        //    DataTable dt = new DataTable();
        //    Type type = typeof(T);
        //    var properties = type.GetProperties();

        //    foreach (PropertyInfo info in properties)
        //        foreach (var info in obj)
        //    {
        //        dt.Rows.Add(info.Name);
        //    }
        //    dt.AcceptChanges();
        //    return dt;
        //}

        protected void updateBtn_Click(object sender, EventArgs e)
        {

        }
    }
}