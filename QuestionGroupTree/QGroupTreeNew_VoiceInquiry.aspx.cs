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
using System.Drawing;

public partial class AuthoringTool_CaseEditor_Paper_QuestionGroupTree_QGroupTreeNew_VoiceInquiry : AuthoringTool_BasicForm_BasicForm, ICallbackEventHandler
{
    protected clsHintsDB hintsDB = new clsHintsDB();
    protected string strSQL = "";
    protected string strSpanIDPrefix = "";
    private string message = "";    // for callback
    protected string sCallBackFunctionInvocation = "";
    protected bool flag = false;    // 增加判斷是否顯示基本題目表的flag   老詹 2014/10/22 

    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(AuthoringTool_CaseEditor_Paper_QuestionGroupTree_QGroupTreeNew_VoiceInquiry));
        this.initPage();
        if (usi.UsingSystem == "")
        {
            usi.UsingSystem = "HINTS";
        }
        if (!this.IsPostBack)
        {
            if (Session["bDisplayQuestionList"] != null) // 增加若顯示基本題目表時的動作   老詹 2014/10/21
            {
                if (Session["bDisplayQuestionList"].ToString() == " Yes ")
                {
                    Lb_HintsForQuestionList.Visible = true;
                    flag = true;
                }
                bDisplayBasicQuestionList.Value = Session["bDisplayQuestionList"].ToString();
            }         
            this.ConstructQuestionGroupTree();

            if (Session["SelectedNodeForRecover"] != null)
            {
                HiddenForNode.Value = Session["SelectedNodeForRecover"].ToString();
                ConstructBasicQuestionList();             
            }
            HF_CaseID.Value = usi.CaseID;           
        }       
        AddnewtbServer.ServerClick += new EventHandler(AddnewtbServer_ServerClick);
        ConstructBasicQuestionList(); // 防止基本題目表Function在點擊內部的button後消失   老詹 2014/10/22
        sCallBackFunctionInvocation = ClientScript.GetCallbackEventReference(this, "message", "show_callback", null);

        if (Session["SelectedGroupForRecover"] != null)
        {
            string strTmp = Session["SelectedGroupForRecover"].ToString();
            Label lbSelectedQuesGroup = (Label)this.FindControl(strTmp);
            if (lbSelectedQuesGroup != null)
            {
                lbSelectedQuesGroup.BackColor = Color.Pink;
                Lb_CurrentSelected.Text = "目前選擇的問題主題：<span style='color:red;'>" + lbSelectedQuesGroup.Text + "</span>";
                strCurrentQuestionRowID.Value = lbSelectedQuesGroup.ID;
                btSubmit.Disabled = false;
                btSubmit.Attributes.Add("class", "button_continue");
            }           
        }
        else
        {
            Lb_CurrentSelected.Text = "";
            strCurrentQuestionRowID.Value = "";
            btSubmit.Disabled = true;
            btSubmit.Attributes.Add("class", "button_gray");
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

        //string strCareer = Request.QueryString["Career"];

        flag = true;
        if (GetProfession() == "醫學院") // 如果是醫學院的ID, 就建置醫學院領域這棵樹
            tvQuestionGroup.Nodes.Add(getTree("HINTSroot", "醫學院"));
        else
            tvQuestionGroup.Nodes.Add(getTree("職業問答樹", "職業問答樹"));
            

        TreeNode tnRoot = new TreeNode();
        TreeNode tnNode = new TreeNode();

        tnRoot = tvQuestionGroup.Nodes[0];
        tnRoot.Expanded = true;

        for (int nIdx = 0; nIdx < tnRoot.ChildNodes.Count; nIdx++)
        {
             tnNode = tvQuestionGroup.Nodes[0].ChildNodes[nIdx];
             tnNode.Expand();
        }

        tvQuestionGroup.ExpandAll();
    }

    //取得此Case的Profession
    protected String GetProfession()
    {
        string strSQL = "SELECT * FROM SetVoiceInquiryTreeByTeacher WHERE cCaseID = '" + usi.CaseID + "'";

        DataTable dt = new DataTable();

        dt = hintsDB.getDataSet(strSQL).Tables[0];

        return dt.Rows[0]["cNodeID"].ToString() ;
    }

    //取得所有的題庫
    public DataTable getQuestionGroupNodeForAll(object cNodeID, object cParentNodeID)
    {
        string strCareer = GetProfession();
        DataTable dtResult = new DataTable();
        if (cParentNodeID == "職業問答樹")
            strSQL = "SELECT * FROM QuestionGroupTree WHERE cNodeID LIKE '" + (cNodeID + strCareer) + "' AND cParentID LIKE '" + cParentNodeID + "' ORDER BY  cNodeID asc";
        else
            strSQL = "SELECT * FROM QuestionGroupTree WHERE cNodeID LIKE '" + cNodeID + "' AND cParentID LIKE '" + cParentNodeID + "' ORDER BY  cNodeID asc";
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
        string strSQL_NodeID = "SELECT * FROM QuestionGroupTree WHERE cNodeID LIKE '" + nodeID + "' AND cNodeName LIKE '" + nodeName + "' ORDER BY cNodeID ASC";
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
            //node.NavigateUrl = "javascript:SelectNode('" + nodeID + "','" + strAuthor + "','" + flag + "')";
        }
        else
        {
            node.Value = nodeID ;
            node.Text = "<span id='" + strSpanIDPrefix + "_" + nodeID + "'>" + nodeName + "</span>";
            //node.NavigateUrl = "javascript:SelectNode('" + nodeID + "','" + strAuthor + "','" + flag + "')";
        }

        //判斷node是否還有children node
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
    #endregion

    #region Question group tree operation

    #region Modify question group
    protected void btModifyGroupSubmit_Click(object sender, EventArgs e)
    {
        strSQL = "UPDATE QuestionGroupTree SET cNodeName='" + tNodeName.Value + "' WHERE cNodeID='" + strCurrentNodeID.Value + "'";
        try
        {
            hintsDB.ExecuteNonQuery(strSQL);
        }
        catch
        {
            this.RegisterClientScriptBlock("", "<script> alert('error'); </script>");
        }

        this.ConstructQuestionGroupTree();
        //Response.Redirect("QGroupTree.aspx");
    }
    #endregion
    #region add question group
    protected void btAddGroupSubmit_Click(object sender, EventArgs e) //修改INSERT的SQL 老詹 2014/08/15
    {
        string[] strTmpArr = tNewNodeName.Text.Split(',');
        for (int i = 0; i < strTmpArr.Length; i++)
        {
            if(strTmpArr[i]!="")
            {
                strSQL = "INSERT INTO QuestionGroupTree (cNodeID,cParentID,cNodeName,cNodeType,cAuthor,qId) VALUES('" + this.getNewNodeID("") + "','" + strCurrentNodeID.Value + "','" + strTmpArr[i] + "', 'HINTS', '" + usi.UserID + "', '25')";
                try
                {
                    hintsDB.ExecuteNonQuery(strSQL);
                }
                catch
                {
                    this.RegisterClientScriptBlock("", "<script> alert('error'); </script>");
                }
            }
        }           

        this.ConstructQuestionGroupTree();
        // Response.Redirect("QGroupTree.aspx");
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
        //Response.Redirect("QGroupTree.aspx");       
    }

    public void Delete_Node(string nodeID_to_delete)
    {
        string strProSQL = "DELETE ProblemTypeTree WHERE cNodeName = '" + nodeID_to_delete + "'";
        hintsDB.ExecuteNonQuery(strProSQL);
        strSQL = "DELETE QuestionGroupTree WHERE cNodeID = '" + nodeID_to_delete + "'";
        hintsDB.ExecuteNonQuery(strSQL);
        //刪除子節點
        strSQL = "SELECT cNodeID FROM QuestionGroupTree WHERE cParentID = '" + nodeID_to_delete + "' ";
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

    #region 建立基本題目表的所有事件   老詹  2014/10/26
    private TreeNode FindNode(TreeNode tnParent, string strValue)// 尋找欲縮起來的節點Function
    {
        if (tnParent == null)
        {
            return null;
        }
        if (tnParent.Value == strValue)
        {
            return tnParent;
        }
        TreeNode tnChild = new TreeNode();
        foreach (TreeNode tnRet in tnParent.ChildNodes)
        {
            tnChild = FindNode(tnRet, strValue);
            if (tnChild != null) break;
        }
        return tnChild;
    }

    private void AddnewtbServer_ServerClick(object sender, EventArgs e)
    {
        //ConstructBasicQuestionList();
    }

    protected void ConstructBasicQuestionList() // 建立基本題目表Function   老詹 2014/10/22
    {
        BasicQuestionList.Controls.Clear(); // 創建前先清除，防止一開始建出重複表格

        DataTable dtTmp = new DataTable(); // 傳過來的HiddenFild是點選的NodeID
        strSQL = "SELECT cNodeID FROM QuestionGroupTree WHERE cNodeID LIKE '" + HiddenForNode.Value.ToString() + "' ORDER BY  cNodeID asc";
        dtTmp = hintsDB.getDataSet(strSQL).Tables[0];
        if (dtTmp.Rows.Count > 0)
        {
            TreeNode tnRoot = new TreeNode();
            tnRoot = tvQuestionGroup.Nodes[0];
            /*TreeNode SelectedNode = new TreeNode();
            tvQuestionGroup.ExpandAll();
            SelectedNode = FindNode(tnRoot, dtTmp.Rows[0]["cNodeID"].ToString());
            SelectedNode.Collapse();// 點擊後其子節點會縮起來*/

            HtmlTableRow row_Title = new HtmlTableRow();
            HtmlTableCell cell_Title = new HtmlTableCell();
            cell_Title.ColSpan = 2;
            cell_Title.Align = "middle";
            cell_Title.Attributes.Add("style", "padding:10px; background-color:yellow;");
            string strPaperTopicSQL = "SELECT cNodeID,cNodeName FROM QuestionGroupTree WHERE cNodeID LIKE '" + dtTmp.Rows[0]["cNodeID"].ToString() + "' ORDER BY  cNodeID asc";
            DataTable dtPaperTopic = hintsDB.getDataSet(strPaperTopicSQL).Tables[0];
            Label Lb_Title = new Label();
            Lb_Title.Font.Bold = true;
            Lb_Title.Font.Size = 24;
            Lb_Title.Text = "<span>主題：</span>" + "<span style='color:red;'>" + dtPaperTopic.Rows[0]["cNodeName"].ToString() + "</span>" + "<span>〈設定為此活動的故事主題：</span>";
            Lb_Title.ForeColor = Color.Blue;
            HtmlInputCheckBox cbStoryTopic = new HtmlInputCheckBox();
            cbStoryTopic.ID = "cbStoryTopic-" + dtPaperTopic.Rows[0]["cNodeID"].ToString();
            cbStoryTopic.Attributes.Add("class", "bigcheck");
            cbStoryTopic.Attributes.Add("onclick", "ConfirmStoryTopic('" + cbStoryTopic.ID + "');");
            string strRecoverTestStoryTopic = "SELECT cTestStoryTopic FROM SetVoiceInquiryTreeByTeacher WHERE cCaseID='" + usi.CaseID + "'";
            DataTable dtRecoverTestStoryTopic = hintsDB.getDataSet(strRecoverTestStoryTopic).Tables[0];
            if (dtRecoverTestStoryTopic.Rows.Count > 0)
            {
                if (cbStoryTopic.ID.IndexOf(dtRecoverTestStoryTopic.Rows[0]["cTestStoryTopic"].ToString()) >= 0 && dtRecoverTestStoryTopic.Rows[0]["cTestStoryTopic"].ToString() != "")
                {
                    cbStoryTopic.Checked = true;
                    btnEditNext.Disabled = false;
                    btnEditNext.Attributes.Add("class", "button_continue");
                }
                else
                {
                    btnEditNext.Disabled = true;
                    btnEditNext.Attributes.Add("class", "button_gray");
                }
            }
            Label Lb_Title2 = new Label();
            Lb_Title2.Font.Bold = true;
            Lb_Title2.Text = "<span>〉</span>";
            Lb_Title2.Font.Size = 24;
            Lb_Title2.ForeColor = Color.Blue;
            HtmlTableRow row_Title2 = new HtmlTableRow();
            HtmlTableCell cell_Title2 = new HtmlTableCell();
            cell_Title2.ColSpan = 2;
            cell_Title2.Attributes.Add("style", "padding:10px; align:left;");
            Label Lb_Space1 = new Label();
            Lb_Space1.ForeColor = Color.Red;
            Lb_Space1.Text = "Hints:<br/>1. 若要編輯問題主題內容，請選擇表格內的問題主題，再點擊下方中間之「編輯問題主題在題庫中的問題」進入編輯頁面。";
            Label Lb_Space2 = new Label();
            Lb_Space2.ForeColor = Color.Red;
            Lb_Space2.Text = "<br/>2. 當問題主題內容編輯完畢，再點擊右下方之「選擇此活動中要使用的問題」進入編輯頁面。";
            /*HtmlInputButton btnEditTestPaper = new HtmlInputButton();
            btnEditTestPaper.Style["width"] = "150px";
            btnEditTestPaper.ID = "btnEditTestPaper-" + dtTmp.Rows[0]["cNodeID"].ToString();
            btnEditTestPaper.Value = "Edit Test Paper";
            string strCareer = Request.QueryString["Career"].ToString();
            btnEditTestPaper.Attributes.Add("onclick", "GoToEditBQL('" + dtTmp.Rows[0]["cNodeID"].ToString() + "','"+ strCareer +"', '"+ usi.CaseID +"');");
            btnEditTestPaper.Attributes.Add("class", "button_continue");*/
            cell_Title.Controls.Add(Lb_Title);
            cell_Title.Controls.Add(cbStoryTopic);
            cell_Title.Controls.Add(Lb_Title2);
            row_Title.Cells.Add(cell_Title);
            cell_Title2.Controls.Add(Lb_Space1);
            //cell_Title2.Controls.Add(btnEditTestPaper);
            cell_Title2.Controls.Add(Lb_Space2);
            row_Title2.Cells.Add(cell_Title2);
            BasicQuestionList.Rows.Add(row_Title);
            BasicQuestionList.Rows.Add(row_Title2);
            DataTable dtChildren = new DataTable();
            strSQL = "SELECT * FROM QuestionGroupTree WHERE cParentID LIKE '" + dtTmp.Rows[0]["cNodeID"].ToString() + "' ORDER BY  cNodeID asc";
            dtChildren = hintsDB.getDataSet(strSQL).Tables[0];

            string[] childArrayID = new string[dtChildren.Rows.Count];
            string strTotalID = "";
            for (int i = 0; i < dtChildren.Rows.Count; i++)
            {
                childArrayID[i] = dtChildren.Rows[i]["cNodeID"].ToString();
                strTotalID += (childArrayID[i] + "/");
            }
            HiddenForAllID.Value = strTotalID;           
            for (int i = 0; i < dtChildren.Rows.Count; i++)
            {
                HtmlTableRow row_Child = new HtmlTableRow();
                HtmlTableCell cell_Child = new HtmlTableCell();
                cell_Child.BorderColor = "#000000";
                cell_Child.Attributes.Add("style", "padding:10px; height:50px;");
                Label child = new Label();
                child.ID = dtChildren.Rows[i]["cNodeID"].ToString();
                child.Text = dtChildren.Rows[i]["cNodeName"].ToString();
                child.Style["cursor"] = "pointer";
                child.Font.Underline = true;
                child.Attributes.Add("onclick", "ShowSelected('" + child.ID + "' , '" + child.Text + "')");
                cell_Child.Controls.Add(child);
                row_Child.Cells.Add(cell_Child);
                BasicQuestionList.Rows.Add(row_Child);
            }
        }
        //復原tdParentNodeName  老詹 2015/06/27
        string strRecover = "";
        if (HiddenForNode.Value.ToString().IndexOf("Group") >= 0)
            strRecover = "SELECT cNodeID,cNodeName FROM QuestionGroupTree WHERE cNodeID LIKE '" + HiddenForNode.Value.ToString() + "'";
        else
            strRecover = "SELECT cNodeID,cNodeName FROM ProblemTypeTree WHERE cNodeName LIKE '" + HiddenForNode.Value.ToString() + "'";
        DataTable dtRecover = hintsDB.getDataSet(strRecover).Tables[0];
        if (dtRecover.Rows.Count > 0)
        {
            tdParentNodeName.InnerText = dtRecover.Rows[0]["cNodeName"].ToString();
            tdNodeID.InnerText = dtRecover.Rows[0]["cNodeID"].ToString();
        }
    }
    #endregion

    #region 改成OnClientClick事件  老詹 2015/06/12
    /*protected void btBack_Click(object sender, EventArgs e)
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
            //prePage = "../Paper_QuestionMainNew.aspx?Career=" + HF_Career.Value.ToString();
        }
        Response.Redirect(prePage);
    }*/

    /*protected void btSubmit_Click(object sender, EventArgs e)
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
            // 增加判斷，看是選取Tree的Case還是選取題目表的Case   老詹 2014/10/24
            if (Session["bDisplayQuestionList"].ToString() == " No ")
                strGroupID = strCurrentNodeID.Value;
            else
                strGroupID = strCurrentQuestionRowID.Value;
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
                    //判斷目前選擇的群組的root 是否為 SubjectiveRoot ObjectiveRoot AssessmentRoot PlanRoot
                    //是則用新的問答題資料表的新頁面 否則用舊的
                    hfRoot.Value = "false";
                    QuestionGroupTree_SELECT_Root(strGroupID);
                    if (hfRoot.Value == "true")
                    {
                        //問答題
                        nextPage = "../Paper_TextQuestionEditorNew.aspx?GroupID=" + strGroupID;
                    }
                    else
                    {
                        //問答題
                        //nextPage = "../Paper_TextQuestionEditor.aspx?GroupID=" + strGroupID;
                        nextPage = "../Paper_TextQuestionEditorNew.aspx?GroupID=" + strGroupID;
                    }
                }
            }
            else
            {
                //判斷目前選擇的群組的root 是否為 SubjectiveRoot ObjectiveRoot AssessmentRoot PlanRoot
                //是則用新的問答題資料表的新頁面 否則用舊的
                hfRoot.Value = "false";
                QuestionGroupTree_SELECT_Root(strGroupID);
                string strCareer = HF_Career.Value.ToString();
                
                if (hfRoot.Value == "true")
                {
                    nextPage = "../Paper_QuestionViewNew.aspx?GroupID=" + strGroupID + "&Career=" + strCareer + "&bDisplayBQL=" + Session["bDisplayQuestionList"].ToString();
                }
                else
                {
                    //編輯或刪除題目
                    //nextPage = "../Paper_QuestionView.aspx?GroupID=" + strGroupID;
                    //nextPage = "../Paper_QuestionViewNew.aspx?GroupID=" + strGroupID + "&Career=" + strCareer + "&bDisplayBQL=" + Session["bDisplayQuestionList"].ToString();
                }
            }
        }

        Response.Redirect(nextPage);
    }*/
    #endregion

    private void QuestionGroupTree_SELECT_Root(string strNodeID)
    {
        string strRootName = "";
        DataTable dtQuestionGroupTree = new DataTable();
        string strSQL_QuestionGroupTree = "SELECT * FROM QuestionGroupTree WHERE cNodeID = '" + strNodeID + "' ";
        dtQuestionGroupTree = hintsDB.getDataSet(strSQL_QuestionGroupTree).Tables[0];
        if (dtQuestionGroupTree.Rows.Count > 0)
        {
            strRootName = dtQuestionGroupTree.Rows[0]["cNodeName"].ToString();
            if (strRootName == "SubjectiveRoot" || strRootName == "ObjectiveRoot" || strRootName == "AssessmentRoot" || strRootName == "PlanRoot")
            {
                hfRoot.Value = "true";
            }
            else
            {
                string strParentID = "";
                strParentID = dtQuestionGroupTree.Rows[0]["cParentID"].ToString();
                QuestionGroupTree_SELECT_Root(strParentID);
            }
        }
    }
    protected void tvQuestionGroup_SelectedNodeChanged(object sender, EventArgs e)
    {
        TreeView tmpTreeView = (TreeView)sender;
        Session["SelectedNodeForRecover"] = tmpTreeView.SelectedNode.Value; //為了在題目編輯頁面時返回能記錄
        HiddenForNode.Value = strCurrentNodeID.Value = tmpTreeView.SelectedNode.Value;
        if (tmpTreeView.SelectedNode.Value != "職業問答樹")
        {
            ConstructBasicQuestionList();
            string strRecoverTestStoryTopic = "SELECT cTestStoryTopic FROM SetVoiceInquiryTreeByTeacher WHERE cCaseID='" + usi.CaseID + "'";
            DataTable dtRecoverTestStoryTopic = hintsDB.getDataSet(strRecoverTestStoryTopic).Tables[0];
            if (dtRecoverTestStoryTopic.Rows.Count > 0)
            {
                HtmlInputCheckBox cbTmp = (HtmlInputCheckBox)this.FindControl("cbStoryTopic-" + dtRecoverTestStoryTopic.Rows[0]["cTestStoryTopic"].ToString());
                if (cbTmp != null)
                {
                    cbTmp.Checked = true;
                    btnEditNext.Disabled = false;
                    btnEditNext.Attributes.Add("class", "button_continue");
                }
                else
                {
                    btSubmit.Disabled = true;
                    btSubmit.Attributes.Add("class", "button_gray");
                }
            }
            Session["SelectedGroupForRecover"] = null;
            Lb_CurrentSelected.Text = "";
        }
    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string GetAllPara(string strGroupID, string strCaseID, string strCareer)
    {
        string strReturn = "";
        DataTable dtTmp = new DataTable(); // 傳過來的HiddenFild是點選的NodeID
        strSQL = "SELECT cNodeID FROM QuestionGroupTree WHERE cNodeID LIKE '" + strGroupID + "' ORDER BY  cNodeID asc";
        dtTmp = hintsDB.getDataSet(strSQL).Tables[0];
        strReturn = dtTmp.Rows[0]["cNodeID"].ToString() + "|" + strCareer + "|" + strCaseID;
        return strReturn;
    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void SaveStoryTopic(string strCaseID, string strGroupID)
    {
        string strUpdateSQL = "UPDATE SetVoiceInquiryTreeByTeacher SET cTestStoryTopic='" + strGroupID + "' WHERE cCaseID = '" + strCaseID + "'";
        hintsDB.ExecuteNonQuery(strUpdateSQL);
    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string CheckIsLeaf(string strGroupID)
    {
        string strReturn = "";
        DataTable dtTmp = new DataTable(); // 傳過來的HiddenFild是點選的NodeID
        strSQL = "SELECT * FROM QuestionGroupTree WHERE cParentID LIKE '" + strGroupID + "' ORDER BY  cNodeID asc";
        dtTmp = hintsDB.getDataSet(strSQL).Tables[0];
        if (dtTmp.Rows.Count > 0)
            strReturn = "False";
        else
            strReturn = "True";
        return strReturn;
    }
}
