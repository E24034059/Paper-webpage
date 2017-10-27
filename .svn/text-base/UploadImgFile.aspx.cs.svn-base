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
	/// UploadImgFile 的摘要描述。
	/// </summary>
    public partial class UploadImgFile : AuthoringTool_BasicForm_BasicForm
	{
		string strCaseID , strDivisionID , strClinicNum , strSectionName , strUserID , strPaperID;

		//Setup objects
		SqlDB sqldb = new SqlDB(System.Configuration.ConfigurationSettings.AppSettings["connstr"]);
		DataReceiver myReceiver = new DataReceiver();

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.Initiate();

			//接收參數
			this.getParameter();

			//加入事件
            btnUpload.ServerClick += new EventHandler(btnUpload_ServerClick);
		}

		private void getParameter()
		{
			//UserID
			if(Session["UserID"] != null)
			{
				strUserID = Session["UserID"].ToString();
			}
			//strUserID = "swakevin";
			
			//CaseID
			if(Session["CaseID"] != null)
			{
				strCaseID = Session["CaseID"].ToString();
			}
			
			//Division
			if(Session["DivisionID"] != null)
			{
				strDivisionID = Session["DivisionID"].ToString();
			}
			
			//ClinicNum
			if(Session["ClinicNum"] != null)
			{
				strClinicNum = Session["ClinicNum"].ToString();
			}
			
			//SectionName
			if(Session["SectionName"] != null)
			{
				strSectionName = Session["SectionName"].ToString();
			}

			//PaperID
			if(Session["PaperID"] != null)
			{
				strPaperID = Session["PaperID"].ToString();
			}
			else
			{
				SQLString mySQL = new SQLString();
				//strPaperID = mySQL.getPaperIDFromCase(strCaseID , strClinicNum.ToString() , strSectionName);
			}
			//strPaperID = "wyt20060510150619";
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

        void btnUpload_ServerClick(object sender, EventArgs e)
        {
            if (fileinput.PostedFile != null)
            {
                //確認上傳的檔案型態是已知的Image(bmp,jpg,png......)

                //建立檔名
                string strFileName = strUserID + "-" + myReceiver.getNowTime();

                //取得Case的資料
                string strSQL = "";
                SQLString mySQL = new SQLString();
                strSQL = mySQL.getCaseFolder(strCaseID);
                DataSet dsCase = sqldb.getDataSet(strSQL);
                if (dsCase.Tables[0].Rows.Count > 0)
                {

                    //檔案儲存資料夾的路徑
                    //string strFolder = @"C:\WebShare\HintsData\HintsCase\VideoFileOfPathologyCut\";
                    string strFolder = "";
                    try
                    {
                        strFolder = dsCase.Tables[0].Rows[0]["cURL"].ToString();
                    }
                    catch
                    {
                    }

                    //此Case所屬的Division

                    try
                    {
                        strDivisionID = dsCase.Tables[0].Rows[0]["cDivisionID"].ToString();
                    }
                    catch
                    {
                    }

                    //取得此Case的Server
                    string strServer = DataReceiver.getDomainNameBySplitingURL(this);

                    dsCase.Dispose();

                    //取得ContentType
                    string strContentType = "";
                    try
                    {
                        strContentType = fileinput.PostedFile.ContentType;
                    }
                    catch
                    {
                    }

                    //依照ContentType建立不同的完整路徑
                    switch (strContentType)
                    {
                        case "video/x-ms-wmv":
                            strFileName += ".wmv";
                            break;
                        case "audio/wav":
                            strFileName += ".wav";
                            break;
                        case "video/mpeg":
                            strFileName += ".mpeg";
                            break;
                        case "video/avi":
                            strFileName += ".avi";
                            break;
                        case "video/x-ms-asf":
                            strFileName += ".asf";
                            break;
                        case "image/gif":
                            strFileName += ".gif";
                            break;
                        case "image/jpg":
                            strFileName += ".jpg";
                            break;
                        case "image/jpeg":
                            strFileName += ".jpg";
                            break;
                        case "image/pjpeg":
                            strFileName += ".jpg";
                            break;
                        case "image/bmp":
                            strFileName += ".bmp";
                            break;
                        case "image/x-png":
                            strFileName += ".png";
                            break;
                        default:
                            strFileName += ".jpg";
                            break;
                    }

                    //建立完整檔案URL******注意!!下方路徑中的WebShare在上傳至Server端時要記得修改成Web_Share******
                    string strFileURL = "";
                    try
                    {
                        strFileURL = @"C:\Web_Share\HintsData" + strFolder + @"\" + strFileName;
                        fileinput.PostedFile.SaveAs(strFileURL);
                    }
                    catch
                    {
                        strFileURL = "";
                        strFileURL = @"D:\Web_Share\HintsData" + strFolder + @"\" + strFileName;
                        fileinput.PostedFile.SaveAs(strFileURL);
                    }

                    //把Server端的檔案位置轉換成網頁的相對位置
                    string strImgSrc = "";
                    strFolder = strFolder.Replace(@"\", "/");
                    strImgSrc = "http://" + strServer + strFolder + "/" + strFileName;

                    strImgSrc = "<IMG src=" + strImgSrc + ">";

                    //關閉此網頁，並將上傳的圖片加入MMD的imgDiv中。(傳入Server位置的網址)
                    string strScript = "<script language='javascript'>\n";
                    strScript += "closePage('" + strImgSrc + "')\n";
                    strScript += "</script>\n";
                    Page.RegisterStartupScript("closePage", strScript);

                }
                else
                {
                    //此Case沒有資料
                }
            }
        }
	}
}
