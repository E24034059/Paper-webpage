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
	/// test 的摘要描述。
	/// </summary>
    public partial class test : AuthoringTool_BasicForm_BasicForm
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.Initiate();

			//CONTOURMANIPULATELib.ContourManipulateClass b = new ContourManipulateClass();
			//b.AboutBox();
			
//			try
//			{
//				int i = b.TestCall();
//			}
//			catch(System.Runtime.InteropServices.COMException ex)
//			{
//				ex.HelpLink.ToString();
////				(System.Runtime.InteropServices.COMException(e))
//			}
			//b.GradeContours("0,0;0,9;9,9;9,0;|","0,0;0,9;9,9;9,0;|",1,1);
			// 在這裡放置使用者程式碼以初始化網頁
			CheckBox ch = new CheckBox();
			this.FindControl("Form1").Controls.Add(ch);
			ch.Text = "Test";
			ch.ID = "ch-Test";
			
			Button btnSubmit = new Button();
			this.FindControl("Form1").Controls.Add(btnSubmit);
			btnSubmit.ID = "btnSubmit";
			btnSubmit.Text = "Submit";
			btnSubmit.Click+=new EventHandler(btnSubmit_Click);
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

		private void btnSubmit_Click(object sender, EventArgs e)
		{
			bool bCheckBox = ((CheckBox)(this.FindControl("Form1").FindControl("ch-Test"))).Checked;
		}
	}
}
