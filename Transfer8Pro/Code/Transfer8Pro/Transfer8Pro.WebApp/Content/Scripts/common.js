function getquerystring(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return (r[2]); return null;
}

function dateFormat(date, withtime) {
    if (date == null || date == undefined || date == "0001-01-01 00:00:00") {
        return "";
    }

    if ($.browser.version <= 8) {
        return date.split('T')[0];
    }
    else {      
        var d = new Date(date.replace(/-/g,"/"));
        if (d.getFullYear() == 4000) {
            return "永不过期";
        }
        if (withtime == 1 || withtime == undefined) {
            return d.getFullYear() + "-" + (d.getMonth() + 1 >= 10 ? d.getMonth() + 1 : "0" + (d.getMonth() + 1)) + "-" + (d.getDate() >= 10 ? d.getDate() : "0" + d.getDate()) + " " + (d.getHours() >= 10 ? d.getHours() : "0" + d.getHours()) + ":" + (d.getMinutes() >= 10 ? d.getMinutes() : "0" + d.getMinutes()) + ":" + (d.getSeconds() >= 10 ? d.getSeconds() : "0" + d.getSeconds());
        }
        else {
            return d.getFullYear() + "-" + (d.getMonth() + 1 >= 10 ? d.getMonth() + 1 : "0" + (d.getMonth() + 1)) + "-" + (d.getDate() >= 10 ? d.getDate() : "0" + d.getDate());
        }
    }
}