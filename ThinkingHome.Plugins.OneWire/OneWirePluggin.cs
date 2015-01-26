﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.OneWire.Attributes;

namespace ThinkingHome.Plugins.OneWire
{
	[Plugin]
	public class OneWirePluggin : PluginBase
	{
		private OneWireAdapter adapter;
        private Type[] oneWireSensorTypes;

		public override void InitPlugin()
		{
			//Debugger.Launch();
			base.InitPlugin();

            adapter = new OneWireAdapter(OneWireAdapterConfiguration.Default);

            oneWireSensorTypes = GetSensorsTypes();
		}

		public override void StartPlugin()
		{
			Logger.Info("Start Plugin OneWire");
			base.StartPlugin();
		}

		public override void StopPlugin()
		{
			base.StopPlugin();

			adapter.Dispose();
			adapter = null;
		}

		public OneWireDevice[] GetDevices()
		{
            var listAddress = adapter.FindAddress();

            List<OneWireDevice> list = new List<OneWireDevice>();

            foreach (var addr in listAddress)
            {
                var device = default(OneWireDevice);

                foreach (var sensorType in oneWireSensorTypes)
                {
                    var attr = sensorType.GetCustomAttribute<SensorTypeAttribute>(true);

                    if (attr != null && attr.Match(addr[0]))
                    {
                        device = Activator.CreateInstance(sensorType, new object[] { addr, adapter }) as OneWireDevice;

                        list.Add(device);
                    }
                }
            
                //if unknown sensor then create OneWireDevice
                if (device == null)
                {
                    list.Add(new OneWireDevice(addr, adapter));
                }
            }

            
            return list.ToArray();
		}

        private Type[] GetSensorsTypes()
        {
            List<Type> listTypes = new List<Type>();

            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var t in a.GetTypes())
                {
                    if (t.IsSubclassOf(typeof(OneWireDevice)))
                    {
                        listTypes.Add(t);
                    }
                }
            }
            
            return listTypes.ToArray();
        }
	}
}
