using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Employee
{
    public partial class AddEmployee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_Error(object sender, EventArgs e)
        {
            // Get the exception details and log it in the database or event viewer
            Exception ex = Server.GetLastError();
            // Clear the exception
            Server.ClearError();
            // Redirect user to Error page
            Response.Redirect("~/error.aspx");
        }

        private void redirectToEmployeeList()
        {
            
                Response.Redirect("~/Employee.aspx");
            
            

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand comand = new SqlCommand("AddEmployee", con);
                comand.CommandType = System.Data.CommandType.StoredProcedure;
                comand.Parameters.AddWithValue("@Name", txtEmployeeName.Text);
                comand.Parameters.AddWithValue("@GenderID", ddlGender.SelectedValue);
                comand.Parameters.AddWithValue("@Salary", txtSalary.Text);

                SqlParameter output = new SqlParameter();
                output.ParameterName = "@Id";
                output.SqlDbType = System.Data.SqlDbType.Int;
                output.Direction = System.Data.ParameterDirection.Output;

                comand.Parameters.Add(output);

                con.Open();
                comand.ExecuteNonQuery();

                string EmployeeId = output.Value.ToString();
                lblMessage.Text = "Employee Id = " + EmployeeId;
                redirectToEmployeeList();
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            redirectToEmployeeList();
        }
    }
}