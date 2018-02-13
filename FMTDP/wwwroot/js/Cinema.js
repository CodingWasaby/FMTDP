function onSearchClick() {
    var cinemaCode = $('#cinemaCode').val();
    var cinemaName = $('#cinemaName').val();
    var city = $('#city').val();
    var regional = $('#regional').val();
    var system = $('#system').val();
    var cinemaLine = $('#cinemaLine').val();

    var url = "/Cinema?";
    if (cinemaCode.length > 0) {
        url += "cinemaCode=" + cinemaCode + "&";
    }
    if (cinemaName.length > 0) {
        url += "cinemaName=" + cinemaName + "&";
    }
    if (city.length > 0) {
        url += "city=" + city + "&";
    }
    if (regional.length > 0) {
        url += "regional=" + regional + "&";
    }
    if (system.length > 0) {
        url += "system=" + system + "&";
    }
    if (cinemaLine.length > 0) {
        url += "cinemaLine=" + cinemaLine + "";
    }
    window.location.href = url;
}