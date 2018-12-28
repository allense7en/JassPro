var User = {
    uploadcover: function () {
        var _img = $('input[name="MainImage"]').val();
        if (_img ='') {
            $('#error_div').html('<span class="text-left eng-error">提交失败:图片不能为空。</span>');
            $('.error_border').show();
            return false;
        }
        $('.error_border').hide();
        $('#imgChangeBtn').html('提交中,请稍后...');
        $('#imgChangeBtn').click(function () {
            alert("0");
            var srcStr = $('.imgBox1').find('img').find('src').val();
            $.ajax({
                type: "POST",
                url: "/UserManager/UpdateUserCover",
                dataType: "json,html",
                contentType: "application/json;charset=utf-8",
                data: {
                    srcStr: srcStr
                },
                success: function (data) {
                    var name = data.name;
                    $('.dropdown profile-element').find('img').attr('src', name);
                },
                error: function (err) {
                    alert("修改失败！");
                }
            });


        });
        return true;
    }



}

//function updateusercover () {

//    $('#imgChangeBtn').click(function () {
//        alert("0");
//        var srcStr = $('.imgBox1').find('img').find('src').val();        
//        $.ajax({
//            type: "POST",
//            url: "/UserManager/UpdateUserCover",
//            dataType: "json,html",
//            contentType: "application/json;charset=utf-8",
//            data: {
//                srcStr: srcStr
//            },
//            success: function (data) {
//                var name = data.name;
//                $('.dropdown profile-element').find('img').attr('src', name);
//            },
//            error: function (err) {
//                alert("修改失败！");
//            }
//        });


//    });
//}
