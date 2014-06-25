﻿using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.Weather.Data;
using ThinkingHome.Plugins.WebUI.Attributes;

namespace ThinkingHome.Plugins.Weather
{
	[AppSection("Weather", SectionType.Common, "/webapp/weather/forecast.js", "ThinkingHome.Plugins.Weather.Resources.weather-forecast.js")]
	[JavaScriptResource("/webapp/weather/forecast-model.js", "ThinkingHome.Plugins.Weather.Resources.weather-forecast-model.js")]
	[JavaScriptResource("/webapp/weather/forecast-view.js", "ThinkingHome.Plugins.Weather.Resources.weather-forecast-view.js")]
	[HttpResource("/webapp/weather/forecast.tpl", "ThinkingHome.Plugins.Weather.Resources.weather-forecast.tpl")]

	// css
	[CssResource("/webapp/weather/css/weather-icons.min.css", "ThinkingHome.Plugins.Weather.Resources.css.weather-icons.min.css", AutoLoad = true)]

	// fonts
	[HttpResource("/webapp/weather/fonts/weathericons-regular-webfont.eot", "ThinkingHome.Plugins.Weather.Resources.fonts.weathericons-regular-webfont.eot", "application/vnd.ms-fontobject")]
	[HttpResource("/webapp/weather/fonts/weathericons-regular-webfont.svg", "ThinkingHome.Plugins.Weather.Resources.fonts.weathericons-regular-webfont.svg", "image/svg+xml")]
	[HttpResource("/webapp/weather/fonts/weathericons-regular-webfont.ttf", "ThinkingHome.Plugins.Weather.Resources.fonts.weathericons-regular-webfont.ttf", "application/x-font-truetype")]
	[HttpResource("/webapp/weather/fonts/weathericons-regular-webfont.woff", "ThinkingHome.Plugins.Weather.Resources.fonts.weathericons-regular-webfont.woff", "application/font-woff")]


	[Plugin]
	public class WeatherUIPlugin : Plugin
	{
		[HttpCommand("/api/weather/all")]
		public object GetAlarmList(HttpRequestParams request)
		{
			using (var session = Context.OpenSession())
			{
				var date = DateTime.Now.AddHours(-3);
				
				var locations = session.Query<Location>().ToList();
				var data = session.Query<WeatherData>().Where(d => d.Date > date).ToList();

				var model = new List<object>();

				foreach (var location in locations)
				{
					var dayly = data
						.Where(d => d.Location.Id == location.Id)
						.OrderBy(d => d.Date)
						.Take(8)
						.Select(d => new
						{
							time = d.Date.ToShortTimeString(),
							temperature = d.Temperature,
							pressure = d.Pressure,
							humidity = d.Humidity,
							weatherCode = d.WeatherCode,
							weatherDescription = d.WeatherDescription
						})
						.ToArray();

					var locationModel = new
					{
						locationName = location.DisplayName,
						daylyForecast = dayly
					};

					model.Add(locationModel);
				}

				return model;
			}
		}

	}
}
