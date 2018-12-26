var Login = {
    login: function () {
        var userName = $.trim($("#username").val()),
            password = $.trim($("#password").val());
        if (!userName) {
            $('#error_div').html('<span class="text-left eng-error">登录失败:用户名不能为空。</span>');
            $('.error_border').show();
            $("#username").focus();
            return false;
        }

        if (!password) {
            $('#error_div').html('<span class="text-left eng-error">登录失败:密码不能为空。</span>');
            $('.error_border').show();
            $("#password").focus();
            return false;
        }
        $('.error_border').hide();
        $('#submitForm').html('登陆中,请稍后...');
        return true;
    },
    doLogin: function () {
        if (this.login())
            document.loginForm.submit();
    },
    doLogin2: function () {
        if (this.login2())
            document.loginFormPhone.submit();
    },
    login2: function () {
        var userName = $.trim($("#username").val()),
            password = $.trim($("#password").val());
        if (!userName) {
            $('#error_divPhone').html('<span class="text-left eng-error">登录失败:用户名不能为空。</span>');
            $('.error_border').show();
            $("#username").focus();
            return false;
        }

        if (!password) {
            $('#error_divPhone').html('<span class="text-left eng-error">登录失败:密码不能为空。</span>');
            $('.error_border').show();
            $("#password").focus();
            return false;
        }
        $('.error_border').hide();
        $('#submitFormPhone').html('登陆中,请稍后...');
        return true;
    },
};