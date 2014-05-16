﻿define(
	['app',
		'webapp/scripts/script-list-model',
		'webapp/scripts/script-list-view'],
	function (application) {

		application.module('Scripts.List', function (module, app, backbone, marionette, $, _) {

			var api = {

				openEditor: function (itemView) {

					var scriptId = itemView.model.get('id');
					app.trigger('page:open', 'webapp/scripts/script-editor', scriptId);
				},
				reload: function () {

					var rq = app.request('load:scripts:list');

					$.when(rq).done(function (items) {

						var view = new module.ScriptListView({ collection: items });

						view.on('itemview:scripts:edit', api.openEditor);
						
						app.setContentView(view);
					});
				}
			};

			module.start = function () {
				api.reload();
			};

		});

		return application.Scripts.List;
	});