using System;
using System.Diagnostics;
using Windows.Devices.Enumeration;
using Windows.Devices.SmartCards;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ScannitSharp.UwpExample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            string selector = SmartCardReader.GetDeviceSelector();
            DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(selector);

            foreach (DeviceInformation device in devices)
            {
                SmartCardReader reader = await SmartCardReader.FromIdAsync(device.Id);
                reader.CardAdded += Reader_CardAdded;
                reader.CardRemoved += Reader_CardRemoved;
            }
        }

        private void Reader_CardAdded(SmartCardReader sender, CardAddedEventArgs args)
        {
            try
            {
                var card = CardOperations.ReadTravelCardAsync(args.SmartCard);
            }

            catch (Exception e)
            {
                Debug.WriteLine($"Oh noes: {e}");
            }
        }

        private void Reader_CardRemoved(SmartCardReader sender, CardRemovedEventArgs args)
        {
            // Update UI, etc.
        }
    }
}
