<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page language="c#" Inherits="PaperSystem.QandA_set_review" CodeFile="QandA_set_review.aspx.cs" %>

<HTML>
	<HEAD runat="server">
		<title>QandA_set_retry</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			Case ID:<%=cCaseID%>
			診次：<%=CurrentTerm%>
			題號：<%=cQID%>
			SectionName:<%=SectionName%>
			<TABLE id="Table1" style="WIDTH: 1016px; HEIGHT: 248px" cellSpacing="1" cellPadding="1"
				width="1016" border="1">
				<TR>
					<TD><FONT face="新細明體">試題重作功能設定</FONT></TD>
				</TR>
				<TR>
					<TD><asp:radiobutton id="Radio1" runat="server" Text="1." Width="24px" AutoPostBack="True" GroupName="RG1" oncheckedchanged="Radio1_CheckedChanged"></asp:radiobutton>只顯示答題歷程，不標示對錯與正確答案也不給予修改與任何的說明。</TD>
				</TR>
				<TR>
					<TD><asp:radiobutton id="Radio2" runat="server" Text="2." Width="24px" AutoPostBack="True" GroupName="RG1" oncheckedchanged="Radio2_CheckedChanged"></asp:radiobutton>顯示答題歷程，並標示出錯誤部分，但不提供正確答案也不給予修改與任何的說明。</TD>
				<TR>
					<TD><asp:radiobutton id="Radio3" runat="server" Text="3." Width="24px" AutoPostBack="True" GroupName="RG1" oncheckedchanged="Radio3_CheckedChanged"></asp:radiobutton>顯示答題歷程，並標示出錯誤部分且提供正確答案，但不給予修改與任何的說明。</TD>
				</TR>
				<TR>
					<TD><asp:radiobutton id="Radio4" runat="server" Text="4." Width="24px" AutoPostBack="True" GroupName="RG1" oncheckedchanged="Radio4_CheckedChanged"></asp:radiobutton>顯示答題歷程，並標示出錯誤部分且提供正確答案與說明，但不給予修改。</TD>
				</TR>
				<TR>
					<TD><asp:radiobutton id="Radio5" runat="server" Text="5." Width="24px" AutoPostBack="True" GroupName="RG1" oncheckedchanged="Radio5_CheckedChanged"></asp:radiobutton>顯示答題歷程，標示出錯誤部分給予修改機會但不給予提示，如果修改之後答對可以得到
						<asp:textbox id="TextBox1" runat="server" Width="32px" AutoPostBack="True" ontextchanged="TextBox1_TextChanged"></asp:textbox>％的分數</TD>
				</TR>
				<TR>
					<TD><asp:radiobutton id="Radio6" runat="server" Text="6." Width="24px" AutoPostBack="True" GroupName="RG1" oncheckedchanged="Radio6_CheckedChanged"></asp:radiobutton>顯示答題歷程，標示出錯誤部分給予修改機會並給予提示，如果修改之後答對可以得到
						<asp:textbox id="TextBox2" runat="server" Width="32px" AutoPostBack="True" ontextchanged="TextBox2_TextChanged"></asp:textbox>％的分數。</TD>
				</TR>
			</TABLE>
			
			<asp:button id="Button1" runat="server" Text="確定" onclick="Button1_Click"></asp:button></form>
	</body>
</HTML>
