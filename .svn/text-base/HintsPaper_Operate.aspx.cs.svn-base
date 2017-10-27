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
	/// HintsPaper_Operate 的摘要描述。
	/// </summary>
    public partial class HintsPaper_Operate : AuthoringTool_BasicForm_BasicForm
	{
		string strCaseID , strDivisionID , strStartTime , strOperationTime , strClinicNum , strSectionName;
		//string strClinicNum , strSectionName ;
		string strUserID;
		string strPaperID;
		string strPresentType = "";

		protected string strPaperName = "";
		string strObjective = "";
		string strEditMethod = "";
		string strGenerationMethod = "";
		string strPresentMethod = "";

		int intTestDuration = 0;
		int intQuestionNum = 0;//問題的數量，要加入Header的欄位中。
		int intQuestionIndex = 0;

		//建立SqlDB物件
		SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
		DataReceiver myReceiver = new DataReceiver();
		RandomSelect myQuestionSelect = new RandomSelect();
		SQLString mySQLString = new SQLString();
		//HintsUtility.DBAccelerator dba = new HintsUtility.DBAccelerator(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);


		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.Initiate();
            
			//接收參數(UserID , StartTime , CaseID , DivisionID , PaperID)
			this.getParameter();

			//檢查此使用者建立strOperationTime字串
			if(this.IsPostBack != true)
			{
				strOperationTime = myReceiver.getNowTime();
				hiddenOperationTime.Value = strOperationTime;
			}

			//get the Paper Header from the database
			string strSQL = "";
			strSQL = "SELECT * FROM Paper_Header WHERE cPaperID = '" + strPaperID + "' ";

			DataSet dsHeader = sqldb.getDataSet(strSQL);
			if(dsHeader.Tables[0].Rows.Count != 0)
			{
				if(dsHeader.Tables[0].Rows[0]["cPaperName"] != DBNull.Value)
				{
					strPaperName = dsHeader.Tables[0].Rows[0]["cPaperName"].ToString();
				}

				if(dsHeader.Tables[0].Rows[0]["cObjective"] != DBNull.Value)
				{
					strObjective = dsHeader.Tables[0].Rows[0]["cObjective"].ToString();
				}

				if(dsHeader.Tables[0].Rows[0]["cEditMethod"] != DBNull.Value)
				{
					strEditMethod = dsHeader.Tables[0].Rows[0]["cEditMethod"].ToString();
				}

				if(dsHeader.Tables[0].Rows[0]["cGenerationMethod"] != DBNull.Value)
				{
					strGenerationMethod = dsHeader.Tables[0].Rows[0]["cGenerationMethod"].ToString();
				}

				if(dsHeader.Tables[0].Rows[0]["cPresentMethod"] != DBNull.Value)
				{
					strPresentMethod = dsHeader.Tables[0].Rows[0]["cPresentMethod"].ToString();
				}

				if(dsHeader.Tables[0].Rows[0]["sTestDuration"] != DBNull.Value)
				{
					intTestDuration = Convert.ToInt32(dsHeader.Tables[0].Rows[0]["sTestDuration"]);
				}
				
				if(strEditMethod == "System" && strGenerationMethod == "Present")
				{
					if(dsHeader.Tables[0].Rows[0]["sQuestionNum"] != DBNull.Value)
					{
						intQuestionNum = myReceiver.getQuestionCountFromGroupingQuestion(strPaperID);
					}
				}
				else
				{
					intQuestionNum = myReceiver.getPaperQuestionCount(strPaperID);
				}
			}
			dsHeader.Dispose();

			this.setupHintsQuestionTable();

			//Hints的問卷系統
            HtmlInputButton btnHints = new HtmlInputButton();
			btnHints.ID = "btnHints";
			phSubmit.Controls.Add(btnHints);
            btnHints.Value = this.GetMultiLanguageString("SubmitPaper");
            btnHints.ServerClick += new EventHandler(btnHints_ServerClick);
		}

		private void setupHintsQuestionTable()
		{
			Table table = new Table();
			
			table.CellSpacing = 0;
			table.CellPadding = 2;
			table.BorderStyle = BorderStyle.Solid;
			table.BorderWidth=Unit.Pixel(1);
			table.BorderColor = System.Drawing.Color.Black;
			table.GridLines = GridLines.None;
			table.Width = Unit.Percentage(100);

			intQuestionIndex = 0;

			//取出此問卷的選擇題內容
			string strSQL = mySQLString.getPaperSelectionContent(strPaperID);
			DataSet dsQuestionList = sqldb.getDataSet(strSQL);
			if(dsQuestionList.Tables[0].Rows.Count > 0)
			{
				for(int i=0 ; i<dsQuestionList.Tables[0].Rows.Count ; i++)
				{
					//取得QuestionType
					string strQuestionType = "1";
					try
					{
						strQuestionType = dsQuestionList.Tables[0].Rows[i]["cQuestionType"].ToString();
					}
					catch
					{
					}

					//取得QID
					string strQID = "";
					try
					{
						strQID = dsQuestionList.Tables[0].Rows[i]["cQID"].ToString();
					}
					catch
					{
					}

					//設定Selection row的CSS所設定的變數
					int intQuestionCount = 0;
					
					//取得問題的SQL
					strSQL = mySQLString.getSingleQuestionInformation(strQID);
					DataSet dsQuestion = sqldb.getDataSet(strSQL);

					if(dsQuestion.Tables[0].Rows.Count > 0)
					{

						//建立問題的內容
						TableRow trQuestion = new TableRow();
						table.Rows.Add(trQuestion);

						intQuestionIndex += 1;
						
						//問題的題號
						string strQuestionNum = "Q" + intQuestionIndex.ToString() + ": ";

						//問題的內容
						string strQuestion = "";
						try
						{
							strQuestion = dsQuestion.Tables[0].Rows[0]["cQuestion"].ToString();
						}
						catch
						{
						}
						TableCell tcQuestion = new TableCell();
						trQuestion.Cells.Add(tcQuestion);
						tcQuestion.Text = strQuestionNum + strQuestion;
						double dQuestionWidth = tcQuestion.Width.Value;

						//建立問題的CSS
						trQuestion.Attributes.Add("Class","TableTitle");
						trQuestion.ForeColor = System.Drawing.Color.DarkRed;
						trQuestion.Font.Bold = true;

						//建立選項
						TableRow trSelection = new TableRow();
						table.Rows.Add(trSelection);
			
						int intColSpan = 0;
						strSQL = mySQLString.getAllSelections(strQID);
						DataSet dsSelection = sqldb.getDataSet(strSQL);
						if(dsSelection.Tables[0].Rows.Count > 0)
						{
							for(int j=0 ; j<dsSelection.Tables[0].Rows.Count ; j++)
							{
								intQuestionCount += 1;

								//SelectionID
								string strSelectionID = "";
								try
								{
									strSelectionID = dsSelection.Tables[0].Rows[j]["cSelectionID"].ToString();
								}
								catch
								{
								}

								//Seq
								string strSeq = "";
								try
								{
									strSeq = dsSelection.Tables[0].Rows[j]["sSeq"].ToString();
								}
								catch
								{
								}

								//Selection
								string strSelection = "";
								try
								{
									strSelection = dsSelection.Tables[0].Rows[j]["cSelection"].ToString();
								}
								catch
								{
								}

								//bCaseSelect
								bool bCaseSelect = false;
								try
								{
									bCaseSelect = Convert.ToBoolean(dsSelection.Tables[0].Rows[j]["bCaseSelect"]);
								}
								catch
								{
								}
								
								//選項內容與RadioButton
								TableCell tcSelection = new TableCell();
								trSelection.Cells.Add(tcSelection);

								RadioButton rbSelection = new RadioButton();
								tcSelection.Controls.Add(rbSelection);
								rbSelection.Text = strSelection;
								rbSelection.GroupName = strQID;
								string strID = "rb-" + strQID + "-" + strSelectionID;
								rbSelection.ID = strID;
								try
								{
									if(Request.Form[strQID] != null)
									{
										if(Request.Form[strQID].ToString() == strID)
										{
											rbSelection.Checked = true;
										}
									}
								}
								catch
								{
								}
								intColSpan += 1;
							}
							tcQuestion.ColumnSpan = intColSpan;
						}
						else
						{
							//此問題沒有選項
						}
						dsSelection.Dispose();
					}
					else
					{
						//此問題沒有選項
					}
					dsQuestion.Dispose();
				}
			}
			else
			{
				//此問卷沒有選擇題的情況
			}
			dsQuestionList.Dispose();

			int intMaxColSpan = 0;

			//調整table的TableCell寬度
			int intSelectionIndex = 0;
			
			for(int i=0 ; i<table.Rows.Count ; i++)
			{
				if(i % 2 != 0)
				{
					for(int j=0 ; j<table.Rows[i].Cells.Count ; j++)
					{
						double dPercentage = Convert.ToDouble(100) / Convert.ToDouble(table.Rows[i].Cells.Count);
						//將每個SelectionCell的寬度設成QuestionCell的寬度/tcSelection的個數
						table.Rows[i].Cells[j].Width = Unit.Percentage(dPercentage);

						if(table.Rows[i].Cells.Count > intMaxColSpan)
						{
							intMaxColSpan = table.Rows[i].Cells.Count;
						}
					}
					//建立選項的CSS
					if((Convert.ToInt32(intSelectionIndex) % 2) != 0)
					{
						table.Rows[i].Attributes.Add("Class","TableOddRow");	
					}
					else
					{
						table.Rows[i].Attributes.Add("Class","TableEvenRow");	
					}
					intSelectionIndex += 1;
				}
			}

			//建立屬於此問卷的問答題
			strSQL = mySQLString.getPaperTextContent(strPaperID);
			DataSet dsTextList = sqldb.getDataSet(strSQL);
			int intTextCount = 0;
			if(dsTextList.Tables[0].Rows.Count > 0)
			{
				for(int i=0 ; i<dsTextList.Tables[0].Rows.Count ; i++)
				{
					intQuestionIndex += 1;

					//取得此問題的QID
					string strQID = "";
					try
					{
						strQID = dsTextList.Tables[0].Rows[i]["cQID"].ToString();
					}
					catch
					{
					}

					TableRow trQuestion = new TableRow();
					table.Rows.Add(trQuestion);
					
					//Question number
					string strQuestionNum = "Q" + intQuestionIndex.ToString() + ": ";

					//Question
					TableCell tcQuestion = new TableCell();
					trQuestion.Cells.Add(tcQuestion);
					tcQuestion.ColumnSpan = intMaxColSpan;
					string strQuestion = "";
					try
					{
						strQuestion = dsTextList.Tables[0].Rows[i]["cQuestion"].ToString();
					}
					catch
					{
					}
					tcQuestion.Text = strQuestionNum + strQuestion;

					//加入CSS
					intTextCount += 1;
					trQuestion.Attributes.Add("Class","TableTitle");
					trQuestion.ForeColor = System.Drawing.Color.DarkRed;
					trQuestion.Font.Bold = true;

					//加入答案TableRow
					TableRow trAnswer = new TableRow();
					table.Rows.Add(trAnswer);


					TableCell tcAnswer = new TableCell();
					trAnswer.Cells.Add(tcAnswer);
					tcAnswer.ColumnSpan = intMaxColSpan;

					//加入TextArea
					HtmlTextArea txtAnswer = new HtmlTextArea();
					tcAnswer.Controls.Add(txtAnswer);
					string strTxtID = "txt-" + strQID;
					txtAnswer.ID = strTxtID;
					txtAnswer.Cols = 80;
					txtAnswer.Rows = 5;
					txtAnswer.Style.Add("WIDTH","100%");
					try
					{
						if(Request.Form[strTxtID] != null)
						{
							txtAnswer.Value = Request.Form[strTxtID].ToString();
						}
					}
					catch
					{
					}

					hiddenTextID.Value += strTxtID + ";";
					hiddenTextCount.Value = Convert.ToString(Convert.ToInt32(hiddenTextCount.Value) + 1);

					//套用CSS
					if((intTextCount % 2) != 0)
					{
						trAnswer.Attributes.Add("Class","TableOddRow");
					}
					else
					{
						trAnswer.Attributes.Add("Class","TableEvenRow");
					}
				}
			}
			else
			{
				//此問卷沒有問答題的情形
			}
			dsTextList.Dispose();

			//加入Empty TableRow
			int a = 0;
			for(int i=table.Rows.Count-1 ; i>=1 ; i--)
			{
				if(a == 1)
				{
					TableRow tr1 = new TableRow();
					table.Rows.AddAt(i,tr1);

					TableCell tc1 = new TableCell();
					tr1.Cells.Add(tc1);
					tc1.ForeColor = System.Drawing.Color.Transparent;
					tc1.Font.Size = FontUnit.Parse("0");
					tc1.Text = "1";

					TableRow tr2 = new TableRow();
					table.Rows.AddAt(i,tr2);

					TableCell tc2 = new TableCell();
					tr2.Cells.Add(tc2);
					tc2.ForeColor = System.Drawing.Color.Transparent;
					tc2.Font.Size = FontUnit.Parse("0");
					tc2.Text = "1";

					a = 0;
				}
				else
				{
					a += 1;
				}
			}

			phQuestion.Controls.Clear();
			phQuestion.Controls.Add(table);
		}		

		private void getParameter()
		{
			//display=&UserID=wyt&UserLevel=1&StartTime=20051020124216&CaseID=gait001Case200401061006167084912&DivisionID=7501&ClinicNum=1&SectionName=Examination&CaseSelect=
			//以Query string 優先
			//UserID
			if(Request.QueryString["UserID"] != null)
			{
				strUserID = Request.QueryString["UserID"].ToString();
			}
			else
			{
				if(usi.UserID != null)
				{
                    strUserID = usi.UserID;
				}
				else
				{
					strUserID = "swakevin";
				}
			}
			

			//StartTime
			if(Request.QueryString["StartTime"] != null)
			{
				strStartTime = Request.QueryString["StartTime"].ToString();
			}
			else
			{
				if(usi.StartTime != null)
				{
                    strStartTime = usi.StartTime;
				}
				else
				{
					strStartTime = "20050906171257";
				}
			}

			//CaseID kyhCase200505301448128593750
			if(Request.QueryString["CaseID"] != null)
			{
				strCaseID = Request.QueryString["CaseID"].ToString();
			}
			else
			{
				if(usi.CaseID != null)
				{
                    strCaseID = usi.CaseID;
				}
				else
				{
					strCaseID = "kyhCase200505301448128593750";
				}
			}

			//Division 9801
			if(Request.QueryString["DivisionID"] != null)
			{
				strDivisionID = Request.QueryString["DivisionID"].ToString();
			}
			else
			{
				if(usi.Division != null)
				{
                    strDivisionID = usi.Division;
				}
				else
				{
					strDivisionID = "9801";
				}
			}

			//ClinicNum
			if(Request.QueryString["ClinicNum"] != null)
			{
				strClinicNum = Request.QueryString["ClinicNum"].ToString();
			}
			else
			{
				if(usi.ClinicNum != null)
				{
                    strClinicNum = usi.ClinicNum.ToString();
				}
				else
				{
					strClinicNum = "1";
				}
			}

			//SectionName
			if(usi.Section != null)
			{
				strSectionName = Request.QueryString["SectionName"].ToString();
			}
			else
			{
                if (usi.Section != null)
				{
                    strSectionName = usi.Section;
				}
				else
				{
					strSectionName = "Examination";
				}
			}

			//PresentType
			if(Request.QueryString["PresentType"] != null)
			{
				strPresentType = Request.QueryString["PresentType"].ToString();

				//依照PresentType取得對應的問卷ID
				strPaperID = myReceiver.getPaperIDByPresentType(strPresentType , strCaseID , strDivisionID , strClinicNum , strSectionName);
			}
			else
			{
				//如果沒有PresentType，就從Session取得PaperID
				if(Request.QueryString["PaperID"] != null)
				{
					strPaperID = Request.QueryString["PaperID"].ToString();
					if(usi.PaperID != null)
					{
                        usi.PaperID = strPaperID;
					}
				}
				else
				{
                    if (usi.PaperID != null)
					{
                        strPaperID = usi.PaperID;
					}
					else
					{
						strPaperID = "swakevin20050907120001";
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

        void btnHints_ServerClick(object sender, EventArgs e)
        {
            //Hints的問卷系統

            //檢查此問卷是不是每個問題都有被回答
            bool bEmptyCheck = true;

            //取出此問卷的選擇題內容
            string strSQL = mySQLString.getPaperSelectionContent(strPaperID);
            DataSet dsQuestionCheck = sqldb.getDataSet(strSQL);
            if (dsQuestionCheck.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsQuestionCheck.Tables[0].Rows.Count; i++)
                {
                    //取得QID
                    string strQID = "";
                    try
                    {
                        strQID = dsQuestionCheck.Tables[0].Rows[i]["cQID"].ToString();
                    }
                    catch
                    {
                    }

                    try
                    {
                        if (Request.Form[strQID] == null)
                        {
                            int intEmptyIndex = i + 1;

                            bEmptyCheck = false;

                            //此問題沒有被選取，顯示警告視窗警告使用者。
                            string strScript = "<script language='javascript'>\n";
                            strScript += "AlertSelectionNull('" + intEmptyIndex + "');\n";
                            strScript += "</script>\n";
                            Page.RegisterStartupScript("AlertSelectionNull", strScript);
                        }
                    }
                    catch
                    {
                        int intEmptyIndex = i + 1;

                        bEmptyCheck = false;

                        //此問題沒有被選取，顯示警告視窗警告使用者。
                        string strScript = "<script language='javascript'>\n";
                        strScript += "AlertSelectionNull('" + intEmptyIndex + "');\n";
                        strScript += "</script>\n";
                        Page.RegisterStartupScript("AlertSelectionNull", strScript);
                    }
                }
            }
            dsQuestionCheck.Dispose();

            //如果每一個問題都有被回答的話，則將每個問題的回答存入資料庫中
            if (bEmptyCheck == true)
            {
                //建立FinishTime
                string strFinishTime = "";
                try
                {
                    strFinishTime = myReceiver.getNowTime();
                }
                catch
                {
                }

                //辨識使用者是否有點選此問卷中的任何一個項目
                bool bPaperCheck = false;

                //取出此問卷的選擇題內容
                strSQL = mySQLString.getPaperSelectionContent(strPaperID);
                DataSet dsQuestionList = sqldb.getDataSet(strSQL);
                if (dsQuestionList.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsQuestionList.Tables[0].Rows.Count; i++)
                    {
                        //取得QuestionType
                        string strQuestionType = "1";
                        try
                        {
                            strQuestionType = dsQuestionList.Tables[0].Rows[i]["cQuestionType"].ToString();
                        }
                        catch
                        {
                        }

                        //取得QID
                        string strQID = "";
                        try
                        {
                            strQID = dsQuestionList.Tables[0].Rows[i]["cQID"].ToString();
                        }
                        catch
                        {
                        }

                        //StandardScore
                        int intStandardScore = 0;
                        try
                        {
                            intStandardScore = Convert.ToInt32(dsQuestionList.Tables[0].Rows[i]["sStandardScore"]);
                        }
                        catch
                        {
                        }

                        //QuestionMode
                        string strQuestionMode = "";
                        try
                        {
                            strQuestionMode = dsQuestionList.Tables[0].Rows[i]["cQuestionMode"].ToString();
                        }
                        catch
                        {
                        }

                        //Seq
                        string strQuestionSeq = "";
                        try
                        {
                            strQuestionSeq = dsQuestionList.Tables[0].Rows[i]["sSeq"].ToString();
                        }
                        catch
                        {
                        }

                        //取得問題的SQL
                        strSQL = mySQLString.getSingleQuestionInformation(strQID);
                        DataSet dsQuestion = sqldb.getDataSet(strSQL);

                        if (dsQuestion.Tables[0].Rows.Count > 0)
                        {
                            intQuestionIndex += 1;

                            //問題的內容
                            string strQuestion = "";
                            try
                            {
                                strQuestion = dsQuestion.Tables[0].Rows[0]["cQuestion"].ToString();
                            }
                            catch
                            {
                            }

                            //Question Level
                            string strQuestionLevel = "";
                            try
                            {
                                strQuestionLevel = dsQuestion.Tables[0].Rows[0]["sLevel"].ToString();
                            }
                            catch
                            {
                            }

                            //QuestionGroupID
                            string strQuestionGroupID = "";
                            try
                            {
                                strQuestionGroupID = dsQuestion.Tables[0].Rows[0]["cQuestionGroupID"].ToString();
                            }
                            catch
                            {
                            }

                            //檢查此問題是不是有被點選
                            bool bQuestionSelect = false;

                            strSQL = mySQLString.getAllSelections(strQID);
                            DataSet dsSelection = sqldb.getDataSet(strSQL);
                            if (dsSelection.Tables[0].Rows.Count > 0)
                            {
                                for (int j = 0; j < dsSelection.Tables[0].Rows.Count; j++)
                                {
                                    //SelectionID
                                    string strSelectionID = "";
                                    try
                                    {
                                        strSelectionID = dsSelection.Tables[0].Rows[j]["cSelectionID"].ToString();
                                    }
                                    catch
                                    {
                                    }

                                    //Seq
                                    string strSelectionSeq = "";
                                    try
                                    {
                                        strSelectionSeq = dsSelection.Tables[0].Rows[j]["sSeq"].ToString();
                                    }
                                    catch
                                    {
                                    }

                                    //Selection
                                    string strSelection = "";
                                    try
                                    {
                                        strSelection = dsSelection.Tables[0].Rows[j]["cSelection"].ToString();
                                    }
                                    catch
                                    {
                                    }

                                    //bCaseSelect
                                    bool bCaseSelect = false;
                                    try
                                    {
                                        bCaseSelect = Convert.ToBoolean(dsSelection.Tables[0].Rows[j]["bCaseSelect"]);
                                    }
                                    catch
                                    {
                                    }

                                    string strCaseSelect = "0";
                                    if (bCaseSelect == false)
                                    {
                                        strCaseSelect = "0";
                                    }
                                    else
                                    {
                                        strCaseSelect = "1";
                                    }

                                    //SelectState
                                    string strSelectState = "";

                                    bool bCheck = false;
                                    try
                                    {
                                        bCheck = ((RadioButton)(this.FindControl("Form1").FindControl("rb-" + strQID + "-" + strSelectionID))).Checked;
                                    }
                                    catch
                                    {
                                    }

                                    if (bCheck == true)
                                    {
                                        strSelectState = "1";
                                    }
                                    else
                                    {
                                        strSelectState = "0";
                                    }


                                    //如果使用者有點選則去比對是否有答對這一個題目
                                    //判斷這一個選項使用者是否有答對
                                    if (strCaseSelect == "0")
                                    {
                                        if (strCaseSelect == strSelectState)
                                        {
                                            //不是建議選項，但是使用者也沒有點選，所以不儲存至TempLog
                                        }
                                        else
                                        {
                                            //使用者有點選此項目
                                            //將資料存入TempLog_PaperSelectionAnswer
                                            strSelectState = "1";
                                            bQuestionSelect = true;
                                            bPaperCheck = true;

                                            //把資料存入TempLog_PaperSelectionAnswer
                                            mySQLString.SaveToTempLog_PaperSelectionAnswer(strPaperID, strStartTime, strUserID, strQID, strSelectionID, strSelectionSeq, strSelection, strCaseSelect);
                                        }
                                    }
                                    else
                                    {
                                        if (strCaseSelect != strSelectState)
                                        {
                                            //是建議選項，但是使用者沒有點選，所以SelectState = 2
                                            //將資料存入TempLog_PaperSelectionAnswer
                                            strSelectState = "2";
                                        }
                                        else
                                        {
                                            //是建議選項，使用者有點選，所以SelectState = 1
                                            //將資料存入TempLog_PaperSelectionAnswer
                                            strSelectState = "1";
                                            bQuestionSelect = true;
                                            bPaperCheck = true;

                                            //把資料存入TempLog_PaperSelectionAnswer
                                            mySQLString.SaveToTempLog_PaperSelectionAnswer(strPaperID, strStartTime, strUserID, strQID, strSelectionID, strSelectionSeq, strSelection, strCaseSelect);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //此問題沒有選項
                            }
                            dsSelection.Dispose();

                            if (bQuestionSelect == true)
                            {
                                //把資料存入TempLog_PaperSelectionQuestion
                                mySQLString.SaveToTempLog_PaperSelectionQuestion(strPaperID, strStartTime, strUserID, strQID, strQuestionLevel, strQuestion);
                            }
                        }
                        else
                        {
                            //此問題沒有選項
                        }
                        dsQuestion.Dispose();

                        //如果使用者有點選此問卷鍾任何一個項目，則新增一筆資料至TempLog_PaperHeader
                        if (bPaperCheck == true)
                        {
                            mySQLString.SaveToTempLog_PaperHeader(strPaperID, strStartTime, strUserID, hiddenOperationTime.Value, strFinishTime);
                        }

                    }
                }
                else
                {
                    //此問卷沒有選擇題的情況
                }
                dsQuestionList.Dispose();


                //建立屬於此問卷的問答題
                strSQL = mySQLString.getPaperTextContent(strPaperID);
                DataSet dsTextList = sqldb.getDataSet(strSQL);
                if (dsTextList.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsTextList.Tables[0].Rows.Count; i++)
                    {
                        //取得此問題的QID
                        string strQID = "";
                        try
                        {
                            strQID = dsTextList.Tables[0].Rows[i]["cQID"].ToString();
                        }
                        catch
                        {
                        }

                        //Question number
                        string strQuestionNum = "Q" + intQuestionIndex.ToString() + ": ";

                        //Question
                        string strQuestion = "";
                        try
                        {
                            strQuestion = dsTextList.Tables[0].Rows[i]["cQuestion"].ToString();
                        }
                        catch
                        {
                        }

                        //取得TextArea的內容
                        string strAnswer = "";
                        try
                        {
                            strAnswer = ((HtmlTextArea)(this.FindControl("Form1").FindControl("txt-" + strQID))).InnerText;
                        }
                        catch
                        {
                        }

                        //StandardScore
                        int intStandardScore = 0;
                        try
                        {
                            intStandardScore = Convert.ToInt32(dsTextList.Tables[0].Rows[i]["sStandardScore"]);
                        }
                        catch
                        {
                        }

                        //如果TextArea的內容不是空的話，則存入資料庫。
                        if (strAnswer.Length > 0)
                        {
                            bPaperCheck = true;

                            //把資料存入TempLog_PaperText
                            mySQLString.SaveToTempLog_PaperTextQuestion(strPaperID, strStartTime, strUserID, strQID, strQuestion, strAnswer);
                        }
                    }
                }
                else
                {
                    //此問卷沒有問答題的情形
                }
                dsTextList.Dispose();

                //如果使用者有點選此問卷鍾任何一個項目，則新增一筆資料至TempLog_PaperHeader
                if (bPaperCheck == true)
                {
                    mySQLString.SaveToTempLog_PaperHeader(strPaperID, strStartTime, strUserID, hiddenOperationTime.Value, strFinishTime);
                }

                //關閉網頁
                string strScript = "<script language='javascript'>\n";
                strScript += "closeWindow();\n";
                strScript += "</script>\n";
                Page.RegisterStartupScript("closeWindow", strScript);
            }
            else
            {
                //有問題沒有回答的情形
                //phQuestion.Controls.Clear();
                this.setupHintsQuestionTable();
            }
        }
	}
}
