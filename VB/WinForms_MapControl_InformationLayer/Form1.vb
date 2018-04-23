Imports DevExpress.XtraMap
Imports System.Windows.Forms

Namespace WinForms_MapControl_InformationLayer
    Partial Public Class Form1
        Inherits Form

        Private ReadOnly Property GeocodeLayer() As InformationLayer
            Get
                Return CType(mapControl1.Layers("GeocodeLayer"), InformationLayer)
            End Get
        End Property
        Private ReadOnly Property GeocodeProvider() As BingGeocodeDataProvider
            Get
                Return CType(GeocodeLayer.DataProvider, BingGeocodeDataProvider)
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            AddHandler GeocodeProvider.LocationInformationReceived, AddressOf GeocodeProvider_LocationInformationReceived
        End Sub

        Private Sub GeocodeProvider_LocationInformationReceived(ByVal sender As Object, ByVal e As LocationInformationReceivedEventArgs)
            If (e.Cancelled) AndAlso (e.Result.ResultCode <> RequestResultCode.Success) Then
                Return
            End If

            GeocodeLayer.Data.Items.Clear()
            For Each locationInformation As LocationInformation In e.Result.Locations
                GeocodeLayer.Data.Items.Add(New MapCallout() With {.Location = locationInformation.Location, .Text = locationInformation.DisplayName})
            Next locationInformation
        End Sub
    End Class
End Namespace
