
Partial Class login
    Inherits System.Web.UI.Page

        Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnLogin.Click
                Dim User As New UserItem
                User.LOGIN_NAME = txtUsername.Text
                User.LOGIN_PASSWORD = txtPassword.Text
        If (New cUser().Login(User, False) = True) Then
            Response.Redirect("./pages/pageupdate.aspx")
            'ClientScript.RegisterStartupScript(Me.GetType(), "uploadNotify", "<script  type='text/javascript' >window.returnValue =true;self.close(); </script>")
        Else
        End If

        End Sub

    Protected Sub btnLogin0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnLogin0.Click
       
        Response.Redirect("./pages/map.aspx")

    End Sub
    Private Sub clearUser()
        Dim myCookie As HttpCookie
        myCookie = New HttpCookie("LastUserWC")
        myCookie.Expires = DateTime.Now.AddDays(-1D)
        Response.Cookies.Add(myCookie)
        Dim myCookie2 As HttpCookie
        myCookie2 = New HttpCookie("UserWC")
        myCookie2.Expires = DateTime.Now.AddDays(-1D)
        Response.Cookies.Add(myCookie2)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        clearUser()
    End Sub
End Class
