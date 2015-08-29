$(document).ready(function() {
    $("#mainContainer").animate({
        opacity: 1.0
    }, 250);

    $(".task_datetime_readable").each(function() {
        if ($(this).text() != "") {
            var datetime = new Date($(this).text());
            datetime = moment(datetime).format("ddd MMM D, YYYY  <br/> &#64; h:mm A");
            $(this).html(datetime);
        }
    });

    $(".form_datetime").each(function() {
        if ($(this).val() != "") {
            var datetime = new Date($(this).val());
            datetime = moment(datetime).format("YYYY-MM-DD HH:mm:00");
            $(this).val(datetime);
        }
    });

    $(".form_date").each(function() {
        if ($(this).val() != "") {
            var date = new Date($(this).val());
            date = moment(date).format("YYYY-MM-DD");
            $(this).val(date);
        }
    });

    $(".form_time").each(function() {
        if ($(this).val() != "") {
            var time = new Date($(this).val());
            time = moment(time).format("HH:mm:00");
            $(this).val(time);
        }
    });

    $(".class_date_readable").each(function() {
        if ($(this).text() != "") {
            var datetime = new Date($(this).text());
            datetime = moment(datetime).format("ddd MMM D, YYYY");
            $(this).html(datetime);
        }
    });

    $(".class_time_readable").each(function() {
        if ($(this).text() != "") {
            var datetime = new Date($(this).text());
            datetime = moment(datetime).format("h:mm A");
            $(this).html(datetime);
        }
    });

    $(".event_datetime_readable").each(function() {
        if ($(this).text() != "") {
            var datetime = new Date($(this).text());
            datetime = moment(datetime).format("ddd MMM D, YYYY  <br/> &#64; h:mm A");
            $(this).html(datetime);
        }
    });

    $(".user_timezone").each(function() {
        var timezone = moment(new Date()).format("ZZ");
        $(this).text(timezone);
    });
    $(".bodyContainer").animate({
        opacity: 1.0
    }, 500);
});

var code = function() {
    $(".datetimepicker_date").datetimepicker({
        format: "yyyy-mm-dd",
        weekStart: 0,
        minView: 2,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        forceParse: 0,
        showMeridian: 1,
        pickerPosition: "bottom-right"
    });

    $(".datetimepicker_datetime").datetimepicker({
        format: "yyyy-mm-dd hh:ii:00",
        weekStart: 0,
        minView: 0,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        forceParse: 0,
        showMeridian: 1,
        pickerPosition: "top-right"
    });

    $(".datetimepicker_time").datetimepicker({
        format: "hh:ii:00",
        weekStart: 0,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 1,
        minView: 0,
        forceParse: 0,
        showMeridian: 1,
        pickerPosition: "bottom-right"
    });
};

$(document).ready(function() {
    code();
});

$(document).change(function() {
    code();
});

$("#addRow").click(function() {
    code();
});