<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page language="c#" Inherits="PaperSystem.UploadImgFile" CodeFile="UploadImgFile.aspx.cs" %>

<HTML>
	<HEAD runat="server">
		<title>UploadImgFile</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
			function FileError()
			{
				alert("You did not select the correct MMD file, please select C:\\temp.asf then press the finish button");
			}
			
			function FileEmpty()
			{
				alert("You did not select any file, please select C:\\temp.asf then press the finish button");
			}
			
			function resizeWindow()
			{
				var myWidth = 800;
				var myHeight = 150;
				var myLeft = screen.availWidth/2 - myWidth/2;
				var myTop = screen.availHeight/2 - myHeight/2;
				try
				{
					window.resizeTo(myWidth,myHeight);
					window.moveTo(myLeft,myTop);
				}
				catch(e)
				{
				}
			}
			
			function closePage(strIMgTag)
			{
				//呼叫MMD的加入新影像的function
				window.opener.addImgToImgDiv(strIMgTag);
				
				//關閉此上傳的網頁
				window.close();
			}
		</script>
	</HEAD>
	<body onload="resizeWindow();">
		<form id="Form1" method="post" runat="server">
			<table id="body_content" align="left">
			    <tr>
			        <td>
				        <span id="spanUploadImg" class="subtitle">
					        Please select an image in the local disk which you want to use.</span>
					</td>
				</tr>
				<tr>
				    <td>
				        <input id="fileinput" style="WIDTH: 500px; HEIGHT: 30px" type="file" name="fileinput" runat="server">&nbsp;&nbsp;&nbsp;
				        <input id="btnUpload" style="CURSOR: hand;WIDTH: 100px; HEIGHT: 30px" type="button" value="OK"
					        name="btnUpload" runat="server">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
