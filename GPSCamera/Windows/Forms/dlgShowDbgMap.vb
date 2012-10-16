Imports Microsoft.VisualBasic.CompilerServices

Namespace Windows.Forms


    Public Class dlgShowDbgMap

        Private m_Amount As Integer
        Private m_ChooseAmount As Integer
        Private m_ChooseCounty As ArrayList
        Private m_CountyCode As Hashtable
        Private m_DbgInfo As Object
        Private m_PreviousCounty As Object



        Public Property Amount As Integer
            Get
                Return m_Amount
            End Get
            Set(ByVal value As Integer)
                m_Amount = value
            End Set
        End Property

        Public ReadOnly Property ChooseCounty As ArrayList
            Get
                Return Me.m_ChooseCounty
            End Get
        End Property


        Public ReadOnly Property CountyCode As Hashtable
            Get
                Return Me.m_CountyCode
            End Get
        End Property

        Public Property DbgInfo As Object
            Get
                Return m_DbgInfo
            End Get
            Set(ByVal value As Object)
                m_DbgInfo = value
            End Set
        End Property

        Public Property PreviousCounty As Object
            Get
                Return m_PreviousCounty
            End Get
            Set(ByVal value As Object)
                m_PreviousCounty = value
            End Set
        End Property


        Public Sub SetCountyCode()
            Try
                Dim enumerator As IEnumerator = Nothing
                Dim enumerator2 As IEnumerator = Nothing
                Me.m_CountyCode.Clear()
                Me.m_ChooseCounty.Clear()
                Try
                    enumerator = Me.Panel1.Controls.GetEnumerator
                    Do While enumerator.MoveNext
                        Dim objectValue As Object = System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(enumerator.Current)
                        If (objectValue.GetType.Name.ToString = "CheckBox") Then
                            Me.m_CountyCode.Add(System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, Nothing, "Tag", New Object(0 - 1) {}, Nothing, Nothing, Nothing)), System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, Nothing, "Tag", New Object(0 - 1) {}, Nothing, Nothing, Nothing)))
                        End If
                    Loop
                Finally
                    If TypeOf enumerator Is IDisposable Then
                        TryCast(enumerator, IDisposable).Dispose()
                    End If
                End Try
                Try
                    enumerator2 = DirectCast(Me.DbgInfo, IEnumerable).GetEnumerator
                    Do While enumerator2.MoveNext
                        Dim current As DictionaryEntry = DirectCast(enumerator2.Current, DictionaryEntry)
                        Dim box As New System.Windows.Forms.CheckBox
                        box = DirectCast(NewLateBinding.LateGet(Me.Panel1.Controls, Nothing, "Item", New Object() {System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(current.Key)}, Nothing, Nothing, Nothing), System.Windows.Forms.CheckBox)
                        Me.m_ChooseCounty.Add(box.Name)
                        Me.m_CountyCode.Remove(System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(box.Tag))
                    Loop
                Finally
                    If TypeOf enumerator2 Is IDisposable Then
                        TryCast(enumerator2, IDisposable).Dispose()
                    End If
                End Try
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
                ProjectData.ClearProjectError()
            End Try
        End Sub






        Private Sub dlgShowDbgMap_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        End Sub

        Private Sub dlgShowDbgMap_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        End Sub

        Public Sub New()

            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            AddHandler MyBase.FormClosing, New System.Windows.Forms.FormClosingEventHandler(AddressOf Me.dlgShowDbgMap_FormClosing)
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.dlgShowDbgMap_Load)
            Me.m_ChooseAmount = 0
            Me.m_ChooseCounty = New ArrayList
            Me.m_CountyCode = New Hashtable

        End Sub
    End Class

End Namespace