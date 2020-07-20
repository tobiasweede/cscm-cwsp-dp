Module main
    Public OrderSequence() As Integer
    Public SKUSequenz() As Integer


    Sub Main()

        For Each foundFile As String In My.Computer.FileSystem.GetFiles(CurDir() & "\Data\") 'batch load
            Dim NewInstance As New Instance

            NewInstance.LoadInst(foundFile)
            Debug.Print("hallo Welt")
        Next

    End Sub

End Module
