<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Paper_SimulatorQuestionEditor.aspx.cs" Inherits="AuthoringTool_CaseEditor_Paper_Paper_SimulatorQuestionEditor" %>

<%--<%@ Register Assembly="System.Web.Silverlight" Namespace="System.Web.UI.SilverlightControls" TagPrefix="asp" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Simulator Question Editor</title>
     <script type="text/javascript">
//        function onSilverlightError(sender, args) {
//            var appSource = "";
//            if (sender != null && sender != 0) {
//                appSource = sender.getHost().Source;
//            }

//            var errorType = args.ErrorType;
//            var iErrorCode = args.ErrorCode;

//            if (errorType == "ImageError" || errorType == "MediaError") {
//                return;
//            }

//            var errMsg = "Unhandled Error in Silverlight Application " + appSource + "\n";

//            errMsg += "程式碼: " + iErrorCode + "    \n";
//            errMsg += "分類: " + errorType + "       \n";
//            errMsg += "訊息: " + args.ErrorMessage + "     \n";

//            if (errorType == "ParserError") {
//                errMsg += "File: " + args.xamlFile + "     \n";
//                errMsg += "Line: " + args.lineNumber + "     \n";
//                errMsg += "Position: " + args.charPosition + "     \n";
//            }
//            else if (errorType == "RuntimeError") {
//                if (args.lineNumber != 0) {
//                    errMsg += "Line: " + args.lineNumber + "     \n";
//                    errMsg += "Position: " + args.charPosition + "     \n";
//                }
//                errMsg += "MethodName: " + args.methodName + "     \n";
//            }

//            throw new Error(errMsg);
//        }

//        function sendSizeTo() {
//            var control = document.getElementById("Silverlight1");
//            getClientWidth();
//            getClientHeight();
//            control.width = document.body.clientWidth - 5;
//            control.height = document.body.clientHeight - 30;
//            control.content.Page.ChangeWindowSizeText(document.body.clientWidth + "_" + document.body.clientHeight);

//        }
//        function updateSilverlightWindowSize() {
//            getClientWidth();
//            getClientHeight();
//            window.onresize = sendSizeTo;
//            window.onscroll = sendSizeTo;
//        }

//        function changeWindowSizeFromSilverlight() {
//            var control = document.getElementById("Silverlight1");
//            getClientWidth();
//            getClientHeight();
//            control.width = document.body.clientWidth - 5;
//            control.height = document.body.clientHeight - 30;
//            control.content.Page.ChangeWindowSizeText(document.body.clientWidth + "_" + document.body.clientHeight);
//        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods ="true"/>
    <div >
        <asp:Label ID="Label2" runat="server" Text="圖形題場景預覽" Font-Size="18pt"></asp:Label><br/>

        <asp:HiddenField ID="hf_QID" runat="server" />
    </div>  
    <div align="center">    <asp:Panel ID="PL_table" runat="server">
    </asp:Panel>
    </div> 
    <%--<table id="body_content" style="WIDTH: 99%;height: 99%;" align="center">--%>
<%--    	<tr style="width: 99%; height: 100%;" >

                            <td id="tdMainTable" align="center" runat="server" style="height: 100%">
                            
                            <asp:Silverlight ID="Silverlight1" runat="server" style="display:none"></asp:Silverlight>
                            <span id="txtClientWidth" runat ="server" style="visibility:hidden;" ></span>
                            <span id="txtClientHeight" runat="server" style="visibility:hidden;" ></span>
                            </td>
   
				</tr>--%>
   <%-- </table>--%>
    		<asp:HiddenField ID="Hidden_CaseID" runat="server" /> 
            <asp:HiddenField ID="Hidden_ClinicNum" runat="server" /> 
            <asp:HiddenField ID="Hidden_SectionName" runat="server" /> 
            <asp:HiddenField ID="Hidden_UserID" runat="server" />             
            <asp:HiddenField ID="Hidden_EditMode" runat="server" /> 
           <%-- <script type ="text/javascript" >
                function getClientWidth() {
                    var txtClientWidth = document.getElementById("txtClientWidth");
                    txtClientWidth.innerHTML = "" + document.body.clientWidth;
                }
                function getClientHeight() {
                    var txtClientHeight = document.getElementById("txtClientHeight");
                    txtClientHeight.innerHTML = "" + document.body.clientHeight;
                }

                getClientWidth();
                getClientHeight();
        </script>--%>
    </form>
</body>
</html>
