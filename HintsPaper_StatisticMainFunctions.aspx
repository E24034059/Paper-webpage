<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page language="c#" Inherits="PaperSystem.Paper_StatisticMainFunctions" CodeFile="HintsPaper_StatisticMainFunctions.aspx.cs" %>

<HTML>
	<HEAD runat="server">
		<title>Paper_StatisticMainFunctions</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Table.css" rel="stylesheet">
		<script language="javascript">
			function Page_onload()
			{
				//把滑鼠遊標設回來
				if(parent.frmSelection != null)
				{
					parent.frmSelection.document.body.style.cursor = "";
				}
				if(parent.frmStatistic != null)
				{
					parent.frmStatistic.document.body.style.cursor = "";
				}
			}
			
			function Submit()
			{
				//把滑鼠遊標設為沙漏
				if(parent.frmSelection != null)
				{
					parent.frmSelection.document.body.style.cursor = "wait";
				}
				if(parent.frmStatistic != null)
				{
					parent.frmStatistic.document.body.style.cursor = "wait";
				}
				
				//改變Frame
				if(parent.framesetMain != null)
				{
					parent.framesetMain.rows = "70px,*";
				}
				
				//取得使用者所選擇的Function
				var strFunction = "";
				if(document.getElementById("hiddenFunction") != null)
				{
					strFunction = document.getElementById("hiddenFunction").value;
				}
				
				//呼叫frmSelection
				if(parent.frmSelection != null)
				{
					parent.frmSelection.document.location.href = "HintsPaper_StatisticSelection.aspx?Function=" + strFunction;
				}
			}
		</script>
	</HEAD>
	<body onload="Page_onload();">
		<form id="Form1" method="post" runat="server">
				<table id="body_content" align="center">
					<tr>
						<td class="title">Please select one function:</td>
					</tr>
					<tr id="trFunctionTable">
						<td id="tdFunctionTable" runat="server"></td>
					</tr>
					<tr id="trSubmit">
						<td id="tdSubmit" align="right" runat="server">
						</td>
					</tr>
				</table>
			<input type="hidden" id="hiddenFunction" name="hiddenFunction" runat="server">
		</form>
	</body>
</HTML>
