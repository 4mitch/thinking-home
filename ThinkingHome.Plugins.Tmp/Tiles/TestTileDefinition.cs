﻿using System;
using System.Threading.Tasks;
using ThinkingHome.Plugins.WebUI.Model;
using ThinkingHome.Plugins.WebUI.Tiles;

namespace ThinkingHome.Plugins.Tmp.Tiles
{
	[Tile]
	public class TestTileDefinition : TileDefinition
	{
		public override void FillModel(TileModel model, dynamic options)
		{
			model.title = "Test" + options.xxx;
			model.url = "webapp/alarm-clock/list";
			model.parameters = new object[] { Guid.Empty, "wegfwegkwelglweg" };
			model.className = "btn-danger";
			model.content = "This is a test";
		}
	}
}
