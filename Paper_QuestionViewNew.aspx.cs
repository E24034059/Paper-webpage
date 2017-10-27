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
using AuthoringTool.QuestionEditLevel;
using Hints.Learning.Question;
using Hints.DB.Section;
using Hints.DB;
using Hints.DB.Conversation;

namespace PaperSystem
{
    /// <summary>
    /// Paper_QuestionViewNew 的摘要描述。
    /// </summary>
    public partial class Paper_QuestionViewNew : AuthoringTool_BasicForm_BasicForm
    {
        //建立SqlDB物件
        SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
        DataReceiver myReceiver = new DataReceiver();
        SQLString mySQL = new SQLString();

        //問題組別的ID
        protected string strGroupID;

        //問題組別的名稱
        protected string strGroupName;

        //題目的編號
        int intQuestionIndex = 0;

        //問題組別的流水號
        protected int iQuestionClassifyID = 0;

        //由哪個頁面來
        private string strPreOpener = "";

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.Initiate();

            Ajax.Utility.RegisterTypeForAjax(typeof(Paper_QuestionViewNew));

            //接收參數
            this.getParameter();

            if (!IsPostBack)
            {
                //如果從課堂練習編輯考卷頁面 新增題目進來 直接導向新增題目頁面
                //if (Session["IsFromClassExercise"] != null && Session["IsFromClassExercise"].ToString().Equals("True"))
                //{
                //    strGroupID = Session["GroupID"].ToString();
                //    Response.Redirect("Paper_QuestionTypeNew.aspx?Opener=Paper_QuestionViewNew&GroupID=" + strGroupID + "&bModify=False");
                //}

                hfSymptoms.Value = "All";
                ddlSymptoms.Items.Clear();
                //建立病徵的下拉選單項目 
                DataTable dtDiseaseSymptomsTree = DiseaseSymptomsTree_SELECT();
                ddlSymptoms.Items.Add("All");
                foreach (DataRow drDiseaseSymptomsTree in dtDiseaseSymptomsTree.Rows)
                {
                    ddlSymptoms.Items.Add(drDiseaseSymptomsTree["cNodeName"].ToString());
                }
            }

            intQuestionIndex = 0;

            if (Request.QueryString["CaseID"] != null)
            {
                //建立情境題表格
                this.setupSituationTable();
            }
            else
            {
                //建立對話題表格
                this.setupConversationQuestionTable();

                //建立選擇題表格
                this.setupQuestionTable();

                //建立選擇題包含理由表格
                this.setupQuestionWithKeyWordsTable();

                //建立問答題表格
                this.setupTextQuestionTable();

                //建立圖形題表格
                this.setupSimulatorQuestionTable();

                //建立情境題表格
                this.setupSituationTable();
            }

            //加入Delete button的事件
            btnDeleteQuestion.ServerClick += new ImageClickEventHandler(btnDeleteQuestion_ServerClick);

            //加入btModify的事件
            btModify.ServerClick += new EventHandler(btModify_ServerClick);

            //如果由課堂練習編輯考卷來
            if (strPreOpener.Equals("SelectPaperModeAddANewQuestion"))
            {
                btnNext.Attributes["onclick"] = "window.close();";
            }

            //從編輯考卷新增題目來
            if (strPreOpener.Equals("SelectPaperModeAddANewQuestion"))
            {
                btnNext.Attributes.Add("style", "display:none;");
            }
        }
        //建立對話題表格
        private void setupConversationQuestionTable()
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

            //依照QuestionMode決定取出此組別的對話題
            string strSQL = mySQL.getGroupConversation(Session["GroupID"].ToString());

            DataSet dsQuestionList = sqldb.getDataSet(strSQL);

            if (dsQuestionList.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsQuestionList.Tables[0].Rows.Count; i++)
                {
                    string strGetKeyword = Hints.Learning.Question.DataReceiver.getTextQuestionKeyword(dsQuestionList.Tables[0].Rows[i]["cQID"].ToString());
                    string[] arrKeyword = strGetKeyword.Split(',');

                    //取得QID
                    string strQID = "";
                    strQID = dsQuestionList.Tables[0].Rows[i]["cQID"].ToString();

                    //取得QuestionType
                    string strQuestionType = "4";
                    strQuestionType = dsQuestionList.Tables[0].Rows[i]["cQuestionType"].ToString();

                    //取得Question
                    string strConversationQuestion = "";
                    strConversationQuestion = dsQuestionList.Tables[0].Rows[i]["cQuestion"].ToString();

                    //取得病徵
                    string strSymptoms = "";
                    strSymptoms = AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevel_SELECT_QuestionSymptoms(strQID);
                    if (hfSymptoms.Value == "All" || hfSymptoms.Value == strSymptoms)
                    {
                        intQuestionIndex += 1;
                        #region 建立對話題的內容
                        TableRow trConversationQuestionTitle = new TableRow();
                        trConversationQuestionTitle.Attributes.Add("Class", "header1_table_first_row");
                        trConversationQuestionTitle.Style.Add("CURSOR", "hand");
                        table.Rows.Add(trConversationQuestionTitle);

                        TableCell tcConversationQuestionTitle = new TableCell();
                        trConversationQuestionTitle.Cells.Add(tcConversationQuestionTitle);
                        tcConversationQuestionTitle.Text = "Q" + intQuestionIndex.ToString() + ": ";
                        tcConversationQuestionTitle.Text = "<IMG id='img_" + strQID + "' src='../../../BasicForm/Image/minus.gif'>&nbsp;Q" + intQuestionIndex.ToString() + " : ";
                        tcConversationQuestionTitle.ColumnSpan = 2;

                        //建立問題的標題
                        TableRow trQuestionTitle = new TableRow();
                        trQuestionTitle.Attributes.Add("Class", "header1_tr_odd_row");
                        table.Rows.Add(trQuestionTitle);
                        trQuestionTitle.ID = "trConversationTitle_" + strQID;

                        TableCell tcQuestionTitle = new TableCell();
                        trQuestionTitle.Cells.Add(tcQuestionTitle);
                        tcQuestionTitle.Text = "<font style='color:Black; font-weight:bold'>Question :&nbsp;<font/>";
                        tcQuestionTitle.Style.Add("text-align", "right");
                        tcQuestionTitle.Width = Unit.Pixel(230);

                        TableCell tcQuestion = new TableCell();
                        trQuestionTitle.Cells.Add(tcQuestion);
                        tcQuestion.Attributes.Add("Class", "header1_tr_even_row");
                        tcQuestion.Width = Unit.Percentage(81);

                        if (arrKeyword.Length > 0)
                        {
                            for (int kcount = 0; kcount < arrKeyword.Length; kcount++)
                            {
                                strConversationQuestion = strConversationQuestion.Replace(arrKeyword[kcount], "<span class='span_keyword' >" + arrKeyword[kcount] + "</span>"); ;
                                tcQuestion.Text = strConversationQuestion;
                            }
                        }

                        //建立同義問題的標題
                        TableRow trSynQuestionTitle = new TableRow();
                        trSynQuestionTitle.Attributes.Add("Class", "header1_tr_odd_row");
                        table.Rows.Add(trSynQuestionTitle);
                        trSynQuestionTitle.ID = "trSynConversationTitle_" + strQID;
                        trSynQuestionTitle.Style.Add("CURSOR", "hand");

                        TableCell tcSynQuestionTitle = new TableCell();
                        trSynQuestionTitle.Cells.Add(tcSynQuestionTitle);
                        tcSynQuestionTitle.Text = "<font style='color:Black; font-weight:bold'>Synonymous Question : <font/>";
                        tcSynQuestionTitle.Style.Add("text-align", "right");
                        tcSynQuestionTitle.Width = Unit.Pixel(230);

                        TableCell tcSynQuestion = new TableCell();
                        trSynQuestionTitle.Cells.Add(tcSynQuestion);
                        BulidInterrogation("Question", strQID, tcSynQuestion, "");
                        tcSynQuestion.Attributes.Add("Class", "header1_tr_even_row");

                        DataTable dtConversation_AnswerType = clsConversation.Conversation_AnswerType_SELECT_AllAnswerType(iQuestionClassifyID);
                        hfAnswerTypeCount.Value = "0";
                        if (dtConversation_AnswerType.Rows.Count > 0)
                        {
                            hfAnswerTypeCount.Value = dtConversation_AnswerType.Rows.Count.ToString();
                            trConversationQuestionTitle.Attributes.Add("onclick", "ShowConversationDetail('" + strQID + "','img_" + strQID + "','" + hfAnswerTypeCount.Value + "')");

                            int iACount = 1;
                            foreach (DataRow drConversation_AnswerType in dtConversation_AnswerType.Rows)
                            {
                                string strAnswerTypeNum = "";
                                string strAnswerTypeName = "";
                                strAnswerTypeNum = drConversation_AnswerType["cAnswerTypeNum"].ToString();
                                strAnswerTypeName = drConversation_AnswerType["cAnswerTypeName"].ToString();

                                //建立Answer Type的標題
                                TableRow trAnswerTypeTitle = new TableRow();
                                trAnswerTypeTitle.Attributes.Add("Class", "header1_tr_odd_row");
                                table.Rows.Add(trAnswerTypeTitle);
                                trAnswerTypeTitle.ID = "trAnswerTypeTitle_" + strQID + "_" + iACount;

                                TableCell tcAnswerTypeTitle = new TableCell();
                                trAnswerTypeTitle.Cells.Add(tcAnswerTypeTitle);
                                RadioButton rbAnswerType = new RadioButton();
                                rbAnswerType.ID = "rbAnswerType|" + strQID + "|" + strAnswerTypeNum;
                                rbAnswerType.GroupName = "rbAnswerTypeGroup_" + strQID;
                                rbAnswerType.Text = "<font style='color:Black; font-weight:bold'>Answer Type " + iACount + "  :&nbsp;<font/>";
                                tcAnswerTypeTitle.Controls.Add(rbAnswerType);
                                if (iACount == 1)
                                    rbAnswerType.Checked = true;
                                tcAnswerTypeTitle.Style.Add("text-align", "right");
                                tcAnswerTypeTitle.Width = Unit.Pixel(230);


                                TableCell tcAnswer = new TableCell();
                                trAnswerTypeTitle.Cells.Add(tcAnswer);

                                for (int kcount = 0; kcount < arrKeyword.Length; kcount++)
                                {
                                    strAnswerTypeName = strAnswerTypeName.Replace(arrKeyword[kcount], "<span class='span_keyword' >" + arrKeyword[kcount] + "</span>"); ;
                                }

                                tcAnswer.Text = strAnswerTypeName;
                                tcAnswer.Attributes.Add("Class", "header1_tr_even_row");

                                //建立答案型態的答案內容的標題
                                TableRow trAnswerTypeContentTitle = new TableRow();
                                trAnswerTypeContentTitle.Attributes.Add("Class", "header1_tr_odd_row");
                                table.Rows.Add(trAnswerTypeContentTitle);
                                trAnswerTypeContentTitle.ID = "trAnswerTypeContentTitle_" + strQID + "_" + iACount;
                                trAnswerTypeContentTitle.Style.Add("CURSOR", "hand");

                                TableCell tcAnswerTypeContentTitle = new TableCell();
                                trAnswerTypeContentTitle.Cells.Add(tcAnswerTypeContentTitle);
                                tcAnswerTypeContentTitle.Text = "<font style='color:Black; font-weight:bold'>Answer Content " + iACount + ":&nbsp;<font/>";
                                tcAnswerTypeContentTitle.Style.Add("text-align", "right");
                                tcAnswerTypeContentTitle.Width = Unit.Pixel(230);

                                TableCell tcAnswerTypeContent = new TableCell();
                                trAnswerTypeContentTitle.Cells.Add(tcAnswerTypeContent);
                                BulidAnswerTypeContent("Answer", strQID, tcAnswerTypeContent, iACount);
                                tcAnswerTypeContent.Attributes.Add("Class", "header1_tr_even_row");

                                iACount++;

                                #region 管控同義項的顯示與隱藏
                                DataTable dtConversationAnswer = new DataTable();
                                dtConversationAnswer = clsConversation.Conversation_Answer_SELECT_Answer(strQID, strAnswerTypeName);
                                if (dtConversationAnswer.Rows.Count == 0)
                                {
                                    trAnswerTypeContentTitle.Style.Add("display", "none");
                                    //trSynAnswer.Style.Add("display", "none");
                                }
                                #endregion
                            }
                        }

                        //建立Keyword的標題
                        TableRow trKeywordTitle = new TableRow();
                        trKeywordTitle.Attributes.Add("Class", "header1_tr_odd_row");
                        //table.Rows.Add(trKeywordTitle);
                        trKeywordTitle.ID = "trKeywordTitle_" + strQID;

                        TableCell tcKeywordTitle = new TableCell();
                        trKeywordTitle.Cells.Add(tcKeywordTitle);
                        tcKeywordTitle.Text = "<font style='color:Black; font-weight:bold'>Keyword :&nbsp; <font/>";
                        tcKeywordTitle.Style.Add("text-align", "right");
                        tcKeywordTitle.Width = Unit.Pixel(230);

                        TableCell tcKeyword = new TableCell();
                        trKeywordTitle.Cells.Add(tcKeyword);
                        tcKeyword.Text = strGetKeyword;
                        tcKeyword.Attributes.Add("Class", "header1_tr_even_row");

                        //建立修改按鈕的TableRow
                        TableRow trModify = new TableRow();
                        table.Rows.Add(trModify);
                        trModify.ID = "trModify_" + strQID;

                        TableCell tcModify = new TableCell();
                        trModify.Cells.Add(tcModify);
                        tcModify.Attributes["align"] = "right";
                        tcModify.ColumnSpan = 2;


                        //建立修改問題的Button
                        Button btnModifyConversation = new Button();
                        tcModify.Controls.Add(btnModifyConversation);
                        btnModifyConversation.Style["width"] = "150px";
                        btnModifyConversation.ID = "btnModifyConversation-" + strQID;
                        btnModifyConversation.Text = "Modify";
                        btnModifyConversation.Click += new EventHandler(btnModifyConversation_Click);
                        btnModifyConversation.CssClass = "button_continue";

                        //建立間隔
                        Label lblCell = new Label();
                        tcModify.Controls.Add(lblCell);
                        lblCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                        //建立刪除對話題的Button
                        Button btnDeleteConversation = new Button();
                        tcModify.Controls.Add(btnDeleteConversation);
                        btnDeleteConversation.Style["width"] = "150px";
                        btnDeleteConversation.ID = "btnDeleteConversation-" + strQID;
                        btnDeleteConversation.Text = "Delete";
                        btnDeleteConversation.Click += new EventHandler(btnDeleteConversation_Click);
                        btnDeleteConversation.CssClass = "button_continue";


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
                            //trSynQuestion.Style.Add("display", "none");
                        }
                        #endregion

                        #endregion
                    }
                }
            }
            else
            {
                //此問卷沒有任何對話題的情況
                trConversationQuestionTable.Style["display"] = "none";
            }
            dsQuestionList.Dispose();


        }
        //建立問答題表格
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
            string strSQL = mySQL.getGroupQuestionAnswer(Session["GroupID"].ToString());

            DataSet dsQuestionList = sqldb.getDataSet(strSQL);

            if (dsQuestionList.Tables[0].Rows.Count > 0)
            {
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

                    //取得病徵
                    string strSymptoms = "";
                    strSymptoms = AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevel_SELECT_QuestionSymptoms(strQID);
                    if (hfSymptoms.Value == "All" || hfSymptoms.Value == strSymptoms)
                    {
                        intQuestionIndex += 1;
                        #region 建立問答題的內容
                        TableRow trTextQuestionTitle = new TableRow();
                        trTextQuestionTitle.Attributes.Add("Class", "header1_table_first_row");
                        trTextQuestionTitle.Style.Add("CURSOR", "hand");
                        table.Rows.Add(trTextQuestionTitle);

                        TableCell tcTextQuestionTitle = new TableCell();
                        trTextQuestionTitle.Cells.Add(tcTextQuestionTitle);
                        tcTextQuestionTitle.Text = "Q" + intQuestionIndex.ToString() + ": ";
                        tcTextQuestionTitle.Text = "<IMG id='img_" + strQID + "' src='../../../BasicForm/Image/minus.gif'>&nbsp;Q" + intQuestionIndex.ToString() + " : ";
                        tcTextQuestionTitle.ColumnSpan = 2;

                        //建立問題的標題
                        TableRow trQuestionTitle = new TableRow();
                        trQuestionTitle.Attributes.Add("Class", "header1_tr_odd_row");
                        table.Rows.Add(trQuestionTitle);
                        trQuestionTitle.ID = "trQuestionTitle_" + strQID;

                        TableCell tcQuestionTitle = new TableCell();
                        trQuestionTitle.Cells.Add(tcQuestionTitle);
                        tcQuestionTitle.Text = "<font style='color:Black; font-weight:bold'>Question :&nbsp;<font/>";
                        tcQuestionTitle.Style.Add("text-align", "right");
                        tcQuestionTitle.Width = Unit.Pixel(230);

                        //建立問題的內容
                        //TableRow trQuestion = new TableRow();
                        //trQuestion.Attributes.Add("Class", "header1_tr_even_row");
                        //table.Rows.Add(trQuestion);
                        //trQuestion.ID = "trQuestion_" + strQID;

                        TableCell tcQuestion = new TableCell();
                        trQuestionTitle.Cells.Add(tcQuestion);
                        tcQuestion.Attributes.Add("Class", "header1_tr_even_row");
                        tcQuestion.Width = Unit.Percentage(81);

                        if (arrKeyword.Length > 0)
                        {
                            for (int kcount = 0; kcount < arrKeyword.Length; kcount++)
                            {
                                strQuestion = strQuestion.Replace(arrKeyword[kcount], "<span class='span_keyword' >" + arrKeyword[kcount] + "</span>"); ;
                                tcQuestion.Text = strQuestion;
                            }
                        }

                        //建立同義問題的標題
                        TableRow trSynQuestionTitle = new TableRow();
                        trSynQuestionTitle.Attributes.Add("Class", "header1_tr_odd_row");
                        table.Rows.Add(trSynQuestionTitle);
                        trSynQuestionTitle.ID = "trSynQuestionTitle_" + strQID;
                        trSynQuestionTitle.Style.Add("CURSOR", "hand");

                        TableCell tcSynQuestionTitle = new TableCell();
                        trSynQuestionTitle.Cells.Add(tcSynQuestionTitle);
                        //tcSynQuestionTitle.Text = "<IMG id='imgSynQuestion_" + strQID + "' src='../../../BasicForm/Image/plus.gif'><font style='color:Black; font-weight:bold'>&nbsp;Synonymous Question : <font/>";
                        tcSynQuestionTitle.Text = "<font style='color:Black; font-weight:bold'>Synonymous Question : <font/>";
                        tcSynQuestionTitle.Style.Add("text-align", "right");
                        tcSynQuestionTitle.Width = Unit.Pixel(230);

                        //建立同義問題的內容
                        //TableRow trSynQuestion = new TableRow();
                        //trSynQuestion.Attributes.Add("Class", "header1_tr_even_row");
                        //table.Rows.Add(trSynQuestion);
                        //trSynQuestion.ID = "trSynQuestion_" + strQID;
                        //trSynQuestionTitle.Attributes.Add("onclick", "ShowSynDetail('" + trSynQuestion.ID + "' , 'imgSynQuestion_" + strQID + "')");
                        //trSynQuestion.Style.Add("display", "none");

                        TableCell tcSynQuestion = new TableCell();
                        trSynQuestionTitle.Cells.Add(tcSynQuestion);
                        BulidInterrogation("Question", strQID, tcSynQuestion, "");
                        tcSynQuestion.Attributes.Add("Class", "header1_tr_even_row");

                        DataTable dtQuestionAnswer_Answer = DataReceiver.QuestionAnswer_Answer_SELECT_AllAnswer(strQID);
                        hfAnswerCount.Value = "0";
                        hfAnswerCount.Value = dtQuestionAnswer_Answer.Rows.Count.ToString();
                        trTextQuestionTitle.Attributes.Add("onclick", "ShowDetail('" + strQID + "','img_" + strQID + "','" + hfAnswerCount.Value + "')");
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

                                TableCell tcAnswerTitle = new TableCell();
                                trAnswerTitle.Cells.Add(tcAnswerTitle);
                                Label lbAnswerTitle = new Label();
                                lbAnswerTitle.Text = "<font style='color:Black; font-weight:bold'>Answer " + iACount + " :&nbsp;<font/>";
                                //tcAnswerTitle.Controls.Add(lbAnswerTitle);
                                RadioButton rbAnswer = new RadioButton();
                                rbAnswer.ID = "rbAnswer|" + strQID + "|" + strAID;
                                rbAnswer.GroupName = "rbAnswerGroup_" + strQID;
                                rbAnswer.Text = "<font style='color:Black; font-weight:bold'> Modify this Answer " + iACount + "  :&nbsp;<font/>";
                                tcAnswerTitle.Controls.Add(rbAnswer);
                                if (iACount == 1)
                                    rbAnswer.Checked = true;
                                tcAnswerTitle.Style.Add("text-align", "right");
                                tcAnswerTitle.Width = Unit.Pixel(230);

                                //建立Answer的內容
                                //TableRow trAnswer = new TableRow();
                                //trAnswer.Attributes.Add("Class", "header1_tr_even_row");
                                //table.Rows.Add(trAnswer);
                                //trAnswer.ID = "trAnswer_" + strQID + "_" + iACount;

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
                                trSynAnswerTitle.Style.Add("CURSOR", "hand");

                                //建立同義答案的內容
                                //TableRow trSynAnswer = new TableRow();
                                //trSynAnswer.Attributes.Add("Class", "header1_tr_even_row");
                                //table.Rows.Add(trSynAnswer);
                                //trSynAnswer.ID = "trSynAnswer_" + strQID + "_" + iACount;
                                //trSynAnswerTitle.Attributes.Add("onclick", "ShowSynDetail('" + trSynAnswer.ID + "' , 'imgSynAnswer_" + strQID + "_" + iACount + "')");
                                //trSynAnswer.Style.Add("display", "none");

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
                                    //trSynAnswer.Style.Add("display", "none");
                                }
                                #endregion
                            }
                        }

                        //建立Keyword的標題
                        TableRow trKeywordTitle = new TableRow();
                        trKeywordTitle.Attributes.Add("Class", "header1_tr_odd_row");
                        table.Rows.Add(trKeywordTitle);
                        trKeywordTitle.ID = "trKeywordTitle_" + strQID;

                        TableCell tcKeywordTitle = new TableCell();
                        trKeywordTitle.Cells.Add(tcKeywordTitle);
                        tcKeywordTitle.Text = "<font style='color:Black; font-weight:bold'>Keyword :&nbsp; <font/>";
                        tcKeywordTitle.Style.Add("text-align", "right");
                        tcKeywordTitle.Width = Unit.Pixel(230);

                        //建立Keyword的內容
                        //TableRow trKeyword = new TableRow();
                        //trKeyword.Attributes.Add("Class", "header1_tr_even_row");
                        //table.Rows.Add(trKeyword);
                        //trKeyword.ID = "trKeyword_" + strQID;

                        TableCell tcKeyword = new TableCell();
                        trKeywordTitle.Cells.Add(tcKeyword);
                        tcKeyword.Text = strGetKeyword;
                        tcKeyword.Attributes.Add("Class", "header1_tr_even_row");

                        //建立修改按鈕的TableRow
                        TableRow trModify = new TableRow();
                        //trModify.Attributes.Add("Class", "header1_tr_even_row");
                        table.Rows.Add(trModify);
                        trModify.ID = "trModify_" + strQID;

                        TableCell tcModify = new TableCell();
                        trModify.Cells.Add(tcModify);
                        tcModify.Attributes["align"] = "right";
                        tcModify.ColumnSpan = 2;

                        //問題的難易度
                        Label lbQuestionLevel = new Label();
                        //tcModify.Controls.Add(lbQuestionLevel);
                        int iQuestionLevel = AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevelValue(strQID);
                        if (iQuestionLevel != -1)
                            lbQuestionLevel.Text = "Question Level：" + AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevelName_SELECT_LevelName(iQuestionLevel) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                        //問題的病徵
                        Label lbQuestionSymptoms = new Label();
                        //tcModify.Controls.Add(lbQuestionSymptoms);
                        lbQuestionSymptoms.Text = "Question Symptoms：" + AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevel_SELECT_QuestionSymptoms(strQID) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                        //建立編輯題組的Button
                        Button btnEditGroupQuestionText = new Button();
                        //tcModify.Controls.Add(btnEditGroupQuestionText);
                        btnEditGroupQuestionText.ID = "btnEditGroupQuestionText-" + strQID;
                        btnEditGroupQuestionText.Text = "Edit group questions";
                        btnEditGroupQuestionText.Click += new EventHandler(btnEditGroupQuestionText_Click);
                        btnEditGroupQuestionText.Style["width"] = "180px";
                        btnEditGroupQuestionText.CssClass = "button_continue";

                        //建立間隔
                        Label lblCellQuestionGroup = new Label();
                        tcModify.Controls.Add(lblCellQuestionGroup);
                        lblCellQuestionGroup.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                        //建立新增答案的Button
                        Button btAddNewAnswer = new Button();
                        tcModify.Controls.Add(btAddNewAnswer);
                        btAddNewAnswer.Style["width"] = "160px";
                        btAddNewAnswer.ID = "btAddNewAnswer-" + strQID;
                        btAddNewAnswer.Text = "Add a new answer";
                        btAddNewAnswer.Click += new EventHandler(btAddNewAnswer_Click);
                        btAddNewAnswer.CssClass = "button_continue";

                        //建立間隔
                        Label lblCell1 = new Label();
                        tcModify.Controls.Add(lblCell1);
                        lblCell1.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                        //建立修改問題的Button
                        Button btnModifyText = new Button();
                        tcModify.Controls.Add(btnModifyText);
                        btnModifyText.Style["width"] = "150px";
                        btnModifyText.ID = "btnModifyText-" + strQID;
                        btnModifyText.Text = "Modify";
                        btnModifyText.Click += new EventHandler(btnModifyText_Click);
                        btnModifyText.CssClass = "button_continue";

                        //建立間隔
                        Label lblCell = new Label();
                        tcModify.Controls.Add(lblCell);
                        lblCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                        //建立刪除問答題的Button
                        Button btnDeleteText = new Button();
                        tcModify.Controls.Add(btnDeleteText);
                        btnDeleteText.Style["width"] = "150px";
                        btnDeleteText.ID = "btnDeleteText-" + strQID;
                        btnDeleteText.Text = "Delete";
                        btnDeleteText.Click += new EventHandler(btnDeleteText_Click);
                        btnDeleteText.CssClass = "button_continue";


                        //建立Space
                        TableRow trSpace = new TableRow();
                        trSpace.Style.Add("background-color", "#EBECED");
                        table.Rows.Add(trSpace);

                        TableCell tcSpace = new TableCell();
                        tcSpace.Style.Add("height", "7px");
                        trSpace.Cells.Add(tcSpace);
                        tcSpace.ColumnSpan = 2;

                        //if (intQuestionIndex % 2 == 0)
                        //{
                        //    trQuestion.Attributes.Add("Class", "header1_tr_odd_row");
                        //    trModify.Attributes.Add("Class", "header1_tr_odd_row");
                        //}
                        //else
                        //{
                        //    trQuestion.Attributes.Add("Class", "header1_tr_even_row");
                        //    trModify.Attributes.Add("Class", "header1_tr_even_row");
                        //}

                        #region 管控同義項的顯示與隱藏
                        DataTable dtSynQuestion = new DataTable();
                        dtSynQuestion = clsInterrogationEnquiry.GetSynQuestion(strQID);
                        if (dtSynQuestion.Rows.Count == 0)
                        {
                            trSynQuestionTitle.Style.Add("display", "none");
                            //trSynQuestion.Style.Add("display", "none");
                        }
                        #endregion

                        #endregion
                    }
                }
            }
            else
            {
                //此問卷沒有任何問答題的情況
                trTextQuestionTable.Style["display"] = "none";
            }
            dsQuestionList.Dispose();
        }

        private void btnModifyText_Click(object sender, EventArgs e)
        {
            //修改一個問答題

            //取得QID
            string[] strIDArray = ((Button)(sender)).ID.Split('-');
            string strQID = strIDArray[1];

            //設定QID的Session
            if (Session["QID"] != null)
            {
                Session["QID"] = strQID;
            }
            else
            {
                Session.Add("QID", strQID);
            }

            RegisterStartupScript("", "<script language='javascript'>setAnswerID('" + strQID + "');</script>");

            hfQuestionID.Value = strQID;

            string[] strAID = null;
            for (int i = 0; i < Request.Form.Count; i++)
            {
                if (Request.Form.Keys[i].ToString().IndexOf("rbAnswerGroup_" + strQID) != -1)
                {
                    strAID = Request.Form[i].ToString().Split('|');
                    break;
                }
            }

            //呼叫Paper_TextQuestionEditorNew.aspx
            Response.Redirect("Paper_TextQuestionEditorNew.aspx?QID=" + strQID + "&AID=" + strAID[2] + "&GroupID=" + Session["GroupID"].ToString() + "&bModify=True");
        }

        private void btnDeleteText_Click(object sender, EventArgs e)
        {
            //刪除一個問答題

            //取得QID
            string[] strIDArray = ((Button)(sender)).ID.Split('-');
            string strQID = strIDArray[1];

            //設定QID的Session
            if (Session["QID"] != null)
            {
                Session["QID"] = strQID;
            }
            else
            {
                Session.Add("QID", strQID);
            }

            //刪除該問答題
            SQLString.QuestionAnswer_DELETE_ByQID(strQID);

            //刪除該問答題的同義問題與答案
            clsInterrogationEnquiry.DeleteQuestionItem(strQID);

            //清除
            tcQuestionTable.Controls.Clear();
            tcTextQuestionTable.Controls.Clear();

            intQuestionIndex = 0;

            //建立選擇題
            this.setupQuestionTable();

            //建立問答題
            this.setupTextQuestionTable();

            //建立對話題
            this.setupConversationQuestionTable();
        }

        void btAddNewAnswer_Click(object sender, EventArgs e)
        {
            //取得QID
            string[] strIDArray = ((Button)(sender)).ID.Split('-');
            string strQID = strIDArray[1];
            DataReceiver myReceiver = new DataReceiver();
            string strAID = usi.UserID + "_A_" + myReceiver.getNowTime();

            //設定QID的Session
            if (Session["QID"] != null)
            {
                Session["QID"] = strQID;
            }
            else
            {
                Session.Add("QID", strQID);
            }

            //呼叫Paper_TextQuestionEditorNew.aspx
            Response.Redirect("Paper_TextQuestionEditorNew.aspx?QID=" + strQID + "&AID=" + strAID + "&GroupID=" + Session["GroupID"].ToString());
        }
        //建立選擇題表格
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
            string strSQL = mySQL.getGroupSelectionQuestion(Session["GroupID"].ToString());


            DataSet dsQuestionList = sqldb.getDataSet(strSQL);
            if (dsQuestionList.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsQuestionList.Tables[0].Rows.Count; i++)
                {
                    //取得QuestionType
                    string strQuestionType = "1";
                    strQuestionType = dsQuestionList.Tables[0].Rows[i]["cQuestionType"].ToString();

                    //取得QID
                    string strQID = "";
                    strQID = dsQuestionList.Tables[0].Rows[i]["cQID"].ToString();

                    //取得問題的SQL
                    DataSet dsQuestion = null;
                    if (hfSymptoms.Value == "All")
                        strSQL = mySQL.getQuestion(strQID);
                    else
                        strSQL = mySQL.getQuestionBySymptoms(strQID, hfSymptoms.Value);

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

                        //問題的內容
                        string strQuestion = "";
                        strQuestion = dsQuestion.Tables[0].Rows[0]["cQuestion"].ToString();

                        TableCell tcQuestion = new TableCell();
                        trQuestion.Cells.Add(tcQuestion);
                        tcQuestion.Text = strQuestion;


                        //建立問題的CSS
                        trQuestion.Attributes.Add("Class", "header1_table_first_row");

                        //建立選項
                        strSQL = mySQL.getAllSelections(strQID);
                        DataSet dsSelection = sqldb.getDataSet(strSQL);
                        trQuestion.Attributes.Add("onclick", "ShowSelectionQuestionDetail('" + strQID + "','img_" + strQID + "','" + dsSelection.Tables[0].Rows.Count + "')");
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

                                //是否為建議選項
                                TableCell tcSuggest = new TableCell();
                                trSelection.Cells.Add(tcSuggest);
                                tcSuggest.Width = Unit.Pixel(25);
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
                                //								TableCell tcSelectionNum = new TableCell();
                                //								trSelection.Cells.Add(tcSelectionNum);
                                //								tcSelectionNum.Text = Convert.ToString((j+1)) + ".";

                                //選項內容
                                TableCell tcSelection = new TableCell();
                                trSelection.Cells.Add(tcSelection);
                                tcSelection.Text = strSelection;

                                //Empty TableCell
                                //								TableCell tcEmpty = new TableCell();
                                //								trSelection.Cells.Add(tcEmpty);

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

                        //Modify and Delete 按鈕的TableRow
                        TableRow trModify = new TableRow();
                        table.Rows.Add(trModify);
                        trModify.ID = "trModify_" + strQID;

                        TableCell tcNone = new TableCell();
                        trModify.Cells.Add(tcNone);

                        //建立修改問題的Button
                        TableCell tcModify = new TableCell();
                        trModify.Cells.Add(tcModify);
                        tcModify.Attributes["align"] = "right";
                        //tcModify.ColumnSpan = 2;

                        //問題的分數
                        Label lbQuestionGrade = new Label();
                        //tcModify.Controls.Add(lbQuestionGrade);
                        string strQuestionGrade = AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevel_SELECT_Grade(strQID);
                        if (strQuestionGrade != "-1")
                            lbQuestionGrade.Text = "Question Grade：" + strQuestionGrade + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                        //問題的難易度
                        Label lbQuestionLevel = new Label();
                        //tcModify.Controls.Add(lbQuestionLevel);
                        int iQuestionLevel = AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevelValue(strQID);
                        if (iQuestionLevel != -1)
                            lbQuestionLevel.Text = "Question Level：" + AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevelName_SELECT_LevelName(iQuestionLevel) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                        //建立編輯題組的Button
                        Button btnEditGroupQuestionSelection = new Button();
                        tcModify.Controls.Add(btnEditGroupQuestionSelection);
                        btnEditGroupQuestionSelection.ID = "btnEditGroupQuestionSelection-" + strQID;
                        btnEditGroupQuestionSelection.Text = "Edit group questions";
                        btnEditGroupQuestionSelection.Click += new EventHandler(btnEditGroupQuestionSelection_Click);
                        btnEditGroupQuestionSelection.Style["width"] = "180px";
                        btnEditGroupQuestionSelection.CssClass = "button_continue";

                        //建立間隔
                        Label lblCellQuestionGroup = new Label();
                        tcModify.Controls.Add(lblCellQuestionGroup);
                        lblCellQuestionGroup.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                        Button btnModifySelection = new Button();
                        tcModify.Controls.Add(btnModifySelection);
                        btnModifySelection.ID = "btnModifySelection-" + strQID;
                        btnModifySelection.Text = "Modify";
                        btnModifySelection.Click += new EventHandler(btnModifySelection_Click);
                        btnModifySelection.Style["width"] = "150px";
                        btnModifySelection.CssClass = "button_continue";

                        //建立間隔
                        Label lblCell = new Label();
                        tcModify.Controls.Add(lblCell);
                        lblCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                        Button btnDeleteSelection = new Button();
                        tcModify.Controls.Add(btnDeleteSelection);
                        btnDeleteSelection.ID = "btnDeleteSelection-" + strQID;
                        btnDeleteSelection.Text = "Delete";
                        btnDeleteSelection.Click += new EventHandler(btnDeleteSelection_Click);
                        btnDeleteSelection.Style["width"] = "150px";
                        btnDeleteSelection.CssClass = "button_continue";


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
            else
            {
                //此問卷沒有任何選擇題的情況
                trQuestionTable.Style["display"] = "none";
            }
            dsQuestionList.Dispose();
        }


        //建立選擇題表格
        private void setupQuestionWithKeyWordsTable()
        {
            tcQuestionWithReasonsTable.Controls.Clear();
            Table table = new Table();
            tcQuestionWithReasonsTable.Controls.Add(table);
            table.CellSpacing = 0;
            table.CellPadding = 2;
            table.BorderStyle = BorderStyle.Solid;
            table.BorderWidth = Unit.Pixel(1);
            table.BorderColor = System.Drawing.Color.Black;
            table.GridLines = GridLines.Both;
            table.Width = Unit.Percentage(100);

            //建立Table的CSS
            table.CssClass = "header1_table";

            //依照QuestionMode決定取出此組別的選擇題包含關鍵字
            string strSQL = mySQL.getGroupSelectionWithKeyWordsQuestion(Session["GroupID"].ToString());


            DataSet dsQuestionList = sqldb.getDataSet(strSQL);
            if (dsQuestionList.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsQuestionList.Tables[0].Rows.Count; i++)
                {
                    //取得QuestionType
                    string strQuestionType = "1";
                    strQuestionType = dsQuestionList.Tables[0].Rows[i]["cQuestionType"].ToString();

                    //取得QID
                    string strQID = "";
                    strQID = dsQuestionList.Tables[0].Rows[i]["cQID"].ToString();

                    //取得問題的SQL
                    DataSet dsQuestion = null;
                    if (hfSymptoms.Value == "All")
                        strSQL = mySQL.getQuestion(strQID);
                    else
                        strSQL = mySQL.getQuestionBySymptoms(strQID, hfSymptoms.Value);

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

                        //問題的內容
                        string strQuestion = "";
                        strQuestion = dsQuestion.Tables[0].Rows[0]["cQuestion"].ToString();

                        TableCell tcQuestion = new TableCell();
                        trQuestion.Cells.Add(tcQuestion);
                        tcQuestion.Text = strQuestion;
                        tcQuestion.Width = Unit.Percentage(62);

                        TableCell tcKeyWordsTitle = new TableCell();
                        trQuestion.Cells.Add(tcKeyWordsTitle);
                        tcKeyWordsTitle.Text = "KeyWords";
                        tcKeyWordsTitle.Attributes["align"] = "center";
                        tcKeyWordsTitle.Width = Unit.Pixel(300);

                        //建立問題的CSS
                        trQuestion.Attributes.Add("Class", "header1_table_first_row");

                        //建立選項
                        strSQL = mySQL.getAllSelections(strQID);
                        DataSet dsSelection = sqldb.getDataSet(strSQL);
                        trQuestion.Attributes.Add("onclick", "ShowSelectionQuestionDetail('" + strQID + "','img_" + strQID + "','" + dsSelection.Tables[0].Rows.Count + "')");
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

                                //是否為建議選項
                                TableCell tcSuggest = new TableCell();
                                trSelection.Cells.Add(tcSuggest);
                                tcSuggest.Width = Unit.Pixel(25);
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
                                //								TableCell tcSelectionNum = new TableCell();
                                //								trSelection.Cells.Add(tcSelectionNum);
                                //								tcSelectionNum.Text = Convert.ToString((j+1)) + ".";

                                //選項內容
                                TableCell tcSelection = new TableCell();
                                trSelection.Cells.Add(tcSelection);
                                tcSelection.Text = strSelection;

                                //Empty TableCell
                                //								TableCell tcEmpty = new TableCell();
                                //								trSelection.Cells.Add(tcEmpty);

                                //建立選項的CSS
                                if ((Convert.ToInt32(strSeq) % 2) != 0)
                                {
                                    trSelection.Attributes.Add("Class", "header1_tr_odd_row");
                                }
                                else
                                {
                                    trSelection.Attributes.Add("Class", "header1_tr_even_row");
                                }

                                //加入KeyWords欄位
                                if (j == 0)
                                {
                                    //取得題目關鍵字並顯示於表格中
                                    string strKeyWords = "";
                                    strKeyWords = dsQuestionList.Tables[0].Rows[i]["cKeyWords"].ToString();
                                    TableCell tcKeyWords = new TableCell();
                                    trSelection.Cells.Add(tcKeyWords);
                                    tcKeyWords.RowSpan= dsSelection.Tables[0].Rows.Count;
                                    tcKeyWords.Text = strKeyWords;
                                    tcKeyWords.Width = Unit.Pixel(300);
                                }
                            }
                        }
                        else
                        {
                            //此問題沒有選項
                        }
                        dsSelection.Dispose();

                        //Modify and Delete 按鈕的TableRow
                        TableRow trModify = new TableRow();
                        table.Rows.Add(trModify);
                        trModify.ID = "trModify_" + strQID;

                        TableCell tcNone = new TableCell();
                        trModify.Cells.Add(tcNone);

                        //建立修改問題的Button
                        TableCell tcModify = new TableCell();
                        trModify.Cells.Add(tcModify);
                        tcModify.Attributes["align"] = "right";
                        tcModify.Attributes["colspan"] = "2";
                        //tcModify.ColumnSpan = 2;

                        //問題的分數
                        Label lbQuestionGrade = new Label();
                        //tcModify.Controls.Add(lbQuestionGrade);
                        string strQuestionGrade = AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevel_SELECT_Grade(strQID);
                        if (strQuestionGrade != "-1")
                            lbQuestionGrade.Text = "Question Grade：" + strQuestionGrade + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                        //問題的難易度
                        Label lbQuestionLevel = new Label();
                        //tcModify.Controls.Add(lbQuestionLevel);
                        int iQuestionLevel = AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevelValue(strQID);
                        if (iQuestionLevel != -1)
                            lbQuestionLevel.Text = "Question Level：" + AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevelName_SELECT_LevelName(iQuestionLevel) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                        //建立編輯題組的Button
                        Button btnEditGroupQuestionSelection = new Button();
                        tcModify.Controls.Add(btnEditGroupQuestionSelection);
                        btnEditGroupQuestionSelection.ID = "btnEditGroupQuestionSelection-" + strQID;
                        btnEditGroupQuestionSelection.Text = "Edit group questions";
                        btnEditGroupQuestionSelection.Click += new EventHandler(btnEditGroupQuestionSelection_Click);
                        btnEditGroupQuestionSelection.Style["width"] = "180px";
                        btnEditGroupQuestionSelection.CssClass = "button_continue";

                        //建立間隔
                        Label lblCellQuestionGroup = new Label();
                        tcModify.Controls.Add(lblCellQuestionGroup);
                        lblCellQuestionGroup.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                        Button btnModifySelection = new Button();
                        tcModify.Controls.Add(btnModifySelection);
                        btnModifySelection.ID = "btnModifySelection-" + strQID;
                        btnModifySelection.Text = "Modify";
                        btnModifySelection.Click += new EventHandler(btnModifySelectionWithReasons_Click);
                        btnModifySelection.Style["width"] = "150px";
                        btnModifySelection.CssClass = "button_continue";

                        //建立間隔
                        Label lblCell = new Label();
                        tcModify.Controls.Add(lblCell);
                        lblCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                        Button btnDeleteSelection = new Button();
                        tcModify.Controls.Add(btnDeleteSelection);
                        btnDeleteSelection.ID = "btnDeleteSelection-" + strQID;
                        btnDeleteSelection.Text = "Delete";
                        btnDeleteSelection.Click += new EventHandler(btnDeleteSelection_Click);
                        btnDeleteSelection.Style["width"] = "150px";
                        btnDeleteSelection.CssClass = "button_continue";


                        //建立Space
                        TableRow trSpace = new TableRow();
                        trSpace.Style.Add("background-color", "#EBECED");
                        table.Rows.Add(trSpace);

                        TableCell tcSpace = new TableCell();
                        tcSpace.ColumnSpan = 3;
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
            else
            {
                //此問卷沒有任何選擇題的情況
                trQuestionWithReasonsTable.Style["display"] = "none";
            }
            dsQuestionList.Dispose();
        }

        private void getParameter()
        {
            if (Session["PreOpener"] != null)
            {
                strPreOpener = Session["PreOpener"].ToString();
            }

            //GroupID
            string strGroupID = "";

            if (Session["GroupID"] != null)
                strGroupID = Session["GroupID"].ToString();

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

            iQuestionClassifyID = Convert.ToInt32(DataReceiver.getQuestionGroupSerialNumByQuestionGroupID(strGroupID).ToString());

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

            //GroupName
            strGroupName = DataReceiver.getQuestionGroupNameByQuestionGroupID(strGroupID);
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

        /// <summary>
        /// 將被勾選的問題自Question group中刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteQuestion_ServerClick(object sender, ImageClickEventArgs e)
        {
            //取得此組別的選擇題(DataSet)
            string strSQL = mySQL.getGroupSelectionQuestion(Session["GroupID"].ToString());
            DataSet dsQuestionList = sqldb.getDataSet(strSQL);
            if (dsQuestionList.Tables[0].Rows.Count > 0)
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
                        //如果有被勾選，則將資料自QuestionIndex , QuestionSelectionIndex , QuestionMode 刪除
                        mySQL.DeleteGeneralQuestion(strQID);
                    }
                }
            }
            else
            {
                //此組別沒有題目的情形
            }
            dsQuestionList.Dispose();

            tcQuestionTable.Controls.Clear();

            intQuestionIndex = 0;

            //建立Main table
            this.setupQuestionTable();
        }

        private void btnModifySelection_Click(object sender, EventArgs e)
        {
            //修改一個選擇題

            //取出ID
            //string strQID = ((Button)(sender)).ID;
            string[] strIDArray = ((Button)(sender)).ID.Split('-');
            string strQID = strIDArray[1];

            //設定QID的Session
            if (Session["QID"] != null)
            {
                Session["QID"] = strQID;
            }
            else
            {
                Session.Add("QID", strQID);
            }

            string strScript = "<script language='javascript'>\n";
            strScript += "alert('Save successfully!');";
            strScript += "</script>\n";
            Page.RegisterStartupScript("ShowErrorMsg", strScript);

            //呼叫Common question editor
            Response.Redirect("./CommonQuestionEdit/Page/ShowQuestion.aspx?QID=" + strQID + "&GroupID=" + Session["GroupID"].ToString() + "&bModify=True");
        }


        private void btnModifySelectionWithReasons_Click(object sender, EventArgs e)
        {
            //修改一個選擇題

            //取出ID
            //string strQID = ((Button)(sender)).ID;
            string[] strIDArray = ((Button)(sender)).ID.Split('-');
            string strQID = strIDArray[1];

            //設定QID的Session
            if (Session["QID"] != null)
            {
                Session["QID"] = strQID;
            }
            else
            {
                Session.Add("QID", strQID);
            }

            string strScript = "<script language='javascript'>\n";
            strScript += "alert('Save successfully!');";
            strScript += "</script>\n";
            Page.RegisterStartupScript("ShowErrorMsg", strScript);

            //呼叫Common question editor
            Response.Redirect("./CommonQuestionEdit/Page/showquestionWithKeyWords.aspx?QID=" + strQID + "&GroupID=" + Session["GroupID"].ToString() + "&bModify=True");
        }

        private void btnDeleteSelection_Click(object sender, EventArgs e)
        {
            //取出ID
            string[] strIDArray = ((Button)(sender)).ID.Split('-');
            string strQID = strIDArray[1];

            //刪除該選擇題
            mySQL.DeleteGeneralQuestion(strQID);

            intQuestionIndex = 0;

            //建立Main table
            tcQuestionTable.Controls.Clear();
            this.setupQuestionTable();
        }

        private void btnDeleteConversation_Click(object sender, EventArgs e)
        {
            //刪除一個對話題

            //取得QID
            string[] strIDArray = ((Button)(sender)).ID.Split('-');
            string strQID = strIDArray[1];

            //設定QID的Session
            if (Session["QID"] != null)
            {
                Session["QID"] = strQID;
            }
            else
            {
                Session.Add("QID", strQID);
            }

            //刪除該問答題
            SQLString.Conversation_DELETE_ByQID(strQID);

            //刪除該問答題的同義問題與答案
            //clsInterrogationEnquiry.DeleteQuestionItem(strQID);

            //清除
            tcQuestionTable.Controls.Clear();
            tcTextQuestionTable.Controls.Clear();
            tcConversationQuestionTable.Controls.Clear();

            intQuestionIndex = 0;

            //建立選擇題
            this.setupQuestionTable();

            //建立問答題
            this.setupTextQuestionTable();

            //建立對話題
            this.setupConversationQuestionTable();
        }

        private void btnModifyConversation_Click(object sender, EventArgs e)
        {
            //修改一個對話題

            //取得QID
            string[] strIDArray = ((Button)(sender)).ID.Split('-');
            string strQID = strIDArray[1];

            //設定QID的Session
            if (Session["QID"] != null)
            {
                Session["QID"] = strQID;
            }
            else
            {
                Session.Add("QID", strQID);
            }

            RegisterStartupScript("", "<script language='javascript'>setAnswerTypeID('" + strQID + "');</script>");


            string[] strAnswerTypeNum = null;
            for (int i = 0; i < Request.Form.Count; i++)
            {
                if (Request.Form.Keys[i].ToString().IndexOf("rbAnswerTypeGroup_" + strQID) != -1)
                {
                    strAnswerTypeNum = Request.Form[i].ToString().Split('|');
                    break;
                }
            }

            //呼叫Paper_ConversationQuestionEditor.aspx
            Response.Redirect("Paper_ConversationQuestionEditor.aspx?QID=" + strQID + "&AnswerType=" + strAnswerTypeNum[2] + "&GroupID=" + Session["GroupID"].ToString() + "&bModify=True");
        }

        void btnEditGroupQuestionSelection_Click(object sender, EventArgs e)
        {
            //編輯選擇題題組

            //取出ID
            string[] strIDArray = ((Button)(sender)).ID.Split('-');
            string strQID = strIDArray[1];

            //設定QID的Session
            if (Session["QID"] != null)
            {
                Session["QID"] = strQID;
            }
            else
            {
                Session.Add("QID", strQID);
            }

            Response.Redirect("Paper_QuestionGroupView.aspx?QID=" + strQID + "&GroupID=" + Session["GroupID"].ToString() + "");

        }
        void btnEditGroupQuestionSimulation_Click(object sender, EventArgs e)
        {
            //編輯圖形題題組

            //取出ID
            string[] strIDArray = ((Button)(sender)).ID.Split('-');
            string strQID = strIDArray[1];

            //設定QID的Session
            if (Session["QID"] != null)
            {
                Session["QID"] = strQID;
            }
            else
            {
                Session.Add("QID", strQID);
            }

            Response.Redirect("Paper_QuestionGroupView.aspx?QID=" + strQID + "&GroupID=" + Session["GroupID"].ToString() + "");

        }

        void btnEditGroupQuestionText_Click(object sender, EventArgs e)
        {
            //編輯問答題題組

            //取出ID
            string[] strIDArray = ((Button)(sender)).ID.Split('-');
            string strQID = strIDArray[1];

            //設定QID的Session
            if (Session["QID"] != null)
            {
                Session["QID"] = strQID;
            }
            else
            {
                Session.Add("QID", strQID);
            }

            Response.Redirect("Paper_QuestionGroupView.aspx?QID=" + strQID + "&GroupID=" + Session["GroupID"].ToString() + "");
        }

        void btModify_ServerClick(object sender, EventArgs e)
        {
            string strQID = "";
            string strAID = "";
            strQID = hfQuestionID.Value;
            strAID = hfAnswerID.Value;

            //呼叫Paper_TextQuestionEditor.aspx
            Response.Redirect("Paper_TextQuestionEditorNew.aspx?QID=" + strQID + "&AID=" + strAID + "&GroupID=" + Session["GroupID"].ToString());
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

        //建立答案型態的答案內容
        private void BulidAnswerTypeContent(string strType, string strQID, TableCell tcAnswerTypeContent, int iAnswerType)
        {
            DataTable dtConversation = new DataTable();
            if (strType == "Answer")
            {
                string strAnswerTypeName = clsConversation.Conversation_AnswerType_SELECT_AssignedAnswerTypeName(iQuestionClassifyID, iAnswerType);
                dtConversation = clsConversation.Conversation_Answer_SELECT_Answer(strQID, strAnswerTypeName);
            }

            if (dtConversation.Rows.Count > 0)
            {
                #region 答案型態的答案內容
                for (int iAnswerTypeContentCount = 0; iAnswerTypeContentCount < dtConversation.Rows.Count; iAnswerTypeContentCount++)
                {
                    #region 項目的序號
                    Label lbAnswerItemT = new Label();
                    if (iAnswerTypeContentCount == 0)
                    {
                        lbAnswerItemT.Text = "Ans. " + (iAnswerTypeContentCount + 1) + ": ";
                    }
                    else
                    {
                        lbAnswerItemT.Text = "<br/>Ans. " + (iAnswerTypeContentCount + 1) + ": ";
                    }
                    tcAnswerTypeContent.Controls.Add(lbAnswerItemT);
                    #endregion

                    #region 答案項目的內容
                    Label lbAnswerItemValue = new Label();
                    lbAnswerItemValue.Text = "( " + dtConversation.Rows[iAnswerTypeContentCount]["cAnswerContentType"].ToString() + " ) " + dtConversation.Rows[iAnswerTypeContentCount]["cAnswer"].ToString();

                    tcAnswerTypeContent.Controls.Add(lbAnswerItemValue);
                    #endregion
                }

                #endregion
            }


        }

        protected void ddlSymptoms_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfSymptoms.Value = ddlSymptoms.SelectedValue;

            intQuestionIndex = 0;

            //建立選擇題表格
            this.setupQuestionTable();

            //建立問答題表格
            this.setupTextQuestionTable();

        }

        //取得病徵項目
        private DataTable DiseaseSymptomsTree_SELECT()
        {
            clsHintsDB HintsDB = new clsHintsDB();
            DataTable dtDiseaseSymptomsTree = new DataTable();
            string strSQL_DiseaseSymptomsTree = "SELECT  DISTINCT cNodeName FROM DiseaseSymptomsTree WHERE cParentID != 'Diseaseroot' ORDER BY cNodeName ASC";
            dtDiseaseSymptomsTree = HintsDB.getDataSet(strSQL_DiseaseSymptomsTree).Tables[0];
            return dtDiseaseSymptomsTree;
        }

        //判斷是否具有同義答案
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public bool CheckSynAnswer(string strQID, int iSeq)
        {
            bool bCheck = false;
            DataTable dtQuestionAnswer_Answer = DataReceiver.QuestionAnswer_Answer_SELECT_AllAnswer(strQID);
            string strAID = dtQuestionAnswer_Answer.Rows[iSeq - 1]["cAID"].ToString();
            DataTable dtItemForAskAnswer = clsInterrogationEnquiry.GetSynAnswer(strQID, strAID);
            if (dtItemForAskAnswer.Rows.Count > 0)
            {
                bCheck = true;
            }
            return bCheck;
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
        protected void btAddQuestion_Click(object sender, EventArgs e)
        {
            strGroupID = Session["GroupID"].ToString();
            if (Request.QueryString["CaseID"] != null)
            Response.Redirect("Paper_QuestionTypeNew.aspx?Opener=Paper_QuestionViewNew&GroupID=" + strGroupID + "&bModify=False&CaseID=" + Request.QueryString["CaseID"].ToString());
            else
                Response.Redirect("Paper_QuestionTypeNew.aspx?Opener=Paper_QuestionViewNew&GroupID=" + strGroupID + "&bModify=False");
        }
        protected void btEditAnswerType_Click(object sender, EventArgs e)
        {
            strGroupID = Session["GroupID"].ToString();
            Response.Redirect("Paper_AnswerTypeEdit.aspx?GroupID=" + strGroupID + "");
        }

        //建立圖形題表格
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
                    if (hfSymptoms.Value == "All")
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
                            trScene.ID = "trimg_" + strQID;
                            trScene.Height = 290;
                            table.Rows.Add(trScene);

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
                                str_URL = "http://140.116.177.54/HintsCase/FileCollection/0101/201108/File20110817120244.JPG";
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
                            //縮放
                            trQuestion.Attributes.Add("onclick", "ShowSimuQuestionDetail('" + strQID + "','img_" + strQID + "','" + hfAnswerCount.Value + "')");
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
                                    trAnswerOrder.ID = "trAnsOrder_" + strQID + "_" + s_no;
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
                        //Modify and Delete 按鈕的TableRow
                        TableRow trModify = new TableRow();
                        table.Rows.Add(trModify);
                        trModify.ID = "trModify_" + strQID;

                        TableCell tcNone = new TableCell();
                        trModify.Cells.Add(tcNone);

                        //建立修改問題的Button
                        TableCell tcModify = new TableCell();
                        trModify.Cells.Add(tcModify);
                        tcModify.Attributes["align"] = "right";

                        //建立編輯題組的Button
                        Button btnEditGroupQuestionSelection = new Button();
                        tcModify.Controls.Add(btnEditGroupQuestionSelection);
                        btnEditGroupQuestionSelection.ID = "btnEditGroupQuestionSimulator-" + strQID;
                        btnEditGroupQuestionSelection.Text = "Edit group questions";
                        btnEditGroupQuestionSelection.Click += new EventHandler(btnEditGroupQuestionSimulation_Click);
                        btnEditGroupQuestionSelection.Style["width"] = "180px";
                        btnEditGroupQuestionSelection.CssClass = "button_continue";

                        //建立間隔
                        Label lblCellQuestionGroup = new Label();
                        tcModify.Controls.Add(lblCellQuestionGroup);
                        lblCellQuestionGroup.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                        Button btnModifySim = new Button();
                        tcModify.Controls.Add(btnModifySim);
                        btnModifySim.ID = "btnModifySim-" + strQID;
                        btnModifySim.Text = "Modify";
                        btnModifySim.Click += new EventHandler(btnModifySim_Click);
                        btnModifySim.CommandArgument = strQID;
                        btnModifySim.Style["width"] = "150px";
                        btnModifySim.CssClass = "button_continue";

                        //建立間隔
                        Label lblCell = new Label();
                        tcModify.Controls.Add(lblCell);
                        lblCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                        Button btnDeleteSimu = new Button();
                        tcModify.Controls.Add(btnDeleteSimu);
                        btnDeleteSimu.ID = "btnDeleteSelection-" + strQID;
                        btnDeleteSimu.Text = "Delete";
                        btnDeleteSimu.CommandArgument = strQID;
                        btnDeleteSimu.Click += new EventHandler(btnDeleteSimu_Click);
                        btnDeleteSimu.Style["width"] = "150px";
                        btnDeleteSimu.CssClass = "button_continue";


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

        protected void btnModifySim_Click(object sender, EventArgs e)
        {
            string strQID = ((Button)sender).CommandArgument;
            Response.Redirect("Paper_SimulatorQE_tree.aspx?QID=" + strQID);
        }
        protected void btnDeleteSimu_Click(object sender, EventArgs e)
        {
            string strQID = ((Button)sender).CommandArgument;
            //Response.Redirect("Paper_SimulatorQE_tree.aspx?Opener=Paper_QuestionViewNew&GroupID=" + strQID);
            //刪除該題
            mySQL.DeleteGeneralQuestion(strQID);
            //刪除Q_S資料表
            mySQL.DeleteQuestion_SimulatorContent(strQID);
        }


        //建立情境題表格
        private void setupSituationTable()
        {
            tcSituationTable.Controls.Clear();
            Table table = new Table();
            tcSituationTable.Controls.Add(table);
            table.CellSpacing = 0;
            table.CellPadding = 2;
            table.BorderStyle = BorderStyle.Solid;
            table.BorderWidth = Unit.Pixel(1);
            table.BorderColor = System.Drawing.Color.Black;
            table.GridLines = GridLines.Both;
            table.Width = Unit.Percentage(100);

            //建立Table的CSS
            table.CssClass = "header1_table";

            //依照QuestionMode決定取出此組別的情境題
            string strSQL = mySQL.getGroupSituationQuestion(Session["GroupID"].ToString());


            DataSet dsQuestionList = sqldb.getDataSet(strSQL);
            if (dsQuestionList.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsQuestionList.Tables[0].Rows.Count; i++)
                {
                    //取得QuestionType
                    string strQuestionType = "5";
                    strQuestionType = dsQuestionList.Tables[0].Rows[i]["cQuestionType"].ToString();

                    //取得QID
                    string strQID = "";
                    strQID = dsQuestionList.Tables[0].Rows[i]["cQID"].ToString();

                    //取得問題的SQL
                    DataSet dsQuestion = null;
                    if (hfSymptoms.Value == "All")
                        strSQL = mySQL.getQuestion(strQID);
                    else
                        strSQL = mySQL.getQuestionBySymptoms(strQID, hfSymptoms.Value);

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
                        tcQuestionNum.Width = Unit.Pixel(250);
                        //tcQuestionNum.Text = "Q" + intQuestionIndex.ToString() + ": ";
                        tcQuestionNum.Text = "<IMG id='img_" + strQID + "' src='../../../BasicForm/Image/minus.gif'>&nbsp;Q" + intQuestionIndex.ToString() + " :  情境題 ";

                        //問題的內容
                        string strQuestion = "";
                        strQuestion = dsQuestion.Tables[0].Rows[0]["cQuestion"].ToString();

                        TableCell tcQuestion = new TableCell();
                        trQuestion.Cells.Add(tcQuestion);
                        tcQuestion.Text = strQuestion;


                        //建立問題的CSS
                        trQuestion.Attributes.Add("Class", "header1_table_first_row");

                        //建立問題縮小按鈕事件
                        //設定要縮小幾排
                        int RowCount = 1;
                        trQuestion.Attributes.Add("onclick", "ShowSelectionQuestionDetail('" + strQID + "','img_" + strQID + "','" + RowCount + "')");

                        //取得情境題的問題與細節事項
                        strSQL = mySQL.getSituationQuestionData(strQID);
                        DataSet dsSituationData = sqldb.getDataSet(strSQL);

                        //取出問題詳細資訊
                        string strInformation = dsSituationData.Tables[0].Rows[0]["strInformation"].ToString();

                        TableRow trSelection = new TableRow();
                        table.Rows.Add(trSelection);
                        trSelection.ID = "trAnswerTitle_" + strQID + "_1";

                        //列出情境題詳細資訊
                        TableCell tcSituationQuestion = new TableCell();
                        trSelection.Cells.Add(tcSituationQuestion);
                        tcSituationQuestion.Width = Unit.Pixel(250);
                        tcSituationQuestion.Text = "<font style='color:Black; font-weight:bold'>Information :&nbsp;<font/>";
                        tcSituationQuestion.Style.Add("text-align", "right");


                        //情境題詳細資訊內容
                        TableCell tcSelection = new TableCell();
                        trSelection.Cells.Add(tcSelection);
                        tcSelection.Text = strInformation;
                        trSelection.Attributes.Add("Class", "header1_tr_odd_row");

                        //Modify and Delete 按鈕的TableRow
                        TableRow trModify = new TableRow();
                        table.Rows.Add(trModify);
                        trModify.ID = "trModify_" + strQID;

                        TableCell tcNone = new TableCell();
                        trModify.Cells.Add(tcNone);

                        //建立修改問題的Button
                        TableCell tcModify = new TableCell();
                        trModify.Cells.Add(tcModify);
                        tcModify.Attributes["align"] = "right";
                        //tcModify.ColumnSpan = 2;

                        Button btnModifySelection = new Button();
                        tcModify.Controls.Add(btnModifySelection);
                        btnModifySelection.ID = "btnModifySituation-" + strQID;
                        btnModifySelection.Text = "Modify";
                        btnModifySelection.Click += new EventHandler(btnModifySituation_Click);
                        btnModifySelection.Style["width"] = "150px";
                        btnModifySelection.CssClass = "button_continue";

                        //建立間隔
                        Label lblCell = new Label();
                        tcModify.Controls.Add(lblCell);
                        lblCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                        Button btnDeleteSelection = new Button();
                        tcModify.Controls.Add(btnDeleteSelection);
                        btnDeleteSelection.ID = "btnDeleteSituation-" + strQID;
                        btnDeleteSelection.Text = "Delete";
                        btnDeleteSelection.Click += new EventHandler(btnDeleteSituation_Click);
                        btnDeleteSelection.Style["width"] = "150px";
                        btnDeleteSelection.CssClass = "button_continue";


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
            else
            {
                strGroupID = Session["GroupID"].ToString();
                //Ben check
                //Response.Redirect("Paper_QuestionTypeNew.aspx?Opener=Paper_QuestionViewNew&GroupID=" + strGroupID + "&bModify=False&CaseID=" + Request.QueryString["CaseID"].ToString());
                //此問卷沒有任何選擇題的情況
                trSituationTable.Style["display"] = "none";
            }
            dsQuestionList.Dispose();
        }

        private void btnModifySituation_Click(object sender, EventArgs e)
        {
            //修改一個情境題

            //取出ID
            //string strQID = ((Button)(sender)).ID;
            string[] strIDArray = ((Button)(sender)).ID.Split('-');
            string strQID = strIDArray[1];

            //設定QID的Session
            if (Session["QID"] != null)
            {
                Session["QID"] = strQID;
            }
            else
            {
                Session.Add("QID", strQID);
            }

            string strScript = "<script language='javascript'>\n";
            strScript += "alert('Save successfully!');";
            strScript += "</script>\n";
            Page.RegisterStartupScript("ShowErrorMsg", strScript);

            //呼叫Common question editor
            Response.Redirect("Paper_EmulationQuestion.aspx?QID=" + strQID + "&GroupID=" + Session["GroupID"].ToString() + "&bModify=True");
        }
        private void btnDeleteSituation_Click(object sender, EventArgs e)
        {
            //取出ID
            string[] strIDArray = ((Button)(sender)).ID.Split('-');
            string strQID = strIDArray[1];

            //刪除該情境題
            mySQL.DeleteSituationQuestion(strQID);

            intQuestionIndex = 0;

            //建立Main table
            tcSituationTable.Controls.Clear();
            this.setupSituationTable();
        }
    }
}