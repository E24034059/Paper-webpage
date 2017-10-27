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
using System.Net;
using AllSystemDB;

namespace PaperSystem
{
    /// <summary>
    /// Paper_QuestionMain 的摘要描述。
    /// </summary>
    public partial class Paper_QuestionMainNew : AuthoringTool_BasicForm_BasicForm
    {
        protected string strUserID, strCaseID, strDivisionID, strClinicNum, strSectionName;


        protected void Page_Load(object sender, System.EventArgs e)
        {
            
            this.Initiate();
            //清除Session值
            if (Session["IsFromClassExercise"] != null)
                Session.Remove("IsFromClassExercise");
            if (Session["PreOpener"] != null)
                Session.Remove("PreOpener");

            //取得參數  
            this.getParameter();

            //加入Next&Back的事件
            btnNext.ServerClick += new EventHandler(btnNext_ServerClick);
            btnBack.ServerClick += new EventHandler(btnBack_ServerClick);
        }

        private void getParameter()
        {
            //SystemFunction
            if (Session["SystemFunction"] != null)
            {
                Session["SystemFunction"] = "EditQuestion";
            }
            else
            {
                Session.Add("SystemFunction", "EditQuestion");
            }

            //UserID
            if (Request.QueryString["UserID"] != null)
            {
                strUserID = Request.QueryString["UserID"].ToString();
                if (Session["UserID"] != null)
                {
                    Session["UserID"] = strUserID;
                }
                else
                {
                    Session.Add("UserID", strUserID);
                }
            }
            else
            {
                if (Session["UserID"] != null)
                {
                    strUserID = Session["UserID"].ToString();
                }
                else
                {
                    strUserID = "swakevin";
                    Session.Add("UserID", strUserID);
                }
            }

            //CaseID kyhCase200505301448128593750
            if (Request.QueryString["CaseID"] != null)
            {
                strCaseID = Request.QueryString["CaseID"].ToString();
                if (Session["CaseID"] != null)
                {
                    Session["CaseID"] = strCaseID;
                }
                else
                {
                    Session.Add("CaseID", strCaseID);
                }
            }
            else
            {
                if (Session["CaseID"] != null)
                {
                    strCaseID = Session["CaseID"].ToString();
                }
                else
                {
                    strCaseID = "gait001Case200401061006167084912";
                    Session.Add("CaseID", strCaseID);
                }
            }

            //ClinicNum
            if (Request.QueryString["ClinicNum"] != null)
            {
                strClinicNum = Request.QueryString["ClinicNum"].ToString();
                if (Session["ClinicNum"] != null)
                {
                    Session["ClinicNum"] = strClinicNum;
                }
                else
                {
                    Session.Add("ClinicNum", strClinicNum);
                }
            }
            else
            {
                if (Session["ClinicNum"] != null)
                {
                    strClinicNum = Session["ClinicNum"].ToString();
                }
                else
                {
                    strClinicNum = "1";
                    Session.Add("ClinicNum", strClinicNum);
                }
            }

            //SectionName 測驗
            if (Request.QueryString["SectionName"] != null)
            {
                strSectionName = Request.QueryString["SectionName"].ToString();
                if (Session["SectionName"] != null)
                {
                    Session["SectionName"] = strSectionName;
                }
                else
                {
                    Session.Add("SectionName", strSectionName);
                }
            }
            else
            {
                if (Session["SectionName"] != null)
                {
                    strSectionName = Session["SectionName"].ToString();
                }
                else
                {
                    strSectionName = "Examination";
                    Session.Add("SectionName", strSectionName);
                }
            }

            //Opener
            if (Session["Opener"] != null)
            {
                hiddenOpener.Value = Session["Opener"].ToString();
            }

            //Setup opener
            if (Session["Opener"] != null)
            {
                Session["Opener"] = "Paper_QuestionMain";
            }
            else
            {
                Session.Add("Opener", "Paper_QuestionMain");
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
            
            //取得使用者所選取的功能
            string strQuestionFunction = "";
            if (rb1.Checked == true)
            {
                
               //新增題目
                strQuestionFunction = "New";

                //設定bModify
                if (Session["bModify"] != null)
                {
                   Session["bModify"] = false;
                }
               else
                {
                   Session.Add("bModify", false);
                }
            }
            else
            {
                //修改或是刪除題目
                strQuestionFunction = "Modify";

                //設定bModify
                if (Session["bModify"] != null)
                {
                    Session["bModify"] = true;
                }
                else
                {
                    Session.Add("bModify", true);
                }
            }

            //加入Session QuestionFunction
            if (Session["QuestionFunction)"] != null)
            {
                Session["QuestionFunction"] = strQuestionFunction;
            }
            else
            {
                Session.Add("QuestionFunction", strQuestionFunction);
            }

            //加入Session ModifyType
            if (Session["ModifyType)"] != null)
            {
                Session["ModifyType"] = "Question";
            }
            else
            {
                Session.Add("ModifyType", "Question");
            }

            //加入QuestionMode
            if (Session["QuestionMode"] != null)
            {
                Session["QuestionMode"] = "General";
            }
            else
            {
                Session.Add("QuestionMode", "General");
            }

            if (rb1.Checked == true)
            {
                
                // 傳遞Career參數，判斷職業領域  老詹 2014/08/17
                string strCareerID = Request.QueryString["Career"];
                Response.Redirect("./QuestionGroupTree/QGroupTreeNew.aspx?Career=" + strCareerID);
            }
            else if (rb2.Checked == true)
            {
                Response.Redirect("/Hints/AuthoringTool/DiseaseSymptoms/ProblemTypeTree.aspx");
            }
            else if (rb4.Checked == true)
            {
                Response.Redirect("./Feature/VisionOfQuesion.aspx");
            }
            else if (rb5.Checked == true)
            {
                Response.Redirect("./Feature/Paper_Featurevalues.aspx");
            }
            else if(rb6.Checked == true)
            {
                //下列仿照ORCS出考卷設定
                //產生考卷所需的CaseID、PaperID和考卷名稱，要給HINTS的Paper_CaseDivisionSection資料表用
                string strNowTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                string strCaseID = usi.UserID + "Case" + strNowTime;   //CaseID
                string strSectionName = "考卷(" + strNowTime + ")";
                string strPaperID = usi.UserID + strNowTime;           //PaperID
                //取得ServerIP
                string ServerIP = (new IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address)).ToString();
                //開啟選擇題目出處
                string strURL = "";
                clsAllSystemDB allSystemDB = new clsAllSystemDB();
                //---------仿照ORCS出考卷作法---------------
                //將取得的CaseID、PaperID和考卷名稱插入Paper_CaseDivisionSection資料表
                string strSQL = " INSERT INTO Paper_CaseDivisionSection (cCaseID , sClinicNum , cSectionName , cPaperID) " +
                    " VALUES ('" + strCaseID + "' , '1'  ,'" + strSectionName + "' , '" + strPaperID + "') ";
                allSystemDB.ExecuteNonQuery(strSQL);
                //新增到資料庫
                strSQL = " INSERT Paper_Header (cPaperID , cPaperName , cTitle , cObjective , cEditMethod ,  cGenerationMethod , cPresentMethod , sTestDuration , cPresentType) " +
                    " VALUES('" + strPaperID + "' , '" + strSectionName + "' , '" + strSectionName + "' , 'General' , 'Author' , 'Edit' , 'All' , '0' , '10') ";
                allSystemDB.ExecuteNonQuery(strSQL);
                //------------------------------------------
                strURL = "http://" + ServerIP + "/HINTS/AuthoringTool/CaseEditor/Paper/Paper_MainPage.aspx?Opener=SelectPaperMode&cCaseID=" + strCaseID + "&cSectionName=" + Server.UrlEncode(strSectionName) + "&cPaperID=" + strPaperID  + "&UserID=" + usi.UserID + "&cComeFromActivityName=EditPaper";
                ClientScript.RegisterStartupScript(this.GetType(), "myScript", "<script language=JavaScript>window.open('" + strURL + "','WindowOpen','fullscreen=yes, scrollbars=yes');</script>");
                
            }
            else
            {
                Response.Redirect("/Hints/Flow control/navigator_case.aspx");
            }
        }

        private void btnBack_ServerClick(object sender, EventArgs e)
        {
            if (Request.QueryString["Career"].ToString() != "")
            {
                Response.Redirect("/Hints/AuthoringTool/CaseEditor/Interrogation/ConversationEnquiry.aspx?Openner=MLAS&CaseID=" + usi.CaseID + "&DivisionName=" + Session["DivisinName"].ToString() + "&CaseName=" + Session["CaseName"].ToString() + "&SectionName=" + usi.Section + "&SectionKind=108");
            }
            else
                Response.Redirect("/Hints/Flow control/navigator_case.aspx");
        }
    }
}
