
window.onkeydown = function (evt) {
    evt = (evt) ? evt : window.event;
    if (evt.keyCode) {
        if (evt.keyCode === 13) {
            $('#sumbit').click();
        }
    }
}

function loginSumbit() {
    if (validateInput()) {
        swal({
            title: "正在验证...",
            text: "",
            confirmButtonColor: "#1ab394",
            confirmButtonText: "确定",
        });
        $.ajax({
            url: "/Login/LoginSumbit",
            type: "POST",
            data: {
                userName: $("#userName").val(),
                password: $("#password").val()
            },
            traditional: true,
            success: function (result) {
                if (result === "true") {
                    MessageShow("登陆成功", "欢迎登陆FSS系统", "success", toIndex);
                }
                else {
                    MessageShow("验证失败", result, "error");
                }
            }
        });
    }
}

function loginOut() {
    $.ajax({
        url: "/Login/LoginOut",
        type: "POST",
        data: {
            userID: $("#userid").val()
        },
        traditional: true,
        success: function (result) {
            toLogin();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            toLogin();
        }
    });
}

function sendMail_Rgister() {
    if (validateInput()) {
        swal({
            title: "正在验证...",
            text: "",
            confirmButtonColor: "#1ab394",
            confirmButtonText: "确定",
        });
        $.ajax({
            url: "/Login/RegisteSumbit",
            type: "POST",
            data: {
                userName: $("#userName").val(),
                password: $("#password").val()
            },
            traditional: true,
            success: function (result) {
                if (result === "true") {
                    MessageShow("验证成功", "审核通过后会发送邮件至您的工作邮箱。", "success");
                }
                else {
                    MessageShow("验证失败", result, "error");
                }
            }
        });
    }
}

function validateInput() {
    var doms = $("[required='required']");
    for (var i = 0; i < doms.length; i++) {
        doms[i].parentElement.setAttribute("class", "form-group");
        if (doms[i].textLength === 0) {
            doms[i].parentElement.setAttribute("class", "form-group has-error");
            return false;
        }
    }
    return true;
}



