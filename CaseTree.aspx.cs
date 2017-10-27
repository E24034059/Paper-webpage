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
	/// 顯示CaseTree or CaseSection tree，並呼叫儲存的網頁把資料存入Paper_CaseDivisionSection 
	/// </summary>
    public partial class TreeView : AuthoringTool_BasicForm_BasicForm
	{
		string strUserLevel = "1";
		string strPresentType = "Case";

		//建立SqlDB物件
		SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);

		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.Initiate();

			//建立Case tree
			this.setupCaseTree();
		}

		private void setupCaseSectionTree()
		{
			//建立tree的必要javascript
			string NodeSc="<SCRIPT LANGUAGE='JavaScript' SRC='../Translate tree/JSCookTree.js'></SCRIPT><link REL='stylesheet' HREF='../Translate tree/theme.css' TYPE='text/css'><SCRIPT LANGUAGE='JavaScript' SRC='../Translate tree/theme.js'></SCRIPT>";
			Page.RegisterStartupScript("NodeSc",NodeSc);

			//建立tree的string
			string NodeStr="[";

			//---------------------------------------取得CaseTree的每一個Node----------------------------------------
			string strSQL = "";

			//-----------------------------依照不同的ViewType來決定CaseTree中要顯示的內容----------------------------
			strSQL = "SELECT DISTINCT Department.cDepartmentName , Department.cDepartmentNameIndex, Division.cDepartmentID FROM Department INNER JOIN Division ON Department.cDepartmentID = Division.cDepartmentID INNER JOIN PresentServer ON Division.cDivisionID = PresentServer.cDivisionID ORDER BY cDepartmentNameIndex";
			
			//-------------------------------------------將CaseTreeView中的Node加到TreeView中------------------------------------

			//-----------------------------------------------建立一個Hospital Node-----------------------------------------------
			NodeStr = NodeStr + "[null,'"+"Hospital"+"',null,'_self',null";
			
			//--------------------------------------建立Hospital的子Node，DepartmentNode-------------------------------------
			DataSet dsDepartment=sqldb.getDataSet(strSQL);
			int DepartmentCounter = 0;
			string strDepartmentCounter = "";
			foreach (DataRow drDepartmentName in dsDepartment.Tables[0].Rows)
			{
				DepartmentCounter = DepartmentCounter + 1;
				strDepartmentCounter = DepartmentCounter.ToString();
				NodeStr = NodeStr +",[null,'" + drDepartmentName["cDepartmentName"].ToString().Trim() + "',null,null,null";

				//建立Department的子Node，DivisionNode				
				string strDivisionSQL = "";
				strDivisionSQL = "SELECT Distinct Division.cDivisionName AS DivisionName , Division.cDivisionID AS DivisionID FROM Division INNER JOIN PresentServer ON Division.cDivisionID = PresentServer.cDivisionID INNER JOIN UserDivision ON Division.cDivisionID = UserDivision.cDivisionID INNER JOIN HintsUser ON UserDivision.cUserID = HintsUser.cUserID WHERE PresentServer.sUseLevel IN (0,'1') AND Division.cDepartmentID ='" + drDepartmentName["cDepartmentID"].ToString() + "' ORDER BY DivisionName";
				DataSet dsDivision=sqldb.getDataSet(strDivisionSQL);
				foreach (DataRow drDivisionName in dsDivision.Tables[0].Rows)
				{
					NodeStr = NodeStr + ",[null,'" + drDivisionName[0].ToString().Trim() + "',null,'_self',null";
					//建立Division的子Node，CaseNode
					string strCaseSQL = "SELECT DISTINCT TeachingCase.cCaseID, TeachingCase.cCaseName, DivIDtoURL.cURL AS cServer, TeachingCase.cURL FROM PresentServer INNER JOIN TeachingCase ON PresentServer.cCaseID = TeachingCase.cCaseID INNER JOIN DivIDtoURL ON PresentServer.cDivisionID = DivIDtoURL.cDivisionID WHERE PresentServer.cDivisionID='" + drDivisionName["DivisionID"].ToString() + "' ORDER BY cCaseName";
					DataSet dsCase=sqldb.getDataSet(strCaseSQL);
					
					foreach (DataRow drCaseName in dsCase.Tables[0].Rows)
					{
						string strCaseID = drCaseName["cCaseID"].ToString();
						//strCaseName = drCaseName["cCaseName"].ToString();
						string strDivisionID = drDivisionName["DivisionID"].ToString();
						
						NodeStr = NodeStr + ",[null,'" + (drCaseName["cCaseName"].ToString()).Trim() + "',null,'_self',null";

						//建立Visit
						strSQL = "SELECT MAX(sClinicNum) AS 'MaxClinic' FROM UserLevelPresent WHERE cUserLevel = '"+strUserLevel+"' AND cCaseID = '"+strCaseID+"' AND cDivisionID = '"+strDivisionID+"' ";
						DataSet dsVisit = sqldb.getDataSet(strSQL);
						if(dsVisit.Tables[0].Rows.Count > 0)
						{
							for(int i=0 ; i<dsVisit.Tables[0].Rows.Count ; i++)
							{
								//get visit number
								int intVisit = 1;
								try
								{
									intVisit = Convert.ToInt32(dsVisit.Tables[0].Rows[i]["sClinicNum"]);
								}
								catch
								{
								}

								NodeStr = NodeStr + ",[null,'" + intVisit.ToString() + "',null,'_self',null";

								//建立Section
								strSQL = "SELECT cSectionName FROM UserLevelPresent WHERE cUserLevel = '"+strUserLevel+"' AND cCaseID = '"+strCaseID+"' AND cDivisionID = '"+strDivisionID+"' AND sClinicNum = '"+intVisit.ToString()+"' ORDER BY sSectionSeq ";
								DataSet dsSection = sqldb.getDataSet(strSQL);
								if(dsSection.Tables[0].Rows.Count > 0)
								{
									for(int j=0 ; j<dsSection.Tables[0].Rows.Count ; j++)
									{
										//get section name
										string strSectionName = "";
										try
										{
											strSectionName = dsSection.Tables[0].Rows[j]["cSectionName"].ToString();
										}
										catch
										{
										}

										NodeStr = NodeStr + ",[null,'" + strSectionName + "',null,'_self',null";

										NodeStr = NodeStr + "]";
									}
								}
								dsSection.Dispose();

								NodeStr = NodeStr + "]";
							}
						}
						dsVisit.Dispose();

						NodeStr = NodeStr + "]";
					}
					dsCase.Dispose();
					//完成建立CaseNode
					NodeStr = NodeStr + "]";
				}
				dsDivision.Dispose();
				//完成建立DivisionNode
				NodeStr = NodeStr + "]";

			}
			dsDepartment.Dispose();

			//完成將TreeNode加到TreeView中
			string NodePut = "<Font size='26'><script language='JavaScript'>var myMenu=" + NodeStr + "]]</script></Font>";
			NodePut=NodePut + "<DIV ID=myMenuID  align='left' ></DIV>";
			NodePut = NodePut + "<SCRIPT LANGUAGE='JavaScript'>ctDraw ('myMenuID', myMenu, ctThemeXP2, '../Translate tree/ThemeXP', 1, 3);</SCRIPT>";
			Page.RegisterStartupScript("NodePut",NodePut);
		}

		private void setupCaseTree()
		{
			//建立tree的必要javascript
			string NodeSc="<SCRIPT LANGUAGE='JavaScript' SRC='../Translate tree/JSCookTree.js'></SCRIPT><link REL='stylesheet' HREF='../Translate tree/theme.css' TYPE='text/css'><SCRIPT LANGUAGE='JavaScript' SRC='../Translate tree/theme.js'></SCRIPT>";
			Page.RegisterStartupScript("NodeSc",NodeSc);

			//建立tree的string
			string NodeStr="[";

			//---------------------------------------取得CaseTree的每一個Node----------------------------------------
			string strSQL = "";

			//-----------------------------依照不同的ViewType來決定CaseTree中要顯示的內容----------------------------
			strSQL = "SELECT DISTINCT Department.cDepartmentName , Department.cDepartmentNameIndex, Division.cDepartmentID FROM Department INNER JOIN Division ON Department.cDepartmentID = Division.cDepartmentID INNER JOIN PresentServer ON Division.cDivisionID = PresentServer.cDivisionID ORDER BY cDepartmentNameIndex";
			
			//-------------------------------------------將CaseTreeView中的Node加到TreeView中------------------------------------

			//-----------------------------------------------建立一個Hospital Node-----------------------------------------------
			NodeStr = NodeStr + "[null,'"+"Hospital"+"',null,'_self',null";
			
			//--------------------------------------建立Hospital的子Node，DepartmentNode-------------------------------------
			DataSet dsDepartment=sqldb.getDataSet(strSQL);
			int DepartmentCounter = 0;
			string strDepartmentCounter = "";
			foreach (DataRow drDepartmentName in dsDepartment.Tables[0].Rows)
			{
				DepartmentCounter = DepartmentCounter + 1;
				strDepartmentCounter = DepartmentCounter.ToString();
				NodeStr = NodeStr +",[null,'" + drDepartmentName["cDepartmentName"].ToString().Trim() + "',null,null,null";

				//建立Department的子Node，DivisionNode				
				string strDivisionSQL = "";
				strDivisionSQL = "SELECT Distinct Division.cDivisionName AS DivisionName , Division.cDivisionID AS DivisionID FROM Division INNER JOIN PresentServer ON Division.cDivisionID = PresentServer.cDivisionID INNER JOIN UserDivision ON Division.cDivisionID = UserDivision.cDivisionID INNER JOIN HintsUser ON UserDivision.cUserID = HintsUser.cUserID WHERE PresentServer.sUseLevel IN (0,'1') AND Division.cDepartmentID ='" + drDepartmentName["cDepartmentID"].ToString() + "' ORDER BY DivisionName";
				DataSet dsDivision=sqldb.getDataSet(strDivisionSQL);
				foreach (DataRow drDivisionName in dsDivision.Tables[0].Rows)
				{
					NodeStr = NodeStr + ",[null,'" + drDivisionName[0].ToString().Trim() + "',null,'_self',null";
					//建立Division的子Node，CaseNode
					string strCaseSQL = "SELECT DISTINCT TeachingCase.cCaseID, TeachingCase.cCaseName, DivIDtoURL.cURL AS cServer, TeachingCase.cURL FROM PresentServer INNER JOIN TeachingCase ON PresentServer.cCaseID = TeachingCase.cCaseID INNER JOIN DivIDtoURL ON PresentServer.cDivisionID = DivIDtoURL.cDivisionID WHERE PresentServer.cDivisionID='" + drDivisionName["DivisionID"].ToString() + "' ORDER BY cCaseName";
					DataSet dsCase=sqldb.getDataSet(strCaseSQL);
					
					foreach (DataRow drCaseName in dsCase.Tables[0].Rows)
					{
						//strCaseID = drCaseName["cCaseID"].ToString();
						//strCaseName = drCaseName["cCaseName"].ToString();
						//strDivisionID = drDivisionName["DivisionID"].ToString();
						
						NodeStr = NodeStr + ",[null,'" + drCaseName["cCaseName"].ToString().Trim() + "','SectionTreeView.aspx?CaseID=" + drCaseName["cCaseID"].ToString().Trim() + "&DivisionID=" + drDivisionName["DivisionID"].ToString().Trim() + "','frmMain',null";
						NodeStr = NodeStr + "]";
					}
					dsCase.Dispose();
					//完成建立CaseNode
					NodeStr = NodeStr + "]";
				}
				dsDivision.Dispose();
				//完成建立DivisionNode
				NodeStr = NodeStr + "]";

			}
			dsDepartment.Dispose();

			//完成將TreeNode加到TreeView中
			string NodePut = "<Font size='26'><script language='JavaScript'>var myMenu=" + NodeStr + "]]</script></Font>";
			NodePut=NodePut + "<DIV ID=myMenuID  align='left' ></DIV>";
			NodePut = NodePut + "<SCRIPT LANGUAGE='JavaScript'>ctDraw ('myMenuID', myMenu, ctThemeXP2, '../Translate tree/ThemeXP', 1, 3);</SCRIPT>";
			Page.RegisterStartupScript("NodePut",NodePut);
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
