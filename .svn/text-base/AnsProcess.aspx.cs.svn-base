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
    /// AnsProcess 的摘要描述。
    /// </summary>
    public partial class AnsProcess : AuthoringTool_BasicForm_BasicForm
    {
        protected string settype = "";
        protected string cAnsProcessID = "";
        protected string cAccessDBO = "";
        protected int cCorrectTypeValue = 1;
        protected int cWrongTypeValue = 1;
        protected int cRetryValue = 1;
        protected bool cAnsSue = false;
        protected string cCallType = "";
        protected bool bReason = false;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.Initiate();

            //接收參數
            this.getParameter();

            SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);

            if (!this.IsPostBack)
            {
                SetDescription(cCallType);

                string strSQL_AnsProcess = "SELECT * FROM AnsProcess WHERE cAnsProcessID = '" + cAnsProcessID + "' AND cAccessDBO = '" + cAccessDBO + "'";
                DataTable dtAnsProcess = new DataTable();
                dtAnsProcess = sqldb.getDataSet(strSQL_AnsProcess).Tables[0];
                if (dtAnsProcess.Rows.Count > 0)
                {
                    settype = "retry";
                }
                else
                {
                    settype = "insert_retry";
                }

                if (settype == "insert_retry")
                {
                    rbCorrectState1.Checked = true;
                    rbState1.Checked = true;
                    ddlCountState2.SelectedValue = "1";
                    ddlCountState3.SelectedValue = "1";
                    ddlCountState2.Enabled = false;
                    ddlCountState3.Enabled = false;
                    rbCorrectAnsYes2.Enabled = false;
                    rbCorrectAnsNo2.Enabled = false;
                    rbCorrectAnsNo2.Checked = true;
                    rbCorrectAnsYes3.Enabled = false;
                    rbCorrectAnsNo3.Enabled = false;
                    rbCorrectAnsNo3.Checked = true;
                    this.ViewState["cCorrectTypeValue"] = 1;
                    this.ViewState["cWrongTypeValue"] = 1;
                    this.ViewState["cRetryValue"] = 1;
                    this.ViewState["cAnsSue"] = false;
                    this.ViewState["bReason"] = false;
                }
                else
                {
                    if (dtAnsProcess.Rows.Count > 0)
                    {
                        cCorrectTypeValue = int.Parse(dtAnsProcess.Rows[0]["cCorrectTypeValue"].ToString());
                        cWrongTypeValue = int.Parse(dtAnsProcess.Rows[0]["cWrongTypeValue"].ToString());
                        cRetryValue = int.Parse(dtAnsProcess.Rows[0]["cRetryValue"].ToString());
                        cAnsSue = Convert.ToBoolean(dtAnsProcess.Rows[0]["cAnsSue"].ToString());
                        bReason = Convert.ToBoolean(dtAnsProcess.Rows[0]["cAnsSue"].ToString());
                        this.ViewState["cCorrectTypeValue"] = cCorrectTypeValue;
                        this.ViewState["cWrongTypeValue"] = cWrongTypeValue;
                        this.ViewState["cRetryValue"] = cRetryValue;
                        this.ViewState["cAnsSue"] = cAnsSue;
                        this.ViewState["bReason"] = bReason;

                        #region 作答正確處理
                        switch (cCorrectTypeValue)
                        {
                            case 1:
                                rbCorrectState1.Checked = true;
                                break;

                            case 2:
                                rbCorrectState2.Checked = true;
                                break;

                            case 3:
                                rbCorrectState3.Checked = true;
                                break;

                            default:
                                cCorrectTypeValue = 1;
                                rbCorrectState1.Checked = true;

                                break;
                        }
                        #endregion

                        #region 作答錯誤處理
                        switch (cWrongTypeValue)
                        {
                            case 1:
                                rbState1.Checked = true;
                                ddlCountState2.SelectedValue = "1";
                                ddlCountState3.SelectedValue = "1";
                                ddlCountState2.Enabled = false;
                                ddlCountState3.Enabled = false;
                                rbCorrectAnsNo2.Checked = true;
                                rbCorrectAnsNo3.Checked = true;
                                rbCorrectAnsNo2.Enabled = false;
                                rbCorrectAnsNo3.Enabled = false;
                                rbCorrectAnsYes2.Enabled = false;
                                rbCorrectAnsYes3.Enabled = false;
                                break;

                            case 2:
                                rbState2.Checked = true;
                                ddlCountState2.SelectedValue = cRetryValue.ToString();
                                ddlCountState3.SelectedValue = "1";
                                ddlCountState2.Enabled = true;
                                ddlCountState3.Enabled = false;
                                if (cAnsSue == false)
                                {
                                    rbCorrectAnsNo2.Checked = true;
                                    if (cCallType == "Paper")
                                    {
                                        this.ViewState["cAnsSue"] = false;
                                    }
                                    else if (cCallType == "Laboratory")
                                    {
                                        this.ViewState["bReason"] = false;
                                    }
                                }
                                else
                                {
                                    rbCorrectAnsYes2.Checked = true;
                                    if (cCallType == "Paper")
                                    {
                                        this.ViewState["cAnsSue"] = true;
                                    }
                                    else if (cCallType == "Laboratory")
                                    {
                                        this.ViewState["bReason"] = true;
                                    }
                                }
                                rbCorrectAnsNo3.Enabled = false;
                                rbCorrectAnsYes3.Enabled = false;
                                rbCorrectAnsNo3.Checked = true;
                                break;

                            case 3:
                                rbState3.Checked = true;
                                ddlCountState2.SelectedValue = "1";
                                ddlCountState3.SelectedValue = cRetryValue.ToString();
                                ddlCountState2.Enabled = false;
                                ddlCountState3.Enabled = true;
                                rbCorrectAnsNo2.Enabled = false;
                                rbCorrectAnsYes2.Enabled = false;
                                rbCorrectAnsNo2.Checked = true;
                                if (cAnsSue == false)
                                {
                                    rbCorrectAnsNo3.Checked = true;
                                    if (cCallType == "Paper")
                                    {
                                        this.ViewState["cAnsSue"] = false;
                                    }
                                    else if (cCallType == "Laboratory")
                                    {
                                        this.ViewState["bReason"] = false;
                                    }
                                }
                                else
                                {
                                    rbCorrectAnsYes3.Checked = true;
                                    if (cCallType == "Paper")
                                    {
                                        this.ViewState["cAnsSue"] = true;
                                    }
                                    else if (cCallType == "Laboratory")
                                    {
                                        this.ViewState["bReason"] = true;
                                    }
                                }

                                break;

                            case 4:
                                rbState4.Checked = true;
                                ddlCountState2.SelectedValue = "1";
                                ddlCountState3.SelectedValue = "1";
                                ddlCountState2.Enabled = false;
                                ddlCountState3.Enabled = false;
                                rbCorrectAnsNo2.Checked = true;
                                rbCorrectAnsNo3.Checked = true;
                                rbCorrectAnsNo2.Enabled = false;
                                rbCorrectAnsNo3.Enabled = false;
                                rbCorrectAnsYes2.Enabled = false;
                                rbCorrectAnsYes3.Enabled = false;
                                break;

                            case 5:

                                rbState5.Checked = true;
                                ddlCountState2.SelectedValue = "1";
                                ddlCountState3.SelectedValue = "1";
                                ddlCountState2.Enabled = false;
                                ddlCountState3.Enabled = false;
                                rbCorrectAnsNo2.Checked = true;
                                rbCorrectAnsNo3.Checked = true;
                                rbCorrectAnsNo2.Enabled = false;
                                rbCorrectAnsNo3.Enabled = false;
                                rbCorrectAnsYes2.Enabled = false;
                                rbCorrectAnsYes3.Enabled = false;
                                break;

                            default:
                                cCorrectTypeValue = 1;
                                cWrongTypeValue = 1;
                                cRetryValue = 1;
                                cAnsSue = false;
                                rbCorrectState1.Checked = true;
                                rbState1.Checked = true;
                                ddlCountState2.SelectedValue = "1";
                                ddlCountState3.SelectedValue = "1";
                                ddlCountState2.Enabled = false;
                                ddlCountState3.Enabled = false;
                                rbCorrectAnsNo2.Checked = true;
                                rbCorrectAnsNo3.Checked = true;
                                rbCorrectAnsNo2.Enabled = false;
                                rbCorrectAnsNo3.Enabled = false;
                                rbCorrectAnsYes2.Enabled = false;
                                rbCorrectAnsYes3.Enabled = false;
                                break;
                        }
                        #endregion
                    }
                }
            }
        }

        private void getParameter()
        {
            //cAnsProcessID
            if (Request.QueryString["AnsProcessID"] != null)
            {
                cAnsProcessID = Request.QueryString["AnsProcessID"].ToString();
            }
            //cAccessDBO
            if (Request.QueryString["AccessDBO"] != null)
            {
                cAccessDBO = Request.QueryString["AccessDBO"].ToString();
            }
            //cCallType
            if (Request.QueryString["CallType"] != null)
            {
                cCallType = Request.QueryString["CallType"].ToString();
            }
        }

        private void SetDescription(string strCallType)
        {
            if (strCallType == "Paper")
            {
                lbCorrectState1.Text = "不作任何動作，直接進入下一題。";
                lbCorrectState2.Text = "告知答案正確，給予正確答案，並直接進入下一題。";
                lbCorrectState3.Text = "告知答案正確，給予相關提示，並直接進入下一題。";
                lbState1.Text = "不作任何動作，直接進入下一題。";
                lbState2.Text = "告知答案錯誤次數，不給予相關提示，若在達到";
                lbState2Description1.Text = "次數時仍然答錯，";
                lbState2Description2.Text = "給予";
                lbState2Description3.Text = "不給予";
                lbState2Description4.Text = "正確答案，並直接進入下一題。";
                lbState3.Text = "告知答案錯誤次數，給予相關提示，若在達到";
                lbState3Description1.Text = "次數時仍然答錯，";
                lbState3Description2.Text = " 給予";
                lbState3Description3.Text = "不給予";
                lbState3Description4.Text = "正確答案，並直接進入下一題。";
                lbState4.Text = "告知答案錯誤次數，不給予相關提示，直到學習者選擇正確答案，再進入下一題。";
                lbState5.Text = " 告知答案錯誤次數，給予相關提示，直到學習者選擇正確答案，再進入下一題。";
            }
            else if (strCallType == "Laboratory")
            {
                lbCorrectState1.Text = "不作任何動作，直接進入下一題。";
                lbCorrectState2.Text = "告知答案正確，給予正確答案，並直接進入下一題。";
                lbCorrectState3.Text = "告知答案正確，給予相關提示，並直接進入下一題。";
                lbState1.Text = "不作任何動作，直接進入下一題。";
                lbState2.Text = "告知答案錯誤次數，不給予相關提示，若在達到";
                lbState2Description1.Text = "次數時仍然答錯，使用者";
                lbState2Description2.Text = "需要";
                lbState2Description3.Text = "不需要";
                lbState2Description4.Text = "填寫理由，並進入下一題。";
                lbState3.Text = "告知答案錯誤次數，給予相關提示，若在達到";
                lbState3Description1.Text = "次數時仍然答錯，使用者";
                lbState3Description2.Text = " 需要";
                lbState3Description3.Text = "不需要";
                lbState3Description4.Text = "填寫理由，並進入下一題。";
                lbState4.Text = "告知答案錯誤次數，不給予相關提示，直到學習者選擇正確答案，再進入下一題。";
                lbState5.Text = " 告知答案錯誤次數，給予相關提示，直到學習者選擇正確答案，再進入下一題。";
            }

        }

        protected void rbCorrectState1_CheckedChanged(object sender, System.EventArgs e)
        {
            this.ViewState["cCorrectTypeValue"] = 1;
        }

        protected void rbCorrectState2_CheckedChanged(object sender, System.EventArgs e)
        {
            this.ViewState["cCorrectTypeValue"] = 2;
        }

        protected void rbCorrectState3_CheckedChanged(object sender, System.EventArgs e)
        {
            this.ViewState["cCorrectTypeValue"] = 3;
        }

        protected void rbState1_CheckedChanged(object sender, System.EventArgs e)
        {
            this.ViewState["cWrongTypeValue"] = 1;
            ddlCountState2.Enabled = false;
            ddlCountState3.Enabled = false;
            rbCorrectAnsYes2.Enabled = false;
            rbCorrectAnsNo2.Enabled = false;
            rbCorrectAnsYes3.Enabled = false;
            rbCorrectAnsNo3.Enabled = false;
            if (cCallType == "Paper")
            {
                this.ViewState["cAnsSue"] = false;
            }
            else if (cCallType == "Laboratory")
            {
                this.ViewState["bReason"] = false;
            }
        }

        protected void rbState2_CheckedChanged(object sender, System.EventArgs e)
        {
            this.ViewState["cWrongTypeValue"] = 2;
            ddlCountState2.Enabled = true;
            ddlCountState3.Enabled = false;
            rbCorrectAnsYes2.Enabled = true;
            rbCorrectAnsNo2.Enabled = true;
            rbCorrectAnsYes3.Enabled = false;
            rbCorrectAnsNo3.Enabled = false;
        }

        protected void rbState3_CheckedChanged(object sender, System.EventArgs e)
        {
            this.ViewState["cWrongTypeValue"] = 3;
            ddlCountState2.Enabled = false;
            ddlCountState3.Enabled = true;
            rbCorrectAnsYes2.Enabled = false;
            rbCorrectAnsNo2.Enabled = false;
            rbCorrectAnsYes3.Enabled = true;
            rbCorrectAnsNo3.Enabled = true;
        }

        protected void rbState4_CheckedChanged(object sender, System.EventArgs e)
        {
            this.ViewState["cWrongTypeValue"] = 4;
            ddlCountState2.Enabled = false;
            ddlCountState3.Enabled = false;
            rbCorrectAnsYes2.Enabled = false;
            rbCorrectAnsNo2.Enabled = false;
            rbCorrectAnsYes3.Enabled = false;
            rbCorrectAnsNo3.Enabled = false;
            this.ViewState["cAnsSue"] = false;
        }

        protected void rbState5_CheckedChanged(object sender, System.EventArgs e)
        {
            this.ViewState["cWrongTypeValue"] = 5;
            ddlCountState2.Enabled = false;
            ddlCountState3.Enabled = false;
            rbCorrectAnsYes2.Enabled = false;
            rbCorrectAnsNo2.Enabled = false;
            rbCorrectAnsYes3.Enabled = false;
            rbCorrectAnsNo3.Enabled = false;
            if (cCallType == "Paper")
            {
                this.ViewState["cAnsSue"] = false;
            }
            else if (cCallType == "Laboratory")
            {
                this.ViewState["bReason"] = false;
            }
        }

        protected void rbCorrectAnsYes2_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cCallType == "Paper")
            {
                this.ViewState["cAnsSue"] = true;
            }
            else if (cCallType == "Laboratory")
            {
                this.ViewState["bReason"] = true;
            }
        }

        protected void rbCorrectAnsNo2_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cCallType == "Paper")
            {
                this.ViewState["cAnsSue"] = false;
            }
            else if (cCallType == "Laboratory")
            {
                this.ViewState["bReason"] = false;
            }
        }

        protected void rbCorrectAnsYes3_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cCallType == "Paper")
            {
                this.ViewState["cAnsSue"] = true;
            }
            else if (cCallType == "Laboratory")
            {
                this.ViewState["bReason"] = true;
            }
        }

        protected void rbCorrectAnsNo3_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cCallType == "Paper")
            {
                this.ViewState["cAnsSue"] = false;
            }
            else if (cCallType == "Laboratory")
            {
                this.ViewState["bReason"] = false;
            }
        }

        protected void ddlCountState2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState["cRetryValue"] = int.Parse(ddlCountState2.SelectedValue);
        }

        protected void ddlCountState3_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState["cRetryValue"] = int.Parse(ddlCountState3.SelectedValue);
        }

        protected void btSave_Click(object sender, System.EventArgs e)
        {
            cWrongTypeValue = int.Parse(this.ViewState["cWrongTypeValue"].ToString());
            cCorrectTypeValue = int.Parse(this.ViewState["cCorrectTypeValue"].ToString());
            if (cCallType == "Paper")
            {
                cAnsSue = Convert.ToBoolean(this.ViewState["cAnsSue"].ToString());
            }
            else if (cCallType == "Laboratory")
            {
                bReason = Convert.ToBoolean(this.ViewState["bReason"].ToString());
            }


            SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);

            if (cWrongTypeValue == 2)
            {
                cRetryValue = int.Parse(ddlCountState2.SelectedValue);
            }
            else
            {
                if (cWrongTypeValue == 3)
                {
                    cRetryValue = int.Parse(ddlCountState3.SelectedValue);
                }
                else
                {
                    cRetryValue = 1;
                }

            }

            string strSQL_AnsProcess = "SELECT * FROM AnsProcess WHERE cAnsProcessID = '" + cAnsProcessID + "' AND cAccessDBO = '" + cAccessDBO + "'";
            DataTable dtAnsProcess = new DataTable();
            dtAnsProcess = sqldb.getDataSet(strSQL_AnsProcess).Tables[0];
            if (dtAnsProcess.Rows.Count > 0)
            {
                string mySQL = "update AnsProcess set cCorrectTypeValue='" + cCorrectTypeValue + "',cWrongTypeValue='" + cWrongTypeValue + "',cRetryValue='" + cRetryValue + "', cAnsSue='" + cAnsSue + "' " +
                    "Where cAnsProcessID = '" + cAnsProcessID + "' And cAccessDBO = '" + cAccessDBO + "'";
                sqldb.ExecuteNonQuery(mySQL);
                RegisterStartupScript("", "<script language='javascript'>SettingSuccess();</script>");

            }
            else
            {
                string mySQL = "insert into AnsProcess (cAnsProcessID,cAccessDBO,cCorrectTypeValue,cWrongTypeValue,cRetryValue, cAnsSue, bReason) " +
                    "values('" + cAnsProcessID + "','" + cAccessDBO + "','" + cCorrectTypeValue + "','" + cWrongTypeValue + "','" + cRetryValue + "','" + cAnsSue + "','" + bReason + "')";
                sqldb.ExecuteNonQuery(mySQL);
                RegisterStartupScript("", "<script language='javascript'>SettingSuccess();</script>");
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
