using DevExpress.XtraMap;
using System.Windows.Forms;

namespace WinForms_MapControl_InformationLayer {
    public partial class Form1 : Form {
        InformationLayer GeocodeLayer { get { return (InformationLayer)mapControl1.Layers["GeocodeLayer"]; } }
        BingGeocodeDataProvider GeocodeProvider { get { return (BingGeocodeDataProvider)GeocodeLayer.DataProvider; } }

        public Form1() {
            InitializeComponent();
            GeocodeProvider.LocationInformationReceived += GeocodeProvider_LocationInformationReceived;
        }

        void GeocodeProvider_LocationInformationReceived(object sender, LocationInformationReceivedEventArgs e) {
            if ((e.Cancelled) && (e.Result.ResultCode != RequestResultCode.Success)) return;

            GeocodeLayer.Data.Items.Clear();
            foreach (LocationInformation locationInformation in e.Result.Locations)
                GeocodeLayer.Data.Items.Add(new MapCallout() {
                    Location = locationInformation.Location,
                    Text = locationInformation.DisplayName
                });
        }
    }
}
