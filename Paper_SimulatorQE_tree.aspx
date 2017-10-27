<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Paper_SimulatorQE_tree.aspx.cs" Inherits="AuthoringTool_CaseEditor_Paper_Paper_SimulatorQE_tree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Select Simulator Template</title>
      <script type="text/javascript" src="ECOTree.js"></script>
      <link type="text/css" rel="style-sheet" href="ECOTree.css" />
    <script src="../../../Scripts/jquery-1.11.2.min.js"></script>
    <xml:namespace ns="urn:schemas-microsoft-com:vml" prefix="v" />
    <style>
        v\:*
        {
            behavior: url(#default#VML);
        }
        .NodeName
        {
            font-family: "Verdana";
            font-size: 14px;
        }
        .NodeImage
        {
            width: 16px;
            height: 16px;
        }
    </style>
    <script language="javascript">
        $(document).ready(function () {
            var url = new URL(window.location.href);
            var Opener = url.searchParams.get("Opener");
            if (Opener == "Paper_QuestionTypeNew")
            {
                $('#btnBack').show();
            }

        });


        //When generating a new 圖片題 (if the Opener is Paper_QuestionTypeNew), show the go back button
        function goBack()
        {
            if (window.confirm("您的資料將不會被儲存，您確定要繼續嗎?")) {
                var url = new URL(window.location.href);
                var Opener = url.searchParams.get("Opener");
                var GroupID = url.searchParams.get("GroupID");
                if (Opener == "Paper_QuestionTypeNew") {
                    window.location.href = Opener + ".aspx?Opener=./QuestionGroupTree/QGroupTreeNew&bModify=False&GroupID=" + GroupID;
                }
            }
        }

     function CloseAndReload()
     {
       window.close();
       window.opener.location.reload();
     }
    </script>
</head>
<body>
   <form id="form1" runat="server">
    <div>
    <table id="body_content" style="width: 99%; height: 99%">
            <tr>
                <td>
                    <table style="width: 100%; height: 100%">
                        <tr style="height: 15px">
                            <td style="height: 15px">
                                <!--Section名稱-->
                                <table class="title" id="Table1" width="100%" border="0">
                                    <tr>
                                        <td>
                                            Step 1:請選擇圖形題的場景
                                        </td>
                                    </tr>
                                </table>
                                <hr />
                            </td>
                        </tr>
                        <tr style="height: 100%; width: 100%">
                            <td valign="top" style="height: 100%">
                                <!--內容開始-->
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr valign="top">
                                        <td width="25%">
                                            <table id="tb" width="300px" height="650px" cellpadding="10" rules="all" border="1"
                                                style="border: solid 1px grey; border-collapse: collapse;" class="header1_table"
                                                runat='server'>
                                                <tr class="header1_table_first_row">
                                                    <td align="center" style="height:10px">
                                                        <b>Simulator Menu</b>
                                                    </td>
                                                </tr>
                                                <tr class="header1_tr_even_row" valign="top">
                                                    <td valign="top" align="left" style="height:80%">
                                                        <asp:TreeView ID="tvCourseTreeMenu" runat="server" ImageSet="XPFileExplorer" NodeIndent="15"
                                                            ExpandDepth="1" OnSelectedNodeChanged="tTD_SelectedNodeChanged">
                                                            <RootNodeStyle CssClass="Parent" />
                                                            <NodeStyle NodeSpacing="5" Font-Names="Tahoma" HorizontalPadding="2px" VerticalPadding="2px" />
                                                            <ParentNodeStyle CssClass="Parent" Font-Bold="False" />
                                                            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                                            <SelectedNodeStyle Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px"
                                                                BackColor="#B5B5B5" />
                                                            <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="0px"
                                                                NodeSpacing="0px" VerticalPadding="0px" />
                                                        </asp:TreeView>
                                                        
                                                        <%--<asp:TreeView ID="tTD" runat="server" ExpandDepth="2" NodeIndent="10" ShowLines="true"
                                                            OnSelectedNodeChanged="tTD_SelectedNodeChanged">
                                                            <SelectedNodeStyle CssClass="ItemMouseOver" />
                                                            <RootNodeStyle ImageUrl="~/BasicForm/Image/diffFolder.gif" />
                                                            <Nodes>
                                                                <asp:TreeNode SelectAction="Expand" Text="Standard" Value="Standard"></asp:TreeNode>
                                                                <asp:TreeNode SelectAction="Expand" Text="Public" Value="Public"></asp:TreeNode>
                                                                <asp:TreeNode SelectAction="Expand" Text="Private" Value="Private"></asp:TreeNode>
                                                            </Nodes>
                                                            <NodeStyle ImageUrl="~/BasicForm/Image/folderclosed.gif" ForeColor="Black" />
                                                        </asp:TreeView>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="75%" align="center">
                                            <table id="templateTable" width="100%" cellpadding="10" border="1"
                                                style="border: solid 1px grey; border-collapse: collapse; height:100%" class="header1_table"
                                                runat="server">
                                                <tr>
                                                    <td align="center" style="height:650px; width:100%">
                                                       <iframe id="imyTreeContainer" runat="server" width="100%" height="100%"></iframe> 
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="height: 15px">
                            <td align="right">
                                <!--內容結尾-->
                                 <hr />
                               <input id="btnBack" runat="server" style="width: 150px; cursor: pointer; height: 30px;display: none;" onclick="goBack()"
                    type="button" value="<<   Back" name="btnBack" class="button_continue" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                                 <asp:Button ID="btn_cancel" runat="server" Text="返回題目列表" class="button_continue" Width="100px" 
                                    onclick="btn_cancel_Click" />&nbsp;&nbsp;<asp:HiddenField ID="hf_QID" runat="server" />
                                                                 <asp:Button ID="btn_select" runat="server" Text="下一步" class="button_continue"
                                    Width="100px"  onclick="btn_select_Click"  />
                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    
    <!--HiddenField-->
    <asp:HiddenField ID="hf_DMTID" runat="server" />
    <asp:HiddenField ID="hfPaperID" runat="server" />
    <asp:HiddenField ID="hfCaseID" runat="server" />
    <!--從server端取得參數值-->

    <script type="text/javascript">
	    var myTree  = new ECOTree('myTree','myTreeContainer');		
        var DesisionMakingTreeID="";  
	    if( document.getElementById('hf_DMTID') !=null && document.getElementById('hf_DMTID').value !="")
	       DesisionMakingTreeID=document.getElementById('hf_DMTID').value;						
    </script>

    <!--建構tree-->

    <script type="text/javascript">	
	function CreateTree() 
	{	
	    var RootNodeID = AuthoringTool_TableDriven_TDForQandA.GetNodeID("root",DesisionMakingTreeID).value;
        var RootNodeName = AuthoringTool_TableDriven_TDForQandA.GetNodeName("root",DesisionMakingTreeID).value;
        //建立tree node root
	    myTree.add(RootNodeID,-1,RootNodeName,"","","","","",RootNodeName);
	    //建立tree node children
	    GetChildNodeIDArray(RootNodeID); 
	    
	    myTree.UpdateTree();
    }
    
    function GetChildNodeIDArray(ParentNodeID)
    { 
        var ChildNodeID = AuthoringTool_TableDriven_TDForQandA.GetChildNodeID(ParentNodeID,DesisionMakingTreeID).value;
        var ChildNodeName = AuthoringTool_TableDriven_TDForQandA.GetChildNodeName(ParentNodeID,DesisionMakingTreeID).value;
        var ChildNodeCondition = AuthoringTool_TableDriven_TDForQandA.GetChildNodeCondition(ParentNodeID,DesisionMakingTreeID).value;
        if(ChildNodeID!="False")
        {
            var ChildNodeIDArray = ChildNodeID.split("$");
            var ChildNodeNameArray = ChildNodeName.split("$");
            var ChildNodeConditionArray = ChildNodeCondition.split("$");     
                 
            for(var i=0;i<ChildNodeIDArray.length;i++)
            {           
                 myTree.add(ChildNodeIDArray[i],ParentNodeID,ChildNodeNameArray[i] + "$" + ChildNodeConditionArray[i],"","","","","",ChildNodeNameArray[i]);
                 GetChildNodeIDArray(ChildNodeIDArray[i]);			    
            }   
        }         
    }	
    </script>
    </form>
</body>
</html>
