using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrawlerNameSpace.StorageSystem;
using CrawlerNameSpace.Utilities;

public partial class CategoryManager : System.Web.UI.Page
{
    private string getCategoryName(List<Category> list, String id)
    {
        if (id == null || id == "") return "root";
        foreach (Category cat in list)
        {
            if (cat.getCategoryID().Trim() == id)
                return cat.getCatrgoryName();
        }
        return "root";
    }
    private string getCategoryID(List<Category> list, String name)
    {
        foreach (Category cat in list)
        {
            if (cat.getCatrgoryName().Trim() == name)
                return cat.getCategoryID();
        }
        return "";
    }
    private void resetDefaults()
    {
        DropDownList1.Items.Clear();
        DropDownList2.Items.Clear();
        ListBox2.Items.Clear();

        TaskStatus ts = validateTask();
        if (ts != null)
        {
            DropDownList1.Items.Add("root");
            List<Category> cats = StorageSystem.getInstance().getCategories(ts.getTaskID().Trim());
            List<Category> previewCats = new List<Category>();
            foreach (Category cat in cats)
            {
                string pname = getCategoryName(cats, cat.getParentName());
                Category newCat = new Category(cat.getCategoryID(), pname, cat.getCatrgoryName(), cat.getKeywordList(), cat.getConfidenceLevel());
                previewCats.Add(newCat);
            }
            Session["CategoryList"] = previewCats;

            foreach (Category cat in previewCats)
            {
                DropDownList1.Items.Add(cat.getCatrgoryName().Trim());
                DropDownList2.Items.Add(cat.getCatrgoryName().Trim());
                ListBox2.Items.Add(cat.getCatrgoryName().Trim());
            }
            DropDownList2_SelectedIndexChanged(null, null);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = "Category Manager";
        LabelUIVersion.Text = Version.getUIVersion();
        LabelEngineVersion.Text = Version.getCoreVersion();
        Label6.Text = "";
        Label7.Text = "";
        
        if (!Page.IsPostBack)
        {
            resetDefaults();
        }
    }

    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (TextBox5.Text != "")
        {
            List<Category> list = (List<Category>)Session["CategoryList"];
            int catIndex = getCategoryIndex(list, DropDownList2.SelectedValue);
            if (catIndex != -1)
            {
                Category oldCat = list[catIndex];
                List<string> keywords = oldCat.getKeywordList();
                keywords.Add(TextBox5.Text.ToLower());
                Category newCat = new Category("", oldCat.getParentName().Trim(), oldCat.getCatrgoryName(), keywords, oldCat.getConfidenceLevel());
                list.RemoveAt(catIndex);
                list.Add(newCat);
                DropDownList2_SelectedIndexChanged(null, null);
                ListBox2_SelectedIndexChanged(null, null);
                TextBox5.Text = "";
            }
        }
    }
    protected void FormView1_PageIndexChanging(object sender, FormViewPageEventArgs e)
    {

    }

    private List<Category> getSons(List<Category> list, string catName)
    {
        List<Category> sons = new List<Category>();
        if (list != null)
        {
            foreach (Category cat in list)
            {
                if (cat.getParentName().Trim() == catName.Trim()) sons.Add(cat);
            }
        }
        return sons;
    }

    protected void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["CategoryList"] != null && ListBox2.SelectedIndex != -1)
        {
            List<Category> list = (List<Category>)Session["CategoryList"];
            Category cat = getCategory(list, ListBox2.SelectedValue);
            if (cat != null)
            {
                Label2.Text = cat.getParentName().Trim();
                Label3.Text = cat.getConfidenceLevel().ToString();
                Label4.Text = cat.getKeywordList().Count.ToString();

                Label8.Text = "";
                int cnt = 0;
                foreach (string key in cat.getKeywordList())
                {
                    if (cnt == cat.getKeywordList().Count - 1)
                    {
                        Label8.Text = Label8.Text + key + ". ";
                    }
                    else
                    {
                        Label8.Text = Label8.Text + key + ", ";
                    }
                    cnt++;
                }
                Label5.Text = getSons(list, cat.getCatrgoryName()).Count.ToString();
            }
        }
        else
        {
            Label2.Text = "None";
            Label3.Text = "0";
            Label4.Text = "0";
            Label8.Text = "No Keywords";
            Label5.Text = "0";
        }
    }
    private bool validateName(List<Category> list, string name)
    {
        foreach (Category cat in list)
        {
            if (cat.getCatrgoryName().Trim() == name)
                return false;
        }
        return true;
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
        int errCnt = 0;
        if (TextBox1.Text == "")
        {
            Label6.Text = "*";
            errCnt++;
        }
        if (TextBox4.Text == "")
        {
            Label7.Text = "*";
            errCnt++;
        }
        if (errCnt != 0) return;

        if (Session["CategoryList"] != null)
        {
            List<Category> list = (List<Category>)Session["CategoryList"];
            if (validateName(list, TextBox1.Text) == false)
            {
                Label6.Text = "exists";
                return;
            }

            Category cat = new Category("", DropDownList1.SelectedValue, TextBox1.Text, new List<string>(), Convert.ToInt32(TextBox4.Text));
            
            DropDownList1.Items.Add(cat.getCatrgoryName());
            DropDownList2.Items.Add(cat.getCatrgoryName());
            ListBox2.Items.Add(cat.getCatrgoryName());
            list.Add(cat);

            ListBox2_SelectedIndexChanged(null, null);
        }
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }

    private int getCategoryIndex(List<Category> list, string category)
    {
        int cnt = 0;
        if (list != null)
        {
            foreach (Category cat in list)
            {
                if (cat.getCatrgoryName().Trim() == category) return cnt;
                cnt++;
            }
        }
        return -1;
    }

    private Category getCategory(List<Category> list, string category)
    {
        foreach (Category cat in list)
        {
            if (cat.getCatrgoryName().Trim() == category) return cat;
        }
        return null;
    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListBox1.Items.Clear();

        if (Session["CategoryList"] != null)
        {
            if (DropDownList2.SelectedIndex != -1)
            {
                List<Category> list = (List<Category>)Session["CategoryList"];
                Category cat = getCategory(list, DropDownList2.SelectedValue);
                if (cat != null)
                {
                    foreach (string key in cat.getKeywordList())
                    {
                        ListBox1.Items.Add(key);
                    }
                }
            }
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditTask.aspx?TID=" + Request["TID"]);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (ListBox1.SelectedValue != null)
        {
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex);

            List<Category> list = (List<Category>)Session["CategoryList"];
            int catIndex = getCategoryIndex(list, DropDownList2.SelectedValue);
            if (catIndex != -1)
            {
                Category oldCat = list[catIndex];

                List<string> keywords = new List<string>();
                foreach (ListItem item in ListBox1.Items)
                {
                    keywords.Add(item.Text);
                }
                Category newCat = new Category("", oldCat.getParentName().Trim(), oldCat.getCatrgoryName(), keywords, oldCat.getConfidenceLevel());
                list.RemoveAt(catIndex);
                list.Add(newCat);
                ListBox2_SelectedIndexChanged(null, null);
            }
        }
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        resetDefaults();
        ListBox2_SelectedIndexChanged(null, null);
    }
    private void remove(List<Category> cats, string cat)
    {
        List<Category> sons = getSons(cats, cat);
        foreach (Category son in sons)
        {
            remove(cats, son.getCatrgoryName().Trim());
        }
        int catIndex = getCategoryIndex(cats, cat);
        if (catIndex != -1)
        {
            DropDownList1.Items.RemoveAt(DropDownList1.Items.IndexOf(new ListItem(cat)));
            DropDownList2.Items.RemoveAt(DropDownList2.Items.IndexOf(new ListItem(cat)));
            ListBox2.Items.RemoveAt(ListBox2.Items.IndexOf(new ListItem(cat)));
            cats.RemoveAt(catIndex);
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (ListBox2.SelectedValue != null)
        {
            List<Category> list = (List<Category>)Session["CategoryList"];
            remove(list, ListBox2.SelectedValue);
            DropDownList2_SelectedIndexChanged(null, null);
            ListBox2_SelectedIndexChanged(null, null);
        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        if (ListBox2.SelectedValue != null)
        {
            List<Category> list = (List<Category>)Session["CategoryList"];
            // moving sons to father
            Category category = getCategory(list, ListBox2.SelectedValue);
            List<Category> newList = new List<Category>();
            foreach (Category cat in list)
            {
                if (cat.getParentName().Trim() == category.getCatrgoryName())
                {
                    Category newCat = new Category(cat.getCategoryID(),
                        category.getParentName(),
                        cat.getCatrgoryName(),
                        cat.getKeywordList(),
                        cat.getConfidenceLevel());
                    newList.Add(newCat);
                }
                else
                {
                    Category newCat = new Category(cat.getCategoryID(),
                        cat.getParentName(),
                        cat.getCatrgoryName(),
                        cat.getKeywordList(),
                        cat.getConfidenceLevel());
                    newList.Add(newCat);
                }
            }
            Session["CategoryList"] = newList;
            remove(newList, ListBox2.SelectedValue);
            DropDownList2_SelectedIndexChanged(null, null);
            ListBox2_SelectedIndexChanged(null, null);
        }
    }

    private void mergeWithFatherKeywords(List<string> fkeys, List<string> skeys)
    {
        foreach (string key in skeys)
        {
            if (fkeys.Contains(key) != true) fkeys.Add(key);
        }
    }

    private void forwardKeywords(List<Category> list, string father)
    {
        int index = getCategoryIndex(list, father);
        if (index != -1 || father == "root")
        {
            List<Category> sons = getSons(list, father);
            foreach (Category cat in sons)
            {
                forwardKeywords(list, cat.getCatrgoryName().Trim());
            }

            if (index != -1)
            {
                List<string> fkeys = list[index].getKeywordList();
                foreach (Category son in sons)
                {
                    mergeWithFatherKeywords(fkeys, son.getKeywordList());
                }

                Category oldCat = list[index];
                Category newCat = new Category(oldCat.getCategoryID(), oldCat.getParentName(), oldCat.getCatrgoryName(), fkeys, oldCat.getConfidenceLevel());
                list[index] = newCat;
            }
        }
    }

    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {

    }
    private List<String> removeDuplicates(List<String> oldList)
    {
        List<String> newList = new List<string>();
        foreach (String word in oldList)
        {
            if (newList.Contains(word) == false) newList.Add(word);
        }
        return newList;
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        TaskStatus ts = validateTask();
        if (Session["CategoryList"] != null && ts != null)
        {
            List<Category> list = (List<Category>)Session["CategoryList"];
            if(CheckBox2.Checked) forwardKeywords(list, "root");

            List<Category> initalList = new List<Category>();
            foreach (Category cat in list)
            {
                List<String> keywords = cat.getKeywordList();
                if (CheckBox2.Checked) keywords = removeDuplicates(cat.getKeywordList());
                Category newCat = new Category("", "", cat.getCatrgoryName().Trim(), keywords, cat.getConfidenceLevel());
                initalList.Add(newCat);
            }
            StorageSystem.getInstance().setCategories(ts.getTaskID().Trim(), initalList);
            List<Category> dbList = StorageSystem.getInstance().getCategories(ts.getTaskID().Trim());
            foreach (Category cat in list)
            {
                string pid = getCategoryID(dbList, cat.getParentName());
                string id = getCategoryID(dbList, cat.getCatrgoryName());
                StorageSystem.getInstance().setParentToSon(pid, id);
            }
        }
    }
}
