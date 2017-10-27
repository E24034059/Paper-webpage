<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page language="c#" Inherits="PaperSystem.HintsPaper_StatisticSelection" CodeFile="HintsPaper_StatisticSelection.aspx.cs" %>

<HTML>
	<HEAD runat="server">
		<title>HintsPaper_StatisticSelection</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK id="FontCSS" href="../ILView/font.css" rel="stylesheet" name="FontCSS">
		<script language="javascript">
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
				
				//Function
				var strFunction = "";
				if(document.getElementById("hiddenFunction") != null)
				{
					strFunction = document.getElementById("hiddenFunction").value;
				}
				
				//Author
				var strAuthor = "";
				if(document.getElementById("hiddenAuthor") != null)
				{
					strAuthor = document.getElementById("hiddenAuthor").value;
				}
				
				//CaseID
				var strCaseID = "";
				if(document.getElementById("hiddenCase") != null)
				{
					strCaseID = document.getElementById("hiddenCase").value;
				}
				
				//Class
				var strClass = "";
				if(document.getElementById("hiddenClass") != null)
				{
					strClass = document.getElementById("hiddenClass").value;
				}
				
				//Group
				var strGroup = "";
				if(document.getElementById("hiddenGroup") != null)
				{
					strGroup = document.getElementById("hiddenGroup").value;
				}
				
				if(parent.frmStatistic != null)
				{
					if(strAuthor != "None" && strCaseID != "None" && strClass != "None" && strGroup == "None")
					{
						//Author , Case , Class
						strFunction = "0";
						parent.frmStatistic.document.location = "HintsPaper_StatisticPresent.aspx?Function="+strFunction+"&Class="+strClass+"&Group="+strGroup+"&CaseID="+strCaseID+"&AuthorID="+strAuthor;
					}
					else if(strAuthor != "None" && strCaseID != "None" && strClass != "None" && strGroup != "None")
					{
						//Author , Case , Class , Group
						strFunction = "1";
						parent.frmStatistic.document.location = "HintsPaper_StatisticPresent.aspx?Function="+strFunction+"&Class="+strClass+"&Group="+strGroup+"&CaseID="+strCaseID+"&AuthorID="+strAuthor;
					}
					else if(strAuthor != "None" && strCaseID != "None" && strClass == "None" && strGroup == "None")
					{
						//Author , Case
						strFunction = "2";
						parent.frmStatistic.document.location = "HintsPaper_StatisticPresent.aspx?Function="+strFunction+"&Class="+strClass+"&Group="+strGroup+"&CaseID="+strCaseID+"&AuthorID="+strAuthor;
					}
					else if(strAuthor != "None" && strCaseID == "None" && strClass != "None" && strGroup == "None")
					{
						//Author , Class 
						strFunction = "3";
						parent.frmStatistic.document.location = "HintsPaper_StatisticPresent.aspx?Function="+strFunction+"&Class="+strClass+"&Group="+strGroup+"&CaseID="+strCaseID+"&AuthorID="+strAuthor;
					}
					else if(strAuthor != "None" && strCaseID == "None" && strClass != "None" && strGroup != "None")
					{
						//Author , Class , Group
						strFunction = "4";
						parent.frmStatistic.document.location = "HintsPaper_StatisticPresent.aspx?Function="+strFunction+"&Class="+strClass+"&Group="+strGroup+"&CaseID="+strCaseID+"&AuthorID="+strAuthor;
					}
					else if(strAuthor != "None" && strCaseID == "None" && strClass == "None" && strGroup == "None")
					{
						//Author
						strFunction = "5";
						parent.frmStatistic.document.location = "HintsPaper_StatisticPresent.aspx?Function="+strFunction+"&Class="+strClass+"&Group="+strGroup+"&CaseID="+strCaseID+"&AuthorID="+strAuthor;
					}
					else if(strAuthor == "None" && strCaseID != "None" && strClass != "None" && strGroup == "None")
					{
						//Case , Class
						strFunction = "6";
						parent.frmStatistic.document.location = "HintsPaper_StatisticPresent.aspx?Function="+strFunction+"&Class="+strClass+"&Group="+strGroup+"&CaseID="+strCaseID+"&AuthorID="+strAuthor;
					}
					else if(strAuthor == "None" && strCaseID != "None" && strClass != "None" && strGroup != "None")
					{
						//Case , Class , Group
						strFunction = "7";
						parent.frmStatistic.document.location = "HintsPaper_StatisticPresent.aspx?Function="+strFunction+"&Class="+strClass+"&Group="+strGroup+"&CaseID="+strCaseID+"&AuthorID="+strAuthor;
					}
					else if(strAuthor == "None" && strCaseID != "None" && strClass == "None" && strGroup == "None")
					{
						//Case
						strFunction = "8";
						parent.frmStatistic.document.location = "HintsPaper_StatisticPresent.aspx?Function="+strFunction+"&Class="+strClass+"&Group="+strGroup+"&CaseID="+strCaseID+"&AuthorID="+strAuthor;
					}
					else if(strAuthor == "None" && strCaseID == "None" && strClass != "None" && strGroup == "None")
					{
						//Class
						strFunction = "9";
						parent.frmStatistic.document.location = "HintsPaper_StatisticPresent.aspx?Function="+strFunction+"&Class="+strClass+"&Group="+strGroup+"&CaseID="+strCaseID+"&AuthorID="+strAuthor;
					}
					else if(strAuthor == "None" && strCaseID == "None" && strClass != "None" && strGroup != "None")
					{
						//Group
						strFunction = "10";
						parent.frmStatistic.document.location = "HintsPaper_StatisticPresent.aspx?Function="+strFunction+"&Class="+strClass+"&Group="+strGroup+"&CaseID="+strCaseID+"&AuthorID="+strAuthor;
					}
					else
					{
						//出現警告訊息
						//alert(strAuthor + "," + strCaseID + "," + strClass + "," + strGroup);
						alert("Parameter error");
						
						//把遊標設為正常
						if(parent.frmSelection != null)
						{
							parent.frmSelection.document.body.style.cursor = "";
						}
						if(parent.frmStatistic != null)
						{
							parent.frmStatistic.document.body.style.cursor = "";
						}
					}
				}
				
			}
			
			function AnotherFunction()
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
					parent.framesetMain.rows = "0px,*";
				}
				
				//呼叫StatisticMainFunction.aspx
				if(parent.frmStatistic != null)
				{
					parent.frmStatistic.document.location.href = "HintsPaper_StatisticMainFunctions.aspx";
				}
			}
			
		</script>
	</HEAD>
	<body background="../picks/bg-g.png">
		<form class="txt" id="Form1" method="post" runat="server">
			<div style="LEFT: 0px; WIDTH: 100%; POSITION: absolute; TOP: 0px" align="center">
				<table class="txt" id="tableFrame">
					<tr id="trSelection">
						<td id="tcAuthorSelection" runat="server"><span id="spanAuthorTitle" style="FONT-WEIGHT: bold; FONT-SIZE: 16px; COLOR: seagreen">Author</span>&nbsp;
							<asp:dropdownlist id="dlAuthor" Runat="server" AutoPostBack="True" CssClass="txt2"></asp:dropdownlist></td>
						<td id="tcCaseSelection" runat="server"><span id="spanCaseTitle" style="FONT-WEIGHT: bold; FONT-SIZE: 16px; COLOR: seagreen">Case</span>&nbsp;
							<asp:dropdownlist id="dlCase" Runat="server" AutoPostBack="True" CssClass="txt"></asp:dropdownlist></td>
						<td id="tcClassSelection" runat="server"><span id="spanClassTitle" style="FONT-WEIGHT: bold; FONT-SIZE: 16px; COLOR: seagreen">Class</span>&nbsp;
							<asp:dropdownlist id="dlClass" Runat="server" AutoPostBack="True" CssClass="txt"></asp:dropdownlist></td>
						<td id="tcGroupSelection" runat="server"><span id="spanGroupTitle" style="FONT-WEIGHT: bold; FONT-SIZE: 16px; COLOR: seagreen">Group</span>&nbsp;
							<asp:dropdownlist id="dlGroup" Runat="server" AutoPostBack="True" CssClass="txt"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td id="tcSubmit" align="right" colSpan="5"><span id="spanSubmitMessage" style="FONT-WEIGHT: bold; FONT-SIZE: 16px; COLOR: seagreen">Compute 
								statistics:</span> &nbsp;&nbsp; <input id="btnSubmit" style="CURSOR: hand;width:150px;" type="button" name="btnSubmit" runat="server">
						</td>
					</tr>
				</table>
			</div>
			<div id="divHidden" style="POSITION: absolute; TOP: 200px"><input id="hiddenMode" type="hidden" value="Case" name="hiddenMode" runat="server">
				<input id="hiddenCase" type="hidden" name="hiddenCase" runat="server"> <input id="hiddenAuthor" type="hidden" name="hiddenAuthor" runat="server">
				<input id="hiddenClass" type="hidden" name="hiddenClass" runat="server"> <input id="hiddenGroup" type="hidden" name="hiddenGroup" runat="server">
				<input id="hiddenFunction" type="hidden" name="hiddenFunction" runat="server">
			</div>
		</form>
	</body>
</HTML>
