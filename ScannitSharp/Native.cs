using System;
using System.Runtime.InteropServices;

namespace ScannitSharp
{
    internal static class Native
    {
        [DllImport("scannit_core_ffi")]
        internal static extern IntPtr create_travel_card(
            IntPtr app_info_ptr, UIntPtr app_info_size,
            IntPtr control_info_ptr, UIntPtr control_info_size,
            IntPtr period_pass_ptr, UIntPtr period_pass_size,
            IntPtr stored_value_ptr, UIntPtr stored_value_size,
            IntPtr e_ticket_ptr, UIntPtr e_ticket_size,
            IntPtr history_ptr, UIntPtr history_size);

        [DllImport("scannit_core_ffi")]
        internal static extern void free_travel_card(IntPtr travel_card_ptr);

        [DllImport("scannit_core_ffi")]
        internal static extern IntPtr get_GET_VERSION_COMMAND();

        [DllImport("scannit_core_ffi")]
        internal static extern IntPtr get_GET_APPLICATION_IDS_COMMAND();

        [DllImport("scannit_core_ffi")]
        internal static extern IntPtr get_SELECT_HSL_COMMAND();

        [DllImport("scannit_core_ffi")]
        internal static extern IntPtr get_READ_APP_INFO_COMMAND();

        [DllImport("scannit_core_ffi")]
        internal static extern IntPtr get_READ_CONTROL_INFO_COMMAND();

        [DllImport("scannit_core_ffi")]
        internal static extern IntPtr get_READ_PERIOD_PASS_COMMAND();

        [DllImport("scannit_core_ffi")]
        internal static extern IntPtr get_READ_STORED_VALUE_COMMAND();

        [DllImport("scannit_core_ffi")]
        internal static extern IntPtr get_READ_E_TICKET_COMMAND();

        [DllImport("scannit_core_ffi")]
        internal static extern IntPtr get_READ_HISTORY_COMMAND();

        [DllImport("scannit_core_ffi")]
        internal static extern IntPtr get_READ_NEXT_COMMAND();

        [DllImport("scannit_core_ffi")]
        internal static extern IntPtr get_OK_RESPONSE();

        [DllImport("scannit_core_ffi")]
        internal static extern IntPtr get_ERROR_RESPONSE();

        [DllImport("scannit_core_ffi")]
        internal static extern IntPtr get_MORE_DATA_RESPONSE();
    }
}

