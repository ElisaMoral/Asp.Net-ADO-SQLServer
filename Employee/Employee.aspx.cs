using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace Employee
{
    public partial class Empleado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getDataFromDatabase();
                Label1.Text = "user "+ User.Identity.Name;

            }
        }

        
        private void getDataFromDatabase()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string selectQuery = "Select * from Employee";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, connection);

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "Employee");
            // Set ID column as the primary key
            dataSet.Tables["Employee"].PrimaryKey =
                new DataColumn[] { dataSet.Tables["Employee"].Columns["Id"] };
            // Store the dataset in Cache
            Cache.Insert("DATASET", dataSet, null, DateTime.Now.AddHours(24),
                System.Web.Caching.Cache.NoSlidingExpiration);

            GridView1.DataSource = dataSet;
            GridView1.DataBind();

           
        }

        private void GetDataFromCache()
        {
            if (Cache["DATASET"] != null)
            {
                GridView1.DataSource = (DataSet)Cache["DATASET"];
                GridView1.DataBind();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            getDataFromDatabase();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                // Retrieve dataset from cache
                DataSet dataSet = (DataSet)Cache["DATASET"];
                // Find datarow to edit using primay key
                DataRow dataRow = dataSet.Tables["Employee"].Rows.Find(e.Keys["Id"]);
                // Update datarow values
                dataRow["Name"] = e.NewValues["Name"];
                dataRow["GenderId"] = e.NewValues["GenderId"];
                dataRow["Salary"] = e.NewValues["Salary"];
                // Overwrite the dataset in cache
                Cache.Insert("DATASET", dataSet, null, DateTime.Now.AddHours(24),
                    System.Web.Caching.Cache.NoSlidingExpiration);
                // Remove the row from edit mode
                GridView1.EditIndex = -1;
                // Reload data to gridview from cache
                GetDataFromCache();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/error.aspx");
            }

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Set row in editing mode
            GridView1.EditIndex = e.NewEditIndex;
            GetDataFromCache();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetDataFromCache();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                DataSet dataSet = (DataSet)Cache["DATASET"];
                dataSet.Tables["Employee"].Rows.Find(e.Keys["Id"]).Delete();
                Cache.Insert("DATASET", dataSet, null, DateTime.Now.AddHours(24),
                System.Web.Caching.Cache.NoSlidingExpiration);
                GetDataFromCache();
            }
            catch (System.NullReferenceException NullReferenceException)
            {
                Response.Redirect("~/error.aspx");
            }
            catch (Exception ex)
            {
                Response.Redirect("~/error.aspx");
            }


        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cache["DATASET"] != null)
                {
                    string connectionString =
                    ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                    SqlConnection connection = new SqlConnection(connectionString);
                    string selectQuery = "Select * from Employee";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, connection);

                    // Update command to update database table
                    string strUpdateCommand = "Update Employee set Name = @Name, GenderId = @GenderId, Salary = @Salary where Id = @Id";
                    // Create an instance of SqlCommand using the update command created above
                    SqlCommand updateCommand = new SqlCommand(strUpdateCommand, connection);
                    // Specify the parameters of the update command
                    updateCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "Name");
                    updateCommand.Parameters.Add("@GenderId", SqlDbType.Int, 0, "GenderId");
                    updateCommand.Parameters.Add("@Salary", SqlDbType.Int, 0, "Salary");
                    updateCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                    // Associate update command with SqlDataAdapter instance
                    dataAdapter.UpdateCommand = updateCommand;

                    // Delete command to delete data from database table
                    string strDeleteCommand = "Delete from Employee where Id = @Id";
                    // Create an instance of SqlCommand using the delete command created above
                    SqlCommand deleteCommand = new SqlCommand(strDeleteCommand, connection);
                    // Specify the parameters of the delete command
                    deleteCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                    // Associate delete command with SqlDataAdapter instance
                    dataAdapter.DeleteCommand = deleteCommand;

                    // Update the underlying database table
                    dataAdapter.Update((DataSet)Cache["DATASET"], "Employee");

                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/error.aspx");
            }
        }

        

        protected void addEmployee_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/AddEmployee.aspx");
        }

        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Registration/ChangePassword");
        }
    }
    
}