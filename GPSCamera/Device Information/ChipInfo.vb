Namespace DeviceInformation

    Friend Class ChipInfo


        Private m_Custom As Byte()
        Private m_Size As Integer = &H40
        Private m_SN As Byte()


        Public ReadOnly Property Custom As Byte()
            Get
                Return Me.m_Custom
            End Get
        End Property

        Public ReadOnly Property RadarDbg As Byte
            Get
                Return Me.m_Custom(&H1F)
            End Get
        End Property

        Public ReadOnly Property SN As Byte()
            Get
                Return Me.m_SN
            End Get
        End Property


        Public Function GetFullData() As Byte()
            Dim bytes As Byte() = GPSCamera.Tools.DataConvert.GetBytes(((Me.m_Size * 2) - 1), &HFF)
            System.Array.ConstrainedCopy(Me.m_Custom, 0, bytes, 0, Me.m_Custom.Length)
            System.Array.ConstrainedCopy(Me.m_SN, 0, bytes, Me.m_Size, Me.m_SN.Length)
            Return bytes
        End Function


        Public Sub New(ByVal pBuffer As Byte())
            Me.m_Custom = New Byte(((Me.m_Size - 1) + 1) - 1) {}
            Me.m_SN = New Byte(&H10 - 1) {}
            If (Not pBuffer Is Nothing) Then
                Dim array As Byte() = New Byte((pBuffer.GetUpperBound(0) + 1) - 1) {}
                pBuffer.CopyTo(array, 0)
                System.Array.ConstrainedCopy(array, 0, Me.m_Custom, 0, Me.m_Size)
                System.Array.ConstrainedCopy(array, &H40, Me.m_SN, 0, &H10)
            End If
        End Sub

    End Class

End Namespace