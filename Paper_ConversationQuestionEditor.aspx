<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Paper_ConversationQuestionEditor.aspx.cs"
    Inherits="PaperSystem.Paper_ConversationQuestionEditor" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Frameset//EN" "http://www.w3.org/TR/html4/frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Paper_ConversationQuestionEditor</title>
    <style type="text/css">
		/* ==================== Player ==================== */
		#player {
			background-color: #e9f6ff;
			color: #000;
			height: 40px;
			width: 400px;
			border: solid 1px #ccc;
		}
		#player .button {
			width: 40px;
			height: 40px;
			background-repeat: no-repeat;
			background-position: 2px 2px;
			float: left;
		}
		#player .button a {
			display: block;
			height: 40px;
            background-repeat: no-repeat;
            background-position: 2px 2px;
			text-indent: -100em;
			overflow: hidden;
		}
		#player .timeline {
			position: relative;
		}
		#player .timeline a {
			position: absolute;
			left: 40px;
			display: block;
			height: 40px;
			width: 20px;
			text-indent: -100em;
			overflow: hidden;
			background-image: url(images/control_slider1.png);
		}
		#player .timeline a:hover {
			background-image: url(images/control_slider_blue1.png);
		}
		#player .play {
			background-image: url(images/control_play.png);
		}
		#player .play a:hover {
			background-image: url(images/control_play_blue.png);
		}
		#player .pause  {
			background-image: url(images/control_pause.png);
			display: none;
		}
		#player .pause a:hover {
			background-image: url(images/control_pause_blue.png);
		}
		#player .stop  {
			background-image: url(images/control_stop.png);
		}
		#player .stop a:hover {
			background-image: url(images/control_stop_blue.png);
		}

        input {
            background-color:transparent;
            border: 0px solid;
        }
        input:focus {
            outline:none;
        }
		</style>
    <script type="text/javascript">
        var myListener = new Object();

        /**
         * Initialisation
         */
        myListener.onInit = function () {
            try
            {
                var filepath = document.getElementById('hfFilePath').value;
                getFlashObject().SetVariable("method:setUrl", filepath);
            }
            catch (e) {
                alert(e);
            }
            //getFlashObject().SetVariable("method:play", "");
        };
        /**
         * onClick event on the video
         */
        myListener.onClick = function () {
            var total = document.getElementById("info_click").innerHTML;
            document.getElementById("info_click").innerHTML = Number(total) + 1;
        };
        /**
         * onKeyUp event on the video
         */
        myListener.onKeyUp = function (pKey) {
            document.getElementById("info_key").innerHTML = pKey;
        };
        /**
         * onComplete event
         */
        myListener.onFinished = function () {
            //alert("finished");
            getFlashObject().SetVariable("method:stop", "");
            //getFlashObject().SetVariable("method:play", "");
        };
        /**
         * Update
         */
        /*myListener.onUpdate = function () {
            document.getElementById("info_playing").innerHTML = this.isPlaying;
            document.getElementById("info_url").innerHTML = this.url;
            document.getElementById("info_volume").innerHTML = this.volume;
            document.getElementById("info_position").innerHTML = this.position;
            document.getElementById("info_duration").innerHTML = this.duration;
            document.getElementById("info_buffer").innerHTML = this.bufferLength + "/" + this.bufferTime;
            document.getElementById("info_bytes").innerHTML = this.bytesLoaded + "/" + this.bytesTotal + " (" + this.bytesPercent + "%)";

            var isPlaying = (this.isPlaying == "true");
            document.getElementById("playerplay").style.display = (isPlaying) ? "none" : "block";
            document.getElementById("playerpause").style.display = (isPlaying) ? "block" : "none";

            var timelineWidth = 280;
            var sliderWidth = 20;
            var sliderPositionMin = 40;
            var sliderPositionMax = sliderPositionMin + timelineWidth - sliderWidth;
            var sliderPosition = sliderPositionMin + Math.round((timelineWidth - sliderWidth) * this.position / this.duration);

            if (sliderPosition < sliderPositionMin) {
                sliderPosition = sliderPositionMin;
            }
            if (sliderPosition > sliderPositionMax) {
                sliderPosition = sliderPositionMax;
            }

            document.getElementById("playerslider").style.left = sliderPosition + "px";
        };*/

        function getFlashObject() {
            return document.getElementById("myFlash");
        }
        function play() {
            try
            {
                if (myListener.position == 0) {
                    // getFlashObject().SetVariable("method:setUrl", 'http://140.116.72.28/AudioRecord/streams/_definst_/20141214104808.flv');
                }
                getFlashObject().SetVariable("method:play", "");
            }
            catch(e)
            {
                alert(e);
            }
        }
        function pause() {
            getFlashObject().SetVariable("method:pause", "");
        }
        function stop() {
            getFlashObject().SetVariable("method:stop", "");
        }
        function setWidth() {
            var width = document.getElementById("inputWidth").value;
            getFlashObject().width = width + "px";
        }
        function setHeight() {
            var height = document.getElementById("inputHeight").value;
            getFlashObject().height = height + "px";
        }
        function setPosition() {
            var position = document.getElementById("inputPosition").value;
            getFlashObject().SetVariable("method:setPosition", position);
        }
        function setVolume() {
            var volume = document.getElementById("inputVolume").value;
            getFlashObject().SetVariable("method:setVolume", volume);
        }
        function loadImage() {
            var url = document.getElementById("inputImage").value;
            var depth = document.getElementById("inputImageDepth").value;
            var verticalAlign = document.getElementById("inputImageVertical").value;
            var horizontalAlign = document.getElementById("inputImageHorizontal").value;

            getFlashObject().SetVariable("method:loadMovieOnTop", url + "|" + depth + "|" + verticalAlign + "|" + horizontalAlign);
        }
        function unloadImage() {
            var depth = document.getElementById("inputUnloadDepth").value;
            getFlashObject().SetVariable("method:unloadMovieOnTop", depth);
        }

        function SaveRecognitionMsg() {
            //alert(parent.document.getElementById("textBox").value);
            CallServer(parent.document.getElementById("textBox").value);
            parent.document.getElementById("textBox").value = "";
        }
        function ReceiveServerData(employee_info) {
            document.getElementById("Result").innerHTML = employee_info;
        }
        function Test() {
            __doPostBack('LinkButton1', '');

        }

        function confirm_delete() {
            if (window.confirm("確定要刪除此正確例句?"))
                document.getElementById('btnDeleteAudioServer').click();
        }

        function ReloadOpenner() {
            window.close();
        }

        /*function init() {          
            var video = document.getElementById('videoSrc');
            alert(video);
            var bModify = document.getElementById('hfFilePath').value;
            var strQID = document.getElementById('hfQID').value;
            var strPath = Paper_ConversationQuestionEditor.CheckAudioID(bModify, strQID).value;
            alert(strPath);
            if ((video != null) && (strPath.toString() != ""))
            {
                document.getElementById('ExCorrectsentence').style.display = '';
            }
            document.getElementById('hfFilePath').value = strPath.toString();
            video.src = document.getElementById('hfFilePath').value;          
            alert('123');
        }*/ 
    </script>

    <script type="text/javascript" event="FSCommand(command,args)" for="myFlash">
        eval(args);
	</script>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="http://vjs.zencdn.net/c/video-js.css" rel="stylesheet">
    <script src="http://vjs.zencdn.net/c/video.js"></script>
</head>
<body>

    <script language="Javascript">
            function KeywordSelectOnMouseUp(){
                //Activity_EEPBL_MLASEEPBLCaseInfo.Addnewtb_Click();
                try {
                    var selectedText="";
                    if (window.getSelection) {  // all browsers, except IE before version 9
                        if (document.activeElement &&
                              (document.activeElement.tagName.toLowerCase() == "textarea" ||
                               document.activeElement.tagName.toLowerCase() == "input")) {
                            var text = document.activeElement.value;
                            selectedText = text.substring(document.activeElement.selectionStart,
                                                          document.activeElement.selectionEnd);
                        }
                        else {
                            var selRange = window.getSelection();
                            selectedText = selRange.toString();
                        }
                    }
                    else {
                        if (document.selection.createRange) { // Internet Explorer
                            var range = document.selection.createRange();
                            selectedText = range.text;
                        }
                    }
                    //alert(selectedText);
                    if (selectedText != "") {
                        document.getElementById('HiddenFieldfortext').value = selectedText;
                        document.getElementById('Addnewtb').click();
                    }
                }
                catch(e)
                {
                    alert(e);
                }
            }
            function KeywordRemoveOnMouseUp() {
                //Activity_EEPBL_MLASEEPBLCaseInfo.Addnewtb_Click();
                var selectedText="";
                if (window.getSelection) {  // all browsers, except IE before version 9
                    if (document.activeElement &&
                          (document.activeElement.tagName.toLowerCase() == "textarea" ||
                           document.activeElement.tagName.toLowerCase() == "input")) {
                        var text = document.activeElement.value;
                        selectedText = text.substring(document.activeElement.selectionStart,
                                                      document.activeElement.selectionEnd);
                    }
                    else {
                        var selRange = window.getSelection();
                        selectedText = selRange.toString();
                    }
                }
                else {
                    if (document.selection.createRange) { // Internet Explorer
                        var range = document.selection.createRange();
                        selectedText = range.text;
                    }
                }
                //alert(selectedText);
                if (selectedText != "") {
                    document.getElementById('HiddenFieldforRemove').value = selectedText;
                    document.getElementById('Remove_Btn').click();
                }                    
            }
			function Clear()
			{
				if(document.all("txtQuestionEdit") != null)
				{
					document.all("txtQuestionEdit").value = "";
	             }

	            if (document.all("txtAnswerEdit") != null) {
	                document.all("txtAnswerEdit").value = "";
	            }
			}

			function NextStep() {
			    //var strTmp2 = document.getElementById('Lb_showKeyword').value;
			    //GetAllCheckedSynonyms(strTmp2, document.getElementById("hfQID").value);
			    document.getElementById("btnSaveNext").click();
			}

			function goBack(strCareer,bDisplayBQL) {
			    if (window.confirm("您的資料將不會被儲存，您確定要繼續嗎?")) {
			        var BackUrl = document.getElementById("hiddenOpener").value;
			        var varGroupID = document.getElementById("hfGroupID").value;
			        if (BackUrl == "Paper_QuestionViewNew_VoiceInquiry") {
			            window.location.href = BackUrl + ".aspx?Opener=Paper_QuestionViewNew_VoiceInquiry&GroupID=" + varGroupID + "&Career=" + strCareer + "&bDisplayBQL=" + bDisplayBQL + "";
			        }
			        else {
			            var PaperID = document.getElementById("hfPaperID").value;
			            if (PaperID == "")//由編輯題庫地方來的
			            {
			                window.location.href = "QuestionGroupTree/" + BackUrl + ".aspx?Career=" + strCareer;
			            }
			            else {
			                window.history.back();
			            }
			        }
			    }
			}

			function GetAllCheckedSynonyms(strAllKeyword, strQID) {			    
			    var ArrKeyword = strAllKeyword.split('|');			    
			    var Temp = document.getElementsByTagName("input");
			    var strTmp = "";
			    for (var j = 0; j < ArrKeyword.length; j++)
			    {
			        var KeyTmp = ArrKeyword[j] + ",";
			        var count = 0;
			        for (var i = 0; i < Temp.length; i++)
			        {
			            if ((Temp[i].type == "checkbox") && (Temp[i].id.indexOf(ArrKeyword[j]) >= 0)) {
			                if (Temp[i].checked == true) {
			                    var strArray = Temp[i].id.split('_');
			                    KeyTmp += (strArray[1] + ",");			                    
			                }
			            }
			        }
			        KeyTmp = KeyTmp.substring(0, KeyTmp.length - 1);
			        strTmp += (KeyTmp + "|");
			    }
			    strTmp = strTmp.substring(0, strTmp.length - 1);
			    document.getElementById('hfNewKeyword').value = strTmp;
			    //alert(document.getElementById('hfNewKeyword').value);
			    Paper_ConversationQuestionEditor.SaveSynonyms(strTmp, strQID);
			}
    </script>

    <form id="Form1" method="post" runat="server">
    <table id="body_content" width="100%" align="center">
        <tr>
            <td>
                Paper Conversation Question Editor
            </td>
        </tr>
        <tr>
            <td>
                <hr />
            </td>
        </tr>
        <tr>
            <td>
                Question Topic : &nbsp;
               <asp:Label ID="lbCurrentGroupName" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Question Contents : &nbsp;
                <asp:Button ID="btAddSynQuestion" runat="server" Text="Add Synonymous Question" Style="cursor: hand; display:none"
                    CssClass="button_continue" Width="250px" OnClick="btAddSynQuestion_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <div align="right">
                    <asp:TextBox ID="txtQuestionEdit" runat="server" Width="100%" TextMode="MultiLine"
                        Rows="4" ></asp:TextBox>
                    
                </div>
                
            </td>
        </tr>
        <tr  >
            <td>
                <table align="left" style="padding: 0px; margin: 0px; width:90%">
                 <tr>
                  <td>
                     <asp:Button ID="btnEditKeyword" CssClass="button_continue" Style="width: 150px; cursor: pointer;
                     height: 30px; " runat="server" Text="Edit Keywords" OnClick="btnEditKeyword_Click" />
                     <table ID="Lb_synonyms" runat="server" style="padding: 0px; margin: 0px; text-align: left; width:100%">
                     </table>
                  </td>
                 </tr>
                </table>
            </td>
        </tr>
         <tr style="display:none;">
            <td>
                <asp:Label ID="Label_Keword_title" runat="server" Text="Keyword :" Font-Size="Larger"></asp:Label>&nbsp;&nbsp;
                <input type="text" id="Lb_showKeyword" runat="server" value="" onmouseup="Javascript:KeywordRemoveOnMouseUp()" readonly="true" style="color: #FF0000; font-size: large; background-color: #ebecee; width: 25%;" /><br/>
                <asp:Label ID="Label_Keyword_Learn" runat="server" Font-Size="Larger" Text="（若要移除Keyword，請反白或點擊兩下。此時Synonyms也會一併消失。）"></asp:Label><br/>               
            </td>
        </tr>        

        <tr>
            <td align="center">
                <hr />
                <asp:Table ID="tbInterrogationClassQutstion" runat="server" BorderWidth="1px" BorderColor="Blue"
                    GridLines="Both" Width="100%">
                </asp:Table>
            </td>
        </tr>
        <tr id="trANswerType" runat="server">
            <td>
                <hr id="hrQuestion" runat="server" />
                <asp:Label ID="Title_Answer_Type" runat="server" Text="Answer Type :" Font-Size="Larger"></asp:Label>
                <asp:Label ID="lbAnswerType" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                <asp:Button ID="btAddAnswer" runat="server" Text="Add a new answer" Style="cursor: hand"
                    CssClass="button_continue" Width="250px" OnClick="btAddAnswer_Click" />
                <asp:Button ID="btRecordAnswer" runat="server" Text="Record Question Contents" Style="cursor: pointer" Height="30"  CssClass="button_continue" Width="250px" OnClick="btRecordAnswer_Click" Visible="False" />
                <hr />
            </td>
        </tr>
        <tr id="ExCorrectsentence" runat="server">
            <td>
                <asp:Label runat="server" Text="《Example of correct sentence》" Font-Size="Larger"></asp:Label>
                <table> 
                 <tr><td id="AudioIframe" runat="server"></td></tr> 
                </table>         
		        <div id="player" style="display:none;">
			        <div id="playerplay" class="button play"><a href="javascript:play()" >PLAY</a></div>
			        <div id="playerpause" class="button pause"><a href="javascript:pause()">PAUSE</a></div>
			        <div id="playerstop" class="button stop"><a href="javascript:stop()">STOP</a></div>
			        <div class="timeline"><a id="playerslider" href="#slider">SLIDER</a></div>
		        </div>
                <object id="myFlash" type="application/x-shockwave-flash" data="player_flv_js.swf" width="320" height="0">
			      <param name="movie" value="player_flv_js.swf" />
			      <param name="AllowScriptAccess" value="always" />
			      <param name="FlashVars" value="listener=myListener&amp;interval=100&amp;useHandCursor=0&amp;bgcolor=fd9f00&amp;buffer=9" />
			      <p>Texte alternatif</p>
		        </object><br/>
                <asp:Label ID="lbDeleteRecordHints" runat="server" Text="Hints：若不滿意此例句，請點擊右邊刪除按鈕：" Font-Size="Larger" ForeColor="Red" Height="30px"></asp:Label>
                <asp:ImageButton ID="ImgB_DeleteExCorrectsentence" runat="server" Height="30px" ImageUrl="~/AuthoringTool/CaseEditor/Paper/images/ed_delete.gif" Width="30px" ToolTip="點擊後會刪除此錄音檔，使用者必須重新設定正確例句。" OnClientClick="confirm_delete()" />
                <input type="button" style="display:none;" id="btnDeleteAudioServer" runat="server" />
            <h2 style="display: none">Exemples javascript</h2>
		    <ul style="display: none">
			<li>Modifier la largeur: <input id="inputWidth" type="text" value="200" size="3"/> <input type="button" value="Modifier" onclick="setWidth()"/></li>
			<li>Modifier la hauteur: <input id="inputHeight" type="text" value="200" size="3"/> <input type="button" value="Modifier" onclick="setHeight()"/></li>
			<li>Modifier la position: <input id="inputPosition" type="text" value="10000" size="6"/> <input type="button" value="Modifier" onclick="setPosition()"/></li>
			<li>Modifier le volume: <input id="inputVolume" type="text" value="100" size="3"/> <input type="button" value="Modifier" onclick="setVolume()"/></li>
			<li>Charger une image <input id="inputImage" type="text" value="logo.jpg" size="10"/> à la profondeur <input id="inputImageDepth" type="text" value="1" size="1"/>, alignement vertical <input id="inputImageVertical" type="text" value="" size="2"/>, alignement horizontal <input id="inputImageHorizontal" type="text" value="" size="2"/> <input type="button" value="Charger" onclick="loadImage()"/></li>
			<li>Décharger une image à la profondeur <input id="inputUnloadDepth" type="text" value="1" size="1"/> <input type="button" value="Décharger" onclick="unloadImage()"/></li>
		</ul>
	
		<h2 style="display: none">Informations</h2>
		<ul style="display: none">
			<li>url : <span id="info_url">undefined</span></li>
			<li>chargement : <span id="info_bytes">undefined</span></li>
			<li>isPlaying : <span id="info_playing">undefined</span></li>
			<li>position : <span id="info_position">undefined</span></li>
			<li>duration : <span id="info_duration">undefined</span></li>
			<li>tampon : <span id="info_buffer">undefined</span></li>
			<li>volume : <span id="info_volume">undefined</span></li>
			<li>click : <span id="info_click">0</span></li>
			<li>key up : <span id="info_key">undefined</span></li>
		</ul>

            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Table ID="tbInterrogationClassAnswer" runat="server" BorderWidth="1px" BorderColor="Blue"
                    GridLines="Both" Width="100%">
                </asp:Table>
            </td>
        </tr>
        <tr style="display:none">
            <td>
                <hr />
                Keyword content :
            </td>
        </tr>
        <tr height="10px" style="display:none">
            <td>
            </td>
        </tr>
        <tr style="display:none">
            <td>
                <asp:Label ID="lbKeyword" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="10px">
                <hr id="hrAnswer"  runat="server" />
            </td>
        </tr>
        <tr id="trShowCurProblemType" runat="server">
            <td>
                <asp:Label ID="lbCurrentProblemType" runat="server" Text="Current Problem Type :" Font-Size="Larger"></asp:Label>&nbsp;&nbsp;
                <asp:DropDownList ID="ddlSymptoms" runat="server" Font-Size="Larger" Visible="False"></asp:DropDownList>
                <asp:Label ID="LbCurrentSymptoms" runat="server" Font-Size="Larger" ForeColor="Red"></asp:Label>
                
                <hr />
            </td>
        </tr>

       
           
        <tr width="100%">
            <td align="right"> 
                <input id="btnSaveNextQuestion" style="display:none" type="button" name="btnSaveNextQuestion"
                    runat="server">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input id="btnSaveNext" style="display:none" type="button" name="btnSaveNext" runat="server">               
                <input id="btnClose" type="button" runat="server" value="Close" class="button_continue" onclick="ReloadOpenner()" style="font-size: larger; width: 150px; height: 30px" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <input id="btnBack" runat="server" style="width: 150px; cursor: pointer; height: 30px" onclick="if( !confirm('您的資料將不會被儲存，您確定要繼續嗎?'))return false;" onserverclick="GobackCareer"
                    type="button" value="<<  Back"  class="button_continue" />
                &nbsp;&nbsp;&nbsp;&nbsp;



               <%--<asp:Button id="btnBack" runat="server" Width="150px" Height="30px" Text="<<  Back" onclick="if( !confirm('您的資料將不會被儲存，您確定要繼續嗎?'))return false;" onserverclick="GobackCareer" Style="cursor: pointer" class="button_continue" />--%>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <input id="btnNextQuestion" style="width: 150px; cursor: pointer; height: 30px; display:none" onclick="NextQuestion()"
                    type="button" value="Edit next question >>" name="btnNextQuestion" class="button_continue">
                <%--&nbsp;&nbsp;&nbsp;&nbsp;--%>
                
                 <input id="btnNext" runat="server" style="width: 150px; cursor: pointer; height: 30px" onclick="NextStep()"
                    type="button" value="Next  >>" name="btnNext" class="button_continue" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button runat ="server"  id="Addnewtb"  Width="0" />
                <asp:Button runat ="server" id="Remove_Btn"  Width="0" />
            </td>
        </tr>
                <tr>
            <td>
                <div>
                    <input id="hiddenOpener" type="hidden" name="hiddenOpener" runat="server">
                    <input id="hiddenQuestionMode" type="hidden" name="hiddenQuestionMode" runat="server">
                    <input id="hiddenPresentType" type="hidden" name="hiddenPresentType" runat="server">
                    <input id="hiddenEditMode" type="hidden" name="hiddenEditMode" runat="server">
                    <asp:HiddenField ID="hfPaperID" runat="server" />
                    <asp:HiddenField ID="hfGroupID" runat="server" />
                    <asp:HiddenField ID="hfGroupSerialNum" runat="server" />
                    <asp:HiddenField ID="HiddenFieldfortext" runat="server" />
                    <asp:HiddenField ID="HiddenFieldforRemove" runat="server" />
                    <asp:HiddenField ID="hfFilePath" runat="server" />
                    <asp:HiddenField ID="hfNewKeyword" runat="server" Value="" />
                    <asp:HiddenField ID="hfQID" runat="server" Value="" />
                </div>
                
            </td>
        </tr>
    </table>
        <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
        <div>
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
            <asp:Timer ID="TimerCheck" runat="server" Enabled="false" Interval="2000" OnTick="TimerCheck_Tick"></asp:Timer>
           </ContentTemplate>
          </asp:UpdatePanel>
        </div>
    </form>

    <script language="javascript">
			
			function NextQuestion()
			{
				document.getElementById("btnSaveNextQuestion").click();
			}
		
			function ShowDetail(strSelectionTRID , strImgID)
            {
                if(document.getElementById(strSelectionTRID) != null)
                {
                    if(document.getElementById(strSelectionTRID).style.display == "none")
                    {
                        document.getElementById(strSelectionTRID).style.display = "";
                        document.getElementById(strImgID).src = "../../../BasicForm/Image/minus.gif";
                    }
                    else
                    {
                        document.getElementById(strSelectionTRID).style.display = "none";
                        document.getElementById(strImgID).src = "../../../BasicForm/Image/plus.gif";
                    }
                }
            }
            
            function PlayFlash(ItemSeq, FileNameNoVice)
            {
               Player = document.getElementById("FlashPlayer" + ItemSeq + FileNameNoVice); 
               Player.Play();
            }

    </script>

    <%=strClientScript%>
</body>
</html>
