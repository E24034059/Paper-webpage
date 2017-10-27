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

public partial class AuthoringTool_CaseEditor_Paper_QuestionGroupTree_QGroupTree : AuthoringTool_BasicForm_BasicForm, ICallbackEventHandler
{
    protected clsHintsDB hintsDB = new clsHintsDB();
    protected string strSQL = "";
    protected string strSpanIDPrefix = "";
    private string message = "";    // for callback
    protected string sCallBackFunctionInvocation = "";
    private string strQuestionFunction = "";
    private string strQuestionType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        this.initPage();
        if (usi.UsingSystem == "")
        {
            usi.UsingSystem = "HINTS";
        }
        if (!this.IsPostBack)
        {
            this.ConstructQuestionGroupTree();
        }

        sCallBackFunctionInvocation = ClientScript.GetCallbackEventReference(this, "message", "show_callback", null);
        //this.ConstructQuestionGroupTree();
        Ajax.Utility.RegisterTypeForAjax(typeof(AuthoringTool_CaseEditor_Paper_QuestionGroupTree_QGroupTree));

        if (hiddenQuestionType.Value == "1" && hiddenQuestionType.Value != "")
            strQuestionType = "選擇題";
        else
            strQuestionType = "問答題";
        if (hiddenQuestionFunction.Value == "New")
        {
            strQuestionFunction = "編輯新問題";
            lbQModeANDFunction.Text = "(" + strQuestionType + "&nbsp;" + strQuestionFunction + ")";
        }
        else
        {
            strQuestionFunction = "修改或刪除問題";
            lbQModeANDFunction.Text = "(" + strQuestionFunction + ")";
        }

    }

    #region swakevin function
    protected void initPage()
    {
        //Opener
        if (Session["Opener"] != null)
        {
            hiddenOpener.Value = Session["Opener"].ToString();
        }

        //Setup opener
        if (Session["Opener"] != null)
        {
            Session["Opener"] = "./QuestionGroupTree/QuestionGroupTree";
        }
        else
        {
            Session.Add("Opener", "./QuestionGroupTree/QuestionGroupTree");
        }

        //PresentType
        if (Session["PresentType"] != null)
        {
            hiddenPresentType.Value = Session["PresentType"].ToString();
        }

        //QuestionMode
        if (Session["QuestionMode"] != null)
        {
            hiddenQuestionMode.Value = Session["QuestionMode"].ToString();
        }

        //QuestionType
        if (Session["QuestionType"] != null)
        {
            hiddenQuestionType.Value = Session["QuestionType"].ToString();
        }

        //EditMode
        if (Session["EditMode"] != null)
        {
            hiddenEditMode.Value = Session["EditMode"].ToString();
        }

        //ModifyType
        if (Session["ModifyType"] != null)
        {
            hiddenModifyType.Value = Session["ModifyType"].ToString();
        }

        //QuestionFunction
        if (Session["QuestionFunction"] != null)
        {
            hiddenQuestionFunction.Value = Session["QuestionFunction"].ToString();
        }
    }
    #endregion

    #region question group tree construction
    protected void ConstructQuestionGroupTree()
    {
        tvQuestionGroup.Nodes.Clear();
        tvMoveGroup.Nodes.Clear();

        strSpanIDPrefix = "display";
        tvQuestionGroup.Nodes.Add(getTreeForPersonal("Personalroot", "Personal"));
        tvQuestionGroup.Nodes.Add(getTree("HINTSroot", "HINTS"));
        tvQuestionGroup.Nodes.Add(getTree("MLASroot", "MLAS"));
        strSpanIDPrefix = "move";
        tvMoveGroup.Nodes.Add(getTree("HINTSroot", "HINTS"));

        //tvQuestionGroup.ExpandDepth = -1;
        tvQuestionGroup.ExpandAll();
        tvMoveGroup.ExpandAll();
    }
    public DataTable getQuestionGroupNode(object cNodeID, object cParentNodeID, object cNodeType)
    {
        DataTable dtResult = new DataTable();
        strSQL = "SELECT * FROM QuestionGroupTree WHERE cNodeID LIKE '" + cNodeID + "' AND cParentID LIKE '" + cParentNodeID + "' AND cNodeType LIKE '" + cNodeType + "'";
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
    //取得所有公開的題庫
    public DataTable getQuestionGroupNodeForPublic(object cNodeID, object cParentNodeID, object cNodeType)
    {
        DataTable dtResult = new DataTable();
        strSQL = "SELECT * FROM QuestionGroupTree WHERE cNodeID LIKE '" + cNodeID + "' AND cParentID LIKE '" + cParentNodeID + "' AND cNodeType LIKE '" + cNodeType + "' AND cAuthor != '" + usi.UserID + "' AND cMode LIKE '0'";
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
    //取得所有私人的題庫
    public DataTable getQuestionGroupNodeForPersonal(object cNodeID, object cParentNodeID, object cNodeType)
    {
        DataTable dtResult = new DataTable();
        strSQL = "SELECT * FROM QuestionGroupTree WHERE cNodeID LIKE '" + cNodeID + "' AND cParentID LIKE '" + cParentNodeID + "' AND cNodeType LIKE '" + cNodeType + "' AND cAuthor = '" + usi.UserID + "'";
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

    public TreeNode getTree(string nodeID, string nodeName)
    {
        //取得node的mode與作者
        DataTable dtNodeID = new DataTable();
        string strSQL_NodeID = "SELECT * FROM QuestionGroupTree WHERE cNodeID LIKE '" + nodeID + "' AND cNodeName LIKE '" + nodeName + "'";
        dtNodeID = hintsDB.getDataSet(strSQL_NodeID).Tables[0];
        string strMode = "";
        string strAuthor = "";
        string strNodeName = "";

        string strGroupNum = GetGroupNum(nodeID);

        TreeNode node = new TreeNode();
        if (dtNodeID.Rows.Count > 0)
        {
            strMode = dtNodeID.Rows[0]["cMode"].ToString();
            strAuthor = dtNodeID.Rows[0]["cAuthor"].ToString();
            strNodeName = dtNodeID.Rows[0]["cNodeName"].ToString();

            node.Value = nodeID;
            node.Text = "<span id='" + strSpanIDPrefix + "_" + nodeID + "'>" + nodeName + "(" + strGroupNum + ")</span>";
            node.NavigateUrl = "javascript:SelectNode('" + nodeID + "','" + strMode + "','" + strAuthor + "')";
        }
        else
        {
            node.Value = nodeID;
            node.Text = "<span id='" + strSpanIDPrefix + "_" + nodeID + "Public'>" + nodeName + "</span>";
            node.NavigateUrl = "javascript:SelectNode('" + nodeID + "Public','" + strMode + "','" + strAuthor + "')";
        }

        //node.ImageUrl = "checked.gif";

        //判斷node是否還有children node
        DataTable dtChildren = this.getQuestionGroupNodeForPublic("%", nodeID, "%");
        if (dtChildren.Rows.Count > 0)
        {
            foreach (DataRow drData in dtChildren.Rows)
            {
                node.ChildNodes.Add(getTree(drData["cNodeID"].ToString(), drData["cNodeName"].ToString()));
            }
        }
        return node;
    }
    //設定個人的樹包含的系統node
    public TreeNode getTreeForPersonal(string nodeID, string nodeName)
    {
        //取得node的mode與作者
        DataTable dtNodeID = new DataTable();
        string strSQL_NodeID = "SELECT * FROM QuestionGroupTree WHERE cNodeID LIKE '" + nodeID + "' AND cNodeName LIKE '" + nodeName + "'";
        dtNodeID = hintsDB.getDataSet(strSQL_NodeID).Tables[0];
        string strMode = "";
        string strAuthor = "";
        string strNodeName = "";
        if (dtNodeID.Rows.Count > 0)
        {
            strMode = dtNodeID.Rows[0]["cMode"].ToString();
            strAuthor = dtNodeID.Rows[0]["cAuthor"].ToString();
            strNodeName = dtNodeID.Rows[0]["cNodeName"].ToString();
        }

        TreeNode node = new TreeNode();
        node.Value = nodeID;
        node.Text = "<span id='" + strSpanIDPrefix + "_" + nodeID + "'>" + nodeName + "</span>";
        node.NavigateUrl = "javascript:SelectNode('" + nodeID + "','" + strMode + "','" + strAuthor + "')";

        //node.ImageUrl = "checked.gif";

        node.ChildNodes.Add(getTreeForPersonalChildren("HINTSroot", "HINTS"));
        node.ChildNodes.Add(getTreeForPersonalChildren("MLASroot", "MLAS"));

        return node;
    }
    //設定個人的樹包含的系統node 屬於此node的所有子node
    public TreeNode getTreeForPersonalChildren(string nodeID, string nodeName)
    {
        //取得node的mode與作者
        DataTable dtNodeID = new DataTable();
        string strSQL_NodeID = "SELECT * FROM QuestionGroupTree WHERE cNodeID LIKE '" + nodeID + "' AND cNodeName LIKE '" + nodeName + "' AND cAuthor = '" + usi.UserID + "'";
        dtNodeID = hintsDB.getDataSet(strSQL_NodeID).Tables[0];
        string strMode = "";
        string strAuthor = "";
        string strNodeName = "";
        if (dtNodeID.Rows.Count > 0)
        {
            strMode = dtNodeID.Rows[0]["cMode"].ToString();
            strAuthor = dtNodeID.Rows[0]["cAuthor"].ToString();
            strNodeName = dtNodeID.Rows[0]["cNodeName"].ToString();
        }
        //取得群組的題數
        string strGroupNum = GetGroupNum(nodeID);

        TreeNode node = new TreeNode();
        node.Value = nodeID;
        node.Text = "<span id='" + strSpanIDPrefix + "_" + nodeID + "'>" + nodeName + "(" + strGroupNum + ")</span>";

        node.NavigateUrl = "javascript:SelectNode('" + nodeID + "','" + strMode + "','" + strAuthor + "')";

        if (dtNodeID.Rows.Count > 0)
        {
            //判斷此node是否為public
            if (dtNodeID.Rows[0]["cMode"].ToString() == "0")
                node.ImageUrl = "share.gif";
        }

        //判斷node是否還有children node
        DataTable dtChildren = this.getQuestionGroupNodeForPersonal("%", nodeID, "%");
        if (dtChildren.Rows.Count > 0)
        {
            foreach (DataRow drData in dtChildren.Rows)
            {
                node.ChildNodes.Add(getTreeForPersonalChildren(drData["cNodeID"].ToString(), drData["cNodeName"].ToString()));
            }
        }
        return node;
    }
    #endregion

    #region Question group tree operation

    #region Modify question group
    protected void btModifyGroupSubmit_Click(object sender, EventArgs e)
    {
        strSQL = "UPDATE QuestionGroupTree SET cNodeName='" + tNodeName.Value + "', cMode='" + ddlNodeMode.SelectedValue + "' WHERE cNodeID='" + strCurrentNodeID.Value + "'";
        try
        {
            hintsDB.ExecuteNonQuery(strSQL);
        }
        catch
        {
            this.RegisterClientScriptBlock("", "<script> alert('error'); </script>");
        }

        this.ConstructQuestionGroupTree();
    }
    #endregion
    #region add question group
    protected void btAddGroupSubmit_Click(object sender, EventArgs e)
    {
        strSQL = "INSERT INTO QuestionGroupTree VALUES('" + this.getNewNodeID("") + "','" + strCurrentNodeID.Value + "','" + tNewNodeName.Text + "','" + usi.UsingSystem + "','" + ddlNewNodeMode.SelectedValue + "','" + usi.UserID + "')";
        try
        {
            hintsDB.ExecuteNonQuery(strSQL);
        }
        catch
        {
            this.RegisterClientScriptBlock("", "<script> alert('error'); </script>");
        }

        this.ConstructQuestionGroupTree();
    }

    private string getNewNodeID(string folder_id_to_add)
    {
        string strNewID;
        int intTemp = 0;
        DateTime dtNow = DateTime.Now;
        while (dtNow.AddSeconds(0.1) < DateTime.Now)
            intTemp++;
        strNewID = "Group_" + DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
        return (strNewID);
    }
    #endregion
    #region delete question group
    protected void btDeletNodeSubmit_Click(object sender, EventArgs e)
    {
        this.Delete_Node(strCurrentNodeID.Value);
        this.ConstructQuestionGroupTree();
    }

    public void Delete_Node(string nodeID_to_delete)
    {
        strSQL = "DELETE QuestionGroupTree WHERE cNodeID = '" + nodeID_to_delete + "'";

        hintsDB.ExecuteNonQuery(strSQL);
        //刪除子節點
        strSQL = "SELECT cNodeID FROM QuestionGroupTree WHERE cParentID = '" + nodeID_to_delete + "' AND cAuthor = '" + usi.UserID + "'";
        DataTable dt = hintsDB.getDataSet(strSQL).Tables[0];
        foreach (DataRow dr in dt.Rows)
        {
            Delete_Node(dr["cNodeID"].ToString());
        }
    }
    #endregion

    #endregion

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
            DataTable dtNode = this.getQuestionGroupNode(message, "%", "%");
            nodeName = dtNode.Rows[0]["cNodeName"].ToString();
        }
        catch
        {
            nodeName = "";
        }

        //lNodeID.Text = message;
        //lParentNodeName.Text = nodeName;
        //tNodeName.Text = nodeName;
        //tNodeName.Value = nodeName;
        return nodeName;
    }
    #endregion


    protected void btBack_Click(object sender, EventArgs e)
    {
        string strOpener = "";
        if (hiddenOpener.Value != null)
        {
            strOpener = hiddenOpener.Value;
        }

        string strQuestionMode = "";
        if (hiddenQuestionMode.Value != null)
        {
            strQuestionMode = hiddenQuestionMode.Value;
        }

        string strModifyType = "";
        if (hiddenModifyType.Value != null)
        {
            strModifyType = hiddenModifyType.Value;
        }

        string prePage = "";
        if (strModifyType == "Paper")
        {
            //編輯考卷
            if (strOpener == "Paper_QuestionMode")
            {
                prePage = "../Paper_QuestionMode.aspx";
            }
            else if (strOpener == "Paper_OtherQuestion")
            {
                prePage = "../Paper_OtherQuestion.aspx";
            }
            else if (strOpener == "Paper_PresentMethod")
            {
                prePage = "../Paper_PresentMethod.aspx";
            }
            else if (strOpener == "Paper_EditMethod")
            {
                prePage = "../Paper_EditMethod.aspx";
            }
            else if (strOpener == "Paper_MainPage")
            {
                prePage = "../Paper_MainPage.aspx";
            }
            else if (strOpener == "Paper_QuestionMain")
            {
                prePage = "../Paper_QuestionMain.aspx";
            }
            else
            {
                prePage = "../Paper_QuestionMode.aspx";
            }
        }
        else
        {
            //編輯題目
            prePage = "../Paper_QuestionMain.aspx";
        }

        Response.Redirect(prePage);
    }

    protected void btSubmit_Click(object sender, EventArgs e)
    {
        string strPresentType = "";
        if (hiddenPresentType.Value != null)
        {
            strPresentType = hiddenPresentType.Value;
        }

        string strEditMode = "";
        if (hiddenEditMode.Value != null)
        {
            strEditMode = hiddenEditMode.Value;
        }

        string strGroupID = "";
        if (strCurrentNodeID.Value != null)
        {
            strGroupID = strCurrentNodeID.Value;
        }

        string strModifyType = "";
        if (hiddenModifyType.Value != null)
        {
            strModifyType = hiddenModifyType.Value;
        }

        string nextPage = "";
        if (strModifyType == "Paper")
        {
            //編輯考卷
            if (strPresentType == "Edit")
            {
                if (strEditMode == "Automatic")
                {
                    nextPage = "../Paper_RandomSelect.aspx?GroupID=" + strGroupID;
                }
                else
                {
                    nextPage = "../Paper_SelectQuestion.aspx?GroupID=" + strGroupID;
                }
            }
            else
            {
                nextPage = "../Paper_RandomSelect.aspx?GroupID=" + strGroupID;
            }
        }
        else
        {
            //編輯題目
            string strQuestionFunction = "";
            if (hiddenQuestionFunction.Value != null)
            {
                strQuestionFunction = hiddenQuestionFunction.Value;
            }

            if (strQuestionFunction == "New")
            {
                //新編題目
                if (hiddenQuestionType.Value == "1")
                {
                    //選擇題
                    nextPage = "../CommonQuestionEdit/Page/ShowQuestion.aspx?GroupID=" + strGroupID;
                }
                else
                {
                    //問答題
                    nextPage = "../Paper_TextQuestionEditor.aspx?GroupID=" + strGroupID;
                }
            }
            else
            {
                //編輯或刪除題目
                nextPage = "../Paper_QuestionView.aspx?GroupID=" + strGroupID;

            }
        }

        Response.Redirect(nextPage);
    }

    //check是否有權限修改node
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public bool bIsPrivateNode(string strNodeID)
    {
        Initiate();
        string strSQL_NodeID = "Select * From QuestionGroupTree Where cNodeID = '" + strNodeID + "'";
        DataTable dtQuestionGroupTree = hintsDB.getDataSet(strSQL_NodeID).Tables[0];
        //若不為樹根 則判斷使用者是否相同
        if (dtQuestionGroupTree.Rows.Count > 0)
        {
            string strUser = dtQuestionGroupTree.Rows[0]["cAuthor"].ToString();
            if (strUser == usi.UserID)
                return true;
            else
                return false;
        }
        //若為樹根 則判斷是Public還是Personal
        else
        {
            if (strNodeID.IndexOf("Public", 0) != -1)
                return false;
            else
                return true;

        }
    }

    //取得群組有多少題目
    private string GetGroupNum(string nodeID)
    {
        DataTable dtGroupNum = new DataTable();
        string strSQL_GroupNum = "SELECT COUNT(*) AS Num FROM QuestionMode WHERE (cQuestionGroupID = '" + nodeID + "')";
        dtGroupNum = hintsDB.getDataSet(strSQL_GroupNum).Tables[0];
        string strGroupNum = "";
        if (dtGroupNum.Rows.Count > 0)
        {
            strGroupNum = dtGroupNum.Rows[0]["Num"].ToString();
        }
        return strGroupNum;
    }
}
