using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrawlerNameSpace.StorageSystem;
using CrawlerNameSpace.Utilities;

public partial class CrawlerOptions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = "Crawler Options";
        LabelUIVersion.Text = Version.getUIVersion();
        LabelEngineVersion.Text = Version.getCoreVersion();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
