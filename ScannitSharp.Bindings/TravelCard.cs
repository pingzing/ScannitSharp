using OneOf;
using ScannitSharp.Bindings.Models;
using ScannitSharp.Bindings.Models.BoardingAreas;
using ScannitSharp.Bindings.Models.BoardingLocations;
using ScannitSharp.Bindings.Models.ProductCodes;
using ScannitSharp.Bindings.Models.SaleDevices;
using ScannitSharp.Bindings.Models.ValidityAreas;
using ScannitSharp.Bindings.Models.ValidityLengths;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace ScannitSharp.Bindings
{
    public class TravelCard
    {
        public byte ApplicationVersion { get; set; }
        public byte ApplicationKeyVersion { get; set; }
        public string ApplicationInstanceId { get; set; }
        public byte PlatformType { get; set; }
        public bool IsMacProtected { get; set; }

        public DateTimeOffset ApplicationIssuingDate { get; set; }
        public bool ApplicationStatus { get; set; }
        public byte ApplicationUnblockingNumber { get; set; }
        public uint ApplicationTransactionCounter { get; set; }
        public uint ActionListCounter { get; set; }

        public PeriodPass PeriodPass { get; set; }

        public uint StoredValueCents { get; set; }
        public DateTimeOffset LastLoadDateTime { get; set; }
        public uint LastLoadValue { get; set; }
        public ushort LastLoadOrganization { get; set; }
        public ushort LastLoadDeviceNum { get; set; }

        public ETicket ETicket { get; set; }

        public History[] History { get; set; }

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

    public class PeriodPass
    {
        public OneOf<FaresFor2010, FaresFor2014> ProductCode1 { get; set; }
        public OneOf<OldZone, NewZone, Models.ValidityAreas.Vehicle> ValidityArea1 { get; set; }
        public DateTimeOffset PeriodStartDate1 { get; set; }
        public DateTimeOffset PeriodEndDate1 { get; set; }

        public OneOf<FaresFor2010, FaresFor2014> ProductCode2 { get; set; }
        public OneOf<OldZone, NewZone, Models.ValidityAreas.Vehicle> ValidityArea2 { get; set; }
        public DateTimeOffset PeriodStartDate2 { get; set; }
        public DateTimeOffset PeriodEndDate2 { get; set; }

        public OneOf<FaresFor2010, FaresFor2014> LoadedPeriodProduct { get; set; }
        public DateTimeOffset LoadedPeriodDateTime { get; set; }
        public ushort LoadedPeriodLength { get; set; }
        /// <summary>
        /// In cents.
        /// </summary>
        public uint LoadedPeriodPrice { get; set; }
        public ushort LoadingOrganizationId { get; set; }
        public ushort LoadingDeviceNumber { get; set; }

        public DateTimeOffset LastBoardDateTime { get; set; }
        public ushort LastBoardVehicleNumber { get; set; }
        public OneOf<NoneOrReserved, BusNumber, TrainNumber, PlatformNumber> LastBoardLocation { get; set; }
        public BoardingDirection LastBoardDirection { get; set; }
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

    public class ETicket
    {
        public OneOf<FaresFor2010, FaresFor2014> ProductCode { get; set; }
        public byte CustomerProfile { get; set; }
        public Language Language { get; set; }
        public OneOf<Minutes, Hours, TwentyFourHourPeriods, Days> ValidityLength { get; set; }
        public OneOf<OldZone, NewZone, Models.ValidityAreas.Vehicle> ValidityArea { get; set; }
        public DateTimeOffset SaleDateTime { get; set; }
        public OneOf<ServicePointSalesDevice, DriverTicketMachine, CardReader, TicketMachine, Server, HSLSmallEquipment, ExternalServiceEquipment, Reserved> SaleDevice { get; set; }
        public ushort TicketFareCents { get; set; }
        public byte GroupSize { get; set; }

        public bool ExtraZone { get; set; }
        public OneOf<OldZone, NewZone, Models.ValidityAreas.Vehicle> PeriodPassValidityArea { get; set; }
        public OneOf<FaresFor2010, FaresFor2014> ExtensionProductCode { get; set; }
        public OneOf<OldZone, NewZone, Models.ValidityAreas.Vehicle> ExtensionValidityArea1 { get; set; }
        public ushort ExtensionFareCents1 { get; set; }
        public OneOf<OldZone, NewZone, Models.ValidityAreas.Vehicle> ExtensionValidityArea2 { get; set; }
        public ushort ExtensionFareCents2 { get; set; }
        public bool SaleStatus { get; set; }

        public DateTimeOffset ValidityStartDateTime { get; set; }
        public DateTimeOffset ValidityEndDateTime { get; set; }
        public bool ValidityStatus { get; set; }

        public DateTimeOffset BoardingDateTime { get; set; }
        public ushort BoardingVehicle { get; set; }
        public OneOf<NoneOrReserved, BusNumber, TrainNumber, PlatformNumber> BoardingLocation { get; set; }
        public BoardingDirection BoardingDirection { get; set; }
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

    public class History
    {
        public TransactionType TransactionType { get; set; }
        public DateTimeOffset BoardingDateTime { get; set; }
        public DateTimeOffset TransferEndDateTime { get; set; }
        public ushort TicketFareCents { get; set; }
        public byte GroupSize { get; set; }
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
