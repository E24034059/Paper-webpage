<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditFeatureItem.aspx.cs" Inherits="AuthoringTool_CaseEditor_Paper_Feature_EditFeatureItem" %>

<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EditFeaturevalue</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 25px;
        }
        .style3
        {
            height: 25px;
            width: 255px;
        }
        .style5
        {
            width: 223px;
        }
        .style6
        {
            width: 94px;
        }
        .style7
        {
            width: 84px;
        }
    </style>
    
    
    <script type="text/javascript">
        function ModifyGroup() 
        {
                showDisplay("divModifyGroup");            
        }

        function showDisplay(id) 
        {
            document.getElementById(id).style.display = "";
        }
        function hideDisplay(id) 
        {
            currentOperation = 4;   // select operation
            document.getElementById(id).style.display = "none";
        }
    </script>
    
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="style1">
            <tr >
                <td colspan="3" class="title">
                    編輯 
                    <asp:Label ID="lbTittle" runat="server"></asp:Label>
&nbsp;特徵值</td>
            </tr>
            <tr>
                <td colspan="3" >
                    <input type="button" id="btModifyGroup" value="Modify Name" onclick="ModifyGroup()"
                                class="button_blue" />
                    
                    <asp:Button ID="btn_delete" runat="server" Text="Delete" CssClass="button_blue" 
                        onclick="btn_delete_Click" CausesValidation="False"/>
                
                </td>
            </tr>
            <tr>
                <td class="style3" rowspan="2" Width="50%">
                    <asp:ListBox ID="lsb_FeatureItem" runat="server" Height="400px" Width="360px"  
                        Font-Size="Large" BackColor="White" Font-Bold="True" Font-Italic="False">
                    </asp:ListBox>
                </td>
                <td class="style2" align=left valign=top colspan="2" Width="50%">
                    <table class="style1">
                        <tr>
                            <td class="style6" colspan="2">
                                FeatureName:
                            </td>
                            <td class="style5">
                                &nbsp;</td>
                            <td>
                                
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style6" colspan="2">
                                <asp:TextBox ID="txt_FeatureName" Text="newFeaturevalue" runat="server" 
                                    Width="178px"></asp:TextBox>
                            </td>
                            <td class="style5" align=left>
                                
                                &nbsp;</td>
                            <td align=right>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style6">
                                
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ErrorMessage="不可為空值" ControlToValidate="txt_FeatureName" 
                                    ></asp:RequiredFieldValidator>
                                
                            </td>
                            <td class="style7" align=right>
                                
                                <asp:Button ID="btn_add" runat="server" CssClass="button_blue" Text="Add" 
                                    onclick="btn_add_Click" />
                                
                            </td>
                            <td class="style5" align=left>
                                
                                &nbsp;</td>
                            <td align=right>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style2" align=left valign=bottom>
                    &nbsp;</td>
                <td class="style2" align=left valign=top>
                    &nbsp;</td>
            </tr>
        </table>
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
                                                        
                                <tr class="header1_tr_even_row">
                                    <td>
                                        New Feature name:
                                    </td>
                                    <td>
                                        <input type="text" id="txtNewFeatureName" runat="server" style="width: 100%;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <hr />
                            <input type="button" onclick="hideDisplay('divModifyGroup')" value="back" class="button_gray" />
                            <asp:Button ID="btModifyGroupSubmit" runat="server" Text="submit" CssClass="button_gray"
                                OnClick="btModifyGroupSubmit_Click" CausesValidation="False" />
                        </td>
                    </tr>
                </table>
                <br />
            </div>
    
    
    </form>
</body>
</html>
