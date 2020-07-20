Module main
    Public OrderSequence() As Integer
    Public SKUSequenz() As Integer


    Sub Main()
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(CurDir() & "\Data\") 'batch load
            Dim NewInstance As New Instance
            NewInstance.LoadInst(foundFile)
            Dim v_cust As Integer = 1 ' Geschwindigkeit des Kunden
            Dim v_wait As Integer = 1 ' Geschwindigkeit des Waiters
            Dim tau_ik As Integer = 2 ' Verarbeitungszeit von Task ik (hier konstant)
            Dim m As Integer = NewInstance.SKUs ' Anzahl Essen
            Dim n As Integer = NewInstance.Order.Length ' Anzahl Kunden
            Dim D As HashSet(Of Integer) = New HashSet(Of Integer) ' Menge der Essen
            For i = 0 To n - 1
                D.Add(i)
            Next
            Dim C As HashSet(Of Integer) = New HashSet(Of Integer) ' Menge der Kunden
            For kunde = 0 To m - 1
                C.Add(kunde)
            Next
            Dim Phi As HashSet(Of Tuple(Of Integer, Integer)) = New HashSet(Of Tuple(Of Integer, Integer)) ' Menge der (Kunden, Essensindex) Zuordnungen
            Dim d_ik As List(Of List(Of Integer)) = New List(Of List(Of Integer)) ' Matrix (Als Liste von Listen realisiert, da effizienter) der (Essensindex, Tresen) Zuordnungen
            For Each kunde In C
                Dim KundenBestellung As List(Of Integer) = New List(Of Integer) 'Liste mit Tresennummer mit für d_ik
                Dim PosKundenBestellung As Integer = 1 'Posiion der Bestellung bzgl. des betrachteten Kunden
                For Each bestellung In NewInstance.Order(kunde).OrderSKU
                    Dim zuordnung As Tuple(Of Integer, Integer) = New Tuple(Of Integer, Integer)(kunde, PosKundenBestellung)
                    KundenBestellung.Add(bestellung)
                    Phi.Add(zuordnung)
                    PosKundenBestellung += 1
                Next
                d_ik.Add(KundenBestellung)
            Next
            Dim T As Integer = Phi.Count ' Anzahl der Serviceperioden
            ' List containing all Stages with all Steps
            Dim stages As List(Of Dictionary(Of Integer, SoltuionStep)) = New List(Of Dictionary(Of Integer, SoltuionStep))
            For i = 0 To T 'Für alle Serviceperioden
                If i = 0 Then ' Virtueller initStep
                    Dim initStep As SoltuionStep = New SoltuionStep
                    For j = 0 To n - 1 ' Initialisiere Kundenpositionen
                        initStep.kundenPositionen.Add(-i)
                    Next
                    Dim stage As Dictionary(Of Integer, SoltuionStep) = New Dictionary(Of Integer, SoltuionStep)
                    Continue For
                End If
                Dim stage As Dictionary(Of Integer, SoltuionStep) = New Dictionary(Of Integer, SoltuionStep)

            Next
        Next
        Debug.Print("hallo Welt")
    End Sub

End Module
