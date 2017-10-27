<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>

<%@ Page Language="C#" EnableViewState="true" AutoEventWireup="true" CodeFile="QGroupTreeNew_VoiceInquiry.aspx.cs" Inherits="AuthoringTool_CaseEditor_Paper_QuestionGroupTree_QGroupTreeNew_VoiceInquiry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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
    <title>Question Group Tree</title>

    <script type="text/javascript">
        var currentNodeID = "";
        var oldNodeID = "";

        function ConfirmStoryTopic(cbStoryTopicID) {
            var Tmpcb = document.getElementById(cbStoryTopicID);
            if (Tmpcb.checked) {
                var tmpArr = cbStoryTopicID.split('-');
                document.getElementById("btnEditNext").disabled = false;
                document.getElementById("btnEditNext").className = "button_continue";
                AuthoringTool_CaseEditor_Paper_QuestionGroupTree_QGroupTreeNew_VoiceInquiry.SaveStoryTopic(document.getElementById('HF_CaseID').value, tmpArr[1]);
            }
            else {
                document.getElementById("btnEditNext").disabled = true;
                document.getElementById("btnEditNext").className = "button_gray";
                AuthoringTool_CaseEditor_Paper_QuestionGroupTree_QGroupTreeNew_VoiceInquiry.SaveStoryTopic(document.getElementById('HF_CaseID').value, "");
            }
        }
        
        function SelectNode(nodeID, nodeAuthor, flag) {
            window.status = "Current selected node: " + nodeID;
            currentNodeID = nodeID;
            document.getElementById("strCurrentNodeID").value = currentNodeID;       
		             
            selected_changed();
            document.getElementById("tdNodeID").innerText = currentNodeID;
           
            //由flag判斷是否要顯示基本題目表    老詹 2014/10/24
            var flag_JSP = flag;
            if (flag_JSP.toString() == "True") {  //flag的Bool值要轉成string，方便判斷  
                document.getElementById('HiddenForNode').value = nodeID;
                CheckAndConstructBQL();
            }

            if (oldNodeID != "")
                document.getElementById(oldNodeID).className = "";

            document.getElementById("display_" + nodeID).className = "ItemMouseOver";
            oldNodeID = "display_" + nodeID;
        }
        
        function AddGroup() {
            if (document.getElementById("strCurrentNodeID").value != "") {
                showDisplay("divAddGroup");
            }
            else {
                alert("Please select a node first.");
            }
        }
        function ModifyGroup() {
            if (document.getElementById("strCurrentNodeID").value != "") {
                showDisplay("divModifyGroup");
            }
            else {
                alert("Please select a node first.");
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
            return confirm("Do you want to delete this node?");
        }

        function init() {
            document.getElementById('HF_Career').value = parent.document.getElementById('hf_Career').value;
        }

        function GoBack() {
            var strCareerID = document.getElementById('HF_Career').value;
            parent.location.href = "../Paper_QuestionMainNew.aspx?Career=" + strCareerID;
        }

        function GoNext() {
            var strCareerID = document.getElementById('HF_Career').value;
            var bDisplayBQL = document.getElementById('bDisplayBasicQuestionList').value;
            var checkFault = document.getElementById('Lb_CurrentSelected').innerHTML;
            if (checkFault != "") {
                if (bDisplayBQL == " Yes ")
                    parent.location.href = "../Paper_QuestionViewNew_VoiceInquiry.aspx?GroupID=" + document.getElementById('strCurrentQuestionRowID').value + "&Career=" + strCareerID + "&bDisplayBQL=" + bDisplayBQL;
                else
                    parent.location.href = "../Paper_QuestionViewNew_VoiceInquiry.aspx?GroupID=" + document.getElementById('strCurrentNodeID').value + "&Career=" + strCareerID + "&bDisplayBQL=" + bDisplayBQL;
            }
            else
                alert("請選擇一列問題主題再點擊此按鈕一次。");
        }

        function GoToEditBQL() {
            try
            {
                var AllPara = AuthoringTool_CaseEditor_Paper_QuestionGroupTree_QGroupTreeNew_VoiceInquiry.GetAllPara(document.getElementById('HiddenForNode').value, document.getElementById('HF_CaseID').value, document.getElementById('HF_Career').value).value;
                var ArrAllPara = AllPara.split('|');
                parent.location.href = "../EditTestPaper_BQL.aspx?GroupID=" + ArrAllPara[0] + "&Career=" + ArrAllPara[1] + "&CaseID=" + ArrAllPara[2];
            }
            catch (e) {
                alert('請參考左上角之Hints開始操作!!!');
            }
        }

        function CheckAndConstructBQL() {
            document.getElementById("AddnewtbServer").click();
        }

        var mouseX, mouseY; var objX, objY; var isDowm = false; var ClickID = "";  //是否按下鼠标  
        function mouseDown(obj, e) {
            ClickID = obj.id;
            obj.style.cursor = "move";
            if (ClickID == "divAddGroup") {
                objX = divAddGroup.style.left;
                objY = divAddGroup.style.top;
            }
            else {
                objX = divModifyGroup.style.left;
                objY = divModifyGroup.style.top;
            }
            mouseX = e.clientX;
            mouseY = e.clientY;
            isDowm = true;
        }
        function mouseMove(e) {
            var div = null;
            if (ClickID == "divAddGroup")
                div = document.getElementById("divAddGroup");
            else
                div = document.getElementById("divModifyGroup");
            var x = e.clientX;
            var y = e.clientY;
            if (isDowm) {
                div.style.left = parseInt(objX) + parseInt(x) - parseInt(mouseX) + "px";
                div.style.top = parseInt(objY) + parseInt(y) - parseInt(mouseY) + "px";
            }
        }
        function mouseUp(e) {
            if (isDowm) {
                var x = e.clientX;
                var y = e.clientY;
                var div = null;
                if (ClickID == "divAddGroup")
                    div = document.getElementById("divAddGroup");
                else
                    div = document.getElementById("divModifyGroup");
                div.style.left = (parseInt(x) - parseInt(mouseX) + parseInt(objX)) + "px";
                div.style.top = (parseInt(y) - parseInt(mouseY) + parseInt(objY)) + "px";
                mouseX = x;
                rewmouseY = y;
                if (ClickID == "divAddGroup")
                    divAddGroup.style.cursor = "default";
                else
                    divModifyGroup.style.cursor = "default";
                isDowm = false;
            }
        }
    </script>

</head>
<body onload="init()" onmousemove="mouseMove(event)" onmouseup="mouseUp(event)">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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

                function ShowSelected(SelectedID, SelectedName) {
                    var i, totalID;
                    totalID = document.getElementById('HiddenForAllID').value;
                    var res = totalID.split("/");
                    for (i = 0; i < res.length - 1; i++) { //先清掉所有列的背景顏色    老詹 2014/10/24
                        document.getElementById(res[i]).style.backgroundColor = "";
                    }
                    for (i = 0; i < res.length - 1; i++) { //再針對選取的某一列來變更顏色    老詹 2014/10/24                      
                        if (res[i] == SelectedID) {
                            document.getElementById(SelectedID).style.backgroundColor = "pink";
                            break;
                        }
                    }
                    var message = "目前選擇的問題主題：<span style='color:red;'>" + SelectedName + "</span>"; //文字提示選取的列    老詹 2014/10/24                  
                    document.getElementById('Lb_CurrentSelected').innerHTML = message;
                    document.getElementById("strCurrentQuestionRowID").value = SelectedID;
                    var IsLeaf = AuthoringTool_CaseEditor_Paper_QuestionGroupTree_QGroupTreeNew_VoiceInquiry.CheckIsLeaf(SelectedID).value;
                    if (IsLeaf == "True") {
                        document.getElementById("btSubmit").disabled = false;
                        document.getElementById("btSubmit").className = "button_continue";
                    }
                    else {
                        document.getElementById("btSubmit").disabled = true;
                        document.getElementById("btSubmit").className = "button_gray";
                    }
                }
                </script>

                <input type="hidden" id="strCurrentNodeID" runat="server" value="" />
                 <input type="hidden" id="strCurrentQuestionRowID" runat="server" value="" />
                <input type="hidden" id="bDisplayBasicQuestionList" runat="server" value="" />
            </div>
            <div id="divMain" align="left" style="width: 100%">
                <table id='body_content' style="height:99%">
                    <tr height="30">
                        <td style="width: 45%">
                            <br />
                            <input type="button" value="Add Children" onclick="AddGroup()" class="button_blue" style="font-size: 16px" />&nbsp;&nbsp;
                            <input type="button" id="btModifyGroup" value="Modify a node name" onclick="ModifyGroup()"
                                class="button_blue" style="font-size: 16px" />&nbsp;&nbsp;
                            <asp:Button ID="btDeletNodeSubmit" runat="server" Text="Delete a node" CssClass="button_blue"
                                OnClientClick="return confirm_delete()" OnClick="btDeletNodeSubmit_Click" style="font-size: 16px" />&nbsp;&nbsp;
                        </td>
                        <td style="width: 55%">                         
                        </td>
                    </tr>
                    <tr>
                       <td colspan="2" style="vertical-align: middle; text-align: left; height: 10px;">
                           <asp:Label ID="Lb_HintsForQuestionList" runat="server" Text="Hints：此職業問答樹中，最底層節點稱為「問題主題」；中間的節點稱為「故事主題」。因此，點擊職業問答樹中的一個故事主題，系統會將問題主題顯示於右方之表格中。" ForeColor="Red" Visible="False" Font-Size="16px" ></asp:Label>
                       </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            <div style="overflow: auto; height: 99%;">
                                <asp:TreeView ID="tvQuestionGroup" runat="server" ShowLines="true" EnableViewState="true" OnSelectedNodeChanged="tvQuestionGroup_SelectedNodeChanged">
                                    <SelectedNodeStyle Font-Underline="True" ForeColor="Red" 
                                        HorizontalPadding="0px" VerticalPadding="0px" />
                                    <HoverNodeStyle BackColor="LightPink" />
                                    <RootNodeStyle ImageUrl="~/BasicForm/Image/diffFolder.gif" />
                                    <NodeStyle ImageUrl="~/BasicForm/Image/folderclosed.gif" ForeColor="Black" />
                                </asp:TreeView>
                            </div>
                        </td>
                        <td align="center" valign="top">
                         <table id="BasicQuestionList" runat="server" border="1">
                         </table>
                         <br/>
                         <asp:Label ID="Lb_CurrentSelected" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr height="30">
                        <td colspan="2" align="right">
                            <hr />
                            <input type="button" id="Addnewtb" onclick="CheckAndConstructBQL()" runat="server" style="display:none;" />&nbsp;&nbsp;&nbsp;
                            <input type="button" id="AddnewtbServer" runat="server" style="display:none;" />
                            <asp:Button ID="btBack" runat="server" Text="Back" CssClass="button_continue" OnClientClick="GoBack()" Height="30px" Width="100px" Font-Size="16px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <input id="btSubmit" type="button" runat="server" value="編輯問題主題在題庫中的問題" style="font-size:16px; height:30px;" OnClick="GoNext()" Class="button_gray" disabled="disabled" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <input id="btnEditNext" type="button" runat="server" value="選擇此活動中要使用的問題" style="font-size:16px; height:30px;" OnClick="GoToEditBQL()" Class="button_gray" disabled="disabled" />&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="HiddenForNode" runat="server" />
                <asp:HiddenField ID="HiddenForAllID" runat="server" />
                <asp:HiddenField ID="HF_Career" runat="server" />
                <asp:HiddenField ID="HF_CaseID" runat="server" />
            </div>
            <div id="divAddGroup" style="position: absolute; z-index: 101; width: 70%; display: none;
                left: 15%; top: 20%; background-color: #446699; text-align: center;" onmousedown="mouseDown(this,event)">
                <br />
                <table style="width: 100%; text-align: center">
                    <tr>
                        <td align="center">
                            <table width="100%">
                                <tr class="header1_table_first_row">
                                    <td colspan="2">
                                        Add children
                                    </td>
                                </tr>
                                <tr class="header1_tr_odd_row">
                                    <td width="20%">
                                        parent node
                                    </td>
                                    <td id="tdParentNodeName" runat="server" width="80%">
                                    </td>
                                </tr>
                                <tr class="header1_tr_even_row">
                                    <td colspan="2">
                                        <span style="color:red">Hints: If you want to add more siblings, please segment the children with commas in the textbox below.</span>
                                    </td>
                                </tr>
                                <tr class="header1_tr_odd_row">
                                    <td width="20%">
                                        new node name
                                    </td>
                                    <td width="80%">
                                        <asp:TextBox ID="tNewNodeName" runat="server" Width="100%" Text="New node"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <hr />                     
                            <asp:Button ID="btAddGroupSubmit" runat="server" Text="submit" CssClass="button_gray"
                                OnClick="btAddGroupSubmit_Click" />&nbsp;&nbsp;&nbsp;
                            <input type="button" onclick="hideDisplay('divAddGroup')" value="canel" class="button_gray" />
                        </td>
                    </tr>
                </table>
                <br />
            </div>
            <div id="divModifyGroup" style="position: absolute; z-index: 102; width: 70%; display: none;
                left: 15%; top: 30%; background-color: #446699; text-align: center;" onmousedown="mouseDown(this,event)">
                <br />
                <table width="100%">
                    <tr>
                        <td align="center">
                            <table width="100%">
                                <tr class="header1_table_first_row">
                                    <td colspan="2">
                                        Modify the node
                                    </td>
                                </tr>
                                <tr class="header1_tr_odd_row">
                                    <td>
                                        current node id
                                    </td>
                                    <td id="tdNodeID" runat="server">
                                    </td>
                                </tr>
                                <tr class="header1_tr_even_row">
                                    <td>
                                        new node name
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
                            <asp:Button ID="btModifyGroupSubmit" runat="server" Text="submit" CssClass="button_gray"
                                OnClick="btModifyGroupSubmit_Click" />&nbsp;&nbsp;&nbsp;
                            <input type="button" onclick="hideDisplay('divModifyGroup')" value="canel" class="button_gray" />
                            <input type="button" value="move to" onclick="MoveGroup()" class="button_gray" style="display: none;" />
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
