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

namespace PaperSystem
{
    /// <summary>
    /// Paper_QuestionView 的摘要描述。
    /// </summary>
    public partial class Paper_QuestionView : AuthoringTool_BasicForm_BasicForm
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


        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.Initiate();

            //接收參數
            this.getParameter();

            if (!IsPostBack)
            {
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

            //建立選擇題表格
            this.setupQuestionTable();

            //建立問答題表格
            this.setupTextQuestionTable();

            //加入Delete button的事件
            btnDeleteQuestion.ServerClick += new ImageClickEventHandler(btnDeleteQuestion_ServerClick);

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
            string strSQL = mySQL.getGroupTextQuestion(Session["GroupID"].ToString());

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

                    //取得Answer
                    string strAnswer = "";
                    strAnswer = dsQuestionList.Tables[0].Rows[i]["cAnswer"].ToString();

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
                        trTextQuestionTitle.Attributes.Add("onclick", "ShowDetail('" + strQID + "','img_" + strQID + "')");

                        //建立問題的標題
                        TableRow trQuestionTitle = new TableRow();
                        trQuestionTitle.Attributes.Add("Class", "header1_tr_odd_row");
                        table.Rows.Add(trQuestionTitle);
                        trQuestionTitle.ID = "trQuestionTitle_" + strQID;

                        //建立問題的內容
                        TableRow trQuestion = new TableRow();
                        trQuestion.Attributes.Add("Class", "header1_tr_even_row");
                        table.Rows.Add(trQuestion);
                        trQuestion.ID = "trQuestion_" + strQID;

                        //建立Answer的標題
                        TableRow trAnswerTitle = new TableRow();
                        trAnswerTitle.Attributes.Add("Class", "header1_tr_odd_row");
                        table.Rows.Add(trAnswerTitle);
                        trAnswerTitle.ID = "trAnswerTitle_" + strQID;

                        //建立Answer的內容
                        TableRow trAnswer = new TableRow();
                        trAnswer.Attributes.Add("Class", "header1_tr_even_row");
                        table.Rows.Add(trAnswer);
                        trAnswer.ID = "trAnswer_" + strQID;

                        //建立Keyword的標題
                        TableRow trKeywordTitle = new TableRow();
                        trKeywordTitle.Attributes.Add("Class", "header1_tr_odd_row");
                        table.Rows.Add(trKeywordTitle);
                        trKeywordTitle.ID = "trKeywordTitle_" + strQID;

                        //建立Keyword的內容
                        TableRow trKeyword = new TableRow();
                        trKeyword.Attributes.Add("Class", "header1_tr_even_row");
                        table.Rows.Add(trKeyword);
                        trKeyword.ID = "trKeyword_" + strQID;

                        //建立同義問題的標題
                        TableRow trSynQuestionTitle = new TableRow();
                        trSynQuestionTitle.Attributes.Add("Class", "header1_tr_odd_row");
                        table.Rows.Add(trSynQuestionTitle);
                        trSynQuestionTitle.ID = "trSynQuestionTitle_" + strQID;
                        trSynQuestionTitle.Style.Add("CURSOR", "hand");

                        //建立同義問題的內容
                        TableRow trSynQuestion = new TableRow();
                        trSynQuestion.Attributes.Add("Class", "header1_tr_even_row");
                        table.Rows.Add(trSynQuestion);
                        trSynQuestion.ID = "trSynQuestion_" + strQID;
                        trSynQuestionTitle.Attributes.Add("onclick", "ShowSynDetail('" + trSynQuestion.ID + "' , 'imgSynQuestion_" + strQID + "')");
                        trSynQuestion.Style.Add("display", "none");

                        //建立同義答案的標題
                        TableRow trSynAnswerTitle = new TableRow();
                        trSynAnswerTitle.Attributes.Add("Class", "header1_tr_odd_row");
                        table.Rows.Add(trSynAnswerTitle);
                        trSynAnswerTitle.ID = "trSynAnswerTitle_" + strQID;
                        trSynAnswerTitle.Style.Add("CURSOR", "hand");

                        //建立同義答案的內容
                        TableRow trSynAnswer = new TableRow();
                        trSynAnswer.Attributes.Add("Class", "header1_tr_even_row");
                        table.Rows.Add(trSynAnswer);
                        trSynAnswer.ID = "trSynAnswer_" + strQID;
                        trSynAnswerTitle.Attributes.Add("onclick", "ShowSynDetail('" + trSynAnswer.ID + "' , 'imgSynAnswer_" + strQID + "')");
                        trSynAnswer.Style.Add("display", "none");

                        #region 管控同義項的顯示與隱藏
                        DataTable dtSynQuestion = new DataTable();
                        DataTable dtSynAnswer = new DataTable();
                        dtSynQuestion = clsInterrogationEnquiry.GetSynQuestion(strQID);
                        dtSynAnswer = clsInterrogationEnquiry.GetSynAnswer(strQID);
                        if (dtSynQuestion.Rows.Count == 0)
                        {
                            trSynQuestionTitle.Style.Add("display", "none");
                            trSynQuestion.Style.Add("display", "none");
                        }
                        if (dtSynAnswer.Rows.Count == 0)
                        {
                            trSynAnswerTitle.Style.Add("display", "none");
                            trSynAnswer.Style.Add("display", "none");
                        }
                        #endregion

                        //問題的題號
                        //					TableCell tcQuestionNum = new TableCell();
                        //					trQuestion.Cells.Add(tcQuestionNum);
                        //					tcQuestionNum.Style["width"] = "50px";
                        //					tcQuestionNum.Text = "Q" + intQuestionIndex.ToString() + ": ";



                        TableCell tcTextQuestionTitle = new TableCell();
                        trTextQuestionTitle.Cells.Add(tcTextQuestionTitle);
                        tcTextQuestionTitle.Text = "Q" + intQuestionIndex.ToString() + ": ";
                        tcTextQuestionTitle.Text = "<IMG id='img_" + strQID + "' src='../../../BasicForm/Image/minus.gif'>&nbsp;Q" + intQuestionIndex.ToString() + " : ";

                        TableCell tcQuestionTitle = new TableCell();
                        trQuestionTitle.Cells.Add(tcQuestionTitle);
                        tcQuestionTitle.Text = "<font style='color:Black; font-weight:bold'>Question : <font/>";

                        TableCell tcSynQuestionTitle = new TableCell();
                        trSynQuestionTitle.Cells.Add(tcSynQuestionTitle);
                        tcSynQuestionTitle.Text = "<IMG id='imgSynQuestion_" + strQID + "' src='../../../BasicForm/Image/plus.gif'><font style='color:Black; font-weight:bold'>&nbsp;Synonymous Question : <font/>";

                        TableCell tcAnswerTitle = new TableCell();
                        trAnswerTitle.Cells.Add(tcAnswerTitle);
                        tcAnswerTitle.Text = "<font style='color:Black; font-weight:bold'>Answer :<font/>";

                        TableCell tcSynAnswerTitle = new TableCell();
                        trSynAnswerTitle.Cells.Add(tcSynAnswerTitle);
                        tcSynAnswerTitle.Text = "<IMG id='imgSynAnswer_" + strQID + "' src='../../../BasicForm/Image/plus.gif'><font style='color:Black; font-weight:bold'>&nbsp;Synonymous Answer : <font/>";

                        TableCell tcKeywordTitle = new TableCell();
                        trKeywordTitle.Cells.Add(tcKeywordTitle);
                        tcKeywordTitle.Text = "<font style='color:Black; font-weight:bold'>Keyword : <font/>";

                        TableCell tcQuestion = new TableCell();
                        trQuestion.Cells.Add(tcQuestion);

                        if (arrKeyword.Length > 0)
                        {
                            for (int kcount = 0; kcount < arrKeyword.Length; kcount++)
                            {
                                strQuestion = strQuestion.Replace(arrKeyword[kcount], "<span class='span_keyword' >" + arrKeyword[kcount] + "</span>"); ;
                                tcQuestion.Text = strQuestion;
                            }
                        }

                        TableCell tcSynQuestion = new TableCell();
                        trSynQuestion.Cells.Add(tcSynQuestion);
                        BulidInterrogation("Question", strQID, tcSynQuestion);

                        TableCell tcAnswer = new TableCell();
                        trAnswer.Cells.Add(tcAnswer);

                        for (int kcount = 0; kcount < arrKeyword.Length; kcount++)
                        {
                            strAnswer = strAnswer.Replace(arrKeyword[kcount], "<span class='span_keyword' >" + arrKeyword[kcount] + "</span>"); ;
                        }

                        tcAnswer.Text = strAnswer;

                        TableCell tcSynAnswer = new TableCell();
                        trSynAnswer.Cells.Add(tcSynAnswer);
                        BulidInterrogation("Answer", strQID, tcSynAnswer);


                        TableCell tcKeyword = new TableCell();
                        trKeyword.Cells.Add(tcKeyword);
                        tcKeyword.Text = strGetKeyword;


                        //建立修改按鈕的TableRow
                        TableRow trModify = new TableRow();
                        //trModify.Attributes.Add("Class", "header1_tr_even_row");
                        table.Rows.Add(trModify);
                        trModify.ID = "trModify_" + strQID;

                        TableCell tcModify = new TableCell();
                        trModify.Cells.Add(tcModify);
                        tcModify.Attributes["align"] = "right";
                        //					tcModify.ColumnSpan = 2;

                        //問題的難易度
                        Label lbQuestionLevel = new Label();
                        tcModify.Controls.Add(lbQuestionLevel);
                        int iQuestionLevel = AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevelValue(strQID);
                        if (iQuestionLevel != -1)
                            lbQuestionLevel.Text = "Question Level：" + AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevelName_SELECT_LevelName(iQuestionLevel) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                        //問題的病徵
                        Label lbQuestionSymptoms = new Label();
                        tcModify.Controls.Add(lbQuestionSymptoms);
                        lbQuestionSymptoms.Text = "Question Symptoms：" + AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevel_SELECT_QuestionSymptoms(strQID) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

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
                        table.Rows.Add(trQuestion);

                        intQuestionIndex += 1;

                        //問題的CheckBox
                        //						TableCell tcCheckBox = new TableCell();
                        //						trQuestion.Cells.Add(tcCheckBox);
                        //						tcCheckBox.Width = Unit.Pixel(25);
                        //
                        //						CheckBox chQuestion = new CheckBox();
                        //						tcCheckBox.Controls.Add(chQuestion);
                        //						tcCheckBox.Attributes.Add("onclick","ShowbtnDelete();");
                        //						string strID = "";
                        //						strID = "ch-" + dsQuestion.Tables[0].Rows[0]["cQID"].ToString();
                        //						
                        //						chQuestion.ID = strID;

                        //問題的題號
                        TableCell tcQuestionNum = new TableCell();
                        trQuestion.Cells.Add(tcQuestionNum);
                        tcQuestionNum.Width = Unit.Pixel(25);
                        tcQuestionNum.Text = "Q" + intQuestionIndex.ToString() + ": ";

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

                        //建立修改問題的Button
                        TableCell tcModify = new TableCell();
                        trModify.Cells.Add(tcModify);
                        tcModify.Attributes["align"] = "right";
                        tcModify.ColumnSpan = 2;

                        //問題的分數
                        Label lbQuestionGrade = new Label();
                        tcModify.Controls.Add(lbQuestionGrade);
                        string strQuestionGrade = AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevel_SELECT_Grade(strQID);
                        if (strQuestionGrade != "-1")
                            lbQuestionGrade.Text = "Question Grade：" + strQuestionGrade + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                        //問題的難易度
                        Label lbQuestionLevel = new Label();
                        tcModify.Controls.Add(lbQuestionLevel);
                        int iQuestionLevel = AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevelValue(strQID);
                        if (iQuestionLevel != -1)
                            lbQuestionLevel.Text = "Question Level：" + AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevelName_SELECT_LevelName(iQuestionLevel) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                        Button btnModifySelection = new Button();
                        tcModify.Controls.Add(btnModifySelection);
                        btnModifySelection.ID = "btnModifySelection-" + strQID;
                        btnModifySelection.Text = "Modify";
                        btnModifySelection.Click += new EventHandler(btnModifySelection_Click);
                        btnModifySelection.Style["width"] = "150px";

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

        private void getParameter()
        {
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
            Response.Redirect("./CommonQuestionEdit/Page/ShowQuestion.aspx?QID=" + strQID + "&GroupID=" + Session["GroupID"].ToString());
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
            SQLString.deleteTextQuestionByQID(strQID);

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

            //呼叫Paper_TextQuestionEditor.aspx
            Response.Redirect("Paper_TextQuestionEditor.aspx?QID=" + strQID + "&GroupID=" + Session["GroupID"].ToString());
        }

        //建立同義問題或答案
        private void BulidInterrogation(string strType, string strQID, TableCell tcSynQuestion)
        {
            DataTable dtSyn = new DataTable();
            if (strType == "Question")
            {
                dtSyn = clsInterrogationEnquiry.GetSynQuestion(strQID);
            }
            else if (strType == "Answer")
            {
                dtSyn = clsInterrogationEnquiry.GetSynAnswer(strQID);
            }

            if (dtSyn.Rows.Count > 0)
            {
                #region 同義問題或答案內容
                DataTable dtSynonymousItem = clsInterrogationEnquiry.GetSynonymousItem(strType, "synonymous", strQID);
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
    }
}
