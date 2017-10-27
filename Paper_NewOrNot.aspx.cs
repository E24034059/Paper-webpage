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

namespace PaperSystem
{
	/// <summary>
	/// Paper_NewOrNot 的摘要描述。
	/// </summary>
    public partial class Paper_NewOrNot : AuthoringTool_BasicForm_BasicForm
	{
		string strUserID , strCaseID , strClinicNum , strSectionName , strPaperID;

		DataReceiver myReceiver = new DataReceiver();

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.Initiate();

			this.getParameter();
		}

		private void getParameter()
		{
			//UserID
			if(Session["UserID"] != null)
			{
				strUserID = Session["UserID"].ToString();
			}
			
			//CaseID kyhCase200505301448128593750
			if(Session["CaseID"] != null)
			{
				strCaseID = Session["CaseID"].ToString();
			}
			
			//ClinicNum
			if(Session["ClinicNum"] != null)
			{
				strClinicNum = Session["ClinicNum"].ToString();
			}
			
			//SectionName 測驗
			if(Session["SectionName"] != null)
			{
				strSectionName = Session["SectionName"].ToString();
			}

			//PaperID
			if(Session["PaperID"] != null)
			{
				strPaperID = Session["PaperID"].ToString();
			}

			//Opener
			if(Session["Opener"] != null)
			{
				hiddenOpener.Value = Session["Opener"].ToString();
			}

			//Setup opener
			if(Session["Opener"] != null)
			{
				Session["Opener"] = "Paper_NewOrNot";
			}
			else
			{
				Session.Add("Opener","Paper_NewOrNot");
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

			//EditMode
			if(Session["EditMode"] != null)
			{
				hiddenEditMode.Value = Session["EditMode"].ToString();
			}

			/* 修改至Paper_SelectQuestion 和 Paper_RandomSelect
			//GroupID
			string strGroupID = "";
			if(Request.QueryString["GroupID"] != null)
			{
				strGroupID = Request.QueryString["GroupID"].ToString();
				if(Session["GroupID"] != null)
				{
					Session["GroupID"] = strGroupID;
				}
				else
				{
					Session.Add("GroupID",strGroupID);
				}
			}

			//GroupDivisionID
			if(strGroupID.Trim().Length > 0)
			{
				string strGroupDivisionID = myReceiver.getGroupDivisionID(strGroupID);
				if(Session["GroupDivisionID"] != null)
				{
					Session["GroupDivisionID"] = strGroupDivisionID;
				}
				else
				{
					Session.Add("GroupDivisionID",strGroupDivisionID);
				}
			}
			*/

			//bModify
			if(Session["bModify"] != null)
			{
				Session["bModify"] = "false";
			}
			else
			{
				Session.Add("bModify" , "false");
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
