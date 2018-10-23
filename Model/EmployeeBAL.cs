using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace MvcApplication.Model
{
    public class EmployeeBAL : DBAccess<Employee>
    {
        public override IEnumerable<Employee> GetAll
        {
            get
            {
                List<Employee> employees = new List<Employee>();
                try
                {
                    StoreProcedureName = "getAllEmployees";
                    SqlDataReader dr = ExecuteReader();
                    while (dr.Read())
                    {
                        Employee employee = new Employee();
                        employee.Id = Convert.ToInt32(dr["Id"]);
                        employee.FirstName = dr["FirstName"].ToString();
                        employee.LastName = dr["LastName"].ToString();
                        employee.Age = Convert.ToByte(dr["Age"]);
                        employees.Add(employee);
                    }
                }
                catch
                {

                }
                finally
                {
                    Close();
                }
                return employees;
            }
        }

        public override Employee GetById(int Id)
        {
            Employee employee = new Employee();
            try
            {
                StoreProcedureName = "getEmployeesById";
                AddParameters("@Id", Id.ToString());
                SqlDataReader dr = ExecuteReader();
                dr.Read();
                employee.Id = Convert.ToInt32(dr["Id"]);
                employee.FirstName = dr["FirstName"].ToString();
                employee.LastName = dr["LastName"].ToString();
                employee.Age = Convert.ToByte(dr["Age"]);
            }
            catch
            {

            }
            finally
            {
                Close();
            }
            return employee;
        }
    }
}