<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Reference Page="~/AuthoringTool/CaseEditor/Paper/qandA_set_review.aspx" %>
<%@ Reference Page="~/AuthoringTool/CaseEditor/Paper/qandA_set_retry.aspx"%>
<%@ Page language="c#" Inherits="PaperSystem.QandA_setRetry" buffer="False" CodeFile="QandA_select_setRetry.aspx.cs" %>

<HTML>
	<HEAD runat="server">
		<title>QandA_setRetry</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body id="body_content">
		<form id="Form1" method="post" runat="server">
			Case ID:<%=CaseID%>
			科別：<%=cDivisionID%>
			診次：<%=CurrentTerm%>
			題號：<%=cQID%>
			SectionName:<%=SectionName%>
			<TABLE language="C#" id="table" cellSpacing="1" cellPadding="1" width="599" align="center"
				border="1">
				<TR>
					<TD style="WIDTH: 190px"><FONT face="新細明體">User Level</FONT></TD>
					<TD>重作機制設定</TD>
					<TD>倒帶功能設定</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 190px">All Level</TD>
					<TD><%=all_retry%></TD>
					<TD><%=all_review%></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 190px">Very Easy</TD>
					<TD><%=vez_retry%></TD>
					<TD><%=vez_review%></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 190px">Easy</TD>
					<TD><%=ez_retry%></TD>
					<TD><%=ez_review%></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 190px">Medium</TD>
					<TD><%=med_retry%></TD>
					<TD><%=med_review%></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 190px">Medium-Advanced</TD>
					<TD><%=med_ad_retry%></TD>
					<TD><%=med_ad_review%></TD>
				<TR>
					<TD style="WIDTH: 190px">Advanced</TD>
					<TD><%=adv_retry%></TD>
					<TD><%=adv_review%></TD>
				</TR>
				</TABLE>
			<asp:Button id="Button1" style="Z-INDEX: 101; LEFT: 200px; POSITION: absolute; TOP: 224px" runat="server"
				Text="Finish" onclick="Button1_Click" CssClass="button_continue"></asp:Button>
		</form>
	</body>
</HTML>
