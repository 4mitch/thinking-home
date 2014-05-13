﻿requirejs.config({
	baseUrl: '/',
	paths: {
		app: 'application/app',
		common: 'application/common/common',

		tpl: 'vendor/js/tpl.min',
		json2: 'vendor/js/json2.min',
		jquery: 'vendor/js/jquery.min',
		underscore: 'vendor/js/underscore.min',
		backbone: 'vendor/js/backbone.min',
		marionette: 'vendor/js/backbone.marionette.min',
		bootstrap: 'vendor/js/bootstrap.min',
		codemirror: 'vendor/js/codemirror-all',
		
		'menu': 'application/menu/menu-controller',
		'menu-model': 'application/menu/menu-model',
		'menu-view': 'application/menu/menu-view'
	},
	shim: {
		bootstrap: ['jquery'],
		backbone: {
			deps: ['json2', 'jquery', 'underscore'],
			exports: 'Backbone'
		},
		marionette: {
			deps: ['backbone'],
			exports: 'Marionette'
		}
	}
});

require(['app', 'common', 'menu', 'bootstrap'], function (app) {
	app.start();
});