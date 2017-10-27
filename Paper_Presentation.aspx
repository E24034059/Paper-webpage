<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page language="c#" Inherits="PaperSystem.Paper_Presentation" CodeFile="Paper_Presentation.aspx.cs" %>

<HTML>
	<HEAD runat="server">
		<title>Paper_Presentation</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="ajax/common.ashx"></script>
		<script language="javascript" src="ajax/PaperSystem,Paper_Presentation.ashx"></script>
		<script language="javascript">
			function openDlDuration()
			{
				var strDlDuration = "dlTestDuration";
				
				if(document.getElementById(strDlDuration) != null)
				{
					document.getElementById(strDlDuration).disabled = "";
				}
			}
			
			function closeDlDuration()
			{
				var strDlDuration = "dlTestDuration";
				
				if(document.getElementById(strDlDuration) != null)
				{
					document.getElementById(strDlDuration).disabled = "disabled";
				}
			}
			
			//開啟dlSection
			function EnableDlIndexBySelectionID(strSelectionID)
			{
				var strDlSectionID = "dlSelection-Section-"+strSelectionID;
				var strDlIndexID = "dlSelection-Index-"+strSelectionID;
				
				if(document.getElementById(strDlSectionID) != null)
				{
					document.getElementById(strDlSectionID).disabled = "disabled";
				}
				
				if(document.getElementById(strDlIndexID) != null)
				{
					document.getElementById(strDlIndexID).disabled = "";
				}
			}
			
			//開啟dlIndex
			function EnableDlSectionBySelectionID(strSelectionID)
			{
				var strDlIndexID = "dlSelection-Index-"+strSelectionID;
				var strDlSectionID = "dlSelection-Section-"+strSelectionID;
				
				if(document.getElementById(strDlSectionID) != null)
				{
					document.getElementById(strDlSectionID).disabled = "";
				}
				
				if(document.getElementById(strDlIndexID) != null)
				{
					document.getElementById(strDlIndexID).disabled = "disabled";
				}
			}
			
			//開啟Default
			function EnableDlDefaultBySelectionID(strSelectionID)
			{
				var strDlIndexID = "dlSelection-Index-"+strSelectionID;
				var strDlSectionID = "dlSelection-Section-"+strSelectionID;
				
				if(document.getElementById(strDlSectionID) != null)
				{
					document.getElementById(strDlSectionID).disabled = "disabled";
				}
				
				if(document.getElementById(strDlIndexID) != null)
				{
					document.getElementById(strDlIndexID).disabled = "disabled";
				}
			}
			
			function callRetrySetting(strPaperID , strCaseID , strClinicNum , strSectionName , strQID)
			{  
			    var DivisionID = document.getElementById("hfDivisionID").value;
			    window.open("./QandA_select_setRetry.aspx?CaseID=" + strCaseID + "&CurrentTerm=" + strClinicNum + "&SectionName=" + strSectionName + "&cQID=" + strQID + "&cDivisionID=" + DivisionID + "","","toolbar=no,status=no,location=no,menubar=no, resizable=1, scrollbars=1");
			}
			
			function goNext()
			{
				alert("Save successfully");

				if (document.getElementById("hiddenPreOpener").value == "SelectPaperMode")
				    location.href = "Paper_MainPage.aspx?Opener=SelectPaperMode"
				else
				    location.href = "Paper_MainPage.aspx?Opener=Paper_Presentation";
			}
			
			function goBack()
			{
				if(window.confirm("您的資料將不會被儲存，要繼續嗎?"))
				{
				    if (document.getElementById("hiddenPreOpener").value == "SelectPaperMode")
				        location.href = "Paper_MainPage.aspx?Opener=SelectPaperMode";
				    else
				        location.href = "Paper_MainPage.aspx?Opener=Paper_Presentation";
				}
			}
			
			function PreAssign()
			{
			    window.open("PreAssignQuestions.aspx",null,"width=900,height=300,left=6000,top=6000");
			}
			function OpenTDTemplate()
			{
			    var PaperID = document.getElementById("hfPaperID").value; 
			    var CaseID = document.getElementById("hfCaseID").value;
			    window.open("../../TableDriven/TDForQandA.aspx?PaperID=" + PaperID + "&CaseID=" + CaseID + "");
			}
			function DeleteTDTemplate()
			{
			  var PaperID = document.getElementById("hfPaperID").value; 
			  var CaseID = document.getElementById("hfCaseID").value;
			  
			  Paper_Presentation.DeleteTDTemplate(PaperID,CaseID);
			  
			  window.opener.location.reload();
			}
			function OpenAnsProcessTool()
			{
			    var PaperID = document.getElementById("hfPaperID").value; 
			    var CaseID = document.getElementById("hfCaseID").value;
			    var AnsProcessID = ""; 
			    AnsProcessID = Paper_Presentation.GetPaperAnsProcessID(PaperID,CaseID).value;  
			    window.open("AnsProcess.aspx?AnsProcessID=" + AnsProcessID + "&AccessDBO=AnsProcess_Paper&CallType=Paper", '_blank', 'directories=0, height=375, location=0, menubar=0, width=960');
			}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table align="center" id="body_content" style="WIDTH: 100%">
				<tr>
					<td id="tdTitle" class="title" runat="server"></td>
				</tr>
				<tr>
					<td id="tdSelect" align="center" runat="server"></td>
				</tr>
				<tr id="trMainTableHR" runat="server">
					<td>
						<hr/>
					</td>
				</tr>
				<tr id="trMainTable" runat="server">
					<td id="tdMainTable" runat="server" align="center"></td>
				</tr>
				<tr id="trHR" runat="server">
					<td>
						<hr/>
					</td>
				</tr>
				<tr id="trRandomTable" runat="server">
					<td id="tdRandomTable" runat="server" align="center"></td>
				</tr>
				<tr>
					<td>
						<hr/>
					</td>
				</tr>
				<tr>
					<td id="tdButton" align="right" runat="server">
						<input id="btnBack" class="button_continue" style="WIDTH: 150px; HEIGHT: 30px" type="button" name="btnBack" runat="server" onclick="goBack()">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<input id="btnPreAssign" runat="server" value="Assign question now" class="button_continue" style="WIDTH: 200px; HEIGHT: 30px" type="button" name="btnPreAssign" onclick="PreAssign()" >
						<%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
						<input id="btnNext" class="button_continue" style="WIDTH: 150px; HEIGHT: 30px" type="button" name="btnNext" runat="server">
					</td>
				</tr>
			</table>
			<div style="POSITION: absolute; TOP: 400px">
			    <input id="hiddenOpener" type="hidden" name="hiddenOpener" runat="server">
				<input id="hiddenQuestionMode" type="hidden" name="hiddenQuestionMode" runat="server">
				<input id="hiddenPresentType" type="hidden" name="hiddenPresentType" runat="server">
				<input id="hiddenEditMode" type="hidden" name="hiddenEditMode" runat="server">
				<input id="hiddenPreOpener" type="hidden" name="hiddenPreOpener" value="" runat="server">
			</div>
         <asp:HiddenField ID="hfPaperID" runat="server" />
		 <asp:HiddenField ID="hfCaseID" runat="server" />	
		 <asp:HiddenField ID="hfDivisionID" runat="server" />
		</form>
	</body>
</HTML>
