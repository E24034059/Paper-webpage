<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page language="c#" Inherits="PaperSystem.HintsPaper_StatisticPresent" CodeFile="HintsPaper_StatisticPresent.aspx.cs" %>

<HTML>
	<HEAD runat="server">
		<title>HintsPaper_StatisticPresent</title>
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
				
				//顯示訊息
				alert("Finish");
			}
			
			function OutputTable()
			{
				try
				{
					//縮放OutputTable
					//alert(document.getElementById("tcOutputTable").style.display);
					if(document.getElementById("trOutputTable").style.display == "none")
					{
						document.getElementById("trOutputTable").style.display = "";
						//alert(document.getElementById("tcOutputTable").style.display);
					}
					else
					{
						document.getElementById("trOutputTable").style.display = "none";
						//alert(document.getElementById("tcOutputTable").style.display);
					}
					window.scrollTo(0,6000);
				}
				catch(e)
				{
				}
			}
		</script>
	</HEAD>
	<body onload="Page_onload();">
		<form id="Form1" method="post" runat="server">
			<table id="body_content" style="WIDTH: 100%;">
				<tr>
					<td id="tdSelectionTable" runat="server"></td>
				</tr>
				<tr>
					<td id="tdTextTable" runat="server"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 50px"></td>
				</tr>
				<tr>
					<td id="tdDurationTable" runat="server"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 50px"></td>
				</tr>
				<tr id="trOutputTable" style="DISPLAY: none">
					<td id="tcOutputTable" runat="server">
					</td>
				</tr>
				<tr id="trOutputButton" align="right" runat="server">
					<td>
						<hr>
						<input type="button" id="btnOutput" name="btnOutput" value="Other presentation"
							onclick="OutputTable();" style="CURSOR: hand;width:250px;" class="button_continue">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
