function upcomingDataTable() {
    const dt = $("#mytable").DataTable({
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
        columnDefs: [{ orderable: false, targets: 4 }],
    });
}

function getSPUpcomingService() {

    $.ajax({
        type: 'GET',
        url: '/ServicePro/GetUpcomingHistory',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',

        success: function (result) {
            if (result == "false") {
                alert("something went wrong");
            }
            else {
                console.log(result);
                $("#mytable tbody").empty();
               
                for (var i = 0; i < result.length; i++) {

                    var cancelBtnClass = "";
                    var completeBtnClass = "background-color: #1d7a8c;";
                    if (result[i].completed == true) {
                        cancelBtnClass = "display:none!important;";
                    }
                    else {
                        completeBtnClass = "display:none!important; ";
                    }
                    
                    $("#mytable tbody").append(
                        `<tr data-value="` + result[i].serviceRequestId +`" data-completed="`+result[i].completed+`">
                <td>
                    `+result[i].serviceRequestId+`
                </td>
                <td>
                    <div>
                        <span><img src="/img/upcomingService/calendar2.png" alt=""></span>
                        <span class="upcoming-date"><b>`+result[i].serviceStartDate+`</b></span>
                    </div>
                    <div>
                        <span><img src="/img/upcomingService/timer.png" alt=""></span> <span class="upcoming-time">`+result[i].startTime+`-`+result[i].endTime+`</span>
                    </div>
                </td>
                <td>
                    <div>`+ result[i].customerName + `</div>
                    <div><span><img src="/img/upcomingService/home.png" alt=""></span> <span class="upcoming-address">`+ result[i].addressLine2+`, `+result[i].addressLine1+`,<br>`+result[i].city+` - `+result[i].postalCode+`</span> </div>
                </td>
                <td class="distance">
                    15 km
                </td>
                <td class="actions">
                    <a href="#" id="upcomingCancelBtn`+ result[i].serviceRequestId + `" style=` + cancelBtnClass + `>Cancel</a>
                    <a href="#" id="upcomingCompleteBtn`+ result[i].serviceRequestId + `" style="` + completeBtnClass +`">Complete</a>
                    

                </td>
            </tr>`
                    );
                }
                upcomingDataTable();
            }
        },
        error: function () {
            alert("error");
        }
    });

}