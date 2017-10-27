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
using Hints.DB;
using Hints.DB.QuestionGroup;
using Hints.HintsUtility;
using Hints;

public partial class AuthoringTool_CaseEditor_Paper_Feature_EditFeatureItem : AuthoringTool_BasicForm_BasicForm
{
    protected string strSQL = "";
    protected clsHintsDB hintsDB = new clsHintsDB();
    protected String strNodeID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strNodeID = Request.QueryString["nodeID"];
        //更換tittle名稱
        set_tittle();
        
        if (!IsPostBack)
        {
            //初始化set_lsb_FeatureItem
            set_lsb_FeatureItem();
        }
    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        try
        {
            strSQL = "DELETE FeaturevalueItem WHERE iFeatureNum = '" + lsb_FeatureItem.SelectedItem.Value + "'";
            hintsDB.ExecuteNonQuery(strSQL);
        }
        catch 
        {
            Response.Write("<script>window.alert('請選擇一個特徵值')</script>");
        }
        //初始化set_lsb_FeatureItem
        set_lsb_FeatureItem();
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        //新增特徵值至資料表中      
        strSQL = "INSERT INTO FeaturevalueItem VALUES('" + txt_FeatureName.Text.ToString() + "','" + usi.UserID.ToString() + "','" + strNodeID + "')";
        try
        {
            hintsDB.ExecuteNonQuery(strSQL);
        }
        catch
        {
            this.RegisterClientScriptBlock("", "<script> alert('出現重複的特徵值名稱'); </script>");
        }       
        //清空txt_FeatureName
        txt_FeatureName.Text = "";
        //初始化set_lsb_FeatureItem
        set_lsb_FeatureItem();
    }
    protected void set_tittle()
    {
        DataTable dbTittle = new DataTable();
        strSQL = "SELECT cNodeName FROM FeaturevalueTree  WHERE (cNodeID = '" + strNodeID + "') ";
        dbTittle = hintsDB.getDataSet(strSQL).Tables[0];
        lbTittle.Text = dbTittle.Rows[0]["cNodeName"].ToString();    
    }
    
    protected void set_lsb_FeatureItem()
    {
        //先清空lsbFeatureItem
        lsb_FeatureItem.Items.Clear();
        //從資料庫中抓取符合此節點的特徵值
        DataTable dtItem = new DataTable();
        strSQL = "SELECT     iFeatureNum, sFeatureName FROM FeaturevalueItem  WHERE (cNodeID = '" + strNodeID + "') ORDER BY iFeatureNum";
        dtItem = hintsDB.getDataSet(strSQL).Tables[0];
        if (dtItem.Rows.Count > 0)
            for (int i = 0; i < dtItem.Rows.Count; i++)
                lsb_FeatureItem.Items.Add(new ListItem(dtItem.Rows[i]["sFeatureName"].ToString(), dtItem.Rows[i]["iFeatureNum"].ToString()));
    
    
    }

    protected void btModifyGroupSubmit_Click(object sender, EventArgs e)
    {
        strSQL = "UPDATE FeaturevalueItem SET sFeatureName='" + txtNewFeatureName.Value + "' WHERE iFeatureNum='" + lsb_FeatureItem.SelectedItem.Value + "'";
        try
        {
            hintsDB.ExecuteNonQuery(strSQL);
        }
        catch
        {
            
        }
        //初始化set_lsb_FeatureItem
        set_lsb_FeatureItem();   
        
    }
}
