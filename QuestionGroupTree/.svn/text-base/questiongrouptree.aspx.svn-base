<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page language="c#" Inherits="WebApplication1.Mt_Favorite" smartNavigation="False" CodeFile="QuestionGroupTree.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD runat=server>
		<title>My Favorite</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">		
		<!-- Code for browser detection -->
		<script src="ua.js"></script>
		<!-- Infrastructure code for the tree -->
		<script src="ftiens4.js"></script>		
	</HEAD>	
	<body class="txt" background="../../../Authoringtool/Images/bg3.gif">
	<script language="javascript">
		    USETEXTLINKS = 0  //replace 0 with 1 for hyperlinks
            // Decide if the tree is to start all open or just showing the root folders
            STARTALLOPEN = 0 //replace 0 with 1 to show the whole tree
            PRESERVESTATE = 1 //維持節點原來的狀態
            ICONPATH = 'image/'; //change if the gif's folder is a subfolder, for example: 'images/'            
		    <%=caseTree.strScript%>	
		    foldersTree.iconSrc = ICONPATH + "Division.gif";
		    function fnHandleDragStart()
			{                                    
				var oData = window.event.dataTransfer;

				// Set the effectAllowed on the source object.
				oData.effectAllowed = "move";
				oData.setData("text",event.srcElement.getAttribute('NodeID'));
			}
			
			function testClick()
			{
			    
			}
						
			var active_folder = null;//記錄Active的folder
			Active_count = 0;
			Edit_count = 0;
			function ClickNodeText()
			{
			    tmpObj = event.srcElement;
			    tmpObj.focus();
			    if(typeof(tmpObj.Mode)=="undefined" || (typeof(tmpObj.Mode)=="string" && tmpObj.Mode=="Default_Mode"))
			    {   
			        //若使用者第一次點選此Node Text,則表示使用者Active此節點
			        tmpObj.Mode = "Active_Mode";//change mode:Default_Mode => Active_Mode
			        active_folder = tmpObj;
			        window.setTimeout('focus_Active_Folder(tmpObj);',300);
			        tmpObj.style.color = "highlighttext";
			        tmpObj.style.backgroundColor = "highlight";	
			        EditGroupQuestion();
			        //window.status = "Active%";		        
			    }
			    else if(typeof(tmpObj.Mode)=="string" && tmpObj.Mode=="Active_Mode")
			    {
			        //若使用者Active此節點之後又點選此節點,則表示使用要編輯此節點			        
			        editNodeText(tmpObj);
			        //window.status = "Edit%";
			    }
			}
			
			function focus_Active_Folder(srcObj)
			{
			    srcObj.focus();
			    if(active_folder==null)
			    {
			        active_folder = srcObj;
			        //window.status = "Focus%";
			    }
			}
			
			function EditGroupQuestion()
			{
				var GroupID = "";
				
				//取得被按下的節點的GroupID
			    tmpObj = event.srcElement;
			    GroupID = tmpObj.getAttribute('NodeID');
			    
			    var strGroupName = tmpObj.outerText;
			    
			    if(document.getElementById("lblMsg") != null)
			    {
					document.getElementById("lblMsg").innerText = "You have selected " + strGroupName;
			    }
			    
			    //把該節點的GroupID存入hiddenGroupID
			    if(GroupID!="root")
			    {
					if(document.getElementById("hiddenGroupID") != null)
					{
						document.getElementById("hiddenGroupID").value = GroupID;
					}
			    }
			    
			    try
			    {
					document.getElementById("addSubGroup1").style.display = "";
					document.getElementById("addSubGroup2").style.display = "none";
			    }
			    catch(e)
			    {
					alert("addSubGroup");
			    }
			    
			    try
			    {
					document.getElementById("btnDeleteGroup1").style.display = "";
					document.getElementById("btnDeleteGroup2").style.display = "none";
			    }
			    catch(e)
			    {
					alert("btnDeleteGroup");
			    }
			}
			
			BlurNode_count = 0;
			function BlurNode()
			{
			    document.selection.empty();
			    event.srcElement.style.color = "black";
			    event.srcElement.style.backgroundColor = "transparent";
			    event.srcElement.contentEditable = false;
			    if(event.srcElement.Mode=="Edit_Mode")
			    {
			        //利用"ServerComunicationPage.aspx"將編輯好的網頁名稱更新回資料庫
			        nodeID = event.srcElement.parentElement.previousSibling.childNodes[1].childNodes[0].getAttribute('NodeID');
			        window.open("ServerComunicationPage.aspx?HandleType=Update_Folder_Name&NodeID="+nodeID+"&Parameter="+event.srcElement.innerText,"ServerComunicationFrame");
			    }
			    event.srcElement.Mode = "Default_Mode";//change mode:Edit_Mode => Active_Mode
			    //window.status = "BlurNode%";
			    reset_Active_FolderTimerID = window.setTimeout('reset_Active_Folder()',200);
			}
			
			var reset_Active_FolderTimerID = null;//執行"reset_Active_Folder()"Function的TimerID
			function reset_Active_Folder()
			{
			     active_folder = null;
			}
			
			//取消"reset_Active_Folder()"Function的執行
			function cancel_Reset_Active_Folder()
			{
			    window.clearTimeout("reset_Active_FolderTimerID");
			}
			
			//由Server端程式來觸發某個節點進入編輯模式
			function editNodeTextFireFromServer(NodeID)
			{
			    tmpNodeObj = getNodeObjByNodeID(NodeID);
			    editNodeText(tmpNodeObj);			    
			}
			
			//根據NodeID屬性取得某個Node物件
			function getNodeObjByNodeID(targetNodeID)
			{
			    ret = null;
			    objCollection = document.getElementsByTagName("a");
			    tmpNodeID = "";
			    for(var i=0;i<objCollection.length;i++)
			    {
			        tmpNodeID = objCollection[i].getAttribute('NodeID');
			        if(tmpNodeID==targetNodeID)
			        {
			            ret = objCollection[i];
			        }
			    }
			    return ret;
			}			
			
			function editNodeText(obj)
			{
			    //若此節點已經被選取,則使用者在點一下就可以編輯此節點
			    obj.Mode = "Edit_Mode";//change mode:Active_Mode => Edit_Mode
			    active_folder = obj;
			    obj.innerText = obj.innerText;
			    obj.style.color = "black";
			    obj.style.backgroundColor = "transparent";			    
			    //obj.outerHTML = "<input type='text' style='font-size:12px;color:black;height:20px;weight:30px' value='"+obj.innerText+"'>";			    
			    obj.contentEditable = true;			    
			    var txtRange = document.body.createTextRange();
                //txtRange.findText(obj.innerText);
                txtRange.moveToElementText(obj);
                txtRange.moveStart('character',4);
                //txtRange.findText(obj.innerText);
                //txtRange.select(); 
                //txtRange.collapse(false);                
                obj.focus();
			}			
			
			function noop()
			{
			    
			}

			// This function is called by the target 
			//  object in the ondrop event.
			function fnHandleDrop()
			{
				var oTarg = window.event.srcElement;
				var oData = window.event.dataTransfer;

				// Cancel default action.
				fnCancelDefault();
				//source node id of draging
				var srcNodeID = oData.getData("text");
				//destination node id of draging
				var desNodeID = event.srcElement.getAttribute('NodeID');
				dragNode(srcNodeID,desNodeID);
			}
			
			//drag Node
			function dragNode(srcNodeID,desNodeID)
			{
			   if(srcNodeID!=desNodeID) 
			   {
				   document.all('dragRecord').value = srcNodeID + "#" + desNodeID;
			       //利用按"dragNode"來觸發後斷程式事件,以改變節點的位置
			       document.all('dragNode_EventHandler_Button').click();
			   }
			}
			
			function addCaseNode_Function(DivsionID,CaseID)
			{
			    desNodeID = active_folder.getAttribute('NodeID');	
			    document.all('dragRecord').value = "RRRR" + "#" + desNodeID;
			    document.all('addCaseNode').click();
			}

			// This function sets the dropEffect when the user moves the 
			//  mouse over the target object.
			function fnHandleDragEnter()
			{
				var oData = window.event.dataTransfer;
				//source node id of draging
				var srcNodeID = oData.getData("text");
				//destination node id of draging
				var desNodeID = event.srcElement.getAttribute('NodeID');
				// Cancel default action.
				fnCancelDefault();
				// Set the dropEffect for the target object.
				oData.dropEffect = "move";
			}
			
			//展開某個結點
			function unfoldNode(str_NodeID)
			{
				tmp_id = getIDByNodeID(str_NodeID);
				folderObj = findObj(tmp_id);
				if(folderObj.isOpen==false)
				{
				    clickOnNodeObj(folderObj);
				}
			}

			function fnCancelDefault()
			{
				// Cancel default action.
				var oEvent = window.event;
				oEvent.returnValue = false;
			}
			
			//增加新資料夾						
			function New_folder()
		    {
				var strGroupID = "";
				if(document.getElementById("hiddenGroupID") != null)
				{
					strGroupID = document.getElementById("hiddenGroupID").value;
				}
				//alert(strGroupID);
				if(strGroupID == "")
				{
					alert("Please select a topic");
				}
				else
				{
				    //alert(active_folder);
					if(active_folder!=null)
					{		        
						cancel_Reset_Active_Folder();
						parentNodeID = active_folder.getAttribute('NodeID');
						document.all('add_folder_Record').value = parentNodeID;
						document.all('addFolder_EventHandler_Button').click();
					}
				}
		    }
		    
		    //刪除資料夾
			function Delete_Node()
			{
				var strGroupID = "";
				if(document.getElementById("hiddenGroupID") != null)
				{
					strGroupID = document.getElementById("hiddenGroupID").value;
				}
				
				if(strGroupID == "")
				{
					alert("Please select a topic");
				}
				else
				{
					if(active_folder!=null)
					{
					srcNodeID = active_folder.getAttribute('NodeID');			    
					document.all('Delete_Node_Record').value = srcNodeID;
					document.all('Delete_Node_EventHandler_Button').click();
					}
			    }
			}
		    
		    function hrefFunction()
			{
			    caseID = event.srcElement.getAttribute('ID');
			    NodeName = event.srcElement.getAttribute('Name');
			    DivisionID = event.srcElement.getAttribute('Did');
			    var url = "/webhints/Translate tree/caseSelect.asp?ID="+caseID+"&Name="+NodeName+"&URL=localhost&Did="+DivisionID;
			    window.open(url,'CONTROL');
			}
			
			function goBack()
			{
				/*
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
				
				var strModifyType = "";
				if(document.getElementById("hiddenModifyType") != null)
				{
					strModifyType = document.getElementById("hiddenModifyType").value;
				}
				
				if(strModifyType == "Paper")
				{
					//編輯考卷
					if(strPresentType == "Edit")
					{
						if(strOpener == "Paper_EditMethod")
						{
							location.href = "../Paper_EditMethod.aspx";
						}
						else if(strOpener == "")
						else
						{
							location.href = "../Paper_MainPage.aspx";
						}
					}
					else
					{
						if(strOpener == "Paper_EditMethod")
						{
							location.href = "../Paper_EditMethod.aspx";
						}
						else
						{
							location.href = "../Paper_MainPage.aspx";
						}
					}
				}
				else
				{
					//編輯題目
					location.href = "../Paper_QuestionMain.aspx";
				}
				*/
				var strOpener = "";
				if(document.getElementById("hiddenOpener") != null)
				{
					strOpener = document.getElementById("hiddenOpener").value;
				}
				
				var strQuestionMode = "";
				if(document.getElementById("hiddenQuestionMode") != null)
				{
					strQuestionMode = document.getElementById("hiddenQuestionMode").value;
				}
				
				var strModifyType = "";
				if(document.getElementById("hiddenModifyType") != null)
				{
					strModifyType = document.getElementById("hiddenModifyType").value;
				}
				
				if(strModifyType == "Paper")
				{
					//編輯考卷
					if(strOpener == "Paper_QuestionMode")
					{
						location.href = "../Paper_QuestionMode.aspx";
					}
					else if(strOpener == "Paper_OtherQuestion")
					{
						location.href = "../Paper_OtherQuestion.aspx";
					}
					else if(strOpener == "Paper_PresentMethod")
					{
						location.href = "../Paper_PresentMethod.aspx";
					}
					else if(strOpener == "Paper_EditMethod")
					{
						location.href = "../Paper_EditMethod.aspx";
					}
					else if(strOpener == "Paper_MainPage")
					{
						location.href = "../Paper_MainPage.aspx";
					}
					else if(strOpener == "Paper_QuestionMain")
					{
						location.href = "../Paper_QuestionMain.aspx";
					}
					else
					{
						location.href = "../Paper_QuestionMode.aspx";
					}
				}
				else
				{
					//編輯題目
					location.href = "../Paper_QuestionMain.aspx";
				}
			}
			
			function goNext()
			{
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
				
				var strGroupID = "";
				if(document.getElementById("hiddenGroupID") != null)
				{
					strGroupID = document.getElementById("hiddenGroupID").value;
				}
				
				var strModifyType = "";
				if(document.getElementById("hiddenModifyType") != null)
				{
					strModifyType = document.getElementById("hiddenModifyType").value;
				}
				
				if(strModifyType == "Paper")
				{
					//編輯考卷
					if(strPresentType == "Edit")
					{
						if(strEditMode == "Automatic")
						{
							location.href = "../Paper_RandomSelect.aspx?GroupID="+strGroupID;
						}
						else
						{
							location.href = "../Paper_SelectQuestion.aspx?GroupID="+strGroupID;
						}
					}
					else
					{
						location.href = "../Paper_RandomSelect.aspx?GroupID="+strGroupID;
					}
				}
				else
				{
					//編輯題目
					var strQuestionFunction = "";
					if(document.getElementById("hiddenQuestionFunction") != null)
					{
						strQuestionFunction = document.getElementById("hiddenQuestionFunction").value;
					}
					
					if(strQuestionFunction == "New")
					{
						//新編題目
						if(document.getElementById("hiddenQuestionType").value == "1")
						{
							//選擇題
							location.href = "../CommonQuestionEdit/Page/ShowQuestion.aspx?GroupID="+strGroupID;
						}
						else
						{
							//問答題
							location.href = "../Paper_TextQuestionEditor.aspx?GroupID="+strGroupID;
						}
					}
					else
					{
						//編輯或刪除題目
						location.href = "../Paper_QuestionView.aspx?GroupID="+strGroupID;
						
					}
				}
			}	
			
		</script>
		<!--
	<INPUT id="addSubGroup" onclick='New_folder();' style="WIDTH: 96px; HEIGHT: 24px" type="button"
				value="Add Sub Group">
	<INPUT id="DeleteGroup" style="WIDTH: 96px; HEIGHT: 24px" type="button" value="Delete Group"
				onclick='Delete_Node();'>
	--><table id='body_content'><tr><td>
		<center>
			<span id="lblMsg" style="FONT-WEIGHT: bold; FONT-SIZE: 28px; LEFT: 40%; COLOR: blue; POSITION: absolute; TOP: 40%">
				Please select a topic</span>
		</center>
		<br>
		<input type='button' id="addSubGroup1" class='button_continue' value='Add folder' style="DISPLAY: none; CURSOR: hand" onclick="New_folder()" name="addSubGroup1">
		<input type='button' class='button_continue' id="addSubGroup2" value='Add Sub Group'  style="CURSOR: hand" onclick="alert('Please select a topic');" name="addSubGroup2">&nbsp;&nbsp;
		<input type='button' class='button_continue' id="btnDeleteGroup1" value='Delete Group' style="DISPLAY: none; CURSOR: hand" onclick="Delete_Node()" name="btnDeleteGroup1">
		<input type='button' class='button_continue' id="btnDeleteGroup2" value='Delete Group' style="CURSOR: hand" onclick="alert('Please select a topic');" name="btnDeleteGroup2">
		<br>
		<br>
		<div style="Z-INDEX: 100; LEFT: 30px; WIDTH: 8px; POSITION: absolute; TOP: 30px; HEIGHT: 3px">
			<table border="0">
				<tr>
					<td><a style="FONT-SIZE: 7pt; COLOR: silver; TEXT-DECORATION: none" href="http://www.treemenu.net/"
							target="_blank"></a></td>
				</tr>
			</table>
		</div>
		<script language="javascript">initializeDocument();</script>
		<form id="Form1" method="post" runat="server">
			<input id="hiddenOpener" type="hidden" name="hiddenOpener" runat="server"> <input id="hiddenEditMode" type="hidden" name="hiddenEditMode" runat="server">
			<input id="hiddenQuestionMode" type="hidden" name="hiddenQuestionMode" runat="server">
			<input id="hiddenQuestionType" type="hidden" name="hiddenQuestionType" runat="server">
			<input id="hiddenPresentType" type="hidden" name="hiddenPresentType" runat="server">
			<input id="hiddenModifyType" type="hidden" name="hiddenModifyType" runat="server">
			<input id="hiddenQuestionFunction" type="hidden" name="hiddenQuestionFunction" runat="server">
			<input type="hidden" id="hiddenGroupID" name="hiddenGroupID" runat="server">
			<P><br>
				<INPUT id="Delete_Node_Record" style="WIDTH: 288px; HEIGHT: 22px" type="hidden" size="42"
					value="當刪除資料夾時,用來記錄哪個欲刪除的資料夾ID" name="Delete_Node_Record" runat="server"><asp:button id="Delete_Node_EventHandler_Button" runat="server" Text="Delete_Node" onclick="Delete_Folder_EventHandler_Button_Click"></asp:button><br>
				<INPUT id="add_folder_Record" style="WIDTH: 315px; HEIGHT: 22px" type="hidden" size="47"
					value="當新增資料夾時,用來記錄哪個父資料欲新增子資料夾" name="add_folder_record" runat="server"> <INPUT id="addFolder_EventHandler_Button" type="button" value="addFolder" name="addFolder_EventHandler_Button"
					runat="server">
				<br>
				<INPUT id="dragRecord" style="WIDTH: 544px; HEIGHT: 22px" type="hidden" size="85" value='當拖曳節點時,用來記錄拖抑來源節點和目標節點的紀錄,其中來源節點和目標節點以"#"間隔'
					name="Hidden1" runat="server"> <INPUT id="dragNode_EventHandler_Button" type="button" value="dragNode" name="Button1"
					runat="server">
				<asp:button id="addCaseNode" runat="server" Text="Button" onclick="addCaseNode_Click"></asp:button></P>
			<div style="WIDTH: 50%" align="right">
				<input id="btnPre" style="WIDTH: 80px; HEIGHT: 30px" class='button_continue' onclick="goBack();" type="button"
					value="<< Back" name="btnPre"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<input id="btnNext" style="WIDTH: 80px; HEIGHT: 30px" class='button_continue' onclick="goNext();" type="button"
					value="Next >>" name="btnNext">
			</div>
		</form>
		<iframe id="ServerComunicationFrame" name="ServerComunicationFrame" src="about:blank" frameBorder="0"
			width="0%" height="0%"></iframe>
		<%=unfoldNode_Script%>
		<%=editNodeText_Script%>
		</td></tr></table>
	</body>
</HTML>
