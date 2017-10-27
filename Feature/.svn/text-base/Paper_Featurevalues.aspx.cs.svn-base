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
using Hints.DB.QuestionGroup;
using Hints.HintsUtility;
using Hints;
using AuthoringTool.DB;
using suro.util;

public partial class AuthoringTool_CaseEditor_Paper_Default : AuthoringTool_BasicForm_BasicForm, ICallbackEventHandler
{
    protected string sCallBackFunctionInvocation = "";
    protected clsHintsDB hintsDB = new clsHintsDB();
    protected string strSpanIDPrefix = "";
    protected string strSQL = "";
    private System.Web.UI.Page m_currentPage = null;
    private string message = "";    // for callback
    protected string strListboxSelect = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        //創造題庫樹
        this.ConstructQuestionGroupTree();
        //回傳點選的Node
        sCallBackFunctionInvocation = ClientScript.GetCallbackEventReference(this, "message", "show_callback", null);        
        Ajax.Utility.RegisterTypeForAjax(typeof(AuthoringTool_CaseEditor_Paper_Default));   
        //初始化lsbFeatureItem

        btn_Edit.Attributes.Add("onclick", "return showEditWindow()");

        if (IsPostBack)
        {
            
        }
    
              
    }
    
    //創造屬性樹的方法
    protected void ConstructQuestionGroupTree()
    {
        //先清空原有的題庫樹
        tvFeatureGroup.Nodes.Clear();
        tvMoveGroup.Nodes.Clear();
        strSpanIDPrefix = "display";

        //創造屬性樹
        tvFeatureGroup.Nodes.Add(getTree("Feature_root", "Feature"));
        tvFeatureGroup.CollapseAll();

        TreeNode tnRoot = new TreeNode();
        TreeNode tnNode = new TreeNode();

        tnRoot = tvFeatureGroup.Nodes[0];
        tnRoot.Expanded = true;

        for (int nIdx = 0; nIdx < tnRoot.ChildNodes.Count; nIdx++)
        {
            tnNode = tvFeatureGroup.Nodes[0].ChildNodes[nIdx];
            tnNode.Expand();
        }
        //將屬性樹展開
        tvFeatureGroup.ExpandAll();
    }

    //創造屬性樹
    public TreeNode getTree(string nodeID, string nodeName)
    {
        //取得node的mode與作者
        DataTable dtNodeID = new DataTable();
        string strSQL_NodeID = "SELECT * FROM FeaturevalueTree WHERE cNodeID LIKE '" + nodeID + "' AND cNodeName LIKE '" + nodeName + "' ORDER BY cNodeID ASC";
        dtNodeID = hintsDB.getDataSet(strSQL_NodeID).Tables[0];
        string strAuthor = "";
        string strNodeName = "";

        TreeNode node = new TreeNode();
        if (dtNodeID.Rows.Count > 0)
        {
            strAuthor = dtNodeID.Rows[0]["cAuthor"].ToString();
            strNodeName = dtNodeID.Rows[0]["cNodeName"].ToString();
            node.Value = nodeID;          
            node.Text = "<span id='" + strSpanIDPrefix + "_" + nodeID + "'>" + nodeName + "</span>";
            node.NavigateUrl = "javascript:SelectNode('" + nodeID + "','" + strAuthor + "')";
       
            }
        else
        {
            node.Value = nodeID;
            node.Text = "<span id='" + strSpanIDPrefix + "_" + nodeID + "'>" + nodeName + "</span>";
            node.NavigateUrl = "javascript:SelectNode('" + nodeID + "','" + strAuthor + "')";
       
        }


        //判斷node是否還有children node
        //加入Try預防Session不存在變數
        DataTable dtChildren = this.getQuestionGroupNodeForAll("%", nodeID);
        if (dtChildren.Rows.Count > 0)
        {
            foreach (DataRow drData in dtChildren.Rows)
            {
                node.ChildNodes.Add(getTree(drData["cNodeID"].ToString(), drData["cNodeName"].ToString()));
            }
        }
        return node;
    }

    //取得所有的屬性節點
    public DataTable getQuestionGroupNodeForAll(object cNodeID, object cParentNodeID)
    {
        DataTable dtResult = new DataTable();
        strSQL = "SELECT * FROM FeaturevalueTree WHERE cNodeID LIKE '" + cNodeID + "' AND cParentID LIKE '" + cParentNodeID + "' ORDER BY  cNodeID asc";
        try
        {
            dtResult = hintsDB.getDataSet(strSQL).Tables[0];
        }
        catch
        {
            dtResult = new DataTable();
        }
        return dtResult;
    }

    //命名節點ID名稱
    private string getNewNodeID(string folder_id_to_add)
    {
        string strNewID;
        int intTemp = 0;
        DateTime dtNow = DateTime.Now;
        while (dtNow.AddSeconds(0.1) < DateTime.Now)
            intTemp++;
        strNewID = "FeatureGroup_" + DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
        return (strNewID);
    }

    #region Callback event
    public void RaiseCallbackEvent(string eventArg)
    {
        this.message = eventArg;
    }
    public string GetCallbackResult()
    {
        string nodeName = "";
        try
        {
            DataTable dtNode = this.getQuestionGroupNodeForAll(message, "%");
            nodeName = dtNode.Rows[0]["cNodeName"].ToString();
        }
        catch
        {
            nodeName = "";
        }
        return nodeName;
    }
    #endregion

   
    #region  按鈕事件區
    protected void btDeletNodeSubmit_Click(object sender, EventArgs e)
    {
        this.Delete_Node(strCurrentNodeID.Value);
        this.ConstructQuestionGroupTree();
    }
    protected void btBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Hints/AuthoringTool/CaseEditor/Paper/Paper_QuestionMainNew.aspx?Career=");
    }  
    //增加屬性樹的節點
    protected void btAddGroupSubmit_Click(object sender, EventArgs e)
    {
     
        //新增節點至資料表中      
        
        strSQL = "INSERT INTO FeaturevalueTree VALUES('" + this.getNewNodeID("") + "','" + strCurrentNodeID.Value + "','" + tNewNodeName.Text + "', '" + usi.UserID + "')";
        try
        {
            hintsDB.ExecuteNonQuery(strSQL);
        }
        catch
        {
            this.RegisterClientScriptBlock("", "<script> alert('error'); </script>");
        }

        //重新建造屬性樹       
        this.ConstructQuestionGroupTree();
    }
    protected void btModifyGroupSubmit_Click(object sender, EventArgs e)
    {
        strSQL = "UPDATE FeaturevalueTree SET cNodeName='" + tNodeName.Value + "' WHERE cNodeID='" + strCurrentNodeID.Value + "'";
        try
        {
            hintsDB.ExecuteNonQuery(strSQL);
        }
        catch
        {
            this.RegisterClientScriptBlock("", "<script> alert('error'); </script>");
        }
        //重新建造屬性樹      
        this.ConstructQuestionGroupTree();
    }    
       
    protected void btnEdit_Click(object sender, EventArgs e)
    {
     
        /*  if(strCurrentNodeID.Value!=""){
            Session["CurrentNodeID"] = strCurrentNodeID.Value;
            Response.Redirect("EditFeatureItem.aspx");
        }
        else
        {
            RegisterStartupScript("", "<script>alert('請先選擇一個特徵值!')</script>");
        }*/

    }
    
    
    #endregion  
    
    #region  AJAX區
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public HtmlTable GetTemplateMenu(string templateID)
    {
        HtmlTable ht = new HtmlTable();      
        HtmlTableRow hr = new HtmlTableRow();
        HtmlTableCell hc = new HtmlTableCell();

        DataTable dtItem = new DataTable();
        strSQL = "SELECT A.iFeatureNum, A.sFeatureName, A.sAuthor,B.cNodeName FROM  FeaturevalueItem AS A INNER JOIN FeaturevalueTree AS B ON A.cNodeID = B.cNodeID WHERE (A.cNodeID = '" + templateID + "')";
        dtItem = hintsDB.getDataSet(strSQL).Tables[0];
        hc.InnerHtml = GetTemplateSectionsTable(dtItem);

        hr.Cells.Add(hc);
        ht.Rows.Add(hr);

        return ht;
    }

    private string GetTemplateSectionsTable(DataTable dtTemplateData)
    {
        string strTemplateData;
        if (dtTemplateData.Rows.Count > 0)
        {
            strTemplateData = "<Table class=\"header1_table\" width=600 rules=\"all\" border=\"1\" style=\"border:solid 1px black;border-collapse:collapse;\">\n";
            strTemplateData += "<TR class=header1_table_first_row>\n";
            strTemplateData += "<TD width=35%>FeatureName</TD><TD width=25%>Author</TD><TD width=25%>NodeName</TD>\n";
            strTemplateData += "</TR>\n";

            strTemplateData += "<tr><td colspan='3'><table id='Clinic' width='100%'>";

            for (int intLoop = 0; intLoop < dtTemplateData.Rows.Count; intLoop++)
            {
                if ((intLoop + 1) % 2 == 0)
                    strTemplateData += "<TR class='header1_tr_even_row'>\n";
                else
                    strTemplateData += "<TR class='header1_tr_odd_row'>\n";

                for (int intLoop2 = 1; intLoop2 < dtTemplateData.Columns.Count; intLoop2++)
                {
                    if (intLoop2 == 1)
                    {
                        strTemplateData += "<TD width=35%>\n";
                    }
                    else if (intLoop2 == 2)
                    {
                        strTemplateData += "<TD width=25%>\n";
                    }
                    else
                    {
                        strTemplateData += "<TD width=25%>\n";
                    }
                    strTemplateData += dtTemplateData.Rows[intLoop][intLoop2].ToString() + "\n";
                    strTemplateData += "</TD>\n";
                }
                strTemplateData += "</TR>\n";
            }
            strTemplateData += "</td></tr></table>";


            strTemplateData += "</Table>\n";
        }
        else 
        {
            strTemplateData = "<Table class=\"header1_table\" width=600 rules=\"all\" border=\"1\" style=\"border:solid 1px black;border-collapse:collapse;\">\n";
            strTemplateData += "<TR class=header1_table_first_row>\n";
            strTemplateData += "<TD width=100%>無資料</TD>\n";
            strTemplateData += "</TR>\n";
            strTemplateData += "<TR class='header1_tr_even_row'>\n";
            strTemplateData += "<TD width=100%>";
            strTemplateData += "點選Edit新增特徵值";
            strTemplateData += "</TD></TR>";
            strTemplateData += "</Table>\n";
        
        }
        
        dtTemplateData.Dispose();

        return (strTemplateData);

    }
   
    #endregion

    public void Delete_Node(string nodeID_to_delete)
    {
        strSQL = "DELETE FeaturevalueTree WHERE cNodeID = '" + strCurrentNodeID.Value + "'";

        hintsDB.ExecuteNonQuery(strSQL);
        //刪除子節點
        strSQL = "SELECT cNodeID FROM FeaturevalueTree WHERE cParentID = '" + strCurrentNodeID.Value + "' ";
        DataTable dt = hintsDB.getDataSet(strSQL).Tables[0];
        foreach (DataRow dr in dt.Rows)
        {
            Delete_Node(dr["cNodeID"].ToString());
        }
        strSQL = "DELETE FeatureSetItem WHERE cNodeID = '" + strCurrentNodeID.Value + "'";

        hintsDB.ExecuteNonQuery(strSQL);
    }

   
}
