//(function ($) {

//    var cityId = window.utils.getCurrentCityId();
//    if(cityId==undefined)
//        $.getScript("http://ip.yiche.com/iplocation/setcookie.ashx", function () {
//            if (bit_locationInfo != undefined) {
//                cityId = bit_locationInfo.cityId;
//                window.utils.seCurrentCityId(cityId);
//                //window.utils.setCookie("cookie-banman-cityInfo", JSON.stringify(bit_locationInfo));
         
//            }
//        });

//})(jQuery, $);

/**
 * h5定位封装，
 * 使用方法：
 * h5Location.getCurrLocation(cbSuccess,cbFail)  h5定位(需要用户授权)
 * h5Location.getCurrIpLocation  IP定位
 * reference utils
**/
(function (win) {
    win.h5Location = {};
    var geolocation = null;
    var init = function(cb) {
        utils.loadScript("https://apis.map.qq.com/tools/geolocation/min?key=DV7BZ-GQ5C6-JWNSA-MLBU2-ZPWBQ-PABRC&referer=bmcw ", function () {
            geolocation = new qq.maps.Geolocation("DV7BZ-GQ5C6-JWNSA-MLBU2-ZPWBQ-PABRC", "bmcw");
            cb && cb();
        });
    };
    
    h5Location.getCurrLocation = function (cbSuccess, cbFail) {//手机定位
        //if (!cbFail) {
        //    cbFail = this.getCurrIpLocation(cbSuccess);
        //}
        if (!geolocation) {//
            init(function() {
                getLocation(cbSuccess, cbFail);
            });
        } else {
            getLocation(cbSuccess,cbFail);
        }
    }
    h5Location.getCurrIpLocation = function (cbSuccess, cbFail) {//IP定位
        if (!geolocation) {//
            init(function () {
                getIpLocation(cbSuccess, cbFail);
            });
        } else {
            getIpLocation(cbSuccess, cbFail);
        }
    }
    function getLocation(cbSuccess, cbFail) {
        h5Location.cbSucess = cbSuccess;
        h5Location.cbFail = cbFail;
        geolocation.getLocation(showPosition, showErr, { timeout: 15000 });
    }

    function getIpLocation(cbSuccess, cbFail) {
        h5Location.cbSucess = cbSuccess;
        h5Location.cbFail = cbFail;
        geolocation.getIpLocation(showPosition, showErr, { timeout: 15000 });
    }

    function showPosition(position) {
        var province = position.province;
        var city = position.city;
        var district = position.district;
        if (h5Location.cbSucess) {
            h5Location.cbSucess({
                province: province,
                city: city,
                district:district || ''
            });
            h5Location.cbSucess = null;
        }
    };
    function showErr() {
        //alert("拒绝了授权");
        if (h5Location.cbFail) {
            h5Location.cbFail();
            h5Location.cbFail = null;
        }
        
    };

    

})(window);