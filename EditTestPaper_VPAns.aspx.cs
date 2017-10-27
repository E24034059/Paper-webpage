﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Hints.DB;
using System.Drawing;
using System.Collections.Generic;

public partial class AuthoringTool_CaseEditor_Paper_EditTestPaper_VPAns : System.Web.UI.Page
{
    protected clsHintsDB hintsDB = new clsHintsDB();
    bool flag, IsInit;
    int colorCount, CongroupCount=1;
    string strAllGroup, strAllQID="";
    DataTable dtCheckProblemType = new DataTable();
    DataTable dtWrongWayQues = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(AuthoringTool_CaseEditor_Paper_EditTestPaper_VPAns));

        if (Page.IsPostBack) { ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "CallJS", "afterpostback();", true); }

        IsInit = false;
        DataTable dtTmp = new DataTable();
        string strSQL = "SELECT cNodeName FROM QuestionGroupTree WHERE cNodeID LIKE '" + Request.QueryString["GroupID"].ToString() + "' ORDER BY  cNodeID asc";
        dtTmp = hintsDB.getDataSet(strSQL).Tables[0];
        Lb_Title.Text = dtTmp.Rows[0]["cNodeName"].ToString();

        if(Request.QueryString["NewTopic"].ToString() == "Yes") // 如果是編輯新主題的題組，則只顯示Close按鈕  老詹 2015/05/17
        {
            BtnNext.Attributes.Add("style","display:none;");
            BtnBack.Visible = false;
            BtnClose.Attributes.Add("style", "font-size: x-large; background-color: #0000FF; color: #FFFFFF; cursor:pointer; display:;");
        }

        ConstructConversation();
        if(!IsPostBack) //建立DDL事件
        {
            ChangeLanguage(); // 多國語言函式  老詹 2015/04/14

            string strddlSQL = "SELECT DISTINCT B.cTestAnswerType FROM BasicQuestionList AS B, QuestionLevel AS L, ProblemTypeTree AS P WHERE B.cQID=L.cQID AND L.cQuestionSymptoms = P.cNodeName AND B.cQuestionTopic LIKE '%" + Request.QueryString["GroupID"].ToString() + "%'";
            DataTable dtCheckProblemType = hintsDB.getDataSet(strddlSQL).Tables[0];
            ddlProblemType.Items.Clear();
            ddlProblemType.Items.Add(new ListItem("Select a Problem Type", "Select a Problem Type"));
            for (int i = 0; i < dtCheckProblemType.Rows.Count; i++)
            {
                if (i > 0) //預防重複Problem Type的BUG
                {
                    if (dtCheckProblemType.Rows[i]["cTestAnswerType"].ToString() == dtCheckProblemType.Rows[i - 1]["cTestAnswerType"].ToString())
                    {
                        continue;
                    }
                }
                ddlProblemType.Items.Add(new ListItem(dtCheckProblemType.Rows[i]["cTestAnswerType"].ToString(), dtCheckProblemType.Rows[i]["cTestAnswerType"].ToString()));
                ddlProblemType.SelectedIndex = 0;
            }
            if (Request.QueryString["SelectedIndex"] != null && Request.QueryString["SelectedIndex"].ToString() != "")
            {
                IsInit = true;
                for (int i = 1; i <= Convert.ToInt32(HF_RowCount.Value); i++)
                {
                    HtmlInputButton btnTmp = (HtmlInputButton)this.FindControl("btnBrowse_" + i + "_" + Request.QueryString["SelectedIndex"].ToString());
                    if (btnTmp != null)
                    {
                        RadioButton rbSelected = (RadioButton)this.FindControl("CheckBoxforConversation_" + i);
                        if (rbSelected != null)
                        {
                            Session["SelectedRb"] = rbSelected;
                            cbConstructVPAns_Checked(rbSelected, e);
                        }
                    }
                }
            }
        }
        HF_CaseID.Value = Request.QueryString["CaseID"].ToString();

        btnSaveNext.ServerClick += new EventHandler(btnSaveNext_ServerClick);
        btnSetScoreServer.ServerClick += new EventHandler(btnSetScoreServer_ServerClick);
        btnConstructCon.ServerClick += new EventHandler(btnConstructCon_ServerClick);   
    }

    protected void RecoverChecked(string strSelectedIndex)
    {
        DataTable dtCheck = new DataTable();
        Label SelectedLabel = (Label)tbSelectConversation.FindControl("lbTopic_" + strSelectedIndex);

        if (SelectedLabel.Text.IndexOf("題組") >= 0)
        {
            string strcheckSQL = "SELECT * FROM BasicQuestionList WHERE cCaseID='" + Request.QueryString["CaseID"].ToString() + "' AND cQuestionTopic LIKE '" + HF_GroupID.Value.ToString() + "%'"; // 找以HF_GroupID開頭的字串
            dtCheck = hintsDB.getDataSet(strcheckSQL).Tables[0];
            foreach(DataRow dr in dtCheck.Rows)
            {
                HtmlInputButton SelectedButton = (HtmlInputButton)tbSelectConversation.FindControl("btnBrowse_" + strSelectedIndex + "_" + dr["cQID"].ToString());
                if (SelectedButton != null)
                {
                    HtmlInputRadioButton SelectedQID = (HtmlInputRadioButton)tbSelectConversation.FindControl("CheckBoxforVPAnswer-" + dr["cVPAID"].ToString());
                    if (SelectedQID != null)
                        SelectedQID.Checked = true;
                }
            }
        }
        else
        {
            string strcheckSQL = "SELECT * FROM BasicQuestionList WHERE cCaseID='" + Request.QueryString["CaseID"].ToString() + "' AND cQuestionTopic = '" + (Request.QueryString["GroupID"].ToString() + "/" + HF_GroupID.Value.ToString()) + "'";
            dtCheck = hintsDB.getDataSet(strcheckSQL).Tables[0];
            if (dtCheck.Rows.Count > 0)
            {
                string[] strQIDArray = dtCheck.Rows[0]["cVPAID"].ToString().Split(',');
                for (int i = 0; i < strQIDArray.Length; i++)
                {
                    HtmlInputRadioButton SelectedQID = (HtmlInputRadioButton)tbSelectConversation.FindControl("CheckBoxforVPAnswer-" + strQIDArray[i]);
                    if (SelectedQID != null)
                        SelectedQID.Checked = true;
                }
            }
        }
    }

    protected void ConstructConversation()
    {
        tbSelectConversation.Controls.Clear();
        if (flag == false && (ddlProblemType.SelectedItem.Text == "Select a Problem Type"))
        {
            if (Request.QueryString["NewTopic"].ToString() == "Yes")
            {
                string strSQL = "SELECT * FROM BasicQuestionList AS B, QuestionMode AS M, Conversation_Question AS C WHERE B.cQID=M.cQID AND B.cQID = C.cQID AND B.cQuestionTopic LIKE '%" + Request.QueryString["GroupID"].ToString() + "%' AND B.cCaseID='" + Request.QueryString["CaseID"].ToString() + "' AND cCaseID='" + Request.QueryString["CaseID"].ToString() + "' ORDER BY B.cPaperID";
                dtCheckProblemType = hintsDB.getDataSet(strSQL).Tables[0];
            }
            else
            {
                string strSQL = "SELECT * FROM BasicQuestionList WHERE (cQuestionTopic LIKE '%" + Request.QueryString["GroupID"].ToString() + "%') AND cCaseID='" + Request.QueryString["CaseID"].ToString() +"' ORDER BY cPaperID";
                dtCheckProblemType = hintsDB.getDataSet(strSQL).Tables[0];
                string strGroupSQL = "SELECT B.* FROM BasicQuestionList AS B, QuestionMode AS M, QuestionGroupTree AS G WHERE (B.cQuestionTopic LIKE '%' + B.cQID + '%') AND B.cQID = M.cQID AND M.cQuestionGroupID = G.cNodeID AND G.cParentID = '" + Request.QueryString["GroupID"].ToString() + "' AND cCaseID='" + Request.QueryString["CaseID"].ToString() + "' ORDER BY B.cPaperID";
                DataTable dtGroup = hintsDB.getDataSet(strGroupSQL).Tables[0];
                if (dtGroup.Rows.Count > 0)
                {   //抓BQL資料表中屬於題組的題目
                    for (int i = 0; i < dtGroup.Rows.Count; i++)
                    {
                        DataRow drGroup = dtCheckProblemType.NewRow();
                        drGroup["cPaperID"] = dtGroup.Rows[i]["cPaperID"];
                        drGroup["cQuestionTopic"] = dtGroup.Rows[i]["cQuestionTopic"];
                        drGroup["cQID"] = dtGroup.Rows[i]["cQID"];
                        drGroup["cVPAID"] = dtGroup.Rows[i]["cVPAID"];
                        drGroup["cTestAnswerType"] = dtGroup.Rows[i]["cTestAnswerType"];
                        drGroup["cCaseID"] = dtGroup.Rows[i]["cCaseID"];
                        drGroup["bIsOriginal"] = dtGroup.Rows[i]["bIsOriginal"];
                        dtCheckProblemType.Rows.Add(drGroup);
                    }
                }
            }
        }
        else
        {
            if (Request.QueryString["NewTopic"].ToString() == "Yes")
            {
                string strSQL = "SELECT * FROM BasicQuestionList AS B, QuestionMode AS M, Conversation_Question AS C WHERE B.cQID=M.cQID AND B.cQID = C.cQID AND B.cQuestionTopic LIKE '%" + Request.QueryString["GroupID"].ToString() + "%' AND B.cCaseID='" + Request.QueryString["CaseID"].ToString() + "' AND B.cTestAnswerType='" + ddlProblemType.SelectedItem.Text.ToString() + "' ORDER BY B.cPaperID";
                dtCheckProblemType = hintsDB.getDataSet(strSQL).Tables[0];
            }
            else
            {
                string strSQL = "SELECT * FROM BasicQuestionList WHERE (cQuestionTopic LIKE '%" + Request.QueryString["GroupID"].ToString() + "%') AND cTestAnswerType='" + ddlProblemType.SelectedItem.Text.ToString() + "' AND cCaseID='" + Request.QueryString["CaseID"].ToString() + "' ORDER BY cPaperID";
                dtCheckProblemType = hintsDB.getDataSet(strSQL).Tables[0];
                string strGroupSQL = "SELECT B.* FROM BasicQuestionList AS B, QuestionMode AS M, QuestionGroupTree AS G WHERE (B.cQuestionTopic LIKE '%' + B.cQID + '%') AND B.cQID = M.cQID AND M.cQuestionGroupID = G.cNodeID AND G.cParentID = '" + Request.QueryString["GroupID"].ToString() + "' AND B.cTestAnswerType='" + ddlProblemType.SelectedItem.Text.ToString() + "' AND cCaseID='" + Request.QueryString["CaseID"].ToString() + "' ORDER BY B.cPaperID";
                DataTable dtGroup = hintsDB.getDataSet(strGroupSQL).Tables[0];
                if (dtGroup.Rows.Count > 0)
                {   //抓BQL資料表中屬於題組的題目
                    for (int i = 0; i < dtGroup.Rows.Count; i++)
                    {
                        DataRow drGroup = dtCheckProblemType.NewRow();
                        drGroup["cPaperID"] = dtGroup.Rows[i]["cPaperID"];
                        drGroup["cQuestionTopic"] = dtGroup.Rows[i]["cQuestionTopic"];
                        drGroup["cQID"] = dtGroup.Rows[i]["cQID"];
                        drGroup["cVPAID"] = dtGroup.Rows[i]["cVPAID"];
                        drGroup["cTestAnswerType"] = dtGroup.Rows[i]["cTestAnswerType"];
                        drGroup["cCaseID"] = dtGroup.Rows[i]["cCaseID"];
                        drGroup["bIsOriginal"] = dtGroup.Rows[i]["bIsOriginal"];
                        dtCheckProblemType.Rows.Add(drGroup);
                    }
                }
            }
        }

        HF_RowCount.Value = Convert.ToString(dtCheckProblemType.Rows.Count);
        strAllGroup = "";
        int CurrentTotalScore = 0;
        for (int i = 0; i < dtCheckProblemType.Rows.Count; i++)
        {
            TableRow TopicTitleRow = new TableRow();
            TableCell TopicTitleCell = new TableCell();
            TopicTitleCell.ColumnSpan = 3;
            TopicTitleCell.Style["width"] = "100%";
            TopicTitleCell.Style["cursor"] = "pointer";
            Label img = new Label();                               //以Label實作HtmlInputImg
            img.ID = "imgPlusMinus-" + Convert.ToString(i + 1);
            img.Text = "<IMG src='/Hints/Summary/Images/minus.gif'>" + "&nbsp;&nbsp;";
            TopicTitleCell.Controls.Add(img);
            string strGetTopicName = "SELECT DISTINCT C.cQuestion, M.cQuestionGroupID, M.cQuestionGroupName FROM QuestionMode AS M, Conversation_Question AS C, BasicQuestionList AS B WHERE C.cQID=M.cQID AND M.cQID=B.cQID AND C.cQID='" + dtCheckProblemType.Rows[i]["cQID"].ToString() + "' AND '" + dtCheckProblemType.Rows[i]["cQuestionTopic"].ToString() + "' LIKE '%'+ M.cQuestionGroupID +'%'";
            DataTable dtTopicName = hintsDB.getDataSet(strGetTopicName).Tables[0];
            Label lbTopic = new Label();
            lbTopic.ID = "lbTopic_" + Convert.ToString(i + 1);
            if (dtCheckProblemType.Rows[i]["bIsOriginal"].ToString() == "False" && Request.QueryString["NewTopic"].ToString() == "No")
            {
                lbTopic.Text = dtTopicName.Rows[0]["cQuestionGroupName"].ToString() + "_題組題目" + CongroupCount.ToString();
                CongroupCount++;
            }
            else
            {
                lbTopic.Text = dtTopicName.Rows[0]["cQuestionGroupName"].ToString();
            }
            lbTopic.Font.Bold = true;
            lbTopic.Font.Size = 14;
            lbTopic.ForeColor = Color.DarkGreen;
            TopicTitleCell.Controls.Add(lbTopic);
            TopicTitleCell.Attributes["onclick"] = "OpenOrCloseSelection('" + Convert.ToString(i + 1) + "',1)";
            TopicTitleRow.Cells.Add(TopicTitleCell);
            tbSelectConversation.Rows.Add(TopicTitleRow);

            TableRow ConversationRow = new TableRow();
            ConversationRow.ID = "trSelection-" + Convert.ToString(i + 1) + "0";
            ConversationRow.Height = 50;           
            TableCell ConversationPreCell = new TableCell();
            ConversationPreCell.Width = 30;
            TableCell ConversationCell = new TableCell();
            RadioButton CheckBox1 = new RadioButton();
            CheckBox1.ID = "CheckBoxforConversation_" + Convert.ToString(i + 1);
            CheckBox1.InputAttributes.Add("class", "bigcheck");
            CheckBox1.GroupName = "Conversation";
            CheckBox1.CheckedChanged += new EventHandler(cbConstructVPAns_Checked);
            CheckBox1.AutoPostBack = true;          
            Label Lb_Conversation = new Label();
            Lb_Conversation.ID = "Conversation" + Convert.ToString(i + 1);
            Lb_Conversation.Text = dtTopicName.Rows[0]["cQuestion"].ToString();
            Lb_Conversation.Font.Size = 14;
            TableCell ConversationCell2 = new TableCell();
            ConversationCell2.Width = 50;
            HtmlInputButton btnBrowse = new HtmlInputButton();
            btnBrowse.ID = "btnBrowse_" + Convert.ToString(i + 1) + "_" + dtCheckProblemType.Rows[i]["cQID"].ToString();
            btnBrowse.Attributes.Add("style","width:60px");
            btnBrowse.Attributes.Add("style", "cursor:pointer;");
            btnBrowse.Value = this.MultiLanguage("btnBrowse");
            string strCareer = Request.QueryString["Career"];
            btnBrowse.Attributes.Add("onclick", "BrowseConversation('" + dtCheckProblemType.Rows[i]["cQID"].ToString() + "','" + dtTopicName.Rows[0]["cQuestionGroupID"].ToString() + "','" + strCareer + "','" + dtCheckProblemType.Rows[i]["cTestAnswerType"].ToString() + "');");
            ConversationPreCell.Controls.Add(CheckBox1);
            ConversationRow.Cells.Add(ConversationPreCell);
            ConversationCell.Controls.Add(Lb_Conversation);
            ConversationCell2.Controls.Add(btnBrowse);
            ConversationRow.Cells.Add(ConversationCell);
            ConversationRow.Cells.Add(ConversationCell2);
            tbSelectConversation.Rows.Add(ConversationRow);
            strAllGroup += dtTopicName.Rows[0]["cQuestionGroupID"].ToString() + "/";
            strAllQID += dtCheckProblemType.Rows[i]["cQID"].ToString() + "/";
            Session["AllQIDForCon"] = strAllQID;
            TopicTitleRow.Attributes.Add("class", "border");
            TopicTitleRow.Cells[0].Attributes.Add("class", "both");
            ConversationRow.Attributes.Add("class", "borderBottom");
            ConversationRow.Cells[0].Attributes.Add("class", "left");
            ConversationRow.Cells[ConversationRow.Cells.Count - 1].Attributes.Add("class", "right");
            TableRow trScore = new TableRow();
            trScore.ID = "trScore-" + Convert.ToString(i + 1) + "0";
            if (dtCheckProblemType.Rows[i]["bIsOriginal"].ToString() == "False" && Request.QueryString["NewTopic"].ToString() == "No")
            {
                TopicTitleRow.Attributes.Add("style", "background:#ffd0ff; vertical-align:middle; text-align: left;");
                ConversationRow.Attributes.Add("style", "background:#ffd0ff; vertical-align:middle; text-align: left;");
                trScore.Attributes.Add("style", "background:#ffd0ff; vertical-align:middle; text-align: right;");
            }
            else
            {
                if (i % 2 == 0)
                {
                    TopicTitleRow.Attributes.Add("style", "background:#E1C4C4; vertical-align:middle; text-align: left;");
                    ConversationRow.Attributes.Add("style", "background:#E1C4C4; vertical-align:middle; text-align: left;");
                    trScore.Attributes.Add("style", "background:#E1C4C4; vertical-align:middle; text-align: right;");
                }
                else
                {
                    TopicTitleRow.Attributes.Add("style", "background:#FFD306; vertical-align:middle; text-align: left;");
                    ConversationRow.Attributes.Add("style", "background:#FFD306; vertical-align:middle; text-align: left;");
                    trScore.Attributes.Add("style", "background:#FFD306; vertical-align:middle; text-align: right;");
                }
            }
            TableCell tdScore = new TableCell();
            tdScore.ColumnSpan = 3;
            Label lbScoreHints = new Label();
            lbScoreHints.ID = "lbScoreHints-" + dtCheckProblemType.Rows[i]["cQID"].ToString();
            lbScoreHints.ForeColor = Color.Blue;
            lbScoreHints.Visible = false;
            Label lbScoreTitle = new Label();
            lbScoreTitle.Text = this.MultiLanguage("lbScoreTitle");
            HtmlInputText EachScore = new HtmlInputText();
            EachScore.ID = "EachScore-" + dtCheckProblemType.Rows[i]["cQID"].ToString();
            EachScore.Style["width"] = "10%";
            string strGetSQL = "";
            if (Request.QueryString["NewTopic"].ToString() == "No")
            { 
                EachScore.Attributes.Add("onchange", "GetTextbox('" + EachScore.ID + "', '" + Request.QueryString["CaseID"].ToString() + "');");
                strGetSQL = "SELECT * FROM Paper_Content WHERE cQID='" + dtCheckProblemType.Rows[i]["cQID"].ToString() + "' AND cPaperID='" + Request.QueryString["CaseID"].ToString() + "'";
            }
            else
            {
                string strPreSQL = "SELECT * FROM BasicQuestionList WHERE cQuestionTopic LIKE '%" + Request.QueryString["GroupID"].ToString() + "%'";
                DataTable dtPre = hintsDB.getDataSet(strPreSQL).Tables[0];
                EachScore.Attributes.Add("onchange", "GetTextbox('" + EachScore.ID + "', '" + dtPre.Rows[0]["cPaperID"].ToString() + "');");
                strGetSQL = "SELECT * FROM Paper_Content WHERE cQID='" + dtCheckProblemType.Rows[i]["cQID"].ToString() + "' AND cPaperID='" + dtPre.Rows[0]["cPaperID"].ToString() + "'";
            }
            DataTable dtPaperContent = hintsDB.getDataSet(strGetSQL).Tables[0];
            if (dtPaperContent.Rows.Count > 0)
            {
                EachScore.Value = dtPaperContent.Rows[0]["cQuestionScore"].ToString();
                CurrentTotalScore += Convert.ToInt32(EachScore.Value);
            }
            else
            {
                string script = "document.getElementById('btnSetScoreServer').click();";
                ClientScript.RegisterStartupScript(this.GetType(), "alert_window", "<script>" + script + "</script>", false);
            }
            tdScore.Controls.Add(lbScoreHints);
            tdScore.Controls.Add(lbScoreTitle);
            tdScore.Controls.Add(EachScore);
            trScore.Cells.Add(tdScore);
            tbSelectConversation.Rows.Add(trScore);
            trScore.Attributes.Add("class", "border");
            trScore.Cells[0].Attributes.Add("class", "both");
        }
        LbCurrentScore.Text = CurrentTotalScore.ToString();
        for (int i = 0; i < dtCheckProblemType.Rows.Count; i++)
        {
            string strPreSQL = "SELECT * FROM StudentAnsType WHERE cVPAID = '" + dtCheckProblemType.Rows[i]["cVPAID"].ToString() + "'";
            DataTable dtPre = hintsDB.getDataSet(strPreSQL).Tables[0];           
            for (int j = 0; j < dtPre.Rows.Count; j++)
            {
                if (dtPre.Rows[j]["bIsCorrect"].ToString() == "False" && dtPre.Rows[j]["iAnswerType"].ToString() !="1")
                {
                    if (dtWrongWayQues.Rows.Count <= 0)
                    {
                        string strGroupSQL = "SELECT * FROM Paper_QuestionSelectionGroupItem WHERE cGroupID='" + (dtPre.Rows[j]["cVPAID"].ToString() + "_" + j) + "'";
                        dtWrongWayQues = hintsDB.getDataSet(strGroupSQL).Tables[0];
                    }
                    break;
                }
            }          
            foreach (DataRow dr in dtWrongWayQues.Rows)
            {
                Label lbTmp = (Label)this.FindControl("lbScoreHints-" + dr["cQID"].ToString());
                HtmlInputText InputTexttmp = (HtmlInputText)this.FindControl("EachScore-" + dr["cQID"].ToString());
                lbTmp.Visible = true;
                lbTmp.Text = this.MultiLanguage("lbScoreHints");
                InputTexttmp.Value = "0";
                InputTexttmp.Attributes.Add("readonly", "true");
            }
        }
            CongroupCount = 1;            
    }

    protected void ConstructVPAnswer(string strSelectedIndex)
    {
        tbSelectVPAnswer.Controls.Clear();
        string strColorStyle = "";
        bool checkRepeat = false;
        int RepeatCount = 0;
        colorCount = 0;
        DataTable dtVPAnswer = new DataTable();
        string strSQL = "SELECT * FROM VP_AnswerSet WHERE cGroupID='"+ HF_GroupID.Value.ToString() +"' ORDER BY cVPAID, cVPResponseType DESC";
        dtVPAnswer = hintsDB.getDataSet(strSQL).Tables[0];
            for (int i = 0; i < dtVPAnswer.Rows.Count; i++)
            {
                TableRow TopicTitleRow = new TableRow();
                TableCell TopicTitleCell = new TableCell();
                TopicTitleCell.ColumnSpan = 3;
                TopicTitleCell.HorizontalAlign = HorizontalAlign.Left;
                TopicTitleCell.Style["width"] = "100%";
                TopicTitleCell.Style["cursor"] = "pointer";
                Label img = new Label();                               //以Label實作HtmlInputImg
                img.ID = "imgVPAnsPlusMinus-" + Convert.ToString(i + 1);
                img.Text = "<IMG src='/Hints/Summary/Images/minus.gif'>" + "&nbsp;&nbsp;";
                TopicTitleCell.Controls.Add(img);
                Label lbTopic = new Label();
                lbTopic.ID = "lbVPAnsTopic_" + Convert.ToString(i + 1);
                lbTopic.Text = dtVPAnswer.Rows[i]["cVPAnsTitle"].ToString();
                lbTopic.Font.Bold = true;
                lbTopic.Font.Size = 14;
                lbTopic.ForeColor = Color.DarkGreen;
                TopicTitleCell.Controls.Add(lbTopic);
                TopicTitleRow.Cells.Add(TopicTitleCell);

                TableRow VPAnswerRow = new TableRow();
                VPAnswerRow.ID = "trVPAns-" + Convert.ToString(i + 1) + "0" + dtVPAnswer.Rows[i]["cVPAID"].ToString();
                VPAnswerRow.Height = 50;
                TableCell VPPreCell = new TableCell();
                VPPreCell.ID = "VPPreCell_" + dtVPAnswer.Rows[i]["cVPAID"].ToString();
                VPPreCell.Width = 30;
                TableCell VPAnswerCell = new TableCell();
                HtmlInputRadioButton CheckBox1 = new HtmlInputRadioButton();
                CheckBox1.ID = "CheckBoxforVPAnswer-" + dtVPAnswer.Rows[i]["cVPAID"].ToString();
                CheckBox1.Attributes.Add("class", "bigcheck");
                CheckBox1.Name = "VPAns";

                string strcheckSQL = "SELECT * FROM BasicQuestionList WHERE cCaseID='" + Request.QueryString["CaseID"].ToString() + "' AND cQuestionTopic LIKE '" + HF_GroupID.Value.ToString() + "%'"; // 找以HF_GroupID開頭的字串
                DataTable dtCheck = hintsDB.getDataSet(strcheckSQL).Tables[0];
                if (dtCheck.Rows.Count > 0)
                {
                    int NullCount = 0;
                    foreach (DataRow dr in dtCheck.Rows)
                    {
                        HtmlInputButton SelectedButton = (HtmlInputButton)tbSelectConversation.FindControl("btnBrowse_" + strSelectedIndex + "_" + dr["cQID"].ToString());
                        if (SelectedButton != null)
                            HF_SelectedQID.Value = dr["cQID"].ToString();
                        else
                            NullCount++;
                    }
                    if (NullCount == dtCheck.Rows.Count)
                        HF_SelectedQID.Value = "";
                }

                CheckBox1.Attributes.Add("onclick", "SaveSelected('" + HF_GroupID.Value.ToString() + "', '" + Request.QueryString["GroupID"].ToString() + "', '" + CheckBox1.ID + "');");
                Label Lb_VPAnswer = new Label();
                Lb_VPAnswer.ID = "VPAnswer" + Convert.ToString(i + 1);
                Lb_VPAnswer.Text = "(" + dtVPAnswer.Rows[i]["cVPResponseType"].ToString() + ") " + dtVPAnswer.Rows[i]["cVPResponseContent"].ToString();
                Lb_VPAnswer.Font.Size = 14;
                TableCell VPAnswerCell2 = new TableCell();
                VPAnswerCell2.Width = 50;
                HtmlInputButton btnBrowseVP = new HtmlInputButton();
                btnBrowseVP.ID = "btnBrowseVP_" + dtVPAnswer.Rows[i]["cVPAID"].ToString() + "_" + Convert.ToString(i + 1);
                btnBrowseVP.Value = "Browse";
                btnBrowseVP.Attributes.Add("style", "cursor:pointer;");
                btnBrowseVP.Attributes.Add("onclick", "BrowseVPAnswer('" + dtVPAnswer.Rows[i]["cProblemType"].ToString() + "','" + dtVPAnswer.Rows[i]["cVPResponseType"].ToString() + "','" + dtVPAnswer.Rows[i]["cVPAID"].ToString() + "');");

                //防止重複產生Checkbox，要控制一個VPAID僅一個Checkbox(最後要洗掉)  老詹 2015/01/11
                if (HF_checkbox.Value == CheckBox1.ID)
                {
                    /*VPPreCell.RowSpan = 1;
                    VPPreCell.Controls.Add(CheckBox1);
                    VPAnswerRow.Cells.Add(VPPreCell);
                    strColorStyle = ColorChange(strColorStyle);
                    VPPreCell.Attributes.Add("style", strColorStyle);
                    VPPreCell.Attributes.Add("class", "both");*/
                    checkRepeat = false;
                    RepeatCount++;
                }
                else
                {
                    RepeatCount = 0;
                }
                /*else
                {   //若ID相同，則將Checkbox的Rowspan變成VPAID之列數  老詹 2015/01/13
                    DataTable dtVPAIDRowCount = new DataTable();
                    string strCountSQL = "SELECT * FROM VP_AnswerSet WHERE cVPAID LIKE '" + dtVPAnswer.Rows[i]["cVPAID"].ToString() + "' ORDER BY cVPAID, cVPResponseType DESC";
                    dtVPAIDRowCount = hintsDB.getDataSet(strCountSQL).Tables[0];

                    TableCell ChangeCell = (TableCell)tbSelectVPAnswer.FindControl(HF_VPPreCell.Value.ToString());
                    ChangeCell.RowSpan = dtVPAIDRowCount.Rows.Count;
                    ChangeCell.Attributes.Add("class", "both");
                    ChangeCell = null;
                }*/
                string strCountSQL = "SELECT * FROM VP_AnswerSet WHERE cVPAID LIKE '" + dtVPAnswer.Rows[i]["cVPAID"].ToString() + "' ORDER BY cVPAID, cVPResponseType DESC";
                DataTable dtVPAIDRowCount = hintsDB.getDataSet(strCountSQL).Tables[0];
                VPPreCell.RowSpan = dtVPAIDRowCount.Rows.Count;
                TopicTitleCell.Attributes["onclick"] = "OpenOrCloseVPAns('" + Convert.ToString(i + 1) + "','" + dtVPAIDRowCount.Rows.Count + "', '" + dtVPAnswer.Rows[i]["cVPAID"].ToString() + "')";
                if ((VPPreCell.RowSpan <= 1) || ((VPPreCell.RowSpan > 1) && (checkRepeat == false) && (RepeatCount == 0)))
                {
                    tbSelectVPAnswer.Rows.Add(TopicTitleRow);
                    TopicTitleRow.Attributes.Add("class", "border");
                    TopicTitleRow.Cells[0].Attributes.Add("class", "both");
                    VPPreCell.Controls.Add(CheckBox1);
                    VPAnswerRow.Cells.Add(VPPreCell);
                    if(VPPreCell.RowSpan > 1)
                        checkRepeat = true;
                    strColorStyle = ColorChange(strColorStyle);
                    TopicTitleRow.Attributes.Add("style", strColorStyle);
                    VPPreCell.Attributes.Add("style", strColorStyle);
                    VPPreCell.Attributes.Add("class", "both");
                }

                HF_checkbox.Value = CheckBox1.ID;
                HF_VPPreCell.Value = VPPreCell.ID;

                VPAnswerCell.Controls.Add(Lb_VPAnswer);
                VPAnswerCell.Attributes.Add("style", strColorStyle);
                VPAnswerCell2.Controls.Add(btnBrowseVP);
                VPAnswerCell2.Attributes.Add("style", strColorStyle);
                VPAnswerRow.Cells.Add(VPAnswerCell);
                VPAnswerRow.Cells.Add(VPAnswerCell2);

                tbSelectVPAnswer.Rows.Add(VPAnswerRow);
                VPAnswerRow.Attributes.Add("class", "border");
                VPAnswerRow.Cells[VPAnswerRow.Cells.Count - 1].Attributes.Add("class", "right");
            }
        HF_checkbox.Value = "";
        HF_VPPreCell.Value = "";
    }

    protected string ColorChange(string strReturn)
    {
        if (colorCount % 2 == 0)
        { strReturn = "background:#E1C4C4; vertical-align:middle; text-align: left;"; }
        else
        { strReturn = "background:#FFD306; vertical-align:middle; text-align: left;"; }
        colorCount++;
        return strReturn;
    }

    protected void cbConstructVPAns_Checked(object sender, EventArgs e)
    {
        strAllGroup = strAllGroup.Remove(strAllGroup.LastIndexOf('/'));
        string[] arrAllGroup = strAllGroup.Split('/');
        string[] strIDArray = ((RadioButton)(sender)).ID.Split('_');
        string strSelectedIndex = strIDArray[1];
        
        for (int i = 0; i < Convert.ToInt32(HF_RowCount.Value); i++)
        {
            RadioButton checkclear = (RadioButton)tbSelectConversation.FindControl("CheckBoxforConversation_" + Convert.ToString(i + 1));
            if (checkclear.ID == "CheckBoxforConversation_" + strSelectedIndex)
            {
                checkclear.Checked = true;
                HF_GroupID.Value = arrAllGroup[i]; // 將選取的對話題之Group放入Hidden，供日後編輯VP時的參數  老詹 2015/04/07                                
                if (IsInit)
                {
                    if (Request.QueryString["SelectedIndex"] != null && Request.QueryString["SelectedIndex"].ToString() != "")
                    {
                        HtmlInputButton btnTmp = (HtmlInputButton)this.FindControl("btnBrowse_" + (i + 1).ToString() + "_" + Request.QueryString["SelectedIndex"].ToString());
                        if (btnTmp != null)
                            HF_SelectedIndex.Value = Request.QueryString["SelectedIndex"].ToString();
                    }
                }                       
                else
                {
                    string strGetNameSQL = "SELECT cNodeName FROM QuestionGroupTree WHERE cNodeID='" + HF_GroupID.Value.ToString() + "'";
                    DataTable dtGetName = hintsDB.getDataSet(strGetNameSQL).Tables[0];
                    Label LbTmp = (Label)this.FindControl("lbTopic_" + Convert.ToString(i+1));
                    if (LbTmp != null)
                    {
                        if (LbTmp.Text.IndexOf("題組") < 0 && LbTmp.Text == dtGetName.Rows[0]["cNodeName"].ToString())
                        {
                            string strTmpSQL = "SELECT cQID FROM BasicQuestionList WHERE cQuestionTopic LIKE '%/" + HF_GroupID.Value.ToString() + "%'";
                            DataTable dtTmp = hintsDB.getDataSet(strTmpSQL).Tables[0];
                            HF_SelectedIndex.Value = dtTmp.Rows[0]["cQID"].ToString();
                        }
                        else
                        {
                            string strTmpSQL = "SELECT cQID FROM BasicQuestionList WHERE cQuestionTopic LIKE '%" + HF_GroupID.Value.ToString() + "/%' ORDER BY cPaperID";
                            DataTable dtTmp = hintsDB.getDataSet(strTmpSQL).Tables[0];
                            string[] arrTmp = LbTmp.Text.Split('目');
                            if(dtTmp.Rows.Count>0)
                                HF_SelectedIndex.Value = dtTmp.Rows[Convert.ToInt32(arrTmp[1])-1]["cQID"].ToString();
                        }
                    }
                    Session["SelectedRb"] = (RadioButton)checkclear;
                }
            }
            else
                checkclear.Checked = false;
        }
        ConstructVPAnswer(strSelectedIndex);
        RecoverChecked(strSelectedIndex);
    }

    //檢查考卷有無零分
    protected void btnFinish3_onserverclick(object sender, EventArgs e)
    {
        //Update db.SetVoiceInquiryTreeByTeacher 的欄位(為了統計時判斷用)
        string strTmpAll = strAllQID;
        strTmpAll = strTmpAll.Remove(strTmpAll.LastIndexOf('/'));
        string strUpdateAllQIDSQL = "UPDATE SetVoiceInquiryTreeByTeacher SET cAllQIDForThisCase = '" + strTmpAll + "',cTestStoryTopic='" + Request.QueryString["GroupID"].ToString() + "' WHERE cCaseID = '" + Request.QueryString["CaseID"].ToString() + "'";
        hintsDB.ExecuteNonQuery(strUpdateAllQIDSQL);
        string script = "", strGetSQL = "";
        if (Request.QueryString["NewTopic"].ToString() == "No")
        {
            strGetSQL = "SELECT * FROM Paper_Content WHERE cPaperID ='"+ Request.QueryString["CaseID"].ToString() +"'";
        }
        else
        {
            string strPreSQL = "SELECT * FROM BasicQuestionList WHERE cQuestionTopic LIKE '%" + Request.QueryString["GroupID"].ToString() + "%'";
            DataTable dtPre = hintsDB.getDataSet(strPreSQL).Tables[0];
            strGetSQL = "SELECT * FROM Paper_Content WHERE cPaperID='" + dtPre.Rows[0]["cPaperID"].ToString() + "'";
        }
        DataTable dtPaperQuestion = hintsDB.getDataSet(strGetSQL).Tables[0];
        foreach (DataRow dr in dtWrongWayQues.Rows)
        {
            DataRow[] drTemp = dtPaperQuestion.Select("cQID='" + dr["cQID"].ToString() + "'");
            dtPaperQuestion.Rows.Remove(drTemp[0]);
        }
        //考卷題目數目
        int intQuestionNum = dtPaperQuestion.Rows.Count;
        if (intQuestionNum > 0)
        {
            //考卷總分
            int intTotalScore = int.Parse(textTotalScore.Value);
            //考卷題目總分
            int intQuestionTotalScore = 0;
            //沒有設定VPAnswer題數
            int NoVPAnswer = 0;
            //紀錄零分題目
            List<string> ZeroPointQuestions = new List<string>();
            foreach (DataRow drPaperQuestion in dtPaperQuestion.Rows)
            {
                if (strAllQID.IndexOf(drPaperQuestion["cQID"].ToString()) >= 0)
                {
                    int QuestionPoint = int.Parse(drPaperQuestion["cQuestionScore"].ToString());
                    if (QuestionPoint > 0)
                    {
                        intQuestionTotalScore += QuestionPoint;
                    }
                    else
                    {
                        //紀錄考卷中零分題目
                        ZeroPointQuestions.Add(drPaperQuestion["cQID"].ToString());
                    }
                }
            }
            string[] arrAllConQID = strTmpAll.Split('/');
            string strSubSQL = "";
            for (int i = 0; i < arrAllConQID.Length; i++)
            {
                strSubSQL += " cQID LIKE '%" + arrAllConQID[i] + "%' OR";
            }
            strSubSQL = strSubSQL.Remove(strSubSQL.LastIndexOf(' '));
            string strSQL = "SELECT cVPAID FROM BasicQuestionList WHERE (" + strSubSQL + ")";
            DataTable dtCheckNoVPAns = hintsDB.getDataSet(strSQL).Tables[0];
            foreach (DataRow dr in dtCheckNoVPAns.Rows)
            {
                if (dr["cVPAID"].ToString() == "")
                    NoVPAnswer++;
            }
            if (NoVPAnswer > 0)
            {
                script = "alert('您仍有"+ NoVPAnswer +"題對話題未設定對應的虛擬人回答!');";
            }
            else
            {
                //如果題目總分大於考卷總分
                if (intQuestionTotalScore > intTotalScore)
                {
                    script = "alert('目前考卷題目分數總和大於考卷總分，請修改考卷題目分數!');";
                }
                else//小於等於
                {
                    //如果題目分數等於考卷總分
                    if (intQuestionTotalScore == intTotalScore)
                    {
                        //存在有無零分題目
                        if (ZeroPointQuestions.Count > 0)
                            script = "alert('目前考卷有設定分數為0分題目，請修改考卷題目分數!');";
                        else
                        {
                            if (Request.QueryString["NewTopic"].ToString() == "No")
                            { script = "document.getElementById('btnSaveNext').click();"; }
                            else
                            { script = "window.close();"; }
                        }
                    }
                    else//小於
                    {
                        if (ZeroPointQuestions.Count > 0)
                        {
                            if (Request.QueryString["NewTopic"].ToString() == "No")
                            { script = "if(confirm('是否平均分配考卷剩餘分數於0分題目並完成編輯考卷?')){document.getElementById('btnSetScoreServer').click();document.getElementById('btnSaveNext').click();}"; }
                            else
                            { script = "if(confirm('是否平均分配考卷剩餘分數於0分題目並完成編輯考卷?')){document.getElementById('btnSetScoreServer').click();window.close();}"; }
                        }
                        else
                            script = "alert('目前考卷題目分數總和小於考卷總分，請修改考卷題目分數');";
                    }
                }
            }
        }
        else
        {
            script = "alert('目前考卷無題目，請重新編輯考卷!');";
        }
        ClientScript.RegisterStartupScript(this.GetType(), "alert_window", "<script>" + script + "</script>", false);
        if (Session["SelectedRb"] != null)
        {
            RadioButton rbTmp = (RadioButton)Session["SelectedRb"];
            cbConstructVPAns_Checked(rbTmp, e);
        }
    }

    private void btnSaveNext_ServerClick(object sender, EventArgs e)
    {
        Session["SelectedRb"] = null;
        Response.Redirect("../Interrogation/ConversationEnquiry.aspx?bDisplayBQL=True&GroupID=" + Request.QueryString["GroupID"].ToString());
    }
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Session["SelectedRb"] = null;
        string strCareer = Request.QueryString["Career"];
        Response.Redirect("EditTestPaper_BQL.aspx?GroupID=" + Request.QueryString["GroupID"].ToString() + "&Career=" + strCareer + "&CaseID=" + Request.QueryString["CaseID"].ToString() + "&SelectedIndex=" + HF_SelectedIndex.Value.ToString());
    }

    protected void ddlProblemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProblemType.SelectedItem.Text == "Select a Problem Type")
        { flag = false; }
        else
        { flag = true; }
        ConstructConversation();
        HF_GroupID.Value = "";
    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void ConfirmSave(string VPAID, string strGroupID, string strMainTopic, string strCaseID, string strSelectedID)
    {
        string strNewSaveVPAID = VPAID.TrimEnd(',');

        if (strSelectedID != "")
        {
            string strUpdateSQL = "UPDATE BasicQuestionList SET cVPAID = '" + strNewSaveVPAID + "' WHERE cCaseID = '" + strCaseID + "' AND cQuestionTopic = '" + (strGroupID + "/" + strSelectedID) + "'";
            hintsDB.ExecuteNonQuery(strUpdateSQL);
        }
        else
        {
            string strUpdateSQL = "UPDATE BasicQuestionList SET cVPAID = '" + strNewSaveVPAID + "' WHERE cCaseID = '" + strCaseID + "' AND cQuestionTopic = '" + (strMainTopic + "/" + strGroupID) + "'";
            hintsDB.ExecuteNonQuery(strUpdateSQL);
        }       
    }

    private void btnConstructCon_ServerClick(object sender, EventArgs e)
    {
        ConstructConversation();
        if (Session["SelectedRb"] != null)
        {
            RadioButton rbTmp = (RadioButton)Session["SelectedRb"];
            cbConstructVPAns_Checked(rbTmp, e);
        }
    }

    private void btnSetScoreServer_ServerClick(object sender, EventArgs e)
    {
        int bIsChagedTestPaperTopic = 0;
        int QuestionNum = 0;
        string strNotZeroQuestion = "";
        DataTable dtPaperContent = new DataTable();
        DataTable dtPre = new DataTable();
        if (Request.QueryString["NewTopic"].ToString() == "No")
        {
            string strGetSQL = "SELECT * FROM Paper_Content WHERE cPaperID ='" + Request.QueryString["CaseID"].ToString() + "' ORDER BY sSeq";
            dtPaperContent = hintsDB.getDataSet(strGetSQL).Tables[0];
        }
        else
        {
            string strPreSQL = "SELECT * FROM BasicQuestionList WHERE cQuestionTopic LIKE '%" + Request.QueryString["GroupID"].ToString() + "%'";
            dtPre = hintsDB.getDataSet(strPreSQL).Tables[0];
            string strGetSQL = "SELECT * FROM Paper_Content WHERE cPaperID='" + dtPre.Rows[0]["cPaperID"].ToString() + "' ORDER BY sSeq";
            dtPaperContent = hintsDB.getDataSet(strGetSQL).Tables[0];
        }
        foreach (DataRow dr in dtPaperContent.Rows)
        {
            if (strAllQID.IndexOf(dr["cQID"].ToString()) >= 0)
                bIsChagedTestPaperTopic ++;
        }
        string strTmpAll = strAllQID;
        strTmpAll = strTmpAll.Remove(strTmpAll.LastIndexOf('/'));
        string[] strTmpArr = strTmpAll.Split('/');
        if ((dtPaperContent.Rows.Count <= 0) || (bIsChagedTestPaperTopic != strTmpArr.Length))
        {
            if (dtPaperContent.Rows.Count > 0)
            {
                for (int j = 0; j < strTmpArr.Length; j++)
                {
                    string strDeleteSQL = "DELETE FROM Paper_Content WHERE cQID = '" + strTmpArr[j] + "' AND cPaperID='" + Request.QueryString["CaseID"].ToString() + "'";
                    hintsDB.ExecuteNonQuery(strDeleteSQL);
                }
            }
            foreach (DataRow dr in dtCheckProblemType.Rows)
            {
                string strGetTmpSQL = "SELECT cQuestion FROM Conversation_Question WHERE cQID='" + dr["cQID"].ToString() + "'";
                DataTable dtQuestion = hintsDB.getDataSet(strGetTmpSQL).Tables[0];
                string strQuestion = dtQuestion.Rows[0]["cQuestion"].ToString();
                if (Request.QueryString["NewTopic"].ToString() == "No")
                {
                    string strInsertSQL = "INSERT INTO Paper_Content (cPaperID,cQID,sStandardScore,cQuestionType,cQuestionMode,cQuestion,sSeq) VALUES ('" + Request.QueryString["CaseID"].ToString() + "','" + dr["cQID"].ToString() + "','0','4','General',@cQuestion,'" + (dtCheckProblemType.Rows.IndexOf(dr) + 1) + "')";
                    object[] pList = { strQuestion };
                    hintsDB.ExecuteNonQuery(strInsertSQL, pList);
                }
                else
                {
                    string strInsertSQL = "INSERT INTO Paper_Content (cPaperID,cQID,sStandardScore,cQuestionType,cQuestionMode,cQuestion,sSeq) VALUES ('" + dtPre.Rows[0]["cPaperID"].ToString() + "','" + dr["cQID"].ToString() + "','0','4','General',@cQuestion,'" + (dtCheckProblemType.Rows.IndexOf(dr) + 1) + "')";
                    hintsDB.ExecuteNonQuery(strInsertSQL);
                    object[] pList = { strQuestion };
                    hintsDB.ExecuteNonQuery(strInsertSQL, pList);
                }
            }
        }
        else
        {
            string strAllWrong = "";
            foreach (DataRow drWrong in dtWrongWayQues.Rows)
            {
                strAllWrong += drWrong["cQID"].ToString();
            }
            foreach (DataRow dr in dtPaperContent.Rows)
            {
                HtmlInputText InputTexttmp = (HtmlInputText)this.FindControl("EachScore-" + dr["cQID"].ToString());
                if (InputTexttmp != null)
                {
                    strNotZeroQuestion += dr["cQID"].ToString() + ",";
                    if (strAllWrong.IndexOf(dr["cQID"].ToString()) < 0)
                        QuestionNum++;
                }
            }
            if (strNotZeroQuestion != "")
                strNotZeroQuestion = strNotZeroQuestion.Remove(strNotZeroQuestion.LastIndexOf(','));
            string[] strArrQuestion = strNotZeroQuestion.Split(',');
            int TotalScore = Convert.ToInt32(textTotalScore.Value);
            int AvgScore = TotalScore / QuestionNum;
            int LastScore = TotalScore - (AvgScore*QuestionNum);
            for (int i = 0; i < QuestionNum; i++)
            {
                if (Request.QueryString["NewTopic"].ToString() == "No")
                {
                    if (LastScore > 0 && i == (QuestionNum - 1))
                    {
                        string strUpdateSQL = "UPDATE Paper_Content SET cQuestionScore = '" + (AvgScore + LastScore) + "' WHERE cQID = '" + strArrQuestion[i] + "' AND cPaperID='" + Request.QueryString["CaseID"].ToString() + "'";
                        hintsDB.ExecuteNonQuery(strUpdateSQL);
                    }
                    else
                    {
                        string strUpdateSQL = "UPDATE Paper_Content SET cQuestionScore = '" + AvgScore + "' WHERE cQID = '" + strArrQuestion[i] + "' AND cPaperID='" + Request.QueryString["CaseID"].ToString() + "'";
                        hintsDB.ExecuteNonQuery(strUpdateSQL);
                    }
                }
                else
                {
                    if (LastScore > 0 && i == (QuestionNum - 1))
                    {
                        string strUpdateSQL = "UPDATE Paper_Content SET cQuestionScore = '" + (AvgScore + LastScore) + "' WHERE cQID = '" + strArrQuestion[i] + "' AND cPaperID='" + dtPre.Rows[0]["cPaperID"].ToString() + "'";
                        hintsDB.ExecuteNonQuery(strUpdateSQL);
                    }
                    else
                    {
                        string strUpdateSQL = "UPDATE Paper_Content SET cQuestionScore = '" + AvgScore + "' WHERE cQID = '" + strArrQuestion[i] + "' AND cPaperID='" + dtPre.Rows[0]["cPaperID"].ToString() + "'";
                        hintsDB.ExecuteNonQuery(strUpdateSQL);
                    }
                }

            }
            //Recover
            DataTable dtNewScore = new DataTable();
            if (Request.QueryString["NewTopic"].ToString() == "No")
            {
                string strGetSQL = "SELECT * FROM Paper_Content WHERE cPaperID ='" + Request.QueryString["CaseID"].ToString() + "'";
                dtNewScore = hintsDB.getDataSet(strGetSQL).Tables[0];
            }
            else
            {
                string strGetSQL = "SELECT * FROM Paper_Content WHERE cPaperID='" + dtPre.Rows[0]["cPaperID"].ToString() + "'";
                dtNewScore = hintsDB.getDataSet(strGetSQL).Tables[0];
            }
            int iTmpTotalScore = 0;
            foreach (DataRow dr in dtNewScore.Rows)
            {
                HtmlInputText InputTexttmp = (HtmlInputText)this.FindControl("EachScore-" + dr["cQID"].ToString());
                if (InputTexttmp != null)
                {
                    InputTexttmp.Value = dr["cQuestionScore"].ToString();
                    iTmpTotalScore += Convert.ToInt32(dr["cQuestionScore"].ToString());
                }
            }
            LbCurrentScore.Text = iTmpTotalScore.ToString();
            if(Session["SelectedRb"] != null)
            {
                RadioButton rbTmp = (RadioButton)Session["SelectedRb"];
                cbConstructVPAns_Checked(rbTmp,e);
            }
        }
    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void UpdateScore(string strQID, string strNewScore, string strCaseID)
    {
        string strUpdateSQL = "UPDATE Paper_Content SET cQuestionScore = '" + strNewScore + "' WHERE cQID = '" + strQID + "' AND cPaperID='" + strCaseID + "'";
        hintsDB.ExecuteNonQuery(strUpdateSQL);
    }

    #region 多國語言所有Function  老詹 2015/04/14
    protected string MultiLanguage(string text)
    {
        string ret_text = MultiLanguage(text, ddl_MutiLanguage.SelectedItem.Text.ToString());
        ret_text = (ret_text == "" || ret_text == null) ? text : ret_text;
        return ret_text;
    }

    public static string MultiLanguage(string text, string culture)
    {
        string ret = "";

        System.Resources.ResourceManager res = Resources.BQL.Resource.ResourceManager;
        //setLocalName(language);
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo(culture);//zh-TW

        try
        {
            ret = res.GetString(text, ci);
        }
        catch
        {
            ret = text;
        }
        return ret;
    }
    protected void ddl_MutiLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ChangeLanguage();
        if (Session["SelectedRb"] != null)
        {
            RadioButton rbTmp = (RadioButton)Session["SelectedRb"];
            cbConstructVPAns_Checked(rbTmp, e);
        }
    }
    protected void ChangeLanguage()
    {
        if (ddl_MutiLanguage.SelectedItem.Text == "en-US")
        {
            Lb_Topic.Text = this.MultiLanguage("PaperTitle");
            Lb_TopicZh.Text = "";
        }
        else
        {
            Lb_TopicZh.Text = this.MultiLanguage("PaperTitle");
            Lb_Topic.Text = "";
        }
        Lb_ConQues.Text = this.MultiLanguage("QuesTitle");
        Lb_VPAns.Text = this.MultiLanguage("VPAnsTitle");
        btnEditVPAns.Value = this.MultiLanguage("BtnEditVPAns");
        Lb_Conversation.Text = this.MultiLanguage("Conversation");
        Lb_VPAnswer.Text = this.MultiLanguage("VPAnswer");
        BtnBack.Text = this.MultiLanguage("BtnBack");
        BtnNext.Value = this.MultiLanguage("BtnNext");
        BtnClose.Value = this.MultiLanguage("BtnClose");
        btnAutoSetScore.Value = this.MultiLanguage("btnAutoSetScore");
        lbTotalScore.Text = this.MultiLanguage("lbTotalScore");
        LbCurrentScoreTitle.Text = this.MultiLanguage("LbCurrentScoreTitle");
    }
    #endregion
}