using System;
using System.Configuration;
using System.Linq;
using MySql.Data.MySqlClient;

public class QueryHandler
{
    private string Email;

    public QueryHandler() {}
    public QueryHandler(String email)
    {
        this.Email = email;
    }
    //check if user exists in database
    #region
    public bool UserExists(string email)
    {
        //open Sql Connection
        string connectionString = ConfigurationManager.ConnectionStrings["mydatabaseConnectionString"].ConnectionString;
        MySqlConnection conn = new MySqlConnection(connectionString);
        MySqlCommand command = conn.CreateCommand();
        MySqlDataReader reader;

        try
        {
            conn.Open();
        }
        catch(Exception ex)
        {
            throw new ApplicationException("Connection can't be opened");
        }
        //write code to check if the user exists in the database
        command.CommandText = String.Format("Selext * from users where Email = \'{0}\'", email);
        reader = command.ExecuteReader();
        reader.Read();
        if (reader.HasRows)
        {
            reader.Close();
            conn.Close();
            return true;
        }
        reader.Close();
        conn.Close();
        return false;
    }
    #endregion

    //varifying user
    #region
    public bool VerifyUser(string email, string password)
    {
        //open Sql Connection
        string connectionString = ConfigurationManager.ConnectionStrings["mydatabaseConnectionString"].ConnectionString;
        MySqlConnection conn = new MySqlConnection(connectionString);
        MySqlCommand command = conn.CreateCommand();
        MySqlDataReader reader;

        try
        {
            conn.Open();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Connection can't be opened");
        }

        //write the coce to check if the user exists in the database
        command.CommandText = String.Format("Select * from users where Email = \'{0}' and Password =\'{1}'", email, password);
        reader = command.ExecuteReader();
        reader.Read();
        if (reader.HasRows)
        {
            reader.Close();
            conn.Close();
            return true;
        }
        reader.Close();
        conn.Close();
        return false;
    }

    public bool CheckAnswer(string answer)
    {
        throw new NotImplementedException();
    }
    #endregion

    public string GetSecurityQuestion(string email)

    {

        //Opening Sql Connection

        string connectionString =

            ConfigurationManager.ConnectionStrings["mydatabaseConnectionString"].ConnectionString;

        MySqlConnection conn = new MySqlConnection(connectionString);

        MySqlCommand command = conn.CreateCommand();

        MySqlDataReader reader;




        try

        {

            conn.Open();

        }

        catch (Exception ex)

        {

            throw new ApplicationException("Connection cant be opened.");

        }




        command.CommandText = String.Format("Select SecurityQuestion from users where Email = \'{0}\'",

                           email);

        reader = command.ExecuteReader();

        reader.Read();




        string securityQuestion = reader.GetValue(0).ToString();




        reader.Close();

        conn.Close();

        return securityQuestion;

    }










    public string GetUserDetails(string email, string password)

    {

        //Opening Sql Connection

        string connectionString =

            ConfigurationManager.ConnectionStrings["mydatabaseConnectionString"].ConnectionString;

        MySqlConnection conn = new MySqlConnection(connectionString);

        MySqlCommand command = conn.CreateCommand();

        MySqlDataReader reader;




        try

        {

            conn.Open();

        }

        catch (Exception ex)

        {

            throw new ApplicationException("Connection cant be opened.");

        }




        command.CommandText = String.Format("Select Fullname from users where Email = \'{0}\' and Password = \'{1}\'",

                           email, password);

        reader = command.ExecuteReader();

        reader.Read();




        string userDetails = reader.GetValue(0).ToString();




        reader.Close();

        conn.Close();

        return userDetails;

    }




    public string SetResetPasswordCode()

    {

        //Opening Sql Connection

        string connectionString =

            ConfigurationManager.ConnectionStrings["mydatabaseConnectionString"].ConnectionString;

        MySqlConnection conn = new MySqlConnection(connectionString);

        MySqlCommand command = conn.CreateCommand();

        MySqlDataReader reader;




        string response;

        try

        {

            conn.Open();

        }

        catch (Exception ex)

        {

            response = "Could not send email";

        }




        command.CommandText = String.Format("SET SQL_SAFE_UPDATES = 0");

        reader = command.ExecuteReader();

        reader.Close();

        response = RandomString(5, "0123456789");

        command.CommandText = String.Format("Update users set ResetPasswordCode = \'{0}\' where Email = \'{1}\'", response, HttpContext.Current.Session["Email"].ToString());

        reader = command.ExecuteReader();

        reader.Read();

        reader.Close();




        conn.Close();

        return response;

    }




    public bool ValidateResetCode(string code)

    {

        //Opening Sql Connection

        string connectionString =

            ConfigurationManager.ConnectionStrings["mydatabaseConnectionString"].ConnectionString;

        MySqlConnection conn = new MySqlConnection(connectionString);

        MySqlCommand command = conn.CreateCommand();

        MySqlDataReader reader;




        try

        {

            conn.Open();

        }

        catch (Exception ex)

        {

            throw new ApplicationException("Connection cant be opened.");

        }




        command.CommandText = String.Format("Select * from users where ResetPasswordCode = \'{0}\' and Email = \'{1}\'", code, HttpContext.Current.Session["Email"].ToString());

        reader = command.ExecuteReader();

        reader.Read();

        if (reader.HasRows)

        {

            reader.Close();

            conn.Close();

            return true;

        }

        reader.Close();

        conn.Close();

        return false;

    }




    //Get randomly generated string as a key for client and creator

    #region

    string RandomString(int length, string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")

    {

        const int byteSize = 0x100;

        var allowedCharSet = new HashSet<char>(allowedChars).ToArray();




        using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())

        {

            var result = new StringBuilder();

            var buf = new byte[128];

            while (result.Length < length)

            {

                rng.GetBytes(buf);

                for (var i = 0; i < buf.Length && result.Length < length; ++i)

                {

                    var outOfRangeStart = byteSize - (byteSize % allowedCharSet.Length);

                    if (outOfRangeStart <= buf[i]) continue;

                    result.Append(allowedCharSet[buf[i] % allowedCharSet.Length]);

                }

            }

            return result.ToString();

        }

    }

    #endregion




    public bool CheckAnswer(string answer)

    {

        //Opening Sql Connection

        string connectionString =

            ConfigurationManager.ConnectionStrings["mydatabaseConnectionString"].ConnectionString;

        MySqlConnection conn = new MySqlConnection(connectionString);

        MySqlCommand command = conn.CreateCommand();

        MySqlDataReader reader;




        try

        {

            conn.Open();

        }

        catch (Exception ex)

        {

            throw new ApplicationException("Connection cant be opened.");

        }




        command.CommandText = String.Format("Select UserId from users where Email = \'{0}\' and SecurityAnswer = \'{1}\'",

                           HttpContext.Current.Session["Email"].ToString(), answer);

        reader = command.ExecuteReader();

        reader.Read();




        if (reader.HasRows)

        {

            reader.Close();

            conn.Close();

            return true;

        }

        reader.Close();

        conn.Close();

        return false;

    }




    public bool Signup(string fullname, string email, string password, string securityQuestion, string securityAnswer)

    {

        //Opening Sql Connection

        string connectionString =

            ConfigurationManager.ConnectionStrings["mydatabaseConnectionString"].ConnectionString;

        MySqlConnection conn = new MySqlConnection(connectionString);

        MySqlCommand command = conn.CreateCommand();

        MySqlDataReader reader;




        try

        {

            conn.Open();

        }

        catch (Exception ex)

        {

            throw new ApplicationException("Connection cant be opened.");

        }




        //Check to make sure the user does not exist in database

        command.CommandText = String.Format("Select UserId from users where Email = \'{0}\'",

                           email);

        reader = command.ExecuteReader();

        reader.Read();

        if (reader.HasRows)

        {

            reader.Close();

            conn.Close();

            return true;

        }

        else

        {

            reader.Close();

            //insert the new user to the database

            command.CommandText = String.Format("INSERT INTO users (Fullname,Email,Password,SecurityQuestion,SecurityAnswer) VALUES (\'{0}\',\'{1}\',\'{2}\',\'{3}\',\'{4}\')",

                               fullname, email, password, securityQuestion, securityAnswer);

            reader = command.ExecuteReader();




            reader.Close();

            conn.Close();

            return false;

        }

    }




    public string UpdatePassword(string newPass)

    {

        //Opening Sql Connection

        string connectionString =

            ConfigurationManager.ConnectionStrings["mydatabaseConnectionString"].ConnectionString;

        MySqlConnection conn = new MySqlConnection(connectionString);

        MySqlCommand command = conn.CreateCommand();

        MySqlDataReader reader;




        string response;

        try

        {

            conn.Open();

        }

        catch (Exception ex)

        {

            response = "Your password could not be updated";

        }




        command.CommandText = String.Format("SET SQL_SAFE_UPDATES = 0");

        reader = command.ExecuteReader();

        reader.Close();




        command.CommandText = String.Format("Update users set Password = \'{0}\' where Email = \'{1}\'", newPass, HttpContext.Current.Session["Email"].ToString());

        reader = command.ExecuteReader();

        reader.Read();

        reader.Close();







        conn.Close();

        response = "Your password is updated successfully.";

        return response;

    }




}