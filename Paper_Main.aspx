<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page language="c#" Inherits="PaperSystem.Paper_Main" CodeFile="Paper_Main.aspx.cs" %>

<HTML>
	<HEAD runat="server">
		<title>Paper_Main</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
			
			function goBack()
			{
				
			}
			
			function goNext()
			{
				if(document.getElementById("rb1") != null && document.getElementById("rb3") != null)
				{
					if(document.getElementById("rb1").checked == true)
					{
						location.href = "Paper_InputName.aspx?Opener=Paper_Main";
					}
					else
					{
						location.href = "Paper_MainPage.aspx?Opener=Paper_Main";
					}
				}
			}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			
			<table align="center" id="body_content" style="WIDTH: 550px" cellPadding="10">
				<tr>
					<td class="title">Please select a function:</td>
				</tr>
				<tr id="trModify" align="left" runat="server">
					<td class="subtitle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<input id="rb1" type="radio" CHECKED value="rb1" name="rb" runat="server">Create 
						a new examination paper
					</td>
				</tr>
				<tr id="trPreview" align="left" runat="server">
					<td class="subtitle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<input id="rb3" type="radio" value="rb3" name="rb" runat="server">Modify and 
						preview the examination paper
					</td>
				</tr>
				<tr align="right">
					<td>
						<input id="btnNext" style="WIDTH: 150px; HEIGHT: 30px" class="button_continue" onclick="goNext();" type="button"
							value="Next >>" name="btnNext">
					</td>
				</tr>
			</table>
			<input id="hiddenOpener" type="hidden" name="hiddenOpener" runat="server">

		</form>
	</body>
</HTML>
