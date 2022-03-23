
function dataTableSPHistory() {
    var spHistoryDT = $("#SPServiceHistoryTable").DataTable({
        dom: 't<"table-bottom d-flex justify-content-between paging"<"table-bottom-inner d-flex "li>p>',
        responsive: true,
        retrieve: true,
        pagingType: "full_numbers",
        language: {
            paginate: {
                first: "<img src='/img/pagination-first.png' alt='first'/>",
                previous: "<img src='/img/pagination-left.png' alt='previous' />",
                next: '<img src="/img/pagination-left.png" alt="next" style="transform: rotate(180deg)" />',
                last: "<img src='/img/pagination-first.png' alt='first' style='transform: rotate(180deg) ' />",
            },
            info: "Total Record: _MAX_",
            lengthMenu: "Show_MENU_Entries",
        },
        buttons: ["excel"],
        "ordering": true,
        "order": [0, 'desc'],

        columnDefs: [{ orderable: false, targets: 2 }],
    });
}

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
                dataTableSPHistory();
            }
        },
        error: function () {
            alert("error");
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
