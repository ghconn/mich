///<reference path="jquery-1.10.2.min.js" />
var ajaxWrapper = function (options) {
    var defaultOptions = {
        type: 'post',
        beforeSend: function (xhr) {
            var csrfToken = getCookie('CsrfToken');
            if (csrfToken != null) {
                xhr.setRequestHeader('Csrf-Token', csrfToken);
            }
            var isUsingHttps = location.protocol.toLowerCase() == "https" ? "Yes" : "No";
            xhr.setRequestHeader('X-Citrix-IsUsingHTTPS', isUsingHttps);
        },
        error: function () {
            console.log('失败');
        }
    }

    options = $.extend({}, defaultOptions, options);

    $.ajax(options);
}

