$(document).ready(function () {
    // $("#admin-um-table").DataTable();
});

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

                    if (data.Status == true) {
                        return `<p style="background-color: #67b644; width:fit-content; margin:0 auto; color: white; padding: 6px 10px;">active</p>`;
                    } else {
                        return `<p class="inactive-label text-center" style="background-color: #ff6b6b; width:fit-content; margin:0 auto; color: white; padding: 6px 10px;">inactive</p>`;
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
        columnDefs: [{ orderable: false, targets: 6 }],

    });


document.getElementById("adminFilterBtn").addEventListener("click", function () {
    dt.ajax.reload();
});
