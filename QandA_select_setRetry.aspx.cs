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
	/// QandA_setRetry 的摘要描述。
	/// </summary>
    public partial class QandA_setRetry : AuthoringTool_BasicForm_BasicForm
	{
		protected System.Web.UI.WebControls.Table Table1;
		protected string all_retry= "";
		protected string vez_retry= "";
		protected string ez_retry= "";
		protected string med_retry= "";
		protected string med_ad_retry= "";
		protected string adv_retry= "";
        protected string cDivisionID="";		
		protected string all_review= "";
		protected string vez_review= "";
		protected string ez_review= "";
		protected string med_review= "";
		protected string med_ad_review="";
		protected string adv_review= "";
		protected string CaseID = "";
		protected string CurrentTerm = "";
		protected string SectionName= "";
		protected string cQID = "";

		//protected clsHintsDB hintsDB = new clsHintsDB();
		protected void Page_Load(object sender, System.EventArgs e)
		{
			CaseID = "wytCase200503071538141718750";//Request.QueryString["CaseID"];
			CurrentTerm = "1";//Request.QueryString["CurrentTerm"];
			SectionName= "Examination_2";//Request.QueryString["SectionName"];
			cQID = "afeng_Question_200605041934595312500";//Request.QueryString["cQID"];
			cDivisionID ="6061";//Request.QueryString["cDivisionID"]

            this.Initiate();

            //接收參數
            this.getParameter();

			SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);

		 //這一個題目是不是已經有設定過			
			string mystrSql = "Select * From Question_Retry Where cCaseID = '" + CaseID + "' And cQID = '" + cQID + "' And cDivisionID = '" + cDivisionID + "' And cSectionName = '" + SectionName + "'   And  CurrentTerm = '" + CurrentTerm + "'" ;
            DataTable dtResult = new DataTable();
			dtResult = sqldb.getDataSet(mystrSql).Tables[0];
			if( dtResult.Rows.Count > 0)
			{
        //有沒有設定過ALL的retry
				string mystrSqlA = "Select * From Question_Retry Where cCaseID = '" + CaseID + "' And cQID = '" + cQID + "' And cDivisionID = '" + cDivisionID + "' And cSectionName = '" + SectionName + "'   And  CurrentTerm = '" + CurrentTerm + "' And  cUserLevel = 'all' And cDescription = 'retry'" ;
				DataTable dtResultA = new DataTable();
				dtResultA = sqldb.getDataSet(mystrSqlA).Tables[0];
				if( dtResultA.Rows.Count > 0)
					{
						all_retry= "<a href=\"QandA_set_retry.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=all"+"&settype=update_retry"+"\">修改設定</a>";
					}
				else
					{
						all_retry= "<a href=\"QandA_set_retry.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=all"+"&settype=insert_retry"+"\">新增設定</a>";
					}

		//判斷very easy是否有設定過retry
				string mystrSql1 = "Select * From Question_Retry Where cCaseID = '" + CaseID + "' And cQID = '" + cQID + "' And cDivisionID = '" + cDivisionID + "' And cSectionName = '" + SectionName + "'   And  CurrentTerm = '" + CurrentTerm + "' And  cUserLevel = 'veryeasy' And cDescription = 'retry'" ;
					DataTable dtResult1 = new DataTable();
					dtResult1 = sqldb.getDataSet(mystrSql1).Tables[0];
				if( dtResult1.Rows.Count > 0)
					{
						vez_retry= "<a href=\"QandA_set_retry.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=veryeasy"+"&settype=update_retry"+"\">修改設定</a>";
					}
				else
					{
						vez_retry= "<a href=\"QandA_set_retry.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=veryeasy"+"&settype=insert_retry"+"\">新增設定</a>";
					}

         //判斷easy是否有設定過retry
				string mystrSql2 = "Select * From Question_Retry Where cCaseID = '" + CaseID + "' And cQID = '" + cQID + "' And cDivisionID = '" + cDivisionID + "' And cSectionName = '" + SectionName + "'   And  CurrentTerm = '" + CurrentTerm + "' And  cUserLevel = 'easy' And cDescription = 'retry'" ;
					DataTable dtResult2 = new DataTable();
					dtResult2 = sqldb.getDataSet(mystrSql2).Tables[0];
				if( dtResult2.Rows.Count > 0)
					{
						ez_retry= "<a href=\"QandA_set_retry.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=easy"+"&settype=update_retry"+"\">修改設定</a>";
					}
				else
					{
						ez_retry= "<a href=\"QandA_set_retry.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=easy"+"&settype=insert_retry"+"\">新增設定</a>";
					}

         //判斷medium是否有設定過retry
				string mystrSql3 = "Select * From Question_Retry Where cCaseID = '" + CaseID + "' And cQID = '" + cQID + "' And cDivisionID = '" + cDivisionID + "' And cSectionName = '" + SectionName + "'   And  CurrentTerm = '" + CurrentTerm + "' And  cUserLevel = 'medium' And cDescription = 'retry'" ;
					DataTable dtResult3 = new DataTable();
					dtResult3 = sqldb.getDataSet(mystrSql3).Tables[0];
				if( dtResult3.Rows.Count > 0)
					{
						med_retry= "<a href=\"QandA_set_retry.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=medium"+"&settype=update_retry"+"\">修改設定</a>";
					}
				else
					{
						med_retry= "<a href=\"QandA_set_retry.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=medium"+"&settype=insert_retry"+"\">新增設定</a>";
					}
            //判斷medium_advanced是否有設定過retry
				string mystrSql4 = "Select * From Question_Retry Where cCaseID = '" + CaseID + "' And cQID = '" + cQID + "' And cDivisionID = '" + cDivisionID + "'  And cSectionName = '" + SectionName + "'   And  CurrentTerm = '" + CurrentTerm + "' And  cUserLevel = 'medium_advanced' And cDescription = 'retry'" ;
					DataTable dtResult4 = new DataTable();
					dtResult4 = sqldb.getDataSet(mystrSql4).Tables[0];
				if( dtResult4.Rows.Count > 0)
					{
						med_ad_retry= "<a href=\"QandA_set_retry.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=medium_advanced"+"&settype=update_retry"+"\">修改設定</a>";
					}
				else
					{
						med_ad_retry= "<a href=\"QandA_set_retry.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=medium_advanced"+"&settype=insert_retry"+"\">新增設定</a>";
					}
			//判斷advanced是否有設定過retry
				string mystrSql5 = "Select * From Question_Retry Where cCaseID = '" + CaseID + "' And cQID = '" + cQID + "' And cDivisionID = '" + cDivisionID + "' And cSectionName = '" + SectionName + "'   And  CurrentTerm = '" + CurrentTerm + "' And  cUserLevel = 'advanced' And  cDescription = 'retry'";
					DataTable dtResult5 = new DataTable();
					dtResult5 = sqldb.getDataSet(mystrSql5).Tables[0];
				if( dtResult5.Rows.Count > 0)
					{
						adv_retry= "<a href=\"QandA_set_retry.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=advanced"+"&settype=update_retry"+"\">修改設定</a>";
					}
				else
					{
						adv_retry= "<a href=\"QandA_set_retry.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=advanced"+"&settype=insert_retry"+"\">新增設定</a>";
					}
				

				//有沒有設定過ALL的review
				string mystrSqlV = "Select * From Question_Retry Where cCaseID = '" + CaseID + "' And cQID = '" + cQID + "' And cDivisionID = '" + cDivisionID + "' And cSectionName = '" + SectionName + "'   And  CurrentTerm = '" + CurrentTerm + "' And  cUserLevel = 'all' And cDescription = 'review'" ;
					DataTable dtResultV = new DataTable();
					dtResultV = sqldb.getDataSet(mystrSqlV).Tables[0];
				if( dtResultV.Rows.Count > 0)
				{
					all_review= "<a href=\"QandA_set_review.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=all"+"&settype=update_review"+"\">修改設定</a>";
				}
				else
				{
					all_review= "<a href=\"QandA_set_review.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=all"+"&settype=insert_review"+"\">新增設定</a>";
				}
     //判斷veryeasy是否有設定過review
				string mystrSql6 = "Select * From Question_Retry Where cCaseID = '" + CaseID + "' And cQID = '" + cQID + "' And cDivisionID = '" + cDivisionID + "' And cSectionName = '" + SectionName + "'   And  CurrentTerm = '" + CurrentTerm + "' And  cUserLevel = 'veryeasy' And  cDescription = 'review'" ;
					DataTable dtResult6 = new DataTable();
					dtResult6 = sqldb.getDataSet(mystrSql6).Tables[0];
				if( dtResult6.Rows.Count > 0)
					{
						vez_review= "<a href=\"QandA_set_review.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=veryeasy"+"&settype=update_review"+"\">修改設定</a>";
					}
				else
					{
						vez_review= "<a href=\"QandA_set_review.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=veryeasy"+"&settype=insert_review"+"\">新增設定</a>";
					}
		//判斷easy是否有設定過review
				string mystrSql7 = "Select * From Question_Retry Where cCaseID = '" + CaseID + "' And cQID = '" + cQID + "' And cDivisionID = '" + cDivisionID + "' And cSectionName = '" + SectionName + "'   And  CurrentTerm = '" + CurrentTerm + "' And  cUserLevel = 'easy' And  cDescription = 'review'" ;
					DataTable dtResult7 = new DataTable();
					dtResult7 = sqldb.getDataSet(mystrSql7).Tables[0];
				if( dtResult7.Rows.Count > 0)
					{
						ez_review= "<a href=\"QandA_set_review.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=easy"+"&settype=update_review"+"\">修改設定</a>";
					}
				else
					{
						ez_review= "<a href=\"QandA_set_review.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=easy"+"&settype=insert_review"+"\">新增設定</a>";
					}

		//判斷medium是否有設定過review
				string mystrSql8 = "Select * From Question_Retry Where cCaseID = '" + CaseID + "' And cQID = '" + cQID + "' And cDivisionID = '" + cDivisionID + "' And cSectionName = '" + SectionName + "'   And  CurrentTerm = '" + CurrentTerm + "' And  cUserLevel = 'medium' And  cDescription = 'review'" ;
					DataTable dtResult8 = new DataTable();
					dtResult8 = sqldb.getDataSet(mystrSql8).Tables[0];
				if( dtResult8.Rows.Count > 0)
					{
						med_review= "<a href=\"QandA_set_review.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=medium"+"&settype=update_review"+"\">修改設定</a>";
					}
				else
					{
						med_review= "<a href=\"QandA_set_review.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=medium"+"&settype=insert_review"+"\">新增設定</a>";
					}
					//判斷medium_advanced是否有設定過review
				string mystrSql9 = "Select * From Question_Retry Where cCaseID = '" + CaseID + "' And cQID = '" + cQID + "' And cDivisionID = '" + cDivisionID + "' And cSectionName = '" + SectionName + "'   And  CurrentTerm = '" + CurrentTerm + "' And  cUserLevel = 'medium_advanced' And  cDescription = 'review'" ;
					DataTable dtResult9 = new DataTable();
					dtResult9 = sqldb.getDataSet(mystrSql9).Tables[0];
				if( dtResult9.Rows.Count > 0)
					{
						med_ad_review= "<a href=\"QandA_set_review.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=medium_advanced"+"&settype=update_review"+"\">修改設定</a>";
					}
				else
					{
						med_ad_review= "<a href=\"QandA_set_review.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=medium_advanced"+"&settype=insert_review"+"\">新增設定</a>";
					}
					//判斷advanced是否有設定過review
				string mystrSql10 = "Select * From Question_Retry Where cCaseID = '" + CaseID + "' And cQID = '" + cQID + "' And cDivisionID = '" + cDivisionID + "'  And cSectionName = '" + SectionName + "'   And  CurrentTerm = '" + CurrentTerm + "' And  cUserLevel = 'advanced' And  cDescription = 'review'" ;
					DataTable dtResult10 = new DataTable();
					dtResult10 = sqldb.getDataSet(mystrSql10).Tables[0];
					if( dtResult10.Rows.Count > 0)
					{
						adv_review= "<a href=\"QandA_set_review.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=advanced"+"&settype=update_review"+"\">修改設定</a>";
					}
					else
					{
						adv_review= "<a href=\"QandA_set_review.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=advanced"+"&settype=insert_review"+"\">新增設定</a>";
					}
				}



			else
			{
			    all_retry= "<a href=\"QandA_set_retry.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=all"+"&settype=insert_retry"+"\">新增設定</a>";
				vez_retry= "<a href=\"QandA_set_retry.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=veryeasy"+"&settype=insert_retry"+"\">新增設定</a>";
				ez_retry= "<a href=\"QandA_set_retry.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=easy"+"&settype=insert_retry"+"\">新增設定</a>";
				med_retry= "<a href=\"QandA_set_retry.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=medium"+"&settype=insert_retry"+"\">新增設定</a>";
				med_ad_retry= "<a href=\"QandA_set_retry.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=medium_advanced"+"&settype=insert_retry"+"\">新增設定</a>";
				adv_retry= "<a href=\"QandA_set_retry.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=advanced"+"&settype=insert_retry"+"\">新增設定</a>";
			
				all_review= "<a href=\"QandA_set_review.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=all"+"&settype=insert_review"+"\">新增設定</a>";
				vez_review= "<a href=\"QandA_set_review.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=veryeasy"+"&settype=insert_review"+"\">新增設定</a>";
				ez_review= "<a href=\"QandA_set_review.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=easy"+"&settype=insert_review"+"\">新增設定</a>";
				med_review= "<a href=\"QandA_set_review.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=medium"+"&settype=insert_review"+"\">新增設定</a>";
				med_ad_review= "<a href=\"QandA_set_review.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=medium_advanced"+"&settype=insert_review"+"\">新增設定</a>";
				adv_review= "<a href=\"QandA_set_review.aspx?CaseID="+CaseID+"&cDivisionID="+cDivisionID+"&CurrentTerm="+CurrentTerm+"&SectionName="+SectionName+"&cQid="+cQID+"&userlevel=advanced"+"&settype=insert_review"+"\">新增設定</a>";

			}

			
			// 在這裡放置使用者程式碼以初始化網頁
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

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			//this.FindControl("Form1").Controls.Add( this.ParseControl("<script language='javascript'> window.close(); </script>") );
            RegisterStartupScript("", "<script language='javascript'> window.close(); </script>");
		}

        private void getParameter()
        {
            //CaseID
            if (Request.QueryString["CaseID"] != null)
            {
                CaseID = Request.QueryString["CaseID"].ToString();
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
            //SectionName
            if (Request.QueryString["cQID"] != null)
            {
                cQID = Request.QueryString["cQID"].ToString();
            }
            //SectionName
            if (Request.QueryString["cDivisionID"] != null)
            {
                cDivisionID = Request.QueryString["cDivisionID"].ToString();
            }
        }

	}
}
