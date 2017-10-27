<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditTestPaper_VPAns.aspx.cs" Inherits="AuthoringTool_CaseEditor_Paper_EditTestPaper_VPAns" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

 <style type="text/css">
        input.bigcheck
        {
            height: 30px;
            width: 30px;
            cursor: pointer;
        }

        table {
			border-collapse:collapse;
		}
		table tr.border td {
			border-top: Solid 1px Black;
			border-bottom: Solid 1px Black;
		}			
		.border .left {
			border-left: Solid 1px Black;
		}		
		.border .right {
			border-right: Solid 1px Black;
		}
        .border .both {
            border-left: Solid 1px Black;
			border-right: Solid 1px Black;
		}
        table tr.borderBottom td{
			border-bottom: Solid 1px Black;
		}
        .borderBottom .left {
			border-left: Solid 1px Black;
		}		
		.borderBottom .right {
			border-right: Solid 1px Black;
		}
</style>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>基本版題目表-虛擬人回應編輯</title>
</head>
<script type="text/javascript">
        function BrowseConversation(strQID, strGroupID, strCareer, ProType) {
            window.open("Paper_ConversationQuestionEditor.aspx?QID=" + strQID + "&AnswerType=1&GroupID=" + strGroupID + "&Career=" + strCareer + "&bModify=True&Browse=" + ProType, '_blank', 'directories=0, height=600, menubar=0, resizable=1, scrollbars=1, status=0, titlebar=1, toolbar=0, width=700');
        }

        function BrowseVPAnswer(ProType, strResponse, strVPAID) {
            window.open("Paper_VPAnswerSetEditor.aspx?CurrentProType=" + ProType + "&VPResponseType=" + strResponse + "&VPAID=" + strVPAID + "&Browse=1", '_blank', 'directories=0, height=600, menubar=0, resizable=1, scrollbars=1, status=0, titlebar=1, toolbar=0, width=700');
        }

        function SaveSelected(strGroupID, strMainTopic, strcbID) {

            var Temp = document.getElementsByTagName("input");
            var strTmp = "";
            for (var i = 0; i < Temp.length; i++) {
                if (Temp[i].type == "radio") {
                    var strArray = Temp[i].id.split("-");
                    if (strArray[0] == "CheckBoxforVPAnswer") {
                        if (Temp[i].checked == true) {
                            strTmp += (strArray[1] + ",");
                        }
                    }
                }
            }
            var strCaseID = document.getElementById('HF_CaseID').value;
            var strSelectedQID = document.getElementById('HF_SelectedQID').value;
            AuthoringTool_CaseEditor_Paper_EditTestPaper_VPAns.ConfirmSave(strTmp, strGroupID, strMainTopic, strCaseID, strSelectedQID);
        }

        function OpenNewPage() {
            var strGroupID = document.getElementById("HF_GroupID").value;
            if (strGroupID == "")
                alert("請至少先勾選一個考卷題目!!!");
            else
                window.open("/Hints/AuthoringTool/CaseEditor/Paper/Paper_IndexListOfVPAnsSet.aspx?CurrentProType=All&GroupID=" + strGroupID + "&SelectedIndex=" + document.getElementById("HF_SelectedIndex").value, '_blank', 'directories=0, height=750, menubar=0, resizable=1, scrollbars=1, status=0, titlebar=1, toolbar=0, width=1200');
        }

        function OpenOrCloseSelection(strIndex, intSelectionCount) {
            var strImageID = "imgPlusMinus-" + strIndex;
            if (document.getElementById(strImageID) != null) {
                //open or close
                for (var i = 0 ; i < intSelectionCount ; i++) {
                    var strTrSelectionID = "trSelection-" + strIndex + i.toString();
                    var strTrScoreID = "trScore-" + strIndex + i.toString();
                    if (document.getElementById(strTrSelectionID) != null) {
                        if (document.getElementById(strTrSelectionID).style.display == "") {
                            //close TableRow
                            document.getElementById(strTrSelectionID).style.display = "none";
                            document.getElementById(strTrScoreID).style.display = "none";
                        }
                        else {
                            //open TableRow
                            document.getElementById(strTrSelectionID).style.display = "";
                            document.getElementById(strTrScoreID).style.display = "";
                        }
                    }
                }
                //change image
                if (document.getElementById("trSelection-" + strIndex + "0") != null) {
                    if (document.getElementById("trSelection-" + strIndex + "0").style.display == "none") {
                        document.getElementById(strImageID).innerHTML = "<IMG src='/Hints/Summary/Images/plus.gif'>" + "&nbsp;&nbsp;";
                    }
                    else {
                        document.getElementById(strImageID).innerHTML = "<IMG src='/Hints/Summary/Images/minus.gif'>" + "&nbsp;&nbsp;";
                    }
                }
            }
        }

        function OpenOrCloseVPAns(strIndex, intSelectionCount, strVPAID) {
            var strImageID = "imgVPAnsPlusMinus-" + strIndex;
            if (document.getElementById(strImageID) != null) {
                //open or close
                for (var i = strIndex ; i < (strIndex+intSelectionCount) ; i++) {
                    var strTrSelectionID = "trVPAns-" + i.toString() + "0" + strVPAID;
                    if (document.getElementById(strTrSelectionID) != null) {
                        if (document.getElementById(strTrSelectionID).style.display == "") {
                            //close TableRow
                            document.getElementById(strTrSelectionID).style.display = "none";
                        }
                        else {
                            //open TableRow
                            document.getElementById(strTrSelectionID).style.display = "";
                        }
                    }
                }
                //change image
                if (document.getElementById("trVPAns-" + strIndex + "0" + strVPAID) != null) {
                    if (document.getElementById("trVPAns-" + strIndex + "0" + strVPAID).style.display == "none") {
                        document.getElementById(strImageID).innerHTML = "<IMG src='/Hints/Summary/Images/plus.gif'>" + "&nbsp;&nbsp;";
                    }
                    else {
                        document.getElementById(strImageID).innerHTML = "<IMG src='/Hints/Summary/Images/minus.gif'>" + "&nbsp;&nbsp;";
                    }
                }
            }
        }

        function AutoSetScore() {
            document.getElementById('btnSetScoreServer').click();
        }

        function GetTextbox(strTextID, strCaseID) {
            var QID = strTextID.split('-');
            var NewScore = document.getElementById(strTextID).value;
            AuthoringTool_CaseEditor_Paper_EditTestPaper_VPAns.UpdateScore(QID[1], NewScore, strCaseID);          
            document.getElementById('btnConstructCon').click();
        }

        window.onload = function () {
            var h = document.getElementById("<%=hfScrollPosition.ClientID%>");
            document.getElementById("<%=Panel1.ClientID%>").scrollTop = h.value;
            var k = document.getElementById("<%=hfScrollPosition2.ClientID%>");
            document.getElementById("<%=Panel2.ClientID%>").scrollTop = k.value;
    }
    function SetDivPosition() {
        var intY = document.getElementById("<%=Panel1.ClientID%>").scrollTop; var h = document.getElementById("<%=hfScrollPosition.ClientID%>");
        h.value = intY;
        var intZ = document.getElementById("<%=Panel2.ClientID%>").scrollTop; var k = document.getElementById("<%=hfScrollPosition2.ClientID%>");
        k.value = intZ;
          }

          function afterpostback() {
              var h = document.getElementById("<%=hfScrollPosition.ClientID%>");
              document.getElementById("<%=Panel1.ClientID%>").scrollTop = h.value;
              var k = document.getElementById("<%=hfScrollPosition2.ClientID%>");
              document.getElementById("<%=Panel2.ClientID%>").scrollTop = k.value;
        }
</script>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hfScrollPosition" runat="server" Value="0"/>
    <asp:HiddenField ID="hfScrollPosition2" runat="server" Value="0"/>
    <div style="text-align: right">
        <asp:DropDownList ID="ddl_MutiLanguage" runat="server" AutoPostBack="True" Font-Size="14px" OnSelectedIndexChanged="ddl_MutiLanguage_SelectedIndexChanged">
            <asp:ListItem Value="en-US">en-US</asp:ListItem>
            <asp:ListItem Value="zh-TW">zh-TW</asp:ListItem>         
        </asp:DropDownList>
    </div>
    <table style="border: 2px solid #000000; width: 1250px; height: 790px" align="center">
      <tr>
       <td style="height: 50px; background-color: #00FFFF;" colspan="2" align="center">
           <asp:Label ID="Lb_Topic" runat="server" Text="" Font-Bold="True" Font-Size="30px" ></asp:Label>&nbsp;
           <asp:Label ID="Lb_Title" runat="server" Text="" Font-Bold="True" ForeColor="Red" Font-Size="30px"></asp:Label>&nbsp;
           <asp:Label ID="Lb_TopicZh" runat="server" Text="" Font-Bold="True" Font-Size="30px" ></asp:Label>
       </td>
      </tr>
      <tr style="height: 50px" class="border">
       <td style="background-color: #CCFFFF" align="center" class="right">
           <asp:Label ID="Lb_ConQues" runat="server" Text="" Font-Bold="True" ForeColor="Red" Font-Size="30px"></asp:Label><br/>
           <asp:DropDownList ID="ddlProblemType" runat="server" AutoPostBack="true" Font-Size="X-Large" Visible="false" OnSelectedIndexChanged="ddlProblemType_SelectedIndexChanged">
               <asp:ListItem>Select a Problem Type</asp:ListItem>
           </asp:DropDownList>
       </td>
       <td style="background-color: #CCFFFF" align="center">
           <asp:Label ID="Lb_VPAns" runat="server" Text="" Font-Bold="True" ForeColor="Red" Font-Size="30px"></asp:Label><br/>
           <input id="btnEditVPAns" type="button" value="" style="font-size: x-large; background-color: #0000FF; cursor:pointer; color: #FFFFFF;" onclick="OpenNewPage()" runat="server" />
       </td>
      </tr>
      <tr style="background-color: #FFFFCC; height:640px">
        <td style="border: 2px solid #000000; width: 50%; text-align: center; vertical-align: top; ">
          <asp:Label ID="Lb_Conversation" runat="server" Text="" Font-Bold="True" ForeColor="Blue" Font-Size="30px"></asp:Label>
             <hr/>
          <asp:Panel ID="Panel1" runat="server" onscroll="SetDivPosition()" ScrollBars="Vertical" Width="100%" Height="600px">
          <asp:Table ID="tbSelectConversation" runat="server" Width="100%" HorizontalAlign="Left"></asp:Table>
          </asp:Panel>
        </td>
        <td style="border: 2px solid #000000; width: 50%; text-align: center; vertical-align: top;">
            <asp:Label ID="Lb_VPAnswer" runat="server" Text="" Font-Bold="True" ForeColor="Blue" Font-Size="30px"></asp:Label>
            <hr/>
            <asp:Panel ID="Panel2" runat="server" onscroll="SetDivPosition()" ScrollBars="Vertical" Width="100%" Height="600px">
            <asp:Table ID="tbSelectVPAnswer" runat="server" Width="100%" HorizontalAlign="Left"></asp:Table>
            </asp:Panel>
        </td>
      </tr>
      <tr>
          <td colspan="2"><table><tr style="background-color: #CCFFFF; height:50px; text-align: left; vertical-align: middle;">
        <td style="background-color: #CCFFFF; height:50px; text-align: left; vertical-align: middle; width: 925px;" >
            <asp:Label runat="server" ID="lbTotalScore" Text="" Font-Bold="True" ForeColor="Blue" Font-Size="22px"></asp:Label>
            <input type="text" id="textTotalScore" runat="server" style="font-size: large; width: 10%;" value="100" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <input type="button" ID="btnAutoSetScore" runat="server" Value="" style="font-size: x-large; background-color: #0000FF; cursor:pointer; color: #FFFFFF;" onclick="AutoSetScore()" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label runat="server" ID="LbCurrentScoreTitle" Text="" Font-Bold="True" ForeColor="Blue" Font-Size="22px"></asp:Label>
            <asp:Label runat="server" ID="LbCurrentScore" Text="" Font-Bold="True" ForeColor="Black" Font-Size="22px"></asp:Label>
            <input type="button" id="btnSetScoreServer" name="btnSetScoreServer" runat="server" style="display:none;" />
            <input type="button" id="btnConstructCon" name="btnConstructCon" runat="server" style="display:none;" />
            <input id="btnFinish3" runat="server" style="WIDTH: 150px; HEIGHT: 30px; display:none;" class="button_continue" onserverclick="btnFinish3_onserverclick" type="button"
			value="Finish" name="btnFinish2" />
        </td>
        <td style="background-color: #CCFFFF; height:50px; width: 325px;" align="right" >
             <asp:Button ID="BtnBack" runat="server" Text="" Font-Size="X-Large" OnClick="BtnBack_Click" Style="cursor:pointer;" BackColor="Blue" ForeColor="White" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <input type="button" ID="BtnNext" runat="server" Value="" onserverclick="btnFinish3_onserverclick"  style="font-size: x-large; background-color: #0000FF; cursor:pointer; color: #FFFFFF;" />&nbsp;&nbsp;
             <input id="btnSaveNext" style="display:none" type="button" name="btnSaveNext" runat="server" />
             <input id="btnTempSave" style="display:none" type="button" name="btnTempSave" runat="server" />
             <input type="button" ID="BtnClose" runat="server" Value="" onserverclick="btnFinish3_onserverclick" style="font-size: x-large; background-color: #0000FF; color: #FFFFFF; display: none;" />
        </td>
          </tr></table></td>
      </tr>
    </table>
        <asp:HiddenField ID="HF_SelectedIndex" runat="server" />
        <asp:HiddenField ID="HF_checkbox" Value="" runat="server" />
        <asp:HiddenField ID="HF_VPPreCell" Value="" runat="server" />
        <asp:HiddenField ID="HF_RowCount" Value="" runat="server" />
        <asp:HiddenField ID="HF_GroupID" Value="" runat="server" />
        <asp:HiddenField ID="HF_CaseID" Value="" runat="server" />
        <asp:HiddenField ID="HF_SelectedQID" Value="" runat="server" />
    </form>
</body>
</html>
