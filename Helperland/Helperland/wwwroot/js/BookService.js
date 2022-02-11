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


$(document).on("keydown", ":input:not(textarea)", function (event) {
    if (event.key == "Enter") {
        event.preventDefault();
    }
});