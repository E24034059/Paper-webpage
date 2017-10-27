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
	/// Paper_StatisticMainFunctions 的摘要描述。
	/// </summary>
    public partial class Paper_StatisticMainFunctions : AuthoringTool_BasicForm_BasicForm
	{
		//建立SqlDB物件
		SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
		SQLString mySQL = new SQLString();

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.Initiate();

			if(this.IsPostBack == false)
			{
				hiddenFunction.Value = "0";
			}

			//建立Statistic functions table
			setupFunctionTable();

			//建立Submit button
            HtmlInputButton btnSubmit = new HtmlInputButton();
			tdSubmit.Controls.Add(btnSubmit);
            btnSubmit.Value = this.GetMultiLanguageString("SubmitPaper");
			btnSubmit.Style.Add("CURSOR","Hand");
            btnSubmit.ServerClick += new EventHandler(btnSubmit_ServerClick);
		}

		private void setupFunctionTable()
		{
			Table table = new Table();
			tdFunctionTable.Controls.Add(table);
			table.Attributes.Add("Class","header1_table");
			table.CellPadding = 5;

			//Title
			TableRow trTitle = new TableRow();
			table.Rows.Add(trTitle);
			trTitle.Attributes.Add("Class","header1_table_first_row");

			//Radio button
			TableCell tcRadioTitle = new TableCell();
			trTitle.Cells.Add(tcRadioTitle);

			//Construction
			TableCell tcConstructionTitle = new TableCell();
			trTitle.Cells.Add(tcConstructionTitle);
			tcConstructionTitle.Text = "Construction";
			tcConstructionTitle.HorizontalAlign = HorizontalAlign.Center;

			string strSQL = mySQL.getStatisticFunctionList();
			DataSet dsFunctions = sqldb.getDataSet(strSQL);
			if(dsFunctions.Tables[0].Rows.Count > 0)
			{
				for(int i=0 ; i<dsFunctions.Tables[0].Rows.Count ; i++)
				{
					TableRow tr = new TableRow();
					table.Rows.Add(tr);
					if(i%2 == 0)
					{
						tr.Attributes.Add("Class","header1_tr_even_row");
					}
					else
					{
						tr.Attributes.Add("Class","header1_tr_odd_row");
					}

					//Radio button
					TableCell tcRadio = new TableCell();
					tr.Cells.Add(tcRadio);

					RadioButton rb = new RadioButton();
					tcRadio.Controls.Add(rb);
					rb.GroupName = "Function";
					if(i == 0)
					{
						rb.Checked = true;
					}
					string strID = "";
					try
					{
						strID = dsFunctions.Tables[0].Rows[i]["sFunctionID"].ToString();
					}
					catch
					{
					}
					rb.ID = "rb-" + strID;

					//Construction
					TableCell tcConstruction = new TableCell();
					tr.Cells.Add(tcConstruction);
					tcConstruction.Text = "";
					try
					{
						tcConstruction.Text = dsFunctions.Tables[0].Rows[i]["cConstruction"].ToString();
					}
					catch
					{
					}
				}
			}
			dsFunctions.Dispose();
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
            //把所有RadioButton掃描一遍
            string strSQL = mySQL.getStatisticFunctionList();
            DataSet dsFunctions = sqldb.getDataSet(strSQL);

            if (dsFunctions.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsFunctions.Tables[0].Rows.Count; i++)
                {
                    //取得ID
                    string strID = "";
                    try
                    {
                        strID = "rb-" + dsFunctions.Tables[0].Rows[i]["sFunctionID"].ToString();
                    }
                    catch
                    {
                    }

                    bool bChecked = false;
                    try
                    {
                        bChecked = ((RadioButton)(this.FindControl("Form1").FindControl(strID))).Checked;
                    }
                    catch
                    {
                    }

                    if (bChecked == true)
                    {
                        hiddenFunction.Value = dsFunctions.Tables[0].Rows[i]["sFunctionID"].ToString();
                    }
                    //string str = Request.Form["strID"];
                }

                //呼叫Client端的OnSubmit
                string strScript = "<script language='javascript'>\n";
                strScript += "Submit();\n";
                strScript += "</script>\n";
                Page.RegisterStartupScript("Submit", strScript);
            }
        }
	}
}
