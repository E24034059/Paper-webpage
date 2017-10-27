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
using suro.util;
using AuthoringTool.QuestionEditLevel;
using Hints.Learning.Question;
using Hints.DB.Section;
using Hints.DB;
using PaperSystem;

public partial class AuthoringTool_CaseEditor_Paper_Paper_SimulatorQuestionEditor2 : AuthoringTool_BasicForm_BasicForm
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            hf_Title.Value = "";
            if (Request.QueryString["cImg"] != null)
            {
                hf_img.Value = Request.QueryString["cImg"].ToString();

            }
            if (Request.QueryString["Title"] != null)
                hf_Title.Value = Request.QueryString["Title"].ToString();
            if (Request.QueryString["QID"] != null)
                hf_QID.Value = Request.QueryString["QID"].ToString();
            //取得simulator的場景及正解還有順序
            //SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
            //SQLString mySQL = new SQLString();
            //string strSQL = mySQL.getQuestion_sim(strQID);
            //DataSet dsQuestion_sim = sqldb.getDataSet(strSQL);
             //initial
            TB_SimulatorTitle.Text = hf_Title.Value;
        }
        //button style
        Btn_back.Attributes.Add("style", "text-align: center; background-color:  #b0c4de; border: #708090 1px solid;  color: #000; cursor: hand; width:80px;");
        Btn_next.Attributes.Add("style", "text-align: center; background-color:  #b0c4de; border: #708090 1px solid;  color: #000; cursor: hand; width:80px;");       
        //讀取場景圖
        editdiplay();
    }
    protected void editdiplay()
    {
        clsHintsDB sqlDB = new clsHintsDB();
        string strSQL = "";
        string str_URL = "";
        DataTable dtTemp = new DataTable();
        if (hf_img.Value.Contains("Internal Medicine|General|1"))
        {
            string str_VRID = "Simulator_20100928144239";
            strSQL = "SELECT * FROM SimulatorBackground WHERE SimulatorID LIKE '" + str_VRID + "'";
            dtTemp = sqlDB.getDataSet(strSQL).Tables[0];
            str_URL = dtTemp.Rows[0]["bgUrl"].ToString();
        }
        else if (hf_img.Value.Contains("Internal Medicine|General|2"))
        {
            str_URL = "http://140.116.72.123/HintsCase/FileCollection/0101/201108/File20110817120244.JPG";
        }
        else
        {
            strSQL = "SELECT * FROM SimulatorBackground WHERE SimulatorID LIKE '" + hf_img.Value + "'";
            dtTemp = sqlDB.getDataSet(strSQL).Tables[0];
            str_URL = dtTemp.Rows[0]["bgUrl"].ToString();
        }
        //DataTable dtTemp = sqlDB.getDataSet(strSQL).Tables[0];
        //DRAW THE IMAGE
        if (str_URL != "")
        {//讀圖並控制大小
            Img_simulator.ImageUrl = str_URL;
            if (Img_simulator.Width.Value > 1024)
                Img_simulator.Width = 1024;
        }
      
    }
    protected void Btn_back_Click(object sender, EventArgs e)
    {
        //go back last step

        Response.Redirect("Paper_SimulatorQE_tree.aspx?QID=" + hf_QID.Value + "");
    }
    protected void Btn_next_Click(object sender, EventArgs e)
    {      
        SqlDB myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);

        string strQuestionMode = "General";
        string strQuestionGroupID = Session["GroupID"].ToString();
        string strQuestionDivisionID = "";
        string strPaperID = "";
        //title update
        hf_Title.Value = TB_SimulatorTitle.Text;
        //update questionIndex and QMode
        clsTextQuestion myText = new clsTextQuestion();
        myText.saveSimulatorQuestion(hf_QID.Value, TB_SimulatorTitle.Text, strPaperID, strQuestionDivisionID, strQuestionGroupID, strQuestionMode);
        //update Question_simulator
        //string strSQL = "SELECT * FROM Question_Simulator WHERE cQID LIKE '" + hf_QID.Value + "'";
        //DataTable dt_update = sqlDB.getDataSet(strSQL).Tables[0];
        //for (int i = 0; i < dt_update.Rows.Count; i++)
        //{
        string strSQL = "UPDATE Question_Simulator SET cContent = @cContent " +
                         "WHERE cQID = '" + hf_QID.Value + "'";
        object[] pList = { hf_Title.Value };
        myDB.ExecuteNonQuery(strSQL, pList);
        //}
            Response.Redirect("Paper_SimulatorQuestionEditor3.aspx?cImg=" + hf_img.Value + "&Title=" + hf_Title.Value + "&QID=" + hf_QID.Value + "");
    }
}
