using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CaseTreeControl;
using suro.util;

namespace WebApplication1
{
	/// <summary>
	/// Mt_Favorite 的摘要描述。
	/// </summary>
    public partial class Mt_Favorite : AuthoringTool_BasicForm_BasicForm  
	{
		SqlDB sqldb=new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
		private string Directory_DataTable = "QuestionGroupTree";
		protected string editNodeText_Script = "";//前端中使某個節點進入編輯模式的Script
		protected string unfoldNode_Script = "";//前端中展開某個節點的Script
		protected CaseTree caseTree = null;
		private string UserID = "";

		//protected string CaseID = "";
		//protected string CaseName = "";
		//protected string DivisionID = "";
        //private string link = "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			
			//CaseName = this.Request.QueryString["CaseName"];
			//CaseID = this.Request.QueryString["CaseID"];
			//DivisionID = this.Request.QueryString["DivisionID"];
			//addCaseNode.Attributes.Add("onclick","addCaseNode_Function('"+DivisionID+"','"+CaseID+"');");

			UserID = "alivs";
			Hide_Fire_ServerEventHandler_Button();

			//Opener
			if(Session["Opener"] != null)
			{
				hiddenOpener.Value = Session["Opener"].ToString();
			}

			//Setup opener
			if(Session["Opener"] != null)
			{
				Session["Opener"] = "./QuestionGroupTree/QuestionGroupTree";
			}
			else
			{
				Session.Add("Opener","./QuestionGroupTree/QuestionGroupTree");
			}

			//PresentType
			if(Session["PresentType"] != null)
			{
				hiddenPresentType.Value = Session["PresentType"].ToString();
			}
		
			//QuestionMode
			if(Session["QuestionMode"] != null)
			{
				hiddenQuestionMode.Value = Session["QuestionMode"].ToString();
			}

			//QuestionType
			if(Session["QuestionType"] != null)
			{
				hiddenQuestionType.Value = Session["QuestionType"].ToString();
			}

			//EditMode
			if(Session["EditMode"] != null)
			{
				hiddenEditMode.Value = Session["EditMode"].ToString();
			}

			//ModifyType
			if(Session["ModifyType"] != null)
			{
				hiddenModifyType.Value = Session["ModifyType"].ToString();
			}

			//QuestionFunction
			if(Session["QuestionFunction"] != null)
			{
				hiddenQuestionFunction.Value = Session["QuestionFunction"].ToString();
			}
			

			if(!IsPostBack)
			{
				//link = this.Request.QueryString["link"]+"";
				//this.ViewState.Add("link",link);
//				if(!link.Equals(""))
//				{
//					caseTree = new CaseTree("alivs",true);	
//				}
//				else
//				{
//					caseTree = new CaseTree("alivs",false);	
//				}
				caseTree = new CaseTree(UserID,false);	
				caseTree.BuildTree();
			}
			else
			{
				//link = ViewState["link"].ToString();
			}
		}

		#region Web Form 設計工具產生的程式碼
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 此為 ASP.NET Web Form 設計工具所需的呼叫。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 此為設計工具支援所必須的方法 - 請勿使用程式碼編輯器修改
		/// 這個方法的內容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		private void dragNodeEventHandler_ServerClick(object sender, System.EventArgs e)
		{
			string srcNodeID = this.dragRecord.Value.Split('#')[0];
			string desNodeID = this.dragRecord.Value.Split('#')[1];
			modifyNode_ParentID(srcNodeID,desNodeID);
//			if(!link.Equals(""))
//			{
//				caseTree = new CaseTree(UserID,true);	
//			}
//			else
//			{
//				caseTree = new CaseTree(UserID,false);	
//			}
			caseTree = new CaseTree(UserID,false);	
			caseTree.BuildTree();
		}

		/// <summary>
		/// 因為在此Page中具有許多會觸發Server端的事件的Button可是這些Button並不讓使用者動到,因此需將其隱藏起來
		/// </summary>
		private void Hide_Fire_ServerEventHandler_Button()
		{
			dragNode_EventHandler_Button.Style.Add("DISPLAY","none");
	        addFolder_EventHandler_Button.Style.Add("DISPLAY","none");
			Delete_Node_EventHandler_Button.Style.Add("DISPLAY","none");
			addCaseNode.Style.Add("DISPLAY","none");
		}

		/// <summary>
		/// 修改節點的ParentID
		/// </summary>
		private void modifyNode_ParentID(string srcNodeID,string desNodeID)
		{
			string strSQL = "SELECT * FROM " + Directory_DataTable + " WHERE cNodeID='"+srcNodeID+"'";
		    DataTable dt = sqldb.getDataSet(strSQL).Tables[0];
			DataRow dr = dt.Rows[0];
			dr["cParentID"] = desNodeID;
			sqldb.Update(dt,"SELECT * FROM " + Directory_DataTable);
			//展開某個結點
			unfoldNode(desNodeID);
		}

		/// <summary>
		/// 註冊前端程式以展開某個結點
		/// </summary>
		/// <param name="NodeID">節點ID</param>
		private void unfoldNode(string NodeID)
		{
			unfoldNode_Script = "<script language='javascript'>unfoldNode('"+NodeID+"')</script>";
		}

		/// <summary>
		/// 註冊前端程式以使某個結點進入編輯摸式
		/// </summary>
		/// <param name="NodeID">節點ID</param>
		private void editNodeText(string NodeID)
		{
			editNodeText_Script = "<script language='javascript'>editNodeTextFireFromServer('"+NodeID+"')</script>";
		}

		/// <summary>
		/// 新增資料夾的EventHandler
		/// </summary>
		private void addFolder_EventHandler_Button_ServerClick(object sender, System.EventArgs e)
		{
			addFolder();
//			if(!link.Equals(""))
//			{
//				caseTree = new CaseTree(UserID,true);	
//			}
//			else
//			{
//				caseTree = new CaseTree(UserID,false);	
//			}
			caseTree = new CaseTree(UserID,false);	
			caseTree.BuildTree();
		}

		/// <summary>
		/// 在資料表中新增某個節點
		/// </summary>
		private void addFolder()
		{
			//在資料庫中新增一節點
			string folder_id_to_add = this.add_folder_Record.Value;
			string NewNodeID = getNewNodeID(folder_id_to_add);
			string strSQL = "SELECT * FROM " + Directory_DataTable + " WHERE 1=0";
			DataTable dt = sqldb.getDataSet(strSQL).Tables[0];
			DataRow dr = dt.NewRow();
			dr["cNodeID"] = NewNodeID;
			dr["cNodeName"] = "New Group";
			dr["cParentID"] = folder_id_to_add;
			dr["cNodeType"] = "Group";
			dt.Rows.Add(dr);
			sqldb.Update(dt,"SELECT * FROM " + Directory_DataTable);	

			//展開某個結點
			unfoldNode(folder_id_to_add);
			//註冊前端程式以使某個結點進入編輯摸式
			editNodeText(NewNodeID);
		}

		private string getNewNodeID(string folder_id_to_add)
		{
			string strNewID;
			int intTemp = 0;
			DateTime dtNow = DateTime.Now;
			while(dtNow.AddSeconds(0.1) < DateTime.Now)
				intTemp ++;
			strNewID = "Group_" + DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
			return(strNewID);
		}

		protected void Delete_Folder_EventHandler_Button_Click(object sender, System.EventArgs e)
		{
			string nodeID_to_delete = this.Delete_Node_Record.Value;
			Delete_Node(nodeID_to_delete);
//			if(!link.Equals(""))
//			{
//				caseTree = new CaseTree(UserID,true);	
//			}
//			else
//			{
//				caseTree = new CaseTree(UserID,false);	
//			}
			caseTree = new CaseTree(UserID,false);	
			caseTree.BuildTree();
		}

		private void Delete_Node(string nodeID_to_delete)
		{
			string strSQL = "DELETE " + this.Directory_DataTable + " WHERE cNodeID = '"+nodeID_to_delete+"'";
			sqldb.ExecuteNonQuery(strSQL);
			//刪除子節點
			strSQL = "SELECT cNodeID FROM " + this.Directory_DataTable + " WHERE cParentID = '"+nodeID_to_delete+"'";
			DataTable dt = sqldb.getDataSet(strSQL).Tables[0];
			foreach(DataRow dr in dt.Rows)
			{
				Delete_Node(dr["cNodeID"].ToString());
			}
		}

		private string GetNewID()
		{			
			string strNewID;
			int intTemp;
			DateTime dtNow = DateTime.Now;
			while(dtNow.AddSeconds(0.1) < DateTime.Now)
				intTemp = 0;
			strNewID =  UserID+ DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
			return(strNewID);
		}

		protected void addCaseNode_Click(object sender, System.EventArgs e)
		{
			string srcNodeID = GetNewID();
			string desNodeID = this.dragRecord.Value.Split('#')[1];
			insertANodeIntoDB(srcNodeID,desNodeID);
//			if(!link.Equals(""))
//			{
//				caseTree = new CaseTree(UserID,true);	
//			}
//			else
//			{
//				caseTree = new CaseTree(UserID,false);	
//			}
			caseTree = new CaseTree(UserID,false);	
			caseTree.BuildTree();
		}

		private void insertANodeIntoDB(string newID,string parentNodeID)
		{
			string strSQL = "SELECT * FROM "+Directory_DataTable+" WHERE 1=0";
			DataTable dt = sqldb.getDataSet(strSQL).Tables[0];
			DataRow dr = dt.NewRow();
			dr["cUserID"] = UserID;
			dr["cNodeID"] = newID;
			//dr["cNodeName"] = this.CaseName;
			dr["cParentID"] = parentNodeID;
			dr["cNodeType"] = "case";
			//dr["cCaseID"] = this.CaseID;
			//dr["cDivisionID"] = this.DivisionID;
			dt.Rows.Add(dr);
			sqldb.Update(dt,"SELECT * FROM " + Directory_DataTable);	
		}
	}
}
