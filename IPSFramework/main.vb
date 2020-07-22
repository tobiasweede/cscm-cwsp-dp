﻿Module main
    Public OrderSequence() As Integer
    Public SKUSequenz() As Integer
    Public Const v_cust As Integer = 1 ' Geschwindigkeit des Kunden
    Public Const v_wait As Integer = 1 ' Geschwindigkeit des Waiters
    Public Const tau As Integer = 2 ' Verarbeitungszeit von Task ik (hier konstant)
    Public Phi As HashSet(Of Tuple(Of Integer, Integer)) ' Menge der (Kunden, Essensindex) Zuordnungen
    Public Requests As List(Of List(Of Integer)) ' Matrix d_ik (Als Liste von Listen realisiert, da effizienter) der (Essensindex, Tresen) Zuordnungen

    Sub Main()
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(CurDir() & "\Data\") 'batch load
            Dim NewInstance As New Instance
            NewInstance.LoadInst(foundFile)
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
            Phi = New HashSet(Of Tuple(Of Integer, Integer)) ' Menge der (Kunden, Essensindex) Zuordnungen
            Requests = New List(Of List(Of Integer)) ' Matrix (Als Liste von Listen realisiert, da effizienter) der (Essensindex, Tresen) Zuordnungen
            For Each kunde In C
                Dim KundenBestellung As List(Of Integer) = New List(Of Integer) 'Liste mit Tresennummer mit für d_ik
                Dim PosKundenBestellung As Integer = 0 'Posiion der Bestellung bzgl. des betrachteten Kunden
                For Each bestellung In NewInstance.Order(kunde).OrderSKU
                    Dim zuordnung As Tuple(Of Integer, Integer) = New Tuple(Of Integer, Integer)(kunde, PosKundenBestellung)
                    KundenBestellung.Add(bestellung)
                    Phi.Add(zuordnung)
                    PosKundenBestellung += 1
                Next
                Requests.Add(KundenBestellung)
            Next
            Dim T As Integer = Phi.Count ' Anzahl der Serviceperioden
            ' List containing all Stages with all Steps
            Dim stages As List(Of Dictionary(Of Integer, SolutionStep)) = New List(Of Dictionary(Of Integer, SolutionStep))
            For i = 0 To T 'Für alle Serviceperioden
                Dim stage As Dictionary(Of Integer, SolutionStep)
                If i = 0 Then ' Virtueller initStep
                    Dim initStep As SolutionStep = New SolutionStep
                    For j = 0 To n - 1 ' Initialisiere Kundenpositionen
                        initStep.kundenPositionen.Add(-j)
                    Next
                    stage = New Dictionary(Of Integer, SolutionStep)
                    stage.Add(initStep.teilZFW, initStep) ' Füge initStep zur Stage hinzu
                    stages.Add(stage) ' Füge Stage zu Liste der Stages hinzu
                    Continue For
                End If
                stage = New Dictionary(Of Integer, SolutionStep) ' Dictionary für aktuelle Stage
                For Each previousStepKVPair In stages(i - 1)
                    Dim feasibleRequests As List(Of Tuple(Of Integer, Integer)) = getFeasibleRequests(previousStepKVPair.Value)
                    For Each request In feasibleRequests
                        Dim actualStep As SolutionStep = New SolutionStep(previousStepKVPair.Value)
                        actualStep.servedRequests.Add(request)
                        Dim movementDistance As Integer = Requests(request.Item1)(request.Item2) - actualStep.kundenPositionen(request.Item1)
                        For pos As Integer = 0 To actualStep.kundenPositionen.Count - 1
                            actualStep.kundenPositionen(pos) += movementDistance
                        Next
                        actualStep.waiterPosition = Requests(request.Item1)(request.Item2)
                        addStepTime(request, previousStepKVPair.Value, actualStep) ' Füge delta(Z,Z') hinzu
                        stage.Add(actualStep.teilZFW, actualStep) ' Füge actualStep zur Stage hinzu
                    Next
                Next
                stages.Add(stage) ' Füge Stage zu Liste der Stages hinzu
                Debug.Print(Requests(0)(0).ToString)
            Next
        Next
    End Sub

    Function getFeasibleRequests(ByRef previousStep As SolutionStep) As List(Of Tuple(Of Integer, Integer))
        Dim feasibleRequests As List(Of Tuple(Of Integer, Integer)) = New List(Of Tuple(Of Integer, Integer))
        ' Dim tmp As Tuple(Of Integer, Integer) = New Tuple(Of Integer, Integer)(0, 0)
        Dim movementDistance As Integer = 1
        While True 'Movement loop, solange bis unzulässig
            ' Prüfe ob nicht bediente Requests vorhanden
            If previousStep.servedRequests.Equals(Phi) Then
                Exit While ' Alles bedient
            End If
            ' Prüfe, ob ein Request zulässig ist oder ob breits zu weit gelaufen
            For Each request In Phi
                If previousStep.servedRequests.Contains(request) Then
                    Continue For ' Schon bedient
                End If
                Dim evalPosition As Integer = previousStep.kundenPositionen(request.Item1) + movementDistance
                If Requests(request.Item1)(request.Item2) = evalPosition Then
                    feasibleRequests.Add(request) ' Am richtigen Tresen
                ElseIf Requests(request.Item1)(request.Item2) < evalPosition Then
                    Exit While ' Zu weit gelaufen
                End If
            Next
            movementDistance += 1 ' eins weiter
        End While
        Return feasibleRequests
    End Function

    Sub addStepTime(ByRef request As Tuple(Of Integer, Integer),
                    ByRef previousStep As SolutionStep,
                    ByRef actualStep As SolutionStep)
        actualStep.teilZFW += Math.Max(Math.Abs(previousStep.waiterPosition - actualStep.waiterPosition) / v_wait, ' |w' - w | / v_wait
                                        Requests(request.Item1)(request.Item2) - previousStep.kundenPositionen(request.Item1) / v_cust ' d_jl - p_j  / v_cust
                                        ) + tau
    End Sub

End Module
