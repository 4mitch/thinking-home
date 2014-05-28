﻿using System;

namespace ThinkingHome.Plugins.AlarmClock.Data
{
	public class AlarmTime
	{
		public virtual Guid Id { get; set; }

		public virtual string Name { get; set; }

		public virtual int Hours { get; set; }
		
		public virtual int Minutes { get; set; }
		
		public virtual bool Enabled { get; set; }
	}
}
