﻿define(
	['app', 'common', 'application/tiles/tiles-model', 'application/tiles/tiles-view'],
	function (application, commonModule) {

		application.module('WebUI.Tiles', function (module, app, backbone, marionette, $, _) {

			var api = {

				open: function (itemView) {

					var id = itemView.model.get('id');
					var url = itemView.model.get('url');

					if (url) {
						app.trigger("page:load", url);
					} else {
						app.request('update:tiles:action', id).done(api.done);
					}
				},

				done: function (message) {
					alert(message || 'Done!');
				},

				reload: function () {

					app.request('load:tiles:all').done(function (collection) {

						var view = new module.TileCollectionView({
							collection: collection
						});

						view.on('itemview:webui:tile:click', api.open);

						app.setContentView(view);
					});
				}
			};

			module.start = function () {
				api.reload();
			};
		});

		return application.WebUI.Tiles;
	});