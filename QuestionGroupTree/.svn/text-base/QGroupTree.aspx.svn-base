<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QGroupTree.aspx.cs" Inherits="AuthoringTool_CaseEditor_Paper_QuestionGroupTree_QGroupTree" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Question Group Tree</title>

    <script type="text/javascript">
        var currentNodeID = "";
        var oldNodeID = "";
        var currentOperation = 4;
        // add = 1; modify =2; move = 3; select =4;
        
        function SelectNode(nodeID, nodeMode , nodeAuthor) {
            window.status = "Current selected node: " + nodeID;
            currentNodeID = nodeID;
            document.getElementById("strCurrentNodeID").value = currentNodeID;
            
            if(oldNodeID != "")
		        document.getElementById(oldNodeID).className = "";
            //alert(currentNodeID);
            //document.getElementById("btModifyGroup").disable = false;
            if(currentOperation == 4) {
		        document.getElementById("display_"+nodeID).className = "ItemMouseOver";
		        oldNodeID = "display_" + nodeID;
            }
            if(currentOperation == 3) {
                document.getElementById("move_"+nodeID).className = "ItemMouseOver";
                oldNodeID = "move_" + nodeID;
            }
            
            selected_changed();
            document.getElementById("tdNodeID").innerText = currentNodeID;
            document.getElementById("ddlNodeMode").selectedIndex = nodeMode;
        }
        
        function AddGroup() {
          var returnValue=AuthoringTool_CaseEditor_Paper_QuestionGroupTree_QGroupTree.bIsPrivateNode(currentNodeID).value;
          if(returnValue == true || returnValue == null)
          {
            if(currentNodeID != "") {
                currentOperation = 1;
                showDisplay("divAddGroup");
            }
            else {
                alert("請先選擇一個Group.");
            }
          }
          else
          {
            alert("無法新增於公用區.");
          }
        }
        function ModifyGroup() {
        var returnValue=AuthoringTool_CaseEditor_Paper_QuestionGroupTree_QGroupTree.bIsPrivateNode(currentNodeID).value;
          if(returnValue == true || returnValue == null)
          {
            if(currentNodeID != "") {
                currentOperation = 2;
                showDisplay("divModifyGroup");
            }
            else {
                alert("請先選擇一個Group.");
            }
          }
          else
          {
             alert("您沒有修改此Group的權利.");
          }
        }
        function MoveGroup() {
            if(currentNodeID != "") {
                currentOperation = 3;
                showDisplay("divMoveGroup");
            }
            else {
                alert("Please select a topic first.");
            }
        }
        function showDisplay(id) {
            document.getElementById(id).style.display = "";
        }
        function hideDisplay(id) {
            currentOperation = 4;   // select operation
            document.getElementById(id).style.display = "none";
        }
        
        function confirm_delete() {
          var returnValue=AuthoringTool_CaseEditor_Paper_QuestionGroupTree_QGroupTree.bIsPrivateNode(currentNodeID).value;
          if(returnValue == true)
          {
            if(!window.confirm("Do you eant to delete this group?"))
                event.returnValue = false;
          }
          else
          {
             alert("您沒有刪除此Group的權利.");
             event.returnValue = false;
          }
        }
        
        function OpenSearchResult()
        {
          var varSearchKeyword = document.getElementById("tbSearchQuestionKeyword").value;
          window.open("SearchQuestionResult.aspx?SearchKeyword=" + varSearchKeyword + "",'_blank','directories=0, height=600, menubar=0, resizable=1, scrollbars=1, status=0, titlebar=1, toolbar=0, width=1000');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="display: none;">

        <script type="text/javascript">
                var message = "";
                function selected_changed() {
                    //alert(""+document.getElementById("_ctl0:MainDisplay:ddlProvider").value);
                    message = currentNodeID;
                    <%=sCallBackFunctionInvocation %>
                }
                
                function show_callback(msg, context) {
                    //document.getElementById("_ctl0:MainDisplay:tAuthor").value = msg;
                    //alert(msg);
                    document.getElementById("tdParentNodeName").innerText = msg;
                    document.getElementById("tNodeName").value = msg;
                }
        </script>

        <input type="hidden" id="strCurrentNodeID" runat="server" value="" />
    </div>
    <div id="divMain" align="left">
        <table id='body_content' height="99%">
            <tr height="20">
                <td colspan="2" class="title">
                    題庫群組選擇&nbsp;&nbsp;<asp:Label ID="lbQModeANDFunction" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr height="30">
                <td width="44%">
                    <br />
                    <input type="button" value="Add sub-group" onclick="AddGroup()" class="button_blue" />&nbsp;&nbsp;
                    <input type="button" id="btModifyGroup" value="Modify group" onclick="ModifyGroup()"
                        class="button_blue" />&nbsp;&nbsp;
                    <asp:Button ID="btDeletNodeSubmit" runat="server" Text="Delete group" CssClass="button_blue"
                        OnClientClick="confirm_delete()" OnClick="btDeletNodeSubmit_Click" />&nbsp;&nbsp;
                    <input type="button" value="move to..." onclick="MoveGroup()" class="button_blue"
                        style="display: none;" />&nbsp;&nbsp;
                </td>
                <td>
                    <asp:Label ID="lbSearchQuestion" runat="server" Text="Enter the keyword for search questions"></asp:Label>
                    <br />
                    <asp:TextBox ID="tbSearchQuestionKeyword" runat="server" Width="80%"></asp:TextBox>&nbsp;
                    <input id="btSearch" type="button" value="Search" class="button_blue" onclick="OpenSearchResult()" />
                </td>
            </tr>
            <tr>
                <%--<td width="40"></td>--%>
                <td align="left" valign="top" colspan="2">
                    <div style="overflow: auto; height: 100%;">
                        <asp:TreeView ID="tvQuestionGroup" runat="server" ShowLines="true">
                            <SelectedNodeStyle BackColor="LightBlue" />
                            <HoverNodeStyle BackColor="LightPink" />
                            <RootNodeStyle ImageUrl="~/BasicForm/Image/diffFolder.gif" />
                            <NodeStyle ImageUrl="~/BasicForm/Image/folderclosed.gif" ForeColor="Black" />
                        </asp:TreeView>
                    </div>
                </td>
            </tr>
            <tr height="20">
                <td colspan="2" align="right">
                    <hr />
                    <asp:Button ID="btBack" runat="server" Text="Back" CssClass="button_continue" OnClick="btBack_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btSubmit" runat="server" Text="Submit" CssClass="button_continue"
                        OnClick="btSubmit_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divAddGroup" style="position: absolute; z-index: 101; width: 70%; display: none;
        left: 15%; top: 20%; background-color: #446699; text-align: center;">
        <br />
        <table width="90%">
            <tr>
                <td align="center">
                    <table width="100%">
                        <tr class="header1_table_first_row">
                            <td colspan="2">
                                Add a sub-group
                            </td>
                        </tr>
                        <tr class="header1_tr_odd_row">
                            <td>
                                parent node
                            </td>
                            <td id="tdParentNodeName">
                            </td>
                        </tr>
                        <tr class="header1_tr_even_row">
                            <td>
                                node name
                            </td>
                            <td>
                                <asp:TextBox ID="tNewNodeName" runat="server" Width="100%" Text="New group"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="header1_tr_odd_row">
                            <td>
                                node mode
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlNewNodeMode" runat="server">
                                    <asp:ListItem Value="0" Text="public" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="private"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <hr />
                    <input type="button" onclick="hideDisplay('divAddGroup')" value="back" class="button_gray" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btAddGroupSubmit" runat="server" Text="submit" CssClass="button_gray"
                        OnClick="btAddGroupSubmit_Click" />
                </td>
            </tr>
        </table>
        <br />
    </div>
    <div id="divModifyGroup" style="position: absolute; z-index: 102; width: 70%; display: none;
        left: 15%; top: 30%; background-color: #446699; text-align: center;">
        <br />
        <table width="90%">
            <tr>
                <td align="center">
                    <table width="100%">
                        <tr class="header1_table_first_row">
                            <td colspan="2">
                                Modify the group
                            </td>
                        </tr>
                        <tr class="header1_tr_odd_row">
                            <td>
                                node id
                            </td>
                            <td id="tdNodeID">
                            </td>
                        </tr>
                        <tr class="header1_tr_even_row">
                            <td>
                                node name
                            </td>
                            <td>
                                <input type="text" id="tNodeName" runat="server" style="width: 100%;" />
                            </td>
                        </tr>
                        <tr class="header1_tr_odd_row">
                            <td>
                                node mode
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlNodeMode" runat="server">
                                    <asp:ListItem Value="0" Text="public"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="private"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <hr />
                    <input type="button" onclick="hideDisplay('divModifyGroup')" value="back" class="button_gray" />
                    <input type="button" value="move to" onclick="MoveGroup()" class="button_gray" style="display: none;" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btModifyGroupSubmit" runat="server" Text="submit" CssClass="button_gray"
                        OnClick="btModifyGroupSubmit_Click" />
                </td>
            </tr>
        </table>
        <br />
    </div>
    <div id="divMoveGroup" style="position: absolute; z-index: 103; width: 60%; display: none;
        left: 20%; top: 10%; background-color: #6688aa; text-align: center; height: 60%;">
        <br />
        <table width="90%" height="100%">
            <tr>
                <td align="center">
                    <table width="100%" height="100%">
                        <tr class="header1_table_first_row" height="10">
                            <td colspan="2">
                                Move the group
                            </td>
                        </tr>
                        <tr class="header1_tr_even_row">
                            <td>
                                <div style="height: 100%; overflow: auto;">
                                    <asp:TreeView ID="tvMoveGroup" runat="server" ShowLines="true" ExpandDepth="2">
                                    </asp:TreeView>
                                </div>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="20">
                <td align="right">
                    <hr />
                    <input type="button" onclick="hideDisplay('divMoveGroup')" value="back" class="button_gray" />
                </td>
            </tr>
        </table>
        <br />
    </div>
    <div style="display: none;">
        <input id="hiddenOpener" type="hidden" name="hiddenOpener" runat="server">
        <input id="hiddenEditMode" type="hidden" name="hiddenEditMode" runat="server">
        <input id="hiddenQuestionMode" type="hidden" name="hiddenQuestionMode" runat="server">
        <input id="hiddenQuestionType" type="hidden" name="hiddenQuestionType" runat="server">
        <input id="hiddenPresentType" type="hidden" name="hiddenPresentType" runat="server">
        <input id="hiddenModifyType" type="hidden" name="hiddenModifyType" runat="server">
        <input id="hiddenQuestionFunction" type="hidden" name="hiddenQuestionFunction" runat="server">
    </div>
    </form>
</body>
</html>
