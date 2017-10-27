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

using Hints.DB;
using PaperSystem;
using System.Text.RegularExpressions;
using System.Xml;
using suro.util;

public partial class AuthoringTool_CaseEditor_Paper_Paper_SimulatorQuestionEditor3 : AuthoringTool_BasicForm_BasicForm
{
    //裝資料的表
    //DataTable dtbackground = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        //裝資料的表
        DataTable dtbackground = new DataTable();
        dtbackground.Columns.Add("name", typeof(string));
        dtbackground.Columns.Add("order", typeof(string));   

            string str_elementtype = "button";
            clsHintsDB sqlDB = new clsHintsDB();
            string strSQL = "";

        if (!this.IsPostBack)
        {              
            //SIMULATORID
            if (Request.QueryString["cImg"] != null)
                hf_img.Value = Request.QueryString["cImg"].ToString();
            //題目
            if (Request.QueryString["Title"] != null)
                hf_Title.Value = Request.QueryString["Title"].ToString();
            if (Request.QueryString["QID"] != null)
                hf_QID.Value = Request.QueryString["QID"].ToString();
            //清空delete
            hf_Delte.Value = "";
            //第一次近來刪掉答案選項重新擷取
            strSQL = "DELETE FROM Question_Simulator_ans WHERE cQuestion_simulator_ID = '" + hf_QID.Value + "'";
            sqlDB.ExecuteNonQuery(strSQL);                     
        }
       
        strSQL = "SELECT * FROM Question_Simulator_ans WHERE cQuestion_simulator_ID LIKE '" + hf_QID.Value + "'";
        DataTable dt_data_ans = sqlDB.getDataSet(strSQL).Tables[0];
        if (dt_data_ans.Rows.Count > 0)
        {//目前有資料
            for (int i = 0; i < dt_data_ans.Rows.Count; i++)
            {
                DataRow dr_in = dtbackground.NewRow();
                dr_in["name"] = dt_data_ans.Rows[i]["cName"].ToString();
                dr_in["order"] = dt_data_ans.Rows[i]["cOrder"].ToString();

                dtbackground.Rows.Add(dr_in);
            }
        }
        else
        {//第一次近來
            //datatable fill in value
            //換讀取VM ANNOTATION的 cFindingNum值
            string str_ID_Organ_disease = "";
            //if (hf_img.Value.Contains("Internal Medicine|General|1"))
            //{
            //    str_ID_Organ_disease = "alivsCase201007111917095468750|Simulation|Simulation|1";
            //}
            //else if (hf_img.Value.Contains("Internal Medicine|General|2"))
            //{
            //    str_ID_Organ_disease = "jeffCase201108171058121942541|Simulation|Simulation|2";
            //}
            //else
            //{
            //    str_ID_Organ_disease = hf_img.Value;
            //}
            str_ID_Organ_disease = hf_img.Value.Replace("Internal Medicine|General","Simulation|Simulation");
            string[] str_VMID_O_D_S = str_ID_Organ_disease.ToString().Split('|');            
            strSQL = "SELECT * FROM ItemForVMAnnotations WHERE cCaseID LIKE '" + str_VMID_O_D_S[0].ToString() + "' AND cOrgan LIKE '" + str_VMID_O_D_S[1].ToString() + "' AND cDisease LIKE '" + str_VMID_O_D_S[2].ToString() + "' AND cSlideNum LIKE '" + str_VMID_O_D_S[3].ToString() + "'";
            //strSQL = "SELECT * FROM SimulatorValue WHERE SimulatorID LIKE '" + hf_img.Value + "' AND Element_kind LIKE '" + str_elementtype + "'";
            DataTable dt_load = sqlDB.getDataSet(strSQL).Tables[0];
            //計算出所有的場景ID

            //將所有的場景ID以 同一個QID 各自的ANNOTATION 屬於哪個場景 點擊順序  記錄起來
            for (int i = 0; i < dt_load.Rows.Count; i++)
            {
                DataRow dr_in = dtbackground.NewRow();
                dr_in["name"] = dt_load.Rows[i]["cAnnotationTitle"].ToString();
                dr_in["order"] = i + 1;

                //insert
                SqlDB myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
                //VM ANNOTATION
                strSQL = "INSERT INTO Question_Simulator_ans (cQuestion_simulator_ID, cName, cOrder) " +
                 "VALUES ('" + hf_QID.Value + "' , @cName , @cOrder) ";
                object[] pList = { dt_load.Rows[i]["cAnnotationTitle"].ToString(), i + 1 };
                myDB.ExecuteNonQuery(strSQL, pList);

                dtbackground.Rows.Add(dr_in);
            }         
        }

        Btn_new.Attributes.Add("style", "text-align: center; background-color:  #b0c4de; border: #708090 1px solid;  color: #000; cursor: hand;");
        Btn_back.Attributes.Add("style", "text-align: center; background-color:  #b0c4de; border: #708090 1px solid;  color: #000; cursor: hand;");
        Btn_finish.Attributes.Add("style", "text-align: center; background-color:  #b0c4de; border: #708090 1px solid;  color: #000; cursor: hand;");
        btn_reset.Attributes.Add("style", "text-align: center; background-color:  #b0c4de; border: #708090 1px solid;  color: #000; cursor: hand;");
        //draw image
        string str_URL = "";
        DataTable dtTemp = new DataTable();
        if (hf_img.Value.Contains("Internal Medicine|General|1"))
        {
            string str_VRID = "Simulator_20100928144239";
            strSQL = "SELECT * FROM SimulatorBackground WHERE SimulatorID LIKE '" + str_VRID + "'";
            dtTemp = sqlDB.getDataSet(strSQL).Tables[0];
            str_URL = dtTemp.Rows[0]["bgUrl"].ToString();
        }
        else if (hf_img.Value.Contains("Internal Medicine|General|2"))
        {
            str_URL = "http://140.116.72.123/HintsCase/FileCollection/0101/201108/File20110817120244.JPG";
        }
        else
        {
            strSQL = "SELECT * FROM SimulatorBackground WHERE SimulatorID LIKE '" + hf_img.Value + "'";
            dtTemp = sqlDB.getDataSet(strSQL).Tables[0];
            str_URL = dtTemp.Rows[0]["bgUrl"].ToString();
        }
        //DataTable dtTemp = sqlDB.getDataSet(strSQL).Tables[0];       
        Img_simulator.ImageUrl = str_URL;
         //draw the edit table
        if (RB1.Checked == true)
        {
            table_O_display(dtbackground);//editdisplay();
        }
        else if (RB2.Checked == true)
        {
            noorderdisplay(dtbackground);
        }
        //draw the answer table
        Ansdisplay();
        //隱藏的textbox值傳給LABEL

    }

    protected void btn_delete_Click(object sender, EventArgs e)
    {
        //裝資料的表
        DataTable dtbackground = new DataTable();
        dtbackground.Columns.Add("name", typeof(string));
        dtbackground.Columns.Add("order", typeof(string));

        SqlDB myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
        clsHintsDB sqlDB = new clsHintsDB();
        string strSQL = "";
        string DeleteNO = ((Button)sender).CommandArgument;
        //hfDELETE記錄哪個是被刪掉的
        hf_Delte.Value += DeleteNO + "|";  
        //刪除記錄中的記錄
        strSQL = "DELETE Question_Simulator_ans WHERE cQuestion_simulator_ID LIKE '" + hf_QID.Value + "' AND cName LIKE '" + DeleteNO + "'";
        clsHintsDB MLASDB = new clsHintsDB();
        MLASDB.ExecuteNonQuery(strSQL);
        ////讀出現在的資料表
        //strSQL = "SELECT * FROM Question_Simulator_ans WHERE cQuestion_simulator_ID LIKE '" + hf_QID.Value + "'";
        //DataTable dt_data_ans = sqlDB.getDataSet(strSQL).Tables[0];

        //for (int i = 0; i < dt_data_ans.Rows.Count; i++)
        //{
        //    int strOrder = Convert.ToInt16( dt_data_ans.Rows[i]["cOrder"])-1;
        //    //textbox
        //    TextBox BoxBuffer = (TextBox)form1.FindControl("Order_" + i);
        //    //Update 更新ORDER
        //    strSQL = "UPDATE Question_Simulator_ans SET cOrder=@cOrder WHERE cQuestion_simulator_ID LIKE @cQuestion_simulator_ID  AND cName LIKE '" + dt_data_ans.Rows[i]["cName"].ToString() + "'";
        //    object[] pList = { BoxBuffer.Text.ToString(), hf_QID.Value };
        //    myDB.ExecuteNonQuery(strSQL, pList);
        //}
        //reorder
        list_order();

        //重新讀取資料
        strSQL = "SELECT * FROM Question_Simulator_ans WHERE cQuestion_simulator_ID LIKE '" + hf_QID.Value + "' ";
        DataTable dt_data_temp = sqlDB.getDataSet(strSQL).Tables[0];
        for (int n = 0; n < dt_data_temp.Rows.Count; n++)
        {
            DataRow dr_in = dtbackground.NewRow();
            dr_in["name"] = dt_data_temp.Rows[n]["cName"].ToString();
            dr_in["order"] = dt_data_temp.Rows[n]["cOrder"].ToString();

            dtbackground.Rows.Add(dr_in);
        }
        //re draw
        PL_table.Controls.Clear();
        if (RB1.Checked == true)
        {
            table_O_display(dtbackground);
        }
        else if (RB2.Checked == true)
        {
            noorderdisplay(dtbackground);
        }
    }
    protected void Btn_back_Click(object sender, EventArgs e)
    {
        //刪除q_s_a中的記錄
        string strSQL = "DELETE Question_Simulator_ans WHERE cQuestion_simulator_ID LIKE '" + hf_QID.Value + "'";
        clsHintsDB MLASDB = new clsHintsDB();
        MLASDB.ExecuteNonQuery(strSQL);
        //go back last step
        Response.Redirect("Paper_SimulatorQuestionEditor2.aspx?cImg=" + hf_img.Value + "&Title=" + hf_Title.Value + "&QID=" + hf_QID.Value + "");
    }
    protected void Btn_finish_Click(object sender, EventArgs e)
    {
        SqlDB myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
        
        //刪除q_s_a中的記錄
        string strSQL = "DELETE Question_Simulator_ans WHERE cQuestion_simulator_ID LIKE '" + hf_QID.Value + "'";
        clsHintsDB MLASDB = new clsHintsDB();
        MLASDB.ExecuteNonQuery(strSQL);
        //UPDATE response
        //strSQL = "SELECT * FROM Question_Simulator WHERE cQID LIKE '" + hf_QID.Value + "'";
        //DataTable dt_firstTime = MLASDB.getDataSet(strSQL).Tables[0];
        //for (int i = 0; i > dt_firstTime.Rows.Count; i++)
        //{
        //    Label LB_temp = (Label)this.FindControl("form1").FindControl("Res_" + dt_firstTime.Rows[i]["cAnsID"].ToString());
        //    strSQL = "UPDATE Question_Simulator SET cAnsID=@cAnsID WHERE cResponse LIKE @cResponse ";
        //    object[] pList = { dt_firstTime.Rows[i]["cAnsID"].ToString(), LB_temp.Text };
        //    myDB.ExecuteNonQuery(strSQL, pList);
        //}

            //go to next step
            Response.Redirect("Paper_QuestionViewNew.aspx?GroupID=" + Session["GroupID"].ToString());
    }
    protected void Btn_new_Click(object sender, EventArgs e)
    {
        SqlDB myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
        
        //string str_elementtype = "button";
        clsHintsDB sqlDB = new clsHintsDB();
        string strSQL = "SELECT * FROM Question_Simulator_ans WHERE cQuestion_simulator_ID LIKE '" + hf_QID.Value + "'";
        DataTable dt_Update = sqlDB.getDataSet(strSQL).Tables[0]; 
        //將此時的ANNOTATION ORDER更新回simulator_ans
        for (int idata = 0; idata < dt_Update.Rows.Count; idata++)
        {
            Label AnsBuff = (Label)this.FindControl("Form1").FindControl("lb_" + idata.ToString());
            for (int i = 0; i < dt_Update.Rows.Count; i++)
            {
                if (dt_Update.Rows[i]["cName"].ToString() == AnsBuff.Text)
                {   //Update order
                    TextBox OrderBuff = (TextBox)this.FindControl("Form1").FindControl("Order_" + idata.ToString());

                    strSQL = "UPDATE Question_Simulator_ans SET cOrder=@cOrder WHERE cQuestion_simulator_ID LIKE @cQuestion_simulator_ID  AND cName LIKE '" + AnsBuff.Text + "'";
                    object[] pList = { OrderBuff.Text, hf_QID.Value };
                    myDB.ExecuteNonQuery(strSQL, pList);
                    //找到就跳開
                    i = dt_Update.Rows.Count;
                }
            }            
        }
        //重新讀取新數據(order)
        strSQL = "SELECT * FROM Question_Simulator_ans WHERE cQuestion_simulator_ID LIKE '" + hf_QID.Value + "'";
        DataTable dtbackground = sqlDB.getDataSet(strSQL).Tables[0]; 
        //建立QID
        DataReceiver myReceiver = new DataReceiver();
        string strUserID = usi.UserID.ToString();
        string strAnsID = "Ans_" + myReceiver.getNowTime();
        string strQuestionMode = "General";
        string strQuestionGroupID = Session["GroupID"].ToString();
        string strQuestionDivisionID = "";
        string strPaperID = "";
        string strQuestion = hf_Title.Value;
        string strOrder = "";
        string strSimulatorID = "";
        //因為hf_img很長 所以只取VM ID的部分
        //if (hf_img.Value == "1111|Internal Medicine|General|1")
        //{
        //    strSimulatorID = "alivsCase201007111917095468750|Internal Medicine|General|1";
        //}
        //else if (hf_img.Value == "2222|Internal Medicine|General|2")
        //{
        //    strSimulatorID = "jeffCase201108171058121942541|Internal Medicine|General|2";
        //}
        //else
        //{
        //}
        //都會走這
        strSimulatorID = hf_img.Value;
        string strAnswer = "";
        clsTextQuestion myText = new clsTextQuestion();
        //如果是第一次存要多存一個其他(答錯)
        //換讀取VM Annotation cFindingNum
        strSQL = "SELECT * FROM Question_Simulator WHERE cQID LIKE '" + hf_QID.Value + "'";
        DataTable dt_firstTime = sqlDB.getDataSet(strSQL).Tables[0];
        if (dt_firstTime.Rows.Count == 0)
        {
            string strRAnsID = "RAns_" + myReceiver.getNowTime();
            //VM ID
            myText.saveIntoQuestionSimulator(hf_QID.Value, "其他(答錯)|", "1|", strSimulatorID, strQuestion, strRAnsID);
        }
        //拼出答案和順序
        string[] arr_Delete = hf_Delte.Value.Split('|');
        for (int x = 0; x < dtbackground.Rows.Count; x++)
        {
                strAnswer += dtbackground.Rows[x]["cName"].ToString() + "|";
                if (RB1.Checked == true)
                {
                    //有順序
                    strOrder += dtbackground.Rows[x]["cOrder"].ToString() + "|";
                }
                else if (RB2.Checked == true)
                {
                    strOrder +="1|";
                }
        }
      
        //save Question
		
        strSQL = "SELECT * FROM QuestionMode WHERE cQID = '" + hf_QID.Value + "' ";
       // SqlDB myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
        DataSet dsCheck = myDB.getDataSet(strSQL);
        if (dsCheck.Tables[0].Rows.Count == 0)
        {//問題只存一次
            myText.saveSimulatorQuestion(hf_QID.Value, strQuestion, strPaperID, strQuestionDivisionID, strQuestionGroupID, strQuestionMode);
        }
        //savE 正確答案 ANS
        //VM ID
        myText.saveIntoQuestionSimulator(hf_QID.Value, strAnswer, strOrder, strSimulatorID, strQuestion, strAnsID);
        //draw the answer table     
        pl_ans.Controls.Clear();
        Ansdisplay();
        //刪除記錄中的記錄
        strSQL = "DELETE Question_Simulator_ans WHERE cQuestion_simulator_ID LIKE '" + hf_QID.Value + "'";
        clsHintsDB MLASDB = new clsHintsDB();
        MLASDB.ExecuteNonQuery(strSQL);
        //重新放進原始資料
        //換VM ANNOTATION
        string str_Annotation = "";
        //if (hf_img.Value == "1111|Internal Medicine|General|1")
        //{
        //    str_Annotation = "alivsCase201007111917095468750|Simulation|Simulation|1";
        //}
        //else if (hf_img.Value.Contains("Internal Medicine|General|2"))
        //{
        //    str_Annotation = "jeffCase201108171058121942541|Simulation|Simulation|2";
        //}
        //else
        //{//都會走這
        //    //str_Annotation = hf_img.Value;
        //    str_Annotation = hf_img.Value.Replace("Internal Medicine|General","Simulation|Simulation");
        //}
        str_Annotation = hf_img.Value.Replace("Internal Medicine|General", "Simulation|Simulation");
        string[] str_VMID_O_D_S = str_Annotation.ToString().Split('|');
        strSQL = "SELECT * FROM ItemForVMAnnotations WHERE cCaseID LIKE '" + str_VMID_O_D_S[0].ToString() + "' AND cOrgan LIKE '" + str_VMID_O_D_S[1].ToString() + "' AND cDisease LIKE '" + str_VMID_O_D_S[2].ToString() + "' AND cSlideNum LIKE '" + str_VMID_O_D_S[3].ToString() + "'";        
        DataTable dt_load = sqlDB.getDataSet(strSQL).Tables[0];
        //計算出所有的場景ID

        //將所有的場景ID以 同一個QID 各自的ANNOTATION 屬於哪個場景 點擊順序  記錄起來
        for (int i = 0; i < dt_load.Rows.Count; i++)
        {
            //insert
            //SqlDB myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
            strSQL = "INSERT INTO Question_Simulator_ans (cQuestion_simulator_ID, cName, cOrder) " +
             "VALUES ('" + hf_QID.Value + "' , @cName , @cOrder) ";
            object[] pList = { dt_load.Rows[i]["cAnnotationTitle"].ToString(), i + 1 };
            myDB.ExecuteNonQuery(strSQL, pList);
        }
        //init
        hf_Delte.Value = "";
        //draw the edit table
        DataTable dtredraw = new DataTable();
        dtredraw.Columns.Add("name", typeof(string));
        dtredraw.Columns.Add("order", typeof(string));
        //重新讀取資料
        strSQL = "SELECT * FROM Question_Simulator_ans WHERE cQuestion_simulator_ID LIKE '" + hf_QID.Value + "' ";
        DataTable dt_data_temp = sqlDB.getDataSet(strSQL).Tables[0];
        for (int n = 0; n < dt_data_temp.Rows.Count; n++)
        {
            DataRow dr_in = dtredraw.NewRow();
            dr_in["name"] = dt_data_temp.Rows[n]["cName"].ToString();
            dr_in["order"] = dt_data_temp.Rows[n]["cOrder"].ToString();

            dtredraw.Rows.Add(dr_in);
        }
        PL_table.Controls.Clear();
        if (RB1.Checked == true)
        {
            table_O_display(dtredraw);       
        }
        else if (RB2.Checked == true)
            noorderdisplay(dtredraw);
        
    }
    protected void Ansdisplay()
    {
        clsHintsDB sqlDB = new clsHintsDB();
        string strSQL = "";
      
        strSQL = "SELECT * FROM Question_Simulator WHERE cQID LIKE '" + hf_QID.Value + "'";
        DataTable dt_Ans = sqlDB.getDataSet(strSQL).Tables[0];

        if (dt_Ans.Rows.Count > 0)
        {
            Table tb_Ans = new Table();
            tb_Ans.Attributes.Add("style", "color: #ffffcc; background-color: #2D6D9F; width:100%");
            //建立Table的CSS
            pl_ans.Controls.Add(tb_Ans);

            TableRow tr = new TableRow();
            tb_Ans.Rows.Add(tr);

            TableCell tcNoTitle = new TableCell();
            tr.Cells.Add(tcNoTitle);
            tcNoTitle.Text = "No";
            tcNoTitle.Attributes.Add("style", "text-align: center;width:5%;");

            TableCell tcCaseTitle = new TableCell();
            tr.Cells.Add(tcCaseTitle);
            tcCaseTitle.Text = "Answer";
            tcCaseTitle.Attributes.Add("style", "text-align: center;width:45%;");

            TableCell tcDivisionName = new TableCell();
            tr.Cells.Add(tcDivisionName);
            tcDivisionName.Text = "Order";
            tcDivisionName.Attributes.Add("style", "text-align: center;width:10%;");

            TableCell tcResponseTitle = new TableCell();
            tr.Cells.Add(tcResponseTitle);
            tcResponseTitle.Text = "Hints";
            tcResponseTitle.Attributes.Add("style", "text-align: center;width:30%;");

            TableCell tcSelect = new TableCell();
            tr.Cells.Add(tcSelect);
            tcSelect.Text = "Delete";
            tcSelect.Attributes.Add("style", "text-align: center;width:10%;");

            for (int i = 0; i < dt_Ans.Rows.Count; i++)
            {

                TableRow tr_New = new TableRow();
                tb_Ans.Controls.Add(tr_New);

                TableCell tcNO_name = new TableCell();
                Label lb_NO = new Label();
                lb_NO.Text = Convert.ToString(i + 1);
                tcNO_name.Controls.Add(lb_NO);
                tr_New.Cells.Add(tcNO_name);

                TableCell tcBtn_name = new TableCell();
                Label lb_btn = new Label();
                //顯示時用,替代|
                //換 VMID
                string tempAns = dt_Ans.Rows[i]["cAnswer"].ToString().Replace('|', ',');
                lb_btn.Text = tempAns.Remove(tempAns.Length - 1);
                tcBtn_name.Controls.Add(lb_btn);
                tr_New.Cells.Add(tcBtn_name);
                lb_btn.Attributes.Add("style", "text-align: center;");

                TableCell tcNo = new TableCell();
                Label lb_order = new Label();
                string tempOrder = dt_Ans.Rows[i]["cOrder"].ToString().Replace('|', ',');
                lb_order.Text = tempOrder.Remove(tempOrder.Length - 1);
                lb_order.Attributes.Add("style", "text-align: center;");
                tcNo.Controls.Add(lb_order);
                tr_New.Cells.Add(tcNo);
                //response
                TableCell tcRes = new TableCell();
                Label lb_Res = new Label();
                lb_Res.ID = "Res_" + dt_Ans.Rows[i]["cAnsID"].ToString();
                lb_Res.Text = dt_Ans.Rows[i]["cResponse"].ToString();
                lb_Res.Attributes.Add("style", "background-color:White; border:solid 1px Gray;");
                lb_Res.Width = 200;

                TextBox tb_Res_value = new TextBox();
                tb_Res_value.ID = "tb_" + dt_Ans.Rows[i]["cAnsID"].ToString();
                tb_Res_value.Text = dt_Ans.Rows[i]["cResponse"].ToString();
                tb_Res_value.Attributes.Add("style", "background-color:White; border:solid 1px Gray;display:none");
                tb_Res_value.Width = 1;

                Button btn_HTMLEditor = new Button();
                btn_HTMLEditor.Text = "...";
                btn_HTMLEditor.OnClientClick = "OpenHTMLEditor('" + dt_Ans.Rows[i]["cAnsID"].ToString() + "')";                                
                tcRes.Controls.Add(lb_Res);
                tcRes.Controls.Add(tb_Res_value);
                tcRes.Controls.Add(btn_HTMLEditor);

                tr_New.Cells.Add(tcRes);
                //delete
                TableCell tcSelectBtn = new TableCell();
                Button btn_delete = new Button();
                btn_delete.Text = "Delete";
                btn_delete.ToolTip = "Delect this item.";
                btn_delete.Click += new EventHandler(btn_del_Ans_Click);
                //cFindingNUM
                btn_delete.CommandArgument = dt_Ans.Rows[i]["cAnswer"].ToString();
                btn_delete.Attributes.Add("style", "text-align: center; background-color:  #b0c4de; border: #708090 1px solid;  color: #000; cursor: hand;");
                btn_delete.ID = "Del_Ans_" + i.ToString();
                tcSelectBtn.Controls.Add(btn_delete);
                tr_New.Cells.Add(tcSelectBtn);
                //奇偶數設定顏色
                if (i % 2 == 0)
                    tr_New.Attributes.Add("style", "text-align: center;font-family: Arial;color: #000000; background-color: #FFFFFF;");//font-size = 20px;
                else
                    tr_New.Attributes.Add("style", "text-align: center;font-family: Arial;color: #000000; background-color: #ECF9FF;");//font-size = 20px;

            }
        }
        else
        {
            Label lb_NA = new Label();
            lb_NA.Text = "N/A";
            lb_NA.Attributes.Add("style", "font-color:black;");
            pl_ans.Controls.Add(lb_NA);
        }
        //pl_ans
    }
    protected void noorderdisplay(DataTable dtbackground)
    {
        //排序 order
        DataRow[] SortDr = dtbackground.Select(null, "order ASC", DataViewRowState.CurrentRows);

        Table tb_element = new Table();
        tb_element.Attributes.Add("style", "font-weight: bold; color: #ffffcc; background-color: #2D6D9F; width:100%");
        //建立Table的CSS
        PL_table.Controls.Add(tb_element);

        TableRow tr = new TableRow();
        tb_element.Rows.Add(tr);

        TableCell tcCaseTitle = new TableCell();
        tr.Cells.Add(tcCaseTitle);
        tcCaseTitle.Text = "Button name";
        tcCaseTitle.Attributes.Add("style", "text-align: center;width:90%;");

        TableCell tcSelect = new TableCell();
        tr.Cells.Add(tcSelect);
        tcSelect.Text = "Delete";
        tcSelect.Attributes.Add("style", "text-align: center;width:10%;");
        int rowcount = 0;
        for (int i = 0; i < SortDr.Length; i++)
        {
            rowcount++;

            TableRow tr_New = new TableRow();
            tb_element.Controls.Add(tr_New);

            TableCell tcBtn_name = new TableCell();
            Label lb_btn = new Label();
            //lb_btn.Text = dtbackground.Rows[i]["ShowName"].ToString();
            lb_btn.Text = SortDr[i]["name"].ToString();
            tcBtn_name.Controls.Add(lb_btn);
            tr_New.Cells.Add(tcBtn_name);

            TableCell tcSelectBtn = new TableCell();
            Button btn_delete = new Button();
            btn_delete.Text = "Delete";
            btn_delete.ToolTip = "Delect this item.";
            btn_delete.Click += new EventHandler(btn_delete_Click);
            btn_delete.CommandArgument = SortDr[i]["name"].ToString();
            btn_delete.Attributes.Add("style", "text-align: center; background-color:  #b0c4de; border: #708090 1px solid;  color: #000; cursor: hand;");
            btn_delete.ID = "Delect" + i.ToString();
            tcSelectBtn.Controls.Add(btn_delete);
            tr_New.Cells.Add(tcSelectBtn);
            //奇偶數設定顏色
            if (rowcount % 2 == 0)
                tr_New.Attributes.Add("style", "text-align: center;font-family: Arial;color: #000000; background-color: #FFFFFF;font-size = 20px;");
            else
                tr_New.Attributes.Add("style", "text-align: center;font-family: Arial;color: #000000; background-color: #ECF9FF;font-size = 20px;");
        }    

    }
    protected void btn_del_Ans_Click(object sender, EventArgs e)
    {
        string DeleteAns = ((Button)sender).CommandArgument;
        //DELETE  ANS和OID相同的
        string strSQL = "DELETE Question_Simulator WHERE cQID LIKE '" + hf_QID.Value + "' AND cAnswer LIKE '" + DeleteAns + "'";
        clsHintsDB MLASDB = new clsHintsDB();
        MLASDB.ExecuteNonQuery(strSQL);
        clsHintsDB sqlDB = new clsHintsDB();
        //draw the answer table 
        pl_ans.Controls.Clear();
        Ansdisplay();
        //init
        hf_Delte.Value = "";
        //draw the edit table
        DataTable dtredraw = new DataTable();
        dtredraw.Columns.Add("name", typeof(string));
        dtredraw.Columns.Add("order", typeof(string));
        //重新讀取資料
        strSQL = "SELECT * FROM Question_Simulator_ans WHERE cQuestion_simulator_ID LIKE '" + hf_QID.Value + "' ";
        DataTable dt_data_temp = sqlDB.getDataSet(strSQL).Tables[0];
        for (int n = 0; n < dt_data_temp.Rows.Count; n++)
        {
            DataRow dr_in = dtredraw.NewRow();
            dr_in["name"] = dt_data_temp.Rows[n]["cName"].ToString();
            dr_in["order"] = dt_data_temp.Rows[n]["cOrder"].ToString();

            dtredraw.Rows.Add(dr_in);
        }
        //draw the edit table
        PL_table.Controls.Clear();
        if (RB1.Checked == true)
        {
            table_O_display(dtredraw);
            //editdisplay();
        }
        else if (RB2.Checked == true)
            noorderdisplay(dtredraw);
    }
    protected void RB1_CheckedChanged(object sender, EventArgs e)
    {
        PL_table.Controls.Clear();
        clsHintsDB sqlDB = new clsHintsDB();
        //draw the edit table
        DataTable dtredraw = new DataTable();
        dtredraw.Columns.Add("name", typeof(string));
        dtredraw.Columns.Add("order", typeof(string));
        //重新讀取資料
        string strSQL = "SELECT * FROM Question_Simulator_ans WHERE cQuestion_simulator_ID LIKE '" + hf_QID.Value + "' ";
        DataTable dt_data_temp = sqlDB.getDataSet(strSQL).Tables[0];
        for (int n = 0; n < dt_data_temp.Rows.Count; n++)
        {
            DataRow dr_in = dtredraw.NewRow();
            dr_in["name"] = dt_data_temp.Rows[n]["cName"].ToString();
            dr_in["order"] = dt_data_temp.Rows[n]["cOrder"].ToString();

            dtredraw.Rows.Add(dr_in);
        }
        table_O_display(dtredraw);
        //editdisplay();
    }
    protected void RB2_CheckedChanged(object sender, EventArgs e)
    {
        PL_table.Controls.Clear();
        clsHintsDB sqlDB = new clsHintsDB();
        //draw the edit table
        DataTable dtredraw = new DataTable();
        dtredraw.Columns.Add("name", typeof(string));
        dtredraw.Columns.Add("order", typeof(string));
        //重新讀取資料
        string strSQL = "SELECT * FROM Question_Simulator_ans WHERE cQuestion_simulator_ID LIKE '" + hf_QID.Value + "' ";
        DataTable dt_data_temp = sqlDB.getDataSet(strSQL).Tables[0];
        for (int n = 0; n < dt_data_temp.Rows.Count; n++)
        {
            DataRow dr_in = dtredraw.NewRow();
            dr_in["name"] = dt_data_temp.Rows[n]["cName"].ToString();
            dr_in["order"] = dt_data_temp.Rows[n]["cOrder"].ToString();

            dtredraw.Rows.Add(dr_in);
        }
        noorderdisplay(dtredraw);
    }
    protected void table_O_display(DataTable dtbackground)
    {
        //排序 order
        DataRow[] SortDr = dtbackground.Select(null, "order ASC", DataViewRowState.CurrentRows);

        Table tb_element = new Table();
        tb_element.Attributes.Add("style", "color: #ffffcc; background-color: #2D6D9F; width:100%");
        //建立Table的CSS
        PL_table.Controls.Add(tb_element);

        TableRow tr = new TableRow();
        tb_element.Rows.Add(tr);

        TableCell tcCaseTitle = new TableCell();
        tr.Cells.Add(tcCaseTitle);
        tcCaseTitle.Text = "Button name";
        tcCaseTitle.Attributes.Add("style", "text-align: center;width:80%;");

        TableCell tcDivisionName = new TableCell();
        tr.Cells.Add(tcDivisionName);
        //tcDivisionName.Text = "Order";
        tcDivisionName.Attributes.Add("style", "text-align: center;width:10%;");
        Button btn_Order = new Button();
        btn_Order.Text = "Order";
        btn_Order.ToolTip = "re-order";
        btn_Order.Attributes.Add("style", "text-align: center; background-color:  #2D6D9F;  color: #ffffcc; cursor: hand;");
        btn_Order.Click += new EventHandler(btn_Order_Click);
        tcDivisionName.Controls.Add(btn_Order);

        TableCell tcSelect = new TableCell();
        tr.Cells.Add(tcSelect);
        tcSelect.Text = "Delete";
        tcSelect.Attributes.Add("style", "text-align: center;width:10%;");
        int rowcount = 0;
        for (int i = 0; i < SortDr.Length; i++)
        {
            rowcount++;

            TableRow tr_New = new TableRow();
            tb_element.Controls.Add(tr_New);

            TableCell tcBtn_name = new TableCell();
            Label lb_btn = new Label();
            //lb_btn.Text = dtbackground.Rows[i]["ShowName"].ToString();
            lb_btn.ID = "lb_" + i.ToString();
            lb_btn.Text = SortDr[i]["name"].ToString();
            tcBtn_name.Controls.Add(lb_btn);
            tr_New.Cells.Add(tcBtn_name);

            TableCell tcNo = new TableCell();
            TextBox tb_no = new TextBox();
            tb_no.Text = SortDr[i]["order"].ToString();
            tb_no.Width = 50;
            tb_no.ID = "Order_" + i.ToString();
            tb_no.Attributes.Add("style", "text-align: center;");
            tcNo.Controls.Add(tb_no);
            tr_New.Cells.Add(tcNo);

            TableCell tcSelectBtn = new TableCell();
            Button btn_delete = new Button();
            btn_delete.Text = "Delete";
            btn_delete.ToolTip = "Delect this item.";
            btn_delete.Click += new EventHandler(btn_delete_Click);
            btn_delete.CommandArgument = SortDr[i]["name"].ToString();
            btn_delete.Attributes.Add("style", "text-align: center; background-color:  #b0c4de; border: #708090 1px solid;  color: #000; cursor: hand;");
            btn_delete.ID = "Delect" + i.ToString();
            tcSelectBtn.Controls.Add(btn_delete);
            tr_New.Cells.Add(tcSelectBtn);
            //奇偶數設定顏色
            if (rowcount % 2 == 0)
                tr_New.Attributes.Add("style", "text-align: center;font-family: Arial;color: #000000; background-color: #FFFFFF;");//font-size = 20px;
            else
                tr_New.Attributes.Add("style", "text-align: center;font-family: Arial;color: #000000; background-color: #ECF9FF;");
        }    
    }
    protected void btn_Order_Click(object sender, EventArgs e)
    {
        DataTable dtbackground = new DataTable();
        dtbackground.Columns.Add("name", typeof(string));
        dtbackground.Columns.Add("order", typeof(string));

        SqlDB myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
        clsHintsDB sqlDB = new clsHintsDB();
        string strSQL = "";

        list_order();
        //重新讀取資料
        strSQL = "SELECT * FROM Question_Simulator_ans WHERE cQuestion_simulator_ID LIKE '" + hf_QID.Value + "' ";
        DataTable dt_data_temp = sqlDB.getDataSet(strSQL).Tables[0];
        for (int n = 0; n < dt_data_temp.Rows.Count; n++)
        {
            DataRow dr_in = dtbackground.NewRow();
            dr_in["name"] = dt_data_temp.Rows[n]["cName"].ToString();
            dr_in["order"] = dt_data_temp.Rows[n]["cOrder"].ToString();

            dtbackground.Rows.Add(dr_in);
        }
        //re draw
        PL_table.Controls.Clear();
        if (RB1.Checked == true)
        {
            table_O_display(dtbackground);
        }
        else if (RB2.Checked == true)
        {
            noorderdisplay(dtbackground);
        }
    }
    protected void Q_S_compared()
    {   //老師的資料表
        DataTable dtredraw = new DataTable();
        dtredraw.Columns.Add("name", typeof(string));
        dtredraw.Columns.Add("order", typeof(string));
        //學生的資料表
        DataTable dt_s = new DataTable();
        dt_s.Columns.Add("name", typeof(string));
        dt_s.Columns.Add("order", typeof(string));
        //學生數據
        string student_ans = "box|pump|case history|breath|";
        //重新讀取資料
        clsHintsDB sqlDB = new clsHintsDB();
        string strSQL = "SELECT * FROM Question_Simulator WHERE cQID LIKE '" + hf_QID.Value + "' ";
        DataTable dt_data_temp = sqlDB.getDataSet(strSQL).Tables[0];
        string[] str_Ans = dt_data_temp.Rows[0]["cAnswer"].ToString().Split('|');
        string[] str_order = dt_data_temp.Rows[0]["cOrder"].ToString().Split('|');
        for (int i = 0; i < str_Ans.Length; i++)
        {//塞入內容及排序
            DataRow dr_in = dtredraw.NewRow();
            dr_in["name"] = str_Ans[i].ToString();
            dr_in["order"] = str_order[i].ToString();
            dtredraw.Rows.Add(dr_in);
        }
        //分解學生資料
        string[] arr_Stu = student_ans.Split('|');
        for (int x = 0; x < arr_Stu.Length; x++)
        {
            DataRow dr_S = dt_s.NewRow();
            dr_S["name"] = arr_Stu[x].ToString();
            dr_S["order"] = "";

            dt_s.Rows.Add(dr_S);
        }
        //填入學生答案的排序
        for (int m = 0; m < dt_s.Rows.Count; m++)
        {
            for (int n = 0; n < dtredraw.Rows.Count; n++)
            {
                if (dt_s.Rows[m]["name"].ToString() == dtredraw.Rows[n]["name"].ToString())
                {
                    dt_s.Rows[m]["order"] = dtredraw.Rows[n]["order"].ToString();
                }
            }
        }
        //學生排序和老師排序做比對
        string str_stu_O = "";
        for (int m = 0; m < dt_s.Rows.Count; m++)
        {
            str_stu_O += dt_s.Rows[m]["order"].ToString();
        }
        string str_chairman = "";
        for (int r = 0; r < dt_s.Rows.Count; r++)
        {
            str_chairman += dt_s.Rows[r]["order"].ToString();
        }
        //MLAS_PathCompare PC = new MLAS_PathCompare();
        //string str_compare = PC.LD(str_stu_O, str_chairman);
    }
    protected void list_order()
    {
        //裝資料的表
        DataTable dtbackground = new DataTable();
        dtbackground.Columns.Add("name", typeof(string));
        dtbackground.Columns.Add("order", typeof(string));

        SqlDB myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
        clsHintsDB sqlDB = new clsHintsDB();
        string strSQL = "";
        //讀出現在的資料表
        strSQL = "SELECT * FROM Question_Simulator_ans WHERE cQuestion_simulator_ID LIKE '" + hf_QID.Value + "'";
        DataTable dt_data_ans = sqlDB.getDataSet(strSQL).Tables[0];

        for (int i = 0; i < dt_data_ans.Rows.Count; i++)
        {
            //textbox
            TextBox BoxBuffer = (TextBox)form1.FindControl("Order_" + i);
            //Update 更新ORDER
            strSQL = "UPDATE Question_Simulator_ans SET cOrder=@cOrder WHERE cQuestion_simulator_ID LIKE @cQuestion_simulator_ID  AND cName LIKE '" + dt_data_ans.Rows[i]["cName"].ToString() + "'";
            //object[] pList = { BoxBuffer.Text.ToString(), hf_QID.Value };
            object[] pList = { (i+1).ToString(), hf_QID.Value };
            myDB.ExecuteNonQuery(strSQL, pList);
        }
      
    }
    protected void btn_reset_Click(object sender, EventArgs e)
    {
        string str_elementtype = "button";
        SqlDB myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
        clsHintsDB sqlDB = new clsHintsDB();
        //刪除記錄中的記錄
        string strSQL = "DELETE Question_Simulator_ans WHERE cQuestion_simulator_ID LIKE '" + hf_QID.Value + "'";
        clsHintsDB MLASDB = new clsHintsDB();
        MLASDB.ExecuteNonQuery(strSQL);
        //重新放進原始資料
        //VM ANNOTATION
        //換讀取VM ANNOTATION的 cFindingNum值
        string str_ID_Organ_disease = "";
        if (hf_img.Value == "1111|Internal Medicine|General|1")
        {
            str_ID_Organ_disease = "alivsCase201007111917095468750|Simulation|Simulation|1";
        }
        else if (hf_img.Value.Contains("Internal Medicine|General|2"))
        {
            str_ID_Organ_disease = "jeffCase201108171058121942541|Simulation|Simulation|2";
        }
        else
        {
            //str_ID_Organ_disease = hf_img.Value;
            str_ID_Organ_disease = hf_img.Value.Replace("Internal Medicine|General", "Simulation|Simulation");
        }
        string[] str_VMID_O_D_S = str_ID_Organ_disease.ToString().Split('|');
        strSQL = "SELECT * FROM ItemForVMAnnotations WHERE cCaseID LIKE '" + str_VMID_O_D_S[0].ToString() + "' AND cOrgan LIKE '" + str_VMID_O_D_S[1].ToString() + "' AND cDisease LIKE '" + str_VMID_O_D_S[2].ToString() + "' AND cSlideNum LIKE '" + str_VMID_O_D_S[3].ToString() + "'";
        //strSQL = "SELECT * FROM SimulatorValue WHERE SimulatorID LIKE '" + hf_img.Value + "' AND Element_kind LIKE '" + str_elementtype + "'";
        DataTable dt_load = sqlDB.getDataSet(strSQL).Tables[0];
        //計算出所有的場景ID

        //將所有的場景ID以 同一個QID 各自的ANNOTATION 屬於哪個場景 點擊順序  記錄起來
        for (int i = 0; i < dt_load.Rows.Count; i++)
        {
            //insert
            myDB = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
            strSQL = "INSERT INTO Question_Simulator_ans (cQuestion_simulator_ID, cName, cOrder) " +
             "VALUES ('" + hf_QID.Value + "' , @cName , @cOrder) ";
            object[] pList = { dt_load.Rows[i]["cAnnotationTitle"].ToString(), i + 1 };
            myDB.ExecuteNonQuery(strSQL, pList);
        }
        //init
        hf_Delte.Value = "";
        //draw the edit table
        DataTable dtredraw = new DataTable();
        dtredraw.Columns.Add("name", typeof(string));
        dtredraw.Columns.Add("order", typeof(string));
        //重新讀取資料
        strSQL = "SELECT * FROM Question_Simulator_ans WHERE cQuestion_simulator_ID LIKE '" + hf_QID.Value + "' ";
        DataTable dt_data_temp = sqlDB.getDataSet(strSQL).Tables[0];
        for (int n = 0; n < dt_data_temp.Rows.Count; n++)
        {
            DataRow dr_in = dtredraw.NewRow();
            dr_in["name"] = dt_data_temp.Rows[n]["cName"].ToString();
            dr_in["order"] = dt_data_temp.Rows[n]["cOrder"].ToString();

            dtredraw.Rows.Add(dr_in);
        }
        PL_table.Controls.Clear();
        if (RB1.Checked == true)
        {
            table_O_display(dtredraw);
        }
        else if (RB2.Checked == true)
            noorderdisplay(dtredraw);
    }
    //protected void re_value()
    //{
    //    clsHintsDB sqlDB = new clsHintsDB();
    //    string strSQL = "SELECT * FROM Question_Simulator WHERE cQID LIKE '" + hf_QID.Value + "' ";
    //    DataTable dt_data_temp = sqlDB.getDataSet(strSQL).Tables[0];
    //    if (dt_data_temp.Rows.Count > 0)
    //    {
    //        for (int i = 0; i > dt_data_temp.Rows.Count; i++)
    //        {
    //            Label BoxBuffer = (Label)form1.FindControl("Res_" + dt_data_temp.Rows[i]["cAnsID"].ToString());
    //            TextBox tb_t = (TextBox)form1.FindControl("tb_" + dt_data_temp.Rows[i]["cAnsID"].ToString());

    //            BoxBuffer.Text
    //        }
    //    }
    //}
}
