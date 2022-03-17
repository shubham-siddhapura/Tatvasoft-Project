

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

                var listItems = `<li><a class="dropdown-item srEdit" data-value="` + data.serviceId + `" role="button">Edit & Reschedule</a></li>
        <li><a class="dropdown-item srCancel" data-value="`+ data.serviceId +`" role="button">Cancel</a></li>`;

                if (data.status == 0) {
                    listItems = `<li class="p-2 ps-3 pe-3">Service request is already Completed</li>`;
                }
                else if (data.status == 1) {
                    listItems = `<li class="p-2 ps-3 pe-3">Service request is Cancelled</li>`;
                }

                return `<div class="dropdown text-center">
                    <button class="admin-table-actionbtn" type="button" id = "dropdownMenuButton`+ data.userId + `"
                data-bs-toggle="dropdown" aria-expanded="false">
                    <i class="fa fa-ellipsis-v" aria-hidden="true" style="color:#646464" class="text-center"></i>
                            </button>
    <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton`+ data.userId + `">
        `+ listItems +`
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

/*================ Edit Service Request ================*/

$("#adminServiceReqTable").on("click", ".srEdit", function (e) {

    console.log(e.target.dataset.value);

    getServiceDetails(e.target.dataset.value);

});

function getServiceDetails(serviceId) {
    var data = {};
    data.serviceRequestId = serviceId;
    
    $.ajax({
        type: 'GET',
        url: '/Admin/GetServiceDetails',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: data,
        success: function (result) {
            console.log(result);
            if (result != "false") {
                putToModal(result);
            }
            else {
                alert("something went wrong");
            }
        },
        error: function () {
            alert("error");
        }
    });
}

function putToModal(result) {
    $("#admin-sr-mdate").attr("type","date");
    $("#admin-sr-mdate").val(result.serviceStartDate.split("T")[0]);
    $("#modal-time").val(result.serviceStartDate.split("T")[1])

    $("#modal-street").val(result.serviceRequestAddresses[0].addressLine2);

    $("#modal-house").val(result.serviceRequestAddresses[0].addressLine1);

    $("#modal-postal").val(result.serviceRequestAddresses[0].postalCode);

    $("#modal-city").val(result.serviceRequestAddresses[0].city);


    $("#modal-street-invoice").val(result.serviceRequestAddresses[0].addressLine2);

    $("#modal-house-invoice").val(result.serviceRequestAddresses[0].addressLine1);

    $("#modal-postal-invoice").val(result.serviceRequestAddresses[0].postalCode);

    $("#modal-city-invoice").val(result.serviceRequestAddresses[0].city);

    $("#adminSRIdModal").val(result.serviceRequestId);
   
    $("#admin-sr-edit-modal").modal("show");
}

document.getElementById("admin-sr-updatebtn").addEventListener("click", function () {
    updateServiceRequest();
});

function updateServiceRequest() {

    var date = $("#admin-sr-mdate").val() + "T" + $("#modal-time").val();
    console.log(date);

    var data = {
        serviceRequestAddresses: [
            {},
            {}
        ],
    };

    data.serviceRequestId = $("#adminSRIdModal").val();
    

    data.serviceStartDate = date;

    data.serviceRequestAddresses[0].addressLine2 = $("#modal-street").val().trim();

    data.serviceRequestAddresses[0].addressLine1 = $("#modal-house").val().trim();

    data.serviceRequestAddresses[0].postalCode = $("#modal-postal").val().trim();

    data.serviceRequestAddresses[0].city = $("#modal-city").val().trim();


    data.serviceRequestAddresses[1].addressLine2 = $("#modal-street-invoice").val().trim();

    data.serviceRequestAddresses[1].addressLine1 = $("#modal-house-invoice").val().trim();

    data.serviceRequestAddresses[1].postalCode = $("#modal-postal-invoice").val().trim();

    data.serviceRequestAddresses[1].city = $("#modal-city-invoice").val().trim();

    data.comments = $("#modal-why-edit").val().trim();

    var todayDate = new Date();
    var srDate = new Date($("#admin-sr-mdate").val())
    console.log(" date = " + srDate);
    console.log(" today = " + todayDate);

    if ($("#admin-sr-mdate").val() == null) {
        $("#editModalAlert").removeClass("d-none").text("Date can not be empty!");
        $("#admin-sr-mdate").focus();
    }
    else if (srDate <= todayDate) {
        $("#editModalAlert").removeClass("d-none").text("Please choose date wisely!");
        $("#admin-sr-mdate").focus();
    }
    else if ($("#modal-house").val().trim() == "") {
        $("#editModalAlert").removeClass("d-none").text("House number can not empty!");
        $("#modal-house").focus();
    }
    else if ($("#modal-street").val().trim() == "") {
        $("#editModalAlert").removeClass("d-none").text("Street can not empty!");
        $("#modal-street").focus();
    }
    else if ($("#modal-postal").val().trim() == "") {
        $("#editModalAlert").removeClass("d-none").text("Postalcode can not empty!");
        $("#modal-postal").focus();
    }
    else if ($("#modal-city").val().trim() == "") {
        $("#editModalAlert").removeClass("d-none").text("City can not empty!");
        $("#modal-city").focus();
    }
    else if (data.serviceRequestAddresses[1].addressLine1 == "") {
        $("#editModalAlert").removeClass("d-none").text("House number can not empty!");
        $("#modal-house-invoice").focus();
    }
    else if ($("#modal-street-invoice").val().trim() == "") {
        $("#editModalAlert").removeClass("d-none").text("Street can not empty!");
        $("#modal-street-invoice").focus();
    }
    else if ($("#modal-postal-invoice").val().trim() == "") {
        $("#editModalAlert").removeClass("d-none").text("Postalcode can not empty!");
        $("#modal-postal-invoice").focus();
    }
    else if ($("#modal-city-invoice").val().trim() == "") {
        $("#editModalAlert").removeClass("d-none").text("City can not empty!");
        $("#modal-city-invoice").focus();
    }
    else if ($("#modal-why-edit").val().trim() == "") {
        $("#editModalAlert").removeClass("d-none").text("You must say, why do you want to edit request?");
        $("#modal-why-edit").focus();
    }
    else {
        $.ajax({
            type: 'POST',
            url: '/Admin/EditServiceRequest',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: data,
            success: function (result) {
                console.log(result);
                if (result == "conflict"){
                    $("#editModalAlert").removeClass("d-none").text("Your Service Provider Already Booked for this date, please change date!");
                    $("#admin-sr-mdate").focus();
                }
                else if (result == "true") {
                    SRDatatable.ajax.reload();
                    $("#admin-sr-edit-modal").modal("hide");
                }
                else  {
                    alert("something went wrong");
                }
            },
            error: function () {
                alert("error");
            }
        });
    }
}

/*================ Cancel Service Request=================*/

$("#adminServiceReqTable").on("click", ".srCancel", function (e) {

    console.log(e.target.dataset.value);

    cancelServiceRequest(e.target.dataset.value);

});

function cancelServiceRequest(serviceId) {
    var data = {};
    data.serviceRequestId = serviceId;

    $.ajax({
        type: 'POST',
        url: '/Admin/ChangeUserStatus',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: data,
        success: function (result) {
            console.log(result);
            if (result.value = "true") {
                SRDatatable.ajax.reload();
            }
            else {
                alert("something went wrong");
            }
        },
        error: function () {
            alert("error");
        }
    });
}