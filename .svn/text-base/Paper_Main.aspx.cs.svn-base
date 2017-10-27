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
	/// Paper_Main 的摘要描述。
	/// </summary>
    public partial class Paper_Main : AuthoringTool_BasicForm_BasicForm
	{
		protected string strUserID , strCaseID , strDivisionID , strClinicNum , strSectionName;

		//建立SqlDB物件
		SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);

		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.Initiate();

			//取得參數  
			this.getParameter();

			//取得目前是否有以存在的問卷
			string strSQL = "SELECT * FROM Paper_CaseDivisionSection WHERE cCaseID = '"+strCaseID+"' AND sClinicNum = '"+strClinicNum+"' AND cSectionName = '"+strSectionName+"' ";
			DataSet dsPaper = sqldb.getDataSet(strSQL);
			if(dsPaper.Tables[0].Rows.Count > 0)
			{
				//存在問卷，Redirce至Paper_MainPage.aspx
				trModify.Style.Add("DISPLAY","none");
				trPreview.Style.Add("DISPLAY","none");

				//取出PaperID加入Session
				string strPaperID = "";
				try
				{
					strPaperID = dsPaper.Tables[0].Rows[0]["cPaperID"].ToString();
				}
				catch
				{
				}

				if(Session["PaperID"] != null)
				{
					Session["PaperID"] = strPaperID;
				}
				else
				{
					Session.Add("PaperID",strPaperID);
				}

				Response.Redirect("Paper_MainPage.aspx?Opener=Paper_Main");
			}
			else
			{	
				//不存在問卷，把修改問卷與預覽問卷的功能關閉
				trModify.Style.Add("DISPLAY","none");
				trPreview.Style.Add("DISPLAY","none");

				Response.Redirect("Paper_InputName.aspx?Opener=Paper_Main");
			}
			dsPaper.Dispose();
		}

		private void getParameter()
		{
			//加入Session ModifyType
			if(Session["ModifyType)"] != null)
			{
				Session["ModifyType"] = "Paper";
			}
			else
			{
				Session.Add("ModifyType" , "Paper");
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
				Session["Opener"] = "Paper_Main";
			}
			else
			{
				Session.Add("Opener","Paper_Main");
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
