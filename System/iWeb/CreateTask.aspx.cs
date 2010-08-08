using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrawlerNameSpace.StorageSystem;
using CrawlerNameSpace.Utilities;

public partial class CreateTask : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = "Create New Task";
        LabelUIVersion.Text = Version.getUIVersion();
        LabelEngineVersion.Text = Version.getCoreVersion();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Label2.ForeColor = System.Drawing.Color.Red;
        string taskName = TextBox1.Text;
        try
        {
            StorageSystem.getInstance().createWorkResources("5df16977-d18e-4a0a-b81b-0073de3c9a7f", taskName);
            Response.Redirect("EditTask.aspx?TID=" + taskName);
        }
        catch (Exception exp)
        {
            Label2.Text = "Error: " + exp.Message;
        }
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
}
