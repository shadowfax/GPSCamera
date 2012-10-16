Imports Microsoft.VisualBasic.CompilerServices

Namespace Tools

    Public Class DataConvert

        ' Methods
        Public Shared Function ByteToDateTime(ByVal pBuffer As Byte()) As DateTime
            If (pBuffer Is Nothing) Then
                Return DateTime.MinValue
            End If
            If (pBuffer.Length < 1) Then
                Return DateTime.MinValue
            End If
            Dim array As Byte() = New Byte((pBuffer.Length + 1) - 1) {}
            pBuffer.CopyTo(array, 0)
            Dim builder As New System.Text.StringBuilder()
            Dim upperBound As Integer = array.GetUpperBound(0)
            Dim i As Integer = 0
            Do While (i <= upperBound)
                If (array(i) = &HFF) Then
                    array(i) = 0
                End If
                builder.Append(array(i).ToString("d2"))
                i += 1
            Loop
            Return New DateTime(Conversions.ToLong(builder.ToString))
        End Function

        Public Shared Function ByteToDecimal(ByVal pBuffer As Byte()) As Object
            If (pBuffer Is Nothing) Then
                Return Nothing
            End If
            If (pBuffer.Length < 1) Then
                Return Nothing
            End If
            Dim array As Byte() = New Byte(((pBuffer.Length - 1) + 1) - 1) {}
            pBuffer.CopyTo(array, 0)
            Dim str As String = DataConvert.ByteToHex(array, ",").Replace(",", "")
            Select Case (array.Length / 2)
                Case 1
                    Return Convert.ToInt16(str, &H10)
                Case 2
                    Return Convert.ToInt32(str, &H10)
                Case 4
                    Return Convert.ToInt64(str, &H10)
            End Select
            Return 0
        End Function

        Public Shared Function ByteToHex(ByVal pBuffer As Byte(), Optional ByVal pSplitChar As String = ",") As String
            If (pBuffer Is Nothing) Then
                Return Nothing
            End If
            If (pBuffer.Length < 1) Then
                Return Nothing
            End If
            Dim array As Byte() = New Byte(((pBuffer.Length - 1) + 1) - 1) {}
            pBuffer.CopyTo(array, 0)
            Dim builder As New System.Text.StringBuilder()
            Dim upperBound As Integer = array.GetUpperBound(0)
            Dim i As Integer = 0
            Do While (i <= upperBound)
                builder.Append((array(i).ToString("X2") & pSplitChar))
                i += 1
            Loop
            Return builder.ToString
        End Function

        Public Shared Function ByteToText(ByVal pBuffer As Byte(), Optional ByVal pDefaultValue As Byte = &HFF) As String
            If (pBuffer Is Nothing) Then
                Return Nothing
            End If
            If (pBuffer.Length < 1) Then
                Return Nothing
            End If
            Dim array As Byte() = New Byte(((pBuffer.Length - 1) + 1) - 1) {}
            pBuffer.CopyTo(array, 0)
            Dim builder As New System.Text.StringBuilder()
            Dim upperBound As Integer = array.GetUpperBound(0)
            Dim i As Integer = 0
            Do While (i <= upperBound)
                Dim num3 As Byte = array(i)
                If ((num3 >= 1) AndAlso (num3 <= &H80)) Then
                    builder.Append(Strings.Chr(array(i)))
                End If
                i += 1
            Loop
            Return builder.ToString
        End Function

        Public Shared Function DateTimeToByte(ByVal pDateTime As DateTime, Optional ByVal pLength As Integer = 8, Optional ByVal pDefaultValue As Byte = &HFF) As Byte()
            If Not Information.IsDate(pDateTime) Then
                Return Nothing
            End If

            Dim str As String = Conversions.ToString(pDateTime.Ticks)
            Dim buffer2 As Byte() = New Byte((Conversions.ToInteger(Interaction.IIf((pLength = 0), 8, (pLength - 1))) + 1) - 1) {}
            Dim startIndex As Byte = 0
            Dim num4 As Byte = CByte((buffer2.Length - 1))
            Dim i As Byte = 0
            Do While (i <= num4)
                If (startIndex < &H12) Then
                    buffer2(i) = Conversions.ToByte(str.Substring(startIndex, 2))
                    startIndex = CByte((startIndex + 2))
                Else
                    buffer2(i) = pDefaultValue
                End If
                i = CByte((i + 1))
            Loop
            Return buffer2
        End Function

        Public Shared Function DecimalToHex(ByVal pBuffer As Integer(), Optional ByVal pSplitChar As String = ",") As String
            If (pBuffer Is Nothing) Then
                Return Nothing
            End If
            If (pBuffer.Length < 1) Then
                Return Nothing
            End If
            Dim array As Integer() = New Integer(((pBuffer.Length - 1) + 1) - 1) {}
            pBuffer.CopyTo(array, 0)
            Dim builder As New System.Text.StringBuilder()
            Dim upperBound As Integer = array.GetUpperBound(0)
            Dim i As Integer = 0
            Do While (i <= upperBound)
                builder.Append((("0x" & array(i).ToString("X2")) & pSplitChar))
                i += 1
            Loop
            Return builder.ToString
        End Function

        Public Shared Function GetBytes(Optional ByVal pLength As Integer = &H100, Optional ByVal pDefaultValue As Byte = &HFF) As Byte()
            Dim buffer2 As Byte() = New Byte(((pLength - 1) + 1) - 1) {}
            Dim index As Integer = 0
            Dim upperBound As Integer = buffer2.GetUpperBound(0)
            index = 0
            Do While (index <= upperBound)
                buffer2(index) = pDefaultValue
                index += 1
            Loop
            Return buffer2
        End Function

        Public Shared Function HexTextToByte(ByVal pHexText As String, Optional ByVal pSplitChar As String = ",", Optional ByVal pLength As Integer = 0, Optional ByVal pDefaultValue As Byte = &HFF) As Byte()
            If String.IsNullOrEmpty(pHexText) Then
                Return Nothing
            End If
            If (pHexText.IndexOf(pSplitChar) > -1) Then
                Dim strArray As String() = pHexText.ToUpper.Split(New Char() {Conversions.ToChar(pSplitChar)})
                Dim buffer2 As Byte() = New Byte((strArray.GetUpperBound(0) + 1) - 1) {}
                Dim num4 As Integer = buffer2.GetUpperBound(0)
                Dim j As Integer = 0
                Do While (j <= num4)
                    If (Not String.IsNullOrEmpty(strArray(j)) AndAlso ((strArray(j).Length > 0) And (strArray(j).Length < 3))) Then
                        buffer2(j) = Convert.ToByte(Conversions.ToString(CInt(Convert.ToInt16(strArray(j), &H10))), 10)
                    Else
                        buffer2(j) = pDefaultValue
                    End If
                    j += 1
                Loop
                If (pLength <= 0) Then
                    Return buffer2
                End If
                Dim buffer3 As Byte() = New Byte(((pLength - 1) + 1) - 1) {}
                Dim num5 As Integer = buffer3.GetUpperBound(0)
                Dim k As Integer = 0
                Do While (k <= num5)
                    buffer3(k) = pDefaultValue
                    k += 1
                Loop
                buffer2.CopyTo(buffer3, 0)
                Return buffer3
            End If
            Dim buffer4 As Byte() = New Byte(1 - 1) {}
            If ((pHexText.Length > 0) And (pHexText.Length < 3)) Then
                buffer4(0) = Convert.ToByte(Conversions.ToString(CInt(Convert.ToInt16(pHexText, &H10))), 10)
            Else
                buffer4(0) = pDefaultValue
            End If
            If (pLength <= 0) Then
                Return buffer4
            End If
            Dim array As Byte() = New Byte(((pLength - 1) + 1) - 1) {}
            Dim upperBound As Integer = array.GetUpperBound(0)
            Dim i As Integer = 0
            Do While (i <= upperBound)
                array(i) = pDefaultValue
                i += 1
            Loop
            buffer4.CopyTo(array, 0)
            Return array
        End Function

        Public Shared Function TextToByte(ByVal pText As String, Optional ByVal pLength As Integer = 0, Optional ByVal pDefaultValue As Byte = &HFF) As Byte()
            If String.IsNullOrEmpty(pText) Then
                Return Nothing
            End If
            Dim bytes As Byte() = System.Text.Encoding.ASCII.GetBytes(pText.ToCharArray)
            If (pLength <= 0) Then
                Return bytes
            End If
            Dim destinationArray As Byte() = New Byte(((pLength - 1) + 1) - 1) {}
            Dim upperBound As Integer = destinationArray.GetUpperBound(0)
            Dim i As Integer = 0
            Do While (i <= upperBound)
                destinationArray(i) = pDefaultValue
                i += 1
            Loop
            Array.ConstrainedCopy(bytes, 0, destinationArray, 0, bytes.Length)
            Return destinationArray
        End Function


    End Class

End Namespace
