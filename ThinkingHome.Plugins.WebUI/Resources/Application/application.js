﻿define(['lib', 'application/router', 'application/layout'], function (lib, router, layout) {

	// init
	var app = new lib.marionette.Application();

	app.router = new router({
		defaultRoute: 'tiles'
	});

	app.layout = new layout({
		template: '#layout-template'
	});

	// start
	app.on('start', function () {

		this.layout.render();
		this.router.start();
	});

	// shortcuts
	app.setContentView = function(view) {

		this.layout.setContentView(view);
	};

	app.navigate = function (route) {

		var args = Array.prototype.slice.call(arguments, 1);
		app.router.navigate(route, args);
	};

	app.addTile = function (def, options) {

		var optionsJson = JSON.stringify(options);

		$.post('/api/webui/tiles/add', { def: def, options: optionsJson })
			.done(function () {
				app.router.navigate('tiles');
			});
	};

	app.loadPath = function (route, args) {

		app.router.navigate(route, args);
	};

	return app;
});