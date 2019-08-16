using System;
using System.Runtime.InteropServices;

namespace ScannitSharp.Bindings
{
    internal class FFITravelCardHandle : SafeHandle
    {
        private TravelCard _cSharpTravelCard;

        internal FFITravelCardHandle(IntPtr ptr) : base(IntPtr.Zero, true)
        {
            this.handle = ptr;
        }

        internal FFITravelCardHandle() : base(IntPtr.Zero, true) { }

        public override bool IsInvalid => false;

        protected override bool ReleaseHandle()
        {
            Native.free_travel_card(handle);
            return true;
        }

        internal TravelCard AsTravelCard()
        {
            if (_cSharpTravelCard == null)
            {
                _cSharpTravelCard = ReadStructData();
            }

            return _cSharpTravelCard;
        }

        private TravelCard ReadStructData()
        {
            FFITravelCard travelCard = Marshal.PtrToStructure<FFITravelCard>(handle);
            return new TravelCard
            {
                ActionListCounter = travelCard.action_list_counter,
                ApplicationInstanceId = new RustString(travelCard.application_instance_id).ToString(),
                ApplicationIssuingDate = DateTimeOffset.FromUnixTimeSeconds(travelCard.application_issuing_date),
                ApplicationKeyVersion = travelCard.application_key_version,
                ApplicationStatus = travelCard.application_status,
                ApplicationTransactionCounter = travelCard.application_transaction_counter,
                ApplicationUnblockingNumber = travelCard.application_unblocking_number,
                ApplicationVersion = travelCard.application_version,
                //ETicket = ,
                //History = ,
                IsMacProtected = travelCard.is_mac_protected,
                LastLoadDateTime = DateTimeOffset.FromUnixTimeSeconds(travelCard.last_load_datetime),
                LastLoadDeviceNum = travelCard.last_load_device_num,
                LastLoadOrganization = travelCard.last_load_organization_id,
                LastLoadValue = travelCard.last_load_value,
                //PeriodPass = ,
                PlatformType = travelCard.platform_type,
                StoredValueCents = travelCard.stored_value_cents,
            };
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct FFITravelCard
    {
        internal byte application_version;
        internal byte application_key_version;
        internal IntPtr application_instance_id;
        internal byte platform_type;
        internal bool is_mac_protected;
        internal long application_issuing_date;
        internal bool application_status;
        internal byte application_unblocking_number;
        internal uint application_transaction_counter;
        internal uint action_list_counter;

        internal FFIPeriodPass period_pass;

        internal uint stored_value_cents;
        internal long last_load_datetime;
        internal uint last_load_value;
        internal ushort last_load_organization_id;
        internal ushort last_load_device_num;

        internal FFIETicket e_ticket;

        internal RustBuffer history;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct FFIPeriodPass
    {
        internal ProductCodeKind product_code_1_kind;
        internal ushort product_code_1_value;
        internal ValidityAreaKind validity_area_1_kind;
        internal RustBuffer validity_area_1_buffer; // of u8s
        internal long period_start_date_1;

        internal ProductCodeKind product_code_2_kind;
        internal ushort product_code_2_value;
        internal ValidityAreaKind validity_area_2_kind;
        internal RustBuffer validity_area_2_buffer; // of u8s
        internal long period_start_date_2;

        internal ProductCodeKind loaded_period_product_kind;
        internal ushort loaded_period_product_value;
        internal long loaded_period_datetime;
        internal ushort loaded_period_length;
        internal uint loaded_period_price;
        internal ushort loading_organization;
        internal ushort loading_device_number;

        internal long last_board_datetime;
        internal ushort last_board_vehicle_number;
        internal BoardingLocationKind last_board_location_kind;
        internal ushort last_board_location_value;
        internal BoardingDirection last_board_direction;
        internal ValidityAreaKind last_board_area_kind;
        internal RustBuffer last_board_area_value;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct FFIETicket
    {
        public ProductCodeKind product_code_kind;
        public ushort product_code_value;
        public byte customer_profile;
        public Language language;
        public ValidityLengthKind validity_length_kind;
        public byte validity_length_value;
        public ValidityAreaKind validity_area_kind;
        public RustBuffer validity_area_value;
        public long sale_datetime;
        public SaleDeviceKind sale_device_kind;
        public ushort sale_device_value;
        public ushort ticket_fare_cents;
        public byte group_size;

        internal bool extra_zone;
        internal ValidityAreaKind period_pass_validity_area_kind;
        internal RustBuffer period_pass_validity_area_value;
        internal ProductCodeKind extension_product_code_kind;
        internal ushort extension_product_code_value;
        internal ValidityAreaKind extension_1_validity_area_kind;
        internal RustBuffer extension_1_validity_area_value;
        internal ushort extension_1_fare_cents;
        internal ValidityAreaKind extension_2_validity_area_kind;
        internal RustBuffer extension_2_validity_area_value;
        internal ushort extension_2_fare_cents;
        internal bool sale_status;

        internal long validity_start_datetime;
        internal long validity_end_datetime;
        internal bool validity_status;

        internal long bording_datetime;
        internal ushort boarding_vehicle;
        internal BoardingLocationKind boarding_location_kind;
        internal ushort boarding_location_value;
        internal BoardingDirection boarding_direction;
        internal BoardingAreaKind boarding_area_kind;
        internal byte boarding_area_value;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct FFIHistory
    {
        internal TransactionType transaction_type;
        internal long boarding_datetime;
        internal long transfer_end_datetime;
        internal ushort ticket_fare_cents;
        internal byte group_size;
        internal uint remaining_value;
    }

    internal enum ProductCodeKind : uint
    {
        FaresFor2010 = 0,
        FaresFor2014 = 1,
    }

    internal enum ValidityAreaKind : uint
    {
        OldZone = 0,
        VehicleType = 1,
        NewZone = 2,
    }

    internal enum BoardingLocationKind : uint
    {
        NoneOrReserved = 0,
        BusNumber = 1,
        TrainNumber = 2,
        PlatformNumber = 3,
    }

    internal enum ValidityLengthKind : uint
    {
        Minutes = 0,
        Hours = 1,
        TwentyFourHourPeriods = 2,
        Days = 3,
    }

    internal enum SaleDeviceKind : uint
    {
        ServicePointSalesDevice = 0,
        DriverTicketMachine = 1,
        CardReader = 2,
        TicketMachine = 3,
        Server = 4,
        HSLSmallEquipment = 5,
        ExternalServiceEquipment = 6,
        Reserved = 7,
    }

    internal enum BoardingAreaKind : uint
    {
        Zone = 0,
        Vehicle = 1,
        ZoneCircle = 2,
    }
}
