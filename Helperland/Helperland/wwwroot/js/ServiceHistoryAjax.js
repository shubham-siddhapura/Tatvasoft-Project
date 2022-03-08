
function getSPServiceHistory() {

    $.ajax({
        type: 'GET',
        url: '/ServicePro/GetServiceHistory',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        
        success: function (result) {
            if (result == false) {
                alert("something went wrong");
            }
            else {
                $("#SPServiceHistoryTable tbody").empty();
                for (var i = 0; i < result.length; i++) {
                    $("#SPServiceHistoryTable tbody").append('<tr> <td>' + result[i].serviceId + '</td><td><div><span><img src="/img/upcomingService/calendar2.png" alt=""></span><span class="upcoming-date"><b>' + result[i].serviceStartDate + '</b></span></div><div><span><img src="/img/upcomingService/timer.png" alt=""></span> <span class="upcoming-time">' + result[i].serviceStartTime + '-'+result[i].serviceEndTime+'</span></div></td><td><div>'+result[i].customerName+'</div><div><span><img src="/img/upcomingService/home.png" alt=""></span> <span class="upcoming-address">'+result[i].customerAddress+'</span> </div></td></tr>');
                }
            }
        },
        error: function () {
            alert("error");
        }
    });

}
