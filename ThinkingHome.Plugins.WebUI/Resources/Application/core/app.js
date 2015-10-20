﻿define(['lib',
	'application/core/app-view',
	'application/core/app-router',
	'application/core/app-radio',
	'application/core/app-time',
	'json!api/webui/styles.json'],
	function (lib, views, routing, messages, time, cssFiles) {

		var homeApplication = lib.marionette.Application.extend({
			initialize: function (options) {
				this.layout = new views.LayoutView();
				this.layout.on('navigate', this._loadPage, this);

				this.router = new routing.Router();
				this.router.on('navigate', this._loadPage, this);

				this.timer = new time.Timer();
				this.timer.on('update', this._updateInfo, this);

				this.radio = new messages.Radio();
			},
			onStart: function () {
				this.layout.render();
				lib.utils.loadCss.apply(null, cssFiles);

				this.router.start();
				this.radio.start();
				this.timer.start();
			},
			onBeforeDestroy: function () {
				this.layout.destroy();
				this.radio.destroy();
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

				require([route], function (appSection) {
					self.appSection && self.appSection.destroy();

					var instance = self.appSection = new appSection({ application: self });
					instance.start.apply(instance, args);
					self.router.setPath(route, args);
				});
			},

			_updateInfo: function(text) {
				this.layout.setInfoText(text);
			}
		});

		return homeApplication;
	});