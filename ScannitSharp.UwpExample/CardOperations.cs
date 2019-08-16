using ScannitSharp.Bindings;
using ScannitSharp.Bindings.Models;
using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.SmartCards;

namespace ScannitSharp.UwpExample
{
    public class CardOperations
    {
        /// <summary>
        /// Reads the travel card data from HSL Mifare DESFire card.
        /// </summary>
        /// <param name="card">The card to try to read.</param>
        /// <returns>A deserialized Travel Card object if the card was valid, otherwise null.</returns>
        public static async Task<TravelCard> ReadTravelCardAsync(SmartCard card)
        {
            using (SmartCardConnection connection = await card.ConnectAsync())
            {
                byte[] selection = (await connection.TransmitAsync(HslCommands.SelectHslCommand.AsBuffer())).ToArray();
                if (selection != null
                    && selection.Length > 0
                    && selection.SequenceEqual(HslCommands.OkResponse))
                {
                    // Travel card info bytes
                    byte[] appInfo = null;
                    byte[] controlInfo = null;
                    byte[] periodPass = null;
                    byte[] storedValue = null;
                    byte[] eTicket = null;
                    byte[] history = null;

                    // Temporary containers for history chunks
                    byte[] hist1 = new byte[2];
                    byte[] hist2 = new byte[2];

                    appInfo = (await connection.TransmitAsync(HslCommands.ReadAppInfoCommand.AsBuffer())).ToArray();
                    controlInfo = (await connection.TransmitAsync(HslCommands.ReadControlInfoCommand.AsBuffer())).ToArray();
                    periodPass = (await connection.TransmitAsync(HslCommands.ReadPeriodPassCommand.AsBuffer())).ToArray();
                    storedValue = (await connection.TransmitAsync(HslCommands.ReadStoredValueCommand.AsBuffer())).ToArray();
                    eTicket = (await connection.TransmitAsync(HslCommands.ReadETicketCommand.AsBuffer())).ToArray();
                    hist1 = (await connection.TransmitAsync(HslCommands.ReadHistoryCommand.AsBuffer())).ToArray();

                    // If we have more history, the last two bytes of the history array will contain the MORE_DATA bytes.
                    if (hist1.Skip(Math.Max(0, hist1.Length - 2)).ToArray() == HslCommands.MoreData)
                    {
                        hist2 = (await connection.TransmitAsync(HslCommands.ReadNextCommand.AsBuffer())).ToArray();
                    }

                    // Combine the two history chunks into a single array, minus their last two MORE_DATA bytes
                    history = hist1.Take(hist1.Length - 2)
                                     .Concat(hist2.Take(hist2.Length - 2)).ToArray();

                    var rawCard = Native.GetTravelCard(appInfo, controlInfo, periodPass, storedValue, eTicket, history);
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
