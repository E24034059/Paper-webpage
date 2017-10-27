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
	/// Paper_QuestionMain 的摘要描述。
	/// </summary>
    public partial class Paper_QuestionMain : AuthoringTool_BasicForm_BasicForm
	{
		protected string strUserID , strCaseID , strDivisionID , strClinicNum , strSectionName;

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.Initiate();

			//取得參數  
			this.getParameter();

			//加入Next的事件
			btnNext.ServerClick+=new EventHandler(btnNext_ServerClick);
		}

		private void getParameter()
		{
			//SystemFunction
			if(Session["SystemFunction"] != null)
			{
				Session["SystemFunction"] = "EditQuestion";
			}
			else
			{
				Session.Add("SystemFunction" , "EditQuestion");
			}

			//UserID
			if(Request.QueryString["UserID"] != null)
			{
				strUserID = Request.QueryString["UserID"].ToString();
				if(Session["UserID"] != null)
				{
					Session["UserID"] = strUserID;
				}
				else
				{
					Session.Add("UserID",strUserID);
				}
			}
			else
			{
				if(Session["UserID"] != null)
				{
					strUserID = Session["UserID"].ToString();
				}
				else
				{
					strUserID = "swakevin";
					Session.Add("UserID",strUserID);
				}
			}
			
			//CaseID kyhCase200505301448128593750
			if(Request.QueryString["CaseID"] != null)
			{
				strCaseID = Request.QueryString["CaseID"].ToString();
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
					strCaseID = "gait001Case200401061006167084912";
					Session.Add("CaseID",strCaseID);
				}
			}

			//ClinicNum
			if(Request.QueryString["ClinicNum"] != null)
			{
				strClinicNum = Request.QueryString["ClinicNum"].ToString();
				if(Session["ClinicNum"] != null)
				{
					Session["ClinicNum"] = strClinicNum;
				}
				else
				{
					Session.Add("ClinicNum",strClinicNum);
				}
			}
			else
			{
				if(Session["ClinicNum"] != null)
				{
					strClinicNum = Session["ClinicNum"].ToString();
				}
				else
				{
					strClinicNum = "1";
					Session.Add("ClinicNum",strClinicNum);
				}
			}

			//SectionName 測驗
			if(Request.QueryString["SectionName"] != null)
			{
				strSectionName = Request.QueryString["SectionName"].ToString();
				if(Session["SectionName"] != null)
				{
					Session["SectionName"] = strSectionName;
				}
				else
				{
					Session.Add("SectionName",strSectionName);
				}
			}
			else
			{
				if(Session["SectionName"] != null)
				{
					strSectionName = Session["SectionName"].ToString();
				}
				else
				{
					strSectionName = "Examination";
					Session.Add("SectionName",strSectionName);
				}
			}

			//Opener
			if(Session["Opener"] != null)
			{
				hiddenOpener.Value = Session["Opener"].ToString();
			}

			//Setup opener
			if(Session["Opener"] != null)
			{
				Session["Opener"] = "Paper_QuestionMain";
			}
			else
			{
				Session.Add("Opener","Paper_QuestionMain");
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
			//取得使用者所選取的功能
			string strQuestionFunction = "";
			if(rb1.Checked == true)
			{
				//新增題目
				strQuestionFunction = "New";

				//設定bModify
				if(Session["bModify"] != null)
				{
					Session["bModify"] = false;
				}
				else
				{
					Session.Add("bModify" , false);
				}
			}
			else
			{
				//修改或是刪除題目
				strQuestionFunction = "Modify";

				//設定bModify
				if(Session["bModify"] != null)
				{
					Session["bModify"] = true;
				}
				else
				{
					Session.Add("bModify" , true);
				}
			}

			//加入Session QuestionFunction
			if(Session["QuestionFunction)"] != null)
			{
				Session["QuestionFunction"] = strQuestionFunction;
			}
			else
			{
				Session.Add("QuestionFunction" , strQuestionFunction);
			}

			//加入Session ModifyType
			if(Session["ModifyType)"] != null)
			{
				Session["ModifyType"] = "Question";
			}
			else
			{
				Session.Add("ModifyType" , "Question");
			}

			//加入QuestionMode
			if(Session["QuestionMode"] != null)
			{
				Session["QuestionMode"] = "General";
			}
			else
			{
				Session.Add("QuestionMode" , "General");
			}

			if(rb1.Checked == true)
			{
				Response.Redirect("Paper_QuestionType.aspx?Opener=Paper_QuestionMain");
			}
            else if (rb2.Checked == true)
            {
                // modified by dolphin @ 2006-07-29, new Question Group Tree
                //Response.Redirect("./QuestionGroupTree/QuestionGroupTree.aspx");
                Response.Redirect("./QuestionGroupTree/QGroupTree.aspx");
            }
            else 
            {
                Response.Redirect("/Hints/Flow control/navigator_case.aspx");
            }
		}
	}
}
