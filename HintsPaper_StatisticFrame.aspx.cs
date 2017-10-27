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
	/// HintsPaper_StatisticFrame 的摘要描述。
	/// </summary>
    public partial class HintsPaper_StatisticFrame : AuthoringTool_BasicForm_BasicForm
	{
		protected string strSelectionURL = "";
		protected string strStatisticURL = "";
		string strPaperID = "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.Initiate();

			//接收參數
			this.getParameter();

			strSelectionURL = "HintsPaper_StatisticSelection.aspx?PaperID="+ strPaperID;
		}

		private void getParameter()
		{
			//PaperID
			if(usi.PaperID != null && usi.PaperID != "")
			{
                strPaperID = usi.PaperID;
			}
			else
			{
                if (Request.QueryString["PaperID"] != null && Request.QueryString["PaperID"] != "") 
				{
					strPaperID = Request.QueryString["PaperID"].ToString();
                    Session["PaperID"] = Request.QueryString["PaperID"].ToString();
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
	}
}
