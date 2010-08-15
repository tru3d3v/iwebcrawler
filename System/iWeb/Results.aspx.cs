using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrawlerNameSpace.StorageSystem;
using CrawlerNameSpace.Utilities;

public partial class Results : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = "View Results";
        TaskStatus requiredTask = null;
        List<TaskStatus> tasks = StorageSystem.getInstance().getWorkDetails("5df16977-d18e-4a0a-b81b-0073de3c9a7f", QueryOption.AllTasks);

        foreach (TaskStatus task in tasks)
        {
            if (task.getTaskName().Trim() == Request["TID"])
            {
                requiredTask = task;
                break;
            }
        }
        if (requiredTask != null)
        {
            Label2.Text = requiredTask.getTaskName().Trim();
            Label3.Text = StorageSystem.getInstance().getTotalURLs(requiredTask.getTaskID()).ToString();
            DropDownList3.Items.Add("ALL");
            List<Category> cats  = StorageSystem.getInstance().getCategories(requiredTask.getTaskID());
            foreach (Category cat in cats)
            {
                DropDownList3.Items.Add(cat.getCatrgoryName().Trim());
            }
        }
        // Button1.Attributes.Add("onClick", "MyJS('a','b');");
        LabelUIVersion.Text = Version.getUIVersion();
        LabelEngineVersion.Text = Version.getCoreVersion();
        Session.Add("Entries", 10);
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public string drawEntries()
    {
        int numOfEntries = Convert.ToInt32(Session["Entries"]);
        string display = "";

        for (int i = 0; i < numOfEntries; i++)
        {
            display = display + "<div class=\"style7\" style=\"height:65px; \" align=\"justify\">" +
            "<table class=\"style3\">" + "<tr>" + "<td class=\"style14\" style=\"text-align: left\">" +
            "URL:</td>" + "<td colspan=\"5\" style=\"text-align: left\">" + 
            "URL" +
            "</td>" + "</tr>" + "<tr>" + "<td class=\"style14\" style=\"text-align: left\">" + "Category:</td>" +
            "<td class=\"style15\" style=\"text-align: left\">" + 
            "CATEGORY" + 
            "</td>" +
            "<td class=\"style17\" style=\"text-align: left\">" + "Trust:</td>" + "<td class=\"style16\" style=\"text-align: left\">" +
            "Trust" + 
            "</td>" + "<td class=\"style16\" style=\"text-align: left\">" + "<span class=\"bodytext\">" +
            "Page Rank:</td>" + "<td style=\"text-align: left\">" + "<span class=\"bodytext\">" + 
            "Rank" +
            "</td>" + "</tr>" + "<tr>" + "<td class=\"style14\" style=\"text-align: left\">" + "More Options:</td>" + "<td class=\"style15\" colspan=\"5\" style=\"text-align: right\">" +
            "Options" 
            + "</td>" + "</tr>" + "</table>" + "</div>";
        }

        return display;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Session["Entries"] = DropDownList1.Items[DropDownList1.SelectedIndex].Value;
        Session["OrderByStr"] = DropDownList2.Items[DropDownList2.SelectedIndex].Value;
        switch ((string)Session["OrderByStr"])
        {
            case "Normal":
                Session["OrderBy"] = Order.NormalOrder;
                break;
            case "Rank - Ascending":
                Session["OrderBy"] = Order.AscendingRank;
                break;
            case "Rank - Decesinding":
                Session["OrderBy"] = Order.DescendingRank;
                break;
            case "Trust - Ascending":
                Session["OrderBy"] = Order.AscendingTrust;
                break;
            case "Trust - Decesinding":
                Session["OrderBy"] = Order.DescendingTrust;
                break;
        }
        Session["Remove"] = CheckBox1.Checked;
    }
}
