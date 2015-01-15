namespace ThinkingHome.Plugins.OneWire
{
	public class OneWireDevice
	{
		public byte[] Address { get; private set; }

		public OneWireAdapter Adapter { get; private set; }

		public virtual byte DeviceType
		{
			get { return 0; }
		}

		public virtual string DeviceName
		{
			get { return "Unknown 1-wire device"; }
		}

        public OneWireDevice(byte[] address, OneWireAdapter adapter)
        {
            Address = address;
            Adapter = adapter;
        }
	}
}