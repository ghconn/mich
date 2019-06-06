<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fupd.aspx.cs" Inherits="wes.aspx.fupd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/script/jquery-1.10.2.min.js"></script>
    <script>
        //todo:
        $.ajax({
            url: "/xxx.ashx",
            type: 'POST',
            cache: false,
            data: new FormData($('#form2')[0]),
            processData: false,
            contentType: false,
            beforeSend: function () {
            },
            success: function (data) {

            }
        });
    </script>
</head>
<body>
    <form id="form2" enctype="multipart/form-data" method="post" onsubmit="return false">
        <li class="pb5 pt5">
            <label class="tit flleft" style="margin-top: 5px;">选择文件：</label>
            <input id="selectImg1" name="selectImg1" accept="image/gif,image/jpeg" multiple="multiple" type="file" />
        </li>
    </form>
</body>
</html>
