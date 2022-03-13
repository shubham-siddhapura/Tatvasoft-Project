function SPBlockDataTable() {
    const SPBlockDT = $("#blockCustomerTable").DataTable({
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
        columnDefs: [{ orderable: false, targets: 2 }],
    });
}

function getUsersForBlock() {

    $.ajax({
        type: 'GET',
        url: '/ServicePro/GetUsersForBlock',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',

        success: function (result) {
            if (result == "false") {
                alert("something went wrong");
            }
            else {
                console.log(result);
                $("#blockCustomerTable tbody").empty();

                for (var i = 0; i < result.length; i++) {
                    var label = "";
                    var style = "";
                    if (result[i].isBlocked == true) {
                        label = "Unblock";
                        style = '';
                    }
                    else {
                        label = "Block";
                        style = 'style="background-color:#FF6B6B;"';
                    }

                    $("#blockCustomerTable tbody").append(
                        `<tr>
                            <td><img src="/img/cap.jpg" /></td>
                            <td><b>`+ result[i].targetUser.firstName + " " + result[i].targetUser.lastName + `</b></td>
                            <td><button id="blockOrUnblock`+ result[i].targetUser.userId + `" data-id="`+result[i].targetUser.userId+`" `+style+`>`+ label +`</button>
                        </td>
                    </tr>`
                    );
                    
                }
                SPBlockDataTable();
            }
        },
        error: function () {
            alert("error");
        }
    });

    
}

$("#blockCustomerTable").click(function(e){

    console.log(e.target.tagName);
    if (e.target.tagName == "BUTTON") {
        var id = e.target.getAttribute("data-id");
        blockAndUnblockCustomer(id);
    }

});

function blockAndUnblockCustomer(id) {

    var data = {};
    data.targetUserId = id;

    $.ajax({
        type: 'POST',
        url: '/ServicePro/BlockOrUnblockCustomer',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: data,
        success: function (result) {
            if (result == "false") {
                alert("something went wrong");
            }
            else {
                getUsersForBlock();
            }
        },
        error: function () {
            alert("error");
        }
    });


}