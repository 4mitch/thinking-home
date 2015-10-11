﻿define(
	['lib', 'application2/app-view', 'application2/app-router', 'application2/app-time'],
	function (lib, views, routing, time) {

		var homeApplication = lib.marionette.Application.extend({
			initialize: function (options) {
				this.layout = new views.LayoutView();
				this.layout.on('navigate', this._loadPage, this);

				this.router = new routing.Router();
				this.router.on('navigate', this._loadPage, this);

				this.timer = new time.Timer();
				this.timer.on('update', this._updateInfo, this);
			},
			onStart: function () {
				this.layout.render();
				this.router.start();
				this.timer.start();
			},
			onBeforeDestroy: function () {
				this.layout.destroy();
				this.timer.destroy();
			},

			// shortcuts
			setContentView: function (view) {
				this.layout.setContentView(view);
			},

			navigate: function (route) {
				var args = Array.prototype.slice.call(arguments, 1);
				this._loadPage(route, args);
			},

			// private
			_loadPage: function (route, args) {

				var self = this;

				route = route || 'dashboard';
				args = args || [];

				require([route], function (obj) {

					obj.start.apply(obj, args);
					self.router.setPath(route, args);
				});
			},

			_updateInfo: function(text) {
				this.layout.setInfoText(text);
			}
		});

		return homeApplication;
	});