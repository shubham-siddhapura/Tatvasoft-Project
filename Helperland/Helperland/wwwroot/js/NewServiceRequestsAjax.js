var newService = $("#newServiceRequestTable").DataTable({
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
        columnDefs: [{ orderable: false, targets: 4 }],
    });



function acceptServiceRequest(serviceRequestId) {

    var data = {};
    data.serviceRequestId = serviceRequestId;

    $.ajax({
        type: 'POST',
        url: '/ServicePro/AcceptServiceRequest',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: data,
        success: function (result) {
            console.log(result);
            if (result.value == "true") {
                location.reload();
                
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

document.getElementById("havepetCheckbox").addEventListener("change", function () {

    
    if (document.getElementById("havepetCheckbox").checked == true) {
        console.log("checked");
        newService.search("").draw();
        
    }
    else {
        console.log("not checked");
        newService.search("dontHavePet").draw();
        
    }
});