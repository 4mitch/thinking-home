﻿define(['app'], function (application) {

	application.module('AlarmClock.List', function (module, app, backbone, marionette, $, _) {

		// entities
		module.AlarmListItem = backbone.Model.extend({
			defaults: {
				hours: 0,
				minutes: 0
			}
		});
		
		module.AlarmEditorModel = backbone.Model.extend();

		module.AlarmCollection = backbone.Collection.extend({
			model: module.AlarmListItem,
			comparator: function(alarm) {
				return alarm.get("hours") * 60 + alarm.get("minutes");
			}
		});

		// api
		var api = {

			loadList: function () {

				var defer = $.Deferred();

				$.getJSON('/api/alarm-clock/list')
					.done(function (items) {
						var collection = new module.AlarmCollection(items);
						defer.resolve(collection);
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			},
			setState: function (id, enabled) {

				return $.post('/api/alarm-clock/set-state', { id: id, enabled: enabled }).promise();
			},
			saveAlarm: function (model) {

				return $.post('/api/alarm-clock/save', model.toJSON()).promise();
			},
			deleteAlarm: function (id) {

				return $.post('/api/alarm-clock/delete', { id: id }).promise();
			},
			stopAlarm: function () {

				return $.post('/api/alarm-clock/stop').promise();
			},
			loadEditor: function (id) {
			
				var defer = $.Deferred();

				$.getJSON('/api/alarm-clock/editor', { id: id })
					.done(function (alarm) {
						var model = new module.AlarmEditorModel(alarm);
						defer.resolve(model);
					})
					.fail(function () {
						defer.resolve(undefined);
					});

				return defer.promise();
			}
		};

		// requests
		app.reqres.setHandler('load:alarm-clock:list', function () {
			return api.loadList();
		});
		
		app.reqres.setHandler('update:alarm-clock:set-state', function (id, enabled) {
			return api.setState(id, enabled);
		});
		
		app.reqres.setHandler('update:alarm-clock:save', function (model) {
			return api.saveAlarm(model);
		});
		
		app.reqres.setHandler('update:alarm-clock:stop', function () {
			return api.stopAlarm();
		});
		
		app.reqres.setHandler('update:alarm-clock:delete', function (id) {
			return api.deleteAlarm(id);
		});
		
		app.reqres.setHandler('load:alarm-clock:editor', function (id) {
			return api.loadEditor(id);
		});
	});

	return application.AlarmClock.List;
});