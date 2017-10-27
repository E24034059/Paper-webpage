<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page language="c#" Inherits="PaperSystem.Paper_OtherQuestion" CodeFile="Paper_OtherQuestion.aspx.cs" %>

<HTML>
	<HEAD runat="server">
		<title>Paper_OtherQuestion</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
			
			function goBack()
			{
				/*
				var Back = "";
				if(document.getElementById("hiddenOpener") != null)
				{
					Back = document.getElementById("hiddenOpener").value + ".aspx";
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
				
				var strQuestionMode = "";
				if(document.getElementById("hiddenQuestionMode") != null)
				{
					strQuestionMode = document.getElementById("hiddenQuestionMode").value;
				}
				
				var strEditMode = "";
				if(document.getElementById("hiddenEditMode") != null)
				{
					strEditMode = document.getElementById("hiddenEditMode").value;
				}
				
				var strGroupID = "";
				if(document.getElementById("hiddenGroupID") != null)
				{
					strGroupID = document.getElementById("hiddenGroupID").value;
				}
				
				if(strPresentType == "Present")
				{
					location.href = "Paper_RandomSelect.aspx?GroupID="+strGroupID;
				}
				else
				{
					if(strEditMode == "Automatic")
					{
						location.href = "Paper_RandomSelect.aspx?GroupID="+strGroupID;
					}
					else
					{
						if(strQuestionMode == "Specific")
						{
							location.href = "./CommonQuestionEdit/Page/ShowQuestion.aspx";
						}
						else
						{
							if(strEditMode == "Automatic")
							{
								location.href = "Paper_RandomSelect.aspx";
							}
							else
							{
								location.href = "Paper_SelectQuestion.aspx";
							}
						}
					}
				}
			}
			
			function goNext()
			{
				var strPresentType = "";
				if(document.getElementById("hiddenPresentType") != null)
				{
					strPresentType = document.getElementById("hiddenPresentType").value;
				}
				
				var strQuestionMode = "";
				if(document.getElementById("hiddenQuestionMode") != null)
				{
					strQuestionMode = document.getElementById("hiddenQuestionMode").value;
				}
				
				var strNext = "";
				if(document.getElementById("hiddenQuestionMode") != null)
				{
					strQuestionMode = document.getElementById("hiddenQuestionMode").value;
				}
				
				if(strPresentType == "Edit")
				{
				    
					if(document.getElementById("rb1") != null && document.getElementById("rb2") != null && document.getElementById("rb3") != null && document.getElementById("rb4") != null)
					{
						if(document.getElementById("rb1").checked == true)
						{
							//從General題庫中手動選題
							//location.href = "./QuestionGroupTree/QuestionGroupTree.aspx?Opener=Paper_OtherQuestion";
							location.href = "Paper_EditMethod.aspx?Opener=Paper_OtherQuestion";
							
						}
						else if(document.getElementById("rb2").checked == true)
						{
							//從General題庫中亂數選題
							// modified by dolphin @ 2006-07-29, new Question Group Tree
							//location.href = "./QuestionGroupTree/QuestionGroupTree.aspx?Opener=Paper_OtherQuestion";
							location.href = "./QuestionGroupTree/QGroupTree.aspx?Opener=Paper_OtherQuestion";
						}
						else if(document.getElementById("rb3").checked == true)
						{
							//編輯其它的Specific問題
							location.href = "./CommonQuestionEdit/Page/ShowQuestion.aspx?Opener=Paper_OtherQuestion";
						}
						else if(document.getElementById("rb4").checked == true)
						{
							//完成考卷編輯
							location.href = "Paper_MainPage.aspx?Opener=Paper_OtherQuestion";
						}
					}
				}
				else
				{
					if(document.getElementById("rb2") != null && document.getElementById("rb4") != null)
					{
						if(document.getElementById("rb2").checked == true)
						{
						    // modified by dolphin @ 2006-07-29, new Question Group Tree
							//location.href = "./QuestionGroupTree/QuestionGroupTree.aspx?Opener=Paper_OtherQuestion";
							location.href = "./QuestionGroupTree/QGroupTree.aspx?Opener=Paper_OtherQuestion";
						}
						else if(document.getElementById("rb4").checked == true)
						{
							location.href = "Paper_MainPage.aspx?Opener=Paper_OtherQuestion";
						}
					}
				}
			}
			/*
					<tr id="tr2" align="left" runat="server">
						<td style="FONT-WEIGHT: bold; FONT-SIZE: 22px; COLOR: blue">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="rb2" type="radio" value="rb2" name="rb" runat="server">General 
							questions generated at run time
						</td>
					</tr>
					<tr id="tr3" align="left" runat="server">
						<td style="FONT-WEIGHT: bold; FONT-SIZE: 22px; COLOR: blue">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="rb3" type="radio" value="rb3" name="rb" runat="server">Specific 
							questions generated at authoring time
						</td>
					</tr>
			*/
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			
				<table id="body_content" align="center" style="WIDTH: 600px" cellPadding="10">
					<tr>
						<td class="title">Please select a 
							function</td>
					</tr>
					<tr id="tr1" align="left" runat="server">
						<td class="subtitle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="rb1" type="radio" CHECKED value="rb1" name="rb" runat="server">Add 
							other questions</td>
					</tr>
					<tr id="tr4" align="left" runat="server">
						<td class="subtitle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="rb4" type="radio" value="rb4" name="rb" runat="server">Preview 
							this examination paper
						</td>
					</tr>
					<tr align="right">
						<td><input id="btnPre" style="WIDTH: 150px; HEIGHT: 30px" onclick="goBack();" type="button" class="button_continue"
								value="<< Back" name="btnPre"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<input id="btnNext" style="WIDTH: 150px; HEIGHT: 30px" type="button" value="Next >>" name="btnNext" class="button_continue"
								runat="server">
						</td>
					</tr>
				</table>
				<input id="hiddenOpener" type="hidden" name="hiddenOpener" runat="server"> <input id="hiddenQuestionMode" type="hidden" name="hiddenQuestionMode" runat="server">
				<input id="hiddenPresentType" type="hidden" name="hiddenPresentType" runat="server">
				<input id="hiddenEditMode" type="hidden" name="hiddenEditMode" runat="server"> <input id="hiddenGroupID" type="hidden" name="hiddenGroupID" runat="server">
		
		</form>
	</body>
</HTML>
