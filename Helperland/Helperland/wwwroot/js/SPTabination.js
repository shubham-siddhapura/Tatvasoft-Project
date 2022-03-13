
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


function showThisTab(id, id2) {
    for (var i = 0; i < allSPTables.length; i++) {
        allSPTables[i].classList.add("d-none");
    }
    for (var i = 0; i < spTabs.length; i++) {
        spTabs[i].classList.remove("active");
    }

    document.getElementById(id2).classList.add("active");
    document.getElementById(id).classList.remove("d-none");
}
