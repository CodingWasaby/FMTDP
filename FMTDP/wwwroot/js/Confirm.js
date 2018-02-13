
function planTypeViewReady() {
    initChart(yqData, "yqchart");
    initChart(tpData, "tpchart");
    initChart(ccData, "ccchart");
    initChart(qdData, "qdchart");

    $('.ibox-content').each(function (index, elem) {
        var h = $(this).children()[1].scrollHeight;
        $(this).height(h + 20);
    })
}
function initChart(summaryData, divID) {
    var charData = [];
    for (var i = 0; i < summaryData.length; i++) {
        var c = [summaryData[i].name, summaryData[i].value];
        charData.push(c);
    }
    c3.generate({
        bindto: '#' + divID,
        data: {
            columns: charData,
            type: 'pie'
        },
        color: {
            pattern: ['#dedede', '#56e514', '#1ab394', '#1C84C6']
        },
    });
}

function propertyViewReady() {
    initBar(cityInfo, '#cityInfo');
    initBar(regionalInfo, '#regionalInfo');
    initBar(customeSourceInfo, '#customerInfo');
    initBar(systemInfo, "#systemInfo");
    initSalesBar(saleAssistantInfo);
}
function initBar(bd, divId) {
    var col = [];
    for (var i = 0; i < bd.barDatas.length; i++) {
        var c = [];
        c.push(bd.barDatas[i].dataName);
        for (var v = 0; v < bd.barDatas[i].dataDetail.length; v++) {
            c.push(bd.barDatas[i].dataDetail[v]);
        }
        col.push(c);
    }
    c3.generate({
        bindto: divId,
        data: {
            columns: col,
            colors: {
                贴片: '#56e514',
                映前: '#1ab394',
                渠道: '#1C84C6',
                场次: '#BABABA',
                时长总计: '#7c8bc7'
            },
            type: 'bar',
            groups: [
                bd.group, ['时长总计']
            ],
            axes: {
                贴片: 'y',
                映前: 'y',
                渠道: 'y',
                场次: 'y',
                时长总计: 'y2'
            }
        },
        axis: {
            x: {
                type: 'category',
                categories: bd.categories
            },
            y: {
                label: {
                    text: '方案数量',
                    position: 'outer-middle'
                }
            },
            y2: {
                show: true,
                label: {
                    text: '投放总时长（秒）'
                }
            }
        }
    });
}
function initSalesBar(bd) {
    var col = [];
    for (var i = 0; i < bd.barDatas.length; i++) {
        var c = [];
        c.push(bd.barDatas[i].dataName);
        for (var v = 0; v < bd.barDatas[i].dataDetail.length; v++) {
            c.push(bd.barDatas[i].dataDetail[v]);
        }
        col.push(c);
    }
    c3.generate({
        bindto: '#saleassitentInfo',
        size: {
            height: $('.row').height() - 108
        },
        data: {
            columns: col,
            colors: {
                贴片: '#56e514',
                映前: '#1ab394',  
                渠道: '#1C84C6',
                场次: '#BABABA',
                时长总计: '#7c8bc7'
            },
            type: 'bar',
            groups: [
                bd.group, ['时长总计']
            ],
            axes: {
                贴片: 'y',
                映前: 'y',
                渠道: 'y',
                场次: 'y',
                时长总计: 'y2'
            }
        },
        axis: {
            rotated: true,
            x: {
                type: 'category',
                categories: bd.categories
            },
            y: {
                label: {
                    text: '方案数量',
                    position: 'outer-middle'
                }
            },
            y2: {
                show: true,
                label: {
                    text: '投放总时长（秒）'
                },
                tick: {
                    values: [0, 50000, 200000, 500000]
                }
            }
        },
        oninit: function (e) {
            var div = $('#salescon').height($('#saleassitentInfo').height());
        }
    });
}

function rateTrendReady() {
    sd.push(spatialDim('#cinema', paramChange, true));
    sd.push(spatialDim('#city', paramChange));
    sd.push(spatialDim('#regional', paramChange));
    sd.push(spatialDim('#system', paramChange));
    td.push(timeDim('#task', paramChange, true));
    td.push(timeDim('#month', paramChange));
    initLine(lines, '#rateInfo');
    $('#dataRange').select2();
    $('#dataRange').on('select2:select', function (e) {
        paramChange(true);
    });
}
function paramChange(isPageChange) {
    $.ajax({
        url: "/Confirm/GetRateByDim",
        async: true,
        type: "POST",
        data: {
            spatialDim: $('.spatial:checked')[0].id,
            timeDim: $('.time:checked')[0].id,
            pages: isPageChange ? $('#dataRange').val() : ''
        },
        traditional: true,
        success: function (result) {
            initLine(result, '#rateInfo');
            if (!isPageChange) {
                resetSelect(result.pageInfo, '#dataRange');
            }
        }
    });
}
function resetSelect(optiondata, selector) {
    $(selector).select2('destroy');
    $(selector).children().remove();
    for (var i = 0; i < optiondata.length; i++) {
        var data = {
            id: optiondata[i].item1 + '-' + optiondata[i].item2,
            text: optiondata[i].item1 + '-' + optiondata[i].item2
        };
        var newOption = new Option(data.text, data.id, false, false);
        $('#dataRange').append(newOption).trigger('change');
    }
    $(selector).select2();
}
function initLine(ln, divId) {
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
                    text: '预计上刊率'
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

