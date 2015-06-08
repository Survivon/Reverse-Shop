$(document).ready(function () {
    $("#uploadify").uploadify({
        'uploader': '/Scripts/uploadify.swf',
        'script': '/Home/UploadFile/' + $('#img_name').val(),
        'cancelImg': '/Content/cancel.png',
        'fileDesc': 'Только файлы в формате jpg или jpeg',
        'auto': true,
        'multi': false,
        'fileExt': '*.jpg;*.jpeg;',
        'width': '32',
        'height': '32',
        'queueSizeLimit': '1',
        'buttonImg': '/Content/add.png',
        'onComplete': function (event, queueID, fileObj, response, data) {
            var timestamp = new Date().getTime();
            $('#my_image').attr("src", response + '?' + timestamp);
            $('#my_image').width(200).height(200);
        }
    });
});