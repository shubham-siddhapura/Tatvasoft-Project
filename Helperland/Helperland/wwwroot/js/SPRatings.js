function dataTableSPRatings() {
    var spRatingDT = $("#SPRatingsTable").DataTable({
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
        columnDefs: [{ orderable: false, targets: 3 }],
    });
}

function getSPRatings() {

    $.ajax({
        type: 'GET',
        url: '/ServicePro/GetMyRatings',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',

        success: function (result) {
            if (result.value == "false") {

                alert("something went wrong");

            }
            else {
                console.table(result);
                $("#SPRatingsTable tbody").empty();
                for (var i = 0; i < result.length; i++) {

                    var responseDate = result[i].serviceRequest.serviceStartDate.split("T");
                    var dateArray = responseDate[0].split("-");
                    var date = dateArray[2] + "/" + dateArray[1] + "/" + dateArray[0];
                    var startTime = responseDate[1].substring(1, 5);
                    
                    var datetime = new Date(result[i].serviceRequest.serviceStartDate);
                    datetime.setHours(datetime.getHours() + result[i].serviceRequest.subTotal);
                    var endTime = datetime.toISOString().substring(12, 16);

                    var comment = result[i].comments;
                    if (comment == null) {
                        comment = "";
                    }

                    $("#SPRatingsTable tbody").append(
                        `<tr>
                <td>
                    <p>`+ result[i].ratingTo + `</p>
                    <p>`+ result[i].ratingToNavigation.firstName + " " + result[i].ratingToNavigation.lastName +`</p>
                </td>
                <td>
                    <div>
                        <span><img src="img/upcomingService/calendar2.png" alt=""></span>
                        <span class="upcoming-date"><b>`+date+`</b></span>
                    </div>
                    <div>
                        <span><img src="img/upcomingService/timer.png" alt=""></span> <span class="upcoming-time">`+ startTime + `-` + endTime +`</span>
                    </div>
                </td>
                <td>
                    <p><b>Ratings</b></p>

                    <div class="d-flex">
                        <div class="spStars" id="spRating`+result[i].serviceRequestId+`"><span class="stars"></span><span class="stars"></span><span class="stars"></span><span class="stars"></span><span class="stars"></span><span class="stars"></span><span class="stars"></span><span class="stars"></span><span class="stars"></span><span class="stars"></span></div>

                        <div><span class="ms-2">Very Good</span></div>
                    </div>
                </td>

                <td>
                    <p><b>Comments:</b></p>
                    <p>`+comment+`</p>
                </td>

            </tr>`
                    );

                    starAsperRating(result[i].ratings, "#spRating"+result[i].serviceRequestId);
                }

                dataTableSPRatings();
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



/* ======= star as per rating ======= */

function starAsperRating(rate, id) {

    var rate = Math.ceil(rate * 2);

    var starDiv = document.querySelector(id);

    var stars = starDiv.querySelectorAll(".stars");

    for (var i = 0; i < rate; i++) {
        stars[i].classList.add("yellowStars");
    }

    for (var i = rate; i < 10; i++) {
        stars[i].classList.remove("yellowStars");
    }

}