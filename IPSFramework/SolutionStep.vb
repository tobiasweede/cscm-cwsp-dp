Public Class SoltuionStep
    Public _plannedTasks As List(Of Tuple(Of Integer, Integer))
    Public _predecessors As List(Of Tuple(Of Integer, Integer))
    Public _kundenPositionen As List(Of Integer)
    Public _waiterPosition As Integer
    Public _teilZFW As Integer
    Public Sub New()
        _plannedTasks = New List(Of Tuple(Of Integer, Integer))
        _predecessors = New List(Of Tuple(Of Integer, Integer))
        _kundenPositionen = New List(Of Integer)
        _WaiterPosition = -1
        _TeilZFW = 0
    End Sub
    Public Sub New(ByRef plannedTasks As List(Of Tuple(Of Integer, Integer)),
                   ByRef predecessors As List(Of Tuple(Of Integer, Integer)),
                   ByRef kundenPositionen As List(Of Integer)
                   )
        _plannedTasks = New List(Of Tuple(Of Integer, Integer))(plannedTasks)
    End Sub
End Class
