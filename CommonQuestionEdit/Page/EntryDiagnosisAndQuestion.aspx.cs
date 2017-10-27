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

namespace AuthoringTool.CaseEditor.DiagnosisAndQuestion
{
    public partial class EntryDiagnosisAndQuestion : AuthoringTool_BasicForm_BasicForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Response.Redirect("ShowQuestion.aspx?PreviousPage=entrydiagnosisandquestion");
        }

        private void TestFunction()
        {
            //usi.UserID = "alivs";
            //usi.CaseID = "doulpCase200410191609443593750";
            //usi.Section = "Diagnosis";
            //usi.ClinicNum = 1;
        }
    }
}
