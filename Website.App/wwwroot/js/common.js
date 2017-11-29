

//通用处理类
window.utils = {
	//是否微信浏览器
	isWeiXin: function () {
		var ua = window.navigator.userAgent.toLowerCase();
		if (ua.match(/MicroMessenger/i) == 'micromessenger') {
			return true;
		} else {
			return false;
		}
	}
	//获取查询字符串变量
	,
	getQueryString: function (name) {
		var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
		var r = window.location.search.substr(1).match(reg);
		if (r != null) return unescape(r[2]);
		return null;
	},
	setCookie: function (name, value) {
		var days = 30;
		var exp = new Date();
		exp.setTime(exp.getTime() + days * 24 * 60 * 60 * 1000);
		document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
	},
	getCookie: function (name) {
		var strCookie = document.cookie;
		//将多cookie切割为多个名/值对 
		var arrCookie = strCookie.split("; ");
		var userId;
		//遍历cookie数组，处理每个cookie对 
		for (var i = 0; i < arrCookie.length; i++) {
			var arr = arrCookie[i].split("=");
			//找到名称为userId的cookie，并返回它的值 
			if (name === arr[0]) {
				return arr[1];
				break;
			}
		}

	},
	loadIframe: function (iframe, callback) {
		iframe = typeof iframe == "string" ? document.getElementById(iframe) : iframe;
		if (iframe.attachEvent) {
			iframe.attachEvent("onload",
				function () {
					callback && callback();
				});
		} else {
			iframe.onload = function () {
				callback && callback();
			}
		}
	} //end loadIrame
	,
	loadScript: function (url, callback, charset) {
		var s = document.createElement("script");
		s.type = "text/javascript";
		charset && (s.charset = charset);
		if (s.readyState) {
			s.attachEvent("onreadystatechange",
				function () {
					if (s.readyState == "loaded" || s.readyState == "complete") {
						s.detachEvent('onreadystatechange', arguments.callee);
						callback && callback();
					}
				});
		} else {
			s.onload = function () {
				callback && callback();
			};
		}
		s.src = url;
		document.getElementsByTagName("head")[0].appendChild(s);
	} //loadScript
	,
	loadImage: function (src, callback, errorCallback) {
		var img = new Image();
		img.onload = function () {
			img.onload = null;
			callback(img);
		}
		if (typeof errorCallback != "undefined") {
			img.onerror = function () {
				img.onerror = null;
				errorCallback(img);
			}
		}
		img.src = src;
	},
	getCurrentCityId: function () {
		//return this.getCookie("cookie-banman-cityId") || 201;
		var cityId = 201;
		var userAddress = Locstor.get("user-address");
		if (userAddress) {
			cityId = userAddress.CityId;
		}
		return cityId;
	},
	//getCurrentCityInfo: function () {
	//    var cityInfo = this.getCookie("cookie-banman-cityInfo");
	//    if (JSON && cityInfo != undefined) {
	//        return JSON.parse(cityInfo);
	//    }
	//    return {};
	//},
	seCurrentCityId: function (value) {
		this.setCookie("cookie-banman-cityId", value);
	},
	getCostPriceTips: function (costPrice) {
		if (costPrice === 0)
			$("#rowCostService").html('工本费<p>需现场支付，以收据发票为准</p>');
		else
			$("#rowCostService").html('工本费<span id="spPriceCost">0</p>');
	},


	//初始化地址组件中的数据
	initLocationMobileSelect: function (drpProvince, drpCity) {
		var userAddress = Locstor.get("user-address");
		if (userAddress) {
			var provinceId = userAddress.ProvinceId;
			var cityId = userAddress.CityId;
			var districtId = userAddress.DistrictId;
			if (provinceId > 0 && cityId > 0) {
				var promiseProvince = $.ajax({
					url: "/ajax/ajaxLocation.ashx",
					data: "rp=getlocation&lid=" + provinceId,
					type: "GET",
					dataType: "JSON"
				});

				var promiseCity = $.ajax({
					url: "/ajax/ajaxLocation.ashx",
					data: "rp=getlocation&lid=" + cityId,
					type: "GET",
					dataType: "JSON"
				});
				promiseProvince.done(function (response) {
					var proProvince = response.LocationName;
					promiseCity.done(function (response) {
						var proCity = response.LocationName;
						//drpCity.on("change", function () {
						//    window.utils.cooperateCityInit($(this).val());
						//});
						window.utils.cooperateCityInit(cityId);
						drpCity.trigger("change");
						$("#trigger1").html(proProvince + "  " + proCity);
						$("#trigger6").html(proProvince + "  " + proCity);
						drpProvince.val(provinceId);
						drpCity.val(cityId);
					});
				});
			}
		}
	},



	//初始化位置下拉框
	initLocationDropdown: function (drpProvince, drpCity) {
		var userAddress = Locstor.get("user-address");
		if (userAddress) {
			var provinceId = userAddress.ProvinceId;
			var cityId = userAddress.CityId;
			var districtId = userAddress.DistrictId;
			if (provinceId > 0 && cityId > 0) {
				drpProvince.find("option[value='" + provinceId + "']").attr("selected", "selected");
				var promise = $.ajax({
					url: "/ajax/ajaxLocation.ashx",
					data: "rp=getcity&provinceId=" + provinceId,
					type: "GET",
					dataType: "JSON"
				});
				promise.done(function (response) {
					for (var i = 0; i < response.length; i++) {
						var item = response[i];
						if (cityId == item.CityId) {
							drpCity.append('<option value="' +
								item.CityId +
								'" selected=\"selected\">' +
								item.CityName +
								'</option>');
						} else {
							drpCity.append('<option value="' + item.CityId + '">' + item.CityName + '</option>');
						}
					} //end for

					drpCity.on("change", function () {

						window.utils.cooperateCityInit($(this).val());
					});
					//window.utils.addEventHandler(drpCity, "change", function () {

					//    window.utils.cooperateCityInit($(this).val());
					//});
					window.utils.cooperateCityInit(cityId);
					drpCity.trigger("change");
				});
			}
		}
	},
	cooperateCityInit: function (cityId) {
		var businessType = 0;
		var limitBusinessType = [];

		if (typeof (bizType) != "undefined") {
			businessType = bizType;
		}
		if (cityId === "2301") {
			limitBusinessType = [4, 12, 16];
			$(".meg_down dl").html(' <dt>快递地址</dt>'
				+ ' <dd>收件人： 杨涛</dd>'
				+ '<dd>联系电话：15332416326</dd>'
				+ '<dd>邮寄地址：陕西省西安市莲湖区南二环西段嘉天Smart 2号楼。</dd>');


		}
		else if (cityId === "502") {
			limitBusinessType = [4, 12, 13, 16];
			$(".meg_down dl").html(' <dt>快递地址</dt>'
				+ ' <dd>收件人： 招主京</dd>'
				+ '<dd>联系电话：13501597678</dd>'
				+ '<dd>邮寄地址：广东省深圳市南山区龙珠三路（沙河东路北行过北环立交100米）。</dd>');
		} else {
			limitBusinessType = [16];//异地年间委托书
			$(".meg_down dl").html(' <dt>快递地址</dt>'
				+ ' <dd>收件人： 斑马车务</dd>'
				+ '<dd>联系电话：400-875-6066</dd>'
				+ '<dd>邮寄地址：北京市朝阳区建国路93号万达广场8号楼1509。</dd>'
				+ '<dd>邮政编码：100022</dd>');
		}

		if (limitBusinessType.length > 0 && limitBusinessType.indexOf(businessType) >= 0) {
			$("#rowCostService").hide();
		} else {
			$("#rowCostService").show();
		}
	},
	addEventHandler: function (target, type, func) {
		if (target.addEventListener)
			target.addEventListener(type, func, false);
		else if (target.attachEvent)
			target.attachEvent("on" + type, func);
		else target["on" + type] = func;
	},
	ajaxOperate: function (url, data, cbSuccess, cbFail, cbAlways) {
		var promise = $.ajax({
			type: "post",
			url: url,
			data: data,
			dataType: "json"
		});
		promise.done(function (response) {
			if (!response.IsError) {
				if (cbSuccess) {
					cbSuccess(response.Data);
				}
			} else {
				cbFail && cbFail(response.Message);
			}
		});
		promise.fail(function (XMLHttpRequest, textStatus, errorThrown) {

		});
		promise.always(function (XMLHttpRequest, textStatus) {
			cbAlways && cbAlways();
		});
	}
	//提示框
	, alert: function (content, btnText) {
		layer.open({
			content: content
			, btn: btnText || '确定'
		});
	}
	//询问框
	, confirm: function (content, cbYes, yesText, noText) {
		yesText = yesText || '是';
		noText = noText || '否';
		layer.open({
			content: content
			, btn: [noText, yesText]
			, yes: function (index) {
				layer.close(index);
			}
			, no: function (index) {
				cbYes && cbYes();
				layer.close(index);
			}
		});
	}
	//skin,msg:普通提示，footer：底部弹出
	, msg: function (content, yes, time, skin) {
		var nTime = time || 2;
		var fYes = yes || function () { };
		layer.open({
			content: content,
			skin: skin || 'msg',
			time: nTime //2秒后自动关闭
		});
		window.setTimeout(function () {
			fYes();
		}, nTime * 1000);
	}
	, showLoading: function (content) {
		var szContent = content || '加载中..';
		//type（0表示信息框，1表示页面层，2表示加载层）
		utils.loadingIndex = layer.open({
			type: 2,
			content: szContent,
			shadeClose: false
		});
	}
	, hideLoading: function () {
		layer.close(utils.loadingIndex);
	}
	//更新Url
	, updateUrl: function (url, key) {
		var key = (key || 't') + '=';  //默认是"t"
		var reg = new RegExp(key + '\\d+');  //正则：t=1472286066028
		var timestamp = +new Date();
		if (url.indexOf(key) > -1) { //有时间戳，直接更新
			return url.replace(reg, key + timestamp);
		} else {  //没有时间戳，加上时间戳
			if (url.indexOf('\?') > -1) {
				var urlArr = url.split('\?');
				if (urlArr[1]) {
					return urlArr[0] + '?' + key + timestamp + '&' + urlArr[1];
				} else {
					return urlArr[0] + '?' + key + timestamp;
				}
			} else {
				if (url.indexOf('#') > -1) {
					return url.split('#')[0] + '?' + key + timestamp + location.hash;
				} else {
					return url + '?' + key + timestamp;
				}
			}
		}
	}
	, updateUrlParam: function (url, key, value) {
		var key = (key || 't') + '=';  //默认是"t"
		var value = (value || '');
		var oldKeyValue = key + value;
		var keyValue = utils.getQueryString(key);
		if (keyValue) {
			oldKeyValue = key + keyValue;
		}
		var newKeyValue = key + value;
		if (url.indexOf(key) > -1) { //有key，直接更新
			return url.replace(oldKeyValue, newKeyValue);
		} else {  //没key则加上
			if (url.indexOf('\?') > -1) {
				var urlArr = url.split('\?');
				if (urlArr[1]) {
					return urlArr[0] + '?' + newKeyValue + '&' + urlArr[1];
				} else {
					return urlArr[0] + '?' + newKeyValue;
				}
			} else {
				if (url.indexOf('#') > -1) {
					return url.split('#')[0] + '?' + newKeyValue + location.hash;
				} else {
					return url + '?' + newKeyValue;
				}
			}
		}
	}
};

/*
HTML5图片处理
*/
//--- start
var HTML5Utility = HTML5Utility || {};
HTML5Utility.imageFileResize = function (file, maxWidth, maxHeight, callback) {
	var img = new Image;
	var canvas = document.createElement('canvas');
	var ctx = canvas.getContext('2d');

	img.onload = function () {
		if (img.width > maxWidth || img.height > maxHeight) {
			var bili = Math.max(img.width / maxWidth, img.height / maxHeight);
			canvas.width = img.width / bili;
			canvas.height = img.height / bili;
			//var bili = img.width / img.height;
			//canvas.width = maxWidth;
			//canvas.height = maxWidth / bili;
		} else {
			canvas.width = img.width;
			canvas.height = img.height;
		}
		ctx.drawImage(img, 0, 0, img.width, img.height, 0, 0, canvas.width, canvas.height);

		//  $('body').append(canvas);
		//canvas.toDataURL(type, encoderOptions);
		callback(canvas.toDataURL("image/png", 0.7));
	};

	try {
		if (typeof (file) == "string")
			img.src = file;
		else
			img.src = window.URL.createObjectURL(file);
	} catch (err) {
		try {
			img.src = window.webkitURL.createObjectURL(file);
		} catch (err) {
			alert(err.message);
		}
	}
};
HTML5Utility.imageFileRotation = function (image_src, rotate, callback) {
	var img = new Image;
	var canvas = document.createElement('canvas');
	var ctx = canvas.getContext('2d');
	img.onload = function () {
		canvas.width = img.width;
		canvas.height = img.height;
		switch (rotate) {
			default:
			case 0:
				canvas.width = img.width;
				canvas.height = img.height;
				ctx.rotate(0 * Math.PI / 180);
				ctx.drawImage(img, 0, 0);
				break;
			case 90:
				//90 rotate right 需要向右旋转90度，PixelYDimension就是宽度了，PixelXDimension就是高度了。 
				canvas.width = img.height;
				canvas.height = img.width;
				ctx.rotate(rotate * Math.PI / 180);
				ctx.drawImage(img, 0, -img.height);
				break;
			case 180:
				canvas.width = img.width;
				canvas.height = img.height;
				ctx.rotate(rotate * Math.PI / 180);
				ctx.drawImage(img, -img.width, -img.height);
				break;
			case 270:
				canvas.width = img.height;
				canvas.height = img.width;
				ctx.rotate(rotate * Math.PI / 180);
				ctx.drawImage(img, -img.width, 0);
				break;
		};
		callback(canvas.toDataURL());
	};
	img.src = image_src;
};

function return_prepage() {
	if (window.document.referrer == "" || window.document.referrer == window.location.href) {
		window.location.href = "{dede:type}[field:typelink /]{/dede:type}";
	} else {
		window.location.href = window.document.referrer;
	}
}

/*

    reference /js/layer_mobile/layer.js,jquery
*/
(function () {
	var pluginName = "h5UploadImg";
	var defaults = {
		dir: "Common",//文件上传目录名称
		afterImgUpload: function (data) {//图片上传完成后回调方法

		}
	};
	function h5UploadImg(element, options) {
		this.element = element;
		this.settings = $.extend({}, defaults, options);
		this._defaults = defaults;
		this._name = pluginName;
		this.version = 'v1.0.0';
		this.init();
	};

	h5UploadImg.prototype = {
		init: function () {
			//var $element = $(this.element).children().first();
			var that = this;
			var settings = that.settings;
			var $element = $(this.element);
			var $file = $element.find(".file");
			var $imgBox = $element.find(".imgBox");
			var $hidIimg = $element.find(".hidImg");
			$file.on("change", function () {
				if (this.value.length == 0) {
					return;
				}
				utils.showLoading("正在上传，请稍等..");
				webUploadImage(this);
			});
			function webUploadImage(fileInput) {
				var file = fileInput.files[0];
				if (file == undefined) {
					return false;
				}
				compressAndUpload(file);
			}
			function compressAndUpload(file) {
				var local_src = "";
				if (typeof (file) == "object") {
					try {
						local_src = window.URL.createObjectURL(file);
					}
					catch (err) {
						try {
							local_src = window.webkitURL.createObjectURL(file);
						} catch (err) {
							//alert(err.message);
						}
					}
				}
				else {
					local_src = file;
				}
				var maxWidth = 1024;
				var maxHeight = 1024;
				HTML5Utility.imageFileResize(file, maxWidth, maxHeight, function (dataUrl) {
					base64ImageUpload(dataUrl, function (data) {
						$imgBox.empty().append('<li >' +
							//'<a href="' + data.HttpImgUrl + '">' +
							'<a href="javascript:;" onclick="ShowPic(\'' + data.HttpImgUrl + '\')">' +
							'<img id="" src="' + data.HttpImgUrl + '" alt=""/>' +
							'</a>' +
							'</li>');
						$hidIimg.val(data.PhysicalImgUrl);
					});
				});
			}
			function base64ImageUpload(dataUrl, afterUploadHandler) {

				$.ajax({
					type: "POST",
					url: "/ajax/fileApi.aspx", //需要链接到服务器地址  
					data: {
						dir: settings.dir,
						imgDataUrl: dataUrl
					},
					success: function (response) {
						if (!response.IsError) {
							if (afterUploadHandler) {
								afterUploadHandler(response.Data);
							}
							//NotifyService.hideLoading();
						} else {
							//NotifyService.alert({ title: '图片上传失败', message: data.message });
						}
					},
					complete: function () {
						utils.hideLoading();
					},
					dataType: "json"
				});
			};
		}
	};

	$.fn[pluginName] = function (options) {
		this.each(function () {
			var instance = $.data(this, "plugin");
			if (!instance) {
				instance = new h5UploadImg(this, options);
				$.data(this, "plugin", instance);
			}
		});

		// chain jQuery functions
		return this;
	};
})();


$(document).on("touchmove mousemove", ".mobileSelectCover", function (e) {
	e.preventDefault();
	e.stopPropagation();
});

