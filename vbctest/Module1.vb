Module Module1

    Sub Main()
        Dim s As String = "xxx.c.123.cvbdsffadec"
        Dim idx As Integer = s.LastIndexOf(".")

        If idx = -1 Or idx = s.Length - 1 Then
            Console.WriteLine("!")
            Console.ReadKey()
            Return
        End If
        If "3" > 2 Then
            Console.WriteLine(1)
        End If
        Dim CostShortCode As String = s.Substring(idx + 1)
        Console.WriteLine(CostShortCode)
        Console.ReadKey()
    End Sub

End Module
