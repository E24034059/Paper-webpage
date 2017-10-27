<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Paper_VPAnswerSetEditor.aspx.cs" Inherits="PaperSystem.Paper_VPAnswerSetEditor" %>

<!DOCTYPE html>

 <style type="text/css">
        input.bigcheck
        {
            height: 30px;
            width: 30px;
            cursor: pointer;
        }
</style>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Paper_VPAnswerSetEditor</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
</head>
<body>
    <script type='text/javascript'>
        function Clear() {
            if (document.all("txtQuestionEdit") != null) {
                document.all("txtQuestionEdit").value = "";
            }

            if (document.all("txtAnswerEdit") != null) {
                document.all("txtAnswerEdit").value = "";
            }
        }

        function goBack(strCurrentProType, strGroupID) {
            if (window.confirm("您的資料將不會被儲存，您確定要繼續嗎?")) {
                var PaperID = document.getElementById("hfPaperID").value;
                var BackURL = document.getElementById("HiddenOpener").value;
                 if (PaperID == "")//由編輯題庫地方來的
                 {
                     if (BackURL == "Paper_IndexListOfVPAnsSet")
                         window.location.href = BackURL + ".aspx?CurrentProType=" + strCurrentProType + "&GroupID=" + strGroupID + "&SelectedIndex=" + document.getElementById("hfSelectedIndex").value;
                     else
                         window.close();
                 }
                 else {
                     window.history.back();
                 }
            }
        }

        function NextQuestion() {
            document.getElementById("btnSaveNextQuestion").click();
        }

        function NextStep() {
            var IsAllfalse = false, IsEditing = "", rblSelected = "";
            var Tmp = document.getElementsByTagName('input');
            for(var i=0;i<Tmp.length; i++)
            {
                if (Tmp[i].type == "text")
                    IsAllfalse = true;
            }
            var radioButtonlist = document.getElementsByName("<%=Rbl_AnswerType.ClientID%>");
            for (var x = 0; x < radioButtonlist.length; x++) {
                if (radioButtonlist[x].checked) {
                    rblSelected = radioButtonlist[x].value;
                }
            }
            IsEditing = Paper_VPAnswerSetEditor.CheckAllFalse(document.getElementById("hfVPAID").value).value;
            if (IsAllfalse)
                alert('您目前正在編輯選項，請按Save後繼續!!!');
            else if (IsEditing=="False" && rblSelected != "0")
                alert('目前的選項內沒有正確答案選項，請設定正確答案後繼續!!!');
            else
                document.getElementById("btnSaveNext").click();
        }

        function ShowDetail(strSelectionTRID, strImgID) {
            if (document.getElementById(strSelectionTRID) != null) {
                if (document.getElementById(strSelectionTRID).style.display == "none") {
                    document.getElementById(strSelectionTRID).style.display = "";
                    document.getElementById(strImgID).src = "../../../BasicForm/Image/minus.gif";
                }
                else {
                    document.getElementById(strSelectionTRID).style.display = "none";
                    document.getElementById(strImgID).src = "../../../BasicForm/Image/plus.gif";
                }
            }
        }

        function ReloadOpenner() {
            window.close();
        }
    </script>

    <form id="form1" method="post" runat="server">
      <table id="body_content" width="100%" align="center">
        <tr>
            <td align="center" class="title">
                Conversation Question：<asp:Label ID="LbCurrentConQues" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
          <td>
              Virtual Person's Answer Editor (For Basic Question List)
          </td>
        </tr>
        <tr>
          <td><hr/>1. Virtual Person's Answer Title：
              <div align="right">
                  <asp:TextBox ID="txtVPAnsTitle" runat="server" Width="100%" TextMode="MultiLine"
                      Rows="4" Font-Size="Large"></asp:TextBox>                 
              </div>            
          </td>
        </tr>                  
        <tr>
          <td><hr />  
              2. Virtual Person's Answer Content : <br/><span style="color:red;"> (Hints: You can choose the response type of VP's answer to edit：</span>
              <asp:DropDownList ID="ddl_VPResponseType" runat="server" AutoPostBack="True" Font-Size="Large" OnSelectedIndexChanged="ddl_VPResponseType_SelectedIndexChanged">
                  <asp:ListItem Value="簡短的">簡短的</asp:ListItem>
                  <asp:ListItem Value="複雜的">複雜的</asp:ListItem>
                  <asp:ListItem Value="模糊不清的">模糊不清的</asp:ListItem>
              </asp:DropDownList> <span style="color:red;">&nbsp;)</span>
          </td>
        </tr>
        <tr>
          <td>
              <div align="right">
                  <asp:TextBox ID="txtQuestionEdit" runat="server" Width="100%" Height="150px" TextMode="MultiLine"
                      Rows="4" Font-Size="Large"></asp:TextBox>                 
              </div>            
          </td>
        </tr>
        <tr>
          <td>
              <hr id="hrQuestion" runat="server" />
              <asp:Label ID="Title_Answer_Type" runat="server" Text="3. Student's Answer Type of recording the VP's Answer:" Font-Size="Larger"></asp:Label>&nbsp;&nbsp;
              <asp:RadioButtonList ID="Rbl_AnswerType" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" RepeatLayout="Flow" Font-Size="Larger" OnSelectedIndexChanged="Rbl_AnswerType_SelectedIndexChanged">
                  <asp:ListItem Value="0" Selected="True">TextBox&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                  <asp:ListItem Value="1">DropDownList&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                  <asp:ListItem Value="2">RadioButton&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                  <asp:ListItem Value="3">CheckBox</asp:ListItem>
              </asp:RadioButtonList>
              <hr />
          </td>
        </tr>
        <tr>
          <td align="center">
              <asp:Button ID="btn_AddAns" runat="server" OnClick="btn_AddAns_Click" Font-Size="Larger" Text="Add a new option" Style="cursor:pointer;" /><br/><br/>
              <asp:GridView ID="GV_AnswerContent" runat="server" OnRowDataBound="GV_AnswerContent_RowDataBound"
                 AutoGenerateColumns="false" OnRowCancelingEdit="GV_AnswerContent_RowCancelingEdit" OnRowDeleting="GV_AnswerContent_RowDeleting"
                 OnRowEditing="GV_AnswerContent_RowEditing" OnRowUpdating="GV_AnswerContent_RowUpdating" Style="width: 80%;" Font-Size="Larger" CellPadding="5"  >
                <Columns>
                  <asp:TemplateField HeaderText="Functions">
                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="Larger" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="Larger" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="Larger"  />
                    <EditItemTemplate>
                        <asp:LinkButton ID="LkUpdateE" runat="server" CommandName="Update">Save</asp:LinkButton> |
                        <asp:LinkButton ID="LkCancelE" runat="server" CommandName="Cancel">Cancel</asp:LinkButton>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:LinkButton ID="LkSaveF" runat="server" OnClick="lkSaveF_Click">Save</asp:LinkButton> |
                        <asp:LinkButton ID="LkCancelF" runat="server" OnClick="lkCancelF_Click">Cancel</asp:LinkButton>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LkModify" runat="server" CommandName="Edit" >Modify</asp:LinkButton> |
                        <asp:LinkButton ID="LkDelete" runat="server" CommandName="Delete" >Delete</asp:LinkButton>
                    </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Option Content" ItemStyle-HorizontalAlign="Center">
                    <EditItemTemplate>
                        <asp:TextBox ID="txAnsE" runat="server" Text='<%#Bind("cAnswerContent") %>' Font-Size="Larger"  Width="90%" ></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txAnsF" runat="server" Text='<%#Bind("cAnswerContent") %>' Font-Size="Larger"  Width="90%"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LbAns" runat="server" Text='<%#Eval("cAnswerContent") %>' Font-Size="Larger"  Width="90%"></asp:Label>
                    </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Correct Answer" ItemStyle-HorizontalAlign="Center">
                    <EditItemTemplate> 
                         <input class="bigcheck" runat="server" type="checkbox" id="CxCorrectE">
                    </EditItemTemplate>
                    <FooterTemplate>
                         <input class="bigcheck" runat="server" type="checkbox" id="CxCorrectF">
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LbCorrect" runat="server" Text='<%#Eval("bIsCorrect") %>' Font-Size="Larger"></asp:Label>
                    </ItemTemplate>
                  </asp:TemplateField>
                </Columns>
              </asp:GridView>
          </td>
        </tr> 
        <tr>
            <td>          
                <hr />
            </td>
        </tr>
        <tr width="100%">
          <td align="right">                  
              <input id="btnSaveNextQuestion" style="display:none" type="button" name="btnSaveNextQuestion"
                  runat="server">
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <input id="btnSaveNext" style="display:none" type="button" name="btnSaveNext" runat="server">
              &nbsp;&nbsp;&nbsp;&nbsp;
              <input id="btnClose" type="button" runat="server" value="Close" class="button_continue" onclick="ReloadOpenner()" style="font-size: larger; width: 150px; height: 30px; cursor:pointer;" />
              &nbsp;&nbsp;&nbsp;&nbsp;
              <input id="btnNext" runat="server" style="width: 150px; cursor: pointer; height: 30px" onclick="NextStep()"
                  type="button" value="OK" name="btnNext" class="button_continue" />
              &nbsp;&nbsp;&nbsp;&nbsp;
              <input id="btnNextQuestion" style="width: 150px; cursor: pointer; height: 30px; display:none" onclick="NextQuestion()"
                  type="button" value="Edit next question >>" name="btnNextQuestion" class="button_continue">
              <%--&nbsp;&nbsp;&nbsp;&nbsp;--%>
              <asp:Button id="btnBack" runat="server" Width="150px" Height="30px" Text="Cancel" OnClick="GobackCareer" class="button_continue" Style="cursor:pointer;" />
          </td>
        </tr>
        <tr>
          <td>
              <div>
                  <asp:HiddenField ID="hfPaperID" runat="server" />
                  <asp:HiddenField ID="hfVPAID" runat="server" />
                  <asp:HiddenField ID="hfCurrentProType" runat="server" />
                  <asp:HiddenField ID="hfGroupSerialNum" runat="server" />
                  <asp:HiddenField ID="HiddenFieldfortext" runat="server" />
                  <asp:HiddenField ID="HiddenFieldforRemove" runat="server" />
                  <asp:HiddenField ID="HiddenOpener" runat="server" />
                  <asp:HiddenField ID="hfSelectedIndex" runat="server" />
              </div>       
          </td>
        </tr>
      </table>
    </form>
</body>
</html>
