Imports System.Data
Partial Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        showmaquee()
        Dim server As System.Web.HttpApplication = New HttpApplication
        Dim cookie As HttpCookie = System.Web.HttpContext.Current.Request.Cookies("UserWC")
        Try
            If Not IsPostBack Then
                If Session("pagename") = Nothing Then
                    setMenu()
                    img_main.Attributes.Add("onMouseOut", "this.src='./Images/menu/main2.gif'")
                    img_main.Attributes.Add("onMouseOver", "this.src='./Images/menu/main2.gif'")
                    img_main.ImageUrl = "./Images/menu/main2.gif"
                ElseIf Session("pagename") = "map" Then
                    setMenu()
                    img_main.Attributes.Add("onMouseOut", "this.src='./Images/menu/main2.gif'")
                    img_main.Attributes.Add("onMouseOver", "this.src='./Images/menu/main2.gif'")
                    img_main.ImageUrl = "./Images/menu/main2.gif"
                ElseIf Session("pagename") = "cctv" Then
                    setMenu()
                    img_cctv.Attributes.Add("onMouseOut", "this.src='./Images/menu/cctv2.gif'")
                    img_cctv.Attributes.Add("onMouseOver", "this.src='./Images/menu/cctv2.gif'")
                    img_cctv.ImageUrl = "./Images/menu/cctv2.gif"
                ElseIf Session("pagename") = "station" Then
                    setMenu()
                    img_station.Attributes.Add("onMouseOut", "this.src='./Images/menu/station2.gif'")
                    img_station.Attributes.Add("onMouseOver", "this.src='./Images/menu/station2.gif'")
                    img_station.ImageUrl = "./Images/menu/station2.gif"
                ElseIf Session("pagename") = "about" Then
                    setMenu()
                    img_about.Attributes.Add("onMouseOut", "this.src='./Images/menu/about2.gif'")
                    img_about.Attributes.Add("onMouseOver", "this.src='./Images/menu/about2.gif'")
                    img_about.ImageUrl = "./Images/menu/about2.gif"
                ElseIf Session("pagename") = "result" Then
                    setMenu()
                    img_conclude.Attributes.Add("onMouseOut", "this.src='./Images/menu/conclude2.gif'")
                    img_conclude.Attributes.Add("onMouseOver", "this.src='./Images/menu/conclude2.gif'")
                    img_conclude.ImageUrl = "./Images/menu/conclude2.gif"
                End If
            End If
        Catch ex As Exception
            Throw New Exception("Pagename error : " & ex.Message)
        End Try

        If Not (cookie Is Nothing) Then
            lbName.InnerText = "คุณ " & server.Server.UrlDecode(cookie.Values("Name")) & "  " & server.Server.UrlDecode(cookie.Values("LastName"))
            lbPositon.InnerText = server.Server.UrlDecode(cookie.Values("POSITION"))
        End If

    End Sub
    Sub showmaquee()
        Try
            Dim cmdLastData As New Oracle.DataAccess.Client.OracleCommand
            Dim dtaSession As New Oracle.DataAccess.Client.OracleDataAdapter(cmdLastData)
            Dim dtsSession As New DataSet
            cmdLastData.CommandType = CommandType.Text
            cmdLastData.CommandText = "select * from tbl_information where type_info = '2' order by date_time asc "
            cmdLastData.Connection = GolbalFunc.Database.Connection
            dtaSession.Fill(dtsSession, "Lasttopic")

            If dtsSession.Tables("Lasttopic").Rows.Count = 1 Then

                lblinformation1.Value = dtsSession.Tables("Lasttopic").Rows(dtsSession.Tables("Lasttopic").Rows.Count - 1).Item("infomation_id")

            ElseIf dtsSession.Tables("Lasttopic").Rows.Count = 2 Then
                lblalarm1.Visible = True
                lblinformation1.Value = dtsSession.Tables("Lasttopic").Rows(dtsSession.Tables("Lasttopic").Rows.Count - 1).Item("infomation_id")
                lblinformation2.Value = dtsSession.Tables("Lasttopic").Rows(dtsSession.Tables("Lasttopic").Rows.Count - 2).Item("infomation_id")

            ElseIf dtsSession.Tables("Lasttopic").Rows.Count >= 3 Then

                lblinformation1.Value = dtsSession.Tables("Lasttopic").Rows(dtsSession.Tables("Lasttopic").Rows.Count - 1).Item("infomation_id")
                lblinformation2.Value = dtsSession.Tables("Lasttopic").Rows(dtsSession.Tables("Lasttopic").Rows.Count - 2).Item("infomation_id")
                lblinformation3.Value = dtsSession.Tables("Lasttopic").Rows(dtsSession.Tables("Lasttopic").Rows.Count - 3).Item("infomation_id")
            End If
        Catch ex As Exception
            Throw New Exception("Get Session error : " & ex.Message)
        End Try

        Dim tablemaquee As DataTable = StationModel.GetDataForMaquee
        Dim dtrow As DataRow
        Dim checklblalarm As Integer = 0
        lblalarm1.Text = "ค่าฝนสะสมมากกว่า " & My.Computer.Registry.GetValue("HKEY_CURRENT_CONFIG\Software\WANGCHAOWEB", "CHECKRAIN", "3.0") & " มม. ตั้งแต่เวลา 00.00 น. – ปัจจุบัน " & GolbalFunc.ThaiDatetime.ThaiFormat(Now, "ประจำวันที่ dd MMMM yyyy") & " :: "
        If (tablemaquee.Rows.Count = 0) Then
            lblalarm1.Visible = True
            lblalarm1.Text &= "ไม่มีข้อมูลปริมาณน้ำฝน"
        Else
            lblalarm1.Visible = True
            For Each dtrow In tablemaquee.Rows
                If checklblalarm = 0 Then
                    lblalarm1.Text &= dtrow.Item("stn_name") & " [" & Trim(dtrow.Item("stn_code")) & "] ค่าฝนสะสม : " & dtrow.Item("rf_7tonow") & " มม.  เวลา " & Format(dtrow.Item("server_time"), "Short Time") & " น."
                    checklblalarm = 1
                Else
                    lblalarm1.Text &= " | "
                    lblalarm1.Text &= dtrow.Item("stn_name") & " [" & Trim(dtrow.Item("stn_code")) & "] ค่าฝนสะสม : " & dtrow.Item("rf_7tonow") & " มม.  เวลา " & Format(dtrow.Item("server_time"), "Short Time") & " น."
                End If
               

            Next
        End If

        


    End Sub

    Private Sub setMenu()
        'หน้าหลัก()
        img_main.Attributes.Add("onMouseOut", "this.src='./Images/menu/main.gif'")
        img_main.Attributes.Add("onMouseOver", "this.src='./Images/menu/main2.gif'")
        'ความเป็นมาโครงการ
        img_about.Attributes.Add("onMouseOut", "this.src='./Images/menu/about.gif'")
        img_about.Attributes.Add("onMouseOver", "this.src='./Images/menu/about2.gif'")
        'ข้อมูลสถานี
        img_station.Attributes.Add("onMouseOut", "this.src='./Images/menu/station.gif'")
        img_station.Attributes.Add("onMouseOver", "this.src='./Images/menu/station2.gif'")
        'สรุปข้อมูลทุกสถานี()
        img_conclude.Attributes.Add("onMouseOut", "this.src='./Images/menu/conclude.gif'")
        img_conclude.Attributes.Add("onMouseOver", "this.src='./Images/menu/conclude2.gif'")
        'แผนผังการไหลของน้ำ
        'img_water.Attributes.Add("onMouseOut", "this.src='./Images/menu/water.gif'")
        'img_water.Attributes.Add("onMouseOver", "this.src='./Images/menu/water2.gif'")
        'ดูภาพปัจจุบันกล้อง CCTV
        img_cctv.Attributes.Add("onMouseOut", "this.src='./Images/menu/cctv.gif'")
        img_cctv.Attributes.Add("onMouseOver", "this.src='./Images/menu/cctv2.gif'")
    End Sub

    'Protected Sub img_login_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_login.Click
    '    Me.Response.Redirect("~/login.aspx")
    'End Sub

    Protected Sub img_station_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_station.Click
        Session("pagename") = "station"
        Response.Redirect("detailstation.aspx")
    End Sub

    Protected Sub img_about_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_about.Click
        Session("pagename") = "about"
        Response.Redirect("History1.aspx")
    End Sub

    Protected Sub img_cctv_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_cctv.Click
        Session("pagename") = "cctv"
        Response.Redirect("CCTV.aspx")
    End Sub

    Protected Sub img_main_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_main.Click
        Session("pagename") = "map"
        Response.Redirect("map.aspx")
    End Sub

    Protected Sub img_conclude_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_conclude.Click
        Session("pagename") = "result"
        Response.Redirect("AllStation.aspx")
    End Sub

    'Protected Sub img_graph_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_graph.Click
    '    'Session("pagename") = "result"
    '    'Response.Redirect("AllStation.aspx")
    'End Sub
    'Protected Sub Image1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Image1.Click
    '    '  OpenRID()
    'End Sub
    Public Sub OpenRID()
        Dim sb As String
        sb = "<div><script language='JavaScript'>window.open('http://www.rid.go.th','rid');</script></div>"
        Page.RegisterClientScriptBlock("OpenWindow", sb)
    End Sub
    Protected Sub imgLogin_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgLogin.Click
        Response.Redirect("../login.aspx")
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        clearcookie()
        ' Dim sb As String
        imgLogin.Visible = False
        divCurrent.Visible = False
        imgLogin.Visible = True
        lbName.InnerText = ""
        If cUser.Logout() = True Then

            'sb = "<div><script language='JavaScript'>Logount()</script></div>"
            'Page.RegisterClientScriptBlock("OpenWindow", sb)
        End If
        Response.Redirect("map.aspx")
    End Sub
    Private Sub clearcookie()
        Dim myCookie As HttpCookie
        myCookie = New HttpCookie("LastUserWC")
        myCookie.Expires = DateTime.Now.AddDays(-1D)
        Response.Cookies.Add(myCookie)
        Dim myCookie2 As HttpCookie
        myCookie2 = New HttpCookie("UserWC")
        myCookie2.Expires = DateTime.Now.AddDays(-1D)
        Response.Cookies.Add(myCookie2)
    End Sub
    'Protected Sub btnalarmdetail1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim sb As String
    '    sb = "<div><script language='JavaScript'>window.open('Popupcontent.aspx?infomation_id=" & lblinformation1.Value & "','MyPopup', 'width==800','menubar=no,directories=no, location=no, resizable=no, scrollbars=yes , toolbar=no, status = no', true);</script></div>"
    '    Page.RegisterClientScriptBlock("OpenWindow", sb)
    'End Sub
    'Protected Sub btnalarmdetail2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim sb As String
    '    sb = "<div><script language='JavaScript'>window.open('Popupcontent.aspx?infomation_id=" & lblinformation2.Value & "','MyPopup', 'width==800','menubar=no,directories=no, location=no, resizable=no, scrollbars=yes , toolbar=no, status = no', true);</script></div>"
    '    Page.RegisterClientScriptBlock("OpenWindow", sb)
    'End Sub
    'Protected Sub btnalarmdetail3_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim sb As String
    '    sb = "<div><script language='JavaScript'>window.open('Popupcontent.aspx?infomation_id=" & lblinformation3.Value & "','MyPopup', 'width==800','menubar=no,directories=no, location=no, resizable=no, scrollbars=yes , toolbar=no, status = no', true);</script></div>"
    '    Page.RegisterClientScriptBlock("OpenWindow", sb)
    'End Sub
End Class

