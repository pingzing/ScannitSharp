using OneOf;
using ScannitSharp.Models;
using ScannitSharp.Models.BoardingAreas;
using ScannitSharp.Models.BoardingLocations;
using ScannitSharp.Models.ProductCodes;
using ScannitSharp.Models.SaleDevices;
using ScannitSharp.Models.ValidityAreas;
using ScannitSharp.Models.ValidityLengths;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace ScannitSharp
{
    /// <summary>
    /// Represent an HSL travel card. Use the static <see cref="CreateTravelCard"/> 
    /// method to create an instance of this class.
    /// </summary>
    public class TravelCard
    {
        /// <summary>
        /// The version number of the application running on the HSL travel card.
        /// </summary>
        public byte ApplicationVersion { get; set; }

        /// <summary>
        /// Unknown.
        /// </summary>
        public byte ApplicationKeyVersion { get; set; }

        /// <summary>
        /// This card's ID string. Also located on the back of the physical card.
        /// </summary>
        public string ApplicationInstanceId { get; set; }

        /// <summary>
        /// Internal value. A 0 indicates that this is an 'NXP DESFire 4kB' card. Other values unknown.
        /// </summary>
        public byte PlatformType { get; set; }

        /// <summary>
        /// Whether or not this card is secured.
        /// </summary>
        public bool IsMacProtected { get; set; }

        /// <summary>
        /// The date and time this card was first initialized.
        /// </summary>
        public DateTimeOffset ApplicationIssuingDate { get; set; }

        /// <summary>
        /// Unknown.
        /// </summary>
        public bool ApplicationStatus { get; set; }

        /// <summary>
        /// Unknown.
        /// </summary>
        public byte ApplicationUnblockingNumber { get; set; }

        /// <summary>
        /// Number of transactions this card has performed.
        /// </summary>
        public uint ApplicationTransactionCounter { get; set; }

        /// <summary>
        /// Number of actions this card has performed.
        /// </summary>
        public uint ActionListCounter { get; set; }

        /// <summary>
        /// Contains this card's two most recent season passes.
        /// </summary>
        public PeriodPass PeriodPass { get; set; }

        /// <summary>
        /// The amount of monetary value on this card, represented as cents, in Euros.
        /// </summary>
        public uint StoredValueCents { get; set; }

        /// <summary>
        /// The date and time this card last had money loaded.
        /// </summary>
        public DateTimeOffset LastLoadDateTime { get; set; }

        /// <summary>
        /// The amount (as cents, in Euros) of money that was last loaded to the card.
        /// </summary>
        public uint LastLoadValue { get; set; }

        /// <summary>
        /// The ID of the organization that performed the last load operation.
        /// </summary>
        public ushort LastLoadOrganization { get; set; }

        /// <summary>
        /// The device number of the device that performed the last load operation.
        /// </summary>
        public ushort LastLoadDeviceNum { get; set; }

        /// <summary>
        /// Contains this card's most recent single ticket.
        /// </summary>
        public ETicket ETicket { get; set; }

        /// <summary>
        /// Contains up to eight of this card's most recent uses.
        /// </summary>
        public History[] History { get; set; }

        /// <summary>
        /// Creates an instance of a <see cref="TravelCard"/>. To obtain the byte arrays for this method,
        /// use the command's found in <see cref="Commands"/> with your platform's NFC transcieve functionality.
        /// </summary>
        /// <param name="appInfo">Byte array containing the card's AppInfo file. Can be obtained by sending the <see cref="Commands.ReadAppInfoCommand"/> command to the travel card.</param>
        /// <param name="controlInfo">Byte array containing the card's ControlInfo file. Can be obtained by sending the <see cref="Commands.ReadControlInfoCommand"/> command to the travel card.</param>
        /// <param name="periodPass">Byte array containing the card's PeriodPass file. Can be obtained by sending the <see cref="Commands.ReadPeriodPassCommand"/> command to the travel card.</param>
        /// <param name="storedValue">Byte array containing the card's StoredValue file. Can be obtained by sending the <see cref="Commands.ReadStoredValueCommand"/> command to the travel card.</param>
        /// <param name="eTicket">Byte array containing the card's ETicket file. Can be obtained by sending the <see cref="Commands.ReadETicketCommand"/> command to the travel card.</param>
        /// <param name="history">Byte array containing the card's History file. Can be obtained by sending the <see cref="Commands.ReadHistoryCommand"/> command to the travel card.
        /// Note that the response to <see cref="Commands.ReadHistoryCommand"/> will probably include a <see cref="Commands.MoreDataResponse"/> in the last two bytes, 
        /// indicating that you should then send a <see cref="Commands.ReadNextCommand"/>. The <see cref="Commands.MoreDataResponse"/> bytes should not be included in this array.</param>   
        /// <exception cref="ArgumentException">Thrown when passed values that are unable to be resolved into a valid <see cref="TravelCard"/>.</exception>
        public static TravelCard CreateTravelCard(
           byte[] appInfo,
           byte[] controlInfo,
           byte[] periodPass,
           byte[] storedValue,
           byte[] eTicket,
           byte[] history)
        {
            GCHandle pinnedAppInfo = GCHandle.Alloc(appInfo, GCHandleType.Pinned);
            IntPtr appInfoPtr = pinnedAppInfo.AddrOfPinnedObject();

            GCHandle pinnedControlInfo = GCHandle.Alloc(controlInfo, GCHandleType.Pinned);
            IntPtr controlInfoPtr = pinnedControlInfo.AddrOfPinnedObject();

            GCHandle pinnedPeriodPass = GCHandle.Alloc(periodPass, GCHandleType.Pinned);
            IntPtr periodPassPtr = pinnedPeriodPass.AddrOfPinnedObject();

            GCHandle pinnedStoredValue = GCHandle.Alloc(storedValue, GCHandleType.Pinned);
            IntPtr storedValuePtr = pinnedStoredValue.AddrOfPinnedObject();

            GCHandle pinnedETicket = GCHandle.Alloc(eTicket, GCHandleType.Pinned);
            IntPtr eTicketPtr = pinnedETicket.AddrOfPinnedObject();

            GCHandle pinnedHistory = GCHandle.Alloc(history, GCHandleType.Pinned);
            IntPtr historyPtr = pinnedHistory.AddrOfPinnedObject();

            IntPtr travelCardPtr = Native.create_travel_card(
                appInfoPtr, new UIntPtr((uint)appInfo.Length),
                controlInfoPtr, new UIntPtr((uint)controlInfo.Length),
                periodPassPtr, new UIntPtr((uint)periodPass.Length),
                storedValuePtr, new UIntPtr((uint)storedValue.Length),
                eTicketPtr, new UIntPtr((uint)eTicket.Length),
                historyPtr, new UIntPtr((uint)history.Length));


            try
            {
                using (FFITravelCardHandle travelCardHandle = new FFITravelCardHandle(travelCardPtr))
                {
                    pinnedAppInfo.Free();
                    pinnedControlInfo.Free();
                    pinnedPeriodPass.Free();
                    pinnedStoredValue.Free();
                    pinnedETicket.Free();
                    pinnedHistory.Free();

                    return travelCardHandle.AsTravelCard();
                }
            }
            catch (SEHException exception)
            {
                throw new ArgumentException("Unable to parse the byte data passed in as a valid travel card.", exception);
            }
        }

        internal TravelCard(FFITravelCard travelCard)
        {
            ActionListCounter = travelCard.action_list_counter;
            ApplicationInstanceId = new RustString(travelCard.application_instance_id).ToString();
            ApplicationIssuingDate = DateTimeOffset.FromUnixTimeSeconds(travelCard.application_issuing_date);
            ApplicationKeyVersion = travelCard.application_key_version;
            ApplicationStatus = travelCard.application_status != 0;
            ApplicationTransactionCounter = travelCard.application_transaction_counter;
            ApplicationUnblockingNumber = travelCard.application_unblocking_number;
            ApplicationVersion = travelCard.application_version;
            ETicket = new ETicket(travelCard.e_ticket);
            History = travelCard.history.AsFFIHistoryArray().Select(x => new History(x)).ToArray();
            IsMacProtected = travelCard.is_mac_protected != 0;
            LastLoadDateTime = DateTimeOffset.FromUnixTimeSeconds(travelCard.last_load_datetime);
            LastLoadDeviceNum = travelCard.last_load_device_num;
            LastLoadOrganization = travelCard.last_load_organization_id;
            LastLoadValue = travelCard.last_load_value;
            PeriodPass = new PeriodPass(travelCard.period_pass);
            PlatformType = travelCard.platform_type;
            StoredValueCents = travelCard.stored_value_cents;
        }
    }

    /// <summary>
    /// Represents a card's season pass.
    /// </summary>
    public class PeriodPass
    {
        /// <summary>
        /// The product code for the Season Pass 1. Indicates which fare and zone scheme this season pass uses.
        /// </summary>
        public OneOf<FaresFor2010, FaresFor2014> ProductCode1 { get; set; }

        /// <summary>
        /// The area in which Season Pass 1 allows travel.
        /// </summary>
        public OneOf<OldZone, NewZone, Models.ValidityAreas.Vehicle> ValidityArea1 { get; set; }

        /// <summary>
        /// Season Pass 1's starting validity date.
        /// </summary>
        public DateTimeOffset PeriodStartDate1 { get; set; }

        /// <summary>
        /// Season Pass 1's ending validity date.
        /// </summary>
        public DateTimeOffset PeriodEndDate1 { get; set; }


        /// <summary>
        /// The product code for the Season Pass 2. Indicates which fare and zone scheme this season pass uses.
        /// </summary>
        public OneOf<FaresFor2010, FaresFor2014> ProductCode2 { get; set; }

        /// <summary>
        /// The area in which Season Pass 2 allows travel.
        /// </summary>
        public OneOf<OldZone, NewZone, Models.ValidityAreas.Vehicle> ValidityArea2 { get; set; }

        /// <summary>
        /// Season Pass 2's starting validity date.
        /// </summary>
        public DateTimeOffset PeriodStartDate2 { get; set; }

        /// <summary>
        /// Season Pass 2's ending validity date.
        /// </summary>
        public DateTimeOffset PeriodEndDate2 { get; set; }


        /// <summary>
        /// The product loaded onto the card most recently.
        /// </summary>
        public OneOf<FaresFor2010, FaresFor2014> LoadedPeriodProduct { get; set; }

        /// <summary>
        /// The date and time that the most recent load occurred.
        /// </summary>
        public DateTimeOffset LoadedPeriodDateTime { get; set; }

        /// <summary>
        /// The number of days that were added to the season pass during the most recent load.
        /// </summary>
        public ushort LoadedPeriodLength { get; set; }


        /// <summary>
        /// The price of the most recent load, as cents, in Euros.
        /// </summary>
        public uint LoadedPeriodPrice { get; set; }

        /// <summary>
        /// The ID of the organization which performed the most recent load.
        /// </summary>
        public ushort LoadingOrganizationId { get; set; }

        /// <summary>
        /// The device number of the device which performed the most recent load.
        /// </summary>
        public ushort LoadingDeviceNumber { get; set; }


        /// <summary>
        /// The date and time this season pass was last used to board public transit.
        /// </summary>
        public DateTimeOffset LastBoardDateTime { get; set; }

        /// <summary>
        /// The vehicle number of the vehicle last boarded.
        /// </summary>
        public ushort LastBoardVehicleNumber { get; set; }

        /// <summary>
        /// The location of the last boarding, on either a bus, train or platform.
        /// </summary>
        public OneOf<NoneOrReserved, BusNumber, TrainNumber, PlatformNumber> LastBoardLocation { get; set; }

        /// <summary>
        /// The direction of the vehicle which was last boarded. Note that this value is speculation--the official documentation is unclear.
        /// </summary>
        public BoardingDirection LastBoardDirection { get; set; }

        /// <summary>
        /// The area in which the last boarding occurred, either in a new-style ABC-zone, a vehicle, or an old-style region number (a.k.a 'Zone Circle'). 
        /// </summary>
        public OneOf<Zone, Models.BoardingAreas.Vehicle, ZoneCircle> LastBoardArea { get; set; }

        internal PeriodPass(FFIPeriodPass periodPass)
        {
            ProductCode1 = ProductCode.Create(periodPass.product_code_1_kind, periodPass.product_code_1_value);
            ValidityArea1 = ValidityArea.Create(periodPass.validity_area_1_kind, periodPass.validity_area_1_buffer);
            PeriodStartDate1 = DateTimeOffset.FromUnixTimeSeconds(periodPass.period_start_date_1);
            PeriodEndDate1 = DateTimeOffset.FromUnixTimeSeconds(periodPass.period_end_date_1);

            ProductCode2 = ProductCode.Create(periodPass.product_code_2_kind, periodPass.product_code_2_value);
            ValidityArea2 = ValidityArea.Create(periodPass.validity_area_2_kind, periodPass.validity_area_2_buffer);
            PeriodStartDate2 = DateTimeOffset.FromUnixTimeSeconds(periodPass.period_start_date_2);
            PeriodEndDate2 = DateTimeOffset.FromUnixTimeSeconds(periodPass.period_end_date_2);

            LoadedPeriodProduct = ProductCode.Create(periodPass.loaded_period_product_kind, periodPass.loaded_period_product_value);
            LoadedPeriodDateTime = DateTimeOffset.FromUnixTimeSeconds(periodPass.loaded_period_datetime);
            LoadedPeriodLength = periodPass.loaded_period_length;
            LoadedPeriodPrice = periodPass.loaded_period_price;
            LoadingOrganizationId = periodPass.loading_organization;
            LoadingDeviceNumber = periodPass.loading_device_number;
            LastBoardDateTime = DateTimeOffset.FromUnixTimeSeconds(periodPass.last_board_datetime);
            LastBoardVehicleNumber = periodPass.last_board_vehicle_number;
            LastBoardLocation = BoardingLocation.Create(periodPass.last_board_location_kind, periodPass.last_board_location_value);
            LastBoardDirection = periodPass.last_board_direction;
            LastBoardArea = BoardingArea.Create(periodPass.last_board_area_kind, periodPass.last_board_area_value);
        }
    }

    /// <summary>
    /// The card's most recent single ticket.
    /// </summary>
    public class ETicket
    {
        /// <summary>
        /// The product code for the ticket. Indicates which fare and zone scheme this ticket pass uses.
        /// </summary>
        public OneOf<FaresFor2010, FaresFor2014> ProductCode { get; set; }

        /// <summary>
        /// Unknown.
        /// </summary>
        public byte CustomerProfile { get; set; }

        /// <summary>
        /// The language this ticket is in.
        /// </summary>
        public Language Language { get; set; }

        /// <summary>
        /// How long this ticket is valid for.
        /// </summary>
        public OneOf<Minutes, Hours, TwentyFourHourPeriods, Days> ValidityLength { get; set; }

        /// <summary>
        /// The area in which this ticket is valid.
        /// </summary>
        public OneOf<OldZone, NewZone, Models.ValidityAreas.Vehicle> ValidityArea { get; set; }

        /// <summary>
        /// The time and date this ticket was purchased.
        /// </summary>
        public DateTimeOffset SaleDateTime { get; set; }

        /// <summary>
        /// The device which sold this ticket.
        /// </summary>
        public OneOf<ServicePointSalesDevice, DriverTicketMachine, CardReader, TicketMachine, Server, HSLSmallEquipment, ExternalServiceEquipment, Reserved> SaleDevice { get; set; }

        /// <summary>
        /// The amount this ticket cost, as cents, in Euros.
        /// </summary>
        public ushort TicketFareCents { get; set; }

        /// <summary>
        /// The size of group this ticket was purchased for.
        /// </summary>
        public byte GroupSize { get; set; }


        /// <summary>
        /// If true, this is an extra zone extension purchased atop an existing season pass.
        /// </summary>
        public bool ExtraZone { get; set; }

        /// <summary>
        /// The validity area for the season pass associated with this extra zone ticket.
        /// </summary>
        public OneOf<OldZone, NewZone, Models.ValidityAreas.Vehicle> PeriodPassValidityArea { get; set; }

        /// <summary>
        /// The product code for this extension ticket.
        /// </summary>
        public OneOf<FaresFor2010, FaresFor2014> ExtensionProductCode { get; set; }

        /// <summary>
        /// The validity area of the first extension ticket.
        /// </summary>
        public OneOf<OldZone, NewZone, Models.ValidityAreas.Vehicle> ExtensionValidityArea1 { get; set; }

        /// <summary>
        /// The price paid for the first extension ticket.
        /// </summary>
        public ushort ExtensionFareCents1 { get; set; }

        /// <summary>
        /// The validity area of the second extension ticket.
        /// </summary>
        public OneOf<OldZone, NewZone, Models.ValidityAreas.Vehicle> ExtensionValidityArea2 { get; set; }

        /// <summary>
        /// The price paid for the second extension ticket.
        /// </summary>
        public ushort ExtensionFareCents2 { get; set; }

        /// <summary>
        /// Whether or not the this extension ticket is valid.
        /// </summary>
        public bool SaleStatus { get; set; }

        /// <summary>
        /// The date and time this ticket became valid.
        /// </summary>
        public DateTimeOffset ValidityStartDateTime { get; set; }

        /// <summary>
        /// The date and time this ticket expires.
        /// </summary>
        public DateTimeOffset ValidityEndDateTime { get; set; }

        /// <summary>
        /// Whether or not this ticket is valid.
        /// </summary>
        public bool ValidityStatus { get; set; }


        /// <summary>
        /// When this ticket was used to board public transit.
        /// </summary>
        public DateTimeOffset BoardingDateTime { get; set; }

        /// <summary>
        /// The ID of the vehicle this ticket was used to board.
        /// </summary>
        public ushort BoardingVehicle { get; set; }

        /// <summary>
        /// The location this ticket was used to board at.
        /// </summary>
        public OneOf<NoneOrReserved, BusNumber, TrainNumber, PlatformNumber> BoardingLocation { get; set; }

        /// <summary>
        /// The direction the vehicle was traveling when this ticket was used to board it.
        /// </summary>
        public BoardingDirection BoardingDirection { get; set; }

        /// <summary>
        /// The area in which boarding occurred.
        /// </summary>
        public OneOf<Zone, Models.BoardingAreas.Vehicle, ZoneCircle> BoardingArea { get; set; }

        internal ETicket(FFIETicket eTicket)
        {
            ProductCode = Models.ProductCodes.ProductCode.Create(eTicket.product_code_kind, eTicket.product_code_value);
            CustomerProfile = eTicket.customer_profile;
            Language = eTicket.language;
            ValidityLength = Models.ValidityLengths.ValidityLength.Create(eTicket.validity_length_kind, eTicket.validity_length_value);
            ValidityArea = Models.ValidityAreas.ValidityArea.Create(eTicket.validity_area_kind, eTicket.validity_area_value);
            SaleDateTime = DateTimeOffset.FromUnixTimeSeconds(eTicket.sale_datetime);
            SaleDevice = Models.SaleDevices.SaleDevice.Create(eTicket.sale_device_kind, eTicket.sale_device_value);
            TicketFareCents = eTicket.ticket_fare_cents;
            GroupSize = eTicket.group_size;
            ExtraZone = eTicket.extra_zone != 0;
            PeriodPassValidityArea = Models.ValidityAreas.ValidityArea.Create(eTicket.period_pass_validity_area_kind, eTicket.period_pass_validity_area_value);
            ExtensionProductCode = Models.ProductCodes.ProductCode.Create(eTicket.extension_product_code_kind, eTicket.extension_product_code_value);
            ExtensionValidityArea1 = Models.ValidityAreas.ValidityArea.Create(eTicket.extension_1_validity_area_kind, eTicket.extension_1_validity_area_value);
            ExtensionFareCents1 = eTicket.extension_1_fare_cents;
            ExtensionValidityArea2 = Models.ValidityAreas.ValidityArea.Create(eTicket.extension_2_validity_area_kind, eTicket.extension_2_validity_area_value);
            ExtensionFareCents2 = eTicket.extension_2_fare_cents;

            SaleStatus = eTicket.sale_status != 0;
            ValidityStartDateTime = DateTimeOffset.FromUnixTimeSeconds(eTicket.validity_start_datetime);
            ValidityEndDateTime = DateTimeOffset.FromUnixTimeSeconds(eTicket.validity_end_datetime);
            ValidityStatus = eTicket.validity_status != 0;

            BoardingDateTime = DateTimeOffset.FromUnixTimeSeconds(eTicket.boarding_datetime);
            BoardingVehicle = eTicket.boarding_vehicle;
            BoardingLocation = Models.BoardingLocations.BoardingLocation.Create(eTicket.boarding_location_kind, eTicket.boarding_location_value);
            BoardingDirection = eTicket.boarding_direction;
            BoardingArea = Models.BoardingAreas.BoardingArea.Create(eTicket.boarding_area_kind, eTicket.boarding_area_value);
        }
    }

    /// <summary>
    /// An entry in the list of previous uses of this travel card.
    /// </summary>
    public class History
    {
        /// <summary>
        /// Whether the last use was a season pass or a single ticket.
        /// </summary>
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// The date and time transit was boarded.
        /// </summary>
        public DateTimeOffset BoardingDateTime { get; set; }

        /// <summary>
        /// The date and time the transfer (if any) ended.
        /// </summary>
        public DateTimeOffset TransferEndDateTime { get; set; }

        /// <summary>
        /// The amount the ticket (if applicable) cost, as cents, in Euros.
        /// </summary>
        public ushort TicketFareCents { get; set; }

        /// <summary>
        /// The size of the group traveling on this ticket.
        /// </summary>
        public byte GroupSize { get; set; }

        /// <summary>
        /// The value left on the card after this transaction.
        /// </summary>
        public uint RemainingValue { get; set; }

        internal History(FFIHistory history)
        {
            TransactionType = history.transaction_type;
            BoardingDateTime = DateTimeOffset.FromUnixTimeSeconds(history.boarding_datetime);
            TransferEndDateTime = DateTimeOffset.FromUnixTimeSeconds(history.transfer_end_datetime);
            TicketFareCents = history.ticket_fare_cents;
            GroupSize = history.group_size;
            RemainingValue = history.remaining_value;
        }
    }
}
