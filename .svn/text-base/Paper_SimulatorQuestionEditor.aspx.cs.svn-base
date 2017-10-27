using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Text.RegularExpressions;
using System.Xml;
using Hints.DB;
using suro.util;
using AuthoringTool.QuestionEditLevel;
//using Hints.Learning.Question;
using PaperSystem;

public partial class AuthoringTool_CaseEditor_Paper_Paper_SimulatorQuestionEditor : AuthoringTool_BasicForm_BasicForm
{
    // Hints parameter
    protected string CaseID = "";
    protected string ClinicNum = "";
    protected string SectionName = "";
    protected string UserID = "";
    protected string strStartTime = "";
    protected string strActionTime = "";
    // VM parameter
    protected string UserLevel = "";
    protected string UserPrivilege = "";
    protected string ViewMode = "";
    protected string Language = "";
    protected string Qid = "";

    string caseID, userID, userLevel, userPrivilege, viewMode, edit, language;
    string width, height;
    string qid = "";
    bool isLogon = false;
    // for save clsLog
    string division, startTime, actionTime, clinicNum, sectionName;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            hf_QID.Value = "";
            //將所有的選項列在表格中        
            if (Session["Simulator"].ToString() != null)
            {
                string str_sim = Session["Simulator"].ToString();
                string[] arr_str_sim = str_sim.Split('|'); //hf_QID.Value
                hf_QID.Value = arr_str_sim[0].ToString();
            }
        }
        //Silverlight1.Style.Add("DISPLAY", "");
        //txtClientWidth.Style.Add("DISPLAY", "");
        //txtClientHeight.Style.Add("DISPLAY", "");

        ////圖形題
        //ViewMode = "Simulator";
        //Silverlight1.Visible = true;
        //txtClientWidth.Visible = true;
        //txtClientHeight.Visible = true;

        ////SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
        //string[] str_arr_VM = hf_QID.Value.ToString().Split('|');
        //InitHintsVariable(str_arr_VM[0].ToString());
        ////呼叫建立問答題表格的Function
        //caseID = "caseID=" + CaseID;
        //userID = "user=" + UserID;
        //userLevel = "userLevel=" + UserLevel;
        //userPrivilege = "userPrivilege=s";
        //viewMode = "viewMode=" + ViewMode;
        //edit = "edit=false";
        //language = "language=" + Language;
        //width = "width=";
        //height = "height=";

        //InitSilverlight();
        setSimulatorButton();
    }
    // INITIAL HINTS參數
    public void InitHintsVariable(string strVMID)
    {
        //if (HttpContext.Current.Session["VM_UserID"] != null)
        //{
        isLogon = true;
        //get VM ID
        //CaseID = usi.CaseID;
        CaseID = strVMID;
        ClinicNum = usi.ClinicNum.ToString();
        SectionName = usi.Section;
        UserID = usi.UserID;
        Hidden_CaseID.Value = CaseID;
        Hidden_ClinicNum.Value = ClinicNum;
        Hidden_SectionName.Value = SectionName;
        Hidden_UserID.Value = UserID;
        strStartTime = usi.StartTime;
        strActionTime = usi.ActionTime;

        UserLevel = usi.UserLevel;
        UserPrivilege = usi.Privilege;
        Language = usi.Language;
        //1
        division = "division=" + usi.Division;
        startTime = "startTime=" + usi.StartTime;
        actionTime = "actionTime=" + usi.ActionTime;
        clinicNum = "clinicNum=" + usi.ClinicNum;
        //modify
        sectionName = "sectionName=Simulator test";
        //}
    }
    // 呈現VM並且INITIAL
    //private void InitSilverlight()
    //{
    //    Silverlight1.Width = Unit.Percentage(100);
    //    Silverlight1.Height = Unit.Percentage(100);
    //    string strW = (string)txtClientWidth.InnerHtml;
    //    string strH = (string)txtClientHeight.InnerHtml;
    //    int w = 1024, h = 768;
    //    if (strW != "")
    //        w = Int32.Parse(strW);
    //    if (strH != "")
    //        h = Int32.Parse(strH);
    //    //Silverlight1.Width = Unit .Pixel (w-5);
    //    //Silverlight1.Height = Unit.Pixel (h-30);
    //    width = width + w;
    //    height = height + h;
    //    this.Silverlight1.InitParameters = caseID + "," + userID + "," + userLevel + "," + userPrivilege + "," + viewMode + "," + division + "," + startTime + "," + actionTime + "," + clinicNum + "," + sectionName + "," + edit + "," + language + "," + width + "," + height;
    //    //if (qid != "")
    //    //    this.Silverlight1.InitParameters += "," + qid;
    //    this.Silverlight1.AutoUpgrade = true;
    //    this.Silverlight1.MinimumVersion = "3.0.40624.0";
    //    //this.Silverlight1.Source = "VMicroscope/VMicroscope.Web/ClientBin/VMicroscope.xap"; 
    //    this.Silverlight1.Source = "../VirtualMicroscope/VMicroscope/VMicroscope.Web/ClientBin/VMicroscope.xap";
    //}
    public void setSimulatorButton()
    {
        clsHintsDB sqlDB = new clsHintsDB();
        string str_ID = Session["Simulator"].ToString();
        string str_URL = "";
        string strSQL = "";
        if (str_ID.Contains("Internal Medicine|General|1"))
        {
            str_ID = "Simulator_20100928144239";
                  
            strSQL = "SELECT * FROM SimulatorBackground WHERE SimulatorID LIKE '" + str_ID + "'";
            DataTable dtbackground = sqlDB.getDataSet(strSQL).Tables[0];
            str_URL = dtbackground.Rows[0]["bgUrl"].ToString();
        }
        else if (str_ID.Contains("Internal Medicine|General|2"))
        {
            str_URL = "http://140.116.72.123/HintsCase/FileCollection/0101/201108/File20110817120244.JPG";
        }
        else
        {
            strSQL = "SELECT * FROM SimulatorBackground WHERE SimulatorID LIKE '" + str_ID + "'";
            DataTable dtbackground = sqlDB.getDataSet(strSQL).Tables[0];
            str_URL = dtbackground.Rows[0]["bgUrl"].ToString();
        }

        

        Table tb_element = new Table();
        PL_table.Controls.Add(tb_element);     
        //tb_element.Attributes.Add("style", "font-weight: bold; color: #ffffcc; width:80%");   //建立Table的CSS
     
        ////tb_element.CssClass = "header1_table";

        //TableRow tr = new TableRow();
        //tb_element.Rows.Add(tr);

        ////TableCell tcCaseTitle = new TableCell();
        ////tr.Cells.Add(tcCaseTitle);
        ////tcCaseTitle.Text = "No.";
        ////tcCaseTitle.Attributes.Add("style", "text-align: center;");

        //TableCell tcDivisionName = new TableCell();
        //tr.Cells.Add(tcDivisionName);
        //tcDivisionName.Text = "Image";
        //tcDivisionName.Attributes.Add("style", "text-align: center;");

        ////TableCell tcSelect = new TableCell();
        ////tr.Cells.Add(tcSelect);
        ////tcSelect.Text = "Select";
        ////tcSelect.Attributes.Add("style", "text-align: center;");
        ////first row color
        //tr.Attributes.Add("Class", "header1_table_first_row");
        if (str_URL != "")
        {
            //for (int i = 0; i < dtbackground.Rows.Count; i++)
            //{
                TableRow tr_New = new TableRow();
                tb_element.Controls.Add(tr_New);

                //TableCell tcNo = new TableCell();
                //Label lb_no = new Label();
                //lb_no.Text =Convert.ToString(i + 1);
                //tcNo.Controls.Add(lb_no);
                //tr_New.Cells.Add(tcNo);

                TableCell tcImage = new TableCell();
                Image Im_background = new Image();
                Im_background.ImageUrl = str_URL;//str_URL
                Im_background.Width = 700;
                Im_background.Height = 560;
                tcImage.Controls.Add(Im_background);

                tr_New.Cells.Add(tcImage);

                //TableCell tcSelectBtn = new TableCell();
                //Button btn_select = new Button();
                //btn_select.Text = "Select";
                //btn_select.ToolTip = "Select this item.";
                //btn_select.Click += new EventHandler(btn_select_Click);
                //btn_select.CommandArgument = dtbackground.Rows[i]["SimulatorID"].ToString();
                //btn_select.Attributes.Add("style", "text-align: center; background-color:  #b0c4de; border: #708090 1px solid;  color: #000; cursor: hand;");
                //btn_select.ID = "Select" + i.ToString();
                //tcSelectBtn.Controls.Add(btn_select);
                //tr_New.Cells.Add(tcSelectBtn);
                //奇偶數設定顏色
                //if(i%2 == 0)
                //    tr_New.Attributes.Add("style", "text-align: center;font-family: Arial;color: #000000; background-color: #FFFFFF;font-size = 20px;");
                //else
                //tr_New.Attributes.Add("style", "text-align: center;font-family: Arial;color: #000000; background-color: #DEC5A7;font-size = 20px;");
            //}
        }
    }
    protected void btn_select_Click(object sender, EventArgs e)
    {
        string SelectID = ((Button)sender).CommandArgument;
        if (hf_QID.Value == "")
        {
            //建立QID
            DataReceiver myReceiver = new DataReceiver();
            string strUserID = usi.UserID.ToString();
            hf_QID.Value = strUserID + "_" + myReceiver.getNowTime();
        }
        Response.Redirect("Paper_SimulatorQuestionEditor2.aspx?cImg=" + SelectID + "&QID=" + hf_QID.Value + "");

    }
}
