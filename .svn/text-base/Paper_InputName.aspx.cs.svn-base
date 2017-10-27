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
using Hints.DB;

namespace PaperSystem
{
	/// <summary>
	/// Paper_InputName 的摘要描述。
	/// </summary>
    public partial class Paper_InputName : AuthoringTool_BasicForm_BasicForm
	{
		//建立SqlDB物件
		SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
		DataReceiver myReceiver = new DataReceiver();
		SQLString mySQL = new SQLString();

		string strUserID , strCaseID , strClinicNum , strSectionName , strPaperID;

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.Initiate();

			this.getParameter();

			//取得Case name
			if(this.IsPostBack == false)
			{
				txtName.Value = myReceiver.getCaseName(strCaseID);
			}

			//get now time
			string strNowTime = myReceiver.getNowTime();

			//setup PaperID
			string strPaperID;
			strPaperID = strUserID + strNowTime;//"swakevin20050907120001";//
			hiddenPaperID.Value = strPaperID;

			//把PaperID存入Session
			if(Session["PaperID"] != null)
			{
				Session["PaperID"] = strPaperID;
			}
			else
			{
				Session.Add("PaperID",strPaperID);
				Session.Timeout = 100;
			}

			//加入Submit事件
			btnNext.ServerClick+=new EventHandler(btnNext_ServerClick);

            #region WebPage物件管理
            //從資料表取得物件的管理型態
            DataTable dtWebPageObjectManage = clsWebPageObjectManage.WebPageObjectManage("Paper_InputName.aspx", usi.UsingSystem);
            //根據資料表的值做一一對應的型態管理
            if (dtWebPageObjectManage != null)
            {
                //根據資料表的值做一一對應的型態管理
                btnPre.Visible = Hints.TableStyle.CheckWebPageObjectManage(dtWebPageObjectManage.Rows[0]["cObjectType"].ToString(), 0);
            }
            if (usi.UsingSystem == "MLAS")
            {
               // body_content.Style.Add("width","99%");
                txtTitle.Style.Add("height","250px");
            }
            #endregion

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

			//Setup opener
			if(Session["Opener"] != null)
			{
				Session["Opener"] = "Paper_InputName";
			}
			else
			{
				Session.Add("Opener","Paper_InputName");
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
			strPaperID = hiddenPaperID.Value;
			string strPaperName = txtName.Value;

			string strTitle = txtTitle.InnerText;

			//把資料存入Paper_Header
			mySQL.saveToPaper_Header(strPaperID , strPaperName , strTitle , "General" , "Author" , "Edit" , "All" , "30" , "10");

			mySQL.saveToPaper_CaseDivisionSection(strPaperID , strCaseID , strClinicNum , strSectionName);
			
			Response.Redirect("Paper_PresentMethod.aspx");
		}
	}
}
