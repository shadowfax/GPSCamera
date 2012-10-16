Namespace DeviceInformation

    Friend Class HostInfo

        Private m_ChipDeviceID As Byte()
        Private m_Company As Byte()
        Private m_County As Byte()
        Private m_DatabaseUpdateUTC As Byte()
        Private m_DatabaseVersion As Byte()
        Private m_DriverUpdateUTC As Byte()
        Private m_DriverVersion As Byte()
        Private m_Model As Byte()
        Private m_PageList As ArrayList
        Private m_Password As Byte()
        Private m_Product As Byte()
        Private m_Size As Integer
        Private m_SN As Byte()
        Private m_VoiceUpdateUTC As Byte()
        Private m_VoiceVersion As Byte()

        Public ReadOnly Property ChipDeviceID As Byte()
            Get
                Return Me.m_ChipDeviceID
            End Get
        End Property

        Public Property Company As Byte()
            Get
                Return Me.m_Company
            End Get
            Set(ByVal value As Byte())
                Me.m_Company = value
            End Set
        End Property

        Public Property County As Byte()
            Get
                Return Me.m_County
            End Get
            Set(ByVal value As Byte())
                Me.m_County = value
            End Set
        End Property

        Public Property DatabaseUpdateUTC As Byte()
            Get
                Return Me.m_DatabaseUpdateUTC
            End Get
            Set(ByVal value As Byte())
                Me.m_DatabaseUpdateUTC = value
            End Set
        End Property

        Public Property DatabaseVersion As Byte()
            Get
                Return Me.m_DatabaseVersion
            End Get
            Set(ByVal value As Byte())
                Me.m_DatabaseVersion = value
            End Set
        End Property

        Public Property DriverUpdateUTC As Byte()
            Get
                Return Me.m_DriverUpdateUTC
            End Get
            Set(ByVal value As Byte())
                Me.m_DriverUpdateUTC = value
            End Set
        End Property

        Public Property DriverVersion As Byte()
            Get
                Return Me.m_DriverVersion
            End Get
            Set(ByVal value As Byte())
                Me.m_DriverVersion = value
            End Set
        End Property

        Public Property Model As Byte()
            Get
                Return Me.m_Model
            End Get
            Set(ByVal value As Byte())
                Me.m_Model = value
            End Set
        End Property

        Public Property Password As Byte()
            Get
                Return Me.m_Password
            End Get
            Set(ByVal value As Byte())
                Me.m_Password = value
            End Set
        End Property

        Public Property Product As Byte()
            Get
                Return Me.m_Product
            End Get
            Set(ByVal value As Byte())
                Me.m_Product = value
            End Set
        End Property

        Public Property SN As Byte()
            Get
                Return Me.m_SN
            End Get
            Set(ByVal value As Byte())
                Me.m_SN = value
            End Set
        End Property

        Public Property VoiceUpdateUTC As Byte()
            Get
                Return Me.m_VoiceUpdateUTC
            End Get
            Set(ByVal value As Byte())
                Me.m_VoiceUpdateUTC = value
            End Set
        End Property

        Public Property VoiceVersion As Byte()
            Get
                Return Me.m_VoiceVersion
            End Get
            Set(ByVal value As Byte())
                Me.m_VoiceVersion = value
            End Set
        End Property

        Public Function GetFullData() As Byte()
            Dim bytes As Byte() = GPSCamera.Tools.DataConvert.GetBytes(&H100, &HFF)
            System.Array.ConstrainedCopy(Me.m_Company, 0, bytes, (Me.m_Size * 0), Me.m_Size)
            System.Array.ConstrainedCopy(Me.m_Model, 0, bytes, (Me.m_Size * 1), Me.m_Size)
            System.Array.ConstrainedCopy(Me.m_SN, 0, bytes, (Me.m_Size * 2), Me.m_Size)
            System.Array.ConstrainedCopy(Me.m_Password, 0, bytes, (Me.m_Size * 3), Me.m_Size)
            System.Array.ConstrainedCopy(Me.m_DriverVersion, 0, bytes, (Me.m_Size * 4), Me.m_Size)
            System.Array.ConstrainedCopy(Me.m_DriverUpdateUTC, 0, bytes, (Me.m_Size * 5), Me.m_Size)
            System.Array.ConstrainedCopy(Me.m_DatabaseVersion, 0, bytes, (Me.m_Size * 6), Me.m_Size)
            System.Array.ConstrainedCopy(Me.m_DatabaseUpdateUTC, 0, bytes, (Me.m_Size * 7), Me.m_Size)
            System.Array.ConstrainedCopy(Me.m_VoiceVersion, 0, bytes, (Me.m_Size * 8), Me.m_Size)
            System.Array.ConstrainedCopy(Me.m_VoiceUpdateUTC, 0, bytes, (Me.m_Size * 9), Me.m_Size)
            System.Array.ConstrainedCopy(Me.m_Product, 0, bytes, (Me.m_Size * 13), Me.m_Size)
            System.Array.ConstrainedCopy(Me.m_County, 0, bytes, (Me.m_Size * 15), Me.m_Size)
            Return bytes
        End Function


        Public Function GetPageItem(ByVal pIndex As Integer) As Byte()
            If (Me.m_PageList.Count < 1) Then
                Return Nothing
            End If
            If ((pIndex < 0) Or (pIndex > Me.m_PageList.Count)) Then
                Return Nothing
            End If
            Return DirectCast(Me.m_PageList.Item(pIndex), Byte())
        End Function


        Private Sub SetPageList(ByVal pSourceBuffer As Byte())
            If (Not pSourceBuffer Is Nothing) Then
                Dim array As Byte() = New Byte((pSourceBuffer.GetUpperBound(0) + 1) - 1) {}
                pSourceBuffer.CopyTo(array, 0)
                Dim num2 As Integer = ((array.Length / Me.m_Size) - 1)
                Dim i As Integer = 0
                Do While (i <= num2)
                    Dim bytes As Byte() = GPSCamera.Tools.DataConvert.GetBytes(Me.m_Size, &HFF)
                    System.Array.ConstrainedCopy(array, (i * Me.m_Size), bytes, 0, Me.m_Size)
                    Me.m_PageList.Add(bytes)
                    i += 1
                Loop
            End If
        End Sub

        Public Sub New()
            Me.m_Size = &H10
            Me.m_Company = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_Model = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_SN = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_Password = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_DriverVersion = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_DriverUpdateUTC = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_DatabaseVersion = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_DatabaseUpdateUTC = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_VoiceVersion = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_VoiceUpdateUTC = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_ChipDeviceID = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_Product = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_County = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_PageList = New ArrayList
        End Sub

        Public Sub New(ByVal pBuffer As Byte())
            Me.m_Size = &H10
            Me.m_Company = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_Model = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_SN = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_Password = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_DriverVersion = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_DriverUpdateUTC = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_DatabaseVersion = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_DatabaseUpdateUTC = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_VoiceVersion = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_VoiceUpdateUTC = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_ChipDeviceID = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_Product = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_County = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_PageList = New ArrayList
            If (Not pBuffer Is Nothing) Then
                Dim array As Byte() = New Byte((pBuffer.GetUpperBound(0) + 1) - 1) {}
                pBuffer.CopyTo(array, 0)
                Me.SetPageList(pBuffer)
                System.Array.ConstrainedCopy(array, 0, Me.m_Company, 0, Me.m_Size)
                System.Array.ConstrainedCopy(array, &H10, Me.m_Model, 0, Me.m_Size)
                System.Array.ConstrainedCopy(array, &H20, Me.m_SN, 0, Me.m_Size)
                System.Array.ConstrainedCopy(array, &H30, Me.m_Password, 0, Me.m_Size)
                System.Array.ConstrainedCopy(array, &H40, Me.m_DriverVersion, 0, Me.m_Size)
                System.Array.ConstrainedCopy(array, 80, Me.m_DriverUpdateUTC, 0, Me.m_Size)
                System.Array.ConstrainedCopy(array, &H60, Me.m_DatabaseVersion, 0, Me.m_Size)
                System.Array.ConstrainedCopy(array, &H70, Me.m_DatabaseUpdateUTC, 0, Me.m_Size)
                System.Array.ConstrainedCopy(array, &H80, Me.m_VoiceVersion, 0, Me.m_Size)
                System.Array.ConstrainedCopy(array, &H90, Me.m_VoiceUpdateUTC, 0, Me.m_Size)
                System.Array.ConstrainedCopy(array, 160, Me.m_ChipDeviceID, 0, Me.m_Size)
                System.Array.ConstrainedCopy(array, &HD0, Me.m_Product, 0, Me.m_Size)
                System.Array.ConstrainedCopy(array, 240, Me.m_County, 0, Me.m_Size)
            End If
        End Sub



    End Class

End Namespace