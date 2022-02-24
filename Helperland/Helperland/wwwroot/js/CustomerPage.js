// datatable
$(document).ready(function () {
    $("#mytable").DataTable();
});

const dt = new DataTable("#mytable", {
    dom: 't<"table-bottom d-flex justify-content-between paging"<"table-bottom-inner d-flex "li>p>',
    responsive: true,
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

// for dashbord table
const dashbordTable = new DataTable("#dashbordTable", {
    dom: 't<"table-bottom d-flex justify-content-between paging"<"table-bottom-inner d-flex "li>p>',
    responsive: true,
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




if (document.querySelector(".customer-sh-status").innerHTML == "Cancelled") {
    document.querySelector(".customer-sh-status").style.backgroundColor = "#FF6B6B";
} else {
    document.querySelector(".customer-sh-status").style.backgroundColor = "#67B644";
}

// export button js
function html_table_to_excel(type) {
    var data = document.getElementById('mytable');

    var file = XLSX.utils.table_to_book(data, { sheet: "sheet1" });

    XLSX.write(file, { bookType: type, bookSST: true, type: 'base64' });

    XLSX.writeFile(file, 'file.' + type);
}

const export_button = document.getElementById('export');

export_button.addEventListener('click', () => {
    html_table_to_excel('xlsx');
});


// change tabs
var myDashbord = document.getElementById("dashbord");
var myServiceHistory = document.getElementById("serviceHistory");
var mysetting = document.getElementById("mysetting");
var dashbordTab = document.getElementById("dashbordTab");
var serviceHistoryTab = document.getElementById("serviceHistoryTab");

myServiceHistory.style.display = "none";

function dashbord() {
    myDashbord.style.display = "block";
    myServiceHistory.style.display = "none";
    mysetting.style.display = "none";
    dashbordTab.classList.add("active");
    serviceHistoryTab.classList.remove("active");
}

function serviceHistory() {
    myDashbord.style.display = "none";
    myServiceHistory.style.display = "block";
    mysetting.style.display = "none";
    dashbordTab.classList.remove("active");
    serviceHistoryTab.classList.add("active");
}

function mySetting() {
    myDashbord.style.display = "none";
    myServiceHistory.style.display = "none";
    mysetting.style.display = "block";
    dashbordTab.classList.remove("active");
    serviceHistoryTab.classList.remove("active");
}

const url = new URLSearchParams(window.location.search);

if (url == "mySetting=true") {

    mySetting();
}


/*============= my setting ===========*/

var myDetailsTab = document.getElementById("myDetailsTab");
var myAddressTab = document.getElementById("myAddressTab");
var changePasswordTab = document.getElementById("changePasswordTab");

var myDetails = document.getElementById("myDetailsMySetting");
var myAddress = document.getElementById("myAddressMySetting");
var changePassword = document.getElementById("changePasswordMySetting");

function mydetails() {
    myAddress.classList.add("d-none");
    changePassword.classList.add("d-none");
    myDetails.classList.remove("d-none");

    myDetailsTab.classList.add("active");
    myAddressTab.classList.remove("active");
    changePasswordTab.classList.remove("active");
}


function myaddress() {
    myDetails.classList.add("d-none");
    myAddress.classList.remove("d-none");
    changePassword.classList.add("d-none");

    myDetailsTab.classList.remove("active");
    myAddressTab.classList.add("active");
    changePasswordTab.classList.remove("active");
}

function changepassword() {
    myDetails.classList.add("d-none");
    myAddress.classList.add("d-none");
    changePassword.classList.remove("d-none");

    myDetailsTab.classList.remove("active");
    myAddressTab.classList.remove("active");
    changePasswordTab.classList.add("active");
}


/* =================== Customer Dashbord ================== */
var serviceRequestId = 0;
document.addEventListener("click", function (e) {
    if (e.target.classList == "customerReschedule") {
        document.getElementById("updateRequestId").value = e.target.value;
    }
    if (e.target.classList == "customerCancel") {
        document.getElementById("CancelRequestId").value = e.target.value;
    }


    serviceRequestId = e.target.closest("tr").getAttribute("data-value");

    if (serviceRequestId != null && (e.target.classList != "customerCancel" && e.target.classList != "customerReschedule")) {
        document.getElementById("serviceReqdetailsbtn").click();
    }
    console.log(e);
});

document.getElementById("CancelRequestBtn").addEventListener("click", function () {

    var ServiceRequestId = document.getElementById("CancelRequestId").value;
    var Comments = document.getElementById("cancelReason").value;
    var data = {};

    data.serviceRequestId = ServiceRequestId;
    data.comments = Comments;

    $.ajax({
        type: 'POST',
        url: '/CustomerPage/CancelServiceRequest',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: data,
        success: function (result) {
            if (result.value == "true") {
                window.location.reload();
            }
            else {
                alert("fail");
            }
        },
        error: function () {
            alert("error");
        }
    });

});

document.getElementById("updateServiceRequest").addEventListener("click", function () {
    var serviceStartDate = document.getElementById("updateRequestDate").value;
    var serviceTime = document.getElementById("rescheduleTime").value;
    var serviceRequestId = document.getElementById("updateRequestId").value;
    console.log(serviceRequestId);
    var data = {};
    data.serviceStartDate = serviceStartDate;
    data.startTime = serviceTime;
    data.serviceRequestId = serviceRequestId;

    $.ajax({
        type: 'POST',
        url: '/CustomerPage/RescheduleServiceRequest',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: data,
        success: function (result) {
            if (result.value == "true") {
                window.location.reload();
            }
            else {
                alert("fail");
            }
        },
        error: function () {
            alert("error");
        }
    });


});

document.getElementById("serviceReqdetailsbtn").addEventListener("click", function () {
    console.log("abcd");

    var dateTime = document.getElementById("CDDetailsDateTime");
    var duration = document.getElementById("CDDetailsDuration");
    document.getElementById("CDDetailsId").innerHTML = serviceRequestId;
    var extra = document.getElementById("CDDetailsExtra");
    var amount = document.getElementById("CDDetailsAmount");
    var address = document.getElementById("CDDetailsAddress");
    var phone = document.getElementById("CDDetailsPhone");
    var email = document.getElementById("CDDetailsEmail");
    var comment = document.getElementById("CDDetailsComment");

    var data = {};
    data.ServiceRequestId = parseInt(serviceRequestId);
    $.ajax({
        type: 'GET',
        url: '/CustomerPage/DashbordServiceDetails',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: data,
        success: function (result) {
            console.log(result);
            if (result != null) {
                dateTime.innerHTML = result.date.substring(0, 10) + " " + result.startTime + " - " + result.endTime;
                duration.innerHTML = result.duration;
                extra.innerHTML = "";
                if (result.cabinet == true) {
                    extra.innerHTML += "<p>Inside Cabinet</p>";
                }
                if (result.laundry == true) {
                    extra.innerHTML += "<p>Laundry Wash & dry</p>";
                }
                if (result.oven == true) {
                    extra.innerHTML += "<p>Inside Oven</p>";
                }
                if (result.fridge == true) {
                    extra.innerHTML += "<p>Inside Fridge</p>";
                }
                if (result.window == true) {
                    extra.innerHTML += "<p>Interior Window</p>";
                }
                amount.innerHTML = result.totalCost + " &euro;";
                address.innerHTML = result.address;
                phone.innerHTML = result.phoneNo;
                email.innerHTML = result.email; 
                comment.innerHTML = "";
                if (result.comments != null) {
                    comment.innerHTML = result.comments;
                }
                
            }
            else {
                alert("result is null");
            }
            
        },
        error: function () {
            alert("error");
        }
    });
});