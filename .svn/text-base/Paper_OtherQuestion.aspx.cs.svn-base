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
	/// Paper_OtherQuestion 的摘要描述。
	/// </summary>
    public partial class Paper_OtherQuestion : AuthoringTool_BasicForm_BasicForm
	{
		SQLString mySQL = new SQLString();
		string strUserID , strCaseID , strClinicNum , strSectionName , strPaperID;

		
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.Initiate();

			this.getParameter();

			//從QuestionMode中把屬於此考卷的Specific問題存入Paper_Content。
			mySQL.saveSpecificToPaper_Content(strPaperID);

			//加入Next的事件
			btnNext.ServerClick+=new EventHandler(btnNext_ServerClick);

			//如果是預先選題的話顯示tr1和tr3，如果是呈現時亂數選題則不顯示。
			if(hiddenPresentType.Value == "Edit")
			{
				//tr1.Style.Add("DISPLAY","");
				//tr3.Style.Add("DISPLAY","");
			}
			else
			{
				//tr1.Style.Add("DISPLAY","none");
				//tr3.Style.Add("DISPLAY","none");
			}
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

			//QuestionGroupID
			if(Session["GroupID"] != null)
			{
				hiddenGroupID.Value = Session["GroupID"].ToString();
			}

			//Opener
			if(Session["Opener"] != null)
			{
				hiddenOpener.Value = Session["Opener"].ToString();
			}

			//Setup opener
			if(Session["Opener"] != null)
			{
				Session["Opener"] = "Paper_OtherQuestion";
			}
			else
			{
				Session.Add("Opener","Paper_OtherQuestion");
			}

			//QuestionMode
			if(Session["QuestionMode"] != null)
			{
				hiddenQuestionMode.Value = Session["QuestionMode"].ToString();
			}

			//PresentType
			if(Session["PresentType"] != null)
			{
				hiddenPresentType.Value = Session["PresentType"].ToString();
			}
		
			//Edit method
			if(Session["EditMode"] != null)
			{
				hiddenEditMode.Value = Session["EditMode"].ToString();
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
			if(hiddenPresentType.Value == "Edit")
			{
				if(rb1.Checked == true)
				{
					if(Session["EditMode"] != null)
					{
						Session["EditMode"] = "Manual";
					}
					else
					{
						Session.Add("EditMode","Manual");
					}

					//設定QuestionMode
					if(Session["QuestionMode"] != null)
					{
						Session["QuestionMode"] = "General";
					}
					else
					{
						Session.Add("QuestionMode","General");
					}

					//從General題庫中手動選題
					//Response.Redirect("./QuestionGroupTree/QuestionGroupTree.aspx?Opener=Paper_OtherQuestion");
                    Response.Redirect("Paper_EditMethod.aspx?Opener=Paper_OtherQuestion");
				}
//				else if(rb2.Checked == true)
//				{
//					if(Session["EditMode"] != null)
//					{
//						Session["EditMode"] = "Automatic";
//					}
//					else
//					{
//						Session.Add("EditMode","Automatic");
//					}
//
//					//設定QuestionMode
//					if(Session["QuestionMode"] != null)
//					{
//						Session["QuestionMode"] = "General";
//					}
//					else
//					{
//						Session.Add("QuestionMode","General");
//					}
//
//					//從General題庫中亂數選題
//					Response.Redirect("./QuestionGroupTree/QuestionGroupTree.aspx?Opener=Paper_OtherQuestion");
//				}
//				else if(rb3.Checked == true)
//				{
//					//設定QuestionMode
//					if(Session["QuestionMode"] != null)
//					{
//						Session["QuestionMode"] = "Specific";
//					}
//					else
//					{
//						Session.Add("QuestionMode","Specific");
//					}
//
//					//編輯其它的Specific問題
//					Response.Redirect("./CommonQuestionEdit/Page/ShowQuestion.aspx?Opener=Paper_OtherQuestion");
//				}
				else if(rb4.Checked == true)
				{
					//完成考卷編輯
					Response.Redirect("Paper_MainPage.aspx?Opener=Paper_OtherQuestion");
				}
			}
			else
			{
//				if(rb2.Checked == true)
//				{
//					Response.Redirect("./QuestionGroupTree/QuestionGroupTree.aspx?Opener=Paper_OtherQuestion");
//				}
//				else if(rb4.Checked == true)
//				{
					Response.Redirect("Paper_MainPage.aspx?Opener=Paper_OtherQuestion");
//				}
			}
		}
	}
}
