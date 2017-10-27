<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page language="c#" Inherits="PaperSystem.Paper_MainFrame" CodeFile="Paper_MainFrame.aspx.cs" %>

<HTML>
	<HEAD runat="server">
		<TITLE>Paper_MainFrame</TITLE>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD><!--modified by dolphin @ 2006-07-29, new Question Group Tree-->
	<frameset id="framesetRow1" rows="0,*">
		<frame src="SequenceManager.aspx?UserID=<%=strUserID%>" scrolling="no" id="frmSeq" name="frmSeq">
		<frameset id="framesetCol1" cols="0,100%">
			<frame scrolling="auto" id="frmTree" name="frmTree" src="./QuestionGroupTree/QGroupTree.aspx">
			<frame scrolling="auto" id="frmMain" name="frmMain" src="Paper_HeaderSetting.aspx?UserID=<%=strUserID%>">
		</frameset>
	</frameset>
</HTML>
