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
using suro.util;
using Hints.DB;

namespace PaperSystem
{
    /// <summary>
    /// Paper_SelectQuestion 的摘要描述。
    /// 使用者自行勾選題目進入此問卷的網頁。
    /// </summary>
    public partial class Paper_SelectQuestion : AuthoringTool_BasicForm_BasicForm
    {
        //建立SqlDB物件
        SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationManager.AppSettings["connstr"]);
        //SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
        DataReceiver myReceiver = new DataReceiver();
        SQLString mySQL = new SQLString();

        string SessionQuestionType = "";
        string strUserID, strCaseID, strClinicNum, strSectionName, strEditMode, strPaperID;
        string strGroupID = "";
        string strGroupDivisionID = "";
        bool bAllowSelect = true;

        int intQuestionIndex = 0;

        //編輯考卷時才會用到(非必須)
        string strCourseID = "";

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.Initiate();

            //接收參數
            this.getParameter();

            if (Session["PaperID"] != null)
            {
                strPaperID = Session["PaperID"].ToString();
            }

            //strPaperID = "wyt20060510150619";

            //顯示Title
            string strDivisionName = mySQL.getDivisionName(strGroupDivisionID);
            spanDivisionName.InnerText = strDivisionName;

            //string strGroupName = mySQL.getQuestionGroupName(Session["GroupID"].ToString());
            string strGroupName = mySQL.getQuestionGroupName(strGroupID);
            spanGroupName.InnerText = strGroupName;
            if (Session["fromVRSimulator"] != null)
            {
                if (Session["fromVRSimulator"].ToString().Contains("vr"))
                    SessionQuestionType = "5";
            }
            //顯示此組別下的所有問題
            setupQuestionTable();

            //建立Finish button的事件
            btnSubmit.ServerClick += new ImageClickEventHandler(btnSubmit_ServerClick);
        }

        private void setupQuestionTable()
        {
            Table table = new Table();
            tcQuestionTable.Controls.Add(table);
            table.CellSpacing = 0;
            table.CellPadding = 2;
            table.BorderStyle = BorderStyle.Solid;
            table.BorderWidth = Unit.Pixel(1);
            table.BorderColor = System.Drawing.Color.Black;
            table.GridLines = GridLines.None;
            table.Width = Unit.Percentage(100);

            //建立Table的CSS
            //table.Attributes.Add("Class","header1_table");

            //依照QuestionMode決定取出此組別的General或是Specific的所有選擇題
            string strSQL = "";
            if (hiddenQuestionMode.Value == "Specific")
            {
                strSQL = mySQL.getSpecificSelectionQuestion(strPaperID);
            }
            else if (SessionQuestionType == "1")
            {
                //strSQL = mySQL.getGroupSelectionQuestion(Session["GroupID"].ToString());
                strSQL = mySQL.getGroupSelectionQuestion(strGroupID);
            }
            else if (SessionQuestionType == "6")
            {
                //strSQL = mySQL.getGroupSelectionQuestion(Session["GroupID"].ToString());
                strSQL = mySQL.getGroupSelectionWithKeyWordsQuestion(strGroupID);
            }


            ////use JS alert() in C#
            ScriptManager.RegisterStartupScript(
             this,
             typeof(Page),
             "Alert",
            "<script>alert('SessionQuestionType: " + SessionQuestionType + "');</script>",
             false);


            #region 列出選擇題
            if (SessionQuestionType == "" || SessionQuestionType == "1")
            {
                DataTable dsQuestionList = new DataTable();

                //判斷是利用何種模式搜尋，Group為直接利用群組搜尋，否則為特徵值搜尋
                if (Request.QueryString["SearchMode"] == "Group")
                {
                    dsQuestionList = sqldb.getDataSet(strSQL).Tables[0];
                }
                else
                {
                    //取得上一個頁面所搜尋的結果資料
                    dsQuestionList = (DataTable)Session["dtSelectedFeatureItemResult"];
                }
                if (dsQuestionList.Rows.Count > 0)
                {
                    for (int i = 0; i < dsQuestionList.Rows.Count; i++)
                    {
                        //若cQuestionType不等於1(不等於選擇題)，則跳過此題目
                        if (dsQuestionList.Rows[i]["cQuestionType"].ToString() != "1")
                            continue;
                        //取得QuestionType
                        string strQuestionType = "1";
                        try
                        {
                            strQuestionType = dsQuestionList.Rows[i]["cQuestionType"].ToString();
                        }
                        catch
                        {
                        }

                        //取得QID
                        string strQID = "";
                        try
                        {
                            strQID = dsQuestionList.Rows[i]["cQID"].ToString();
                        }
                        catch
                        {
                        }

                        //取得問題的SQL
                        strSQL = mySQL.getQuestion(strQID);
                        DataSet dsQuestion = sqldb.getDataSet(strSQL);

                        if (dsQuestion.Tables[0].Rows.Count > 0)
                        {
                            //建立問題的內容
                            TableRow trQuestion = new TableRow();
                            table.Rows.Add(trQuestion);

                            intQuestionIndex += 1;

                            //問題的CheckBox
                            TableCell tcCheckBox = new TableCell();
                            trQuestion.Cells.Add(tcCheckBox);
                            tcCheckBox.Width = Unit.Pixel(25);

                            if (bAllowSelect == true)
                            {
                                CheckBox chQuestion = new CheckBox();
                                tcCheckBox.Controls.Add(chQuestion);
                                string strID = "";
                                try
                                {
                                    strID = "ch-" + dsQuestion.Tables[0].Rows[0]["cQID"].ToString();
                                }
                                catch
                                {
                                }
                                chQuestion.ID = strID;
                                //如果此問題己經存在於Paper_Content，則CheckBox被勾選。
                                chQuestion.Checked = myReceiver.checkExistPaperContent(strPaperID, strQID);

                            }

                            //問題的題號
                            TableCell tcQuestionNum = new TableCell();
                            trQuestion.Cells.Add(tcQuestionNum);
                            tcQuestionNum.Width = Unit.Pixel(25);
                            tcQuestionNum.Text = "Q" + intQuestionIndex.ToString() + ": ";

                            //問題的內容
                            string strQuestion = "";
                            try
                            {
                                strQuestion = dsQuestion.Tables[0].Rows[0]["cQuestion"].ToString();
                            }
                            catch
                            {
                            }
                            TableCell tcQuestion = new TableCell();
                            trQuestion.Cells.Add(tcQuestion);
                            tcQuestion.Text = strQuestion;

                            //建立問題的CSS
                            trQuestion.Attributes.Add("Class", "header1_table_first_row");

                            //建立選項
                            strSQL = mySQL.getAllSelections(strQID);
                            DataSet dsSelection = sqldb.getDataSet(strSQL);
                            if (dsSelection.Tables[0].Rows.Count > 0)
                            {
                                for (int j = 0; j < dsSelection.Tables[0].Rows.Count; j++)
                                {
                                    //Seq
                                    string strSeq = "";
                                    try
                                    {
                                        strSeq = dsSelection.Tables[0].Rows[j]["sSeq"].ToString();
                                    }
                                    catch
                                    {
                                    }

                                    //Selection
                                    string strSelection = "";
                                    try
                                    {
                                        strSelection = dsSelection.Tables[0].Rows[j]["cSelection"].ToString();
                                    }
                                    catch
                                    {
                                    }

                                    //bCaseSelect
                                    bool bCaseSelect = false;
                                    try
                                    {
                                        bCaseSelect = Convert.ToBoolean(dsSelection.Tables[0].Rows[j]["bCaseSelect"]);
                                    }
                                    catch
                                    {
                                    }

                                    TableRow trSelection = new TableRow();
                                    table.Rows.Add(trSelection);

                                    //是否為建議選項
                                    TableCell tcSuggest = new TableCell();
                                    trSelection.Cells.Add(tcSuggest);
                                    HtmlImage imgSuggest = new HtmlImage();
                                    tcSuggest.Controls.Add(imgSuggest);
                                    if (bCaseSelect == true)
                                    {
                                        imgSuggest.Src = "/Hints/Summary/Images/smiley4.gif";
                                    }
                                    else
                                    {
                                        imgSuggest.Src = "/Hints/Summary/Images/smiley11.gif";
                                    }

                                    //選項編號
                                    TableCell tcSelectionNum = new TableCell();
                                    trSelection.Cells.Add(tcSelectionNum);
                                    tcSelectionNum.Text = Convert.ToString((j + 1)) + ".";

                                    //選項內容
                                    TableCell tcSelection = new TableCell();
                                    trSelection.Cells.Add(tcSelection);
                                    tcSelection.Text = strSelection;

                                    //建立選項的CSS
                                    if ((Convert.ToInt32(strSeq) % 2) != 0)
                                    {
                                        trSelection.Attributes.Add("Class", "header1_tr_odd_row");
                                    }
                                    else
                                    {
                                        trSelection.Attributes.Add("Class", "header1_tr_even_row");
                                    }
                                }
                            }
                            else
                            {
                                //此問題沒有選項
                            }
                            dsSelection.Dispose();
                        }
                        else
                        {
                            //此問題沒有選項
                        }
                        dsQuestion.Dispose();

                    }
                }
                else
                {
                    //此問卷沒有任何選擇題的情況
                }
                dsQuestionList.Dispose();
            }
            #endregion

            #region 列出選擇題包含關鍵字
            if (SessionQuestionType == "6")
            {
                DataTable dsQuestionList = new DataTable();

                //判斷是利用何種模式搜尋，Group為直接利用群組搜尋，否則為特徵值搜尋
                if (Request.QueryString["SearchMode"] == "Group")
                {
                    dsQuestionList = sqldb.getDataSet(strSQL).Tables[0];
                }
                else
                {
                    //取得上一個頁面所搜尋的結果資料
                    dsQuestionList = (DataTable)Session["dtSelectedFeatureItemResult"];
                }
                if (dsQuestionList.Rows.Count > 0)
                {
                    for (int i = 0; i < dsQuestionList.Rows.Count; i++)
                    {
                        //若cQuestionType不等於1(不等於選擇題包含關鍵字)，則跳過此題目
                        if (dsQuestionList.Rows[i]["cQuestionType"].ToString() != "6")
                            continue;
                        //取得QuestionType
                        string strQuestionType = "6";
                        try
                        {
                            strQuestionType = dsQuestionList.Rows[i]["cQuestionType"].ToString();
                        }
                        catch
                        {
                        }

                        //取得QID
                        string strQID = "";
                        try
                        {
                            strQID = dsQuestionList.Rows[i]["cQID"].ToString();
                        }
                        catch
                        {
                        }

                        //取得問題的SQL
                        strSQL = mySQL.getQuestion(strQID);
                        DataSet dsQuestion = sqldb.getDataSet(strSQL);

                        if (dsQuestion.Tables[0].Rows.Count > 0)
                        {
                            //建立問題的內容
                            TableRow trQuestion = new TableRow();
                            table.Rows.Add(trQuestion);

                            intQuestionIndex += 1;

                            //問題的CheckBox
                            TableCell tcCheckBox = new TableCell();
                            trQuestion.Cells.Add(tcCheckBox);
                            tcCheckBox.Width = Unit.Pixel(25);

                            if (bAllowSelect == true)
                            {
                                CheckBox chQuestion = new CheckBox();
                                tcCheckBox.Controls.Add(chQuestion);
                                string strID = "";
                                try
                                {
                                    strID = "ch-" + dsQuestion.Tables[0].Rows[0]["cQID"].ToString();
                                }
                                catch
                                {
                                }
                                chQuestion.ID = strID;
                                //如果此問題己經存在於Paper_Content，則CheckBox被勾選。
                                chQuestion.Checked = myReceiver.checkExistPaperContent(strPaperID, strQID);

                            }

                            //問題的題號
                            TableCell tcQuestionNum = new TableCell();
                            trQuestion.Cells.Add(tcQuestionNum);
                            tcQuestionNum.Width = Unit.Pixel(25);
                            tcQuestionNum.Text = "Q" + intQuestionIndex.ToString() + ": ";

                            //問題的內容
                            string strQuestion = "";
                            try
                            {
                                strQuestion = dsQuestion.Tables[0].Rows[0]["cQuestion"].ToString();
                            }
                            catch
                            {
                            }
                            TableCell tcQuestion = new TableCell();
                            trQuestion.Cells.Add(tcQuestion);
                            tcQuestion.Text = strQuestion;

                            //關鍵字標題
                            TableCell tcKeyWordsTitle = new TableCell();
                            trQuestion.Cells.Add(tcKeyWordsTitle);
                            tcKeyWordsTitle.Text = "KeyWords";
                            tcKeyWordsTitle.Attributes["align"] = "center";
                            tcKeyWordsTitle.Width = Unit.Pixel(300);
                            tcKeyWordsTitle.BorderWidth = Unit.Pixel(2);
                            tcKeyWordsTitle.BorderColor = Color.Black;

                            //建立問題的CSS
                            trQuestion.Attributes.Add("Class", "header1_table_first_row");

                            //建立選項
                            strSQL = mySQL.getAllSelections(strQID);
                            DataSet dsSelection = sqldb.getDataSet(strSQL);
                            if (dsSelection.Tables[0].Rows.Count > 0)
                            {
                                for (int j = 0; j < dsSelection.Tables[0].Rows.Count; j++)
                                {
                                    //Seq
                                    string strSeq = "";
                                    try
                                    {
                                        strSeq = dsSelection.Tables[0].Rows[j]["sSeq"].ToString();
                                    }
                                    catch
                                    {
                                    }

                                    //Selection
                                    string strSelection = "";
                                    try
                                    {
                                        strSelection = dsSelection.Tables[0].Rows[j]["cSelection"].ToString();
                                    }
                                    catch
                                    {
                                    }

                                    //bCaseSelect
                                    bool bCaseSelect = false;
                                    try
                                    {
                                        bCaseSelect = Convert.ToBoolean(dsSelection.Tables[0].Rows[j]["bCaseSelect"]);
                                    }
                                    catch
                                    {
                                    }

                                    TableRow trSelection = new TableRow();
                                    table.Rows.Add(trSelection);

                                    //是否為建議選項
                                    TableCell tcSuggest = new TableCell();
                                    trSelection.Cells.Add(tcSuggest);
                                    HtmlImage imgSuggest = new HtmlImage();
                                    tcSuggest.Controls.Add(imgSuggest);
                                    if (bCaseSelect == true)
                                    {
                                        imgSuggest.Src = "/Hints/Summary/Images/smiley4.gif";
                                    }
                                    else
                                    {
                                        imgSuggest.Src = "/Hints/Summary/Images/smiley11.gif";
                                    }

                                    //選項編號
                                    TableCell tcSelectionNum = new TableCell();
                                    trSelection.Cells.Add(tcSelectionNum);
                                    tcSelectionNum.Text = Convert.ToString((j + 1)) + ".";

                                    //選項內容
                                    TableCell tcSelection = new TableCell();
                                    trSelection.Cells.Add(tcSelection);
                                    tcSelection.Text = strSelection;


                                    //在選項第一列加入關鍵字欄位
                                    if (j == 0)
                                    {
                                        //取得題目關鍵字
                                        string strKeyWords = "";
                                        strKeyWords = dsQuestionList.Rows[i]["cKeyWords"].ToString();
                                        TableCell tcKeyWords = new TableCell();
                                        trSelection.Cells.Add(tcKeyWords);
                                        tcKeyWords.RowSpan = dsSelection.Tables[0].Rows.Count;
                                        tcKeyWords.Text = strKeyWords;
                                        tcKeyWords.Width = Unit.Pixel(300);
                                        tcKeyWords.BorderWidth = Unit.Pixel(2);
                                        tcKeyWords.BorderColor = Color.Black;
                                    }

                                    //建立選項的CSS
                                    if ((Convert.ToInt32(strSeq) % 2) != 0)
                                    {
                                        trSelection.Attributes.Add("Class", "header1_tr_odd_row");
                                    }
                                    else
                                    {
                                        trSelection.Attributes.Add("Class", "header1_tr_even_row");
                                    }
                                }
                            }
                            else
                            {
                                //此問題沒有選項
                            }
                            dsSelection.Dispose();
                        }
                        else
                        {
                            //此問題沒有選項
                        }
                        dsQuestion.Dispose();

                    }
                }
                else
                {
                    //此問卷沒有任何選擇題的情況
                }
                dsQuestionList.Dispose();
            }
            #endregion

            #region 列出問答題
            else if (SessionQuestionType == "" || SessionQuestionType == "2")
            {





                //建立屬於此組別的問答題
                if (Request.QueryString["SearchMode"] == "Group")
                {
                    strSQL = mySQL.getGroupTextQuestion(strGroupID);
                }
                else
                {
                    strSQL = mySQL.getFeatureTextQuestion((DataTable)Session["dtSelectedFeatureItemResult"]);
                    // (DataTable)Session["dtSelectedFeatureItemResult"]
                }

                DataSet dsTextList = sqldb.getDataSet(strSQL);

                int intTextCount = 0;
                if (dsTextList.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsTextList.Tables[0].Rows.Count; i++)
                    {
                        //取得此問題的QID
                        string strQID = "";
                        try
                        {
                            strQID = dsTextList.Tables[0].Rows[i]["cQID"].ToString();
                        }
                        catch
                        {
                        }

                        TableRow trQuestion = new TableRow();
                        table.Rows.Add(trQuestion);

                        //CheckBox
                        TableCell tcCheck = new TableCell();
                        trQuestion.Cells.Add(tcCheck);

                        if (bAllowSelect == true)
                        {
                            CheckBox chText = new CheckBox();
                            tcCheck.Controls.Add(chText);
                            string strID = "";
                            strID = "ch-" + strQID;
                            chText.ID = strID;
                            //如果此問題己經存在於Paper_Content，則CheckBox被勾選。
                            chText.Checked = myReceiver.checkExistPaperContent(strPaperID, strQID);
                        }

                        //Question number
                        intQuestionIndex += 1;
                        TableCell tcTextNum = new TableCell();
                        trQuestion.Cells.Add(tcTextNum);
                        tcTextNum.Text = "Q" + intQuestionIndex.ToString() + ": ";

                        //Question
                        TableCell tcQuestion = new TableCell();
                        trQuestion.Cells.Add(tcQuestion);
                        string strQuestion = "";
                        try
                        {
                            strQuestion = dsTextList.Tables[0].Rows[i]["cQuestion"].ToString();
                        }
                        catch
                        {
                        }
                        tcQuestion.Text = strQuestion;

                        //加入CSS
                        intTextCount += 1;
                        if ((intTextCount % 2) != 0)
                        {
                            trQuestion.Attributes.Add("Class", "header1_table_first_row");
                        }
                        else
                        {
                            trQuestion.Attributes.Add("Class", "header1_tr_even_row");
                        }
                    }
                }
                else
                {
                    //此問卷沒有問答題的情形
                }
                dsTextList.Dispose();
            }
            #endregion
            else if (SessionQuestionType == "3")
            {
                this.setupSimulatorQuestionTable();
            }
            else if (SessionQuestionType == "5")
            {
                this.setupSituationQuestionTable();
            }
        }

        private void getParameter()
        {
            //GroupID
            if (Request.QueryString["GroupID"] != null)
            {
                strGroupID = Request.QueryString["GroupID"].ToString();
                if (Session["GroupID"] != null)
                {
                    Session["GroupID"] = strGroupID;
                }
                else
                {
                    Session.Add("GroupID", strGroupID);
                }
            }

            //GroupDivisionID
            if (strGroupID.Trim().Length > 0)
            {
                string strGroupDivisionID = myReceiver.getGroupDivisionID(strGroupID);
                if (Session["GroupDivisionID"] != null)
                {
                    Session["GroupDivisionID"] = strGroupDivisionID;
                }
                else
                {
                    Session.Add("GroupDivisionID", strGroupDivisionID);
                }
            }

            //UserID
            if (Session["UserID"] != null)
            {
                strUserID = Session["UserID"].ToString();
            }
            //strUserID = "swakevin";

            //CaseID kyhCase200505301448128593750
            if (Session["CaseID"] != null)
            {
                strCaseID = Session["CaseID"].ToString();
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

            //GenerationMethod
            if (Request.QueryString["GenerationMethod"] != null)
            {
                string strGenerationMethod = Request.QueryString["GenerationMethod"].ToString();
                if (strGenerationMethod == "Edit")
                {
                    bAllowSelect = true;
                }
                else
                {
                    bAllowSelect = false;
                }
            }

            //Opener
            if (Session["Opener"] != null)
            {
                hiddenOpener.Value = Session["Opener"].ToString();
            }

            //Setup opener
            if (Session["Opener"] != null)
            {
                Session["Opener"] = "Paper_SelectQuestion";
            }
            else
            {
                Session.Add("Opener", "Paper_SelectQuestion");
            }

            //QuestionMode
            if (Session["QuestionMode"] != null)
            {
                hiddenQuestionMode.Value = Session["QuestionMode"].ToString();
            }

            //QuestionType
            if (Session["QuestionType"] != null)
                SessionQuestionType = Session["QuestionType"].ToString();

            //PreOpener
            if (Session["PreOpener"] != null)
            {
                hiddenPreOpener.Value = Session["PreOpener"].ToString();
            }

            //CourseID
            if (Request.QueryString["cCourseID"] != null)
            {
                strCourseID = Request.QueryString["cCourseID"].ToString();
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

        private void btnSubmit_ServerClick(object sender, ImageClickEventArgs e)
        {
            //按下Submit按鈕的事件

            //選擇題和問答題各選了幾題
            int intSelectionCount = 0;
            int intTextCount = 0;
            int intSimulationCount = 0;
            int intSituationCount = 0;

            //取得此組別的選擇題(DataSet)
            //string strSQL = mySQL.getGroupSelectionQuestion(strGroupID);

            #region 將所勾選的選擇題儲存
            //依照QuestionMode決定取出此組別的General或是Specific的所有選擇題
            string strSQL = "";
            if (hiddenQuestionMode.Value == "Specific")
            {
                strSQL = mySQL.getSpecificSelectionQuestion(strPaperID);
            }
            else
            {
                //若SearchMode為Group則利用Group為條件搜尋，否則以上一頁的搜尋結果為條件
                if (Request.QueryString["SearchMode"] == "Group")
                {
                    strSQL = mySQL.getGroupSelectionQuestion(Session["GroupID"].ToString());
                }
                else
                {
                    strSQL = mySQL.getFeatureSelectionQuestion((DataTable)Session["dtSelectedFeatureItemResult"]);
                }
            }

            string strQuestionGroupQIDList = "";
            DataSet dsQuestionList = sqldb.getDataSet(strSQL);

            if (dsQuestionList.Tables[0].Rows.Count > 0 && (SessionQuestionType == "" || SessionQuestionType == "1"))
            {
                for (int i = 0; i < dsQuestionList.Tables[0].Rows.Count; i++)
                {
                    //取得QID
                    string strQID = "";
                    try
                    {
                        strQID = dsQuestionList.Tables[0].Rows[i]["cQID"].ToString();
                    }
                    catch
                    {
                    }

                    //檢查此題目是否有被勾選
                    bool bCheck = false;
                    try
                    {
                        bCheck = ((CheckBox)(this.FindControl("Form1").FindControl("ch-" + strQID))).Checked;
                    }
                    catch
                    {
                        Response.Write("<span style='DISPLAY: none'>讀取" + strQID + "的CheckBox失敗</span>");
                    }

                    if (bCheck == true)
                    {
                        //有被勾選，將資料存入資料庫中。

                        intSelectionCount += 1;

                        //Standard score
                        string strScore = "0";

                        //QuestionType
                        string strQuestionType = "";
                        try
                        {
                            strQuestionType = dsQuestionList.Tables[0].Rows[i]["cQuestionType"].ToString();
                        }
                        catch
                        {
                        }

                        //QuestionMode
                        string strQuestionMode = "";
                        try
                        {
                            strQuestionMode = dsQuestionList.Tables[0].Rows[i]["cQuestionMode"].ToString();
                        }
                        catch
                        {
                        }

                        //Seq
                        string strSeq = Convert.ToString(myReceiver.getPaperContentMaxSeq(strPaperID) + 1);

                        //Question
                        string strQuestion = "";
                        try
                        {
                            strQuestion = dsQuestionList.Tables[0].Rows[i]["cQuestion"].ToString();
                        }
                        catch
                        {
                        }

                        //將此題目的資料存入資料庫
                        mySQL.SaveToQuestionContent(strPaperID, strQID, strScore, strQuestionType, strQuestionMode, strQuestion, strSeq);
                        //mySQL.SaveToQuestionContent(strPaperID , strQID , strScore , strQuestionType , strQuestionMode , strSeq);

                        //檢查此問題是否為題組 是則自動新增題組題目至資料表
                        DataTable dtQuestionGroup = CheckQuestionGroup(strQID);
                        if (dtQuestionGroup.Rows.Count > 0)
                        {
                            strQuestionGroupQIDList += strQID + "|";
                            foreach (DataRow drQuestionGroup in dtQuestionGroup.Rows)
                            {
                                string strSelectionRelatedQID = drQuestionGroup["cQID"].ToString();
                                //Seq
                                string strSelectionRelatedQSeq = Convert.ToString(myReceiver.getPaperContentMaxSeq(strPaperID) + 1);
                                //QuestionType
                                string strSelectionRelatedQType = "";

                                DataTable dtQuestionMode = Hints.Learning.Question.SQLString.getQuestionClassifyName(strSelectionRelatedQID);
                                strSelectionRelatedQType = dtQuestionMode.Rows[0]["cQuestionType"].ToString();

                                mySQL.SaveToQuestionContent(strPaperID, strSelectionRelatedQID, strScore, strSelectionRelatedQType, "General", strSelectionRelatedQSeq);

                            }
                        }

                    }
                    else
                    {
                        //檢查問題是否已在題組裡
                        bool bExistQGQuestion = false;
                        bExistQGQuestion = CheckQuestionExistGroup(strQuestionGroupQIDList, strQID);

                        if (!bExistQGQuestion)
                        {
                            //將資料自Paper_Conent刪除
                            mySQL.DeleteFromQuestionContent(strPaperID, strQID);
                        }
                    }
                }
            }
            else
            {
                //此組別沒有題目的情形
            }
            dsQuestionList.Dispose();
            #endregion

            #region 將所勾選的選擇題包含關鍵字儲存
            //依照QuestionMode決定取出此組別的General或是Specific的所有選擇題
            if (hiddenQuestionMode.Value == "Specific")
            {
                strSQL = mySQL.getSpecificSelectionQuestion(strPaperID);
            }
            else
            {
                //若SearchMode為Group則利用Group為條件搜尋，否則以上一頁的搜尋結果為條件
                if (Request.QueryString["SearchMode"] == "Group")
                {
                    strSQL = mySQL.getGroupSelectionWithKeyWordsQuestion(Session["GroupID"].ToString());
                }
                else
                {
                    strSQL = mySQL.getFeatureSelectionWithKeyWordsQuestion((DataTable)Session["dtSelectedFeatureItemResult"]);
                }
            }

            string strQuestionGroupWithKeyWordsQIDList = "";
            DataSet dsQuestionWithKeyWordsList = sqldb.getDataSet(strSQL);

            if (dsQuestionWithKeyWordsList.Tables[0].Rows.Count > 0 && SessionQuestionType == "6")
            {
                for (int i = 0; i < dsQuestionWithKeyWordsList.Tables[0].Rows.Count; i++)
                {
                    //取得QID
                    string strQID = "";
                    try
                    {
                        strQID = dsQuestionWithKeyWordsList.Tables[0].Rows[i]["cQID"].ToString();
                    }
                    catch
                    {
                    }

                    //檢查此題目是否有被勾選
                    bool bCheck = false;
                    try
                    {
                        bCheck = ((CheckBox)(this.FindControl("Form1").FindControl("ch-" + strQID))).Checked;
                    }
                    catch
                    {
                        Response.Write("<span style='DISPLAY: none'>讀取" + strQID + "的CheckBox失敗</span>");
                    }

                    if (bCheck == true)
                    {
                        //有被勾選，將資料存入資料庫中。

                        intSelectionCount += 1;

                        //Standard score
                        string strScore = "0";

                        //QuestionType
                        string strQuestionType = "";
                        try
                        {
                            strQuestionType = dsQuestionWithKeyWordsList.Tables[0].Rows[i]["cQuestionType"].ToString();
                        }
                        catch
                        {
                        }

                        //QuestionMode
                        string strQuestionMode = "";
                        try
                        {
                            strQuestionMode = dsQuestionWithKeyWordsList.Tables[0].Rows[i]["cQuestionMode"].ToString();
                        }
                        catch
                        {
                        }

                        //Seq
                        string strSeq = Convert.ToString(myReceiver.getPaperContentMaxSeq(strPaperID) + 1);

                        //Question
                        string strQuestion = "";
                        try
                        {
                            strQuestion = dsQuestionWithKeyWordsList.Tables[0].Rows[i]["cQuestion"].ToString();
                        }
                        catch
                        {
                        }

                        //將此題目的資料存入資料庫
                        mySQL.SaveToQuestionContent(strPaperID, strQID, strScore, strQuestionType, strQuestionMode, strQuestion, strSeq);

                        //檢查此問題是否為題組 是則自動新增題組題目至資料表
                        DataTable dtQuestionGroup = CheckQuestionGroup(strQID);
                        if (dtQuestionGroup.Rows.Count > 0)
                        {
                            strQuestionGroupWithKeyWordsQIDList += strQID + "|";
                            foreach (DataRow drQuestionGroup in dtQuestionGroup.Rows)
                            {
                                string strSelectionRelatedQID = drQuestionGroup["cQID"].ToString();
                                //Seq
                                string strSelectionRelatedQSeq = Convert.ToString(myReceiver.getPaperContentMaxSeq(strPaperID) + 1);
                                //QuestionType
                                string strSelectionRelatedQType = "";

                                DataTable dtQuestionMode = Hints.Learning.Question.SQLString.getQuestionClassifyName(strSelectionRelatedQID);
                                strSelectionRelatedQType = dtQuestionMode.Rows[0]["cQuestionType"].ToString();

                                mySQL.SaveToQuestionContent(strPaperID, strSelectionRelatedQID, strScore, strSelectionRelatedQType, "General", strSelectionRelatedQSeq);

                            }
                        }

                    }
                    else
                    {
                        //檢查問題是否已在題組裡
                        bool bExistQGQuestion = false;
                        bExistQGQuestion = CheckQuestionExistGroup(strQuestionGroupWithKeyWordsQIDList, strQID);

                        if (!bExistQGQuestion)
                        {
                            //將資料自Paper_Conent刪除
                            mySQL.DeleteFromQuestionContent(strPaperID, strQID);
                        }
                    }
                }
            }
            else
            {
                //此組別沒有題目的情形
            }
            dsQuestionWithKeyWordsList.Dispose();
            #endregion

            #region 將所勾選的問答題儲存
            //取得此組別的問答題
            if (Request.QueryString["SearchMode"] == "Group")
            {
                strSQL = mySQL.getGroupTextQuestion(Session["GroupID"].ToString());
            }
            else
            {
                strSQL = mySQL.getFeatureTextQuestion((DataTable)Session["dtSelectedFeatureItemResult"]);
            }


            DataSet dsTextList = sqldb.getDataSet(strSQL);

            if (dsTextList.Tables[0].Rows.Count > 0 && (SessionQuestionType == "" || SessionQuestionType == "2"))
            {
                for (int i = 0; i < dsTextList.Tables[0].Rows.Count; i++)
                {
                    //取得QID
                    string strQID = "";
                    try
                    {
                        strQID = dsTextList.Tables[0].Rows[i]["cQID"].ToString();
                    }
                    catch
                    {
                    }

                    //檢查此題目是否有被勾選
                    bool bCheck = false;
                    try
                    {
                        bCheck = ((CheckBox)(this.FindControl("Form1").FindControl("ch-" + strQID))).Checked;
                    }
                    catch
                    {
                        Response.Write("<span style='DISPLAY: none'>讀取" + strQID + "的CheckBox失敗</span>");
                    }

                    if (bCheck == true)
                    {
                        intTextCount += 1;

                        //Standard score
                        string strScore = "0";

                        //QuestionType
                        string strQuestionType = "";
                        try
                        {
                            strQuestionType = dsTextList.Tables[0].Rows[i]["cQuestionType"].ToString();
                        }
                        catch
                        {
                        }

                        //QuestionMode
                        string strQuestionMode = "";
                        try
                        {
                            strQuestionMode = dsTextList.Tables[0].Rows[i]["cQuestionMode"].ToString();
                        }
                        catch
                        {
                        }

                        //Seq
                        string strSeq = Convert.ToString(myReceiver.getPaperContentMaxSeq(strPaperID) + 1);

                        //Question
                        string strQuestion = "";
                        try
                        {
                            strQuestion = dsTextList.Tables[0].Rows[i]["cQuestion"].ToString();
                        }
                        catch
                        {
                        }

                        //將此題目的資料存入資料庫
                        mySQL.SaveToQuestionContent(strPaperID, strQID, strScore, strQuestionType, strQuestionMode, strQuestion, strSeq);
                    }
                    else
                    {
                        //將資料自Paper_Conent刪除
                        mySQL.DeleteFromQuestionContent(strPaperID, strQID);
                    }
                }
            }
            else
            {
                //此組別沒有問答題的情形
            }
            dsTextList.Dispose();
            #endregion
            #region 將所勾選的圖形題儲存
            //取得此組別的圖形題
            strSQL = mySQL.getGroupSimulationQuestion(Session["GroupID"].ToString());
            DataSet ds_simulation_List = sqldb.getDataSet(strSQL);
            if (ds_simulation_List.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds_simulation_List.Tables[0].Rows.Count; i++)
                {
                    //取得QID
                    string strQID = "";
                    try
                    {
                        strQID = ds_simulation_List.Tables[0].Rows[i]["cQID"].ToString();
                    }
                    catch
                    {
                    }

                    //檢查此題目是否有被勾選
                    bool bCheck = false;
                    try
                    {
                        bCheck = ((CheckBox)(this.FindControl("Form1").FindControl("ch-" + strQID))).Checked;
                    }
                    catch
                    {
                        Response.Write("<span style='DISPLAY: none'>讀取" + strQID + "的CheckBox失敗</span>");
                    }

                    if (bCheck == true)
                    {
                        intSimulationCount += 1;

                        //Standard score
                        string strScore = "0";

                        //QuestionType
                        string strQuestionType = "";
                        try
                        {
                            strQuestionType = ds_simulation_List.Tables[0].Rows[i]["cQuestionType"].ToString();
                        }
                        catch
                        {
                        }

                        //QuestionMode
                        string strQuestionMode = "";
                        try
                        {
                            strQuestionMode = ds_simulation_List.Tables[0].Rows[i]["cQuestionMode"].ToString();
                        }
                        catch
                        {
                        }

                        //Seq
                        string strSeq = Convert.ToString(myReceiver.getPaperContentMaxSeq(strPaperID) + 1);

                        //Question
                        string strQuestion = "";
                        try
                        {
                            strQuestion = ds_simulation_List.Tables[0].Rows[i]["cContent"].ToString();
                        }
                        catch
                        {
                        }

                        //將此題目的資料存入資料庫
                        //mySQL.SaveToQuestionContent(strPaperID , strQID , strScore , strQuestionType , strQuestionMode , strQuestion , strSeq);
                        mySQL.SaveToQuestionContent(strPaperID, strQID, strScore, strQuestionType, strQuestionMode, strSeq);
                        //檢查此問題是否為題組 是則自動新增題組題目至資料表
                        DataTable dtQuestionGroup = CheckQuestionGroup(strQID);
                        if (dtQuestionGroup.Rows.Count > 0)
                        {
                            strQuestionGroupQIDList += strQID + "|";
                            foreach (DataRow drQuestionGroup in dtQuestionGroup.Rows)
                            {
                                string strSelectionRelatedQID = drQuestionGroup["cQID"].ToString();
                                //Seq
                                string strSelectionRelatedQSeq = Convert.ToString(myReceiver.getPaperContentMaxSeq(strPaperID) + 1);
                                //QuestionType
                                string strSelectionRelatedQType = "";

                                DataTable dtQuestionMode = Hints.Learning.Question.SQLString.getQuestionClassifyName(strSelectionRelatedQID);
                                strSelectionRelatedQType = dtQuestionMode.Rows[0]["cQuestionType"].ToString();

                                mySQL.SaveToQuestionContent(strPaperID, strSelectionRelatedQID, strScore, strSelectionRelatedQType, "General", strSelectionRelatedQSeq);

                            }
                        }
                    }
                    else
                    {
                        //檢查問題是否已在題組裡
                        bool bExistQGQuestion = false;
                        bExistQGQuestion = CheckQuestionExistGroup(strQuestionGroupQIDList, strQID);

                        if (!bExistQGQuestion)
                        {
                            //將資料自Paper_Conent刪除
                            mySQL.DeleteFromQuestionContent(strPaperID, strQID);
                        }
                        ////將資料自Paper_Conent刪除
                        // mySQL.DeleteFromQuestionContent(strPaperID, strQID);
                    }
                }
            }
            #endregion
            #region 將所勾選的情境題儲存
            //取得此組別的問答題
            /*if (Request.QueryString["SearchMode"] == "Group")
            {
                strSQL = mySQL.getGroupTextQuestion(Session["GroupID"].ToString());
            }
            else
            {
                strSQL = mySQL.getFeatureTextQuestion((DataTable)Session["dtSelectedFeatureItemResult"]);
            }*/

            strSQL = mySQL.getGroupSituationQuestion(Session["GroupID"].ToString());
            DataSet dsSituationList = sqldb.getDataSet(strSQL);
            if (dsSituationList.Tables[0].Rows.Count > 0 && (SessionQuestionType == "" || SessionQuestionType == "5"))
            {
                for (int i = 0; i < dsSituationList.Tables[0].Rows.Count; i++)
                {
                    //取得QID
                    string strQID = "";
                    strQID = dsSituationList.Tables[0].Rows[i]["cQID"].ToString();
                    //檢查此題目是否有被勾選
                    bool bCheck = false;
                    try
                    {
                        bCheck = ((CheckBox)(this.FindControl("Form1").FindControl("ch-" + strQID))).Checked;
                    }
                    catch
                    {
                        Response.Write("<span style='DISPLAY: none'>讀取" + strQID + "的CheckBox失敗</span>");
                    }

                    if (bCheck == true)
                    {
                        intSituationCount += 1;

                        //Standard score
                        string strScore = "0";

                        //QuestionType
                        string strQuestionType = "";
                        try
                        {
                            strQuestionType = dsSituationList.Tables[0].Rows[i]["cQuestionType"].ToString();
                        }
                        catch
                        {
                        }

                        //QuestionMode
                        string strQuestionMode = "";
                        try
                        {
                            strQuestionMode = dsSituationList.Tables[0].Rows[i]["cQuestionMode"].ToString();
                        }
                        catch
                        {
                        }

                        //Seq
                        string strSeq = Convert.ToString(myReceiver.getPaperContentMaxSeq(strPaperID) + 1);

                        //Question
                        string strQuestion = "";
                        try
                        {
                            strQuestion = dsSituationList.Tables[0].Rows[i]["cQuestion"].ToString();
                        }
                        catch
                        {
                        }

                        //將此題目的資料存入資料庫
                        //mySQL.SaveToQuestionContent(strPaperID , strQID , strScore , strQuestionType , strQuestionMode , strQuestion , strSeq);
                        mySQL.SaveToQuestionContent(strPaperID, strQID, strScore, strQuestionType, strQuestionMode, strSeq);
                    }
                    else
                    {
                        //將資料自Paper_Conent刪除
                        mySQL.DeleteFromQuestionContent(strPaperID, strQID);
                    }
                }
            }

            #endregion
            #region 20110407 暫時處理模式
            //20110407 因為程式運作模式 在新增題目時 若同一群組沒有一次選完題目 問題順序會無法照順序排
            //所以當新增完題目後 重新設定排序
            //此部份必須崇新改寫程式 
            //若不需要重新排序 請將此段註解
            DataTable dtPaper_Content = new DataTable();
            string strPaper_Content = "SELECT * FROM Paper_Content WHERE cPaperID = '" + strPaperID + "' ORDER BY cQID";
            clsHintsDB HintsDB = new clsHintsDB();
            dtPaper_Content = HintsDB.getDataSet(strPaper_Content).Tables[0];
            if (dtPaper_Content.Rows.Count > 0)
            {
                int iNewSeq = 1;
                foreach (DataRow drPaper_Content in dtPaper_Content.Rows)
                {
                    string strQID = drPaper_Content["cQID"].ToString();
                    strPaper_Content = "UPDATE Paper_Content SET sSeq = '" + iNewSeq + "' WHERE cPaperID = '" + strPaperID + "' AND cQID = '" + strQID + "'";
                    HintsDB.ExecuteNonQuery(strPaper_Content);
                    iNewSeq++;
                }

            }
            #endregion

            //顯示共選擇了幾題選擇題與幾題問答題
            /*
			string strScript="<script language='javascript'>\n";
			strScript+="showFinishMsg('" + intSelectionCount.ToString() + "','" + intTextCount.ToString() + "');\n";
			strScript+="</script>\n";
			Page.RegisterStartupScript("showFinishMsg",strScript);
			*/
            if (hiddenPreOpener.Value == "SelectPaperMode")
                Response.Redirect("Paper_MainPage.aspx?Opener=SelectPaperMode&cCaseID=" + strCaseID + "&cSectionName=" + strSectionName + "&cPaperID=" + strPaperID + "&bIsAutoSetScore=true");
            else
                Response.Redirect("Paper_OtherQuestion.aspx?Opener=Paper_SelectQuestion");
        }

        //檢查是否為題組
        protected DataTable CheckQuestionGroup(string strQID)
        {
            DataTable dtQuestionGroup = new DataTable();
            string strSQL_Paper_QuestionSelectionGroupIDJoinPaper_QuestionSelectionGroupItem = "SELECT dbo.Paper_QuestionSelectionGroupID.cSelectionID, " +
            "dbo.Paper_QuestionSelectionGroupID.cQID AS cQGroupID, dbo.Paper_QuestionSelectionGroupItem.cGroupID, " +
            "dbo.Paper_QuestionSelectionGroupItem.cQID, dbo.Paper_QuestionSelectionGroupItem.cSequence " +
            "FROM dbo.Paper_QuestionSelectionGroupID INNER JOIN " +
            "dbo.Paper_QuestionSelectionGroupItem ON dbo.Paper_QuestionSelectionGroupID.cGroupID = dbo.Paper_QuestionSelectionGroupItem.cGroupID " +
            "WHERE (dbo.Paper_QuestionSelectionGroupID.cQID = '" + strQID + "') " +
            "ORDER BY dbo.Paper_QuestionSelectionGroupID.cSelectionID";
            clsHintsDB HintsDB = new clsHintsDB();
            DataTable dtPaper_QuestionSelectionGroupID = HintsDB.getDataSet(strSQL_Paper_QuestionSelectionGroupIDJoinPaper_QuestionSelectionGroupItem).Tables[0];
            if (dtPaper_QuestionSelectionGroupID.Rows.Count > 0)
            {
                dtQuestionGroup = dtPaper_QuestionSelectionGroupID;
            }
            return dtQuestionGroup;
        }


        //檢查要刪除的問題ID是否已是題組的題目
        protected bool CheckQuestionExistGroup(string strQuestionGroupQIDList, string strQID)
        {
            bool bExistQGQuestion = false;
            string[] strArrayQuestionGroupQID = strQuestionGroupQIDList.Split('|');
            for (int iQGCount = 0; iQGCount < strArrayQuestionGroupQID.Length; iQGCount++)
            {
                if (strArrayQuestionGroupQID[iQGCount] != "")
                {
                    string strSQL_Paper_QuestionSelectionGroupIDJoinPaper_QuestionSelectionGroupItem = "SELECT dbo.Paper_QuestionSelectionGroupID.cSelectionID, " +
                    "dbo.Paper_QuestionSelectionGroupID.cQID AS cQGroupID, dbo.Paper_QuestionSelectionGroupItem.cGroupID, " +
                    "dbo.Paper_QuestionSelectionGroupItem.cQID, dbo.Paper_QuestionSelectionGroupItem.cSequence " +
                    "FROM dbo.Paper_QuestionSelectionGroupID INNER JOIN " +
                    "dbo.Paper_QuestionSelectionGroupItem ON dbo.Paper_QuestionSelectionGroupID.cGroupID = dbo.Paper_QuestionSelectionGroupItem.cGroupID " +
                    "WHERE (dbo.Paper_QuestionSelectionGroupID.cQID = '" + strArrayQuestionGroupQID[iQGCount].ToString() + "') AND " +
                    "(dbo.Paper_QuestionSelectionGroupItem.cQID = '" + strQID + "')" +
                    "ORDER BY dbo.Paper_QuestionSelectionGroupID.cSelectionID";
                    clsHintsDB HintsDB = new clsHintsDB();
                    DataTable dtPaper_QuestionSelectionGroupID = HintsDB.getDataSet(strSQL_Paper_QuestionSelectionGroupIDJoinPaper_QuestionSelectionGroupItem).Tables[0];
                    if (dtPaper_QuestionSelectionGroupID.Rows.Count > 0)
                    {
                        bExistQGQuestion = true;
                    }
                    dtPaper_QuestionSelectionGroupID.Dispose();
                }
            }
            return bExistQGQuestion;
        }
        protected void setupSimulatorQuestionTable()
        {
            tcSimQuestionTable.Controls.Clear();
            Table table = new Table();
            tcSimQuestionTable.Controls.Add(table);
            table.CellSpacing = 0;
            table.CellPadding = 2;
            table.BorderStyle = BorderStyle.Solid;
            table.BorderWidth = Unit.Pixel(1);
            table.BorderColor = System.Drawing.Color.Black;
            table.GridLines = GridLines.Both;
            table.Width = Unit.Percentage(100);

            //建立Table的CSS
            table.CssClass = "header1_table";

            //依照QuestionMode決定取出此組別的圖形題
            string strSQL = mySQL.getGroupQuestionSimulator(Session["GroupID"].ToString());


            DataSet dsQuestionList = sqldb.getDataSet(strSQL);
            if (dsQuestionList.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsQuestionList.Tables[0].Rows.Count; i++)
                {
                    //取得QuestionType
                    string strQuestionType = "3";
                    strQuestionType = dsQuestionList.Tables[0].Rows[i]["cQuestionType"].ToString();

                    //取得QID
                    string strQID = "";
                    strQID = dsQuestionList.Tables[0].Rows[i]["cQID"].ToString();

                    //取得問題的SQL
                    DataSet dsQuestion = null;
                    //if (hfSymptoms.Value == "All")
                    strSQL = mySQL.getQuestion(strQID);
                    //NO Symptoms
                    //else
                    //    strSQL = mySQL.getQuestionBySymptoms(strQID, hfSymptoms.Value);

                    dsQuestion = sqldb.getDataSet(strSQL);
                    if (dsQuestion.Tables[0].Rows.Count > 0)
                    {
                        //建立問題的內容
                        TableRow trQuestion = new TableRow();
                        trQuestion.Style.Add("CURSOR", "hand");
                        table.Rows.Add(trQuestion);

                        intQuestionIndex += 1;

                        //問題的題號
                        TableCell tcQuestionNum = new TableCell();
                        trQuestion.Cells.Add(tcQuestionNum);
                        tcQuestionNum.Width = Unit.Pixel(150);
                        //tcQuestionNum.Text = "Q" + intQuestionIndex.ToString() + ": ";
                        tcQuestionNum.Text = "<IMG id='img_" + strQID + "' src='../../../BasicForm/Image/minus.gif'>&nbsp;Q" + intQuestionIndex.ToString() + " : ";
                        //CheckBox
                        //TableCell tcCheck = new TableCell();
                        //trQuestion.Cells.Add(tcCheck);

                        if (bAllowSelect == true)
                        {
                            CheckBox chText = new CheckBox();
                            tcQuestionNum.Controls.Add(chText);
                            string strID = "";
                            strID = "ch-" + strQID;
                            chText.ID = strID;
                            //如果此問題己經存在於Paper_Content，則CheckBox被勾選。
                            chText.Checked = myReceiver.checkExistPaperContent(strPaperID, strQID);
                        }
                        //問題的內容
                        string strQuestion = "";
                        strQuestion = dsQuestion.Tables[0].Rows[0]["cQuestion"].ToString();

                        TableCell tcQuestion = new TableCell();
                        trQuestion.Cells.Add(tcQuestion);
                        tcQuestion.Text = strQuestion;

                        //建立問題的CSS
                        trQuestion.Attributes.Add("Class", "header1_table_first_row");

                        //取得simulator的場景及正解還有順序
                        strSQL = mySQL.getQuestion_sim(strQID);
                        DataSet dsQuestion_sim = sqldb.getDataSet(strSQL);
                        //建立圖形題的場景
                        if (dsQuestion_sim.Tables[0].Rows.Count > 0)
                        {
                            TableRow trScene = new TableRow();
                            trScene.Height = 290;
                            table.Rows.Add(trScene);

                            TableCell tcSceneTitle = new TableCell();
                            trScene.Cells.Add(tcSceneTitle);
                            tcSceneTitle.Text = "<font style='color:Black; font-weight:bold'>Scene :&nbsp;<font/>";
                            tcSceneTitle.Style.Add("text-align", "right");
                            tcSceneTitle.Width = Unit.Pixel(230);
                            //取得場景ID
                            string strSimuID = dsQuestion_sim.Tables[0].Rows[0]["cSimulatorID"].ToString();
                            string str_URL = "";
                            DataTable dtTemp = new DataTable();
                            if (strSimuID.Contains("Internal Medicine|General|1"))
                            {
                                string str_VRID = "Simulator_20100928144239";
                                strSQL = "SELECT * FROM SimulatorBackground WHERE SimulatorID LIKE '" + str_VRID + "'";
                                dtTemp = sqldb.getDataSet(strSQL).Tables[0];
                                str_URL = dtTemp.Rows[0]["bgUrl"].ToString();
                            }
                            else if (strSimuID.Contains("Internal Medicine|General|2"))
                            {
                                str_URL = "http://140.116.72.28/HintsCase/FileCollection/0101/201108/File20110817120244.JPG";
                            }
                            else
                            {
                                strSQL = "SELECT * FROM SimulatorBackground WHERE SimulatorID LIKE '" + strSimuID + "'";
                                dtTemp = sqldb.getDataSet(strSQL).Tables[0];
                                str_URL = dtTemp.Rows[0]["bgUrl"].ToString();
                            }
                            //strSQL = "SELECT * FROM SimulatorBackground WHERE SimulatorID LIKE '" + strSimuID + "'";
                            //DataTable dtTemp = sqldb.getDataSet(strSQL).Tables[0];

                            TableCell tcSceneImg = new TableCell();
                            trScene.Cells.Add(tcSceneImg);
                            HtmlImage h_image = new HtmlImage();
                            h_image.Src = str_URL;
                            h_image.Width = 320;
                            h_image.Height = 280;
                            tcSceneImg.Controls.Add(h_image);
                            tcSceneImg.Attributes.Add("Class", "header1_tr_even_row");
                            tcSceneImg.Width = Unit.Percentage(81);
                            //縮放
                            //trQuestion.Attributes.Add("onclick", "ShowDetail('" + strQID + "','img_" + strQID + "','" + hfAnswerCount.Value + "')");
                            //建立圖形題的正確解答以及順序
                            if (dsQuestion_sim.Tables[0].Rows.Count > 0)
                            {
                                int s_no = 0;
                                for (int s = 0; s < dsQuestion_sim.Tables[0].Rows.Count; s++)
                                {
                                    s_no = s + 1;
                                    //答案行
                                    TableRow trAnswer = new TableRow();
                                    table.Rows.Add(trAnswer);

                                    TableCell tcAnswerTitle = new TableCell();
                                    trAnswer.Cells.Add(tcAnswerTitle);
                                    tcAnswerTitle.Text = "<font style='color:Black; font-weight:bold'>Answer " + s_no.ToString() + " :&nbsp;<font/>";
                                    tcAnswerTitle.Style.Add("text-align", "right");
                                    tcAnswerTitle.Width = Unit.Pixel(230);
                                    //置入答案
                                    TableCell tcAnswerValue = new TableCell();
                                    trAnswer.Cells.Add(tcAnswerValue);
                                    tcAnswerValue.Attributes.Add("Class", "header1_tr_even_row");
                                    tcAnswerValue.Width = Unit.Percentage(81);
                                    string temp_ans = dsQuestion_sim.Tables[0].Rows[s]["cAnswer"].ToString().Replace('|', ',');
                                    tcAnswerValue.Text = temp_ans.Substring(0, temp_ans.Length - 1);
                                    //答案順序行
                                    TableRow trAnswerOrder = new TableRow();
                                    table.Rows.Add(trAnswerOrder);

                                    TableCell tcAns_order_Title = new TableCell();
                                    trAnswerOrder.Cells.Add(tcAns_order_Title);
                                    tcAns_order_Title.Text = "<font style='color:Black; font-weight:bold'>Answer " + s_no.ToString() + " order:&nbsp;<font/>";
                                    tcAns_order_Title.Style.Add("text-align", "right");
                                    tcAns_order_Title.Width = Unit.Pixel(230);
                                    //置入順序
                                    TableCell tcAnswer_Order = new TableCell();
                                    trAnswerOrder.Cells.Add(tcAnswer_Order);
                                    tcAnswer_Order.Attributes.Add("Class", "header1_tr_even_row");
                                    tcAnswer_Order.Width = Unit.Percentage(81);
                                    string temp_order = dsQuestion_sim.Tables[0].Rows[s]["cOrder"].ToString().Replace('|', ',');
                                    tcAnswer_Order.Text = temp_order.Substring(0, temp_order.Length - 1);
                                }
                            }
                        }
                    }
                }
            }

        }

        #region 列出情境題
        //創造情境題
        protected void setupSituationQuestionTable()
        {
            Table table = new Table();
            tcQuestionTable.Controls.Add(table);
            table.CellSpacing = 0;
            table.CellPadding = 2;
            table.BorderStyle = BorderStyle.Solid;
            table.BorderWidth = Unit.Pixel(1);
            table.BorderColor = System.Drawing.Color.Black;
            table.GridLines = GridLines.None;
            table.Width = Unit.Percentage(100);

            string strSQL;
            /* //建立屬於此組別的問答題
             if (Request.QueryString["SearchMode"] == "Group")
             {
                 strSQL = mySQL.getGroupTextQuestion(strGroupID);
             }
             else
             {
                 strSQL = mySQL.getFeatureTextQuestion((DataTable)Session["dtSelectedFeatureItemResult"]);
                 // (DataTable)Session["dtSelectedFeatureItemResult"]
             }*/

            strSQL = mySQL.getGroupSituationQuestion(strGroupID);
            DataSet dsSituationList = sqldb.getDataSet(strSQL);

            int intSituationCount = 0;
            if (dsSituationList.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsSituationList.Tables[0].Rows.Count; i++)
                {
                    //取得此問題的QID
                    string strQID = "";
                    try
                    {
                        strQID = dsSituationList.Tables[0].Rows[i]["cQID"].ToString();
                    }
                    catch
                    {
                    }

                    TableRow trQuestion = new TableRow();
                    table.Rows.Add(trQuestion);

                    //CheckBox
                    TableCell tcCheck = new TableCell();
                    trQuestion.Cells.Add(tcCheck);

                    if (bAllowSelect == true)
                    {
                        CheckBox chSituation = new CheckBox();
                        tcCheck.Controls.Add(chSituation);
                        string strID = "";
                        strID = "ch-" + strQID;
                        chSituation.ID = strID;
                        //如果此問題己經存在於Paper_Content，則CheckBox被勾選。
                        chSituation.Checked = myReceiver.checkExistPaperContent(strPaperID, strQID);
                    }

                    //Question number
                    intQuestionIndex += 1;
                    TableCell tcTextNum = new TableCell();
                    trQuestion.Cells.Add(tcTextNum);
                    tcTextNum.Text = "Q" + intQuestionIndex.ToString() + ": ";

                    //Question
                    TableCell tcQuestion = new TableCell();
                    trQuestion.Cells.Add(tcQuestion);
                    string strQuestion = "";
                    try
                    {
                        strQuestion = dsSituationList.Tables[0].Rows[i]["cQuestion"].ToString();
                    }
                    catch
                    {
                    }
                    tcQuestion.Text = strQuestion;

                    //加入CSS
                    intSituationCount += 1;
                    if ((intSituationCount % 2) != 0)
                    {
                        trQuestion.Attributes.Add("Class", "header1_table_first_row");
                    }
                    else
                    {
                        trQuestion.Attributes.Add("Class", "header1_tr_even_row");
                    }
                }
            }
            else
            {
                if (Session["fromVRSimulator"] != null)
                    //此問卷沒有問答題的情形
                    Response.Redirect("Paper_MainPage.aspx?Opener=Paper_Main&CaseID=" + Session["fromVRSimulator"].ToString().Replace("vr", ""));
            }
            dsSituationList.Dispose();
        }
        #endregion
    }
}
