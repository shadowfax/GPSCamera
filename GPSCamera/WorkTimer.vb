Imports System.Timers

Public Class WorkTimer
    Inherits Timer

    ' Variables
    Private _Pause As Boolean
    Private _Second As Long

    ' Delegates 
    Public Delegate Sub TimerElapsedEventHandler(ByVal pTime As String)

    ' Events
    Public Event TimerElapsed As TimerElapsedEventHandler

    ' Methods
    Public Sub New()
        AddHandler MyBase.Elapsed, New ElapsedEventHandler(AddressOf Me.WorkTimer_Elapsed)
        MyBase.Interval = 1000
    End Sub

    Public Sub Pause()
        Me._Pause = True
        MyBase.Stop()
    End Sub

    Public Sub Play()
        If Not Me._Pause Then
            Me._Second = 0
        End If
        Me._Pause = False
        MyBase.Start()
    End Sub

    Private Function TimerFormat(ByVal pSecond As Long) As String
        Dim num As Integer = CInt(((pSecond / &HE10) Mod &H18))
        Dim num2 As Integer = CInt(((pSecond / 60) Mod 60))
        Dim num3 As Integer = CInt((pSecond Mod 60))
        Return String.Format("{0:00}:{1:00}:{2:00}", num, num2, num3)
    End Function

    Private Sub WorkTimer_Elapsed(ByVal sender As Object, ByVal e As ElapsedEventArgs)
        Me._Second = (Me._Second + 1)
        Dim timerElapsedEvent As TimerElapsedEventHandler = Me.TimerElapsedEvent
        If (Not timerElapsedEvent Is Nothing) Then
            timerElapsedEvent.Invoke(Me.TimerFormat(Me._Second))
        End If
    End Sub

End Class