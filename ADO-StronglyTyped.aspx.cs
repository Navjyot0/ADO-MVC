using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MvcApplication.EmployeeDataSetTableAdapters;

namespace MvcApplication
{
    public partial class ADO_StronglyTyped : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EmployeeTableAdapter employeeDataSetTableAdapters = new EmployeeTableAdapter();
            EmployeeDataSet.EmployeeDataTable eds = new EmployeeDataSet.EmployeeDataTable();
            employeeDataSetTableAdapters.Fill(eds);
            gvEmployee.DataSource = eds;
            gvEmployee.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                EmployeeTableAdapter employeeDataSetTableAdapters = new EmployeeTableAdapter();
                EmployeeDataSet.EmployeeDataTable eds = new EmployeeDataSet.EmployeeDataTable();
                employeeDataSetTableAdapters.Fill(eds);
                gvEmployee.DataSource = from dr in eds where dr.FirstName.Contains(txtSearch.Text) || dr.LastName.Contains(txtSearch.Text) select new { dr.LastName, dr.FirstName };
                gvEmployee.DataBind();
            }
            else
            {
                EmployeeTableAdapter employeeDataSetTableAdapters = new EmployeeTableAdapter();
                EmployeeDataSet.EmployeeDataTable eds = new EmployeeDataSet.EmployeeDataTable();
                employeeDataSetTableAdapters.Fill(eds);
                gvEmployee.DataSource = eds;
                gvEmployee.DataBind();
            }
        }
    }
}