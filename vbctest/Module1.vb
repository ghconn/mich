Imports System.Linq.Expressions
Imports System.Reflection
Imports System.Threading
Imports System.Data

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


        Dim dict = New Dictionary(Of String, String)() From {
            {"1", "x"},
            {"2", "y"},
            {"3", "a"},
            {"4", "b"},
            {"5", "c"}
        }
        Console.WriteLine(ConcatToXml(dict))

        Console.WriteLine(Environment.Is64BitOperatingSystem)
        'JSer._Main()

        Dim f = Single.Parse("220538.29")
        Console.WriteLine(f.ToString("f2"))


        Dim sx = $"123,
                                {f},{1}"
        Console.WriteLine(sx)

        Dim i As Integer
        For i = 0 To 2
            Console.WriteLine("for" & i)
        Next

        Dim vals As String() = New String(5) {}
        vals(0) = 1
        vals(1) = 1
        vals(2) = "2"
        For Each val As String In vals
            If val < 2 Then
                Continue For
            End If

            Console.WriteLine("5")

        Next

        Dim lst = New List(Of List(Of String))()
        Dim dict2 = New Dictionary(Of String, List(Of String))()



        dict2.Add("x", New List(Of String)() From {"123"})
        dict2("x").Add("abc")
        Console.WriteLine(dict2("x")(0))
        Console.WriteLine(dict2("x")(1))
        Console.WriteLine(dict2.ContainsKey("y"))
        Console.WriteLine(dict2.ContainsKey("x"))

        Console.ReadKey()
    End Sub

    Private Async Sub d()
        Dim i = Await Task.Run(Function()
                                   Thread.Sleep(5000)
                                   Return 1

                               End Function)
        Console.WriteLine(i.ToString())
    End Sub

    Public Function ConcatToXml(variables As Dictionary(Of String, String)) As String
        Dim declaration = "<?xml version=""1.0"" encoding=""UTF-8""?>"
        Dim xml = declaration + "<root>"
        For Each v As Object In variables
            xml &= $"<{v.Key}>{v.Value}</{v.Key}>"
        Next
        xml &= "</root>"
        Return xml
    End Function


    Public Function ExpressionTree(Of T, V)(list As List(Of T), propertyName As String, propertyValue As V) As List(Of T)
        Dim parameter As ParameterExpression = Expression.Parameter(GetType(T), "x")
        Dim value As ParameterExpression = Expression.Parameter(GetType(V), "propertyValue")
        Dim setter As MethodInfo = GetType(T).GetMethod("set_" + propertyName)
        Dim [call] As MethodCallExpression = Expression.[Call](parameter, setter, value)
        'Dim lambda = Expression.Lambda([call], parameter, value)   '这种方式极慢
        Dim lambda = Expression.Lambda(Of Action(Of T, V))([call], parameter, value) 'Function(x, propertyValue) x.set_xxx(propertyValue)
        Dim exp = lambda.Compile()
        Dim i = 0
        While i < List.Count
            'exp.DynamicInvoke(list(i), propertyValue)  '这种方式极慢
            exp(List(i), propertyValue)
            i += 1
        End While

        Return List
    End Function

    Private Sub createdatatable()
        Dim dt = New DataTable()
        dt.Columns.Add(New DataColumn("m"))
        dt.Columns.Add(New DataColumn("year"))
        dt.Columns.Add(New DataColumn("scope"))

        dt.Rows.Add("a", "18", "20.2")
        dt.Rows.Add("b", "18", "2.6")
        dt.Rows.Add("a", "18", "22.2")
        dt.Rows.Add("a", "19", "29.66")
        dt.Rows.Add("b", "18", "20.16")
        dt.Rows.Add("b", "18", "12.6")

        Dim rows = dt.[Select]("m='a' and year=" + 18)
        Console.WriteLine(rows.Length)
    End Sub

End Module
