$(function () {
            //��ʼ������֤
            $("#form1").initValidform();

            //��ʼ����Ĭ�ϵ�����
            $.upLoadDefaults = $.upLoadDefaults || {};
            $.upLoadDefaults.property = {
                multiple: false, //�Ƿ���ļ�
                water: false, //�Ƿ��ˮӡ
                thumbnail: false, //�Ƿ���������ͼ
                sendurl: null, //���͵�ַ
                filetypes: "jpg,jpeg,png", //�ļ�����
                filesize: "10240", //�ļ���С
                btntext: "���...", //�ϴ���ť������
                swf: null //SWF�ϴ��ؼ���Ե�ַ
            };
            //��ʼ���ϴ��ؼ�
            $.fn.InitUploader = function (b) {
                var fun = function (parentObj) {
                    var p = $.extend({}, $.upLoadDefaults.property, b || {});
                    var btnObj = $('<div class="upload-btn">' + p.btntext + '</div>').appendTo(parentObj);
                    //��ʼ������
                    p.sendurl += "?action=MultiSizeImg";

                    if (!p.multiple) {
                        p.sendurl += "&DelFilePath=" + parentObj.siblings(".upload-path").val();
                    }

                    //��ʼ��WebUploader
                    var uploader = WebUploader.create({
                        auto: true, //�Զ��ϴ�
                        swf: p.swf, //SWF·��
                        server: p.sendurl, //�ϴ���ַ
                        pick: {
                            id: btnObj,
                            multiple: p.multiple
                        },
                        accept: {
                            /*title: 'Images',*/
                            extensions: p.filetypes
                            /*mimeTypes: 'image/*'*/
                        },
                        formData: {
                            'DelFilePath': '', //�������Ҫɾ�����ļ�·��
                            'ImgSize': '', //�������Ҫ���ɵ�ͼƬ�ߴ�
                        },
                        fileVal: 'Filedata', //�ϴ��������
                        fileSingleSizeLimit: p.filesize * 1024 //�ļ���С
                    });

                    //��validate��ͨ��ʱ���������ʹ����¼�����ʽ֪ͨ
                    uploader.on('error', function (type) {
                        switch (type) {
                            case 'Q_EXCEED_NUM_LIMIT':
                                alert("�����ϴ��ļ��������࣡");
                                break;
                            case 'Q_EXCEED_SIZE_LIMIT':
                                alert("�����ļ��ܴ�С�������ƣ�");
                                break;
                            case 'F_EXCEED_SIZE':
                                alert("�����ļ���С�������ƣ�");
                                break;
                            case 'Q_TYPE_DENIED':
                                alert("���󣺽�ֹ�ϴ��������ļ���");
                                break;
                            case 'F_DUPLICATE':
                                alert("���������ظ��ϴ����ļ���");
                                break;
                            default:
                                alert('������룺' + type);
                                break;
                        }
                    });

                    //�����ļ���ӽ�����ʱ��
                    uploader.on('fileQueued', function (file) {
                        //����ǵ��ļ��ϴ����Ѿɵ��ļ���ַ����ȥ
                        if (!p.multiple) {
                            uploader.options.formData.DelFilePath = $("#hfoldfiles").val();
                        }
                        uploader.options.formData.ImgSize = $('.rule-multi-porp.multi-porp ul li.selected a').text();
                        //��ֹ�ظ�����
                        if (parentObj.children(".upload-progress").length == 0) {
                            //����������
                            var fileProgressObj = $('<div class="upload-progress"></div>').appendTo(parentObj);
                            var progressText = $('<span class="txt">�����ϴ������Ժ�...</span>').appendTo(fileProgressObj);
                            var progressBar = $('<span class="bar"><b></b></span>').appendTo(fileProgressObj);
                            var progressCancel = $('<a class="close" title="ȡ���ϴ�">�ر�</a>').appendTo(fileProgressObj);
                            //�󶨵���¼�
                            progressCancel.click(function () {
                                uploader.cancelFile(file);
                                fileProgressObj.remove();
                            });
                        }
                    });

                    //�ļ��ϴ������д���������ʵʱ��ʾ
                    uploader.on('uploadProgress', function (file, percentage) {
                        var progressObj = parentObj.children(".upload-progress");
                        progressObj.children(".txt").html(file.name);
                        progressObj.find(".bar b").width(percentage * 100 + "%");
                    });

                    //���ļ��ϴ�����ʱ����
                    uploader.on('uploadError', function (file, reason) {
                        uploader.removeFile(file); //�Ӷ������Ƴ�
                        alert(file.name + "�ϴ�ʧ�ܣ�������룺" + reason);
                    });

                    //���ļ��ϴ��ɹ�ʱ����
                    uploader.on('uploadSuccess', function (file, data) {
                        if (data.status == '0') {
                            if (data.myerr && data.myerr == 1) {
                                alert(data.myerrmsg);
                            }
                            else {
                                alert(data.msg);
                                //var progressObj = parentObj.children(".upload-progress");
                                //progressObj.children(".txt").html(data.msg);
                            }
                        }
                        if (data.status == '1') {
                            //����ǵ��ļ��ϴ�����ֵ��Ӧ�ı�
                            if (!p.multiple) {
                                parentObj.siblings(".upload-name").val(data.name);
                                parentObj.siblings(".upload-path").val(data.path);

                                $('#hfoldfiles').val(data.path);

                                parentObj.siblings(".upload-size").val(data.size);
                            } else {
                                addImage(parentObj, data.path, data.thumb);
                            }
                            var progressObj = parentObj.children(".upload-progress");
                            progressObj.children(".txt").html("�ϴ��ɹ���" + file.name);
                        }
                        uploader.removeFile(file); //�Ӷ������Ƴ�

                    });

                    //���ܳɹ�����ʧ�ܣ��ļ��ϴ����ʱ����
                    uploader.on('uploadComplete', function (file) {
                        var progressObj = parentObj.children(".upload-progress");
                        progressObj.children(".txt").html("�ϴ����");
                        //�������Ϊ�գ����Ƴ�������
                        if (uploader.getStats().queueNum == 0) {
                            progressObj.remove();
                        }
                    });
                };
                return $(this).each(function () {
                    fun($(this));
                });
            }

            //��ʼ���ϴ��ؼ�
            $(".upload-img").InitUploader({ sendurl: "/tools/upload_ajax.ashx", swf: "/scripts/webuploader/uploader.swf" });
        });
