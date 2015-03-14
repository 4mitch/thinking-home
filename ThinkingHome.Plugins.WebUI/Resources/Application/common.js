﻿define([
	'app',
	'json!api/webui/styles.json',
	'application/common/complex-view',
	'application/common/form-view',
	'application/common/sortable-view',
	'application/common/utils'],
	function (application, cssFiles, complexView, formView, sortableView, utils) {

		var commonModule = {
			ComplexView: complexView,
			FormView: formView
		};

		application.Common.ComplexView = complexView;
		application.Common.FormView = formView;

		application.module('Common', function (module, app, backbone, marionette, $, _) {

			module.utils.loadCss.apply(null, cssFiles);
			module.utils.displayCurrentTime('.js-cur-time');
		});

		return application.Common;
	});