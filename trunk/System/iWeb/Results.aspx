<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Results.aspx.cs" Inherits="Results" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta name="author" content="iWeb Crawler UI" />
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
	<link rel="stylesheet" href="images/style.css" type="text/css" />
	<title>iWeb Crawler Control Page</title>

    <script language="javascript" type="text/javascript">
// <!CDATA[

        function Button1_onclick() {
            
        }

// ]]>
    </script>
    <style type="text/css">
        .style2
        {
            font-size: 11px;
            font-weight: bold;
            color: #CCCCCC;
            font-style: normal;
            font-variant: normal;
            line-height: normal;
            font-family: Tahoma, sans-serif;
            text-align: left;
        }
        .style3
        {
            width: 100%;
        }
        .style4
        {
            width: 140px;
        }
        .style5
        {
            width: 120px;
        }
        .style6
        {
            text-align: right;
            height: 29px;
        }
        .style7
        {
            padding: 12px;
            border: solid 1px #E4E4E4;
            background-color: #EEEEEE;
            margin: 10px;
            padding: 10px;
            width: 550px;
            height: 160px;
            text-align: right;
        }
        .style9
        {
            width: 75px;
        }
        .style10
        {
            width: 40px;
        }
        .style11
        {
            width: 60px;
        }
        .style13
        {
            width: 70px;
        }
        #form1
        {
            text-align: left;
        }
        </style>
</head>
<script type="text/javascript">
    function showContent(vThis) {
        vParent = vThis.parentNode;
        vSibling = vParent.nextSibling;
        while (vSibling.nodeType == 3) { // Fix for Mozilla/FireFox Empty Space becomes a TextNode or Something
            vSibling = vSibling.nextSibling;
        };
        if (vSibling.style.display == "none") {
            vThis.src = "images/collapse.bmp";
            vThis.alt = "Hide Div";
            vSibling.style.display = "block";
        } else {
            vSibling.style.display = "none";
            vThis.src = "images/expand.bmp";
            vThis.alt = "Show Div";
        }
        return;
    }
</script>
<body>
	<form id="form1" runat="server">
	<div id="page" align="center">
		<div id="toppage" align="center">
			<div id="date">
				<div class="smalltext" style="padding:13px;"><strong>
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></strong></div>
			</div>
			<div id="topbar">
				<div align="right" style="padding:12px;" class="smallwhitetext"><a href="Default.aspx">Main Control</a></div>
			</div>
		</div>
		<div id="header" align="center">
			<div class="titletext" id="logo">
				<div class="logotext" style="margin:30px"><span class="orangelogotext">i</span>Crawler</div> 
			</div>
			<div id="pagetitle">
				<div id="title" class="titletext" align="right">View Results</div>
			</div>
		</div>
		<div id="content" align="center">
			<div id="menu" align="right">
				<div align="right" style="width:189px; height:8px;"><img src="images/mnu_topshadow.gif" width="189" height="8" alt="mnutopshadow" /></div>
				<div id="linksmenu" align="center">
					<a href="Default.aspx" title="Main Control">Main Control</a>
					<a href="CreateTask.aspx" title="Create a new Task">Create Task</a>
					<a href="SelectTask.aspx?target=edit" title="Edit Task Proporties">Edit Task</a>
					<a href="SelectTask.aspx?target=invoke" title="Start/Stop Task">Start/Stop Task</a>
					<a href="SelectTask.aspx?target=results" title="View Crawler Results">View Results</a>
				</div>
				<div align="right" style="width:189px; height:8px;"><img src="images/mnu_bottomshadow.gif" width="189" height="8" alt="mnubottomshadow" /></div>
			</div>
		<div id="contenttext">
			
			<h6 class="style2" 
                style="margin-left:1px; margin-right:1px; margin-top:5px; margin-bottom:1px;"><img src="images/expand.bmp" alt="Show Div" border="0" style="margin-left:10px; margin-right:1px; margin-top:0px; margin-bottom:0px; cursor:pointer;" onclick="showContent(this);" />
                <span class="bodytext">Display Settings</span></h6>
				
			
			<div class="style7" style="height:75px; " align="justify">
				<span class="bodytext">
					 <table class="style3">
                         <tr>
                             <td class="style4">
                                 Results per page:</td>
                             <td class="style5">
                                 <asp:DropDownList ID="DropDownList1" runat="server" 
                                     onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                                     <asp:ListItem>5</asp:ListItem>
                                     <asp:ListItem Selected="True">10</asp:ListItem>
                                     <asp:ListItem>15</asp:ListItem>
                                     <asp:ListItem>20</asp:ListItem>
                                     <asp:ListItem>25</asp:ListItem>
                                 </asp:DropDownList>
                             </td>
                             <td class="style4">
				<span class="bodytext">
                                 Allow Remove:</td>
                             <td>
                                 <asp:CheckBox ID="CheckBox1" runat="server" />
                             </td>
                         </tr>
                         <tr>
                             <td class="style4">
                                 Order By:</td>
                             <td class="style5">
                                 <asp:DropDownList ID="DropDownList2" runat="server" 
                                     onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                                     <asp:ListItem Selected="True">Normal</asp:ListItem>
                                     <asp:ListItem>Rank - Ascending</asp:ListItem>
                                     <asp:ListItem>Rank - Decesinding</asp:ListItem>
                                     <asp:ListItem>Trust - Ascending</asp:ListItem>
                                     <asp:ListItem>Trust - Decesinding</asp:ListItem>
                                 </asp:DropDownList>
                             </td>
                             <td class="style4">
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                         </tr>
                </table>
                <div class="style6">
                    <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Apply" />
                <br />
                <br />
                <br />
                </div>
            </div>
				<div class="style7" style="height:20px; " align="justify">
				    <table class="style3" style="margin-tp:-15px">
                        <tr>
                            <td class="style13" style="text-align: center">
                                Task Name:</td>
                            <td class="style9" style="text-align: center">
                                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td class="style10" style="text-align: center">
                                Total:</td>
                            <td class="style11" style="text-align: center">
                                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td class="style11" style="text-align: center">
                                Category:</td>
                            <td>
                                <asp:DropDownList ID="DropDownList3" runat="server" Height="20px" 
                                    style="text-align: center" Width="140px">
                                </asp:DropDownList>
&nbsp;<asp:Button ID="Button2" runat="server" Text="View" />
                            </td>
                        </tr>
                    </table>
				</div>
				<div style="margin-left:20px; text-align: left" align="justify">
				Go to page: 
            <asp:DropDownList ID="DropDownList4" runat="server">
            </asp:DropDownList>
            &nbsp;<asp:Button ID="Button1" runat="server" Text="go" /></div>	
				
				&nbsp;<% =drawEntries()%>

			</div>
			
		</div>
		<div id="footer" class="smallgraytext" align="center">
		<img src="images/crawler.jpg" /><br />
<a href="Default.aspx">Main Control Page</a> | <a href="About.aspx">About iWeb Crawler</a> | <a href="CreateTask.aspx">Create Task</a> | <a href="SelectTask.aspx?target=edit">Edit Task Proporties</a> | <a href="SelectTask.aspx?target=invoke">Start/Stop Task</a><br />
			UI Version: <asp:Label ID="LabelUIVersion" runat="server" Text="Label"></asp:Label>, Engine Version: <asp:Label ID="LabelEngineVersion" runat="server" Text="Label"></asp:Label><br />
			iWeb Crawler Development Team &copy; 2010<br />
				
			
		</div>
	</div>
    </form>
</body>
</html>

