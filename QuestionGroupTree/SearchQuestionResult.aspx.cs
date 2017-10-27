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
using Hints.DB;
using Hints.Learning.Question;

public partial class AuthoringTool_CaseEditor_Paper_QuestionGroupTree_SearchQuestionResult : BasicForm_BasicForm05
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["OpenGroupID"] != null)
            hfOpenGroupID.Value = Request.QueryString["OpenGroupID"].ToString();
        
        //關鍵字搜尋結果頁面
        if (Request.QueryString["SearchType"] == "1")
        {
            if (Request.QueryString["SearchKeyword"] != null)
            {
                gvSearchResult.DataSource = getQuestionKeywordResult(Request.QueryString["SearchKeyword"].ToString());
                gvSearchResult.DataBind();
            }
        }
        //特徵值搜尋結果頁面
        else if (Request.QueryString["SearchType"] == "2")
        {
            //利用session傳遞參數到此頁面
            gvSearchResult.DataSource = getQuestionFeatureResult((DataTable)Session["dtSelectedFeatureItemResult"]);
            gvSearchResult.DataBind();
        }
    }
    #region 關鍵字搜尋結果頁面區
    //取得問答題題目或答案或Keyword 以及選擇題、對話題題目 有符合search keyword
    private DataTable getQuestionKeywordResult(string strKeyword)
    {
        DataTable dtSearchResult = new DataTable();
        dtSearchResult.Columns.Add("cQID");
        dtSearchResult.Columns.Add("cQuestion");
        //dtSearchResult.Columns.Add("cAnswer");
        dtSearchResult.Columns.Add("cKeyword");
        dtSearchResult.Columns.Add("cQuestionType");
        dtSearchResult.Columns.Add("cQuestionGroupID");
        dtSearchResult.Columns.Add("cQuestionGroupName");

        string ZipRegex = @"<\s*(?<Tag>/*\s*[^<^>^\s]+)(?<Para>(\s*[^<^>^\s]+)*)\s*>";
        string strQuestion = "";
        //string strAnswer = "";
        string strNodeName = "";

        #region 暫時註解非對話題之外的搜尋結果  老詹 2015/09/06
        #region 取得問答題資料表Paper_TextQuestion資料
        /*DataTable dtPaper_TextQuestion = Paper_TextQuestion_SELECT_Keyword(strKeyword);
        foreach (DataRow drPaper_TextQuestion in dtPaper_TextQuestion.Rows)
        {
            DataTable dtQuestionMode = QuestionMode_SELECT(drPaper_TextQuestion["cQID"].ToString());
            if (dtQuestionMode.Rows.Count > 0)
            {
                strNodeName = PaperSystem.DataReceiver.getQuestionGroupNameByQuestionGroupID(dtQuestionMode.Rows[0]["cQuestionGroupID"].ToString());
                if (dtQuestionMode.Rows.Count > 0 && dtQuestionMode.Rows[0]["cQuestionGroupID"].ToString() != "" && strNodeName != "")
                {
                    DataRow row = dtSearchResult.NewRow();
                    strQuestion = System.Text.RegularExpressions.Regex.Replace(drPaper_TextQuestion["cQuestion"].ToString(), ZipRegex, "").Replace("&nbsp", "").Replace(";", "");
                    //strAnswer = System.Text.RegularExpressions.Regex.Replace(drPaper_TextQuestion["cAnswer"].ToString(), ZipRegex, "").Replace("&nbsp", "").Replace(";", "");

                    row["cQID"] = drPaper_TextQuestion["cQID"].ToString();
                    row["cQuestion"] = strQuestion.Replace(strKeyword, "<span class='span_keyword'>" + strKeyword + "</span>");

                    //if (strAnswer != "")
                    //    row["cAnswer"] = strAnswer.Replace(strKeyword, "<span class='span_keyword'>" + strKeyword + "</span>");
                    //else
                    //    row["cAnswer"] = "N/A";

                    row["cKeyword"] = DataReceiver.getTextQuestionKeyword(drPaper_TextQuestion["cQID"].ToString()).Replace(strKeyword, "<span class='span_keyword'>" + strKeyword + "</span>"); ;
                    row["cQuestionType"] = "問答題";
                    row["cQuestionGroupID"] = dtQuestionMode.Rows[0]["cQuestionGroupID"].ToString();
                    row["cQuestionGroupName"] = strNodeName;
                    dtSearchResult.Rows.Add(row);
                }
            }
        }*/
        #endregion

        #region 取得問答題關鍵字資料表Paper_TextQuestionKeyword資料
        /*DataTable dtPaper_TextQuestionKeyword = Paper_TextQuestionKeyword_SELECT_Keyword(strKeyword);
        foreach (DataRow drPaper_TextQuestionKeyword in dtPaper_TextQuestionKeyword.Rows)
        {
            //檢查Paper_TextQuestionKeyword的資料表的問題是否已經在dtSearchResult存在過
            if (dtSearchResult.Select("cQID = '" + drPaper_TextQuestionKeyword["cQID"].ToString() + "'").Length == 0)
            {
                DataTable dtQuestionMode = QuestionMode_SELECT(drPaper_TextQuestionKeyword["cQID"].ToString());
                strNodeName = PaperSystem.DataReceiver.getQuestionGroupNameByQuestionGroupID(dtQuestionMode.Rows[0]["cQuestionGroupID"].ToString());
                if (dtQuestionMode.Rows.Count > 0 && dtQuestionMode.Rows[0]["cQuestionGroupID"].ToString() != "" && strNodeName != "")
                {
                    DataRow row = dtSearchResult.NewRow();
                    strQuestion = DataReceiver.getTextQuestionContentByQID(drPaper_TextQuestionKeyword["cQID"].ToString());
                    strQuestion = System.Text.RegularExpressions.Regex.Replace(strQuestion, ZipRegex, "").Replace("&nbsp", "").Replace(";", "");

                    //strAnswer = DataReceiver.getTextQuestionAnswerByQID(drPaper_TextQuestionKeyword["cQID"].ToString());
                    //strAnswer = System.Text.RegularExpressions.Regex.Replace(strAnswer, ZipRegex, "").Replace("&nbsp", "").Replace(";", "");

                    row["cQuestion"] = strQuestion.Replace(strKeyword, "<span class='span_keyword'>" + strKeyword + "</span>");

                    //if (strAnswer != "")
                    //    row["cAnswer"] = strAnswer.Replace(strKeyword, "<span class='span_keyword'>" + strKeyword + "</span>");
                    //else
                    //    row["cAnswer"] = "N/A";

                    row["cKeyword"] = drPaper_TextQuestionKeyword["cKeyword"].ToString().Replace(strKeyword, "<span class='span_keyword'>" + strKeyword + "</span>"); ;
                    row["cQuestionType"] = "問答題";
                    row["cQuestionGroupID"] = dtQuestionMode.Rows[0]["cQuestionGroupID"].ToString();
                    row["cQuestionGroupName"] = strNodeName;
                    dtSearchResult.Rows.Add(row);
                }
            }

        }*/
        #endregion

        #region 取得選擇題資料表QuestionIndex資料
        /*DataTable dtQuestionIndex = QuestionIndex_SELECT_Keyword(strKeyword, "1");
        foreach (DataRow drQuestionIndex in dtQuestionIndex.Rows)
        {
            DataTable dtQuestionMode = QuestionMode_SELECT(drQuestionIndex["cQID"].ToString());
            strNodeName = PaperSystem.DataReceiver.getQuestionGroupNameByQuestionGroupID(dtQuestionMode.Rows[0]["cQuestionGroupID"].ToString());
            if (dtQuestionMode.Rows.Count > 0 && dtQuestionMode.Rows[0]["cQuestionGroupID"].ToString() != "" && strNodeName != "")
            {
                DataRow row = dtSearchResult.NewRow();
                strQuestion = System.Text.RegularExpressions.Regex.Replace(dtQuestionMode.Rows[0]["cQuestion"].ToString(), ZipRegex, "").Replace("&nbsp", "").Replace(";", "");
                //strAnswer = System.Text.RegularExpressions.Regex.Replace(drPaper_TextQuestion["cAnswer"].ToString(), ZipRegex, "").Replace("&nbsp", "").Replace(";", "");
                row["cQID"] = drQuestionIndex["cQID"].ToString();
                row["cQuestion"] = strQuestion.Replace(strKeyword, "<span class='span_keyword'>" + strKeyword + "</span>");
                //row["cAnswer"] = "N/A";
                row["cKeyword"] = "N/A";
                row["cQuestionType"] = "選擇題";
                row["cQuestionGroupID"] = dtQuestionMode.Rows[0]["cQuestionGroupID"].ToString();
                row["cQuestionGroupName"] = strNodeName;
                dtSearchResult.Rows.Add(row);
            }
        }*/
        #endregion
        #endregion

        #region 取得對話題資料表Conversation_Question資料
        DataTable dtConversation_Question = Conversation_Question_SELECT_Keyword(strKeyword);
        foreach (DataRow drConversation_Question in dtConversation_Question.Rows)
        {
            DataTable dtQuestionMode = QuestionMode_SELECT(drConversation_Question["cQID"].ToString());
            if (dtQuestionMode.Rows.Count > 0)
            {
                strNodeName = PaperSystem.DataReceiver.getQuestionGroupNameByQuestionGroupID(dtQuestionMode.Rows[0]["cQuestionGroupID"].ToString());
                if (dtQuestionMode.Rows.Count > 0 && dtQuestionMode.Rows[0]["cQuestionGroupID"].ToString() != "" && strNodeName != "")
                {
                    DataRow row = dtSearchResult.NewRow();
                    strQuestion = System.Text.RegularExpressions.Regex.Replace(drConversation_Question["cQuestion"].ToString(), ZipRegex, "").Replace("&nbsp", "").Replace(";", "");
                    //strAnswer = System.Text.RegularExpressions.Regex.Replace(drConversation_Question["cAnswer"].ToString(), ZipRegex, "").Replace("&nbsp", "").Replace(";", "");

                    row["cQID"] = drConversation_Question["cQID"].ToString();
                    row["cQuestion"] = strQuestion.Replace(strKeyword, "<span class='span_keyword'>" + strKeyword + "</span>");

                    row["cKeyword"] = DataReceiver.getConversation_Question_Keyword(drConversation_Question["cQID"].ToString()).Replace(strKeyword, "<span class='span_keyword'>" + strKeyword + "</span>"); ;
                    row["cQuestionType"] = "對話題";
                    row["cQuestionGroupID"] = dtQuestionMode.Rows[0]["cQuestionGroupID"].ToString();
                    row["cQuestionGroupName"] = strNodeName;
                    dtSearchResult.Rows.Add(row);
                }
            }
        }
        #endregion

        return dtSearchResult;

    }

    //取得對話題 問題有符合Keyword的問題
    private DataTable Conversation_Question_SELECT_Keyword(string strKeyword)
    {
        DataTable dtConversation_Question = new DataTable();
        clsHintsDB HintsDB = new clsHintsDB();
        string strSQL_Conversation_Question = "SELECT * FROM Conversation_Question " +
         "WHERE cQuestion LIKE '%" + strKeyword + "%' ";
        dtConversation_Question = HintsDB.getDataSet(strSQL_Conversation_Question).Tables[0];
        return dtConversation_Question;
    }

    //取得問答題 問題或答案有符合Keyword的問題
    private DataTable Paper_TextQuestion_SELECT_Keyword(string strKeyword)
    {
        DataTable dtPaper_TextQuestion = new DataTable();
        clsHintsDB HintsDB = new clsHintsDB();
        //string strSQL_Paper_TextQuestion = "SELECT * FROM Paper_TextQuestion " +
        //    "WHERE cQuestion LIKE '%" + strKeyword + "%' OR cAnswer LIKE '%" + strKeyword + "%'";
        string strSQL_Paper_TextQuestion = "SELECT * FROM Paper_TextQuestion " +
         "WHERE cQuestion LIKE '%" + strKeyword + "%' ";
        dtPaper_TextQuestion = HintsDB.getDataSet(strSQL_Paper_TextQuestion).Tables[0];
        return dtPaper_TextQuestion;
    }

    //取得問答題 KEYWORD有符合Keyword的問題
    private DataTable Paper_TextQuestionKeyword_SELECT_Keyword(string strKeyword)
    {
        DataTable dtPaper_TextQuestionKeyword = new DataTable();
        clsHintsDB HintsDB = new clsHintsDB();
        string strSQL_Paper_TextQuestionKeyword = "SELECT * FROM Paper_TextQuestionKeyword " +
            "WHERE cKeyword LIKE '%" + strKeyword + "%'";
        dtPaper_TextQuestionKeyword = HintsDB.getDataSet(strSQL_Paper_TextQuestionKeyword).Tables[0];
        return dtPaper_TextQuestionKeyword;
    }

    //取得選擇題 問題有符合Keyword的問題
    private DataTable QuestionIndex_SELECT_Keyword(string strKeyword, string strQuestionType)
    {
        DataTable dtQuestionIndex = new DataTable();
        clsHintsDB HintsDB = new clsHintsDB();
        string strSQL_QuestionIndex = "SELECT * FROM dbo.QuestionIndex INNER JOIN " +
            "dbo.QuestionMode ON dbo.QuestionIndex.cQID = dbo.QuestionMode.cQID " +
            "WHERE cQuestion LIKE '%" + strKeyword + "%' AND cQuestionType = '" + strQuestionType + "'";
        dtQuestionIndex = HintsDB.getDataSet(strSQL_QuestionIndex).Tables[0];
        return dtQuestionIndex;
    }

    //取得問題的GroupID與GroupName
    private DataTable QuestionMode_SELECT(string strQID)
    {
        DataTable dtQuestionMode = new DataTable();
        clsHintsDB HintsDB = new clsHintsDB();
        string strSQL_QuestionMode = "SELECT * FROM QuestionMode A,QuestionIndex B" +
            " WHERE (A.cQID=B.cQID) AND (A.cQID = '" + strQID + "')";
        dtQuestionMode = HintsDB.getDataSet(strSQL_QuestionMode).Tables[0];
        return dtQuestionMode;
    }
    //取得問答題的AID     老詹  2013/08/15
    private DataTable TextQuestion_Answer(string strQID)
    {
        DataTable dtQuestionAnswer_Answer = new DataTable();
        clsHintsDB HintsDB = new clsHintsDB();
        string strSQL_QuestionMode = "SELECT * FROM QuestionMode A,QuestionIndex B,QuestionAnswer_Answer C" +
            " WHERE (A.cQID=B.cQID) AND (B.cQID=C.cQID) AND (A.cQID = '" + strQID + "')";
        dtQuestionAnswer_Answer = HintsDB.getDataSet(strSQL_QuestionMode).Tables[0];
        return dtQuestionAnswer_Answer;
    }

    protected void btEdit_Click(object sender, EventArgs e)
    {
        //修改題目所需的Session
        Session["bModify"] = true;

        string strQID = ((Button)sender).CommandArgument;
        string strAID = "";
        string strGroupID = "";
        string strQuestionType = "";
        DataTable dtQuestionMode = QuestionMode_SELECT(strQID);
        if (dtQuestionMode.Rows.Count > 0 && dtQuestionMode.Rows[0]["cQuestionGroupID"].ToString() != "")
        {
            strGroupID = dtQuestionMode.Rows[0]["cQuestionGroupID"].ToString();
            strQuestionType = dtQuestionMode.Rows[0]["cQuestionType"].ToString();
            if (strQuestionType == "4" && Session["tmpCareer"] != null)
                hfCareer.Value = Session["tmpCareer"].ToString();
            else
                hfCareer.Value = null;
        }
        DataTable dtQuestionAnswer_Answer = TextQuestion_Answer(strQID);
        if (dtQuestionAnswer_Answer.Rows.Count > 0)
        {
            strAID = dtQuestionAnswer_Answer.Rows[0]["cAID"].ToString();
        }
        RegisterStartupScript("", "<script language='javascript'>EditQuestion('" + strQID + "', '" + strAID + "', '" + strGroupID + "', '" + strQuestionType + "');</script>");
    }

    //分頁的功能
    protected void gvSearchResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSearchResult.PageIndex = e.NewPageIndex;
        gvSearchResult.DataBind();
    }
    #endregion

    #region 特徵值搜尋結果頁面區
    private DataTable getQuestionFeatureResult(DataTable dtFeatureItem)
    {
        DataTable dtSearchResult = new DataTable();
        dtSearchResult.Columns.Add("cQID");
        dtSearchResult.Columns.Add("cQuestion");
        dtSearchResult.Columns.Add("cKeyword");
        dtSearchResult.Columns.Add("cQuestionType");
        dtSearchResult.Columns.Add("cQuestionGroupID");
        dtSearchResult.Columns.Add("cQuestionGroupName");

        string ZipRegex = @"<\s*(?<Tag>/*\s*[^<^>^\s]+)(?<Para>(\s*[^<^>^\s]+)*)\s*>";
        string strQuestion = "";
        string strNodeName = "";

        #region 取得問答題資料表Paper_TextQuestion資料
        foreach (DataRow drPaper_TextQuestion in dtFeatureItem.Rows)
        {
            DataTable dtQuestionMode = TextQuestionMode_SELECT(drPaper_TextQuestion["cQID"].ToString());
            if (dtQuestionMode.Rows.Count > 0)
            {
                strNodeName = PaperSystem.DataReceiver.getQuestionGroupNameByQuestionGroupID(dtQuestionMode.Rows[0]["cQuestionGroupID"].ToString());
                if (dtQuestionMode.Rows.Count > 0 && dtQuestionMode.Rows[0]["cQuestionGroupID"].ToString() != "" && strNodeName != "")
                {
                    DataRow row = dtSearchResult.NewRow();
                    strQuestion = System.Text.RegularExpressions.Regex.Replace(dtQuestionMode.Rows[0]["cQuestion"].ToString(), ZipRegex, "").Replace("&nbsp", "").Replace(";", "");
                    row["cQID"] = drPaper_TextQuestion["cQID"].ToString();
                    row["cQuestion"] = strQuestion;
                    row["cKeyword"] = DataReceiver.getTextQuestionKeyword(drPaper_TextQuestion["cQID"].ToString());
                    row["cQuestionType"] = "問答題";
                    row["cQuestionGroupID"] = dtQuestionMode.Rows[0]["cQuestionGroupID"].ToString();
                    row["cQuestionGroupName"] = strNodeName;
                    dtSearchResult.Rows.Add(row);
                }
            }
        }
        #endregion

        #region 取得選擇題資料表QuestionIndex資料
        DataTable dtQuestionIndex = QuestionIndex_SELECT_FeatureValue(dtFeatureItem, "1");
        foreach (DataRow drQuestionIndex in dtQuestionIndex.Rows)
        {
            DataTable dtQuestionMode = QuestionMode_SELECT(drQuestionIndex["cQID"].ToString());
            
            strNodeName = PaperSystem.DataReceiver.getQuestionGroupNameByQuestionGroupID(dtQuestionMode.Rows[0]["cQuestionGroupID"].ToString());
            if (dtQuestionMode.Rows.Count > 0 && dtQuestionMode.Rows[0]["cQuestionGroupID"].ToString() != "" && strNodeName != "")
            {
                DataRow row = dtSearchResult.NewRow();
                strQuestion = System.Text.RegularExpressions.Regex.Replace(drQuestionIndex["cQuestion"].ToString(), ZipRegex, "").Replace("&nbsp", "").Replace(";", "");
                //strAnswer = System.Text.RegularExpressions.Regex.Replace(drQuestionIndex["cAnswer"].ToString(), ZipRegex, "").Replace("&nbsp", "").Replace(";", "");
                row["cQID"] = drQuestionIndex["cQID"].ToString();
                row["cQuestion"] = strQuestion;
                //row["cAnswer"] = "N/A";
                row["cKeyword"] = "N/A";
                row["cQuestionType"] = "選擇題";
                row["cQuestionGroupID"] = dtQuestionMode.Rows[0]["cQuestionGroupID"].ToString();
                row["cQuestionGroupName"] = strNodeName;
                dtSearchResult.Rows.Add(row);
            }
        }
        #endregion

        #region 取得情境資料表Question_Situation資料
        foreach (DataRow drQuestion_Situation in dtFeatureItem.Rows)
        {
            DataTable dtQuestionMode = Situation_SELECT(drQuestion_Situation["cQID"].ToString());
            if (dtQuestionMode.Rows.Count > 0)
            {
                strNodeName = PaperSystem.DataReceiver.getQuestionGroupNameByQuestionGroupID(dtQuestionMode.Rows[0]["cQuestionGroupID"].ToString());
                if (dtQuestionMode.Rows.Count > 0 && dtQuestionMode.Rows[0]["cQuestionGroupID"].ToString() != "" && strNodeName != "")
                {
                    DataRow row = dtSearchResult.NewRow();
                    strQuestion = System.Text.RegularExpressions.Regex.Replace(dtQuestionMode.Rows[0]["cQuestion"].ToString(), ZipRegex, "").Replace("&nbsp", "").Replace(";", "");
                    row["cQID"] = drQuestion_Situation["cQID"].ToString();
                    row["cQuestion"] = strQuestion;
                    row["cKeyword"] = DataReceiver.getTextQuestionKeyword(drQuestion_Situation["cQID"].ToString());
                    row["cQuestionType"] = "情境題";
                    row["cQuestionGroupID"] = dtQuestionMode.Rows[0]["cQuestionGroupID"].ToString();
                    row["cQuestionGroupName"] = strNodeName;
                    dtSearchResult.Rows.Add(row);
                }
            }
        }
        #endregion

        return dtSearchResult;
    }

    //取得問答題的GroupID與GroupName
    private DataTable TextQuestionMode_SELECT(string strQID)
    {
        DataTable dtQuestionMode = new DataTable();
        clsHintsDB HintsDB = new clsHintsDB();
        string strSQL_QuestionMode = "SELECT * FROM QuestionMode A,QuestionIndex B,QuestionAnswer_Question C" +
            " WHERE (A.cQID=B.cQID) AND (B.cQID=C.cQID) AND (A.cQID = '" + strQID + "')";
        dtQuestionMode = HintsDB.getDataSet(strSQL_QuestionMode).Tables[0];
        return dtQuestionMode;
    }

    //取得選擇題有符合特徵值的問題
    private DataTable QuestionIndex_SELECT_FeatureValue(DataTable dtFeatureItem, string strQuestionType)
    {
        
        DataTable dtQuestionIndex = new DataTable();
        //若沒有特徵條件，則不執行搜尋        
        if (dtFeatureItem.Rows.Count > 0)
        {
            clsHintsDB HintsDB = new clsHintsDB();
            string strSQL_QuestionIndex = "SELECT * FROM QuestionIndex AS A INNER JOIN QuestionMode AS B ON A.cQID = B.cQID";
            for (int i = 0; i < dtFeatureItem.Rows.Count; i++)
            {
                if (i == 0)
                {
                    strSQL_QuestionIndex += " WHERE ";
                    strSQL_QuestionIndex += " ( ";
                }
                else
                    strSQL_QuestionIndex += " OR ";
                //比對上一頁搜尋的結果與資料庫所有的選擇題
                strSQL_QuestionIndex += "A.cQID = '" + dtFeatureItem.Rows[i][0].ToString()+ "' ";
            }
            strSQL_QuestionIndex += ")";
            strSQL_QuestionIndex += "AND (B.cQuestionType = '1')";
            dtQuestionIndex = HintsDB.getDataSet(strSQL_QuestionIndex).Tables[0];
        }
        return dtQuestionIndex;
    }

    //取得情境題的GroupID與GroupName    老詹 2013/07/30
    private DataTable Situation_SELECT(string situationQID)
    {
        DataTable dtQuestionMode = new DataTable();
        clsHintsDB HintsDB = new clsHintsDB();
        string strSQL_QuestionMode = "SELECT * FROM QuestionMode A,QuestionIndex B,Question_Situational C" +
            " WHERE (A.cQID=B.cQID) AND (B.cQID=C.cQID) AND (A.cQID = '" + situationQID + "')";
        dtQuestionMode = HintsDB.getDataSet(strSQL_QuestionMode).Tables[0];
        return dtQuestionMode;
    }

    #endregion
}
