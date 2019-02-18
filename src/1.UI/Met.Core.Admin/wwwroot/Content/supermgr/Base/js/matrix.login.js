//登录
$("#login-btn").click(function () {
    var uname = $("#username").val();
    var pwd = $("#password").val();
    var reg = /^[0-9a-zA-Z\-]+$/;

    if (uname.length == 0) {
        $("#msg").show().html("请填写用户名！");
        return;
    } else if (!uname.match(reg)) {
        $("#msg").show().html("用户名，只能输入数字、字母、-");
        return;
    } else if (pwd.length == 0) {

        $("#msg").show().text("请填写帐号密码！");
        return;
    }
    $btnsubmit = $(this);
    $("#msg").hide().html("");
    $.ajax({
        type: "POST",
        url: "/Account/Login?r=" + Math.random(),
        data: { 'LoginName': uname, 'Password': pwd },
        success: function (res) {
            if (res.code === 1) {
                window.location.href = "/Home/Index";
            }
            else {
                $("#msg").show().html(data.msg);
                return;
            }
        },
        error: function () {
            $("#msg").show().text("登录超时!");
        }
    });
});
//安全退出
function loginOut() {
    $.fn.modalConfirm("注：您确定要安全退出本次登录吗？", function (data) {
        if (data) {
            $.ajax({
                type: "POST",
                url: "/Account/OutLogin?r=" + Math.random(),
                data: {},
                success: function (data) {
                    window.location.href = "/Account/Login";
                },
                error: function () {
                }
            });
        }
    });

}