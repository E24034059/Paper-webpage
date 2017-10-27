<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page language="c#" Inherits="PaperSystem.UploadMediaFile" CodeFile="UploadMediaFile.aspx.cs" %>

<HTML>
	<HEAD runat="server">
		<title>UploadMediaFile</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
			
			function closePage(strIMgTag , strContentType)
			{
			    alert(strIMgTag);
				//呼叫MMD的加入新影像的function
				window.opener.addFile(strIMgTag , strContentType);
				
				//關閉此上傳的網頁
				window.close();
			}
		</script>
	</HEAD>
	<body onload="resizeWindow()">
		<form id="Form1" method="post" encType="multipart/form-data" runat="server">
		    <table id="body_content" align="left">
		        <tr>
		            <td>
			            <span id="spanUploadImg" class="subtitle">Please select a multimedia file in the local disk which you want to use.</span>
			        </td>
				</tr>
				<tr>
				    <td>
			            <input id="fileinput" style="WIDTH: 500px; CURSOR: hand; HEIGHT: 30px" type="file" name="fileinput"
					            runat="server">&nbsp;&nbsp;&nbsp; 
			            <input id="btnUpload" style="WIDTH: 100px; CURSOR: hand; HEIGHT: 30px" type="button" value="OK"
					            name="btnUpload" runat="server" onserverclick="btnUpload_ServerClick" class="button_continue">&nbsp;&nbsp;&nbsp;
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
