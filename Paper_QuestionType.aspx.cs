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
	/// Paper_QuestionType 的摘要描述。
	/// </summary>
    public partial class Paper_QuestionType : AuthoringTool_BasicForm_BasicForm
	{
		string strUserID , strCaseID , strDivisionID , strClinicNum , strSectionName , strPaperID , strPresentType;
		protected string strGroupID = "";
		protected string strGroupDivisionID = "";

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.Initiate();

			//接收參數
			this.getParameter();

			//下一步的機制
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
			
			//Division 9801
			if(Session["DivisionID"] != null)
			{
				strDivisionID = Session["DivisionID"].ToString();
			}
			
			//ClinicNum
			if(Session["ClinicNum"] != null)
			{
				strClinicNum = Session["ClinicNum"].ToString();
			}
			
			//SectionName
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
			if(Request.QueryString["Opener"] != null)
			{
				hiddenOpener.Value = Request.QueryString["Opener"].ToString();
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
				strPresentType = Session["PresentType"].ToString();
			}

			//Edit method
			if(Session["EditMode"] != null)
			{
				hiddenEditMode.Value = Session["EditMode"].ToString();
			}

			//GroupID
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
				DataReceiver myReceiver = new DataReceiver();
				strGroupDivisionID = myReceiver.getGroupDivisionID(strGroupID);
				if(Session["GroupDivisionID"] != null)
				{
					Session["GroupDivisionID"] = strGroupDivisionID;
				}
				else
				{
					Session.Add("GroupDivisionID",strGroupDivisionID);
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

		private void btnNext_ServerClick(object sender, EventArgs e)
		{
			string strQuestionType = "";

			if(rb1.Checked == true)
			{
				//選擇題
				strQuestionType = "1";
				//Response.Redirect("../CommonQuestionEdit/Page/ShowQuestion.aspx?Opener=Paper_QuestionType&GroupID="+strGroupID);
				//Response.Redirect("./QuestionGroupTree/QuestionGroupTree.aspx?Opener=Paper_QuestionType");
			}
            else if (rb2.Checked == true)
			{
				//問答題
				strQuestionType = "2";
				//Response.Redirect("Paper_TextQuestionEditor.aspx?Opener=Paper_QuestionType");
			}
            else if (rb3.Checked == true)//20110215
			{
				//問答題
				strQuestionType = "3";
				//Response.Redirect("Paper_TextQuestionEditor.aspx?Opener=Paper_QuestionType");
			}

			//把QuestionType存入Session
			if(Session["QuestionType"] != null)
			{
				Session["QuestionType"] = strQuestionType;
			}
			else
			{
				Session.Add("QuestionType" , strQuestionType);
			}

			if(hiddenQuestionMode.Value == "Specific")
			{
				if(strQuestionType == "1")
				{
					//選擇題
					Response.Redirect("./CommonQuestionEdit/Page/ShowQuestion.aspx?Opener=Paper_QuestionType");
				}
                else if (strQuestionType == "2")//20110215
				{
					//問答題
					Response.Redirect("Paper_TextQuestionEditor.aspx?Opener=Paper_QuestionType");
				}
                else if (strQuestionType == "3")
                {
                    //圖形題
                    Response.Redirect("Paper_SimulatorQE_tree.aspx");
                }
			}
			else
			{
                // modified by dolphin @ 2006-07-29, new Question Group Tree
				//Response.Redirect("./QuestionGroupTree/QuestionGroupTree.aspx?Opener=Paper_QuestionType");
                Response.Redirect("./QuestionGroupTree/QGroupTree.aspx?Opener=Paper_QuestionType");
			}
		}
	}
}
