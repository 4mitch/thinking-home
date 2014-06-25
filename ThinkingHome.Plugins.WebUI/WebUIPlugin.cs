﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Core.Plugins.Utils;
using ThinkingHome.Plugins.Listener;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.Listener.Handlers;
using ThinkingHome.Plugins.WebUI.Attributes;
using ThinkingHome.Plugins.WebUI.Model;

namespace ThinkingHome.Plugins.WebUI
{
	#region resources

	// VENDOR

	// js
	[JavaScriptResource("/vendor/js/require.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.require.min.js")]
	[JavaScriptResource("/vendor/js/json2.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.json2.min.js")]
	[JavaScriptResource("/vendor/js/jquery.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.jquery.min.js")]
	[JavaScriptResource("/vendor/js/underscore.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.underscore.min.js")]
	[JavaScriptResource("/vendor/js/backbone.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.backbone.min.js")]
	[JavaScriptResource("/vendor/js/backbone.marionette.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.backbone.marionette.min.js")]
	[JavaScriptResource("/vendor/js/backbone.syphon.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.backbone.syphon.js")]
	[JavaScriptResource("/vendor/js/bootstrap.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.bootstrap.min.js")]
	[JavaScriptResource("/vendor/js/tpl.min.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.tpl.min.js")]

	[JavaScriptResource("/vendor/js/codemirror-all.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.codemirror-all.js")]
	[JavaScriptResource("/vendor/js/codemirror.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.codemirror.js")]
	[JavaScriptResource("/vendor/js/codemirror-javascript.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.codemirror-javascript.js")]
	[JavaScriptResource("/vendor/js/codemirror-closebrackets.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.codemirror-closebrackets.js")]
	[JavaScriptResource("/vendor/js/codemirror-matchbrackets.js", "ThinkingHome.Plugins.WebUI.Resources.Vendor.js.codemirror-matchbrackets.js")]


	// css
	[CssResource("/vendor/css/bootstrap.min.css", "ThinkingHome.Plugins.WebUI.Resources.Vendor.css.bootstrap.min.css")]
	[CssResource("/vendor/css/font-awesome.min.css", "ThinkingHome.Plugins.WebUI.Resources.Vendor.css.font-awesome.min.css")]
	[CssResource("/vendor/css/site.css", "ThinkingHome.Plugins.WebUI.Resources.Vendor.css.site.css")]

	// fonts
	[HttpResource("/vendor/fonts/glyphicons-halflings-regular.eot", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.glyphicons-halflings-regular.eot", "application/vnd.ms-fontobject")]
	[HttpResource("/vendor/fonts/glyphicons-halflings-regular.svg", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.glyphicons-halflings-regular.svg", "image/svg+xml")]
	[HttpResource("/vendor/fonts/glyphicons-halflings-regular.ttf", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.glyphicons-halflings-regular.ttf", "application/x-font-truetype")]
	[HttpResource("/vendor/fonts/glyphicons-halflings-regular.woff", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.glyphicons-halflings-regular.woff", "application/font-woff")]
	
	[HttpResource("/vendor/fonts/fontawesome-webfont.eot", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.fontawesome-webfont.eot", "application/vnd.ms-fontobject")]
	[HttpResource("/vendor/fonts/fontawesome-webfont.svg", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.fontawesome-webfont.svg", "image/svg+xml")]
	[HttpResource("/vendor/fonts/fontawesome-webfont.ttf", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.fontawesome-webfont.ttf", "application/x-font-truetype")]
	[HttpResource("/vendor/fonts/fontawesome-webfont.woff", "ThinkingHome.Plugins.WebUI.Resources.Vendor.fonts.fontawesome-webfont.woff", "application/font-woff")]

	//APPLICATION

	// html
	[HttpResource("/", "ThinkingHome.Plugins.WebUI.Resources.Application.index.html", "text/html")]

	// webapp: main
	[JavaScriptResource("/application/index.js", "ThinkingHome.Plugins.WebUI.Resources.Application.index.js")]
	[JavaScriptResource("/application/app.js", "ThinkingHome.Plugins.WebUI.Resources.Application.app.js")]

	// webapp: common
	[JavaScriptResource("/application/common/common.js", "ThinkingHome.Plugins.WebUI.Resources.Application.Common.common.js")]

	// webapp: menu settings
	[JavaScriptResource("/webapp/webui/section-list-common.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.section-list-common.js")]
	[JavaScriptResource("/webapp/webui/section-list-system.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.section-list-system.js")]
	[JavaScriptResource("/webapp/webui/section-model.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.section-model.js")]
	[JavaScriptResource("/webapp/webui/section-view.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.section-view.js")]
	[HttpResource("/webapp/webui/section-list.tpl", "ThinkingHome.Plugins.WebUI.Resources.Plugin.section-list.tpl")]
	[HttpResource("/webapp/webui/section-list-item.tpl", "ThinkingHome.Plugins.WebUI.Resources.Plugin.section-list-item.tpl")]

	[JavaScriptResource("/webapp/webui/tiles-editor.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tiles-editor.js")]
	[JavaScriptResource("/webapp/webui/tiles-editor-view.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tiles-editor-view.js")]
	[JavaScriptResource("/webapp/webui/tiles-editor-model.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tiles-editor-model.js")]
	[HttpResource("/webapp/webui/tiles-editor-layout.tpl", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tiles-editor-layout.tpl")]
	[HttpResource("/webapp/webui/tiles-editor-form.tpl", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tiles-editor-form.tpl")]
	[HttpResource("/webapp/webui/tiles-editor-list-item.tpl", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tiles-editor-list-item.tpl")]
	
	[JavaScriptResource("/webapp/webui/tiles.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tiles.js")]
	[JavaScriptResource("/webapp/webui/tiles-model.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tiles-model.js")]
	[JavaScriptResource("/webapp/webui/tiles-view.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tiles-view.js")]
	[HttpResource("/webapp/webui/tile.tpl", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tile.tpl")]

	#endregion

	[Plugin]
	public class WebUIPlugin : Plugin
	{
		private readonly List<AppSectionAttribute> sections = new List<AppSectionAttribute>();
		private readonly HashSet<string> cssFiles = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
		
		public override void Init()
		{
			base.Init();

			foreach (var plugin in Context.GetAllPlugins())
			{
				var type = plugin.GetType();

				// разделы
				var sectionAttributes = type.GetCustomAttributes<AppSectionAttribute>();
				sections.AddRange(sectionAttributes);

				// стили
				var cssResourceAttributes = type
					.GetCustomAttributes<CssResourceAttribute>()
					.Where(attr => attr.AutoLoad)
					.ToArray();

				var urls = cssResourceAttributes.Select(attr => attr.Url).ToArray();
				cssFiles.UnionWith(urls);
			}
		}

		[HttpCommand("/api/webui/sections/common")]
		public object GetSections(HttpRequestParams request)
		{
			return GetSections(SectionType.Common);
		}

		[HttpCommand("/api/webui/sections/system")]
		public object GetSystemSections(HttpRequestParams request)
		{
			return GetSections(SectionType.System);
		}

		private object GetSections(SectionType sectionType)
		{
			var list = sections
				.Where(section => section.Type == sectionType)
				.Select(x => new { id = Guid.NewGuid(), name = x.Title, path = x.GetModulePath(), sortOrder = x.SortOrder })
				.ToArray();

			return list;
		}

		[HttpDynamicFile("/application/style-loader.js", "text/javascript")]
		public byte[] LoadStylesBundle(HttpRequestParams request)
		{
			const string cssLoaderFormat = "define(['common'], function (common) {{ common.LoadCss({0}); return null; }});";

			string argumentsJson = cssFiles.ToJson("[]");
			string content = string.Format(cssLoaderFormat, argumentsJson);
			return Encoding.UTF8.GetBytes(content);
		}
	}
}
