
function getFavouritePros() {

    $.ajax({
        type: 'GET',
        url: "/CustomerPage/GetFavouritePros",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        success: function (result) {
            if (result != "false") {

            }
            else {
                alert("something went wrong");
            }
        },
        error: function (error) {

        }
    });

}
