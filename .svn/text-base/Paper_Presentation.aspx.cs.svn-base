using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Hints.DB;
using Hints.HintsUtility;
using suro.util;

namespace PaperSystem
{
    /// <summary>
    /// Paper_Presentation 的摘要描述。
    /// </summary>
    public partial class Paper_Presentation : AuthoringTool_BasicForm_BasicForm
    {
        string strUserID, strCaseID, strDivisionID, strClinicNum, strSectionName, strPaperID;
        string strBackText, strNextText, strTitleText;


        protected void Page_Load(object sender, System.EventArgs e)
        {
            //此頁面部份功能隱藏 請搜索20110324 並把註解取消
            this.Initiate();

            Ajax.Utility.RegisterTypeForAjax(typeof(Paper_Presentation));

            //接收參數
            this.getParameter();

            //建立標題和按鈕的多國語言
            this.setupText();

            //建立選項區域
            this.setupPresentationSelection();

            //是否同時存在tableContent和tableRandom
            bool bExist = true;

            //建立Paper_Content
            clsPresentTable myTable = new clsPresentTable();
            Table tableContent = new Table();
            //將OLD的跳題與回饋機制隱藏 20110324
            //tableContent = myTable.setupSuggestedQuestionTableByPaperID(strPaperID, strCaseID, Convert.ToInt32(strClinicNum), strSectionName);
            if (tableContent.Rows.Count > 0)
            {
                tdMainTable.Controls.Add(tableContent);
            }
            else
            {
                trMainTable.Style["DISPLAY"] = "none";
                trMainTableHR.Style["DISPLAY"] = "none";
                bExist = false;
            }

            //建立Paper_RandomNum
            Table tableRandom = myTable.getListTableFromRandomNum(strPaperID);
            if (tableRandom.Rows.Count > 0)
            {
                tdRandomTable.Controls.Add(tableRandom);
            }
            else
            {
                trRandomTable.Style["DISPLAY"] = "none";
                bExist = false;
            }

            //如果兩個表格沒有同時出現，則隱藏兩個表格中間的<HR>
            if (bExist == false)
            {
                trHR.Style["DISPLAY"] = "none";
            }

            btnNext.ServerClick += new EventHandler(btnNext_ServerClick);

            //暫時將ASSIGN QUESTION功能隱藏 不知道功能用途20110324
            btnPreAssign.Style["DISPLAY"] = "none";
        }

        /// <summary>
        /// 建立選項區域
        /// </summary>
        private void setupPresentationSelection()
        {
            clsPresentTable myTable = new clsPresentTable();

            Table table = new Table();
            tdSelect.Controls.Add(table);

            //Switch
            myTable.getSwitchSelectionTable(table, strPaperID);

            //Mark
            myTable.getMarkSelectionTable(table, strPaperID);

            //Modify
            myTable.getModifySelectionTable(table, strPaperID);

            //Assign method
            myTable.getAssignSelectionTable(table, strPaperID);

            //Random select
            myTable.getRandomSelectionSelectionTable(table, strPaperID);

            //Test Duration
            myTable.getTestDurationTable(table, strPaperID);

            //顯示正確答案
            myTable.getCorrectAnswerTable(table, strPaperID);

            //Present method
            myTable.getPresentMethodSelectionTable(table, strPaperID, strCaseID, Convert.ToInt32(strClinicNum), strSectionName);

            //重作機制設定
            myTable.getAnsProcess(table, strPaperID, strCaseID);

            //Table Driven
            //myTable.getTableDriven(table, strPaperID, strCaseID);

            //選擇情境題模式設定
            myTable.getQuestionSituationMode(table, strPaperID, strCaseID);


            //批改問答題時否顯示學生姓名
            myTable.getStudentNameVisibleMode(table, strPaperID, strCaseID);

            //設定CSS
            TableAttribute.setupTopHeaderTableStyle(table, "TableName", 1, "70%", 5, 0, GridLines.Both, false);
        }

        private void setupText()
        {
            strTitleText = "Please setup the presentation for this examination";
            strBackText = "<< Back";
            strNextText = "Finish and save";

            tdTitle.InnerText = strTitleText;
            btnBack.Value = strBackText;
            btnNext.Value = strNextText;
        }

        private void getParameter()
        {
            //PaperID
            if (Session["PaperID"] != null)
            {
                strPaperID = Session["PaperID"].ToString();
                hfPaperID.Value = strPaperID;
            }

            //UserID
            if (Session["UserID"] != null)
            {
                strUserID = Session["UserID"].ToString();
            }

            //CaseID
            if (Session["CaseID"] != null)
            {
                strCaseID = Session["CaseID"].ToString();
                hfCaseID.Value = strCaseID;
            }


            //Division 9801
            if (Session["DivisionID"] != null)
            {
                strDivisionID = Session["DivisionID"].ToString();
                hfDivisionID.Value = strDivisionID;
            }

            //ClinicNum
            if (Session["ClinicNum"] != null)
            {
                strClinicNum = Session["ClinicNum"].ToString();
            }


            //SectionName
            if (Session["SectionName"] != null)
            {
                strSectionName = Session["SectionName"].ToString();
            }


            //Opener
            if (Request.QueryString["Opener"] != null)
            {
                hiddenOpener.Value = Request.QueryString["Opener"].ToString();
            }

            //QuestionMode
            if (Session["QuestionMode"] != null)
            {
                hiddenQuestionMode.Value = Session["QuestionMode"].ToString();
            }

            //PresentType
            if (Session["PresentType"] != null)
            {
                hiddenPresentType.Value = Session["PresentType"].ToString();
            }

            //Edit method
            if (Session["EditMode"] != null)
            {
                hiddenEditMode.Value = Session["EditMode"].ToString();
            }

            //PreOpener
            if (Session["PreOpener"] != null)
            {
                hiddenPreOpener.Value = Session["PreOpener"].ToString();
            }
        }

        #region Web Form 設計工具產生的程式碼
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 此為 ASP.NET Web Form 設計工具所需的呼叫。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 此為設計工具支援所必須的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        private void btnNext_ServerClick(object sender, EventArgs e)
        {
            //儲存此問卷的Presentation資料

            //儲存Paper_Header
            this.savePresentationIntoPaperHeader(this, strPaperID, strCaseID, Convert.ToInt32(strClinicNum), strSectionName);

            //儲存Paper_NextStepBySelectionID
            this.saveNextStepByPaperID(this, strPaperID);
        }

        /// <summary>
        /// 儲存呈現設定到paper_Header
        /// </summary>
        /// <param name="WebPage"></param>
        /// <param name="strPaperID"></param>
        private void savePresentationIntoPaperHeader(System.Web.UI.Page WebPage, string strPaperID, string strCaseID, int intClinicNum, string strSectionName)
        {
            //Switch
            this.saveSwitchPresentation(this, strPaperID);

            //Mark
            this.saveMarkPresentation(this, strPaperID);

            //Modify
            this.saveModifyPresentation(this, strPaperID);

            //Present
            this.savePresentPresentation(this, strPaperID, strCaseID, intClinicNum, strSectionName);

            //Assign
            this.saveAssignPresentation(this, strPaperID);

            //Random selection
            this.saveRandomSelection(this, strPaperID);

            //Test duration
            this.saveTestDurationPresentation(this, strPaperID);

            //Correct Answer
            this.saveCorrectAnswerPresentation(this, strPaperID);

            //SituationQuestionMode
            this.saveSituationQuestionModePresentation(this, strPaperID);

            //StudentNameVisibleMode
            this.saveStudentNameVisibleModePresentation(this, strPaperID);
        }

        /// <summary>
        /// 儲存Switch的設定
        /// </summary>
        public void saveSwitchPresentation(System.Web.UI.Page WebPage, string strPaperID)
        {
            RadioButton rbYes = ((RadioButton)(WebPage.FindControl("Form1").FindControl("rb-Switch-Yes")));

            //此功能是否有被開啟
            int intSelect = 1;

            if (rbYes.Checked)
            {
                intSelect = 1;
            }
            else
            {
                intSelect = 0;
            }

            SqlDB myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
            string strSQL = "UPDATE Paper_Header SET bSwitch = '" + intSelect.ToString() + "' WHERE cPaperID = '" + strPaperID + "' ";
            myDB.ExecuteNonQuery(strSQL);
        }

        /// <summary>
        /// 儲存Mark的設定
        /// </summary>
        public void saveMarkPresentation(System.Web.UI.Page WebPage, string strPaperID)
        {
            RadioButton rbYes = ((RadioButton)(WebPage.FindControl("Form1").FindControl("rb-Mark-Yes")));

            //此功能是否有被開啟
            int intSelect = 1;

            if (rbYes.Checked)
            {
                intSelect = 1;
            }
            else
            {
                intSelect = 0;
            }

            SqlDB myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
            string strSQL = "UPDATE Paper_Header SET bMarkable = '" + intSelect.ToString() + "' WHERE cPaperID = '" + strPaperID + "' ";
            myDB.ExecuteNonQuery(strSQL);
        }

        /// <summary>
        /// 儲存SectionSummaryShowCorrectAnswer的設定
        /// </summary>
        /// <param name="WebPage"></param>
        /// <param name="strPaperID"></param>
        public void saveCorrectAnswerPresentation(System.Web.UI.Page WebPage, string strPaperID)
        {
            RadioButton rbYes = ((RadioButton)(WebPage.FindControl("Form1").FindControl("rb-CorrectAnswer-Yes")));

            //此功能是否有被開啟
            int intSelect = 1;

            if (rbYes.Checked)
            {
                intSelect = 1;
            }
            else
            {
                intSelect = 0;
            }

            SqlDB myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
            string strSQL = "UPDATE Paper_Header SET bSectionSummaryShowCorrectAnswer = '" + intSelect.ToString() + "' WHERE cPaperID = '" + strPaperID + "' ";
            myDB.ExecuteNonQuery(strSQL);
        }

        /// <summary>
        /// 儲存SituationQuestionMode的設定
        /// </summary>
        public void saveSituationQuestionModePresentation(System.Web.UI.Page WebPage, string strPaperID)
        {
            RadioButton rbExam = ((RadioButton)(WebPage.FindControl("Form1").FindControl("rb-SituationMode-Exam")));

            //此功能是否有被開啟
            int intSelect = 1;

            if (rbExam.Checked)
            {
                intSelect = 1;
            }
            else
            {
                intSelect = 0;
            }

            SqlDB myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
            string strSQL = "UPDATE Paper_Header SET bIsSituationExam = '" + intSelect.ToString() + "' WHERE cPaperID = '" + strPaperID + "' ";
            myDB.ExecuteNonQuery(strSQL);
        }

        /// <summary>
        /// 儲存StudentNameVisibleMode的設定
        /// </summary>
        public void saveStudentNameVisibleModePresentation(System.Web.UI.Page WebPage, string strPaperID)
        {
            RadioButton rbVisible = ((RadioButton)(WebPage.FindControl("Form1").FindControl("rb-Visible")));

            //此功能是否有被開啟
            int intSelect = 1;

            if (rbVisible.Checked)
            {
                intSelect = 1;
            }
            else
            {
                intSelect = 0;
            }

            SqlDB myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
            string strSQL = "UPDATE Paper_Header SET bIsStudentNameVisible = '" + intSelect.ToString() + "' WHERE cPaperID = '" + strPaperID + "' ";
            myDB.ExecuteNonQuery(strSQL);
        }

        /// <summary>
        /// 儲存Modify的設定
        /// </summary>
        public void saveModifyPresentation(System.Web.UI.Page WebPage, string strPaperID)
        {
            RadioButton rbYes = ((RadioButton)(WebPage.FindControl("Form1").FindControl("rb-Modify-Yes")));

            //此功能是否有被開啟
            int intSelect = 1;

            if (rbYes.Checked)
            {
                intSelect = 1;
            }
            else
            {
                intSelect = 0;
            }

            SqlDB myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
            string strSQL = "UPDATE Paper_Header SET bModify = '" + intSelect.ToString() + "' WHERE cPaperID = '" + strPaperID + "' ";
            myDB.ExecuteNonQuery(strSQL);
        }

        /// <summary>
        /// 儲存Assign的設定
        /// </summary>
        public void saveAssignPresentation(System.Web.UI.Page WebPage, string strPaperID)
        {
            RadioButton rbYes = ((RadioButton)(WebPage.FindControl("Form1").FindControl("rb-Assign-Yes")));

            //此功能是否有被開啟
            int intSelect = 1;

            if (rbYes.Checked)
            {
                intSelect = 1;
            }
            else
            {
                intSelect = 2;
            }

            SqlDB myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
            string strSQL = "UPDATE Paper_Header SET sAssignMethod = '" + intSelect.ToString() + "' WHERE cPaperID = '" + strPaperID + "' ";
            myDB.ExecuteNonQuery(strSQL);
        }

        /// <summary>
        /// 儲存RandomSelection的設定
        /// </summary>
        public void saveRandomSelection(System.Web.UI.Page WebPage, string strPaperID)
        {
            RadioButton rbYes = ((RadioButton)(WebPage.FindControl("Form1").FindControl("rb-RandomSelection-Yes")));

            //此功能是否有被開啟
            int intSelect = 1;

            if (rbYes.Checked)
            {
                intSelect = 1;
            }
            else
            {
                intSelect = 0;
            }

            SqlDB myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
            string strSQL = "UPDATE Paper_Header SET bRandomSelection = '" + intSelect.ToString() + "' WHERE cPaperID = '" + strPaperID + "' ";
            myDB.ExecuteNonQuery(strSQL);
        }

        /// <summary>
        /// 儲存TestDuration的設定
        /// </summary>
        public void saveTestDurationPresentation(System.Web.UI.Page WebPage, string strPaperID)
        {
            RadioButton rbYes = ((RadioButton)(WebPage.FindControl("Form1").FindControl("rb-Duration-Yes")));

            //TestDuration的時間
            int intDuration = 0;

            DropDownList dlDuration = new DropDownList();
            if (rbYes.Checked)
            {
                //取得時間
                dlDuration = ((DropDownList)(WebPage.FindControl("Form1").FindControl("dlTestDuration")));

                intDuration = Convert.ToInt32(dlDuration.SelectedItem.Value);
            }
            else
            {
                intDuration = 0;
            }

            //			if(intDuration > 0)
            //			{
            //				dlDuration.Attributes["disabled"] = "";
            //			}
            //			else
            //			{
            //				dlDuration.Attributes["disabled"] = "disabled";
            //			}

            SqlDB myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
            string strSQL = "UPDATE Paper_Header SET sTestDuration = '" + intDuration.ToString() + "' WHERE cPaperID = '" + strPaperID + "' ";
            myDB.ExecuteNonQuery(strSQL);
        }

        /// <summary>
        /// 儲存PresentMethod的設定
        /// </summary>
        /// <param name="WebPage"></param>
        /// <param name="strPaperID"></param>
        public void savePresentPresentation(System.Web.UI.Page WebPage, string strPaperID, string strCaseID, int intClinicNum, string strSectionName)
        {
            DropDownList dlPresent = ((DropDownList)(WebPage.FindControl("Form1").FindControl("dlPresent")));

            string strSelectWorkType = dlPresent.SelectedItem.Value;

            SqlDB myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
            string strSQL = "UPDATE UserLevelPresent SET sWorkType = '" + strSelectWorkType + "' WHERE cCaseID = '" + strCaseID + "' AND sClinicNum = '" + intClinicNum.ToString() + "' AND cSectionName = '" + strSectionName + "' ";
            myDB.ExecuteNonQuery(strSQL);
        }

        /// <summary>
        /// 儲存資料至Paper_NextStepBySelectionID
        /// </summary>
        public void saveNextStepByPaperID(System.Web.UI.Page WebPage, string strPaperID)
        {
            DataReceiver myReceiver = new DataReceiver();

            //取得此問卷的所有題目(Paper_Content)
            int intContentQuestionCount = myReceiver.getPaperContentQuestionNum(strPaperID);

            //目前顯示的題號
            int intQuestionIndex = 1;

            //取得此問卷中的所有問題
            for (int i = 1; i <= intContentQuestionCount; i++)
            {
                //取得問題的資料(From Paper_Content)
                DataRow[] drQuestion = myReceiver.getQIDFromPaper_ContentByQuestionIndex(strPaperID, intQuestionIndex);

                if (drQuestion.Length > 0)
                {
                    //取得要顯示問題的QID
                    string strQID = drQuestion[0]["cQID"].ToString();

                    //QuestionType
                    string strQuestionType = drQuestion[0]["cQuestionType"].ToString();

                    //QuestionMode
                    string strQuestionMode = drQuestion[0]["cQuestionMode"].ToString();

                    //問題是選擇題還是問答題?
                    switch (drQuestion[0]["cQuestionType"].ToString())
                    {
                        case "2":
                            //問答題

                            break;
                        default:
                            //選擇題
                            //將OLD的跳題與回饋機制隱藏20110324
                            //this.saveNextStepBySelectionID(WebPage, strPaperID, strQID, intQuestionIndex);
                            break;
                    }
                }

                intQuestionIndex += 1;
            }

            //顯示儲存成功訊息，並且回到MainPage
            string strScript = "<script language='javascript'>\n";
            strScript += "goNext();\n";
            strScript += "</script>\n";
            Page.RegisterStartupScript("goNext", strScript);
        }

        /// <summary>
        /// 儲存一個題目下所有選項的資料至Paper_NextStepBySelectionID
        /// </summary>
        /// <param name="WebPage"></param>
        /// <param name="strPaperID"></param>
        /// <param name="strQID"></param>
        /// <param name="strSelectionID"></param>
        /// <param name="intQuestionIndex"></param>
        public void saveNextStepBySelectionID(System.Web.UI.Page WebPage, string strPaperID, string strQID, int intQuestionIndex)
        {
            //DataReceiver myReceiver = new DataReceiver();

            //選項
            SQLString mySQL = new SQLString();
            string strSQL = mySQL.getAllSelections(strQID);

            SqlDB myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
            DataSet dsSelection = myDB.getDataSet(strSQL);
            if (dsSelection.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsSelection.Tables[0].Rows.Count; i++)
                {
                    string strSelectionID = dsSelection.Tables[0].Rows[i]["cSelectionID"].ToString();
                    int intSelectionIndex = Convert.ToInt32(dsSelection.Tables[0].Rows[i]["sSeq"]);
                    string strSelection = dsSelection.Tables[0].Rows[i]["cSelection"].ToString();
                    bool bSuggested = Convert.ToBoolean(dsSelection.Tables[0].Rows[i]["bCaseSelect"]);

                    //取得題號下拉式選單被選取的內容
                    string strDlIndexID = "dlSelection-Index-" + strSelectionID;
                    DropDownList dlIndex = ((DropDownList)(WebPage.FindControl("Form1").FindControl(strDlIndexID)));
                    int intNextIndex = Convert.ToInt32(dlIndex.SelectedItem.Value);

                    //取得Section下拉式選單被選取的內容
                    string strDlSectionID = "dlSelection-Section-" + strSelectionID;
                    DropDownList dlSection = ((DropDownList)(WebPage.FindControl("Form1").FindControl(strDlSectionID)));
                    string strNextSection = "";
                    if (dlSection.SelectedItem != null)
                        strNextSection = dlSection.SelectedItem.Value;

                    //此選項的NextStep是By Section還是題號
                    int intNextMethod = 2;

                    //檢查此選項的NextStep是Section還是題號
                    string strRbIndexID = "rbIndex-" + strSelectionID;
                    RadioButton rbIndex = ((RadioButton)(WebPage.FindControl("Form1").FindControl(strRbIndexID)));

                    string strRbSectionID = "rbSection-" + strSelectionID;
                    RadioButton rbSection = ((RadioButton)(WebPage.FindControl("Form1").FindControl(strRbSectionID)));

                    string strRbDefaultID = "rbDefault-" + strSelectionID;
                    RadioButton rbDefault = ((RadioButton)(WebPage.FindControl("Form1").FindControl(strRbDefaultID)));

                    if (rbIndex.Checked == true)
                    {
                        if (intNextIndex == 0)
                        {
                            //結束Section
                            intNextMethod = 0;
                        }
                        else
                        {
                            //題號
                            intNextMethod = 2;
                        }
                    }
                    else if (rbSection.Checked == true)
                    {
                        //Section
                        intNextMethod = 1;
                    }
                    else
                    {
                        //Default
                        intNextMethod = 9999;
                        intNextIndex = 9999;
                    }

                    //把資料存入Paper_NextStepBySelectionID
                    SQLString.SaveToPaper_NextStepBySelectionID(strPaperID, strQID, intQuestionIndex, strSelectionID, intSelectionIndex, intNextMethod, strNextSection, intNextIndex);
                }
            }
            dsSelection.Dispose();


        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public void DeleteTDTemplate(string strPaperID, string strCaseID)
        {
            Hints.DB.TableDriven.TD_QandA.TD_QandA_DELETE(strPaperID, strCaseID);
        }

        /// <summary>
        /// 取得AnsProcess_Paper的AnsProcessID
        /// 若無則新增一筆
        /// </summary>
        /// <param name="strPaperID"></param>
        /// <param name="strCaseID"></param>
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public string GetPaperAnsProcessID(string strPaperID, string strCaseID)
        {
            string strAnsProcessID = "";
            string strSQL = "";
            clsHintsDB HintsDB = new clsHintsDB();
            DataTable dt = new DataTable();
            strSQL = "SELECT * FROM AnsProcess_Paper WHERE cCaseID = '" + strCaseID + "' AND cPaperID = '" + strPaperID + "'";
            dt = HintsDB.getDataSet(strSQL).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strAnsProcessID = dt.Rows[0]["cAnsProcessID"].ToString();
            }
            else
            {
                strAnsProcessID = "AnsProcess" + DateTime.Now.ToString("yyyyMMddHHmmss");
                strSQL = "INSERT INTO AnsProcess_Paper (cAnsProcessID, cCaseID, cPaperID) " +
                    " VALUES ('" + strAnsProcessID + "', '" + strCaseID + "', '" + strPaperID + "')";
                HintsDB.ExecuteNonQuery(strSQL);
            }

            return strAnsProcessID;

            dt.Dispose(); 
        }
    }
}
