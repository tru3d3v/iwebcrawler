using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrawlerNameSpace.StorageSystem;
using CrawlerNameSpace.Utilities;

public partial class SelectTask : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LabelUIVersion.Text = Version.getUIVersion();
        LabelEngineVersion.Text = Version.getCoreVersion();

        DropDownList1.Items.Add("None");
        List<TaskStatus> tasks = StorageSystem.getInstance().getWorkDetails("Demo", QueryOption.AllTasks);
        foreach (TaskStatus task in tasks)
        {
            DropDownList1.Items.Add(task.getTaskName());
        }

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
