<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VisionOfQuesion.aspx.cs" Inherits="AuthoringTool_CaseEditor_Paper_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>VisionOfQuestion</title>
    
    <script type="text/javascript">
            var SelectNodeID=""
            var SelectNodeName=""
            
            function AddGroup() 
            {
                showDisplay("divAddQuestion");
            }
            function AddFeatureSet() {
                showDisplay("AddFeatureSet");
            }
            function ShowDivSearch() {
                if (SelectNodeID == "")
                { alert('請選擇一個子特徵集') }
                else {
                    document.getElementById("tdFeatureSetName").innerText = SelectNodeName;
                    showDisplay("DivSearch"); 
                }
            }
            
            function hideDisplay(id) 
            {
                currentOperation = 4;   // select operation
                document.getElementById(id).style.display = "none";
            }
            function showDisplay(id) 
            {
                document.getElementById(id).style.display = "";
            }
            
            function SelectNode(nodeID, nodeAuthor ,nodeName) {
                document.getElementById("strCurrentNodeID").value = nodeID;
                SelectNodeID = nodeID;
                SelectNodeName = nodeName;
            }

    </script>
    
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 178px;
        }
        .style4
        {
            width: 10px;
        }
        .style5
        {
            width: 59px;
        }
        .style6
        {
            width: 38px;
        }
        .style7
        {
            width: 100%;
            background-color: #FFFFCC;
        }
        .style8
        {
            width: 45%;
        }
        .style10
        {
            width: 50%;
        }
        .style11
        {
            width: 50%;
        }
        .style12
        {
            width: 5%;
        }
        #btAddGroup
        {
            width: 80px;
        }
        #btnAdd
        {
            width: 80px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="strCurrentNodeID" runat="server" value="" />
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
   <table>
    <tr height="20">
            <td colspan="2" class="title">
                題庫群組選擇
            </td>
    </tr>
    <tr height="20">
        <td colspan="2">
            
            <asp:DropDownList ID="ddlQusetionList"  Width="200px" runat="server" 
                AutoPostBack="True" Font-Size="Large" Height="30px">
            </asp:DropDownList> 
        </td>
        <td colspan="1">
            <asp:Button ID="btnQuestionList" runat="server" class="button_blue" 
                Text="Select" onclick="btnQuestionList_Click" Width="80px" />
        </td>
       
        <td colspan="1">
        <asp:Button ID="btnDeleteQuestion" runat="server" class="button_blue" Text="Delete" 
                onclick="btnDeleteQuestion_Click" Width="80px" />
    </td>    
    <td colspan="1">
        <input type="button" id="btAddGroup" value="New" onclick="AddGroup()" class="button_blue" onclick="return btAddGroup_onclick()" />
    </td> 
    </tr>   
   </table> 
   
   
   <hr /> 
   
    <asp:Panel ID="Panel_out" runat="server" Visible="False">
     
     <table class="style1">
         <tr>
             <td class="style2" style="font-size: large">
                 設定特徵集合：</td>
             <td>
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style2">
                 
                    <asp:DropDownList ID="ddlFeatureSet" runat="server" Height="30px" Width="200px" 
                     onselectedindexchanged="ddlFeatureSet_SelectedIndexChanged" 
                        AutoPostBack="True" Font-Size="Large">
                     <asp:ListItem Selected="True" Value="0">Null</asp:ListItem>
                    </asp:DropDownList>

             </td>
             <td>
                 <table>
                 <tr>
                 <td>
                 <input ID="btnAdd" class="button_blue" onclick="AddFeatureSet()" type="button" 
                     value="Add" />
                     </td>
                     <td>
                 <asp:Button ID="btnEdit" runat="server" class="button_blue" 
                     onclick="btnEdit_Click" Text="Edit" Width="80px" />
                         <asp:Button ID="btnHiddenEdit" runat="server" class="button_blue" 
                             onclick="btnHiddenEdit_Click" Text="HideEdit" Visible="False" />
                     </td>
                     <td>
                 <asp:Button ID="btnDelet" runat="server" class="button_blue" 
                     onclick="btnDelet_Click" style="margin-left: 0px" Text="Delete" Width="80px" />
                     </td>
                  </tr>
                  </table>
             </td>
         </tr>
    </table>
    
    <asp:Panel ID="Panel_inner" runat="server" Visible="False">
    <table class="style7">
        <tr>
            <td class="style8" valign=top style="background-color: #CCFFFF">
                <span>Featurevalue List :</span>
                    <asp:UpdatePanel ID="upAllFeatureSet" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TreeView ID="tvFeatureGroup" runat="server" Font-Size="X-Large" 
                                ImageSet="Contacts" NodeIndent="10" ShowLines="True">
                                <SelectedNodeStyle Font-Underline="True" 
                                    HorizontalPadding="0px" VerticalPadding="0px" />
                                <ParentNodeStyle Font-Bold="True" ForeColor="#5555DD" />
                                <HoverNodeStyle Font-Underline="False" />
                                <RootNodeStyle ImageUrl="~/BasicForm/Image/diffFolder.gif" />
                                <NodeStyle ForeColor="Black" ImageUrl="~/BasicForm/Image/folderclosed.gif" 
                                    Font-Names="Verdana" Font-Size="13pt" HorizontalPadding="5px" NodeSpacing="0px" 
                                    VerticalPadding="0px" />
                            </asp:TreeView>
                        </ContentTemplate>
                    </asp:UpdatePanel></td>
            
            <td  align=center class="style12">
                <asp:UpdatePanel ID="upButton" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    <asp:ImageButton ID="btn_Select" ImageUrl="~/AuthoringTool/images/NEXT.GIF" ToolTip="加入"
                   runat="server" onclick="btn_Select_Click" style="height: 22px"/><br />
                    <br />
                    <asp:ImageButton ID="btn_Del" runat="server" ImageUrl="~/AuthoringTool/images/PREV.GIF"
                    ToolTip="刪除" onclick="btn_Del_Click" /><br />
                    <br /> 
                    <asp:ImageButton ID="btn_search" runat="server" ImageUrl="~/AuthoringTool/images/search.png"
                    ToolTip="詳細特徵值" onclick="btn_search_Click" /><br />      
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td  align=center  class="style10" style="background-color: #CCFFFF">
                <asp:UpdatePanel ID="upSelectedFeature" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    <span>Selected Featurevalue :</span>
                    <asp:ListBox ID="lb_Selected" runat="server" Rows="22" Width="90%" 
                            SelectionMode="Multiple" Font-Size="X-Large" Height="593px">
                    </asp:ListBox>
                    </ContentTemplate>
                 </asp:UpdatePanel>
                </td>
        </tr>
        <tr>
            <td class="style8" valign="top">
                &nbsp;</td>
            <td class="style12">
                &nbsp;</td>
            <td class="style11">
                &nbsp;</td>
        </tr>
    </table>
    </asp:Panel>
    <hr />
    </asp:Panel>
    
    
    <table align="right" class="style1">
        <tr>
            <td align="right">
                <asp:Button ID="btnBack" runat="server" Text="Back" class="button_blue" 
                    onclick="btnBack_Click" />
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="button_blue" 
                    onclick="btnSubmit_Click"/>
            </td>
        </tr>
    </table>
    
    <div id="divAddQuestion" style="position: absolute; z-index: 101; width: 70%; display: none;
                left: 15%; top: 20%; background-color: #446699; text-align: center;">
                <br />
                <table width="100%">
                    <tr>
                        <td align="center">
                            <table width="100%">
                                <tr class="header1_table_first_row">
                                    <td colspan="2">
                                        Add a Question Vision
                                    </td>
                                </tr>                     
                                 <tr class="header1_tr_odd_row">
                                    <td>
                                        Question Vision name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNewQuestionName" runat="server" Width="90%" Text="New Question"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            
                            <input type="button" onclick="hideDisplay('divAddQuestion')" class="button_gray" value="back" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btAddQuestionSubmit" runat="server" Text="submit" class="button_gray" OnClick="btAddQuestionSubmit_Click" />
                        </td>
                    </tr>
                </table>
                <br />
            </div>
            
            <div id="AddFeatureSet" style="position: absolute; z-index: 101; width: 70%; display: none;
                left: 15%; top: 20%; background-color: #446699; text-align: center;">
                <br />
                <table width="100%">
                    <tr>
                        <td align="center">
                            <table width="100%">
                                <tr class="header1_table_first_row">
                                    <td colspan="2">
                                        Add a FeatureSet
                                    </td>
                                </tr>                     
                                 <tr class="header1_tr_odd_row">
                                    <td>
                                        Question Vision name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFeatureSet" runat="server" Width="90%" Text="New FeatureSet"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            
                            <input type="button" onclick="hideDisplay('AddFeatureSet')" class="button_gray" value="back" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnAddFeatureSet" runat="server" Text="submit" class="button_gray" OnClick="btnAddFeatureSet_Click" />
                        </td>
                    </tr>
                </table>
                <br />
            </div>
            
            <div id="DivSearch" 
        
        style="position: absolute; z-index: 101; width: 25%; display: none;
                left: 15%; top: 20%; background-color: #C8C8C8; text-align: center;">
                <br /> 
                      <table>
                      <tr class="header1_table_first_row">
                           <td id="tdFeatureSetName">        
                           </td>
                      </tr> 
                      <tr>
                        <td align=center>
                        <asp:UpdatePanel ID="upSearch" runat="server">
                        <ContentTemplate>
                            <asp:ListBox ID="listboxSearch" runat="server" Height="450" Width="300" 
                                Enabled="True" Font-Bold="True" Font-Italic="False" Font-Size="Larger" 
                                ForeColor="Black"></asp:ListBox>          
                        </ContentTemplate>
                         </asp:UpdatePanel>
                        </td>
                      </tr>                     
                        <tr>
                            <td align=right>
                                <input type="button" onclick="hideDisplay('DivSearch')" class="button_gray" value="back" />&nbsp;&nbsp;&nbsp;         
                            </td>
                        </tr>
                       </table>
                <br />
            </div>
    
    
     </form>
    
    </body>
</html>
