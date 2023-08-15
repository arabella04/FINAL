Imports MySql.Data.MySqlClient
Module Module1
    Dim host As String = "host=127.0.0.1;"
    Dim user As String = "user=root;"
    Dim pass As String = "password=root;"
    Dim db As String = "database=Emplorex;"
    Public conn As New MySqlConnection(host & user & pass & db)
End Module
