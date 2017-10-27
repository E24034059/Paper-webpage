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
using System.Linq;
using Hints.Learning.Question;
using Hints.DB;
using suro.util;
using Hints.DB.Section;

public partial class AuthoringTool_CaseEditor_Paper_Paper_QuestionGroupEdit : AuthoringTool_BasicForm_BasicForm
{
    string strAssignedQID = "";
    string strGroupID = "";
    string strQuestionType = "";
    string strSelectionID = "";
    string strPaperID = "";
    //建立SqlDB物件
    SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
    //SQLString mySQL = new SQLString();
    //題目的編號
    int intQuestionIndex = 0;
    DataTable dtPrefix = new DataTable();
    DataTable dtPrefix2 = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(AuthoringTool_CaseEditor_Paper_Paper_QuestionGroupEdit));

        getParametor();

        clsHintsDB HintsDB = new clsHintsDB();
        string strPrefix = "SELECT cNodeID,cNodeName FROM QuestionGroupTree WHERE cNodeID = (SELECT cParentID FROM QuestionGroupTree WHERE cNodeID='" + strGroupID + "')";
        dtPrefix = HintsDB.getDataSet(strPrefix).Tables[0];
        string strPrefix2 = "SELECT cNodeID FROM QuestionGroupTree WHERE cNodeID = (SELECT cParentID FROM QuestionGroupTree WHERE cNodeID='" + dtPrefix.Rows[0]["cNodeID"].ToString() + "')";
        dtPrefix2 = HintsDB.getDataSet(strPrefix2).Tables[0];
        if (!IsPostBack)
        {
            if (strQuestionType == "4")
            {              
                lbCurrentTopic.Visible = true;
                ddlChangeTopic.Visible = true;

                string strddlSQL = "SELECT cNodeID,cNodeName FROM QuestionGroupTree WHERE cParentID = (SELECT cParentID FROM QuestionGroupTree WHERE cNodeID='" + dtPrefix2.Rows[0]["cNodeID"].ToString() + "')";
                DataTable dtTopic = HintsDB.getDataSet(strddlSQL).Tables[0];
                foreach (DataRow dr in dtTopic.Rows)
                {
                    ddlChangeTopic.Items.Add(new ListItem(dr["cNodeName"].ToString(), dr["cNodeID"].ToString()));
                }
                ddlChangeTopic.SelectedValue = dtPrefix2.Rows[0]["cNodeID"].ToString();

                //建立對話題表格
                this.setupConversationQuestionTable();

                string[] strIndex = strSelectionID.Split('_');
                string strIsReturnSQL = "SELECT bIsReturn, cDisplayTime FROM Paper_ConversationGroupID WHERE cQID = '" + strAssignedQID + "' AND iSelectionID = '" + Convert.ToInt32(strIndex[0]) + "'";
                DataTable dtIsReturn = HintsDB.getDataSet(strIsReturnSQL).Tables[0];
                if (dtIsReturn.Rows.Count > 0)
                {
                    if (dtIsReturn.Rows[0]["bIsReturn"].ToString() == "False")
                    { rbGoToNew.Checked = true; }

                    if (dtIsReturn.Rows[0]["cDisplayTime"].ToString() == "1|N")
                    {
                        rbDisplayBoth.Checked = true;
                        rbGiveWarningNo.Checked = true;
                    }
                    else if (dtIsReturn.Rows[0]["cDisplayTime"].ToString() == "2|#")
                    {
                        rbDisplayWhenTrue.Checked = true;
                        rbDisplayWhenTrue_CheckedChanged(sender, e);
                    }
                    else // 初始值
                    {
                        rbDisplayBoth.Checked = true;
                        rbGiveWarningYes.Checked = true;
                    }
                }
            }
            else
            {
                //建立選擇題表格
                this.setupQuestionTable();
                //建立問答題表格
                this.setupTextQuestionTable();
                //建立圖形題表格
                this.setupSimulatorQuestionTable();
                //setupSimulatorQuestionTable
            }
            //設定選項的內容
            GetSelectionItem();
        }
    }

    /// <summary>
    /// 接收參數
    /// </summary>
    private void getParametor()
    {
        //QID
        if (Request.QueryString["QID"] != null)
        {
            strAssignedQID = Request.QueryString["QID"].ToString();
            if (strAssignedQID.IndexOf("VPAns") >= 0)
            {
                strQuestionType = "4";
                hfCurrentProType.Value = Request.QueryString["CurrentProType"].ToString();
            }
            else
            {
                DataTable dtQuestionMode = SQLString.getQuestionClassifyName(strAssignedQID);
                strQuestionType = dtQuestionMode.Rows[0]["cQuestionType"].ToString();
                dtQuestionMode.Dispose();
                dtQuestionMode = null;              
            }
            hfQID.Value = strAssignedQID;
        }

        //GroupID
        if (Request.QueryString["GroupID"] != null)
        {
            strGroupID = Request.QueryString["GroupID"].ToString();
            hfGroupID.Value = strGroupID;

            if (Session["GroupID"] != null)
            {
                Session["GroupID"] = strGroupID;
            }
            else
            {
                Session.Add("GroupID", strGroupID);
            }
        }

        //SelectionID
        if (Request.QueryString["SelectionID"] != null)
        {
            strSelectionID = Request.QueryString["SelectionID"].ToString();
        }
    }

    protected void ddlChangeTopic_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlChangeTopic.SelectedValue.ToString() == dtPrefix2.Rows[0]["cNodeID"].ToString())
        {
            hfIsOriTopic.Value = "1";
            strGroupID = Request.QueryString["GroupID"].ToString();
            hfChangedGroupID.Value = strGroupID;
            this.setupConversationQuestionTable();
        }
        else
        {
            hfIsOriTopic.Value = "0";
            strGroupID = ddlChangeTopic.SelectedValue.ToString();
            hfChangedGroupID.Value = strGroupID;
            this.setupConversationQuestionTable();
        }
    }

    private void setupQuestionTable()
    {
        tcQuestionTable.Controls.Clear();
        Table table = new Table();
        tcQuestionTable.Controls.Add(table);
        table.CellSpacing = 0;
        table.CellPadding = 2;
        table.BorderStyle = BorderStyle.Solid;
        table.BorderWidth = Unit.Pixel(1);
        table.BorderColor = System.Drawing.Color.Black;
        table.GridLines = GridLines.Both;
        table.Width = Unit.Percentage(100);

        //建立Table的CSS
        table.CssClass = "header1_table";

        //依照QuestionMode決定取出此組別的選擇題
        PaperSystem.SQLString PaperSystemSQLString = new PaperSystem.SQLString();
        string strSQL = PaperSystemSQLString.getGroupSelectionQuestion(strGroupID);

        DataSet dsQuestionList = sqldb.getDataSet(strSQL);
        if (dsQuestionList.Tables[0].Rows.Count > 0)
        {
            clsHintsDB HintsDB = new clsHintsDB();
            string strQuestionGroupID = "";
            string strSQL_Paper_QuestionSelectionGroupID = "SELECT * FROM Paper_QuestionSelectionGroupID " +
               "WHERE cQID = '" + strAssignedQID + "' AND cSelectionID = '" + strSelectionID + "'";
            DataTable dtPaper_QuestionSelectionGroupID = new DataTable();
            dtPaper_QuestionSelectionGroupID = HintsDB.getDataSet(strSQL_Paper_QuestionSelectionGroupID).Tables[0];
            if (dtPaper_QuestionSelectionGroupID.Rows.Count > 0)
            {
                strQuestionGroupID = dtPaper_QuestionSelectionGroupID.Rows[0]["cGroupID"].ToString();
            }

            for (int i = 0; i < dsQuestionList.Tables[0].Rows.Count; i++)
            {
                //取得QID
                string strQID = "";
                strQID = dsQuestionList.Tables[0].Rows[i]["cQID"].ToString();

                if (strAssignedQID != strQID)
                {
                    //取得問題的SQL
                    DataSet dsQuestion = null;
                    strSQL = PaperSystemSQLString.getQuestion(strQID);

                    dsQuestion = sqldb.getDataSet(strSQL);
                    if (dsQuestion.Tables[0].Rows.Count > 0)
                    {
                        //建立問題的內容
                        TableRow trQuestion = new TableRow();
                        table.Rows.Add(trQuestion);

                        intQuestionIndex += 1;

                        //問題的題號
                        TableCell tcQuestionNum = new TableCell();
                        trQuestion.Cells.Add(tcQuestionNum);
                        tcQuestionNum.Width = Unit.Pixel(25);
                        tcQuestionNum.Style.Add("text-align", "center");
                        CheckBox cbQuestionGroup = new CheckBox();
                        cbQuestionGroup.ID = "cbQuestionGroup" + strQID;
                        tcQuestionNum.Controls.Add(cbQuestionGroup);
                        cbQuestionGroup.Attributes.Add("onclick", "SaveQuestionGroup('" + strQID + "','" + strAssignedQID + "', '" + strSelectionID + "')");

                        string strSQL_Paper_QuestionSelectionGroupItem = "SELECT * FROM Paper_QuestionSelectionGroupItem " +
                        "WHERE cGroupID = '" + strQuestionGroupID + "' AND cQID = '" + strQID + "'";
                        DataTable dtPaper_QuestionSelectionGroupItem = HintsDB.getDataSet(strSQL_Paper_QuestionSelectionGroupItem).Tables[0];
                        if (dtPaper_QuestionSelectionGroupItem.Rows.Count > 0)
                        {
                            cbQuestionGroup.Checked = true;
                        }

                        //問題的內容
                        string strQuestion = "";
                        strQuestion = dsQuestion.Tables[0].Rows[0]["cQuestion"].ToString();

                        TableCell tcQuestion = new TableCell();
                        trQuestion.Cells.Add(tcQuestion);
                        tcQuestion.Text = "<IMG id='img_" + strQID + "' src='../../../BasicForm/Image/plus.gif'>&nbsp;" + strQuestion;
                        tcQuestion.Style.Add("CURSOR", "hand");

                        //建立問題的CSS
                        trQuestion.Attributes.Add("Class", "header1_table_first_row");

                        //建立選項
                        strSQL = PaperSystemSQLString.getAllSelections(strQID);
                        DataSet dsSelection = sqldb.getDataSet(strSQL);
                        tcQuestion.Attributes.Add("onclick", "ShowSelectionQuestionDetail('" + strQID + "','img_" + strQID + "','" + dsSelection.Tables[0].Rows.Count + "')");
                        if (dsSelection.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dsSelection.Tables[0].Rows.Count; j++)
                            {
                                //Seq
                                string strSeq = "";
                                strSeq = dsSelection.Tables[0].Rows[j]["sSeq"].ToString();

                                //Selection
                                string strSelection = "";
                                strSelection = dsSelection.Tables[0].Rows[j]["cSelection"].ToString();

                                //bCaseSelect
                                bool bCaseSelect = false;
                                bCaseSelect = Convert.ToBoolean(dsSelection.Tables[0].Rows[j]["bCaseSelect"]);

                                TableRow trSelection = new TableRow();
                                table.Rows.Add(trSelection);
                                trSelection.ID = "trAnswerTitle_" + strQID + "_" + (j + 1);
                                trSelection.Style.Add("display", "none");

                                //是否為建議選項
                                TableCell tcSuggest = new TableCell();
                                trSelection.Cells.Add(tcSuggest);
                                tcSuggest.Width = Unit.Pixel(25);
                                tcSuggest.Style.Add("text-align", "center");
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

                        //建立Space
                        TableRow trSpace = new TableRow();
                        trSpace.Style.Add("background-color", "#EBECED");
                        table.Rows.Add(trSpace);

                        TableCell tcSpace = new TableCell();
                        tcSpace.ColumnSpan = 2;
                        tcSpace.Style.Add("height", "7px");
                        trSpace.Cells.Add(tcSpace);


                    }
                    else
                    {
                        //此問題沒有選項
                    }
                    dsQuestion.Dispose();
                }
            }
        }
        else
        {
            //此問卷沒有任何選擇題的情況
            trQuestionTable.Style["display"] = "none";
        }
        dsQuestionList.Dispose();
    }

    private void setupConversationQuestionTable()  //對話題的Case 老詹 2015/05/11
    {
        tcConversationQuestionTable.Controls.Clear();
        Table table = new Table();
        tcConversationQuestionTable.Controls.Add(table);
        table.CellSpacing = 0;
        table.CellPadding = 5;
        table.BorderStyle = BorderStyle.Solid;
        table.BorderWidth = Unit.Pixel(1);
        table.BorderColor = System.Drawing.Color.Black;
        table.GridLines = GridLines.Both;
        table.Width = Unit.Percentage(100);


        //建立Table的CSS
        table.CssClass = "header1_table";

        PaperSystem.SQLString PaperSystemSQLString = new PaperSystem.SQLString();
        DataSet dsQuestionList = new DataSet(); 
        //依照QuestionMode決定取出此組別的對話題(IF條件為判斷是否選單更改了主題)
        if (ddlChangeTopic.SelectedValue.ToString() == dtPrefix2.Rows[0]["cNodeID"].ToString())
        {
            string strSQL = PaperSystemSQLString.getGroupConversationByProblemType(strGroupID, hfCurrentProType.Value);
            dsQuestionList = sqldb.getDataSet(strSQL);
        }
        else
        {
            dsQuestionList.Tables.Add("TotalConversation");
            dsQuestionList.Tables["TotalConversation"].Columns.Add("cQID");
            dsQuestionList.Tables["TotalConversation"].Columns.Add("cQuestion");
            string strTmpPreSQL = "SELECT * FROM QuestionGroupTree WHERE cParentID='" + strGroupID + "'";
            DataTable dtTmpPreTree = sqldb.getDataSet(strTmpPreSQL).Tables[0];
            DataTable dtTmpTree = new DataTable();
            for (int i = 0; i < dtTmpPreTree.Rows.Count; i++)
            {
                bool CheckTopic = false;
                char[] charr1 = dtTmpPreTree.Rows[i]["cNodeName"].ToString().ToCharArray();
                char[] charr2 = dtPrefix.Rows[0]["cNodeName"].ToString().ToCharArray();
                for (int j = 0; j < charr1.Length; i++)
                {  // 將新主題的名稱與原有主題的名稱轉成char[]後比對，若CheckTopic = true代表找同問題類型的新主題(EX:Host Failure對應Host Purchase)  老詹 2015/08/28
                    if (charr1[j] == charr2[j])
                    {
                        CheckTopic = true;
                        break;
                    }
                }
                if (CheckTopic)
                {
                    string strTmpSQL = "SELECT * FROM QuestionGroupTree WHERE cParentID='" + dtTmpPreTree.Rows[i]["cNodeID"].ToString() + "'";
                    dtTmpTree = sqldb.getDataSet(strTmpSQL).Tables[0];
                    break;
                }
            }
            foreach (DataRow dr in dtTmpTree.Rows)
            {
                //dsTmpTree先抓其他主題下的細部主題有哪些，再以原本取得對話題的function來將所有細部主題的題目放入dsQuestionList中
                string strSQL = PaperSystemSQLString.getGroupConversationByProblemType(dr["cNodeID"].ToString(), hfCurrentProType.Value);
                DataSet dsTmp = sqldb.getDataSet(strSQL);
                DataTable dtTmp = dsTmp.Tables[0];
                DataRow dr_Conversation = dsQuestionList.Tables["TotalConversation"].NewRow();
                dr_Conversation["cQID"] = dtTmp.Rows[0]["cQID"].ToString();
                dr_Conversation["cQuestion"] = dtTmp.Rows[0]["cQuestion"].ToString();
                dsQuestionList.Tables["TotalConversation"].Rows.Add(dr_Conversation);
            }
        }

        if (dsQuestionList.Tables[0].Rows.Count > 0)
        {
            clsHintsDB HintsDB = new clsHintsDB();
            string strQuestionGroupID = "";
            string[] strIndex = strSelectionID.Split('_');
            string strSQL_Paper_ConversationGroupID = "SELECT * FROM Paper_ConversationGroupID " +
               "WHERE cQID = '" + strAssignedQID + "' AND iSelectionID = '" + Convert.ToInt32(strIndex[0]) + "'";
            DataTable dtPaper_ConversationGroupID = new DataTable();
            dtPaper_ConversationGroupID = HintsDB.getDataSet(strSQL_Paper_ConversationGroupID).Tables[0];
            if (dtPaper_ConversationGroupID.Rows.Count > 0)
            {
                strQuestionGroupID = dtPaper_ConversationGroupID.Rows[0]["cQID"].ToString() + "_" + dtPaper_ConversationGroupID.Rows[0]["iSelectionID"].ToString();
            }

            for (int i = 0; i < dsQuestionList.Tables[0].Rows.Count; i++)
            {
                string strGetKeyword = Hints.Learning.Question.DataReceiver.getTextQuestionKeyword(dsQuestionList.Tables[0].Rows[i]["cQID"].ToString());
                string[] arrKeyword = strGetKeyword.Split(',');

                //取得QID
                string strQID = "";
                strQID = dsQuestionList.Tables[0].Rows[i]["cQID"].ToString();

                if (strAssignedQID != strQID)
                {
                    //建立問題的內容
                    TableRow trQuestion = new TableRow();
                    table.Rows.Add(trQuestion);

                    //問題的題號(在這裡以checkbox取代)
                    TableCell tcQuestionNum = new TableCell();
                    trQuestion.Cells.Add(tcQuestionNum);
                    tcQuestionNum.Width = Unit.Pixel(25);
                    tcQuestionNum.Style.Add("text-align", "left");
                    HtmlInputCheckBox cbConversationGroup = new HtmlInputCheckBox();
                    cbConversationGroup.ID = "cbQuestionGroup" + strQID;
                    cbConversationGroup.Attributes.Add("class", "bigcheck");
                    tcQuestionNum.Width = Unit.Pixel(100);
                    tcQuestionNum.Controls.Add(cbConversationGroup);
                    cbConversationGroup.Attributes.Add("onclick", "SaveQuestionGroup('" + strQID + "','" + strAssignedQID + "', '" + strIndex[0] + "')");

                    string strISInPaperSQL = "SELECT * FROM BasicQuestionList WHERE cQID='" + dsQuestionList.Tables[0].Rows[i]["cQID"].ToString() + "' AND bIsOriginal='1'";
                    DataTable dtISInPaper = HintsDB.getDataSet(strISInPaperSQL).Tables[0];
                    if (dtISInPaper.Rows.Count > 0)
                    {
                        cbConversationGroup.Disabled = true;
                        tcQuestionNum.ToolTip = "此題已經在原始考卷中，故無法編進題組。";
                    }

                    string strSQL_Paper_ConversationGroupItem = "SELECT * FROM Paper_QuestionSelectionGroupItem " +
                    "WHERE cGroupID = '" + strQuestionGroupID + "'";
                    DataTable dtPaper_ConversationGroupItem = HintsDB.getDataSet(strSQL_Paper_ConversationGroupItem).Tables[0];
                    if (dtPaper_ConversationGroupItem.Rows.Count > 0)
                    {
                        foreach(DataRow dr in dtPaper_ConversationGroupItem.Rows)
                        {
                            if (dr["cQID"].ToString() == strQID)
                                cbConversationGroup.Checked = true;
                        }
                    }

                    //問題的內容
                    string strQuestion = "";
                    strQuestion = dsQuestionList.Tables[0].Rows[i]["cQuestion"].ToString();

                    TableCell tcQuestion = new TableCell();
                    trQuestion.Cells.Add(tcQuestion);
                    tcQuestion.Text = "<IMG id='img_" + strQID + "' src='../../../BasicForm/Image/minus.gif'>&nbsp;" + strQuestion;
                    tcQuestion.Style.Add("CURSOR", "hand");

                    //建立問題的CSS
                    trQuestion.Attributes.Add("Class", "header1_table_first_row");
                    tcQuestion.Attributes.Add("onclick", "ShowConversationDetail('" + strQID + "','img_" + strQID + "')");

                    //建立Keyword的標題
                    TableRow trKeywordTitle = new TableRow();
                    trKeywordTitle.Attributes.Add("Class", "header1_tr_odd_row");
                    table.Rows.Add(trKeywordTitle);
                    trKeywordTitle.ID = "trKeywordTitle_" + strQID;

                    TableCell tcKeywordTitle = new TableCell();
                    trKeywordTitle.Cells.Add(tcKeywordTitle);
                    tcKeywordTitle.Text = "<font style='color:Black; font-weight:bold'>Keyword :&nbsp; <font/>";
                    tcKeywordTitle.Style.Add("text-align", "right");
                    tcKeywordTitle.Width = Unit.Pixel(100);

                    TableCell tcKeyword = new TableCell();
                    trKeywordTitle.Cells.Add(tcKeyword);
                    tcKeyword.Text = Hints.Learning.Question.DataReceiver.getConversation_Question_Keyword(strQID);
                    tcKeyword.Style.Add("color", "Red");
                    tcKeyword.Attributes.Add("Class", "header1_tr_even_row");

                    //建立Space
                    TableRow trSpace = new TableRow();
                    trSpace.Style.Add("background-color", "#EBECED");
                    table.Rows.Add(trSpace);

                    TableCell tcSpace = new TableCell();
                    tcSpace.ColumnSpan = 2;
                    tcSpace.Style.Add("height", "7px");
                    trSpace.Cells.Add(tcSpace);
                }
            }
            trConversationQuestionTable.Style["display"] = "";
        }
        else
        {
            //此問卷沒有任何對話題的情況
            trConversationQuestionTable.Style["display"] = "none";
        }
        dsQuestionList.Dispose();


    }

    private void setupTextQuestionTable()
    {
        tcTextQuestionTable.Controls.Clear();
        Table table = new Table();
        tcTextQuestionTable.Controls.Add(table);
        table.CellSpacing = 0;
        table.CellPadding = 5;
        table.BorderStyle = BorderStyle.Solid;
        table.BorderWidth = Unit.Pixel(1);
        table.BorderColor = System.Drawing.Color.Black;
        table.GridLines = GridLines.Both;
        table.Width = Unit.Percentage(100);


        //建立Table的CSS
        table.CssClass = "header1_table";

        //依照QuestionMode決定取出此組別的問答題
        PaperSystem.SQLString PaperSystemSQLString = new PaperSystem.SQLString();
        string strSQL = PaperSystemSQLString.getGroupQuestionAnswer(strGroupID);

        DataSet dsQuestionList = sqldb.getDataSet(strSQL);

        if (dsQuestionList.Tables[0].Rows.Count > 0)
        {
            clsHintsDB HintsDB = new clsHintsDB();
            string strQuestionGroupID = "";
            string strSQL_Paper_QuestionSelectionGroupID = "SELECT * FROM Paper_QuestionSelectionGroupID " +
               "WHERE cQID = '" + strAssignedQID + "' AND cSelectionID = '" + strSelectionID + "'";
            DataTable dtPaper_QuestionSelectionGroupID = new DataTable();
            dtPaper_QuestionSelectionGroupID = HintsDB.getDataSet(strSQL_Paper_QuestionSelectionGroupID).Tables[0];
            if (dtPaper_QuestionSelectionGroupID.Rows.Count > 0)
            {
                strQuestionGroupID = dtPaper_QuestionSelectionGroupID.Rows[0]["cGroupID"].ToString();
            }

            for (int i = 0; i < dsQuestionList.Tables[0].Rows.Count; i++)
            {
                string strGetKeyword = Hints.Learning.Question.DataReceiver.getTextQuestionKeyword(dsQuestionList.Tables[0].Rows[i]["cQID"].ToString());
                string[] arrKeyword = strGetKeyword.Split(',');

                //取得QID
                string strQID = "";
                strQID = dsQuestionList.Tables[0].Rows[i]["cQID"].ToString();

                //取得QuestionType
                string strQuestionType = "2";
                strQuestionType = dsQuestionList.Tables[0].Rows[i]["cQuestionType"].ToString();

                //取得Question
                string strQuestion = "";
                strQuestion = dsQuestionList.Tables[0].Rows[i]["cQuestion"].ToString();

                intQuestionIndex += 1;

                #region 建立問答題的內容

                //建立問題的標題
                TableRow trTextQuestionTitle = new TableRow();
                trTextQuestionTitle.Attributes.Add("Class", "header1_table_first_row");
                table.Rows.Add(trTextQuestionTitle);

                TableCell tcQuestionNum = new TableCell();
                trTextQuestionTitle.Cells.Add(tcQuestionNum);
                tcQuestionNum.Width = Unit.Pixel(25);
                tcQuestionNum.Style.Add("text-align", "center");
                CheckBox cbQuestionGroup = new CheckBox();
                cbQuestionGroup.ID = "cbQuestionGroup" + strQID;
                tcQuestionNum.Controls.Add(cbQuestionGroup);
                cbQuestionGroup.Attributes.Add("onclick", "SaveQuestionGroup('" + strQID + "','" + strAssignedQID + "', '" + strSelectionID + "')");

                string strSQL_Paper_QuestionSelectionGroupItem = "SELECT * FROM Paper_QuestionSelectionGroupItem " +
                        "WHERE cGroupID = '" + strQuestionGroupID + "' AND cQID = '" + strQID + "'";
                DataTable dtPaper_QuestionSelectionGroupItem = HintsDB.getDataSet(strSQL_Paper_QuestionSelectionGroupItem).Tables[0];
                if (dtPaper_QuestionSelectionGroupItem.Rows.Count > 0)
                {
                    cbQuestionGroup.Checked = true;
                }

                TableCell tcTextQuestionTitle = new TableCell();
                trTextQuestionTitle.Cells.Add(tcTextQuestionTitle);
                if (arrKeyword.Length > 0)
                {
                    for (int kcount = 0; kcount < arrKeyword.Length; kcount++)
                    {
                        strQuestion = strQuestion.Replace(arrKeyword[kcount], "<span class='span_keyword' >" + arrKeyword[kcount] + "</span>"); ;
                        tcTextQuestionTitle.Text = "<IMG id='img_" + strQID + "' src='../../../BasicForm/Image/plus.gif'>&nbsp;" + strQuestion;
                    }
                }
                tcTextQuestionTitle.Style.Add("CURSOR", "hand");

                //建立同義問題的標題
                TableRow trSynQuestionTitle = new TableRow();
                trSynQuestionTitle.Attributes.Add("Class", "header1_tr_odd_row");
                table.Rows.Add(trSynQuestionTitle);
                trSynQuestionTitle.ID = "trSynQuestionTitle_" + strQID;
                trSynQuestionTitle.Style.Add("display", "none");

                TableCell tcSynQuestionTitle = new TableCell();
                trSynQuestionTitle.Cells.Add(tcSynQuestionTitle);
                tcSynQuestionTitle.Text = "<font style='color:Black; font-weight:bold'>Synonymous Question : <font/>";
                tcSynQuestionTitle.Style.Add("text-align", "right");
                tcSynQuestionTitle.Width = Unit.Pixel(230);


                TableCell tcSynQuestion = new TableCell();
                trSynQuestionTitle.Cells.Add(tcSynQuestion);
                BulidInterrogation("Question", strQID, tcSynQuestion, "");
                tcSynQuestion.Attributes.Add("Class", "header1_tr_even_row");

                PaperSystem.DataReceiver PaperSystemDataReceiver = new PaperSystem.DataReceiver();
                DataTable dtQuestionAnswer_Answer = PaperSystemDataReceiver.QuestionAnswer_Answer_SELECT_AllAnswers(strQID);
                hfAnswerCount.Value = "0";
                hfAnswerCount.Value = dtQuestionAnswer_Answer.Rows.Count.ToString();
                tcTextQuestionTitle.Attributes.Add("onclick", "ShowDetail('" + strQID + "','img_" + strQID + "','" + hfAnswerCount.Value + "')");
                if (dtQuestionAnswer_Answer.Rows.Count > 0)
                {
                    int iACount = 1;
                    foreach (DataRow drQuestionAnswer_Answer in dtQuestionAnswer_Answer.Rows)
                    {
                        string strAID = "";
                        string strAnswer = "";
                        strAID = drQuestionAnswer_Answer["cAID"].ToString();
                        strAnswer = drQuestionAnswer_Answer["cAnswer"].ToString();

                        //建立Answer的標題
                        TableRow trAnswerTitle = new TableRow();
                        trAnswerTitle.Attributes.Add("Class", "header1_tr_odd_row");
                        table.Rows.Add(trAnswerTitle);
                        trAnswerTitle.ID = "trAnswerTitle_" + strQID + "_" + iACount;
                        trAnswerTitle.Style.Add("display", "none");

                        TableCell tcAnswerTitle = new TableCell();
                        trAnswerTitle.Cells.Add(tcAnswerTitle);
                        Label lbAnswerTitle = new Label();
                        lbAnswerTitle.Text = "<font style='color:Black; font-weight:bold'>Answer " + iACount + " :&nbsp;<font/>";
                        tcAnswerTitle.Controls.Add(lbAnswerTitle);

                        tcAnswerTitle.Style.Add("text-align", "right");
                        tcAnswerTitle.Width = Unit.Pixel(230);


                        TableCell tcAnswer = new TableCell();
                        trAnswerTitle.Cells.Add(tcAnswer);

                        for (int kcount = 0; kcount < arrKeyword.Length; kcount++)
                        {
                            strAnswer = strAnswer.Replace(arrKeyword[kcount], "<span class='span_keyword' >" + arrKeyword[kcount] + "</span>"); ;
                        }

                        tcAnswer.Text = strAnswer;
                        tcAnswer.Attributes.Add("Class", "header1_tr_even_row");

                        //建立同義答案的標題
                        TableRow trSynAnswerTitle = new TableRow();
                        trSynAnswerTitle.Attributes.Add("Class", "header1_tr_odd_row");
                        table.Rows.Add(trSynAnswerTitle);
                        trSynAnswerTitle.ID = "trSynAnswerTitle_" + strQID + "_" + iACount;
                        trSynAnswerTitle.Style.Add("display", "none");

                        TableCell tcSynAnswerTitle = new TableCell();
                        trSynAnswerTitle.Cells.Add(tcSynAnswerTitle);
                        //tcSynAnswerTitle.Text = "<IMG id='imgSynAnswer_" + strQID + "_" + iACount + "' src='../../../BasicForm/Image/plus.gif'><font style='color:Black; font-weight:bold'>&nbsp;Synonymous Answer " + iACount + " : <font/>";
                        tcSynAnswerTitle.Text = "<font style='color:Black; font-weight:bold'>Synonymous Answer " + iACount + "  :&nbsp;<font/>";
                        tcSynAnswerTitle.Style.Add("text-align", "right");
                        tcSynAnswerTitle.Width = Unit.Pixel(230);

                        TableCell tcSynAnswer = new TableCell();
                        trSynAnswerTitle.Cells.Add(tcSynAnswer);
                        BulidInterrogation("Answer", strQID, tcSynAnswer, strAID);
                        tcSynAnswer.Attributes.Add("Class", "header1_tr_even_row");

                        iACount++;

                        #region 管控同義項的顯示與隱藏
                        DataTable dtSynAnswer = new DataTable();
                        dtSynAnswer = clsInterrogationEnquiry.GetSynAnswer(strQID, strAID);
                        if (dtSynAnswer.Rows.Count == 0)
                        {
                            trSynAnswerTitle.Style.Add("display", "none");
                        }
                        #endregion
                    }
                }

                //建立Keyword的標題
                TableRow trKeywordTitle = new TableRow();
                trKeywordTitle.Attributes.Add("Class", "header1_tr_odd_row");
                table.Rows.Add(trKeywordTitle);
                trKeywordTitle.ID = "trKeywordTitle_" + strQID;
                trKeywordTitle.Style.Add("display", "none");

                TableCell tcKeywordTitle = new TableCell();
                trKeywordTitle.Cells.Add(tcKeywordTitle);
                tcKeywordTitle.Text = "<font style='color:Black; font-weight:bold'>Keyword :&nbsp; <font/>";
                tcKeywordTitle.Style.Add("text-align", "right");
                tcKeywordTitle.Width = Unit.Pixel(230);

                TableCell tcKeyword = new TableCell();
                trKeywordTitle.Cells.Add(tcKeyword);
                tcKeyword.Text = strGetKeyword;
                tcKeyword.Attributes.Add("Class", "header1_tr_even_row");


                //建立Space
                TableRow trSpace = new TableRow();
                trSpace.Style.Add("background-color", "#EBECED");
                table.Rows.Add(trSpace);

                TableCell tcSpace = new TableCell();
                tcSpace.Style.Add("height", "7px");
                trSpace.Cells.Add(tcSpace);
                tcSpace.ColumnSpan = 2;


                #region 管控同義項的顯示與隱藏
                DataTable dtSynQuestion = new DataTable();
                dtSynQuestion = clsInterrogationEnquiry.GetSynQuestion(strQID);
                if (dtSynQuestion.Rows.Count == 0)
                {
                    trSynQuestionTitle.Style.Add("display", "none");
                }
                #endregion

                #endregion

            }
        }
        else
        {
            //此問卷沒有任何問答題的情況
            trTextQuestionTable.Style["display"] = "none";
        }
        dsQuestionList.Dispose();
    }

    protected void setupSimulatorQuestionTable()
    {
        PaperSystem.SQLString PaperSystemSQLString = new PaperSystem.SQLString();

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
        string strSQL = PaperSystemSQLString.getGroupQuestionSimulator(strGroupID);       
        DataSet dsQuestionList = sqldb.getDataSet(strSQL);
        if (dsQuestionList.Tables[0].Rows.Count > 0)
        {
            clsHintsDB HintsDB = new clsHintsDB();
            string strQuestionGroupID = "";
            string strSQL_Paper_QuestionSelectionGroupID = "SELECT * FROM Paper_QuestionSelectionGroupID " +
               "WHERE cQID = '" + strAssignedQID + "' AND cSelectionID = '" + strSelectionID + "'";
            DataTable dtPaper_QuestionSelectionGroupID = new DataTable();
            dtPaper_QuestionSelectionGroupID = HintsDB.getDataSet(strSQL_Paper_QuestionSelectionGroupID).Tables[0];
            if (dtPaper_QuestionSelectionGroupID.Rows.Count > 0)
            {
                strQuestionGroupID = dtPaper_QuestionSelectionGroupID.Rows[0]["cGroupID"].ToString();
            }

            for (int i = 0; i < dsQuestionList.Tables[0].Rows.Count; i++)
            {
                //取得QuestionType
                string strQuestionType = "3";
                strQuestionType = dsQuestionList.Tables[0].Rows[i]["cQuestionType"].ToString();

                //取得QID
                string strQID = "";
                strQID = dsQuestionList.Tables[0].Rows[i]["cQID"].ToString();
                if (strAssignedQID != strQID)//本身不顯示
                {
                    //取得問題的SQL
                    DataSet dsQuestion = null;
                    //if (hfSymptoms.Value == "All")
                    strSQL = PaperSystemSQLString.getQuestion(strQID);
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
                        tcQuestionNum.Width = Unit.Pixel(25);
                        //tcQuestionNum.Text = "Q" + intQuestionIndex.ToString() + ": ";
                        //tcQuestionNum.Text = "<IMG id='img_" + strQID + "' src='../../../BasicForm/Image/minus.gif'>&nbsp;Q" + intQuestionIndex.ToString() + " : ";
                        CheckBox cbQuestionGroup = new CheckBox();
                        cbQuestionGroup.ID = "cbQuestionGroup" + strQID;
                        tcQuestionNum.Controls.Add(cbQuestionGroup);
                        cbQuestionGroup.Attributes.Add("onclick", "SaveQuestionGroup('" + strQID + "','" + strAssignedQID + "', '" + strSelectionID + "')");
                        //此題是否是題組中的一題
                        string strSQL_Paper_QuestionSelectionGroupItem = "SELECT * FROM Paper_QuestionSelectionGroupItem " +
                        "WHERE cGroupID = '" + strQuestionGroupID + "' AND cQID = '" + strQID + "'";
                        DataTable dtPaper_QuestionSelectionGroupItem = HintsDB.getDataSet(strSQL_Paper_QuestionSelectionGroupItem).Tables[0];
                        if (dtPaper_QuestionSelectionGroupItem.Rows.Count > 0)
                        {
                            cbQuestionGroup.Checked = true;
                        }
                        //問題的內容
                        string strQuestion = "";
                        strQuestion = dsQuestion.Tables[0].Rows[0]["cQuestion"].ToString();

                        TableCell tcQuestion = new TableCell();
                        trQuestion.Cells.Add(tcQuestion);
                        tcQuestion.Text = "<IMG id='img_" + strQID + "' src='../../../BasicForm/Image/plus.gif'>" + strQuestion;//縮放

                        //縮放
                        tcQuestion.Attributes.Add("onclick", "ShowSimuQuestionDetail('" + strQID + "','img_" + strQID + "','" + hfAnswerCount.Value + "')");

                        //建立問題的CSS
                        trQuestion.Attributes.Add("Class", "header1_table_first_row");

                        //取得simulator的場景及正解還有順序
                        strSQL = PaperSystemSQLString.getQuestion_sim(strQID);
                        DataSet dsQuestion_sim = sqldb.getDataSet(strSQL);
                        //建立圖形題的場景

                        TableRow trScene = new TableRow();
                        trScene.ID = "trimg_" + strQID;
                        trScene.Height = 290;
                        table.Rows.Add(trScene);
                        trScene.Style.Add("display", "none");

                        TableCell tcSceneTitle = new TableCell();
                        trScene.Cells.Add(tcSceneTitle);
                        tcSceneTitle.Text = "<font style='color:Black; font-weight:bold'>Scene :&nbsp;<font/>";
                        tcSceneTitle.Style.Add("text-align", "right");
                        tcSceneTitle.Width = Unit.Pixel(230);
                        //取得場景ID
                        string strSimuID = dsQuestion_sim.Tables[0].Rows[0]["cSimulatorID"].ToString();
                        DataTable dtTemp = new DataTable();
                        string str_URL = "";
                        if (strSimuID.Contains("Internal Medicine|General|1"))
                        {
                            string str_VRID = "Simulator_20100928144239";
                            strSQL = "SELECT * FROM SimulatorBackground WHERE SimulatorID LIKE '" + str_VRID + "'";
                            dtTemp = sqldb.getDataSet(strSQL).Tables[0];
                            str_URL = dtTemp.Rows[0]["bgUrl"].ToString();
                        }
                        else if (strSimuID.Contains("Internal Medicine|General|2"))
                        {
                            str_URL = "http://140.116.72.123/HintsCase/FileCollection/0101/201108/File20110817120244.JPG";
                        }
                        else
                        {
                            strSQL = "SELECT * FROM SimulatorBackground WHERE SimulatorID LIKE '" + strSimuID + "'";
                            dtTemp = sqldb.getDataSet(strSQL).Tables[0];
                            str_URL = dtTemp.Rows[0]["bgUrl"].ToString();
                        }

                        TableCell tcSceneImg = new TableCell();
                        trScene.Cells.Add(tcSceneImg);
                        HtmlImage h_image = new HtmlImage();
                        h_image.Src = str_URL;
                        h_image.Width = 320;
                        h_image.Height = 280;
                        tcSceneImg.Controls.Add(h_image);
                        tcSceneImg.Attributes.Add("Class", "header1_tr_even_row");
                        tcSceneImg.Width = Unit.Percentage(81);
                        //建立圖形題的正確解答以及順序
                        if (dsQuestion_sim.Tables[0].Rows.Count > 0)
                        {
                            int s_no = 0;
                            for (int s = 0; s < dsQuestion_sim.Tables[0].Rows.Count; s++)
                            {
                                s_no = s + 1;
                                //答案行
                                TableRow trAnswer = new TableRow();
                                trAnswer.ID = "trAns_" + strQID + "_" + s_no;
                                table.Rows.Add(trAnswer);
                                trAnswer.Style.Add("display", "none");

                                TableCell tcAnswerTitle = new TableCell();
                                trAnswer.Cells.Add(tcAnswerTitle);
                                tcAnswerTitle.Text = "<font style='color:Black; font-weight:bold'>Answer " + s_no.ToString() + " :&nbsp;<font/>";
                                tcAnswerTitle.Style.Add("text-align", "right");
                                tcAnswerTitle.Width = Unit.Pixel(230);
                                //置入答案
                                TableCell tcAnswerValue = new TableCell();
                                trAnswer.Cells.Add(tcAnswerValue);
                                tcAnswerValue.Attributes.Add("Class", "header1_tr_even_row");
                                //tcAnswerValue.Width = Unit.Percentage(81);
                                string temp_ans = dsQuestion_sim.Tables[0].Rows[s]["cAnswer"].ToString().Replace('|', ',');
                                tcAnswerValue.Text = temp_ans.Substring(0, temp_ans.Length - 1);
                                //答案順序行
                                TableRow trAnswerOrder = new TableRow();
                                trAnswerOrder.ID = "trAnsOrder_" + strQID + "_" + s_no;
                                table.Rows.Add(trAnswerOrder);
                                trAnswerOrder.Style.Add("display", "none");

                                TableCell tcAns_order_Title = new TableCell();
                                trAnswerOrder.Cells.Add(tcAns_order_Title);
                                tcAns_order_Title.Text = "<font style='color:Black; font-weight:bold'>Answer " + s_no.ToString() + " order:&nbsp;<font/>";
                                tcAns_order_Title.Style.Add("text-align", "right");
                                tcAns_order_Title.Width = Unit.Pixel(230);
                                //置入順序
                                TableCell tcAnswer_Order = new TableCell();
                                trAnswerOrder.Cells.Add(tcAnswer_Order);
                                tcAnswer_Order.Attributes.Add("Class", "header1_tr_even_row");
                                //tcAnswer_Order.Width = Unit.Percentage(81);
                                string temp_order = dsQuestion_sim.Tables[0].Rows[s]["cOrder"].ToString().Replace('|', ',');
                                tcAnswer_Order.Text = temp_order.Substring(0, temp_order.Length - 1);

                            }
                        }
                        ////Modify and Delete 按鈕的TableRow
                        //TableRow trModify = new TableRow();
                        //table.Rows.Add(trModify);
                        //trModify.ID = "trModify_" + strQID;

                        //TableCell tcNone = new TableCell();
                        //trModify.Cells.Add(tcNone);

                        ////建立修改問題的Button
                        //TableCell tcModify = new TableCell();
                        //trModify.Cells.Add(tcModify);
                        //tcModify.Attributes["align"] = "right";

                        ////建立編輯題組的Button
                        //Button btnEditGroupQuestionSelection = new Button();
                        //tcModify.Controls.Add(btnEditGroupQuestionSelection);
                        //btnEditGroupQuestionSelection.ID = "btnEditGroupQuestionSimulator-" + strQID;
                        //btnEditGroupQuestionSelection.Text = "Edit group questions";
                        ////btnEditGroupQuestionSelection.Click += new EventHandler(btnEditGroupQuestionSimulation_Click);
                        //btnEditGroupQuestionSelection.Style["width"] = "180px";
                        //btnEditGroupQuestionSelection.CssClass = "button_continue";

                        ////建立間隔
                        //Label lblCellQuestionGroup = new Label();
                        //tcModify.Controls.Add(lblCellQuestionGroup);
                        //lblCellQuestionGroup.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                        //Button btnModifySim = new Button();
                        //tcModify.Controls.Add(btnModifySim);
                        //btnModifySim.ID = "btnModifySim-" + strQID;
                        //btnModifySim.Text = "Modify";
                        ////btnModifySim.Click += new EventHandler(btnModifySim_Click);
                        //btnModifySim.CommandArgument = strQID;
                        //btnModifySim.Style["width"] = "150px";
                        //btnModifySim.CssClass = "button_continue";

                        ////建立間隔
                        //Label lblCell = new Label();
                        //tcModify.Controls.Add(lblCell);
                        //lblCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                        //Button btnDeleteSimu = new Button();
                        //tcModify.Controls.Add(btnDeleteSimu);
                        //btnDeleteSimu.ID = "btnDeleteSelection-" + strQID;
                        //btnDeleteSimu.Text = "Delete";
                        //btnDeleteSimu.CommandArgument = strQID;
                        ////btnDeleteSimu.Click += new EventHandler(btnDeleteSimu_Click);
                        //btnDeleteSimu.Style["width"] = "150px";
                        //btnDeleteSimu.CssClass = "button_continue";


                        //建立Space
                        TableRow trSpace = new TableRow();
                        trSpace.Style.Add("background-color", "#EBECED");
                        table.Rows.Add(trSpace);

                        TableCell tcSpace = new TableCell();
                        tcSpace.ColumnSpan = 2;
                        tcSpace.Style.Add("height", "7px");
                        trSpace.Cells.Add(tcSpace);
                    }
                }
            }
        }

    }
    //建立同義問題或答案
    private void BulidInterrogation(string strType, string strQID, TableCell tcSynQuestion, string strAID)
    {
        DataTable dtSyn = new DataTable();
        if (strType == "Question")
        {
            dtSyn = clsInterrogationEnquiry.GetSynQuestion(strQID);
        }
        else if (strType == "Answer")
        {
            dtSyn = clsInterrogationEnquiry.GetSynAnswer(strQID, strAID);
        }

        if (dtSyn.Rows.Count > 0)
        {
            #region 同義問題或答案內容
            DataTable dtSynonymousItem = clsInterrogationEnquiry.GetSynonymousItem(strType, "synonymous", strQID, strAID);
            if (dtSynonymousItem.Rows.Count > 0)
            {
                string strDataName = "";//表示資料表欄位名稱
                string strMode = "";//表示是同義問題或答案的模式
                if (strType == "Question")
                {
                    strDataName = "cQDataKind";
                    strMode = "SynQ";
                }
                else if (strType == "Answer")
                {
                    strDataName = "cADataKind";
                    strMode = "SynA";
                }

                for (int iSyn = 0; iSyn < dtSynonymousItem.Rows.Count; iSyn++)
                {
                    #region 同義項目的序號
                    Label lbSynItemT = new Label();
                    if (iSyn == 0)
                    {
                        lbSynItemT.Text = "Syn. " + (iSyn + 1) + ": ";
                    }
                    else
                    {
                        lbSynItemT.Text = "<br/>Syn. " + (iSyn + 1) + ": ";
                    }
                    tcSynQuestion.Controls.Add(lbSynItemT);
                    #endregion

                    #region 同義項目的內容
                    if (dtSynonymousItem.Rows[iSyn][strDataName].ToString().IndexOf("Multimedia") != -1)
                    {
                        string strItemSeq = dtSynonymousItem.Rows[iSyn]["sSeq"].ToString();
                        string filePath = dtSynonymousItem.Rows[iSyn]["cItemValue"].ToString();
                        string strFileName = filePath.Split('/')[2];//檔名(含副檔名)
                        string strTemp = strFileName.Split('.')[1];//副檔名
                        string strFileNameNoVice = strFileName.Split('.')[0]; //檔名
                        if (strTemp == "swf")
                        {
                            Label lbFlash = new Label();
                            lbFlash.Text = "<object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' id='FlashPlayer" + strItemSeq + strFileNameNoVice + "' width='320' height='240' " +
                             " codebase='http://fpdownload.macromedia.com/get/flashplayer/current/swflash.cab'> " +
                             " <param name='pluginspage' value='http://www.macromedia.com/go/getflashplayer' /> " +
                             " <param name='movie' value='../../MultiMediaDB/Upload/Image/" + strFileName + "' /> " +
                             " <param name='quality' value='high' /> " +
                             " <param name='bgcolor' value='#869ca7' /> " +
                             " <param name='allowScriptAccess' value='always' /> " +
                             " <param name='Play' value='false' /> " +
                              "</object>" +
                              "<input id='btnPlay' type='button' value='Play' onclick=\"PlayFlash('" + strItemSeq + "','" + strFileNameNoVice + "')\"' style='width:100px' />";
                            tcSynQuestion.Controls.Add(lbFlash);
                        }
                        else if (strTemp == "mp3" || strTemp == "wav" || strTemp == "wmv" || strTemp == "avi")
                        {
                            Label lbMedia = new Label();
                            lbMedia.Text = " <object id='MediaPlayer' height='240' width='320' classid='CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6'> " +
                                           " <param name='AutoStart' value='false' />" +
                                           " <param name='uiMode' value='Full' />" +
                                           " <param name='enabled' value='true' />" +
                                           " <param name='URL' value='../../MultiMediaDB/Upload/Image/" + strFileName + "'>" +
                                           " </object> ";
                            tcSynQuestion.Controls.Add(lbMedia);
                        }
                        else if (strTemp == "jpg" || strTemp == "bmp" || strTemp == "gif" || strTemp == "png")
                        {
                            System.Web.UI.WebControls.Image imgPic = new System.Web.UI.WebControls.Image();
                            imgPic.ImageUrl = "../../MultiMediaDB/Upload/Image/" + strFileName + "";
                            imgPic.Width = Unit.Pixel(320);
                            imgPic.Height = Unit.Pixel(240);
                            tcSynQuestion.Controls.Add(imgPic);
                        }
                    }
                    else
                    {
                        Label lbSynItemValue = new Label();
                        lbSynItemValue.Text = dtSynonymousItem.Rows[iSyn]["cItemValue"].ToString();

                        tcSynQuestion.Controls.Add(lbSynItemValue);
                    }

                    #endregion


                }
            }
            #endregion
        }


    }

    //判斷是否具有同義問題
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public bool CheckSynQuestion(string strQID)
    {
        bool bCheck = false;
        DataTable dtItemForAskQuestion = clsInterrogationEnquiry.GetSynQuestion(strQID);
        if (dtItemForAskQuestion.Rows.Count > 0)
        {
            bCheck = true;
        }
        return bCheck;
    }

    //判斷是否具有同義答案
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public bool CheckSynAnswer(string strQID, int iSeq)
    {
        bool bCheck = false;
        PaperSystem.DataReceiver PaperSystemDataReceiver = new PaperSystem.DataReceiver();
        DataTable dtQuestionAnswer_Answer = PaperSystemDataReceiver.QuestionAnswer_Answer_SELECT_AllAnswers(strQID);
        string strAID = dtQuestionAnswer_Answer.Rows[iSeq - 1]["cAID"].ToString();
        DataTable dtItemForAskAnswer = clsInterrogationEnquiry.GetSynAnswer(strQID, strAID);
        if (dtItemForAskAnswer.Rows.Count > 0)
        {
            bCheck = true;
        }
        return bCheck;
    }

    private string GetSelectionItem()
    {
        string strSelectionItem = "";

        switch (strQuestionType)
        {
            //選擇題
            case "1":
                lbSelectionItem.Text = QuestionSelectionIndex_SELECT(strAssignedQID, strSelectionID).Rows[0]["cSelection"].ToString();
                break;

            //圖型題
            case "3":
                break;

            //對話題
            case "4":
                string[] strIndex = strSelectionID.Split('_');
                lbSelectionItem.Text = strIndex[1];
                break;

        }

        return strSelectionItem;
    }

    public static DataTable QuestionSelectionIndex_SELECT(string strQID, string strSelectionID)
    {
        DataTable dtResult = new DataTable();
        string strSQL = "SELECT * FROM QuestionSelectionIndex WHERE cQID = '" + strQID + "' AND cSelectionID = '" + strSelectionID + "' ";
        SqlDB myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
        DataTable dt = myDB.getDataSet(strSQL).Tables[0];
        if (dt.Rows.Count > 0)
        {
            dtResult = dt;
        }
        return dtResult;
    }

    //儲存選項的相關問題
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void SaveSelectionRelatedQuestion(string strQID, string strAssignQuestion, string strSelection, string strCheckBoxState)
    {
        clsHintsDB HintsDB = new clsHintsDB();
        string strSQL_Paper_QuestionSelectionGroupID = "";
        string strSQL_Paper_QuestionSelectionGroupItem = "";
        string strQuestionGroupID = "";
        if (strCheckBoxState == "true")
        {
            //先檢查Paper_QuestionSelectionGroupID是否已存在cGroupID
            strSQL_Paper_QuestionSelectionGroupID = "SELECT * FROM Paper_QuestionSelectionGroupID " +
                "WHERE cQID = '" + strAssignQuestion + "' AND cSelectionID = '" + strSelection + "'";
            DataTable dtPaper_QuestionSelectionGroupID = new DataTable();
            dtPaper_QuestionSelectionGroupID = HintsDB.getDataSet(strSQL_Paper_QuestionSelectionGroupID).Tables[0];
            ////////////////////////////////////////////////////////

            if (dtPaper_QuestionSelectionGroupID.Rows.Count > 0)
            {
                //取得QuestionGroupID
                strQuestionGroupID = dtPaper_QuestionSelectionGroupID.Rows[0]["cGroupID"].ToString();
                //////////////////////

                //取得最大的順序
                strSQL_Paper_QuestionSelectionGroupItem = "SELECT * FROM Paper_QuestionSelectionGroupItem " +
               "WHERE cGroupID = '" + strQuestionGroupID + "' ORDER BY  cSequence DESC";
                DataTable dtPaper_QuestionSelectionGroupItem = HintsDB.getDataSet(strSQL_Paper_QuestionSelectionGroupItem).Tables[0];
                string strMaxSeq = dtPaper_QuestionSelectionGroupItem.Rows[0]["cSequence"].ToString();
                ////////////////

                //新增資料
                strSQL_Paper_QuestionSelectionGroupItem = "INSERT INTO Paper_QuestionSelectionGroupItem (cGroupID, cQID, cSequence)" +
                   " VALUES('" + strQuestionGroupID + "','" + strQID + "','" + (Convert.ToInt32(strMaxSeq) + 1) + "')";
                HintsDB.ExecuteNonQuery(strSQL_Paper_QuestionSelectionGroupItem);
            }
            else
            {
                //新建QuestionGroupID
                strQuestionGroupID = "QuestionGroup_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                /////////////////////

                //新增一筆相關問題
                strSQL_Paper_QuestionSelectionGroupID = "INSERT INTO Paper_QuestionSelectionGroupID (cQID, cSelectionID, cGroupID)" +
                    " VALUES('" + strAssignQuestion + "','" + strSelection + "','" + strQuestionGroupID + "')";
                HintsDB.ExecuteNonQuery(strSQL_Paper_QuestionSelectionGroupID);

                //新增一筆題組的群組ID
                strSQL_Paper_QuestionSelectionGroupItem = "INSERT INTO Paper_QuestionSelectionGroupItem (cGroupID, cQID, cSequence)" +
                    " VALUES('" + strQuestionGroupID + "','" + strQID + "','1')";
                HintsDB.ExecuteNonQuery(strSQL_Paper_QuestionSelectionGroupItem);
            }
        }
        else
        {         
            DataTable dtPaper_QuestionSelectionGroupID = new DataTable();
            strSQL_Paper_QuestionSelectionGroupID = "SELECT * FROM Paper_QuestionSelectionGroupID " +
              "WHERE cQID = '" + strAssignQuestion + "' AND cSelectionID = '" + strSelection + "'";
            dtPaper_QuestionSelectionGroupID = HintsDB.getDataSet(strSQL_Paper_QuestionSelectionGroupID).Tables[0];
            strQuestionGroupID = dtPaper_QuestionSelectionGroupID.Rows[0]["cGroupID"].ToString();

            //取得刪除的問題順序
            strSQL_Paper_QuestionSelectionGroupItem = "SELECT * FROM Paper_QuestionSelectionGroupItem " +
            "WHERE cGroupID = '" + strQuestionGroupID + "' AND cQID = '" + strQID + "'";
            DataTable dtPaper_QuestionSelectionGroupItem = new DataTable();
            dtPaper_QuestionSelectionGroupItem = HintsDB.getDataSet(strSQL_Paper_QuestionSelectionGroupItem).Tables[0];
            string strDeleteSeq = dtPaper_QuestionSelectionGroupItem.Rows[0]["cSequence"].ToString();
            ////////////////////

            //刪除點選的問題資料
            strSQL_Paper_QuestionSelectionGroupItem = "DELETE Paper_QuestionSelectionGroupItem " +
                "WHERE cGroupID = '" + strQuestionGroupID + "' AND cQID = '" + strQID + "'";
            HintsDB.ExecuteNonQuery(strSQL_Paper_QuestionSelectionGroupItem);
            ////////////////////

            //更新此群組ID 比刪除的問題順序還大的問題 將順序-1
            strSQL_Paper_QuestionSelectionGroupItem = "SELECT * FROM Paper_QuestionSelectionGroupItem " +
               "WHERE cGroupID = '" + strQuestionGroupID + "' AND cSequence  > '" + strDeleteSeq + "' ORDER BY cSequence";
            dtPaper_QuestionSelectionGroupItem = HintsDB.getDataSet(strSQL_Paper_QuestionSelectionGroupItem).Tables[0];
            if (dtPaper_QuestionSelectionGroupItem.Rows.Count > 0)
            {
                foreach (DataRow drPaper_QuestionSelectionGroupItem in dtPaper_QuestionSelectionGroupItem.Rows)
                {
                    int iNewSeq = Convert.ToInt32(drPaper_QuestionSelectionGroupItem["cSequence"].ToString()) - 1;
                    strSQL_Paper_QuestionSelectionGroupItem = "UPDATE Paper_QuestionSelectionGroupItem SET cSequence = '" + iNewSeq + "' " +
                    " WHERE cGroupID = '" + strQuestionGroupID + "' AND cQID = '" + drPaper_QuestionSelectionGroupItem["cQID"].ToString() + "'";
                    HintsDB.ExecuteNonQuery(strSQL_Paper_QuestionSelectionGroupItem);
                }
            }
            //////////////////////////////////////////////////

            //檢查刪除問題後 是否為空 為空則刪除此選項的GroupID
            strSQL_Paper_QuestionSelectionGroupItem = "SELECT * FROM Paper_QuestionSelectionGroupItem " +
           "WHERE cGroupID = '" + strQuestionGroupID + "'";
            dtPaper_QuestionSelectionGroupItem = HintsDB.getDataSet(strSQL_Paper_QuestionSelectionGroupItem).Tables[0];
            if (dtPaper_QuestionSelectionGroupItem.Rows.Count > 0)
            {

            }
            else
            {
                strSQL_Paper_QuestionSelectionGroupID = "DELETE Paper_QuestionSelectionGroupID WHERE cGroupID = '" + strQuestionGroupID + "'";
                HintsDB.ExecuteNonQuery(strSQL_Paper_QuestionSelectionGroupID);
            }
        }
    }

    //儲存對話題的相關問題  老詹 2015/05/12
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void SaveConversationRelatedQuestion(string strQID, string strAssignQuestion, string strSelection, string strCheckBoxState, string strIsOriTopic, string strNewGroupID)
    {
        clsHintsDB HintsDB = new clsHintsDB();
        string strSQL_Paper_ConversationGroupID = "";
        string strSQL_Paper_ConversationGroupItem = "";
        string strQuestionGroupID = "";
        if (strCheckBoxState == "true")
        {
            //先檢查Paper_ConversationGroupID是否已存在cGroupID
            strSQL_Paper_ConversationGroupID = "SELECT * FROM Paper_ConversationGroupID " +
                "WHERE cQID = '" + strAssignQuestion + "' AND iSelectionID = '" + Convert.ToInt32(strSelection) + "'";
            DataTable dtPaper_ConversationGroupID = new DataTable();
            dtPaper_ConversationGroupID = HintsDB.getDataSet(strSQL_Paper_ConversationGroupID).Tables[0];

            if (dtPaper_ConversationGroupID.Rows.Count > 0)
            {
                strQuestionGroupID = strAssignQuestion + "_" + strSelection;
                //取得最大的順序
                strSQL_Paper_ConversationGroupItem = "SELECT * FROM Paper_QuestionSelectionGroupItem " +
               "WHERE cGroupID = '" + strQuestionGroupID + "' ORDER BY cSequence DESC";
                DataTable dtPaper_ConversationGroupItem = HintsDB.getDataSet(strSQL_Paper_ConversationGroupItem).Tables[0];
                string strMaxSeq = dtPaper_ConversationGroupItem.Rows[0]["cSequence"].ToString();
                ////////////////

                //新增資料
                strSQL_Paper_ConversationGroupItem = "INSERT INTO Paper_QuestionSelectionGroupItem (cGroupID, cQID, cSequence)" +
                   " VALUES('" + strQuestionGroupID + "','" + strQID + "','" + (Convert.ToInt32(strMaxSeq) + 1) + "')";
                HintsDB.ExecuteNonQuery(strSQL_Paper_ConversationGroupItem);
            }
            else
            {
                //新建QuestionGroupID
                strQuestionGroupID = strAssignQuestion + "_" + strSelection;

                //新增一筆相關問題
                strSQL_Paper_ConversationGroupID = "INSERT INTO Paper_ConversationGroupID (cQID, iSelectionID, bIsReturn, cDisplayTime)" +
                    " VALUES('" + strAssignQuestion + "','" + Convert.ToInt32(strSelection) + "','0','1|Y')";
                HintsDB.ExecuteNonQuery(strSQL_Paper_ConversationGroupID);

                //新增一筆題組的群組ID
                strSQL_Paper_ConversationGroupItem = "INSERT INTO Paper_QuestionSelectionGroupItem (cGroupID, cQID, cSequence)" +
                    " VALUES('" + strQuestionGroupID + "','" + strQID + "','1')";
                HintsDB.ExecuteNonQuery(strSQL_Paper_ConversationGroupItem);              
            }
            #region 先刪除BasicQuestionList資料表的資料(以便後面INSERT)
            string strGetSQL = "SELECT * FROM Paper_QuestionSelectionGroupItem WHERE cGroupID LIKE '" + strQuestionGroupID + "' ORDER BY cSequence";
            DataTable dtAllGroup = HintsDB.getDataSet(strGetSQL).Tables[0];
            foreach (DataRow dr in dtAllGroup.Rows)
            {
                string strSQL = "SELECT * FROM BasicQuestionList WHERE cQID='" + dr["cQID"].ToString() + "'";
                DataTable dtCheckGroupInPaper = HintsDB.getDataSet(strSQL).Tables[0];
                if (dtCheckGroupInPaper.Rows.Count > 0)
                {
                    string strDELETESQL = "DELETE BasicQuestionList WHERE cQID='" + dr["cQID"].ToString() + "'";
                    HintsDB.ExecuteNonQuery(strDELETESQL);
                }
            }
            #endregion
        }
        else
        {           
            strSQL_Paper_ConversationGroupID = "SELECT * FROM Paper_ConversationGroupID " +
              "WHERE cQID = '" + strAssignQuestion + "' AND iSelectionID = '" + Convert.ToInt32(strSelection) + "'";
            DataTable dtPaper_ConversationGroupID = HintsDB.getDataSet(strSQL_Paper_ConversationGroupID).Tables[0];
            strQuestionGroupID = dtPaper_ConversationGroupID.Rows[0]["cQID"].ToString() + "_" + dtPaper_ConversationGroupID.Rows[0]["iSelectionID"].ToString();

            #region 先刪除BasicQuestionList資料表的資料(以便後面INSERT)
            string strGetSQL = "SELECT * FROM Paper_QuestionSelectionGroupItem WHERE cGroupID LIKE '" + strQuestionGroupID + "' ORDER BY cSequence";
            DataTable dtAllGroup = HintsDB.getDataSet(strGetSQL).Tables[0];
            foreach (DataRow dr in dtAllGroup.Rows)
            {
                string strSQL = "SELECT * FROM BasicQuestionList WHERE cQID='" + dr["cQID"].ToString() + "'";
                DataTable dtCheckGroupInPaper = HintsDB.getDataSet(strSQL).Tables[0];
                if (dtCheckGroupInPaper.Rows.Count > 0)
                {
                    string strDELETESQL = "DELETE BasicQuestionList WHERE cQID='" + dr["cQID"].ToString() + "'";
                    HintsDB.ExecuteNonQuery(strDELETESQL);
                }
            }
            #endregion

            //取得刪除的問題順序
            strSQL_Paper_ConversationGroupItem = "SELECT * FROM Paper_QuestionSelectionGroupItem " +
            "WHERE cGroupID = '" + strQuestionGroupID + "' AND cQID = '" + strQID + "'";
            DataTable dtPaper_ConversationGroupItem = new DataTable();
            dtPaper_ConversationGroupItem = HintsDB.getDataSet(strSQL_Paper_ConversationGroupItem).Tables[0];
            string strDeleteSeq = dtPaper_ConversationGroupItem.Rows[0]["cSequence"].ToString();

            //刪除點選的問題資料
            strSQL_Paper_ConversationGroupItem = "DELETE Paper_QuestionSelectionGroupItem " +
                "WHERE cGroupID = '" + strQuestionGroupID + "' AND cQID = '" + strQID + "'";
            HintsDB.ExecuteNonQuery(strSQL_Paper_ConversationGroupItem);

            //更新此群組ID 比刪除的問題順序還大的問題 將順序-1
            strSQL_Paper_ConversationGroupItem = "SELECT * FROM Paper_QuestionSelectionGroupItem " +
               "WHERE cGroupID = '" + strQuestionGroupID + "' AND cSequence  > '" + strDeleteSeq + "' ORDER BY cSequence";
            dtPaper_ConversationGroupItem = HintsDB.getDataSet(strSQL_Paper_ConversationGroupItem).Tables[0];
            if (dtPaper_ConversationGroupItem.Rows.Count > 0)
            {
                foreach (DataRow drPaper_ConversationGroupItem in dtPaper_ConversationGroupItem.Rows)
                {
                    int iNewSeq = Convert.ToInt32(drPaper_ConversationGroupItem["cSequence"].ToString()) - 1;
                    strSQL_Paper_ConversationGroupItem = "UPDATE Paper_QuestionSelectionGroupItem SET cSequence = '" + iNewSeq + "' " +
                    " WHERE cGroupID = '" + strQuestionGroupID + "' AND cQID = '" + drPaper_ConversationGroupItem["cQID"].ToString() + "'";
                    HintsDB.ExecuteNonQuery(strSQL_Paper_ConversationGroupItem);
                }
            }

            //檢查刪除問題後 是否為空 為空則刪除此選項的GroupID
            strSQL_Paper_ConversationGroupItem = "SELECT * FROM Paper_QuestionSelectionGroupItem " +
           "WHERE cGroupID = '" + strQuestionGroupID + "'";
            dtPaper_ConversationGroupItem = HintsDB.getDataSet(strSQL_Paper_ConversationGroupItem).Tables[0];
            if (dtPaper_ConversationGroupItem.Rows.Count <= 0)
            {
                strSQL_Paper_ConversationGroupID = "DELETE Paper_ConversationGroupID WHERE cQID = '" + strAssignQuestion + "'";
                HintsDB.ExecuteNonQuery(strSQL_Paper_ConversationGroupID);

                string strDeleteTmpSQL = "DELETE Paper_ConversationGroupItem WHERE cGroupID = '" + strQuestionGroupID + "'";
                HintsDB.ExecuteNonQuery(strDeleteTmpSQL);
            }
        }
        //於Paper_QuestionSelectionGroupItem新增完後額外 新增/更新 至Paper_ConversationGroupItem
        string strTmpSQL = "SELECT * FROM Paper_QuestionSelectionGroupItem WHERE cGroupID='" + strQuestionGroupID + "' ORDER BY cSequence";
        DataTable dtTmp = HintsDB.getDataSet(strTmpSQL).Tables[0];
        DataRow[] drMax = dtTmp.Select("cSequence = MAX(cSequence)");
        string strNewTmpSQL = "SELECT * FROM Paper_ConversationGroupItem WHERE cGroupID='" + strQuestionGroupID + "'";
        DataTable dtNewTmp = HintsDB.getDataSet(strNewTmpSQL).Tables[0];
        if (dtTmp.Rows.Count > 0)
        {
            if (strIsOriTopic == "0")
            {
                string strNewTopicSQL = "SELECT * FROM Paper_ConversationGroupItem WHERE cNewTopic='" + strNewGroupID + "'";
                DataTable dtNewTopic = HintsDB.getDataSet(strNewTopicSQL).Tables[0];
                if (dtNewTopic.Rows.Count > 0)
                {
                    strSQL_Paper_ConversationGroupItem = "UPDATE Paper_ConversationGroupItem SET cQID_first='" + dtNewTopic.Rows[0]["cQID_end"].ToString() + "', cQID_end='" + drMax[0]["cQID"].ToString() + "' WHERE cGroupID='" + strQuestionGroupID + "' AND cNewTopic='" + strNewGroupID + "'";
                    HintsDB.ExecuteNonQuery(strSQL_Paper_ConversationGroupItem);
                }
                else
                {                    
                    strSQL_Paper_ConversationGroupItem = "INSERT INTO Paper_ConversationGroupItem (cGroupID, cNewTopic, cQID_first, cQID_end)" +
                        " VALUES('" + strQuestionGroupID + "','" + strNewGroupID + "','" + drMax[0]["cQID"].ToString() + "','" + drMax[0]["cQID"].ToString() + "')";
                    HintsDB.ExecuteNonQuery(strSQL_Paper_ConversationGroupItem);
                }
            }
            else
            {
                if (dtNewTmp.Rows.Count > 0)
                {
                    strSQL_Paper_ConversationGroupItem = "UPDATE Paper_ConversationGroupItem SET cQID_first='" + dtTmp.Rows[0]["cQID"].ToString() + "', cQID_end='" + drMax[0]["cQID"].ToString() + "' WHERE cGroupID='" + strQuestionGroupID + "' AND cNewTopic=''";
                    HintsDB.ExecuteNonQuery(strSQL_Paper_ConversationGroupItem);
                }
                else
                {
                    strSQL_Paper_ConversationGroupItem = "INSERT INTO Paper_ConversationGroupItem (cGroupID, cNewTopic, cQID_first, cQID_end)" +
                        " VALUES('" + strQuestionGroupID + "','','" + dtTmp.Rows[0]["cQID"].ToString() + "','" + drMax[0]["cQID"].ToString() + "')";
                    HintsDB.ExecuteNonQuery(strSQL_Paper_ConversationGroupItem);
                }
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string[] strIndex = strSelectionID.Split('_');
        clsHintsDB HintsDB = new clsHintsDB();
        if (rbGoToNew.Checked == true)
        {
            string strUpdateSQL = "UPDATE Paper_ConversationGroupID SET bIsReturn='0' WHERE cQID='" + strAssignedQID + "' AND iSelectionID = '"+ Convert.ToInt32(strIndex[0]) +"'";
            HintsDB.ExecuteNonQuery(strUpdateSQL);
        }
        else
        {
            string strUpdateSQL = "UPDATE Paper_ConversationGroupID SET bIsReturn='1' WHERE cQID='" + strAssignedQID + "' AND iSelectionID = '" + Convert.ToInt32(strIndex[0]) + "'";
            HintsDB.ExecuteNonQuery(strUpdateSQL);
        }

        if (rbDisplayBoth.Checked == true)
        {
            if (rbGiveWarningYes.Checked == true)
            {
                string strUpdateSQL = "UPDATE Paper_ConversationGroupID SET cDisplayTime ='1|Y' WHERE cQID='" + strAssignedQID + "' AND iSelectionID = '" + Convert.ToInt32(strIndex[0]) + "'";
                HintsDB.ExecuteNonQuery(strUpdateSQL);
            }
            else if (rbGiveWarningNo.Checked == true)
            {
                string strUpdateSQL = "UPDATE Paper_ConversationGroupID SET cDisplayTime ='1|N' WHERE cQID='" + strAssignedQID + "' AND iSelectionID = '" + Convert.ToInt32(strIndex[0]) + "'";
                HintsDB.ExecuteNonQuery(strUpdateSQL);
            }
        }
        else if (rbDisplayWhenTrue.Checked == true)
        {          
            string strUpdateSQL = "UPDATE Paper_ConversationGroupID SET cDisplayTime ='2|#' WHERE cQID='" + strAssignedQID + "' AND iSelectionID = '" + Convert.ToInt32(strIndex[0]) + "'";
            HintsDB.ExecuteNonQuery(strUpdateSQL);
        }

        #region 將新題組題目插入BasicQuestionList資料表
        string iQuestionSerialNum = "Paper";
        //以當下日期時間作為流水號
        DateTime now = DateTime.Now;
        string temp = now.ToString("yyyyMMddHHmmssFFFFF");
        //建立QID
        strPaperID = iQuestionSerialNum + "_" + temp;

        string strCheckSQL = "SELECT * FROM Paper_QuestionSelectionGroupItem AS I, BasicQuestionList AS B WHERE I.cQID=B.cQID AND I.cGroupID = '" + (strAssignedQID + "_" + strIndex[0]) + "'";
        DataTable dtCheck = HintsDB.getDataSet(strCheckSQL).Tables[0];
        string strGetSQL = "SELECT * FROM Paper_QuestionSelectionGroupItem WHERE cGroupID = '" + (strAssignedQID + "_" + strIndex[0]) + "' ORDER BY cSequence";
        DataTable dtAllGroup = HintsDB.getDataSet(strGetSQL).Tables[0];
        string strTmpSQL = "SELECT * FROM Paper_ConversationGroupItem WHERE cGroupID = '" + (strAssignedQID + "_" + strIndex[0]) + "'";
        DataTable dtTmp = HintsDB.getDataSet(strTmpSQL).Tables[0];      
        if (dtTmp.Rows[0]["cNewTopic"].ToString() == "")
        {
            if (dtCheck.Rows.Count <= 0) // 防止重複插入
            {
                foreach (DataRow dr in dtAllGroup.Rows)
                {
                    string strInsertSQL = "INSERT INTO BasicQuestionList (cPaperID, cQuestionTopic, cQID, cVPAID, cTestAnswerType, cCaseID, bIsOriginal) " +
                            "VALUES ('" + strPaperID + "' , '" + (strGroupID + "/" + dr["cQID"].ToString()) + "' , '" + dr["cQID"].ToString() + "', '' , '" + hfCurrentProType.Value.ToString() + "', '" + usi.CaseID + "', '0') ";
                    HintsDB.ExecuteNonQuery(strInsertSQL);
                }
            }
            RegisterStartupScript("", "<script>ClosePage();</script>");
        }
        else
        {
            if (dtCheck.Rows.Count <= 0) // 防止重複插入
            {
                foreach (DataRow dr in dtAllGroup.Rows)
                {
                    string strGetEachGroupSQL = "SELECT cQuestionGroupID FROM QuestionMode WHERE cQID='" + dr["cQID"].ToString() + "'";
                    DataTable dtEachGroup = HintsDB.getDataSet(strGetEachGroupSQL).Tables[0];
                    string strInsertSQL = "INSERT INTO BasicQuestionList (cPaperID, cQuestionTopic, cQID, cVPAID, cTestAnswerType, cCaseID, bIsOriginal) " +
                            "VALUES ('" + strPaperID + "' , '" + (dtTmp.Rows[0]["cNewTopic"].ToString() + "/" + dtEachGroup.Rows[0]["cQuestionGroupID"].ToString()) + "' , '" + dr["cQID"].ToString() + "', '' , '" + hfCurrentProType.Value.ToString() + "', '" + usi.CaseID + "', '0') ";
                    HintsDB.ExecuteNonQuery(strInsertSQL);
                }
            }
            RegisterStartupScript("", "<script>GoToNewTopicEdit('" + dtTmp.Rows[0]["cNewTopic"].ToString() + "', '" + usi.CaseID + "', '"+ Session["tmpCareer"].ToString() +"');</script>");
        }
        #endregion 
    }
    protected void rbDisplayBoth_CheckedChanged(object sender, EventArgs e)
    {
        rbGiveWarningYes.Checked = true;
        rbGiveWarningYes.Enabled = true;
        rbGiveWarningNo.Enabled = true;
    }
    protected void rbDisplayWhenTrue_CheckedChanged(object sender, EventArgs e)
    {
        rbGiveWarningYes.Checked = false;
        rbGiveWarningNo.Checked = false;
        rbGiveWarningYes.Enabled = false;
        rbGiveWarningNo.Enabled = false;
    }
}
