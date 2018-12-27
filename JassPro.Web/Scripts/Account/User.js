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
        return true;
    }
}