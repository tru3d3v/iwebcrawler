using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrawlerNameSpace.StorageSystem;
using CrawlerNameSpace.Utilities;
using System.Web.UI.HtmlControls;

public partial class CrawlerOptions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = "Crawler Options";
        LabelUIVersion.Text = Version.getUIVersion();
        LabelEngineVersion.Text = Version.getCoreVersion();
        if (!Page.IsPostBack)
        {
            foreach(FrontierDesign fd in supportedFrontierDesigns.selection)
            {
                DropDownList1.Items.Add(fd.ToString());
            }
            TaskStatus ts = validateTask();
            if (ts != null)
            {
                loadSavedOptions(StorageSystem.getInstance(), ts.getTaskID().Trim());
            }
        }
    }
    private void saveOptions(StorageSystem ss, String tid)
    {
        ss.setProperty(tid, TaskProperty.THREADS.ToString(), TextBox1.Text);
        ss.setProperty(tid, TaskProperty.FRONTIER.ToString(), DropDownList1.SelectedValue);

        ss.setProperty(tid, TaskProperty.CAT_ALPHA.ToString(), TextBox2.Text);
        ss.setProperty(tid, TaskProperty.CAT_BETA.ToString(), TextBox3.Text);
        ss.setProperty(tid, TaskProperty.CAT_GAMMA.ToString(), TextBox4.Text);
        ss.setProperty(tid, TaskProperty.CAT_MIN.ToString(), TextBox5.Text);
        ss.setProperty(tid, TaskProperty.CAT_PENLTY.ToString(), TextBox6.Text);

        ss.setProperty(tid, TaskProperty.NER_ALPHA.ToString(), TextBox12.Text);
        ss.setProperty(tid, TaskProperty.NER_BETA.ToString(), TextBox13.Text);
        ss.setProperty(tid, TaskProperty.NER_GAMMA.ToString(), TextBox14.Text);
        ss.setProperty(tid, TaskProperty.NER_MIN.ToString(), TextBox15.Text);
        ss.setProperty(tid, TaskProperty.NER_PENLTY.ToString(), TextBox16.Text);

        ss.setProperty(tid, TaskProperty.ANC_ALPHA.ToString(), TextBox17.Text);
        ss.setProperty(tid, TaskProperty.ANC_BETA.ToString(), TextBox18.Text);
        ss.setProperty(tid, TaskProperty.ANC_GAMMA.ToString(), TextBox19.Text);
        ss.setProperty(tid, TaskProperty.ANC_MIN.ToString(), TextBox20.Text);
        ss.setProperty(tid, TaskProperty.ANC_PENLTY.ToString(), TextBox21.Text);

        ss.setProperty(tid, TaskProperty.RAN_ALPHA.ToString(), TextBox22.Text);
        ss.setProperty(tid, TaskProperty.RAN_BETA.ToString(), TextBox23.Text);
        ss.setProperty(tid, TaskProperty.RAN_GAMMA.ToString(), TextBox24.Text);
        ss.setProperty(tid, TaskProperty.RAN_DELTA.ToString(), TextBox25.Text);
        ss.setProperty(tid, TaskProperty.RAN_NEARBY.ToString(), TextBox26.Text);
    }
    private void loadSavedOptions(StorageSystem ss, String tid)
    {
        String value = ss.getProperty(tid, TaskProperty.THREADS.ToString());
        if(value != null) TextBox1.Text = value.Trim();
        value = ss.getProperty(tid, TaskProperty.FRONTIER.ToString());
        if (value != null) DropDownList1.SelectedValue = value.Trim();
        DropDownList1_SelectedIndexChanged(null, null);

        value = ss.getProperty(tid, TaskProperty.CAT_ALPHA.ToString());
        if (value != null) TextBox2.Text = value.Trim();
        value = ss.getProperty(tid, TaskProperty.CAT_BETA.ToString());
        if (value != null) TextBox3.Text = value.Trim();
        value = ss.getProperty(tid, TaskProperty.CAT_GAMMA.ToString());
        if (value != null) TextBox4.Text = value.Trim();
        value = ss.getProperty(tid, TaskProperty.CAT_MIN.ToString());
        if (value != null) TextBox5.Text = value.Trim();
        value = ss.getProperty(tid, TaskProperty.CAT_PENLTY.ToString());
        if (value != null) TextBox6.Text = value.Trim();

        value = ss.getProperty(tid, TaskProperty.NER_ALPHA.ToString());
        if (value != null) TextBox12.Text = value.Trim();
        value = ss.getProperty(tid, TaskProperty.NER_BETA.ToString());
        if (value != null) TextBox13.Text = value.Trim();
        value = ss.getProperty(tid, TaskProperty.NER_GAMMA.ToString());
        if (value != null) TextBox14.Text = value.Trim();
        value = ss.getProperty(tid, TaskProperty.NER_MIN.ToString());
        if (value != null) TextBox15.Text = value.Trim();
        value = ss.getProperty(tid, TaskProperty.NER_PENLTY.ToString());
        if (value != null) TextBox16.Text = value.Trim();

        value = ss.getProperty(tid, TaskProperty.ANC_ALPHA.ToString());
        if (value != null) TextBox17.Text = value.Trim();
        value = ss.getProperty(tid, TaskProperty.ANC_BETA.ToString());
        if (value != null) TextBox18.Text = value.Trim();
        value = ss.getProperty(tid, TaskProperty.ANC_GAMMA.ToString());
        if (value != null) TextBox19.Text = value.Trim();
        value = ss.getProperty(tid, TaskProperty.ANC_MIN.ToString());
        if (value != null) TextBox20.Text = value.Trim();
        value = ss.getProperty(tid, TaskProperty.ANC_PENLTY.ToString());
        if (value != null) TextBox21.Text = value.Trim();

        value = ss.getProperty(tid, TaskProperty.RAN_ALPHA.ToString());
        if (value != null) TextBox22.Text = value.Trim();
        value = ss.getProperty(tid, TaskProperty.RAN_BETA.ToString());
        if (value != null) TextBox23.Text = value.Trim();
        value = ss.getProperty(tid, TaskProperty.RAN_GAMMA.ToString());
        if (value != null) TextBox24.Text = value.Trim();
        value = ss.getProperty(tid, TaskProperty.RAN_DELTA.ToString());
        if (value != null) TextBox25.Text = value.Trim();
        value = ss.getProperty(tid, TaskProperty.RAN_NEARBY.ToString());
        if (value != null) TextBox26.Text = value.Trim();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue == FrontierDesign.FIFO_BFS.ToString())
        {
            divCategorizerOptionsText.Visible = false;
            divCategorizerOptions.Visible = false;
            divNearbyOptionsText.Visible = false;
            divNearbyOptions.Visible = false;
            divAnchorOptionsText.Visible = false;
            divAnchorOptions.Visible = false;
            divRankerOptionsText.Visible = false;
            divRankerOptions.Visible = false;
        }
        else
        {
            divCategorizerOptionsText.Visible = true;
            divCategorizerOptions.Visible = true;
            divNearbyOptionsText.Visible = true;
            divNearbyOptions.Visible = true;
            divAnchorOptionsText.Visible = true;
            divAnchorOptions.Visible = true;
            divRankerOptionsText.Visible = true;
            divRankerOptions.Visible = true;
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        TaskStatus ts = validateTask();
        if(ts != null)
        {
            saveOptions(StorageSystem.getInstance(), ts.getTaskID().Trim());
            //loadSavedOptions(StorageSystem.getInstance(), ts.getTaskID().Trim());
        }
    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox5_TextChanged(object sender, EventArgs e)
    {

    }
}
