﻿requirejs.config({
	baseUrl: '/',
	paths: {
		app: 'application/app',
		common: 'application/common',

		text: 'vendor/js/require-text',
		json: 'vendor/js/require-json',

		json2: 'vendor/js/json2.min',
		jquery: 'vendor/js/jquery.min',
		underscore: 'vendor/js/underscore.min',
		backbone: 'vendor/js/backbone.min',
		marionette: 'vendor/js/backbone.marionette.min',
		syphon: 'vendor/js/backbone.syphon',
		bootstrap: 'vendor/js/bootstrap.min',
		codemirror: 'vendor/js/codemirror-all',
		
		tiles:		'application/tiles/tiles',
		apps:		'application/sections/user',
		settings:	'application/sections/system'
	},
	shim: {
		bootstrap: ['jquery'],
		backbone: {
			deps: ['json2', 'jquery', 'underscore'],
			exports: 'Backbone'
		},
		syphon: {
			deps: ['backbone'],
			exports: 'Backbone.Syphon'
		},
		marionette: {
			deps: ['backbone', 'syphon'],
			exports: 'Marionette'
		}
	}
});

require(['app', 'common', 'bootstrap'], function (app) {
	app.start();
});