using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections;

namespace MvcApplication
{
    public partial class Adapter_Practice_1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGridData();
            }
        }

        public void BindGridData()
        {
            using (SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.CommandText = "select * from Department; Select * from Employee; ";
                da.SelectCommand.Connection = sqlCon;
                DataSet ds = new DataSet();
                da.Fill(ds);
                //gvEmployee.DataSource = ds.Tables["Table"];
                //gvEmployee.DataSource = ds.Tables["Table1"];
                //gvEmployee.DataSource = ds.Tables[0];
                //gvEmployee.DataSource = ds.Tables[1];
                ds.Tables[0].TableName = "Department";
                ds.Tables[1].TableName = "Employee";
                gvEmployee.DataSource = ds.Tables["Employee"];
                //gvEmployee.DataSource = ds.Tables["Department"];
                gvEmployee.DataBind();
            }
        }

        protected void gvEmployee_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmployee.EditIndex = e.NewEditIndex;
            BindGridData();
        }

        protected void gvEmployee_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEmployee.EditIndex = -1;
            BindGridData();
        }

        protected void gvEmployee_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from employee", sqlcon);
                DataSet ds = new DataSet();
                da.Fill(ds);
                SqlCommandBuilder scb = new SqlCommandBuilder(da);
                //da.UpdateCommand = new SqlCommand();
                //da.UpdateCommand.CommandText = "Update employee firstname='" + e.NewValues["FirstName"] + "', lastname='"+e.NewValues["LastName"]+"', age="+e.NewValues["Age"]+" where Id="+e.NewValues["Id"];
                ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["Id"] };
                string FirstName=e.NewValues["FirstName"].ToString();
                int Id = Convert.ToInt32(e.RowIndex);
                ds.Tables[0].Rows[Id]["FirstName"] = e.NewValues["FirstName"].ToString();
                ds.Tables[0].Rows[Id]["LastName"] = e.NewValues["LastName"].ToString();
                ds.Tables[0].Rows[Id]["Age"] = e.NewValues["Age"].ToString();
                da.Update(ds);
                gvEmployee.EditIndex = -1;
                BindGridData();
            }
            BindGridData();
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
                object obj = e.Values["Id"];
                (ds).Tables[0].Rows.Find(e.Values["Id"]).Delete();
                da.Update(ds);
            }
            BindGridData();
        }

        protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEmployee.PageIndex = e.NewPageIndex;
            BindGridData();
        }
    }
}