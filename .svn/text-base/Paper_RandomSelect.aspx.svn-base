<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page language="c#" Inherits="PaperSystem.Paper_RandomSelect" CodeFile="Paper_RandomSelect.aspx.cs" %>

<HTML>
	<HEAD runat="server">
		<title>Paper_RandomSelect</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
		function callMainPage()
		{
			if(parent.parent.frmMain != null)
			{
				parent.parent.frmMain.src = "";
				parent.parent.frmMain.document.location.href = "Paper_MainPage.aspx";
			}
			else if(parent.frmMain != null)
			{
				parent.frmMain.src = "";
				parent.frmMain.document.location.href = "Paper_MainPage.aspx";
			}
			else
			{
				frmMain.src = "";
				frmMain.document.location.href = "Paper_MainPage.aspx";
			}
			//parent.parent.frmMain.document.location.href = "Paper_MainPage.aspx";
			//window.open("Paper_MainPage.aspx","frmMain","","");
		}
		
		function alert(strMsg)
		{
			alert(strMsg);
		}
		
		function goBack()
		{
			/*
			var Back = "";
			if(document.getElementById("hiddenOpener") != null)
			{
				Back = document.getElementById("hiddenOpener").value
				if(Back == "ShowQuestion")
				{
					Back = "./CommonQuestionEdit/Page/" + Back;
				}
				Back = Back + ".aspx";
			}
			location.href=Back;
			*/
			var strOpener = "";
			if(document.getElementById("hiddenOpener") != null)
			{
				strOpener = document.getElementById("hiddenOpener").value;
			}
			
			var strPresentType = "";
			if(document.getElementById("hiddenPresentType") != null)
			{
				strPresentType = document.getElementById("hiddenPresentType").value;
			}
			
			var strEditMode = "";
			if(document.getElementById("hiddenEditMode") != null)
			{
				strEditMode = document.getElementById("hiddenEditMode").value;
			}
			
			
			if(strOpener == "Paper_NewOrNot")
			{
				location.href = "Paper_NewOrNot.aspx";
			}
			else
			{
				location.href = "./CommonQuestionEdit/Page/ShowQuestion.aspx";
			}
			
		}
		
		function goNext()
		{
			//location.href = "Paper_OtherQuestion.aspx";
			if(document.getElementById("btnSubmit") != null)
			{
				document.getElementById("btnSubmit").click();
			}
		}
		
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="body_content" style="WIDTH: 100%;" align="center">
				<tr id="trTitle">
					<td id="tcTitle" class="title" colspan="2">
						<span>Please input number 
							of random questions:</span>&nbsp;&nbsp;<%--<input id="txtNumber" name="txtNumber" style="WIDTH: 100px" runat="server">--%>
                        <asp:PlaceHolder ID="phQuestionLevel" runat="server"></asp:PlaceHolder>
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<span class="subtitle">Maximum available: <span style="FONT-WEIGHT: bold; FONT-SIZE: 22px; COLOR: red" runat="server" id="spanMax">
							</span></span>
					</td>
				</tr>
				<tr style="DISPLAY: none">
					<td colspan="2">
						<span class="subtitle">Click this button 
							after you have finished:</span>&nbsp;&nbsp; 
						<input type="image" id="btnSubmit" name="btnSubmit" runat="server" src="../ILView/r-botton/finish.gif"
							style="CURSOR: hand">
					</td>
				</tr>
				<tr>
                    <td style="TEXT-ALIGN: left">
                        <asp:Label ID="lbQuestionLevel" runat="server" Text="" class="subtitle"></asp:Label>
                    </td>
					<td style="TEXT-ALIGN: right">
						<input class="button_continue" style="WIDTH: 150px; HEIGHT: 30px" type="button" id="btnPre" name="btnPre" value="<< Back"
							onclick="goBack();"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
						<input class="button_continue" style="WIDTH: 150px; HEIGHT: 30px" type="button" id="btnNext" name="btnNext" value="Next >>"
							onclick="goNext();">
					</td>
				</tr>
			</table>
			<input id="hiddenQuestionMode" type="hidden" name="hiddenQuestionMode" runat="server">
			<input type="hidden" id="hiddenOpener" name="hiddenOpener" runat="server"> <input type="hidden" id="hiddenPresentType" name="hiddenPresentType" runat="server">
			<input type="hidden" id="hiddenEditMode" name="hiddenEditMode" runat="server">
		</form>
	</body>
</HTML>
