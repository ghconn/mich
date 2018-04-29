<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="np.aspx.cs" Inherits="wes.aspx.np" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/script/jquery-1.8.2.min.js"></script>

    <script>
        //var obj = [];
        //console.log(["1", "2", "110"].map(parseInt));
        //console.log(["1", "2", "110"].reduce(function (t, c) { return t + c }, "0000"));
        //console.log(["1", "2", "110"].forEach(function (v, i, o) { obj.unshift(v) }));
        //console.log(obj);
        //obj.shift();
        //console.log(obj);
        //obj.shift(2);
        //console.log(obj);

        //var arr = [1, 2, '3'];
        //arr['js'] = 'jquery';
        //arr['css'] = 'oocss';
        //var obj = {};
        //for (var i in arr) {
        //    obj[i] = i;
        //}
        //console.log(obj);
        //console.log(arr);
        //console.log(obj[1]);
        
        // 这是一个普通函数,我们把它用来当做构造函数,也当做一个[父类]
        function Car(name) {
            this.name = name;
        }
        Car.prototype.introduce = function () {
            console.log('[From Car.prototype.introduce] ' + 'Hello, my name is: ' + this.name);
        };
        var car = new Car('porsche');
        console.log(car.name); // porsche
        car.introduce(); // [From Car.prototype.introduce] Hello, my name is: porsche

        // 我们开始构建另外一个函数,我们把这个函数当做一个[子类],暂时这么说.
        function MiniCar(name, color) {
            this.name = name;
            this.color = color;

            this.getColor = function () {
                console.log('My color is: ' + this.color);
            }
        }
        MiniCar.prototype = new Car();

        var miniCar = new MiniCar('benz', 'black');
        console.log('\n');
        console.log('name: ' + miniCar.name + ';color: ' + miniCar.color); // name: benz;color: black
        miniCar.introduce(); // [From Car.prototype.introduce] Hello, my name is: benz
        miniCar.getColor();  // My color is: black

        // 如果使用A表示一个构造函数,那么 (new A()).__proto__ === A.prototype
        console.log((new MiniCar()).__proto__ === MiniCar.prototype); // true

        // 如果使用a表示A的一个示例的话,那么 a.__proto__ === A.prototype
        console.log(miniCar.__proto__ === MiniCar.prototype); // true

        // 一个对象是没有prototype属性的
        console.log(miniCar.prototype === undefined); // true

        console.log("一张网页，要经历怎样的过程，才能抵达用户面前？\n一位新人，要经历怎样的成长，才能站在技术之巅？\n探寻这里的秘密；\n体验这里的挑战；\n成为这里的主人；\n加入百度，加入网页搜索，你，可以影响世界。\n");
        console.log("请将简历发送至 %c ps_recruiter@baidu.com（邮件标题请以“姓名-应聘XX职位-来自console”命名）", "color:green");
        console.log("职位介绍：http://dwz.cn/hr2013");
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
