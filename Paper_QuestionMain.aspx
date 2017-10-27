<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page language="c#" Inherits="PaperSystem.Paper_QuestionMain" CodeFile="Paper_QuestionMain.aspx.cs" %>

<HTML>
	<HEAD runat="server">
		<title>Paper_QuestionMain</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
				<br>
				<br>
				<br>
				<table id="body_content" style="WIDTH: 400px" cellPadding="10" align="center">
					<tr>
						<td class="title">Please select a 
							function:</td>
					</tr>
					<tr align="left">
						<td class="subtitle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<input id="rb1" type="radio" CHECKED value="rb1" name="rb" runat="server">Edit 
							new questions
						</td>
					</tr>
					<tr id="trPreview" align="left" runat="server">
						<td class="subtitle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<input id="rb2" type="radio" value="rb2" name="rb" runat="server">Modify or 
							delete questions
						</td>
					</tr>
					<tr id="tr1" align="left" runat="server">
						<td class="subtitle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<input id="rb3" type="radio" value="rb3" name="rb" runat="server">Return to case menu</td>
					</tr>
					<tr align="right">
						<td>
							<input id="btnNext" style="WIDTH: 150px; HEIGHT: 30px" class="button_continue" type="button" runat="server" value="Next >>"
								name="btnNext">
						</td>
					</tr>
				</table>
				<input id="hiddenOpener" type="hidden" name="hiddenOpener" runat="server">
		</form>
	</body>
</HTML>
