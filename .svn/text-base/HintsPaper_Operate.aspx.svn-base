<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page language="c#" Inherits="PaperSystem.HintsPaper_Operate" CodeFile="HintsPaper_Operate.aspx.cs" %>

<HTML>
	<HEAD runat="server">
		<title>HintsPaper_Operate</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Table.css" rel="stylesheet">
		<script language="javascript">
		
		//Resize window
		function ResizeWindow()
		{
			var myWidth = screen.availWidth * 0.9;
			var myHeight = screen.availHeight;
			var myLeft = screen.availWidth/2 - myWidth/2;
			var myTop = screen.availHeight/2 - myHeight/2;
			if(parent != null)
			{
				window.resizeTo(myWidth,myHeight);
				window.moveTo(myLeft,myTop);
			}
		}
		
		function closeWindow()
		{
			window.close();
		}
		
		function getWindowSize()
		{
			var myWidth = screen.availWidth;
			
			var myHeight = screen.availHeight;
			
			var TextIDList = document.getElementById("hiddenTextID").value;
			
			var TextCount = document.getElementById("hiddenTextCount").value
			
			var intTextCount = parseInt(TextCount);
			
			var arrayText = TextIDList.split(';');
			/*
			//alert(intTextCount);
			for(var i=0 ; i<1 ; i++)
			{
				if(document.getElementById(arrayText[i]) != null)
				{
					if(myWidth < 1100)
					{
						document.getElementById(arrayText[i]).cols = "100";
					}
					else
					{
						document.getElementById(arrayText[i]).cols = "150";
					}
				}
			}
			*/
		}
		
		function AlertSelectionNull(intQuestionNum)
		{
			//使用者沒有全部操作完所有問題的話出現此視窗警告。
			
			alert("You did not finish question " + intQuestionNum + " on the window");
		}
		
		</script>
	</HEAD>
	<body onload="ResizeWindow();">
		<form id="Form1" method="post" runat="server">
			<div id="divTable" style="LEFT: 0px; WIDTH: 100%; POSITION: absolute; TOP: 0px" align="center">
				<table style="WIDTH: 90%">
					<tr>
						<td align="center"><font style="FONT-WEIGHT: bold; FONT-SIZE: 30px; COLOR: seagreen"><%=strPaperName%></font></td>
					</tr>
					<tr>
						<td align="center"><asp:placeholder id="phQuestion" Runat="server"></asp:placeholder></td>
					</tr>
					<tr>
						<td align="right"><font style="FONT-WEIGHT: bold; FONT-SIZE: 24px; COLOR: seagreen">Click 
								this button after you have finished</font>&nbsp;&nbsp;
							<asp:placeholder id="phSubmit" Runat="server"></asp:placeholder></td>
					</tr>
				</table>
				<input id="hiddenOperationTime" type="hidden" name="hiddenOperationTime" runat="server">
				<input id="hiddenTextID" type="hidden" name="hiddenTextID" runat="server"> <input id="hiddenTextCount" type="hidden" name="hiddenTextCount" runat="server" value="0">
			</div>
		</form>
	</body>
</HTML>
