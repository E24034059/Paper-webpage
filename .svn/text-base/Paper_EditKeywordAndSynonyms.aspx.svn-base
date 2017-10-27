<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Paper_EditKeywordAndSynonyms.aspx.cs" Inherits="AuthoringTool_CaseEditor_Paper_Paper_EditKeywordAndSynonyms" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>編輯關鍵字與同義詞共同頁面</title>
</head>
<body>
    <style type="text/css">
        .button_continue
        {
	        font-size: 14px; 
	        color: #ffffff; 
	        letter-spacing: 1px; 
	        border: 1px solid #000000; 
	        /*	background-color: #5B84A0;*/
	        background-color: #3d77cb;
	        cursor: pointer;
	        font-family: Verdana, Arial, Helvetica, sans-serif;
	        overflow: hidden;
        }
        /* table 第一橫列(head)的style */
        .header1_table_first_row {
	        font-weight:bold;
	        color: #FAFFDD; /*darkred  ffe9e3*/
	        font-size: 100%;
	        background-color: #AA9f8E;
        }
        /* table的奇數列的style */
        .header1_tr_odd_row {
	        background-color: #F2E1CF; /*#DDFFFF; */
        }

        /* table的偶數列的style */
        .header1_tr_even_row {
	        background-color: #FAFFDD;
        }
        input {
            background-color:transparent;
            border: 0px solid;
        }
        input:focus {
            outline:none;
        }
    </style>
    <link href="../../main.css" type="text/css" rel="stylesheet" />
    <link href="analysis.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript">
        function checkAndSave() {
            var NewType = document.getElementById("<%=tbTypeNewKeyword.ClientID %>").value;
            if (NewType != "") {
                var OriKey = AuthoringTool_CaseEditor_Paper_Paper_EditKeywordAndSynonyms.GetAllKeyword().value;
                if (OriKey.indexOf(NewType) >= 0) {
                    alert('您已新增過"' + NewType + '"這個關鍵字!!!');
                }
                else {
                    document.getElementById('HiddenFieldfortext').value = NewType;
                    document.getElementById('Addnewtb').click();
                }
            }
        }

        function GetAllCheckedSynonyms(strAllKeyword) {
            var ArrKeyword = strAllKeyword.split('|');
            var Temp = document.getElementsByTagName("input");
            var strTmp = "";
            for (var j = 0; j < ArrKeyword.length; j++) {
                var KeyTmp = ArrKeyword[j] + ",";
                var count = 0;
                for (var i = 0; i < Temp.length; i++) {
                    if ((Temp[i].type == "checkbox") && (Temp[i].id.indexOf(ArrKeyword[j]) >= 0)) {
                        if (Temp[i].checked == true) {
                            var strArray = Temp[i].id.split('_');
                            KeyTmp += (strArray[1] + ",");
                        }
                    }
                }
                KeyTmp = KeyTmp.substring(0, KeyTmp.length - 1);
                strTmp += (KeyTmp + "|");
            }
            strTmp = strTmp.substring(0, strTmp.length - 1);
            //alert(strTmp);
            AuthoringTool_CaseEditor_Paper_Paper_EditKeywordAndSynonyms.SaveSynonyms(strTmp);
            document.getElementById("btnKeyTable").click();
        }

        function AddNewSynonyms(strCheckID, strQID) {
            var strNewSynonyms = strCheckID.split('_');
            var strTmp3 = AuthoringTool_CaseEditor_Paper_Paper_EditKeywordAndSynonyms.GetAllKeyword().value;
            if (strTmp3.indexOf(strNewSynonyms[1]) < 0) {
                GetAllCheckedSynonyms(strTmp3 + "," + strNewSynonyms[1]);
            }
            else {
                var tmpSplit = strTmp3.split(strNewSynonyms[1]);
                tmpSplit[0] = tmpSplit[0].substring(0, tmpSplit[0].length - 1);
                var tmpSub = tmpSplit[0] + tmpSplit[1];
                GetAllCheckedSynonyms(tmpSub);
            }
        }

        function DeleteCurrentKeyword(DeleteID) {
            var DeleteKey = document.getElementById(DeleteID).innerText;
            document.getElementById('HiddenFieldforRemove').value = DeleteKey;
            document.getElementById('Remove_Btn').click();
        }
    </script>
    <form id="form1" runat="server">
    <div style="background-color:#ebecee;">
        <div id="div_section3" class="div_section3">
            <div style="width: 95%; text-align: left;">  
                <span style="text-align: left; vertical-align: middle;">Keywords：</span>              
                &nbsp;<asp:TextBox ID="tbTypeNewKeyword" runat="server" Width="30%" ToolTip="Type new keyword here!" BackColor="White" BorderWidth="1" Font-Size="16px" align="middle"></asp:TextBox>&nbsp;&nbsp;<input id="btnTypeNewKeyword" type="button" value="Add" class="button_continue" onclick="checkAndSave()" style="text-align: center; width: 100px; vertical-align:middle;" />
            </div><br/>
            <table style="width:100%">
                <tr>
                <td>
                    <asp:Label ID="Label_Keword_title" runat="server" Text="Current Keyword :" Font-Size="Larger"></asp:Label>&nbsp;&nbsp;
                    <asp:Label id="Lb_showKeyword" runat="server" Text="" style="color: #FF0000; font-size: large; background-color: #ebecee;"></asp:Label><br/>                  
                    <table align="left" style="padding: 0px; margin: 0px; text-align: left; width:90%">
                        <tr>
                        <td style="width:182px;">
                            <asp:Label ID="Lb_synonyms_title" runat="server" Text="Current Synonyms :" Font-Size="Larger"></asp:Label>
                        </td>
                        <td>
                            <table ID="Lb_synonyms" runat="server" style="padding: 0px; margin: 0px; text-align: left; width:100%">
                            </table>
                        </td>
                        </tr>
                    </table>
                    <br/><br/><asp:Label ID="Label_Keyword_Learn" runat="server" ForeColor="Blue" Text="(Hints: You can choose the Synonyms you want.)"></asp:Label>
                </td>
            </tr> 
            </table>
        </div>
        <hr />
        <div id="div_section4" runat="server" align="center">
        </div>
        <br />
        <div align="center">
            <asp:Button ID="btmComplete" class="button_continue" runat="server" Text="Finish" style="width:150px;font-size:larger;" OnClick="btmComplete_Click"   />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancel" class="button_continue" runat="server" Text="Canel" style="width:150px;font-size:larger;" OnClick="btnCancel_Click"   />
        </div>
        <input id="Addnewtb" type="button" style="display:none;" name="Addnewtb" runat ="server"  />
        <input id="Remove_Btn" type="button" style="display:none;" name="Addnewtb" runat ="server"  />
        <input id="btnKeyTable" runat="server" type="button" style="display:none;" name="btnKeyTable" class="button_continue">
        <asp:HiddenField ID="HiddenFieldfortext" runat="server" />
        <asp:HiddenField ID="HiddenFieldforRemove" runat="server" />
     </div>
    </form>
</body>
</html>
