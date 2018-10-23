using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace MvcApplication
{
    public partial class Disconnected_ADO_With_Cache : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //    BindDataFromDB(); 
        }

        protected void gvEmployee_RowEditing(object sender, GridViewEditEventArgs e)
        {
            btnUpdate.Enabled = false;
            gvEmployee.EditIndex = e.NewEditIndex;
            BindDataFromCache();
        }

        protected void gvEmployee_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (Cache["Employee"] != null)
            {
                DataSet ds = (DataSet)Cache["Employee"];
                ds.Tables["Employee"].Rows.Find(e.Keys["Id"]).Delete();
                Cache.Insert("Employee", ds, null, DateTime.Now.AddHours(12), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            BindDataFromCache();
        }

        protected void gvEmployee_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEmployee.EditIndex = -1;
            BindDataFromCache();
            btnUpdate.Enabled = true;
        }

        protected void gvEmployee_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Cache["Employee"] != null)
            {
                DataSet ds = (DataSet)Cache["Employee"];
                DataRow dr = ds.Tables["Employee"].Rows.Find(e.Keys["Id"]);
                dr["FirstName"] = e.NewValues["FirstName"];
                dr["LastName"] = e.NewValues["LastName"];
                dr["Age"] = e.NewValues["Age"];
                Cache.Insert("Employee", ds, null, DateTime.Now.AddHours(12), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            gvEmployee.EditIndex = -1;
            BindDataFromCache();
            btnUpdate.Enabled = true;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateDBTable();
        }

        protected void btnGetFromCache_Click(object sender, EventArgs e)
        {
            BindDataFromCache();
        }

        protected void btnGetFromDB_Click(object sender, EventArgs e)
        {
            BindDataFromDB();
        }

        protected void btnClearCache_Click(object sender, EventArgs e)
        {
            Cache.Remove("Employee");
            lblMessage.Text = "Cache Cleared";
            lblMessage.ForeColor = System.Drawing.Color.Green;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Cache["Employee"] != null)
            {
                DataSet ds = (DataSet)Cache["Employee"];
                int lastId = Convert.ToInt32(ds.Tables["Employee"].Rows[ds.Tables["Employee"].Rows.Count - 1]["Id"]);
                DataRow dr = ds.Tables["Employee"].NewRow();
                dr["Id"] = lastId + 1;
                dr["FirstName"] = txtFirst.Text;
                dr["LastName"] = txtLastName.Text;
                dr["Age"] = Convert.ToInt16(txtAge.Text);
                ds.Tables["Employee"].Rows.Add(dr);
                Cache.Insert("Employee", ds, null, DateTime.Now.AddHours(12), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            BindDataFromCache();
        }

        public void BindDataFromDB()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from employee", con);
                DataSet ds = new DataSet();
                da.Fill(ds, "Employee");
                ds.Tables["Employee"].PrimaryKey = new DataColumn[] { ds.Tables["Employee"].Columns["Id"] };
                Cache["Employee"] = ds;
                gvEmployee.DataSource = ds;
                gvEmployee.DataBind();
                lblMessage.Text = "Data Loaded From DB Table";
            }

        }

        public void BindDataFromCache()
        {
            if (Cache["Employee"] != null)
            {
                DataSet ds = (DataSet)Cache["Employee"];
                gvEmployee.DataSource = ds;
                gvEmployee.DataBind();
                lblMessage.Text = "Data Loaded From Cache";
            }
            else
            {
                gvEmployee.DataSource = null;
                gvEmployee.DataBind();
                lblMessage.Text = "Cache is empty";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        public void UpdateDBTable()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.CommandText = "select * from employee";
                da.SelectCommand.Connection = con;
                SqlCommandBuilder cb = new SqlCommandBuilder();
                cb.DataAdapter = da;
                DataSet ds = (DataSet)Cache["Employee"];
                da.Update(ds, "Employee");
                BindDataFromDB();
            }
        }

        protected void btnUndo_Click(object sender, EventArgs e)
        {
            DataSet ds = (DataSet)Cache["Employee"];
            ds.RejectChanges();
            Cache.Insert("Employee", ds, null, DateTime.Now.AddHours(12), System.Web.Caching.Cache.NoSlidingExpiration);
            BindDataFromCache();
        }
    }
}