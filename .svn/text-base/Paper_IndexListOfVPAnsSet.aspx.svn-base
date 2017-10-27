<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Paper_IndexListOfVPAnsSet.aspx.cs" Inherits="PaperSystem.Paper_IndexListOfVPAnsSet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Paper_IndexListOfVPAnsSet</title>

    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

    <script language="javascript">
        function setAnswerTypeID(strQID) {
            var selectedRB = "";
            var rbAnswerType = document.getElementsByName("rbAnswerTypeGroup_" + strQID);
            alert(rbAnswerType);
            var rbAnswerTypeCount = rbAnswerType.length;
            for (var i = 0; i < rbAnswerTypeCount; i++) {
                var currentElement = rbAnswerType[i];
                if (currentElement.checked) {
                    selectedRB = currentElement.value;
                    document.getElementById("hfAnswerTypeID").value = selectedRB.split('|')[2];
                    break;
                }
            }
        }

        function ClosePage() {      
            //window.opener.location.reload(true);
            var strSplit = window.opener.location.href.split('SelectedIndex=');
            window.opener.location = strSplit[0] + "&SelectedIndex=" + document.getElementById('hfSelectedIndex').value;
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" method="post" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
     </asp:ScriptManager>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
       <ContentTemplate>
        <table id="body_content" style="width: 99%;" align="center" runat="server">
         <tr>
            <td align="center" class="title">
                Conversation Question：<asp:Label ID="LbCurrentConQues" runat="server" Text=""></asp:Label>
            </td>
         </tr>
         <tr id="trFunctionList">
          <td id="tcFunctionList" align="center" class="title">
            <span id="spanFunctionList">Virtual Person's Answer Set</span>
          </td>
         </tr>
         <tr style="display:none;">
          <td>
            <asp:Label ID="LbCurrentProType" runat="server" Text=" Current Problem Type：" Font-Size="Larger"></asp:Label>
            <asp:Label ID="LbCurrentSymptoms" runat="server" Font-Size="Larger" ForeColor="Red"></asp:Label>
          </td>
         </tr>
         <tr id="trIndexList" runat="server">
          <td id="tcIndexList" runat="server">
          </td>
         </tr>
         <tr>
          <td align="right">
            <hr />
            <asp:Button ID="btAddQuestion" runat="server" Text="Add a new VP Answer" Style="cursor:pointer;" CssClass="button_continue"
             Height="30px" OnClick="btAddVPAnswer_Click" />
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnClose" runat="server"  style="width: 80px; height: 30px; cursor:pointer;" 
             Text="Finish" CssClass="button_continue" OnClientClick="ClosePage" />
            <input id="btModify" type="button" value="" class="button_continue" runat="server"
             style="display: none" />
           </td>
          </tr>
        </table>
        <asp:HiddenField ID="hfAnswerTypeID" runat="server" />
        <asp:HiddenField ID="hfSelectedIndex" runat="server" />
       </ContentTemplate>
      </asp:UpdatePanel>
    </form>
</body>
</html>
