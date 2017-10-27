<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Register Src="~/AuthoringTool/MultiMediaDB/Preview.ascx" TagName="Preview" TagPrefix="uc3" %>

<%@ Page Language="c#" ValidateRequest="false" Inherits="PaperSystem.Paper_ProgramQuestionEditorNew"
    CodeFile="Paper_ProgramQuestionEditorNew.aspx.cs" %>

<html>
<head id="Head1" runat="server">
    <title>Paper_TextQuestionEditortamsky</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

    <script language="javascript" src="../EditHtml/editor.js"></script>

</head>
<body>

    <script language="Javascript">
			_editor_url = "/hints/authoringtool/caseeditor/EditHtml/"; 
			var Bold = "<%= title_Bold%>";
			var Italic = "<%= title_Italic%>";
			var Underline = "<%= title_Underline%>";
			var Strikethrough = "<%= title_Strikethrough%>";
			var Subscript = "<%= title_Subscript%>";
			var Superscript = "<%= title_Superscript%>";
			var JustifyLeft = "<%= title_JustifyLeft%>";
			var JustifyCenter = "<%= title_JustifyCenter%>";
			var JustifyRight = "<%= title_JustifyRight%>";
			var OrderedList = "<%= title_OrderedList%>";
			var BulletedList = "<%= title_BulletedList%>";
			var DecreaseIndent = "<%= title_DecreaseIndent%>";
			var IncreaseIndent = "<%= title_IncreaseIndent%>";
			var FontColor = "<%= title_FontColor%>";
			var BackgroundColor = "<%= title_BackgroundColor%>";
			var HorizontalRule = "<%= title_HorizontalRule%>";
			var UploadFile = "<%= title_UploadFile%>";
			var InsertWebLink = "<%= title_InsertWebLink%>";
			var InsertImage = "<%= title_InsertImage%>";
			var InsertTable = "<%= title_InsertTable%>";
			var ViewHTMLSource = "<%= title_ViewHTMLSource%>";
			var EnlargeEditor = "<%= title_EnlargeEditor%>";
			var Aboutthiseditor = "<%= title_Aboutthiseditor%>";
			function transferHTMLToOpener()
			{
			    window.dialogArguments.targetTextBoxToEditHTML.value = document.all("txtQuestionEdit").value;
			    window.close();
			}
			
			function addFile(strSrc , strContentType)
			{
				var strMediaTag = strSrc;
				/*
				if(strContentType == "image/gif" || strContentType == "image/jpg" || strContentType == "image/jpeg" || strContentType == "image/pjpeg" || strContentType == "image/bmp" || strContentType == "image/x-png")
				{
					strMediaTag = "<IMG src=" + strSrc + ">";
				}
				
				else
				{
					strMediaTag += "<DIV id='testNS' style='VERTICAL-ALIGN: super; WIDTH: 281px; HEIGHT: 64px'>"
					strMediaTag += "<OBJECT id='nstv' type='application/x-oleobject' height='300' width='300' classid='CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6' name='nstv'>"
					strMediaTag += "	<PARAM NAME='autoStart' VALUE='0'>"
					strMediaTag += "	<PARAM NAME='URL' VALUE='"+strSrc+"'>"
					strMediaTag += "	<PARAM NAME='rate' VALUE='1'>"
					strMediaTag += "	<PARAM NAME='balance' VALUE='0'>"
					strMediaTag += "	<PARAM NAME='currentPosition' VALUE='0'>"
					strMediaTag += "	<PARAM NAME='defaultFrame' VALUE=''>"
					strMediaTag += "	<PARAM NAME='playCount' VALUE='1'>"
					strMediaTag += "	<PARAM NAME='currentMarker' VALUE='0'>"
					strMediaTag += "	<PARAM NAME='invokeURLs' VALUE='-1'>"
					strMediaTag += "	<PARAM NAME='baseURL' VALUE=''>"
					strMediaTag += "	<PARAM NAME='volume' VALUE='100'>"
					strMediaTag += "	<PARAM NAME='mute' VALUE='0'>"
					strMediaTag += "	<PARAM NAME='uiMode' VALUE='full'>"
					strMediaTag += "	<PARAM NAME='stretchToFit' VALUE='0'>"
					
					strMediaTag += "	<PARAM NAME='windowlessVideo' VALUE='0'>"
					strMediaTag += "	<PARAM NAME='enabled' VALUE='-1'>"
					strMediaTag += "	<PARAM NAME='enableContextMenu' VALUE='0'>"
					
					strMediaTag += "	<PARAM NAME='fullScreen' VALUE='0'>"
					strMediaTag += "	<PARAM NAME='SAMIStyle' VALUE=''>"
					strMediaTag += "	<PARAM NAME='SAMILang' VALUE=''>"
					strMediaTag += "	<PARAM NAME='SAMIFilename' VALUE=''>"
					strMediaTag += "	<PARAM NAME='captioningID' VALUE=''>"
					strMediaTag += "	<PARAM NAME='enableErrorDialogs' VALUE='0'>"
					strMediaTag += "	<PARAM NAME='_cx' VALUE='7408'>"
					strMediaTag += "	<PARAM NAME='_cy' VALUE='1588'>"
					strMediaTag += "</OBJECT>"
					
				}
			*/	
				strMediaTag = strMediaTag.replace(/\&lt;/g, "<"); 
				
				strMediaTag = strMediaTag.replace(/\gt;/g, ">"); 
				
				//alert(strMediaTag);
				var str = document.selection.createRange();
				str.text = strMediaTag;
				
				document.getElementById("txtQuestionEdit").innerHTML = document.getElementById("txtQuestionEdit").innerHTML.replace(/\&lt;/g, "<");
				document.getElementById("txtQuestionEdit").innerHTML = document.getElementById("txtQuestionEdit").innerHTML.replace(/\gt;/g, ">");
				document.getElementById("txtAnswerEdit").innerHTML = document.getElementById("txtAnswerEdit").innerHTML.replace(/\&lt;/g, "<");
				document.getElementById("txtAnswerEdit").innerHTML = document.getElementById("txtAnswerEdit").innerHTML.replace(/\gt;/g, ">"); 
			}
			
			function Clear()
			{
				if(document.all("txtQuestionEdit") != null)
				{
					document.all("txtQuestionEdit").value = "";
	             }

	            if (document.all("txtAnswerEdit") != null) {
	                document.all("txtAnswerEdit").value = "";
	            }
				
			}

    </script>

    <form id="Form1" method="post" runat="server">
    <table id="body_content" width="100%" align="center">
        <tr height="25px">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Paper Essay Question Editor
            </td>
        </tr>
        <tr>
            <td>
                <hr />
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtQuestionData" Style="display: none" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtAnswerData" Style="display: none" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtTestData" Style="display: none" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtOutputFormatExData" Style="display: none" runat="server"></asp:TextBox>
               

                <div style="position: absolute; top: 600px">
                    <input id="hiddenOpener" type="hidden" name="hiddenOpener" value="" runat="server"/>
                    <input id="hiddenQuestionMode" type="hidden" name="hiddenQuestionMode" runat="server"/>
                    <input id="hiddenPresentType" type="hidden" name="hiddenPresentType" runat="server"/>
                    <input id="hiddenEditMode" type="hidden" name="hiddenEditMode" runat="server"/>
                    <input id="hiddenModifyType" type="hidden" name="hiddenModifyType" value="" runat="server"/>
                    <input id="hiddenCaseID" type="hidden" name="hiddenCaseID" value="" runat="server"/>
                    <input id="hiddenSectionName" type="hidden" name="hiddenSectionName" value="" runat="server"/>
                    <input id="hiddenPreOpener" type="hidden" name="hiddenPreOpener" value="" runat="server"/>
                    <asp:HiddenField ID="hfPaperID" runat="server" />
                    <asp:HiddenField ID="hfGroupID" runat="server" />
                    <asp:HiddenField ID="hfGroupSerialNum" runat="server" />
                </div>
            </td>
        </tr>
        <%--Question Content  with CKEditor for teacher to highlight the key points in the question description--%>
        <tr>
            <td>
                Question Content : &nbsp;
                <asp:Button ID="btAddSynQuestion" runat="server" Text="Add Synonymous Question" Style="cursor: hand"
                    CssClass="button_continue" Width="250px" OnClick="btAddSynQuestion_Click" OnClientClick="TransferTextData();" />
            </td>
        </tr>
        <tr>
            <td>
                <div align="right">
                    <textarea style="width: 100%; height: 200px" name="txtQuestionEdit" id="txtQuestionEdit"></textarea>
                    <!--<input id="Image1" type="image" name="Image1" runat="server" style="DISPLAY: none; CURSOR: hand">-->
                    
                </div>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Table ID="tbInterrogationClassQutstion" runat="server" BorderWidth="1px" BorderColor="Blue"
                    GridLines="Both" Width="100%">
                </asp:Table>
            </td>
        </tr>

        <%--output format example--%>
        <tr>
            <td>
                <hr id="hr1" runat="server" />
                Output Format Example :&nbsp;
                
            </td>
        </tr>
        <tr>
            <td>
                <div align="right">
                    <textarea style="width: 100%; height: 200px" name="txtOutputFormatExEdit" id="txtOutputFormatExEdit"></textarea>
                    
                </div>
            </td>
        </tr>

         <%--testing data--%>
        <tr>
            <td>
                <hr id="hr2" runat="server" />
                Testing Data:&nbsp;
                
                    
            </td>
        </tr>
        <tr>
            <td>
                <div align="right">
                    <textarea style="width: 100%; height: 200px" name="txtTestDataEdit" id="txtTestDataEdit"></textarea>
                    
                </div>
            </td>
        </tr>

        <%--corresponding answer content--%>
        <tr>
            <td>
                <hr id="hrQuestion" runat="server" />
                Corresponding Answer Content :&nbsp;
                <asp:Button ID="btAddSynAnswer" runat="server" Text="Add Synonymous Answer" Style="cursor: hand"
                    CssClass="button_continue" Width="250px" OnClick="btAddSynAnswer_Click" OnClientClick="TransferTextData();" />
            </td>
        </tr>
        <tr>
            <td>
                <div align="right">
                    <textarea style="width: 100%; height: 200px" name="txtAnswerEdit" id="txtAnswerEdit"></textarea>
                    
                </div>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Table ID="tbInterrogationClassAnswer" runat="server" BorderWidth="1px" BorderColor="Blue"
                    GridLines="Both" Width="100%">
                </asp:Table>
            </td>
        </tr>
        <tr style="border-width:1px; border-style:solid;">
            <td style="border:#000000 2px double; ">
                <hr id="hrAnswer" runat="server" />
                Keyword content :<asp:Button ID="btnEditKeyword" CssClass="button_continue" Style="width: 150px;" runat="server" Text="Edit Keywords" OnClientClick="TransferTextData();" OnClick="btnEditKeyword_Click" /><br />
                <div id="divKeyWords" runat="server" align="center"></div>
                <asp:Label ID="lbKeyword" runat="server" style="display:inline" Text=""></asp:Label>
            </td>
        </tr>
        <tr height="10px">
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td height="10px">
                <hr>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbQuestionLevel" runat="server" Text="Question Level :  &nbsp;" style="display:none"></asp:Label>
                <asp:DropDownList ID="ddlQuestionLevel" runat="server" style="display:none">
                </asp:DropDownList>
                <asp:Label ID="lbSymptoms" runat="server" Text="Symptoms :"></asp:Label>
                <asp:DropDownList ID="ddlSymptoms" runat="server"> 
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lbAnswerOptions" runat="server" Text="請選擇欲編輯的答案 :" Width="45mm"></asp:Label>
                <asp:DropDownList ID="ddlAnswerOptions" runat="server"
                Width="250px" Font-Bold="True" AutoPostBack="True">
                </asp:DropDownList>
                <hr />
                <asp:Panel ID="PanelFeature" runat="server">
                </asp:Panel>
                <hr />
            </td>
        </tr>
        <tr width="100%">
            <td align="right">
                <input id="btnSaveNextQuestion" style="width: 0px" type="button" name="btnSaveNextQuestion"
                    runat="server" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input id="btnSaveNext" style="width: 0px" type="button" name="btnSaveNext" runat="server" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <input id="btnNextQuestion" style="width: 150px; cursor: hand; height: 30px" onclick="NextQuestion()"
                    type="button" value="Edit next question >>" name="btnNextQuestion" class="button_continue" runat="server" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <input id="btnContinue" style="display: none; cursor: hand" onclick="transferHTMLToOpener()"
                    type="image" src="/webhints/ILView/r-botton/finish.gif" name="btnContinue" runat="server" />
                <input id="btnBack" style="width: 150px; cursor: hand; height: 30px" onclick="goBack()"
                    type="button" value="<< Back" name="btnBack" class="button_continue" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <input id="btnNext" style="width: 150px; cursor: hand; height: 30px" onclick="NextStep()"
                    type="button" value="Next >>" name="btnNext" class="button_continue"  />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btSaveNew" runat="server" Text="Save as a new question" CssClass="button_continue" Visible="false"
                    Style="width: 200px; cursor: hand; height: 30px;" OnClick="btSaveNew_Click" OnClientClick="SaveNew()" />
                &nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    </form>

    <script language="javascript">
			editor_generate('txtQuestionEdit');

			editor_generate('txtAnswerEdit');
			
			function NextQuestion()
			{
				//將txtQuestionEdit的資料存入txtQuestionData
				//alert(document.all("txtQuestionEdit").value);
			    document.all("txtQuestionData").value = document.all("txtQuestionEdit").value;
			    document.all("txtAnswerData").value = document.all("txtAnswerEdit").value;
				
				//alert(document.all("txtQuestionData").value);
				document.getElementById("btnSaveNextQuestion").click();
			}

			function TransferTextData() {
			    document.all("txtQuestionData").value = document.all("txtQuestionEdit").value;
			    document.all("txtAnswerData").value = document.all("txtAnswerEdit").value;
			    document.all("txtTestData").value = document.all("txtTestDataEdit").value;
			    document.all("txtOutputFormatExData").value = document.all("txtOutputFormatExEdit").value;
			}

			function NextStep() {
			    //將txtQuestionEdit的資料存入txtQuestionData
			    //alert(document.all("txtQuestionEdit").value);
			    //document.all("txtQuestionData").value = document.all("txtQuestionEdit").value;
			    //document.all("txtAnswerData").value = document.all("txtAnswerEdit").value;
			    TransferTextData();

			    //alert(document.all("txtQuestionData").value);
			    document.getElementById("btnSaveNext").click();

			}
			
			function SaveNew()
			{
			 TransferTextData();
			}
			
			function goBack() {
			    if (window.confirm("您的資料將不會被儲存，您確定要繼續嗎?")) {



			        //the original 回上一頁 mechanism
			        var BackUrl = document.getElementById("hiddenOpener").value;
			        var varGroupID = document.getElementById("hfGroupID").value;
			        if (BackUrl != "") {


			            if (document.getElementById("hiddenPreOpener").value == "SelectPaperMode")
			                window.location.href = BackUrl + ".aspx?Opener=SelectPaperMode&cCaseID=" + document.getElementById("hiddenCaseID").value + "&cSectionName=" + document.getElementById("hiddenSectionName").value + "&cPaperID=" + document.getElementById("hfPaperID").value;

			                //Ben check
			                //產生新題目流程中的回上一頁機制
			            else if (BackUrl == "Paper_QuestionTypeNew") {
			                window.location.href = BackUrl + ".aspx?Opener=./QuestionGroupTree/QGroupTreeNew&bModify=False&GroupID=" + varGroupID;
			            }


			            else
			                //the original 回上一頁 mechanism
			                window.location.href = BackUrl + ".aspx?Opener=Paper_QuestionViewNew&GroupID=" + varGroupID + "";

			        }
			        else {
			            var PaperID = document.getElementById("hfPaperID").value;
			            if (PaperID == "")//由編輯題庫地方來的
			            {
			                //window.location.href = "Paper_QuestionMain.aspx";
			                window.location.href = "Paper_QuestionViewNew.aspx";
			            }
			            else {
			                window.history.back();
			            }
			        }
			    }
			}

			document.all("txtQuestionEdit").value = document.all("txtQuestionData").value;
			document.all("txtAnswerEdit").value = document.all("txtAnswerData").value;
			document.all("txtTestDataEdit").value = document.all("txtTestData").value;
			document.all("txtOutputFormatExEdit").value = document.all("txtOutputFormatExData").value;
			
			function ShowDetail(strSelectionTRID , strImgID)
            {
                if(document.getElementById(strSelectionTRID) != null)
                {
                    if(document.getElementById(strSelectionTRID).style.display == "none")
                    {
                        document.getElementById(strSelectionTRID).style.display = "";
                        document.getElementById(strImgID).src = "../../../BasicForm/Image/minus.gif";
                    }
                    else
                    {
                        document.getElementById(strSelectionTRID).style.display = "none";
                        document.getElementById(strImgID).src = "../../../BasicForm/Image/plus.gif";
                    }
                }
            }
            
            function PlayFlash(ItemSeq, FileNameNoVice)
            {
               Player = document.getElementById("FlashPlayer" + ItemSeq + FileNameNoVice); 
               Player.Play();
            }

    </script>

    <%=strClientScript%>
</body>
</html>
