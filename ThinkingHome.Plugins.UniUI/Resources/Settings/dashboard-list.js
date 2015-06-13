﻿define(['app', 'common',
		'webapp/uniui/settings/dashboard-list-model.js',
		'webapp/uniui/settings/dashboard-list-view.js'
],
	function (application, common, models, views) {

		var api = {



			openDashboard: function (childView) {
				
				var id = childView.model.get("id");
				application.navigate('webapp/uniui/settings/widget-list', id);
			},
			moveUp: function (childView) {
				
				var id = childView.model.get("id");
				models.moveDashboard(id, true).done(api.loadDashboardList);

				childView.mute();
			},

			moveDown: function (childView) {

				var id = childView.model.get("id");
				models.moveDashboard(id, false).done(api.loadDashboardList);

				childView.mute();
			},

			renameDashboard: function (childView) {

				var id = childView.model.get("id"),
					title = childView.model.get("title"),
					newTitle = window.prompt("Enter new title", title);

				if (newTitle) {

					models.renameDashboard(id, newTitle).done(api.loadDashboardList);
				}
			},

			deleteDashboard: function (childView) {

				var id = childView.model.get("id"),
					title = childView.model.get("title");

				if (common.utils.confirm('Do you want to delete the dashboard "{0}"?', title)) {

					models.deleteDashboard(id).done(api.loadDashboardList);
				}
			},

			loadDashboardList: function () {

				models.loadDashboardList()
					.done(function (list) {

						var view = new views.DashboardListView({
							collection: list
						});

						view.on("childview:dashboard:open", api.openDashboard);
						view.on("childview:dashboard:rename", api.renameDashboard);
						view.on("childview:dashboard:delete", api.deleteDashboard);
						view.on("childview:dashboard:move:up", api.moveUp);
						view.on("childview:dashboard:move:down", api.moveDown);

						application.setContentView(view);
					});
			}
		};

		return {
			start: api.loadDashboardList
		};
	});