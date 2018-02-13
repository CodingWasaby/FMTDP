
var doughnutOptions = {
    segmentShowStroke: true,
    segmentStrokeColor: "#fff",
    segmentStrokeWidth: 8,
    percentageInnerCutout: 45, // This is 0 for Pie charts
    animationSteps: 100,
    animationEasing: "easeOutBounce",
    animateRotate: true,
    animateScale: false
};
function planTypeViewReady() {
    initChart(yqData, "yqchart");
    initChart(tpData, "tpchart");
    initChart(ccData, "ccchart");
    initChart(qdData, "qdchart");
}
function initChart(summaryData, canvasID) {
    var chartData = [];
    for (var i = 0; i < summaryData.length; i++) {
        var c = {
            value: summaryData[i].percent,
            color: getChartPartColor(summaryData[i].name),
            highlight: "#1ab394",
            label: summaryData[i].name
        }
        chartData.push(c);
    }
    var ctx = document.getElementById(canvasID).getContext("2d");
    var DoughnutChart = new Chart(ctx).Doughnut(chartData, doughnutOptions);
}
function getChartPartColor(str) {
    if (str == "预定") {
        return "#dedede";
    }
    if (str == "保留") {
        return "#bce18a";
    }
    if (str == "销售") {
        return "#a3e1d4";
    }
    if (str == "特批执行") {
        return "#A4CEE8";
    }
}