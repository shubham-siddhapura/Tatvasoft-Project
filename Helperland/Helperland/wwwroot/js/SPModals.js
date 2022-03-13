
var serviceRequestId = 0;

$("#newServiceRequestTable").click(function (e) {

    console.log(e);
    if (e.target.closest("tr") != null)
        serviceRequestId = e.target.closest("tr").getAttribute("data-value");

    if (serviceRequestId != null && !e.target.classList.contains("btn")) {


        document.getElementById("AcceptBtnModal").classList.remove("d-none");
        document.getElementById("CancelBtnModal").classList.add("d-none");
        document.getElementById("CompleteBtnModal").classList.add("d-none");

        document.getElementById("serviceReqdetailsbtn").click();

    }

    if (e.target.classList.contains("timeConflict")) {
        serviceRequestId = e.target.getAttribute("data-value");
        document.getElementById("serviceReqdetailsbtn").click();
    }

    if (e.target.classList.contains("acceptService")) {
        acceptServiceRequest(serviceRequestId);
    }
});


$("#mytable").click(function (e) {

    console.log(e);
    serviceRequestId = e.target.closest("tr").getAttribute("data-value");

    if (serviceRequestId != null && (e.target.tagName != "A" && e.target.tagName != "a")) {

        document.getElementById("AcceptBtnModal").classList.add("d-none");
        document.getElementById("CancelBtnModal").classList.remove("d-none");
        var completed = e.target.closest("tr").getAttribute("data-completed");
        if (completed == "true") {
            document.getElementById("CompleteBtnModal").classList.remove("d-none");
        }
        else {
            document.getElementById("CompleteBtnModal").classList.add("d-none");
        }

        document.getElementById("serviceReqdetailsbtn").click();
    }
   
    if ((e.target.tagName == "A" || e.target.tagName == "a") && e.target.textContent == "Cancel") {

        console.log("hello");
        $("#CancelRequestId").val(serviceRequestId);
        cancelRequest(serviceRequestId);
        /*$("#serviceRequestCancelModal").modal('show');*/
    }
    else if ((e.target.tagName == "A" || e.target.tagName == "a") && e.target.textContent == "Complete") {
        completeRequest(serviceRequestId);
    }

});

document.getElementById("AcceptBtnModal").addEventListener("click", function () {
    document.getElementById("newAcceptBtn" + serviceRequestId).click();
});

document.getElementById("CancelBtnModal").addEventListener("click", function () {
    document.getElementById("upcomingCancelBtn" + serviceRequestId).click();
});

document.getElementById("CompleteBtnModal").addEventListener("click", function () {
    document.getElementById("upcomingCompleteBtn" + serviceRequestId).click();
});

function getAllServiceDetails() {
    var data = {};
    data.ServiceRequestId = parseInt(serviceRequestId);
    $.ajax({
        type: 'GET',
        url: '/ServicePro/DashbordServiceDetails',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: data,
        success: function (result) {
            console.log(result);
            if (result != null) {
                showAllServiceRequestDetails(result);
                getlon_len(result.zipCode);
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
    var address = document.getElementById("ModalAddress");
    var phone = document.getElementById("ModalPhoneNo");

    var comment = document.getElementById("ModalComments");
    var dontPet = document.getElementById("dontHavePet");
    var pet = document.getElementById("HavePet");
    var CustomerName = document.getElementById("ModalCustomerName");

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
    CustomerName.innerHTML = result.customerName;
    address.innerHTML = result.address;
    phone.innerHTML = result.phoneNo;
    email.innerHTML = result.email;
    comment.innerHTML = "";
    if (result.comments != null) {
        comment.innerHTML = result.comments;
    }

    if (result.hasPet) {
        dontPet.style.display = "none";
        pet.style.display = "block";
    }
    else {
        dontPet.style.display = "block";
        pet.style.display = "none";
    }
}

document.getElementById("serviceReqdetailsbtn").addEventListener("click", function () {
    getAllServiceDetails();
});

/*=========== Cancel Service Requests ======================*/

document.getElementById("SPCancelRequestBtn").addEventListener("click", function () {

    var ServiceRequestId = document.getElementById("CancelRequestId").value;
    var Comments = document.getElementById("cancelReason").value;
    var data = {};

    data.serviceRequestId = ServiceRequestId;
    data.comments = Comments;

    $.ajax({
        type: 'POST',
        url: '/ServicePro/CancelServiceRequest',
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