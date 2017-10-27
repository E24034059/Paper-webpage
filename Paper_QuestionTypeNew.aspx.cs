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

namespace PaperSystem
{
    /// <summary>
    /// Paper_QuestionType 的摘要描述。
    /// </summary>
    public partial class Paper_QuestionTypeNew : AuthoringTool_BasicForm_BasicForm
    {
        string strUserID, strCaseID, strDivisionID, strClinicNum, strSectionName, strPaperID, strPresentType;
        protected string strGroupID = "";
        protected string strGroupDivisionID = "";
        protected string strModify = "";


        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.Initiate();

            //接收參數
            this.getParameter();

            if (!IsPostBack)
            {
                if (Request.QueryString["CaseID"] != null)
                    Response.Redirect("Paper_EmulationQuestion.aspx?Opener=Paper_QuestionTypeNew&GroupID=" + strGroupID);
                else if (Session["fromVRSimulator"] != null && Session["fromVRSimulator"].ToString().Contains("vr"))
                {
                    Session["QuestionType"] = "5";
                    Response.Redirect("./QuestionGroupTree/QGroupTreeNew.aspx?Opener=Paper_QuestionType&Career=");
                }
            }
            
            //從ORCS課堂練習來，隱藏對話題選項
            //if (hiddenPreOpener.Value == "SelectPaperMode" || hiddenPreOpener.Value == "SelectPaperModeAddANewQuestion")
            //    trConversation.Visible = false;

            //下一步的機制
            btnNext.ServerClick += new EventHandler(btnNext_ServerClick);
        }

        private void getParameter()
        {
            //UserID
            if (Session["UserID"] != null)
            {
                strUserID = Session["UserID"].ToString();
            }
            else
            {
                strUserID = usi.UserID;
            }

            //CaseID kyhCase200505301448128593750
            if (Session["CaseID"] != null)
            {
                strCaseID = Session["CaseID"].ToString();
                hiddenCaseID.Value = Session["CaseID"].ToString();
            }

            //Division 9801
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
                hiddenSectionName.Value = Session["SectionName"].ToString();
            }

            //PaperID
            if (Session["PaperID"] != null)
            {
                strPaperID = Session["PaperID"].ToString();
                hiddenPaperID.Value = Session["PaperID"].ToString();
            }

            //Opener
            if (Request.QueryString["Opener"] != null)
            {
                

                string strCareer = Request.QueryString["Career"];
                if (Session["bDisplayQuestionList"] != null)
                {
                    string strDisplayBQL = Session["bDisplayQuestionList"].ToString();  //加入是否顯示BQL參數  老詹 2014/11/12
                    hiddenOpener.Value = Request.QueryString["Opener"].ToString() + ".aspx?Career=" + strCareer + "&bDisplayBQL=" + strDisplayBQL;
                }
                else
                {
                    hiddenOpener.Value = Request.QueryString["Opener"].ToString();
                }
                

                if (Request.QueryString["Opener"].ToString() == "Paper_QuestionViewNew_VoiceInquiry")
                    rbConversation.Checked = true;
               
                
                //Ben check 
                //昭成加入由QGroupTreeNew.aspx點下Submit後 可連接到此頁面以選擇要新增的問題類型

                /*
                //use JS alert() in C#
                ScriptManager.RegisterStartupScript(
                 this,
                 typeof(Page),
                 "Alert",
                 "<script>alert('" + hiddenOpener.Value + "');</script>",
                 false);
                /////
                 */
                
            }

            //QuestionMode
            if (Session["QuestionMode"] != null)
            {
                hiddenQuestionMode.Value = Session["QuestionMode"].ToString();
            }

            //PresentType
            if (Session["PresentType"] != null)
            {
                hiddenPresentType.Value = Session["PresentType"].ToString();
                strPresentType = Session["PresentType"].ToString();
            }

            //Edit method
            if (Session["EditMode"] != null)
            {
                hiddenEditMode.Value = Session["EditMode"].ToString();
            }

            //GroupID
            if (Request.QueryString["GroupID"] != null)
            {
                strGroupID = Request.QueryString["GroupID"].ToString();
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

            //bModify
            if (Request.QueryString["bModify"] != null)
            {
                if (Session["bModify"] != null)
                {
                    Session["bModify"] = Request.QueryString["bModify"].ToString();
                }
                else
                {
                    Session.Add("bModify", Request.QueryString["bModify"].ToString());
                }
                strModify = Request.QueryString["bModify"].ToString();
            }

            //PreOpener
            if (Session["PreOpener"] != null)
            {
                if (Request.QueryString["Opener"] != null)
                {
                    if (Request.QueryString["Opener"].ToString() == "Paper_QuestionViewNew")
                    {
                        hiddenPreOpener.Value = Session["PreOpener"].ToString();
                    }
                    else
                        hiddenPreOpener.Value = Session["PreOpener"].ToString();
                    if (Request.QueryString["Opener"].ToString() == "Paper_QuestionViewNew_VoiceInquiry")
                        rbConversation.Checked = true;
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

        private void btnNext_ServerClick(object sender, EventArgs e)
        {
            string strQuestionType = "";

            if (rb1.Checked == true)
            {
                //選擇題
                strQuestionType = "1";
            }
            else if (rb2.Checked == true)
            {
                //問答題
                strQuestionType = "2";
            }
            else if (rb3.Checked == true)
            {
                strQuestionType = "3";
            }
            else if (rbConversation.Checked == true)
            {
                //對話題
                strQuestionType = "4";
            }
            else if (rbSituation.Checked == true)
            {
                //情境題
                strQuestionType = "5";
            }
            else if (rbSelectionWithKeyWords.Checked == true)
            {
                //選擇題含關鍵字
                strQuestionType = "6";
            }
            else if (rbProgram.Checked == true)
            {
                //程式題
                strQuestionType = "7";
            }


            //把QuestionType存入Session
            if (Session["QuestionType"] != null)
            {
                Session["QuestionType"] = strQuestionType;
            }
            else
            {
                Session.Add("QuestionType", strQuestionType);
            }

            //If strModify == "False", redirect to question editint page according to strQuestionType.
            if (hiddenQuestionMode.Value == "Specific" || strModify == "False")
            {
                if (strQuestionType == "1")
                {
                    //選擇題
                    if (hiddenPreOpener.Value == "SelectPaperMode") //從ORCS課堂練習來
                        Response.Redirect("../Paper/CommonQuestionEdit/Page/ShowQuestion.aspx?Opener=Paper_MainPage&GroupID=" + strGroupID + "&bModify=False");
                    else
                        Response.Redirect("../Paper/CommonQuestionEdit/Page/ShowQuestion.aspx?Opener=Paper_QuestionTypeNew&GroupID=" + strGroupID);
                }
                else if (strQuestionType == "2")
                {
                    //問答題
                    if (hiddenPreOpener.Value == "SelectPaperMode") //從ORCS課堂練習來
                        Response.Redirect("Paper_TextQuestionEditorNew.aspx?Opener=Paper_MainPage&GroupID=" + strGroupID + "&bModify=False");
                    else
                        Response.Redirect("Paper_TextQuestionEditorNew.aspx?Opener=Paper_QuestionTypeNew&GroupID=" + strGroupID);
                }
                else if (strQuestionType == "3")
                {
                    //圖形題
                    if (hiddenPreOpener.Value == "SelectPaperMode") //從ORCS課堂練習來
                        Response.Redirect("Paper_SimulatorQE_tree.aspx?Opener=Paper_MainPage&bModify=False");
                    else
                        Response.Redirect("Paper_SimulatorQE_tree.aspx?Opener=Paper_QuestionTypeNew&GroupID=" + strGroupID);
                }
                else if (strQuestionType == "4")
                {
                    string strCareer = Request.QueryString["Career"];
                    //對話題
                    if (hiddenPreOpener.Value == "SelectPaperMode") //從ORCS課堂練習來
                        Response.Redirect("Paper_ConversationQuestionEditor.aspx?Opener=Paper_MainPage&GroupID=" + strGroupID +"&Career=" + strCareer + "&bModify=False");
                    else
                        Response.Redirect("Paper_ConversationQuestionEditor.aspx?Opener=Paper_QuestionTypeNew&GroupID=" + strGroupID + "&Career=" + strCareer + "&bModify=False");
                }
                else if (strQuestionType == "5")
                {
                    //情境題
                    if (hiddenPreOpener.Value == "SelectPaperMode") //從ORCS課堂練習來
                        Response.Redirect("Paper_EmulationQuestion.aspx?Opener=Paper_MainPage&GroupID=" + strGroupID + "&bModify=False");
                    else
                        Response.Redirect("Paper_EmulationQuestion.aspx?Opener=Paper_QuestionTypeNew&GroupID=" + strGroupID);
                }
                else if (strQuestionType == "6")
                {
                    //對話題
                    if (hiddenPreOpener.Value == "SelectPaperMode") //從ORCS課堂練習來
                        Response.Redirect("../Paper/CommonQuestionEdit/Page/showquestionWithKeyWords.aspx?Opener=Paper_MainPage&GroupID=" + strGroupID + "&bModify=False");
                    else
                        Response.Redirect("../Paper/CommonQuestionEdit/Page/showquestionWithKeyWords.aspx?Opener=Paper_QuestionTypeNew&GroupID=" + strGroupID);
                }

                else if (strQuestionType == "7")
                {
                    //程式題
                    // if (hiddenPreOpener.Value == "SelectPaperMode") //從ORCS課堂練習來
                    //     Response.Redirect("Paper_ProgramQuestionEditorNew.aspx?Opener=Paper_MainPage&GroupID=" + strGroupID + "&bModify=False");
                    // else
                    Response.Redirect("Paper_ProgramQuestionEditorNew.aspx?Opener=Paper_QuestionTypeNew&GroupID=" + strGroupID);
                }

            }
            else
            {
                Response.Redirect("./QuestionGroupTree/QGroupTreeNew.aspx?Opener=Paper_QuestionType&Career=");
            }
        }
    }
}