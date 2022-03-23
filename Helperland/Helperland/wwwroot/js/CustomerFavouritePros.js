
function getFavouritePros() {

    $.ajax({
        type: 'GET',
        url: "/CustomerPage/GetFavouritePros",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        success: function (result) {
            if (result != "false") {

                $("#customerFavProTbody").empty();
                for (let i = 0; i < result.length; i++) {

                    var favBtnLabel = "Favourite";
                    var blockBtnLabel = "Block";

                    if (result[i].isBlock == true) {
                        blockBtnLabel = "Unblock";
                    }

                    if (result[i].isFav == true) {
                        favBtnLabel = "Remove";
                    }

                    var avatar = "cap.jpg";
                    if (result[i].avatar != null) {
                        avatar = result[i].avatar;
                    }

                    $("#customerFavProTbody").append(`<tr>
                <td><img src="/img/`+avatar+`"/></td>
                <td>
                    <span>`+result[i].spName+`</span>

                    <div class="d-flex justify-content-center">
                        <div class="spStars" id="favProRating`+result[i].spId+`">
                            <span class="stars"></span><span class="stars"></span><span class="stars"></span><span class="stars"></span><span class="stars"></span><span class="stars"></span><span class="stars"></span><span class="stars"></span><span class="stars"></span><span class="stars"></span>
                        </div>
                        <div><span class="ms-2 SP-stars">`+result[i].spRatings+`</span></div>
                    </div>

                   <p>`+result[i].cleanings+` Cleanings</p>
                </td>
                <td>
                    <button class="favouriteBtn" data-value="`+result[i].spId+`">`+ favBtnLabel + `</button>
                    <button style="background-color:#FF6B6B" class="blockBtn" data-value="`+ result[i].spId +`">`+ blockBtnLabel +`</button>
                </td>
            </tr>`);

                    starAsperRating(result[i].spRatings, "#favProRating"+result[i].spId);
                }

            }
            else {
                alert("something went wrong");
            }
        },
        error: function (error) {

        },

        beforeSend: function () {
            $("#loadingAnimation").removeClass("d-none");

        },

        complete: function () {
            setTimeout(function () {
                $("#loadingAnimation").addClass("d-none");
            }, 500);
        },
    });
}


/*==== favourite and block click ======*/
$("#customerFavProTbody").click(function (e) {

    if (e.target.classList.contains("favouriteBtn")) {
        console.log(e.target.dataset.value);
        favouriteSp(e.target.dataset.value);
    }

    if (e.target.classList.contains("blockBtn")) {
        console.log(e.target.dataset.value);
        blockSp(e.target.dataset.value);
    }

});

/*=== favourite sp ===*/
function favouriteSp(spId) {
    var data = {};
    data.spId = spId;

    $.ajax({
        type: 'POST',
        url: "/CustomerPage/SetFavouritePro",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: data,
        success: function (result) {
            if (result.value == "true") {

                getFavouritePros();
            }
            else {
                alert("something went wrong");
            }
        },
        error: function (error) {

        },

        beforeSend: function () {
            $("#loadingAnimation").removeClass("d-none");

        },

        complete: function () {
            $("#loadingAnimation").addClass("d-none");

        },
    });
    
}

/*=== Block SP function ===*/
function blockSp(spId) {
    var data = {};
    data.spId = spId;

    $.ajax({
        type: 'POST',
        url: "/CustomerPage/SetBlockPro",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: data,
        success: function (result) {
            if (result.value == "true") {

                getFavouritePros();
            }
            else {
                alert("something went wrong");
            }
        },
        error: function (error) {

        },

        beforeSend: function () {
            $("#loadingAnimation").removeClass("d-none");

        },

        complete: function () {
            $("#loadingAnimation").addClass("d-none");

        },
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