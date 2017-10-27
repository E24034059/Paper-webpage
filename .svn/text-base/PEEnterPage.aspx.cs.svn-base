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
	/// PEEnterPage 編輯PE問題的入口網頁，主要功能在產生PaperID，並且將網頁導入Paper_PresentMethod，本身沒有UI。
	/// </summary>
    public partial class PEEnterPage : AuthoringTool_BasicForm_BasicForm
	{
		//建立SqlDB物件
		SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
		DataReceiver myReceiver = new DataReceiver();
		SQLString mySQL = new SQLString();

		string strCaseID ,  strSectionName , strItem;
		string strUserID = "";
		string strPaperID = "";
        int iVisitNum = 0;

		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.Initiate();

			this.getParameter();

			//把PaperPurpose存入Session
			this.savePaperPurposeIntoSession();

			this.saveModifyTypeIntoSession();

			//判斷此PE部位是否有編輯問卷
            string strSQL = "SELECT cPaperID FROM Paper_PEQuestion WHERE cCaseID = '" + strCaseID + "' AND sClinicNum = '" + iVisitNum + "' AND cSectionName = '" + strSectionName + "' AND cItem = '" + strItem + "' ";
			DataSet dsPE = sqldb.getDataSet(strSQL);
			if(dsPE.Tables[0].Rows.Count > 0)
			{
				//有 取出PaperID並Redirect至Paper_MainPage.aspx
				strPaperID = dsPE.Tables[0].Rows[0]["cPaperID"].ToString();

				this.savePaperIDIntoSession();

				Response.Redirect("Paper_MainPage.aspx?Item="+strItem);
			}
			else
			{
				//沒有 產生PaperID 並Redirect至Paper_Main.aspx

				//get now time
				string strNowTime = myReceiver.getNowTime();

				//setup PaperID
				strPaperID = strUserID + strNowTime;

				this.savePaperIDIntoSession();

				//把資料存入Paper_Header
                mySQL.saveToPaper_Header(strPaperID, "Physical examination", strItem, "General", "Author", "Edit", "All", "30", "10");

				//把資料儲存至Paper_PEQuestion
                strSQL = "Insert Into Paper_PEQuestion (cCaseID , sClinicNum , cSectionName , cItem , cPaperID) VALUES ('" + strCaseID + "' , '" + iVisitNum + "' , '" + strSectionName + "' , '" + strItem + "' , '" + strPaperID + "')";
				sqldb.ExecuteNonQuery(strSQL);

				Response.Redirect("Paper_PresentMethod.aspx");
			}
			dsPE.Dispose();
		}

		private void savePaperIDIntoSession()
		{
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
		}

		private void savePaperPurposeIntoSession()
		{
			//把PaperPurpose存入Session
			if(Session["PaperPurpose"] != null)
			{
				Session["PaperPurpose"] = "PEQuestion";
			}
			else
			{
				Session.Add("PaperPurpose","PEQuestion");
				Session.Timeout = 100;
			}
		}

		private void saveModifyTypeIntoSession()
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
		}

		private void saveItemIntoSession()
		{
			//加入Session ModifyType
			if(Session["Item)"] != null)
			{
				Session["Item"] = strItem;
			}
			else
			{
				Session.Add("Item" , strItem);
			}
		}

		private void getParameter()
		{   // checked @ 2006-06-29 by dolphin, EditPE_HumanBody.aspx will pass only the 'Item' parameter.
            if (Request.QueryString["UserID"] != null)   // null
            {
                strUserID = Request.QueryString["UserID"].ToString();
            }
            if (Session["UserID"] != null)   // not null, removed @ 2006-0629 by dolphin
            {
                strUserID = usi.UserID;
                Session["UserID"] = strUserID;
            }
            else
            {
                Session.Add("UserID", strUserID);
            }

            if (Request.QueryString["CaseID"] != null)   // null
            {
                strCaseID = Request.QueryString["CaseID"].ToString();
            }
            if (Session["CaseID"] != null)   // not null
            {
                strCaseID = usi.CaseID;
                Session["CaseID"] = strCaseID;
            }
            else
            {
                Session.Add("CaseID", strCaseID);
            }

            if (Request.QueryString["ClinicNum"] != null)
            {
                iVisitNum = Convert.ToInt16(Request.QueryString["ClinicNum"].ToString());
            }
            if (Session["ClinicNum"] != null)
            {
                iVisitNum = usi.ClinicNum;
                Session["ClinicNum"] = iVisitNum;
            }
            else
            {
                Session.Add("ClinicNum", iVisitNum);
            }

            if (Request.QueryString["SectionName"] != null)
            {
                strSectionName = Request.QueryString["SectionName"].ToString();
            }
            if (Session["SectionName"] != null)
            {
                strSectionName = usi.Section;
                Session["SectionName"] = strSectionName;
            }
            else
            {
                Session.Add("SectionName", strSectionName);
            }

			if(Request.QueryString["Item"] != null)
			{
				strItem = Request.QueryString["Item"].ToString();
			}
			if(Session["Item"] != null)
			{
				Session["Item"] = strItem;
			}
			else
			{
				Session.Add("Item" , strItem);
			}

			//Setup opener, for wizard
			if(Session["Opener"] != null)
			{
				Session["Opener"] = "PEEnterPage";
			}
			else
			{
				Session.Add("Opener","PEEnterPage");
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
