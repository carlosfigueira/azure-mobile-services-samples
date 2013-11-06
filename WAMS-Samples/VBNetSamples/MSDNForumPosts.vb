Imports Microsoft.WindowsAzure.MobileServices
Imports Newtonsoft.Json

Public Class Post_96bdf254_2738_43aa_b146_3d95d84f599e

    Public Shared Async Sub Run(ByVal page As MainPage, ByVal client As MobileServiceClient)
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
    End Sub

    <DataTable("foo")> _
    Public Class Test
        <JsonProperty("id")> _
        Public Property Id As String
        <JsonProperty("text")> _
        Public Property Text As String
    End Class
End Class
