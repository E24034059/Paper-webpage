﻿<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="setLearningPoint.aspx.cs" Inherits="AuthoringTool_CaseEditor_Paper_setLearningPoint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Set Learning Point</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!-- Bootstrap -->
    <link href="~/bootstrap/css/bootstrap.min.css" rel="stylesheet" media="screen">
    <script src="http://code.jquery.com/jquery.js"></script>
    <script src="~/bootstrap/js/bootstrap.min.js"></script>
    
    <!--jQuery draggable-Revert position -->
    
  <script src="//code.jquery.com/jquery-1.10.2.js"></script>
  <script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
  <link rel="stylesheet" href="/resources/demos/style.css">
  
  <script>
  $(function() {
    $( "#divAdd" ).draggable({ revert: true });
    $( "#divModify" ).draggable({ revert: true });
    $( "#divEditLearningPoint" ).draggable({ revert: true });
  });


  </script>
  
    <script type="text/javascript">
     var currentNodeID = "";
     var oldNodeID = "";
     var currentNodeName="";
    
     function showVMWindow() {
         //newwin = window.open("../AuthoringTool/CaseEditor/VirtualMicroscope/VMSimulatorAuthoring.aspx", "full", "fullscreen=yes,resizable=yes");
         //window.open('', '_self', '');
         window.close();
     }

    function confirm_delete() {
            if (!window.confirm("Do you want to delete the Node ?")) {
                event.returnValue = false;
            }
            else {
                //oldNodeID = "";
            }
        }
     function showDisplay(id) 
     {
        document.getElementById(id).style.display = "";
     }
     
     function hideDisplay(id) 
     {
         document.getElementById(id).style.display = "none";
     }
     
   function divAddjsButton()
   {
       __doPostBack('btCancel','');
   }
   
   function divModifyjsButton()
   {
       __doPostBack('Button1','');
   }
   function divEditLearningPointjsButton()
   {
       __doPostBack('Button3','');
   }
    </script>
    
    <style type="text/css">
        #Button1
        {
            width: 30%;
        }
        #Button2
        {
            width: 30%;
            height: 30px;
        }
        #Button3
        {
            width: 30%;
            height: 30px;
        }
        .auto-style1
        {
            height: 800px;
        }
    </style>
    
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
            <div style="display: none;"> 
            
            <input type="hidden" id="Hidden1" runat="server" value="" />
            <script type="text/javascript">
                var message = "";
                function selected_changed() {
                    //alert(""+document.getElementById("_ctl0:MainDisplay:ddlProvider").value);
                    message = currentNodeID;
                   
                }
                
                function show_callback(msg, context) {
                    //document.getElementById("_ctl0:MainDisplay:tAuthor").value = msg;
                    //alert(msg);
                    document.getElementById("tdParentNodeName").innerText = msg;
                    document.getElementById("tNodeName").value = msg;
                }



            </script>             
             <input type="hidden" id="strCurrentNodeID" runat="server" value="" />
             
            </div>
    <div>
      
      
      <table id="body_content" width="100%" align="center" style="font-size: 14px" >
        <tr>
          <td colspan="2">
             <table width="100%" border="0" cellpadding="0" cellspacing="0">
                     <tr>
                        <td class="title"><%=strTitle1%></td>   
                         <td style="text-align: right">
                             <asp:DropDownList ID="ddl_MutiLanguage" runat="server" AutoPostBack="True" Font-Size="14px" OnSelectedIndexChanged="ddl_MutiLanguage_SelectedIndexChanged">
            <asp:ListItem Value="en-US">en-US</asp:ListItem>
            <asp:ListItem Value="zh-TW">zh-TW</asp:ListItem>         
        </asp:DropDownList>
                         </td>                 
                     </tr>
          </table>
          </td>
        </tr>
          
        <tr>
          <td style="COLOR: red" colspan="2" valign="top">
			&nbsp;&nbsp;<%=strHint1%>
			<br>
			&nbsp;&nbsp;<%=strHint2%>
			<br>
			&nbsp;&nbsp;<%=strHint3%>
			<br>
              &nbsp;&nbsp;<%=strHint4%>
		  <hr />
          </td>  
        </tr>
        <tr>
          <td Width="25%" align="center" >
                    <asp:Button ID="btAdd" runat="server" Text="Add"  width="30%" Height=30px
                        class="button_continue" onclick="btAdd_Click"/>
                    <asp:Button ID="btModify" runat="server" Text="Modify"  width="30%" Height=30px
                        class="button_continue" onclick="btModify_Click"/>
                    <asp:Button ID="btDelet" runat="server" Text="Delete"  width="30%" Height=30px
                        class="button_continue" OnClick="btDelet_Click"/>
                    
              
             </td>
        </tr>
        <tr valign="top" width="100%">
          <td width="25%" class="auto-style1">
              <asp:Panel ID="Panel1" runat="server" Width="550px" Height="850px" ScrollBars="Both">
                  <table id="Table1"  runat='server'  rules="all" border="1" style="border-collapse:collapse; height: 600px;" class="header1_table">
              <tr class="header1_table_first_row" >
                <td align="center" height = "10px">
               
                        <asp:Label ID="lbLearningPoint" runat="server" Text="Learning Point Tree" Font-Size="X-Large"></asp:Label>
                        
                    
                </td>
              </tr>
             
              
              <tr class="header1_tr_even_row" valign="top>
                <td class="style1" valign="top" width="25%">
                
                <asp:TextBox ID="tbcKeyWord"  runat="server" AutoPostBack="True" Width="85%"  OnTextChanged = "tbcKeyWord_TextChanged"></asp:TextBox>
                <asp:ImageButton ID="imbtSearch" runat="server"  OnClick="imbtSearch_Click" 
                        ImageUrl="~/AuthoringTool/CaseEditor/Paper/images/led-icons/magnifier.png"/>
                   
                   
                    <asp:TreeView ID="tvLearningPointTree" runat="server" Font-Size="16pt" 
                        ImageSet="simple" ShowExpandCollapse="true" 
                        onselectednodechanged="tvLearningPointTree_SelectedNodeChanged">
                    <SelectedNodeStyle BackColor="#99FFCC" />
                    <HoverNodeStyle BackColor="LightPink" />
                    </asp:TreeView>
                    
                    
                  
                </td>
              </tr>
            </table>
              </asp:Panel>
            
          </td>
          
          <td width="75%" valign="top" align="center" rowspan="3" class="auto-style1">
             
              <asp:Panel ID="panStep" runat="server" ScrollBars="Auto" Font-Size="Larger" Width = "90%" Height="800px">
                  
              </asp:Panel>
              
              
            </td>
        </tr>
          <td align="right" colspan="2">
                    <asp:Button ID="btCancelEdit"  OnClick="btCancelEdit_Click" runat="server" class="button_continue" 
                         style="width: 150px; cursor: hand; height: 30px" 
                        Text="Cancel" />&nbsp;
                    <asp:Button ID="btnFinishEdit" OnClick="btFinishEdit_Click" runat="server" class="button_continue" 
                         style="width: 150px;  height: 30px" 
                        Text="Finish" />
                </td>
      </table>
      
    </div>
    
    <!--Add-->
    <div id="divAdd" class="modal-content"  
            style="background-color: #FFFFFF;  display: none;  position: absolute; z-index: 108; left:50%; top:50%; margin-left: -200px; margin-top: -150px; " 
            runat="server">
       <div class="modal-content">
     
          <div class="modal-header">
            <button type="button" class="close" onclick="divAddjsButton();" >&times;</button>
            <h4 class="modal-title" id="H1" >Add Learning Point Node</h4>
          </div>
      
          <div class="modal-body">
             <table  class="table table-hover">
      <tr>
       <td id = "tdNodeID">
            <asp:Label ID="Label4" runat="server" Text="Parent Node :" Font-Size="Large"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lbSelectNode" runat="server" Text="Label" Font-Size="Large"></asp:Label>
        </td>
      </tr>
      <tr >
        <td>
          <asp:Label ID="Label3" runat="server" Text="New Node :" Font-Size="Large"></asp:Label>
          
        </td>
        <td>
        <asp:TextBox ID="tbxNewPoint" runat="server" 
                ForeColor="Black" Font-Size="Large"></asp:TextBox>
        </td>
      </tr>
     </table>
          </div>
          
          <div class="modal-footer">
            <asp:Button ID="btAddSubmit" runat="server" Text="Submit"  class="button_continue"  OnClick="btAddSubmit_Click" width=70px Height=30px/>
          <asp:Button ID="btCancel" runat="server" Text="Cancel"  class="button_continue"  OnClick="btCancel_Click" width=70px Height=30px/>
          </div>
    
       </div>
    </div>
    
    
    <div id="x" style="background-color: #FFFFFF;  display: none;  position: absolute; z-index: 102; left:50%; top:50%; margin-left: -200px; margin-top: -150px;" runat="server" >
    
      <asp:HiddenField ID="HiddenSelectRtb" runat="server" />
      <asp:HiddenField ID="HiddenSelectNodeValue" runat="server" />
      <asp:HiddenField ID="HiddenSelectNodeText" runat="server" />
      <asp:HiddenField ID="HiddenSituation" runat="server" Value = "Edit"/>
      <asp:HiddenField ID="HiddenSelectLearningPointValue" runat="server"/>
      <asp:HiddenField ID="HiddenWeightChange" runat="server" />
    </div>
    
     <!--Modify-->
     
     <div id="divModify" class="modal-content" style="background-color: #FFFFFF;  display: none;  position: absolute; z-index: 108; left:50%; top:50%;  margin-left: -200px; margin-top: -150px; " 
            runat="server">
       <div class="modal-content">
     
          <div class="modal-header">
            <button type="button" class="close" onclick="divModifyjsButton();" >&times;</button>
            <h4 class="modal-title" id="H2" >Modify Learning Point Node</h4>
          </div>
      
          <div class="modal-body">
            <table class="table table-hover"> 
              <tr >
        <td>
          <asp:Label ID="Label8" runat="server" Text="Node Content:" Font-Size="Large"></asp:Label>
          
        </td>
        <td>
        <asp:TextBox ID="tbxModifyNode" runat="server"
                ForeColor="Black" Font-Size="Large"></asp:TextBox>
        </td>
      </tr>
     </table>  
          </div>
          
          <div class="modal-footer">
           <asp:Button ID="Button4" runat="server" Text="Submit"  class="button_continue"  OnClick="btModifySubmit_Click" width=70px Height=30px/>
           <asp:Button ID="Button1" runat="server" Text="Cancel"  class="button_continue"  OnClick="btCancel_Click" width=70px Height=30px/>
          </div>
    
       </div>
    </div>
    
   
  <!--EditLearningPoint-->
  <div id="divEditLearningPoint" class="modal-content" style="background-color: #FFFFFF;  display: none;  position: absolute; z-index: 108; left:50%; top:50%; margin-left: -200px; margin-top: -150px; " 
            runat="server">
       <div class="modal-content">
     
          <div class="modal-header">
            <button type="button" class="close" onclick="divEditLearningPointjsButton();" >&times;</button>
            <h4 class="modal-title" id="H3" >Edit Learning Point</h4>
          </div>
      
          <div class="modal-body">
            <table class="table table-hover"> 
                    <tr>
        <td id = "td2">
            <asp:Label ID="Label9" runat="server" Text="Category:" Font-Size="Large"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lbCategory" runat="server" Text="Label" Font-Size="Large"></asp:Label>
        </td>
      </tr>
      <tr>
        <td>
          <asp:Label ID="Label11" runat="server" Text="Node Content:" Font-Size="Large"></asp:Label>  
        </td>
        <td>
        <asp:TextBox ID="tbxEditLearningPoint" runat="server"
                ForeColor="Black" Font-Size="Large"></asp:TextBox>
        </td>
      </tr>
     </table>  
          </div>
          
          <div class="modal-footer">
           <asp:Button ID="btEdit" runat="server" Text="Submit"  class="button_continue" OnClick="btEditLearningPointSubmit_Click" width=70px Height=30px/>
          <asp:Button ID="Button3" runat="server" Text="Cancel"  class="button_continue"  OnClick="btCancel_Click" width=70px Height=30px/>
          </div>
    
       </div>
    </div> 
   
   
    </form>
    </body>
</html>
