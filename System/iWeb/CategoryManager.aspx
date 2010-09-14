<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="CategoryManager.aspx.cs" Inherits="CategoryManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta name="author" content="iWeb Crawler UI" />
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
	<link rel="stylesheet" href="images/style.css" type="text/css" />
	<title>iWeb Crawler Control Page</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            text-align: left;
        }
        .style3
        {
            width: 190px;
            text-align: left;
        }
        .style4
        {
            font-size: small;
            font-weight: bold;
            color: #C0C0C0;
        }
        .style5
        {
            color: #666666;
            font-style: normal;
            font-variant: normal;
            font-size: small;
            line-height: normal;
            font-family: "normal Tahoma", sans-serif;
            direction: rtl;
            text-align: left;
        }
        .style6
        {
            text-align: left;
        }
        .style7
        {
            text-align: center;
        }
        .style8
        {
            padding: 12px;
            text-align: left;
            font-size: small;
            font-weight: bold;
            color: #666666;
            font-style: normal;
            font-variant: normal;
            line-height: normal;
            font-family: "normal Tahoma", sans-serif;
            direction: rtl;
        }
        .style9
        {
            width: 90px;
        }
        .style10
        {
            width: 100px;
        }
        .style11
        {
            width: 81px;
        }
        .style12
        {
            text-align: left;
            width: 90px;
        }
        .style13
        {
            width: 50px;
        }
        .style14
        {
            text-align: left;
            width: 120px;
        }
        .style15
        {
            width: 120px;
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
				<div align="right" style="padding:12px;" class="smallwhitetext"><a href="Default.aspx">
                    Main Control</a></div>
			</div>
		</div>
		<div id="header" align="center">
			<div class="titletext" id="logo">
				<div class="logotext" style="margin:30px"><span class="orangelogotext">i</span>Crawler</div> 
			</div>
			<div id="pagetitle">
				<div id="title" class="titletext" align="right">Category Manager</div>
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
			<div class="style8" align="justify">
				Create a new Category</div>
			<div class="panel" style="height:60px; text-align: center;">
				<table class="style1">
                    <tr>
                        <td class="style2">
				            <span class="bodytext">Category Name:</td>
                        <td class="style3">
                            <asp:TextBox ID="TextBox1" runat="server" style="text-align: left" 
                                MaxLength="20" ontextchanged="TextBox1_TextChanged" Width="115px"></asp:TextBox>
                            &nbsp;<asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td class="style2">
                            <span class="bodytext">Confidence Level:</td>
                        <td>
                            <asp:TextBox ID="TextBox4" runat="server" ontextchanged="TextBox2_TextChanged" 
                                Width="65px" Height="20px"></asp:TextBox>
                            <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <span class="bodytext">Parent Category:</td>
                        <td class="style3" nowrap="nowrap">
                            <asp:DropDownList ID="DropDownList1" runat="server" style="text-align: left" 
                                onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style2">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="TextBox4" ErrorMessage="CL range: 0-100" 
                                ValidationExpression="\d{1,3}"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" style="text-align: center" 
                                Text="Create" onclick="Button1_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="style2" style="padding:12px;" align="justify">
				<span class="style4"><span class="style5">Edit Keywords</span></span><br />
				
			</div>
            </span>
                
            <div class="panel" style="height:80px; text-align: center;">
				<table class="style1">
                    <tr>
                        <td class="style6">
				            <span class="bodytext">Category Name:</td>
                        <td class="style6">
                            <asp:DropDownList ID="DropDownList2" runat="server" style="text-align: left" 
                                AutoPostBack="True" onselectedindexchanged="DropDownList2_SelectedIndexChanged" 
                                ontextchanged="DropDownList2_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style6" rowspan="3">
                            <asp:ListBox ID="ListBox1" runat="server" style="margin-right: 0px; text-align: center;" 
                                Width="115px" Height="80px"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style6">
                            <span class="bodytext">Keyword:</td>
                        <td class="style2" nowrap="nowrap">
                            <asp:TextBox ID="TextBox5" runat="server" 
                                style="margin-left: 0px; text-align: left;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style7" colspan="2">
                            <asp:Button ID="Button2" runat="server" Text="Add" onclick="Button2_Click" 
                                style="height: 26px" />
                            &nbsp;<asp:Button ID="Button3" runat="server" Text="Remove Selected" onclick="Button3_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="style2" style="padding:12px;" align="justify">
				<span class="style4"><span class="style5">Summary</span></span></span><br />
				
			</div>
			<div class="panel" style="height:auto; text-align: center;">
			    <table class="style1">
                    <tr>
                        <td class="style9">
				            <span class="bodytext">Category Name</td>
                        <td class="style9">
                            &nbsp;</td>
                        <td class="style10">
                            &nbsp;</td>
                        <td class="style11">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td VALIGN=TOP>
                            <asp:ListBox ID="ListBox2" runat="server" Height="81px" Width="122px" 
                                onselectedindexchanged="ListBox2_SelectedIndexChanged" AutoPostBack="True">
                            </asp:ListBox>
                        </td>
                        <td colspan="4">
                            <table class="style1">
                                <tr>
                                    <td class="style12">
				            <span class="bodytext">Parent Name:</td>
                                    <td class="style13">
                                        <asp:Label ID="Label2" runat="server" Text="None"></asp:Label>
                                    </td>
                                    <td class="style14">
                                        <span class="bodytext">Confidence Level:</td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style12">
                                        <span class="bodytext">Keywords:
                                        </td>
                                    <td class="style13">
                                        &nbsp;</td>
                                    <td class="style14">
                                        <span class="bodytext">Keywords(#):</td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: left">
                                        <asp:Label ID="Label8" runat="server" Text="None"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style12">
                                        <span class="bodytext">Sons(#):</td>
                                    <td class="style13">
                                        <asp:Label ID="Label5" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="style15">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Button ID="Button4" runat="server" Text="Remove (include sons)" 
                                onclick="Button4_Click" />
                                
                            &nbsp;<asp:Button ID="Button8" runat="server" 
                                Text="Remove (move sons to father)" onclick="Button8_Click" />
                        </td>
                    </tr>
                </table>
			</div>
			<div class="style2" style="padding:12px;" align="justify">
				<span class="style4"><span class="style5">Save Options</span></span></span><br />
				
			</div>
			
			<div class="panel" style="height:auto; text-align: center;">
			
			    <table class="style1">
                    <tr>
                        <td style="text-align: left">
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" />
&nbsp;<span class="bodytext">Remove duplicate keywords when saving</td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:CheckBox ID="CheckBox2" runat="server" Checked="True" 
                                oncheckedchanged="CheckBox2_CheckedChanged" />
                            <span class="bodytext">&nbsp;Add son's keywords to fathers when saving 
                        </td>
                    </tr>
                </table>
			
			</div>
			
			
			<div class="style2" style="padding:12px;" align="justify">
				<span class="style4"><span class="style5">Actions</span></span><br />
				
			</div>
            <asp:Button ID="Button5" runat="server" Text="Back (Unsaved changes will be lost)" 
                onclick="Button5_Click" />
		    &nbsp;<asp:Button ID="Button6" runat="server" Text="Save" 
                onclick="Button6_Click" />
            &nbsp;<asp:Button ID="Button7" runat="server" Text="Restore Saved Settings" 
                onclick="Button7_Click" />
		</div>
		
		
			
		</div>
		<div id="footer" class="smallgraytext" align="center">
		<img src="images/crawler.jpg" /><br />
<a href="Default.aspx">Main Control Pagespx?target=edit&quot;&gt;Edit Task Proporties</a> | <a href="SelectTask.aspx?target=invoke">
            Start/Stop Task</a> | <a href="SelectTask.aspx?target=results">View Results</a><br />
			UI Version: <asp:Label ID="LabelUIVersion" runat="server" Text="Label"></asp:Label>
            , Core Version: <asp:Label ID="LabelEngineVersion" runat="server" Text="Label"></asp:Label><br />
			iWeb Crawler Development Team © 2010<br />
				
			
		</div>
	</div>
    </form>
</body>
</html>