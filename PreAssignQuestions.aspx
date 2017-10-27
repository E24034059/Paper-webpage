<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PreAssignQuestions.aspx.cs" Inherits="AuthoringTool_CaseEditor_Paper_PreAssignQuestions" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Assign questions</title>
</head>
<body onload="resizeWindow()">
    <script language="javascript">
        function closeWindow()
        {
            document.body.style.cursor = "";
            
            window.close();
        }
        
        function startAssign()
        {
            document.body.style.cursor = "wait";
            
            alert("Please wait");
            
            if(document.getElementById("btnFinish") != null)
            {
                document.getElementById("btnFinish").click();
            }
        }
        
        function resizeWindow()
		{
			var myWidth = 900;
			var myHeight = 300;
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

		function openTrClass()
		{
		    document.getElementById("trClass").style.display = "";
		    document.getElementById("trGroup").style.display = "none";
		}
		
		function openTrGroup()
		{
		    document.getElementById("trClass").style.display = "none";
		    document.getElementById("trGroup").style.display = "";
		}
		//fadis 2006.8.3
		//開啟月曆的FUNCTION
		function OpenCalender(objID)
		{
		    window.open("../GetCalendar.aspx?Destination="+objID,"","menubars=no,scrollbars=no,toolbars=no,resizable=no,width=430,height=300");
		}
		//取得月曆回傳值的FUNCTION
		function SetDate(objID,date)
		{
		    document.getElementById(objID).value=date;
		}
    </script>
    <form id="form1" runat="server" >
    <div>
        <table class="header1_table" align="center" id="body_content" style="WIDTH: 100%;" cellspacing="0" border="1" cellPadding="10">
            <tr class="header1_table_first_row">
                <td width="50%">
                    Select a method to assign
                </td>
                <td>
                    <table width="100%" height="100%">
                        <tr><td width="50%"><input type="radio" id="rbClass" name="rb" runat="server" onclick="openTrClass()" />Class</td>
                        <td><input type="radio" id="rbGroup" name="rb" runat="server" onclick="openTrGroup()" />Group</td></tr>
                    </table>
                </td>
            </tr>
            <tr id="trClass" class="header1_tr_even_row" runat="server">
                <td width="50%">Class name:&nbsp;<asp:DropDownList runat="server" Width="200px" ID="dlClass1"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr id="trGroup" class="header1_tr_even_row" style="display:none;" runat="server">
                <td width="50%">Class name:&nbsp;<asp:DropDownList Width="200px" runat="server" ID="dlClass2" AutoPostBack="true"></asp:DropDownList></td>
                <td>Group name:&nbsp;<asp:DropDownList runat="server" Width="200px" ID="dlGroup"></asp:DropDownList></td>
            </tr>
		    <tr class="header1_table_first_row">
		        <td colspan="2" align="left">Online Quiz Duration
		        </td>
		    </tr>
		    <tr class="header1_tr_even_row">
		        <td align="left">
                    Start Time:
                    <input type="text" id="tb_StartTime" runat=server onclick="OpenCalender('tb_StartTime')" style="width: 84px" readonly="readOnly" title="Select a date from the calendar" unselectable="on"/>
                    <asp:DropDownList ID="ddl_StartHr" runat="server">
                        <asp:ListItem Selected="True">00</asp:ListItem>
                        <asp:ListItem>01</asp:ListItem>
                        <asp:ListItem>02</asp:ListItem>
                        <asp:ListItem>03</asp:ListItem>
                        <asp:ListItem>04</asp:ListItem>
                        <asp:ListItem>05</asp:ListItem>
                        <asp:ListItem>06</asp:ListItem>
                        <asp:ListItem>07</asp:ListItem>
                        <asp:ListItem>08</asp:ListItem>
                        <asp:ListItem>09</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>16</asp:ListItem>
                        <asp:ListItem>17</asp:ListItem>
                        <asp:ListItem>18</asp:ListItem>
                        <asp:ListItem>19</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>21</asp:ListItem>
                        <asp:ListItem>22</asp:ListItem>
                        <asp:ListItem>23</asp:ListItem>
                    </asp:DropDownList>&nbsp;Hour&nbsp;<asp:DropDownList ID="ddl_StartMin" runat="server">
                        <asp:ListItem Selected="True">00</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                        <asp:ListItem>40</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                    </asp:DropDownList>&nbsp;Minute</td><td align="left">
                    Finish Time:
                    <input type="text" id="tb_EndTime" runat=server onclick="OpenCalender('tb_EndTime')" style="width: 85px" readonly="readOnly" title="Select a date from the calendar" unselectable="on"/>
                        <asp:DropDownList ID="ddl_EndHr" runat="server">
                            <asp:ListItem>00</asp:ListItem>
                            <asp:ListItem>01</asp:ListItem>
                            <asp:ListItem>02</asp:ListItem>
                            <asp:ListItem>03</asp:ListItem>
                            <asp:ListItem>04</asp:ListItem>
                            <asp:ListItem>05</asp:ListItem>
                            <asp:ListItem>06</asp:ListItem>
                            <asp:ListItem>07</asp:ListItem>
                            <asp:ListItem>08</asp:ListItem>
                            <asp:ListItem>09</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                            <asp:ListItem>13</asp:ListItem>
                            <asp:ListItem>14</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>16</asp:ListItem>
                            <asp:ListItem>17</asp:ListItem>
                            <asp:ListItem>18</asp:ListItem>
                            <asp:ListItem>19</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>21</asp:ListItem>
                            <asp:ListItem>22</asp:ListItem>
                            <asp:ListItem Selected="True">23</asp:ListItem>
                        </asp:DropDownList>&nbsp;Hour&nbsp;<asp:DropDownList ID="ddl_EndMin" runat="server">
                            <asp:ListItem>00</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>40</asp:ListItem>
                            <asp:ListItem Selected="True">50</asp:ListItem>
                        </asp:DropDownList>&nbsp;Minute</td>
		    </tr>
		    <tr>
			    <td colspan="2" align="right" class="header1_tr_odd_row">
			        <input id="btnAssign" name="btnAssign" value="Finish" style="width:150px;" class="button_continue" runat="server" type="button" onclick="startAssign()" />
			        <input id="btnFinish" name="btnFinish" value="Finish" style="width:150px;display:none;" class="button_continue" runat="server" type="button" />
			    </td>
		    </tr>
        </table>
    </div>
    </form>
</body>
</html>
