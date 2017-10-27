<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page language="c#" Inherits="PaperSystem.Paper_EditMethod" CodeFile="Paper_EditMethod.aspx.cs" %>

<HTML>
	<HEAD runat="server">
		<title>Paper_EditMethod</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
			
			function goBack()
			{
				location.href = "Paper_MainPage.aspx";
			}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
				<table align="center" id="body_content" style="WIDTH: 600px" cellPadding="10">
					<tr>
						<td class="title">Please select a 
							function</td>
					</tr>
					<tr align="left">
						<td class="subtitle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<input id="rb1" type="radio" CHECKED value="rb1" name="rb" runat="server">Questions 
							generated at authoring time
						</td>
					</tr>
					<tr align="left">
						<td class="subtitle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<input id="rb2" type="radio" value="rb2" name="rb" runat="server">Questions 
							generated at run time
						</td>
					</tr>
					<tr align="right">
						<td><input id="btnPre" style="WIDTH: 150px; HEIGHT: 30px" onclick="goBack();" type="button"
								value="<< Back" name="btnPre" class="button_continue"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<input id="btnNext" style="WIDTH: 150px; HEIGHT: 30px" type="submit" value="Next >>" name="btnNext" class="button_continue"
								runat="server">
						</td>
					</tr>
				</table>
				<input id="hiddenOpener" type="hidden" name="hiddenOpener" runat="server"> <input id="hiddenQuestionMode" type="hidden" name="hiddenQuestionMode" runat="server">
		</form>
	</body>
</HTML>
