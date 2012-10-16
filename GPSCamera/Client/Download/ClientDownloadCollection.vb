Imports Microsoft.VisualBasic.CompilerServices

Namespace Client.Download

    Public Class ClientDownloadCollection
        Inherits System.Collections.Hashtable

        Public ReadOnly Property IsAllDownloaded As Boolean
            Get
                Dim enumerator As IEnumerator = Nothing
                Try
                    enumerator = Me.Keys.GetEnumerator
                    Do While enumerator.MoveNext
                        Dim str As String = Conversions.ToString(enumerator.Current)
                        Dim download As ClientDownload = DirectCast(Me.Item(str), ClientDownload)
                        If Not download.IsDownloaded Then
                            Return False
                        End If
                    Loop
                Finally
                    If TypeOf enumerator Is IDisposable Then
                        TryCast(enumerator, IDisposable).Dispose()
                    End If
                End Try
                Return True
            End Get
        End Property

        Public ReadOnly Property TotalLength As Long
            Get
                Dim enumerator As IEnumerator = Nothing
                Dim num As Long = 0
                Try
                    enumerator = Me.Keys.GetEnumerator
                    Do While enumerator.MoveNext
                        Dim str As String = Conversions.ToString(enumerator.Current)
                        Dim download As ClientDownload = DirectCast(Me.Item(str), ClientDownload)
                        num = (num + download.DataLength)
                    Loop
                Finally
                    If TypeOf enumerator Is IDisposable Then
                        TryCast(enumerator, IDisposable).Dispose()
                    End If
                End Try
                Return num
            End Get
        End Property

        Public ReadOnly Property TotalPercent As Integer
            Get
                Dim enumerator As IEnumerator = Nothing
                Dim num As Integer = 0
                Try
                    enumerator = Me.Keys.GetEnumerator
                    Do While enumerator.MoveNext
                        Dim str As String = Conversions.ToString(enumerator.Current)
                        Dim download As ClientDownload = DirectCast(Me.Item(str), ClientDownload)
                        num = (num + download.Percent)
                    Loop
                Finally
                    If TypeOf enumerator Is IDisposable Then
                        TryCast(enumerator, IDisposable).Dispose()
                    End If
                End Try
                Return CInt(Math.Round(Conversion.Int(CDbl(((CDbl(num) / CDbl((Me.Count * 100))) * 100)))))
            End Get
        End Property

        Public ReadOnly Property TotalRecivedLength As Long
            Get
                Dim enumerator As IEnumerator = Nothing
                Dim num As Long = 0
                Try
                    enumerator = Me.Keys.GetEnumerator
                    Do While enumerator.MoveNext
                        Dim str As String = Conversions.ToString(enumerator.Current)
                        Dim download As ClientDownload = DirectCast(Me.Item(str), ClientDownload)
                        num = (num + download.RecivedLength)
                    Loop
                Finally
                    If TypeOf enumerator Is IDisposable Then
                        TryCast(enumerator, IDisposable).Dispose()
                    End If
                End Try
                Return num
            End Get
        End Property

    End Class

End Namespace