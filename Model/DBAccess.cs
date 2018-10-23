using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace MvcApplication.Model
{
    public abstract class DBAccess<T>
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
        private SqlCommand cmd = new SqlCommand();
        private SqlConnection SqlCon = new SqlConnection();
        private SqlDataReader sdr;

        protected string StoreProcedureName 
        { 
            set
            {
                cmd.CommandText = value;
            }
        }

        

        public DBAccess()
        {
            this.SqlCon.ConnectionString = connectionString;
            this.cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = SqlCon;
        }

        protected void Open()
        {
            this.SqlCon.Open();
        }

        protected void Close()
        {
            this.SqlCon.Close();
        }

        protected SqlDataReader ExecuteReader()
        {
            try
            {
                Open();
                this.sdr = this.cmd.ExecuteReader();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return sdr;
        }

        protected void AddParameters(string Key, string value)
        {
            SqlParameter parameter = new SqlParameter(Key, value);
            this.cmd.Parameters.Add(parameter);
        }

        //Abstract Method
        public abstract IEnumerable<T> GetAll { get; }

        public abstract T GetById(int Id);
    }
}