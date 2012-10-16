Imports Microsoft.VisualBasic.CompilerServices


Namespace Communications

    Public Class GMx
        Inherits System.IO.Ports.SerialPort

#Region "        Variables "

        Private _QueueContain As String = ""
        Private m_Agps As Byte()
        Private m_BaudRate As Integer
        Private m_Break As Byte() = New Byte() {1, 2}
        Private m_Dbg As Byte()
        Private m_DrvB As Byte()
        Private m_DrvG As Byte()
        Private m_DrvS As Byte()
        Private m_Empty As Byte() = New Byte() {1, 6}
        Private m_Finish As Byte() = New Byte() {1, 1}
        Private m_Foot As Byte() = New Byte() {&H59, &H10}
        Private m_Head As Byte() = New Byte() {8, 0}
        Private m_IsDetect As Boolean
        Private m_Ready As Byte() = New Byte() {1, 7}
        Private m_Voc As Byte()
        Private m_Vot As Byte()
        Private m_Vow As Byte()

        Private WriteErrorEvent As WriteErrorEventHandler
        Private WritingEvent As WritingEventHandler
        Private WrittedEvent As WrittedEventHandler

#End Region

#Region "        Delegates "

        Public Delegate Sub WriteErrorEventHandler(ByVal pItem As DataItem, ByVal e As Exception)
        Public Delegate Sub WritingEventHandler(ByVal pItem As DataItem, ByVal pWritedBytes As Long, ByVal pTotalBytes As Long, ByVal pPercent As Integer, ByVal pBuffer As Byte())
        Public Delegate Sub WrittedEventHandler(ByVal pItem As DataItem)

#End Region

#Region "        Events "

        Public Custom Event WriteError As WriteErrorEventHandler
            AddHandler(ByVal value As WriteErrorEventHandler)
                Me.WriteErrorEvent = DirectCast(System.Delegate.Combine(Me.WriteErrorEvent, value), WriteErrorEventHandler)
            End AddHandler

            RemoveHandler(ByVal value As WriteErrorEventHandler)
                Me.WriteErrorEvent = DirectCast(System.Delegate.Remove(Me.WriteErrorEvent, value), WriteErrorEventHandler)
            End RemoveHandler

            RaiseEvent(ByVal pItem As DataItem, ByVal e As System.Exception)
                Me.WriteErrorEvent.Invoke(pItem, e)
            End RaiseEvent
        End Event

        Public Custom Event Writing As WritingEventHandler
            AddHandler(ByVal value As WritingEventHandler)
                Me.WritingEvent = DirectCast(System.Delegate.Combine(Me.WritingEvent, value), WritingEventHandler)
            End AddHandler

            RemoveHandler(ByVal value As WritingEventHandler)
                Me.WritingEvent = DirectCast(System.Delegate.Remove(Me.WritingEvent, value), WritingEventHandler)
            End RemoveHandler

            RaiseEvent(ByVal pItem As DataItem, ByVal pWritedBytes As Long, ByVal pTotalBytes As Long, ByVal pPercent As Integer, ByVal pBuffer() As Byte)
                Me.WritingEvent.Invoke(pItem, pWritedBytes, pTotalBytes, pPercent, pBuffer)
            End RaiseEvent
        End Event

        Public Custom Event Writted As WrittedEventHandler
            AddHandler(ByVal value As WrittedEventHandler)
                Me.WrittedEvent = DirectCast(System.Delegate.Combine(Me.WrittedEvent, value), WrittedEventHandler)
            End AddHandler

            RemoveHandler(ByVal value As WrittedEventHandler)
                Me.WrittedEvent = DirectCast(System.Delegate.Remove(Me.WrittedEvent, value), WrittedEventHandler)
            End RemoveHandler

            RaiseEvent(ByVal pItem As DataItem)
                Me.WrittedEvent.Invoke(pItem)
            End RaiseEvent
        End Event

#End Region

        ' Methods
        Public Sub New()
            MyBase.Close()
            MyBase.BaudRate = Conversions.ToInteger(Interaction.IIf((Me.SetBaudRate = 0), &H1C200, Me.SetBaudRate))
            MyBase.DataBits = 8
            MyBase.Parity = System.IO.Ports.Parity.None
            MyBase.StopBits = System.IO.Ports.StopBits.One
            MyBase.ReadTimeout = &H3E8
            MyBase.WriteTimeout = &H3E8
        End Sub

        Public Function DeviceDetect() As Boolean
            Dim flag As Boolean
            Dim queue As New Queue
            Dim buffer As Byte() = New Byte() {8, 0, 2, 7, &H59, &H10}
            Try
                If Not Me.IsOpen Then
                    Me.Open()
                End If
                Me.DiscardOutBuffer()
                queue.Clear()
                Me.Write(buffer, 0, buffer.Length)
                Do
                    queue.Enqueue(Me.ReadByte)
                Loop While (((queue.Count <= 1) OrElse Not Operators.ConditionalCompareObjectEqual(queue.Dequeue, 1, False)) OrElse Not Operators.ConditionalCompareObjectEqual(queue.Dequeue, 1, False))
                flag = True
            Catch exception1 As TimeoutException
                ProjectData.SetProjectError(exception1)
                Dim exception As TimeoutException = exception1
                flag = False
                ProjectData.ClearProjectError()
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                flag = False
                ProjectData.ClearProjectError()
            Finally
                Me.Close()
            End Try
            Return flag
        End Function

        Protected Overrides Sub Finalize()
            MyBase.Close()
            MyBase.Finalize()
        End Sub

        Public Function FinishToWork(ByVal pBooree As String, Optional ByVal pGpsDetect As Boolean = False) As Boolean
            Dim flag As Boolean
            System.Threading.Thread.Sleep(&H7D0)
            Dim queue As New Queue
            Dim buffer As Byte() = New Byte() {8, 0, 2, 10, &H59, &H10}
            Dim str As String = pBooree
            If (str = "Y") Then
                Try
                    If Not Me.IsOpen Then
                        Me.Open()
                    End If
                    Me.DiscardOutBuffer()
                    queue.Clear()
                    Me.Write(buffer, 0, buffer.Length)
                    System.Threading.Thread.Sleep(&HBB8)
                    flag = True
                Catch exception1 As TimeoutException
                    ProjectData.SetProjectError(exception1)
                    Dim exception As TimeoutException = exception1
                    flag = False
                    ProjectData.ClearProjectError()
                Catch exception5 As Exception
                    ProjectData.SetProjectError(exception5)
                    Dim exception2 As Exception = exception5
                    flag = False
                    ProjectData.ClearProjectError()
                Finally
                    System.Threading.Thread.Sleep(&H3E8)
                End Try
                Return flag
            End If
            If (str <> "N") Then
                Return flag
            End If
            Try
                Dim mx As GMx = Me
                If Not mx.IsOpen Then
                    mx.Open()
                End If
                mx.DiscardInBuffer()
                mx.DiscardOutBuffer()
                queue.Clear()
                mx.Write(buffer, 0, buffer.Length)
                System.Threading.Thread.Sleep(&HBB8)
                Return True
            Catch exception6 As TimeoutException
                ProjectData.SetProjectError(exception6)
                Dim exception3 As TimeoutException = exception6
                ProjectData.ClearProjectError()
            Catch exception7 As Exception
                ProjectData.SetProjectError(exception7)
                Dim exception4 As Exception = exception7
                ProjectData.ClearProjectError()
            Finally
                Me.Close()
                System.Threading.Thread.Sleep(&H3E8)
            End Try
            Return False
        End Function

        Private Function GetByte(ByVal pLength As Integer, Optional ByVal pDefaultValue As Byte = &HFF) As Byte()
            Dim buffer2 As Byte() = New Byte(((pLength - 1) + 1) - 1) {}
            Dim upperBound As Integer = buffer2.GetUpperBound(0)
            Dim i As Integer = 0
            Do While (i <= upperBound)
                buffer2(i) = pDefaultValue
                i += 1
            Loop
            Return buffer2
        End Function

        Private Function GetCheckSum(ByVal pSourceBuffer As Byte()) As Byte
            Dim num2 As Byte = &HFF
            Dim num4 As Integer = (pSourceBuffer.GetUpperBound(0) - 3)
            Dim i As Integer = 2
            Do While (i <= num4)
                num2 = CByte((num2 Xor pSourceBuffer(i)))
                i += 1
            Loop
            Return num2
        End Function

        Private Function GetCheckSum(ByVal pSourceBuffer As Byte(), ByVal pStartIndex As Long, ByVal pLength As Long) As Byte
            Dim array As Object() = New Object((pSourceBuffer.GetUpperBound(0) + 1) - 1) {}
            pSourceBuffer.CopyTo(array, 0)
            Dim left As Byte = &HFF
            Dim num4 As Integer = CInt(((pStartIndex + pLength) - 1))
            Dim i As Integer = CInt(pStartIndex)
            Do While (i <= num4)
                left = Conversions.ToByte(Operators.XorObject(left, array(i)))
                i += 1
            Loop
            Return left
        End Function

        Private Function GetDataCommand(ByVal pCommand As Byte(), ByVal pData As Byte(), ByVal pPercent As Integer) As Byte()
            Dim destinationArray As Byte() = New Byte((((((Me.m_Head.Length + pCommand.Length) + pData.Length) + 1) + Me.m_Foot.Length) + 1) - 1) {}
            Array.ConstrainedCopy(Me.m_Head, 0, destinationArray, 0, Me.m_Head.Length)
            Array.ConstrainedCopy(Me.m_Foot, 0, destinationArray, (destinationArray.Length - Me.m_Foot.Length), Me.m_Foot.Length)
            Array.ConstrainedCopy(pCommand, 0, destinationArray, Me.m_Head.Length, pCommand.Length)
            Array.ConstrainedCopy(pData, 0, destinationArray, (Me.m_Head.Length + pCommand.Length), pData.Length)
            destinationArray(((destinationArray.Length - Me.m_Foot.Length) - 2)) = CByte(pPercent)
            destinationArray(((destinationArray.Length - Me.m_Foot.Length) - 1)) = Me.GetCheckSum(destinationArray)
            Return destinationArray
        End Function

        Private Function GetDataHeader(ByVal pLength As Long, ByVal pPageSize As Integer) As Byte()
            Dim sourceArray As Byte() = Me.ValueToByte(pLength, TypeCode.Int32)
            Dim buffer3 As Byte() = Me.ValueToByte(CLng(pPageSize), TypeCode.Int16)
            Dim destinationArray As Byte() = New Byte((((sourceArray.Length + buffer3.Length) - 1) + 1) - 1) {}
            Array.ConstrainedCopy(sourceArray, 0, destinationArray, 0, sourceArray.Length)
            Array.ConstrainedCopy(buffer3, 0, destinationArray, 4, buffer3.Length)
            Return destinationArray
        End Function

        Private Function GetDataHeaderCommand(ByVal pCommand As Byte(), ByVal pDataHeader As Byte()) As Byte()
            Dim destinationArray As Byte() = New Byte(((((Me.m_Head.Length + pCommand.Length) + pDataHeader.Length) + Me.m_Foot.Length) + 1) - 1) {}
            Array.ConstrainedCopy(Me.m_Head, 0, destinationArray, 0, Me.m_Head.Length)
            Array.ConstrainedCopy(Me.m_Foot, 0, destinationArray, (destinationArray.Length - Me.m_Foot.Length), Me.m_Foot.Length)
            Array.ConstrainedCopy(pCommand, 0, destinationArray, Me.m_Head.Length, pCommand.Length)
            Array.ConstrainedCopy(pDataHeader, 0, destinationArray, (Me.m_Head.Length + pCommand.Length), pDataHeader.Length)
            destinationArray(((destinationArray.Length - Me.m_Foot.Length) - 1)) = Me.GetCheckSum(destinationArray)
            Return destinationArray
        End Function

        Public Function GetGpsInformation() As String
            Dim str As String
            Dim port As New System.IO.Ports.SerialPort
            Try
                MyBase.Close()
                Dim port2 As System.IO.Ports.SerialPort = port
                port2.Close()
                port2.PortName = MyBase.PortName
                port2.BaudRate = &H2580
                port2.DataBits = 8
                port2.Parity = System.IO.Ports.Parity.None
                port2.StopBits = System.IO.Ports.StopBits.One
                port2.ReadBufferSize = &H200
                port2.ReadTimeout = &H3E8
                port2.WriteTimeout = &H3E8
                port2.Open()
                System.Threading.Thread.Sleep(&H3E8)
                str = port2.ReadExisting
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                str = Nothing
                ProjectData.ClearProjectError()
            Finally
                port.Close()
                port.Dispose()
            End Try
            Return str
        End Function

        Private Function GetInfoCheckSum(ByVal pSourceBuffer As Byte()) As Byte
            Dim num2 As Byte = &HFF
            Dim upperBound As Integer = pSourceBuffer.GetUpperBound(0)
            Dim i As Integer = 0
            Do While (i <= upperBound)
                num2 = CByte((num2 Xor pSourceBuffer(i)))
                i += 1
            Loop
            Return num2
        End Function

        Public Function GoToWork(ByVal pBooree As String) As Boolean
            Dim flag As Boolean
            Dim queue As New Queue
            Dim buffer As Byte() = New Byte() {8, 0, 2, 11, &H59, &H10}
            Dim str As String = pBooree
            If (str = "Y") Then
                Try
                    If Not Me.IsOpen Then
                        Me.Open()
                    End If
                    Me.DiscardOutBuffer()
                    queue.Clear()
                    Me.Write(buffer, 0, buffer.Length)
                    System.Threading.Thread.Sleep(&HBB8)
                    Do
                        queue.Enqueue(Me.ReadByte)
                    Loop While (((queue.Count <= 1) OrElse Not Operators.ConditionalCompareObjectEqual(queue.Dequeue, 1, False)) OrElse Not Operators.ConditionalCompareObjectEqual(queue.Dequeue, 7, False))
                    Return True
                Catch exception1 As TimeoutException
                    ProjectData.SetProjectError(exception1)
                    Dim exception As TimeoutException = exception1
                    flag = False
                    ProjectData.ClearProjectError()
                Catch exception5 As Exception
                    ProjectData.SetProjectError(exception5)
                    Dim exception2 As Exception = exception5
                    flag = False
                    ProjectData.ClearProjectError()
                End Try
                Return flag
            End If
            If (str <> "N") Then
                Return flag
            End If
            Try
                Dim mx As GMx = Me
                If Not mx.IsOpen Then
                    mx.Open()
                End If
                mx.DiscardInBuffer()
                mx.DiscardOutBuffer()
                queue.Clear()
                mx.Write(buffer, 0, buffer.Length)
                System.Threading.Thread.Sleep(&HBB8)
                Do
                    queue.Enqueue(mx.ReadByte)
                Loop While (((queue.Count <= 1) OrElse Not Operators.ConditionalCompareObjectEqual(queue.Dequeue, 1, False)) OrElse Not Operators.ConditionalCompareObjectEqual(queue.Dequeue, 7, False))
                Return True
            Catch exception6 As TimeoutException
                ProjectData.SetProjectError(exception6)
                Dim exception3 As TimeoutException = exception6
                ProjectData.ClearProjectError()
            Catch exception7 As Exception
                ProjectData.SetProjectError(exception7)
                Dim exception4 As Exception = exception7
                ProjectData.ClearProjectError()
            Finally
                Me.Close()
            End Try
            Return False
        End Function

        Public Function GpsDetect(Optional ByVal pTimeout As Integer = 5) As Boolean
            Dim flag As Boolean
            Dim port As New System.IO.Ports.SerialPort
            Try
                MyBase.Close()
                Dim port2 As System.IO.Ports.SerialPort = port
                port2.Close()
                port2.PortName = MyBase.PortName
                port2.BaudRate = &H2580
                port2.DataBits = 8
                port2.Parity = System.IO.Ports.Parity.None
                port2.StopBits = System.IO.Ports.StopBits.One
                port2.ReadBufferSize = &H200
                port2.ReadTimeout = &H3E8
                port2.WriteTimeout = &H3E8
                port2.Open()
                Dim num As Integer = 0
                Do
                    System.Threading.Thread.Sleep(&H3E8)
                    Dim str As String = port2.ReadExisting
                    If (str <> "") Then
                        If (str.IndexOf("$GPGGA") > -1) Then
                            Return True
                        End If
                        If (str.IndexOf("$GPGSV") > -1) Then
                            Return True
                        End If
                        If (str.IndexOf("$GPRMC") > -1) Then
                            Return True
                        End If
                    End If
                    num += 1
                Loop While (num < pTimeout)
                flag = False
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                flag = False
                ProjectData.ClearProjectError()
            Finally
                port.Close()
                port.Dispose()
            End Try
            Return flag
        End Function

        Public Function IsSupportAGPS() As Boolean
            Dim queue As New Queue
            Dim buffer As Byte() = New Byte() {8, 0, 10, 5, &H59, &H10}
            Try
                If Not MyBase.IsOpen Then
                    MyBase.Open()
                End If
                MyBase.DiscardInBuffer()
                MyBase.DiscardOutBuffer()
                queue.Clear()
                MyBase.Write(buffer, 0, buffer.Length)
                Do While True
                    queue.Enqueue(MyBase.ReadByte)
                    Dim array As Integer() = New Integer(((queue.Count - 1) + 1) - 1) {}
                    queue.CopyTo(array, 0)
                    Me._QueueContain = GPSCamera.Tools.DataConvert.DecimalToHex(array, ",")
                    If (((queue.Count > 1) AndAlso Operators.ConditionalCompareObjectEqual(queue.Dequeue, 1, False)) AndAlso Operators.ConditionalCompareObjectEqual(queue.Dequeue, 10, False)) Then
                        Return True
                    End If
                    If Operators.ConditionalCompareObjectEqual(queue.Peek, 0, False) Then
                        Throw New Exception("Download mode is break.")
                    End If
                Loop
            Catch exception1 As TimeoutException
                ProjectData.SetProjectError(exception1)
                Dim exception As TimeoutException = exception1
                ProjectData.ClearProjectError()
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                ProjectData.ClearProjectError()
            Finally
                MyBase.Close()
            End Try
            Return False
        End Function

        Public Function ReadChipInfo() As Byte()
            Dim queue As New Queue
            Dim buffer2 As Byte() = New Byte() {8, 0, 3, 1, &H59, &H10}
            Try
                If Not MyBase.IsOpen Then
                    MyBase.Open()
                End If
                MyBase.DiscardOutBuffer()
                queue.Clear()
                MyBase.Write(buffer2, 0, buffer2.Length)
                Do While True
                    queue.Enqueue(MyBase.ReadByte)
                    If ((queue.Count > 1) AndAlso Operators.ConditionalCompareObjectNotEqual(queue.Peek, 8, False)) Then
                        Throw New Exception("Download mode is break.")
                    End If
                Loop
            Catch exception1 As TimeoutException
                ProjectData.SetProjectError(exception1)
                Dim exception As TimeoutException = exception1
                If (queue.Count > 5) Then
                    Dim destinationArray As Byte() = New Byte((((((queue.Count - 2) - 1) - 2) - 1) + 1) - 1) {}
                    Dim sourceArray As Byte() = New Byte(((queue.Count - 1) + 1) - 1) {}
                    Dim num2 As Integer = (queue.Count - 1)
                    Dim i As Integer = 0
                    Do While (i <= num2)
                        sourceArray(i) = Conversions.ToByte(queue.Dequeue)
                        i += 1
                    Loop
                    Array.ConstrainedCopy(sourceArray, 2, destinationArray, 0, destinationArray.Length)
                    If (Me.GetInfoCheckSum(destinationArray) = sourceArray((sourceArray.GetUpperBound(0) - 2))) Then
                        Dim buffer3 As Byte() = destinationArray
                        ProjectData.ClearProjectError()
                        Return buffer3
                    End If
                    Me.ReadChipInfo()
                End If
                ProjectData.ClearProjectError()
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                ProjectData.ClearProjectError()
            Finally
                MyBase.Close()
            End Try
            Return Nothing
        End Function

        Public Function ReadChipInfo(ByVal pCount As Integer) As Byte()
            Dim buffer As Byte() = Nothing
            Dim queue As New Queue
            Dim buffer3 As Byte() = New Byte() {8, 0, 3, 1, &H59, &H10}
            Dim destinationArray As Byte() = New Byte(&H10 - 1) {}
            Try
                If Not MyBase.IsOpen Then
                    MyBase.Open()
                End If
                MyBase.DiscardOutBuffer()
                queue.Clear()
                MyBase.Write(buffer3, 0, buffer3.Length)
                Do While True
                    queue.Enqueue(MyBase.ReadByte)
                    If ((queue.Count > 1) AndAlso Operators.ConditionalCompareObjectNotEqual(queue.Peek, 8, False)) Then
                        Throw New Exception("Download mode is break.")
                    End If
                Loop
            Catch exception1 As TimeoutException
                ProjectData.SetProjectError(exception1)
                Dim exception As TimeoutException = exception1
                If (queue.Count > &H80) Then
                    buffer = New Byte((((((queue.Count - 2) - 1) - 2) - 1) + 1) - 1) {}
                    Dim sourceArray As Byte() = New Byte(((queue.Count - 1) + 1) - 1) {}
                    Dim num2 As Integer = (queue.Count - 1)
                    Dim i As Integer = 0
                    Do While (i <= num2)
                        sourceArray(i) = Conversions.ToByte(queue.Dequeue)
                        i += 1
                    Loop
                    Array.ConstrainedCopy(sourceArray, 2, buffer, 0, buffer.Length)
                    Array.ConstrainedCopy(buffer, &H40, destinationArray, 0, destinationArray.Length)
                    If (((Me.GetInfoCheckSum(buffer) <> sourceArray((sourceArray.GetUpperBound(0) - 2))) Or (Convert.ToBase64String(destinationArray) = Convert.ToBase64String(GPSCamera.Tools.DataConvert.GetBytes(&H10, &HFF)))) Or (Convert.ToBase64String(destinationArray) = Convert.ToBase64String(GPSCamera.Tools.DataConvert.GetBytes(&H10, 0)))) Then
                        If (pCount > 0) Then
                            pCount -= 1
                            buffer = Me.ReadChipInfo(pCount)
                        Else
                            buffer = Nothing
                        End If
                    End If
                ElseIf (pCount > 0) Then
                    pCount -= 1
                    buffer = Me.ReadChipInfo(pCount)
                Else
                    buffer = Nothing
                End If
                ProjectData.ClearProjectError()
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                buffer = Nothing
                ProjectData.ClearProjectError()
            Finally
                MyBase.Close()
            End Try
            Return buffer
        End Function

        Public Function ReadEndWeek() As Byte()
            Dim queue As New Queue
            Dim buffer2 As Byte() = New Byte() {8, 0, 10, 3, &H59, &H10}
            Try
                If Not MyBase.IsOpen Then
                    MyBase.Open()
                End If
                MyBase.DiscardInBuffer()
                MyBase.DiscardOutBuffer()
                queue.Clear()
                MyBase.Write(buffer2, 0, buffer2.Length)
                Do While True
                    queue.Enqueue(MyBase.ReadByte)
                Loop
            Catch exception1 As TimeoutException
                ProjectData.SetProjectError(exception1)
                Dim exception As TimeoutException = exception1
                If (queue.Count > 0) Then
                    Dim destinationArray As Byte() = New Byte((((((queue.Count - 2) - 1) - 2) - 1) + 1) - 1) {}
                    Dim sourceArray As Byte() = New Byte(((queue.Count - 1) + 1) - 1) {}
                    Dim num2 As Integer = (queue.Count - 1)
                    Dim i As Integer = 0
                    Do While (i <= num2)
                        sourceArray(i) = Conversions.ToByte(queue.Dequeue)
                        i += 1
                    Loop
                    Array.ConstrainedCopy(sourceArray, 2, destinationArray, 0, destinationArray.Length)
                    If (Me.GetCheckSum(destinationArray) = sourceArray((sourceArray.GetUpperBound(0) - 2))) Then
                        Dim buffer3 As Byte() = destinationArray
                        ProjectData.ClearProjectError()
                        Return buffer3
                    End If
                End If
                ProjectData.ClearProjectError()
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                ProjectData.ClearProjectError()
            Finally
                MyBase.Close()
            End Try
            Return Nothing
        End Function

        Public Function ReadEPO() As Byte()
            Dim queue As New Queue
            Dim buffer2 As Byte() = New Byte() {8, 0, 10, 1, &H59, &H10}
            Try
                If Not MyBase.IsOpen Then
                    MyBase.Open()
                End If
                MyBase.DiscardInBuffer()
                MyBase.DiscardOutBuffer()
                queue.Clear()
                MyBase.Write(buffer2, 0, buffer2.Length)
                Do While True
                    queue.Enqueue(MyBase.ReadByte)
                Loop
            Catch exception1 As TimeoutException
                ProjectData.SetProjectError(exception1)
                Dim exception As TimeoutException = exception1
                If (queue.Count > 0) Then
                    Dim destinationArray As Byte() = New Byte((((((queue.Count - 2) - 1) - 2) - 1) + 1) - 1) {}
                    Dim sourceArray As Byte() = New Byte(((queue.Count - 1) + 1) - 1) {}
                    Dim num2 As Integer = (queue.Count - 1)
                    Dim i As Integer = 0
                    Do While (i <= num2)
                        sourceArray(i) = Conversions.ToByte(queue.Dequeue)
                        i += 1
                    Loop
                    Array.ConstrainedCopy(sourceArray, 2, destinationArray, 0, destinationArray.Length)
                    If (Me.GetCheckSum(destinationArray) = sourceArray((sourceArray.GetUpperBound(0) - 2))) Then
                        Dim buffer3 As Byte() = destinationArray
                        ProjectData.ClearProjectError()
                        Return buffer3
                    End If
                End If
                ProjectData.ClearProjectError()
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                ProjectData.ClearProjectError()
            Finally
                MyBase.Close()
            End Try
            Return Nothing
        End Function

        Public Function ReadHostInfo(Optional ByVal pPageIndex As Byte = 0) As Byte()
            If ((pPageIndex < 0) Or (pPageIndex > 15)) Then
                pPageIndex = 0
            End If
            Dim queue As New Queue
            Dim buffer2 As Byte() = New Byte() {8, 0, 2, 12, pPageIndex, &H59, &H10}
            Try
                If Not MyBase.IsOpen Then
                    MyBase.Open()
                End If
                MyBase.DiscardOutBuffer()
                queue.Clear()
                MyBase.Write(buffer2, 0, buffer2.Length)
                System.Threading.Thread.Sleep(&H3E8)
                Do While True
                    queue.Enqueue(MyBase.ReadByte)
                Loop
            Catch exception1 As TimeoutException
                ProjectData.SetProjectError(exception1)
                Dim exception As TimeoutException = exception1
                If (queue.Count > 0) Then
                    Dim destinationArray As Byte() = New Byte((((((queue.Count - 2) - 1) - 2) - 1) + 1) - 1) {}
                    Dim sourceArray As Byte() = New Byte(((queue.Count - 1) + 1) - 1) {}
                    Dim num2 As Integer = (queue.Count - 1)
                    Dim i As Integer = 0
                    Do While (i <= num2)
                        sourceArray(i) = Conversions.ToByte(queue.Dequeue)
                        i += 1
                    Loop
                    Array.ConstrainedCopy(sourceArray, 2, destinationArray, 0, destinationArray.Length)
                    If (Me.GetInfoCheckSum(destinationArray) = sourceArray((sourceArray.GetUpperBound(0) - 2))) Then
                        Dim buffer3 As Byte() = destinationArray
                        ProjectData.ClearProjectError()
                        Return buffer3
                    End If
                    Me.ReadHostInfo(0)
                End If
                ProjectData.ClearProjectError()
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                ProjectData.ClearProjectError()
            Finally
                MyBase.Close()
            End Try
            Return Nothing
        End Function

        Public Function ReadHostInfo(ByVal pCount As Integer, Optional ByVal pPageIndex As Byte = 0) As Byte()
            Dim buffer As Byte() = Nothing
            If ((pPageIndex < 0) Or (pPageIndex > 15)) Then
                pPageIndex = 0
            End If
            System.Threading.Thread.Sleep(&H3E8)
            Dim queue As New Queue
            Dim buffer2 As Byte() = New Byte() {8, 0, 2, 12, pPageIndex, &H59, &H10}
            Try
                If Not MyBase.IsOpen Then
                    MyBase.Open()
                End If
                MyBase.DiscardOutBuffer()
                queue.Clear()
                MyBase.Write(buffer2, 0, buffer2.Length)
                System.Threading.Thread.Sleep(&HBB8)
                Do While True
                    queue.Enqueue(MyBase.ReadByte)
                Loop
            Catch exception1 As TimeoutException
                ProjectData.SetProjectError(exception1)
                Dim exception As TimeoutException = exception1
                If (queue.Count > &H100) Then
                    buffer = New Byte((((((queue.Count - 2) - 1) - 2) - 1) + 1) - 1) {}
                    Dim sourceArray As Byte() = New Byte(((queue.Count - 1) + 1) - 1) {}
                    Dim num2 As Integer = (queue.Count - 1)
                    Dim i As Integer = 0
                    Do While (i <= num2)
                        sourceArray(i) = Conversions.ToByte(queue.Dequeue)
                        i += 1
                    Loop
                    Array.ConstrainedCopy(sourceArray, 2, buffer, 0, buffer.Length)
                    If (Me.GetInfoCheckSum(buffer) <> sourceArray((sourceArray.GetUpperBound(0) - 2))) Then
                        If (pCount > 0) Then
                            pCount -= 1
                            buffer = Me.ReadHostInfo(pCount, pPageIndex)
                        Else
                            buffer = Nothing
                        End If
                    End If
                ElseIf (pCount > 0) Then
                    pCount -= 1
                    buffer = Me.ReadHostInfo(pCount, pPageIndex)
                Else
                    buffer = Nothing
                End If
                ProjectData.ClearProjectError()
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                buffer = Nothing
                ProjectData.ClearProjectError()
            Finally
                MyBase.Close()
            End Try
            Return buffer
        End Function

        Public Function ReadHostInfo(ByVal pCount As Integer, ByVal pPageIndex As Byte, ByVal pItem As HostInfo) As Byte()
            If ((pPageIndex < 0) Or (pPageIndex > 15)) Then
                pPageIndex = 0
            End If
            Dim queue As New Queue
            Dim buffer2 As Byte() = New Byte() {8, 0, 2, 1, &H59, &H10}
            Dim destinationArray As Byte() = Nothing
            If (pPageIndex = 0) Then
                Select Case pItem
                    Case HostInfo.Company
                        buffer2(2) = 2
                        buffer2(3) = 1
                        Exit Select
                    Case HostInfo.Motherboard
                        buffer2(2) = 2
                        buffer2(3) = 3
                        Exit Select
                    Case HostInfo.Project
                        buffer2(2) = 2
                        buffer2(2) = 5
                        Exit Select
                    Case HostInfo.Product
                        buffer2(2) = 2
                        buffer2(3) = &H12
                        Exit Select
                    Case HostInfo.County
                        buffer2(2) = 2
                        buffer2(2) = &H10
                        Exit Select
                End Select
            End If
            Try
                If Not MyBase.IsOpen Then
                    MyBase.Open()
                End If
                MyBase.DiscardOutBuffer()
                queue.Clear()
                MyBase.Write(buffer2, 0, buffer2.Length)
                System.Threading.Thread.Sleep(&HBB8)
                Do While True
                    queue.Enqueue(MyBase.ReadByte)
                Loop
            Catch exception1 As TimeoutException
                ProjectData.SetProjectError(exception1)
                Dim exception As TimeoutException = exception1
                If (queue.Count > &H10) Then
                    destinationArray = New Byte((((((queue.Count - 2) - 1) - 2) - 1) + 1) - 1) {}
                    Dim sourceArray As Byte() = New Byte(((queue.Count - 1) + 1) - 1) {}
                    Dim num2 As Integer = (queue.Count - 1)
                    Dim i As Integer = 0
                    Do While (i <= num2)
                        sourceArray(i) = Conversions.ToByte(queue.Dequeue)
                        i += 1
                    Loop
                    Array.ConstrainedCopy(sourceArray, 2, destinationArray, 0, destinationArray.Length)
                    If (Me.GetInfoCheckSum(destinationArray) <> sourceArray((sourceArray.GetUpperBound(0) - 2))) Then
                        If (pCount > 0) Then
                            pCount -= 1
                            destinationArray = Me.ReadHostInfo(pCount, pPageIndex, pItem)
                        Else
                            destinationArray = Nothing
                        End If
                    End If
                ElseIf (pCount > 0) Then
                    pCount -= 1
                    destinationArray = Me.ReadHostInfo(pCount, pPageIndex, pItem)
                Else
                    destinationArray = Nothing
                End If
                ProjectData.ClearProjectError()
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                destinationArray = Nothing
                ProjectData.ClearProjectError()
            Finally
                MyBase.Close()
            End Try
            Return destinationArray
        End Function

        Public Function ReadStartWeek() As Byte()
            Dim queue As New Queue
            Dim buffer2 As Byte() = New Byte() {8, 0, 10, 2, &H59, &H10}
            Try
                If Not MyBase.IsOpen Then
                    MyBase.Open()
                End If
                MyBase.DiscardInBuffer()
                MyBase.DiscardOutBuffer()
                queue.Clear()
                MyBase.Write(buffer2, 0, buffer2.Length)
                Do While True
                    queue.Enqueue(MyBase.ReadByte)
                Loop
            Catch exception1 As TimeoutException
                ProjectData.SetProjectError(exception1)
                Dim exception As TimeoutException = exception1
                If (queue.Count > 0) Then
                    Dim destinationArray As Byte() = New Byte((((((queue.Count - 2) - 1) - 2) - 1) + 1) - 1) {}
                    Dim sourceArray As Byte() = New Byte(((queue.Count - 1) + 1) - 1) {}
                    Dim num2 As Integer = (queue.Count - 1)
                    Dim i As Integer = 0
                    Do While (i <= num2)
                        sourceArray(i) = Conversions.ToByte(queue.Dequeue)
                        i += 1
                    Loop
                    Array.ConstrainedCopy(sourceArray, 2, destinationArray, 0, destinationArray.Length)
                    If (Me.GetCheckSum(destinationArray) = sourceArray((sourceArray.GetUpperBound(0) - 2))) Then
                        Dim buffer3 As Byte() = destinationArray
                        ProjectData.ClearProjectError()
                        Return buffer3
                    End If
                End If
                ProjectData.ClearProjectError()
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                ProjectData.ClearProjectError()
            Finally
                MyBase.Close()
            End Try
            Return Nothing
        End Function

        Public Sub ResetGPS(ByVal pCount As Integer)
            Dim queue As New Queue
            Dim buffer As Byte() = New Byte() {8, 0, 10, 8, &H59, &H10}
            Dim num2 As Integer = pCount
            Dim i As Integer = 1
            Do While (i <= num2)
                Try
                    If Not MyBase.IsOpen Then
                        MyBase.Open()
                    End If
                    MyBase.DiscardInBuffer()
                    MyBase.DiscardOutBuffer()
                    queue.Clear()
                    MyBase.Write(buffer, 0, buffer.Length)
Label_0067:
                    queue.Enqueue(MyBase.ReadByte)
                    Dim array As Integer() = New Integer(((queue.Count - 1) + 1) - 1) {}
                    queue.CopyTo(array, 0)
                    Me._QueueContain = GPSCamera.Tools.DataConvert.DecimalToHex(array, ",")
                    If (((queue.Count <= 1) OrElse Not Operators.ConditionalCompareObjectEqual(queue.Dequeue, 1, False)) OrElse Not Operators.ConditionalCompareObjectEqual(queue.Dequeue, 1, False)) Then
                        If Operators.ConditionalCompareObjectEqual(queue.Peek, 0, False) Then
                            Throw New Exception("Download mode is break.")
                        End If
                        GoTo Label_0067
                    End If
                Catch exception1 As TimeoutException
                    ProjectData.SetProjectError(exception1)
                    Dim exception As TimeoutException = exception1
                    ProjectData.ClearProjectError()
                Catch exception3 As Exception
                    ProjectData.SetProjectError(exception3)
                    Dim exception2 As Exception = exception3
                    ProjectData.ClearProjectError()
                Finally
                    MyBase.Close()
                End Try
                System.Threading.Thread.Sleep(300)
                i += 1
            Loop
        End Sub

        Private Function SendCommand(ByVal pCommand As Byte(), ByVal pResult As Byte()) As Boolean
            Dim flag As Boolean
            Dim queue As New Queue
            Me._QueueContain = ""
            Try
                If Not MyBase.IsOpen Then
                    MyBase.Open()
                End If
                MyBase.DiscardInBuffer()
                MyBase.DiscardOutBuffer()
                queue.Clear()
                MyBase.Write(pCommand, 0, pCommand.Length)
                If ((pCommand(2) = 2) And ((pCommand(3) = 10) Or (pCommand(3) = 11))) Then
                    System.Threading.Thread.Sleep(&HBB8)
                End If
                Do
                    queue.Enqueue(MyBase.ReadByte)
                Loop While (queue.Count <= 1)
                Dim flag2 As Boolean = False
                Dim array As Integer() = New Integer(((queue.Count - 1) + 1) - 1) {}
                queue.CopyTo(array, 0)
                Me._QueueContain = GPSCamera.Tools.DataConvert.DecimalToHex(array, ",")
                Dim upperBound As Integer = pResult.GetUpperBound(0)
                Dim i As Integer = 0
                Do While (i <= upperBound)
                    If Operators.ConditionalCompareObjectEqual(queue.Dequeue, pResult(i), False) Then
                        flag2 = True
                    Else
                        flag2 = False
                    End If
                    i += 1
                Loop
                flag = flag2
            Catch exception1 As TimeoutException
                ProjectData.SetProjectError(exception1)
                Dim exception As TimeoutException = exception1
                flag = False
                ProjectData.ClearProjectError()
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                flag = False
                ProjectData.ClearProjectError()
            End Try
            Return flag
        End Function

        Public Function SwitchToAtmel() As Boolean
            Dim flag As Boolean
            Dim queue As New Queue
            Dim bytes As Byte() = GPSCamera.Tools.DataConvert.GetBytes(&H17, &HFF)
            bytes(0) = 8
            bytes(1) = 0
            bytes(2) = 9
            bytes(3) = 1
            bytes(4) = 5
            bytes((bytes.GetUpperBound(0) - 2)) = Me.GetCheckSum(bytes, 2, &H12)
            bytes((bytes.GetUpperBound(0) - 1)) = &H59
            bytes(bytes.GetUpperBound(0)) = &H10
            Try
                If Not Me.IsOpen Then
                    Me.Open()
                End If
                Me.DiscardOutBuffer()
                queue.Clear()
                Me.Write(bytes, 0, bytes.Length)
                System.Threading.Thread.Sleep(&HBB8)
                Do
                    queue.Enqueue(Me.ReadByte)
                Loop While (((queue.Count <= 1) OrElse Not Operators.ConditionalCompareObjectEqual(queue.Dequeue, 1, False)) OrElse Not Operators.ConditionalCompareObjectEqual(queue.Dequeue, 1, False))
                flag = True
            Catch exception1 As TimeoutException
                ProjectData.SetProjectError(exception1)
                Dim exception As TimeoutException = exception1
                flag = False
                ProjectData.ClearProjectError()
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                flag = False
                ProjectData.ClearProjectError()
            End Try
            Return flag
        End Function

        Public Function SwitchToBooree() As Boolean
            Dim flag As Boolean
            Dim queue As New Queue
            Dim bytes As Byte() = GPSCamera.Tools.DataConvert.GetBytes(&H17, &HFF)
            bytes(0) = 8
            bytes(1) = 0
            bytes(2) = 9
            bytes(3) = 1
            bytes(4) = 4
            bytes((bytes.GetUpperBound(0) - 2)) = Me.GetCheckSum(bytes, 2, &H12)
            bytes((bytes.GetUpperBound(0) - 1)) = &H59
            bytes(bytes.GetUpperBound(0)) = &H10
            Try
                If Not Me.IsOpen Then
                    Me.Open()
                End If
                Me.DiscardOutBuffer()
                queue.Clear()
                Me.Write(bytes, 0, bytes.Length)
                System.Threading.Thread.Sleep(&HBB8)
                Do
                    queue.Enqueue(Me.ReadByte)
                Loop While (((queue.Count <= 1) OrElse Not Operators.ConditionalCompareObjectEqual(queue.Dequeue, 1, False)) OrElse Not Operators.ConditionalCompareObjectEqual(queue.Dequeue, 1, False))
                flag = True
            Catch exception1 As TimeoutException
                ProjectData.SetProjectError(exception1)
                Dim exception As TimeoutException = exception1
                flag = False
                ProjectData.ClearProjectError()
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                flag = False
                ProjectData.ClearProjectError()
            End Try
            Return flag
        End Function

        Private Function ValueToByte(ByVal pValue As Long, ByVal pType As TypeCode) As Byte()
            Dim str As String = ""
            Select Case pType
                Case TypeCode.Int16
                    str = Conversion.Hex(pValue).PadLeft(4, "0"c)
                    Dim buffer3 As Byte() = New Byte(2 - 1) {}
                    Dim upperBound As Integer = buffer3.GetUpperBound(0)
                    Dim i As Integer = 0
                    Do While (i <= upperBound)
                        buffer3(i) = Convert.ToByte(str.Substring((i * 2), 2), &H10)
                        i += 1
                    Loop
                    Return DirectCast(buffer3.Clone, Byte())
                Case TypeCode.Int32
                    str = Conversion.Hex(pValue).PadLeft(8, "0"c)
                    Dim buffer2 As Byte() = New Byte(4 - 1) {}
                    Dim num3 As Integer = buffer2.GetUpperBound(0)
                    Dim j As Integer = 0
                    Do While (j <= num3)
                        buffer2(j) = Convert.ToByte(str.Substring((j * 2), 2), &H10)
                        j += 1
                    Loop
                    Return DirectCast(buffer2.Clone, Byte())
            End Select
            Return Nothing
        End Function

        Public Overloads Function Write(ByVal pTarget As DataItem, ByVal pPageSize As Short) As Boolean
            Dim buffer As Byte()
            Dim sourceArray As Byte() = Nothing
            Dim flag As Boolean
            Dim pCommand As Byte() = New Byte() {8, 3}
            Dim buffer3 As Byte() = New Byte() {8, 6}
            Dim buffer4 As Byte() = New Byte() {8, 0, 8, 4, &H59, &H10}
            Select Case pTarget
                Case DataItem.DrvG
                    buffer = New Byte() {5, 5}
                    sourceArray = Me.m_DrvG
                    Exit Select
                Case DataItem.DrvS
                    buffer = New Byte() {5, 5}
                    sourceArray = Me.m_DrvS
                    Exit Select
                Case DataItem.DrvB
                    buffer = New Byte() {5, 5}
                    sourceArray = Me.m_DrvB
                    Exit Select
                Case DataItem.Dbg
                    buffer = New Byte() {6, 5}
                    sourceArray = Me.m_Dbg
                    Exit Select
                Case DataItem.Voc
                    buffer = New Byte() {7, 5}
                    sourceArray = Me.m_Voc
                    Exit Select
                Case DataItem.Vow
                    buffer = New Byte() {7, 14}
                    sourceArray = Me.m_Vow
                    Exit Select
                Case DataItem.Vot
                    buffer = New Byte() {7, &H10}
                    sourceArray = Me.m_Vot
                    Exit Select
                Case DataItem.Agps
                    buffer = New Byte() {10, 4}
                    sourceArray = Me.m_Agps
                    Exit Select
                Case Else
                    Return False
            End Select
            If (sourceArray Is Nothing) Then
                Throw New Exception("data is empty")
            End If
            If (sourceArray.Length < 1) Then
                Throw New Exception("data is empty")
            End If
            Try
                Dim num2 As Integer = 50
                Dim num As Integer = 0
                Dim dataHeader As Byte() = Me.GetDataHeader(CLng(sourceArray.Length), pPageSize)
                Dim dataHeaderCommand As Byte() = Me.GetDataHeaderCommand(buffer, dataHeader)
                Do While Not Me.SendCommand(dataHeaderCommand, Me.m_Finish)
                    num += 1
                    If (num >= num2) Then
                        Throw New Exception("send data header is timeout")
                    End If
                Loop
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Throw New Exception("send data header is timeout")
            End Try
            Try
                Dim num8 As Integer = 50
                Dim ticks As Long = DateAndTime.Now.Ticks
                Dim pWritedBytes As Long = 0
                Do
                    Dim pPercent As Integer = CInt(Math.Round(Conversion.Int(CDbl(((CDbl(pWritedBytes) / CDbl(sourceArray.Length)) * 100)))))
                    Dim num9 As Long = Conversions.ToLong(Interaction.IIf(((sourceArray.Length - pWritedBytes) < pPageSize), (sourceArray.Length - pWritedBytes), pPageSize))
                    Dim destinationArray As Byte() = New Byte((CInt((num9 - 1)) + 1) - 1) {}
                    Dim abyte As Byte() = Me.GetByte(pPageSize, &HFF)
                    Array.ConstrainedCopy(sourceArray, CInt(pWritedBytes), destinationArray, 0, CInt(num9))
                    Array.ConstrainedCopy(destinationArray, 0, [abyte], 0, destinationArray.Length)
                    Dim buffer9 As Byte() = Me.GetDataCommand(pCommand, [abyte], pPercent)
                    Dim num10 As Integer = 0
                    Do While True
                        If Me.SendCommand(buffer9, Me.m_Finish) Then
                            Exit Do
                        End If
                        num10 += 1
                        If (num10 >= num8) Then
                            Throw New Exception("send data is timeout")
                        End If
                        If (num10 < 1) Then
                            buffer9 = Me.GetDataCommand(buffer3, [abyte], pPercent)
                        End If
                    Loop
                    pWritedBytes = (pWritedBytes + num9)
                    Dim writingEvent As WritingEventHandler = Me.WritingEvent
                    If (Not writingEvent Is Nothing) Then
                        writingEvent.Invoke(pTarget, pWritedBytes, CLng(sourceArray.Length), pPercent, destinationArray)
                    End If
                Loop While (pWritedBytes < sourceArray.Length)
                If Not Me.SendCommand(buffer4, Me.m_Finish) Then
                    Throw New Exception("send write commit error!")
                End If
                Dim writtedEvent As WrittedEventHandler = Me.WrittedEvent
                If (Not writtedEvent Is Nothing) Then
                    writtedEvent.Invoke(pTarget)
                End If
                Dim now As DateTime = DateAndTime.Now
                Dim num7 As Long = now.Ticks
                now = New DateTime((num7 - ticks))
                Dim time2 As DateTime = now
                Dim minute As Long = time2.Minute
                time2 = New DateTime((num7 - ticks))
                now = time2
                Dim second As Long = now.Second
                flag = True
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim e As Exception = exception3
                Dim writeErrorEvent As WriteErrorEventHandler = Me.WriteErrorEvent
                If (Not writeErrorEvent Is Nothing) Then
                    writeErrorEvent.Invoke(pTarget, e)
                End If
                flag = False
                ProjectData.ClearProjectError()
            End Try
            Return flag
        End Function

        Public Function WriteHostInfo(ByVal pBuffer As Byte(), ByVal pPageIndex As Integer) As Boolean
            If ((pPageIndex < 0) Or (pPageIndex > 15)) Then
                pPageIndex = 0
            End If
            Dim queue As New Queue
            Dim bytes As Byte() = GPSCamera.Tools.DataConvert.GetBytes(&H108, &HFF)
            bytes(0) = 8
            bytes(1) = 0
            bytes(2) = 2
            bytes(3) = 13
            bytes(4) = CByte(pPageIndex)
            pBuffer.CopyTo(bytes, 5)
            bytes((bytes.GetUpperBound(0) - 2)) = Me.GetCheckSum(bytes)
            bytes((bytes.GetUpperBound(0) - 1)) = &H59
            bytes((bytes.GetUpperBound(0) - 0)) = &H10
            Try
                If Not MyBase.IsOpen Then
                    MyBase.Open()
                End If
                MyBase.DiscardInBuffer()
                MyBase.DiscardOutBuffer()
                queue.Clear()
                MyBase.Write(bytes, 0, bytes.Length)
                System.Threading.Thread.Sleep(&HBB8)
                Do While True
                    queue.Enqueue(MyBase.ReadByte)
                Loop
            Catch exception1 As TimeoutException
                ProjectData.SetProjectError(exception1)
                Dim exception As TimeoutException = exception1
                If (((queue.Count > 0) AndAlso Operators.ConditionalCompareObjectEqual(queue.Dequeue, 1, False)) AndAlso Operators.ConditionalCompareObjectEqual(queue.Dequeue, 1, False)) Then
                    ProjectData.ClearProjectError()
                    Return True
                End If
                ProjectData.ClearProjectError()
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                ProjectData.ClearProjectError()
            Finally
                MyBase.Close()
            End Try
            Return False
        End Function

        Public Function WriteHostInfo(ByVal pBuffer As Byte(), ByVal pPageIndex As Integer, ByVal pItem As HostInfo) As Boolean
            If ((pPageIndex < 0) Or (pPageIndex > 15)) Then
                pPageIndex = 0
            End If
            Dim queue As New Queue
            Dim bytes As Byte() = GPSCamera.Tools.DataConvert.GetBytes(&H17, &HFF)
            If (pPageIndex = 0) Then
                Select Case pItem
                    Case HostInfo.Company
                        bytes(2) = 2
                        bytes(3) = 2
                        Exit Select
                    Case HostInfo.Motherboard
                        bytes(2) = 2
                        bytes(3) = 4
                        Exit Select
                    Case HostInfo.Project
                        bytes(2) = 2
                        bytes(3) = 6
                        Exit Select
                    Case HostInfo.Password
                        bytes(2) = 2
                        bytes(3) = 9
                        Exit Select
                    Case HostInfo.DbgVersion
                        bytes(2) = 6
                        bytes(3) = 4
                        Exit Select
                    Case HostInfo.DbgUpdateTime
                        bytes(2) = 6
                        bytes(3) = 6
                        Exit Select
                    Case HostInfo.VoiceVersion
                        bytes(2) = 7
                        bytes(3) = 4
                        Exit Select
                    Case HostInfo.VoiceUpdateTime
                        bytes(2) = 7
                        bytes(3) = 6
                        Exit Select
                    Case HostInfo.Product
                        bytes(2) = 2
                        bytes(3) = &H13
                        Exit Select
                    Case HostInfo.County
                        bytes(2) = 2
                        bytes(3) = &H11
                        Exit Select
                End Select
            End If
            bytes(0) = 8
            bytes(1) = 0
            pBuffer.CopyTo(bytes, 4)
            bytes((bytes.GetUpperBound(0) - 2)) = Me.GetCheckSum(bytes)
            bytes((bytes.GetUpperBound(0) - 1)) = &H59
            bytes((bytes.GetUpperBound(0) - 0)) = &H10
            Try
                If Not MyBase.IsOpen Then
                    MyBase.Open()
                End If
                MyBase.DiscardOutBuffer()
                queue.Clear()
                MyBase.Write(bytes, 0, bytes.Length)
                System.Threading.Thread.Sleep(&HBB8)
                Do While True
                    queue.Enqueue(MyBase.ReadByte)
                Loop
            Catch exception1 As TimeoutException
                ProjectData.SetProjectError(exception1)
                Dim exception As TimeoutException = exception1
                If (((queue.Count > 0) AndAlso Operators.ConditionalCompareObjectEqual(queue.Dequeue, 1, False)) AndAlso Operators.ConditionalCompareObjectEqual(queue.Dequeue, 1, False)) Then
                    ProjectData.ClearProjectError()
                    Return True
                End If
                ProjectData.ClearProjectError()
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                ProjectData.ClearProjectError()
            Finally
                MyBase.Close()
            End Try
            Return False
        End Function


        ' Properties
        Public Property Agps As Byte()
            Get
                Return Me.m_Agps
            End Get
            Set(ByVal value As Byte())
                Me.m_Agps = value
            End Set
        End Property

        Public Property Dbg As Byte()
            Get
                Return Me.m_Dbg
            End Get
            Set(ByVal value As Byte())
                Me.m_Dbg = value
            End Set
        End Property

        Public Property DrvB As Byte()
            Get
                Return Me.m_DrvB
            End Get
            Set(ByVal value As Byte())
                Me.m_DrvB = value
            End Set
        End Property

        Public Property DrvG As Byte()
            Get
                Return Me.m_DrvG
            End Get
            Set(ByVal value As Byte())
                Me.m_DrvG = value
            End Set
        End Property

        Public Property DrvS As Byte()
            Get
                Return Me.m_DrvS
            End Get
            Set(ByVal value As Byte())
                Me.m_DrvS = value
            End Set
        End Property

        Public ReadOnly Property IsDetect As Boolean
            Get
                Return Me.m_IsDetect
            End Get
        End Property

        Public ReadOnly Property QueueContain As String
            Get
                Return Me._QueueContain
            End Get
        End Property

        Public Property SetBaudRate As Integer
            Get
                Return Me.m_BaudRate
            End Get
            Set(ByVal value As Integer)
                Me.m_BaudRate = value
            End Set
        End Property

        Public Property Voc As Byte()
            Get
                Return Me.m_Voc
            End Get
            Set(ByVal value As Byte())
                Me.m_Voc = value
            End Set
        End Property

        Public Property Vot As Byte()
            Get
                Return Me.m_Vot
            End Get
            Set(ByVal value As Byte())
                Me.m_Vot = value
            End Set
        End Property

        Public Property Vow As Byte()
            Get
                Return Me.m_Vow
            End Get
            Set(ByVal value As Byte())
                Me.m_Vow = value
            End Set
        End Property



    End Class

End Namespace
