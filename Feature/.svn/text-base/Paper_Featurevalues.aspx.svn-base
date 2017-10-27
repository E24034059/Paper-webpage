<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Paper_Featurevalues.aspx.cs" Inherits="AuthoringTool_CaseEditor_Paper_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>EditEigenvalue</title>
    <script type="text/javascript">
        var currentNodeID = "";
        var oldNodeID = "";

        function SelectNode(nodeID, nodeAuthor) {
            window.status = "Current selected node: " + nodeID;
            currentNodeID = nodeID;
            document.getElementById("strCurrentNodeID").value = currentNodeID;
            
            if (oldNodeID != "")
                document.getElementById(oldNodeID).className = "";

            document.getElementById("display_" + nodeID).className = "ItemMouseOver";
            oldNodeID = "display_" + nodeID;

            selected_changed();
            document.getElementById("tdNodeID").innerText = currentNodeID;
            var Panel_listbox = document.all('Panel_listbox').id;
            HtmlControlUpdate('AuthoringTool_CaseEditor_Paper_Default.GetTemplateMenu', Panel_listbox, currentNodeID, new HtmlControl(Panel_listbox));
            
           }    
            function AddGroup() {
            if (currentNodeID != "") {
                showDisplay("divAddGroup");
            }
            else {
                alert("請先選擇一個Group.");
            }
        }
        function ModifyGroup() {
            if (currentNodeID != "") {
                showDisplay("divModifyGroup");
            }
            else {
                alert("請先選擇一個Group.");
            }
        }
        function AddFeature() {
            showDisplay("divAddFeature");
        }
        function showDisplay(id) {
            document.getElementById(id).style.display = "";
        }
        function hideDisplay(id) {
            currentOperation = 4;   // select operation
            document.getElementById(id).style.display = "none";
        }

        function confirm_delete() {
            if (!window.confirm("Do you want to delete this Feature?")) {
                event.returnValue = false;
            }
            else {
                oldNodeID = "";
            }
        }
        function showEditWindow() {
           window.open("EditFeatureItem.aspx?nodeID=" + currentNodeID + "", "_blank", "width=650,height=600,top=30,left=40,resizable=no,toolbar=no,status=no"); 
        }


    </script>
    </head>


<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table>
    <tr height="20">
            <td colspan="2" class="title">
                Edit Featurevalue
            </td>
    </tr>
    </table>    
                
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
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

function btn_AddItem_onclick() {

}


            </script>             
             <input type="hidden" id="strCurrentNodeID" runat="server" value="" />
            </div>
            
                <table id='body_content' height="99%">                    
                    <tr height="30">
                        <td>
                            <input type="button" id="btAddGroup" value="Add Children" onclick="AddGroup()" 
                                class="button_blue" onclick="return btAddGroup_onclick()" />&nbsp;&nbsp;
                            <input type="button" id="btModifyGroup" value="Modify a node name" onclick="ModifyGroup()"
                                class="button_blue" />&nbsp;&nbsp;
                            <asp:Button ID="btDeletNodeSubmit" runat="server" Text="Delete a node" CssClass="button_blue"
                                OnClientClick="confirm_delete()" OnClick="btDeletNodeSubmit_Click" />&nbsp;&nbsp;
                        </td>                      
                    </tr>
                    </table>
                    
                     <table  id='body_content'>
                     
                     <tr class="header1_table_first_row">
                     <td align="center">
                     特徵樹
                     </td>
                     </tr>
                     <tr  valign="top">
                        <td  class="header1_tr_even_row" valign="top" weight="10%" rowspan="2" >
                            <asp:TreeView ID="tvFeatureGroup" runat="server" Font-Size="Large" 
                                ShowLines="true">
                                <SelectedNodeStyle BackColor="Black" />
                                <HoverNodeStyle BackColor="LightPink" />
                                <RootNodeStyle ImageUrl="~/BasicForm/Image/diffFolder.gif" />
                                <NodeStyle ForeColor="Black" ImageUrl="~/BasicForm/Image/folderclosed.gif" />
                            </asp:TreeView>
                        </td>                                                                                                                                                                                                          
                         <td align="center">                                                                                                
                             <asp:Button ID="btn_Edit" runat="server" Text="Edit" CssClass="button_blue" 
                                 onclick="btnEdit_Click" />
                          
                          </td>  
                             <tr height="600" weight="30%">
                           
                                 <td align="center" valign="top" weight="50%" id="templateTable">
                                     
                                     <asp:Panel ID="Panel_listbox" runat="server">                                     
                                     
                                     <Table class="header1_table" width=600 rules="all" border="1" style="border:solid 1px black;border-collapse:collapse;">
                                        <TR class=header1_table_first_row>
                                            <TD width=10%>無資料</TD>
                                         </TR>                                           
                                        <TR class='header1_tr_even_row'>
                                                <TD width=35%>
                                                請選擇一個特徵值
                                                </TD>
                                        </TR>
                                     
                                        
                                     </table>
                                     
                                     </asp:Panel>
                                     
                                     &nbsp;</td>
                             </tr>
                             <caption>
                                 &nbsp;
                         </caption>
                    </tr>
                         <tr>
                             <td>
                             </td>                            
                             <td align="right">
                                 <asp:Button ID="btBack" runat="server" CssClass="button_continue" 
                                     OnClick="btBack_Click" Text="Back" />
                                 </td>
                         </tr>
                </table>
            
            
            
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
                <asp:HiddenField ID="hfRoot" runat="server" />
            </div>
           
        
        </ContentTemplate>
    </asp:UpdatePanel>
        
    </form>
</body>
</html>
