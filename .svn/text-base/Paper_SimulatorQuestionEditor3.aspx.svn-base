<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeFile="Paper_SimulatorQuestionEditor3.aspx.cs" Inherits="AuthoringTool_CaseEditor_Paper_Paper_SimulatorQuestionEditor3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Simulator Question Editor</title>
    <script type="text/javascript" language="javascript">    	

    	function OpenHTMLEditor(temp) {

//    	    var obj = document.getElementById(temp);
//    	    var ans = document.getElementById("hf_QID").value;
    	    //    	    obj.focus();
    	    window.open("../LaboratoryTest/EditArea.aspx?cAnsID=" + temp, "_blank", "left=0,top=0,scrollbars,resizable=0,width=" + screen.availWidth + ",height=400px");
				//window.showModalDialog("../LaboratoryTest/EditArea.aspx?cAnsID=" + ans, obj, "dialogWidth:" + document.body.clientWidth);
    	    //window.showModalDialog("../LaboratoryTest/EditArea.htm?cAnsID=" + ans, obj, "dialogWidth:" + document.body.clientWidth);	
			event.returnValue = false;
		}
    </script>
</head>
<body>
   
    <form id="form1" runat="server">
    <div>  
    <asp:HiddenField ID="hf_img" runat="server" Value="" />
                <asp:HiddenField ID="hf_Title" runat="server" Value="" />
                <asp:HiddenField ID="hf_Delte" runat="server" Value="" />
                <asp:HiddenField ID="hf_QID" runat="server" Value="" />
    </div>
    <div> <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
  <ContentTemplate>
            <asp:Label ID="Label2" runat="server" Text="Step 3:請設定答案" Font-Size="20pt"></asp:Label><br/>
            <div align=center>
            <asp:Image ID="Img_simulator" runat="server" Height="420" Width="600" /><br/>
        </div>
       <table width=95%>

        <tr><td valign=top >
        <asp:Label ID="Label3" runat="server" Text="已設定過的答案列表："></asp:Label>
           </td><td>
           <asp:Panel ID="pl_ans" runat="server" Width="100%">           
        </asp:Panel></td>
    </tr>
           <caption><hr />
          <tr>
                   <td align="right" colspan="2">
               <hr />
                            </td>
               </tr>
               <tr>
                   <td width="15%">
                       <asp:Label ID="Label1" runat="server" Text="請設定答案及其順序性："></asp:Label>
                   </td>
                   <td width="85%">
                       <asp:RadioButton ID="RB1" runat="server" AutoPostBack="True" Checked="true" 
                           GroupName="order" oncheckedchanged="RB1_CheckedChanged" Text="有順序性" />
                       &nbsp;&nbsp;<asp:RadioButton ID="RB2" runat="server" AutoPostBack="True" 
                           GroupName="order" oncheckedchanged="RB2_CheckedChanged" Text="無順序性" />
                   </td>
               </tr>
               <tr>
                   <td valign="top">
                       <asp:Label ID="Label4" runat="server" Text="現有的按鈕列表："></asp:Label>
                   </td>
                   <td>
                       <asp:Panel ID="PL_table" runat="server">
                       </asp:Panel>
                   </td>
               </tr>
               <tr>
                   <td align="right" colspan="2">
                       <asp:Label ID="Label5" runat="server" style="vertical-align:bottom;" 
                           Text="儲存後的答案會出現在答案列表中"></asp:Label>
                       &nbsp;
                       <asp:Button ID="Btn_new" runat="server" onclick="Btn_new_Click" Text="儲存答案" 
                           Width="80" />
                       &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btn_reset" runat="server" onclick="btn_reset_Click" 
                           Text="重設按鈕列表" />
                       <hr />
                   </td>
               </tr>
               <tr>
                   <td align="right" colspan="2">
                       <asp:Button ID="Btn_back" runat="server" onclick="Btn_back_Click" Text="回上一步" 
                           Width="80" />
                       &nbsp;&nbsp;&nbsp;&nbsp;
                       <asp:Button ID="Btn_finish" runat="server" onclick="Btn_finish_Click" Text="完成" 
                           Width="80" />
                   </td>
               </tr>
           </caption>
       </table>
       </ContentTemplate>
           </asp:UpdatePanel>
              
    </div>
    
    </form>
</body>
</html>
