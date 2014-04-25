﻿define(['marionette', 'backbone'], function (Marionette, Backbone) {

	var app = new Marionette.Application();

	app.addRegions({
		regionNavigation: "#nav-region",
		regionNavigationRight: "#right-nav-region",
		regionMain: "#main-region"
	});
	
	app.navigate = function (route, options) {

		options || (options = {});

		Backbone.history.navigate(route, options);
	};

	app.on('initialize:after', function() {

		if (Backbone.history) {
			Backbone.history.start();
		}
	});

	return app;
});