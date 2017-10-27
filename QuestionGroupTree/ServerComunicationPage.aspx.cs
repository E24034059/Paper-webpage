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

namespace WebApplication1
{
	/// <summary>
	/// 此網頁處理"Mt_Favorite.aspx"跟資料庫之間的溝通
	/// </summary>
	public partial class ServerComunicationPage : System.Web.UI.Page
	{
		private string HandleType = "";//要處理的事情型態
	    private string NodeID = "";//要處理的節點ID
		private string Parameter = "";//要處理的節點參數,視要處理的事情型態不同而有不同的意義,例如若要修改一個資料夾(節點)名稱,則Parameter就是新名稱
        private SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
		private string Directory_DataTable = "QuestionGroupTree";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			init();
			ProcessHandleType();
		}

		/// <summary>
		/// 將QueryString設定給此網頁的類別屬性
		/// </summary>
		private void init()
		{
			HandleType = Request.QueryString["HandleType"] + "";
			NodeID = Request.QueryString["NodeID"] + "";
			Parameter = Request.QueryString["Parameter"] + "";
		}

		/// <summary>
		/// 處理HandleType要作的事情
		/// </summary>
		private void ProcessHandleType()
		{
			switch(HandleType)
			{
				case "Update_Folder_Name":
					Update_Folder_Name();
					break;
			}
		}

		/// <summary>
		/// 更新資料夾的名稱
		/// </summary>
		private void Update_Folder_Name()
		{
			string strSQL = "SELECT * FROM " + Directory_DataTable + " WHERE cNodeID='"+NodeID+"'";
			DataTable dt = sqldb.getDataSet(strSQL).Tables[0];
			DataRow dr = dt.Rows[0];
			dr["cNodeName"] = Parameter;
			sqldb.Update(dt,"SELECT * FROM " + Directory_DataTable);
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
