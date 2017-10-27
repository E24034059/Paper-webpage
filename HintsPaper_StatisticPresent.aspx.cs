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
	/// HintsPaper_StatisticPresent 的摘要描述。
	/// </summary>
    public partial class HintsPaper_StatisticPresent : AuthoringTool_BasicForm_BasicForm
	{	
		//建立SqlDB物件
		SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
		DataReceiver myReceiver = new DataReceiver();
		SQLString mySQL = new SQLString();

		string strPaperID , strFunction , strCaseID , strAuthorID , strClass , strGroup;
		int intQuestionNum = 0;

		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.Initiate();

			//接收參數
			this.getParameter();

			//建立選擇題的table
			Table table = new Table();
			tdSelectionTable.Controls.Add(table);
			table.Attributes.Add("Class","header1_table");
			table.Style.Add("WIDTH","100%");
            table.GridLines = GridLines.Vertical;

			//建立選擇題的另一種呈現方式的表格
			Table tableOutput = new Table();
			tcOutputTable.Controls.Add(tableOutput);
			tableOutput.Attributes.Add("Class","header1_table");
			tableOutput.Style.Add("WIDTH","100%");

			//建立Output table 的Title
			TableRow trTitle = new TableRow();
			tableOutput.Rows.Add(trTitle);
			trTitle.CssClass = "header1_table_first_row";

			//Question
			TableCell tcQuestionTitle = new TableCell();
			trTitle.Cells.Add(tcQuestionTitle);
			tcQuestionTitle.Text = "Questions";

			//非常同意
			TableCell tcTitle1 = new TableCell();
			trTitle.Cells.Add(tcTitle1);
			tcTitle1.Text = "非常同意";

			//同意
			TableCell tcTitle2 = new TableCell();
			trTitle.Cells.Add(tcTitle2);
			tcTitle2.Text = "同意";

			//普通
			TableCell tcTitle3 = new TableCell();
			trTitle.Cells.Add(tcTitle3);
			tcTitle3.Text = "普通";

			//不同意
			TableCell tcTitle4 = new TableCell();
			trTitle.Cells.Add(tcTitle4);
			tcTitle4.Text = "不同意";

			//非常不同意
			TableCell tcTitle5 = new TableCell();
			trTitle.Cells.Add(tcTitle5);
			tcTitle5.Text = "非常不同意";

			//在表格中建立選擇題
			setupSelectionContent(table , tableOutput);
            tableOutput.GridLines = GridLines.Vertical;

			//建立問答題的Table
			Table tableText = new Table();
			tdTextTable.Controls.Add(tableText);
			tableText.Attributes.Add("Class","header1_table");
			tableText.Style.Add("WIDTH","100%");
            tableText.GridLines = GridLines.Vertical;

			//在表格中建立問答題
			setupTextContent(tableText);
			
			//建立Duration的Table
			Table tableDuration = new Table();
			tdDurationTable.Controls.Add(tableDuration);
			tableDuration.Attributes.Add("Class","header1_table");
			tableDuration.Style.Add("WIDTH","100%");
            tableDuration.GridLines = GridLines.Vertical;

			//在表格中建立Duration
			setupDurationContent(tableDuration);
		}

		private void setupDurationContent(Table table)
		{
			//依照不同Function呼叫不同的Function
			string strSQL = "";
			switch (strFunction)
			{
				case "0":
					//Author,Case , Class
					strSQL = mySQL.getCaseClassDuration(strPaperID , strCaseID , strClass);
					break;
				case "1":
					//Author , Case , Group
					strSQL = mySQL.getCaseGroupDuration(strPaperID , strCaseID , strGroup);
					break;
				case "2":
					//Author , Case
					strSQL = mySQL.getCaseDuration(strPaperID , strCaseID);
					break;
				case "3":
					//Author , Class
					//strSQL = mySQL.getSelectionSummaryByAuthorClass(strPaperID , strQID , strSelectionID , strAuthorID , strClass);
					break;
				case "4":
					//Author , Group
					//strSQL = mySQL.getSelectionSummaryByAuthorGroup(strPaperID , strQID , strSelectionID , strAuthorID , strGroup);
					break;
				case "5":
					//Author
					//strSQL = mySQL.getSelectionSummaryByAuthor(strPaperID , strQID , strSelectionID , strAuthorID);
					break;
				case "6":
					//Case , Class
					strSQL = mySQL.getCaseClassDuration(strPaperID , strCaseID , strClass);
					break;
				case "7":
					//Case , Group
					strSQL = mySQL.getCaseGroupDuration(strPaperID , strCaseID , strGroup);
					break;
				case "8":
					//Case
					strSQL = mySQL.getCaseDuration(strPaperID , strCaseID);
					break;
				case "9":
					//Class
					//strSQL = mySQL.getSelectionSummaryByClass(strPaperID , strQID , strSelectionID , strClass);
					break;
				case "10":
					//Group
					//strSQL = mySQL.getSelectionSummaryByGroup(strPaperID , strQID , strSelectionID , strGroup);
					break;
			}

			if(strSQL.Trim().Length == 0)
			{
				tdDurationTable.Controls.Clear();
			}
			else
			{
				if(strFunction == "0" || strFunction == "1" || strFunction == "2" || strFunction == "6" || strFunction == "7" || strFunction == "8")
				{
					//只有單一教案的情形
					setupSingleCaseDuration(strSQL , table);
				}
				else
				{
					//多個教案的情形
				}
			}
		}

		private void setupSingleCaseDuration(string strSQL , Table table)
		{
			//Title
			TableRow trTitle = new TableRow();
			table.Rows.Add(trTitle);
			trTitle.Attributes.Add("Class","header1_table_first_row");
			trTitle.ForeColor = System.Drawing.Color.DarkRed;
			trTitle.Font.Bold = true;

			//UserName
			TableCell tcUserNameTitle = new TableCell();
			trTitle.Cells.Add(tcUserNameTitle);
			tcUserNameTitle.Text = "User name";
			tcUserNameTitle.Width = System.Web.UI.WebControls.Unit.Percentage(50);
			tcUserNameTitle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;

			//Duration
			TableCell tcDurationTitle = new TableCell();
			trTitle.Cells.Add(tcDurationTitle);
			tcDurationTitle.Text = "Learning duration";
			tcDurationTitle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;

			DataSet dsDuration = sqldb.getDataSet(strSQL);
			if(dsDuration.Tables[0].Rows.Count > 0)
			{
				for(int i=0 ; i<dsDuration.Tables[0].Rows.Count ; i++)
				{
					TableRow tr = new TableRow();
					table.Rows.Add(tr);
					if(i % 2 == 0)
					{
						tr.Attributes.Add("Class","header1_tr_odd_row");
					}
					else
					{
						tr.Attributes.Add("Class","header1_tr_even_row");
					}

					//User name
					TableCell tcUserName = new TableCell();
					tr.Cells.Add(tcUserName);
					tcUserName.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
					try
					{
						tcUserName.Text = dsDuration.Tables[0].Rows[i]["cUserID"].ToString();
					}
					catch
					{
					}

					//Duration
					string strStartTime = "";
					string strUserID = "";
					try
					{
						strStartTime = dsDuration.Tables[0].Rows[i]["cStartTime"].ToString();
						strUserID = dsDuration.Tables[0].Rows[i]["cUserID"].ToString();
					}
					catch
					{
					}

					//從Summary_Header找出這一筆資料
					string strUserLevel = "";
					string strCaseID = "";
					string strDivisionID = "";
					strSQL = "SELECT * FROM Summary_Header H WHERE cUserID = '"+strUserID+"' AND cStartTime = '"+strStartTime+"' ORDER BY cStartTime ";
					DataSet dsHeader = sqldb.getDataSet(strSQL);
					if(dsHeader.Tables[0].Rows.Count > 0)
					{
						try
						{
							strUserLevel = dsHeader.Tables[0].Rows[0]["cUserLevel"].ToString();
							strCaseID = dsHeader.Tables[0].Rows[0]["cCaseID"].ToString();
							strDivisionID = dsHeader.Tables[0].Rows[0]["cDivisionID"].ToString();
						}
						catch
						{
						}
					}
					dsHeader.Dispose();

					//找出第一次操作此教案的StartTime
					strSQL = "SELECT MIN(cStartTime) AS 'cStartTime' FROM Summary_Header H WHERE cUserLevel = '"+strUserLevel+"' AND cUserID = '"+strUserID+"' AND cCaseID = '"+strCaseID+"' AND cDivisionID = '"+strDivisionID+"' ";
					DataSet dsStartTime = sqldb.getDataSet(strSQL);
					if(dsStartTime.Tables[0].Rows.Count > 0)
					{
						try
						{
							strStartTime = dsStartTime.Tables[0].Rows[0]["cStartTime"].ToString();
						}
						catch
						{
						}
					}
					dsStartTime.Dispose();

					string strDuration = "";
					try
					{
						strDuration = myReceiver.calculateSectionDuration(strCaseID , strDivisionID , strUserLevel , strUserID , strStartTime);
					}
					catch
					{
					}

					
					TableCell tcDuration = new TableCell();
					tr.Cells.Add(tcDuration);
					tcDuration.ForeColor = System.Drawing.Color.Blue;
					tcDuration.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
					try
					{
						tcDuration.Text = strDuration;
					}
					catch
					{
					}

				}
			}
			dsDuration.Dispose();
		}

		private void setupSelectionContent(Table table , Table tableOutput)
		{
			//取得此問卷的所有選擇題
			string strSQL = "";
			strSQL = mySQL.getPaperSelectionContent(strPaperID);
			DataSet dsQuestion = sqldb.getDataSet(strSQL);
			if(dsQuestion.Tables[0].Rows.Count > 0)
			{
				for(int i=0 ; i<dsQuestion.Tables[0].Rows.Count ; i++)
				{
					//取得此問題的QID
					string strQID = "";
					try
					{
						strQID = dsQuestion.Tables[0].Rows[i]["cQID"].ToString();
					}
					catch
					{
					}

					//取得此問題的內容
					string strQuestion = "";
					try
					{
						strQuestion = dsQuestion.Tables[0].Rows[i]["cQuestion"].ToString();
                        if (strQuestion == "") {	// get from QuestionIndex table
                            // added by dolphin @ 2007-03-22
                            strSQL = "SELECT * FROM QuestionIndex WHERE cQID='" + strQID + "'";
                            DataTable dtQuestion = sqldb.getDataSet(strSQL).Tables[0];
                            if (dtQuestion.Rows.Count > 0)
                            {
                                strQuestion = dtQuestion.Rows[0]["cQuestion"].ToString();
                            }
                        }
					}
					catch
					{
					}

					//此問題的題號
					intQuestionNum = intQuestionNum + 1;

					//------------------建立TableRow--------------------
					//問題的TableRow
					TableRow trQuestion = new TableRow();
					table.Rows.Add(trQuestion);

					//建立問題的CSS
					trQuestion.Attributes.Add("Class","header1_table_first_row");
					trQuestion.ForeColor = System.Drawing.Color.DarkRed;
					trQuestion.Font.Bold = true;

					//選項的TableRow
					TableRow trSelection = new TableRow();
					table.Rows.Add(trSelection);
					trSelection.Attributes.Add("Class","header1_tr_odd_row");

					//人數的TableRow
					TableRow trNum = new TableRow();
					table.Rows.Add(trNum);
					trNum.Attributes.Add("Class","header1_tr_even_row");

					//非常同意+同意的百分比
					TableRow trPer = new TableRow();
					table.Rows.Add(trPer);
					trPer.CssClass = "header1_tr_odd_row";

					//間隔的TableRow
					TableRow trEmpty = new TableRow();
					table.Rows.Add(trEmpty);

					TableCell tcEmpty = new TableCell();
					trEmpty.Cells.Add(tcEmpty);

					Label lbEmpty = new Label();
					tcEmpty.Controls.Add(lbEmpty);
					lbEmpty.Text = "";
					lbEmpty.Style.Add("WIDTH","0px");

					//------------------建立TableCell-------------------
					//建立此問題的TableCell
					TableCell tcQuestion = new TableCell();
					trQuestion.Cells.Add(tcQuestion);
					tcQuestion.Text = intQuestionNum.ToString() + ". " + strQuestion;

					//建立Output table 的內容
					TableRow trOutput = new TableRow();
					tableOutput.Rows.Add(trOutput);
					TableCell tcOutput = new TableCell();
					if(i % 2 == 0)
					{
						trOutput.CssClass = "header1_tr_even_row";
					}
					else
					{
						trOutput.CssClass = "header1_tr_odd_row";
					}
					trOutput.Cells.Add(tcOutput);
					tcOutput.Text = strQuestion;

					//此問題選項的TableCell
					this.setupSelectionAndNumRow(tcQuestion , trSelection , trNum , trPer , strQID , trOutput);
				}
			}
			else
			{
				//此問卷沒有選擇題
			}
			dsQuestion.Dispose();
		}

		private void setupSelectionAndNumRow(TableCell tcQuestion , TableRow trSelection , TableRow trNum , TableRow trPer , string strQID , TableRow trOutput)
		{
			//找出此問題的選項
			string strSQL = "";
			strSQL = mySQL.getQuestionAndSelection(strQID);
			DataSet dsSelection = sqldb.getDataSet(strSQL);
			if(dsSelection.Tables[0].Rows.Count > 0)
			{
				//紀錄此選項總共有幾個人選
				double dTotalNum = 0;

				for(int i=0 ; i<dsSelection.Tables[0].Rows.Count ; i++)
				{
					//取得選項的內容
					string strSelection = "";
					try
					{
						strSelection = dsSelection.Tables[0].Rows[i]["cSelection"].ToString();
					}
					catch
					{
					}

					//取得選項的SelectionID
					string strSelectionID = "";
					try
					{
						strSelectionID = dsSelection.Tables[0].Rows[i]["cSelectionID"].ToString();
					}
					catch
					{
					}

					//在trSelection建立選項的內容
					TableCell tcSelection = new TableCell();
					trSelection.Cells.Add(tcSelection);
					tcSelection.Text = strSelection;
					tcSelection.HorizontalAlign = HorizontalAlign.Center;

					//在trSelection建立此選項的人數
					TableCell tcNum = new TableCell();
					trNum.Cells.Add(tcNum);
					
					//依照不同Function呼叫不同的Function
					switch (strFunction)
					{
						case "0":
							//Author,Case , Class
							strSQL = mySQL.getSelectionSummaryByCaseClass(strPaperID , strQID , strSelectionID , strCaseID , strClass);
							break;
						case "1":
							//Author , Case , Group
							strSQL = mySQL.getSelectionSummaryByCaseGroup(strPaperID , strQID , strSelectionID , strCaseID , strGroup);
							break;
						case "2":
							//Author , Case
							strSQL = mySQL.getSelectionSummaryByCase(strPaperID , strQID , strSelectionID , strCaseID);
							break;
						case "3":
							//Author , Class
							strSQL = mySQL.getSelectionSummaryByAuthorClass(strPaperID , strQID , strSelectionID , strAuthorID , strClass);
							break;
						case "4":
							//Author , Group
							strSQL = mySQL.getSelectionSummaryByAuthorGroup(strPaperID , strQID , strSelectionID , strAuthorID , strGroup);
							break;
						case "5":
							//Author
							strSQL = mySQL.getSelectionSummaryByAuthor(strPaperID , strQID , strSelectionID , strAuthorID);
							break;
						case "6":
							//Case , Class
							strSQL = mySQL.getSelectionSummaryByCaseClass(strPaperID , strQID , strSelectionID , strCaseID , strClass);
							break;
						case "7":
							//Case , Group
							strSQL = mySQL.getSelectionSummaryByCaseGroup(strPaperID , strQID , strSelectionID , strCaseID , strGroup);
							break;
						case "8":
							//Case
							strSQL = mySQL.getSelectionSummaryByCase(strPaperID , strQID , strSelectionID , strCaseID);
							break;
						case "9":
							//Class
							strSQL = mySQL.getSelectionSummaryByClass(strPaperID , strQID , strSelectionID , strClass);
							break;
						case "10":
							//Group
							strSQL = mySQL.getSelectionSummaryByGroup(strPaperID , strQID , strSelectionID , strGroup);
							break;
					}

					//設定其內容
					DataSet dsNum = sqldb.getDataSet(strSQL);

					//選題的人數
					tcNum.Text = dsNum.Tables[0].Rows.Count.ToString();
					//將人數累加進總人數
					dTotalNum += dsNum.Tables[0].Rows.Count;
					//設定tableOutput的人數
					TableCell tcOutput = new TableCell();
					trOutput.Cells.Add(tcOutput);
					tcOutput.Text = dsNum.Tables[0].Rows.Count.ToString();
					tcOutput.HorizontalAlign = HorizontalAlign.Center;
					
					tcNum.ForeColor = System.Drawing.Color.Blue;
					dsNum.Dispose();

					tcNum.HorizontalAlign = HorizontalAlign.Center;
				}

				
				//將已經產生的人數資料行掃描一遍
				long intSelectPer1 = 0;
                long intSelectPer2 = 0;         
				for(int i=0 ; i<trNum.Cells.Count ; i++)
				{
					//將人數的旁邊加入百分比

					//取出人數
                    double dNum = Convert.ToDouble(trNum.Cells[i].Text);

					//將人數/總人數=百分比
                    long intPer;
                    if(dTotalNum==0)//避免除零
                    {
                        intPer = 0;
                    }else{
                        intPer = Convert.ToInt64((dNum / dTotalNum) * 100);
                    } 
                    
                    
					//紀錄非常同意和同意的百分比
					if(i==0)
					{
						intSelectPer1 = intPer;
					}
					else if(i==1)
					{
						intSelectPer2 = intPer;
					}

					//將百分比加入TableCell
					trNum.Cells[i].Text = trNum.Cells[i].Text + "  " + "(" + intPer.ToString() + "%)";
					
					//設定非常同意與同意的百分比
					if(i==2)
					{
						TableCell tcPer = new TableCell();
						trPer.Cells.Add(tcPer);
						tcPer.ColumnSpan = 2;
						tcPer.HorizontalAlign = HorizontalAlign.Center;

						long intAgree = intSelectPer1 + intSelectPer2;
						//tcPer.Text = "非常同意(%) + 同意(%) = " + intAgree.ToString() + "(%)";
						tcPer.Text = intAgree.ToString() + "(%)";

						TableCell tcEmpty = new TableCell();
						trPer.Cells.Add(tcEmpty);
						tcEmpty.ColumnSpan = 3;
					}
				}

				//調整tcQuestion的ColSpan屬性
				tcQuestion.ColumnSpan = dsSelection.Tables[0].Rows.Count;
			}
			else
			{
				//此問題沒有選項
			}
			dsSelection.Dispose();
		}

		private void setupTextContent(Table table)
		{
			//取得此問卷的所有問答題
			string strSQL = "";
			strSQL = mySQL.getPaperTextContent(strPaperID);
			DataSet dsText = sqldb.getDataSet(strSQL);
			if(dsText.Tables[0].Rows.Count > 0)
			{
				for(int i=0 ; i<dsText.Tables[0].Rows.Count ; i++)
				{
					//此問題的題號
					intQuestionNum = intQuestionNum + 1;

					//取得此問題的內容
					string strQuestion = "";
					try
					{
						strQuestion = dsText.Tables[0].Rows[i]["cQuestion"].ToString();
					}
					catch
					{
					}

					//取得此問題的QID
					string strQID = "";
					try
					{
						strQID = dsText.Tables[0].Rows[i]["cQID"].ToString();
					}
					catch
					{
					}

					//建立問題的TableRow
					TableRow trQuestion = new TableRow();
					table.Rows.Add(trQuestion);

					//建立問題的TableCell
					TableCell tcQuestion = new TableCell();
					trQuestion.Cells.Add(tcQuestion);
					tcQuestion.Text = intQuestionNum.ToString() + ". " + strQuestion;
					//***如果要顯示學生姓名請改成2
					tcQuestion.ColumnSpan = 1;

					//建立問題的CSS
					trQuestion.Attributes.Add("Class","header1_table_first_row");
					trQuestion.ForeColor = System.Drawing.Color.DarkRed;
					trQuestion.Font.Bold = true;

					//建立此問題的學生答案
					this.setupTextAnswer(table , strQID);

					//間隔的TableRow
					if(i != (dsText.Tables[0].Rows.Count - 1))
					{
						TableRow trEmpty = new TableRow();
						table.Rows.Add(trEmpty);

						TableCell tcEmpty = new TableCell();
						trEmpty.Cells.Add(tcEmpty);

						Label lbEmpty = new Label();
						tcEmpty.Controls.Add(lbEmpty);
						lbEmpty.Text = "";
						lbEmpty.Style.Add("WIDTH","0px");
					}
				}
			}
			else
			{
				//此問卷沒有問答題
			}
			dsText.Dispose();
		}

		private void setupTextAnswer(Table table , string strQID)
		{
			//建立學生答案的function
			string strSQL = "";
			//依照不同MODE呼叫不同的Function
			switch(strFunction)
			{
				case "0":
					//Author,Case , Class
					strSQL = mySQL.getTextSummaryByCaseClass(strPaperID , strQID , strCaseID , strClass);
					break;
				case "1":
					//Author , Case , Group
					strSQL = mySQL.getTextSummaryByCaseGroup(strPaperID , strQID , strCaseID , strGroup);
					break;
				case "2":
					//Author , Case
					strSQL = mySQL.getTextSummaryByCase(strPaperID , strQID , strCaseID);
					break;
				case "3":
					//Author , Class
					strSQL = mySQL.getTextSummaryByAuthorClass(strPaperID , strQID , strAuthorID , strClass);
					break;
				case "4":
					//Author , Group
					strSQL = mySQL.getTextSummaryByAuthorGroup(strPaperID , strQID , strAuthorID , strGroup);
					break;
				case "5":
					//Author
					strSQL = mySQL.getTextSummaryByAuthor(strPaperID , strQID ,strAuthorID);
					break;
				case "6":
					//Case , Class
					strSQL = mySQL.getTextSummaryByCaseClass(strPaperID , strQID , strCaseID , strClass);
					break;
				case "7":
					//Case , Group
					strSQL = mySQL.getTextSummaryByCaseGroup(strPaperID , strQID , strCaseID , strGroup);
					break;
				case "8":
					//Case
					strSQL = mySQL.getTextSummaryByCase(strPaperID , strQID , strCaseID);
					break;
				case "9":
					//Class
					strSQL = mySQL.getTextSummaryByClass(strPaperID , strQID , strClass);
					break;
				case "10":
					//Group
					strSQL = mySQL.getTextSummaryByGroup(strPaperID , strQID , strGroup);
					break;
			}

			//設定其內容
			
			DataSet dsAnswer = sqldb.getDataSet(strSQL);
			if(dsAnswer.Tables[0].Rows.Count > 0)
			{
				for(int i=0 ; i< dsAnswer.Tables[0].Rows.Count ; i++)
				{
					//UsrID
					string strUserID = "";
					try
					{
						strUserID = dsAnswer.Tables[0].Rows[i]["cUserID"].ToString();
					}
					catch
					{
					}

					//User name
					string strUserName = "";
					try
					{
						strUserName = myReceiver.getUserName(strUserID);
					}
					catch
					{
					}

					//Answer
					string strAnswer = "";
					try
					{
						strAnswer = dsAnswer.Tables[0].Rows[i]["cAnswer"].ToString();
					}
					catch
					{
					}

					TableRow trAnswer = new TableRow();
					table.Rows.Add(trAnswer);
					//建立CSS
					if((i % 2) != 0)
					{
						trAnswer.Attributes.Add("Class","header1_tr_odd_row");	
					}
					else
					{
						trAnswer.Attributes.Add("Class","header1_tr_even_row");	
					}
					/***如果要顯示學生姓名請移除註解
					//學生姓名
					TableCell tcName = new TableCell();
					trAnswer.Cells.Add(tcName);
					tcName.Text = "";//strUserName;
					tcName.Width = Unit.Pixel(0);
					*/
					//答案
					TableCell tcAnswer = new TableCell();
					trAnswer.Cells.Add(tcAnswer);
					tcAnswer.Text = strAnswer;
					tcAnswer.Style.Add("FONT-WEIGHT","bold");
					tcAnswer.Style.Add("FONT-SIZE","16px");
					tcAnswer.ForeColor = System.Drawing.Color.Blue;
				}
			}
			dsAnswer.Dispose();
		}


		private void getParameter()
		{
			//PaperID
			if(usi.PaperID != "")
			{
                strPaperID = usi.PaperID;
			}
			else
			{
                // modified @ 2007-03-22 by dolphin, modified the Query string check
                if (Request["PaperID"] != null && Request.QueryString["PaperID"] != "")
                {
                    strPaperID = Request.QueryString["PaperID"];
                }
			}

			//Function
			if(Request.QueryString["Function"] != null)
			{
				strFunction = Request.QueryString["Function"].ToString();
			}
			else
			{
				strFunction = "0";
			}

			//CaseID
			if(usi.CaseID != null)
			{
                strCaseID = usi.CaseID;
			}
			else
			{
				if(Request.QueryString["CaseID"] != null)
				{
					strCaseID = Request.QueryString["CaseID"].ToString();
				}
			}

			//AuthorID
			if(Session["Author"] != null)
			{
				strAuthorID = Session["Author"].ToString();
			}
			else
			{
				if(Request.QueryString["AuthorID"] != null)
				{
					strAuthorID = Request.QueryString["AuthorID"].ToString();
				}
			}
			

			//Class
			if(Session["Class"] != null)
			{
				strClass = Session["Class"].ToString();
			}
			else
			{
				if(Request.QueryString["Class"] != null)
				{
					strClass = Request.QueryString["Class"].ToString();
				}
			}
			
			//Group
			if(Session["Group"] != null)
			{
				strGroup = Session["Group"].ToString();
			}
			else
			{
				if(Request.QueryString["Group"] != null)
				{
					strGroup = Request.QueryString["Group"].ToString();
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
