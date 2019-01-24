Imports System.Data.SqlClient

Module Module1

    Sub Main2()
        Dim lst = New List(Of List(Of String))()
        Dim dict2 = New Dictionary(Of String, List(Of String))()


        Dim sDeptGUID = ""
        dict2.Add("x", New List(Of String)() From {"123"})
        dict2("x").Add("abc")
        dict2.Add("z", New List(Of String)() From {sDeptGUID})
        Console.WriteLine(dict2("x")(0))
        Console.WriteLine(dict2("x")(1))
        Console.WriteLine(dict2.ContainsKey("y"))
        Console.WriteLine(dict2.ContainsKey("x"))

        For Each k As String In dict2.Keys
            Console.WriteLine(k)
        Next

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
        dt.Rows.Add("c", "12", "210.16")
        dt.Rows.Add("d", "20", "122.6")

        Console.WriteLine("m IN ('" & String.Join("','", dict2("x").ToArray()) & "')")


        Dim rows = dt.[Select]("m IN ('" & String.Join("','", dict2("x").ToArray()) & "')")
        Console.WriteLine(rows.Length)



        Console.ReadKey()
    End Sub

    Sub Main()
        Dim myConnection As SqlConnection = New SqlConnection("server=10.5.10.78\SQL2008R2_LY;database=erp306_lianying_njlsy;uid=team_LY;pwd=95938")
        Dim myCommand As SqlCommand = New SqlCommand("select top 1 1 from p_EcommerceUnit", myConnection)
        Dim MyAdapter As SqlDataAdapter
        Dim a_ds As New DataSet
        Dim dt As DataTable
        MyAdapter = New SqlDataAdapter
        myCommand.CommandType = CommandType.Text
        MyAdapter.SelectCommand = myCommand

        Try
            MyAdapter.Fill(a_ds)
            dt = a_ds.Tables(0)
        Finally
            myConnection.Close()
        End Try

        Console.WriteLine("123abc")
        Dim i As Integer = dt.Rows(0)(0).ToString

        Console.WriteLine(i)

        Console.ReadKey()
    End Sub

End Module
