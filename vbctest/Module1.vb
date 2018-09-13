Imports System.Linq.Expressions
Imports System.Reflection
Imports System.Threading

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


        'JSer._Main()

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

End Module
