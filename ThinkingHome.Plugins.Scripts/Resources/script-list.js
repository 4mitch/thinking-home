﻿define(
	['app',
		'lib',
		'webapp/scripts/script-list-model',
		'webapp/scripts/script-list-view'],
	function (application, lib, models, views) {

		var api = {
			runScript: function (view) {

				var scriptId = view.model.get('id');

				models.runScript(scriptId).done(function () {

					var name = view.model.get('name');
					lib.utils.alert('The script "{0}" has been executed.', name);
				});
			},

			deleteScript: function (view) {

				var scriptName = view.model.get('name');

				if (lib.utils.confirm('Delete the script "{0}"?', scriptName)) {

					var scriptId = view.model.get('id');

					models.deleteScript(scriptId).done(api.reload);
				}
			},

			addScript: function () {

				application.navigate('webapp/scripts/script-editor');
			},
			editScript: function (childView) {

				var scriptId = childView.model.get('id');
				application.navigate('webapp/scripts/script-editor', scriptId);
			},
			reload: function () {

				models.loadScriptList()
					.done(function (items) {

						var view = new views.ScriptListView({ collection: items });

						view.on('scripts:add', api.addScript);
						view.on('childview:scripts:edit', api.editScript);
						view.on('childview:scripts:run', api.runScript);
						view.on('childview:scripts:delete', api.deleteScript);

						application.setContentView(view);
					});
			}
		};

		return {
			start: function () {
				api.reload();
			}
		};
	});