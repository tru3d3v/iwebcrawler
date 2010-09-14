<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditTask.aspx.cs" Inherits="EditTask" %>

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
            width: 147px;
        }
        .style4
        {
            color: #666666;
            font-style: normal;
            font-variant: normal;
            font-size: 0.7em;
            line-height: normal;
            font-family: Tahoma, sans-serif;
        }
        .style7
        {
            font-size: 11px;
            color: #666666;
            font-style: normal;
            font-variant: normal;
            font-weight: bold;
            line-height: normal;
            font-family: Tahoma, sans-serif;
        }
        .style8
        {
            width: 266px;
        }
    </style>
</head>
<body>
	<form id="form1" runat="server">
	<div id="page" align="center">
		<div id="toppage" align="center">
			<div id="date">
				<div class="smalltext" style="padding:13px;"><strong>
                    <asp:Label ID="Label1" runat="server" Text="Edit Task"></asp:Label></strong></div>
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
				<div id="title" class="titletext" align="right">Edit task</div>
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
		<div id="contenttext">
			<div class="style7" style="padding:12px;" align="justify">
				Task Basic Details<br />
				
			</div>
			<div class="panel" style="height:60px; text-align: center;">
				<table class="style1">
                    <tr>
                        <td class="style2">
				<span class="bodytext">Task Name:</td>
                        <td class="style2">
                            <asp:Label ID="Label2" runat="server" Text="TaskNameLabel"></asp:Label>
                        </td>
                        <td class="style2">
                            <span class="bodytext">Url Parameters:</td>
                        <td>
                            <asp:DropDownList ID="DropDownList1" runat="server" 
                                onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                                <asp:ListItem Selected="True">False</asp:ListItem>
                                <asp:ListItem>True</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <span class="bodytext">Link Depth:</td>
                        <td class="style2" nowrap="nowrap">
                            <asp:TextBox ID="TextBox1" runat="server" Width="56px"></asp:TextBox>
                        </td>
                        <td class="style2">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="TextBox1" Display="Dynamic" 
                    ErrorMessage="You must insert positive number only !" 
                    ValidationExpression="\d{1,5}"></asp:RegularExpressionValidator>
            </div>
                <span class="style4">
            <div class="style7" style="padding:12px;" align="justify">
				Seeds List<br />
				
			</div>
            </span>
                <span class="bodytext">
            <div class="panel" style="height:85px; text-align: center;">
                <table class="style1">
                    <tr>
                        <td class="style8">
                            http://<asp:TextBox ID="TextBox2" runat="server" Width="220px"></asp:TextBox>
                        </td>
                        <td rowspan="2">
                            <asp:ListBox ID="ListBox1" runat="server" Width="225px" Height="80px"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                            <asp:Button ID="Button1" runat="server" Text="Add To Seed" 
                                onclick="Button1_Click" />
                        &nbsp;<asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
                                Text="Remove Selected" />
                        </td>
                    </tr>
                </table>
            </div>
                        <div class="style7" style="padding:12px;" align="justify">
				Allowed Networks<br />
				
			</div>
            <div class="panel" style="height:85px; text-align: center;">
                <table class="style1">
                    <tr>
                        <td class="style8">
                            <asp:TextBox ID="TextBox3" runat="server" Width="220px"></asp:TextBox>
                        </td>
                        <td rowspan="2">
                            <asp:ListBox ID="ListBox2" runat="server" Width="225px" Height="80px"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                            <asp:Button ID="Button7" runat="server" onclick="Button7_Click" Text="Add" />
&nbsp;<asp:Button ID="Button8" runat="server" onclick="Button8_Click" Text="Remove Selected" />
                        </td>
                    </tr>
                </table>
            </div>
                        <div class="style7" style="padding:12px;" align="justify">
				Restricted Networks<br />
				
			</div>
            <div class="panel" style="height:85px; text-align: center;">
                <table class="style1">
                    <tr>
                        <td class="style8">
                            <asp:TextBox ID="TextBox4" runat="server" Width="220px"></asp:TextBox>
                        </td>
                        <td rowspan="2">
                            <asp:ListBox ID="ListBox3" runat="server" Width="225px" Height="80px"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                            <asp:Button ID="Button9" runat="server" onclick="Button9_Click" Text="Add" />
&nbsp;<asp:Button ID="Button10" runat="server" onclick="Button10_Click" Text="Remove Selected" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                ControlToValidate="TextBox2" Display="Dynamic" 
                ErrorMessage="You must insert a legal seed url !!" 
                ValidationExpression="([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></asp:RegularExpressionValidator>
            <div class="style7" style="padding:12px;" align="justify">
				Actions<br />
				
			</div>
			<div style="text-align: center">
			
			    <asActions<br />
				
			</div>
			<div style="text-align: center">
			
			    <asp:Button ID="Button11" runat="server" Text="Restore Saved Settings" 
                    onclick="Button11_Click" />
                &nbsp;<asp:Button ID="Button12" runat="server" Text="Save Current Settings" 
                    onclick="Button12_Click" />
&nbsp;<asp:Button ID="Button14" runat="server" onclick="Button14_Click" 
                    Text="Category Manager" />
                &nbsp;<asp:Button ID="Button13" runat="server" Text="Delete Task" onclick="Button13_Click" />
			
			</div>
					
				
			</div>
			
		</div>
		<div id="footer" class="smallgraytext" align="center">
		<img src="images/crawler.jpg" /><br />
<a href="Default.aspx">Main Control Page<a href="SelectTask.aspx?target=invoke">Start/Stop Task</a> | <a href="SelectTask.aspx?target=results">View Results</a><br />
			UI Version: <asp:Label ID="LabelUIVersion" runat="server" Text="Label"></asp:Label>
            , Core Version: <asp:Label ID="LabelEngineVersion" runat="server" Text="Label"></asp:Label><br />
			iWeb Crawler Development Team © 2010<br />
				
			
		</div>
	</div>
    </form>
</body>
</html>

