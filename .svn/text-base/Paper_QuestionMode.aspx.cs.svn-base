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
	/// Paper_QuestionMode 的摘要描述。
	/// </summary>
    public partial class Paper_QuestionMode : AuthoringTool_BasicForm_BasicForm
	{
		string strUserID , strCaseID , strClinicNum , strSectionName , strPaperID , strPresentType;

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.Initiate();

			this.getParameter();

			btnNext.ServerClick+=new EventHandler(btnNext_ServerClick);
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
				Session["Opener"] = "Paper_QuestionMode";
			}
			else
			{
				Session.Add("Opener","Paper_QuestionMode");
			}

			//PresentType
			if(Session["PresentType"] != null)
			{
				strPresentType = Session["PresentType"].ToString();
			}
	
			//bModify
			if(Session["bModify"] != null)
			{
				Session["bModify"] = false;
			}
			else
			{
				Session.Add("bModify" , false);
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

		private void btnNext_ServerClick(object sender, EventArgs e)
		{
			string strQuestionType = "";

			if(rb1.Checked == true)
			{
				strQuestionType = "General";
			}
			else
			{
				strQuestionType = "Specific";
			}

			//加入Session
			if(Session["QuestionMode"] != null)
			{
				Session["QuestionMode"] = strQuestionType;
			}
			else
			{
				Session.Add("QuestionMode" , strQuestionType);
			}

			Response.Redirect("Paper_QuestionTypeNew.aspx?Opener=Paper_QuestionMode");

//			if(strQuestionType == "General")
//			{
//				Response.Redirect("./QuestionGroupTree/QuestionGroupTree.aspx");
//			}
//			else
//			{
//				Response.Redirect("./CommonQuestionEdit/Page/ShowQuestion.aspx");
//			}

			/*
			if(strPresentType == "Edit")
			{
				if(strQuestionType == "General")
				{
					Response.Redirect("Paper_EditMethod.aspx");
				}
				else
				{
					Response.Redirect("Paper_EditMethod.aspx");
				}
			}
			else
			{
				if(strQuestionType == "General")
				{
					Response.Redirect("./QuestionGroupTree/QuestionGroupTree.aspx");
				}
				else
				{
					Response.Redirect("Paper_NewOrNot.aspx");
				}
			}
			*/
		}
	}
}
