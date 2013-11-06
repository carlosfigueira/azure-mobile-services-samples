Imports Microsoft.WindowsAzure.MobileServices

' The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' An empty page that can be used on its own or navigated to within a Frame.
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page

    Private Const AppUrlSetting As String = "AppUrl"
    Private Const AppKeySetting As String = "AppKey"

    Private Shared Property MobileService As MobileServiceClient

    ''' <summary>
    ''' Invoked when this page is about to be displayed in a Frame.
    ''' </summary>
    ''' <param name="e">Event data that describes how this page was reached.  The Parameter
    ''' property is typically used to configure the page.</param>
    Protected Overrides Sub OnNavigatedTo(e As Navigation.NavigationEventArgs)
        Dim localSettings = Windows.Storage.ApplicationData.Current.LocalSettings
        If localSettings.Values.ContainsKey(AppUrlSetting) Then
            Me.txtAppUrl.Text = CType(localSettings.Values(AppUrlSetting), String)
        End If
        If localSettings.Values.ContainsKey(AppKeySetting) Then
            Me.txtAppKey.Text = CType(localSettings.Values(AppKeySetting), String)
        End If
    End Sub

    Private Sub btnStart_Click(sender As Object, e As RoutedEventArgs)
        Try
            If MobileService Is Nothing Then
                MobileService = New MobileServiceClient(Me.txtAppUrl.Text, Me.txtAppKey.Text)
                Dim localSettings = Windows.Storage.ApplicationData.Current.LocalSettings
                localSettings.Values(AppUrlSetting) = Me.txtAppUrl.Text
                localSettings.Values(AppKeySetting) = Me.txtAppKey.Text
            End If

            Post_96bdf254_2738_43aa_b146_3d95d84f599e.Run(Me, MobileService)
        Catch ex As Exception
            AddToDebug("Error: {0}", ex)
        End Try
    End Sub

    Public Sub AddToDebug(ByVal text As String, ByVal ParamArray args As Object())
        If args IsNot Nothing And args.Length > 0 Then text = String.Format(text, args)
        Me.txtDebug.Text = Me.txtDebug.Text & text & Environment.NewLine
    End Sub

    Private Sub btnReset_Click(sender As Object, e As RoutedEventArgs)
        MobileService = Nothing
        Dim localSettings = Windows.Storage.ApplicationData.Current.LocalSettings
        localSettings.Values.Remove(AppUrlSetting)
        localSettings.Values.Remove(AppKeySetting)
    End Sub
End Class
