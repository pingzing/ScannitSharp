using ScannitSharp;
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
                byte[] selection = (await connection.TransmitAsync(Commands.SelectHslCommand.AsBuffer())).ToArray();
                if (selection != null
                    && selection.Length > 0
                    && selection.SequenceEqual(Commands.OkResponse))
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

                    appInfo = (await connection.TransmitAsync(Commands.ReadAppInfoCommand.AsBuffer())).ToArray();
                    controlInfo = (await connection.TransmitAsync(Commands.ReadControlInfoCommand.AsBuffer())).ToArray();
                    periodPass = (await connection.TransmitAsync(Commands.ReadPeriodPassCommand.AsBuffer())).ToArray();
                    storedValue = (await connection.TransmitAsync(Commands.ReadStoredValueCommand.AsBuffer())).ToArray();
                    eTicket = (await connection.TransmitAsync(Commands.ReadETicketCommand.AsBuffer())).ToArray();
                    hist1 = (await connection.TransmitAsync(Commands.ReadHistoryCommand.AsBuffer())).ToArray();

                    // If we have more history, the last two bytes of the history array will contain the MORE_DATA bytes.
                    if (hist1.Skip(Math.Max(0, hist1.Length - 2)).ToArray() == Commands.MoreDataResponse)
                    {
                        hist2 = (await connection.TransmitAsync(Commands.ReadNextCommand.AsBuffer())).ToArray();
                    }

                    // Combine the two history chunks into a single array, minus their last two MORE_DATA bytes
                    history = hist1.Take(hist1.Length - 2)
                                     .Concat(hist2.Take(hist2.Length - 2)).ToArray();

                    return TravelCard.CreateTravelCard(appInfo, controlInfo, periodPass, storedValue, eTicket, history);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
