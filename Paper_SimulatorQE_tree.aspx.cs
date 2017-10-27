﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Hints.HintsUtility;
using Hints.DB.TableDriven;

using System.Text.RegularExpressions;
using System.Xml;
using Hints.DB;
using suro.util;
using AuthoringTool.QuestionEditLevel;
//using Hints.Learning.Question;
using PaperSystem;

public partial class AuthoringTool_CaseEditor_Paper_Paper_SimulatorQE_tree : AuthoringTool_BasicForm_BasicForm
{
    protected clsHintsDB HintsDB = new clsHintsDB();
    string GID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(AuthoringTool_CaseEditor_Paper_Paper_SimulatorQE_tree));
        clsHintsDB MLASDB = new clsHintsDB();
        
        if (!Page.IsPostBack)
        {
            //InitTree();
            CreateTree();
            //getParameter();
            templateTable.Visible = false;
            if (Request.QueryString["QID"] != null)
            {
                hf_QID.Value = Request.QueryString["QID"].ToString();
                string strSQL = "SELECT * FROM Question_Simulator WHERE cQID LIKE '" + hf_QID.Value + "'";       
                DataTable dt_firstTime = MLASDB.getDataSet(strSQL).Tables[0];

                hf_DMTID.Value = dt_firstTime.Rows[0]["cSimulatorID"].ToString();
                templateTable.Visible = true;
                Session["Simulator"] = hf_DMTID.Value;
                imyTreeContainer.Attributes.Add("src", "Paper_SimulatorQuestionEditor.aspx");
            }
            //如果hfQID.Value != ""  就讀取圖片 同時寫入Session["Simulator"] = hf_DMTID.Value;hf_DMTID.Value = QID|||
            //if (Request.QueryString["GroupID"] != null)
            //    GID = Request.QueryString["GroupID"].ToString();
        }


       
    }
    //protected void InitTree()
    //{

    //    //列出 private template
    //    string query = "Select * From SimulatorBackground Where UserID = '" + usi.UserID + "' Order By SimulatorID";
    //    DataTable dt = HintsDB.getDataSet(query).Tables[0];

    //    //authot id Node
    //    TreeNode UserNode = new TreeNode(usi.UserID);
    //    UserNode.SelectAction = TreeNodeSelectAction.Expand;
    //    tTD.Nodes[2].ChildNodes.Add(UserNode);

    //    //所有的 private template
    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        TreeNode NewNode = new TreeNode();
    //        NewNode.Text = "<span id='" + dt.Rows[i]["SimulatorID"].ToString() + "'>" + dt.Rows[i]["SName"].ToString() + "</span>";
    //        NewNode.Value = dt.Rows[i]["SimulatorID"].ToString();
    //        NewNode.SelectAction = TreeNodeSelectAction.Select;
    //        tTD.Nodes[2].ChildNodes[0].ChildNodes.Add(NewNode);
    //    }

    //}

    protected void tTD_SelectedNodeChanged(object sender, EventArgs e)
    {
        hf_DMTID.Value = tvCourseTreeMenu.SelectedNode.Value;
        templateTable.Visible = true;
        Session["Simulator"] = hf_DMTID.Value;
        imyTreeContainer.Attributes.Add("src", "Paper_SimulatorQuestionEditor.aspx");
    }
    //建立課程Tree
    public void CreateTree()
    {
        tvCourseTreeMenu.Nodes.Clear();
        tvCourseTreeMenu.Nodes.Add(getTree("Hospitalroot", "Hospital", ""));

        tvCourseTreeMenu.ExpandDepth = 1;
    }

    //取得Tree資料
    public TreeNode getTree(string nodeID, string nodeName, string strNodeDivisionID)
    {
        TreeNode node = new TreeNode();
        node.ImageUrl = "Images/ftv2folderopen.gif";
        node.SelectAction = TreeNodeSelectAction.Expand;
        node.Text = nodeName;
        node.NavigateUrl = null;

        //取得Division ID
        if (strNodeDivisionID == "")
        {
            //clsIELSDB IELSDB = new clsIELSDB();
            //string strDivisionID = "";
            string strSQL_Division = "SELECT * FROM Division WHERE cDivisionName = '" + nodeName + "'";
            DataTable dtDivision = HintsDB.getDataSet(strSQL_Division).Tables[0];//.GetDataSet(strSQL_Division).Tables[0];
            if (dtDivision.Rows.Count > 0)
            {
                strNodeDivisionID = dtDivision.Rows[0]["cDivisionID"].ToString();
            }
        }

        string cNodeID = "%";
        //判斷node是否還有children node
        //clsIELSDB IELSDB = new clsIELSDB();
        DataTable dtChildren = new DataTable();
        string strSQL_TreeManagement = "SELECT * FROM TreeManagement WHERE cNodeID LIKE '" + cNodeID + "' AND cParentID LIKE '" + nodeID + "' ORDER BY cNodeSeq ASC ";
        try
        {
            dtChildren = HintsDB.getDataSet(strSQL_TreeManagement).Tables[0];
        }
        catch
        {
            dtChildren = new DataTable();
        }

        if (dtChildren.Rows.Count > 0)
        {
            foreach (DataRow drData in dtChildren.Rows)
            {
                node.ChildNodes.Add(getTree(drData["cNodeID"].ToString(), drData["cNodeName"].ToString(), strNodeDivisionID));
            }
        }
        else
        {
            string str_VM_Disease = ""; 
            //轉換general to simulation 因為VM VR
            if (nodeName == "General")
            {
                str_VM_Disease = "simulation";
            }
            else
            {
                str_VM_Disease = nodeName;
            }
            //建立 3D Scene階層
            string strSQLGet3DSenceID = "SELECT DISTINCT iSlideNum FROM ItemForVM Where cDisease = '" + str_VM_Disease + "'";
            DataTable dt_3D = HintsDB.getDataSet(strSQLGet3DSenceID).Tables[0];//只用在sildernum

            for (int i = 0; i < dt_3D.Rows.Count; i++)
            {
                //authot id Node
                TreeNode UserNode = new TreeNode(usi.UserID);
                UserNode.ImageUrl = "Images/ftv2folderopen.gif";
                UserNode.Text = dt_3D.Rows[i]["iSlideNum"].ToString();
                UserNode.SelectAction = TreeNodeSelectAction.Expand;
                node.ChildNodes.Add(UserNode);

                string SQL_Division = "SELECT * FROM Division WHERE cDivisionID = '" + strNodeDivisionID + "'";
                DataTable dt_Division = HintsDB.getDataSet(SQL_Division).Tables[0];
                string str_VM_Organ = "";
                //轉換general to simulation 因為VM VR
                if (dt_Division.Rows[0]["cDivisionName"].ToString() == "Internal Medicine")
                {
                    str_VM_Organ = "simulation";
                }
                else
                {
                    str_VM_Organ = dt_Division.Rows[0]["cDivisionName"].ToString();
                }

                string strSQLGetsimulation = "Select Distinct cCaseID FROM ItemForVM Where cDisease = '" + str_VM_Disease + "' AND iSlideNum = '" + dt_3D.Rows[i]["iSlideNum"].ToString() + "' AND cOrgan LIKE '" + str_VM_Organ + "'";
                DataTable dt_simulation = HintsDB.getDataSet(strSQLGetsimulation).Tables[0];

                if (dt_simulation.Rows.Count > 0)
                {//創sim的節點
                    for (int x = 0; x < dt_simulation.Rows.Count; x++)
                    {
                        string strSQLID = "SELECT * FROM ItemForVM Where cCaseID = '" + dt_simulation.Rows[x]["cCaseID"].ToString() + "'";
                        DataTable dt_VMID = HintsDB.getDataSet(strSQLID).Tables[0];
                        if (dt_VMID.Rows.Count > 0)
                        {   //場景必須有場景名稱VM VR
                            if (dt_VMID.Rows[0]["cShowName"].ToString() != "")
                            {
                                //sim Node
                                TreeNode simuNode = new TreeNode(usi.UserID);
                                simuNode.Text = dt_VMID.Rows[0]["cShowName"].ToString();
                                simuNode.Value = dt_VMID.Rows[0]["cCaseID"].ToString() + "|" + dt_Division.Rows[0]["cDivisionName"].ToString() + "|" + nodeName + "|" + dt_3D.Rows[i]["iSlideNum"].ToString();
                                simuNode.SelectAction = TreeNodeSelectAction.Select;
                                UserNode.ChildNodes.Add(simuNode);
                            }
                        }
                    }
                }
            }
 
        }
        return node;
    }

    #region 前端ajax function
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string GetNodeID(string ParentID, string strDMTID)
    {
        if (strDMTID != "")
        {
            DataTable dtParentID = clsTableDriven.ViewMainItem_SELECT(strDMTID, ParentID);
            string ReturnStr = "";
            if (dtParentID.Rows.Count > 0)
            {
                ReturnStr = dtParentID.Rows[0]["cMItemID"].ToString();

                return ReturnStr;
            }
            else
                return "False";
        }
        else
            return "False";
    }
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string GetNodeName(string ParentID, string strDMTID)
    {
        if (strDMTID != "")
        {
            DataTable dtParentID = clsTableDriven.ViewMainItem_SELECT(strDMTID, ParentID);
            string ReturnStr = "";
            if (dtParentID.Rows.Count > 0)
            {
                ReturnStr = dtParentID.Rows[0]["cMItem"].ToString();

                return ReturnStr;
            }
            else
                return "False";
        }
        else
            return "False";
    }
    //取得所有child node id
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string GetChildNodeID(string ParentID, string strDMTID)
    {
        if (strDMTID != "")
        {
            DataTable dtChildID = clsTableDriven.TDSetOfSubItem_SELECT(strDMTID, ParentID);
            string ReturnStr = "";
            if (dtChildID.Rows.Count > 0)
            {
                for (int i = 0; i < dtChildID.Rows.Count; ++i)
                {
                    if (i == dtChildID.Rows.Count - 1) //final node 不加/
                        ReturnStr += dtChildID.Rows[i]["cSItemID"].ToString().Replace("Sub", "Main");
                    else
                        ReturnStr += dtChildID.Rows[i]["cSItemID"].ToString().Replace("Sub", "Main") + "$";
                }

                return ReturnStr;
            }
            else
                return "False";
        }
        else
            return "False";
    }

    //取得所有child node name
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string GetChildNodeName(string ParentID, string strDMTID)
    {
        if (strDMTID != "")
        {
            DataTable dtChildID = clsTableDriven.ViewOfTableDriven_SELECT(strDMTID, ParentID);
            string ReturnStr = "";
            if (dtChildID.Rows.Count > 0)
            {
                for (int i = 0; i < dtChildID.Rows.Count; ++i)
                {
                    if (i == dtChildID.Rows.Count - 1) //final node 不加/
                        ReturnStr += dtChildID.Rows[i]["cResultItem"].ToString();
                    else
                        ReturnStr += dtChildID.Rows[i]["cResultItem"].ToString() + "$";
                }

                return ReturnStr;
            }
            else
                return "False";

        }
        else
            return "False";
    }
    //取得所有child node Condition
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string GetChildNodeCondition(string ParentID, string strDMTID)
    {
        if (strDMTID != "")
        {
            DataTable dtChildID = clsTableDriven.ViewOfTableDriven_SELECT(strDMTID, ParentID);
            string ReturnStr = "";
            if (dtChildID.Rows.Count > 0)
            {
                for (int i = 0; i < dtChildID.Rows.Count; ++i)
                {
                    if (i == dtChildID.Rows.Count - 1) //final node 不加/
                    {
                        DataTable dtCondition = clsTableDriven.TDViewOfSubItem_SELECT(dtChildID.Rows[i]["cSItemID"].ToString());
                        ReturnStr += dtCondition.Rows[0]["cSItem"].ToString();
                    }
                    else
                    {
                        DataTable dtCondition = clsTableDriven.TDViewOfSubItem_SELECT(dtChildID.Rows[i]["cSItemID"].ToString());
                        ReturnStr += dtCondition.Rows[0]["cSItem"].ToString() + "$";
                    }
                }

                return ReturnStr;
            }
            else
                return "False";
        }
        else
            return "False";
    }
    #endregion

    protected void btn_select_Click(object sender, EventArgs e)
    {
        string strTitle = "";
        if (hf_QID.Value == "")
        {
            //建立QID
            DataReceiver myReceiver = new DataReceiver();
            string strUserID = usi.UserID.ToString();
            hf_QID.Value = strUserID + "_" + myReceiver.getNowTime();
        }
        else
        {
            clsHintsDB MLASDB = new clsHintsDB();
            string strSQL = "SELECT * FROM Question_Simulator WHERE cQID LIKE '" + hf_QID.Value + "'";
            DataTable dt_firstTime = MLASDB.getDataSet(strSQL).Tables[0];
            if (dt_firstTime.Rows.Count > 0)
            {
                strTitle = dt_firstTime.Rows[0]["cContent"].ToString();
            }
        }
        Response.Redirect("Paper_SimulatorQuestionEditor2.aspx?cImg=" + hf_DMTID.Value + "&QID=" + hf_QID.Value + "&Title=" + strTitle + "");

    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Paper_QuestionViewNew.aspx?GroupID=" + Session["GroupID"].ToString());
    }
}
