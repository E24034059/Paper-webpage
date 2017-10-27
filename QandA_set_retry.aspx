<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page language="c#" Inherits="PaperSystem.QandA_set_retry" CodeFile="QandA_set_retry.aspx.cs" %>

<HTML>
	<HEAD runat="server">
		<title>QandA_set_retry</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body id="body_content">
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
					<TD><asp:RadioButton id="Radio1" runat="server" Text="1." Width="24px" AutoPostBack="True" GroupName="RG1" oncheckedchanged="Radio1_CheckedChanged"></asp:RadioButton>不予理會，直接進入下一題。</TD>
				</TR>
				<TR>
					<TD><asp:RadioButton id="Radio2" runat="server" Text="2." Width="24px" AutoPostBack="True" GroupName="RG1" oncheckedchanged="Radio2_CheckedChanged"></asp:RadioButton>告知答案錯誤數次但不給予提示，若在達到
						<asp:TextBox id="TextBox1" AutoPostBack="True" runat="server" Width="24px" ontextchanged="TextBox1_TextChanged"></asp:TextBox>次時仍然答錯就不予理會，進入下一題；如果重作之後答對可以得到
						<asp:TextBox id="TextBox2" runat="server" Width="28px" ontextchanged="TextBox2_TextChanged"></asp:TextBox>％的分數。</TD>
				<TR>
					<TD><asp:RadioButton id="Radio3" runat="server" Text="3." Width="24px" AutoPostBack="True" GroupName="RG1" oncheckedchanged="Radio3_CheckedChanged"></asp:RadioButton>告知答案錯誤且給予提示，若在達到
						<asp:TextBox id="TextBox3" AutoPostBack="True" runat="server" Width="24px" ontextchanged="TextBox3_TextChanged"></asp:TextBox>次時仍然答錯就不予理會，進入下一題；如果重作之後答對可以得到的
						<asp:TextBox id="TextBox4" AutoPostBack="True" runat="server" Width="28px" ontextchanged="TextBox4_TextChanged"></asp:TextBox>%分數。</TD>
				</TR>
				<TR>
					<TD><asp:RadioButton id="Radio4" runat="server" Text="4." Width="24px" AutoPostBack="True" GroupName="RG1" oncheckedchanged="Radio4_CheckedChanged"></asp:RadioButton>告知答案錯誤，但不給予相關提示，直到學習者選擇正確答案，再進入下一題。</TD>
				</TR>
				<TR>
					<TD><asp:RadioButton id="Radio5" runat="server" Text="5." Width="24px" AutoPostBack="True" GroupName="RG1" oncheckedchanged="Radio5_CheckedChanged"></asp:RadioButton>告知答案錯誤，同時給予相關提示，直到學習者選擇正確答案，再進入下一題。</TD>
				</TR>
			</TABLE>
			<br />
			&nbsp;<asp:button id="Button1" runat="server" Text="確定" onclick="Button1_Click" CssClass="button_continue"></asp:button></form>
	</body>
</HTML>
