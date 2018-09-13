///<reference path='jquery-1.10.2.min.js' />
var airbubble = (function () {
    var option = {
        useicon: true,
        iconsrc: '/img/help.gif',
        iconsize: 16,
        relpos: 'right',//top,right,bottom,left
        airtype: 'text',//text,img,html
        text: '',
        src: '',
        html: '',//
        airborder: '1px solid #C6C6C6',
        offsetleft: 0,
        offsettop: 0,
        cursor: 'help',
        show: true,
        animate: { enabled: false/*todo*/ }
    }
    var _bind = function (that) {
        if (option.airtype === 'text') {
            that.$eventele.attr('title', option.text);
            return;
        }

        var air = $('<div></div>');
        air.css({ position: 'absolute', display: 'none', border: option.airborder });
        if (option.airtype === 'img') {
            var img = $('<img src="' + option.src + '" />')
            air.append(img);
            air.insertAfter(that.$eventele);
        }
        else if (option.airtype === 'html') {
            air.append($(option.html));
            air.insertAfter(that.$eventele);
        }

        that.$eventele.hover(
            function () {
                var left, top;
                //显示在上、右、下、左边自动选择
                var airwidth = air.width();
                var airheight = air.height();
                var eletop = that.$eventele.offset().top - $(document).scrollTop();
                var eleleft = that.$eventele.offset().left - $(document).scrollLeft();
                var vawidth = $(window).width();
                var vaheight = $(window).height();
                var eleright = $(window).width() - eleleft;
                var elebottom = $(window).height() - eletop;

                /*
                *元素在左上角，显示在右边
                *元素在右上角，显示在左边
                *元素在左下角，显示在右边，向上偏移air的高度
                *元素在右下角，显示在左边，向上偏移air的高度
                *无素在上边，显示在下边
                *无素在下边，显示在上边
                *无素在右边，显示在左边
                *无素在左边，显示在右边
                *元素宽度/高度大于2.5，元素高度大于eletop，显示在下边
                *元素宽度/高度大于2.5，元素高度不大于eletop，显示在上边
                *元素宽度/高度不大于2.5，元素宽度大于eleright，显示在左边
                *非以上情况，显示在右边
                */
                if (eleleft < airwidth && eletop < airheight && eleleft < vawidth / 2 && eletop < vaheight / 2) {
                    left = that.$eventele.offset().left + that.$eventele.width();
                    top = that.$eventele.offset().top;
                }
                else if (eleright < airwidth && eletop < airheight && eleright < vawidth / 2 && eletop < vaheight / 2) {
                    left = that.$eventele.offset().left - air.width();
                    top = that.$eventele.offset().top;
                }
                else if (eleleft < airwidth && elebottom < airheight && eleleft < vawidth / 2 && elebottom < vaheight / 2) {
                    left = that.$eventele.offset().left + that.$eventele.width();
                    top = that.$eventele.offset().top - air.height();
                }
                else if (eleright < airwidth && elebottom < airheight && eleright < vawidth / 2 && elebottom < vaheight / 2) {
                    left = that.$eventele.offset().left - air.width();
                    top = that.$eventele.offset().top - air.height();
                }
                else if (eletop < airheight) {
                    left = that.$eventele.offset().left + (that.$eventele.width() - air.width()) / 2;
                    top = that.$eventele.offset().top + that.$eventele.height();
                }
                else if (elebottom < airheight) {
                    left = that.$eventele.offset().left + (that.$eventele.width() - air.width()) / 2;
                    top = that.$eventele.offset().top - air.height();
                }
                else if (eleright < airwidth) {
                    left = that.$eventele.offset().left - air.width();
                    top = that.$eventele.offset().top;
                }
                else if (eleleft < airwidth) {
                    left = that.$eventele.offset().left + that.$eventele.width();
                    top = that.$eventele.offset().top;
                }
                else if (airwidth / airheight > 2.5 && air.height() > eletop) {
                    left = that.$eventele.offset().left + (that.$eventele.width() - air.width()) / 2;
                    top = that.$eventele.offset().top + that.$eventele.height();
                }
                else if (airwidth / airheight > 2.5 && air.height() <= eletop) {
                    left = that.$eventele.offset().left + (that.$eventele.width() - air.width()) / 2;
                    top = that.$eventele.offset().top - air.height();
                }
                else if (airwidth / airheight <= 2.5 && air.width() > eleright) {
                    left = that.$eventele.offset().left - air.width();
                    top = that.$eventele.offset().top;
                }
                else {
                    left = that.$eventele.offset().left + that.$eventele.width();
                    top = that.$eventele.offset().top;
                }
                air.css({ left: left, top: top });

                air.show();
            },
            function () {
                air.hide();
            }
        );
    }
    var abf = function () { }
    abf.prototype.init = function (id, option) {
        this.$host = $('#' + id);
        this.render(option);
        _bind(this);
        return this;
    }
    abf.prototype.render = function (config) {
        $.extend(option, config);
        if (option.useicon) {
            var icon = $('<img src="" />');
            icon.attr('src', option.iconsrc);
            icon.attr('width', option.iconsize);
            icon.css({ cursor: option.cursor, position: 'absolute' });
            if (!option.show) {
                icon.css('display', 'none');
            }
            var left, top;
            switch (option.relpos) {
                case "top":
                    left = this.$host.offset().left + (this.$host.width() - option.iconsize) / 2 + option.offsetleft;
                    top = this.$host.offset().top - option.iconsize + option.offsettop;
                    break;
                case "right":
                    left = this.$host.offset().left + this.$host.width() + option.offsetleft;
                    top = this.$host.offset().top + option.offsettop;
                    break;
                case "bottom":
                    left = this.$host.offset().left + (this.$host.width() - option.iconsize) / 2 + option.offsetleft;
                    top = this.$host.offset().top + this.$host.height() + option.offsettop;
                    break;
                case "left":
                    left = this.$host.offset().left - option.iconsize + option.offsetleft;
                    top = this.$host.offset().top + option.offsettop;
                    break;
            }
            icon.css({ left: left, top: top });
            this.$eventele = icon;
            this.$eventele.insertAfter(this.$host);
        }
        else {
            this.$eventele = this.$host;
        }
    }
    abf.prototype.show = function () {
        this.$eventele !== this.$host && this.$eventele.show();
    }
    abf.prototype.hide = function () {
        this.$eventele !== this.$host && this.$eventele.hide();
    }
    return abf;
})()