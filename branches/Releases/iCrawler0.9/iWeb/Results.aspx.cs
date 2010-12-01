using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrawlerNameSpace.StorageSystem;
using CrawlerNameSpace.Utilities;
using System.Text;

public partial class Results : System.Web.UI.Page
{
    private static int defaultEntriesPerPage = 10;
    private void putNumOfPages(int numOfEntries, int entriesPerPage)
    {
        int numOfPages = numOfEntries / entriesPerPage;
        if (numOfPages == 0 || numOfEntries % entriesPerPage != 0) numOfPages = numOfPages + 1;

        DropDownList4.Items.Clear();
        for (int i = 0; i < numOfPages; i++)
        {
            DropDownList4.Items.Add((i + 1).ToString());
        }
    }

    private string convertOrderToString(Order order)
    {
        switch (order)
        {
            case Order.NormalOrder:
                return "N";
            case Order.DescendingTrust:
                return "DT";
            case Order.DescendingRank:
                return "DR";
            case Order.AscendingTrust:
                return "AT";
            case Order.AscendingRank:
                return "AR";
            default:
                return null;
        }
    }

    private Order convertToOrder(string order)
    {
        switch (order)
        {
            case "DT":
                return Order.DescendingTrust;
            case "DR":
                return Order.DescendingRank;
            case "AT":
                return Order.AscendingTrust;
            case "AR":
                return Order.AscendingRank;
            default:
                return Order.NormalOrder;
        }
    }

    private TaskStatus validateTask()
    {
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
        return requiredTask;
    }

    public bool SetCookie(string cookiename, string cookievalue, int iDaysToExpire)
    {
        try
        {
            HttpCookie objCookie = new HttpCookie(cookiename);
            Response.Cookies.Clear();
            Response.Cookies.Add(objCookie);
            objCookie.Values.Add(cookiename, cookievalue);
            DateTime dtExpiry = DateTime.Now.AddDays(iDaysToExpire);
            Response.Cookies[cookiename].Expires = dtExpiry;
        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public string GetCookie(string cookiename)
    {
        string cookyval = null;
        try
        {
            cookyval = Request.Cookies[cookiename].Value;
            cookyval = cookyval.Split('=')[1];
            cookyval = cookyval.Trim();
        }
        catch (Exception e)
        {
            cookyval = null;
        }
        return cookyval;
    }

    private void createCookie(TaskStatus task)
    {
        if (GetCookie("Created") == null)
        {
            SetCookie("Created", "T", 1);
            SetCookie("Records", Convert.ToString(10), 1);
            if (task != null)
            {
                Response.Cookies["resultInfo"]["TID"] = task.getTaskID();
            }
            else
            {
                Response.Cookies["resultInfo"]["TID"] = "NONE";
            }
            Response.Cookies["resultInfo"]["Remove"] = Convert.ToString(false);
            Response.Cookies["resultInfo"]["View"] = Convert.ToString("ALL");
            Response.Cookies["resultInfo"]["Order"] = convertOrderToString(Order.NormalOrder);
            Response.Cookies["resultInfo"]["Page"] = Convert.ToString(0);
            Response.Cookies["resultInfo"]["numOfResults"] = Convert.ToString(0);
        }
    }

    private void loadSettings()
    {
        if (GetCookie("Records") != null)
        {
            int records = Convert.ToInt32(GetCookie("Records"));
            //DropDownList1.SelectedIndex = records / 5 -1;
        }
    }

    private Category getSelectedCategory(string taskID)
    {
        List<Category> categories = StorageSystem.getInstance().getCategories(taskID);
        foreach (Category category in categories)
        {
            if (category.getCatrgoryName() == DropDownList3.SelectedValue)
            {
                return category;
            }
        }
        return null;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = "View Results";
        TaskStatus requiredTask = validateTask();

        if (!Page.IsPostBack)
        {
            createCookie(requiredTask);
        }
        if (requiredTask != null)
        {
            loadSettings();
            Label2.Text = requiredTask.getTaskName().Trim();

            int prevSelectView = DropDownList3.SelectedIndex;
            DropDownList3.Items.Clear();
            DropDownList3.Items.Add("ALL");
            List<Category> cats = StorageSystem.getInstance().getCategories(requiredTask.getTaskID());
            foreach (Category cat in cats)
            {
                DropDownList3.Items.Add(cat.getCatrgoryName().Trim());
            }
            if (DropDownList3.Items.Count > prevSelectView)
            {
                DropDownList3.SelectedIndex = prevSelectView;
            }
            int numOfResults = 0;
            if (DropDownList3.SelectedValue == "ALL")
            {
                numOfResults = (int)StorageSystem.getInstance().getTotalURLs(requiredTask.getTaskID(), null);
                Label3.Text = StorageSystem.getInstance().getTotalURLs(requiredTask.getTaskID(), null).ToString();
            }
            else
            {
                Category category = getSelectedCategory(requiredTask.getTaskID());
                Label3.Text = StorageSystem.getInstance().getTotalURLs(requiredTask.getTaskID(), category.getCategoryID()).ToString();
                numOfResults = (int)StorageSystem.getInstance().getTotalURLs(requiredTask.getTaskID(), category.getCategoryID());
            }
            int prevSelect = DropDownList4.SelectedIndex;
            putNumOfPages(numOfResults, Convert.ToInt32(DropDownList1.SelectedValue));
            if (DropDownList4.Items.Count > prevSelect)
            {
                DropDownList4.SelectedIndex = prevSelect;
            }
        }
        // Button1.Attributes.Add("onClick", "MyJS('a','b');");
        LabelUIVersion.Text = Version.getUIVersion();
        LabelEngineVersion.Text = Version.getCoreVersion();
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

    private Order getSelectedOrder()
    {
        switch (DropDownList2.SelectedValue)
        {
            case "Normal":
                return Order.NormalOrder;
            case "Rank - Ascending":
                return Order.AscendingRank;
            case "Rank - Decesinding":
                return Order.DescendingRank;
            case "Trust - Ascending":
                return Order.AscendingTrust;
            case "Trust - Decesinding":
                return Order.DescendingTrust;
        }
        return Order.NormalOrder;
    }

    private string getCategoryName(string categoryID, List<Category> categories)
    {
        foreach (Category category in categories)
        {
            if (categoryID.Trim() == category.getCategoryID().Trim())
            {
                return category.getCatrgoryName();
            }
        }
        return "Web Page";
    }

    public string drawEntries()
    {
        int numOfEntries = Convert.ToInt32(DropDownList1.SelectedValue);
        int page = 0;
        string sv = DropDownList4.SelectedValue;
        if(DropDownList4.SelectedValue != "")
            page = Convert.ToInt32(DropDownList4.SelectedValue);
        int from = (page - 1) * numOfEntries;
        int to = page * numOfEntries - 1;
        TaskStatus task = validateTask();
        Order order = getSelectedOrder();
        List<Result> results = null;
        List<Category> categories = null;
        
        if(task != null)
        {
            categories = StorageSystem.getInstance().getCategories(task.getTaskID());
            if (DropDownList3.SelectedValue == "ALL")
            {
                results = StorageSystem.getInstance().getURLsFromCategory(task.getTaskID(), null, order, from, to);
            }
            else
            {
                foreach (Category category in categories)
                {
                    if (category.getCatrgoryName().Trim() == DropDownList3.SelectedValue)
                    {
                        results = StorageSystem.getInstance().getURLsFromCategory(task.getTaskID(), category.getCategoryID(), order, from, to);
                        break;
                    }
                }
            }
        }
        
        string display = "";

        for (int i = 0; i < numOfEntries; i++)
        {
            try 
            {
                if (CheckBox1.Checked)
                {
                    display = display + "<div class=\"style7\" style=\"height:auto; \" align=\"justify\">" +
                    "<table class=\"style3\">" + "<tr>" + "<td class=\"style14\" style=\"width:20%; text-align: left;\">" +
                    "URL:</td>" + "<td colspan=\"5\" style=\"text-align: left\"><a href=\"" +
                    results[i].getUrl().Trim() + "\">" +
                    results[i].getUrl().Trim() + "</a>" +
                    "</td>" + "</tr>" + "<tr>" + "<td class=\"style14\" style=\"width:20%; text-align: left;\">" + "Category:</td>" +
                    "<td class=\"style15\" style=\"width:15%; \" style=\"text-align: left\">" +
                    getCategoryName(results[i].getCategoryID(), categories) +
                    "</td>" +
                    "<td class=\"style17\" style=\"width:15%; \" style=\"text-align: left\">" + "Trust:</td>" + "<td class=\"style16\" style=\"width:15%; \" style=\"text-align: left\">" +
                    results[i].getTrustMeter().ToString() +
                    "</td>" + "<td class=\"style16\" style=\"width:15%; \" style=\"text-align: left\">" + "<span class=\"bodytext\">" +
                    "Page Rank:</td>" + "<td style=\"width:15%; \" style=\"text-align: left\">" + "<span class=\"bodytext\">" +
                    results[i].getRank().ToString() +
                    "</td>" + "</tr>" +

                    "<tr>" + "<td class=\"style14\" style=\"width:20%; text-align: left;\">" + "More Options:</td>" + "<td class=\"style15\" colspan=\"5\" style=\"text-align: right\">" +
                    "<img onclick=\"__doPostBack(\'PostBack\',\'\');\" src=\"images/remove.bmp\">"
                    + "</td>" + "</tr>" +

                    "</table>" + "</div>";
                }
                else
                {
                    display = display + "<div class=\"style7\" style=\"height:auto; \" align=\"justify\">" +
                    "<table class=\"style3\">" + "<tr>" + "<td class=\"style14\" style=\"width:20%; text-align: left;\">" +
                    "URL:</td>" + "<td colspan=\"5\" style=\"text-align: left\"><a href=\"" +
                    results[i].getUrl().Trim() + "\">" + 
                    results[i].getUrl().Trim() + "</a>" +
                    "</td>" + "</tr>" + "<tr>" + "<td class=\"style14\" style=\"width:20%; text-align: left;\">" + "Category:</td>" +
                    "<td class=\"style15\" style=\"width:15%; \" style=\"text-align: left\">" +
                    getCategoryName(results[i].getCategoryID(), categories) +
                    "</td>" +
                    "<td class=\"style17\" style=\"width:15%; \" style=\"text-align: left\">" + "Trust:</td>" + "<td class=\"style16\" style=\"width:15%; \" style=\"text-align: left\">" +
                    results[i].getTrustMeter().ToString() +
                    "</td>" + "<td class=\"style16\" style=\"width:15%; \" style=\"text-align: left\">" + "<span class=\"bodytext\">" +
                    "Page Rank:</td>" + "<td style=\"width:15%; \" style=\"text-align: left\">" + "<span class=\"bodytext\">" +
                    results[i].getRank().ToString() +
                    "</td>" + "</tr>" +
                    "</table>" + "</div>";
                }
            }
            catch(Exception e)
            {
            }
        }

        return display;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        /*SetCookie("Records", DropDownList1.Items[DropDownList1.SelectedIndex].Value.ToString(), 1);
        string orderBy = DropDownList2.Items[DropDownList2.SelectedIndex].Value;
        switch (orderBy)
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
        }*/
        Button5.Enabled = CheckBox1.Checked;
        //Session["Remove"] = CheckBox1.Checked;
        //Response.Redirect("Results.aspx?TID=" + Request["TID"]);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        TaskStatus task = validateTask();
        if(task != null)
        {
            StorageSystem.getInstance().removeAllResults(task.getTaskID());
        }
    }
}
