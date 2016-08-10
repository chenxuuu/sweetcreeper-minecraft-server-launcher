Public Class Form1
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        MsgBox("糖拌苦力怕Minecraft服务器启动器" & vbCrLf & "晨旭制作" & vbCrLf & "http://chenxublog.com",, "关于软件")
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox2.Text = "欢迎使用糖拌苦力怕服务器启动器！" & vbCrLf & "提交bug及建议请打开https://github.com/chenxuuu/sweetcreeper-minecraft-server-launcher提交Issues" & vbCrLf & vbCrLf & "服务器命令信息：" & vbCrLf
    End Sub
End Class
