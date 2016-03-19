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
                    <asp:Button CssClass="btn universalButton" ID="btnProceed" runat="server" Text="Proceed" OnClick="btnProceed_Click" TabIndex="3" /><br />
                    <br />
                    <asp:Label ID="errorInfo" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <br /><br />
                <div id="divForgotPassword" runat="server" style="display:none">
                    Forgot your password? <a id="InForgotPassword">Reset it</a>
                    <div id="divReset">
                        <h3>By answer</h3>
                        <div style="height:50px">
                            <div id="divResetByAnswer">
                                <label id="lblSecurityQuestion" runat="server"></label>
                                <input id="txtSecurityAnswer" />
                                <input type="button" id="btnReset" value="reset" /><br />
                                <label id="lblAnswerError" style="font-size:10px; color:red;"></label>
                            </div>
                        </div>
                        <h3>By email</h3>
                        <div>
                            <div id="divConfirmEmail" style="display:block">
                                <input type="email" style="border-radius:5px" id="txtResetEmail" class="txtResetEmail" runat="server" />
                                <input type="button" id="btnSendCode" value="Send Code" /><br />
                            </div>
                            <div id="divConfirmCode" style="display:none; float:left">
                                <input type="text" style="border-radius: 5px" placeholder="Enter Code" id="txtConfirmCode" />
                                <input type="button" id="btnConfirmCode" value="Confirm Code" />
                                <input type="button" id="btnResendCode" value="Resend Code" /><br />
                            </div>
                            <label class="lblCodeError"></label>
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="col-lg-2"></div>
            
        </div>
        <div class="row" style="height: 80px"></div>
    </form>

    <script>
        $("#divReset").accordion({
            collapsible: true
        });

        $("#lnForgotPassword").click(function () {
            $("#divReset").css("display", "block");
            $(".txtResetEmail").val($("#txtEmail").val());
        });

        $("btnReset").click(function () {
            if ($("#txtSecurityAnswer").val() != '') {
                var answer = $("#txtSecurityAnswer").val();
                $.ajax({
                    type: "POST",
                    url: "login.aspx/CheckAnswer",
                    contentType: "application/json; charset=utf-8",
                    data: '{"answer":"' + answer + '"}',
                    dataType: "json",
                    success: function (data) {
                        if (data.d == "true") {
                            $("#divResetByAnswer").css("display", "none");
                            window.location.replace("changepass.aspx");
                        }
                        else {
                            $("#lblAnswerError").text("try again");
                        }
                    },
                    error: function (data) { alert("server too busy"); }
                });
            }
            else {
                $("#lblAnswerError").text("Enter your answer first");
            }
        });
    </script>


</asp:Content>