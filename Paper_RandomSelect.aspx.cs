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
	/// Paper_RandomSelect 的摘要描述。
	/// </summary>
    public partial class Paper_RandomSelect : AuthoringTool_BasicForm_BasicForm
	{
		//建立SqlDB物件
		SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
		DataReceiver myReceiver = new DataReceiver();
		SQLString mySQL = new SQLString();
		RandomSelect myRandom = new RandomSelect();

		int intQuestionNum = 0;

		string strGenerationMethod = "Edit";

		string strUserID , strCaseID , strDivisionID , strClinicNum , strSectionName , strEditMode , strFunction , strPaperID;
		string strGroupID = "Group_200509042152345474592";
		string strGroupDivisionID = "0101";

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.Initiate();

			//接收參數
			this.getParameter();

			//找出此Case有哪一個問卷，取出其PaperID。
			strPaperID = mySQL.getPaperIDFromCase(strCaseID , strClinicNum , strSectionName);

			if(Session["PaperID"] != null)
			{
				strPaperID = Session["PaperID"].ToString();
			}
			
			//取得此問卷是使用者自行編輯或是系統在呈現題目時才亂數選題
			string strSQL = mySQL.getPaperHeader(strPaperID);
			DataSet dsHeader = sqldb.getDataSet(strSQL);
			try
			{
				strGenerationMethod = dsHeader.Tables[0].Rows[0]["cGenerationMethod"].ToString();
			}
			catch
			{
			}
			dsHeader.Dispose();

			//取得該組別的總問題數(依照PresentMethod有不同)
			if(strGenerationMethod == "Edit")
			{
				//取得此General組別尚未被選取的問題數量
				intQuestionNum = myReceiver.getGroupSelectionQuestionCountLevel1NotSelect(strGroupID , strPaperID);
			}
			else
			{
				intQuestionNum = myReceiver.getRandomSelectionQuestionCountLevel1NotSelect(strGroupID , strPaperID);
			}

			if(intQuestionNum <= 0)
			{
				//txtNumber.Disabled = true;
			}
			else
			{
				//txtNumber.Disabled = false;
			}

			//設定Title
			spanMax.InnerText = intQuestionNum.ToString();

            //設定問題難易度的顯示數量
            DataTable dtQuestionLevel = new DataTable();
            dtQuestionLevel = AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevelName();
            foreach (DataRow drQuestionLevel in dtQuestionLevel.Rows)
            {               
                DataTable dtQuestionLevelNum = new DataTable();
                dtQuestionLevelNum = PaperSystem.DataReceiver.GetQuestionLevelNum(strGroupID);
                string strQuestionLevelName = drQuestionLevel["cLevelName"].ToString();
                int iQuestionLevel = AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevelName_SELECT_QuestionLevel(strQuestionLevelName);
                Label lbQuestionLevelName = new Label();
                lbQuestionLevelName.Text = "&nbsp;" + strQuestionLevelName;
                phQuestionLevel.Controls.Add(lbQuestionLevelName);
                int iQuestionLevelNum = 0;
                foreach (DataRow drQuestionLevelNum in dtQuestionLevelNum.Rows)
                {
                    if (Convert.ToInt16(drQuestionLevelNum["cQuestionLevel"].ToString()) == iQuestionLevel)
                        iQuestionLevelNum = Convert.ToInt16(drQuestionLevelNum["QuestionLevelNum"].ToString());
                }
                DropDownList ddlQuestionLevelNum = new DropDownList();
                for (int i = 0; i <= iQuestionLevelNum; i++)
                {
                    ddlQuestionLevelNum.Items.Add(i.ToString());     
                }
                ddlQuestionLevelNum.ID = "ddlQuestionLevelNum_" + iQuestionLevel;
                phQuestionLevel.Controls.Add(ddlQuestionLevelNum);
            }


			//設定Finish button的事件
			btnSubmit.ServerClick+=new ImageClickEventHandler(btnSubmit_ServerClick);
		}

		private void getParameter()
		{
			//GroupID
			if(Request.QueryString["GroupID"] != null)
			{
				strGroupID = Request.QueryString["GroupID"].ToString();
				if(Session["GroupID"] != null)
				{
					Session["GroupID"] = strGroupID;
				}
				else
				{
					Session.Add("GroupID",strGroupID);
				}
			}

			//GroupDivisionID
			if(strGroupID.Trim().Length > 0)
			{
				string strGroupDivisionID = myReceiver.getGroupDivisionID(strGroupID);
				if(Session["GroupDivisionID"] != null)
				{
					Session["GroupDivisionID"] = strGroupDivisionID;
				}
				else
				{
					Session.Add("GroupDivisionID",strGroupDivisionID);
				}
			}
			
			//UserID
			if(Session["UserID"] != null)
			{
				strUserID = Session["UserID"].ToString();
			}
			
			//CaseID kyhCase200505301448128593750
			if(Session["CaseID"] != null)
			{
				strCaseID = Session["CaseID"].ToString();
			}
			
			//Division 9801
			if(Session["DivisionID"] != null)
			{
				strDivisionID = Session["DivisionID"].ToString();
			}
			
			//ClinicNum
			if(Session["ClinicNum"] != null)
			{
				strClinicNum = Session["ClinicNum"].ToString();
			}
			
			//SectionName
			if(Session["SectionName"] != null)
			{
				strSectionName = Session["SectionName"].ToString();
			}

			//Opener
			if(Session["Opener"] != null)
			{
				hiddenOpener.Value = Session["Opener"].ToString();
			}

			//Setup opener
			if(Session["Opener"] != null)
			{
				Session["Opener"] = "Paper_OtherQuestion";
			}
			else
			{
				Session.Add("Opener","Paper_OtherQuestion");
			}

			//QuestionMode
			if(Session["QuestionMode"] != null)
			{
				hiddenQuestionMode.Value = Session["QuestionMode"].ToString();
			}

			//PresentType
			if(Session["PresentType"] != null)
			{
				hiddenPresentType.Value = Session["PresentType"].ToString();
			}
		
			//Edit method
			if(Session["EditMode"] != null)
			{
				hiddenEditMode.Value = Session["EditMode"].ToString();
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

		private string[] getQIDArray(int intUserNum)
		{
			if(hiddenQuestionMode.Value == "Specific")
			{
				return myRandom.getSpecificRandomQIDArrayNotSelectLevel1(intUserNum , strPaperID);
			}
			else
			{
				return myRandom.getGroupRandomQIDArrayNotSelectLevel1(strGroupID , intUserNum , strPaperID);
			}
		}

		private void btnSubmit_ServerClick(object sender, ImageClickEventArgs e)
		{
            ////取得txtNum的值
            //int intUserNum = 0;
            //try
            //{
            //    intUserNum = Convert.ToInt32(((HtmlInputText)(this.FindControl("Form1").FindControl("txtNumber"))).Value);
            //}
            //catch
            //{
            //    intUserNum = 0;
            //}

			//檢查是否大於問題數量
			//if(intUserNum <= intQuestionNum)
			//{
				/*
				if(strGenerationMethod == "Edit")
				{
					//此問卷是使用者自行編輯
					
//					if(strEditMode == "System" && strFunction == "4")
//					{
//						//將資料儲存到Paper_GroupingQuestion
//					
//						mySQL.SaveToPaperGroupingQuestion(strPaperID , strGroupID , strGroupDivisionID , intUserNum.ToString());
//					}
//					else
//					{
					

					//取得此組別的亂數選取的QID陣列
					string[] arrayQID = this.getQIDArray(intUserNum);//myRandom.getGroupRandomQIDArrayNotSelectLevel1(strGroupID , intUserNum , strPaperID);
					
					
					string strSQL = "";
					if(hiddenQuestionMode.Value == "Specific")
					{
						strSQL = mySQL.getSpecificQuestionLevel1NotSelect(strPaperID);
					}
					else
					{
						strSQL = mySQL.getGroupQuestionLevel1NotSelect(strGroupID,strPaperID);
					}

					DataSet dsQuestionList = sqldb.getDataSet(strSQL);

					//將此陣列的值存入Paper_Content中
					if(arrayQID.Length > 0)
					{
						for(int i=0 ; i< arrayQID.Length ; i++)
						{
							string strQID = arrayQID[i];
							DataRow[] tr = dsQuestionList.Tables[0].Select("cQID = '"+strQID+"'");

							//Standard score
							string strScore = "0";

							//QuestionType
							string strQuestionType = "";
							try
							{
								strQuestionType = tr[0]["cQuestionType"].ToString();
							}
							catch
							{
							}

							//QuestionMode
							string strQuestionMode = "";
							try
							{
								strQuestionMode = tr[0]["cQuestionMode"].ToString();
							}
							catch
							{
							}

							//Seq
							string strSeq = Convert.ToString(myReceiver.getPaperContentMaxSeq(strPaperID) + 1);

							//Question
							string strQuestion = "";
							try
							{
								strQuestion = tr[0]["cQuestion"].ToString();
							}
							catch
							{
							}

							//將此題目的資料存入資料庫
							mySQL.SaveToQuestionContent(strPaperID , strQID , strScore , strQuestionType , strQuestionMode , strQuestion , strSeq);
						}
					}
					else
					{
						//陣列沒有資料的情形
					}
					dsQuestionList.Dispose();

					//}
				}
				else
				{
					//此問卷是系統在呈現時隨機選題
					
					//把使用者選擇的問題個數和問題組別資料存入 Paper_RandomQuestionNum
					
					if(hiddenQuestionMode.Value == "Specific")
					{
						mySQL.saveRandomSpecificQuestionNum(strPaperID , intUserNum);
					}
					else
					{
					
						mySQL.saveRandomQuestionNum(strPaperID , strGroupID , intUserNum);
					}
					//Response.Write(intUserNum);
				}
				*/
				//mySQL.saveRandomQuestionNum(strPaperID , strGroupID , intUserNum);
			//}

            //將使用者選擇問題難易度的數量存入資料表
            for (int i = 0; i < Request.Form.Count; i++)
            {
                if (Request.Form.Keys[i].ToString().IndexOf("ddlQuestionLevelNum_") != -1)
                {
                    string[] strQuestionLevelNum = Request.Form.Keys[i].ToString().Split('_');
                    int iQuestionLevel = Convert.ToInt16(strQuestionLevelNum[1]);
                    int iQuestionLevelNum = Convert.ToInt16(Request.Form[i].ToString());
                    mySQL.saveRandomQuestionNum(strPaperID, strGroupID, iQuestionLevelNum, iQuestionLevel);
                }           
            }

            Response.Redirect("Paper_OtherQuestion.aspx");
		}
	}
}
