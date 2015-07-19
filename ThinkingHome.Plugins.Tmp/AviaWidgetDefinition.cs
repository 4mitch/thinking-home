﻿using NHibernate;
using NLog;
using ThinkingHome.Plugins.UniUI.Model;
using ThinkingHome.Plugins.UniUI.Widgets;

namespace ThinkingHome.Plugins.Tmp
{
	[Widget("avia-form")]
	public class AviaWidgetDefinition : IWidgetDefinition
	{
		public string DisplayName
		{
			get { return "Airplane tickets"; }
		}
		public WidgetParameterMetaData[] GetWidgetMetaData(ISession session, Logger logger)
		{
			return new[]
			{
				new WidgetParameterMetaData
				{
					DisplayName = "From", Name = "from", Type = WidgetParameterType.String,
					Items = new[]
					{
						new WidgetSelectItem("MOW", "Москва"), 
						new WidgetSelectItem("LED", "Санкт-Петербург"), 
						new WidgetSelectItem("CHK", "Челябинск") 
					}
				}, 
				new WidgetParameterMetaData
				{
					DisplayName = "To", Name = "to", Type = WidgetParameterType.String,
					Items = new[]
					{
						new WidgetSelectItem("MOW", "Москва"), 
						new WidgetSelectItem("LED", "Санкт-Петербург"), 
						new WidgetSelectItem("CHK", "Челябинск") 
					}
				},
 
				new WidgetParameterMetaData
				{
					DisplayName = "Count", Name = "count", Type = WidgetParameterType.Int32
				},

				new WidgetParameterMetaData
				{
					DisplayName = "Notes", Name = "notes", Type = WidgetParameterType.String
				},

				new WidgetParameterMetaData
				{
					DisplayName = "Display name", Name = "displayName", Type = WidgetParameterType.String
				}
			};
		}

		public object GetWidgetData(Widget widget, WidgetParameter[] parameters, ISession session, Logger logger)
		{
			return null;
		}
	}

	[Widget("empty-widget")]
	public class EmptyWidget : IWidgetDefinition
	{
		public string DisplayName
		{
			get { return "Empty widget (with no fields)"; }
		}

		public object GetWidgetData(Widget widget, WidgetParameter[] parameters, ISession session, Logger logger)
		{
			return null;
		}

		public WidgetParameterMetaData[] GetWidgetMetaData(ISession session, Logger logger)
		{
			return null;
		}
	}

	[Widget("noolite-on-off")]
	public class NooliteSwitcherWidget : IWidgetDefinition
	{
		public string DisplayName
		{
			get { return "nooLite (ON/OFF)"; }
		}

		public object GetWidgetData(Widget widget, WidgetParameter[] parameters, ISession session, Logger logger)
		{
			return null;
		}

		public WidgetParameterMetaData[] GetWidgetMetaData(ISession session, Logger logger)
		{
			var fldChannel = new WidgetParameterMetaData
			{
				DisplayName = "Channel",
				Name = "channel",
				Type = WidgetParameterType.Int32,
				Items = new WidgetSelectItem[32]
			};

			for (var i = 0; i < 32; i++)
			{
				fldChannel.Items[i] = new WidgetSelectItem(i, i.ToString());
			}

			return new[] { fldChannel };
		}
	}
}
