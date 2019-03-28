using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Employee
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /* (Request.QueryString["uid"] == null && txtUserId.Text == "")
               {
                 Response.Redirect("~/Login.aspx");
           }*/
            if (!IsPostBack)
            {
                if (Request.QueryString["uid"] != null)
                {
                    if (!IsPasswordResetLinkValid())
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = "Password Reset link has expired or is invalid";
                    }
                    trCurrentPassword.Visible = false;
                }
                else if (User.Identity.Name != "")
                {
                    trCurrentPassword.Visible = true;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //if ((Request.QueryString["uid"] != null && ChangeUserPassword()) ||
            //            (User.Identity.Name != "" && ChangeUserPasswordUsingCurrentPassword()))
            if ((Request.QueryString["uid"] != null && ChangeUserPassword()) ||
                         ChangeUserPasswordUsingCurrentPassword())
                {
                lblMessage.Text = "Password Changed Successfully!";
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                if (trCurrentPassword.Visible)
                {
                    lblMessage.Text = "Invalid Current Password!";
                }
                else
                {
                    lblMessage.Text = "Password Reset link has expired or is invalid";
                }
            }
        }

        private bool ExecuteSP(string SPName, List<SqlParameter> SPParameters)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand(SPName, con);
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter parameter in SPParameters)
                {
                    cmd.Parameters.Add(parameter);
                }

                con.Open();
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }

        private bool IsPasswordResetLinkValid()
        {
            List<SqlParameter> paramList = new List<SqlParameter>()
    {
        new SqlParameter()
        {
            ParameterName = "@GUID",
            Value = Request.QueryString["uid"]
        }
    };

            return ExecuteSP("spIsPasswordResetLinkValid", paramList);
        }

        private bool ChangeUserPassword()
        {
            List<SqlParameter> paramList = new List<SqlParameter>()
    {
        new SqlParameter()
        {
            ParameterName = "@GUID",
            Value = Request.QueryString["uid"]
        },
        new SqlParameter()
        {
            ParameterName = "@Password",
            Value = txtNewPassword.Text
           // Value = FormsAuthentication.HashPasswordForStoringInConfigFile(txtNewPassword.Text, "SHA1")
        }
    };

            return ExecuteSP("spChangePassword", paramList);
        }

        private bool ChangeUserPasswordUsingCurrentPassword()
        {
            List<SqlParameter> paramList = new List<SqlParameter>()
    {
        new SqlParameter()
        {
            ParameterName = "@UserName",
            Value = txtUserId.Text
        },
        new SqlParameter()
        {
            ParameterName = "@CurrentPassword",
            Value = txtCurrentPassword.Text
            //Value = FormsAuthentication.HashPasswordForStoringInConfigFile(txtCurrentPassword.Text, "SHA1")
        },
        new SqlParameter()
        {
            ParameterName = "@NewPassword",
            Value = txtNewPassword.Text
            //Value = FormsAuthentication.HashPasswordForStoringInConfigFile(txtNewPassword.Text, "SHA1")
        }
    };

            return ExecuteSP("spChangePasswordUsingCurrentPassword", paramList);
        }
    }

}