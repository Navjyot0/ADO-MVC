using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace MvcApplication.Views
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("select * from Employee where FirstName like '%'+@Fname+'%' or LastName like '%'+@Lname+'%'", con);
                cmd.Parameters.AddWithValue("@Fname", txtSearch.Text.Trim());
                cmd.Parameters.AddWithValue("@Lname", txtSearch.Text.Trim());
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                GridView1.DataSource = rdr;
                GridView1.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                //SqlCommand cmd = new SqlCommand("Insert into Employee (firstname, lastname, age ) values (@Fname, @Lname, @Age)", con);
                SqlCommand cmd = new SqlCommand("AddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@FirstName", txtFirstName.Text.Trim()));
                cmd.Parameters.Add(new SqlParameter("@LastName", txtLastName.Text.Trim()));
                cmd.Parameters.Add(new SqlParameter("@Age", txtAge.Text.Trim()));
                SqlParameter outputParameter = new SqlParameter();
                outputParameter.ParameterName = "@Id";
                outputParameter.SqlDbType = SqlDbType.Int;
                outputParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParameter);
                con.Open();
                int EmpId=cmd.ExecuteNonQuery();
                Response.Write("<script>alert('Added New Employee with Id : " + outputParameter.Value.ToString() + "')</script>");
                LoadGrid();
            }
        }

        public void LoadGrid()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Select * from Employee", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                GridView1.DataSource = rdr;
                GridView1.DataBind();
            }
            LoadTowTableData();
            GetDataWithTable();
        }

        protected void btnTruncate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("truncate table Employee", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                LoadGrid();
            }
        }

        protected void LoadTowTableData()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                con.Open();
                cmd.CommandText = "select * from Employee; Select * from Department;";
                cmd.Connection = con;
                SqlDataReader dr = cmd.ExecuteReader();
                gvEmployee.DataSource = dr;
                gvEmployee.DataBind();
                dr.NextResult();
                gvDepartment.DataSource = dr;
                gvDepartment.DataBind();
            }
        }

        protected void GetDataWithTable()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("select * from Department", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable tblDepartment = new DataTable();
                tblDepartment.Columns.Add("Id");
                tblDepartment.Columns.Add("Name");
                while (dr.Read())
                {
                    DataRow dataRow = tblDepartment.NewRow();
                    dataRow["Id"] = dr["Id"];
                    dataRow["Name"] = dr["DepartmentName"];
                    tblDepartment.Rows.Add(dataRow);
                }

                gvTable.DataSource = tblDepartment;
                gvTable.DataBind();
            }
        }
    }
}