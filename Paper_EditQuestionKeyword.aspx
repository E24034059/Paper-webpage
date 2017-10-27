<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Paper_EditQuestionKeyword.aspx.cs" Inherits="AuthoringTool_CaseEditor_Paper_Paper_EditQuestionKeyword" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Reference Analysis</title>
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
        function show_content() {
            document.location.href = "../../Learning/CaseNote/EntryNLP.aspx";
            //document.location.href = "Entry_CaseNote.aspx";
        }
        function check_text(id, textID) {
            if (document.getElementById(id).checked) {
                document.getElementById(textID).className = 'span_keyword';
            }
            else {
                document.getElementById(textID).className = '';
            }
            //alert(document.getElementById(textID).className);
        }
        function check_box(id, objID) {
            //alert(document.getElementById(id).checked + "," + document.getElementById(objID).checked);
            if (document.getElementById(id).checked) {
                document.getElementById(objID).checked = !document.getElementById(id).checked;
            }

            //check_text(id, "span_" + id);
            //check_text(objID, "span_" + objID);
        }
        function check_box(id, objID, keyID, objKeyID) {
            //alert(document.getElementById(id).checked + "," + document.getElementById(objID).checked);
            if (document.getElementById(id).checked) {
                document.getElementById(objID).checked = !document.getElementById(id).checked;
            }

            check_text(id, "span_" + keyID);
            check_text(objID, "span_" + objKeyID);
        }

        function show_keyword_help() {
            //alert("You can remove the keyword by clearing the checkboxes.\nYou can also add or modify the keywords for further analysis.\nNotice that all data will be saved when you press the 'Submit' or 'Continue'.");
            window.open("../Example/ReferenceAnalysis.htm", "_blank", "width=800,height=600,scrollbars=yes");
        }

    </script>
</head>
<body>
    <script type="text/javascript">
        function edit_fuzzy(analysisID,keyID,groupID) {
            //if(window.confirm(analysisID+","+keyID+","+groupID))
            var modalWindowStyle = "dialogHeight: "+(screen.availHeight-100)+"px; dialogWidth: "+(screen.availWidth-100)+"px; scroll: no; help: no; resizable: yes; status: no;";
            var fuzzy = window.showModalDialog("iframe.aspx?URL=ReferenceAnalysis_EditFuzzy.aspx?AnalysisID="+analysisID+"$KeyID="+keyID+"$GroupID="+groupID+"&TITLE=EditGroup","ParseTable",modalWindowStyle);
            if(fuzzy!=null && fuzzy!="" && fuzzy!="undefined" && fuzzy!="FALSE") {
                //alert(fuzzy);
                fuzzy = "Add$" + keyID + "$" + fuzzy;
                <%=ClientScript.GetCallbackEventReference(this, "fuzzy", "reload_page", null) %>
            }
        }
        function reload_page(msg, context) {
            //alert(msg);
            //if(msg != "") {
                history.go();
            //}
        }
        function delete_fuzzy(keyID) {
            //if(window.confirm(analysisID+","+keyID+","+groupID))
            var fuzzy = "Del$" + keyID + "$";
            if(window.confirm("Do you want to delete the fuzzy relation?")) {
                <%=ClientScript.GetCallbackEventReference(this, "fuzzy", "reload_page", null) %>
            }
            //document.getElementById("chk_fuzzy_"+keyID).checked = false;
        }
        function re_analysis() {
            if(!window.confirm("You want to use the computer-suggested analysis result?\nIt will clear all current keywords.")) {
                event.returnValue = false;
            }
            
            //window.setTimeout("window.alert('Please select the keywords you need.')", 1000);
        }
        function redo_individual_analysis() {
            document.getElementById("<%=hfRedoAnalysis.ClientID %>").value = "False";
            if(window.confirm("Do you want to clear all the previous analyze results?\nIf you modify the keywords, we suggest you clear them all by clicking .Yes'.")) {
                document.getElementById("<%=hfRedoAnalysis.ClientID %>").value = "True";
            }
        }
        
        function on_mouse_up() {
            if(document.selection.createRange().text != "") {
                var text = document.selection.createRange().text;
                //alert(text);
                
                document.getElementById("<%=tbNewKeyword.ClientID %>").value = text;
                document.getElementById("<%=btAddKeyword.ClientID %>").click();
            }
        }
        function check_keyword() {
            var text = document.getElementById("<%=tbNewKeyword.ClientID %>").value;
            if(text == null || text == "") {
                window.alert("You should key in the words!");
                event.returnValue = false;
            }
        }
        
        function DeleteKeyword(QID,KeySeq)
        {
            AuthoringTool_CaseEditor_EssayQuestion_EditEssayKeyword.DeleteKeyword(QID,KeySeq);
            AuthoringTool_CaseEditor_EssayQuestion_EditEssayKeyword.init_keyword_table(QID);
        }
        
        function mouseover(obj)
        {
            obj.style.background = "#F3B9C6";
            obj.style.color = "#000000";
        }
        
        function mouseouteven(obj)
        {
            obj.style.background = "#EFF3FB";
            obj.style.color = "#000000";
        }
        
        function mouseoutodd(obj)
        {
            obj.style.background = "White";
            obj.style.color = "#000000";
        }
        
        //Drop down list event
        function SelectWeight(obj) {

            AuthoringTool_CaseEditor_EssayQuestion_EditEssayKeyword.UpdateWeight(obj.name, obj.value);

        }
           
        function finish() {
           window.close();
        }
        
        function KeywordSelectOnMouseUp() {
            //Activity_EEPBL_MLASEEPBLCaseInfo.Addnewtb_Click();
            try {
                var selectedText = "";
                if (window.getSelection) {  // all browsers, except IE before version 9
                    if (document.activeElement &&
                          (document.activeElement.tagName.toLowerCase() == "textarea" ||
                           document.activeElement.tagName.toLowerCase() == "input")) {
                        var text = document.activeElement.value;
                        selectedText = text.substring(document.activeElement.selectionStart,
                                                      document.activeElement.selectionEnd);
                    }
                    else {
                        var selRange = window.getSelection();
                        selectedText = selRange.toString();
                    }
                }
                else {
                    if (document.selection.createRange) { // Internet Explorer
                        var range = document.selection.createRange();
                        selectedText = range.text;
                    }
                }
                //alert(selectedText);
                if (selectedText != "") {                   
                    var OriKey = AuthoringTool_CaseEditor_Paper_Paper_EditQuestionKeyword.GetAllKeyword(document.getElementById("hfQID").value).value;
                    if (OriKey == selectedText) {
                        alert('您已新增過"' + selectedText + '"這個關鍵字!!!');
                    }
                    else {
                        document.getElementById('HiddenFieldfortext').value = selectedText;
                        document.getElementById('Addnewtb').click();
                    }
                }
            }
            catch (e) {
                alert(e);
            }
        }
        function KeywordRemoveOnMouseUp() {
            //Activity_EEPBL_MLASEEPBLCaseInfo.Addnewtb_Click();
            var selectedText = "";
            if (window.getSelection) {  // all browsers, except IE before version 9
                if (document.activeElement &&
                      (document.activeElement.tagName.toLowerCase() == "textarea" ||
                       document.activeElement.tagName.toLowerCase() == "input")) {
                    var text = document.activeElement.value;
                    selectedText = text.substring(document.activeElement.selectionStart,
                                                  document.activeElement.selectionEnd);
                }
                else {
                    var selRange = window.getSelection();
                    selectedText = selRange.toString();
                }
            }
            else {
                if (document.selection.createRange) { // Internet Explorer
                    var range = document.selection.createRange();
                    selectedText = range.text;
                }
            }
            //alert(selectedText);
            if (selectedText != "") {
                if (selectedText.indexOf(' ') >= 0 || selectedText.indexOf(',') >= 0)
                    alert('您刪除的同義詞不能包含「,」或「空白」字元!!!');
                else {
                    var OriKey = AuthoringTool_CaseEditor_Paper_Paper_EditQuestionKeyword.GetAllKeyword(document.getElementById("hfQID").value).value;
                    var CurrentKeyword = document.getElementById("<%=Lb_showKeyword.ClientID %>").innerText;
                    var SavedSynonyms = "";
                    var TmpArr = OriKey.split('|');
                    if (CurrentKeyword != "") {
                        for (var i = 0; i < TmpArr.length; i++) {
                            if (TmpArr[i].indexOf(CurrentKeyword) >= 0) {
                                SavedSynonyms = TmpArr[i];
                                break;
                            }
                        }
                        if (SavedSynonyms.indexOf(selectedText) < 0)
                            alert('您只能刪除Current Keyword: ' + CurrentKeyword + '下的同義詞!!!');
                        else {
                            SavedSynonyms = SavedSynonyms.substring(0, SavedSynonyms.indexOf(selectedText)) + SavedSynonyms.substring((SavedSynonyms.indexOf(selectedText) + 1 + selectedText.length), SavedSynonyms.length);
                            if (SavedSynonyms.substring(SavedSynonyms.length - 1, SavedSynonyms.length) == ',')
                                SavedSynonyms = SavedSynonyms.substring(0, SavedSynonyms.length - 1);
                            //alert(SavedSynonyms);
                            var AllSavedKeyword = "";
                            for (var j = 0; j < TmpArr.length; j++) {
                                if (TmpArr[j].indexOf(CurrentKeyword) >= 0) {
                                    AllSavedKeyword += SavedSynonyms + "|";
                                }
                                else
                                    AllSavedKeyword += TmpArr[j] + "|";
                            }
                            AllSavedKeyword = AllSavedKeyword.substring(0, AllSavedKeyword.length - 1);
                            //alert(AllSavedKeyword);
                            AuthoringTool_CaseEditor_Paper_Paper_EditQuestionKeyword.SaveSynonyms(AllSavedKeyword, document.getElementById("hfQID").value);
                            document.getElementById("btnKeyTableNoSearch").click();
                        }
                    }
                }
            }
        }
        function AddNewSynonyms(strCheckID, strQID) {
            var strNewSynonyms = strCheckID.split('_');
            var strTmp3 = AuthoringTool_CaseEditor_Paper_Paper_EditQuestionKeyword.GetAllKeyword(strQID).value;
            if (strTmp3.indexOf(strNewSynonyms[1]) < 0) {
                GetAllCheckedSynonyms(strTmp3 + "," + strNewSynonyms[1], strQID, false);
            }
            else {
                var tmpSplit = strTmp3.split(strNewSynonyms[1]);
                tmpSplit[0] = tmpSplit[0].substring(0, tmpSplit[0].length - 1);
                var tmpSub = tmpSplit[0] + tmpSplit[1];
                GetAllCheckedSynonyms(tmpSub, strQID, false);
            }
        }

        function GetAllCheckedSynonyms(strAllKeyword, strQID, bIsSearch) {
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
            AuthoringTool_CaseEditor_Paper_Paper_EditQuestionKeyword.SaveSynonyms(strTmp, strQID);
            if (bIsSearch)
                document.getElementById("btnKeyTable").click();
            else
                document.getElementById("btnKeyTableNoSearch").click();
        }
        function NextStep() {
            var strTmp2 = AuthoringTool_CaseEditor_Paper_Paper_EditQuestionKeyword.GetAllKeyword(document.getElementById("hfQID").value).value;
            GetAllCheckedSynonyms(strTmp2, document.getElementById("hfQID").value, false);
            document.getElementById("btnFinish").click();
        }

        function Cancel() {
            document.getElementById("btnFinish").click();
        }
        function DeleteCurrentKeyword(DeleteID) {
            var DeleteKey = document.getElementById(DeleteID).value;
            document.getElementById('HiddenFieldforRemove').value = DeleteKey;
            document.getElementById('Remove_Btn').click();
        }
        function checkAndSave() {
            var NewType = document.getElementById("<%=tbTypeNewKeyword.ClientID %>").value;
                if (NewType != "") {
                    var OriKey = AuthoringTool_CaseEditor_Paper_Paper_EditQuestionKeyword.GetAllKeyword(document.getElementById("hfQID").value).value;
                    if (OriKey == NewType) {
                        alert('您已新增過"' + NewType + '"這個關鍵字!!!');
                    }
                    else {
                        document.getElementById('HiddenFieldfortext').value = NewType;
                        document.getElementById('Addnewtb').click();
                    }
                }
        }
        function checkAndSaveSynonyms() {
            var NewType = document.getElementById("<%=tbTypeNewSynonyms.ClientID %>").value;
            if (NewType != "") {
                var OriKey = AuthoringTool_CaseEditor_Paper_Paper_EditQuestionKeyword.GetAllKeyword(document.getElementById("hfQID").value).value;
                var CurrentKeyword = document.getElementById("<%=Lb_showKeyword.ClientID %>").innerText;
                var SavedSynonyms = "";
                var TmpArr = OriKey.split('|');
                if (CurrentKeyword != "") {
                    for (var i = 0; i < TmpArr.length; i++) {
                        if (TmpArr[i].indexOf(CurrentKeyword) >= 0) {
                            SavedSynonyms = TmpArr[i];
                            break;
                        }
                    }
                    SavedSynonyms += ("," + NewType);
                    //alert(SavedSynonyms);
                    var AllSavedKeyword = "";
                    for (var j = 0; j < TmpArr.length; j++) {
                        if (TmpArr[j].indexOf(CurrentKeyword) >= 0) {
                            AllSavedKeyword += SavedSynonyms + "|";
                        }
                        else
                            AllSavedKeyword += TmpArr[j] + "|";
                    }                    
                    AllSavedKeyword = AllSavedKeyword.substring(0, AllSavedKeyword.length - 1);
                    //alert(AllSavedKeyword);
                    AuthoringTool_CaseEditor_Paper_Paper_EditQuestionKeyword.SaveSynonyms(AllSavedKeyword, document.getElementById("hfQID").value);
                    document.getElementById("btnKeyTableNoSearch").click();
                }
                else
                    alert('目前沒有Current Keyword，故無法手動加入新同義詞!!!');
            }
        }

        window.onload = function () {
            var h = document.getElementById("<%=hfScrollPosition.ClientID%>");
            document.getElementById("<%=scrollArea.ClientID%>").scrollTop = h.value;
          }
          function SetDivPosition() {
              var intY = document.getElementById("<%=scrollArea.ClientID%>").scrollTop; var h = document.getElementById("<%=hfScrollPosition.ClientID%>");
            h.value = intY;
        }

        function afterpostback() {
            var h = document.getElementById("<%=hfScrollPosition.ClientID%>");
            document.getElementById("<%=scrollArea.ClientID%>").scrollTop = h.value;
        }
    </script>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hfScrollPosition" runat="server" Value="0"/>
    <div id="scrollArea" onscroll="SetDivPosition()" runat="server" class="div_body" style="background-color:#ebecee; height:100%; overflow:auto; overflow-x:hidden;" >
        <div class="div_h3">Essay Question Keyword Editor</div>
        <div id="targetSentence" class="div_analysis_content">
            <hr />
            <div class="div_section1">
                <div class="div_title1">
                    Please enter keywords by <span style=' font-weight:bold'>highlight</span> the words in the question
                    or <span style=' font-weight:bold'>entering</span> keywords in the box below.
                </div>
                <div style="text-align: center;">
                    <div id="lOriginalContent" class="paragraph_content" runat="server" >                      
                    </div>
                    <asp:HiddenField ID="hfOriginalContent" runat="server" />
                    <asp:HiddenField ID="hfAnalysisID" runat="server" Value="_Test000" />
                </div>
            </div>
            <hr />
            <div id="div_section2" class="div_section2" style="display:none;">
                <div class="div_title1">
                    The following area shows the keywords of this content. 
                    &nbsp;&nbsp;
                </div>
                <div style="text-align: center;">
                    <div style="width: 95%; text-align: left; padding-bottom: 10px;">
                        <asp:Button ID="btAddKeyword" runat="server" Text="Add" Width="100" OnClientClick="check_keyword()" OnClick="btAddKeyword_Click" />
                        <asp:TextBox ID="tbNewKeyword" runat="server" Width="30%" ToolTip="New keyword"></asp:TextBox>
                    </div>
                    <div style="width: 96%;">

                        <asp:PlaceHolder ID="phKeywordList" runat="server" Visible=false></asp:PlaceHolder>

                        <asp:GridView ID="gvShow" runat="server" Width=95% CellSpacing="5" 
                            CellPadding="5" ForeColor="#333333" BorderWidth=1px BorderStyle=Outset 
                             AutoGenerateColumns="False" DataSourceID="SqlDataSource1" BackColor=#C6D9FF
                            DataKeyNames="cKeywordID" onrowdatabound="gvShow_RowDataBound" 
                            onselectedindexchanging="gvShow_SelectedIndexChanging">
                            <RowStyle BackColor="#EFF3FB" />
                            
                            <Columns>
                                <asp:TemplateField HeaderText= "No">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DisplayIndex +1 %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="cKeywordID" DataFormatString="{0:000#}" 
                                    HeaderText="cKeywordID" ReadOnly="True" SortExpression="cKeywordID" 
                                    Visible="False" />
                                <asp:BoundField DataField="cQID" HeaderText="cQID" SortExpression="cQID" 
                                    Visible="False" />
                                <asp:TemplateField HeaderText= "Weight">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlShowWeight" runat="server" onchange=SelectWeight(this)>
                                    </asp:DropDownList>
                                </ItemTemplate>
 
                                </asp:TemplateField>
                                <asp:BoundField DataField="iWeight" HeaderText="Weight" 
                                    SortExpression="iWeight" ReadOnly="True" Visible=false />
                                <asp:BoundField DataField="cKeyword" HeaderText="Keyword" 
                                    SortExpression="cKeyword" />
                                <asp:CommandField ShowEditButton="True" HeaderText="Edit" 
                                    ButtonType="Image" DeleteImageUrl="~/App_Themes/djrm1/delete.gif" 
                                    EditImageUrl="~/App_Themes/djrm1/Edit.gif" 
                                    CancelImageUrl="~/App_Themes/djrm1/revert.png" 
                                    UpdateImageUrl="~/App_Themes/djrm1/accept.png" >
                                <ItemStyle BorderWidth="0px" />
                                </asp:CommandField>
                                 <asp:TemplateField HeaderText= "Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/App_Themes/djrm1/delete.gif" CommandArgument='<%# Eval("cKeywordID") %>' OnClick="btnDelete_Click"  />
                                </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:NewVersionHintsDBConnectionString %>" 
                            SelectCommand="SELECT * FROM [Paper_TextQuestionKeyword] WHERE ([cQID] = @cQID)"
                            
                            UpdateCommand = "UPDATE [Paper_TextQuestionKeyword] SET  [cKeyword] = @cKeyword WHERE [cKeywordID] = @original_cKeywordID" 
                            
                            DeleteCommand="DELETE FROM [Paper_TextQuestionKeyword] WHERE [cKeywordID] = @original_cKeywordID" 
                            InsertCommand="INSERT INTO [Paper_TextQuestionKeyword] ([cKeywordID], [cQID], [iWeight], [cKeyword]) VALUES (@cKeywordID, @cQID, @iWeight, @cKeyword)" 
                            OldValuesParameterFormatString="original_{0}"  >
                            
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hfQID" Name="cQID" PropertyName="Value" 
                                    Type="String" />
                            </SelectParameters>
                            <DeleteParameters>
                                <asp:Parameter Name="original_cKeywordID" Type="String" />
                            </DeleteParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="cQID" Type="String" />
                                <asp:Parameter Name="iWeight" Type="Int32" />
                                <asp:Parameter Name="cKeyword" Type="String" />
                                <asp:Parameter Name="original_cKeywordID" Type="String" />
                            </UpdateParameters>
                            <InsertParameters>
                                <asp:Parameter Name="cKeywordID" Type="String" />
                                <asp:Parameter Name="cQID" Type="String" />
                                <asp:Parameter Name="iWeight" Type="Int32" />
                                <asp:Parameter Name="cKeyword" Type="String" />
                            </InsertParameters>
                        </asp:SqlDataSource>
                    </div>
                </div>
            </div>
            <div id="div_section3" class="div_section3">
               <div style="width: 95%; text-align: left;">  
                    <span style="text-align: left; vertical-align: middle;">Keywords：</span>              
                    &nbsp;<asp:TextBox ID="tbTypeNewKeyword" runat="server" Width="30%" ToolTip="Type new keyword here!" BackColor="White" BorderWidth="1" Font-Size="16px" align="middle"></asp:TextBox>&nbsp;&nbsp;<input id="btnTypeNewKeyword" type="button" value="Add" class="button_continue" onclick="checkAndSave()" style="text-align: center; width: 100px; vertical-align:middle;" />
               </div>
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
                            <td style="text-align:left">
                                <table ID="Lb_synonyms" runat="server" style="padding: 0px; margin: 0px; text-align: left; width:100%">
                                </table>
                            </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label_Keyword_Learn" runat="server" ForeColor="Blue" Text="(Hints1: You can choose the Synonyms you want, or you can entering Synonyms in the box below.)"></asp:Label>
                                </td>
                            </tr>
                        </table>               
                    </td>
                </tr> 
                <tr>
                    <td>
                       <span style="text-align: left; vertical-align: middle;">Synonyms：</span>              
                       &nbsp;<asp:TextBox ID="tbTypeNewSynonyms" runat="server" Width="30%" ToolTip="Type new synonyms here!" BackColor="White" BorderWidth="1" Font-Size="16px" align="middle"></asp:TextBox>&nbsp;&nbsp;<input id="btnTypeNewSynonyms" type="button" value="Add" class="button_continue" onclick="checkAndSaveSynonyms()" style="text-align: center; width: 100px; vertical-align:middle;" />
                        <br/><asp:Label ID="Label2" runat="server" ForeColor="Blue" Text="(Hints2: If you want to delete the synonyms of current keyword, you can highlight the synonym in the row of current keyword to delete.)"></asp:Label>
                    </td>
                </tr>
              </table>
            </div>
            <hr />
            <div id="div_section4" class="div_section4">
              <table id="tbTotalKeyword" runat="server" style="border: 1px solid #000000; width: 90%" align="center">
              </table>
            </div>
            <hr />          
            <div align=center class="div_bottom_control">
                <asp:HiddenField ID="hfKeywordControl" runat="server" />
                <asp:HiddenField ID="hfRedoAnalysis" runat="server" />
                <asp:HiddenField ID="hfQID" runat="server" />
                <asp:HiddenField ID="hfGroupID" runat="server" />
                <asp:HiddenField ID="hfAID" runat="server" />
                <asp:HiddenField ID="HiddenFieldfortext" runat="server" />
                <asp:HiddenField ID="HiddenFieldforRemove" runat="server" />
                
                <input id="btnNext" runat="server" style="width: 150px;" onclick="NextStep()"
                    type="button" value="Finish" name="btnNext" class="button_continue" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input id="btnCanel" value="Canel" class="button_continue" onclick="Cancel()" type="button" style="width: 150px;">
                <input id="btnFinish" runat="server" type="button" style="display:none;" name="btnFinish" class="button_continue">
                <input id="btnKeyTable" runat="server" type="button" style="display:none;" name="btnKeyTable" class="button_continue">
                <input id="btnKeyTableNoSearch" runat="server" type="button" style="display:none;" name="btnKeyTableNoSearch" class="button_continue">
                <asp:Button ID="btReAnalyze" runat="server" Text="Load suggested keywords" OnClientClick="re_analysis()" OnClick="btReAnalyze_Click" Visible=false />&nbsp;&nbsp;
                <asp:Button ID="btSubmit" runat="server" Text="Save" Width="100" OnClick="btSubmit_Click" ToolTip="Save data"  Visible=false />&nbsp;&nbsp;
                <asp:Button ID="btAnalyze" runat="server" Text="Continue" Width="100" OnClientClick="redo_individual_analysis()" OnClick="btAnalyze_Click" ToolTip="next step: individual analysis"  Visible=false />
                <asp:Button runat ="server" onclick="Addnewtb_Click" id="Addnewtb"  Width="0" />
                <asp:Button runat ="server" onclick="Remove_Click" id="Remove_Btn"  Width="0" />
            </div>
        </div>
    </div>     
    </form>
</body>
</html>


