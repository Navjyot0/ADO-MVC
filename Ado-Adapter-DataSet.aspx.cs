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
    public partial class Ado_Adapter_DataSet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                getEmployees();
        }

        protected void btnSerch_Click(object sender, EventArgs e)
        {
            searchEmployeeByFirstOrLastName();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AddEmployee();
        }
        
        public void getEmployees()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.CommandText = "Select * from Employee";
                da.SelectCommand.Connection = con;
                DataSet ds = new DataSet();
                da.Fill(ds);
                ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["Id"] };
                Cache.Insert("EmployeeData", ds, null, DateTime.Now.AddHours(12), System.Web.Caching.Cache.NoSlidingExpiration);
                gvEmployee.DataSource = ds;
                gvEmployee.DataBind();
            }
        }

        public void searchEmployeeByFirstOrLastName()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.CommandText = "Select * from Employee where firstname like '%'+@FirstName+'%' or lastname like '%'+@LastName+'%'";
                da.SelectCommand.Parameters.AddWithValue("@FirstName", txtSearch.Text);
                da.SelectCommand.Parameters.AddWithValue("@LastName", txtSearch.Text);
                da.SelectCommand.Connection = con;
                DataSet ds = new DataSet();
                da.Fill(ds);
                gvEmployee.DataSource = ds;
                gvEmployee.DataBind();
            }
        }

        public void AddEmployee()
        {
            using(SqlConnection con =new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from employee", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                SqlCommandBuilder cb=new SqlCommandBuilder(da);
                DataRow dr = ds.Tables[0].NewRow();
                dr["FirstName"] = txtFirst.Text;
                dr["LastName"] = txtLastName.Text;
                dr["Age"] = txtAge.Text;
                ds.Tables[0].Rows.Add(dr);
                da.Update(ds);
            }
            getEmployees();
        }

        protected void gvEmployee_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from employee", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["Id"] };
                (ds).Tables[0].Rows.Find(e.Keys["Id"]).Delete();
                da.Update(ds);
            }
            getEmployees();
        }

        public void getDataFromCache()
        {
            if (Cache["EmployeeData"] != null)
            {
                gvEmployee.DataSource = (DataSet)Cache["EmployeeData"];
                gvEmployee.DataBind();
            }
            else
            {
                getEmployees();
            }
        }

    }
}