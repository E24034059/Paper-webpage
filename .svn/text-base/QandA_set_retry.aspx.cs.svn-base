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
    public partial class QandA_set_retry : AuthoringTool_BasicForm_BasicForm
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
		protected int cValue2=1;
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

            //接收參數
            this.getParameter();

			SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);

			if( ! this.IsPostBack )
			{
				if (settype=="insert_retry")
				{
					Radio1.Checked=true;
					TextBox1.Text="1";
					TextBox2.Text="100";
					TextBox3.Text="1";
					TextBox4.Text="100";
					TextBox1.ReadOnly=true;
					TextBox2.ReadOnly=true;
					TextBox3.ReadOnly=true;
					TextBox4.ReadOnly=true;
                    this.ViewState["cValue"] = 1;
				}
				else
				{
					string Description = "retry";
					string mystrSql = "Select cValue, cValue2,cValue3 From Question_Retry Where cCaseID = '" + cCaseID + "' And cQID = '" + cQID + "'  And cDivisionID = '" + cDivisionID + "' And cSectionName = '" + SectionName + "' And  cUserLevel = '" + cUserlevel + "' And  CurrentTerm = '" + CurrentTerm + "'And  cDescription = '" + Description + "'";
					DataTable dtResult = new DataTable();
					dtResult = sqldb.getDataSet(mystrSql).Tables[0];
					if(dtResult.Rows.Count > 0)
					{
						cValue=int.Parse(dtResult.Rows[0]["cValue"].ToString());
						cValue2=int.Parse(dtResult.Rows[0]["cValue2"].ToString());
						cValue3=int.Parse(dtResult.Rows[0]["cValue3"].ToString());
						this.ViewState["cValue"]=cValue;
						this.ViewState["cValue2"]=cValue2;
						this.ViewState["cValue3"]=cValue3;
						switch (cValue)
						{
							case 1:
								Radio1.Checked=true;
								TextBox1.Text="1";
								TextBox2.Text="100";
								TextBox3.Text="1";
								TextBox4.Text="100";
								TextBox1.ReadOnly=true;
								TextBox2.ReadOnly=true;
								TextBox3.ReadOnly=true;
								TextBox4.ReadOnly=true;
								break;

							case 2:
								Radio2.Checked=true;
								TextBox1.Text=dtResult.Rows[0]["cValue2"].ToString();
								TextBox2.Text=dtResult.Rows[0]["cValue3"].ToString();
								TextBox3.Text="1";
								TextBox4.Text="100";
								TextBox3.ReadOnly=true;
								TextBox4.ReadOnly=true;
								break;
					
							case 3:
								Radio3.Checked=true;
								TextBox1.Text="1";
								TextBox2.Text="100";
								TextBox1.ReadOnly=true;
								TextBox2.ReadOnly=true;
								TextBox3.Text=dtResult.Rows[0]["cValue2"].ToString();
								TextBox4.Text=dtResult.Rows[0]["cValue3"].ToString();
								break;
					
							case 4:
								Radio4.Checked=true;
								TextBox1.Text="1";
								TextBox2.Text="100";
								TextBox3.Text="1";
								TextBox4.Text="100";
								TextBox1.ReadOnly=true;
								TextBox2.ReadOnly=true;
								TextBox3.ReadOnly=true;
								TextBox4.ReadOnly=true;
								break;

							case 5:
					
								Radio5.Checked=true;
								TextBox1.Text="1";
								TextBox2.Text="100";
								TextBox3.Text="1";
								TextBox4.Text="100";
								TextBox1.ReadOnly=true;
								TextBox2.ReadOnly=true;
								TextBox3.ReadOnly=true;
								TextBox4.ReadOnly=true;
								break;
							default:
								cValue=1;
								cValue2=1;
								cValue3=100;
								Radio1.Checked=true;
								TextBox1.Text="1";
								TextBox2.Text="100";
								TextBox3.Text="1";
								TextBox4.Text="100";
								TextBox1.ReadOnly=true;
								TextBox2.ReadOnly=true;
								TextBox3.ReadOnly=true;
								TextBox4.ReadOnly=true;
								break;
						}
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
			TextBox3.ReadOnly=true;
			TextBox4.ReadOnly=true;
		}

		protected void Radio2_CheckedChanged(object sender, System.EventArgs e)
		{
			this.ViewState["cValue"]=2;
			TextBox1.ReadOnly=false;
			TextBox2.ReadOnly=false;
			TextBox3.ReadOnly=true;
			TextBox4.ReadOnly=true;
		}

		protected void Radio3_CheckedChanged(object sender, System.EventArgs e)
		{
			this.ViewState["cValue"]=3;
			TextBox1.ReadOnly=true;
			TextBox2.ReadOnly=true;
			TextBox3.ReadOnly=false;
			TextBox4.ReadOnly=false;
		}

		protected void Radio4_CheckedChanged(object sender, System.EventArgs e)
		{
			this.ViewState["cValue"]=4;
			TextBox1.ReadOnly=true;
			TextBox2.ReadOnly=true;
			TextBox3.ReadOnly=true;
			TextBox4.ReadOnly=true;
		}

		protected void Radio5_CheckedChanged(object sender, System.EventArgs e)
		{
			this.ViewState["cValue"]=5;
			TextBox1.ReadOnly=true;
			TextBox2.ReadOnly=true;
			TextBox3.ReadOnly=true;
			TextBox4.ReadOnly=true;
		}

		protected void TextBox1_TextChanged(object sender, System.EventArgs e)
		{
			this.ViewState["cValue2"]=int.Parse(TextBox1.Text.ToString());
		}

		protected void TextBox2_TextChanged(object sender, System.EventArgs e)
		{
			this.ViewState["cValue3"]=int.Parse(TextBox2.Text.ToString());
		}

		protected void TextBox3_TextChanged(object sender, System.EventArgs e)
		{
			this.ViewState["cValue2"]=int.Parse(TextBox3.Text.ToString());
		}

		protected void TextBox4_TextChanged(object sender, System.EventArgs e)
		{
			this.ViewState["cValue3"]=int.Parse(TextBox4.Text.ToString());
		}

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			cValue=int.Parse(this.ViewState["cValue"].ToString());
			
			SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
		
			if (cValue==2)
			{
				cValue2=int.Parse(TextBox1.Text.ToString());
				cValue3=int.Parse(TextBox2.Text.ToString());
			}
			else
			{
				if (cValue==3)
				{
					cValue2=int.Parse(TextBox3.Text.ToString());
					cValue3=int.Parse(TextBox4.Text.ToString());
				}
				else
				{
					cValue2=1;
					cValue3=100;
				}

			}
			string mystrSql2 = "Select cValue From Question_Retry Where cCaseID = '" + cCaseID + "' And cQID = '" + cQID + "' And cDivisionID = '" + cDivisionID + "'  And cSectionName = '" + SectionName + "' And  cUserLevel = '" + cUserlevel + "' And  CurrentTerm = '" + CurrentTerm + "'And cDescription='retry'";
			DataTable dtResult = new DataTable();
			dtResult = sqldb.getDataSet(mystrSql2).Tables[0];
			if( dtResult.Rows.Count > 0)
			{
				string mySQL = "update Question_Retry set cValue='"+cValue+"',cValue2='"+cValue2+"' Where cCaseID = '" + cCaseID + "' And cQID = '" + cQID + "'  And cDivisionID = '" + cDivisionID + "' And cSectionName = '" + SectionName + "' And  cUserLevel = '" + cUserlevel + "' And  CurrentTerm = '" + CurrentTerm +"'And cDescription='retry'";
				sqldb.ExecuteNonQuery(mySQL);
                Response.Redirect("QandA_select_setRetry.aspx?CaseID=" + cCaseID + "&CurrentTerm=" + CurrentTerm + "&SectionName=" + SectionName + "&cQID=" + cQID + "&cDivisionID=" + cDivisionID + "");
			}
			else
			{
				string mySQL = "insert into Question_Retry (cCaseID,CurrentTerm,cSectionName,cQID,cDivisionID,cUserLevel,cDescription,cValue,cValue2) values('"+cCaseID+"','"+CurrentTerm+"','"+SectionName+"','"+cQID+"','"+cDivisionID+"','"+cUserlevel+"','retry','"+cValue+"','"+cValue2+"')";
				sqldb.ExecuteNonQuery(mySQL);
                Response.Redirect("QandA_select_setRetry.aspx?CaseID=" + cCaseID + "&CurrentTerm=" + CurrentTerm + "&SectionName=" + SectionName + "&cQID=" + cQID + "&cDivisionID=" + cDivisionID + "");
			}
		}

        private void getParameter()
        {
            //CaseID
            if (Request.QueryString["CaseID"] != null)
            {
                cCaseID = Request.QueryString["CaseID"].ToString();
            }
            //CurrentTerm
            if (Request.QueryString["CurrentTerm"] != null)
            {
                CurrentTerm = Request.QueryString["CurrentTerm"].ToString();
            }
            //SectionName
            if (Request.QueryString["SectionName"] != null)
            {
                SectionName = Request.QueryString["SectionName"].ToString();
            }
            //cQID
            if (Request.QueryString["cQID"] != null)
            {
                cQID = Request.QueryString["cQID"].ToString();
            }
            //cDivisionID
            if (Request.QueryString["cDivisionID"] != null)
            {
                cDivisionID = Request.QueryString["cDivisionID"].ToString();
            }
        }
	}
}
