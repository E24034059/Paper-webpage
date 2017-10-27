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
	/// Paper_EditMethod 的摘要描述。
	/// </summary>
    public partial class Paper_EditMethod : AuthoringTool_BasicForm_BasicForm
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
			//SystemFunction
			if(Session["SystemFunction"] != null)
			{
				Session["SystemFunction"] = "EditPaper";
			}
			else
			{
				Session.Add("SystemFunction" , "EditPaper");
			}

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
				Session["Opener"] = "Paper_EditMethod";
			}
			else
			{
				Session.Add("Opener","Paper_EditMethod");
			}

			//QuestionMode
			if(Session["QuestionMode"] != null)
			{
				hiddenQuestionMode.Value = Session["QuestionMode"].ToString();
			}

			//PresentType
			if(Request.QueryString["PresentType"] != null)
			{
				strPresentType = Request.QueryString["PresentType"].ToString();
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
			string strEditMode = "";
			if(rb1.Checked == true)
			{
				strEditMode = "Manual";
			}
			else
			{
				strEditMode = "Automatic";
			}

			//加入Session
			if(Session["EditMode"] != null)
			{
				Session["EditMode"] = strEditMode;
			}
			else
			{
				Session.Add("EditMode" , strEditMode);
			}

			if(strEditMode == "Manual")
			{
				Response.Redirect("Paper_QuestionMode.aspx?Opener=Paper_EditMethod");
			}
			else
			{
                // modified by dolphin @ 2006-07-29, new Question Group Tree
				//Response.Redirect("./QuestionGroupTree/QuestionGroupTree.aspx?Opener=Paper_EditMethod");
                Response.Redirect("./QuestionGroupTree/QGroupTree.aspx?Opener=Paper_EditMethod");
			}
		}
	}
}
