﻿using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using ThinkingHome.Core.Plugins.Utils;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.UniUI.Model;
using ThinkingHome.Plugins.UniUI.Widgets;
using ThinkingHome.Plugins.WebUI.Attributes;

namespace ThinkingHome.Plugins.UniUI
{
	[JavaScriptResource("/webapp/uniui/ui/dashboard.js", "ThinkingHome.Plugins.UniUI.Resources.UI.dashboard.js")]
	[JavaScriptResource("/webapp/uniui/ui/dashboard-view.js", "ThinkingHome.Plugins.UniUI.Resources.UI.dashboard-view.js")]
	[JavaScriptResource("/webapp/uniui/ui/dashboard-model.js", "ThinkingHome.Plugins.UniUI.Resources.UI.dashboard-model.js")]
	[HttpResource("/webapp/uniui/ui/dashboard.tpl", "ThinkingHome.Plugins.UniUI.Resources.UI.dashboard.tpl")]

	public partial class UniUiPlugin
	{
		[HttpCommand("/api/uniui/dashboard/details")]
		public object GetDdashboardDetails(HttpRequestParams request)
		{
			Guid dashboardId = request.GetRequiredGuid("id");

			using (var session = Context.OpenSession())
			{
				var allWidgets = session.Query<Widget>()
						.Where(w => w.Dashboard.Id == dashboardId)
						.ToArray();

				var allParameters = session.Query<WidgetParameter>()
						.Where(w => w.Widget.Dashboard.Id == dashboardId)
						.ToArray();

				var list = GetWidgetListModel(allWidgets, allParameters, session);

				return list;
			}
		}

		private object[] GetWidgetListModel(Widget[] allWidgets, WidgetParameter[] allParameters, ISession session)
		{
			var list = new List<object>();

			foreach (var widget in allWidgets)
			{
				var def = defs.GetValueOrDefault(widget.TypeAlias);

				if (def != null)
				{
					var parameters = allParameters.Where(p => p.Widget.Id == widget.Id).ToArray();

					var model = GetWidgetModel(def, widget, parameters, session);

					if (model != null)
					{
						list.Add(model);
					}
				}
			}
			return list.ToArray(); ;
		}

		private object GetWidgetModel(IWidgetDefinition def, Widget widget, WidgetParameter[] parameters, ISession session)
		{
			try
			{
				var data = def.GetWidgetData(widget, parameters, session, Logger);

				var model = new
				{
					id = widget.Id,
					type = widget.TypeAlias,
					displayName = widget.DisplayName,
					sortOrder = widget.SortOrder,
					data
				};

				return model;
			}
			catch (Exception ex)
			{
				var message = string.Format(
					"UI widget error (id: {0}, type:{1})", widget.Id, widget.TypeAlias);
				Logger.ErrorException(message, ex);

				return null;
			}
		}
	}
}
