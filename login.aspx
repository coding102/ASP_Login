<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server">
        <div class="row" style="height:80px; margin-top:6%"></div>
        <div class="clearfix"></div>
        <div class="row middle-row" style="border-radius:10px">
            <div class="col-sm-2"></div>
            <div class="col-lg-8 Email-holder" style="text-align:center; background-color:#0D031D">
                <div id="Emailholder" style="position:relative; top: 40px">
                    <h3 style="display: block; vertical-align: central; color: Grey">Email</h3>
                    <input type="text" style="border-radius: 5px" id="txtEmail" runat="server" name="Email" tabindex="1" /><br />
                    <h3 style="display:block; vertical-align:central; color: Grey">Password</h3>
                    <input type="password" style="border-radius: 5px" id="txtPassword" runat="server" name="password" tabindex="2" /><br />
                    <br />


                </div>
            </div>
            <div class="col-lg-2"></div>
        </div>
    </form>

</asp:Content>