///<reference path='jquery-1.10.2.min.js' />
var lwloading = (function () {
    var option = {
        relpos: 'center',//top,bottom,center
        offsetx: 0,
        offsety: 0,
        color: '#0000FF',//text,img,html
        wdith: 260,
        ibarwidth: 60,
        thickness: 2,
        speed: 'slow'
    }    
    var lwl = function () { }
    lwl.prototype.init = function (id, option) {
        this.$host = $('#' + id);
        this.$host.css('position', 'relative');//设为relative容器
        this.render(option);
        return this;
    }
    lwl.prototype.render = function (config) {
        $.extend(option, config);
        this.$lwlele = $('<div></div>');
        var ibar = $('<i></i>');
        this.$lwlele.append(ibar);
        this.$lwlele.css({
            display: 'none',
            position: 'absolute',
            width: option.wdith + 'px',
            height: option.thickness + 'px',
            border: '1px solid ' + option.color,
            overflow: 'hidden'
        });
        ibar.css({ display: 'block', background: option.color, width: option.ibarwidth + 'px', height: option.thickness + 'px', position: 'absolute' });

        var left, top;
        var hw = this.$host.width(), hh = this.$host.height();
        switch (option.relpos) {
            case "center":
                left = (hw - option.wdith) / 2;
                top = (hh - option.thickness) / 2;
                break;
            case "top":
                left = (hw - option.wdith) / 2;
                top = -option.thickness;
                break;
            case "bottom":
                left = (hw - option.wdith) / 2;
                top = hh + option.thickness;
                break;
        }
        this.$lwlele.css({ left: left + 'px', top: top + 'px' });
        this.$host.append(this.$lwlele);
        option.speed === 'fast' && (option.speed = 200)
        option.speed === 'normal' && (option.speed = 400)
        option.speed === 'slow' && (option.speed = 600)
        function start() {
            ibar.animate({ left: option.wdith + 'px' }, option.speed, function () {
                ibar.css('left', -option.ibarwidth);
                start();
            });
        }
        start();
    }
    lwl.prototype.show = function () {
        this.$lwlele !== this.$host && this.$lwlele.show();
    }
    lwl.prototype.hide = function () {
        this.$lwlele !== this.$host && this.$lwlele.hide();
    }
    return lwl;
})()