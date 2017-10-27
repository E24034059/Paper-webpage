<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Paper_SimulatorQuestionEditor2.aspx.cs" Inherits="AuthoringTool_CaseEditor_Paper_Paper_SimulatorQuestionEditor2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Simulator Question Editor</title>
</head>
<body>
    <form id="form1" runat="server">
    
     <asp:Label ID="Label2" runat="server" Text="Step 2:設定題目內容" Font-Size="20pt"></asp:Label><br/>
     <table width="98%">
     <tr><td align="center">  
        <asp:Image ID="Img_simulator" runat="server" Height="420" width="600"/></td></tr>
       <tr><td> 
         <asp:Label ID="Label1" runat="server" Text="題目內容：" ></asp:Label> </td></tr>
          <tr><td align="center">  
        <asp:TextBox ID="TB_SimulatorTitle" runat="server" Width="98%" Height="200px" Font-Size="20pt"></asp:TextBox></td></tr>
         <tr><td align="right">  
        <asp:Button ID="Btn_back" runat="server" Text="回上一步" onclick="Btn_back_Click"  />&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Button ID="Btn_next" runat="server" Text="下一步" onclick="Btn_next_Click"  />&nbsp;&nbsp;</td></tr>
        </table><asp:HiddenField
            ID="hf_img" runat="server" Value="" />
            <asp:HiddenField ID="hf_Title" runat="server" Value="" />
            <asp:HiddenField ID="hf_QID" runat="server" Value="" />
    
    </form>
</body>
</html>
