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
using Hints.DB;
using OpenNLP.Tools.SentenceDetect;
using OpenNLP.Tools.Parser;
using OpenNLP.Tools.Tokenize;
using OpenNLP.Tools.Chunker;
using OpenNLP.Tools.PosTagger;
using NaturalLanguage.Classes;
using System.IO;
using HtmlAgilityPack;
using System.Text;
using System.Net;
using System.Collections.Generic;
using System.Drawing;

public partial class AuthoringTool_CaseEditor_Paper_Paper_EditQuestionKeyword : System.Web.UI.Page, ICallbackEventHandler
{
    protected string[] strCallbackMsg;
    protected clsHintsDB HintsDB = new clsHintsDB();
    protected string strCaseID = "";
    protected string strClinicNum = "";
    protected string strSection = "";
    protected string strQID = "";
    protected string strCareer = "";
    protected string strQuestionType = ""; // 問題題型  老詹 2015/06/12

    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(AuthoringTool_CaseEditor_Paper_Paper_EditQuestionKeyword));
        InitQueryString();

        if (Page.IsPostBack) { ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "CallJS", "afterpostback();", true); }

        if (!this.IsPostBack)
        {           
            lOriginalContent.InnerHtml = GetContent(hfQID.Value, hfAID.Value);
            //lOriginalContent.Text = NLPAnalysisTransfer.GetAnalyzeText(hfAnalysisID.Value);
            //lOriginalContent.Text = NLPKeyword.GetAnalyzedTextWithKeyword(hfAnalysisID.Value);

            //this.init_keyword_table(hfAnalysisID.Value, NLPAnalysisTransfer.IsAnalyzed(hfAnalysisID.Value));

            lOriginalContent.Attributes.Add("onmouseup", "KeywordSelectOnMouseUp()");

            ConstructKeywordTable();
        }
        //this.init_keyword_table(hfQID.Value);

        btnFinish.ServerClick += new EventHandler(btnFinish_ServerClick);
        btnKeyTable.ServerClick += new EventHandler(btnKeyTable_ServerClick);
        btnKeyTableNoSearch.ServerClick += new EventHandler(btnKeyTableNoSearch_ServerClick);
    }

    private void btnKeyTable_ServerClick(object sender, EventArgs e)
    {
        ConstructKeywordTable();
        if (HiddenFieldfortext.Value != "")
            ShowSynonyms(HiddenFieldfortext.Value);
    }

    private void btnKeyTableNoSearch_ServerClick(object sender, EventArgs e)
    {
        Lb_synonyms.Rows.Add((HtmlTableRow)Session["tmpSynonyms"]);
        if (Session["GlobalSynonyms"] != null)
        {           
            string[] strTmpArr = (string[])Session["GlobalSynonyms"];
            string strTmpAllKeyword = GetAllKeyword(strQID);
            string[] strAllKeyword = strTmpAllKeyword.Split('|');
            for (int z = 1; z <= strTmpArr.Length; z++)
            {
                HtmlInputCheckBox tmpCb = (HtmlInputCheckBox)this.FindControl(HiddenFieldfortext.Value.ToString() + z.ToString() + "_" + strTmpArr[z - 1]);
                if (tmpCb != null)
                { tmpCb.Checked = false; }
            }
            for (int i = 0; i < strAllKeyword.Length; i++)
            {
                if (strAllKeyword[i].IndexOf(HiddenFieldfortext.Value.ToString()) >= 0)
                {
                    string[] strKeywordArr = strAllKeyword[i].Split(',');
                    for (int j = 1; j < strKeywordArr.Length; j++)
                    {
                        for (int k = 1; k <= strTmpArr.Length; k++)
                        {
                            HtmlInputCheckBox tmpCb = (HtmlInputCheckBox)this.FindControl(strKeywordArr[0] + k.ToString() + "_" + strKeywordArr[j]);
                            if (tmpCb != null)
                            { tmpCb.Checked = true; }
                        }
                    }
                }
            }
        }
        ConstructKeywordTable();
    }

    protected void ConstructKeywordTable()
    {
        //顯示keyword的事件
        string stringGet = "";     
        if (strQuestionType == "4")
        {
            stringGet = Hints.Learning.Question.DataReceiver.getConversation_Question_Keyword(strQID);
        }
        else
        {
            string strTextKeyword = "SELECT cKeyword FROM Paper_TextQuestionKeyword WHERE cQID='" + strQID + "'";
            DataTable dtTextKeyword = HintsDB.getDataSet(strTextKeyword).Tables[0];
            if (dtTextKeyword.Rows.Count > 0)
                stringGet = dtTextKeyword.Rows[0]["cKeyword"].ToString();
        }
        if (stringGet != "")
        {
            tbTotalKeyword.Controls.Clear();
            HtmlTableRow trKeywordTitle = new HtmlTableRow();
            trKeywordTitle.Attributes.Add("class", "header1_table_first_row");
            HtmlTableCell tdKeywordTitle = new HtmlTableCell();
            tdKeywordTitle.Style["width"] = "30%";
            tdKeywordTitle.InnerHtml = "<span style='font-size:20px;'>Keyword</span>";
            trKeywordTitle.Cells.Add(tdKeywordTitle);
            HtmlTableCell tdSynonymsTitle = new HtmlTableCell();
            tdSynonymsTitle.Style["width"] = "50%";
            tdSynonymsTitle.InnerHtml = "<span style='font-size:20px;'>Synonyms</span>";
            trKeywordTitle.Cells.Add(tdSynonymsTitle);
            HtmlTableCell tdDeleteTitle = new HtmlTableCell();
            tdDeleteTitle.Style["width"] = "20%";
            tdDeleteTitle.Attributes.Add("style", "text-align:center;");
            tdDeleteTitle.InnerHtml = "<span style='font-size:20px;'>Delete Keyword</span>";
            trKeywordTitle.Cells.Add(tdDeleteTitle);
            tbTotalKeyword.Rows.Add(trKeywordTitle);
            string[] strAllKeyword = stringGet.Split('|');
            for (int i = 0; i < strAllKeyword.Length; i++)
            {
                string[] strKeywordArr = strAllKeyword[i].Split(',');
                if (strKeywordArr[0] != "")
                {
                    HtmlTableRow trKeyword = new HtmlTableRow();
                    HtmlTableCell tdKeyword = new HtmlTableCell();
                    tdKeyword.Style["width"] = "30%";
                    tdKeyword.InnerHtml = "<input type='text' id='Lb_showKeyword_"+ i +"' runat='server' value='" + strKeywordArr[0] + "' readonly='true' style='color: #FF0000; font-size: 20px; width: 100%;' />";
                    HtmlTableCell tdSynonyms = new HtmlTableCell();
                    tdSynonyms.Style["width"] = "50%";
                    Label lbAllSynonyms = new Label();
                    lbAllSynonyms.Font.Size = 16;
                    lbAllSynonyms.ForeColor = Color.Blue;
                    lbAllSynonyms.Attributes.Add("onmouseup", "KeywordRemoveOnMouseUp()");
                    for (int j = 1; j < strKeywordArr.Length; j++)
                    {
                        lbAllSynonyms.Text += strKeywordArr[j] + ",  ";
                    }
                    if (lbAllSynonyms.Text != "")
                    { lbAllSynonyms.Text = lbAllSynonyms.Text.Remove(lbAllSynonyms.Text.LastIndexOf(',')); }
                    tdSynonyms.Controls.Add(lbAllSynonyms);
                    HtmlTableCell tdDelete = new HtmlTableCell();
                    tdDelete.Attributes.Add("style","text-align:center;");
                    tdDelete.Style["width"] = "20%";
                    tdDelete.InnerHtml = "<img runat='server' style='cursor:pointer' src='../../../App_Themes/djrm1/delete.gif' onclick=" + "DeleteCurrentKeyword('Lb_showKeyword_" + i + "')" + " />";
                    trKeyword.Cells.Add(tdKeyword);
                    trKeyword.Cells.Add(tdSynonyms);
                    trKeyword.Cells.Add(tdDelete);
                    tbTotalKeyword.Rows.Add(trKeyword);
                    if (i % 2 == 0)
                        trKeyword.Attributes.Add("class", "header1_tr_even_row");
                    else
                        trKeyword.Attributes.Add("class", "header1_tr_odd_row");
                }
            }
        }
    }

    protected string GetContent(string strQID, string strAID)
    {
        string strContent = "";
        strContent = "<input type='text' runat='server' value='' readonly='true' style='color: #FF0000; font-size: large; background-color: #ebecee; width: 100%;' /><span style='color:blue;'>Essay Question:</span><br /><textarea rows='4' cols='50' readonly='true' style='font-size:Larger; width:99%'>" + GetEssayContent(strQID, strAID).Split('$')[0] + "</textarea><br />";
        strContent += "<span style='color:blue;'>Essay Answer:</span><br /><textarea rows='4' cols='50' readonly='true' style='font-size:Larger; width:99%'>" + GetEssayContent(strQID, strAID).Split('$')[1] + "</textarea>";
        return strContent;
    }

    protected void InitQueryString()
    {
        strCaseID = Request.QueryString["CaseID"];
        strClinicNum = Request.QueryString["ClinicNum"];
        strSection = Request.QueryString["Section"];
        if (Request.QueryString["Career"] != null)
        { strCareer = Request.QueryString["Career"].ToString(); }
        hfAnalysisID.Value = Request.QueryString["AnalysisID"];
        hfQID.Value = (Request["QID"] != null) ? Request.QueryString["QID"] : "";
        strQID = hfQID.Value.ToString();
        hfGroupID.Value = Request.QueryString["GroupID"];
        hfAID.Value = (Request["AID"] != null) ? Request.QueryString["AID"] : "";

        //先判斷題型，來決定顯示的內容  老詹 2015/06/12
        string strQuesType = "SELECT cQuestionType FROM QuestionMode WHERE cQID='" + strQID + "'";
        DataTable dtQuestionType = HintsDB.getDataSet(strQuesType).Tables[0];
        if (dtQuestionType.Rows.Count>0)
            strQuestionType = dtQuestionType.Rows[0]["cQuestionType"].ToString();
    }
    
    protected string GetEssayContent(string strQID, string strAID)
    {
        if (strAID != "")
        {
            string strSQL_QuestionAnswer_Question = "SELECT cQuestion FROM QuestionAnswer_Question where cQID = '" + strQID + "'  ";
            DataTable dtQuestionAnswer_Question = HintsDB.getDataSet(strSQL_QuestionAnswer_Question).Tables[0];
            string strSQL_QuestionAnswer_Answer = "SELECT cAnswer FROM QuestionAnswer_Answer where cQID = '" + strQID + "' AND cAID = '" + strAID + "'  ";
            DataTable dtQuestionAnswer_Answer = HintsDB.getDataSet(strSQL_QuestionAnswer_Answer).Tables[0];

            string retValue = "";
            retValue = dtQuestionAnswer_Question.Rows[0]["cQuestion"].ToString() + "$" + dtQuestionAnswer_Answer.Rows[0]["cAnswer"].ToString();
            if (retValue.IndexOf("<P>") >= 0)
            {
                retValue = retValue.Replace("<P>", "");
                retValue = retValue.Replace("</P>", "");
            }
            return retValue;
        }
        else
        {
            string retValue = "";
            if (strQuestionType != "4") // 非對話題(問答題)
            {
                string strSQL = "SELECT cQuestion,cAnswer FROM Paper_TextQuestion where cQID = '" + strQID + "'  ";
                DataTable dtQuestionAnswer = HintsDB.getDataSet(strSQL).Tables[0];
                //DataTable dtEssayQuestion = HintsDB.getDataSet(strSQL).Tables[0];
                retValue = dtQuestionAnswer.Rows[0]["cQuestion"].ToString() + "$" + dtQuestionAnswer.Rows[0]["cAnswer"].ToString();
            }
            else
            {
                string strSQL = "SELECT cQuestion FROM Conversation_Question WHERE cQID = '" + strQID + "'  ";
                DataTable dtConversationQuestion = HintsDB.getDataSet(strSQL).Tables[0];
                retValue = dtConversationQuestion.Rows[0]["cQuestion"].ToString() + "$" + "";
            }
            return retValue;
        }
    }

    //protected string GetEssayAnswer(string strQID)
    //{
    //    string strSQL = "SELECT cAnswer FROM ItemForAnswer where cCaseID = '" + strCaseID + "'  AND cQID='" + strQID + "' AND sSeq = '1' ";
    //    string retAnswer = HintsDB.ExecuteScalar(strSQL).ToString();
    //    //DataTable dtEssayQuestion = HintsDB.getDataSet(strSQL).Tables[0];
    //    retAnswer = retAnswer.Replace("''", "'");
    //    return retAnswer;
    //}

    public string RemoveStringtag(string strsource)
    {
        int count = strsource.Length;

        string tempstring = "";

        int precount = 0, nextcount = 0;
        tempstring = strsource;

        for (int a = 0; a < count; a++)
        {
            if (tempstring.IndexOf('<') == (-1) || tempstring.IndexOf('>') == (-1) || (tempstring.IndexOf('<') > tempstring.IndexOf('>')))
            {
                break;
            }
            precount = tempstring.IndexOf('<');
            nextcount = tempstring.IndexOf('>');
            tempstring = tempstring.Remove(precount, nextcount - precount + 1);


        }
        return tempstring;

    }

    #region initial keyword table
    public void init_keyword_table(string QID)
    {
        //HtmlTable table = new HtmlTable();
        //HtmlTableRow trTitle = new HtmlTableRow();
        //HtmlTableCell tcChkBox = new HtmlTableCell();
        //HtmlTableCell tcWeight = new HtmlTableCell();
        //HtmlTableCell tcSeq = new HtmlTableCell();
        //HtmlTableCell tcFuzzy = new HtmlTableCell();
        //HtmlTableCell tcPartial = new HtmlTableCell();
        //HtmlTableCell tcText = new HtmlTableCell();
        //HtmlTableCell tcAnalyzed = new HtmlTableCell();
        //HtmlTableCell tcDelete = new HtmlTableCell();


        //trTitle.Cells.Add(tcFuzzy);
        //trTitle.Cells.Add(tcPartial);
        //trTitle.Cells.Add(tcWeight);

        //trTitle.Cells.Add(tcChkBox);
        //trTitle.Cells.Add(tcText);
        //trTitle.Cells.Add(tcDelete);
        //trTitle.Cells.Add(tcAnalyzed);    // user override
        //table.Rows.Add(trTitle);

        //int row = 0;
        //string[] row_color = { "#EFF3FB", "#FFFFFF" };
        hfKeywordControl.Value = "";
        //DataTable dtKeywords = NLPKeyword.GetAnalyzeKeyword(analysisID, "%");
        string strGetKeyword = "Select * from Paper_TextQuestionKeyword where cQID = '" + QID + "' order by cKeywordID ";
        //DataTable
        DataTable dtKeywords = HintsDB.getDataSet(strGetKeyword).Tables[0];
        lOriginalContent.InnerHtml = GetContent(hfQID.Value, hfAID.Value);
        foreach (DataRow drKeyword in dtKeywords.Rows)
        {
            //for (int i = 0; i < 3; i++) {
            //string keyID = drKeyword["KeyID"].ToString();   // keyID
            //string fuzzyID = drKeyword["FuzzyID"].ToString();   // fuzzyID
            string keyText = drKeyword["cKeyword"].ToString();
            string keyID = drKeyword["cKeywordID"].ToString();
            //string ketTemp = "";

            lOriginalContent.InnerHtml = lOriginalContent.InnerHtml.Replace(keyText, "<span class='span_keyword' >" + keyText + "</span>");
            //HtmlTableRow trKeyword = new HtmlTableRow();

            #region OLD
            // fuzzy analysis
            //HtmlTableCell tcKeywordFuzzy = new HtmlTableCell();
            //HtmlInputCheckBox chkFuzzy = new HtmlInputCheckBox();
            //chkFuzzy.ID = "chk_fuzzy_" + keyID;
            //chkFuzzy.Checked = System.Convert.ToBoolean(drKeyword["bFuzzyAnalyze"]) | (fuzzyID != "");
            //tcKeywordFuzzy.Controls.Add(chkFuzzy);
            //tcKeywordFuzzy.Controls.Add(this.ParseControl("&nbsp;"));
            // edit
            //Label spanEdit = new Label();
            //spanEdit.Text = "<span onclick=\"edit_fuzzy('" + analysisID + "','" + keyID + "','" + fuzzyID + "')\" style='cursor: hand;'><u>Edit</u></span>";
            //tcKeywordFuzzy.Controls.Add(spanEdit);
            //tcKeywordFuzzy.Controls.Add(this.ParseControl("&nbsp;"));
            // delete
            //Label spanDelete = new Label();
            //spanDelete.Text = "<span onclick=\"delete_fuzzy('" + keyID + "')\"" + ((fuzzyID != "") ? " style='cursor: hand;'" : " ") + "><u>Delete</u></span>";
            //spanDelete.Enabled = (fuzzyID != "");
            //tcKeywordFuzzy.Controls.Add(spanDelete);

            //trKeyword.Cells.Add(tcKeywordFuzzy);


            // partial analysis
            //HtmlTableCell tcKeywordPartial = new HtmlTableCell();
            // modified @ 2007-08-06 by dolphin, use integer to do partial analysis
            //HtmlInputCheckBox chkPartial = new HtmlInputCheckBox();
            //chkPartial.ID = "chk_part_" + keyID;
            //chkPartial.Checked = System.Convert.ToBoolean(drKeyword["bPartialAnalyze"]);
            //tcKeywordPartial.Controls.Add(chkPartial);
            //HtmlSelect selPartial = new HtmlSelect();
            //selPartial.ID = "sel_partial_" + keyID;
            //for (int k = 0; k <= 2; selPartial.Items.Add((++k).ToString())) ;
            //selPartial.Items.Add(new ListItem("No", "0"));
            //selPartial.Items.Add(new ListItem("Yes, get partial scores", "1"));
            //selPartial.Items.Add(new ListItem("Yes, get all scores", "2"));
            //selPartial.SelectedIndex = System.Convert.ToInt32(drKeyword["cKeywordSeq"]);
            //tcKeywordPartial.Controls.Add(selPartial);
            //trKeyword.Cells.Add(tcKeywordPartial);

            // keyword weight
            //HtmlTableCell tcKeywordWeight = new HtmlTableCell();
            //HtmlSelect selWeight = new HtmlSelect();
            //selWeight.ID = "sel_" + drKeyword["cKeyID"].ToString();
            //for (int j = 0; j < 5; selWeight.Items.Add((++j).ToString())) ;
            //selWeight.SelectedIndex = System.Convert.ToInt32(drKeyword["iWeight"]) - 1;   // weight
            //tcKeywordWeight.Controls.Add(selWeight);
            //trKeyword.Cells.Add(tcKeywordWeight);

            //HtmlTableCell tcKeywordSeq = new HtmlTableCell();
            //Label lbSeq = new Label();
            //lbSeq.Text = drKeyword["cKeywordSeq"].ToString();
            //tcKeywordSeq.Controls.Add(lbSeq);
            //trKeyword.Cells.Add(tcKeywordSeq);


            // checkbox
            //HtmlTableCell tcKeywordChkBox = new HtmlTableCell();
            //HtmlInputCheckBox chkKeyword = new HtmlInputCheckBox();
            //chkKeyword.ID = "chk_ctrl_" + keyID;
            // analyzed, set to true
            // not analyzed, let user decide which one he wants
            //chkKeyword.Checked = IsAnalyzed | System.Convert.ToBoolean(drKeyword["bOverride"]);
            //chkKeyword.Attributes.Add("onclick", "check_text('" + chkKeyword.ClientID + "', 'span_" + keyID + "')");
            //tcKeywordChkBox.Controls.Add(chkKeyword);
           // trKeyword.Cells.Add(tcKeywordChkBox);
            //hfKeywordControl.Value += keyID + ";";


            // keyword
            //HtmlTableCell tcKeywordText = new HtmlTableCell();
            //HtmlInputText txtKeyword = new HtmlInputText();
            
            //txtKeyword.ID = "txt_" + keyID;
            //txtKeyword.Value = keyText; // keyword

            //tcKeywordText.Controls.Add(txtKeyword);
            //trKeyword.Cells.Add(tcKeywordText);

            //HtmlTableCell tcKeywordDelete = new HtmlTableCell();
            //HtmlInputButton btnDelete = new HtmlInputButton();
            //btnDelete.ID = QID + "$" + drKeyword["cKeywordSeq"].ToString();
            //btnDelete.Value = "Delete";
            //btnDelete.CommandArgument = keyID + "$" + QID;
            //btnDelete.Click +=new EventHandler(btnDelete_Click);
            //btnDelete.Attributes.Add("OnClick", "DeleteKeyword('"+drKeyword["cKeyID"].ToString() + "')");

            //tcKeywordDelete.Controls.Add(btnDelete);
            //trKeyword.Cells.Add(tcKeywordDelete);


            // ********************************************************************** //
            // check if a parent's text is include the text but not the same => 縮排
            // use a table to record the keyword heirachy
            // added this for LSChen
            //#region . 縮排 for LS
            //{
            //    int count = NLPAnalysisKeywordHierarchy.GetHierarchy(keyID, 0);
            //    //string space = "";
            //    //for (int i = 0; i < count * 3; space += "&nbsp;", i++) ;
            //    //tcKeywordText.Controls.Add(this.ParseControl(space));
            //    txtKeyword.Attributes.Add("style", "width: " + (99 - count * 5).ToString() + "%;");

            //    // ********************************************************************************
            //    // redo by new method, use ArrayList to do that
            //    #region removed by dolphin @ 2007-05-09
            //    //DataTable dtHierarchy = NLPAnalysisKeywordHierarchy.CheckHierarchy(keyID);
            //    //if (dtHierarchy.Rows.Count > 0) {
            //    //    chkKeyword.Checked = !(count > 0);
            //    //    // add checkbox script
            //    //    string script = "";
            //    //    // ********************************************************************** //
            //    //    //// add all children node script
            //    //    //foreach (DataRow dr in dtHierarchy.Rows) {
            //    //    //    string temp_keyID = dr["KeyID"].ToString();
            //    //    //    // not to check myself
            //    //    //    // get similar id by string.Replace
            //    //    //    script += (temp_keyID == keyID) ? "" : "check_box('" + chkKeyword.ClientID + "','" + chkKeyword.ClientID.Replace(keyID, temp_keyID) + "');";
            //    //    //}
            //    //    //// if this key is not parent key, add script to check parent
            //    //    //script += (dtHierarchy.Rows[0]["PKeyID"].ToString() == keyID) ? "" : "check_box('" + chkKeyword.ClientID + "','" + chkKeyword.ClientID.Replace(keyID, dtHierarchy.Rows[0]["PKeyID"].ToString()) + "');";

            //    //    // the above has been change to the following codes by dolphin @ 2007-03-24

            //    //    if (dtHierarchy.Rows[0]["PKeyID"].ToString() == keyID) {
            //    //        foreach (DataRow dr in dtHierarchy.Rows) {
            //    //            string temp_keyID = dr["KeyID"].ToString();
            //    //            // not to check myself
            //    //            // get similar id by string.Replace
            //    //            script += (temp_keyID == keyID) ? "" : "check_box('" + chkKeyword.ClientID + "','" + chkKeyword.ClientID.Replace(keyID, temp_keyID) + "');";
            //    //        }
            //    //    }
            //    //    else {
            //    //        script += "check_box('" + chkKeyword.ClientID + "','" + chkKeyword.ClientID.Replace(keyID, dtHierarchy.Rows[0]["PKeyID"].ToString()) + "');";
            //    //    }

            //    //    // ********************************************************************** //

            //    //    chkKeyword.Attributes.Add("onclick", script);
            //    //}
            //    #endregion
            //    // the following is the new method
            //    {
            //        ArrayList list = NLPAnalysisKeywordHierarchy.GetHierarchy(keyID);
            //        if (list.Count > 0)
            //        {
            //            string script = "";
            //            for (int l = 0; l < list.Count; l++)
            //            {
            //                string temp_keyID = list[l].ToString();
            //                script += "check_box('" + chkKeyword.ClientID + "','" + chkKeyword.ClientID.Replace(keyID, temp_keyID) + "','" + keyID + "','" + temp_keyID + "');";
            //            }

            //            chkKeyword.Attributes.Add("onclick", script);   // replace the previous script
            //        }
            //    }
            //    //chkKeyword.Checked = (count == 0);
            //    // ********************************************************************************
            //}
            //#endregion
            //lOriginalContent.Text = lOriginalContent.Text.Replace(keyText, "<span " + ((chkKeyword.Checked) ? "class='span_keyword'" : "") + " id='span_" + keyID + "'>" + keyText + "</span>");
           
            // check NLPAnalysisKeywordHierarchy table
            // ********************************************************************** //


            // user override
            //HtmlTableCell tcKeywordAnalyzed = new HtmlTableCell();
            //tcKeywordAnalyzed.InnerText = drKeyword["bOverride"].ToString().Substring(0, 1);
            //trKeyword.Cells.Add(tcKeywordAnalyzed);

            //table.Rows.Add(trKeyword);

            // row style
            //tcKeywordChkBox.Width = "20";
            //tcKeywordFuzzy.Width = "20";
            //tcKeywordFuzzy.Visible = false;
            //tcKeywordWeight.Width = "300";
            //tcKeywordText.Align = "right";
            //tcKeywordFuzzy.Width = "120";
            //trKeyword.BgColor = row_color[row++ % 2];

            //// onmouse over style change
            //trKeyword.Attributes.Add("onmouseover", "this.style.backgroundColor='#F3B9C6';");
            //trKeyword.Attributes.Add("onmouseout", "this.style.backgroundColor='" + row_color[row % 2] + "';");
            #endregion
        }

        // table style
        //table.Border = 1;
        //table.BgColor = "#aaccff";
        //table.Width = "95%";

        // title row style
       // tcChkBox.InnerText = "key";
        //tcChkBox.Width = "20";
        //tcFuzzy.InnerText = "fuzzy analyze";
        //tcFuzzy.Visible = false;
        //tcFuzzy.Width = "120";
        //tcPartial.Width = "180";
        //tcPartial.InnerText = "partial analyze";
        //tcWeight.InnerText = "Weight";
        //tcWeight.Width = "80";
        //tcText.InnerText = "Keyword";
        //tcText.Align = "center";
        //tcDelete.InnerText = "Delete";
        //tcSeq.InnerText = "Sequence";
        //tcAnalyzed.Width = "20";
        //trTitle.BgColor = "#507CD1";
        //trTitle.Attributes.Add("style", "color: #FFFFFF; font-weight: bold;");
        //phKeywordList.Controls.Clear();
        //phKeywordList.Controls.Add(table);


    }
    #endregion
    #region analyze text
    protected void analyze(string analysisID)
    {
        EnglishMaximumEntropySentenceDetector mSentenceDetector = (EnglishMaximumEntropySentenceDetector)Application["NLPSentenceDetect"];
        string[] sentenceList = mSentenceDetector.SentenceDetect(NLPAnalysisTransfer.GetAnalyzeText(analysisID));

        for (int list = 0; list < sentenceList.Length; list++)
        {
            NLPParser NewParser = new NaturalLanguage.Classes.NLPParser((EnglishMaximumEntropyTokenizer)Application["NLPTokenizer"],
                (EnglishTreebankChunker)Application["NLPChunk"], (EnglishMaximumEntropyPosTagger)Application["NLPPosTagger"], (EnglishTreebankParser)Application["NLPParser"]);
            NLPAnalyze analyzer = new NLPAnalyze(NewParser);
            analyzer.ParseKeyword(hfAnalysisID.Value, sentenceList[list], true);
        }
    }
    #endregion
    #region store keyword data

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void DeleteKeyword(string strKeyID)
    {

        string strSQLDelKey = "Delete from Paper_TextQuestionKeyword where cKeywordID = '" + strKeyID + "' ";
        
            HintsDB.ExecuteNonQuery(strSQLDelKey);
        
        //catch { }
        init_keyword_table(hfQID.Value);
        gvShow.DataBind();
    }

    /// <summary>
    /// Save the new keyword with QID and Weight, and the primary key of keyword is KeyID
    /// </summary>
    /// <param name="strQID">Question ID</param>
    /// <param name="strKeyword">keyword text</param>
    protected void SaveKeyword(string strQID, string strKeyword)
    {
        string strKeyID;
        DateTime dtNow = DateTime.Now;
        while (dtNow.AddSeconds(0.1) < DateTime.Now) { }
        strKeyID = strQID + DateTime.Now.ToString("mmssfffffff");

        int KeywordSeq = GetKeywordMaxSeq(strQID);
        strKeyword = strKeyword.Replace("'", "");
        string strSQLSave = "Insert into Paper_TextQuestionKeyword(cKeywordID,cQID,iWeight,cKeyword) Values('" + strKeyID + "','" + strQID + "' , '1','" + strKeyword + "') ";

        HintsDB.ExecuteNonQuery(strSQLSave);

        #region OLD
        //// update analysis pool data
        //NLPParser parser = new NaturalLanguage.Classes.NLPParser((EnglishMaximumEntropyTokenizer)Application["NLPTokenizer"],
        //    (EnglishTreebankChunker)Application["NLPChunk"], (EnglishMaximumEntropyPosTagger)Application["NLPPosTagger"], (EnglishTreebankParser)Application["NLPParser"]);
        //NLPAnalyze analyzer = new NLPAnalyze(parser);

        //string analyzeText = NLPAnalysisTransfer.GetAnalyzeText(analysisID);

        //string[] controlID = keywordControl.Split(';');
        //string refNLPID = NLPAnalysisTransfer.GetAnalyzeNLPID(hfAnalysisID.Value);
        //// the last one is empty
        //for (int i = 0; i < controlID.Length - 1; i++)
        //{
        //    int fuzzy = (Request["chk_fuzzy_" + controlID[i]] != null) ? 1 : 0;
        //    // modified @ 2007-08-06 by dolphin, use integer to do partial analysis
           // int part = System.Convert.ToInt32(Request["sel_partial_" + controlID[i]]); //(Request["chk_part_" + controlID[i]] != null) ? 1 : 0;
           // string keywordText = Request["txt_" + controlID[i]];
        //    string keywordWeight = Request["sel_" + controlID[i]];

        //    if (Request["chk_ctrl_" + controlID[i]] != null)
        //    { // the checkbox is on

        //        NLPKeyword.UpdateAnalyzeKeyword(controlID[i], keywordText, keywordWeight, fuzzy, part);
        //        NLPKeyword.UpdateAnalyzeKeywordMappingRepeatedCount(analysisID, controlID[i], analyzer.GetWordRepeatCount(analyzeText, keywordText));
        //    }
        //    else
        //    {
        //        // no check, delete it
        //        NLPKeyword.DeleteAnalyzeKeyword(controlID[i]);

        //        //// we can insert it as redundant words
        //        //string newRedID = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        //        //NLPKeyword.InsertRedundantWord(refNLPID, newRedID, keywordText, ref newRedID);
        //    }
        //}

        //int contentWords = analyzer.GetWords(analyzeText);
        //int keywordWords = analyzer.GetWords(analysisID, "%");
        //int matchedWords = NLPKeyword.GetAnalyzeKeyword(analysisID, "%").Rows.Count;
        //int matchedWeight = NLPKeyword.GetAnalyzeKeywordWeight(analysisID, "%");
        //NLPAnalysisTransfer.SetAnalyzedWords(analysisID, contentWords, keywordWords, matchedWords, matchedWeight);

        //NLPAnalysisTransfer.SetAnalyzed(analysisID);
        #endregion
    }

    private int GetKeywordMaxSeq(string strQID)
    {
        string strSQLGetSeq = "Select Max(iWeight) from Paper_TextQuestionKeyword where cQID = '" + strQID + "' Group by cQID ";
        int KeywordSeq = 0;

        try { KeywordSeq = Convert.ToInt32(HintsDB.ExecuteScalar(strSQLGetSeq)); }
        catch { }

        KeywordSeq += 1;
        return KeywordSeq;
    }
    #endregion
    protected void btReAnalyze_Click(object sender, EventArgs e)
    {
        //NLPKeyword.ReAnalyzeReferenceKeyword(hfAnalysisID.Value);
        ////NLPAnalysisTransfer.SetReAnalyze(hfAnalysisID.Value);
        //this.analyze(hfAnalysisID.Value);

        //// generate in init_keyword_table, use the defalut data, modified @ 2007-05-09 by dolphin
        ////lOriginalContent.Text = NLPKeyword.GetAnalyzedTextWithKeyword(hfAnalysisID.Value);
        //lOriginalContent.Text = NLPAnalysisTransfer.GetAnalyzeText(hfAnalysisID.Value);
        //this.init_keyword_table(hfAnalysisID.Value, NLPAnalysisTransfer.IsAnalyzed(hfAnalysisID.Value));

        //// added @ 2007-08-27 by dolphin
        //ClientScript.RegisterClientScriptBlock(this.GetType(), "reanalysis", "<script>window.setTimeout(\"window.alert('Please select the keywords you need.')\", 200);</script>");
    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ImageButton btnDeleteTemp = new ImageButton();
        btnDeleteTemp = (ImageButton)sender;
        string DelKeyID = btnDeleteTemp.CommandArgument;
        DeleteKeyword(DelKeyID);
        //init_keyword_table(hfQID.Value);
    }

    protected void btSubmit_Click(object sender, EventArgs e)
    {
        // save data
        //this.SaveKeyword(hfQID.Value);
        //NLPAnalysisTransfer.SetAnalyzed(hfAnalysisID.Value);

        //Response.Redirect("ReferenceAnalysis.aspx?AnalysisID=" + hfAnalysisID.Value);
    }
    protected void btAnalyze_Click(object sender, EventArgs e)
    {
        // save data
        //this.SaveKeyword(hfAnalysisID.Value, hfKeywordControl.Value);
        //NLPAnalysisTransfer.SetAnalyzed(hfAnalysisID.Value);

        // delete all previous data in case users may change some setting of analysis
        //if (System.Convert.ToBoolean(hfRedoAnalysis.Value))
        //{
        //    NLPKeyword.ReAnalyzeIndividualKeyword(hfAnalysisID.Value);

        //    // generate redundant list   
        //    NLPKeyword.DeleteNonKeyword(NLPAnalysisTransfer.GetAnalyzeNLPID(hfAnalysisID.Value), "%");

        //    EnglishMaximumEntropySentenceDetector mSentenceDetector = (EnglishMaximumEntropySentenceDetector)Application["NLPSentenceDetect"];
        //    string[] sentenceList = mSentenceDetector.SentenceDetect(NLPAnalysisTransfer.GetAnalyzeText(hfAnalysisID.Value));

        //    for (int list = 0; list < sentenceList.Length; list++)
        //    {
        //        NLPParser parser = new NaturalLanguage.Classes.NLPParser((EnglishMaximumEntropyTokenizer)Application["NLPTokenizer"],
        //            (EnglishTreebankChunker)Application["NLPChunk"], (EnglishMaximumEntropyPosTagger)Application["NLPPosTagger"], (EnglishTreebankParser)Application["NLPParser"]);
        //        NLPAnalyze analyzer = new NLPAnalyze(parser);
        //        analyzer.ParseNonKeyword(hfAnalysisID.Value, sentenceList[list]);
        //    }
        //}

        //Response.Redirect("Entry_IndivisualCaseNote.aspx?ReferenceID=" + hfAnalysisID.Value);
    }
    
    protected void btAddKeyword_Click(object sender, EventArgs e)
    {
        // added @ 2007-08-27 by dolphin
        if (tbNewKeyword.Text.Trim() == "")
        {
            return;
        }
        string text = tbNewKeyword.Text;
        SaveKeyword(hfQID.Value, text);
        init_keyword_table(hfQID.Value);
        tbNewKeyword.Text = "";
        gvShow.DataBind();
    }

    #region ICallbackEventHandler 成員

    public string GetCallbackResult()
    {
        //throw new Exception("The method or operation is not implemented.");
        return strCallbackMsg[2];
    }

    public void RaiseCallbackEvent(string eventArgument)
    {
        //throw new Exception("The method or operation is not implemented.");
        strCallbackMsg = eventArgument.Split('$');
        //switch(strCallbackMsg[0]) {
        //    case "Add":
        //        NLPKeyword.UpdateAnalyzeKeywordMapping(hfAnalysisID.Value, strCallbackMsg[1], strCallbackMsg[2]);
        //        break;
        //    case "Del":
        //        NLPKeyword.UpdateAnalyzeKeywordMapping(hfAnalysisID.Value, strCallbackMsg[1], "");
        //        break;
        //    default:
        //        NLPKeyword.UpdateAnalyzeKeywordMapping(hfAnalysisID.Value, strCallbackMsg[1], strCallbackMsg[2]);
        //        break;
        //}
        NLPKeyword.UpdateAnalyzeKeywordMapping(hfAnalysisID.Value, strCallbackMsg[1], strCallbackMsg[2]);
    }

    #endregion
    protected void gvShow_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
                DropDownList ddlW = new DropDownList();
                GridViewRow Row = (GridViewRow)e.Row;
                
                ddlW = (DropDownList)Row.FindControl("ddlShowWeight");
                string strSQLGetKeyCount = "Select Count(*) from Paper_TextQuestionKeyword where cQID = '" + hfQID.Value + "' Group by cQID";

                string strSQLGetKeyID = "Select * from Paper_TextQuestionKeyword where cQID = '" + hfQID.Value + "' Order by cKeywordID";
            
                //int KeywordCount = Convert.ToInt32(HintsDB.ExecuteScalar(strSQLGetKeyCount));
                DataTable dtKeyID = HintsDB.getDataSet(strSQLGetKeyID).Tables[0];
                int Weight = Convert.ToInt32(dtKeyID.Rows[e.Row.RowIndex]["iWeight"]);
                ddlW.ID = dtKeyID.Rows[e.Row.RowIndex]["cKeywordID"].ToString(); 
                
                int KeywordCount = dtKeyID.Rows.Count;  
                for(int i=1 ; i<=KeywordCount ; i++)
                {
                    ddlW.Items.Add(new ListItem(i.ToString(),i.ToString()));
                    if (i == Weight)
                    {
                        ddlW.SelectedItem.Text = Weight.ToString();
                    }
                }
                
                if (e.Row.RowState == DataControlRowState.Edit || ((int)e.Row.RowState) == 5)
                {
                    
                    ddlW.Enabled = true;
                    //ddlW.Visible = false;
                }
                else
                {
                    ddlW.Enabled = false;
                    //ddlW.Visible = true;

                    
                }

            e.Row.Attributes.Add("onmouseover", "mouseover(this)");
            if (e.Row.RowIndex % 2 == 0)
            {
                e.Row.Attributes.Add("onmouseout", "mouseouteven(this)");
            }
            else
            {
                e.Row.Attributes.Add("onmouseout", "mouseoutodd(this)");
            }
        }
    }


    protected void gvShow_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (gvShow.EditIndex != -1)
        {
            gvShow.SelectedIndex = -1;
            e.Cancel = true;
            Literal txtmsg = new Literal();
            txtmsg.Text = "<script>alert('編輯模式下禁止換列!')</script>";
            Page.Controls.Add(txtmsg);
        }

    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void UpdateWeight(string strDDLID,string strSelectValue)
    {
        string strKeyID = strDDLID.Split('$')[2];

        string strSQLUpdateWeight = "Update Paper_TextQuestionKeyword Set iWeight = '" + strSelectValue + "' Where cKeywordID = '" + strKeyID + "'";
        HintsDB.ExecuteNonQuery(strSQLUpdateWeight);
    }

    private void btnFinish_ServerClick(object sender, EventArgs e)
    {
        Session["SynonymsRow"] = null;
        Session["tmpSynonyms"] = null;
        Session["GlobalSynonyms"] = null;
        if (hfAID.Value != "")
        {
            Response.Redirect("~/AuthoringTool/CaseEditor/Paper/Paper_TextQuestionEditorNew.aspx?QID=" + hfQID.Value + "&GroupID=" + hfGroupID.Value + "&AID=" + hfAID.Value+ "");
        }
        else
        {
            if (strQuestionType != "4") // 非對話題(問答題)
            {
                Response.Redirect("~/AuthoringTool/CaseEditor/Paper/Paper_TextQuestionEditor.aspx?QID=" + hfQID.Value + "&GroupID=" + hfGroupID.Value + "");
            }
            else
            {
                Response.Redirect("~/AuthoringTool/CaseEditor/Paper/Paper_ConversationQuestionEditor.aspx?Opener=Paper_QuestionViewNew_VoiceInquiry&QID=" + hfQID.Value + "&GroupID=" + hfGroupID.Value + "&Career=" + strCareer + "&bModify=True");
            }
        }

    }

    #region 新關鍵字加入法(含同義詞)  老詹 2015/06/10
    protected void Addnewtb_Click(object sender, EventArgs e)
    {
        tbTypeNewKeyword.Text = "";
        string keyword = "";
        if (HiddenFieldfortext.Value != null)
        {
            if (strQuestionType == "4")
            {
                keyword = Hints.Learning.Question.DataReceiver.getConversation_Question_Keyword(strQID);
            }
            else
            {
                string strTextKeyword = "SELECT cKeyword FROM Paper_TextQuestionKeyword WHERE cQID='" + strQID + "'";
                DataTable dtTextKeyword = HintsDB.getDataSet(strTextKeyword).Tables[0];
                if (dtTextKeyword.Rows.Count > 0)
                    keyword = dtTextKeyword.Rows[0]["cKeyword"].ToString();
            }
            Label NewKeyword = (Label)this.Page.FindControl("Lb_showKeyword");
            if ((keyword != "")&&(keyword != "tmp"))
            {
                // 防止問答題中，新增一筆新的資料時會錯的BUG，即先給一個tmp，故在新增時要多一個判斷。  老詹 2015/06/19
                keyword += "|" + HiddenFieldfortext.Value;
                NewKeyword.Text = HiddenFieldfortext.Value;
            }
            else
            {
                NewKeyword.Text = HiddenFieldfortext.Value;
                keyword = HiddenFieldfortext.Value;
            }
            UpdateConversationKeyword(keyword, strQID, true);           
        }
        //this.ViewState["TableAdded"] = true;
    }
    //移除keyword
    protected void Remove_Click(object sender, EventArgs e)
    {
        tbTypeNewKeyword.Text = "";
        string keyword = "";
        if (HiddenFieldforRemove.Value != null)
        {
            if (strQuestionType == "4")
            {
                keyword = Hints.Learning.Question.DataReceiver.getConversation_Question_Keyword(strQID);
            }
            else
            {
                string strTextKeyword = "SELECT cKeyword FROM Paper_TextQuestionKeyword WHERE cQID='" + strQID + "'";
                DataTable dtTextKeyword = HintsDB.getDataSet(strTextKeyword).Tables[0];
                if (dtTextKeyword.Rows.Count > 0)
                    keyword = dtTextKeyword.Rows[0]["cKeyword"].ToString();
            }
            //Label NewKeyword = (Label)this.Page.FindControl("Lb_showKeyword");
            string[] arrKeyword = keyword.Split('|');
            keyword = null;
            for (int i = 0; i < arrKeyword.Length; i++)
            {
                if ((arrKeyword[i].IndexOf(HiddenFieldforRemove.Value.ToString()) >= 0) || (arrKeyword[i] == ""))
                    continue;
                else
                {
                    if (i == arrKeyword.Length - 1)
                        keyword += arrKeyword[i];
                    else
                        keyword += arrKeyword[i] + "|";
                }
            }
            if (keyword != null)
            {
                if (keyword.LastIndexOf('|') == keyword.Length - 1)
                { keyword = keyword.Remove(keyword.LastIndexOf('|')); }
            }

            UpdateConversationKeyword(keyword, strQID, false);
        }
        //this.ViewState["TableAdded"] = true;
        Lb_showKeyword.Text = "";
        HiddenFieldfortext.Value = "";
    }
    //將Keyword寫入資料庫(更新同義詞到關鍵字欄位  老詹 2015/06/04)
    protected void UpdateConversationKeyword(string str, string strQID, bool bIsSearch)
    {
        //str = str.Remove(str.LastIndexOf('|'));
        RegisterStartupScript("", "<script> GetAllCheckedSynonyms('" + str + "', '" + strQID + "', '" + bIsSearch + "'); </script>");
    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void SaveSynonyms(string strNewkeyword, string strQID)
    {
        //更新keyword
        clsHintsDB myDB = new clsHintsDB();
        string strGetTypeSQL = "SELECT cQuestionType FROM QuestionMode WHERE cQID='"+ strQID +"'";
        DataTable dtQuestionType = myDB.getDataSet(strGetTypeSQL).Tables[0];
        if (dtQuestionType.Rows[0]["cQuestionType"].ToString() == "4")
        {
            string strSQL = "UPDATE Conversation_Question SET cKeyword = '" + strNewkeyword + "' " + "WHERE cQID  = '" + strQID + "' ";
            myDB.ExecuteNonQuery(strSQL);
        }
        else
        {
            if (strNewkeyword == "")
            {
                string strDeleteSQL = "DELETE Paper_TextQuestionKeyword WHERE cQID  = '" + strQID + "' ";
                myDB.ExecuteNonQuery(strDeleteSQL);
            }
            else
            {
                string strSQL = "UPDATE Paper_TextQuestionKeyword SET cKeyword = '" + strNewkeyword + "' " + "WHERE cQID  = '" + strQID + "' ";
                myDB.ExecuteNonQuery(strSQL);
            }
        }
    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string GetAllKeyword(string strQID)
    {
        clsHintsDB myDB = new clsHintsDB();
        string strGetTypeSQL = "SELECT cQuestionType FROM QuestionMode WHERE cQID='" + strQID + "'";
        string keyword = "";
        DataTable dtQuestionType = myDB.getDataSet(strGetTypeSQL).Tables[0];
        if (dtQuestionType.Rows[0]["cQuestionType"].ToString() == "4")
        { keyword = Hints.Learning.Question.DataReceiver.getConversation_Question_Keyword(strQID); }
        else
        {
            string strGetTextSQL = "SELECT cKeyword FROM Paper_TextQuestionKeyword WHERE cQID='" + strQID + "'";
            DataTable dtTextKeyword = myDB.getDataSet(strGetTextSQL).Tables[0];
            if (dtTextKeyword.Rows.Count > 0)
            {
                keyword = dtTextKeyword.Rows[0]["cKeyword"].ToString();
            }
            else
            {
                string strKeyID;
                DateTime dtNow = DateTime.Now;
                while (dtNow.AddSeconds(0.1) < DateTime.Now) { }
                strKeyID = strQID + DateTime.Now.ToString("mmssfffffff");
                string strSQLSave = "Insert into Paper_TextQuestionKeyword(cKeywordID,cQID,iWeight,cKeyword) Values('" + strKeyID + "','" + strQID + "' , '1','tmp') ";
                myDB.ExecuteNonQuery(strSQLSave);
                keyword = "tmp";
            }
        }
        return keyword;
    }

    protected void ShowSynonyms(string strKeyword)  //顯示同義詞tr  老詹 2015/06/04
    {
        string strResult = "";
        HtmlTableRow trSynonyms = new HtmlTableRow();
        trSynonyms.ID = "trSynonyms_" + strKeyword;
        try
        {
            //指定來源網頁
            WebClient url = new WebClient();
            int intCode = 0;
            int intChineseFrom = Convert.ToInt32("4e00", 16);
            int intChineseEnd = Convert.ToInt32("9fff", 16);
            if (strKeyword != "")
            {
                //取得input字串中指定判斷的index字元的unicode碼
                intCode = Char.ConvertToUtf32(strKeyword, 0);

                if (intCode >= intChineseFrom && intCode <= intChineseEnd) //判斷是否為中文字 老詹 2015/08/06
                {
                    //將網頁來源資料暫存到記憶體內
                    MemoryStream ms = new MemoryStream(url.DownloadData("https://www.moedict.tw/" + strKeyword));
                    // 使用預設編碼讀入 HTML 
                    HtmlDocument doc = new HtmlDocument();
                    doc.Load(ms, Encoding.UTF8);
                    string strTest = doc.DocumentNode.SelectSingleNode(@"/html[1]").InnerHtml;

                    string strSubTest2 = strTest.Substring(strTest.IndexOf(">似</span>"));
                    string strSubTest3 = "";
                    if (strSubTest2.IndexOf("反")<=0)
                        strSubTest3 = strSubTest2.Substring(0, strSubTest2.IndexOf("</p>"));
                    else
                        strSubTest3 = strSubTest2.Substring(0, strSubTest2.IndexOf("反"));
                    string strSubTest4 = strSubTest3.Substring(9);

                    string[] strArr = strSubTest4.Split('、');
                    for (int i = 0; i < strArr.Length; i++)
                    {
                        string strTmp = strArr[i].Substring(strArr[i].IndexOf(">"));
                        string strTmp2 = strTmp.Substring(0, strTmp.IndexOf("</a>"));
                        string[] strTmpArr = strTmp2.Split('>');
                        if (i == 0)
                        {
                            if (strTmpArr[2] == "尊")
                            { strTmpArr[2] = "尊姓"; }
                            strResult += strTmpArr[2] + ",";
                        }
                        else
                            strResult += strTmpArr[3] + ",";
                    }
                    strResult = strResult.Remove(strResult.LastIndexOf(','));
                }
                else
                {
                    MemoryStream ms = new MemoryStream(url.DownloadData("https://tw.dictionary.yahoo.com/dictionary?p=" + strKeyword));
                    // 使用預設編碼讀入 HTML 
                    HtmlDocument doc = new HtmlDocument();
                    doc.Load(ms, Encoding.UTF8);
                    string strTest = doc.DocumentNode.SelectSingleNode(@"/html[1]").InnerHtml;
                    if (strTest.IndexOf("synonyms") >= 0)
                    {
                        string strSubTest2 = strTest.Substring(strTest.IndexOf("synonyms"));
                        string strSubTest3 = strSubTest2.Substring(0, strSubTest2.IndexOf("</ul>"));
                        string strSubTest4 = strSubTest3.Substring(strSubTest3.IndexOf("<a"));

                        string[] strArr = strSubTest4.Split(',');
                        for (int i = 0; i < strArr.Length; i++)
                        {
                            string strTmp = strArr[i].Substring(strArr[i].IndexOf(">"));
                            string strTmp2 = strTmp.Substring(0, strTmp.IndexOf("</a>"));
                            string[] strTmpArr = strTmp2.Split('>');
                            strResult += strTmpArr[1] + ",";
                        }
                        strResult = strResult.Remove(strResult.LastIndexOf(','));
                    }
                    else //Yahoo字典找不到就找Dreye
                    {
                        ms = new MemoryStream(url.DownloadData("http://yun.dreye.com/dict_new/dict.php?w="+ strKeyword +"&hidden_codepage=01"));
                        // 使用預設編碼讀入 HTML 
                        doc.Load(ms, Encoding.UTF8);
                        strTest = doc.DocumentNode.SelectSingleNode(@"/html[1]").InnerHtml;
                        string strSubTest2 = strTest.Substring(strTest.IndexOf("同義:"));
                        string strSubTest3 = strSubTest2.Substring(0, strSubTest2.IndexOf(";<p>"));
                        string strSubTest4 = strSubTest3.Substring(strSubTest3.IndexOf("<a"));

                        string[] strArr = strSubTest4.Split(';');
                        for (int i = 0; i < strArr.Length; i++)
                        {
                            string strTmp = strArr[i].Substring(strArr[i].IndexOf(">"));
                            string strTmp2 = strTmp.Substring(0, strTmp.IndexOf("</a>"));
                            string[] strTmpArr = strTmp2.Split('>');
                            strResult += strTmpArr[1] + ",";
                        }
                        strResult = strResult.Remove(strResult.LastIndexOf(','));
                    }
                }
            }                      
            //將每個同義詞拆開，前面各加一個checkbox
            string[] strAllSynonyms = strResult.Split(',');
            Session["GlobalSynonyms"] = strAllSynonyms;
            trSynonyms.Style["width"] = "100%";          
            HtmlTableCell tdSynonyms = new HtmlTableCell();
            tdSynonyms.Style["width"] = "100%";          
            for (int i = 0; i < strAllSynonyms.Length; i++)
            {
                HtmlInputCheckBox cbSynonyms = new HtmlInputCheckBox();
                cbSynonyms.ID = strKeyword + (i + 1).ToString() + "_" + strAllSynonyms[i];
                cbSynonyms.Attributes.Add("onclick", "AddNewSynonyms('" + cbSynonyms.ID + "','" + strQID + "');");
                Label lbSynonyms = new Label();
                lbSynonyms.ForeColor = Color.Red;
                lbSynonyms.Text = strAllSynonyms[i] + "&nbsp;&nbsp;&nbsp;";
                tdSynonyms.Controls.Add(cbSynonyms);
                tdSynonyms.Controls.Add(lbSynonyms);
            }           
            trSynonyms.Cells.Add(tdSynonyms);
            Lb_synonyms.Rows.Add(trSynonyms);
            Session["tmpSynonyms"] = trSynonyms;        
        }
        catch
        {
            trSynonyms.Style["width"] = "100%";
            trSynonyms.Attributes.Add("class", "header1_tr_odd_row");
            HtmlTableCell tdException = new HtmlTableCell();
            tdException.Style["width"] = "100%";
            tdException.Attributes.Add("Class", "header1_tr_even_row");
            tdException.InnerHtml = "<span style='color:red'>" + "此關鍵字沒有相關同義詞。</span>";
            trSynonyms.Cells.Add(tdException);
            Lb_synonyms.Rows.Add(trSynonyms);
            Session["GlobalSynonyms"] = null;
            Session["tmpSynonyms"] = trSynonyms;
        }
    }
    #endregion
}
