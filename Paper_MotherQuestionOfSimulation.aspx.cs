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
using System.Drawing;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Drawing2D;

public partial class AuthoringTool_CaseEditor_Paper_Paper_MotherQuestionOfSimulation : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    { 
        
     

    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        int height = 500;
        int width = 500;
        Random r = new Random();
        int x = r.Next(75);

        Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
        Graphics g = Graphics.FromImage(bmp);
       
        g.TextRenderingHint = TextRenderingHint.AntiAlias;
        g.Clear(Color.Black);
        
        Pen snowPen = new Pen(Color.Black, 4);
        Rectangle rct = new Rectangle();
        rct.Width = 400;
        rct.Height = 400;
        rct.X = 0;
        rct.Y = 0;       
        
        g.DrawRectangle(snowPen, rct);
        
        Point p_x = new Point(100);
        Point p_y = new Point(200);
        g.DrawLine(snowPen, p_x, p_y);

        bmp.Save(Response.OutputStream, ImageFormat.Jpeg);
  
        g.Dispose();
        bmp.Dispose();
        Response.End();
    }
}
