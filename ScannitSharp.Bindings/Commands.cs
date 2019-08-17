namespace ScannitSharp.Bindings
{
    public static class Commands
    {
        private static byte[] _versionCommand;

        public static byte[] VersionCommand

        {
            get
            {
                if (_versionCommand == null)

                {
                    _versionCommand = Native.get_GET_VERSION_COMMAND();

                }
                return _versionCommand;

            }
        }

        private static byte[] _applicationIdsCommand;

        public static byte[] ApplicationIdsCommand

        {
            get
            {
                if (_applicationIdsCommand == null)

                {
                    _applicationIdsCommand = Native.get_GET_APPLICATION_IDS_COMMAND();

                }
                return _applicationIdsCommand;

            }
        }

        private static byte[] _selectHslCommand;

        public static byte[] SelectHslCommand

        {
            get
            {
                if (_selectHslCommand == null)

                {
                    _selectHslCommand = Native.get_SELECT_HSL_COMMAND();

                }
                return _selectHslCommand;

            }
        }

        private static byte[] _readAppInfoCommand;

        public static byte[] ReadAppInfoCommand

        {
            get
            {
                if (_readAppInfoCommand == null)

                {
                    _readAppInfoCommand = Native.get_READ_APP_INFO_COMMAND();

                }
                return _readAppInfoCommand;

            }
        }

        private static byte[] _readControlInfoCommand;

        public static byte[] ReadControlInfoCommand

        {
            get
            {
                if (_readControlInfoCommand == null)

                {
                    _readControlInfoCommand = Native.get_READ_CONTROL_INFO_COMMAND();

                }
                return _readControlInfoCommand;

            }
        }

        private static byte[] _readPeriodPassCommand;

        public static byte[] ReadPeriodPassCommand

        {
            get
            {
                if (_readPeriodPassCommand == null)

                {
                    _readPeriodPassCommand = Native.get_READ_PERIOD_PASS_COMMAND();

                }
                return _readPeriodPassCommand;

            }
        }

        private static byte[] _readStoredValueCommand;
        public static byte[] ReadStoredValueCommand
        {
            get
            {
                if (_readStoredValueCommand == null)
                {
                    _readStoredValueCommand = Native.get_READ_STORED_VALUE_COMMAND();
                }
                return _readStoredValueCommand;
            }
        }

        private static byte[] _readETicketCommand;
        public static byte[] ReadETicketCommand
        {
            get
            {
                if (_readETicketCommand == null)
                {
                    _readETicketCommand = Native.get_READ_E_TICKET_COMMAND();
                }
                return _readETicketCommand;
            }
        }

        private static byte[] _readHistoryCommand;
        public static byte[] ReadHistoryCommand
        {
            get
            {
                if (_readHistoryCommand == null)
                {
                    _readHistoryCommand = Native.get_READ_HISTORY_COMMAND();
                }
                return _readHistoryCommand;
            }
        }

        private static byte[] _readNextCommand;
        public static byte[] ReadNextCommand
        {
            get
            {
                if (_readNextCommand == null)
                {
                    _readNextCommand = Native.get_READ_NEXT_COMMAND();
                }
                return _readNextCommand;
            }
        }

        private static byte[] _okResponse;
        public static byte[] OkResponse
        {
            get
            {
                if (_okResponse == null)
                {
                    _okResponse = Native.get_OK_RESPONSE();
                }
                return _okResponse;
            }
        }

        private static byte[] _errorResponse;
        public static byte[] ErrorResponse
        {
            get
            {
                if (_errorResponse == null)
                {
                    _errorResponse = Native.get_ERROR_RESPONSE();
                }
                return _errorResponse;
            }
        }

        private static byte[] _moreDataResponse;
        public static byte[] MoreDataResponse
        {
            get
            {
                if (_moreDataResponse == null)
                {
                    _moreDataResponse = Native.get_MORE_DATA_RESPONSE();
                }
                return _moreDataResponse;
            }
        }

    }
}
