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

using PaperSystem;
using Hints.DB;

public partial class AuthoringTool_CaseEditor_Paper_Paper_AnswerTypeEdit : AuthoringTool_BasicForm_BasicForm
{
    string strGroupID = "";
    string strCareer = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(AuthoringTool_CaseEditor_Paper_Paper_AnswerTypeEdit));

        getParametor();

        lbQuestionClassify.Text = DataReceiver.getQuestionGroupNameByQuestionGroupID(strGroupID);

        if (!IsPostBack)
        {
            //設定預設值
            clsHintsDB HintsDB = new clsHintsDB();
            string strSQL_Conversation_AnswerType = "SELECT * FROM Conversation_AnswerType " +
                    "WHERE cQuestionClassifyID = '" + Convert.ToInt32(hfGroupSerialNum.Value) + "' ORDER BY cAnswerTypeNum DESC";
            DataTable dtConversation_AnswerType = HintsDB.getDataSet(strSQL_Conversation_AnswerType).Tables[0];
            if (dtConversation_AnswerType.Rows.Count > 0)
            {
                int iAnswerType = Convert.ToInt32(dtConversation_AnswerType.Rows[0]["cAnswerTypeNum"].ToString());
                ddlAnswerTypeNum.SelectedValue = iAnswerType.ToString();
                hfAnswerTypeNum.Value = iAnswerType.ToString();
                //SetAnswerTypeName(iAnswerType);
            }
            else
            {
                hfAnswerTypeNum.Value = "0";
                //SetAnswerTypeName(0);
            }
        }
        SetAnswerTypeName(Convert.ToInt32(hfAnswerTypeNum.Value));
    }

    /// <summary>
    /// 接收參數
    /// </summary>
    private void getParametor()
    {
        //GroupID
        if (Request.QueryString["GroupID"] != null)
        {
            strGroupID = Request.QueryString["GroupID"].ToString();
            hfGroupID.Value = strGroupID;
            hfGroupSerialNum.Value = DataReceiver.getQuestionGroupSerialNumByQuestionGroupID(strGroupID).ToString();

            if (Session["GroupID"] != null)
            {
                Session["GroupID"] = strGroupID;
            }
            else
            {
                Session.Add("GroupID", strGroupID);
            }
        }
        //Career  老詹 2014/08/20
        strCareer = Request.QueryString["Career"].ToString();
        hfCareer.Value = strCareer;
    }

    protected void ddlAnswerTypeNum_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iAnswerTypeNum = Convert.ToInt16(ddlAnswerTypeNum.SelectedValue);
        hfAnswerTypeNum.Value = iAnswerTypeNum.ToString();
        SetAnswerTypeName(iAnswerTypeNum);
    }

    protected void btAddAnswerType_Click(object sender, EventArgs e)
    {
        hfAnswerTypeNum.Value = (Convert.ToInt32(hfAnswerTypeNum.Value) + 1).ToString();
        SetAnswerTypeName(Convert.ToInt32(hfAnswerTypeNum.Value));
    }

    protected void SetAnswerTypeName(int iAnswerTypeNum)
    {
        pSettingAnswerTypeName.Controls.Clear();

        if (iAnswerTypeNum == 0)
        {
            Label lbEmpty = new Label();
            lbEmpty.Text = "目前無編輯任何答案型態";
            pSettingAnswerTypeName.Controls.Add(lbEmpty);
        }
        else
        {
            ////loading setting file name panel
            Panel pEditAnswerTypeNameBlock = new Panel();

            //panel title
            Table tbEditAnswerTypeNameT = new Table();
            tbEditAnswerTypeNameT.Width = Unit.Percentage(100);
            TableRow trEditAnswerTypeNameT = new TableRow();
            TableCell tcEditAnswerTypeNameHeader = new TableCell();
            tcEditAnswerTypeNameHeader.Text = "請輸入答案型態的名稱";
            tcEditAnswerTypeNameHeader.Width = Unit.Percentage(100);
            tcEditAnswerTypeNameHeader.ColumnSpan = 2;
            tcEditAnswerTypeNameHeader.Style.Add("text-align", "left");
            trEditAnswerTypeNameT.Cells.Add(tcEditAnswerTypeNameHeader);
            tbEditAnswerTypeNameT.Rows.Add(trEditAnswerTypeNameT);

            //loading Answer Type name edit block
            for (int iIdx = 0; iIdx < iAnswerTypeNum; iIdx++)
            {
                TableRow trAnswerTypeNumT = new TableRow();
                tbEditAnswerTypeNameT.Rows.Add(trAnswerTypeNumT);
                trAnswerTypeNumT.CssClass = (iIdx % 2 == 1) ? "header1_tr_even_row" : "header1_tr_odd_row";

                TableCell tcAnswerTypeNumT = new TableCell();
                tcAnswerTypeNumT.Width = Unit.Percentage(10);
                Label lbAnswerTypeNumT = new Label();
                lbAnswerTypeNumT.Text = "答案型態: " + (iIdx + 1).ToString();
                lbAnswerTypeNumT.Attributes.Add("style", "font-weight:bold;");
                tcAnswerTypeNumT.Controls.Add(lbAnswerTypeNumT);
                trAnswerTypeNumT.Cells.Add(tcAnswerTypeNumT);

                TableCell tcEditAnswerTypeName = new TableCell();
                tcEditAnswerTypeName.Width = Unit.Percentage(80);
                TextBox tbEditAnswerTypeName = new TextBox();
                tbEditAnswerTypeName.ID = "tbEditAnswerTypeName_" + (iIdx + 1).ToString();
                tbEditAnswerTypeName.Width = Unit.Percentage(95);
                tcEditAnswerTypeName.Controls.Add(tbEditAnswerTypeName);
                trAnswerTypeNumT.Cells.Add(tcEditAnswerTypeName);

                clsHintsDB HintsDB = new clsHintsDB();
                string strSQL_Conversation_AnswerType = "SELECT * FROM Conversation_AnswerType " +
                "WHERE cQuestionClassifyID = '" + Convert.ToInt32(hfGroupSerialNum.Value) + "' AND " +
                "cAnswerTypeNum = '" + (iIdx + 1) + "'";
                DataTable dtConversation_AnswerType = HintsDB.getDataSet(strSQL_Conversation_AnswerType).Tables[0];
                //更新
                if (dtConversation_AnswerType.Rows.Count > 0)
                {
                    tbEditAnswerTypeName.Text = dtConversation_AnswerType.Rows[0]["cAnswerTypeName"].ToString();
                }

                TableCell tcDeleteAnswerTypeName = new TableCell();
                tcDeleteAnswerTypeName.Width = Unit.Percentage(10);
                Button btDelete = new Button();
                btDelete.Text = "Delete";
                btDelete.CssClass = "button_continue";
                btDelete.ID = "btDelete_" + (iIdx + 1).ToString();
                tcDeleteAnswerTypeName.Controls.Add(btDelete);
                trAnswerTypeNumT.Cells.Add(tcDeleteAnswerTypeName);
                btDelete.Click += new EventHandler(btDelete_Click);
            }

            pEditAnswerTypeNameBlock.Controls.Add(tbEditAnswerTypeNameT);

            pSettingAnswerTypeName.Controls.Add(pEditAnswerTypeNameBlock);
        }
    }

    void btDelete_Click(object sender, EventArgs e)
    {
        string strTempAnswerTypeNum = "";
        Button btnDelete = new Button();
        btnDelete = (Button)(sender);

        strTempAnswerTypeNum = btnDelete.ID.Split('_')[1];
        ArrayList alAnswerTypeName = new ArrayList();
        for (int i = 0; i < Request.Form.Count; i++)
        {

            if (Request.Form.Keys[i].ToString().IndexOf("tbEditAnswerTypeName_") != -1)
            {
                alAnswerTypeName.Add(Request.Form[i].ToString());
            }
        }
        alAnswerTypeName.RemoveAt(Convert.ToInt32(strTempAnswerTypeNum)-1);

        clsHintsDB HintsDB = new clsHintsDB();
        string strSQL_Conversation_AnswerType = "";
        //先刪除存在的資料
        strSQL_Conversation_AnswerType = "DELETE Conversation_AnswerType WHERE cQuestionClassifyID = '" + hfGroupSerialNum.Value + "'";
        HintsDB.ExecuteNonQuery(strSQL_Conversation_AnswerType);

        for (int i = 0; i < alAnswerTypeName.Count; i++)
        {
            if (alAnswerTypeName[i] != "")
            {
                //新增
                strSQL_Conversation_AnswerType = "INSERT INTO Conversation_AnswerType" +
                  "(cQuestionClassifyID, cAnswerTypeName, cAnswerTypeNum)" +
                  " VALUES ('" + Convert.ToInt32(hfGroupSerialNum.Value) + "','" + alAnswerTypeName[i] + "','" + (i + 1) + "')";
                HintsDB.ExecuteNonQuery(strSQL_Conversation_AnswerType);
            }
        }



        hfAnswerTypeNum.Value = (Convert.ToInt32(hfAnswerTypeNum.Value) - 1).ToString();
        SetAnswerTypeName(Convert.ToInt32(hfAnswerTypeNum.Value));
    }

    //儲存答案型態
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void SaveAnswerType(string strGroupSerialID, string strArrayAnswerTypeList)
    {
        clsHintsDB HintsDB = new clsHintsDB();
        string strSQL_Conversation_AnswerType = "";
        //先刪除存在的資料
        strSQL_Conversation_AnswerType = "DELETE Conversation_AnswerType WHERE cQuestionClassifyID = '" + strGroupSerialID + "'";
        HintsDB.ExecuteNonQuery(strSQL_Conversation_AnswerType);

        string[] strArrayAnswerTypeName = strArrayAnswerTypeList.Split('|');
        for (int i = 0; i < strArrayAnswerTypeName.Length; i++)
        {
            if (strArrayAnswerTypeName[i] != "")
            {
                //新增
                strSQL_Conversation_AnswerType = "INSERT INTO Conversation_AnswerType" +
                  "(cQuestionClassifyID, cAnswerTypeName, cAnswerTypeNum)" +
                  " VALUES ('" + Convert.ToInt32(strGroupSerialID) + "','" + strArrayAnswerTypeName[i] + "','" + (i + 1) + "')";
                HintsDB.ExecuteNonQuery(strSQL_Conversation_AnswerType);
            }
        }

    }

}
