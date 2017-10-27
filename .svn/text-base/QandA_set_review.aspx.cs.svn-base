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
	/// QandA_set_retry 的摘要描述。
	/// </summary>
    public partial class QandA_set_review : AuthoringTool_BasicForm_BasicForm
	{
		//protected clsHintsDB hintsDB = new clsHintsDB();

		protected string cCaseID = "";
		protected string CurrentTerm = "";
		protected string SectionName= "";
		protected string cQID = "";
		protected string cDivisionID="";
		protected string cUserlevel= "";
		protected string settype= "";
		protected int cValue=1;
		protected int cValue3=100;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在這裡放置使用者程式碼以初始化網頁
			cCaseID = Request.QueryString["CaseID"];
			CurrentTerm = Request.QueryString["CurrentTerm"];
			SectionName= Request.QueryString["SectionName"];
			cQID = Request.QueryString["cQID"];
			cUserlevel= Request.QueryString["userlevel"];
			settype= Request.QueryString["settype"];
			cDivisionID = Request.QueryString["cDivisionID"];

            this.Initiate();

			SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);

			if( ! this.IsPostBack )
			{
				if (settype=="insert_review")
				{
					Radio1.Checked=true;
					cValue=1;
					cValue3=100;
					TextBox1.Text="1";
					TextBox2.Text="100";
					TextBox1.ReadOnly=true;
					TextBox2.ReadOnly=true;
				}
				else
				{
					if (settype=="update_review")
					{
						string mystrSql = "Select cValue,cValue3 From Question_Retry Where cCaseID = '" + cCaseID + "' And cQID = '" + cQID + "' And cSectionName = '" + SectionName + "' And  cUserLevel = '" + cUserlevel + "' And  CurrentTerm = '" + CurrentTerm + "'And cDescription='review'";
						DataTable dtResult = new DataTable();
						dtResult = sqldb.getDataSet(mystrSql).Tables[0];
						cValue=int.Parse(dtResult.Rows[0]["cValue"].ToString());
						cValue3=int.Parse(dtResult.Rows[0]["cValue3"].ToString());
						this.ViewState["cValue"]=cValue;
						this.ViewState["cValue3"]=cValue3;

						switch (cValue)
						{
							case 1:
								Radio1.Checked=true;
								TextBox1.Text="100";
								TextBox2.Text="100";
								TextBox1.ReadOnly=true;
								TextBox2.ReadOnly=true;

								break;

							case 2:
								Radio2.Checked=true;
								TextBox1.Text="100";
								TextBox2.Text="100";
								TextBox1.ReadOnly=true;
								TextBox2.ReadOnly=true;
								break;
					
							case 3:
								Radio3.Checked=true;
								TextBox1.Text="100";
								TextBox2.Text="100";
								TextBox1.ReadOnly=true;
								TextBox2.ReadOnly=true;
								break;
					
							case 4:
								Radio4.Checked=true;
								TextBox1.Text="100";
								TextBox2.Text="100";
								TextBox1.ReadOnly=true;
								TextBox2.ReadOnly=true;
								break;

							case 5:
								Radio5.Checked=true;
								TextBox1.Text=dtResult.Rows[0]["cValue3"].ToString();
								TextBox2.Text="100";
								TextBox1.ReadOnly=false;
								TextBox2.ReadOnly=true;
								break;

							case 6:
								Radio6.Checked=true;
								TextBox1.Text="100";
								TextBox2.Text=dtResult.Rows[0]["cValue3"].ToString();
								TextBox1.ReadOnly=true;
								TextBox2.ReadOnly=false;
								break;

							default:
								Radio1.Checked=true;
								cValue=1;
								cValue3=100;
								TextBox1.Text="100";
								TextBox2.Text="100";
								TextBox1.ReadOnly=true;
								TextBox2.ReadOnly=true;
								break;
						}
					}
					else
					{
						Response.Redirect("QandA_select_setRetry.aspx");
					}
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

		protected void Radio1_CheckedChanged(object sender, System.EventArgs e)
		{
			
			this.ViewState["cValue"]=1;
			TextBox1.ReadOnly=true;
			TextBox2.ReadOnly=true;
		}

		protected void Radio2_CheckedChanged(object sender, System.EventArgs e)
		{
			
			this.ViewState["cValue"]=2;
			TextBox1.ReadOnly=true;
			TextBox2.ReadOnly=true;
		}

		protected void Radio3_CheckedChanged(object sender, System.EventArgs e)
		{
			
			this.ViewState["cValue"]=3;
			TextBox1.ReadOnly=true;
			TextBox2.ReadOnly=true;

		}

		protected void Radio4_CheckedChanged(object sender, System.EventArgs e)
		{
			this.ViewState["cValue"]=4;
			TextBox1.ReadOnly=true;
			TextBox2.ReadOnly=true;

		}

		protected void Radio5_CheckedChanged(object sender, System.EventArgs e)
		{
            this.ViewState["cValue"]=5;
			TextBox1.ReadOnly=false;
			TextBox2.ReadOnly=true;
		}

		protected void Radio6_CheckedChanged(object sender, System.EventArgs e)
		{
			this.ViewState["cValue"]=6;
			TextBox1.ReadOnly=true;
			TextBox2.ReadOnly=false;
		}
		protected void TextBox1_TextChanged(object sender, System.EventArgs e)
		{
			this.ViewState["cValue3"]=int.Parse(TextBox1.Text.ToString());
		}

		protected void TextBox2_TextChanged(object sender, System.EventArgs e)
		{
			this.ViewState["cValue3"]=int.Parse(TextBox2.Text.ToString());
		}

		

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			cValue=int.Parse(this.ViewState["cValue"].ToString());
			
			SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
			if (cValue==5)
			{
				cValue3=int.Parse(TextBox1.Text.ToString());
			}
			else
			{
				if (cValue==6)
				{
					cValue3=int.Parse(TextBox2.Text.ToString());
				}
				else
				{
					cValue3=100;
				}
			}

				string mystrSql2 = "Select cValue From Question_Retry Where cCaseID = '" + cCaseID + "' And cQID = '" + cQID + "'  And cDivisionID = '" + cDivisionID + "'  And cSectionName = '" + SectionName + "' And  cUserLevel = '" + cUserlevel + "' And  CurrentTerm = '" + CurrentTerm + "'And cDescription='review'";
				DataTable dtResult = new DataTable();
				dtResult = sqldb.getDataSet(mystrSql2).Tables[0];
				if( dtResult.Rows.Count > 0)
				{
					string mySQL = "update Question_Retry set cValue='"+cValue+"',cValue3='"+cValue3+"' Where cCaseID = '" + cCaseID + "' And cQID = '" + cQID + "'  And cDivisionID = '" + cDivisionID + "'  And cSectionName = '" + SectionName + "' And  cUserLevel = '" + cUserlevel + "' And  CurrentTerm = '" + CurrentTerm + "'And cDescription='review'";
					sqldb.ExecuteNonQuery(mySQL);
					
				}
				else
				{
					string mySQL = "insert into Question_Retry (cCaseID,CurrentTerm,cSectionName,cQID,cDivisionID,cUserLevel,cDescription,cValue,cValue3) values('"+cCaseID+"','"+CurrentTerm+"','"+SectionName+"','"+cQID+"','"+cDivisionID+"','"+cUserlevel+"','review','"+cValue+"','"+cValue3+"')";
					sqldb.ExecuteNonQuery(mySQL);
				}

				Response.Redirect("QandA_select_setRetry.aspx");

			}

		}


	
}
