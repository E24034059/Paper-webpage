<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page language="c#" Inherits="PaperSystem.Paper_PresentMethod" CodeFile="Paper_PresentMethod.aspx.cs" %>

<HTML>
	<HEAD runat="server">
		<title>Paper_PresentMethod</title>
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
				
				if(strOpener == "Paper_InputName")
				{
					location.href = "Paper_InputName.aspx";
				}
				else if(strOpener == "Paper_OtherQuestion")
				{
					location.href = "Paper_OtherQuestion.aspx";
				}
				else
				{
					location.href = "Paper_Main.aspx";
				}
			}
			/*
			function goNext()
			{
				if(document.getElementById("rb1") != null && document.getElementById("rb2") != null && document.getElementById("rb3") != null)
				{
					if(document.getElementById("rb1").checked == true)
					{
						location.href = "Paper_InputName.aspx?Opener=Paper_PresentMethod";
					}
					else if(document.getElementById("rb2").checked == true)
					{
						location.href = "Paper_QuestionMode.aspx?Opener=Paper_PresentMethod";
					}
				}
			}
			*/
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
				<table align="center" style="WIDTH: 600px" cellPadding="10">
					<tr>
						<td class="title">Please 
							select a function</td>
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
						<td><input id="btnPre" class="button_continue" style="WIDTH: 150px; HEIGHT: 30px" onclick="goBack();" type="button"
								value="<< Back" name="btnPre" runat="server"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<input id="btnNext" class="button_continue" style="WIDTH: 150px; HEIGHT: 30px" type="submit" value="Next >>" name="btnNext"
								runat="server">
						</td>
					</tr>
				</table>
				<input id="hiddenOpener" type="hidden" name="hiddenOpener" runat="server">
		</form>
	</body>
</HTML>
