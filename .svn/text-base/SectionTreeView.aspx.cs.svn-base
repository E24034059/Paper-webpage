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
using suro.util;

namespace PaperSystem
{
	/// <summary>
	/// SectionTreeView 的摘要描述。
	/// </summary>
    public partial class SectionTreeView : AuthoringTool_BasicForm_BasicForm
	{
		string strUserLevel = "1";
		string strCaseID = "";
		string strDivisionID = "6061";

		//建立SqlDB物件
		SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);

		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.Initiate();

			//接收參數CaseID , DivisionID
			this.getParameter();

			//建立Section tree
			this.setupSectionTree();
		}

		private void setupSectionTree()
		{
			//建立tree的必要javascript
			string NodeSc="<SCRIPT LANGUAGE='JavaScript' SRC='../Translate tree/JSCookTree.js'></SCRIPT><link REL='stylesheet' HREF='../Translate tree/theme.css' TYPE='text/css'><SCRIPT LANGUAGE='JavaScript' SRC='../Translate tree/theme.js'></SCRIPT>";
			Page.RegisterStartupScript("NodeSc",NodeSc);

			//建立tree的string
			string NodeStr="[";

			//---------------------------------------取得CaseTree的每一個Node----------------------------------------
			string strSQL = "";

			//建立Visit
			strSQL = "SELECT MAX(sClinicNum) AS 'MaxClinic' FROM UserLevelPresent WHERE cUserLevel = '"+strUserLevel+"' AND cCaseID = '"+strCaseID+"' AND cDivisionID = '"+strDivisionID+"' ";
			
			//-------------------------------------------將CaseTreeView中的Node加到TreeView中------------------------------------

			//-----------------------------------------------建立一個Hospital Node-----------------------------------------------
		//	NodeStr = NodeStr + "[null,'"+"Hospital"+"',null,'_self',null";
			
			//--------------------------------------建立Hospital的子Node，DepartmentNode-------------------------------------
			DataSet dsVisit = sqldb.getDataSet(strSQL);
			foreach (DataRow drVisit in dsVisit.Tables[0].Rows)
			{
				int intVisit = 1;
				try
				{
					intVisit = Convert.ToInt32(drVisit["sClinicNum"]);
				}
				catch
				{
				}

				NodeStr = NodeStr +",[null,'" + intVisit.ToString().Trim() + "',null,null,null";

				strSQL = "SELECT cSectionName FROM UserLevelPresent WHERE cUserLevel = '"+strUserLevel+"' AND cCaseID = '"+strCaseID+"' AND cDivisionID = '"+strDivisionID+"' AND sClinicNum = '"+intVisit.ToString()+"' ORDER BY sSectionSeq ";
				DataSet dsSection=sqldb.getDataSet(strSQL);
				foreach (DataRow drSection in dsSection.Tables[0].Rows)
				{
					NodeStr = NodeStr + ",[null,'" + drSection["cSectionName"].ToString().Trim() + "',null,'frmMain',null";
					
					NodeStr = NodeStr + "]";
				}
				dsSection.Dispose();
				//完成建立DivisionNode
				NodeStr = NodeStr + "]";

			}
			dsVisit.Dispose();

			//完成將TreeNode加到TreeView中
			string NodePut = "<Font size='26'><script language='JavaScript'>var myMenu=" + NodeStr + "]</script></Font>";
			NodePut=NodePut + "<DIV ID=myMenuID  align='left' ></DIV>";
			NodePut = NodePut + "<SCRIPT LANGUAGE='JavaScript'>ctDraw ('myMenuID', myMenu, ctThemeXP2, '../Translate tree/ThemeXP', 1, 3);</SCRIPT>";
			Page.RegisterStartupScript("NodePut",NodePut);
		}

		private void getParameter()
		{
			//CaseID
			if(Request.QueryString["CaseID"] != null)
			{
				strCaseID = Request.QueryString["CaseID"];
				if(Session["CaseID"] != null)
				{
					Session["CaseID"] = strCaseID;
				}
				else
				{
					Session.Add("CaseID",strCaseID);
				}
			}
			else
			{
				if(Session["CaseID"] != null)
				{
					strCaseID = Session["CaseID"].ToString();
				}
				else
				{
					strCaseID = "linCase200308131525133152432";
				}
			}

			//DivisionID
			if(Request.QueryString["DivisionID"] != null)
			{
				strDivisionID = Request.QueryString["DivisionID"];
				if(Session["DivisionID"] != null)
				{
					Session["DivisionID"] = strDivisionID;
				}
				else
				{
					Session.Add("DivisionID",strDivisionID);
				}
			}
			else
			{
				if(Session["DivisionID"] != null)
				{
					strDivisionID = Session["DivisionID"].ToString();
				}
				else
				{
					strDivisionID = "6061";
				}
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
	}
}
