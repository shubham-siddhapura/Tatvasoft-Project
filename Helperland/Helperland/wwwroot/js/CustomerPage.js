//const { data } = required("jquery");

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
    getUserData();
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
    getAddress();
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


$("#dashbordTable").click(function (e) {
    serviceRequestId = e.target.closest("tr").getAttribute("data-value");

    if (e.target.classList == "customerReschedule") {
        document.getElementById("updateRequestId").value = e.target.value;
    }
    if (e.target.classList == "customerCancel") {
        document.getElementById("CancelRequestId").value = e.target.value;
    }

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


/*==================== MY SETTING ========================*/

function getUserData(){
    console.log("hello");
    $.ajax({
        type: 'GET',
        url: '/CustomerPage/GetUserData',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        
        success: function (result) {
            if (result != false) {
                var fname = document.getElementById("mySettingFname");
                var lname = document.getElementById("mySettingLname");
                var email = document.getElementById("mySettingEmail");
                var mobile = document.getElementById("mySettingMobile");

                fname.value = result.firstName;
                lname.value = result.lastName;
                email.value = result.email;

                mobile.value = result.mobile;


                if (result.dateOfBirth != null) {
                    var dateOfBirth = result.dateOfBirth.split('T');
                    var dateOfBirthArray = dateOfBirth[0].split("-");
                    $(".day").val(dateOfBirthArray[2]);
                    $(".month").val(dateOfBirthArray[1]);
                    $(".year").val(dateOfBirthArray[0]);
                }

            }
        },
        error: function () {
            alert("error");
        }
    });
}

/*========= custom date ======*/
$("#mySettingDob").dateDropdowns({
    submitFieldName: 'mySettingDob',
    minAge: 18,
    submitFormat: "dd/mm/yyyy"
});


document.getElementById('customerMySettingDetailsSave').addEventListener("click", function () {
    console.log("clicked save");
    updateUserData();
});

function updateUserData() {
    console.log("inside update");
    var data = {};
    data.firstName = document.getElementById("mySettingFname").value;
    data.lastName = document.getElementById("mySettingLname").value;
    data.email = document.getElementById("mySettingEmail").value;
    data.mobile = document.getElementById("mySettingMobile").value;
    data.dateOfBirth = $(".month").val() + "/" + $(".day").val() + "/" + $(".year").val();

    $.ajax({
        type: 'POST',
        url: '/CustomerPage/UpdateUserData',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: data,
        success: function (result) {
            if (result.value == "true") {
                window.location.reload();
            }
            else {
                alert("not updated");
            }
        },
        error: function () {
            alert("error");
        }
    });
}

/*-------------------- my setting Address ------------------------*/

function getAddress() {
    
    
    $.ajax({
        type: 'GET',
        url: '/CustomerPage/GetUserAddresses',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',

        success: function (result) {
            if (result != false) {
                console.log(result);

                $("#customerMySettingAddressBody").empty();
                for (var i = 0; i < result.length; i++){
                    
                    $("#customerMySettingAddressBody").append('<tr data-value=' + result[i].addressId + ' ><td><p><strong>Address: </strong>' + result[i].addressLine2 + ", " + result[i].addressLine1 + ', ' + result[i].postalCode + ' - ' + result[i].city + '</p><p><strong>Phone number: </strong>' + result[i].mobile + '</p></td><td class="myAddressBtns"><button class="myAddressButton myAddressEditBtn" data-value=' + result[i].addressId + '><svg xmlns="http://www.w3.org/2000/svg" class="edit-icon" fill="none" viewBox="0 0 24 24" stroke="currentColor"> <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M11 5H6a2 2 0 00-2 2v11a2 2 0 002 2h11a2 2 0 002-2v-5m-1.414-9.414a2 2 0 112.828 2.828L11.828 15H9v-2.828l8.586-8.586z" /> </svg> </button> <button class="myAddressButton myAddressDeleteBtn" data-value=' + result[i].addressId +'> <svg xmlns="http://www.w3.org/2000/svg" class="delete-icon" fill="none" viewBox="0 0 24 24" stroke="currentColor"> <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" /> </svg> </button> </td> </tr >');
                }

            }
            else {
                alert("something wrong");
            }
        },
        error: function () {
            alert("error");
        }
    });
}

var addressId = 0;

document.getElementById("myAddressMySetting").addEventListener("click", function (e) {
    var element = e.target.closest("button");
    console.log(element);
    if (element != null) {
        if (element.classList.contains("myAddressEditBtn")) {
            console.log("edit");
            addressId = element.getAttribute("data-value");
        }
        if (element.classList.contains("myAddressDeleteBtn")) {
            console.log("delete");
            addressId = element.getAttribute("data-value");
            $("#MySettingDeleteAddressModalBtn").click();
        }
    }
    
});

$("#MySettingDeleteAddress").click(function () {
    var data = {};
    data.addressId = addressId;
    $.ajax({
        type: "POST",
        url: "/CustomerPage/DeleteUserAddress",
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: data,
        success: function (result) {
            if (result==true) {
                getAddress();
            }
            else {
                alert("fail");
            }
        },
        error: function (error) {

        }
    });

});


document.getElementById("mySettingAddressModalSubmitBtn").addEventListener("click", function () {

    addNewAddress();

});

function addNewAddress() {
    var data = {};
    data.addressLine1 = document.getElementById("ms_addAddr_house").value;
    data.addressLine2 = document.getElementById("ms_addAddr_street").value;
    data.postalCode = document.getElementById("ms_addAddr_postal").value;
    data.city = document.getElementById("ms_addAddr_city").value;
    data.mobile = document.getElementById("ms_addAddr_mobile").value;

    $.ajax({
        type: "POST",
        url: "/CustomerPage/AddUserAddress",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: data,
        success: function (result) {
            if (result.value == "true") {
                getAddress();
                clearAddAddressField();
            }
            else {
                alert("not saved");
            }
                
        },
        error: function (error) {
            alert(error);
        }

    });
}

function clearAddAddressField() {
    document.getElementById("ms_addAddr_house").value = null;
    document.getElementById("ms_addAddr_street").value = null;
    document.getElementById("ms_addAddr_postal").value = null;
    document.getElementById("ms_addAddr_city").value = null;
    document.getElementById("ms_addAddr_mobile").value = null;
}

/* ================ My Setting Change Password ==================*/
document.getElementById("mySettingChangePasswordBtn").addEventListener("click", function () {
    changeUserPassword();
});




function changeUserPassword() {
    var data = {};
    data.oldPassword = document.getElementById("ms_OldPassword").value;
    data.newPassword = document.getElementById("ms_NewPassword").value;
    data.confirmPassword = document.getElementById("ms_ConfirmPassword").value;


    window.setTimeout(function () {
        $('#ms_changePasswordAlert').addClass('d-none');
    }, 5000);

    if (data.oldPassword == "" && data.newPassword == "" && data.confirmPassword == "") {
        $("#ms_changePasswordAlert").removeClass("alert-success d-none").addClass("alert-danger").text("All Fields are Required.");
        $("#ms_OldPassword").focus();
    }
    else if (data.oldPassword == "") {
        $("#ms_changePasswordAlert").removeClass("alert-success d-none").addClass("alert-danger").text("Old password is Required.");
        $("#ms_OldPassword").focus();
    }
    else if (data.newPassword == "") {
        $("#ms_changePasswordAlert").removeClass("alert-success d-none").addClass("alert-danger").text("New password is Required.");
        $("#ms_NewPassword").focus();
    }
    else if (data.confirmPassword == "") {
        $("#ms_changePasswordAlert").removeClass("alert-success d-none").addClass("alert-danger").text("Confirm password is Required.");
        $("#ms_ConfirmPassword").focus();
    }
    else if (data.newPassword != data.confirmPassword) {
        $("#ms_changePasswordAlert").removeClass("alert-success d-none").addClass("alert-danger").text("New Password and Confirm Password must be same.");
        $("#ms_ConfirmPassword").focus();
    }
    else {
        $.ajax({
            type: "POST",
            url: "/CustomerPage/ChangePassword",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            data: data,
            success: function (result) {
                if (result.value == "true") {
                    $("#ms_changePasswordAlert").removeClass("alert-danger d-none").addClass("alert-success").text("Password Changed Successfully.");
                }
                else if (result.value == "wrong password") {
                    $("#ms_changePasswordAlert").removeClass("alert-success d-none").addClass("alert-danger").text("Current(Old) Password is wrong! Please try again.");
                }
            },
            error: function (error) {
                $("#ms_changePasswordAlert").removeClass("alert-success d-none").addClass("alert-danger").text("Something went wrong! Please try again.");
            }
        });
    }
    
}
