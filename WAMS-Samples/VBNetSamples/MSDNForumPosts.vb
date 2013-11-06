Imports Microsoft.WindowsAzure.MobileServices
Imports Newtonsoft.Json

'http://social.msdn.microsoft.com/Forums/windowsazure/en-US/96bdf254-2738-43aa-b146-3d95d84f599e/it-seems-impossible-to-do-a-simple-where?forum=azuremobile
Public Class Post_96bdf254_2738_43aa_b146_3d95d84f599e

    Public Shared Async Function Run(ByVal page As MainPage, ByVal client As MobileServiceClient) As Task
        Dim table = client.GetTable(Of Test)()
        Dim item = New Test
        item.Text = "first"
        Await table.InsertAsync(item)
        page.AddToDebug("Inserted: {0}-{1}", item.Id, item.Text)
        item = New Test
        item.Text = "second"
        Await table.InsertAsync(item)
        page.AddToDebug("Inserted: {0}-{1}", item.Id, item.Text)

        Dim Letter = "f"c

        Dim retrieved = Await table.Where(Function(p) p.Text.StartsWith(Letter)).ToListAsync()
        page.AddToDebug("Retrieved: [{0}]", String.Join(", ", retrieved.Select(Function(p) String.Format("{0}-{1}", p.Id, p.Text))))
    End Function

    <DataTable("foo")> _
    Public Class Test
        <JsonProperty("id")> _
        Public Property Id As Integer
        <JsonProperty("text")> _
        Public Property Text As String
    End Class
End Class

'http://social.msdn.microsoft.com/Forums/windowsazure/en-US/d9dd10a9-3f23-4990-892e-bc73b002ea69/simple-where?forum=azuremobile
Public Class Post_d9dd10a9_3f23_4990_892e_bc73b002ea69

    Public Shared Async Function Run(ByVal page As MainPage, ByVal client As MobileServiceClient) As Task
        Dim table = client.GetTable(Of Test)()
        Dim item = New Test
        item.Text = "first"
        item.Number = 1
        Await table.InsertAsync(item)
        page.AddToDebug("Inserted: {0}", item)
        item = New Test
        item.Text = "second"
        item.Number = 2
        Await table.InsertAsync(item)
        page.AddToDebug("Inserted: {0}", item)

        Dim Letter = "f"

        Dim retrieved = Await table.Where(Function(p) _
            p.Text = "first" And p.Number = 1).ToListAsync()
        page.AddToDebug("Retrieved: [{0}]", String.Join(", ", retrieved))

        retrieved = Await table.Where(Function(p) _
            p.Text.StartsWith("s")).ToListAsync()
        page.AddToDebug("Retrieved: [{0}]", String.Join(", ", retrieved))
    End Function

    <DataTable("foo")> _
    Public Class Test
        <JsonProperty("id")> _
        Public Property Id As Integer
        <JsonProperty("text")> _
        Public Property Text As String
        <JsonProperty("number")> _
        Public Property Number As Integer

        Public Overrides Function ToString() As String
            Return String.Format("{0}-{1}-{2}", Id, Text, Number)
        End Function
    End Class
End Class