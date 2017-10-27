using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class AuthoringTool_CaseEditor_Paper_CommonQuestionEdit_Page_EditRationale : AuthoringTool_BasicForm_BasicForm
{
    protected string CaseID = "";
    protected string SectionName = "Diagnosis";
    protected string UserID = "";
    protected string ClinicNum = "";
    protected string SelectionID = "";
    protected string query = "";
    protected Hints.DB.clsHintsDB mydb = new Hints.DB.clsHintsDB();

    protected void Page_Load(object sender, EventArgs e)
    {
        Initiate();
        CaseID = usi.CaseID;
        SectionName = usi.Section;
        ClinicNum = usi.ClinicNum.ToString();
        SelectionID = Request.QueryString["SelectionID"];
        hfQIDandASeq.Value = SelectionID;
        hfCaseID.Value = usi.CaseID;

        if (!this.IsPostBack)
        {
            txtData.Text = GetRationale(CaseID, ClinicNum, SectionName, SelectionID);
        }
        else
        {
            string strValue = txtData.Text;
            strValue = strValue.Replace("'", "''");
            DelRationale(CaseID, ClinicNum, SectionName, SelectionID);
            AddRationale(CaseID, ClinicNum, SectionName, SelectionID, strValue);

            // Close window
            Page.RegisterStartupScript("close", "<script language='javascript'>window.close();</script>");
        }
    }

    public string GetRationale(string CaseID, string ClinicNum, string SectionName, string SelectionID)
    {
        query = "SELECT * FROM RationaleForExam WHERE cCaseID='" + CaseID + "' AND iClinicNum=" + ClinicNum +
                " AND cSectionName='" + SectionName + "' AND cExamID='" + SelectionID + "'";
        try
        {
            return mydb.getDataSet(query).Tables[0].Rows[0]["cRationale"].ToString();
        }
        catch
        {
            return "";
        }
    }

    // 新增一筆rationale
    public bool AddRationale(string CaseID, string ClinicNum, string SectionName, string SelectionID, string Content)
    {
        query = "Insert Into RationaleForExam Values('" + CaseID + "'," + ClinicNum + ",'" + SectionName + "','" + SelectionID + "','" + Content + "')";
        try
        {
            mydb.ExecuteNonQuery(query);
            return true;
        }
        catch
        {
            return false;
        }
    }

    // 刪除一筆rationale
    public bool DelRationale(string CaseID, string ClinicNum, string SectionName, string SelectionID)
    {
        query = "DELETE RationaleForExam WHERE cCaseID='" + CaseID + "' AND iClinicNum=" + ClinicNum +
                " AND cSectionName='" + SectionName + "' AND cExamID='" + SelectionID + "'";
        try
        {
            mydb.ExecuteNonQuery(query);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
