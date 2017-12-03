!function ($) {
	$(function () {

		$('.gallery').each(function () {
			var links = this.getElementsByTagName('a');
			$(links).click(function (a) {
				var target = a.target || a.srcElement,
					link = target.src ? target.parentNode : target,
					options = { index: link, event: a };
				blueimp.Gallery(links, options);
			})
		});

	});
}(window.jQuery);