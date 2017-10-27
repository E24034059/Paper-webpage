using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class AuthoringTool_CaseEditor_Paper_Paper_EditKeywordAndSynonyms : System.Web.UI.Page
{
    public clsKeywordsUtil KeywordsUtil = null;
    string strDataBaseName = "";
    string strDataTableName = "";
    string strKeyWordsFieldName = "";
    string strCompareFieldsNames = "";
    string strCompareFieldsValues = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(AuthoringTool_CaseEditor_Paper_Paper_EditKeywordAndSynonyms));

        if (!IsPostBack)
        {
            if (Request.QueryString["DataBaseName"] != null)
            {
                strDataBaseName = Request.QueryString["DataBaseName"].ToString();//"Hints";
            }
            if (Request.QueryString["DataTableName"] != null)
            {
                strDataTableName = Request.QueryString["DataTableName"].ToString();//"Conversation_Question";
            }
            if (Request.QueryString["KeyWordsFieldName"] != null)
            {
                strKeyWordsFieldName = Request.QueryString["KeyWordsFieldName"].ToString();//"cKeyword";
            }
            if (Request.QueryString["CompareFieldsNames"] != null)
            {
                strCompareFieldsNames = Request.QueryString["CompareFieldsNames"].ToString();//"cQuestion|cQID";
            }
            if (Request.QueryString["CompareFieldsValues"] != null)
            {
                strCompareFieldsValues = Request.QueryString["CompareFieldsValues"].ToString();//"test conversation question|7046_178";
            }

            if (KeywordsUtil == null)
            {
                KeywordsUtil = new clsKeywordsUtil(strDataBaseName, strDataTableName, strKeyWordsFieldName, strCompareFieldsNames, strCompareFieldsValues);
                string strOriKey = KeywordsUtil.GetKeywords();
                if (strOriKey != "")
                {
                    div_section4.Controls.Add(KeywordsUtil.ConstructKeywordTable(strOriKey, true));
                }
                Session["KeywordsUtil"] = KeywordsUtil;
            }
        }

        Addnewtb.ServerClick += new EventHandler(Addnewtb_ServerClick);
        btnKeyTable.ServerClick += new EventHandler(btnKeyTable_ServerClick);
        Remove_Btn.ServerClick += new EventHandler(Remove_Btn_ServerClick);
    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string GetAllKeyword()
    {
        clsKeywordsUtil KeywordsUtil = (clsKeywordsUtil)Session["KeywordsUtil"];
        string strTest = KeywordsUtil.GetKeywords();
        return strTest;
    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void SaveSynonyms(string strNewkeyword)
    {
        clsKeywordsUtil KeywordsUtil = (clsKeywordsUtil)Session["KeywordsUtil"];
        KeywordsUtil.SaveKeyword(strNewkeyword);
    }

    private void Addnewtb_ServerClick(object sender, EventArgs e)
    {
        tbTypeNewKeyword.Text = "";
        clsKeywordsUtil KeywordsUtil = (clsKeywordsUtil)Session["KeywordsUtil"];
        string keyword = KeywordsUtil.GetKeywords();
        if (HiddenFieldfortext.Value != null)
        {
            Label NewKeyword = (Label)this.Page.FindControl("Lb_showKeyword");
            if ((keyword != "") && (keyword != "tmp"))
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
            UpdateConversationKeyword(keyword);
        }
    }

    private void Remove_Btn_ServerClick(object sender, EventArgs e)
    {
        tbTypeNewKeyword.Text = "";
        clsKeywordsUtil KeywordsUtil = (clsKeywordsUtil)Session["KeywordsUtil"];
        string keyword = KeywordsUtil.GetKeywords();
        if (HiddenFieldforRemove.Value != null)
        {
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

            UpdateConversationKeyword(keyword);
        }
        Lb_showKeyword.Text = "";
        HiddenFieldfortext.Value = "";
    }

    //將Keyword寫入資料庫(更新同義詞到關鍵字欄位)
    protected void UpdateConversationKeyword(string str)
    {
        //str = str.Remove(str.LastIndexOf('|'));
        RegisterStartupScript("", "<script> GetAllCheckedSynonyms('" + str + "'); </script>");
    }

    private void btnKeyTable_ServerClick(object sender, EventArgs e)
    {
        string strKeyword = HiddenFieldfortext.Value.ToString();
        clsKeywordsUtil KeywordsUtil = (clsKeywordsUtil)Session["KeywordsUtil"];
        string strTmpAllKeyword = KeywordsUtil.GetKeywords();
        div_section4.Controls.Add(KeywordsUtil.ConstructKeywordTable(strTmpAllKeyword,true));
        if (strKeyword != "")
        {
            string strSynonym = KeywordsUtil.SearchSynonym(strKeyword);
            HtmlTableRow trSynonyms = new HtmlTableRow();
            if (strSynonym == "")
            {
                trSynonyms.Style["width"] = "100%";
                trSynonyms.Attributes.Add("class", "header1_tr_odd_row");
                HtmlTableCell tdException = new HtmlTableCell();
                tdException.Style["width"] = "100%";
                tdException.Attributes.Add("Class", "header1_tr_even_row");
                tdException.InnerHtml = "<span style='color:red'>" + "此關鍵字沒有相關同義詞。</span>";
                trSynonyms.Cells.Add(tdException);
                Lb_synonyms.Rows.Add(trSynonyms);
            }
            else
            {               
                trSynonyms.ID = "trSynonyms_" + strKeyword;
                string[] strAllSynonyms = strSynonym.Split(',');
                trSynonyms.Style["width"] = "100%";
                HtmlTableCell tdSynonyms = new HtmlTableCell();
                tdSynonyms.Style["width"] = "100%";
                for (int i = 0; i < strAllSynonyms.Length; i++)
                {
                    HtmlInputCheckBox cbSynonyms = new HtmlInputCheckBox();
                    cbSynonyms.ID = strKeyword + (i + 1).ToString() + "_" + strAllSynonyms[i];
                    cbSynonyms.Attributes.Add("onclick", "AddNewSynonyms('" + cbSynonyms.ID + "');");
                    Label lbSynonyms = new Label();
                    lbSynonyms.ForeColor = Color.Red;
                    lbSynonyms.Text = strAllSynonyms[i] + "&nbsp;&nbsp;&nbsp;";
                    tdSynonyms.Controls.Add(cbSynonyms);
                    tdSynonyms.Controls.Add(lbSynonyms);
                }
                trSynonyms.Cells.Add(tdSynonyms);
                Lb_synonyms.Rows.Add(trSynonyms);
                string[] strAllKeyword = strTmpAllKeyword.Split('|');
                for (int i = 0; i < strAllKeyword.Length; i++)
                {
                    if (strAllKeyword[i].IndexOf(strKeyword) >= 0)
                    {
                        string[] strKeywordArr = strAllKeyword[i].Split(',');
                        for (int j = 1; j < strKeywordArr.Length; j++)
                        {
                            for (int k = 1; k <= strAllSynonyms.Length; k++)
                            {
                                HtmlInputCheckBox tmpCb = (HtmlInputCheckBox)this.FindControl(strKeywordArr[0] + k.ToString() + "_" + strKeywordArr[j]);
                                if (tmpCb != null)
                                { tmpCb.Checked = true; }
                            }
                        }
                    }
                }
            }
        }
        //Session["KeywordsUtil"] = null;
    }
    protected void btmComplete_Click(object sender, EventArgs e)
    {
        Session.Remove("KeywordsUtil");
        RegisterStartupScript("", "<script>try { opener.document.getElementById('ctl00_cphContentArea_btReload').click(); } catch (e) { try { opener.location.reload(); } catch (e) { } };window.close(); </script>");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        RegisterStartupScript("", "<script>window.close();</script>");
    }
}