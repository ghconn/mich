///<reference path='jquery-1.10.2.min.js' />
(function (c) {
    window.YZY = c.extend(window.YZY || {}, {
        getCookie: function (name) {
            return (name = document.cookie.match("(^|;) ?" + name + "=([^;]*)")) ? decodeURIComponent(name[2]) : null
        },

        setCookie: function (name, value, path) {
            var exp = new Date();
            exp.setTime(exp.getTime() + 20 * 60 * 1000); //20分钟过期
            document.cookie = name + '=' + encodeURIComponent(value) + ';expires=' + exp.toGMTString() + ';path=' + path;
            return true;
        },

        refreshCookie: function (name) {
            //todo:
        },

        delCookie: function (name) {
            name += "=";
            var b = new Date;
            b.setFullYear(b.getFullYear() - 1);
            name += "; expires=" + b.toUTCString();
            document.cookie = name
        },

        setSessionCookie: function (name, value, cookiePath) {
            var isIE = !-[1, ];//判断是否是ie核心浏览器  
            if (isIE) {
                if (value) {
                    var expire = '; expires=At the end of the Session';
                    var path = '';
                    if (cookiePath != null) {
                        path = '; path=' + cookiePath;
                    }
                    document.cookie = name + '=' + escape(value) + expire + path;
                }
            } else {
                if (value) {
                    var expire = '; expires=Session';
                    var path = '';
                    if (cookiePath != null) {
                        path = '; path=' + cookiePath;
                    }
                    document.cookie = name + '=' + escape(value) + expire + path;
                }
            }
        },
        urlSafeBase64Encode: function (a) {
            return window.btoa(a).replace(/\+/g, '_').replace(/\//g, '!').replace(/=/g, '-')
        },
        ctxs_Ajax_Wrapper: function (options) {
            var defaultOptions = {
                type: 'post',
                beforeSend: function (xhr) {
                    var csrfToken = YZY.getCookie('CsrfToken');
                    if (csrfToken != null) {
                        xhr.setRequestHeader('Csrf-Token', csrfToken);
                    }
                    var isUsingHttps = location.protocol.toLowerCase() == 'https' ? 'Yes' : 'No';
                    xhr.setRequestHeader('X-Citrix-IsUsingHTTPS', isUsingHttps);
                }
            }
            options = $.extend({}, defaultOptions, options);
            $.ajax(options);
        },

        displayPopup: function (a, b) {
            b.fadeIn(500, function () {
                a.show()
                b.PlaceFocusOnFirstElement()
            })
        },
        fadePopup: function (a, b, c) {
            b.fadeOut(500, function () {
                a.hide();
                c && c()
            })
        },
        hidePopup: function (a, b) {
            b.hide();
            a.hide()
        },
        isPopupVisible: function (a) {
            return a.is(':visible')
        },
        LAUNCH_STARTING: 0,
        LAUNCH_SUCCESS: 1,
        LAUNCH_READY: 2,
        LAUNCH_FAILURE: 3
    })

    c.extend(c.fn, {
        elDisplayPane: function () {//切换页面视图
            c('.fullscreen-pane').hide();
            this.show();
            return this
        },
        elCenter: function () {
            return this.elCenterHorizontally().elCenterVertically()
        },
        elCenterHorizontally: function () {
            return this.each(function () {
                var a = c(this), b = a.outerWidth() / 2 * -1;
                a.css({ left: '50%', 'margin-left': b + 'px' })
            })
        },
        elCenterVertically: function () {
            return this.each(function () {
                var a = c(this), b = a.outerHeight() / 2 * -1;
                a.css({ top: '50%', 'margin-top': b + 'px' })
            })
        },
        PlaceFocusOnFirstElement: function (a) {
            a = a || {};
            this.find(a.selector || ':focusable').first().focus();
            return this
        },
        AddCustomTooltip: function (a, b) {
            function d(d, h) {
                b.find('.pluginassistant-popup').hide();
                200 < c(window).height() - h ? a.css({
                    left: d,
                    top: h
                }).show() : a.css({
                    left: d,
                    top: e.offset().top - a.height() - 40
                }).show()
            }
            var e = this;
            this.on('mouseleave focusout', function () {
                a.hide()
            });
            this.on('mouseenter', function (a) {
                var b = c(this);
                d(a.pageX, b.offset().top + b.outerHeight(!0) + 20)
            });
            this.on('click', function (a) {
                a.preventDefault();
                a = c(this);
                d(e.offset().left, a.offset().top + a.outerHeight(!0))
            });
            a.hide()
        }
    })

    String.prototype.format = function () {
        for (var a = this, b = 0; b < arguments.length; b++)
            a = a.replace('{' + b + '}', arguments[b]);
        return a
    };
})(jQuery);

(function (c) {
    function b(a) {
        return 'com:yzy:web:' + a
    }
    window.YZY = window.YZY || {};
    YZY.Events = {
        publish: function (a, e) {
            a && c(document).triggerHandler(b(a), [e])
        },
        unsubscribe: function (a) {
            a && c(document).off(b(a))
        },
        subscribe: function (a, e) {
            if (a)
                if (c.isFunction(e))
                    c(document).on(b(a), e);
                else
                    c.isArray(e) && c.each(e, function (c, e) {
                        c(document).on(b(a), e)
                    })
        }
    }
})(jQuery);

(function (d) {
    var b = {
        IOS: 'ios',
        ANDROID: 'android',
        WINDOWS: 'windows',
        MAC: 'mac',
        LINUX: 'linux',
        WINDOWS_MOBILE: 'windows_mobile',
        CHROME_OS: 'Chrome OS',
        OTHER: 'other'
    },
    a = d(window);
    window.YZY = window.YZY || {};
    YZY.Orientation = {
        PORTRAIT: 'portrait',
        LANDSCAPE: 'landscape'
    };
    YZY.DeviceInfo = function (a, c, d) {
        this._userAgent = a || '';
        this._vendor = c || '';
        this._currentPlatform = b.OTHER;
        d && 0 <= d.indexOf('ios') ? this._currentPlatform = b.IOS : d && 0 <= d.indexOf('android') ? this._currentPlatform = b.ANDROID : this._userAgent.match(/(Android)\s+([\d.]+)/) ? this._currentPlatform = b.ANDROID : this._userAgent.match(/(iPad|iPhone|iPod)/i) ? this._currentPlatform = b.IOS : this._userAgent.match(/(Windows Phone)/i) ? this._currentPlatform = b.WINDOWS_MOBILE : this._userAgent.match(/(windows)/i) ? this._currentPlatform = b.WINDOWS : this._userAgent.match(/(mac os x)/i) ? this._currentPlatform = b.MAC : this._userAgent.match(/(CrOS)/i) ? this._currentPlatform = b.CHROME_OS : this._userAgent.match(/(linux)/i) && (this._currentPlatform = b.LINUX)
    };
    YZY.DeviceInfo.prototype = {
        getPlatform: function () {
            return this._currentPlatform
        },
        isSupported: function () {
            return this._currentPlatform !== b.OTHER
        },
        isDesktop: function () {
            return this._currentPlatform != b.ANDROID && this._currentPlatform != b.IOS && this._currentPlatform != b.WINDOWS_MOBILE
        },
        isMobileDevice: function () {
            return this._currentPlatform === b.ANDROID || this._currentPlatform === b.IOS || this._currentPlatform === b.WINDOWS_MOBILE
        },
        supportsHighResDisplay: function () {
            return window.devicePixelRatio && 1.25 <= window.devicePixelRatio
        },
        getOrientation: function () {
            return a.width() > a.height() ? YZY.Orientation.LANDSCAPE : YZY.Orientation.PORTRAIT
        },
        isNativeClient: function () {
            return YZY.isUrlParamDefined('native')
        },
        isCitrixChromeApp: function () {
            return !!this._userAgent.match(/(CitrixChromeApp)/i)
        },
        isWindowsPlatform: function () {
            return this._currentPlatform === b.WINDOWS
        },
        isWindowsRT: function () {
            return this.isWindowsPlatform() && this._userAgentContains('ARM')
        },
        isMacOSX: function () {
            return this._userAgentContains('Mac OS X') && !this._userAgentContains('Mobile')
        },
        isUnsupportedMacOSXVersion: function () {
            var a = YZY.Config.getConfigValue('pluginAssistant.macOS.minimumSupportedOSVersion');
            if (!a)
                return !1;
            var c = this._getMacOSXVersion();
            return -1 == c ? !1 : YZY.isOlderThan(a, c)
        },
        isLinux: function () {
            return this._currentPlatform === b.LINUX
        },
        isChromeOS: function () {
            return this._currentPlatform === b.CHROME_OS
        },
        isNetscapePluginSupported: function () {
            return this.isWindowsPlatform() && (this.isFirefox() || this.isChrome()) || this.isMacOSX() && (this.isSafari() || this.isFirefox() || this.isChrome()) || this.isLinux() && this.isFirefox()
        },
        isBrowserPluginSupported: function () {
            return !this.isProtocolHandlerSupported() && (this.isNetscapePluginSupported() || this.isWindowsPlatform() && this.isIE())
        },
        isProtocolHandlerSupported: function () {
            var a = YZY.Config.getConfigValue('pluginAssistant.protocolHandler.platforms');
            return this.isUserAgentMatchedByRegex(a) && !this.isMobileDevice()
        },
        isProtocolHandlerEnabled: function () {
            return 'true' == YZY.Config.getConfigValue('pluginAssistant.protocolHandler.enabled')
        },
        isIE: function () {
            return this._userAgentContains('MSIE') || this._userAgentContains('Internet Explorer') || this._userAgentContains('Trident')
        },
        isLegacyIE: function () {
            return this._userAgentContains('Trident/4') || 8 === document.documentMode
        },
        isSafari: function () {
            return 0 <= this._vendor.indexOf('Apple')
        },
        isFirefox: function () {
            return this._userAgentContains('Firefox')
        },
        isChrome: function () {
            return this._userAgentContains('Chrome') && !this._userAgentContains('Edge')
        },
        isSpartan: function () {
            return this._userAgentContains('Chrome') && this._userAgentContains('Edge') && this._userAgentContains('Windows')
        },
        isUserAgentMatchedByRegex: function (a) {
            try {
                var c = a.split(/;|,/);
                for (a = 0; a < c.length; a++)
                    if (0 != c[a].trim().length && (new RegExp(c[a], 'i')).test(this._userAgent))
                        return !0
            } catch (b) { }
            return !1
        },
        _userAgentContains: function (a) {
            return 0 <= this._userAgent.indexOf(a)
        },
        _getMacOSXVersion: function () {
            if (!this.isMacOSX() || !this.isSafari() && !this.isFirefox())
                return -1;
            var a = /Mac OS X (\d+([\.|_]\d+)*)/.exec(this._userAgent);
            return a && 1 < a.length ? a[1].replace(/_/g, '.') : -1
        }
    };
    YZY.Device = new YZY.DeviceInfo(navigator.userAgent, navigator.vendor, window.location.search);
    YZY.Device.Platform = b
})(jQuery);