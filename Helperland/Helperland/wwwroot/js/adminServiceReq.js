

const SRDatatable = $("#adminServiceReqTable").DataTable({
    dom: 't<"table-bottom paging d-flex justify-content-between"<"table-bottom-inner d-flex"li>p>',
    responsive: true,
    retrieve: true,
    serverSide: true,
    processing: true,

    ajax: {
        url: "/Admin/GetServiceRequests",
        type: "POST",
        dataType: "json",
        data: function (filterObj) {
            filterObj.serviceId = $("#admin-serviceid").val();
            filterObj.customerName = $("#admin-sr-form-customer").val();
            filterObj.spName = $("#admin-sr-form-sp").val();
            filterObj.fromDate = $("#admin-sr-fdate").val();
            filterObj.toDate = $("#admin-sr-tdate").val();
            filterObj.status = $("#admin-sr-status").val();
            
        },
    },
    "columnDefs": [{
        "targets": [0],
        "visible": false,
        "searchable": false
    }],
    "columns": [
        { "data": "serviceId", "name": "ServiceId" },

        {
            "data": {},
            "name": "Date",
            "render": (data, now) => {
                return `<div>
                            <span><img src="/img/upcomingService/calendar2.png" alt=""></span>
                            <span class="upcoming-date"><b>`+ data.serviceStartDate + `</b></span>
                        </div>
                        <div>
                            <span><img src="/img/upcomingService/timer.png" alt=""></span> <span class="upcoming-time">`+ data.startTime + `-` + data.endTime + `</span>
                        </div>`;
            }
        },

        {
            "data": {},
            "name": "CustomerName",
            "render": (data, row) => {
                return `<div>` + data.customerName + `</div>
                        <div><span><img src="/img/upcomingService/home.png" alt=""></span> <span class="upcoming-address">`+ data.customerAddress + `</span> </div>`;
            }
        },

        {
            "data": {},
            "name": "ServiceProvider",
            "render": (data, row) => {

                if (data.spName != null) {

                    var rate = Math.ceil(parseFloat(data.rating) * 2)
                    var ratings = '';
                    for (var i = 0; i < rate; i++) {
                        ratings += '<span class="stars yellowStars"></span>';
                    }

                    for (var i = rate; i < 10; i++) {
                        ratings += '<span class="stars"></span>';
                    }


                    return `<div class="customer-sh-SP">
                            <div>
                                <span class="cap-span"><img id="ratingModalSPAvtar" src="/img/customer-serviceHistory/cap.png" class="cap" alt=""></span>
                            </div>
                            <div class="sp-detail">
                                <p class="SP-name">`+ data.spName + `</p>
                                <div class="d-flex">
                                    <div class="spStars">`+ ratings + `</div>
                                    <div><span class="ms-2 SP-stars" >`+ data.rating + `</span></div></div></div ></div >`;
                }
                else {
                    return '';
                }
            }
        },

        {
            "data": {},
            "name": "Status",
            "render": (data, row) => {
                if (data.status == 1) {
                    return `<p style="background-color: #FF6B6B; width:fit-content; margin:0 auto; color: white; padding: 6px 10px;">Cancelled</p>`;
                }
                else if (data.status == 0) {
                    return `<p style="background-color: green; width:fit-content; margin:0 auto; color: white; padding: 6px 10px;">Completed</p>`;
                }
                else if (data.status == 2 && data.spName != null) {
                    return `<p style="background-color: #1fb6ff ; width:fit-content; margin:0 auto; color: white; padding: 6px 10px;">Pending</p>`;
                }
                else if (data.status == 2 && data.spName == null) {
                    return `<p style="background-color: #f2bb37  ; width:fit-content; margin:0 auto; color: white; padding: 6px 10px;">New</p>`;
                }
            }
        },

        {
            "data": {},
            "render": function (data, row) {
                return `<div class="dropdown text-center">
                    <button class="admin-table-actionbtn" type="button" id = "dropdownMenuButton`+ data.userId + `"
                data-bs-toggle="dropdown" aria-expanded="false">
                    <i class="fa fa-ellipsis-v" aria-hidden="true" style="color:#646464" class="text-center"></i>
                            </button>
    <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton`+ data.userId + `">
        <li><a class="dropdown-item" href="#">Action</a></li>
        <li><a class="dropdown-item" href="#">Another action</a></li>
        <li><a class="dropdown-item" href="#">Something else here</a></li>
    </ul>
                        </div>`;
            }
        },
    ],


    pagingType: "full_numbers",
    language: {
        paginate: {

            previous: '<img  src="/img/pagination-left.png" alt="previous" />',
            next: '<img src="/img/pagination-left.png" alt="next" style="transform: rotate(180deg)" />',
        },
        info: "Total Record: _MAX_",
        lengthMenu: "Show_MENU_Entries",
    },
    buttons: ["excel"],
    columnDefs: [{ orderable: false, targets: 5 }],

});

document.getElementById("admin-um-form-searchbtn").addEventListener("click", function () {
    SRDatatable.ajax.reload();
});

