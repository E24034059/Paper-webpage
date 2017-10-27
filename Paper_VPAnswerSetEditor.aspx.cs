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
using Hints.DB.Conversation;
using System.Data.SqlClient;

namespace PaperSystem
{
    public partial class Paper_VPAnswerSetEditor : AuthoringTool_BasicForm_BasicForm
    {
        string strUserID, strCaseID, strDivisionID, strClinicNum, strSectionName, strVPAID, strQID;
        string CurrentProType,strVPResponseType;
        int iAnswerType = 0;

        SQLString mySQL = new SQLString();

        private SqlConnection _DbConn;
        private SqlCommand _DbCmd;
        private string _DbConnectionString;

        public Paper_VPAnswerSetEditor()
        {
            this._DbConnectionString = @"Data Source=localhost;Initial Catalog=NewVersionHintsDB;
                                         User Id=hints;Password=mirac;";

            this._DbConn = new SqlConnection(this._DbConnectionString);
            this._DbCmd = new SqlCommand();
            this._DbCmd.CommandType = CommandType.Text;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(Paper_VPAnswerSetEditor));
            this.Initiate();

            if (!IsPostBack)
            {
                //接收參數
                this.getParametor();

                if (Request.QueryString["SelectedIndex"] != null)
                {
                    clsHintsDB hintsDB = new clsHintsDB();
                    string strGetConQuesSQL = "SELECT cQuestion FROM Conversation_Question WHERE cQID= '" + Request.QueryString["SelectedIndex"].ToString() + "'";
                    DataTable dtGetConQues = hintsDB.getDataSet(strGetConQuesSQL).Tables[0];
                    if (dtGetConQues.Rows.Count > 0)
                        LbCurrentConQues.Text = "<span style='color:red;'>" + dtGetConQues.Rows[0]["cQuestion"].ToString() + "</span>";
                }
            }

            if (Request.QueryString["Browse"] == "1") // 如果是Browse狀態，則不能進行編輯  老詹 2015/01/13
            {
                btnBack.Visible = false;
                btnNext.Visible = false;
                btnClose.Visible = true;
                Rbl_AnswerType.Enabled = false;
                btn_AddAns.Enabled = false;
            }
            else
            {
                btnClose.Visible = false;
            }

            //加入btnSaveNext的事件
            btnSaveNext.ServerClick += new EventHandler(btnSaveNext_ServerClick);

            hrQuestion.Style.Add("display", "none");
        }

        #region 接收參數
        private void getParametor()
        {
            //Opener
            if (Request.QueryString["Opener"] != null)
            {
                HiddenOpener.Value = Request.QueryString["Opener"].ToString();
            }

            //SelectedIndex
            if (Request.QueryString["SelectedIndex"] != null)
            {
                hfSelectedIndex.Value = Request.QueryString["SelectedIndex"].ToString();
            }

            //UserID
            if (Session["UserID"] != null)
            {
                strUserID = Session["UserID"].ToString();
            }

            //CaseID
            if (Session["CaseID"] != null)
            {
                strCaseID = Session["CaseID"].ToString();
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

           /* hfPaperID.Value = "";
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
            }*/

            //VPAID
            if (Request.QueryString["VPAID"] != null)
            {
                strVPAID = Request.QueryString["VPAID"].ToString();
                hfVPAID.Value = strVPAID;
            }

            //CurrentProType
            if (Request.QueryString["CurrentProType"] != null)
            {
                CurrentProType = Request.QueryString["CurrentProType"].ToString();
                hfCurrentProType.Value = CurrentProType;
            }

            //VPResponseType
            if (Request.QueryString["VPResponseType"] != null)
            {
                if (Request.QueryString["VPResponseType"].ToString() == "簡短的")
                {
                    ddl_VPResponseType.SelectedIndex = 0;
                    strVPResponseType = ddl_VPResponseType.SelectedValue.ToString();
                }
                else if (Request.QueryString["VPResponseType"].ToString() == "複雜的")
                {
                    ddl_VPResponseType.SelectedIndex = 1;
                    strVPResponseType = ddl_VPResponseType.SelectedValue.ToString();
                }
                else if (Request.QueryString["VPResponseType"].ToString() == "模糊不清的")
                {
                    ddl_VPResponseType.SelectedIndex = 2;
                    strVPResponseType = ddl_VPResponseType.SelectedValue.ToString();
                }
            }

            //QID(即VPAID 老詹 2014/12/08)
            clsHintsDB HintsDB = new clsHintsDB();
            string strSQL = "SELECT * FROM VP_AnswerSet WHERE cVPResponseType = '" + strVPResponseType + "' AND cVPAID='"+ strVPAID +"'";
            DataTable dt = HintsDB.getDataSet(strSQL).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strQID = dt.Rows[0]["cVPAID"].ToString();

                //Recover 
                if (this.IsPostBack == false)
                {
                    txtQuestionEdit.Text = dt.Rows[0]["cVPResponseContent"].ToString();
                    txtVPAnsTitle.Text = dt.Rows[0]["cVPAnsTitle"].ToString();
                    if (dt.Rows[0]["cVPResponseType"].ToString() == "簡短的")
                        ddl_VPResponseType.SelectedIndex = 0;
                    else if (dt.Rows[0]["cVPResponseType"].ToString() == "複雜的")
                        ddl_VPResponseType.SelectedIndex = 1;
                    else if (dt.Rows[0]["cVPResponseType"].ToString() == "模糊不清的")
                        ddl_VPResponseType.SelectedIndex = 2;

                    DataTable dtGetStudentAnsType = clsConversation.StudentAnsType_SELECT_AnswerType(strQID);
                    if (dtGetStudentAnsType.Rows.Count > 0)
                    {
                        int iSelected = Convert.ToInt32(dtGetStudentAnsType.Rows[0]["iAnswerType"].ToString())-1;
                        Rbl_AnswerType.SelectedIndex = iSelected;
                    }
                } 
            }
            else
            {
                //問題流水號(在此Title改為"VPAns")
                string iQuestionSerialNum = "VPAns";
                //以當下日期時間作為流水號
                DateTime now = DateTime.Now;
                string temp = now.ToString("yyyyMMddHHmmssFFFFF");
                //建立QID
                strQID = iQuestionSerialNum + "_" + temp;
                hfVPAID.Value = strQID;
            }
            if (Rbl_AnswerType.SelectedIndex != 0)
            { BindData(); }
            else
            { btn_AddAns.Enabled = false; }
        }
        #endregion

        protected void Rbl_AnswerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Rbl_AnswerType.SelectedIndex != 0)
            {
                this.BindData();  // 顯示GridView之答案欄位  老詹 2014/10/29
                this.GV_AnswerContent.Visible = true;
                btn_AddAns.Enabled = true;
            }
            else  // index=0表示是Textbox，無須設定答案內容  老詹 2014/10/29
            {
                this.GV_AnswerContent.Visible = false;
                btn_AddAns.Enabled = false;
            }
        }

        private void SaveQuestionText()
        {
            string strQTextContent = txtQuestionEdit.Text;
            strQTextContent = strQTextContent.Replace("&lt;", "<");
            strQTextContent = strQTextContent.Replace("&gt;", ">");

            string strVPAnsTitle = txtVPAnsTitle.Text;
            strVPAnsTitle = strVPAnsTitle.Replace("&lt;", "<");
            strVPAnsTitle = strVPAnsTitle.Replace("&gt;", ">");

            clsConversation.saveVPAnswer_BasicQuestionList(hfVPAID.Value.ToString(), hfCurrentProType.Value.ToString(), ddl_VPResponseType.SelectedItem.Text.ToString(), strVPAnsTitle, strQTextContent, Request.QueryString["GroupID"].ToString());

            clsHintsDB HintsDB = new clsHintsDB();
            if (Rbl_AnswerType.SelectedIndex == 0)  // 當學生回答模式為Textbox時，須採取的動作  老詹 2014/01/13
            {
                string strSQL = "SELECT * FROM StudentAnsType WHERE cVPAID = '" + hfVPAID.Value.ToString() + "'";
                DataTable dtTmp = HintsDB.getDataSet(strSQL).Tables[0];
                if (dtTmp.Rows.Count <= 0)
                {
                    string strInsertSQL = "INSERT INTO StudentAnsType (cVPAID , iAnswerType, cAnswerContent, bIsCorrect) " +
                    "VALUES ('" + hfVPAID.Value.ToString() + "' , '" + (Rbl_AnswerType.SelectedIndex + 1) + "' , '', '0') ";
                    HintsDB.ExecuteNonQuery(strInsertSQL);
                }
                else
                {
                    if (dtTmp.Rows[0]["iAnswerType"].ToString() != "1")
                    {
                        string strDeleteSQL = "DELETE FROM StudentAnsType WHERE cVPAID = '" + hfVPAID.Value.ToString() + "' AND iAnswerType != '1'";
                        HintsDB.ExecuteNonQuery(strDeleteSQL);

                        string strInsertSQL = "INSERT INTO StudentAnsType (cVPAID , iAnswerType, cAnswerContent, bIsCorrect) " +
                        "VALUES ('" + hfVPAID.Value.ToString() + "' , '" + (Rbl_AnswerType.SelectedIndex + 1) + "' , '', '0') ";
                        HintsDB.ExecuteNonQuery(strInsertSQL);
                    }
                }
            }
            else
            {   //刪除db.StudentAnsType中非教師設定的AnsType選項
                string strSQL = "SELECT * FROM StudentAnsType WHERE cVPAID = '" + hfVPAID.Value.ToString() + "' AND iAnswerType != '" + (Rbl_AnswerType.SelectedIndex+1) + "'";
                DataTable dtTmp = HintsDB.getDataSet(strSQL).Tables[0];
                if (dtTmp.Rows.Count > 0)
                {
                    string strDeleteSQL = "DELETE FROM StudentAnsType WHERE cVPAID = '" + hfVPAID.Value.ToString() + "' AND iAnswerType != '" + (Rbl_AnswerType.SelectedIndex + 1) + "'";
                    HintsDB.ExecuteNonQuery(strDeleteSQL);
                }
            }
        }

        private void btnSaveNext_ServerClick(object sender, EventArgs e)
        {
            SaveQuestionText();
            string strCurrentProType = Request.QueryString["CurrentProType"];
            string strGroupID = Request.QueryString["GroupID"];
            Response.Redirect("Paper_IndexListOfVPAnsSet.aspx?CurrentProType=" + strCurrentProType + "&GroupID=" + strGroupID + "&SelectedIndex=" + hfSelectedIndex.Value.ToString());
        }

        protected void GobackCareer(object sender, EventArgs e)
        {
            string strCurrentProType = Request.QueryString["CurrentProType"];
            string strGroupID = Request.QueryString["GroupID"];
            RegisterStartupScript("", "<script language='javascript'>goBack('" + strCurrentProType + "', '"+ strGroupID +"');</script>");
            //BindData();       
        }

        //繫結資料
        private void BindData()
        {
            DataTable oDT = this.GetData();
            this.BindGridView(oDT, this.GV_AnswerContent);
        }

        //繫結 GridView
        private void BindGridView(DataTable InDT, GridView InGridView)
        {
            //判斷 DataTable 有無資料
            if (InDT.Rows.Count == 0)
            {
                //使用與來源資料表相同的結構新增資料列
                DataRow oDR = InDT.NewRow();

                //允許資料列的欄位可以是 DBNULL 值
                foreach (DataColumn item in oDR.Table.Columns)
                {
                    item.AllowDBNull = true;
                }

                //DataTable 加入資料列
                InDT.Rows.Add(oDR);

                //將資料來源結構指定給 Grid DataSource
                InGridView.DataSource = InDT;
                InGridView.DataBind();

                //取得儲存格筆數
                int columnCount = InGridView.Rows[0].Cells.Count;
                InGridView.Rows[0].Cells.Clear();
                InGridView.Rows[0].Cells.Add(new TableCell());
                //合併儲存格
                InGridView.Rows[0].Cells[0].ColumnSpan = columnCount;
                //設定儲存格文字
                InGridView.Rows[0].Cells[0].Text = "No Data!";
                InGridView.RowStyle.HorizontalAlign = HorizontalAlign.Center;
                InGridView.RowStyle.VerticalAlign = VerticalAlign.Middle;
            }
            else
            {
                InGridView.DataSource = InDT;
                InGridView.DataBind();
            }
        }

        /// <summary>
        /// 取得資料表。
        /// </summary>        
        private DataTable GetData()
        {
            DataTable oDT = new DataTable();
            SqlDataReader oDbReader = null;
            if (this._DbConn.State != ConnectionState.Open)
            {
                this._DbConn.Open();
            }
            try
            {
                this._DbCmd.Connection = this._DbConn;
                this._DbCmd.CommandText = this.GetQueryStringSelect();
                oDbReader = this._DbCmd.ExecuteReader(CommandBehavior.CloseConnection);
                oDT.Load(oDbReader);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (oDbReader != null)
                {
                    oDbReader.Dispose();
                }
            }
            return oDT;
        }

        /// <summary>
        /// 取得 SQLCommand Select 字串。
        /// </summary>        
        private string GetQueryStringSelect()
        {
            iAnswerType = Rbl_AnswerType.SelectedIndex + 1;
            string sResult = string.Empty;
            sResult = @"Select cAnswerContent,bIsCorrect
                        From [dbo].StudentAnsType WHERE cVPAID='" + hfVPAID.Value.ToString() + "' AND iAnswerType='" + iAnswerType + "'";
            return sResult;
        }

        private void SetLightStick(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int iIndex = e.Row.RowIndex;
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='yellow'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            }
        }
        protected void GV_AnswerContent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            this.SetLightStick(sender, e);
        }
        protected void lkSaveF_Click(object sender, EventArgs e)
        {
            string sAnswerContent = ((TextBox)this.GV_AnswerContent.FooterRow.FindControl("txAnsF")).Text.Trim();
            if (sAnswerContent != "")
            {
                clsHintsDB HintsDB = new clsHintsDB();
                string strSQL = "SELECT * FROM StudentAnsType WHERE cVPAID = '" + hfVPAID.Value.ToString() + "' AND iAnswerType = '1'";
                DataTable dtTmp = HintsDB.getDataSet(strSQL).Tables[0];
                if (dtTmp.Rows.Count > 0) //當新增非Textbox的答題模式時，需先刪除原本Textbox答題模式的那一列資料  老詹 2014/01/13
                {
                    string strDeleteSQL = "DELETE FROM StudentAnsType WHERE cVPAID = '" + hfVPAID.Value.ToString() + "'";
                    HintsDB.ExecuteNonQuery(strDeleteSQL);
                }
                iAnswerType = Rbl_AnswerType.SelectedIndex + 1;
                if (((HtmlInputCheckBox)this.GV_AnswerContent.FooterRow.FindControl("CxCorrectF")).Checked)
                {
                    if (iAnswerType == 2 || iAnswerType == 3) //如果是ddl或rb時，判斷單選
                    {
                        string strPreUpdateSQL = "UPDATE StudentAnsType SET bIsCorrect='0'" +
                        "WHERE cVPAID='" + hfVPAID.Value.ToString() + "' AND iAnswerType='" + iAnswerType + "'";
                        HintsDB.ExecuteNonQuery(strPreUpdateSQL);
                    }
                    string strInsertSQL = "INSERT INTO StudentAnsType (cVPAID , iAnswerType, cAnswerContent, bIsCorrect) " +
                    "VALUES ('" + hfVPAID.Value.ToString() + "' , '" + iAnswerType + "' , '" + sAnswerContent + "', '1') ";
                    HintsDB.ExecuteNonQuery(strInsertSQL);
                }
                else
                {
                    string strInsertSQL = "INSERT INTO StudentAnsType (cVPAID , iAnswerType, cAnswerContent, bIsCorrect) " +
                    "VALUES ('" + hfVPAID.Value.ToString() + "' , '" + iAnswerType + "' , '" + sAnswerContent + "', '0') ";
                    HintsDB.ExecuteNonQuery(strInsertSQL);
                }
                this.DoCancelAdd();
            }
            else
            {
                string script = "alert('選項內容不可為空!!!');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert_window", "<script>" + script + "</script>", false);
            }
        }
        protected void lkCancelF_Click(object sender, EventArgs e)
        {
            this.DoCancelAdd();
        }
        //編輯
        protected void GV_AnswerContent_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.GV_AnswerContent.EditIndex = e.NewEditIndex;
            //取得原欄位資料，供更新時使用  老詹  2014/11/02
            string sAnswerContent = ((Label)this.GV_AnswerContent.Rows[GV_AnswerContent.EditIndex].FindControl("LbAns")).Text.Trim();
            Session["currentAnswerContent"] = sAnswerContent;
            this.BindData();
        }
        //更新    老詹 2014/10/31
        protected void GV_AnswerContent_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            clsHintsDB myDB = new clsHintsDB();

            //取得欲更新的欄位資料
            string sNewAnswerContent = ((TextBox)this.GV_AnswerContent.Rows[e.RowIndex].FindControl("txAnsE")).Text.Trim();
            bool bIsCorrectAnswer = ((HtmlInputCheckBox)this.GV_AnswerContent.Rows[e.RowIndex].FindControl("CxCorrectE")).Checked;

            if ((bIsCorrectAnswer) && (Rbl_AnswerType.SelectedIndex==1 || Rbl_AnswerType.SelectedIndex==2)) //判斷ddl&Radio時正解唯一機制  老詹 2015/07/02
            {
                string strPreUpdateSQL = "UPDATE StudentAnsType SET bIsCorrect='0'" +
                "WHERE cVPAID='" + hfVPAID.Value.ToString() + "' AND iAnswerType='" + (Rbl_AnswerType.SelectedIndex+1) + "'";
                myDB.ExecuteNonQuery(strPreUpdateSQL);
            }

            string strUpdateSQL = "UPDATE StudentAnsType SET cAnswerContent='" + sNewAnswerContent + "', bIsCorrect='" + bIsCorrectAnswer + "'" +
             "WHERE cAnswerContent = '" + Session["currentAnswerContent"].ToString() + "' AND cVPAID='" + hfVPAID.Value.ToString() + "'";
            myDB.ExecuteNonQuery(strUpdateSQL);

            DoCancelEidt();
        }
        //刪除    老詹 2014/11/02
        protected void GV_AnswerContent_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sAnswerContent = ((Label)this.GV_AnswerContent.Rows[e.RowIndex].FindControl("LbAns")).Text.Trim();
            clsHintsDB myDB = new clsHintsDB();
            string strSQL = "DELETE FROM StudentAnsType WHERE cAnswerContent = '" + sAnswerContent + "'";
            myDB.ExecuteNonQuery(strSQL);

            BindData();
        }
        //執行取消編輯
        protected void DoCancelEidt()
        {
            this.GV_AnswerContent.EditIndex = -1;
            this.BindData();
        }
        //執行取消新增
        protected void DoCancelAdd()
        {
            this.GV_AnswerContent.ShowFooter = false;
            this.BindData();
        }
        //取消編輯
        protected void GV_AnswerContent_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.DoCancelEidt();
        }
        //btn_AddAns_Click
        protected void btn_AddAns_Click(object sender, EventArgs e)
        {
            this.DoAdd();
        }
        //新增資料
        private void DoAdd()
        {
            this.GV_AnswerContent.ShowFooter = true;
            this.BindData();
            //關閉 No Data 資料列
            this.VisibleGridViewNoData(this.GV_AnswerContent, false);
        }
        //顯示 No Data 資料列。
        protected void VisibleGridViewNoData(GridView InGridView, bool IsShow)
        {
            if (InGridView.Rows.Count == 1 && !IsShow)
            {
                if (InGridView.Rows[0].Cells[0].Text.Trim().Equals("No Data!"))
                {
                    InGridView.Rows[0].Visible = IsShow;
                }
            }
        }

        // VP Response Type選取事件  老詹 2014/12/08
        protected void ddl_VPResponseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (strVPResponseType != "Default")
            {
                strVPResponseType = ddl_VPResponseType.SelectedValue.ToString();
                if (strVPResponseType != "Select a Response Type")
                {
                    clsHintsDB HintsDB = new clsHintsDB();
                    string strSQL = "SELECT cVPResponseContent FROM VP_AnswerSet WHERE cVPResponseType = '" + strVPResponseType + "' AND cVPAID='"+ hfVPAID.Value.ToString() +"'";
                    DataTable dtTemp = HintsDB.getDataSet(strSQL).Tables[0];
                    if (dtTemp.Rows.Count > 0)
                    {
                        txtQuestionEdit.Text = dtTemp.Rows[0]["cVPResponseContent"].ToString();
                    }
                    else
                    {
                        txtQuestionEdit.Text = "";
                    }
                }
            }
            //BindData();
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public string CheckAllFalse(string strVPAID)
        {
            string strReturn = "False";
            clsHintsDB hintsDB = new clsHintsDB();
            string strSQL = "SELECT bIsCorrect FROM StudentAnsType WHERE cVPAID = '" + strVPAID + "'";
            DataTable dtDescription = hintsDB.getDataSet(strSQL).Tables[0];
            foreach(DataRow dr in dtDescription.Rows)
            {
                if (dr["bIsCorrect"].ToString() == "True")
                    strReturn = "True";
            }
            return strReturn;
        }
}
}