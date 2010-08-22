<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta name="author" content="iWeb Crawler UI" />
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
	<link rel="stylesheet" href="images/style.css" type="text/css" />
	<title>iWeb Crawler Control Page</title>
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
				<div id="title" class="titletext" align="right">Welcome to iWeb Crawler!</div>
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
				<strong>Hi! This is iWeb Crawler Main Control Page, this is the first working version of iWeb Crawler created mainly for testing and debuging</strong><br />
				
			</div>
			<div class="panel" style="height:90px;" align="justify">
				<span class="orangetitle">How To Start ..</span>
				<span class="bodytext"><br />
				In order to invoke a specified task, first you need to create the task with unique name and you may inserted the task proporties like categories and thier keywords - you can change the proporties later via edit mode - after this you may send the task to be procced in the crawler, you may look at the results in the results section</span>			
			</div>
			<div class="panel" style="height:75px;" align="justify">
				<span class="orangetitle">Credits</span>
				<span class="bodytext"><br />
				This Crawler has been developped as Software Crawler Project, at the Technion EE faculty<br />
				by: Mohammed Agabaria, Adam Shobash</span>			
			</div>
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