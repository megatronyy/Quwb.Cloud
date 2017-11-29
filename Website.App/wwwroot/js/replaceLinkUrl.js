/*
替换Url前缀
reference:jquery,js/common.js
*/
$(document).ready(function () {
	$("a").each(function (index, domEle) {
		var href = $(domEle).attr("href");
		var isCotainJs = href.indexOf("javascript:") > -1;
		var noReplace = (
			href == "javascript:void(0);" || href == "javascript:;" || isCotainJs || href == "#" || href == "###");
		var key = "cientUserId";
		var keyValue = utils.getQueryString(key);
		if (!noReplace && keyValue) {//客服代下单替换Url
			var url = window.utils.updateUrlParam(href, key, keyValue);
			$(domEle).attr("href", url);
		}

	});
});