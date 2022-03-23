

    const dt = $("#admin-um-table").DataTable({
        dom: 't<"table-bottom paging d-flex justify-content-between"<"table-bottom-inner d-flex"li>p>',
        responsive: true,
        retrieve: true,
        serverSide: true,
        processing: true,

        ajax: {
            url: "/Admin/GetUsers",
            type: "POST",
            dataType: "json",
            data: function (filterObj) {
                filterObj.userName = $("#adminUsernameFilter").val();
                filterObj.phoneNo = $("#adminMobileFilter").val();
                filterObj.zipCode = $("#adminZipcodeFilter").val();
                filterObj.userType = $("#adminUserTypeFilter").val();
                filterObj.email = $("#adminEmailFilter").val();

                filterObj.fromDate = $("#adminFromDate").val();

                filterObj.toDate = $("#adminToDate").val();
            },
            beforeSend: function () {
                $("#loadingAnimation").removeClass("d-none");

            },

            complete: function () {
                setTimeout(function () {
                    $("#loadingAnimation").addClass("d-none");
                }, 500);
            },
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "userName", "name":"UserName" },
            {
                "data": "regDate",
                "name": "Date",
                "render": (data, now) => {
                    return `<img src="/img/upcomingService/calendar2.png" > <span>` + data + `</span>`;
                }
            },
            { "data": "userType", "name": "UserTypeId" },
            { "data": "phoneNo", "name": "Mobile" },
            { "data": "zipCode", "name": "ZipCode"},

            {
                "data": {},
                "name": "Status",
                "render": (data, row) => {

                    if (data.status == true) {
                        return `<p style="background-color: #67b644; width:fit-content; margin:0 auto; color: white; padding: 6px 10px;">Active</p>`;
                    } else {
                        return `<p class="text-center" style="background-color: #ff6b6b; width:fit-content; margin:0 auto; color: white; padding: 6px 10px;">Inactive</p>`;
                    }
                }
            },
            {
                "data": {},
                "render": function (data, row) {
                    var btntext = "";
                    if (data.status == true) {
                        btntext = "Deactive";
                    }
                    else {
                        btntext = "Active";
                    }
                    return `<div class="dropdown text-center">
                    <button class="admin-table-actionbtn" type="button" id = "dropdownMenuButton`+ data.userId + `"
                data-bs-toggle="dropdown" aria-expanded="false">
                    <i class="fa fa-ellipsis-v" aria-hidden="true" style="color:#646464" class="text-center"></i>
                            </button>
    <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton`+ data.userId + `">
        <li><a class="dropdown-item umAction" role="button" data-value="`+ data.userId + `">` + btntext +`</a></li>
        
    </ul>
                        </div>`;
                }
            },
            
        ],
        'createdRow': function (row, data, dataIndex) {
            $(row).attr('data-value', data.userId);
        },

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
        columnDefs: [{ orderable: false, targets: 6 }],

    });


document.getElementById("adminFilterBtn").addEventListener("click", function () {
    dt.ajax.reload();
    console.log("search");
});


$("#admin-um-table").on("click", ".umAction", function(e) {

    console.log(e.target.dataset.value);

    changeUserStatus(e.target.dataset.value);

});

function changeUserStatus(userId) {
    var data = {};
    data.userId = userId;
    console.log("inside");
    $.ajax({
        type: 'POST',
        url: '/Admin/ChangeUserStatus',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: data,
        success: function (result) {
            console.log(result);
            if (result.value = "true") {
                dt.ajax.reload();
            }
            else {
                alert("something went wrong");
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