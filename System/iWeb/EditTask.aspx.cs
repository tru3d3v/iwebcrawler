using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrawlerNameSpace.StorageSystem;
using CrawlerNameSpace.Utilities;

public partial class EditTask : System.Web.UI.Page
{
    protected void resetDefaults()
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
        if (requiredTask != null)
        {
            Session.Add("TID", requiredTask.getTaskID());
            putTaskSettings(requiredTask);

            ListBox1.Items.Clear();
            List<string> seeds = StorageSystem.getInstance().getSeedList(requiredTask.getTaskID());
            foreach (string seed in seeds)
            {
                ListBox1.Items.Add(seed);
            }
        }
    }

    protected void putTaskSettings(TaskStatus task)
    {
        Label2.Text = task.getTaskName().Trim();
        Constraints limits = StorageSystem.getInstance().getRestrictions(task.getTaskID());
        TextBox1.Text = limits.getAllowedDepth().ToString();
        bool paramsAllowed = limits.isParametrizationAllowed();
        if (paramsAllowed == true) DropDownList1.SelectedIndex = 1;
        else DropDownList1.SelectedIndex = 0;

        ListBox2.Items.Clear();
        List<string> crawlNetworks = limits.getCrawlList();
        foreach (string net in crawlNetworks)
        {
            ListBox2.Items.Add(net);
        }

        ListBox3.Items.Clear();
        List<string> restrictNetworks = limits.getRestrictionList();
        foreach (string net in restrictNetworks)
        {
            ListBox3.Items.Add(net);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LabelUIVersion.Text = Version.getUIVersion();
        LabelEngineVersion.Text = Version.getCoreVersion();

        if(!Page.IsPostBack) resetDefaults();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (ListBox1.Items.Count != 0 && ListBox1.SelectedIndex < ListBox1.Items.Count && ListBox1.SelectedIndex >= 0)
        {
            ListBox1.Items.Remove(ListBox1.Items[ListBox1.SelectedIndex]);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox2.Text != "")
        {
            ListBox1.Items.Add("http://" + TextBox2.Text);
            TextBox2.Text = "";
        }
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        if (TextBox3.Text != "")
        {
            ListBox2.Items.Add(TextBox3.Text);
            TextBox3.Text = "";
        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        if (ListBox2.Items.Count != 0 && ListBox2.SelectedIndex < ListBox2.Items.Count && ListBox2.SelectedIndex >= 0)
        {
            ListBox2.Items.Remove(ListBox2.Items[ListBox2.SelectedIndex]);
        }
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        if (TextBox4.Text != "")
        {
            ListBox3.Items.Add(TextBox4.Text);
            TextBox4.Text = "";
        }
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        if (ListBox3.Items.Count != 0 && ListBox3.SelectedIndex < ListBox3.Items.Count && ListBox3.SelectedIndex >= 0)
        {
            ListBox3.Items.Remove(ListBox3.Items[ListBox3.SelectedIndex]);
        }
    }
    protected void Button11_Click(object sender, EventArgs e)
    {
        resetDefaults();
    }
    protected void Button13_Click(object sender, EventArgs e)
    {
        try
        {
            StorageSystem.getInstance().releaseWorkResources((string)Session["TID"]);
            Response.Redirect("Default.aspx");
        }
        catch (Exception exp)
        {
            Label3.Text = "Error: " + exp.Message;
        }
    }

    private List<string> getListBoxContent(ListBox listbox)
    {
        List<string> list = new List<string>();
        foreach (ListItem item in listbox.Items)
        {
            list.Add(item.Text);
        }
        return list;
    }

    protected void Button12_Click(object sender, EventArgs e)
    {
        if ((string)Session["TID"] != null)
        {
            List<string> seeds = getListBoxContent(ListBox1);
            StorageSystem.getInstance().setSeedList((string)Session["TID"], seeds);

            List<string> crawl = getListBoxContent(ListBox2);
            List<string> restrict = getListBoxContent(ListBox3);
            bool paramAllowed = false;
            if (DropDownList1.SelectedIndex == 1) paramAllowed = true;
            int depth = Convert.ToInt32(TextBox1.Text);
            Constraints constraints = new Constraints(4, paramAllowed, restrict, crawl);
            StorageSystem.getInstance().setRestrictions((string)Session["TID"], constraints);
        }
    }
}
