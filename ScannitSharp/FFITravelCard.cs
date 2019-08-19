using ScannitSharp.Models;
using System;
using System.Runtime.InteropServices;

namespace ScannitSharp
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
            return new TravelCard(travelCard);
        }
    }

    // Note: Rust bools are 1-byte members. C# bools in a Sequential struct seem to align to 4-byte boundaries, though.
    // Probably for legacy reasons--I think Win32 BOOLs are 32-bit values.
    // For that reason, we use 'byte' for boolean fields, and convert in the AsTravelCard method.
    [StructLayout(LayoutKind.Sequential)]
    internal struct FFITravelCard
    {
        internal byte application_version;
        internal byte application_key_version;
        internal IntPtr application_instance_id;
        internal byte platform_type;
        internal byte is_mac_protected;
        internal long application_issuing_date;
        internal byte application_status;
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
        internal long period_end_date_1;

        internal ProductCodeKind product_code_2_kind;
        internal ushort product_code_2_value;
        internal ValidityAreaKind validity_area_2_kind;
        internal RustBuffer validity_area_2_buffer; // of u8s
        internal long period_start_date_2;
        internal long period_end_date_2;

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
        internal BoardingAreaKind last_board_area_kind;
        internal byte last_board_area_value;
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

        internal byte extra_zone;
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
        internal byte sale_status;

        internal long validity_start_datetime;
        internal long validity_end_datetime;
        internal byte validity_status;

        internal long boarding_datetime;
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
}
