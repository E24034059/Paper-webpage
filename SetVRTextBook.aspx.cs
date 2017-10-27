using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Hints.DB;
using Hints.HintsUtility;
using Hints.HintsUtility.ScriptUtility;
using System.Web.UI.HtmlControls;

public partial class AuthoringTool_CaseEditor_Paper_SetVRTextBook : System.Web.UI.Page
{
    protected clsHintsDB sqldb = new clsHintsDB();
    protected string cQuestionID;//這個題目的ID
    protected string cUser;//編輯這個教材的使用者ID

    protected void Page_Load(object sender, EventArgs e)
    {
        //cQuestionID = "afengQuestionSituation201506161125586359980";
        //cUser = "afeng";
        if (Request.QueryString["VMQuestionID"] != null)
            cQuestionID = Request.QueryString["VMQuestionID"].ToString();
        else if (Session["VMQuestionID"] != null)
            cQuestionID = Session["VMQuestionID"].ToString();
        else
            cQuestionID = "afengQuestionSituation201408012324523323336";

        if (Request.QueryString["UserID"] != null)
            cUser = Request.QueryString["UserID"].ToString();
        else if (Session["UserID"] != null)
            cUser = Session["UserID"].ToString();
        else
            cUser = "afeng";

        if (!IsPostBack)
        {
            setDDLNewStep();
            updateCheckTable();
            insertSatrt();
        }

        setStep();
        //setTextBookDefaultName();
        setQuestionName();
        btCancelEdit.Attributes.Add("onclick", "return confirm('Cancel the Edit ?')");

    }

    protected void insertSatrt()
    {
        //檢查是否已經有了
        //得到這題的答案
        DataTable dtRecord = new DataTable();
        dtRecord = getStepData(cQuestionID);
        //取得現在時間，用來做ID
        DateTime myDate = DateTime.Now;
        string date = myDate.ToString("yyyyMMddhhmmssfff");
        //檢查該表中是否有該record的資料了
        for (int i = 0; i < dtRecord.Rows.Count; i++)
        {
            DataTable dtcheckText = new DataTable();
            dtcheckText = checkRecordIsExisting(dtRecord.Rows[i]["strRecordID"].ToString(), "Text");
            if (dtcheckText.Rows.Count > 0)
            {

            }
            else
            {
                //讀取這個record對應的ann
                DataTable dtAnn = getAnn(dtRecord.Rows[i]["strRecordID"].ToString());
                string annID = dtAnn.Rows[0]["strAnnotationID"].ToString();
                //判斷這個Ann在對應表中是否存在
                DataTable dtCheck = checkTempTable(cQuestionID, dtRecord.Rows[i]["strRecordID"].ToString(), annID);
                if (dtCheck.Rows.Count > 0)
                {
                }
                else
                {
                    //寫入對應資料
                    insertTextBookTemp(cQuestionID, dtRecord.Rows[i]["strRecordID"].ToString(), annID);
                }
                insertTextBookData(dtRecord.Rows[i]["strRecordID"].ToString(), "TextBookID" + cUser + date + "1", "Text", "1", "說明1");
            }

            DataTable dtcheckSuggest = new DataTable();
            dtcheckSuggest = checkRecordIsExisting(dtRecord.Rows[i]["strRecordID"].ToString(), "Suggest");
            if (dtcheckSuggest.Rows.Count > 0)
            {

            }
            else
            {
                //讀取這個record對應的ann
                DataTable dtAnn = getAnn(dtRecord.Rows[i]["strRecordID"].ToString());
                string annID = dtAnn.Rows[0]["strAnnotationID"].ToString();
                //判斷這個Ann在對應表中是否存在
                DataTable dtCheck = checkTempTable(cQuestionID, dtRecord.Rows[i]["strRecordID"].ToString(), annID);
                if (dtCheck.Rows.Count > 0)
                {
                }
                else
                {
                    //寫入對應資料
                    insertTextBookTemp(cQuestionID, dtRecord.Rows[i]["strRecordID"].ToString(), annID);
                }
                insertTextBookData(dtRecord.Rows[i]["strRecordID"].ToString(), "TextBookID" + cUser + date + "2", "Suggest", "1", "提示1");
            }

        }
        //若沒有插入預設

    }

    protected void updateCheckTable()
    {
        //得到這題的答案
        DataTable dtRecord = new DataTable();
        dtRecord = getStepData(cQuestionID);

        //將對應表中的資料拿去跟答案表做比對
        for (int i = 0; i < dtRecord.Rows.Count; i++)
        {
            DataTable dtcheck = getTempData1(dtRecord.Rows[i]["strAnnotationID"].ToString());
            if (dtcheck.Rows.Count > 0)
            {
                //更新VRTextbookTemp跟VRTextBookData的recordID
                UpdateTextBookDataRecordID(dtcheck.Rows[0]["Rid"].ToString(), dtRecord.Rows[i]["strRecordID"].ToString());
                UpdateTextBookTempRecordID(dtcheck.Rows[0]["Rid"].ToString(), dtRecord.Rows[i]["strRecordID"].ToString());
            }

        }
        //得到這個題目的對應表資料



        //有一樣的Aid則改變textbookData及textbooktemp中對應的record資料
    }

    protected DataTable checkRecordIsExisting(string recordID, string type)
    {
        DataTable dt = new DataTable();
        string strSQL = "SELECT * FROM VRTextBookContent WHERE cQID ='" + cQuestionID + "' AND strRecordID='" + recordID + "' AND cType ='" + type + "'";
        dt = sqldb.getDataSet(strSQL).Tables[0];

        return dt;
    }

    protected DataTable getTempData1(string Aid)
    {
        DataTable dt = new DataTable();
        string strSQL = "SELECT * FROM VRTextBookTemp WHERE  QID ='" + cQuestionID + "' AND Aid = '" + Aid + "'";
        dt = sqldb.getDataSet(strSQL).Tables[0];

        return dt;
    }

    protected DataTable getAnnsRecord(string Annid)
    {
        DataTable dt = new DataTable();
        string strSQL = "SELECT * FROM  ItemForVRStepRecords S  WHERE strAnnotationID ='" + Annid + "'";
        dt = sqldb.getDataSet(strSQL).Tables[0];

        return dt;
    }

    protected void UpdateTextBookDataRecordID(string oldRecordID, string newRecordID)
    {

        string strSQL = "UPDATE VRTextBookContent  SET strRecordID = '" + newRecordID + "' WHERE cQID = '" + cQuestionID + "' AND strRecordID = '" + oldRecordID + "';";
        try
        {
            sqldb.ExecuteNonQuery(strSQL);
        }
        catch
        {
            //this.RegisterClientScriptBlock("", "<script> alert('error'); </script>");
        }
    }

    protected void UpdateTextBookTempRecordID(string oldRecordID, string newRecordID)
    {

        string strSQL = "UPDATE VRTextBookTemp  SET Rid = '" + newRecordID + "' WHERE QID = '" + cQuestionID + "' AND Rid = '" + oldRecordID + "';";
        try
        {
            sqldb.ExecuteNonQuery(strSQL);
        }
        catch
        {
            //this.RegisterClientScriptBlock("", "<script> alert('error'); </script>");
        }
    }
    protected void setDDLNewStep()
    {
        DataTable dtRecord = new DataTable();
        dtRecord = getStepData(cQuestionID);

        if (dtRecord.Rows.Count > 0)
        {
            for (int i = 0; i < dtRecord.Rows.Count; i++)
            {
                //設定在另存新檔出現的div的步驟ddl
                ddlNewStep.Items.Add(new ListItem((i + 1).ToString() + ". " + dtRecord.Rows[i]["strAnnotationName"].ToString(), dtRecord.Rows[i]["strRecordID"].ToString()));
            }
        }
    }

    //設定題目名稱
    protected void setQuestionName()
    {
        DataTable dtQName = new DataTable();
        dtQName = getQuestionName(cQuestionID);
        //Label1.Text = "Step Of "+dtQName.Rows[0]["cQuestion"].ToString();
        Label1.Text = "Step";
    }

    //設定教材名稱的初始值
    protected void setTextBookDefaultName()
    {
        DateTime myDate = DateTime.Now;
        string date = myDate.ToString("yyyyMMddhhmm");
        tbxTextBookName.Text = "TextBook";
    }

    //產生頁面左側的Rrecord(步驟)選單
    protected void setStep()
    {
        PanelStep.Controls.Clear();
        //ddlNewStep.Items.Clear();
        //得到標準步驟的資訊
        DataTable dtRecord = new DataTable();
        dtRecord = getStepData(cQuestionID);

        //加入Accordion的效果
        PanelStep.Attributes.Add("class", "classAccordion");
        PanelStep.Attributes.Add("width", "95%");
        if (dtRecord.Rows.Count > 0)
        {

            for (int i = 0; i < dtRecord.Rows.Count; i++)
            {
                //動態產生在前端的<h3>Section(i+1)</h3>
                HtmlGenericControl h3Yogesh = new HtmlGenericControl("h3");
                h3Yogesh.InnerText = (i + 1).ToString() + ". " + dtRecord.Rows[i]["strAnnotationName"].ToString();

                //動態產生在前端的<div></div>
                HtmlGenericControl divYogesh = new HtmlGenericControl("div");
                //divYogesh.Attributes.Add("ID", "div_" + dtGroupTemplateContent.Rows[i]["GroupTemplateID"].ToString() + "_" + dtGroupTemplateContent.Rows[i]["NodeOrLinkID"].ToString());
                Label lb = new Label();
                lb.Text = i.ToString();
                //divYogesh.Controls.Add(lb);
                //建立各個Record(步驟)中的說明、提示、暗示等列表，並加入accordion的內容欄中
                divYogesh.Controls.Add(setTextBookContent(dtRecord.Rows[i]["strRecordID"].ToString()));
                PanelStep.Controls.Add(h3Yogesh);
                PanelStep.Controls.Add(divYogesh);

                //設定在另存新檔出現的div的步驟ddl
                //ddlNewStep.Items.Add(new ListItem((i + 1).ToString() + ". " + dtRecord.Rows[i]["strAnnotationName"].ToString(), dtRecord.Rows[i]["strRecordID"].ToString()));
            }
        }



    }

    //設定頁面左側各個Record(步驟)中的說明、提示、暗示等列表
    protected Panel setTextBookContent(string strRecordID)
    {
        Panel panelContent = new Panel();
        panelContent.Attributes.Add("width", "95%");
        panelContent.ID = "panelContent" + strRecordID;

        //textBook的內容，type為--說明--
        Label lbContent = new Label();
        lbContent.Text = "說明:";
        lbContent.Font.Size = 16;
        lbContent.Font.Bold = true;

        panelContent.Controls.Add(lbContent);
        panelContent.Controls.Add(setDetailData(strRecordID, "Text"));




        //textBook的內容，type為--提示 Suggest--
        Label lbSuggest = new Label();
        lbSuggest.Text = "暗示:";
        lbSuggest.Font.Size = 16;
        lbSuggest.Font.Bold = true;

        panelContent.Controls.Add(lbSuggest);
        panelContent.Controls.Add(setDetailData(strRecordID, "Suggest"));



        //textBook的內容，type為--暗示 Hint--

        Label lbHint = new Label();
        lbHint.Text = "暗示:";
        lbHint.Font.Size = 16;
        lbHint.Font.Bold = true;

        // panelContent.Controls.Add(lbHint);
        //panelContent.Controls.Add(setDetailData(strRecordID, "Hint"));

        return panelContent;

    }

    //設定各個textBook，分為說明、提示、暗示
    protected Table setDetailData(string strRecordID, string cType)
    {
        DataTable dtDetailData = new DataTable();
        dtDetailData = getTextBookData(strRecordID, cType);

        Table tbContent = new Table();
        tbContent.Attributes.Add("style", "margin-bottom: 10px;");
        TableRow trTitle = new TableRow();
        TableCell tcTextEdit = new TableCell();
        tcTextEdit.Attributes.Add("align", "right");
        tbContent.Attributes.Add("width", "100%");


        LinkButton lbtnModify = new LinkButton();
        lbtnModify.Text = "編輯";
        lbtnModify.ID = "lbtnModify_" + cType + "_" + strRecordID;
        lbtnModify.ForeColor = System.Drawing.Color.Blue;
        lbtnModify.Font.Underline = true;
        lbtnModify.Click += new EventHandler(lbtnModify_Click);

        // tcTextEdit.Controls.Add(lbtnModify);
        trTitle.Controls.Add(tcTextEdit);

        TableRow trContent = new TableRow();
        TableCell tcContent = new TableCell();



        //HtmlGenericControl ulYogesh = new HtmlGenericControl("ul");
        Panel ulYogesh = new Panel();//將ul改成 panel
        ulYogesh.Attributes.Add("class", "sortable");

        //ulYogesh.Attributes.Add("ID", "ul_" + cType + "_" + strRecordID);//ID為ul+類型+RecordID(ex:ul_Text_Recordafeng201412241616524178830)
        ulYogesh.ID = "ul_" + cType + "_" + strRecordID;

        if (dtDetailData.Rows.Count > 0)
        {
            for (int i = 0; i < dtDetailData.Rows.Count; i++)
            {
                HtmlGenericControl divYogesh = new HtmlGenericControl("div");
                divYogesh.Attributes["style"] = "border:solid 1px black;border-collaspse:collapse;border-style: groove; border-width: thin; color: #FFFFFF; font-weight: 900;font-size: X-large;";
                //divYogesh.Attributes.Add("width", "1000px");
                divYogesh.Attributes.Remove("class");
                divYogesh.Attributes.Add("class", "ui-state-default");
                //divYogesh.Attributes.Add("class", "table table-hover");
                divYogesh.ID = "div_" + strRecordID + "_" + dtDetailData.Rows[i]["cTextBookID"].ToString();//ID為div+strRecordID+TextBookID(ex:ul_Record123456_TextBookID201412241616524178830)
                //divYogesh.Attributes.Add("onmouseover", "setMouseOverView()");
                Image imgMove = new Image();
                imgMove.ImageUrl = "~/AuthoringTool/CaseEditor/Paper/images/led-icons/triangles1.png";
                imgMove.ID = "imgMove_" + strRecordID + "_" + dtDetailData.Rows[i]["cTextBookID"].ToString();
                //imgMove.Visible = false;
                //imgMove.Visible = false;

                divYogesh.Controls.Add(imgMove);

                LinkButton lBtn = new LinkButton();
                lBtn.ID = "lbtn_" + strRecordID + "_" + dtDetailData.Rows[i]["cTextBookID"].ToString();
                lBtn.Text = dtDetailData.Rows[i]["cTextBookName"].ToString();
                lBtn.Click += new EventHandler(lbtn_Click);
                lBtn.ForeColor = System.Drawing.Color.Blue;
                //lBtn.Enabled = true;
                divYogesh.Controls.Add(lBtn);

                ImageButton imbtnDel = new ImageButton();
                imbtnDel.ID = "imgbtnDel_" + strRecordID + "_" + dtDetailData.Rows[i]["cTextBookID"].ToString();
                imbtnDel.ImageUrl = "~/AuthoringTool/CaseEditor/Paper/images/led-icons/cancel.png";
                imbtnDel.ImageAlign = ImageAlign.Right;
                //imbtnDel.Visible = false;
                imbtnDel.Click += new ImageClickEventHandler(imbtnDel_Click);//在按了新增後新增的按鈕也要加入觸發
                imbtnDel.Width = 30;
                imbtnDel.Height = 30;
                imbtnDel.Attributes.Add("onclick", "return confirm('確定刪除 ?')");

                divYogesh.Controls.Add(imbtnDel);

                ulYogesh.Controls.Add(divYogesh);
            }
        }

        //加入可以點擊新增Text的div
        HtmlGenericControl divAddYogesh = new HtmlGenericControl("div");
        divAddYogesh.Attributes["style"] = "border:solid 1px black;border-collaspse:collapse;border-style: dashed; text-align: center; border-width: thin; color: #FFFFFF; font-weight: 900;font-size: X-large;";
        divAddYogesh.ID = "div_ADD_" + cType + "_" + strRecordID;//ID為div_ADD_+類型+TextBookID(ex:ul_Text_TextBookID201412241616524178830)
        //divAddYogesh.Visible = false;

        ImageButton imbtnAdd = new ImageButton();
        imbtnAdd.ID = "imbtnAdd_" + cType + "_" + strRecordID;
        imbtnAdd.ImageUrl = "~/AuthoringTool/CaseEditor/Paper/images/led-icons/add.png";
        imbtnAdd.Click += new ImageClickEventHandler(imbtnAdd_Click);


        divAddYogesh.Controls.Add(imbtnAdd);
        ulYogesh.Controls.Add(divAddYogesh);

        tcContent.Controls.Add(ulYogesh);
        trContent.Controls.Add(tcContent);

        tbContent.Controls.Add(trTitle);
        tbContent.Controls.Add(trContent);

        return tbContent;
    }

    //控制在編輯頁面上的物件的出現與否  HiddenFieldEdit.value = 0:目前不為編輯；1:編輯
    protected void setIsEditState()
    {
        if (HiddenFieldEdit.Value.ToString() == "1")
        {
            lbtnEdit.Text = "完成";

            lbImageName.Visible = false;
            UploadImage.Visible = true;
            btnImportImage.Visible = true;
            btnRemoveImage.Visible = true;

            lbVideoName.Visible = false;
            VideoUpload.Visible = true;
            btnVideoImport.Visible = true;
            btnRemoveVideo.Visible = true;

            lbAudioName.Visible = false;
            AudioUpload.Visible = true;
            btnImportAudio.Visible = true;
            btnRemoveAudio.Visible = true;

            //lbPDFName.Visible = false;
            //PDFUpload.Visible = true;
            //btnPDFbImport.Visible = true;
            //btnRemovePDF.Visible = true;

            lbTextName.Visible = false;
            tbxEditTextContent.Visible = true;

            lbWebName.Visible = false;
            tbxWeb.Visible = true;


        }
        else
        {
            lbtnEdit.Text = "編輯";

            lbImageName.Visible = true;
            UploadImage.Visible = false;
            btnImportImage.Visible = false;
            btnRemoveImage.Visible = false;

            lbVideoName.Visible = true;
            VideoUpload.Visible = false;
            btnVideoImport.Visible = false;
            btnRemoveVideo.Visible = false;

            lbAudioName.Visible = true;
            AudioUpload.Visible = false;
            btnImportAudio.Visible = false;
            btnRemoveAudio.Visible = false;

            //lbPDFName.Visible = true;
            //PDFUpload.Visible = false;
            //btnPDFbImport.Visible = false;
            //btnRemovePDF.Visible = false;

            lbTextName.Visible = true;
            tbxEditTextContent.Visible = false;

            lbWebName.Visible = true;
            tbxWeb.Visible = false;


            lbWebName.InnerText = tbxWeb.Text.ToString();
            lbWebName.Attributes.Remove("style");
            lbWebName.Attributes.Add("style", "font-size: larger; color: #0066FF");
            lbWebName.Attributes.Remove("href");
            lbWebName.Attributes.Add("href", tbxWeb.Text.ToString());

        }
    }

    //控制被點選的顏色
    protected void setTextBookSelectView()
    {

    }
    //--------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------按鈕觸發--------------------------------------------------------------------------

    //點選編輯的觸發，會讓這個類別的textbook可以移動，刪除
    void lbtnModify_Click(object sender, EventArgs e)
    {
        LinkButton templbtn = (LinkButton)sender;

        //將傳入的ID做切割 [0]lbtnModify , [1]type , [2] recordID
        string[] strSpilt = templbtn.ID.ToString().Split('_');

        if (templbtn.Text.ToString() == "編輯")
        {
            //讓div(說明、提示、暗示)可以被移動
            Panel ulYogeshTemp = (Panel)FindControl("ul_" + templbtn.ID.ToString().Replace("lbtnModify_", ""));
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + ulYogeshTemp.ClientID.ToString()+ "');</script>");
            ulYogeshTemp.Attributes.Add("class", "sortable");


            //讀取該record中這個type有哪些textbook，用來控制div上的圖示或btn
            DataTable dtDetailData = new DataTable();
            dtDetailData = getTextBookData(strSpilt[2], strSpilt[1]);

            if (dtDetailData.Rows.Count > 0)
            {
                for (int i = 0; i < dtDetailData.Rows.Count; i++)
                {
                    //顯示可以移動的圖示
                    Image imgTemp = (Image)FindControl("imgMove_" + strSpilt[2] + "_" + dtDetailData.Rows[i]["cTextBookID"].ToString());
                    imgTemp.Visible = true;

                    //讓textBook不可被點擊
                    //LinkButton lbtnTextBookTemp = (LinkButton)FindControl("lbtn_" + dtDetailData.Rows[i]["cTextBookID"].ToString());
                    //lbtnTextBookTemp.Enabled = false;

                    //顯示移除的按鈕
                    ImageButton imbtnDelTemp = (ImageButton)FindControl("imgbtnDel_" + strSpilt[2] + "_" + dtDetailData.Rows[i]["cTextBookID"].ToString());
                    imbtnDelTemp.Visible = true;
                }
            }

            //隱藏新增textbook的+號區域
            HtmlGenericControl divAddYogeshTemp = (HtmlGenericControl)FindControl("div_ADD_" + templbtn.ID.ToString().Replace("lbtnModify_", ""));
            divAddYogeshTemp.Visible = false;

            //編輯選項變成"完成"
            templbtn.Text = "完成";
        }

        else if (templbtn.Text.ToString() == "完成")
        {
            Panel ulYogeshTemp = (Panel)FindControl("ul_" + templbtn.ID.ToString().Replace("lbtnModify_", ""));
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + ulYogeshTemp.ClientID.ToString()+ "');</script>");
            ulYogeshTemp.Attributes.Remove("class");

            //讀取該record中這個type有哪些textbook，用來控制div上的圖示或btn
            DataTable dtDetailData = new DataTable();
            dtDetailData = getTextBookData(strSpilt[2], strSpilt[1]);

            if (dtDetailData.Rows.Count > 0)
            {
                for (int i = 0; i < dtDetailData.Rows.Count; i++)
                {
                    //隱藏可以移動的圖示
                    Image imgTemp = (Image)FindControl("imgMove_" + strSpilt[2] + "_" + dtDetailData.Rows[i]["cTextBookID"].ToString());
                    imgTemp.Visible = false;

                    //讓textBook不可被點擊
                    //LinkButton lbtnTextBookTemp = (LinkButton)FindControl("lbtn_" + dtDetailData.Rows[i]["cTextBookID"].ToString());
                    //lbtnTextBookTemp.Enabled = true;
                    //string a = lbtnTextBookTemp.Text.ToString();
                    //隱藏移除的按鈕
                    ImageButton imbtnDelTemp = (ImageButton)FindControl("imgbtnDel_" + strSpilt[2] + "_" + dtDetailData.Rows[i]["cTextBookID"].ToString());
                    imbtnDelTemp.Visible = false;
                }
            }

            //顯示新增textbook的+號區域
            HtmlGenericControl divAddYogeshTemp = (HtmlGenericControl)FindControl("div_ADD_" + templbtn.ID.ToString().Replace("lbtnModify_", ""));
            divAddYogeshTemp.Visible = true;

            templbtn.Text = "編輯";
        }


    }

    //選擇TextBook後的觸發
    void lbtn_Click(object sender, EventArgs e)
    {
        //HiddenFieldEdit.Value = "0";
        //setIsEditState();

        LinkButton tempImBtn = (LinkButton)sender;

        EventWhenSelectTextBook(tempImBtn.ID.ToString());

    }

    //選擇textbook後所做的處理
    protected void EventWhenSelectTextBook(string textBoolID)
    {
        HiddenFieldEdit.Value = "0";
        //setIsEditState();

        //將傳入的ID做切割 [0]imbtn , [1]recordID , [2] textBookID
        string[] strSpilt = textBoolID.Split('_');

        //得到TextBook的資料
        DataTable dtTextBook = new DataTable();
        dtTextBook = getTextBookName(strSpilt[2]);

        //得到TextBook的詳細資訊(圖、影片、聲音、PDF、文字)
        DataTable dtTextBookDetailData = new DataTable();
        dtTextBookDetailData = getTextBookDetailData(strSpilt[2]);

        if (HiddenFieldSelectTextBook.Value.ToString() != "0")
        {
            try
            {
                //先將前一個選的textbook還原
                HtmlGenericControl divAddYogeshPre = (HtmlGenericControl)FindControl("div_" + HiddenFieldSelectTextBook.Value.ToString());
                divAddYogeshPre.Attributes.Remove("class");
                divAddYogeshPre.Attributes.Add("class", "ui-state-default");
            }
            catch
            {

            }


        }

        HtmlGenericControl divAddYogeshTemp = (HtmlGenericControl)FindControl("div_" + strSpilt[1] + "_" + strSpilt[2]);
        divAddYogeshTemp.Attributes.Remove("class");
        divAddYogeshTemp.Attributes.Add("class", "ui-state-highlight");

        //記錄現在所選擇的TextBook
        HiddenFieldSelectTextBook.Value = strSpilt[1] + "_" + strSpilt[2];
        HiddenFieldTextBook.Value = strSpilt[2];

        //將被點擊的div標示出來
        setTextBookSelectView();
        tbxTextBookName.Text = dtTextBook.Rows[0]["cTextBookname"].ToString();
        //先把原先的清空
        lbImageName.Text = "請點選編輯新增圖片";
        lbVideoName.Text = "請點選編輯新增影片";
        lbAudioName.Text = "請點選編輯新增音訊檔";
        //lbPDFName.Text = "請點選編輯新增PDF";
        lbTextName.Text = "請點選編輯新增文字說明";
        lbWebName.InnerText = "請點選編輯新增網頁連結";
        tbxTextContent.Text = "";
        tbxEditTextContent.Text = "";
        tbxWeb.Text = "";
        lbWebName.Attributes.Remove("href");
        imgUpLoadImage.ImageUrl = "~/AuthoringTool/CaseEditor/VirtualMicroscope/VMicroscope/VMicroscope.Web/UploadFiles/@.png";
        vdUploadvideo.Attributes.Remove("src");
        audUploadAudio.Attributes.Add("src", "");

        //判斷讀到的資料是什麼型別，分別將資訊設入相對應的項目
        for (int i = 0; i < dtTextBookDetailData.Rows.Count; i++)
        {
            //圖片
            if (dtTextBookDetailData.Rows[i]["cMediaType"].ToString() == "Image")
            {
                imgUpLoadImage.ImageUrl = "~/AuthoringTool/CaseEditor/VirtualMicroscope/VMicroscope/VMicroscope.Web/UploadFiles/" + dtTextBookDetailData.Rows[i]["cContent"].ToString();
                lbImageName.Text = dtTextBookDetailData.Rows[i]["cContent"].ToString();
                HiddenFieldImage.Value = dtTextBookDetailData.Rows[i]["cContent"].ToString();
            }

            //影片
            else if (dtTextBookDetailData.Rows[i]["cMediaType"].ToString() == "Video")
            {
                vdUploadvideo.Attributes.Add("src", "~/AuthoringTool/CaseEditor/VirtualMicroscope/VMicroscope/VMicroscope.Web/UploadFiles/" + dtTextBookDetailData.Rows[i]["cContent"].ToString());
                lbVideoName.Text = dtTextBookDetailData.Rows[i]["cContent"].ToString();
                HiddenFieldVideo.Value = dtTextBookDetailData.Rows[i]["cContent"].ToString();
            }

            //聲音
            else if (dtTextBookDetailData.Rows[i]["cMediaType"].ToString() == "Audio")
            {
                audUploadAudio.Attributes.Add("src", "~/AuthoringTool/CaseEditor/VirtualMicroscope/VMicroscope/VMicroscope.Web/UploadFiles/" + dtTextBookDetailData.Rows[i]["cContent"].ToString());
                lbAudioName.Text = dtTextBookDetailData.Rows[i]["cContent"].ToString();
                HiddenFieldAudio.Value = dtTextBookDetailData.Rows[i]["cContent"].ToString();
            }
            //PDF
            //else if (dtTextBookDetailData.Rows[i]["cMediaType"].ToString() == "PDF")
            //{
            //    lbPDFName.Text = dtTextBookDetailData.Rows[i]["cContent"].ToString();
            //    HiddenFieldPDF.Value = dtTextBookDetailData.Rows[i]["cContent"].ToString();
            //}
            //文字
            else if (dtTextBookDetailData.Rows[i]["cMediaType"].ToString() == "Text")
            {
                tbxTextContent.Text = dtTextBookDetailData.Rows[i]["cContent"].ToString();
                lbTextName.Text = dtTextBookDetailData.Rows[i]["cContent"].ToString();
                tbxTextContent.Text = dtTextBookDetailData.Rows[i]["cContent"].ToString();
                tbxEditTextContent.Text = dtTextBookDetailData.Rows[i]["cContent"].ToString();
            }

             //Web
            else if (dtTextBookDetailData.Rows[i]["cMediaType"].ToString() == "Web")
            {
                tbxWeb.Text = dtTextBookDetailData.Rows[i]["cContent"].ToString();
                lbWebName.InnerText = dtTextBookDetailData.Rows[i]["cContent"].ToString();
                lbWebName.Attributes.Add("href", dtTextBookDetailData.Rows[i]["cContent"].ToString());
                lbWebName.Attributes.Remove("style");
                lbWebName.Attributes.Add("style", "font-size: larger; color: #0066FF");
            }
        }

    }

    //點選"+"新增新的TextBook的觸發
    void imbtnAdd_Click(object sender, EventArgs e)
    {
        ImageButton imbtnAddTemp = (ImageButton)sender;

        //將傳入的ID做切割 [0]imbtnAdd , [1]type , [2] recordID
        string[] strSpilt = imbtnAddTemp.ID.ToString().Split('_');

        //得到包dive的panel
        Panel ulYogeshTemp = (Panel)FindControl("ul_" + imbtnAddTemp.ID.ToString().Replace("imbtnAdd_", ""));

        //得到新的texBook是第幾個
        int count = 0;//計算現在到第幾個
        foreach (Control x in ulYogeshTemp.Controls)
        {
            if (x is HtmlGenericControl)
            {
                count++;
            }
        }


        //取得現在時間，用來做ID
        DateTime myDate = DateTime.Now;
        string date = myDate.ToString("yyyyMMddhhmmssfff");

        //新增的TextBook的div
        HtmlGenericControl divYogesh = new HtmlGenericControl("div");
        divYogesh.Attributes["style"] = "border:solid 1px black;border-collaspse:collapse;border-style: groove; border-width: thin; color: #FFFFFF; font-weight: 900;font-size: X-large;";
        //divYogesh.Attributes.Add("width", "1000px");
        divYogesh.Attributes.Add("class", "ui-state-default");
        divYogesh.ID = "div_" + strSpilt[2] + "_" + "TextBookID" + cUser + date;//ID為div+strRecordID+TextBookID(ex:ul_Record12315421_TextBookID201412241616524178830)

        Image imgMove = new Image();
        imgMove.ImageUrl = "~/AuthoringTool/CaseEditor/Paper/images/led-icons/triangles1.png";
        imgMove.ID = "imgMove_" + strSpilt[2] + "_" + "TextBookID" + cUser + date;
        //imgMove.Visible = false;
        //imgMove.Visible = false;

        divYogesh.Controls.Add(imgMove);

        LinkButton lBtn = new LinkButton();
        lBtn.ID = "lbtn_" + strSpilt[2] + "_" + "TextBookID" + cUser + date;
        if (strSpilt[1] == "Text")
            lBtn.Text = "說明" + (count).ToString();
        else if (strSpilt[1] == "Suggest")
            lBtn.Text = "提示" + (count).ToString();
        else
            lBtn.Text = "暗示" + (count).ToString();


        lBtn.Click += new EventHandler(lbtn_Click);
        lBtn.Enabled = true;
        divYogesh.Controls.Add(lBtn);

        tbxTextBookName.Text = lBtn.Text.ToString();
        ImageButton imbtnDel = new ImageButton();
        imbtnDel.ID = "imgbtnDel_" + strSpilt[2] + "_" + "TextBookID" + cUser + date;
        imbtnDel.ImageUrl = "~/AuthoringTool/CaseEditor/Paper/images/led-icons/remove.png";
        imbtnDel.ImageAlign = ImageAlign.Right;
        //imbtnDel.Visible = false;
        imbtnDel.Click += new ImageClickEventHandler(imbtnDel_Click);
        imbtnDel.Attributes.Add("onclick", "return confirm('確定刪除 ?')");

        /*imbtnDel.Width = 48;
        imbtnDel.Height = 48;*/
        divYogesh.Controls.Add(imbtnDel);

        //先刪除原先可以點擊新增的那個區塊
        HtmlGenericControl divAddYogeshTemp = (HtmlGenericControl)FindControl("div_ADD_" + imbtnAddTemp.ID.ToString().Replace("imbtnAdd_", ""));
        ClientScript.RegisterStartupScript(this.GetType(), "remove", "<script>$('#" + divAddYogeshTemp.ID.ToString() + "').remove();</script>");

        //加入新的textBook
        ulYogeshTemp.Controls.Add(divYogesh);

        //讀取這個record對應的ann
        DataTable dtAnn = getAnn(strSpilt[2]);
        string annID = dtAnn.Rows[0]["strAnnotationID"].ToString();
        //寫入DB
        insertTextBookData(strSpilt[2], "TextBookID" + cUser + date, strSpilt[1], (count).ToString(), lBtn.Text.ToString());

        //判斷這個Ann在對應表中是否存在
        DataTable dtCheck = checkTempTable(cQuestionID, strSpilt[2], annID);
        if (dtCheck.Rows.Count > 0)
        {
        }
        else
        {
            //寫入對應資料
            insertTextBookTemp(cQuestionID, strSpilt[2], annID);
        }

        //重新加入可以點擊新增Text的div
        HtmlGenericControl divAddYogesh = new HtmlGenericControl("div");
        divAddYogesh.Attributes["style"] = "border:solid 1px black;border-collaspse:collapse;border-style: dashed; text-align: center; border-width: thin; color: #FFFFFF; font-weight: 900;font-size: X-large;";
        divAddYogesh.ID = "div_ADD_" + imbtnAddTemp.ID.ToString().Replace("imbtnAdd_", "");//ID為div_ADD_+類型+TextBookID(ex:ul_Text_TextBookID201412241616524178830)

        //"+"按鈕
        ImageButton imbtnAdd = new ImageButton();
        imbtnAdd.ID = "imbtnAdd_" + imbtnAddTemp.ID.ToString().Replace("imbtnAdd_", "");
        imbtnAdd.ImageUrl = "~/AuthoringTool/CaseEditor/Paper/images/led-icons/add.png";
        imbtnAdd.Click += new ImageClickEventHandler(imbtnAdd_Click);

        divAddYogesh.Controls.Add(imbtnAdd);
        ulYogeshTemp.Controls.Add(divAddYogesh);

        //EventWhenSelectTextBook(lBtn.ID.ToString());


    }

    //另存新檔
    protected void SaveToOrtherStep_Click(object sender, EventArgs e)
    {
        DateTime myDate = DateTime.Now;
        string date = myDate.ToString("yyyyMMddhhmmssfff");

        HiddenFieldTextBook.Value = "TextBookID" + cUser + date;

        divSaveToStep.Style["display"] = "";
    }

    //取消
    protected void SaveToStepCancel_Click(object sender, EventArgs e)
    {
        HiddenFieldTextBook.Value = "0";
        divSaveToStep.Style["display"] = "none";
    }

    //另存新檔的確認
    protected void SaveToStepConfirm_Click(object sender, EventArgs e)
    {
        //得到包dive的panel
        Panel ulYogeshTemp = (Panel)FindControl("ul_" + ddlType.SelectedValue.ToString() + "_" + ddlNewStep.SelectedValue.ToString());

        //得到新的texBook是第幾個
        int count = 0;//計算現在到第幾個
        foreach (Control x in ulYogeshTemp.Controls)
        {
            if (x is HtmlGenericControl)
            {
                count++;
            }
        }

        DateTime myDate = DateTime.Now;
        string date = myDate.ToString("yyyyMMddhhmmssfff");

        string TextbookIDOfSaveTo = "TextBookID" + cUser + date;

        HiddenSaveToID.Value = TextbookIDOfSaveTo;

        insertTextBookData(ddlNewStep.SelectedValue.ToString(), HiddenSaveToID.Value.ToString(), ddlType.SelectedValue.ToString(), count.ToString(), tbxTextBookName.Text.ToString());

        btnSave_Click(sender, e);

        divSaveToStep.Style["display"] = "none";
        setStep();
    }

    //儲存至DB
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (HiddenFieldTextBook.Value.ToString() != "0")
        {
            try
            {
                DelTextBookDetailDataFromDB(HiddenFieldTextBook.Value.ToString());
                if (HiddenFieldImage.Value.ToString() != "")
                {
                    DateTime myDate = DateTime.Now;
                    string date = myDate.ToString("yyyyMMddhhmmssfff");

                    InsertTextBookDetailDataToDB("TextBookDataID" + date + "1", HiddenSaveToID.Value.ToString(), HiddenFieldImage.Value.ToString(), "Image");
                }

                if (HiddenFieldVideo.Value.ToString() != "")
                {
                    DateTime myDate = DateTime.Now;
                    string date = myDate.ToString("yyyyMMddhhmmssfff");

                    InsertTextBookDetailDataToDB("TextBookDataID" + date + "2", HiddenSaveToID.Value.ToString(), HiddenFieldVideo.Value.ToString(), "Video");
                }

                if (HiddenFieldAudio.Value.ToString() != "")
                {
                    DateTime myDate = DateTime.Now;
                    string date = myDate.ToString("yyyyMMddhhmmssfff");

                    InsertTextBookDetailDataToDB("TextBookDataID" + date + "3", HiddenSaveToID.Value.ToString(), HiddenFieldAudio.Value.ToString(), "Audio");
                }

                if (HiddenFieldPDF.Value.ToString() != "")
                {
                    DateTime myDate = DateTime.Now;
                    string date = myDate.ToString("yyyyMMddhhmmssfff");

                    InsertTextBookDetailDataToDB("TextBookDataID" + date + "4", HiddenSaveToID.Value.ToString(), HiddenFieldPDF.Value.ToString(), "PDF");
                }

                if (tbxEditTextContent.Text.ToString() != "")
                {

                    //文字
                    DateTime myDate2 = DateTime.Now;
                    string date2 = myDate2.ToString("yyyyMMddhhmmssfff");

                    InsertTextBookDetailDataToDB("TextBookDataID" + date2 + "5", HiddenSaveToID.Value.ToString(), tbxEditTextContent.Text.ToString(), "Text");
                }

                if (tbxWeb.Text.ToString() != "")
                {

                    //web
                    DateTime myDate2 = DateTime.Now;
                    string date2 = myDate2.ToString("yyyyMMddhhmmssfff");

                    InsertTextBookDetailDataToDB("TextBookDataID" + date2 + "6", HiddenSaveToID.Value.ToString(), tbxWeb.Text.ToString(), "Web");
                }

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('儲存完成！');</script>");
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('儲存失敗！');</script>");
            }

        }

        else
        {
            SaveToOrtherStep_Click(sender, e);
        }

    }




    #region 上傳的"匯入"按鈕觸發
    //匯入圖片按鈕的觸發
    protected void imageUploadFile_Click(object sender, EventArgs e)
    {
        String savePath = Server.MapPath("~/AuthoringTool/CaseEditor/VirtualMicroscope/VMicroscope/VMicroscope.Web/UploadFiles/");

        string image = "";
        Boolean fileOK = false;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();


        if (UploadImage.FileName.Length > 0)
        {
            String fileExtension =
            System.IO.Path.GetExtension(UploadImage.FileName).ToLower();
            String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    fileOK = true;
                }
            }
            if (fileOK)
            {
                try
                {
                    image = UploadImage.FileName.ToString();
                    UploadImage.SaveAs(savePath + image);
                    //設定照片的預覽
                    imgUpLoadImage.ImageUrl = "~/AuthoringTool/CaseEditor/VirtualMicroscope/VMicroscope/VMicroscope.Web/UploadFiles/" + image;
                    HiddenFieldImage.Value = image;
                    lbImageName.Text = image;
                    DateTime myDate = DateTime.Now;
                    string date = myDate.ToString("yyyyMMddhhmmssfff");

                    DelTextBookDetailContent(HiddenFieldTextBook.Value.ToString(), "Image");
                    InsertTextBookDetailDataToDB("TextBookDataID" + date + "1", HiddenFieldTextBook.Value.ToString(), HiddenFieldImage.Value.ToString(), "Image");

                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('圖片上傳錯誤！');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('圖片檔案格式錯誤！');</script>");
            }

        }
    }


    //匯入影片按鈕的觸發
    protected void videoUploadFile_Click(object sender, EventArgs e)
    {
        String savePath = Server.MapPath("~/AuthoringTool/CaseEditor/VirtualMicroscope/VMicroscope/VMicroscope.Web/UploadFiles/");

        string video = "";
        Boolean fileOK = false;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();


        if (VideoUpload.FileName.Length > 0)
        {
            String fileExtension =
            System.IO.Path.GetExtension(VideoUpload.FileName).ToLower();
            String[] allowedExtensions = { ".avi", ".mov", ".mp4", ".mpg", ".wmv" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    fileOK = true;
                }
            }
            if (fileOK)
            {
                try
                {
                    video = VideoUpload.FileName.ToString();
                    VideoUpload.SaveAs(savePath + video);
                    //設定影片的預覽
                    vdUploadvideo.Attributes.Remove("src");
                    vdUploadvideo.Attributes.Add("src", "~/AuthoringTool/CaseEditor/VirtualMicroscope/VMicroscope/VMicroscope.Web/UploadFiles/" + video);
                    HiddenFieldVideo.Value = video;
                    lbVideoName.Text = video;

                    DateTime myDate = DateTime.Now;
                    string date = myDate.ToString("yyyyMMddhhmmssfff");

                    DelTextBookDetailContent(HiddenFieldTextBook.Value.ToString(), "Video");
                    InsertTextBookDetailDataToDB("TextBookDataID" + date + "2", HiddenFieldTextBook.Value.ToString(), HiddenFieldVideo.Value.ToString(), "Video");


                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('影片上傳錯誤！');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('影片檔案格式錯誤！');</script>");
            }

        }
    }

    //匯入聲音按鈕的觸發
    protected void AudioUploadFile_Click(object sender, EventArgs e)
    {
        String savePath = Server.MapPath("~/AuthoringTool/CaseEditor/VirtualMicroscope/VMicroscope/VMicroscope.Web/UploadFiles/");

        string Audio = "";
        Boolean fileOK = false;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();


        if (AudioUpload.FileName.Length > 0)
        {
            String fileExtension =
            System.IO.Path.GetExtension(AudioUpload.FileName).ToLower();
            String[] allowedExtensions = { ".mp3", ".ogg", ".wav" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    fileOK = true;
                }
            }
            if (fileOK)
            {
                try
                {
                    Audio = AudioUpload.FileName.ToString();
                    AudioUpload.SaveAs(savePath + Audio);
                    //設定聲音的預覽
                    audUploadAudio.Attributes.Remove("src");
                    audUploadAudio.Attributes.Add("src", "~/AuthoringTool/CaseEditor/VirtualMicroscope/VMicroscope/VMicroscope.Web/UploadFiles/" + Audio);
                    HiddenFieldAudio.Value = Audio;
                    lbAudioName.Text = Audio;

                    DateTime myDate = DateTime.Now;
                    string date = myDate.ToString("yyyyMMddhhmmssfff");

                    DelTextBookDetailContent(HiddenFieldTextBook.Value.ToString(), "Audio");
                    InsertTextBookDetailDataToDB("TextBookDataID" + date + "3", HiddenFieldTextBook.Value.ToString(), HiddenFieldAudio.Value.ToString(), "Audio");

                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('音訊檔上傳錯誤！');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('音訊檔檔案格式錯誤！');</script>");
            }

        }
    }

    //匯入PDF按鈕的觸發
    protected void PDFUploadFile_Click(object sender, EventArgs e)
    {
        //String savePath = Server.MapPath("~/AuthoringTool/CaseEditor/VirtualMicroscope/VMicroscope/VMicroscope.Web/UploadFiles/");

        //string PDF = "";
        //Boolean fileOK = false;
        //System.Text.StringBuilder sb = new System.Text.StringBuilder();


        //if (PDFUpload.FileName.Length > 0)
        //{
        //    String fileExtension =
        //    System.IO.Path.GetExtension(PDFUpload.FileName).ToLower();
        //    String[] allowedExtensions = { ".pdf" };
        //    for (int i = 0; i < allowedExtensions.Length; i++)
        //    {
        //        if (fileExtension == allowedExtensions[i])
        //        {
        //            fileOK = true;
        //        }
        //    }
        //    if (fileOK)
        //    {
        //        try
        //        {
        //            PDF = PDFUpload.FileName.ToString();
        //            PDFUpload.SaveAs(savePath + PDF);
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('PDF檔上傳成功！');</script>");
        //            HiddenFieldPDF.Value = PDF;
        //            lbPDFName.Text = PDF;

        //        }
        //        catch (Exception ex)
        //        {
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('PDF檔上傳錯誤！');</script>");
        //        }
        //    }
        //    else
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('PDF檔案格式錯誤！');</script>");
        //    }

        //}
    }

    //web的textbox text change
    protected void tbxWeb_TextChanged(object sender, EventArgs e)
    {
        if (tbxWeb.Text.ToString() != "" && tbxWeb.Text.ToString() != " ")
        {
            DateTime myDate2 = DateTime.Now;
            string date2 = myDate2.ToString("yyyyMMddhhmmssfff");


            DelTextBookDetailContent(HiddenFieldTextBook.Value.ToString(), "Web");
            InsertTextBookDetailDataToDB("TextBookDataID" + date2 + "6", HiddenFieldTextBook.Value.ToString(), tbxWeb.Text.ToString(), "Web");
        }
        else//如果為空白就不加入，並刪除
        {
            DelTextBookDetailContent(HiddenFieldTextBook.Value.ToString(), "Web");
        }

    }

    //文字的textbox text change
    protected void tbxEditTextContent_TextChanged(object sender, EventArgs e)
    {
        if (tbxEditTextContent.Text.ToString() != "" && tbxEditTextContent.Text.ToString() != " ")
        {
            DateTime myDate2 = DateTime.Now;
            string date2 = myDate2.ToString("yyyyMMddhhmmssfff");
            tbxTextContent.Text = tbxEditTextContent.Text.ToString();

            DelTextBookDetailContent(HiddenFieldTextBook.Value.ToString(), "Text");
            InsertTextBookDetailDataToDB("TextBookDataID" + date2 + "5", HiddenFieldTextBook.Value.ToString(), tbxEditTextContent.Text.ToString(), "Text");
        }
        else//如果為空白就不加入，並刪除
        {
            DelTextBookDetailContent(HiddenFieldTextBook.Value.ToString(), "Text");
            tbxTextContent.Text = "";
        }
    }


    #endregion

    //教材名稱的的textbox text change
    protected void tbxTextBookName_TextChanged(object sender, EventArgs e)
    {
        UpdateTextBookDataName(HiddenFieldTextBook.Value.ToString(), tbxTextBookName.Text.ToString());
        LinkButton lbtn = (LinkButton)FindControl("lbtn_" + HiddenFieldSelectTextBook.Value.ToString());
        lbtn.Text = tbxTextBookName.Text.ToString();
        //HiddenFieldSelectTextBook.Value
    }

    //在移動textbook的順序後，重新儲存資訊
    protected void btnReUpdateOrder_Click(object sender, EventArgs e)
    {
        string[] strSpilt = HiddenFieldCurrentOrder.Value.ToString().Split('@');


        for (int i = 0; i < strSpilt.Length; i++)
        {
            if (!strSpilt[i].Contains("Add") && strSpilt[i] != "")
            {
                //取得textBook的ID , [0]div , [1]recordID , [2]textBookID
                string[] strTemp = strSpilt[i].Split('_');
                //修改在資料庫中的順序
                UpdateTextBookDataOrder(strTemp[2], (i + 1).ToString());
            }

        }

        setStep();
    }



    //移除的觸發 
    protected void imbtnDel_Click(object sender, EventArgs e)
    {

        ImageButton imbtnDelTemp = (ImageButton)sender;

        //[0]imbtnDel , [1]recordID , [2] textBookID
        string[] strSplit = imbtnDelTemp.ID.ToString().Split('_');


        //移除div
        HtmlGenericControl divDelYogeshTemp = (HtmlGenericControl)FindControl("div_" + imbtnDelTemp.ID.ToString().Replace("imgbtnDel_", ""));
        ClientScript.RegisterStartupScript(this.GetType(), "remove", "<script>$('#" + divDelYogeshTemp.ID.ToString() + "').remove();</script>");

        DataTable dtTextbookType = new DataTable();
        dtTextbookType = getTextBookType(strSplit[1], strSplit[2]);


        //得到包dive的panel
        Panel ulYogeshTemp = (Panel)FindControl("ul_" + dtTextbookType.Rows[0]["cType"].ToString() + "_" + strSplit[1]);

        //取得順序
        HiddenFieldCurrentOrder.Value = "";
        foreach (Control x in ulYogeshTemp.Controls)
        {
            if (x is HtmlGenericControl)
            {
                HiddenFieldCurrentOrder.Value = HiddenFieldCurrentOrder.Value.ToString() + x.ID.ToString() + "@";
            }
        }

        int count = 1;//計算現在到第幾個
        string[] strTextBookOrder = HiddenFieldCurrentOrder.Value.ToString().Split('@');
        //更新textbook的順序
        for (int i = 0; i < strTextBookOrder.Length; i++)
        {
            if (strTextBookOrder[i].Replace("div_", "").Replace(strSplit[1] + "_", "") != "" && strTextBookOrder[i].Replace("div_", "").Replace(strSplit[1] + "_", "") != strSplit[2])//要存入的須不為空且不等於要被刪除的
            {
                UpdateTextBookDataOrder(strTextBookOrder[i].Replace("div_", "").Replace(strSplit[1] + "_", ""), count.ToString());
                count++;
            }
        }

        DelTextBookDataFromDB(strSplit[2]);
        DelTextBookDetailDataFromDB(strSplit[2]);

    }

    //移除頁面上的檔案(圖、影片、聲音、PDF)
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        Button btnTemp = (Button)sender;

        if (btnTemp.ID.ToString().Replace("btnRemove", "") == "Image")
        {
            DelTextBookDetailContent(HiddenFieldTextBook.Value.ToString(), "Image");
            lbImageName.Text = "請點選編輯新增圖片";
            imgUpLoadImage.ImageUrl = "";
            HiddenFieldImage.Value = "";
        }
        else if (btnTemp.ID.ToString().Replace("btnRemove", "") == "Video")
        {
            DelTextBookDetailContent(HiddenFieldTextBook.Value.ToString(), "Video");
            lbVideoName.Text = "請點選編輯新增影片";
            vdUploadvideo.Attributes.Remove("src");
            HiddenFieldVideo.Value = "";
        }

        else if (btnTemp.ID.ToString().Replace("btnRemove", "") == "Audio")
        {
            DelTextBookDetailContent(HiddenFieldTextBook.Value.ToString(), "Audio");
            lbAudioName.Text = "請點選編輯新增音訊檔";
            audUploadAudio.Attributes.Remove("src");
            HiddenFieldAudio.Value = "";
        }

        //else if (btnTemp.ID.ToString().Replace("btnRemove", "") == "PDF")
        //{
        //    lbPDFName.Text = "請點選編輯新增PDF";
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('PDF已刪除！');</script>");
        //    HiddenFieldPDF.Value = "";
        //}
    }

    //頁面上點選"編輯"的觸發顯示可以上傳的物件(圖、影片、聲音、PDF)
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (lbtnEdit.Text.ToString() == "編輯")
        {
            HiddenFieldEdit.Value = "1";
            setIsEditState();
        }
        else
        {
            HiddenFieldEdit.Value = "0";
            setIsEditState();
        }
    }

    //---------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------SQL-----------------------------------------------------------------------------

    //得到題目名稱
    protected DataTable getQuestionName(string cQuestionID)
    {
        DataTable dt = new DataTable();
        string strSQL = "SELECT * FROM QuestionIndex  A  WHERE A.cQID ='" + cQuestionID + "'";
        dt = sqldb.getDataSet(strSQL).Tables[0];

        return dt;
    }

    //得到textbook的資訊
    protected DataTable getTempData()
    {
        DataTable dt = new DataTable();
        string strSQL = "SELECT * FROM VRTextBookTemp WHERE  QID ='" + cQuestionID + "'";
        dt = sqldb.getDataSet(strSQL).Tables[0];

        return dt;
    }

    //得到標準步驟的資訊
    protected DataTable getStepData(string cQuestionID)
    {
        DataTable dt = new DataTable();
        string strSQL = "SELECT * FROM ItemForVRAuthoringStepsList  A , ItemForVRStepRecords S  WHERE A.cQuestionID ='" + cQuestionID + "' AND A.strStepID = S.strStepID";
        dt = sqldb.getDataSet(strSQL).Tables[0];

        return dt;
    }

    //得到標準步驟的資訊
    protected DataTable getAnn(string recordID)
    {
        DataTable dt = new DataTable();
        string strSQL = "SELECT * FROM  ItemForVRStepRecords S  WHERE strRecordID ='" + recordID + "'";
        dt = sqldb.getDataSet(strSQL).Tables[0];

        return dt;
    }


    //得到textbook的資訊
    protected DataTable getTextBookName(string textbook)
    {
        DataTable dt = new DataTable();
        string strSQL = "SELECT * FROM VRTextBookContent C WHERE  C.cTextBookID ='" + textbook + "'";
        dt = sqldb.getDataSet(strSQL).Tables[0];

        return dt;
    }

    //得到textbook的資訊
    protected DataTable checkTempTable(string QID, string Rid, string Aid)
    {
        DataTable dt = new DataTable();
        string strSQL = "SELECT * FROM VRTextBookTemp  WHERE  QID ='" + QID + "' AND Rid = '" + Rid + "' AND Aid ='" + Aid + "'";
        dt = sqldb.getDataSet(strSQL).Tables[0];

        return dt;
    }
    //得到Record中有哪些教材
    protected DataTable getTextBookData(string strRecordID, string cType)
    {
        DataTable dt = new DataTable();
        string strSQL = "SELECT * FROM VRTextBookContent C WHERE  C.strRecordID ='" + strRecordID + "' AND C.cType = '" + cType + "' ORDER BY  C.cOrder";
        dt = sqldb.getDataSet(strSQL).Tables[0];

        return dt;
    }

    //得到textBook在Record中類型
    protected DataTable getTextBookType(string strRecordID, string textBookID)
    {
        DataTable dt = new DataTable();
        string strSQL = "SELECT * FROM VRTextBookContent C WHERE  C.strRecordID ='" + strRecordID + "' AND C.cTextBookID = '" + textBookID + "'";
        dt = sqldb.getDataSet(strSQL).Tables[0];

        return dt;
    }

    //得到各個TextBook的詳細資訊(圖、影片、聲音、PDF、文字)
    protected DataTable getTextBookDetailData(string cTextBookID)
    {
        DataTable dt = new DataTable();
        string strSQL = "SELECT * FROM VRTextBookDetailData D WHERE  D.cTextBookID ='" + cTextBookID + "'";
        dt = sqldb.getDataSet(strSQL).Tables[0];

        return dt;
    }



    //新增一筆textBook的資訊至資料表VRTextBookContent中      
    protected void insertTextBookData(string RecordID, string TextBookID, string Type, string Order, string TextBookName)
    {
        //新增一筆textBook的資訊至資料表VRTextBookContent中      
        string strSQL = "INSERT INTO VRTextBookContent VALUES('" + cQuestionID + "','" + RecordID + "','" + cUser + "','" + TextBookID + "','" + Type + "','" + Order + "','" + TextBookName + "')";
        try
        {
            sqldb.ExecuteNonQuery(strSQL);
        }
        catch
        {
            //this.RegisterClientScriptBlock("", "<script> alert('error'); </script>");
        }
    }

    protected void insertTextBookTemp(string QID, string RecordID, string AnnID)
    {
        //新增一筆textBook的資訊至資料表VRTextBookContent中      
        string strSQL = "INSERT INTO VRTextBookTemp VALUES('" + QID + "','" + RecordID + "','" + AnnID + "')";
        try
        {
            sqldb.ExecuteNonQuery(strSQL);
        }
        catch
        {
            //this.RegisterClientScriptBlock("", "<script> alert('error'); </script>");
        }
    }

    //更新在資料表VRTextBookContent中Order      
    protected void UpdateTextBookDataOrder(string textBookID, string NewOrder)
    {

        string strSQL = "UPDATE VRTextBookContent  SET cOrder = " + NewOrder + " WHERE cQID = '" + cQuestionID + "' AND cUserID = '" + cUser + "' AND cTextBookID = '" + textBookID + "';";
        try
        {
            sqldb.ExecuteNonQuery(strSQL);
        }
        catch
        {
            //this.RegisterClientScriptBlock("", "<script> alert('error'); </script>");
        }
    }

    //更新在資料表VRTextBookContent中名稱     
    protected void UpdateTextBookDataName(string textBookID, string newName)
    {

        string strSQL = "UPDATE VRTextBookContent  SET cTextbookName = '" + newName + "' WHERE cQID = '" + cQuestionID + "' AND cUserID = '" + cUser + "' AND cTextBookID = '" + textBookID + "';";
        try
        {
            sqldb.ExecuteNonQuery(strSQL);
        }
        catch
        {
            //this.RegisterClientScriptBlock("", "<script> alert('error'); </script>");
        }
    }
    //新增textBook中的各種詳細資料(圖、影片、聲音、PDF、文字)    
    protected void InsertTextBookDetailDataToDB(string textBookDataID, string textBookID, string Content, string type)
    {

        string strSQL = "INSERT INTO VRTextBookDetailData VALUES('" + textBookDataID + "','" + textBookID + "','" + Content + "','" + type + "')";
        try
        {
            sqldb.ExecuteNonQuery(strSQL);
        }
        catch
        {
            //this.RegisterClientScriptBlock("", "<script> alert('error'); </script>");
        }
    }

    //刪除textBook中的各種詳細資料(圖、影片、聲音、PDF、文字)    
    protected void DelTextBookDetailDataFromDB(string textBookID)
    {

        string strSQL = "DELETE VRTextBookDetailData WHERE cTextBookID = '" + textBookID + "'";
        try
        {
            sqldb.ExecuteNonQuery(strSQL);
        }
        catch
        {
            //this.RegisterClientScriptBlock("", "<script> alert('error'); </script>");
        }
    }

    //刪除textBook中的某種類別的詳細資料(圖、影片、聲音、PDF、文字)    
    protected void DelTextBookDetailContent(string textBookID, string type)
    {

        string strSQL = "DELETE VRTextBookDetailData WHERE cTextBookID = '" + textBookID + "' AND cMediaType = '" + type + "'";
        try
        {
            sqldb.ExecuteNonQuery(strSQL);
        }
        catch
        {
            //this.RegisterClientScriptBlock("", "<script> alert('error'); </script>");
        }
    }

    //移除TextBook     
    protected void DelTextBookDataFromDB(string textBookID)
    {

        string strSQL = "DELETE VRTextBookContent WHERE cTextBookID = '" + textBookID + "'";

        try
        {
            sqldb.ExecuteNonQuery(strSQL);
        }
        catch
        {
            //this.RegisterClientScriptBlock("", "<script> alert('error'); </script>");
        }
    }

    protected void btCancelEdit_Click(object sender, EventArgs e)
    {
        Page.RegisterStartupScript("js", "<script language=\"javascript\">showVMWindow()</script>");
    }

    protected void btFinishEdit_Click(object sender, EventArgs e)
    {
        Page.RegisterStartupScript("js", "<script language=\"javascript\">showVMWindow()</script>");
    }

}