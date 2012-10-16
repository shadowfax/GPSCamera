Imports Microsoft.VisualBasic.CompilerServices


Namespace Tools

    Public Class FileAccess

        Public Shared Function ReadFile(ByVal pFileName As String) As Byte()
            If Not String.IsNullOrEmpty(pFileName) Then
                Dim stream As System.IO.FileStream = Nothing
                Try
                    stream = New System.IO.FileStream(pFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read)
                    Dim array As Byte() = New Byte((CInt((stream.Length - 1)) + 1) - 1) {}
                    stream.Read(array, 0, CInt(stream.Length))
                    stream.Close()
                    Return array
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    ProjectData.ClearProjectError()
                Finally
                    If (Not stream Is Nothing) Then
                        stream.Dispose()
                    End If
                End Try
            End If
            Return Nothing
        End Function

        Public Shared Function SaveFile(ByVal pSourceBuffer As Byte(), ByVal pFileName As String) As String
            Dim str As String
            If (String.IsNullOrEmpty(pFileName) Or (pSourceBuffer Is Nothing)) Then
                Return Nothing
            End If
            Dim directory As System.IO.DirectoryInfo = New System.IO.FileInfo(pFileName).Directory
            If Not directory.Exists Then
                directory.Create()
            End If
            Dim array As Byte() = New Byte((pSourceBuffer.GetUpperBound(0) + 1) - 1) {}
            pSourceBuffer.CopyTo(array, 0)
            Dim stream As System.IO.FileStream = Nothing
            Try
                stream = New System.IO.FileStream(pFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.Write)
                stream.Write(array, 0, array.Length)
                stream.Close()
                str = pFileName
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                str = Nothing
                ProjectData.ClearProjectError()
            Finally
                If (Not stream Is Nothing) Then
                    stream.Dispose()
                End If
            End Try
            Return str
        End Function


    End Class

End Namespace