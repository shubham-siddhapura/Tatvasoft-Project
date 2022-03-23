$(window).on("load", function () {

    $("#loadingAnimation").removeClass("d-none");

    setTimeout(function () {
        $("#loadingAnimation").addClass("d-none");
    }, 500);

});


var spDashbordTab = document.getElementById("spDashbordTab");
var spNewServiceRequestTab = document.getElementById("spNewServiceRequestTab");
var spUpcomingServiceTab = document.getElementById("spUpcomingServiceTab");
var spServiceHistoryTab = document.getElementById("spServiceHistoryTab");
var spServiceScheduleTab = document.getElementById("spServiceScheduleTab");
var spMyRatingsTab = document.getElementById("spMyRatingsTab");
var spBlockCustomerTab = document.getElementById("spBlockCustomerTab");

var allSPTables = document.getElementsByClassName("tableContents");

var spTabs = document.getElementsByClassName("spTabs");

spNewServiceRequestTab.addEventListener("click", function () {
    showThisTab("newServiceRequest", "spNewServiceRequestTab");
});

spUpcomingServiceTab.addEventListener("click", function () {
    getSPUpcomingService();
    showThisTab("upcoming-table", "spUpcomingServiceTab");
});

spServiceHistoryTab.addEventListener("click", function () {
    getSPServiceHistory();
    showThisTab("SPServiceHistory", "spServiceHistoryTab");
});

spMyRatingsTab.addEventListener("click", function () {
    getSPRatings();
    showThisTab("SPRatings", "spMyRatingsTab");
});

spBlockCustomerTab.addEventListener("click", function () {
    getUsersForBlock();
    showThisTab("blockCustomer", "spBlockCustomerTab");
});

spServiceScheduleTab.addEventListener("click", function () {
    addServiceSchedule();
    showThisTab("ServiceSchedule", "spServiceScheduleTab");
});

function showThisTab(id, id2) {
    for (var i = 0; i < allSPTables.length; i++) {
        allSPTables[i].style.display = "none";
    }
    for (var i = 0; i < spTabs.length; i++) {
        spTabs[i].classList.remove("active");
    }

    document.getElementById(id2).classList.add("active");
   
    document.getElementById(id).style.display="block";
}
