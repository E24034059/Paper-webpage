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
	/// HintsPaper_StatisticSelection 的摘要描述。
	/// </summary>
    public partial class HintsPaper_StatisticSelection : AuthoringTool_BasicForm_BasicForm
	{
		string strPaperID = "";
		string strFunction = "0";

		//建立SqlDB物件
		SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
		DataReceiver myReceiver = new DataReceiver();
		SQLString mySQL = new SQLString();
		protected System.Web.UI.HtmlControls.HtmlInputImage btnFunction;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.Initiate();

			//取得參數
			this.getParameter();

			if(this.IsPostBack == false)
			{
				//設定DropDownList的開啟與否
				this.setupDropDownList();
			}

			//加入事件
			dlAuthor.SelectedIndexChanged+=new EventHandler(dlAuthor_ServerChange);
			dlCase.SelectedIndexChanged+=new EventHandler(dlCase_ServerChange);
			dlClass.SelectedIndexChanged+=new EventHandler(dlClass_ServerChange);
			dlGroup.SelectedIndexChanged+=new EventHandler(dlGroup_SelectedIndexChanged);

            btnSubmit.Value = this.GetMultiLanguageString("SubmitPaper");
            btnSubmit.ServerClick += new EventHandler(btnSubmit_ServerClick);
		}

		private void setupDropDownList()
		{
			this.setupAuthor();
			this.setupCase("Author");
			this.setupClass("Case");
			this.setupGroup("Case");
		}

		private void setupCase(string strMethod)
		{
			//清除內容
			dlCase.Items.Clear();

			//建立Case的DropDownList
			string strSQL = "";
			if(strMethod == "Case")
			{
				//By Case
				strSQL = mySQL.getCasePaperList(strPaperID);
			}
			else
			{
				//By Author
				strSQL = mySQL.getAuthorCasePaperList(strPaperID , hiddenAuthor.Value);
			}

			DataSet dsCase = sqldb.getDataSet(strSQL);
			if(dsCase.Tables[0].Rows.Count > 0)
			{
				for(int i=0 ; i<dsCase.Tables[0].Rows.Count ; i++)
				{
					//有資料就把資料加入DropDownList
					string strText = "";
					try
					{
						strText = dsCase.Tables[0].Rows[i]["cCaseName"].ToString();
					}
					catch
					{
					}
					string strValue = "";
					try
					{
						strValue = dsCase.Tables[0].Rows[i]["cCaseID"].ToString();
					}
					catch
					{
					}
					ListItem liCase = new ListItem(strText , strValue);
					dlCase.Items.Add(liCase);

				}

				//加入None
				ListItem liNone = new ListItem("None","None");
				dlCase.Items.Add(liNone);
			}
			else
			{
				//沒有資料的情形
				ListItem liCase = new ListItem("None" , "None");
				dlCase.Items.Add(liCase);
			}
			//設定hiddenCase 
			string Value = "";
			if(dsCase.Tables[0].Rows.Count > 0)
			{
				Value = dsCase.Tables[0].Rows[0]["cCaseID"].ToString();
				
			}
			else
			{
				Value = "None";
			}
			hiddenCase.Value = Value;
			if(Session["CaseID"] != null)
			{
				Session["CaseID"] = Value;
			}
			else
			{
				Session.Add("CaseID",Value);
			}
			dsCase.Dispose();
		}

		private void setupAuthor()
		{
			//清除內容
			dlAuthor.Items.Clear();

			//建立Author的DropDownList
			string strSQL = mySQL.getAuthorPaperList(strPaperID);
			
			DataSet dsAuthor = sqldb.getDataSet(strSQL);
			if(dsAuthor.Tables[0].Rows.Count > 0)
			{
				for(int i=0 ; i<dsAuthor.Tables[0].Rows.Count ; i++)
				{
					//有資料就把資料加入DropDownList
					string strText = "";
					try
					{
						strText = dsAuthor.Tables[0].Rows[i]["cFullName"].ToString();
					}
					catch
					{
					}
					string strValue = "";
					try
					{
						strValue = dsAuthor.Tables[0].Rows[i]["cFullName"].ToString();
					}
					catch
					{
					}
					ListItem liAuthor = new ListItem(strText , strValue);
					dlAuthor.Items.Add(liAuthor);

				}

				//加入None
				ListItem liNone = new ListItem("None","None");
				dlAuthor.Items.Add(liNone);
			}
			else
			{
				//沒有資料的情形
				ListItem liAuthor = new ListItem("None" , "None");
				dlAuthor.Items.Add(liAuthor);
			}
			//設定hiddenAuthor
			string Value = "";
			if(dsAuthor.Tables[0].Rows.Count > 0)
			{
				Value = dsAuthor.Tables[0].Rows[0]["cFullName"].ToString();
				
			}
			else
			{
				Value = "None";
			}
			hiddenAuthor.Value = Value;

			if(Session["Author"] != null)
			{
				Session["Author"] = Value;
			}
			else
			{
				Session.Add("Author",Value);
			}
			dsAuthor.Dispose();

		}

		private void setupClass(string strMethod)
		{
			//清除內容
			dlClass.Items.Clear();

			//建立Class的DropDownList
			string strSQL = "";
			if(strMethod == "Class")
			{
				//By Class
				strSQL = mySQL.getClassPaperList(strPaperID);
			}
			else if(strMethod == "Author")
			{
				//By Author
				strSQL = mySQL.getAuthorClassPaperList(strPaperID , hiddenAuthor.Value);
			}
			else
			{
				//By Case
				strSQL = mySQL.getCaseClassPaperList(strPaperID , hiddenCase.Value);
			}

			DataSet dsClass = sqldb.getDataSet(strSQL);
			if(dsClass.Tables[0].Rows.Count > 0)
			{
				for(int i=0 ; i<dsClass.Tables[0].Rows.Count ; i++)
				{
					//有資料就把資料加入DropDownList
					string strText = "";
					try
					{
						strText = dsClass.Tables[0].Rows[i]["cClass"].ToString();
					}
					catch
					{
					}
					string strValue = "";
					try
					{
						strValue = dsClass.Tables[0].Rows[i]["cClass"].ToString();
					}
					catch
					{
					}
					ListItem liClass = new ListItem(strText , strValue);
					dlClass.Items.Add(liClass);
				}
				
				//加入None
				ListItem liNone = new ListItem("None","None");
				dlClass.Items.Add(liNone);
				
			}
			else
			{
				//沒有資料的情形
				ListItem liClass = new ListItem("None" , "None");
				dlClass.Items.Add(liClass);
			}

			//設定hiddenCase 
			string Value = "";
			if(dsClass.Tables[0].Rows.Count > 0)
			{
				Value = dsClass.Tables[0].Rows[0]["cClass"].ToString();
				
			}
			else
			{
				Value = "None";
			}
			hiddenClass.Value = Value;
			if(Session["Class"] != null)
			{
				Session["Class"] = Value;
			}
			else
			{
				Session.Add("Class",Value);
			}
			dsClass.Dispose();
		}

		private void setupGroup(string strMethod)
		{
			//清除內容
			dlGroup.Items.Clear();

			//建立Group的DropDownList
			string strSQL = "";
			if(strMethod == "Group")
			{
				//By Group
				strSQL = mySQL.getGroupPaperList(strPaperID);
			}
			else if(strMethod == "Author")
			{
				//By Author
				strSQL = mySQL.getAuthorGroupPaperList(strPaperID , hiddenClass.Value , hiddenAuthor.Value);
			}
			else if(strMethod == "Class")
			{
				//By Class
				strSQL = mySQL.getClassGroupPaperList(strPaperID , hiddenClass.Value);
			}
			else
			{
				//By Case
				strSQL = mySQL.getCaseGroupPaperList(strPaperID , hiddenClass.Value , hiddenCase.Value);
			}
			DataSet dsGroup = sqldb.getDataSet(strSQL);
			if(dsGroup.Tables[0].Rows.Count > 0)
			{
				for(int i=0 ; i<dsGroup.Tables[0].Rows.Count ; i++)
				{
					//有資料就把資料加入DropDownList
					string strText = "";
					try
					{
						strText = dsGroup.Tables[0].Rows[i]["cGroup"].ToString();
					}
					catch
					{
					}
					string strValue = "";
					try
					{
						strValue = dsGroup.Tables[0].Rows[i]["cGroup"].ToString();
					}
					catch
					{
					}
					ListItem liGroup = new ListItem(strText , strValue);
					dlGroup.Items.Add(liGroup);

				}

				//加入None
				ListItem liNone = new ListItem("None","None");
				dlGroup.Items.Add(liNone);
			}
			else
			{
				//沒有資料的情形
				ListItem liGroup = new ListItem("None" , "None");
				dlGroup.Items.Add(liGroup);
			}

			//設定hiddenCase 
			string Value = "";
			if(dsGroup.Tables[0].Rows.Count > 0)
			{
				Value = dsGroup.Tables[0].Rows[0]["cGroup"].ToString();
				
			}
			else
			{
				Value = "None";
			}
			hiddenGroup.Value = Value;

			if(Session["Group"] != null)
			{
				Session["Group"] = Value;
			}
			else
			{
				Session.Add("Group",Value);
			}
			dsGroup.Dispose();
		}

        private void getParameter()
        {
            //PaperID
            if (Session["PaperID"] != null)
            {
                strPaperID = Session["PaperID"].ToString();
            }
            else
            {
                // modified @ 2007-03-22 by dolphin, modified the Query string check
                if (Request["PaperID"] != null && Request.QueryString["PaperID"] != "")
                {
                    strPaperID = Request.QueryString["PaperID"].ToString();
                    Session["PaperID"] = Request.QueryString["PaperID"].ToString();
                }
            }

            //Function
            if (Request.QueryString["Function"] != null)
            {
                strFunction = Request.QueryString["Function"].ToString();
                hiddenFunction.Value = strFunction;
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

        void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            //把Hidden的直存入Session

            //Mode
            if (Session["Mode"] != null)
            {
                Session["Mode"] = hiddenMode.Value;
            }
            else
            {
                Session.Add("Mode", hiddenMode.Value);
            }

            //CaseID
            if (Session["CaseID"] != null)
            {
                Session["CaseID"] = hiddenCase.Value;
            }
            else
            {
                Session.Add("CaseID", hiddenCase.Value);
            }

            //AuthorID
            if (Session["AuthorID"] != null)
            {
                Session["AuthorID"] = hiddenAuthor.Value;
            }
            else
            {
                Session.Add("AuthorID", hiddenAuthor.Value);
            }

            //Class
            if (Session["Class"] != null)
            {
                Session["Class"] = hiddenClass.Value;
            }
            else
            {
                Session.Add("Class", hiddenClass.Value);
            }

            //Group
            if (Session["Group"] != null)
            {
                Session["Group"] = hiddenGroup.Value;
            }
            else
            {
                Session.Add("Group", hiddenGroup.Value);
            }

            //呼叫Client端的OnSubmit
            string strScript = "<script language='javascript'>\n";
            strScript += "Submit();\n";
            strScript += "</script>\n";
            Page.RegisterStartupScript("Submit", strScript);
        }

        private void dlAuthor_ServerChange(object sender, EventArgs e)
		{
			//存入Hidden
			string strValue = dlAuthor.SelectedItem.Value;
			hiddenAuthor.Value = strValue;

			if(Session["Author"] != null)
			{
				Session["Author"] = strValue;
			}
			else
			{
				Session.Add("Author",strValue);
			}

			//呼叫setupClass()
			if(strValue != "None")
			{
				this.setupCase("Author");
				this.setupClass("Case");
				this.setupGroup("Case");
			}
			else
			{
				this.setupCase("Case");
				this.setupClass("Case");
				this.setupGroup("Case");
			}

		}

		private void dlCase_ServerChange(object sender, EventArgs e)
		{
			string strValue = dlCase.SelectedItem.Value;
			//存入Hidden
			hiddenCase.Value = strValue;

			if(Session["CaseID"] != null)
			{
				Session["CaseID"] = strValue;
			}
			else
			{
				Session.Add("CaseID",strValue);
			}

			//建立Class
			if(hiddenAuthor.Value != "None" && hiddenCase.Value != "None")
			{
				this.setupClass("Case");
				this.setupGroup("Case");
			}
			else if(hiddenAuthor.Value != "None" && hiddenCase.Value == "None")
			{
				this.setupClass("Author");
				this.setupGroup("Author");
			}
			else if(hiddenAuthor.Value == "None" && hiddenCase.Value != "None")
			{
				this.setupClass("Case");
				this.setupGroup("Case");
			}
			else if(hiddenAuthor.Value == "None" && hiddenCase.Value == "None")
			{
				this.setupClass("Class");
				this.setupGroup("Class");
			}


		}

		private void dlClass_ServerChange(object sender, EventArgs e)
		{
			string strValue = dlClass.SelectedItem.Value;
			//save to hidden
			hiddenClass.Value = strValue;

			if(Session["Class"] != null)
			{
				Session["Class"] = strValue;
			}
			else
			{
				Session.Add("Class",strValue);
			}

			//建立dlGroup
			if(strValue != "None")
			{
				if(hiddenAuthor.Value != "None" && hiddenCase.Value != "None" && hiddenClass.Value != "None")
				{
					this.setupGroup("Case");
				}
				else if(hiddenAuthor.Value != "None" && hiddenCase.Value == "None" && hiddenClass.Value != "None")
				{
					this.setupGroup("Author");
				}
				else if(hiddenAuthor.Value == "None" && hiddenCase.Value != "None" && hiddenClass.Value != "None")
				{
					this.setupGroup("Case");
				}
				else if(hiddenAuthor.Value == "None" && hiddenCase.Value == "None" && hiddenClass.Value != "None")
				{
					this.setupGroup("Class");
				}
				else if(hiddenAuthor.Value == "None" && hiddenCase.Value == "None" && hiddenClass.Value == "None")
				{
					this.setupGroup("Group");
				}
			}

		}

		private void dlGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			hiddenGroup.Value = dlGroup.Items[dlGroup.SelectedIndex].Value;

			if(Session["Group"] != null)
			{
				Session["Group"] = dlGroup.Items[dlGroup.SelectedIndex].Value;
			}
			else
			{
				Session.Add("Group",dlGroup.Items[dlGroup.SelectedIndex].Value);
			}
		}
	}
}
