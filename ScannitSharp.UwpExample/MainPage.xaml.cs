using ScannitSharp;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SmartCards;
using Windows.UI.Core;
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
                    await ReadCard(foundCard);
                }
            }
        }

        private async void Reader_CardAdded(SmartCardReader sender, CardAddedEventArgs args)
        {
            await ReadCard(args.SmartCard);
        }

        private async Task ReadCard(SmartCard card)
        {
            try
            {
                TravelCard travelCard = await CardOperations.ReadTravelCardAsync(card);
                StringBuilder builder = new StringBuilder();
                BuildPropertyString(travelCard, 0, builder);

                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    PropertiesTextBlock.Text = builder.ToString();
                });
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

        private void BuildPropertyString(object obj, int indentLevel, StringBuilder builder)
        {
            if (obj == null)
                return;
            string indentString = new string(' ', indentLevel);
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(obj);
                if (value is IEnumerable enumerable && !(value is string))
                {
                    foreach (var element in enumerable)
                    {
                        BuildPropertyString(element, indentLevel + 5, builder);
                    }
                }
                else
                {
                    if (descriptor.PropertyType.Assembly == obj.GetType().Assembly)
                    {
                        builder.AppendLine($"{indentString}{name}:");
                        BuildPropertyString(value, indentLevel + 5, builder);
                    }
                    else
                    {
                        builder.AppendLine($"{indentString}{name}: {value}");
                    }
                }
            }
        }
    }
}
