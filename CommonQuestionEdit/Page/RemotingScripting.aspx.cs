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
using Ajax;

namespace AuthoringTool.CommonQuestionEdit
{
	/// <summary>
	/// RemotingScripting 的摘要描述。
	/// </summary>
	public partial class RemotingScripting : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		[Ajax.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
		public void ChangeTextBoxContentForQuestion(string strQID,string strQuestionText)
		{
            ((QuestionAccessor)this.Page.Session["QuestionAccessor"]).modifyQuestionText(strQID, strQuestionText);
		}

		[Ajax.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
		public void chkbox_CheckedChanged(string strControlID,bool IsCheck)
		{
			string tmpQID =	strControlID.Split('@')[1].Split('#')[0];  					//取得要選項所屬的問題的ID
			string tmpSelectionID = strControlID.Split('@')[1].Split('#')[1];           //取得選項ID
			((QuestionSelectionAccessor)Session["QuestionSelectionAccessor"]).modifySelectionCorrect(tmpQID,tmpSelectionID,IsCheck);
		}

		[Ajax.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
		public void ChangeTextBoxContentForSelection(string strSelectionID,string strSelectionText)
		{
			string strQID = ((QuestionSelectionAccessor)this.Page.Session["QuestionSelectionAccessor"]).GetQIDBySelectionID(strSelectionID);
			((QuestionSelectionAccessor)this.Page.Session["QuestionSelectionAccessor"]).modifySelectionText(strQID,strSelectionID,strSelectionText);
		}

		[Ajax.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
		public void ChangeTextBoxContentForSelectionRespons(string strSelectionID,string strResponseText)
		{
			string strQID = ((QuestionSelectionAccessor)this.Page.Session["QuestionSelectionAccessor"]).GetQIDBySelectionID(strSelectionID);
			((QuestionSelectionAccessor)this.Page.Session["QuestionSelectionAccessor"]).modifySelectionResponseText(strQID,strSelectionID,strResponseText);
		}

        [Ajax.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
        public void ChangeTextBoxContentForKeyWords(string strQID, string strKeyWordsText)
        {
            ((QuestionAccessor)this.Page.Session["QuestionAccessor"]).modifyKeyWordsText(strQID, strKeyWordsText);
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
