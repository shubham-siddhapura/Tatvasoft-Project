// const { data } = required("jquery");

// datatable
$(document).ready(function () {
});

function serviceHistoryDatatable() {
    console.log("inside datatable")
    $("#mytable").DataTable({
        
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

/*if (document.querySelector(".customer-sh-status").innerHTML == "Cancelled") {
    document.querySelector(".customer-sh-status").style.backgroundColor = "#FF6B6B";
} else {
    document.querySelector(".customer-sh-status").style.backgroundColor = "#67B644";
}*/

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

    getServiceHistoryTable();

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
var serviceDate="";
var serviceTime="";

$("#dashbordTable").click(function (e) {
    serviceRequestId = e.target.closest("tr").getAttribute("data-value");

    if (e.target.classList == "customerReschedule") {
        serviceRequestId = e.target.value;
        console.log(serviceRequestId)
        document.getElementById("updateRequestId").value = e.target.value;
        getDateforReschedule();
        
    }
    if (e.target.classList == "customerCancel") {
        document.getElementById("CancelRequestId").value = e.target.value;
    }

    if (serviceRequestId != null && (e.target.classList != "customerCancel" && e.target.classList != "customerReschedule")) {
        document.getElementById("modalCancelBtn").style.display = "inline";
        document.getElementById("modalRescheduleBtn").style.display = "inline";
        document.getElementById("modalRateSPBtn").style.display = "none";
        document.getElementById("serviceReqdetailsbtn").click();
    }
    console.log(e);
});

$("#mytable").click(function(e){

    console.log(e);
    serviceRequestId = e.target.closest("tr").getAttribute("data-value");

    if (serviceRequestId != null && !e.target.classList.contains("btn")) {
        document.getElementById("modalCancelBtn").style.display = "none";
        document.getElementById("modalRescheduleBtn").style.display = "none";
        document.getElementById("modalRateSPBtn").style.display = "inline";

       
        document.getElementById("serviceReqdetailsbtn").click();

    }

    if (e.target.classList.contains("btn")) {
        var name = $("#serviceProviderId" + serviceRequestId + " .SP-name").text();
        var rating = $("#serviceProviderId" + serviceRequestId + " .SP-stars").text();
        console.log(name + " " + rating);
        $("#ratingModalSPName").text(name);
        $("#ratingModalSPStarts").text(rating);
        starAsperRating(rating, ".spStarsRatingModal");
    }

});

/*== Onclick on service details modal's Reschedule Btn ==*/
document.getElementById("modalRescheduleBtn").addEventListener("click", function () {

    $("#customerDashbordRescheduleBtn").click();

});
/*== Onclick on service details modal's Cancel Btn ==*/
document.getElementById("modalCancelBtn").addEventListener("click", function () {
    $("#customerDashbordCancelBtn").click();
});

/*== Onclick on service details modal's RateSP Btn ==*/
document.getElementById("modalRateSPBtn").addEventListener("click", function () {
    console.log($("#CustomerServiceHistoryRateSPBtn"));
    
});


function getDateforReschedule() {
    var data = {};
    data.serviceRequestId = serviceRequestId;
    $.ajax({
        type: 'GET',
        url: "/CustomerPage/GetDateforReschedule",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: data,
        success: function (result) {
            if (result != false) {

                console.log(result);
                var date = result.split("T");
                console.log(date);

                var time = date[1];
                console.log(time.substring(0, 2));
                if (parseInt(time.substring(0, 2)) <= 12) {
                    time = time + " AM";
                } else {
                    var temp = parseInt(time.substring(0, 2)) - 12;

                    time = temp + time.substring(2, time.length) + " PM";
                }
                console.log(time);
                $("#updateRequestDate").val(date[0]);
                $("#rescheduleTime").val(time);

            }
        },
        error: function (error) {

        }
    });
}


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

function getAllServiceDetails() {
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
                showAllServiceRequestDetails(result);
            }
            else {
                alert("result is null");
            }

        },
        error: function () {
            alert("error");
        }
    });
}

function showAllServiceRequestDetails(result) {
    var dateTime = document.getElementById("CDDetailsDateTime");
    var duration = document.getElementById("CDDetailsDuration");
    document.getElementById("CDDetailsId").innerHTML = serviceRequestId;
    var extra = document.getElementById("CDDetailsExtra");
    var amount = document.getElementById("CDDetailsAmount");
    var address = document.getElementById("CDDetailsAddress");
    var phone = document.getElementById("CDDetailsPhone");
    var email = document.getElementById("CDDetailsEmail");
    var comment = document.getElementById("CDDetailsComment");


    if (result.serviceProvider != null) {
        $("#serviceProviderDetailsInModal").removeClass("d-none");
        $("#CDDetails_SPName").text(result.serviceProvider);
        $("#CDDetailsRating").text(result.spRatings);
        $("#totalCompletedServices").text(result.completedService);
        console.table(result.status);
        if (result.status != 0) {
            $("#modalRateSPBtn").addClass("disabled");
        }
        else {
            $("#modalRateSPBtn").removeClass("disabled");

        }
    }
    else {
        $("#serviceProviderDetailsInModal").addClass("d-none");
    }

    if (result.status != 0) {
        $("#modalRateSPBtn").addClass("disabled");
    }
    else {
        $("#modalRateSPBtn").removeClass("disabled");

    }

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

    starAsperRating(result.spRatings, ".spRatingDetailsModal");
}

document.getElementById("serviceReqdetailsbtn").addEventListener("click", function () {
    getAllServiceDetails();
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

                console.log(result);
                console.log(result.dateOfBirth);
                if (result.dateOfBirth != null) {
                    var dateOfBirth = result.dateOfBirth.split('T');
                    var dateOfBirthArray = dateOfBirth[0].split("-");
                    console.log(dateOfBirthArray);
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
    data.dateOfBirth = $(".day").val() + "-" + $(".month").val() + "-" + $(".year").val();

    console.log(data.dateOfBirth);
    window.setTimeout(function () {
        $('#ms_myDetailsAlert').addClass('d-none');
    }, 5000);

    if (data.firstName == "") {
        $("#ms_myDetailsAlert").removeClass("alert-success d-none").addClass("alert-danger").text("Firstname is Required.");
        $("#mySettingFname").focus();
    }
    else if (data.lastName == "") {
        $("#ms_myDetailsAlert").removeClass("alert-success d-none").addClass("alert-danger").text("Lastname is Required.");
        $("#mySettingLname").focus();
    }
    else if (data.mobile == "") {
        $("#ms_myDetailsAlert").removeClass("alert-success d-none").addClass("alert-danger").text("Mobile is Required.");
        $("#mySettingMobile").focus();
    }
    else {
        $.ajax({
            type: 'POST',
            url: '/CustomerPage/UpdateUserData',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: data,
            success: function (result) {
                if (result.value == "true") {
                    $("#ms_myDetailsAlert").removeClass("alert-danger d-none").addClass("alert-success").text("User is updated.");
                    window.setTimeout(function () {
                        window.location.reload();
                    }, 5000);
                    
                }
                else {
                    $("#ms_myDetailsAlert").removeClass("alert-success d-none").addClass("alert-danger").text("Something went wrong! please try again later.");
                }
            },
            error: function () {
                alert("error");
            }
        });
    }
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

                showAddressPagination();
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
            addressId = element.getAttribute("data-value");
            console.log("edit");
            getAddressDataToModal();
            $("#mySettingAddressModalUpdateBtn").removeClass("d-none");
            $("#mySettingAddressModalSubmitBtn").addClass("d-none");
            
            console.log(addressId);
            $("#mySettingAddAddressModal").modal("show");
            $("#mySettingAddressModalTitle").text("Edit Address");
            
        }
        if (element.classList.contains("myAddressDeleteBtn")) {
            console.log("delete");
            addressId = element.getAttribute("data-value");
            $("#MySettingDeleteAddressModalBtn").click();
        }
    }
    
});

$("#mySettingAddNewAddress").click(function () {
    $("#mySettingAddressModalUpdateBtn").addClass("d-none");
    $("#mySettingAddressModalSubmitBtn").removeClass("d-none");
    $("#mySettingAddressModalTitle").text("Add New Address");
    clearAddAddressField();
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

document.getElementById("mySettingAddressModalUpdateBtn").addEventListener("click", function () {
    
    EditAddress();

});

function addNewAddress() {
    var data = {};
    data.addressLine1 = document.getElementById("ms_addAddr_house").value.trim();
    data.addressLine2 = document.getElementById("ms_addAddr_street").value.trim();
    data.postalCode = document.getElementById("ms_addAddr_postal").value.trim();
    data.city = document.getElementById("ms_addAddr_city").value.trim();
    data.mobile = document.getElementById("ms_addAddr_mobile").value.trim();
    data.state = document.getElementById("ms_addAddr_state").value;

    window.setTimeout(function () {
        $('#ms_addAddressAlert').addClass('d-none');
    }, 5000);

    if (data.addressLine1 == "" && data.addressLine2 == "" && data.postalCode == "" && data.city == "" && data.mobile == "") {
        $("#ms_addAddressAlert").removeClass("alert-success d-none").addClass("alert-danger").text("All Fields are Required.");
        $("#ms_addAddr_street").focus();
    }
    else if (data.addressLine1 == "") {
        $("#ms_addAddressAlert").removeClass("alert-success d-none").addClass("alert-danger").text("House no. is Required.");
        $("#ms_addAddr_house").focus();
    }
    else if (data.addressLine2 == "") {
        $("#ms_addAddressAlert").removeClass("alert-success d-none").addClass("alert-danger").text("Street name is Required.");
        $("#ms_addAddr_street").focus();
    }
    else if (data.postalCode == "") {
        $("#ms_addAddressAlert").removeClass("alert-success d-none").addClass("alert-danger").text("Postalcode is Required.");
        $("#ms_addAddr_postal").focus();
    }
    else if (data.city == "") {
        $("#ms_addAddressAlert").removeClass("alert-success d-none").addClass("alert-danger").text("City is Required.");
        $("#ms_addAddr_city").focus();
    }
    else if (data.mobile == "") {
        $("#ms_addAddressAlert").removeClass("alert-success d-none").addClass("alert-danger").text("Mobile is Required.");
        $("#ms_addAddr_house").focus();
    }
    else {
        $.ajax({
            type: "POST",
            url: "/CustomerPage/AddUserAddress",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            data: data,
            success: function (result) {
                if (result.value == "true") {
                    getAddress();
                    clearAddAddressField();
                    $("#mySettingAddAddressModal").modal("hide");
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
}

function getAddressDataToModal() {
    var data = {};
    data.addressId = addressId;
    
    $.ajax({
        type: 'GET',
        url: '/CustomerPage/GetAddressDataToModal',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: data,
        success: function (result) {
            if (result != false) {

                document.getElementById("ms_addAddr_house").value = result.addressLine1;
                document.getElementById("ms_addAddr_street").value = result.addressLine2;
                document.getElementById("ms_addAddr_postal").value = result.postalCode;
                document.getElementById("ms_addAddr_city").value = result.city;
                document.getElementById("ms_addAddr_mobile").value = result.mobile;
            }
        },
        error: function (error) {
            alert(error);
        }
    })
}

function EditAddress() {
    var data = {};
    data.addressId = addressId;
    data.addressLine1 = document.getElementById("ms_addAddr_house").value.trim();
    data.addressLine2 = document.getElementById("ms_addAddr_street").value.trim();
    data.postalCode = document.getElementById("ms_addAddr_postal").value.trim();
    data.city = document.getElementById("ms_addAddr_city").value.trim();
    data.mobile = document.getElementById("ms_addAddr_mobile").value.trim();


    window.setTimeout(function () {
        $('#ms_addAddressAlert').addClass('d-none');
    }, 5000);

    if (data.addressLine1 == "" && data.addressLine2 == "" && data.postalCode == "" && data.city == "" && data.mobile == "") {
        $("#ms_addAddressAlert").removeClass("alert-success d-none").addClass("alert-danger").text("All Fields are Required.");
        $("#ms_addAddr_street").focus();
    }
    else if (data.addressLine1 == "") {
        $("#ms_addAddressAlert").removeClass("alert-success d-none").addClass("alert-danger").text("House no. is Required.");
        $("#ms_addAddr_house").focus();
    }
    else if (data.addressLine2 == "") {
        $("#ms_addAddressAlert").removeClass("alert-success d-none").addClass("alert-danger").text("Street name is Required.");
        $("#ms_addAddr_street").focus();
    }
    else if (data.postalCode == "") {
        $("#ms_addAddressAlert").removeClass("alert-success d-none").addClass("alert-danger").text("Postalcode is Required.");
        $("#ms_addAddr_postal").focus();
    }
    else if (data.city == "") {
        $("#ms_addAddressAlert").removeClass("alert-success d-none").addClass("alert-danger").text("City is Required.");
        $("#ms_addAddr_city").focus();
    }
    else if (data.mobile == "") {
        $("#ms_addAddressAlert").removeClass("alert-success d-none").addClass("alert-danger").text("Mobile is Required.");
        $("#ms_addAddr_house").focus();
    }
    else {
        $.ajax({
            type: "POST",
            url: "/CustomerPage/EditUserAddress",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            data: data,
            success: function (result) {
                if (result.value == "true") {
                    getAddress();
                    clearAddAddressField();
                    $("#ms_addAddressAlert").removeClass("alert-danger d-none").addClass("alert-success").text("Address is updated successfully.");

                    $("#mySettingAddAddressModal").modal("hide");
                }
                else {
                    alert("not saved");
                }
                $("#mySettingAddressModalUpdateBtn").addClass("d-none");
                $("#mySettingAddressModalSubmitBtn").removeClass("d-none");

            },
            error: function (error) {
                $("#mySettingAddressModalUpdateBtn").addClass("d-none");
                $("#mySettingAddressModalSubmitBtn").removeClass("d-none");
                alert(error);
            }

        });
    }

}

function clearAddAddressField() {
    document.getElementById("ms_addAddr_house").value = null;
    document.getElementById("ms_addAddr_street").value = null;
    document.getElementById("ms_addAddr_postal").value = null;
    document.getElementById("ms_addAddr_city").value = null;
    document.getElementById("ms_addAddr_mobile").value = null;
}

$("#ms_addAddr_postal").keyup(function () {
    console.log($("#ms_addAddr_postal").val());
    if ($("#ms_addAddr_postal").val().length == 6) {
        getCityFromPostalCode($("#ms_addAddr_postal").val());
    }
});


function getCityFromPostalCode(zip) {
    $.ajax({
        method: "GET",
        url: "https://api.postalpincode.in/pincode/"+zip,
        dataType: 'json',
        cache: false,
        success: function (result) {
            if (result[0].status == "Error" || result[0].status == "404") {
                $("#ms_addAddressAlert").removeClass("alert-success d-none").addClass("alert-danger").text("Enter Valid PostalCode.");

            }
            else {
                console.log(result);
                $("#ms_addAddr_city").val(result[0].PostOffice[0].District);
                $("#ms_addAddr_state").val(result[0].PostOffice[0].State);
                $("#ms_addAddr_city").prop("disabled", true);
            }
        },
        error: function (error) {

        }
    });
}

/* ==== my address pagination =====*/
var showedRows = 5

function showAddressPagination() {

    showedRows = 5;

    var $table = $('#myAddressMySetting').find('tbody');
    var totalRows = $table.find('tr').length;

    if (showedRows > totalRows) {
        showedRows = totalRows;
    }

    $table.find('tr:gt(' + (showedRows - 1) + ')').hide();
    
    $("#showedAddress").text(showedRows);
    $("#totalAddress").text(totalRows);
}

$("#showMoreAddressBtn").click(function () {

    showedRows += 5;

    var $table = $('#myAddressMySetting').find('tbody');
    var totalRows = $table.find('tr').length;


    if (showedRows > totalRows) {
        showedRows = totalRows;
    }

    $("#showedAddress").text(showedRows);
    $table.find('tr:lt(' + showedRows + ')').show();
});


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

/*====================== Customer Service History ========================*/

function getServiceHistoryTable() {

    console.log("inside getServiceHistoryTable");

    $.ajax({
        type: "GET",
        url: "/CustomerPage/GetServiceHistoryTable",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        success: function (result) {
            console.table(result);

            $("#CustomerServiceHistoryTBody").empty();
           
            for (var i = 0; i < result.length; i++) {
                var status = "";
                var background = "";
                
                var disabled = "btn lh-base";
                if (result[i].status == 1 ) {
                    status = "Cancelled";
                    background = "background-color:#FF6B6B";
                    disabled = "btn disabled lh-base";
                }
                else if (result[i].status == 0) {
                    status = "Completed";
                    background = "background-color: #67B644";
                    if (result[i].alreadyRated == true) {
                        disabled = "btn disabled lh-base";
                    }
                    else {
                        disabled = "btn lh-base";
                    }
                }

                if (result[i].serviceProvider != null) {

                    $("#CustomerServiceHistoryTBody").append('<tr data-value=' + result[i].serviceRequestId + '><td>' + result[i].serviceRequestId + '</td><td><div><span><img src="/img/customer-serviceHistory/calendar.png" alt=""></span><span class="upcoming-date"><b>' + result[i].serviceStartDate + '</b></span></div><div><span class="upcoming-time">' + result[i].startTime + ' - ' + result[i].endTime + '</span></div></td><td id="serviceProviderId' + result[i].serviceRequestId + '"> <div class="customer-sh-SP"><div><span class="cap-span" ><img id="ratingModalSPAvtar" src="/img/customer-serviceHistory/cap.png" class="cap" alt=""></span></div><div class="sp-detail"><p class="SP-name" >' + result[i].serviceProvider + '</p><div class="d-flex"><div class="spStars" id="serviceHistoryRating' + result[i].serviceRequestId + '"><span class="stars"></span><span class="stars"></span><span class="stars"></span><span class="stars"></span><span class="stars"></span><span class="stars"></span><span class="stars"></span><span class="stars"></span><span class="stars"></span><span class="stars"></span></div > <div><span class="ms-2 SP-stars" >' + result[i].spRatings + '</span></div></div></div ></div ></td ><td><div class="customer-sh-pay">&euro;<span class="payment"> ' + result[i].totalCost + '</span></div></td><td><p class="customer-sh-status" style="' + background + ';">' + status + '</p></td><td class="allPageActionButtons "><a href="#" data-bs-toggle="modal" data-bs-target="#myRatingModal" class="' + disabled + '">Rate SP</a></td></tr > ');

                }
                else {
                    
                    $("#CustomerServiceHistoryTBody").append('<tr data-value=' + result[i].serviceRequestId + '><td>' + result[i].serviceRequestId + '</td><td><div><span><img src="/img/customer-serviceHistory/calendar.png" alt=""></span><span class="upcoming-date"><b>' + result[i].serviceStartDate + '</b></span></div><div><span class="upcoming-time">' + result[i].startTime + ' - ' + result[i].endTime + '</span></div></  td><td></td> <td><div class="customer-sh-pay">&euro;<span class="payment"> ' + result[i].totalCost + '</span></div></td><td><p class="customer-sh-status" style="' + background + ';">' + status + '</p></td><td class="allPageActionButtons "><a href="#"  data-bs-toggle="modal" data-bs-target="#myRatingModal" class="' + disabled + '">Rate SP</a></td></tr>');
                }

                if (result[i].serviceProvider != null) {

                    starAsperRating(result[i].spRatings, "#serviceHistoryRating" + result[i].serviceRequestId);
                }
                
            }

            

            serviceHistoryDatatable();
        },
        error: function (error) {
            console.log(error);
        }
    });

}

/*=-=- Rating service Provider -=-=*/

document.getElementById("myRatingModalSubmit").addEventListener("click", function () {
    
    rateServiceProvider();

});

function rateServiceProvider() {
    var data = {};
    data.onTimeArrival = $("#ratingModalOnTime input[type=radio]:checked").val();
    data.friendly = $("#ratingModalFriendly input[type=radio]:checked").val();
    data.qualityOfService = $("#ratingModalQuality input[type=radio]:checked").val();
    data.ratings = (parseFloat(data.onTimeArrival) + parseFloat(data.friendly) + parseFloat(data.qualityOfService)) / 3;
    data.comments = $("#myRatingFeedback").val();
    data.serviceRequestId = serviceRequestId;

    console.log(data);

    $.ajax({

        type: "POST",
        url: "/CustomerPage/RateServiceProvider",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: data,
        success: function (result) {
            if (result.value == "true") {
                $("#myRatingModal").modal("hide");
                getServiceHistoryTable();
                console.log("submited");
            }
            else {
                alert("kirpal");
            }
        },
        error: function (error) {
            alert("error");
        }

    });

}

/* ======= star as per rating ======= */

function starAsperRating(rate, id) {

    console.log(id);

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