Imports System.Text

Public Class JSer
    Public Shared Sub _Main()
        Dim ent = New mdl.chartpie(Of mdl.point)() With {
            .datasets = New List(Of mdl.dataset(Of mdl.point))() From {
                New mdl.dataset(Of mdl.point)() With {
                    .backgroundColor = New String() {"red", "green"},
                    .data = New mdl.point() {New mdl.point() With {
                        .x = 0.1F,
                        .y = 0.2F
                    }, New mdl.point() With {
                        .x = 0.3F,
                        .y = 0.5F
                    }}
                },
                New mdl.dataset(Of mdl.point)() With {
                    .backgroundColor = New String() {"blue", "dark"},
                    .data = New mdl.point() {New mdl.point() With {
                        .x = 0.123F,
                        .y = 0.99F
                    }, New mdl.point() With {
                        .x = 0.7F,
                        .y = 0.17F
                    }}
                }
            },
            .labels = New String() {"mingyuan", "pactera"}
        }

        Dim sw As New Stopwatch()
        sw.Start()
        Dim val = JSer.ComplexObjToString(ent)
        Console.WriteLine(val)
        sw.[Stop]()
        Console.WriteLine(sw.ElapsedMilliseconds)
    End Sub

    Public Shared Function ObjToString(obj As Object, ParamArray properties As String()) As String
        Dim re = "{"
        If properties.Length <> 0 Then
            re += String.Join(",", properties.[Select](Function(p) $"""{p}"":""{obj.GetType().GetProperty(p).GetValue(obj)}"""))
        Else
            re += String.Join(",", obj.[GetType]().GetProperties().[Select](Function(p) $"""{p.Name}"":""{p.GetValue(obj)}"""))
        End If
        re &= "}"
        Return re
    End Function

    Public Shared Function ComplexObjToString(obj As Object) As String
        Dim re = New StringBuilder()
        If obj.[GetType]().IsArray OrElse (obj.[GetType]().IsGenericType AndAlso obj.[GetType]().GetInterface("IEnumerable") IsNot Nothing) Then
            re.Append(ArrayAndGenericToString(obj))
        Else
            re.Append(SimpleObjToString(obj))
        End If
        Return re.ToString()
    End Function

    Private Shared Function ArrayAndGenericToString(obj As Object) As StringBuilder
        Dim re = New StringBuilder("[")

        Dim ie As IEnumerable = CType(obj, IEnumerable)
        For Each element As Object In ie
            Dim type = element?.GetType()
            If element Is Nothing Then
                re.Append("null,")
            ElseIf type = GetType(Integer) OrElse type = GetType([Single]) OrElse type = GetType(Int16) OrElse type = GetType(Long) OrElse type = GetType(UInteger) OrElse type = GetType(Byte) OrElse type = GetType(SByte) OrElse type = GetType(ULong) OrElse type = GetType(Double) Then
                re.Append($"{element},")
            ElseIf type = GetType(String) OrElse type = GetType(DateTime) Then
                re.Append($"""{element}"",")
            ElseIf type = GetType([Boolean]) Then
                re.Append($"{element.ToString().ToLower()},")
            Else
                re.Append(ComplexObjToString(element)).Append(",")
            End If
        Next

        If re(re.Length - 1) = ","c Then
            re.Remove(re.Length - 1, 1)
        End If
        re.Append("]")
        Return re
    End Function

    Private Shared Function SimpleObjToString(obj As Object) As StringBuilder
        Dim re = New StringBuilder("{")

        For Each p As Object In obj.[GetType]().GetProperties()
            Dim val = p.GetValue(obj)
            ' Not Implemented
            Dim type = val?.GetType()
            If val Is Nothing Then
                re.Append($"""{p.Name}"":null,")
            ElseIf type = GetType(Integer) OrElse type = GetType([Single]) OrElse type = GetType(Int16) OrElse type = GetType(Long) OrElse type = GetType(UInteger) OrElse type = GetType(Byte) OrElse type = GetType(SByte) OrElse type = GetType(ULong) OrElse type = GetType(Double) Then
                re.Append($"""{p.Name}"":{val},")
            ElseIf type = GetType(String) OrElse type = GetType(DateTime) Then
                re.Append($"""{p.Name}"":""{val}"",")
            ElseIf type = GetType([Boolean]) Then
                re.Append($"""{p.Name}"":{val.ToString().ToLower()},")
            Else
                re.Append($"""{p.Name}"":").Append(ComplexObjToString(val)).Append(","c)
            End If
        Next

        If re(re.Length - 1) = ","c Then
            re.Remove(re.Length - 1, 1)
        End If
        re.Append("}")
        Return re
    End Function
End Class