<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectTask.aspx.cs" Inherits="SelectTask" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta name="author" content="iWeb Crawler UI" />
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
	<link rel="stylesheet" href="images/style.css" type="text/css" />
	<title>iWeb Crawler Control Page</title>

    <script language="javascript" type="text/javascript">
// <!CDATA[


        function Select_onclick() {

        }

        function Button1_onclick() {
            Button1_action();
        }

// ]]>
    </script>
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
				<div align="right" style="padding:12px;" class="smallwhitetext"><a href="Default.aspx">
                    Main Control</a></div>
			</div>
		</div>
		<div id="header" align="center">
			<div class="titletext" id="logo">
				<div class="logotext" style="margin:30px"><span class="orangelogotext">i</span>Crawler</div> 
			</div>
			<div id="pagetitle">
				<div id="title" class="titletext" align="right">Select a Task</div>
			</div>
		</div>
		<div id="content" align="center">
			<div id="menu" align="right">
				<div align="right" style="width:189px; height:8px;"><img src="images/mnu_topshadow.gif" width="189" height="8" alt="mnutopshadow" /></div>
				<div id="linksmenu" align="center">
					<a href="Default.aspx" title="Main Control">Main Control</a>
					<a href="CreateTask.aspx" title="Create a new Task">Create Task</a>
					<a href="SelectTask.aspx?target=edit" title="Edit Task Proporties">Edit Task</a>
					<a href="SelectTask.aspx?target=options" title="Crawler Options">Crawler Options</a>
					<a href="SelectTask.aspx?target=invoke" title="Start/Stop Task">Start/Stop Task</a>
					<a href="SelectTask.aspx?target=results" title="View Crawler Results">View 
                    Results</a>
				</div>
				<div align="right" style="width:189px; height:8px;"><img src="images/mnu_bottomshadow.gif" width="189" height="8" alt="mnubottomshadow" /></div>
			</div>
		<div id="contenttext" style="height:120px">
			<div class="bodytext" style="padding:12px;" align="center">
				<strong style="text-align: center">Please Select the task : </strong>
                <asp:DropDownList ID="DropDownList1" runat="server" 
                    onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;<asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                    Text="Select" />
                <br />
				
			</div>
			<img src="images/loading.gif" style=" margin-top:-20px;" />
			</div>
			
		</div>
		<div id="footer" class="smallgraytext" align="center">
		<img src="images/crawler.jpg" /><br />
<a href="Default.aspx">Main Control Page</a> | <a href="CreateTask.aspx">Create Task</a> | <a href="SelectTask.aspx?target=edit">Edit Task Proporties</a> | <a href="SelectTask.aspx?target=invoke">Start/Stop Task</a> | <a href="SelectTask.aspx?target=results">View Results</a><br />
			UI Version: <asp:Label ID="LabelUIVersion" runat="server" Text="Label"></asp:Label>
            , Core Version: <asp:Label ID="LabelEngineVersion" runat="server" Text="Label"></asp:Label><br />
			iWeb Crawler Development Team © 2010<br />
				
			
		</div>
	</div>
    </form>
</body>
</html>

