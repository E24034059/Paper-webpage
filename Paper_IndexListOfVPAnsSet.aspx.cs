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
    public partial class Paper_IndexListOfVPAnsSet : AuthoringTool_BasicForm_BasicForm
    {
        //建立SqlDB物件
        SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
        DataReceiver myReceiver = new DataReceiver();
        SQLString mySQL = new SQLString();

        //題目的編號
        int intQuestionIndex = 0;
        string strCurrentProType;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["SelectedIndex"] != null)
                {
                    hfSelectedIndex.Value = Request.QueryString["SelectedIndex"].ToString();
                    string strGetConQuesSQL = "SELECT cQuestion FROM Conversation_Question WHERE cQID= '" + hfSelectedIndex.Value.ToString() + "'";
                    DataTable dtGetConQues = sqldb.getDataSet(strGetConQuesSQL).Tables[0];
                    if (dtGetConQues.Rows.Count > 0)
                        LbCurrentConQues.Text = "<span style='color:red;'>" + dtGetConQues.Rows[0]["cQuestion"].ToString() + "</span>";
                }

                string strSQL = "SELECT * FROM VP_AnswerSet WHERE cGroupID= '" + Request.QueryString["GroupID"].ToString() + "' ORDER BY cVPAID";
                DataSet dsQuestionList = sqldb.getDataSet(strSQL);
                if (dsQuestionList.Tables[0].Rows.Count <= 0)
                {
                    Response.Redirect("Paper_VPAnswerSetEditor.aspx?Opener=EditTestPaper_VPAns&GroupID=" + Request.QueryString["GroupID"].ToString() + "&CurrentProType=All&VPResponseType=Default&SelectedIndex=" + hfSelectedIndex.Value.ToString());
                }
            }
            strCurrentProType = Request.QueryString["CurrentProType"].ToString();
            LbCurrentSymptoms.Text = strCurrentProType;

            intQuestionIndex = 0;

            //建立IndexList表格
            this.setupIndexList();

            btnClose.Attributes.Add("onclick", "ClosePage()");
        }

        protected void setupIndexList()
        {
            tcIndexList.Controls.Clear();
            Table table = new Table();
            tcIndexList.Controls.Add(table);
            table.CellSpacing = 0;
            table.CellPadding = 5;
            table.BorderStyle = BorderStyle.Solid;
            table.BorderWidth = Unit.Pixel(1);
            table.BorderColor = System.Drawing.Color.Black;
            table.GridLines = GridLines.Both;
            table.Width = Unit.Percentage(100);

            //建立Table的CSS
            table.CssClass = "header1_table";

            //依照CurrentProType決定取出VP Answer
            string strSQL = "SELECT * FROM VP_AnswerSet WHERE cProblemType = '" + Request.QueryString["CurrentProType"].ToString() + "' AND cGroupID= '" + Request.QueryString["GroupID"].ToString() + "' ORDER BY cVPAID";
            DataSet dsIndexList = sqldb.getDataSet(strSQL);
            if (dsIndexList.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsIndexList.Tables[0].Rows.Count; i++)
                {
                    //取得VPAID
                    string strVPAID = "";
                    strVPAID = dsIndexList.Tables[0].Rows[i]["cVPAID"].ToString();
                    if (i > 0)
                    {
                        // 判斷VPAID是否重複，目的是為了讓顯示題目時不會只顯示一個題目  老詹 2014/12/10
                        if (dsIndexList.Tables[0].Rows[i - 1]["cVPAID"].ToString() == dsIndexList.Tables[0].Rows[i]["cVPAID"].ToString())
                            continue;
                    }

                    //取得ProblemType
                    string strProblemType = "";
                    strProblemType = dsIndexList.Tables[0].Rows[i]["cProblemType"].ToString();

                    //取得VPResponseType
                    string strVPResponseType = "";
                    strVPResponseType = dsIndexList.Tables[0].Rows[i]["cVPResponseType"].ToString();

                    //取得VPResponseContent
                    string strVPResponseContent = "";
                    strVPResponseContent = dsIndexList.Tables[0].Rows[i]["cVPResponseContent"].ToString();

                    //建立VP Answer
                    intQuestionIndex += 1;
                    TableRow trIndexListTitle = new TableRow();
                    trIndexListTitle.Attributes.Add("Class", "header1_table_first_row");
                    table.Rows.Add(trIndexListTitle);

                    TableCell tcIndexListTitle = new TableCell();
                    clsHintsDB HintsDB = new clsHintsDB();
                    string strGetVPAnsTitle = "SELECT cVPAnsTitle FROM VP_AnswerSet WHERE cVPAID = '" + strVPAID + "'";
                    DataTable dtVPAnsTitle = HintsDB.getDataSet(strGetVPAnsTitle).Tables[0];
                    string strVPAnsTitle = "";
                    if (dtVPAnsTitle.Rows.Count>0)
                        strVPAnsTitle = dtVPAnsTitle.Rows[0]["cVPAnsTitle"].ToString();
                    tcIndexListTitle.Text = "VP's Answer" + intQuestionIndex.ToString() + ": <span style='color:yellow;'>" + strVPAnsTitle + "</span>";
                    tcIndexListTitle.ColumnSpan = 2;
                    trIndexListTitle.Cells.Add(tcIndexListTitle);            

                    DataTable dtVPResponseType = clsConversation.VPResponseType_SELECT_AllResponseType(strVPAID);
                    if (dtVPResponseType.Rows.Count > 0)
                    {
                        int iACount = 1;
                        foreach (DataRow drVPResponseType in dtVPResponseType.Rows)
                        {
                            string strResponseTypeName = "";
                            strResponseTypeName = drVPResponseType["cVPResponseType"].ToString();

                            //建立Response Type的標題
                            TableRow trResponseTypeTitle = new TableRow();
                            trResponseTypeTitle.Attributes.Add("Class", "header1_tr_odd_row");
                            table.Rows.Add(trResponseTypeTitle);
                            trResponseTypeTitle.ID = "trResponseTypeTitle_" + strVPAID + "_" + iACount;

                            TableCell tcResponseTypeTitle = new TableCell();
                            trResponseTypeTitle.Cells.Add(tcResponseTypeTitle);
                            RadioButton rbResponseType = new RadioButton();
                            rbResponseType.ID = "rbResponseType|" + strVPAID + "|" + strResponseTypeName;
                            rbResponseType.GroupName = "rbResponseTypeGroup_" + strVPAID;
                            rbResponseType.Text = "<font style='color:Black; font-weight:bold'>Response Type " + iACount + "  :&nbsp;<font/>";
                            tcResponseTypeTitle.Controls.Add(rbResponseType);
                            if (iACount == 1)
                                rbResponseType.Checked = true;
                            tcResponseTypeTitle.Style.Add("text-align", "right");
                            tcResponseTypeTitle.Width = Unit.Pixel(230);

                            TableCell tcResponse = new TableCell();
                            trResponseTypeTitle.Cells.Add(tcResponse);
                            tcResponse.Text = strResponseTypeName;
                            tcResponse.Attributes.Add("Class", "header1_tr_even_row");
                            tcResponse.Width = Unit.Percentage(81);

                            //建立答案型態的答案內容的標題
                            TableRow trResponseTypeContentTitle = new TableRow();
                            trResponseTypeContentTitle.Attributes.Add("Class", "header1_tr_odd_row");
                            table.Rows.Add(trResponseTypeContentTitle);
                            trResponseTypeContentTitle.ID = "trResponseTypeContentTitle_" + strVPAID + "_" + iACount;

                            TableCell tcResponseTypeContentTitle = new TableCell();
                            trResponseTypeContentTitle.Cells.Add(tcResponseTypeContentTitle);
                            tcResponseTypeContentTitle.Text = "<font style='color:Black; font-weight:bold'>Response Content " + iACount + ":&nbsp;<font/>";
                            tcResponseTypeContentTitle.Style.Add("text-align", "right");
                            tcResponseTypeContentTitle.Width = Unit.Pixel(230);

                            TableCell tcResponseTypeContent = new TableCell();
                            trResponseTypeContentTitle.Cells.Add(tcResponseTypeContent);
                            BulidResponseTypeContent("Response", strVPAID, tcResponseTypeContent, strResponseTypeName);
                            tcResponseTypeContent.Attributes.Add("Class", "header1_tr_even_row");
                            tcResponseTypeContent.Width = Unit.Percentage(81);

                            iACount++;

                        }
                    }

                    //建立StudentAnswerType的標題
                    TableRow trStudentAnswerType = new TableRow();
                    trStudentAnswerType.Attributes.Add("Class", "header1_tr_odd_row");
                    trStudentAnswerType.ID = "trStudentAnswerType_" + strVPAID;
                    table.Rows.Add(trStudentAnswerType);

                    TableCell tcStudentAnswerType = new TableCell();
                    trStudentAnswerType.Cells.Add(tcStudentAnswerType);
                    tcStudentAnswerType.Text = "<font style='color:Black; font-weight:bold'>Student Answer Type :&nbsp; <font/>";
                    tcStudentAnswerType.Style.Add("text-align", "right");
                    tcStudentAnswerType.Width = Unit.Pixel(230);

                    TableCell tcAnswerTypeName = new TableCell();
                    trStudentAnswerType.Cells.Add(tcAnswerTypeName);
                    DataTable dtGetStudentAnsType = clsConversation.StudentAnsType_SELECT_AnswerType(strVPAID);
                    if (dtGetStudentAnsType.Rows.Count > 0)
                    {
                        //以Index設定Answer Type Name  老詹 2014/12/10
                        if (dtGetStudentAnsType.Rows[0]["iAnswerType"].ToString() == "1")
                            tcAnswerTypeName.Text = "TextBox";
                        else if (dtGetStudentAnsType.Rows[0]["iAnswerType"].ToString() == "2")
                            tcAnswerTypeName.Text = "DropDownList";
                        else if (dtGetStudentAnsType.Rows[0]["iAnswerType"].ToString() == "3")
                            tcAnswerTypeName.Text = "RadioButton";
                        else if (dtGetStudentAnsType.Rows[0]["iAnswerType"].ToString() == "4")
                            tcAnswerTypeName.Text = "CheckBox";
                        tcAnswerTypeName.Style.Add("color", "Green");
                    }
                    else
                    {
                        tcAnswerTypeName.Text = "None";
                        tcAnswerTypeName.Style.Add("color", "Red");
                    }
                    tcAnswerTypeName.Attributes.Add("Class", "header1_tr_even_row");

                    //建立修改按鈕的TableRow
                    TableRow trModify = new TableRow();
                    table.Rows.Add(trModify);
                    trModify.ID = "trModify_" + strVPAID;

                    TableCell tcModify = new TableCell();
                    trModify.Cells.Add(tcModify);
                    tcModify.Attributes["align"] = "right";
                    tcModify.ColumnSpan = 2;

                    //建立編輯題組的Button(只有單選選項才可以編輯)
                    Button btnEditGroupQuestionSelection = new Button();
                    if (dtGetStudentAnsType.Rows[0]["iAnswerType"].ToString() != "1" && dtGetStudentAnsType.Rows[0]["iAnswerType"].ToString() != "4")
                    { tcModify.Controls.Add(btnEditGroupQuestionSelection); }
                    btnEditGroupQuestionSelection.ID = "btnEditGroupQuestionSelection-" + strVPAID;
                    btnEditGroupQuestionSelection.Text = "Edit group questions";
                    btnEditGroupQuestionSelection.Click += new EventHandler(btnEditGroupQuestionSelection_Click);
                    btnEditGroupQuestionSelection.Style["width"] = "180px";
                    btnEditGroupQuestionSelection.Style["cursor"] = "pointer";
                    btnEditGroupQuestionSelection.CssClass = "button_continue";

                    //建立間隔
                    Label lblCellQuestionGroup = new Label();
                    tcModify.Controls.Add(lblCellQuestionGroup);
                    lblCellQuestionGroup.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                    //建立修改問題的Button
                    Button btnModifyConversation = new Button();
                    btnModifyConversation.Style["width"] = "150px";
                    btnModifyConversation.ID = "btnModifyConversation-" + strVPAID;
                    btnModifyConversation.Text = "Modify";
                    btnModifyConversation.Click += new EventHandler(btnModifyVPAnswer_Click);
                    btnModifyConversation.CssClass = "button_continue";
                    btnModifyConversation.Style["cursor"] = "pointer";
                    tcModify.Controls.Add(btnModifyConversation);

                    //建立間隔
                    Label lblCell = new Label();
                    tcModify.Controls.Add(lblCell);
                    lblCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                    //建立刪除對話題的Button
                    Button btnDeleteConversation = new Button();
                    tcModify.Controls.Add(btnDeleteConversation);
                    btnDeleteConversation.Style["width"] = "150px";
                    btnDeleteConversation.Style["cursor"] = "pointer";
                    btnDeleteConversation.ID = "btnDeleteConversation-" + strVPAID;
                    btnDeleteConversation.Text = "Delete";
                    btnDeleteConversation.Click += new EventHandler(btnDeleteVPAnswer_Click);
                    btnDeleteConversation.CssClass = "button_continue";
                }
            }
            else
            {
                //此問卷沒有任何VPAnswer的情況
                trIndexList.Style["display"] = "none";
            }
            dsIndexList.Dispose();
        }

        //建立Response型態的答案內容
        private void BulidResponseTypeContent(string strType, string strQID, TableCell tcResponseTypeContent, string strResponseTypeName)
        {
            DataTable dtConversation = new DataTable();
            if (strType == "Response")
            {
                clsHintsDB HintsDB = new clsHintsDB();
                string strSQL = "SELECT * FROM VP_AnswerSet WHERE cVPAID = '" + strQID + "' AND cVPResponseType='" + strResponseTypeName + "'";
                dtConversation = HintsDB.getDataSet(strSQL).Tables[0];
            }
            if (dtConversation.Rows.Count > 0)
            {
                //答案項目的內容
                Label lbAnswerItemValue = new Label();
                lbAnswerItemValue.Text = dtConversation.Rows[0]["cVPResponseContent"].ToString();

                tcResponseTypeContent.Controls.Add(lbAnswerItemValue);
            }
        }

        protected void btnModifyVPAnswer_Click(object sender, EventArgs e)
        {
            //修改一個VPAsnwer  
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
                if (Request.Form.Keys[i].ToString().IndexOf("rbResponseTypeGroup_" + strQID) != -1)
                {
                    strAnswerTypeNum = Request.Form[i].ToString().Split('|');
                    break;
                }
            }

            //呼叫Paper_VPAnswerSetEditor.aspx
            Response.Redirect("Paper_VPAnswerSetEditor.aspx?Opener=Paper_IndexListOfVPAnsSet&GroupID=" + Request.QueryString["GroupID"].ToString() + "&CurrentProType=" + strCurrentProType + "&VPResponseType=" + strAnswerTypeNum[2] + "&VPAID=" + strQID + "&SelectedIndex=" + hfSelectedIndex.Value.ToString());
        }

        protected void btnDeleteVPAnswer_Click(object sender, EventArgs e)
        {
            //刪除一個VPAsnwer  
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

            SqlDB myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
            string strSQL = "DELETE VP_AnswerSet WHERE cVPAID = '" + strQID + "' ";
            myDB.ExecuteNonQuery(strSQL);
            string strSQL2 = "DELETE StudentAnsType WHERE cVPAID = '" + strQID + "' ";
            myDB.ExecuteNonQuery(strSQL2);

            intQuestionIndex = 0;
            this.setupIndexList();
        }

        protected void btAddVPAnswer_Click(object sender, EventArgs e)
        {
            Response.Redirect("Paper_VPAnswerSetEditor.aspx?Opener=Paper_IndexListOfVPAnsSet&GroupID=" + Request.QueryString["GroupID"].ToString() + "&CurrentProType=" + strCurrentProType + "&VPResponseType=Default&SelectedIndex=" + hfSelectedIndex.Value.ToString());
        }

        void btnEditGroupQuestionSelection_Click(object sender, EventArgs e)
        {
            //編輯選擇題題組

            string[] strIDArray = ((Button)(sender)).ID.Split('-');
            string strVPAID = strIDArray[1];

            Response.Redirect("Paper_QuestionGroupView.aspx?QID=" + strVPAID + "&GroupID=" + Request.QueryString["GroupID"].ToString() + "&CurrentProType=" + strCurrentProType);
        }
    }
}