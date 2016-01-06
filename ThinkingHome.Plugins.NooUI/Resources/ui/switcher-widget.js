﻿define(['lib', 'lang!webapp/nooui/lang.json'], function (lib, lang) {

	var switcherWidgetView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(
			'<div>{{displayName}}</div>' +
				'<div class="btn-group btn-group-justified">' +
					'<a href="#" class="btn btn-default js-btn-on">{{lang \'On\'}}</a>' +
					'<a href="#" class="btn btn-default js-btn-off">{{lang \'Off\'}}</a>' +
				'</div>'),
		triggers: {
			"click .js-btn-on":  "switcher:on",
			"click .js-btn-off": "switcher:off"
		},
		templateHelpers: { lang: lang }
	});

	var createSender = function (channel, cmd) {
		return function () {
			return lib.$.getJSON('/api/noolite', { ch: channel, cmd: cmd });
		}
	};

	var switcherWidget = lib.common.Widget.extend({
		show: function (model) {
			var view = new switcherWidgetView({ model: model }),
				channel = model.get('data').channel;

			this.listenTo(view, 'switcher:on', createSender(channel, 2));
			this.listenTo(view, 'switcher:off', createSender(channel, 0));
			this.region.show(view);
		}
	});

	return switcherWidget;
});
