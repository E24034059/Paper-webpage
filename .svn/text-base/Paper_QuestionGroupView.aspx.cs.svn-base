using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Hints.Learning.Question;
using Hints.DB;

public partial class AuthoringTool_CaseEditor_Paper_Paper_QuestionGroupView : AuthoringTool_BasicForm_BasicForm
{
    string strQID = "";
    string strGroupID = "";
    string strQuestionType = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        getParametor();

        BuildQuestionGroupTable();
    }

    /// <summary>
    /// 接收參數
    /// </summary>
    private void getParametor()
    {
        //QID
        if (Request.QueryString["QID"] != null)
        {
            strQID = Request.QueryString["QID"].ToString();
            if(strQID.IndexOf("VPAns") >= 0)
            {
                strQuestionType = "4";
                hfQuestionType.Value = strQuestionType;
                hfCurrentProType.Value = Request.QueryString["CurrentProType"].ToString();
            }
            else
            {
                DataTable dtQuestionMode = SQLString.getQuestionClassifyName(strQID);
                strQuestionType = dtQuestionMode.Rows[0]["cQuestionType"].ToString();
                dtQuestionMode.Dispose();
                dtQuestionMode = null;
            }
        }

        //GroupID
        if (Request.QueryString["GroupID"] != null)
        {
            strGroupID = Request.QueryString["GroupID"].ToString();
            hfGroupID.Value = strGroupID;

            if (Session["GroupID"] != null)
            {
                Session["GroupID"] = strGroupID;
            }
            else
            {
                Session.Add("GroupID", strGroupID);
            }
        }
    }

    //建立題組的表格
    private void BuildQuestionGroupTable()
    {
        switch (strQuestionType)
        {
            #region 選擇題
            case "1":
                tcQuestionGroupTable.Controls.Clear();
                Table table = new Table();
                tcQuestionGroupTable.Controls.Add(table);
                table.CellSpacing = 0;
                table.CellPadding = 2;
                table.BorderStyle = BorderStyle.Solid;
                table.BorderWidth = Unit.Pixel(1);
                table.BorderColor = System.Drawing.Color.Black;
                table.GridLines = GridLines.Both;
                table.Width = Unit.Percentage(100);

                //建立Table的CSS
                table.CssClass = "header1_table";

                //建立問題的內容
                TableRow trQuestion = new TableRow();
                table.Rows.Add(trQuestion);

                //問題的內容
                TableCell tcQuestion = new TableCell();
                trQuestion.Cells.Add(tcQuestion);
                tcQuestion.Text = GetQuestionContext();
                tcQuestion.ColumnSpan = 2;
                tcQuestion.Style.Add("text-align", "left");

                //建立問題的CSS
                trQuestion.Attributes.Add("Class", "header1_table_first_row");

                //建立選項的標題
                TableRow trSelectionTitle = new TableRow();
                table.Rows.Add(trSelectionTitle);
                TableCell tcSelectionTitle = new TableCell();
                trSelectionTitle.Cells.Add(tcSelectionTitle);
                tcSelectionTitle.Text = "選項內容";
                tcSelectionTitle.Width = Unit.Pixel(100);
                TableCell tcSelectionQuestionGroup = new TableCell();
                trSelectionTitle.Cells.Add(tcSelectionQuestionGroup);
                tcSelectionQuestionGroup.Text = "編輯功能";

                //建立選項
                SQLString mySQLString = new SQLString();
                string strSQL_QuestionSelectionIndex = mySQLString.getAllSelections(strQID);
                clsHintsDB HintsDB = new clsHintsDB();
                DataSet dsSelection = HintsDB.getDataSet(strSQL_QuestionSelectionIndex);
                if (dsSelection.Tables[0].Rows.Count > 0)
                {
                    for (int j = 0; j < dsSelection.Tables[0].Rows.Count; j++)
                    {
                        //Seq
                        string strSeq = "";
                        strSeq = dsSelection.Tables[0].Rows[j]["sSeq"].ToString();

                        //Selection
                        string strSelection = "";
                        strSelection = dsSelection.Tables[0].Rows[j]["cSelection"].ToString();

                        //bCaseSelect
                        bool bCaseSelect = false;
                        bCaseSelect = Convert.ToBoolean(dsSelection.Tables[0].Rows[j]["bCaseSelect"]);

                        //SelectionID
                        string strSelectionID = "";
                        strSelectionID = dsSelection.Tables[0].Rows[j]["cSelectionID"].ToString();

                        TableRow trSelection = new TableRow();
                        table.Rows.Add(trSelection);

                        //選項的相關內容
                        TableCell tcSelectionContext = new TableCell();
                        trSelection.Cells.Add(tcSelectionContext);
                        tcSelectionContext.Width = Unit.Percentage(80);
                        tcSelectionContext.Style.Add("text-align", "left");

                        //是否為建議選項
                        HtmlImage imgSuggest = new HtmlImage();
                        tcSelectionContext.Controls.Add(imgSuggest);
                        if (bCaseSelect == true)
                        {
                            imgSuggest.Src = "/Hints/Summary/Images/smiley4.gif";
                        }
                        else
                        {
                            imgSuggest.Src = "/Hints/Summary/Images/smiley11.gif";
                        }

                        //選項內容
                        Label lbSelection = new Label();
                        lbSelection.Text = "&nbsp;" + strSelection;
                        tcSelectionContext.Controls.Add(lbSelection);

                        //選項的相關題目
                        TableCell tcSelectionRelatedQuestion = new TableCell();
                        trSelection.Cells.Add(tcSelectionRelatedQuestion);
                        tcSelectionRelatedQuestion.Width = Unit.Percentage(20);
                        tcSelectionRelatedQuestion.Style.Add("text-align", "center");

                        DataTable dt = new DataTable();

                        if (dt.Rows.Count > 0)
                        {
                            Button btEditQuestionGroup = new Button();
                            btEditQuestionGroup.Text = "編輯相關問題";
                            btEditQuestionGroup.CssClass = "button_continue";
                            btEditQuestionGroup.Click += new EventHandler(btEditQuestionGroup_Click);
                            tcSelectionRelatedQuestion.Controls.Add(btEditQuestionGroup);
                            btEditQuestionGroup.CommandArgument = strSelectionID;
                        }
                        else
                        {
                            Button btEditQuestionGroup = new Button();
                            btEditQuestionGroup.Text = "編輯相關問題";
                            btEditQuestionGroup.CssClass = "button_continue";
                            btEditQuestionGroup.Click += new EventHandler(btEditQuestionGroup_Click);
                            tcSelectionRelatedQuestion.Controls.Add(btEditQuestionGroup);
                            btEditQuestionGroup.CommandArgument = strSelectionID;

                            Label lbNull = new Label();
                            lbNull.Text = "&nbsp;NULL";
                            //tcSelectionRelatedQuestion.Controls.Add(lbNull);
                        }

                        //建立選項的CSS
                        if ((Convert.ToInt32(strSeq) % 2) != 0)
                        {
                            trSelection.Attributes.Add("Class", "header1_tr_even_row");
                        }
                        else
                        {
                            trSelection.Attributes.Add("Class", "header1_tr_odd_row");
                        }
                    }
                }
                else
                {
                    //此問題沒有選項
                }
                dsSelection.Dispose();

                break;
            #endregion

            //問答題
            case "2":
                break;

            #region 圖型題
            case "3":
                tcQuestionGroupTable.Controls.Clear();
                Table table_sim = new Table();
                tcQuestionGroupTable.Controls.Add(table_sim);
                table_sim.CellSpacing = 0;
                table_sim.CellPadding = 2;
                table_sim.BorderStyle = BorderStyle.Solid;
                table_sim.BorderWidth = Unit.Pixel(1);
                table_sim.BorderColor = System.Drawing.Color.Black;
                table_sim.GridLines = GridLines.Both;
                table_sim.Width = Unit.Percentage(100);

                //建立Table的CSS
                table_sim.CssClass = "header1_table";

                //建立問題的內容
                TableRow trQuestion_sim = new TableRow();
                table_sim.Rows.Add(trQuestion_sim);

                //問題的內容
                TableCell tcQuestion_sim = new TableCell();
                trQuestion_sim.Cells.Add(tcQuestion_sim);
                tcQuestion_sim.Text = GetQuestionContext();
                tcQuestion_sim.ColumnSpan = 2;
                tcQuestion_sim.Style.Add("text-align", "left");

                //建立問題的CSS
                trQuestion_sim.Attributes.Add("Class", "header1_table_first_row");

                //建立選項的標題
                TableRow trSelectionTitle_sim = new TableRow();
                table_sim.Rows.Add(trSelectionTitle_sim);
                TableCell tcSelectionTitle_sim = new TableCell();
                trSelectionTitle_sim.Cells.Add(tcSelectionTitle_sim);
                tcSelectionTitle_sim.Text = "選項內容";
                trSelectionTitle_sim.Width = Unit.Pixel(100);
                TableCell tcSelectionQuestionGroup_sim = new TableCell();
                trSelectionTitle_sim.Cells.Add(tcSelectionQuestionGroup_sim);
                tcSelectionQuestionGroup_sim.Text = "編輯功能";

                //建立選項
                SQLString mySQLString_sim = new SQLString();
                string strSQL_QuestionSelectionIndex_sim = mySQLString_sim.getAllSimulation(strQID);
                clsHintsDB HintsDB_sim = new clsHintsDB();
                DataSet dsSelection_sim = HintsDB_sim.getDataSet(strSQL_QuestionSelectionIndex_sim);
                if (dsSelection_sim.Tables[0].Rows.Count > 0)
                {
                    for (int j = 0; j < dsSelection_sim.Tables[0].Rows.Count; j++)
                    {
                        //Seq
                        //string strSeq = "";
                        //strSeq = dsSelection_sim.Tables[0].Rows[j]["sSeq"].ToString();

                        //Selection
                        string strSelection = "";
                        strSelection = dsSelection_sim.Tables[0].Rows[j]["cAnswer"].ToString();
                        strSelection = strSelection.Replace('|', ',');//把線換成逗號
                        strSelection = strSelection.Substring(0, strSelection.Length - 1);//刪掉最後一個逗號
                        ////bCaseSelect
                        //bool bCaseSelect = false;
                        //bCaseSelect = Convert.ToBoolean(dsSelection_sim.Tables[0].Rows[j]["bCaseSelect"]);

                        //SelectionID
                        string strSelectionID = "";
                        strSelectionID = dsSelection_sim.Tables[0].Rows[j]["cAnsID"].ToString();



                        TableRow trSelection = new TableRow();
                        table_sim.Rows.Add(trSelection);

                        //選項的相關內容
                        TableCell tcSelectionContext = new TableCell();
                        trSelection.Cells.Add(tcSelectionContext);
                        tcSelectionContext.Width = Unit.Percentage(80);
                        tcSelectionContext.Style.Add("text-align", "left");

                        ////是否為建議選項
                        //HtmlImage imgSuggest = new HtmlImage();
                        //tcSelectionContext.Controls.Add(imgSuggest);
                        //if (bCaseSelect == true)
                        //{
                        //    imgSuggest.Src = "/Hints/Summary/Images/smiley4.gif";
                        //}
                        //else
                        //{
                        //    imgSuggest.Src = "/Hints/Summary/Images/smiley11.gif";
                        //}

                        //選項內容
                        Label lbSelection = new Label();
                        lbSelection.Text = "&nbsp;" + strSelection;
                        tcSelectionContext.Controls.Add(lbSelection);

                        //選項的相關題目
                        TableCell tcSelectionRelatedQuestion = new TableCell();
                        trSelection.Cells.Add(tcSelectionRelatedQuestion);
                        tcSelectionRelatedQuestion.Width = Unit.Percentage(20);
                        tcSelectionRelatedQuestion.Style.Add("text-align", "center");

                        DataTable dt = new DataTable();

                        if (dt.Rows.Count > 0)
                        {
                            Button btEditQuestionGroup = new Button();
                            btEditQuestionGroup.Text = "編輯相關問題";
                            btEditQuestionGroup.CssClass = "button_continue";
                            btEditQuestionGroup.Click += new EventHandler(btEditQuestionGroup_Click);
                            tcSelectionRelatedQuestion.Controls.Add(btEditQuestionGroup);
                            btEditQuestionGroup.CommandArgument = strSelectionID;
                        }
                        else
                        {
                            Button btEditQuestionGroup = new Button();
                            btEditQuestionGroup.Text = "編輯相關問題";
                            btEditQuestionGroup.CssClass = "button_continue";
                            btEditQuestionGroup.Click += new EventHandler(btEditQuestionGroup_Click);
                            tcSelectionRelatedQuestion.Controls.Add(btEditQuestionGroup);
                            btEditQuestionGroup.CommandArgument = strSelectionID;

                            Label lbNull = new Label();
                            lbNull.Text = "&nbsp;NULL";
                            //tcSelectionRelatedQuestion.Controls.Add(lbNull);
                        }

                        //建立選項的CSS
                        //if ((Convert.ToInt32(strSeq) % 2) != 0)
                        if ((j % 2) != 0)
                        {
                            trSelection.Attributes.Add("Class", "header1_tr_even_row");
                        }
                        else
                        {
                            trSelection.Attributes.Add("Class", "header1_tr_odd_row");
                        }
                    }
                }
                else
                {
                    //此問題沒有選項
                }
                dsSelection_sim.Dispose();

                break;
                #endregion

            case "4":
                tcQuestionGroupTable.Controls.Clear();
                Table Contable = new Table();
                tcQuestionGroupTable.Controls.Add(Contable);
                Contable.CellSpacing = 0;
                Contable.CellPadding = 2;
                Contable.BorderStyle = BorderStyle.Solid;
                Contable.BorderWidth = Unit.Pixel(1);
                Contable.BorderColor = System.Drawing.Color.Black;
                Contable.GridLines = GridLines.Both;
                Contable.Width = Unit.Percentage(100);

                //建立Table的CSS
                Contable.CssClass = "header1_table";

                //建立問題的內容
                TableRow trConQuestion = new TableRow();
                Contable.Rows.Add(trConQuestion);

                //問題的內容
                TableCell tcConQuestion = new TableCell();
                trConQuestion.Cells.Add(tcConQuestion);
                tcConQuestion.Text = "Student's Answer Options";
                tcConQuestion.ColumnSpan = 2;
                tcConQuestion.Style.Add("text-align", "left");

                //建立問題的CSS
                trConQuestion.Attributes.Add("Class", "header1_table_first_row");

                //建立選項的標題
                TableRow trConSelectionTitle = new TableRow();
                Contable.Rows.Add(trConSelectionTitle);
                TableCell tcConSelectionTitle = new TableCell();
                trConSelectionTitle.Cells.Add(tcConSelectionTitle);
                tcConSelectionTitle.Text = "選項內容";
                tcConSelectionTitle.Width = Unit.Pixel(100);
                TableCell tcConSelectionQuestionGroup = new TableCell();
                trConSelectionTitle.Cells.Add(tcConSelectionQuestionGroup);
                tcConSelectionQuestionGroup.Text = "編輯功能";

                //建立選項
                SQLString myConSQLString = new SQLString();
                string strSQL_VPAns = "SELECT * FROM StudentAnsType WHERE cVPAID = '"+ strQID +"'";
                clsHintsDB HintsConDB = new clsHintsDB();
                DataSet dsConSelection = HintsConDB.getDataSet(strSQL_VPAns);
                if (dsConSelection.Tables[0].Rows.Count > 0)
                {
                    for (int j = 0; j < dsConSelection.Tables[0].Rows.Count; j++)
                    {
                        //Selection
                        string strSelection = "";
                        strSelection = dsConSelection.Tables[0].Rows[j]["cAnswerContent"].ToString();

                        //bCaseSelect
                        bool bCaseSelect = false;
                        bCaseSelect = Convert.ToBoolean(dsConSelection.Tables[0].Rows[j]["bIsCorrect"]);

                        //SelectionID
                        string strSelectionID = "";
                        strSelectionID = Convert.ToString(j);

                        TableRow trConSelection = new TableRow();
                        Contable.Rows.Add(trConSelection);

                        //選項的相關內容
                        TableCell tcConSelectionContext = new TableCell();
                        trConSelection.Cells.Add(tcConSelectionContext);
                        tcConSelectionContext.Width = Unit.Percentage(80);
                        tcConSelectionContext.Style.Add("text-align", "left");

                        //是否為建議選項
                        HtmlImage imgConSuggest = new HtmlImage();
                        tcConSelectionContext.Controls.Add(imgConSuggest);
                        if (bCaseSelect == true)
                        {
                            imgConSuggest.Src = "/Hints/Summary/Images/smiley4.gif";
                        }
                        else
                        {
                            imgConSuggest.Src = "/Hints/Summary/Images/smiley11.gif";
                        }

                        //選項內容
                        Label lbConSelection = new Label();
                        lbConSelection.Text = "&nbsp;" + strSelection;
                        tcConSelectionContext.Controls.Add(lbConSelection);

                        //選項的相關題目
                        TableCell tcConSelectionRelatedQuestion = new TableCell();
                        trConSelection.Cells.Add(tcConSelectionRelatedQuestion);
                        tcConSelectionRelatedQuestion.Width = Unit.Percentage(20);
                        tcConSelectionRelatedQuestion.Style.Add("text-align", "center");

                        DataTable dt = new DataTable();

                        if (dt.Rows.Count > 0)
                        {
                            Button btEditQuestionGroup = new Button();
                            btEditQuestionGroup.Text = "編輯相關問題";
                            btEditQuestionGroup.CssClass = "button_continue";
                            btEditQuestionGroup.Style["cursor"] = "pointer";
                            btEditQuestionGroup.Click += new EventHandler(btEditQuestionGroup_Click);
                            tcConSelectionRelatedQuestion.Controls.Add(btEditQuestionGroup);
                            btEditQuestionGroup.CommandArgument = strSelectionID + "_" + strSelection;
                        }
                        else
                        {
                            Button btEditQuestionGroup = new Button();
                            btEditQuestionGroup.Text = "編輯相關問題";
                            btEditQuestionGroup.CssClass = "button_continue";
                            btEditQuestionGroup.Style["cursor"] = "pointer";
                            btEditQuestionGroup.Click += new EventHandler(btEditQuestionGroup_Click);
                            tcConSelectionRelatedQuestion.Controls.Add(btEditQuestionGroup);
                            btEditQuestionGroup.CommandArgument = strSelectionID + "_" + strSelection;

                            Label lbNull = new Label();
                            lbNull.Text = "&nbsp;NULL";
                            //tcSelectionRelatedQuestion.Controls.Add(lbNull);
                        }

                        //建立選項的CSS
                        if (((j+1) % 2) != 0)
                        {
                            trConSelection.Attributes.Add("Class", "header1_tr_even_row");
                        }
                        else
                        {
                            trConSelection.Attributes.Add("Class", "header1_tr_odd_row");
                        }
                    }
                }
                else
                {
                    //此問題沒有選項
                }
                dsConSelection.Dispose();
                break;

            default:
                break;
        }

    }

    void btEditQuestionGroup_Click(object sender, EventArgs e)
    {
        string strSelectionID = ((Button)(sender)).CommandArgument;
        Response.Redirect("Paper_QuestionGroupEdit.aspx?QID=" + strQID + "&GroupID=" + strGroupID + "&SelectionID=" + strSelectionID + "&CurrentProType=" + hfCurrentProType.Value.ToString());       
    }

    //取得問題的題目
    private string GetQuestionContext()
    {
        string strQuestionContext = "";

        switch (strQuestionType)
        {
            #region 選擇題
            case "1":
                strQuestionContext = PaperSystem.DataReceiver.getQuestionContentByQID(strQID);
                break;
            #endregion

            //問答題
            case "2":
                break;

            //圖型題
            case "3":
                strQuestionContext = PaperSystem.DataReceiver.getQuestionContentByQID_sim(strQID);
                break;

            default:
                break;
        }

        return strQuestionContext;

    }

}
