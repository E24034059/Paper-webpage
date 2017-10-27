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

public partial class AuthoringTool_CaseEditor_Paper_Default : AuthoringTool_BasicForm_BasicForm
{
    protected clsHintsDB hintsDB = new clsHintsDB();
    string strSpanIDPrefix = "";
    string strSQL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //在btnDeleteQuestion按鈕加入屬性，詢問是否要刪除
        btnDeleteQuestion.Attributes.Add("onclick ", "return confirm( '確認要刪除嗎？');");
        btnDelet.Attributes.Add("onclick ", "return confirm( '確認要刪除嗎？');");
        
        //在btn_search加入javascript屬性
        btn_search.Attributes.Add("onclick", "return ShowDivSearch()");
        
        if (!this.IsPostBack)
        {
            //初始化ddlSelectQuestion下拉式選單
            setddlQusetionList();
            //初始化ddlFeatureSet下拉式選單
            setddlFeatureSet();
        }
    }
    
    #region 初始區
    //初始化ddlQusetionList
    protected void setddlQusetionList()
    {
        //先將ddlQusetionList物件清空
        ddlQusetionList.Items.Clear();
        //將物件放入ddlSelectQuestion下拉式選單
        DataTable dtSelectQuestion = new DataTable();
        string strSQL_SelectQuestion = "SELECT * FROM QuestionList";
        dtSelectQuestion = hintsDB.getDataSet(strSQL_SelectQuestion).Tables[0];

        for (int i = 0; i < dtSelectQuestion.Rows.Count; i++)
        {
            ddlQusetionList.Items.Add(new ListItem(dtSelectQuestion.Rows[i]["qName"].ToString(), dtSelectQuestion.Rows[i]["qId"].ToString()));
        }
    }
    //初始化ddlFeatureSet
    protected void setddlFeatureSet()
    {
        //先將ddlFeatureSet物件清空
        ddlFeatureSet.Items.Clear();
        //預先在ddlFeatureSet增加null的item
        ddlFeatureSet.Items.Add(new ListItem("Null", "0"));
        
        DataTable dtSelectFeatureSet = new DataTable();
        string strSQL_SelectFeatureSet = "SELECT * FROM FeatureSetList";
        dtSelectFeatureSet = hintsDB.getDataSet(strSQL_SelectFeatureSet).Tables[0];

        for (int i = 0; i < dtSelectFeatureSet.Rows.Count; i++)
        {
            ddlFeatureSet.Items.Add(new ListItem(dtSelectFeatureSet.Rows[i]["FeatureSetName"].ToString(), dtSelectFeatureSet.Rows[i]["iFeatureSetID"].ToString()));
        }
        //判斷是否要隱藏按鈕
        checkVisibleForddlFeature();
    }
    //創造屬性樹的方法
    protected void ConstructQuestionGroupTree()
    {
        //先清空原有的題庫樹
        tvFeatureGroup.Nodes.Clear();
        strSpanIDPrefix = "display";

        //創造屬性樹
        tvFeatureGroup.Nodes.Add(getTree("Feature_root", "Feature"));
        //tvFeatureGroup.CollapseAll();

        TreeNode tnRoot = new TreeNode();
        TreeNode tnNode = new TreeNode();

        tnRoot = tvFeatureGroup.Nodes[0];
        tnRoot.Expanded = true;

        /*for (int nIdx = 0; nIdx < tnRoot.ChildNodes.Count; nIdx++)
        {
            tnNode = tvFeatureGroup.Nodes[0].ChildNodes[nIdx];
            tnNode.Expand();
        }
        //將屬性樹展開
        tvFeatureGroup.ExpandAll();*/  
    }
    //得到特徵樹
    public TreeNode getTree(string nodeID, string nodeName)
    {
        //取得node的mode與作者
        DataTable dtNodeID = new DataTable();
        string strSQL_NodeID = "SELECT * FROM FeaturevalueTree WHERE cNodeID LIKE '" + nodeID + "' AND cNodeName LIKE '" + nodeName + "' ORDER BY cNodeID ASC";
        dtNodeID = hintsDB.getDataSet(strSQL_NodeID).Tables[0];
        string strAuthor = "";
        string strNodeName = "";
        TreeNode node = new TreeNode();
        
        //判斷node是否還有children node
        DataTable dtChildren = this.getQuestionGroupNodeForAll("%", nodeID);
        if (dtChildren.Rows.Count > 0)
        {
            node.Expanded = true;
            foreach (DataRow drData in dtChildren.Rows)
            {
                node.ChildNodes.Add(getTree(drData["cNodeID"].ToString(), drData["cNodeName"].ToString()));
            }
        }
        //若為0，則代表為葉子，則繼續新增特徵組的特徵屬性   朱君 2012/12/10
        else
        {
            node.Expanded = false;
            DataTable dtFaturevalue = this.getFeatureSetNodeForAll(nodeID);
            foreach (DataRow drData in dtFaturevalue.Rows)
            {
                node.ChildNodes.Add(getTree("", drData["sFeatureName"].ToString()));
            }
        }
        
        if (dtNodeID.Rows.Count > 0)
        {
            strAuthor = dtNodeID.Rows[0]["cAuthor"].ToString();
            strNodeName = dtNodeID.Rows[0]["cNodeName"].ToString();
            
            node.Value = nodeID;

            node.Text = "<span id='" + strSpanIDPrefix + "_" + nodeID + "'>" + nodeName + "</span>";
            //若不為子結點，則不帶參數呼叫javascript 朱君 2012/12/10
            if (dtChildren.Rows.Count > 0)
            {
                nodeID = "";
            }
            node.NavigateUrl = "javascript:SelectNode('" + nodeID + "','" + strAuthor + "','" + nodeName + "')";
            //若不為特徵組的葉子則展開
        }
      
        //若在資料庫沒有資料則不需要利用javascript設定結點資料
          else
        {
            node.Value = nodeID;
            node.Text = "<span id='" + strSpanIDPrefix + "_" + nodeID + "'>" + nodeName + "</span>";
            node.NavigateUrl = "javascript:SelectNode('" + "" + "','" + strAuthor + "','" + nodeName + "')";
        }

        
        return node;
    }

    //取得所有的子特徵組節點
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
    //取得所有的子特徵組屬性節點的特徵值屬性
    public DataTable getFeatureSetNodeForAll(string cNodeID)
    {
        DataTable dtResult = new DataTable();
        strSQL = "SELECT * FROM  FeaturevalueItem WHERE (cNodeID = '"+cNodeID+"')";
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

    protected void setlb_Selected()
    {
        //先將lb_Selected物件清空
        lb_Selected.Items.Clear();
        //若選擇null則不顯示任何item
        if (ddlFeatureSet.SelectedItem.Value != "null")
        {
            DataTable dtSelectFeatureItem = new DataTable();
            string strSQL_SelectFeatureItem = "SELECT B.cNodeName,B.cNodeID FROM  FeatureSetItem AS A INNER JOIN FeaturevalueTree AS B ON A.cNodeID = B.cNodeID WHERE (A.iFeatureSetID = '" + ddlFeatureSet.SelectedItem.Value + "')";
            dtSelectFeatureItem = hintsDB.getDataSet(strSQL_SelectFeatureItem).Tables[0];

            for (int i = 0; i < dtSelectFeatureItem.Rows.Count; i++)
            {
                lb_Selected.Items.Add(new ListItem(dtSelectFeatureItem.Rows[i]["cNodeName"].ToString(), dtSelectFeatureItem.Rows[i]["cNodeID"].ToString()));
            }
        }

    }
    //初始化ListboxSearch
    protected void setListboxSearch()
    {
        //先清空listboxSearch
        listboxSearch.Items.Clear();
        
        DataTable dtFeatureItem = new DataTable();
        string strSQL_FeatureItem = "SELECT sFeatureName FROM  FeaturevalueItem WHERE (cNodeID = '" + strCurrentNodeID.Value + "')";
        dtFeatureItem = hintsDB.getDataSet(strSQL_FeatureItem).Tables[0];
        if (dtFeatureItem.Rows.Count > 0)
        {
            for (int i = 0; i < dtFeatureItem.Rows.Count; i++)
            {
                listboxSearch.Items.Add(dtFeatureItem.Rows[i]["sFeatureName"].ToString());
            }
        }    
    }
        
    #endregion
    #region 按鈕定義區
    protected void btnDeleteQuestion_Click(object sender, EventArgs e)
    {
        try
        {
            strSQL = "DELETE QuestionList WHERE qId = '" + ddlQusetionList.SelectedItem.Value + "'";
            hintsDB.ExecuteNonQuery(strSQL);
        }
        catch
        {
            Response.Write("<script>window.alert('請選擇一個題庫')</script>");
        }        
        //初始化ddlSelectQuestion下拉式選單
        setddlQusetionList();
        //刪除後關閉下列所有表格
        Panel_out.Visible = false;
        Panel_inner.Visible = false;

    }
    protected void btAddQuestionSubmit_Click(object sender, EventArgs e)
    {        
        //新增Question到QuestionList資料表裡     
        string strSQL_QuestionNewNode = "INSERT INTO QuestionList VALUES('" + txtNewQuestionName.Text + "','"+txtNewQuestionName.Text+"_root"+"','')";                      
        try
        {
            hintsDB.ExecuteNonQuery(strSQL_QuestionNewNode);
            //新增QuestionRoot到QuestionGroupTree資料表裡 
            insertQuestionRoot();
        }
        catch
        {
            
        }
    }
    
   
    protected void btnQuestionList_Click(object sender, EventArgs e)
    {
        Panel_out.Visible = true;
        ddlFeatureSetDefault();
        //判斷是否要隱藏按鈕
        checkVisibleForddlFeature();
        //初始化setlb_Selected
        setlb_Selected();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Panel_inner.Visible = true;
        //建造特徵樹
        ConstructQuestionGroupTree();
        //初始化setlb_Selected
        setlb_Selected();
        btnEdit.Visible = false;
        btnHiddenEdit.Visible = true;
    }
    protected void btn_Select_Click(object sender, ImageClickEventArgs e)
    {
        
        //新增節點至資料表中      
        strSQL = "INSERT INTO FeatureSetItem VALUES('" + strCurrentNodeID.Value + "','" + ddlFeatureSet.SelectedItem.Value + "')";
        try
        {
            hintsDB.ExecuteNonQuery(strSQL);
        }
        catch
        {
            this.RegisterClientScriptBlock("", "<script> alert('error'); </script>");
        }
        //重新顯示setlb_Selected
        setlb_Selected();
        //只postbackupSelectedFeature
        upSelectedFeature.Update();
    }
    protected void btnHiddenEdit_Click(object sender, EventArgs e)
    {
        //隱藏編輯table
        Panel_inner.Visible = false;
        btnEdit.Visible = true;
        btnHiddenEdit.Visible = false;
    }
    //特徵值刪除按鈕事件
    protected void btn_Del_Click(object sender, ImageClickEventArgs e)
    {
        int iFeatureItem = Int32.Parse(ddlFeatureSet.SelectedItem.Value);
        try
        {
            strSQL = "DELETE FeatureSetItem WHERE cNodeID = '" + lb_Selected.SelectedItem.Value + "' AND iFeatureSetID='" + iFeatureItem + "'";
            hintsDB.ExecuteNonQuery(strSQL);
        }
        catch
        {
            Response.Write("<script>window.alert('請選擇一個特徵值')</script>");
        }
        //重新顯示setlb_Selected
        setlb_Selected();
        //只postbackupSelectedFeature
        upSelectedFeature.Update();        
    }
    
    //submit按鈕事件
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //更新QuestionList資料表，紀錄每一個題庫與特徵集合的關係
        strSQL = "UPDATE QuestionList SET iFeatureSetID='" + ddlFeatureSet.SelectedItem.Value.ToString() + "' WHERE qId='" + ddlQusetionList.SelectedItem.Value.ToString() + "'";
        try
        {
            hintsDB.ExecuteNonQuery(strSQL);
        }
        catch
        {
            this.RegisterClientScriptBlock("", "<script> alert('error'); </script>");
        }
        Response.Write("<script>window.alert('設定成功')</script>");
    }
    //前一頁按鈕事件
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Hints/AuthoringTool/CaseEditor/Paper/Paper_QuestionMainNew.aspx?Career=");
    }
    protected void btnAddFeatureSet_Click(object sender, EventArgs e)
    {
        //新增節點至資料表中      
        strSQL = "INSERT INTO FeatureSetList VALUES('" + txtFeatureSet.Text.ToString() + "')";
        try
        {
            hintsDB.ExecuteNonQuery(strSQL);
        }
        catch
        {
            this.RegisterClientScriptBlock("", "<script> alert('error'); </script>");
        }
        //初始化ddlFeatureSet
        setddlFeatureSet();
    }
    
    protected void btnDelet_Click(object sender, EventArgs e)
    {
        int iFeatureItem = Int32.Parse(ddlFeatureSet.SelectedItem.Value);
        try
        {
            //刪除FeatureSetList特徵組紀錄
            strSQL = "DELETE FeatureSetList WHERE iFeatureSetID = '" + iFeatureItem + "'";            
            hintsDB.ExecuteNonQuery(strSQL);
            //將以設定刪除紀錄的題庫的特徵組紀錄設為0
            strSQL = "UPDATE QuestionList SET iFeatureSetID='" + "0" + "' WHERE iFeatureSetID='" + iFeatureItem + "'";
            hintsDB.ExecuteNonQuery(strSQL);
        }
        catch
        {
            Response.Write("<script>window.alert('請選擇一個特徵組')</script>");
        }

        //初始化ddlFeatureSet
        setddlFeatureSet();

    }
    //檢視詳細特徵值
    protected void btn_search_Click(object sender, ImageClickEventArgs e)
    {
        //初始化ListboxSearch
        setListboxSearch();               
    }
    #endregion
    #region 方法區
    protected void insertQuestionRoot()
    {
        //取得QuestionRoot的資料
        DataTable dtQuestionNewNode = new DataTable();
        string strSQL_QuestionId = "SELECT * FROM QuestionList WHERE qName='" + txtNewQuestionName.Text + "' ORDER BY qId DESC";
        dtQuestionNewNode = hintsDB.getDataSet(strSQL_QuestionId).Tables[0];
        //新增QuestionRoot到QuestionGroupTree資料表裡 
        string strSQL_InsertRoot = "INSERT INTO QuestionGroupTree VALUES ('" + txtNewQuestionName.Text + "_root" + "','" + "_self" + "','" + "root" + "','" + txtNewQuestionName.Text + "','" + usi.UserID + "','" + dtQuestionNewNode.Rows[0]["qId"] + "')";
        try
        {
            hintsDB.ExecuteNonQuery(strSQL_InsertRoot);
        }
        catch
        {
            this.RegisterClientScriptBlock("", "<script> alert('error'); </script>");
        }
        //初始化ddlSelectQuestion下拉式選單
        setddlQusetionList();
    }

   

    protected void ddlFeatureSet_SelectedIndexChanged(object sender, EventArgs e)
    {    
        //重新顯示setlb_Selected
        setlb_Selected();
        //判斷是否要隱藏按鈕
        checkVisibleForddlFeature();
    }

    //初始化，畫面起始時顯示被設定的特徵值
    protected void ddlFeatureSetDefault()
    {
        DataTable dtSelectFeatureItem = new DataTable();
        string strSQL_SelectFeatureItem = "SELECT iFeatureSetID FROM  QuestionList WHERE qID='" + ddlQusetionList .SelectedItem.Value.ToString() + "'";
        dtSelectFeatureItem = hintsDB.getDataSet(strSQL_SelectFeatureItem).Tables[0];
        String iFeatureSetID=dtSelectFeatureItem.Rows[0]["iFeatureSetID"].ToString(); 
        ddlFeatureSet.SelectedValue = dtSelectFeatureItem.Rows[0]["iFeatureSetID"].ToString();
    
    }
    //判斷是否要隱藏按鈕
    protected void checkVisibleForddlFeature()
    {
        //若選擇null，則關閉edit和delet功能，且不顯示編輯介面
        if (ddlFeatureSet.SelectedItem.Value.ToString() == "0")
        {
            btnEdit.Visible = false;
            btnHiddenEdit.Visible = false;
            btnDelet.Visible = false;
            Panel_inner.Visible = false;
        }
        else
        {
            //若此時Panel_inner為顯示的，則隱藏btnEdit
            if (Panel_inner.Visible == true)
                btnEdit.Visible = false;
            else
                btnEdit.Visible = true;
            btnDelet.Visible = true;     
        }
    }
    #endregion
}
