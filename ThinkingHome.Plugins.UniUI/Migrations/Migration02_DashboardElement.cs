﻿using System.Data;
using ECM7.Migrator.Framework;
using ForeignKeyConstraint = ECM7.Migrator.Framework.ForeignKeyConstraint;

namespace ThinkingHome.Plugins.UniUI.Migrations
{
	[Migration(2)]
	public class Migration02_DashboardElement : Migration
	{
		public override void Apply()
		{
			Database.AddTable("UniUI_DashboardElement",
				new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey, "newid()"),
				new Column("DashboardId", DbType.Guid, ColumnProperty.NotNull),
				new Column("TypeAlias", DbType.String, ColumnProperty.NotNull),
				new Column("SortOrder", DbType.Int32, ColumnProperty.NotNull, 0),
				new Column("SerializedParameters", DbType.String.WithSize(int.MaxValue))
			);

			Database.AddForeignKey("FK_UniUI_DashboardElement_DashboardId",
				"UniUI_DashboardElement", "DashboardId", "UniUI_Dashboard", "Id", ForeignKeyConstraint.Cascade);
		}

		public override void Revert()
		{
			Database.RemoveTable("UniUI_DashboardElement");
		}
	}
}
