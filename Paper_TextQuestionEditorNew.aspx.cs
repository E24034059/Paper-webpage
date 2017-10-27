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
using Hints.DB;
using Hints.DB.Section;
using AuthoringTool.CommonQuestionEdit;
using System.Data.SqlClient;
using iSlide.AppCode.DB;

namespace PaperSystem
{
    /// <summary>
    /// 用來編輯問答題的工具
    /// </summary>
    public partial class Paper_TextQuestionEditorNew : AuthoringTool_BasicForm_BasicForm
    {
        protected string title_Bold = "Bold";
        protected string title_Italic = "Italic";
        protected string title_Underline = "Underline";
        protected string title_Strikethrough = "Strikethrough";
        protected string title_Subscript = "Subscript";
        protected string title_Superscript = "Superscript";
        protected string title_JustifyLeft = "Justify Left";
        protected string title_JustifyCenter = "Justify Center";
        protected string title_JustifyRight = "Justify Right";
        protected string title_OrderedList = "Ordered List";
        protected string title_BulletedList = "Bulleted List";
        protected string title_DecreaseIndent = "Decrease Indent";
        protected string title_IncreaseIndent = "Increase Indent";
        protected string title_FontColor = "Font Color";
        protected string title_BackgroundColor = "Background Color";
        protected string title_HorizontalRule = "Horizontal Rule";
        protected string title_UploadFile = "Upload File";
        protected string title_InsertWebLink = "Insert Web Link";
        protected string title_InsertImage = "Insert Image";
        protected string title_InsertTable = "Insert Table";
        protected string title_ViewHTMLSource = "View HTML Source";
        protected string title_EnlargeEditor = "Enlarge Editor";
        protected string title_Aboutthiseditor = "About this editor";

        protected string btnFinish = "";
        protected string strClientScript = "";

        bool bModify = false;

        string strUserID, strCaseID, strDivisionID, strClinicNum, strSectionName, strPaperID, strAID;
        string strQID = "";
        string strGroupID = "";
        string strGroupDivisionID = "";
        
        private QuestionAccessor qAccessor = null;
        SQLString mySQL = new SQLString();
        protected System.Web.UI.WebControls.Table LayoutTableForFeature = new Table();
        SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.Initiate();

            //接收參數
            this.getParametor();

            //從ORCS的課堂練習頁面接收參數
            if (hiddenPreOpener.Value == "SelectPaperMode")
            {
                //設定從課堂練習頁面進來時，此頁面的控制項
                SetPageStyle();
            }

            //加入btnSaveNextQuestion的事件
            btnSaveNextQuestion.ServerClick += new EventHandler(btnSaveNextQuestion_ServerClick);

            //加入btnSaveNext的事件
            btnSaveNext.ServerClick += new EventHandler(btnSaveNext_ServerClick);


            //初始化Layout
            InititateLayoutTableForFeature();
            //朱君 增加編輯特徵值屬性表格
            this.FindControl("PanelFeature").Controls.Add(LayoutTableForFeature);
            
            string strGetKeyword = Hints.Learning.Question.DataReceiver.getTextQuestionKeyword(strQID);
            if (strGetKeyword != "N/A")
            {
                clsKeywordsUtil keywordsUtil = new clsKeywordsUtil();
                divKeyWords.Controls.Add(keywordsUtil.ConstructKeywordTable(strGetKeyword,false));
                //btnEditKeyword.Text = "Edit the Keyword";
            }

            #region 設定問題Level與病徵的下拉選單項目
            DataTable dtQuestionLevel = new DataTable();
            dtQuestionLevel = AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevelName();
            foreach (DataRow drQuestionLevel in dtQuestionLevel.Rows)
            {
                ddlQuestionLevel.Items.Add(drQuestionLevel["cLevelName"].ToString());
            }

            //建立病徵的下拉選單項目
            DataTable dtDiseaseSymptomsTree = DiseaseSymptomsTree_SELECT();
            ddlSymptoms.Items.Add("All");
            foreach (DataRow drDiseaseSymptomsTree in dtDiseaseSymptomsTree.Rows)
            {
                ddlSymptoms.Items.Add(drDiseaseSymptomsTree["cNodeName"].ToString());
            }
            #endregion

            #region 設定問題Level與病徵的下拉選單選擇項目
            if (!IsPostBack)
            {
                int iQuestionLevel = AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevelValue(strQID);
                if (iQuestionLevel != -1)
                    ddlQuestionLevel.SelectedValue = AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevelName_SELECT_LevelName(iQuestionLevel);
                string strQuestionSymptoms = AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevel_SELECT_QuestionSymptoms(strQID);
                if (strQuestionSymptoms != "-1")
                    ddlSymptoms.SelectedValue = strQuestionSymptoms;
            }
            #endregion

            //設定答案選項的下拉選單項目  老詹 2013/8/28
            if (this.IsPostBack == false)
            {
                this.setupddlAnswerOptions(strQID);
            }

            //如果從課堂練習編輯考卷頁面新增題目功能來 直接關閉視窗
            if (Session["IsFromClassExercise"] != null && Session["IsFromClassExercise"].ToString() == "True")
            {
                btnNextQuestion.Attributes["style"] = "display:none;";
            }

            ddlAnswerOptions.SelectedIndexChanged += new EventHandler(ddlAnswerOptions_SelectedIndexChanged);

            hrQuestion.Style.Add("display", "none");
            hrAnswer.Style.Add("display", "none");
            BulidInterrogation("Question");
            BulidInterrogation("Answer");
        }

        /// <summary>
        /// 接收參數
        /// </summary>
        private void getParametor()
        {
            //UserID
            if (Session["UserID"] != null)
            {
                strUserID = Session["UserID"].ToString();
            }
            //strUserID = "swakevin";

            //CaseID
            if (Session["CaseID"] != null)
            {
                strCaseID = Session["CaseID"].ToString();
                hiddenCaseID.Value = Session["CaseID"].ToString();
            }

            //Division
            if (Session["DivisionID"] != null)
            {
                strDivisionID = Session["DivisionID"].ToString();
            }

            //ClinicNum
            if (Session["ClinicNum"] != null)
            {
                strClinicNum = Session["ClinicNum"].ToString();
            }

            //SectionName
            if (Session["SectionName"] != null)
            {
                strSectionName = Session["SectionName"].ToString();
            }

            hfPaperID.Value = "";
            //PaperID
            if (Session["PaperID"] != null)
            {
                strPaperID = Session["PaperID"].ToString();
                hfPaperID.Value = strPaperID;
            }
            else
            {
                SQLString mySQL = new SQLString();
                strPaperID = mySQL.getPaperIDFromCase(strCaseID, strClinicNum.ToString(), strSectionName);
                hfPaperID.Value = strPaperID;
            }
            //strPaperID = "wyt20060510150619";

            //Opener
            if (Request.QueryString["Opener"] != null)
            {
                hiddenOpener.Value = Request.QueryString["Opener"].ToString();
            }

            //QuestionMode
            if (Session["QuestionMode"] != null)
            {
                hiddenQuestionMode.Value = Session["QuestionMode"].ToString();
            }
            //hiddenQuestionMode.Value = "General";

            //PresentType
            if (Session["PresentType"] != null)
            {
                hiddenPresentType.Value = Session["PresentType"].ToString();
            }

            //Edit method
            if (Session["EditMode"] != null)
            {
                hiddenEditMode.Value = Session["EditMode"].ToString();
            }

            //ModifyType
            if (Session["ModifyType"] != null)
            {
                hiddenModifyType.Value = Session["ModifyType"].ToString();
            }

            //bModify
            if (Session["bModify"] != null)
            {
                bModify = Convert.ToBoolean(Session["bModify"]);
            }

            //PreOpener
            if (Session["PreOpener"] != null)
            {
                if (Request.QueryString["Opener"] != null)
                    if (Request.QueryString["Opener"].ToString() == "Paper_MainPage")
                        hiddenPreOpener.Value = Session["PreOpener"].ToString();
                    else
                        hiddenPreOpener.Value = "";
            }

            //QID
            if (!IsPostBack)
                Session["TextQuestionQID"] = null ;
            if (Request.QueryString["QID"] != null)
            {
                strQID = Request.QueryString["QID"].ToString();
                //把QID的題目內容寫入txtData中
                string strQuestion = DataReceiver.QuestionAnswer_Question_SELECT_Question(strQID);
                string strQuestionAnswer = DataReceiver.getTextQuestionContentByQID(strQID);
                if (this.IsPostBack == false)     
                {
                    txtQuestionData.Text = strQuestion;
                    txtAnswerData.Text = strQuestionAnswer.Split('$')[1];  
                }   
            }        
            else    
            {
                //若不用session把strQID暫存，則postback後會有第二個strQID產生，則特徵listbox會被自動刷新，讀取不到使用者所選的特徵值
                //朱君 2012/12/18
                if (Session["TextQuestionQID"] != null)
                {
                    strQID = Session["TextQuestionQID"].ToString();
                }
                else
                { 
                    //建立QID
                    DataReceiver myReceiver = new DataReceiver();
                    strQID = strUserID + "_Q_" + myReceiver.getNowTime();
                    Session["TextQuestionQID"] = strQID;
                }
            }


            //AID
            if (Request.QueryString["AID"] != null)
            {
                //暫存AID以防止PostBack後AID會改變         老詹 2013/09/09
                if (ViewState["TextQuestionAID"] == null)
                    strAID = Request.QueryString["AID"].ToString();
                else
                    strAID = ViewState["TextQuestionAID"].ToString();

                //把QID的題目答案寫入txtEdit中
                string strAnswer = DataReceiver.QuestionAnswer_Answer_SELECT_Answer(strQID, strAID);
                if (this.IsPostBack == false)
                {
                    txtAnswerData.Text = strAnswer;
                }
            }
            else
            {
                //建立AID
                DataReceiver myReceiver = new DataReceiver();
                System.Threading.Thread.Sleep(1000);
                strAID = strUserID + "_A_" + myReceiver.getNowTime();
            }

            //GroupID
            if (Request.QueryString["GroupID"] != null)
            {
                strGroupID = Request.QueryString["GroupID"].ToString();
                hfGroupID.Value = strGroupID;
                hfGroupSerialNum.Value = DataReceiver.getQuestionGroupSerialNumByQuestionGroupID(strGroupID).ToString();

                if (Session["GroupID"] != null)
                {
                    Session["GroupID"] = strGroupID;
                }
                else
                {
                    Session.Add("GroupID", strGroupID);
                }
            }
            else
            {
                SQLString sqlstring = new SQLString();
                strGroupID = sqlstring.GetQuestionGroupIDByQID(strQID);
                hfGroupID.Value = strGroupID;
                hfGroupSerialNum.Value = DataReceiver.getQuestionGroupSerialNumByQuestionGroupID(strGroupID).ToString();

                if (Session["GroupID"] != null)
                {
                    Session["GroupID"] = strGroupID;
                }
                else
                {
                    Session.Add("GroupID", strGroupID);
                }
            }


            //GroupDivisionID
            if (strGroupID != null)
            {
                if (strGroupID.Trim().Length > 0)
                {
                    DataReceiver myReceiver = new DataReceiver();
                    strGroupDivisionID = myReceiver.getGroupDivisionID(strGroupID);

                    if (Session["GroupDivisionID"] != null)
                    {
                        Session["GroupDivisionID"] = strGroupDivisionID;
                    }
                    else
                    {
                        Session.Add("GroupDivisionID", strGroupDivisionID);
                    }
                }
            }
        }

        //設定從課堂練習頁面進來時，此頁面的控制項
        private void SetPageStyle()
        {
            btnEditKeyword.Visible = false;
            btnNextQuestion.Visible = false;
            if (Request.QueryString["bModify"] != null)
                if (Request.QueryString["bModify"].ToString() == "False")
                    btSaveNew.Visible = false;
        }

        private void InititateLayoutTableForFeature()
        {
            LayoutTableForFeature.ID = "LayoutTableForFeature";
            //第一列
            LayoutTableForFeature.Width = Unit.Parse("100%");
            //LayoutTable.BorderWidth = Unit.Parse("3px");
            //LayoutTable.BorderColor = Color.Green;
            LayoutTableForFeature.ID = "LayoutTableForFeature_1";
            LayoutTableForFeature.Rows.Add(new TableRow());
            LayoutTableForFeature.Rows[LayoutTableForFeature.Rows.Count - 1].Cells.Add(new TableCell());
            Label FeatureTitle = new Label();
            FeatureTitle.Text = "SetFeature";

            LayoutTableForFeature.Rows[LayoutTableForFeature.Rows.Count - 1].Cells[0].Controls.Add(FeatureTitle);
            LayoutTableForFeature.Rows[LayoutTableForFeature.Rows.Count - 1].Cells[0].CssClass = "title";

            //第二列
            LayoutTableForFeature.Rows.Add(new TableRow());
            LayoutTableForFeature.Rows[LayoutTableForFeature.Rows.Count - 1].Cells.Add(new TableCell());
            Table containerForFeature = CommonQuestionUtility.get_HTMLTable(1, 2);

            LayoutTableForFeature.Rows[LayoutTableForFeature.Rows.Count - 1].Cells[0].Controls.Add(containerForFeature);

            FeatureItemControl qFeatureItemControlTable = new FeatureItemControl(this, 1, strGroupID, strQID); //FeatureItemControlTable  
            qFeatureItemControlTable.ID = "FeatureItemControlTable";
            this.LayoutTableForFeature.Rows.Add(new TableRow());
            this.LayoutTableForFeature.Rows[LayoutTableForFeature.Rows.Count - 1].Cells.Add(new TableCell());
            this.LayoutTableForFeature.Rows[LayoutTableForFeature.Rows.Count - 1].Cells[0].Controls.Add(qFeatureItemControlTable); 
 
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

        private DataTable GetKeyword(string strQID)
        {
            clsHintsDB sqldb = new clsHintsDB();
            string strSQL = "Select * From Paper_TextQuestionKeyword Where cQID = '" + strQID + "' ";
            DataTable dtTemp = sqldb.getDataSet(strSQL).Tables[0];
            return dtTemp;
        }

        private void btnSaveNextQuestion_ServerClick(object sender, EventArgs e)
        {
            DataReceiver myReceiver = new DataReceiver();

            //儲存題目
            clsTextQuestion myText = new clsTextQuestion();
            string strTextQContent = txtQuestionData.Text;
            string strTextAContent = txtAnswerData.Text;
            strTextQContent = strTextQContent.Replace("&lt;", "<");
            strTextQContent = strTextQContent.Replace("&gt;", ">");
            strTextAContent = strTextAContent.Replace("&lt;", "<");
            strTextAContent = strTextAContent.Replace("&gt;", ">");
            myText.saveQuestionAnswer(strQID, strAID, strTextQContent, strTextAContent, strUserID, strPaperID, strGroupDivisionID, strGroupID, hiddenQuestionMode.Value);

            //儲存問題難易度
            int iQuestionLevel = AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevelName_SELECT_QuestionLevel(ddlQuestionLevel.SelectedValue);
            AuthoringTool.QuestionEditLevel.QuestionLevel.INSERT_QuestionLevel(strQID, iQuestionLevel);

            //儲存問題的病徵
            AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevel_INSERT_QuestionSymptoms(strQID, ddlSymptoms.SelectedValue);

            //如果是Specific題目則需儲存一筆資料至Paper_Content
            if (hiddenQuestionMode.Value == "Specific")
            {
                int intContentSeq = myReceiver.getPaperContentMaxSeq(strPaperID) + 1;
                SQLString mySQL = new SQLString();
                mySQL.SaveToQuestionContent(strPaperID, strQID, "0", "2", hiddenQuestionMode.Value, intContentSeq.ToString());
            }

            //Redirect至此網頁
            //Response.Redirect("Paper_TextQuestionEditor.aspx?Opener=" + hiddenOpener.Value);

            //建立QID
            strQID = strUserID + "_" + myReceiver.getNowTime();

            //清除TextArea
            txtQuestionData.Text = "";
            txtAnswerData.Text = "";
            string strScript = "<script language='javascript'>\n";
            strScript += "Clear()\n";
            strScript += "</script>\n";
            Page.RegisterStartupScript("Clear", strScript);

            hrQuestion.Style.Add("display", "none");
            hrAnswer.Style.Add("display", "none");
            BulidInterrogation("Question");
            BulidInterrogation("Answer");
            
        }

        /// <summary>
        /// Save text question
        /// </summary>
        private void SaveQuestionText()
        {
            //儲存題目
            clsTextQuestion myText = new clsTextQuestion();
            string strQTextContent = txtQuestionData.Text;//get the question description from question textarea
            string strATextContent = txtAnswerData.Text;//get the correct answer from answer textarea
            strQTextContent = strQTextContent.Replace("&lt;", "<");
            strQTextContent = strQTextContent.Replace("&gt;", ">");
            strATextContent = strATextContent.Replace("&lt;", "<");
            strATextContent = strATextContent.Replace("&gt;", ">");

            ////use JS alert() in C#
            ScriptManager.RegisterStartupScript(
             this,
             typeof(Page),
             "Alert",
             "<script>alert('aa: " + strATextContent + "');</script>",
             false);
            ///////

            
            //store the question description and correct answer to the question to DB.
            myText.saveQuestionAnswer(strQID, strAID, strQTextContent, strATextContent, strUserID, strPaperID, strGroupDivisionID, strGroupID, hiddenQuestionMode.Value);

            //儲存問題難易度
            int iQuestionLevel = AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevelName_SELECT_QuestionLevel(ddlQuestionLevel.SelectedValue);
            AuthoringTool.QuestionEditLevel.QuestionLevel.INSERT_QuestionLevel(strQID, iQuestionLevel);

            //儲存問題的病徵
            AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevel_INSERT_QuestionSymptoms(strQID, ddlSymptoms.SelectedValue);

            //如果是Specific題目則需儲存一筆資料至Paper_Content
            if (hiddenQuestionMode.Value == "Specific")
            {
                DataReceiver myReceiver = new DataReceiver();
                int intContentSeq = myReceiver.getPaperContentMaxSeq(strPaperID) + 1;
                SQLString mySQL = new SQLString();
                mySQL.SaveToQuestionContent(strPaperID, strQID, "0", "2", hiddenQuestionMode.Value, intContentSeq.ToString());
            }

            //若從編輯考卷來，直接將問題新增至考卷裡 蕭凱 2014/3/25
            if (Session["IsFromClassExercise"] != null && Session["IsFromClassExercise"].ToString() == "True")
            {
                DataReceiver myReceiver = new DataReceiver();
                SQLString mySQL = new SQLString();
                //取得考卷題數
                string strSeq = Convert.ToString(myReceiver.getPaperContentMaxSeq(strPaperID) + 1);
                mySQL.SaveToQuestionContent(strPaperID, strQID, "0", "2", "General", strSeq);            
            }
            
        }

        private void btnSaveNext_ServerClick(object sender, EventArgs e)
        {
            
            //store the question description and correct answer to the question to DB.
            SaveQuestionText();
            
            //store selected features for future feature search use.
            ////////沒有存入FeatureForSelect table 中
            //朱君 2012/11/25 將使用者所選擇的特徵值存入暫存陣列中，並儲存於資料庫中。 
            clsFeaturevalue clsSaveFeature = new clsFeaturevalue();
            clsSaveFeature.update_FeatureItemIntoDataBase(clsSaveFeature.get_dtFeatureItem_Data(strGroupID, this));
            ////////沒有存入FeatureForSelect table 中
            
            

        
            //Redirect至下一個網頁
            string strSystemFunction = "";
            if (Session["SystemFunction"] != null)
            {
                strSystemFunction = Session["SystemFunction"].ToString();
            }

            //如果從課堂練習編輯考卷頁面新增題目功能來 直接關閉視窗
            if (Session["IsFromClassExercise"] != null && Session["IsFromClassExercise"].ToString() == "True" && Session["PreOpener"] != null && Session["PreOpener"] == "SelectPaperModeAddANewQuestion")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Refresh", "opener.document.getElementById('btnRefresh').click();window.close();", true);
                return;
            }
           
            switch (strSystemFunction)
            {
                case "EditPaper":
                    Response.Redirect("Paper_OtherQuestion.aspx?Opener=Paper_TextQuestionEditor");
                    break;
                case "EditQuestion":
                    if (Request.QueryString["QID"] != null)
                    {
                        Response.Redirect("Paper_QuestionViewNew.aspx?Opener=Paper_TextQuestionEditor");
                    }
                    else
                    {
                        //Response.Redirect("Paper_QuestionMain.aspx?Opener=Paper_TextQuestionEditor");
                        strGroupID = Session["GroupID"].ToString();
                        Response.Redirect("Paper_QuestionViewNew.aspx?GroupID=" + strGroupID);
                    }
                    break;
                case "PreviewPaper":
                    if (hiddenPreOpener.Value == "SelectPaperMode")
                        Response.Redirect("Paper_MainPage.aspx?Opener=SelectPaperMode&cCaseID=" + strCaseID + "&cSectionName=" + strSectionName + "&cPaperID=" + strPaperID);
                    else
                        Response.Redirect("Paper_MainPage.aspx?Opener=Paper_TextQuestionEditor");
                    break;
                default:
                    Response.Redirect("Paper_QuestionMain.aspx?Opener=Paper_TextQuestionEditor");
                    break;
            }
            
        }

        protected void btnEditKeyword_Click(object sender, EventArgs e)
        {
            SaveQuestionText();
            Response.Redirect("~/AuthoringTool/CaseEditor/Paper/Paper_EditQuestionKeyword.aspx?QID=" + strQID + "&GroupID=" + strGroupID + "&CaseID=" + usi.CaseID + "&ClinicNum=" + usi.ClinicNum + "&Section=" + usi.Section + "&AID=" + strAID + " ");
        }

        protected void btAddSynQuestion_Click(object sender, EventArgs e)
        {
            SaveQuestionText();
            Response.Redirect("../Interrogation/EditAddAskQuestionAnswer.aspx?Mode=AddQuestion&RecentItemID=" + strQID + "&EditPositation=QuestionDatabase&GroupID=" + strGroupID + "&AID=" + strAID + "");
        }

        protected void btAddSynAnswer_Click(object sender, EventArgs e)
        {
            SaveQuestionText();
            Response.Redirect("../Interrogation/EditAddAskQuestionAnswer.aspx?Mode=AddAnswer&RecentItemID=" + strQID + "&EditPositation=QuestionDatabase&GroupID=" + strGroupID + "&AID=" + strAID + "");
        }

        //建立同義問題或答案
        private void BulidInterrogation(string strType)
        {
            DataTable dtSyn = new DataTable();
            if (strType == "Question")
            {
                tbInterrogationClassQutstion.Controls.Clear();
                tbInterrogationClassQutstion.Style.Add("background-color", "#F8F0E7");
                dtSyn = clsInterrogationEnquiry.GetSynQuestion(strQID);
            }
            else if (strType == "Answer")
            {
                tbInterrogationClassAnswer.Controls.Clear();
                tbInterrogationClassAnswer.Style.Add("background-color", "#F8F0E7");
                if (strAID != "")
                {
                    dtSyn = clsInterrogationEnquiry.GetSynAnswer(strQID,strAID);
                }
                else
                {
                    dtSyn = clsInterrogationEnquiry.GetSynAnswer(strQID);
                }
            }

            if (dtSyn.Rows.Count > 0)
            {
                #region 同義問題或答案的標題
                TableRow trSynT = new TableRow();
                trSynT.Attributes.Add("style", "border:0px black solid;background:#7BC2FA; font-weight:bold;color:black");
                trSynT.Style.Add("CURSOR", "hand");
                trSynT.Style.Add("TEXT-ALIGN", "left");

                TableCell tcSynT = new TableCell();
                trSynT.Cells.Add(tcSynT);

                tcSynT.Text = "<IMG id='img" + strType + "_" + strQID + "' src='../../../BasicForm/Image/minus.gif'>&nbsp;Synonymous " + strType;

                TableRow trSynTable = new TableRow();
                trSynTable.ID = "tr" + strType + "" + strQID;
                trSynT.Attributes.Add("onclick", "ShowDetail('" + trSynTable.ID + "' , 'img" + strType + "_" + strQID + "')");

                if (strType == "Question")
                {
                    tbInterrogationClassQutstion.Rows.Add(trSynT);
                    tbInterrogationClassQutstion.Rows.Add(trSynTable);
                    hrQuestion.Style.Add("display", "");
                }
                else if (strType == "Answer")
                {
                    tbInterrogationClassAnswer.Rows.Add(trSynT);
                    tbInterrogationClassAnswer.Rows.Add(trSynTable);
                    hrAnswer.Style.Add("display", "");
                }


                TableCell tcSynTable = new TableCell();
                trSynTable.Cells.Add(tcSynTable);

                Table tbSynTable = new Table();
                tcSynTable.Controls.Add(tbSynTable);
                tbSynTable.Width = Unit.Percentage(100);
                tbSynTable.GridLines = GridLines.Both;

                #endregion

                #region 同義問題或答案內容
                DataTable dtSynonymousItem = clsInterrogationEnquiry.GetSynonymousItem(strType, "synonymous", strQID, strAID);
                if (dtSynonymousItem.Rows.Count > 0)
                {
                    string strDataName = "";//表示資料表欄位名稱
                    string strMode = "";//表示是同義問題或答案的模式
                    if (strType == "Question")
                    {
                        strDataName = "cQDataKind";
                        strMode = "SynQ";
                    }
                    else if (strType == "Answer")
                    {
                        strDataName = "cADataKind";
                        strMode = "SynA";
                    }

                    for (int iSyn = 0; iSyn < dtSynonymousItem.Rows.Count; iSyn++)
                    {
                        #region 同義項目的序號
                        TableRow trSynItemT = new TableRow();
                        trSynItemT.CssClass = "header1_tr_even_row";
                        tbSynTable.Rows.Add(trSynItemT);

                        TableCell tcSynItemT = new TableCell();
                        trSynItemT.Cells.Add(tcSynItemT);
                        tcSynItemT.Text = "Syn." + (iSyn + 1) + ":";
                        tcSynItemT.Width = Unit.Percentage(8);

                        string strSynItemValue = dtSynonymousItem.Rows[iSyn]["cItemValue"].ToString();
                        TableCell tcSynContext = new TableCell();
                        trSynItemT.Cells.Add(tcSynContext);
                        #endregion

                        #region 同義項目的內容
                        if (dtSynonymousItem.Rows[iSyn][strDataName].ToString().IndexOf("Multimedia") != -1)
                        {
                            string strItemSeq = dtSynonymousItem.Rows[iSyn]["sSeq"].ToString();
                            string filePath = dtSynonymousItem.Rows[iSyn]["cItemValue"].ToString();
                            string strFileName = filePath.Split('/')[2];//檔名(含副檔名)
                            string strTemp = strFileName.Split('.')[1];//副檔名
                            string strFileNameNoVice = strFileName.Split('.')[0]; //檔名
                            if (strTemp == "swf")
                            {
                                Label lbFlash = new Label();
                                lbFlash.Text = "<object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' id='FlashPlayer" + strItemSeq + strFileNameNoVice + "' width='320' height='240' " +
                                 " codebase='http://fpdownload.macromedia.com/get/flashplayer/current/swflash.cab'> " +
                                 " <param name='pluginspage' value='http://www.macromedia.com/go/getflashplayer' /> " +
                                 " <param name='movie' value='../../MultiMediaDB/Upload/Image/" + strFileName + "' /> " +
                                 " <param name='quality' value='high' /> " +
                                 " <param name='bgcolor' value='#869ca7' /> " +
                                 " <param name='allowScriptAccess' value='always' /> " +
                                 " <param name='Play' value='false' /> " +
                                  "</object>" +
                                  "<input id='btnPlay' type='button' value='Play' onclick=\"PlayFlash('" + strItemSeq + "','" + strFileNameNoVice + "')\"' style='width:100px' />";
                                tcSynContext.Controls.Add(lbFlash);
                            }
                            else if (strTemp == "mp3" || strTemp == "wav" || strTemp == "wmv" || strTemp == "avi")
                            {
                                Label lbMedia = new Label();
                                lbMedia.Text = " <object id='MediaPlayer' height='240' width='320' classid='CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6'> " +
                                               " <param name='AutoStart' value='false' />" +
                                               " <param name='uiMode' value='Full' />" +
                                               " <param name='enabled' value='true' />" +
                                               " <param name='URL' value='../../MultiMediaDB/Upload/Image/" + strFileName + "'>" +
                                               " </object> ";
                                tcSynContext.Controls.Add(lbMedia);
                            }
                            else if (strTemp == "jpg" || strTemp == "bmp" || strTemp == "gif" || strTemp == "png")
                            {
                                System.Web.UI.WebControls.Image imgPic = new System.Web.UI.WebControls.Image();
                                imgPic.ImageUrl = "../../MultiMediaDB/Upload/Image/" + strFileName + "";
                                imgPic.Width = Unit.Pixel(320);
                                imgPic.Height = Unit.Pixel(240);
                                tcSynContext.Controls.Add(imgPic);
                            }
                        }
                        else
                        {
                            tcSynContext.Text = strSynItemValue;
                        }

                        tcSynContext.Width = Unit.Percentage(82);
                        #endregion

                        #region 同義項目的編輯
                        TableCell tcSynItemModify = new TableCell();
                        trSynItemT.Cells.Add(tcSynItemModify);
                        tcSynItemModify.Style.Add("text-align", "center");
                        tcSynItemModify.Width = Unit.Percentage(5);

                        Button btModifySynItem = new Button();
                        btModifySynItem.Click += new EventHandler(btModifySynItem_Click);
                        btModifySynItem.CssClass = "button_Edit";
                        btModifySynItem.CommandArgument = "btnModify$" + strMode + "$" + strQID + "$" + dtSynonymousItem.Rows[iSyn][strDataName].ToString() + "$" + strAID;
                        tcSynItemModify.Controls.Add(btModifySynItem);


                        TableCell tcSynItemDelete = new TableCell();
                        trSynItemT.Cells.Add(tcSynItemDelete);
                        tcSynItemDelete.Style.Add("text-align", "center");
                        tcSynItemDelete.Width = Unit.Percentage(5);

                        Button btnDeleteSyn = new Button();
                        btnDeleteSyn.ID = "btnDelete|" + dtSynonymousItem.Rows[iSyn]["sSeq"].ToString() + "|" + strQID;
                        btnDeleteSyn.CssClass = "button_Delete";
                        btnDeleteSyn.Click += new EventHandler(btnDeleteSyn_Click);
                        btnDeleteSyn.CommandArgument = "btnDelete$" + strMode + "$" + strQID + "$" + dtSynonymousItem.Rows[iSyn][strDataName].ToString() + "$" + strAID;
                        tcSynItemDelete.Controls.Add(btnDeleteSyn);
                        #endregion
                    }
                }
                #endregion
            }
        }

        //刪除項目
        void btnDeleteSyn_Click(object sender, EventArgs e)
        {
            string strTempItem = "";
            string strTempDataKind = "";
            string strTempMode = "";
            string strTempTarget = "";
            string strTempAID = "";
            Button btnDelete = new Button();
            btnDelete = (Button)(sender);

            strTempMode = btnDelete.CommandArgument.Split('$')[0];//mode
            strTempTarget = btnDelete.CommandArgument.Split('$')[1];//Syn Question or syn Answer 
            strTempItem = btnDelete.CommandArgument.Split('$')[2];//ItemID
            strTempDataKind = btnDelete.CommandArgument.Split('$')[3];//Datakind
            strTempAID = btnDelete.CommandArgument.Split('$')[4];//AID

            clsInterrogationEnquiry.DeleteSynonymousItem(strTempTarget, strTempItem, strTempDataKind, strTempAID);

            hrQuestion.Style.Add("display", "none");
            hrAnswer.Style.Add("display", "none");
            BulidInterrogation("Question");
            BulidInterrogation("Answer");
        }

        //修改項目
        void btModifySynItem_Click(object sender, EventArgs e)
        {
            string strArgument = "";
            string strTempItem = "";
            string strTempDataKind = "";
            string strTempMode = "";
            string strTempTarget = "";
            string strTempAID = "";
            Button btnModify = new Button();
            btnModify = (Button)(sender);

            strArgument = btnModify.CommandArgument.ToString();
            strTempMode = strArgument.Split('$')[0];
            strTempTarget = strArgument.Split('$')[1];
            strTempItem = strArgument.Split('$')[2];
            strTempDataKind = strArgument.Split('$')[3];
            strTempAID = strArgument.Split('$')[4];

            Response.Redirect("../Interrogation/EditAddAskQuestionAnswer.aspx?Mode=Modify" + strTempTarget + "&RecentItemID=" + strTempItem + "&DataKind=" + strTempDataKind + "&EditPositation=QuestionDatabase&GroupID=" + strGroupID + "&AID=" + strTempAID + "");
        }

        protected void btSaveNew_Click(object sender, EventArgs e)
        {
            //建立QID
            DataReceiver myReceiver = new DataReceiver();
            strQID = strUserID + "_Q_" + myReceiver.getNowTime();
            System.Threading.Thread.Sleep(1000);
            strAID = strUserID + "_A_" + myReceiver.getNowTime();

            //儲存題目
            clsTextQuestion myText = new clsTextQuestion();
            string strQTextContent = txtQuestionData.Text;
            string strATextContent = txtAnswerData.Text;
            strQTextContent = strQTextContent.Replace("&lt;", "<");
            strQTextContent = strQTextContent.Replace("&gt;", ">");
            strATextContent = strATextContent.Replace("&lt;", "<");
            strATextContent = strATextContent.Replace("&gt;", ">");

            myText.saveQuestionAnswer(strQID, strAID, strQTextContent, strATextContent, strUserID, strPaperID, strGroupDivisionID, strGroupID, hiddenQuestionMode.Value);

            //儲存問題難易度
            int iQuestionLevel = AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevelName_SELECT_QuestionLevel(ddlQuestionLevel.SelectedValue);
            AuthoringTool.QuestionEditLevel.QuestionLevel.INSERT_QuestionLevel(strQID, iQuestionLevel);

            //儲存問題的病徵
            AuthoringTool.QuestionEditLevel.QuestionLevel.QuestionLevel_INSERT_QuestionSymptoms(strQID, ddlSymptoms.SelectedValue);

            //如果是Specific題目則需儲存一筆資料至Paper_Content
            if (hiddenQuestionMode.Value == "Specific")
            {
                int intContentSeq = myReceiver.getPaperContentMaxSeq(strPaperID) + 1;
                SQLString mySQL = new SQLString();
                mySQL.SaveToQuestionContent(strPaperID, strQID, "0", "2", hiddenQuestionMode.Value, intContentSeq.ToString());
            }

            //若從編輯考卷來，直接將問題新增至考卷裡 蕭凱 2014/3/25
            if (hiddenPreOpener.Value == "SelectPaperMode" && hiddenQuestionMode.Value != "Specific")
            {
                SQLString mySQL = new SQLString();
                //取得考卷題數
                string strSeq = Convert.ToString(myReceiver.getPaperContentMaxSeq(strPaperID) + 1);
                mySQL.SaveToQuestionContent(strPaperID, strQID, "0", "2", "General", strSeq);
                Session.Remove("PreOpener");
            }

            //Redirect至下一個網頁
            string strSystemFunction = "";
            if (Session["SystemFunction"] != null)
            {
                strSystemFunction = Session["SystemFunction"].ToString();
            }

            switch (strSystemFunction)
            {
                case "EditPaper":
                    Response.Redirect("Paper_OtherQuestion.aspx?Opener=Paper_TextQuestionEditor");
                    break;
                case "EditQuestion":
                    if (Request.QueryString["QID"] != null)
                    {
                        Response.Redirect("Paper_QuestionViewNew.aspx?Opener=Paper_TextQuestionEditor");
                    }
                    else
                    {
                        Response.Redirect("Paper_QuestionMain.aspx?Opener=Paper_TextQuestionEditor");
                    }
                    break;
                case "PreviewPaper":
                    if (hiddenPreOpener.Value == "SelectPaperMode")
                        Response.Redirect("Paper_MainPage.aspx?Opener=SelectPaperMode&cCaseID=" + strCaseID + "&cSectionName=" + strSectionName + "&cPaperID=" + strPaperID);
                    else
                        Response.Redirect("Paper_MainPage.aspx?Opener=Paper_TextQuestionEditor");
                    break;
                default:
                    Response.Redirect("Paper_QuestionMain.aspx?Opener=Paper_TextQuestionEditor");
                    break;
            }
        }

        //取得病徵項目
        private DataTable DiseaseSymptomsTree_SELECT()
        {
            clsHintsDB HintsDB = new clsHintsDB();
            DataTable dtDiseaseSymptomsTree = new DataTable();
            string strSQL_DiseaseSymptomsTree = "SELECT  DISTINCT cNodeName FROM DiseaseSymptomsTree WHERE cParentID != 'Diseaseroot' ORDER BY cNodeName ASC";
            dtDiseaseSymptomsTree = HintsDB.getDataSet(strSQL_DiseaseSymptomsTree).Tables[0];
            return dtDiseaseSymptomsTree;
        }

        //設定答案下拉式選單內容 老詹 2013/8/28
        protected void setupddlAnswerOptions(string strQID)
        {
            ddlAnswerOptions.Items.Clear();

            string sqlStatement = "SELECT * FROM QuestionAnswer_Answer WHERE cQID = '" + strQID + "' ";
            DataTable dt = sqldb.getDataSet(sqlStatement).Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //將資料庫中有<P></P>符號的答案將<P></P>去除
                string temp = dt.Rows[i]["cAnswer"].ToString().Replace("<P>", "");
                string strddlAnswer = temp.Replace("</P>", "").Replace("<BR>", "");
                ddlAnswerOptions.Items.Add(new ListItem(strddlAnswer, dt.Rows[i]["cAID"].ToString()));
            }        
        }
        protected void ddlAnswerOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            strAID = ddlAnswerOptions.SelectedValue;
            ViewState["TextQuestionAID"] = strAID;
            string strAnswer = DataReceiver.QuestionAnswer_Answer_SELECT_Answer(strQID, strAID);
            txtAnswerData.Text = strAnswer;
        }
}
}
