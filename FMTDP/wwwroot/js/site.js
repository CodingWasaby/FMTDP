function toLogin() {
    try {
        window.opener.location.href = "/Login/Index";
    }
    catch (e) {
        window.location.href = "/Login/Index";
    }
}

function toIndex() {
    window.location.href = "/Home/Index";
}

function reloadAllframes() {
    for (var i = 0; i < self.frames.length; i++) {
        self.frames[i].document.location.reload();
    }
}

function MessageShow(title, text, type, callback) {
    swal({
        title: title,
        text: text,
        type: type,
        confirmButtonColor: "#1ab394",
        confirmButtonText: "确定",
        closeOnConfirm: true,
    }, callback);
}


function initCheckBox(id, color, dim, cevent) {
    var data;
    if (dim === 'spatial')
        data = sd;
    else if (dim === 'time')
        data = td;
    else
        data = ad;
    var elem = document.querySelector(id);
    var sw = new Switchery(elem, { color: color, size: 'small', className: 'switchery rateleft-my' });
    elem.onchange = function () {
        if (elem.checked) {
            sw.disable();
            for (var i = 0; i < data.length; i++) {
                if (data[i].element.id !== elem.id) {
                    data[i].enable();
                    if (data[i].element.checked) {
                        data[i].element.click();
                    }
                }
            }
            if (cevent) {
                cevent();
            }
        }
    }
    return sw;
}
function spatialDim(id, cevent, dis) {
    var sw = initCheckBox(id, '#1Aa394', 'spatial', cevent);
    if (dis)
        sw.disable();
    return sw;
}
function timeDim(id, cevent, dis) {
    var sw = initCheckBox(id, '#7c8bc7', 'time', cevent);
    if (dis)
        sw.disable();
    return sw;
}
function arangeType(id, cevent, dis) {
    var sw = initCheckBox(id, '#ff6a00', 'dataType', cevent);
    if (dis)
        sw.disable();
    return sw;
}


