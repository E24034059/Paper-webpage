<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page language="c#" Inherits="PaperSystem.Paper_InputName" CodeFile="Paper_InputName.aspx.cs" %>

<HTML>
	<HEAD runat="server">
		<title>Paper_InputName</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
			
			function goBack()
			{
				location.href="Paper_Main.aspx";
			}
			
			function goNext()
			{
				
			}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
				<table align="center" id="body_content" style="WIDTH: 400px" cellpadding="10" runat="server">
					<tr>
						<td class="title">Subject:</td>
					</tr>
					<tr align="center">
						<td runat="server" id="tdName" style="FONT-WEIGHT: bold; FONT-SIZE: 22px; COLOR: blue">
							<input id="txtName" name="txtName" type="text" style="WIDTH: 500px" runat="server">
						</td>
					</tr>
					<tr>
						<td class="title">Title:</td>
					</tr>
					<tr align="center">
						<td runat="server" id="Td1">
							<textarea id="txtTitle" name="txtTitle" style="WIDTH: 500px; HEIGHT: 300px" runat="server"></textarea>
						</td>
					</tr>
					<tr align="right">
						<td>
							<input style="WIDTH: 150px; HEIGHT: 30px" type="button" id="btnPre" name="btnPre" value="<< Back" class="button_continue"
								onclick="goBack();" runat="server"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input style="WIDTH: 150px; HEIGHT: 30px" class="button_continue" runat="server" type="submit" id="btnNext" name="btnNext"
								value="Next >>">
						</td>
					</tr>
				</table>
				<input id="hiddenPaperID" type="hidden" name="hiddenPaperID" runat="server">
		</form>
	</body>
</HTML>
