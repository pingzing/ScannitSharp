using ScannitSharp.Bindings;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
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
                foreach (var foundCard in (await reader.FindAllCardsAsync()))
                {
                    var card = await CardOperations.ReadTravelCardAsync(foundCard);
                }
            }
        }

        private async void Reader_CardAdded(SmartCardReader sender, CardAddedEventArgs args)
        {
            try
            {
                TravelCard card = await CardOperations.ReadTravelCardAsync(args.SmartCard);
                StringBuilder allProperties = new StringBuilder();
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(card))
                {
                    allProperties.AppendLine($"{descriptor.Name}={descriptor.GetValue(card)}");
                }
                PropertiesTextBlock.Text = allProperties.ToString();
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
