Imports System.Data
Imports System.Data.OleDb
Public Class Form1
    Dim uName, phone, amount As String
    Dim Username, Password, password1, username1 As String
    Dim dates, Time As Date
    Dim plan1, plan2, plan3, plan4 As String
    Dim rnd, id, log As Integer
    Dim random As New Random()
    Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\E-Billing_GAD\EBilling.accdb")
    Public Sub AllUserData()
        Try
            con.Open()
            Dim cmd As New OleDbCommand("Select * from BillsInfo", con)
            Dim da As New OleDbDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            DataGridView1.DataSource = dt
            con.Close()
            Me.TabControl1.SelectedTab = Me.TabPage2
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub InsertData()
        Try
            con.Open()
            Dim cmd1 As New OleDbCommand("INSERT INTO BillsInfo([Name],[PlanDetail],[PayBy],[OrderId],[Phone],[Amount],[Date],[Time]) VALUES ('" & uName & "','" & Label132.Text & "','" & Label20.Text & "','" & Label139.Text & "','" & phone & "','" & amount & "','" & dates & "','" & Time & "')", con)
            cmd1.ExecuteNonQuery()
            cmd1.Dispose()
            Timer2.Start()
            Label35.Text = "Reacharge Successfully !"
            Me.TabControl1.SelectedTab = Me.TabPage9
            con.Close()
        Catch ex As Exception
            MsgBox(ex.InnerException)
        End Try
    End Sub
    Public Sub Login(ByVal username As String, ByVal password As String)

        Try
            con.Open()
            Dim cmd2 As New OleDbCommand("Select * from Register where Username ='" & username & "' and Password ='" & password & "'", con)
            Dim da As New OleDbDataAdapter(cmd2)
            Dim dt2 As New DataTable()
            da.Fill(dt2)
            username1 = dt2.Rows(0)("Username").ToString()
            password1 = dt2.Rows(0)("Password").ToString()

            If username.Equals(username1) Then
                If password.Equals(password1) Then
                    If ((username.Equals(username1)) And (password.Equals(password1))) Then
                        Timer5.Start()
                        Label146.Text = "Login Successfully !"
                        Me.TabControl1.SelectedTab = Me.TabPage3
                    End If
                Else
                    Timer5.Start()
                    Label146.Text = "Invalid Credentials !"
                End If
            Else
                Timer5.Start()
                Label146.Text = "Invalid Credentials !"
            End If
            con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            con.Close()
        End Try
    End Sub

    Public Sub Register(ByVal username As String, ByVal password As String)
        Try
            con.Open()
            Dim cmd1 As New OleDbCommand("INSERT INTO REGISTER([Username],[Password]) VALUES ('" & username & "','" & password & "')", con)
            cmd1.ExecuteNonQuery()
            Me.TabControl1.SelectedTab = Me.TabPage6
            Timer4.Start()
            Label155.Text = "Register Successfully !"
            TextBox5.Text = ""
            TextBox6.Text = ""
            TextBox7.Text = ""
            con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        log = 0
        rnd = random.Next(2590, 50000)
        plan1 = "12GB data and 28 days"
        plan2 = "Truly Unlimited call, 3GB per day and 70 days validity"
        plan3 = "81.75 talktime and no validity"
        plan4 = "Truly Unlimited call, 2GB per day and 70 days validity"

    End Sub

    'reacharge now button
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim right As Integer
        right = 0
        'Error Provider HAndling
        If TextBox4.Text = "" Then
            ErrorProvider1.SetError(TextBox4, "Please enter Name")
            right = 0
        Else
            ErrorProvider1.SetError(TextBox4, "")
            right = 1
        End If
        If TextBox3.Text = "" Then
            ErrorProvider1.SetError(TextBox3, "Please enter Phone No.")
            right = 0
        Else
            ErrorProvider1.SetError(TextBox3, "")
            right = 1
        End If

        If right = 1 Then
            dates = Today
            Time = TimeOfDay
            rnd = rnd * 6
            uName = TextBox4.Text
            phone = Val(TextBox3.Text)
            id = rnd
            Label134.Text = uName
            Label32.Text = phone
            Label139.Text = "OID" + id.ToString
            Label38.Text = dates

            Me.TabControl1.SelectedTab = Me.TabPage9
            InsertData()
            TextBox3.Text = ""
            TextBox4.Text = ""
            Label31.Text = ""
        End If

       
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.TabControl1.SelectedTab = Me.TabPage6
        Timer1.Stop()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Username = TextBox1.Text
        Password = TextBox2.Text
        If ((Username = "") And (Password = "")) Then
            Label146.Text = "Please Fill all the fields !"
        Else
            Login(Username, Password)
            TextBox1.Text = ""
            TextBox2.Text = ""
        End If


    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        Me.TabControl1.SelectedTab = Me.TabPage8
        Label132.Text = plan1
        Label142.Text = plan1
        amount = Label2.Text
        Label24.Text = amount
        Label25.Text = amount
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        Me.TabControl1.SelectedTab = Me.TabPage8
        Label132.Text = plan2
        Label142.Text = plan2
        amount = Label3.Text
        Label24.Text = amount
        Label25.Text = amount
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        Me.TabControl1.SelectedTab = Me.TabPage8
        Label132.Text = plan3
        Label142.Text = plan3
        amount = Label4.Text
        Label24.Text = amount
        Label25.Text = amount
    End Sub

    Private Sub PictureBox12_Click(sender As Object, e As EventArgs) Handles PictureBox12.Click
        Me.TabControl1.SelectedTab = Me.TabPage8
        Label132.Text = plan4
        Label142.Text = plan4
        amount = Label11.Text
        Label24.Text = amount
        Label25.Text = amount
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        Label20.Text = "Gpay"
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        Label20.Text = "PhonePay"
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        Label20.Text = "UPI"
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        Label20.Text = "PayTm"
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        AllUserData()

    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.TabControl1.SelectedTab = Me.TabPage6
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Label35.Text = ""
        Timer2.Stop()

    End Sub


    Private Sub Label148_Click(sender As Object, e As EventArgs) Handles Label148.Click
        Me.TabControl1.SelectedTab = Me.TabPage6
    End Sub

    Private Sub Label22_Click(sender As Object, e As EventArgs) Handles Label22.Click
        Me.TabControl1.SelectedTab = Me.TabPage10
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim Username, Cpassword As String
        If TextBox6.Text = "" Then
            Label154.Text = "Please fill all fields !"
            Timer3.Start()
        ElseIf (TextBox5.Text = "") Then
            Label154.Text = "Please fill all fields !"
            Timer3.Start()
        ElseIf (TextBox7.Text = "") Then
            Label154.Text = "Please fill all fields !"
            Timer3.Start()
        ElseIf TextBox5.Text.Equals(TextBox7.Text) Then
            Username = TextBox6.Text
            Cpassword = TextBox7.Text
            Register(Username, Cpassword)

        Else
            Timer3.Start()
            Label154.Text = "Password not same !"
        End If

    End Sub
    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Label154.Text = ""
        Timer3.Stop()

    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick
        Label155.Text = ""
        Timer4.Stop()
    End Sub

    Private Sub Timer5_Tick(sender As Object, e As EventArgs) Handles Timer5.Tick
        Label146.Text = ""
        Timer5.Stop()
    End Sub

    Private Sub PictureBox39_Click(sender As Object, e As EventArgs) Handles PictureBox39.Click
        Me.TabControl1.SelectedTab = Me.TabPage6
    End Sub

    Private Sub PictureBox40_Click(sender As Object, e As EventArgs) Handles PictureBox40.Click
        Me.TabControl1.SelectedTab = Me.TabPage6
    End Sub

    Private Sub PictureBox41_Click(sender As Object, e As EventArgs) Handles PictureBox41.Click
        Me.TabControl1.SelectedTab = Me.TabPage6
    End Sub


End Class
