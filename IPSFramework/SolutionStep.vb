Public Class SolutionStep
    Public servedRequests As HashSet(Of Tuple(Of Integer, Integer)) ' entspricht J
    Public Lambda As HashSet(Of Tuple(Of Integer, Integer))
    Public kundenPositionen As List(Of Integer) ' entspricht p
    Public waiterPosition As Integer
    Public teilZFW As Integer
    Public stepBefore As SolutionStep
    Public addedRequest As Tuple(Of Integer, Integer)
    Public Sub New() ' Default Konstruktor
        Me.servedRequests = New HashSet(Of Tuple(Of Integer, Integer))
        Me.Lambda = New HashSet(Of Tuple(Of Integer, Integer))
        Me.kundenPositionen = New List(Of Integer)
        Me.waiterPosition = 0
        Me.teilZFW = 0
        Me.stepBefore = Nothing
    End Sub
    Public Sub New(ByRef stepBefore As SolutionStep)
        Me.stepBefore = stepBefore
        Me.servedRequests = New HashSet(Of Tuple(Of Integer, Integer))(stepBefore.servedRequests)
        Me.Lambda = New HashSet(Of Tuple(Of Integer, Integer))(stepBefore.Lambda)
        Me.kundenPositionen = New List(Of Integer)(stepBefore.kundenPositionen)
        Me.waiterPosition = stepBefore.waiterPosition
        Me.teilZFW = stepBefore.teilZFW
    End Sub
End Class
