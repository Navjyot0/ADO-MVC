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
    public partial class ADO_Practice_2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindDataByReader();
            if (!Page.IsPostBack)
                BindDeptWithAdapter_ddl();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            CreateStudentByAdapterWithBuilder();
        }

        public void BindDataWithAdapter()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = con;
                da.SelectCommand.CommandText = "Select * from employee";
                DataSet ds = new DataSet();
                da.Fill(ds);
                gvEmployee.DataSource = ds.Tables[0];
                gvEmployee.DataBind();
            }
        }

        public void BindDataByReader()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NathanArk"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * from getStudentDept";
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                gvEmployee.DataSource = dr;
                gvEmployee.DataBind();
            }
        }

        public void BindDeptWithAdapter_ddl()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NathanArk"].ConnectionString))
            {
                //With Adapter
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = con;
                da.SelectCommand.CommandText = "select * from department";
                DataSet ds = new DataSet();
                da.Fill(ds);
                ddlDepartment.DataSource = ds.Tables[0];
                ddlDepartment.DataTextField = "department";
                ddlDepartment.DataValueField = "deptId";
                ddlDepartment.DataBind();
            }
        }

        public void BindDeptWithReader_ddl()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NathanArk"].ConnectionString))
            {
                //with Reader
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from department";
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                ddlDepartment.DataSource = dr;
                ddlDepartment.DataTextField = "department";
                ddlDepartment.DataValueField = "deptId";
                ddlDepartment.DataBind();
            }
        }

        public void CreateStudentByAdapterWithoutBuilder()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NathanArk"].ConnectionString))
            {
                //DataAdapter without SqlCommandBuilder
                SqlDataAdapter da = new SqlDataAdapter("select * from student", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                SqlCommand InsertCommand = new SqlCommand();
                InsertCommand.Connection = con;
                InsertCommand.CommandText = "Insert into student(Name, Age, City, Fees, DeptId) values (@Name, @Age, @City, @Fees, @DeptId)";
                InsertCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100, "Name"));
                InsertCommand.Parameters.Add(new SqlParameter("@Age", SqlDbType.TinyInt, 0, "Age"));
                InsertCommand.Parameters.Add(new SqlParameter("@City",SqlDbType.NVarChar, 100, "City"));
                InsertCommand.Parameters.Add(new SqlParameter("@Fees", SqlDbType.Money, 0,  "Fees"));
                InsertCommand.Parameters.Add(new SqlParameter("@DeptId", SqlDbType.Int, 0, "DeptId"));
                da.InsertCommand = InsertCommand;
                DataRow dr=ds.Tables[0].NewRow();
                dr["Name"] = txtName.Text;
                dr["Age"] = txtAge.Text;
                dr["City"] = txtCity.Text;
                dr["Fees"] = txtFees.Text;
                dr["DeptId"] = ddlDepartment.SelectedValue;
                ds.Tables[0].Rows.Add(dr);
                da.Update(ds);
            }
        }

        public void CreateStudentByAdapterWithBuilder()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NathanArk"].ConnectionString))
            {
                //DataAdapter with SqlCommandBuilder
                SqlDataAdapter da = new SqlDataAdapter("select * from student", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                //SqlCommand InsertCommand = new SqlCommand();
                //InsertCommand.Connection = con;
                //InsertCommand.CommandText = "Insert into student(Name, Age, City, Fees, DeptId) values (@Name, @Age, @City, @Fees, @DeptId)";
                //InsertCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100, "Name"));
                //InsertCommand.Parameters.Add(new SqlParameter("@Age", SqlDbType.TinyInt, 0, "Age"));
                //InsertCommand.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, 100, "City"));
                //InsertCommand.Parameters.Add(new SqlParameter("@Fees", SqlDbType.Money, 0, "Fees"));
                //InsertCommand.Parameters.Add(new SqlParameter("@DeptId", SqlDbType.Int, 0, "DeptId"));
                //da.InsertCommand = InsertCommand;
                SqlCommandBuilder scb = new SqlCommandBuilder(da);
                DataRow dr = ds.Tables[0].NewRow();
                dr["Name"] = txtName.Text;
                dr["Age"] = txtAge.Text;
                dr["City"] = txtCity.Text;
                dr["Fees"] = txtFees.Text;
                dr["DeptId"] = ddlDepartment.SelectedValue;
                ds.Tables[0].Rows.Add(dr);
                da.Update(ds);
                BindDataByReader();
            }
        }

        public void CreateStudentByReader()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NathanArk"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Insert into student(Name, Age, City, Fees, DeptId) values (@Name, @Age, @City, @Fees, @DeptId)";
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Age", txtAge.Text);
                cmd.Parameters.AddWithValue("@City", txtCity.Text);
                cmd.Parameters.AddWithValue("@Fees", txtFees.Text);
                cmd.Parameters.Add(new SqlParameter("@DeptId", ddlDepartment.SelectedValue));
                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                    BindDataByReader();
                else
                    Response.Write("Please try again...");
            }
        }

        protected void gvEmployee_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Delete_With_SqlCommand_Reader(Convert.ToInt32(e.Values["Id"]));
            Delete_By_Adapter_With_SqlCommandBilder(Convert.ToInt32(e.Values["Id"]));
            BindDataByReader();
        }

        protected void gvEmployee_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmployee.EditIndex = e.NewEditIndex;
            BindDataByReader();
        }

        protected void gvEmployee_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
        }

        protected void gvEmployee_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEmployee.EditIndex = -1;
            BindDataByReader();
        }

        public void Delete_With_SqlCommand_Reader(int Id)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NathanArk"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Delete from student where studentid=" + Id;
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                    Response.Write("Deleted Record with Id:" + Id);
                else
                    Response.Write("Error, Please try again");
                con.Close();
            }
        }

        public void Delete_By_Adapter_Without_SqlCommandBilder(int Id)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NathanArk"].ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("Select * from Student", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                SqlCommand DeleteCommand = new SqlCommand();
                DeleteCommand.Connection = con;
                DeleteCommand.CommandText = "Delete from student where StudentId=@StudentId";
                DeleteCommand.Parameters.Add(new SqlParameter("@StudentId", SqlDbType.Int, 0, "StudentId"));
                da.DeleteCommand = DeleteCommand;
                ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["StudentId"] };
                ds.Tables[0].Rows.Find(Id).Delete();
                da.Update(ds);
            }
        }

        public void Delete_By_Adapter_With_SqlCommandBilder(int Id)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NathanArk"].ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from student", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                SqlCommandBuilder scb = new SqlCommandBuilder(da);
                ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["StudentId"] };
                ds.Tables[0].Rows.Find(Id).Delete();
                da.Update(ds);
            }
        }

        public void Update_With_SqlCommand_Reader()
        {

        }

        public void Update_By_Adapter_Without_SqlCommandBilder()
        {

        }

        public void Update_By_Adapter_With_SqlCommandBilder()
        {

        }

    }
}