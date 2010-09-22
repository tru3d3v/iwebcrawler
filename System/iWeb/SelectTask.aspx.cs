using System;
using System.Collections.Generic;
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

        //DropDownList1.Items.Add("None");
        List<TaskStatus> tasks = StorageSystem.getInstance().getWorkDetails("5df16977-d18e-4a0a-b81b-0073de3c9a7f", QueryOption.AllTasks);
        foreach (TaskStatus task in tasks)
        {
            DropDownList1.Items.Add(task.getTaskName());
        }
        //Label1.Text = Request["target"];
        if (Request["target"] != null)
        {
            switch (Request["target"].ToLower())
            {
                case "edit":
                    Label1.Text = "Edit a Task";
                    break;
                case "invoke":
                    Label1.Text = "Run a Task";
                    break;
                case "results":
                    Label1.Text = "Results";
                    break;
                case "options":
                    Label1.Text = "Options";
                    break;
                default:
                    Label1.Text = "Edit a Task";
                    break;
            }
        }
        else
        {
            Label1.Text = "Unknown Option";
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        Label1.Text = "Processing ..";
        if (Request["target"] != null)
        {
            switch (Request["target"].ToLower())
            {
                case "edit":
                    Response.Redirect("EditTask.aspx?TID=" + DropDownList1.SelectedValue);
                    break;
                case "invoke":
                    Response.Redirect("InvokeTask.aspx?TID=" + DropDownList1.SelectedValue);
                    break;
                case "results":
                    Response.Redirect("Results.aspx?TID=" + DropDownList1.SelectedValue);
                    break;
                case "options":
                    Response.Redirect("CrawlerOptions.aspx?TID=" + DropDownList1.SelectedValue);
                    break;
                default:
                    Response.Redirect("EditTask.aspx?TID=" + DropDownList1.SelectedValue);
                    break;
            }
        }
    }
}
