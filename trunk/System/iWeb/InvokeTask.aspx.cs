using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrawlerNameSpace.StorageSystem;
using CrawlerNameSpace.Utilities;

public partial class InvokeTask : System.Web.UI.Page
{
    private string printList(List<TaskStatus> list)
    {
        string text = "none";
        int count = 0;

        foreach(TaskStatus task in list)
        {
            if (text == "none") text = task.getTaskName();
            else text = text + ", " + task.getTaskName();

            count = count + 1;
            if (count == 5 && list.Count > 5)
            {
                text = text + " and more .. ";
                return text;
            }
        }
        return text;
    }

    private string printWorkingTime(long timeSecs)
    {
        if (timeSecs < 60) return timeSecs.ToString() + " seconds";
        else
        {
            long timeMinutes = timeSecs / 60;
            long secs = timeSecs % 60;
            if (timeMinutes < 60) return timeMinutes + " minutes, " + secs + " secs";
            else
            {
                long timeHours = timeMinutes / 60;
                long mins = timeMinutes % 60;
                if (timeHours < 24) return timeHours + " hours, " + mins + " mins";
                else
                {
                    long timeDays = timeHours / 24;
                    long hrs = timeHours % 24;
                    return timeDays + " days, " + hrs + " hrs";
                }
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LabelUIVersion.Text = Version.getUIVersion();
        LabelEngineVersion.Text = Version.getCoreVersion();
        Label1.Text = "Invoke a Task";

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
            Session.Add("Task", requiredTask);
            Label2.Text = requiredTask.getTaskName().Trim();
            Label3.Text = TaskStatus.convertToStatusString(requiredTask.getTaskStatus());
            Label4.Text = printWorkingTime(requiredTask.getTaskElapsedTime());
            Label5.Text = StorageSystem.getInstance().getTotalURLs(requiredTask.getTaskID().Trim()).ToString();
        }

        Label6.Text = StorageSystem.getInstance().getWorkDetails("5df16977-d18e-4a0a-b81b-0073de3c9a7f", QueryOption.ActiveTasks).Count.ToString();
        Label7.Text = StorageSystem.getInstance().getWorkDetails("5df16977-d18e-4a0a-b81b-0073de3c9a7f", QueryOption.WaitingTasks).Count.ToString();

        Label8.Text = printList(StorageSystem.getInstance().getWorkDetails("5df16977-d18e-4a0a-b81b-0073de3c9a7f", QueryOption.ActiveTasks));
        Label9.Text = printList(StorageSystem.getInstance().getWorkDetails("5df16977-d18e-4a0a-b81b-0073de3c9a7f", QueryOption.WaitingTasks));
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Session["Task"] != null)
        {
            TaskStatus task = (TaskStatus)Session["Task"];
            task.setTaskStatus(Status.Idle);
            StorageSystem.getInstance().changeWorkDetails(task);
            Page_Load(null, null);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Session["Task"] != null)
        {
            TaskStatus task = (TaskStatus)Session["Task"];
            task.setTaskStatus(Status.Waiting);
            StorageSystem.getInstance().changeWorkDetails(task);
            Page_Load(null, null);
        }
    }
}
