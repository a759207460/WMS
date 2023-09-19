function InitUpload(url) {
    $("#f_upload").fileinput({
        language: 'zh',//设置语言
        uploadUrl: url,//上传的地址
        allowedFileExtensions: ["xls", "xlsx"],//接收的文件后缀
        dropZoneTitle: '可以将文件拖放到这里',
        uploadAsync: true, //默认异步上传
        showPreview: true,//是否显示预览
        showUpload: true,//是否显示上传按钮
        showRemove: true, //显示移除按钮
        showCancel: true,   //是否显示文件上传取消按钮。默认为true。只有在AJAX上传过程中，才会启用和显示
        showCaption: true,//是否显示文件标题，默认为true
        browseClass: "btn btn-primary", //文件选择器/浏览按钮的CSS类。默认为btn btn-primary
        dropZoneEnabled: true,//是否显示拖拽区域
        maxFileSize: 0,//最大上传文件数限制，单位为kb，如果为0表示不限制文件大小
        minFileCount: 1, //每次上传允许的最少文件数。如果设置为0，则表示文件数是可选的。默认为0
        maxFileCount: 1, //每次上传允许的最大文件数。如果设置为0，则表示允许的文件数是无限制的。默认为0
        previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",//当检测到用于预览的不可读文件类型时，将在每个预览文件缩略图中显示的图标。默认为<i class="glyphicon glyphicon-file"></i>
        previewFileIconSettings: {
            'docx': '<i ass="fa fa-file-word-o text-primary"></i>',
            'xlsx': '<i class="fa fa-file-excel-o text-success"></i>',
            'xls': '<i class="fa fa-file-excel-o text-success"></i>',
            'pptx': '<i class="fa fa-file-powerpoint-o text-danger"></i>',
            'jpg': '<i class="fa fa-file-photo-o text-warning"></i>',
            'pdf': '<i class="fa fa-file-archive-o text-muted"></i>',
            'zip': '<i class="fa fa-file-archive-o text-muted"></i>',
        },
        msgFilesTooMany: "选择上传的文件数量({n}) 超过允许的最大数值{m}！",//字符串，当文件数超过设置的最大计数时显示的消息 maxFileCount。默认为：选择上传的文件数（{n}）超出了允许的最大限制{m}。请重试您的上传！
        elErrorContainer: '#kartik-file-errors'
    }).on("fileuploaded", function (event, data, previewId, index) {
        console.log("fileuploaded");
        console.log("event" + event);
        console.log("data" + data);
        console.log("previewId" + previewId);
        console.log("index" + index);
    }).on('fileerror', function (event, data, msg) {
        
    });
}