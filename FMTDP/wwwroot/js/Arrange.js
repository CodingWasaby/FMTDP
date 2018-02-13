function ArrangeReady() {
    sd.push(spatialDim('#cinema', paramChange_Arrange, true));
    sd.push(spatialDim('#city', paramChange_Arrange));
    sd.push(spatialDim('#regional', paramChange_Arrange));
    sd.push(spatialDim('#system', paramChange_Arrange));
    td.push(timeDim('#task', paramChange_Arrange, true));
    td.push(timeDim('#month', paramChange_Arrange));
    ad.push(arangeType('#YQ', paramChange_Arrange));
    ad.push(arangeType('#YQ_CC', paramChange_Arrange));
    ad.push(arangeType('#YQ_CC_TP', paramChange_Arrange));
    initLine_Arrange(lines, '#rateInfo');
    $('#dataRange').select2();
    $('#dataRange').on('select2:select', function (e) {
        paramChange_Arrange(true);
    });
}
function paramChange_Arrange(isPageChange) {
    $.ajax({
        url: "/Arrange/GetRateByDim",
        async: true,
        type: "POST",
        data: {
            spatialDim: $('.spatial:checked')[0].id,
            timeDim: $('.time:checked')[0].id,
            arrangeRateType: $('.dataType:checked')[0].id,
            pages: isPageChange ? $('#dataRange').val() : ''
        },
        traditional: true,
        success: function (result) {
            initLine_Arrange(result, '#rateInfo');
            if (!isPageChange) {
                resetSelect(result.pageInfo, '#dataRange');
            }
        }
    });
}
function initLine_Arrange(ln, divId) {
    console.log(ln);
    var col = [];
    for (var i = 0; i < ln.lines.length; i++) {
        var c = [];
        c.push(ln.lines[i].lineName);
        for (var v = 0; v < ln.lines[i].datas.length; v++) {
            c.push(ln.lines[i].datas[v]);
        }
        col.push(c);
    }
    c3.generate({
        size: {
            height: $('.gray-bg').height() * 0.75
        },
        bindto: divId,
        data: {
            columns: col,
            type: 'area-spline',
        },
        axis: { 
            x: {
                type: 'category',
                categories: ln.categories
            },
            y: {
                label: {
                    text: '上刊率统计'
                }
            }
        },
        oninit: function (e) {
            $('.ibox-content').each(function (index, elem) {
                var h = $(this).children()[1].scrollHeight;
                $(this).height(h + 20);
            })
        }
    });
}