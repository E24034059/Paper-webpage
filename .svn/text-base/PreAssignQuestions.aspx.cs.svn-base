using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class AuthoringTool_CaseEditor_Paper_PreAssignQuestions : AuthoringTool_BasicForm_BasicForm
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Initiate();

        if (this.IsPostBack == false)
        {
            //預設點選rbClass
            rbClass.Checked = true;

            //建立dlClass1 , dlClass2
            this.setupDlClass1();
            this.setupDlClass2();

            //選取第一個項目
            dlClass1.Items[0].Selected = true;
            dlClass2.Items[0].Selected = true;

            //建立dlGroup
            this.setupDlGroup();

            //將測驗期間日期預設為今天
            tb_StartTime.Value = DateTime.Now.ToShortDateString();
            tb_EndTime.Value = DateTime.Now.ToShortDateString();
            tb_StartTime.Style.Add("cursor","hand");
            tb_EndTime.Style.Add("cursor", "hand");
        }

        dlClass2.SelectedIndexChanged += new EventHandler(dlClass2_SelectedIndexChanged);

        btnFinish.ServerClick += new EventHandler(btnFinish_ServerClick);
    }

    void btnFinish_ServerClick(object sender, EventArgs e)
    {
        //呼叫寫入Paper_AssignedQuestion的函式
        string strSQL = "";
        if (rbClass.Checked == true)
        {
            strSQL = "SELECT DISTINCT cUserID FROM HintsUser WHERE cClass = '" + dlClass1.SelectedItem.Value + "' ";
        }
        else
        {
            strSQL = "SELECT DISTINCT cUserID FROM UserGroup WHERE cGroup = '" + dlGroup.SelectedItem.Value + "' ";
        }

        Hints.Learning.Question.QuestionUtility myUtility = new Hints.Learning.Question.QuestionUtility();
        Hints.DB.clsHintsDB myDB = new Hints.DB.clsHintsDB();
        DataTable dt = myDB.getDataSet(strSQL).Tables[0];

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strUserID = dt.Rows[i]["cUserID"].ToString();

                //確認此問卷是不是有資料存在Paper_AssignedQuestion，如果沒有則寫入資料。
                bool bCheck = Hints.Learning.Question.DataReceiver.checkUserDataFromPaper_AssignedQuestion(usi.PaperID, "00000000000000", strUserID);
                if (bCheck == false)
                {
                    myUtility.setupPaper_AssignedQuestionByPaperIDStartTimeUserID(usi.PaperID, "00000000000000", strUserID);
                }
            }
        }

        //將指派測驗的日期和對象記錄在OnlineQuiz_Assign
        string StartTime = TransferDateFormat(tb_StartTime, ddl_StartHr, ddl_StartMin);
        string EndTime = TransferDateFormat(tb_EndTime, ddl_EndHr, ddl_EndMin);
        string SelectedValue="";
        if (rbClass.Checked == true)
        {
            SelectedValue=dlClass1.SelectedItem.Value;
            strSQL = "INSERT INTO OnlineQuiz_Assign VALUES ('" + usi.CaseID + "','" + usi.PaperID + "','" + StartTime + "','" + EndTime + "','" + SelectedValue+"','class',0)";
        }
        else
        {
            SelectedValue=dlGroup.SelectedItem.Value;
            strSQL = "INSERT INTO OnlineQuiz_Assign VALUES ('" + usi.CaseID + "','" + usi.PaperID + "','" + StartTime + "','" + EndTime + "','" + SelectedValue + "','group',0)";
        }
        try
        {
            myDB.ExecuteNonQuery(strSQL);
        }
        catch
        { }

        string strScript = "<script language='javascript'>\n";
        strScript += "closeWindow();\n";
        strScript += "</script>\n";
        Page.RegisterStartupScript("closeWindow", strScript);
    }

    void dlClass2_SelectedIndexChanged(object sender, EventArgs e)
    {
        setupDlGroup();
        trClass.Style["display"] = "none";
        trGroup.Style["display"] = "";
    }

    private void setupDlClass1()
    {
        dlClass1.Items.Clear();
        string strSQL = "SELECT DISTINCT cClass FROM HintsUser WHERE cClass <> '' AND cClass IS NOT NULL";
        setupDlClassContent(dlClass1, strSQL);
    }

    private void setupDlClass2()
    {
        dlClass2.Items.Clear();
        string strSQL = "SELECT DISTINCT cClass FROM UserGroup INNER JOIN HintsUser ON UserGroup.cUserID = HintsUser.cUserID AND HintsUser.cClass <> '' AND HintsUser.cClass IS NOT NULL";
        setupDlClassContent(dlClass2 , strSQL);
    }

    private void setupDlClassContent(DropDownList dlClass , string strSQL)
    {
        Hints.DB.clsHintsDB myDB = new Hints.DB.clsHintsDB();
        DataTable dt = myDB.getDataSet(strSQL).Tables[0];

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                string strClassName = dt.Rows[i]["cClass"].ToString();

                ListItem liClass = new ListItem(strClassName, strClassName);
                dlClass.Items.Add(liClass);
            }
        }
    }

    private void setupDlGroup()
    {
        dlGroup.Items.Clear();
        string strClassName = dlClass2.SelectedItem.Value;
        string strSQL = "SELECT DISTINCT UserGroup.cGroup FROM UserGroup INNER JOIN HintsUser ON UserGroup.cUserID = HintsUser.cUserID AND HintsUser.cClass = '" + strClassName + "' AND HintsUser.cClass <> '' AND HintsUser.cClass IS NOT NULL";
        setupDlGroupContent(dlGroup , strSQL);
    }

    private void setupDlGroupContent(DropDownList dlGroup , string strSQL)
    {
        Hints.DB.clsHintsDB myDB = new Hints.DB.clsHintsDB();
        DataTable dt = myDB.getDataSet(strSQL).Tables[0];

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strGroupName = dt.Rows[i]["cGroup"].ToString();

                ListItem liGroup = new ListItem(strGroupName, strGroupName);
                dlGroup.Items.Add(liGroup);
            }
        }
    }

    //將控制項選出的日期轉為HINTS所接受的14數字格式
    private string TransferDateFormat(HtmlInputText tb_Date,DropDownList ddl_hr,DropDownList ddl_min)
    {
        string Date = tb_Date.Value;
        string[] tmparray = Date.Split('/');//[0]:月  [1]:日  [2]:年

        //將個位數的月日前面補零
        if (tmparray[0].Length == 1)
            tmparray[0] = "0" + tmparray[0];
        if (tmparray[1].Length == 1)
            tmparray[1] = "0" + tmparray[1];

        string hr = ddl_hr.SelectedValue;
        string min = ddl_min.SelectedValue;

        //月+日+年+時+分+"00"
        return tmparray[2] + tmparray[0] + tmparray[1] + hr + min + "00";
    }
}
