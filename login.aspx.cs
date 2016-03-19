using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Email"] = null;
        Session["LoggedIn"] = "";
    }

    protected void btnProceed_Click(object sender, EventArgs e)
    {
        String email = txtEmail.Value;
        String password = txtPassword.Value;

        if (email == "" || password == "")
        {
            errorInfo.Text = "Please fill in the details";
        } else {
            Session["Email"] = email;
            QueryHandler query = new QueryHandler();
            Boolean userVerified = query.VerifyUser(email, password);

            if (!userVerified)
            {
                if (query.UserExists(email))
                {
                    txtResetEmail.Value = email;
                    divForgotPassword.Attributes.CssStyle.Add("display", "block");
                    lblSecurityQuestion.InnerHtml = query.GetSecurityQuestion(email);
                } else
                {
                    errorInfo.Text = "Scheduler could not recognize you. If you are a first time user please sign up";
                }
            }
            else
            {
                string userDetails = query.GetUserDetails(email, password);

                Session["Firstname"] = userDetails.Split(' ')[0];
                Session["Fullname"] = userDetails;
                Session["LoggedIn"] = "ture";
                Response.Redirect("usershub.aspx");
            }
        }
    }
}