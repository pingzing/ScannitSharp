using ScannitSharp.Bindings.Models;
using System;
using System.Runtime.InteropServices;

namespace ScannitSharp.Bindings
{
    public static class Native
    {
        [DllImport("native/scannit_core_ffi")]
        internal static extern IntPtr create_travel_card(
            IntPtr app_info_ptr, UIntPtr app_info_size,
            IntPtr control_info_ptr, UIntPtr control_info_size,
            IntPtr period_pass_ptr, UIntPtr period_pass_size,
            IntPtr stored_value_ptr, UIntPtr stored_value_size,
            IntPtr e_ticket_ptr, UIntPtr e_ticket_size,
            IntPtr history_ptr, UIntPtr history_size);

        [DllImport("native/scannit_core_ffi")]
        internal static extern void free_travel_card(IntPtr travel_card_ptr);

        public static TravelCard GetTravelCard(
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
    }
}

