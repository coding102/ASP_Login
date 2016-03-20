using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Services;
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

    [WebMethod]
    public static string CheckAnswer(string answer)
    {
        QueryHandler query = new QueryHandler();
        Boolean rightAnswer = query.CheckAnswer(answer);
        if (rightAnswer)
        {
            HttpContext.Current.Session["Verified"] = true;
        }
        return rightAnswer ? "true" : "false";
    }

    [WebMethod]
    public static string SendCodeInEmail(string email)
    {
        if (HttpContext.Current.Session["Email"] != null)
        {

            QueryHandler query = new QueryHandler();

            var fromAddress = new MailAddress("mark@gmail.com", "Mark Moral");
            var toAddress = new MailAddress(HttpContext.Current.Session["Email"].ToString(), " ");
            const string fromPassword = "ark";
            const string subject = "Password Reset";
            string resetCode = query.SetResetPasswordcode();
            string body = "This is the password reset code sent from scheduler.\n" + resetCode;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
            return "Reset code has been sent to your registered email address";
        }
        else
        {
            return "Did you try to log in first. Try it";
        }
    }
    //validateResetCode
    [WebMethod]
    public static string ValidateResetCode(string code)
    {
        QueryHandler query = new QueryHandler();
        Boolean rightCode = query.validateResetcode(code);
        if (rightCode)
        {
            HttpContext.Current.Session["Verified"] = true;
        }
        return rightCode ? "right" : "wront";
    }
}