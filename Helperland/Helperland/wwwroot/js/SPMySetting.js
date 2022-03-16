$("#mySettingDob").dateDropdowns({
    submitFieldName: 'mySettingDob',
    minAge: 18,
    submitFormat: "dd/mm/yyyy"
});

var url = new URLSearchParams(window.location.search);

if (url == "mySetting=true") {
    console.log("hello");
    getSPDetails();
    showThisTab("mySetting", "mySetting");
}

var myDetailsTab = document.getElementById("myDetailsTab");
var changePasswordTab = document.getElementById("changePasswordTab");

var myDetails = document.getElementById("mySettingDetails");

var changePassword = document.getElementById("changePasswordMySetting");

myDetailsTab.addEventListener("click", mydetails);

changePasswordTab.addEventListener("click", changepassword);

function mydetails() {
    changePassword.classList.add("d-none");

    myDetails.classList.remove("d-none");

    myDetailsTab.classList.add("active");

    changePasswordTab.classList.remove("active");
}


function changepassword() {
    myDetails.classList.add("d-none");

    changePassword.classList.remove("d-none");

    myDetailsTab.classList.remove("active");
    changePasswordTab.classList.add("active");
}


var SPSelectedAvatar = document.getElementById("SPSelectedAvatar");

$("input[name=avatar]").change(function () {


    var imgName = $(".allAvatars input[name=avatar]:checked").val();

    SPSelectedAvatar.src = "/img/" + imgName;

});


/*======== all variables ============*/
var fname = document.getElementById("mySettingFname");
var lname = document.getElementById("mySettingLname");
var email = document.getElementById("mySettingEmail");
var mobile = document.getElementById("mySettingMobile");
var nationality = document.getElementById("mySettingNationality");
var street = document.getElementById("mySettingStreetname");
var houseNo = document.getElementById("mySettingHouse");
var postalCode = document.getElementById("mySettingPostal");
var city = document.getElementById("mySettingCity");
var state = document.getElementById("mySettingState");

/*====== get user details ========*/

function getSPDetails() {

    $.ajax({
        type: 'GET',
        url: '/ServicePro/GetSPDetails',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',

        success: function (result) {
            console.log(result);

              
            if (result == false) {
                alert("something went wrong");
            }
            else {
               
                fname.value = result.firstName;
                lname.value = result.lastName;
                email.value = result.email;
                mobile.value = result.mobile;
                nationality.value = result.nationalityId;

                if (result.dateOfBirth != null) {
                    var dateOfBirth = result.dateOfBirth.split('T');
                    var dateOfBirthArray = dateOfBirth[0].split("-");
                    console.log(result.dateOfBirth);
                    $(".day").val(dateOfBirthArray[2]);
                    $(".month").val(dateOfBirthArray[1]);
                    $(".year").val(dateOfBirthArray[0]);
                }

                if (result.userProfilePicture != null) {
                    $("input[name='avatar'][value='" + result.userProfilePicture + "']").prop("checked", true);
                    document.getElementById("SPSelectedAvatar").src = "/img/"+result.userProfilePicture;
                }

                if (result.gender != null) {
                    $("input[name='gender'][value='" + result.gender + "']").prop("checked", true);
                }

                console.log(result.userAddresses.length);

                if (result.userAddresses.length > 0) {
                    street.value = result.userAddresses[0].addressLine2;
                    houseNo.value = result.userAddresses[0].addressLine1;
                    postalCode.value = result.userAddresses[0].postalCode;
                    city.value = result.userAddresses[0].city;
                    
                    state.value = result.userAddresses[0].state;
                }
                
            }
        },
        error: function () {
            alert("error");
        }
    });


}

document.getElementById("myDetailsSave").addEventListener("click", function () {
    updateSPDetails();
});

function updateSPDetails() {

    var gender = $("input[name='gender']:checked").val();
    var avatar = $("input[name='avatar']:checked").val();


    var data = {};
    data.firstName = fname.value.trim();
    data.lastName = lname.value.trim();
    data.mobile = mobile.value.trim();
    data.dateOfBirth = $(".day").val() + "-" + $(".month").val() + "-" + $(".year").val();

    console.log(data.dateOfBirth);
    data.nationality = nationality.value.trim();
    data.gender = gender;
    data.userProfilePicture = avatar;

    console.log(avatar);
    console.log(gender)

    var addressData = {};

    addressData.addressLine2 = street.value;
    addressData.addressLine1 = houseNo.value;
    addressData.postalCode = postalCode.value;
    addressData.city = city.value;
    addressData.state = state.value;

    data.userAddresses = [addressData];
    
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
    else if (data.addressLine1 == "") {
        $("#ms_myDetailsAlert").removeClass("alert-success d-none").addClass("alert-danger").text("House number is Required.");
        $("#mySettingHouse").focus();
    }
    else if (data.addressLine2 == "") {
        $("#ms_myDetailsAlert").removeClass("alert-success d-none").addClass("alert-danger").text("House number is Required.");
        $("#mySettingStreetname").focus();
    }
    else if (data.postalCode == "") {
        $("#ms_myDetailsAlert").removeClass("alert-success d-none").addClass("alert-danger").text("House number is Required.");
        $("#mySettingPostal").focus();
    }
    else {
        $.ajax({

            type: "POST",
            url: "/ServicePro/UpdateSPDetails",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            data: data,
            success: function (result) {
                if (result.value == "true") {
                    getSPDetails();
                    $("#ms_myDetailsAlert").removeClass("alert-danger d-none").addClass("alert-success").text("Details are updated successfully.");
                    
                }
                else if (result.value == "mobile") {
                    
                    $("#ms_myDetailsAlert").removeClass("alert-success d-none").addClass("alert-danger").text("Mobile number is already taken by other user, please try with new number.");
                }
                else {
                    $("#ms_myDetailsAlert").removeClass("alert-success d-none").addClass("alert-danger").text("Something went wrong, please try again later.");
                }
            },
            error: function (error) {

            }


        });
    }
}

/*========  get city from postal code =================*/
$("#mySettingPostal").keyup(function () {
    
    if ($("#mySettingPostal").val().length == 6) {
        getCityFromPostalCode($("#mySettingPostal").val());
    }
});


function getCityFromPostalCode(zip) {
    $.ajax({
        method: "GET",
        url: "https://api.postalpincode.in/pincode/" + zip,
        dataType: 'json',
        cache: false,
        success: function (result) {
            if (result[0].status == "Error" || result[0].status == "404") {
                $("#ms_myDetailsAlert").removeClass("alert-success d-none").addClass("alert-danger").text("Enter Valid PostalCode.");
            }
            else {
                console.log(result);
                $("#mySettingCity").val(result[0].PostOffice[0].District);
                $("#mySettingState").val(result[0].PostOffice[0].State);
                $("#mySettingCity").prop("disabled", true);
            }
        },
        error: function (error) {

        }
    });
}

document.getElementById("changePasswordBtn").addEventListener("click", function () {
    changeSPPassword();
});

function changeSPPassword() {
    var data = {};
    data.oldPassword = document.getElementById("oldPassword").value;
    data.newPassword = document.getElementById("newPassword").value;
    data.confirmPassword = document.getElementById("confirmPassword").value;

    window.setTimeout(function () {
        $('#ms_changePasswordAlert').addClass('d-none');
    }, 5000);

    if (data.oldPassword == "" && data.newPassword == "" && data.confirmPassword == "") {
        $("#ms_changePasswordAlert").removeClass("alert-success d-none").addClass("alert-danger").text("All Fields are Required.");
        $("#oldPassword").focus();
    }
    else if (data.oldPassword == "") {
        $("#ms_changePasswordAlert").removeClass("alert-success d-none").addClass("alert-danger").text("Old password is Required.");
        $("#oldPassword").focus();
    }
    else if (data.newPassword == "") {
        $("#ms_changePasswordAlert").removeClass("alert-success d-none").addClass("alert-danger").text("New password is Required.");
        $("#newPassword").focus();
    }
    else if (data.confirmPassword == "") {
        $("#ms_changePasswordAlert").removeClass("alert-success d-none").addClass("alert-danger").text("Confirm password is Required.");
        $("#confirmPassword").focus();
    }
    else if (data.newPassword != data.confirmPassword) {
        $("#ms_changePasswordAlert").removeClass("alert-success d-none").addClass("alert-danger").text("New Password and Confirm Password must be same.");
        $("#confirmPassword").focus();
    }
    else {
        $.ajax({
            type: "POST",
            url: "/ServicePro/ChangePassword",
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

