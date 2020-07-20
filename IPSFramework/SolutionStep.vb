Public Class SoltuionStep
    Public plannedTasks As HashSet(Of Tuple(Of Integer, Integer))
    Public Lambda As HashSet(Of Tuple(Of Integer, Integer))
    Public kundenPositionen As List(Of Integer)
    Public waiterPosition As Integer
    Public teilZFW As Integer
    Public stepBefore As SoltuionStep
    Public Sub New() ' Default Konstruktor
        Me.plannedTasks = New HashSet(Of Tuple(Of Integer, Integer))
        Me.Lambda = New HashSet(Of Tuple(Of Integer, Integer))
        Me.kundenPositionen = New List(Of Integer)
        Me.waiterPosition = -1
        Me.teilZFW = 0
        Me.stepBefore = Nothing
    End Sub
    Public Sub New(ByRef stepBefore As SoltuionStep,
                   ByVal waiterPosition As Integer,
                   ByVal teilZFW As Integer
                   )
        Me.stepBefore = stepBefore
        Me.plannedTasks = New HashSet(Of Tuple(Of Integer, Integer))(stepBefore.plannedTasks)
        Me.Lambda = New HashSet(Of Tuple(Of Integer, Integer))(stepBefore.Lambda)
        Me.kundenPositionen = New List(Of Integer)(stepBefore.kundenPositionen)
        Me.waiterPosition = stepBefore.waiterPosition
        Me.teilZFW = stepBefore.teilZFW
    End Sub
End Class
