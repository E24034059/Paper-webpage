<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page language="c#" Inherits="PaperSystem.Paper_MainPageFunctions" CodeFile="Paper_MainPageFunctions.aspx.cs" %>

<HTML>
  <HEAD runat="server">
		<title>Paper_MainPageFunctions</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
			function goBack()
			{
				location.href = "Paper_MainPage.aspx";
			}
			
			function goNext()
			{
				if(document.getElementById("rb1") != null && document.getElementById("rb2") != null)
				{
					if(document.getElementById("rb1").checked == true)
					{
						//location.href = "Paper_InputName.aspx?Opener=Paper_PresentMethod";
						location.href = "Paper_EditMethod.aspx?Opener=Paper_MainPageFunctions";
					}
					else if(document.getElementById("rb2").checked == true)
					{
						location.href = "Paper_Presentation.aspx?Opener=Paper_MainPageFunctions";
					}
				}
			}
		</script>
</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			
				<br>
				<br>
				<br>
				<table id="body_content" align="center" style="WIDTH: 400px" cellPadding="10">
					<tr>
						<td class="title">Please select a 
							function:</td>
					</tr>
					<tr align="left">
						<td class="subtitle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<input id="rb1" type="radio" CHECKED value="rb1" name="rb" runat="server">Add 
							more questions
						</td>
					</tr>
					<tr id="trPreview" align="left" runat="server">
						<td class="subtitle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<input id="rb2" type="radio" value="rb2" name="rb" runat="server">Edit the 
							presentation
						</td>
					</tr>
					<tr>
						<td align="right">
							<input id="btnBack" style="WIDTH: 150px; HEIGHT: 30px" type="button" runat="server" value="<< Back"
								name="btnBack" onclick="goBack()" class="button_continue"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<input id="btnNext" style="WIDTH: 150px; HEIGHT: 30px" type="button" runat="server" value="Next >>"
								name="btnNext" onclick="goNext()" class="button_continue">
						</td>
					</tr>
				</table>
				<input id="hiddenOpener" type="hidden" name="hiddenOpener" runat="server">
		</form>
	</body>
</HTML>
