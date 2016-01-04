var DNMC_pointer = 1;


function RenderLayout() {

    var margin_left = 0;
    var wwidth = $(window).width();
    $("#header_Bar").css("width", wwidth + "px");

    /*
    if ($(window).width() > 400) {
        var margin_left = $(window).width() - 400 - 46; //44
        var wwidth = $(window).width() - (400 + 30 + (margin_left / 2) - 15);
        $("#header_Bar").css("width", wwidth + "px");
    }
    else {
        $("#header_Bar").css("width", 20 + "px");
    }
    */
}




function RenderPage() {

    var margin_left = 0;
    var cont_width = 400;


    
    margin_left = 0;
    cont_width = $(window).width();
    

    /*
    if ($(window).width() > 400) {
        margin_left = $(window).width() - 400 - 46; //44
        cont_width = 400 + 30 + (margin_left / 2) - 15;
    } else {
        margin_left = 0;
        cont_width = 400;
    }*/

    //===================================================================
    $(".slideRight").css("float", "right");
    $(".slideRight").css("width", cont_width + "px");
    //===================================================================

    //===================================================================
    $(".slideLeft").css("float", "left");
    $(".slideLeft").css("width", (cont_width-8) + "px");
    //===================================================================
}

//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






//---------------------------------------------------------------------------------------------//
function RenderHome() {
    var margin_left = 0;
    var cont_width = 400;


    margin_left = 0;
    cont_width = $(window).width();


    /*
    if ($(window).width() > 400) {
        margin_left = $(window).width() - 400 - 46; //44
        cont_width = 400 + 30 + (margin_left / 2) - 15;
    }else {
        margin_left = 0;
        cont_width = 400;
    }
    */

    /*
    if ($(window).width() > 1190) { $("#ContNextItm").show("slow"); } else { $("#ContNextItm").css("display", "none"); }


    $("#listenSlide1_search").css("width", (cont_width - 560 - 20) + "px");
    $("#listenSlide3_cont").css("width", (cont_width - 440 - 180) + "px");


    $('.middleC1').mouseover(function () { $('.middleC1').css("background-color", "#292e30"); });
    $('.middleC1').mouseout(function () { $('.middleC1').css("background-color", "#2e3335"); });

    $('.middleC2').mouseover(function () { $('.middleC2').css("background-color", "#292e30"); });
    $('.middleC2').mouseout(function () { $('.middleC2').css("background-color", "#2e3335"); });

    $('.middleC3').mouseover(function () { $('.middleC3').css("background-color", "#292e30"); });
    $('.middleC3').mouseout(function () { $('.middleC3').css("background-color", "#2e3335"); });
    */


    //StartStopSlideshow();


    /*
    DNMC_pointer = 1;
    $(window).scroll(function () {
        if ($(window).scrollTop() + $(window).height() == $(document).height()) {

            $.ajax({
                type: "POST",
                url: "/home/Get_More_Content/" + DNMC_pointer,
                async: true,
                success: function (data) {
                    DNMC_pointer += 1;
                    $("#DynamicContentHoler").append(data);
                }
            });
        }
    });
    */
}

function ReRenderHome() {
    var margin_left = 0;
    var cont_width = 400;


    margin_left = 0;
    cont_width = $(window).width();

    /*
    if ($(window).width() > 400) {
        margin_left = $(window).width() - 400 - 46; //44
        cont_width = 400 + 30 + (margin_left / 2) - 15;
    } else {
        margin_left = 0;
        cont_width = 400;
    }
    */

    if ($(window).width() > 1190) { $("#ContNextItm").show("slow"); } else { $("#ContNextItm").css("display", "none"); }


    $("#listenSlide1_search").css("width", (cont_width - 560 - 20) + "px");
    $("#listenSlide3_cont").css("width", (cont_width - 440 - 180) + "px");
}
//---------------------------------------------------------------------------------------------//








//---------------------------------------------------------------------------------------------//
function RenderListen() {
    
    /*
    DNMC_pointer = 1;
    $(window).scroll(function () {
        if ($(window).scrollTop() + $(window).height() == $(document).height()) {

            $.ajax({
                type: "POST",
                url: "/home/Get_More_Content/" + DNMC_pointer,
                async: true,
                success: function (data) {
                    DNMC_pointer += 1;
                    $("#DynamicContentHoler").append(data);
                }
            });
        }
    });
    */

}

function ReRenderListen() {
}
//---------------------------------------------------------------------------------------------//




//---------------------------------------------------------------------------------------------//
function RenderCreate() {
    
    /*
    DNMC_pointer = 1;
    $(window).scroll(function () {
        if ($(window).scrollTop() + $(window).height() == $(document).height()) {

            $.ajax({
                type: "POST",
                url: "/home/Get_More_Content/" + DNMC_pointer,
                async: true,
                success: function (data) {
                    DNMC_pointer += 1;
                    $("#DynamicContentHoler").append(data);
                }
            });
        }
    });
    */

}

function ReRenderCreate() {
}
//---------------------------------------------------------------------------------------------//







//---------------------------------------------------------------------------------------------//
function RenderConnect() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();


    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46; //44
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    } else {
        margin_left = 0;
        cont_width = 1024;
    }
    */

    $(".ffHeader").css("width", (cont_width - 770) + "px");


    /*
    DNMC_pointer = 1;
    $(window).scroll(function () {
        if ($(window).scrollTop() + $(window).height() == $(document).height()) {

            $.ajax({
                type: "POST",
                url: "/home/Get_More_Content/" + DNMC_pointer,
                async: true,
                success: function (data) {
                    DNMC_pointer += 1;
                    $("#DynamicContentHoler").append(data);
                }
            });
        }
    });
    */

}

function ReRenderConnect() {
    var margin_left = 0;
    var cont_width = 1024;


    margin_left = 0;
    cont_width = $(window).width();

    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46; //44
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    } else {
        margin_left = 0;
        cont_width = 1024;
    }
    */

    $(".ffHeader").css("width", (cont_width - 770) + "px");
}
//---------------------------------------------------------------------------------------------//









function RenderShare() {
    if ($(window).width() > 1190) {
        $("#ShrContH1").css("display", "block");
    }
    else {
        $("#ShrContH1").css("display", "none");
    }
 }






function RenderCharts() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();

    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46; //44
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    } else {
        margin_left = 0;
        cont_width = 1024;
    }
    */


    //===================================================================
    $(".slideRight").css("float", "right");
    $(".slideRight").css("width", cont_width + "px");
    //===================================================================
    //===================================================================
    $(".slideLeft").css("float", "left");
    $(".slideLeft").css("width", cont_width + "px");
    //===================================================================
}


function RenderFestivals() {
    var margin_left = 0;
    var cont_width = 1024;


    margin_left = 0;
    cont_width = $(window).width();

    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46; //44
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    } else {
        margin_left = 0;
        cont_width = 1024;
    }
    */


    //===================================================================
    $(".slideRight").css("float", "right");
    $(".slideRight").css("width", cont_width + "px");
    //===================================================================
    //===================================================================
    $(".slideLeft").css("float", "left");
    $(".slideLeft").css("width", cont_width + "px");
    //===================================================================
}







function RenderRadio() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();

    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46; //44
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    }else {
        margin_left = 0;
        cont_width = 1024;
    }
    */



    $("#listenSlide1").css("float", "right");
    $("#listenSlide1").css("width", cont_width + "px");
    $("#listenSlide2").css("float", "left");
    $("#listenSlide2").css("width", cont_width + "px");
}






function RenderCreatePlayer() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();

    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46; //44
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    }else {
        margin_left = 0;
        cont_width = 1024;
    }
    */

    $("#listenSlide1").css("float", "right");
    $("#listenSlide1").css("width", cont_width + "px");
    $("#listenSlide2").css("float", "left");
    $("#listenSlide2").css("width", cont_width + "px");
}


function RenderCreatePlaylist() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();

    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46; //44
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    }else {
        margin_left = 0;
        cont_width = 1024;
    }
    */

    $("#listenSlide1").css("float", "right");
    $("#listenSlide1").css("width", cont_width + "px");
    $("#listenSlide2").css("float", "left");
    $("#listenSlide2").css("width", cont_width + "px");
}



function RenderCreateStation() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();

    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46; //44
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    }else {
        margin_left = 0;
        cont_width = 1024;
    }
    */

    $("#listenSlide1").css("float", "right");
    $("#listenSlide1").css("width", cont_width + "px");
    $("#listenSlide2").css("float", "left");
    $("#listenSlide2").css("width", cont_width + "px");
}




function RenderCreateStation() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();

    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46; //44
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    }else {
        margin_left = 0;
        cont_width = 1024;
    }
    */

    $("#listenSlide1").css("float", "right");
    $("#listenSlide1").css("width", cont_width + "px");
    $("#listenSlide2").css("float", "left");
    $("#listenSlide2").css("width", cont_width + "px");
}











function RenderAboutUs() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();

    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46; //44
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    }else {
        margin_left = 0;
        cont_width = 1024;
    }
    */
    $("#listenSlide1").css("float", "right");
    $("#listenSlide1").css("width", cont_width + "px");
}

function RenderContactUs() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();

    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46; //44
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    }else {
        margin_left = 0;
        cont_width = 1024;
    }
    */
    $("#listenSlide1").css("float", "right");
    $("#listenSlide1").css("width", cont_width + "px");
}

function RenderPrivacyPolicy() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();

    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46; //44
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    }else {
        margin_left = 0;
        cont_width = 1024;
    }
    */
    $("#listenSlide1").css("float", "right");
    $("#listenSlide1").css("width", cont_width + "px");
}


function RenderTermsOfService() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();


    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46; //44
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    }else {
        margin_left = 0;
        cont_width = 1024;
    }
    */


    $("#listenSlide1").css("float", "right");
    $("#listenSlide1").css("width", cont_width + "px");
}







function RenderSong() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();

    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46;
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    }else {
        margin_left = 0;
        cont_width = 1024;
    }
    */

    //===================================================================
    $("#listenSlide1").css("float", "right");
    $("#listenSlide1").css("width", cont_width + "px");
    //===================================================================
    //===================================================================
    $("#listenSlide2").css("float", "left");
    $("#listenSlide2").css("width", cont_width + "px");
    //===================================================================
}








/* +++++++++++++++++++ RENDER ACCOUNT +++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/


//
//+++++
//
function RenderAccountInfo() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();


    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46;
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    }else {
        margin_left = 0;
        cont_width = 1024;
    }
    */

    if ($(window).height() < 750) {
        $("#SectionMenu1").css("height", "130px");
        $("#SectionMenu2").css("height", "130px");
        $("#SectionMenu3").css("height", "130px");
    }

}



//
//+++++
//
function RenderAccountMusic() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();

    
    /*    
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46;
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    }else {
        margin_left = 0;
        cont_width = 1024;
    }
    */

    if ($(window).height() < 750) {
        $("#SectionMenu1").css("height", "130px");
        $("#SectionMenu2").css("height", "130px");
        $("#SectionMenu3").css("height", "130px");
    }

}


//
//+++++
//
function RenderAccountFriends() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();

    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46;
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    }else {
        margin_left = 0;
        cont_width = 1024;
    }
    */

    if ($(window).height() < 750) {
        $("#SectionMenu1").css("height", "130px");
        $("#SectionMenu2").css("height", "130px");
        $("#SectionMenu3").css("height", "130px");
    }

}


function RenderAccountPics() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();

    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46;
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    }else {
        margin_left = 0;
        cont_width = 1024;
    }
    */


    $("#listenSlide1").css("float", "right");
    $("#listenSlide1").css("width", cont_width + "px");
    
}


function RenderForgotPassword() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();

    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46;
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    }else {
        margin_left = 0;
        cont_width = 1024;
    }
    */

    //===================================================================
    $("#listenSlide1").css("float", "right");
    $("#listenSlide1").css("width", cont_width + "px");
    //===================================================================
}




function RenderRegister() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();

    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46;
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    }else {
        margin_left = 0;
        cont_width = 1024;
    }
    */

    $("#listenSlide1").css("float", "right");
    $("#listenSlide1").css("width", cont_width + "px");
    $("#listenSlide2").css("float", "left");
    $("#listenSlide2").css("width", cont_width + "px");
}


function RenderSignIn() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();

    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46;
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    }else {
        margin_left = 0;
        cont_width = 1024;
    }
    */

    $("#listenSlide1").css("float", "right");
    $("#listenSlide1").css("width", cont_width + "px");
    $("#listenSlide2").css("float", "left");
    $("#listenSlide2").css("width", cont_width + "px");
}

/* +++++++++++++++++++ RENDER ACCOUNT +++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/









/* +++++++++++++++++++ RENDER ACCOUNT PUBLIC +++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

function RenderAccountPublic() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();
     
        
    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46;
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    }else {
        margin_left = 0;
        cont_width = 1024;
    }
    */


    if ($(window).height() < 750) {
        $("#PublicPSectionMenu1").css("height", "130px");
        $("#PublicPSectionMenu2").css("height", "130px");
        $("#PublicPSectionMenu3").css("height", "130px");
    }


}


/* +++++++++++++++++++ RENDER ACCOUNT PUBLIC +++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/


function RenderError() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();

    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46;
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    } else {
        margin_left = 0;
        cont_width = 1024;
    }
    */


    $("#listenSlide1").css("float", "right");
    $("#listenSlide1").css("width", cont_width + "px");
}




function RenderHowToUse() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();


    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46;
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    } else {
        margin_left = 0;
        cont_width = 1024;
    }
    */

    $("#listenSlide0").css("float", "right");
    $("#listenSlide0").css("width", cont_width + "px");
    $("#listenSlide1").css("float", "left");
    $("#listenSlide1").css("width", cont_width + "px");
    $("#listenSlide2").css("float", "right");
    $("#listenSlide2").css("width", cont_width + "px");
    $("#listenSlide3").css("float", "left");
    $("#listenSlide3").css("width", cont_width + "px");
    $("#listenSlide4").css("float", "right");
    $("#listenSlide4").css("width", cont_width + "px");
    $("#listenSlide5").css("float", "left");
    $("#listenSlide5").css("width", cont_width + "px");
}



function RenderHypsterOnMobile() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();


    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46;
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    } else {
        margin_left = 0;
        cont_width = 1024;
    }
    */


    $("#listenSlide0").css("float", "right");
    $("#listenSlide0").css("width", cont_width + "px");
    $("#listenSlide1").css("float", "left");
    $("#listenSlide1").css("width", cont_width + "px");
    $("#listenSlide2").css("float", "right");
    $("#listenSlide2").css("width", cont_width + "px");
}



function RenderRemoveAccount() {
    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();

    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46;
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    } else {
        margin_left = 0;
        cont_width = 1024;
    }
    */

    $("#listenSlide0").css("float", "right");
    $("#listenSlide0").css("width", cont_width + "px");
}








//========================================================================
// Public Page
//========================================================================


function RenderPagePP() {

    var margin_left = 0;
    var cont_width = 1024;

    margin_left = 0;
    cont_width = $(window).width();


    /*
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 46;
        cont_width = 1024 + 30 + (margin_left / 2) - 15;
    } else {
        margin_left = 0;
        cont_width = 1024;
    }
    */
    
    $(".slideRightPP").css("float", "right");
    $(".slideRightPP").css("width", cont_width + "px");
    

    $(".slideLeftPP").css("float", "left");
    $(".slideLeftPP").css("width", cont_width + "px");
}





function AlignSimilarTHeader() {
/*
    if ($(window).width() > 1024) {
        var margin_left = $(window).width() - 1024 - 30;
        var cont_width = 1024 + 30 + (margin_left / 2) - 15;
        $("#CompToolHeader2_header").css("width", (cont_width - 650 - 20) + "px");
    }
*/
}



function AlignAcctInfoHeader() {
/*
    if ($(window).width() > 1024) {
        var margin_left = $(window).width() - 1024 - 30;
        var cont_width = 1024 + 30 + (margin_left / 2) - 15;
        $("#PopularPlaylistsSlide2_header").css("width", (cont_width - 730 - 20) + "px");
    }
*/
}