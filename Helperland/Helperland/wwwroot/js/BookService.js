let ans1 = 0;
document.getElementById("faq-q1").onclick = function () {
    ans1 += 1;
    if (ans1 & 1) {
        document.getElementById("faq-ar-1").src = "/img/faq/down-arrow.png";
    }
    else {
        document.getElementById("faq-ar-1").src = "/img/faq/right-arrow.png";
    }
}

let ans2 = 0;
document.getElementById("faq-q2").onclick = function () {
    ans2 += 1;
    if (ans2 & 1) {
        document.getElementById("faq-ar-2").src = "/img/faq/down-arrow.png";
    }
    else {
        document.getElementById("faq-ar-2").src = "/img/faq/right-arrow.png";
    }
}

let ans3 = 0;
document.getElementById("faq-q3").onclick = function () {
    ans1 += 1;
    if (ans1 & 1) {
        document.getElementById("faq-ar-3").src = "/img/faq/down-arrow.png";
    }
    else {
        document.getElementById("faq-ar-3").src = "/img/faq/right-arrow.png";
    }
}


/**/

var insideCabinet = document.getElementById("insideCabinetCheck");
var insideFridge = document.getElementById("insideFridgeCheck");
var insideOven = document.getElementById("insideOvenCheck");
var laundry = document.getElementById("laundryCheck");
var interior = document.getElementById("interiorCheck");

function onInsideCabinet() {
    if (insideCabinet.checked) {
        document.getElementById("insideCabinetImg").src = "/img/3-green.png";
    }
    else {
        document.getElementById("insideCabinetImg").src = "/img/3.png";
    }
}

function onInsideFridge() {
    if (insideFridge.checked) {
        document.getElementById("insideFridgeImg").src = "/img/5-green.png";
    }
    else {
        document.getElementById("insideFridgeImg").src = "/img/5.png";
    }
}

function onInsideOven() {
    if (insideOven.checked) {
        document.getElementById("insideOvenImg").src = "/img/4-green.png";
    }
    else {
        document.getElementById("insideOvenImg").src = "/img/4.png";
    }
}

function onLaundry() {
    if (laundry.checked) {
        document.getElementById("laundryImg").src = "/img/2-green.png";
    }
    else {
        document.getElementById("laundryImg").src = "/img/2.png";
    }
}

function onInterior() {
    if (interior.checked) {
        document.getElementById("interiorImg").src = "/img/1-green.png";
    }
    else {
        document.getElementById("interiorImg").src = "/img/1.png";
    }
}

function writefDate() {
    document.getElementById("admin-sr-fdate").value = document.getElementById("admin-sr-fdatepicker").value;
}

/**/

var addAddressBtn = document.getElementById("addnewAddressA");
var addAddressDiv = document.getElementById("addAddressDiv");
var addStreetname = document.getElementById("addStreetname");
var addHouseno = document.getElementById("addHouseno");
var addPostalcode = document.getElementById("addPostalcode");
var addPhoneno = document.getElementById("addPhoneno");
var addCity = document.getElementById("addCity");
var invoiceAddress = document.getElementById("invoiceAddressDiv");


function addAddressdiv() {
    addAddressBtn.style.display = "none";
    addAddressDiv.style.display = "block";
    addPostalcode.value = document.getElementById("postalCode").value;
}

function displayInvoiceAd() {
    if (invoiceAddress.classList.contains("showAd")) {
        invoiceAddress.classList.remove("showAd");
    }
    else {
        invoiceAddress.classList.add("showAd");
    }
}

function cancelAddAddress() {
    addAddressBtn.style.display = "inline-block";
    addAddressDiv.style.display = "none";
    addStreetname.value = null;
    addHouseno.value = null;
    addPhoneno.value = null;
    addCity.value = '#';
    document.getElementById("addingNewAddress").checked = false;
}



/**/

var form1div = document.getElementById("form1");
var form2div = document.getElementById("form2");
var form3div = document.getElementById("form3");
var form4div = document.getElementById("form4");

var form1down = document.getElementById("form1Down");
var form2down = document.getElementById("form2Down");
var form3down = document.getElementById("form3Down");
var form4down = document.getElementById("form4Down");

var form1img = document.getElementById("form1Img");
var form2img = document.getElementById("form2Img");
var form3img = document.getElementById("form3Img");
var form4img = document.getElementById("form4Img");

var form1span = document.getElementById("form1Span");
var form2span = document.getElementById("form2Span");
var form3span = document.getElementById("form3Span");
var form4span = document.getElementById("form4Span");

var step1 = document.getElementById("setup-service");
var step2 = document.getElementById("schedule-service");
var step3 = document.getElementById("details-service");
var step4 = document.getElementById("payment-service");

step2.style.pointerEvents = "none";
step3.style.pointerEvents = "none";
step4.style.pointerEvents = "none";

function form1() {
    form1div.style.display = "block";
    form2div.style.display = "none";
    form3div.style.display = "none";
    form4div.style.display = "none";

    form1down.style.display = "block";
    form2down.style.display = "none";
    form3down.style.display = "none";
    form4down.style.display = "none";

    form2img.src = "/img/schedule.png";
    form3img.src = "/img/details.png";
    form4img.src = "/img/payment.png";

    form2span.style.color = "#646464";
    form3span.style.color = "#646464";
    form4span.style.color = "#646464";

    step2.style.backgroundColor = "#F3F3F3";
    step3.style.backgroundColor = "#F3F3F3";
    step4.style.backgroundColor = "#F3F3F3";

    step2.style.pointerEvents = "none";
    step3.style.pointerEvents = "none";
    step4.style.pointerEvents = "none";

}

function form2() {

    var now = new Date(new Date().getTime() + 24 * 60 * 60 * 1000).toISOString().slice(0, 10);
    $("#admin-sr-fdate").attr("min", now);
    form1div.style.display = "none";
    form2div.style.display = "block";
    form3div.style.display = "none";
    form4div.style.display = "none";

    form1down.style.display = "none";
    form2down.style.display = "block";
    form3down.style.display = "none";
    form4down.style.display = "none";

    form2img.src = "/img/schedule-white.png";
    form3img.src = "/img/details.png";
    form4img.src = "/img/payment.png";

    form2span.style.color = "#fff";
    form3span.style.color = "#646464";
    form4span.style.color = "#646464";

    step2.style.backgroundColor = "#1d7a8c";
    step3.style.backgroundColor = "#F3F3F3";
    step4.style.backgroundColor = "#F3F3F3";

    step1.style.borderRight = "1px solid #fff";
    step2.style.borderRight = "1px solid #c8c8c8";
    step3.style.borderRight = "1px solid #c8c8c8";

    step2.style.pointerEvents = "auto";
    step3.style.pointerEvents = "none";
    step4.style.pointerEvents = "none";
}

function form3() {
    form1div.style.display = "none";
    form2div.style.display = "none";
    form3div.style.display = "block";
    form4div.style.display = "none";

    form1down.style.display = "none";
    form2down.style.display = "none";
    form3down.style.display = "block";
    form4down.style.display = "none";

    form2img.src = "/img/schedule-white.png";
    form3img.src = "/img/details-white.png";
    form4img.src = "/img/payment.png";

    form2span.style.color = "#fff";
    form3span.style.color = "#fff";
    form4span.style.color = "#646464";

    step2.style.backgroundColor = "#1d7a8c";
    step3.style.backgroundColor = "#1d7a8c";
    step4.style.backgroundColor = "#F3F3F3";

    step1.style.borderRight = "1px solid #fff";
    step2.style.borderRight = "1px solid #fff";
    step3.style.borderRight = "1px solid #c8c8c8";

    step2.style.pointerEvents = "auto";
    step3.style.pointerEvents = "auto";
    step4.style.pointerEvents = "none";

    loadAddress();
}

function form4() {
    form1div.style.display = "none";
    form2div.style.display = "none";
    form3div.style.display = "none";
    form4div.style.display = "block";

    form1down.style.display = "none";
    form2down.style.display = "none";
    form3down.style.display = "none";
    form4down.style.display = "block";

    form2img.src = "/img/schedule-white.png";
    form3img.src = "/img/details-white.png";
    form4img.src = "/img/payment-white.png";

    form2span.style.color = "#fff";
    form3span.style.color = "#fff";
    form4span.style.color = "#fff";

    step2.style.backgroundColor = "#1d7a8c";
    step3.style.backgroundColor = "#1d7a8c";
    step4.style.backgroundColor = "#1d7a8c";

    step1.style.borderRight = "1px solid #fff";
    step2.style.borderRight = "1px solid #fff";
    step3.style.borderRight = "1px solid #fff";

}

const url = new URLSearchParams(window.location.search);


if (url == "validPostcode=true") {
    step2.pointerEvents = true;
    form2();
}

if (url == "details=true") {
    step3.pointerEvents = true;
    form3();
}

window.history.forward();
function noBack() {
    window.history.forward();
}

var paymentbill_date = document.getElementsByClassName("paymentbill_date");
var paymentbill_time = document.getElementsByClassName("paymentbill_time");
var paymentbill_basic = document.getElementsByClassName("paymentbill_basic");
var paymentbill_cabinet = document.getElementsByClassName("paymentbill_cabinet");
var paymentbill_fridge = document.getElementsByClassName("paymentbill_fridge");
var paymentbill_oven = document.getElementsByClassName("paymentbill_oven");
var paymentbill_laundry = document.getElementsByClassName("paymentbill_laundry");
var paymentbill_window = document.getElementsByClassName("paymentbill_window");
var paymentbill_totalhours = document.getElementsByClassName("paymentbill_totalhours");
var paymentbill_amount = document.getElementsByClassName("paymentbill_amount");

function paymentbill_making() {
    paymentbill_date[0].innerHTML = document.getElementById("admin-sr-fdate").value;
    paymentbill_date[1].innerHTML = document.getElementById("admin-sr-fdate").value;
    paymentbill_time[0].innerHTML = document.getElementById("startingtime").value;
    paymentbill_time[1].innerHTML = document.getElementById("startingtime").value;
    paymentbill_basic[0].innerHTML = document.getElementById("servicehours").value;
    paymentbill_basic[1].innerHTML = document.getElementById("servicehours").value;
    var cabinet = 0;
    if (document.getElementById("insideCabinetCheck").checked) {
        paymentbill_cabinet[0].classList.remove("d-none");
        paymentbill_cabinet[1].classList.remove("d-none");
        cabinet = 0.5;
    }
    else {
        paymentbill_cabinet[0].classList.add("d-none");
        paymentbill_cabinet[1].classList.add("d-none");
        cabinet = 0;
    }
    var fridge = 0;
    if (document.getElementById("insideFridgeCheck").checked) {
        paymentbill_fridge[0].classList.remove("d-none");
        paymentbill_fridge[1].classList.remove("d-none");
        fridge = 0.5;
    }
    else {
        paymentbill_fridge[0].classList.add("d-none");
        paymentbill_fridge[1].classList.add("d-none");
        fridge = 0;
    }
    var oven = 0;
    if (document.getElementById("insideOvenCheck").checked) {
        paymentbill_oven[0].classList.remove("d-none");
        paymentbill_oven[1].classList.remove("d-none");
        oven = 0.5;
    }
    else {
        paymentbill_oven[0].classList.add("d-none");
        paymentbill_oven[1].classList.add("d-none");
        oven = 0;
    }
    var laundry =0;
    if (document.getElementById("laundryCheck").checked) {
        paymentbill_laundry[0].classList.remove("d-none");
        paymentbill_laundry[1].classList.remove("d-none");
        laundry = 0.5;
    }
    else {
        paymentbill_laundry[0].classList.add("d-none");
        paymentbill_laundry[1].classList.add("d-none");
        laundry = 0;
    }
    var window = 0;
    if (document.getElementById("interiorCheck").checked) {
        paymentbill_window[0].classList.remove("d-none");
        paymentbill_window[1].classList.remove("d-none");
        window = 0.5;
    }
    else {
        paymentbill_window[0].classList.add("d-none");
        paymentbill_window[1].classList.add("d-none");
        window = 0;
    }

    paymentbill_totalhours[0].innerHTML = (parseFloat(document.getElementById("servicehours").value) + cabinet + fridge + oven + laundry + window);
    paymentbill_totalhours[1].innerHTML = (parseFloat(document.getElementById("servicehours").value) + cabinet + fridge + oven + laundry + window);
    var bill_amount = (parseFloat(document.getElementById("servicehours").value) + cabinet + fridge + oven + laundry + window) * 25;
    paymentbill_amount[0].innerHTML = bill_amount;

    paymentbill_amount[1].innerHTML = bill_amount;

    paymentbill_amount[2].innerHTML = bill_amount;

    paymentbill_amount[3].innerHTML = bill_amount;
}


$(document).on("keydown", ":input:not(textarea)", function (event) {
    if (event.key == "Enter") {
        event.preventDefault();
    }
});

function postalSubmit() {
    var data = $("#setupform").serialize();
    var postalcode = $("#postalCode").val().trim();
    console.log(postalcode)
    var isNum = /^[0-9]+$/.test(postalcode);
    if (postalcode == "") {
        $("#postalcodeAlert").removeClass("d-none").text("Please Enter Valid PostalCode!");
    }
    else if (!isNum) {
        $("#postalcodeAlert").removeClass("d-none").text("Postalcode must contains only numbers!");
    }
    else {

        $.ajax({
            type: 'POST',
            url: '/CustomerPage/Validpost',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: data,
            success: function (result) {
                $("#postalProgess").addClass("d-none");
                if (result.value == "true") {
                    $("#postalcodeAlert").addClass("d-none");
                    var cookie = document.cookie;
                    var output = {};
                    cookie.split(/\s*;\s*/).forEach(function (pair) {
                        pair = pair.split(/\s*=\s*/);
                        output[pair[0]] = pair.splice(1).join('=');
                    });
                    var json = JSON.stringify(output, null, 4);
                    
                    console.log(JSON.parse(json).city);
                    $("#addCity").val(JSON.parse(json).city);
                    form2();
                }
                else {
                    $("#postalcodeAlert").removeClass("d-none").text("Sorry, Service is not available for your area!");
                }
            },
            xhr: function () {
                var xhr = $.ajaxSettings.xhr();
                xhr.upload.onprogress = function (event) {
                    $("#postalProgess").removeClass("d-none");
                    console.log('progress', event.loaded / event.total * 100);
                }
                xhr.upload.onload = function () {

                    $("#postalProgess").removeClass("d-none");
                    console.log('DONE!');
                };
                return xhr;
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}

function scheduleSubmit() {
    var data = $("#scheduleform").serialize();
    console.log(data);

    var serviceDate = new Date($("#admin-sr-fdate").val() + " " + $("#startingtime").val()).getTime() / 1000;
    var todayDate = new Date().getTime() / 1000;

    if (serviceDate <= todayDate) {
        $("#scheduleServiceAlert").removeClass("d-none").text("Please Select Valid Date and Time!");
    }
    else {
        $.ajax({
            type: 'POST',
            url: '/CustomerPage/ScheduleService',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: data,
            success: function (result) {
                if (result.value == "true") {
                    $("#scheduleServiceAlert").addClass("d-none")
                    form3();
                }
                else {
                    $("#scheduleServiceAlert").removeClass("d-none").text("Please Select Valid Date and time!");
                }
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}

function loadAddress() {

    $.ajax({
        type: 'GET',
        url: '/CustomerPage/DetailsService',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',

        success: function (result) {
            var address = $("#addresses");
            address.empty();
            for (let i = 0; i < result.length; i++) {
                var checked = "";
                if (result[i].isDefault == true) {
                    checked = "checked";
                }
                address.append('<div><input type="radio" id="address' + i + '"' + checked + ' value="' + result[i].addressId + '" name="bookAddress"><label for="address' + i + '"><p><b>Address:</b> ' + result[i].addressLine2 + ' ' + result[i].addressLine1 + ', ' + result[i].city + ' ' + result[i].postalCode + '</p ><p><b>Phone number:</b> ' + result[i].mobile + '</p></label></div>');
                checked = "";
            }
            console.log(result);
        },
        error: function () {
            alert('Failed to receive the Data');
            console.log('Failed ');
        }
    });
}


function saveAddress() {
    var data = {};
    data.addressLine2 = document.getElementById("addStreetname").value;
    data.addressLine1 = document.getElementById("addHouseno").value;
    data.postalCode = document.getElementById("addPostalcode").value;
    data.city = document.getElementById("addCity").value;
    data.mobile = document.getElementById("addPhoneno").value;
    console.log(data);

    var numbers = /^[0-9]+$/.test(data.mobile);

    if (data.addressLine1 == "" && data.addressLine2 == "" && data.mobile == "") {
        $("#detailServiceAlert").removeClass("d-none").text("Please enter Address!");
    }
    else if (data.addressLine1.trim(" ") == "") {
        $("#detailServiceAlert").removeClass("d-none").text("Please enter value of Street!");
    } else if (data.addressLine2.trim(" ") == "") {
        $("#detailServiceAlert").removeClass("d-none").text("Please enter value of House no!");
    } else if (data.mobile.trim(" ") == "" || !numbers) {
        $("#detailServiceAlert").removeClass("d-none").text("Please enter value of Mobile!");
    }
    else {
        $.ajax({
            type: 'POST',
            url: '/CustomerPage/AddNewAddress',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: data,
            success: function (result) {
                if (result.value == "true") {
                    $("#detailServiceAlert").addClass("d-none");
                    form3();
                    cancelAddAddress();
                }
                else {
                    $("#detailServiceAlert").removeClass("d-none").text("Sorry! Something went wrong please try again later.");
                }
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}

function completeBookService() {
    var data = {};
    var extrahour = 0;
    var cabinet = document.getElementById("insideCabinetCheck");
    var window = document.getElementById("interiorCheck");
    var fridge = document.getElementById("insideFridgeCheck");
    var oven = document.getElementById("insideOvenCheck");
    var laundry = document.getElementById("laundryCheck");

    if (cabinet.checked == true) {
        extrahour += 0.5;
        data.cabinet = true;
    }
    if (window.checked == true) {
        extrahour += 0.5;
        data.window = true;
    }
    if (fridge.checked == true) {
        extrahour += 0.5;
        data.fridge = true;
    }
    if (oven.checked == true) {
        extrahour += 0.5;
        data.oven = true;
    }
    if (laundry.checked == true) {
        extrahour += 0.5;
        data.laundry = true;
    }
    data.postalCode = document.getElementById("postalCode").value;
    data.serviceStartDate = document.getElementById("admin-sr-fdate").value;
    data.serviceHours = document.getElementById("servicehours").value;
    data.extraHours = extrahour;
    data.subTotal = extrahour + document.getElementById("servicehours").value;
    data.totalCost = (extrahour + document.getElementById("servicehours").value) * 25; //25rs per hour
    data.comments = document.getElementById("comments").value;
    data.paymentDue = false;
    data.hasPets = document.getElementById("havepet").checked;
    data.paymentDone = true;

    data.addressId = $('#addresses div input[type=radio]:checked').val();

    console.log(data.addressId);
    $.ajax({
        type: 'POST',
        url: '/CustomerPage/CompleteBooking',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: data,
        success: function (result) {
            if (result.value == "false") {
                
                alert("schedule is not valid");

            }
            else {
                $("#modalServiceId").text("Service Id : " + result.value);
                $("#completebookingmodalbtn").click();
            }
        },

        error: function () {
            alert('Failed to receive the Data');
            console.log('Failed ');
        }
    });
}  