<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page language="c#" Inherits="PaperSystem.Paper_QuestionMode" CodeFile="Paper_QuestionMode.aspx.cs" %>

<HTML>
	<HEAD runat="server">
		<title>Paper_QuestionMode</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
			
			function goBack()
			{
				var strOpener = "";
				if(document.getElementById("hiddenOpener") != null)
				{
					strOpener = document.getElementById("hiddenOpener").value;
				}
				
				if(strOpener == "Paper_PresentMethod")
				{
					location.href = "Paper_PresentMethod.aspx";
				}
				else
				{
					location.href = "Paper_EditMethod.aspx";
				}
			}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
				<table id="body_content" align="center" style="WIDTH: 400px" cellPadding="10">
					<tr>
						<td class="title">Question mode:</td>
					</tr>
					<tr align="left">
						<td class="subtitle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<input id="rb1" type="radio" CHECKED value="rb1" name="rb" runat="server">General
						</td>
					</tr>
					<tr align="left">
						<td class="subtitle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<input id="rb2" type="radio" value="rb2" name="rb" runat="server">Specific
						</td>
					</tr>
					<tr align="right">
						<td><input id="btnPre" class="button_continue" style="WIDTH: 150px; HEIGHT: 30px" onclick="goBack();" type="button"
								value="<< Back" name="btnPre"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<input id="btnNext" class="button_continue" style="WIDTH: 150px; HEIGHT: 30px" type="submit" value="Next >>" name="btnNext"
								runat="server">
						</td>
					</tr>
				</table>
				<input id="hiddenOpener" type="hidden" name="hiddenOpener" runat="server">
		</form>
	</body>
</HTML>
