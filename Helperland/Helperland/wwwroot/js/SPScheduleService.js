var calendarEl = document.getElementById('calendar');
var calendar;

function addServiceSchedule() {

    $.ajax({
        type: 'GET',
        url: "/ServicePro/GetServiceSchedule",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",

        success: function (result) {
            console.log(result);
            var events = [];
            if (result != "false") {

                for (let i = 0; i < result.length; i++) {
                    var bgColor = "#555";
                    if (result[i].status == 0) {
                        bgColor = "#146371";
                    }

                    console.log("result" + i + " : " + result[i].serviceRequestId);
                    events.push({
                        id: result[i].serviceRequestId,
                        start: result[i].serviceStartDate,
                        title: result[i].startTime + "-" + result[i].endTime,
                        backgroundColor: bgColor,
                        borderColor: "#fff",
                    });
                }

                console.log(events);

                calendar = new FullCalendar.Calendar(calendarEl, {
                    initialView: 'dayGridMonth',
                    headerToolbar: {
                        left: 'prev,next title',
                        center: '',
                        right: ''
                    },
                    events: events,
                    eventClick: function (info) {

                        console.log(info.event.id);
                        serviceRequestId = info.event.id;
                        $("#serviceReqdetailsbtn").click();
                    },
                });
                calendar.render();
            }
            else {
                alert("something went wrong!");
            }
        },
        error: function (error) {
            alert(error);
        },

        beforeSend: function () {
            $("#loadingAnimation").removeClass("d-none");

        },

        complete: function () {
            setTimeout(function () {
                $("#loadingAnimation").addClass("d-none");
            }, 500);
        },
    });
}
