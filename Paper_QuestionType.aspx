<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page language="c#" Inherits="PaperSystem.Paper_QuestionType" CodeFile="Paper_QuestionType.aspx.cs" %>

<HTML>
	<HEAD runat="server">
		<title>Paper_QuestionType</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
			
			function goBack()
			{
			    //ben check
			    alert(document.getElementById("hiddenOpener"));


				var strOpener = "";
				if(document.getElementById("hiddenOpener") != null)
				{
					strOpener = document.getElementById("hiddenOpener").value;
				}
				location.href = strOpener+".aspx";
			}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
				<table id="body_content" align="center" style="WIDTH: 400px" cellPadding="10">
					<tr>
						<td class="title">Question type:</td>
					</tr>
					<tr align="left">
						<td class="subtitle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<input id="rb1" type="radio" CHECKED value="rb1" name="rb" runat="server">選擇題
						</td>
					</tr>
					<tr align="left">
						<td class="subtitle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<input id="rb2" type="radio" value="rb2" name="rb" runat="server">問答題
						</td>
					</tr>
								<tr align="left">
						<td class="subtitle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<input id="rb3" type="radio" value="rb3" name="rb" runat="server">圖形題
						</td>
					</tr>
					<tr align="right">
						<td><input id="btnPre" class="button_continue" style="WIDTH: 150px; HEIGHT: 30px" onclick="goBack()" type="button"
								value="<< Back" name="btnPre"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<input id="btnNext" class="button_continue" style="WIDTH: 150px; HEIGHT: 30px" type="submit" value="Next >>" name="btnNext"
								runat="server">
						</td>
					</tr>
				</table>
				<input id="hiddenOpener" type="hidden" name="hiddenOpener" runat="server">
			<div style="POSITION: absolute; TOP: 400px">
				<input id="Hidden1" type="hidden" name="hiddenOpener" runat="server"> <input id="hiddenQuestionMode" type="hidden" name="hiddenQuestionMode" runat="server">
				<input id="hiddenPresentType" type="hidden" name="hiddenPresentType" runat="server">
				<input id="hiddenEditMode" type="hidden" name="hiddenEditMode" runat="server">
			</div>
		</form>
	</body>
</HTML>
