<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvokeTask.aspx.cs" Inherits="InvokeTask" %>

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
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 130px;
        }
        .style4
        {
            width: 130px;
            height: 16px;
            font-size: small;
        }
        .style5
        {
            width: 130px;
            height: 16px;
        }
        .style6
        {
            height: 16px;
        }
        .style7
        {
            width: 125px;
        }
        .style8
        {
            width: 125px;
            height: 16px;
            font-size: small;
        }
    </style>
</head>
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
				<div id="title" class="titletext" align="right">Invoke a task</div>
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
			<div class="bodytext" style="padding:12px;" align="justify">
				<strong style="font-size: small">Please specify the requested operation</strong><br />
				
			</div>
			<div class="panel" style="height:100px; text-align: center;" align="justify">
				<span class="bodytext"><br />
					<table class="style1">
                        <tr>
                            <td class="style7">
				<span class="bodytext">Task Name:
                            </td>
                            <td class="style2">
                                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td class="style2">
                                Status:</td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style8">
                                Working Time:</td>
                            <td class="style5">
                                <asp:Label ID="Label4" runat="server" Text="Label" style="font-size: small"></asp:Label>
                            </td>
                            <td class="style4">
                                Results:</td>
                            <td class="style6">
                                <asp:Label ID="Label5" runat="server" Text="Label" style="font-size: small"></asp:Label>
                            </td>
                        </tr>
                </table>
                <br />
                <asp:Button ID="Button2" runat="server" Text="Mark as Ready" 
                    onclick="Button2_Click" />
&nbsp;<asp:Button ID="Button1" runat="server" Text="Suspend" onclick="Button1_Click" />
                <br />
                <br />
            </div><br />
					
			<div class="panel" style="height:140px;" align="justify">
				<span class="orangetitle">Important Notice ..</span>
				<span class="bodytext"><br />
				This Operation will insert\remove your task to the waiting list, which may contain another tasks and thus your task may not invoked first, morever it won't change the active task status, you may need to suspend the active task if you have one; The system indices that you have 
                    <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label> current active tasks and <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label> waiting tasks. In more details:<br />
                    Active Tasks: <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label><br />
                    Waiting Tasks: <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label><br />     
                        </span>			
			</div>	
			</div>
			
		</div>
		<div id="footer" class="smallgraytext" align="center">
		<img src="images/crawler.jpg" /><br />
<a href="Default.aspx">Main Control Page</a> | <a href="CreateTask.aspx">Create Task</a> | <a href="SelectTask.aspx?target=edit">Edit Task Proporties</a> | <a href="SelectTask.aspx?target=invoke">Start/Stop Task</a> | <a href="SelectTask.aspx?target=results">View Results</a><br />
			UI Version: <asp:Label ID="LabelUIVersion" runat="server" Text="Label"></asp:Label>, Core Version: <asp:Label ID="LabelEngineVersion" runat="server" Text="Label"></asp:Label><br />
			iWeb Crawler Development Team &copy; 2010<br />
				
			
		</div>
	</div>
    </form>
</body>
</html>

