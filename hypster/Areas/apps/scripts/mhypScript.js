var myWidth = 0;
var myHeight = 0;
var active_menu = "";

$(document).ready(function () {

    Calculate_Screen_Dimensions();

    Adjust_Hide_Back_Div();






    //------------------------------------------------------------------------------------------------------------------
    $(".txtLogin").focus(function () {
        $(this).css("background-color", "#fdf1b0");
        $(this).css("font-weight", "bold");
    });

    $(".txtLogin").blur(function () {
        $(this).css("background-color", "#EEEEEE");
        $(this).css("font-weight", "normal");
    });
    //------------------------------------------------------------------------------------------------------------------

});



$(window).resize(function () {
    Adjust_Hide_Back_Div();
});




function subMouseOver(sender) {
    $(sender).css('background-color', '#173860');
}

function subMouseOut(sender) {
    $(sender).css('background-color', '');
}


function HDAM() {
    $('#HypsterSub').css('display', 'none');
    $('#ListenSub').css('display', 'none');
    $('#CreateSub').css('display', 'none');
    $('#ConnectSub').css('display', 'none');
}





var isMobile = {
    Android: function () {
        return navigator.userAgent.match(/Android/i);
    },
    BlackBerry: function () {
        return navigator.userAgent.match(/BlackBerry/i);
    },
    iOS: function () {
        return navigator.userAgent.match(/iPhone|iPad|iPod/i);
    },
    Opera: function () {
        return navigator.userAgent.match(/Opera Mini/i);
    },
    Windows: function () {
        return navigator.userAgent.match(/IEMobile/i);
    },
    any: function () {
        return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
    }
};

//------------------------------------------------------------------------------------------------------------------












//------------------------------------------------------------------------------------------------------------------
var player_ins = null;
function OpenExsPlayer(user_name) {
    if (player_ins != null) {
        player_ins.focus();
    }
    else {
        //OpenPlayer("media_type=DEFPL");

        var player_W = 1210;
        var player_H = 710;

        if (myWidth < 1210)
            player_W = myWidth;

        if (myHeight < 710) {
            player_H = myHeight;
            player_W = 1210;
        }

        //var myWindow;
        //myWindow = window.open('/m/mplaylists/user/' + user_name, 'HypsterPlayer', 'location=0, status=0, scrollbars=1, width=' + player_W + ', height=' + player_H);
        //myWindow.focus();

        window.location = '/m/mplaylists/user/' + user_name;
    }
}
//------------------------------------------------------------------------------------------------------------------






//------------------------------------------------------------------------------------------------------------------


function OpenPlayer(params) {
    var player_W = 1210;
    var player_H = 710;

    if (myWidth < 1210)
        player_W = myWidth;

    if (myHeight < 710) {
        player_H = myHeight;
        player_W = 1210;
    }

    //var myWindow;
    //myWindow = window.open('/hypsterPlayer/MPL?' + params, 'HypsterPlayer', 'location=0, status=0, scrollbars=1, width=' + player_W + ', height=' + player_H);
    //myWindow.focus();

    window.location = '/hypsterPlayer/MPL?' + params;
}

//new
function OpenPlayerM(params) {
    var player_W = 1210;
    var player_H = 710;

    if (myWidth < 1210)
        player_W = myWidth;

    if (myHeight < 710) {
        player_H = myHeight;
        player_W = 1210;
    }

    
    //var myWindow;
    //myWindow = window.open(params, 'HypsterPlayer', 'location=0, status=0, scrollbars=1, width=' + player_W + ', height=' + player_H);
    //myWindow.focus();

    window.location = params;
}





function OPLRPlst(p_pl_id, p_us_id) {
    var player_W = 1210;
    var player_H = 710;

    if (myWidth < 1210)
        player_W = myWidth;

    if (myHeight < 710) {
        player_H = myHeight;
        player_W = 1210;
    }

    //var myWindow;
    //myWindow = window.open('/hypsterPlayer/MPL?media_type=playlist&playlist_id=' + p_pl_id + '&us_id=' + p_us_id, 'HypsterPlayer', 'location=0, status=0, scrollbars=1, width=' + player_W + ', height=' + player_H);
    //myWindow.focus();

    window.location = '/hypsterPlayer/MPL?media_type=playlist&playlist_id=' + p_pl_id + '&us_id=' + p_us_id;
}


//Open player radio
function OPLRRadio(p_genre) {
    var player_W = 1210;
    var player_H = 710;

    if (myWidth < 1210)
        player_W = myWidth;

    if (myHeight < 710) {
        player_H = myHeight;
        player_W = 1210;
    }


    var genre_str = p_genre.replace('&', '_and_').replace('?', ' ').replace('%', ' ').replace('*', ' ').replace('#', ' ');


    //var myWindow;
    ////myWindow = window.open('/hypsterPlayer/MPL?media_type=Radio&Genre=' + p_genre, 'HypsterPlayer', 'location=0, status=0, scrollbars=1, width=' + player_W + ', height=' + player_H);
    //myWindow = window.open('/m/mplaylists/station/' + encodeURIComponent(genre_str), 'HypsterPlayer', 'location=0, status=0, scrollbars=1, width=' + player_W + ', height=' + player_H);
    //myWindow.focus();

    window.location = '/m/mplaylists/station/' + encodeURIComponent(genre_str);
}



function oMPL(pl_id, us_id) {
    var player_W = 1210;
    var player_H = 710;

    if (myWidth < 1210)
        player_W = myWidth;

    if (myHeight < 710) {
        player_H = myHeight;
        player_W = 1210;
    }

    //var myWindow;
    //myWindow = window.open('/hypsterPlayer/MPL?media_type=playlist&playlist_id=' + pl_id + '&us_id=' + us_id, 'HypsterPlayer', 'location=0, status=0, scrollbars=1, width=' + player_W + ', height=' + player_H);
    //myWindow.focus();

    window.location = '/hypsterPlayer/MPL?media_type=playlist&playlist_id=' + pl_id + '&us_id=' + us_id;
}


function OpenPlsts(params) {
    //window.location = '/hypsterPlayer/MPL?' + params;
    //window.location = '/m/mplaylists/user/' + params;

    var player_W = 1210;
    var player_H = 710;

    if (myWidth < 1210)
        player_W = myWidth;

    if (myHeight < 710) {
        player_H = myHeight;
        player_W = 1210;
    }


    //var myWindow;
    //myWindow = window.open('/m/mplaylists/user/' + params, 'HypsterPlayer', 'location=0, status=0, scrollbars=1, width=' + player_W + ', height=' + player_H);
    //myWindow.focus();

    window.location = '/m/mplaylists/user/' + params;
}



function OpenSong(song_guid, title) {
    var player_W = 1210;
    var player_H = 710;

    if (myWidth < 1210)
        player_W = myWidth;

    if (myHeight < 710) {
        player_H = myHeight;
        player_W = 1210;
    }

    var params = "song_guid=" + song_guid + "&song_title=" + title;

    //var myWindow;
    //myWindow = window.open('/hypsterPlayer/MPL?' + params, 'HypsterPlayer', 'location=0, status=0, scrollbars=1, width=' + player_W + ', height=' + player_H);
    //myWindow.focus();

    window.location = '/hypsterPlayer/MPL?' + params;
}



//hypDesktop
function OpenSong_D(song_guid, title) {
    var player_W = 1210;
    var player_H = 710;

    if (myWidth < 1210)
        player_W = myWidth;

    if (myHeight < 710) {
        player_H = myHeight;
        player_W = 1210;
    }

    var params = "song_guid=" + song_guid + "&song_title=" + title;

    window.location = '/hypsterPlayer/MPL_D?' + params, 'HypsterPlayer', 'location=0, status=0, scrollbars=1, width=' + player_W + ', height=' + player_H;

}



function CustomRadioStation() {
    var station_name = "";
    station_name = $("#stationName").val();
    station_name = station_name.replace('&', 'and').replace('?', ' ').replace('%', ' ').replace('*', ' ').replace('#', ' ');


    var player_W = 1210;
    var player_H = 710;

    if (myWidth < 1210)
        player_W = myWidth;

    if (myHeight < 710) {
        player_H = myHeight;
        player_W = 1210;
    }


    //var myWindow;
    //myWindow = window.open('/m/mplaylists/station/' + station_name, 'HypsterPlayer', 'location=0, status=0, scrollbars=1, width=' + player_W + ', height=' + player_H);
    //myWindow.focus();

    ////OpenPlayer('media_type=Radio&Genre=' + station_name + "&search=1");


    window.location = '/m/mplaylists/station/' + station_name;
}

//****************************************************************************************














//****************************************************************************************

function Calculate_Screen_Dimensions() {
    myWidth = $(window).width();
    myHeight = $(window).height();
}



function Adjust_Hide_Back_Div() {
    var hdiv = document.getElementById("HideBackDiv");
    hdiv.style.width = $(document).width() + 'px';
    hdiv.style.height = $(document).height() + 'px';
}

function Adjust_Lang_Btns() {
    var lww = $(window).width() - 90;
    if ($(window).width() < 950) {
        lww = 950;
    }

    $("#langBar").css("display", "block");
    $("#langBar").css("left", lww + "px");
}

function Adjust_Like_Btns() {
    var lww = $(window).width() - 90;
    if ($(window).width() < 950) {
        lww = 950;
    }

    $("#LikeHypsterCH").css("display", "block");
    $("#LikeHypsterCH").css("left", lww + "px");
}


//****************************************************************************************


function Show_Content() {
    document.getElementById('HideBackDiv').style.display = 'none';
    document.getElementById('popupContainer').style.display = 'none';
    $('#popupContainer').html("");
}

function Hide_Content() {
    document.getElementById('HideBackDiv').style.display = 'block';
    document.getElementById('popupContainer').style.display = 'block';
}




function Show_Content_PopupPlayer() {
    document.getElementById('HideBackDiv_PopupPlayer').style.display = 'none';
    document.getElementById('popupContainer_PopupPlayer').style.display = 'none';
    $('#popupContainer').html("");

    $("#MainContHolder").css("visibility", "visible");
}

function Hide_Content_PopupPlayer() {

    var hdiv = document.getElementById("HideBackDiv_PopupPlayer");
    hdiv.style.width = $(document).width() + 'px';
    hdiv.style.height = $(document).height() + 'px';

    
    document.getElementById('HideBackDiv_PopupPlayer').style.display = 'block';
    document.getElementById('popupContainer_PopupPlayer').style.display = 'block';

    $("#MainContHolder").css("visibility", "hidden");
}




function getQuerystring(key, default_) {
    if (default_ == null) default_ = "";
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
    var qs = regex.exec(window.location.href);
    if (qs == null)
        return default_;
    else
        return qs[1];
}


function getQuerystring_str(key, default_) {
    
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
    var qs = regex.exec(default_);
    if (qs == null)
        return "";
    else
        return qs[1];
}
//****************************************************************************************






// -----SLIDESHOW-----
//****************************************************************************************
var slideshow_stst = 0;
function StartSlideshow() {
    $('.mainSlideshow').cycle({
        fx: 'scrollRight',
        speed: 500,
        timeout: 7000
    });

    slideshow_stst = 1;
}


function StartStopSlideshow() {
    if (slideshow_stst == 1) {
        $('.mainSlideshow').cycle('pause');
        slideshow_stst = 0;
    }
    else {
        $('.mainSlideshow').cycle('resume');
        slideshow_stst = 1;
    }
}


function opnSlide(id) {
    
    $('.mainSlideshow').cycle('pause');

    $('.HomeSldShow').css('display', 'none');
    
    $('#HomeSlide' + id).css('visibility', 'visible');
    $('#HomeSlide' + id).css('display', 'block');
    $('#HomeSlide' + id).css('left', '0px');
}
//****************************************************************************************







//****************************************************************************************
function ShowImage(image_src) {
    Hide_Content();


    $(document).scrollTop(0);

    var close_btn = "";
    close_btn += "<img alt=\"\" src=\"/imgs/exit_button.png\" style=\" float:right; margin:10px 20px 0 0; cursor:pointer;\" onclick=\"Show_Content();\" />";


    $('#popupContainer').html("<div style=' width:" + $(window).width() + "px; '><center> <div>" + close_btn + " <img alt='' src='" + image_src + "' style=' margin-top:60px; border:3px solid #ABABAB; border-radius:3px;' /></div>    </center> </div>");
    
}


function ShowImageViewer(username, active_image) {
    Hide_Content();

    
    $(document).scrollTop(0);

    $.ajax({
        type: "POST",
        url: "/account/pr_PicturesViewer?username=" + username + "&imgid=" + active_image,
        async: true,
        success: function (data) {
            $('#popupContainer').html(data);
        }
    });
}



function langBar_click() {
    if ($('#OLangs').css('display') == 'none') {
        $('#OLangs').css('display', 'block');
    }
    else {
        $('#OLangs').css('display', 'none'); 
    }
}



function ShowAddToMyPlaylist(song_id_url) {
    Hide_Content();

    $(document).scrollTop(0);


    try {
        if ($("#txtSearchString") != null) {
            var ss = $("#txtSearchString").val();

            if (ss != " Enter Artist, Song or Genre") {
                ss = ss.replace(" ", "+");
                ss = ss.replace("/", "+");
                ss = ss.replace("\\", "+");
                ss = ss.replace("&", "+");
                ss = ss.replace("$", "+");
                ss = ss.replace("?", "+");
                ss = ss.replace("!", "+");
                song_id_url += "&ss=" + ss;
            }
        }
    } catch (ex) { }


    $.ajax({
        type: "POST",
        url: "/playlist/AddToPlayList" + song_id_url,
        async: true,
        success: function (data) {
            $('#popupContainer').html(data);
        }
    });
}


function ShowAddToMyPlaylistPopup(song_id_url) {
    $('#popupContainer_PopupPlayer').html("");

    Hide_Content_PopupPlayer();

    $(document).scrollTop(0);

    $.ajax({
        type: "POST",
        url: "/m/mplaylist/AddToPlayListPopup" + song_id_url,
        async: true,
        success: function (data) {
            $('#popupContainer_PopupPlayer').html(data);
        }
    });
}


function ShowSubmitFeedbackPopup() {
    $('#popupContainer_PopupPlayer').html("");

    Hide_Content_PopupPlayer();

    $(document).scrollTop(0);

    $.ajax({
        type: "POST",
        url: "/playlist/SubmitFeedbackPopup",
        async: true,
        success: function (data) {
            $('#popupContainer_PopupPlayer').html(data);
        }
    });
}







function ShowLoginRegister(song_id_url) {
    Hide_Content();

    $(document).scrollTop(0);
    
    $.ajax({
        type: "POST",
        url: "/m/maccount/Register",
        async: true,
        success: function (data) {
            $('#popupContainer').html(data);
        }
    });
}












/* HOME */
/* ------------------------------------------------------------------ */


function StartMusicSearch() {
    var ss_str = $("#txtSearchString").val();

    if (ss_str != " Enter Artist, Song or Genre") {
        ss_str = ss_str.replace(" ", "+");
        ss_str = ss_str.replace("/", "+");
        ss_str = ss_str.replace("\\", "+");
        ss_str = ss_str.replace("&", "+");
        ss_str = ss_str.replace("$", "+");
        ss_str = ss_str.replace("?", "+");
        ss_str = ss_str.replace("!", "+");
        window.location = "/m/mlisten?ss=" + ss_str;
    }
}

function SearchString_KeyUp(e) {

    if (window.event) {

        if (window.event.keyCode == 13) {
            StartMusicSearch();
        }
    }
    else {

        if (e.which == 13) {
            StartMusicSearch();
        }
    }

}




function StartMusicSearchTags() {
    var ss_str = $("#txtSearchString").val();

    if (ss_str != " Enter Search Term") {
        ss_str = ss_str.replace(" ", "+");
        ss_str = ss_str.replace("/", "+");
        ss_str = ss_str.replace("\\", "+");
        ss_str = ss_str.replace("&", "+");
        ss_str = ss_str.replace("$", "+");
        ss_str = ss_str.replace("?", "+");
        ss_str = ss_str.replace("!", "+");
        window.location = "/tags/" + ss_str;
    }
}


function SearchString_KeyUpTags(e) {

    if (window.event) {

        if (window.event.keyCode == 13) {
            StartMusicSearchTags();
        }
    }
    else {

        if (e.which == 13) {
            StartMusicSearchTags();
        }
    }

}

/* ------------------------------------------------------------------ */













/* LISTEN */
/* ------------------------------------------------------------------ */

function Open_Menu1() {

    $('#listenTab1').css('display', 'block');
    $('#listenTab2').css('display', 'none');
    $('#listenTab3').css('display', 'none');
    $('#Menu1').css('background-color', '#1d1d1c');
    $('#Menu2').css('background-color', '#2b3032');
    $('#Menu3').css('background-color', '#2b3032');
}


function Open_Menu2() {

    $('#listenTab1').css('display', 'none');
    $('#listenTab2').css('display', 'block');
    $('#listenTab3').css('display', 'none');
    $('#Menu1').css('background-color', '#2b3032');
    $('#Menu2').css('background-color', '#1d1d1c');
    $('#Menu3').css('background-color', '#2b3032');

    lsnLoadRadioSt();
}

function Open_Menu3() {

    $('#listenTab1').css('display', 'none');
    $('#listenTab2').css('display', 'none');
    $('#listenTab3').css('display', 'block');
    $('#Menu1').css('background-color', '#2b3032');
    $('#Menu2').css('background-color', '#2b3032');
    $('#Menu3').css('background-color', '#1d1d1c');
}






var isradioStLoaded = false;
function lsnLoadRadioSt() {
    if (isradioStLoaded == false) {
        $.ajax({
            type: "POST",
            url: "/m/mlisten/RadioStationsBar",
            async: true,
            success: function (data) {
                $('#RSBch1').html(data);
                isradioStLoaded = true;
            }
        });
    }
}




function lsnLoadCompCheck() {
    $.ajax({
        type: "POST",
        url: "/compatibilityCheck/cComp_st1",
        async: true,
        success: function (data) {
            $('#listenSlideCompCheck2').html(data);
        }
    });
}

function lsnLoadPopularPlsts() {
    
    $.ajax({
        type: "POST",
        url: "/apps/appPopular/PopularPlaylists",
        async: true,
        success: function (data) {
            $('#listenSlidePopPlst4').html(data);
        }
    });
}

/* ------------------------------------------------------------------ */





/* LISTEN */
/* ------------------------------------------------------------------ */

function Open_Plst_Menu1() {

    $('#plstsListenTab1').css('display', 'block');
    $('#plstsListenTab2').css('display', 'none');
    $('#plstsListenTab3').css('display', 'none');
    $('#Menu1').css('background-color', '#1d1d1c');
    $('#Menu2').css('background-color', '#2b3032');
    $('#Menu3').css('background-color', '#2b3032');
}


function Open_Plst_Menu2() {

    $('#plstsListenTab1').css('display', 'none');
    $('#plstsListenTab2').css('display', 'block');
    $('#plstsListenTab3').css('display', 'none');
    $('#Menu1').css('background-color', '#2b3032');
    $('#Menu2').css('background-color', '#1d1d1c');
    $('#Menu3').css('background-color', '#2b3032');
}

function Open_Plst_Menu3() {

    $('#plstsListenTab1').css('display', 'none');
    $('#plstsListenTab2').css('display', 'none');
    $('#plstsListenTab3').css('display', 'block');
    $('#Menu1').css('background-color', '#2b3032');
    $('#Menu2').css('background-color', '#2b3032');
    $('#Menu3').css('background-color', '#1d1d1c');
}






//desktop
function Open_Plst_Menu1_D() {

    $('#plstsListenTab1').css('display', 'block');
    $('#plstsListenTab2').css('display', 'none');
    $('#plstsListenTab3').css('display', 'none');
    $('#Menu1').css('background-color', '#202020');
    $('#Menu2').css('background-color', '#2b3032');
    $('#Menu3').css('background-color', '#2b3032');
}


function Open_Plst_Menu2_D() {

    $('#plstsListenTab1').css('display', 'none');
    $('#plstsListenTab2').css('display', 'block');
    $('#plstsListenTab3').css('display', 'none');
    $('#Menu1').css('background-color', '#2b3032');
    $('#Menu2').css('background-color', '#202020');
    $('#Menu3').css('background-color', '#2b3032');
}

function Open_Plst_Menu3_D() {

    $('#plstsListenTab1').css('display', 'none');
    $('#plstsListenTab2').css('display', 'none');
    $('#plstsListenTab3').css('display', 'block');
    $('#Menu1').css('background-color', '#2b3032');
    $('#Menu2').css('background-color', '#2b3032');
    $('#Menu3').css('background-color', '#202020');
}

/* ------------------------------------------------------------------ */













/* ------------------------------------------------------------------ */
var isOrderOn = "";

function SearchMusic() {

    var ss = $("#txtSearchString").val();

    
    if ($("input[name=group_orderBy]:radio").val() != undefined) {
        isOrderOn = $("input[name=group_orderBy]:radio").val();
    }

    SearchMusicStr(ss);
}



function SearchMusicStr(search_string) {

    //$('#listenSlideContHolder').html("<div style='float:right; width:680px;'><span style='font-size:24px; color:#454545;'>Searching...</span></div>");
    $('#listenSlideContHolder').html("<div style='float:right; width:100%;'><span style='font-size:24px; color:#454545;'>Searching...</span></div>");

    var ss = search_string;
    ss = ss.replace(" ", "+");
    ss = ss.replace("/", "+");
    ss = ss.replace("\\", "+");
    ss = ss.replace("&", "+");
    ss = ss.replace("$", "+");
    ss = ss.replace("?", "+");
    ss = ss.replace("!", "+");


    var search_url = "/m/msearch/Music?ss=" + ss;
    if (isOrderOn != "") {
        search_url += "&orderBy=" + isOrderOn;
    }


    $.ajax({
        type: "POST",
        url: search_url,
        async: true,
        success: function (data) {
            $('#listenSlidePopPlst4').html(data);
        }
    });

    $(document).scrollTop(640);
}



function SearchMusicStrPage(search_string, page) {

    //$('#listenSlideContHolder').html("<div style='float:right; width:680px;'><span style='font-size:24px; color:#454545;'>Searching...</span></div>");
    $('#listenSlideContHolder').html("<div style='float:right; width:100%;'><span style='font-size:24px; color:#454545;'>Searching...</span></div>");

    var ss = search_string;
    ss = ss.replace(" ", "+");
    ss = ss.replace("/", "+");
    ss = ss.replace("\\", "+");
    ss = ss.replace("&", "+");
    ss = ss.replace("$", "+");
    ss = ss.replace("?", "+");
    ss = ss.replace("!", "+");

    var search_url = "/m/msearch/Music?ss=" + ss + "&page=" + page;
    if (isOrderOn != "") {
        search_url += "&orderBy=" + isOrderOn;
    }

    $.ajax({
        type: "POST",
        url: search_url,
        async: true,
        success: function (data) {
            
            $('#listenSlidePopPlst4').html(data);
        }
    });

    $(document).scrollTop(640);
}






function SearchMusicYTID() {

    var ss = $("#txtSearchString_youtube").val();
    ss = ss.replace(" ", "+");

    //$('#listenSlideContHolder').html("<div style='float:right; width:680px;'><span style='font-size:24px; color:#454545;'>Searching...</span></div>");
    $('#listenSlideContHolder').html("<div style='float:right; width:100%;'><span style='font-size:24px; color:#454545;'>Searching...</span></div>");


    $.ajax({
        type: "POST",
        url: "//m/msearch/MusicYTID?ss=" + ss,
        async: true,
        success: function (data) {
            $('#listenSlideContHolder').html(data);
        }
    });

    $(document).scrollTop(640);
}




function ListenSearchString_KeyUp(e) {
    
    if (window.event) {

        if (window.event.keyCode == 13) {
            SearchMusic();
        }
    }
    else {

        if (e.which == 13) {
            SearchMusic();
        }
    }
}
/* ------------------------------------------------------------------ */







/* CREATE */
/* ------------------------------------------------------------------ */

function shrinkAllSLides() {
    $("#CreateSection1").css("height", "140px");
    $("#CreateSection2").css("height", "140px");
    $("#CreateSection3").css("height", "140px");

    $("#CreateSection1").css("background-color", "#2b3032");
    $("#CreateSection2").css("background-color", "#2b3032");
    $("#CreateSection3").css("background-color", "#2b3032");


    $("#CreateSection1_img").css("display", "none");
    $("#CreateSection2_img").css("display", "none");
    $("#CreateSection3_img").css("display", "none");


    $("#CreateContHolder").css("display", "block");
    $("#CreateContHolder").css("min-height", "450px");

    $("#CreateContHolder").html("");
}

/* ------------------------------------------------------------------ */










/* CREATE PLAYER */
/* ------------------------------------------------------------------ */

function LoadPlayerCont(cont_url) {
    //--------------------------------------------------------
    //load playlists
    $.ajax({
        type: "POST",
        url: cont_url,
        async: true,
        success: function (data) {
            $('#CreateContHolder').html(data);
        }
    });
    //-------------------------------------------------------
}


function playlist_changed(object) {

    $.ajax({
        type: "POST",
        url: "/playlist/getPlaylistDetailsEdt?PL_ID=" + object[object.selectedIndex].value + "&PL_TYPE=BarPlayer",
        async: true,
        success: function (data) {
            $('#PlaylistSongsContainer').html(data);
        }
    });

}


function preview_skin_bar(skin_id) {
    $('#skinImgHolder1').attr('src', '/imgs/get_player/bar_skins/' + skin_id + ".png");
}

function preview_skin_classic(skin_id) {

    if (skin_id == "6") {
        $("#CustomizePlayerContH").css("display", "block");
    }
    else {
        $('#skinImgHolder1').attr('src', '/imgs/get_player/classic_skins/' + skin_id + ".png");
        $("#CustomizePlayerContH").css("display", "none");
    }

}

/* ------------------------------------------------------------------ */













/* ------------------------------------------------------------------ */
function showHide_AddSearch(caller) {

    if ($("#AddSearch").css("display") == "none") {
        $("#AddSearch").css("display", "block");
        $("#MainSearchLn").css("height", "130px;");

        $(".contMiddle").css("display", "none");

        //caller.innerHTML = "Hide";

        $("#txtSearchString").css("background-color", "#DDDDDD");
        $("#txtSearchString").css("border", "3px solid #DDDDDD");
    }
    else {
        $("#AddSearch").css("display", "none");
        $("#MainSearchLn").css("height", "70px;");

        $(".contMiddle").css("display", "block");

        //caller.innerHTML = "Advanced Search";

        $("#txtSearchString").css("background-color", "#FFFFFF");
        $("#txtSearchString").css("border", "3px solid #FFFFFF");
    }
}
/* ------------------------------------------------------------------ */











/* ------------------------------------------------------------------ */
/* Comp */
/* ------------------------------------------------------------------ */

var SongID_str = "";
function LoadStep2_SongSelected(song_id, youtube_id) {

    var songtitle = $("#SongCompTitle_" + song_id).html();
    $("#SongsCompContHolder1").append("<div id=\"searchFor_" + song_id + "\" class=\"plst_song_itm\" onclick=\"removeCompSongFS('" + song_id + "')\" >" + "<img alt='' src='http://i.ytimg.com/vi/" + youtube_id + "/0.jpg' />&nbsp;" + songtitle + " </div>");

    SongID_str += song_id + "|";
    SearchForCompatiblePlaylists(SongID_str);
}


//
// WHEN USER CLICKED ON SEARCH FOR SONG
function SearchForCompatiblePlaylists(p_song_id) {

    var num_of_song_items = SongID_str.split("|");
    var margin_lev = ((num_of_song_items.length - 1) * 7) * -1;
    if (margin_lev < -80)
        margin_lev = -80;
    $("#CompCir2").css("margin-left", margin_lev + "px");


    $('#PlstCompSearchResultsCt').html("Searshing....");

    $.ajax({
        type: "POST",
        url: "/compatibilityCheck/getCompatiblePlaylists?SongID=" + p_song_id,
        async: true,
        success: function (data) {
            $('#PlstCompSearchResultsCt').html(data);
        }
    });
}


//
// WHEN PLAYLIST DROPDOWN CHANGED
function change_UserComp_playlist(object) {
    if (object.selectedIndex >= 0) {

        var url = "";
        url += "/compatibilityCheck/cComp_GetPlaylistData?PL_ID=" + object[object.selectedIndex].value;
        if (comp_tool_user_id != undefined) {
            url += "&US_ID=" + comp_tool_user_id;
        }

        $.ajax({
            type: "POST",
            url: url,
            async: true,
            success: function (data) {
                $('#UserCompPlstHolder1').html(data);
            }
        });
    }
}



function removeCompSongFS(p_id) {
    $("#searchFor_" + p_id).remove();
    SongID_str = SongID_str.replace(p_id + "|", '');
    SearchForCompatiblePlaylists(SongID_str);
}



function CleanSearchResults() {
    $('#SongsCompContHolder1').html("");
    SongID_str = '';
    $('#PlstCompSearchResultsCt').html("");

    $("#CompCir2").css("margin-left", "0px");
}

/* ------------------------------------------------------------------ */







/* PLAYLIST */
/* ------------------------------------------------------------------ */

function Pl_DelSong(p_song_id, p_playlist_id) {
    if (confirm("Are you sure you want to delete?") == true) {
        window.location = "/m/mcreate/playlist?ACT=delete_song&song_id=" + p_song_id + "&playlist_id=" + p_playlist_id; 
    }
}



function Pl_EditPlaylist(p_playlist_id) {
    $('#textName_cont_' + p_playlist_id).css('display', 'none');
    $('#editName_cont_' + p_playlist_id).css('display', 'block');

}

function Pl_DelPlaylist(p_playlist_id) {
    if (confirm('Are you sure you want to delete?') == true) {
        window.location = '/m/mcreate/playlist?ACT=delete_playlist&playlist_id=' + p_playlist_id; 
    }
}

function Pl_CancelPlaylist(p_id) {
    $('#textName_cont_' + p_id).css('display', 'block'); $('#editName_cont_' + p_id).css('display', 'none');
}






var isMOUSE_UP_ON = false;
var old_order = 0;

function mouse_down(obj, link_id) {
    var index = $("#sortablePlaylist .srt_itm").index(obj);
    old_order = index;
}

function mouse_up(obj, song_id, aplid) {

    if (isMOUSE_UP_ON == true) {
        var new_order = $("#sortablePlaylist .srt_itm").index(obj);
        var set_order = (new_order - old_order);


        var randomnumber = Math.floor(Math.random() * 1100000);
        $.ajax({
            type: "POST",
            url: "/playlist/applyOrder?SONG_ID=" + song_id + "&SONG_ORDER=" + set_order + "&ACTIVE_PL=" + aplid + "&Rnum=" + randomnumber,
            async: true,
            success: function (data) {
            }
        });

        isMOUSE_UP_ON = false;
        old_order = 0;
    }
}
/* ------------------------------------------------------------------ */





/*  VISUAL SEARCH */
/* ------------------------------------------------------------------ */

var curr_counter = 37;
var top_original = 0;
var vis_width = 0;

function ScrlInit() {
    top_original = $(".SbTop").offset().left;
    if ($(window).width() > 1024) {
        margin_left = $(window).width() - 1024 - 44;
        vis_width = (1024 + 30 + (margin_left / 2) - 15 - 300) * -1;
    } else {
        margin_left = 0;
        vis_width = (1024 - 300) * -1;
    }
}

var cNum = 0;
var topLS = false;
var off_start = 0;
var off_end = 0;

function mvMouseDown() { off_start = $(".SbTop").offset().left; }
function mvMouseUp() { off_end = $(".SbTop").offset().left; ScrlLdr(".SbTop"); }

function mvMouseDownB() { off_start = $(".SbBottom").offset().left; }
function mvMouseUpB() { off_end = $(".SbBottom").offset().left; ScrlLdr(".SbBottom"); }


function VisualSearchClick(object) {
    if ((off_start - off_end) == 0) {
        window.location = '/m/mlisten?ss=' + object + '&rtg=x2982';
    }
    off_start = 0;
    off_end = 0;
}


function ScrlLdr(object) {
    if (cNum > 12) { return; }

    var item_pos = ($(object).offset().left - top_original) + ($(object).width() + (vis_width * 2) - 100);
    

    if (item_pos < vis_width && topLS == false) {
        topLS = true;
        
        $.ajax({
            type: "POST",
            url: "/m/mlisten/visualSearchBarLoad/" + curr_counter,
            async: true,
            success: function (data) {
                if (data != "") {
                    var data_arr = data.split('|');

                    $(object).css('width', ($(object).width() + parseInt(data_arr[1])) + "px");
                    $(object).append(data_arr[0]);

                    curr_counter += 11;
                    topLS = false;
                    cNum += 1;
                }
            }
        });
    }
}

/* ------------------------------------------------------------------ */













/* Most Viewed Playlist Details Preview  */
/* ------------------------------------------------------------------ */

function DispPlstDetails(obj, pl_id, us_id) {
    

    var p = $(obj);
    var position = p.position();

    if ($("#GMVP_PlstPreview").position().left != position.left && $("#GMVP_PlstPreview").position().top != position.top) {
        $("#GMVP_PlstPreview").css("left", position.left + "px");
        $("#GMVP_PlstPreview").css("top", (position.top + 36) + "px");

        $('#GMVP_PlstPreview').html("<div class='LdingPrev'>Loading...</div>");
        $("#GMVP_PlstPreview").css("display", "block");

        $.ajax({
            type: "POST",
            url: "/playlist/getPlaylistDetailsPreview?PL_ID=" + pl_id + "&US_ID=" + us_id,
            async: true,
            success: function (data) {
                $('#GMVP_PlstPreview').html(data);
            }
        });
    }
}

function DPLDD(obj, pl_id, us_id) {


    var p = $(obj);
    var position = p.position();

    if ($("#GMVP_PlstPreview").position().left != position.left && $("#GMVP_PlstPreview").position().top != position.top) {
        $("#GMVP_PlstPreview").css("left", position.left + "px");
        $("#GMVP_PlstPreview").css("top", (position.top + 36) + "px");

        $('#GMVP_PlstPreview').html("<div class='LdingPrev'>Loading...</div>");
        $("#GMVP_PlstPreview").css("display", "block");

        $.ajax({
            type: "POST",
            url: "/playlist/getPlaylistDetailsPreview?PL_ID=" + pl_id + "&US_ID=" + us_id,
            async: true,
            success: function (data) {
                $('#GMVP_PlstPreview').html(data);
            }
        });
    }
}





function HidePlstDetails(event, obj) {
    var e = event.toElement || event.relatedTarget;
    if (e.parentNode == obj || e == obj) {
        return;
    }

    $("#GMVP_PlstPreview").css("display", "none");
    $('#GMVP_PlstPreview').html("<div class='LdingPrev'>Loading...</div>");
}

function HPLDD(event, obj) {
    var e = event.toElement || event.relatedTarget;
    if (e.parentNode == obj || e == obj) {
        return;
    }

    $("#GMVP_PlstPreview").css("display", "none");
    $('#GMVP_PlstPreview').html("<div class='LdingPrev'>Loading...</div>");
}

/* ------------------------------------------------------------------ */




/* Popular Playlist Details Preview  */
/* ------------------------------------------------------------------ */

function DispPopPlstDetails(obj, pl_id, us_id) {

    
    var p = $(obj);
    var position = p.position();

    if ($("#PP_PlstPreview").position().left != position.left && $("#PP_PlstPreview").position().top != position.top) {
        $("#PP_PlstPreview").css("left", position.left + "px");
        $("#PP_PlstPreview").css("top", (position.top + 49) + "px");

        $('#PP_PlstPreview').html("<div class='LdingPrev'>Loading...</div>");
        $("#PP_PlstPreview").css("display", "block");

        $.ajax({
            type: "POST",
            url: "/playlist/getPlaylistDetailsPreviewPop?PL_ID=" + pl_id + "&US_ID=" + us_id,
            async: true,
            success: function (data) {
                $('#PP_PlstPreview').html(data);
            }
        });
    }
}

function HidePopPlstDetails(event, obj) {
    var e = event.toElement || event.relatedTarget;
    if (e.parentNode == obj || e == obj) {
        return;
    }

    $("#PP_PlstPreview").css("display", "none");
    $('#PP_PlstPreview').html("<div class='LdingPrev'>Loading...</div>");
}

/* ------------------------------------------------------------------ */




/* Popular Playlist Details Preview  */
/* ------------------------------------------------------------------ */

//DispPopPlstDetailsDG
function DspPPDDG(obj, pl_id, us_id) {


    var p = $(obj);
    var position = p.position();

    if ($("#PP_PlstPreviewDG").position().left != position.left && $("#PP_PlstPreviewDG").position().top != position.top) {
        $("#PP_PlstPreviewDG").css("left", position.left + "px");
        $("#PP_PlstPreviewDG").css("top", (position.top + 49) + "px");

        $('#PP_PlstPreviewDG').html("<div class='LdingPrevDG'>Loading...</div>");
        $("#PP_PlstPreviewDG").css("display", "block");

        $.ajax({
            type: "POST",
            url: "/playlist/getPlaylistDetailsPreviewPopDG?PL_ID=" + pl_id + "&US_ID=" + us_id,
            async: true,
            success: function (data) {
                $('#PP_PlstPreviewDG').html(data);
            }
        });
    }
}


//HidePopPlstDetailsDG
function HdPPDDG(event, obj) {
    var e = event.toElement || event.relatedTarget;
    if (e.parentNode == obj || e == obj) {
        return;
    }

    $("#PP_PlstPreviewDG").css("display", "none");
    $('#PP_PlstPreviewDG').html("<div class='LdingPrevDG'>Loading...</div>");
}

/* ------------------------------------------------------------------ */








/* Most Viewed Playlist Details Preview  */
/* ------------------------------------------------------------------ */

function DispRadioDetails(obj, pl_id, us_id) {

    var p = $(obj);
    var position = p.position();


    if ($("#Radio_PlstPreview").position().left != position.left && $("#Radio_PlstPreview").position().top != position.top) {
        $("#Radio_PlstPreview").css("left", position.left + "px");
        $("#Radio_PlstPreview").css("top", (position.top + 36) + "px");

        $('#Radio_PlstPreview').html("<div class='LdingPrev'>Loading...</div>");
        $("#Radio_PlstPreview").css("display", "block");

        $.ajax({
            type: "POST",
            url: "/playlist/getPlaylistDetailsPreview?PL_ID=" + pl_id + "&US_ID=" + us_id,
            async: true,
            success: function (data) {
                $('#Radio_PlstPreview').html(data);
            }
        });
    }
}

function DRDD(obj, pl_id, us_id) {

    var p = $(obj);
    var position = p.position();


    if ($("#Radio_PlstPreview").position().left != position.left && $("#Radio_PlstPreview").position().top != position.top) {
        $("#Radio_PlstPreview").css("left", position.left + "px");
        $("#Radio_PlstPreview").css("top", (position.top + 36) + "px");

        $('#Radio_PlstPreview').html("<div class='LdingPrev'>Loading...</div>");
        $("#Radio_PlstPreview").css("display", "block");

        $.ajax({
            type: "POST",
            url: "/playlist/getPlaylistDetailsPreview?PL_ID=" + pl_id + "&US_ID=" + us_id,
            async: true,
            success: function (data) {
                $('#Radio_PlstPreview').html(data);
            }
        });
    }
}






function HideRadioDetails(event, obj) {
    var e = event.toElement || event.relatedTarget;
    if (e.parentNode == obj || e == obj) {
        return;
    }
    $("#Radio_PlstPreview").css("display", "none");
    $('#Radio_PlstPreview').html("<div class='LdingPrev'>Loading...</div>");
}

function HRDD(event, obj) {
    var e = event.toElement || event.relatedTarget;
    if (e.parentNode == obj || e == obj) {
        return;
    }
    $("#Radio_PlstPreview").css("display", "none");
    $('#Radio_PlstPreview').html("<div class='LdingPrev'>Loading...</div>");
}

/* ------------------------------------------------------------------ */




/* Most Viewed Playlist Details Preview  */
/* ------------------------------------------------------------------ */

function DispPlstMyLikeDetails(obj, pl_id, us_id) {

    var p = $(obj);
    var position = p.position();

    if ($("#PMYLIKE_PlstPreview").position().left != position.left && $("#PMYLIKE_PlstPreview").position().top != position.top) {
            $("#PMYLIKE_PlstPreview").css("left", position.left + "px");
            $("#PMYLIKE_PlstPreview").css("top", (position.top + 36) + "px");

            $('#PMYLIKE_PlstPreview').html("<div class='LdingPrev'>Loading...</div>");
            $("#PMYLIKE_PlstPreview").css("display", "block");

            $.ajax({
                type: "POST",
                url: "/playlist/getPlaylistDetailsPreview?PL_ID=" + pl_id + "&US_ID=" + us_id,
                async: true,
                success: function (data) {
                    $('#PMYLIKE_PlstPreview').html(data);
                }
            });
    }
}


function HidePlstMyLikeDetails(event, obj) {
    var e = event.toElement || event.relatedTarget;
    if (e.parentNode == obj || e == obj) {
        return;
    }
    
    $("#PMYLIKE_PlstPreview").css("display", "none");
    $('#PMYLIKE_PlstPreview').html("<div class='LdingPrev'>Loading...</div>");
}
/* ------------------------------------------------------------------ */








/* TAGS */
/* ------------------------------------------------------------------ */
function add_new_tag() {

    if ($('#newTagName').val() == "") {
        alert("Tag is empty");
        return;
    }


    var res_str = $('#newTagName').val();
    res_str = res_str.replace("http://", "");
    res_str = res_str.replace("https://", "");
    res_str = res_str.replace("http", "");


    $.ajax({
        type: "POST",
        url: "/create/addnewtag?tag_name=" + res_str + "&playlist_id=" + $('#newTagPlst').val(),
        async: true,
        success: function (data) {
            if ($('span.delTagSpn').length > 0) {
                $("#tagsMCH1").prepend("<a id='tg" + $('#newTagPlst').val() + "' class='TagI' onclick='delete_plst_tag(" + $('#newTagPlst').val() + ")'>" + res_str + "&nbsp;<span class='delTagSpn'>X</span> </a>");
            }
            else {
                $("#tagsMCH1").prepend("<a class='TagI' href='/tags/" + res_str + "'>" + res_str + "</a>");
            }
            $("#newTagName").val("");
        }
    });
}
function loadtagsForEdit(toggle) {
    $.ajax({
        type: "POST",
        url: "/apps/appCreate/tagsForEdit?playlist_id=" + $('#newTagPlst').val() + "&toggle=" + toggle,
        async: true,
        success: function (data) {
            $("#tagsMCH1").html(data);
            if (toggle == "on")
                $("#tagsEditBtn").css("background-color", "#FF5C5C");
            else
                $("#tagsEditBtn").css("background-color", "#4A4A4A");
        }
    });
}
function delete_plst_tag(tag_id) {
    $.ajax({
        type: "POST",
        url: "/create/deletePlaylistTag?tag_plst_id=" + tag_id + "&playlist_id=" + $('#newTagPlst').val(),
        async: true,
        success: function (data) {
            $("#tg" + tag_id).css("display", "none");
        }
    });
}
/* ------------------------------------------------------------------ */









/* ------------------------------------------------------------------ */


var menuHOpened = false;
function ShowHideMenuH() {

    if (menuHOpened == false) {
        var wwH = $(window).height() - 50 - 83;

        
        if (wwH < 725) {
            wwH = 725;
        }


        $('#MenuHSubCont').css('width', $(window).width() + 'px');
        $('#MenuHSubCont').css('left', 0 + 'px');
        $('#MenuHSubCont').css('height', (wwH - 50) + 'px');
        $('#MenuHSubCont').css('top', (30 + 30) + 'px');


        $('#MenuHSub').css('width', ($(window).width() - 20) + 'px');
        $('#MenuHSub').css('left', (10) + 'px');

        $('#MenuHSub').css('height', (wwH) + 'px');
        $('#MenuHSub').css('top', (20) + 'px');


        $('.ContentRootCont').css('display', 'none');
        HDAM();
        $('#MenuHSub').css('display', 'block');
        $('#MenuHSubCont').css('display', 'block');
        menuHOpened = true;
    }
    else {
        $('.ContentRootCont').css('display', 'block');
        $('#MenuHSub').css('display', 'none');
        $('#MenuHSubCont').css('display', 'none');
        menuHOpened = false;
    }
}


function ShowMenuH() {

    if (menuHOpened == false) {
        menuHOpened = false;
        ShowHideMenuH();
    }
    else {
        HideMenuH();
    }

}
function HideMenuH() {
    menuHOpened = true;
    ShowHideMenuH();
}

/* ------------------------------------------------------------------ */






/*   Popular radio stations */
/* ------------------------------------------------------------------ */
function change_radio_stations() {

    $.ajax({
        type: "POST",
        url: "/popular/PopularRadioStations_Random",
        async: true,
        success: function (data) {
            $('.FeaturedVideoInner').html(data);
        }
    });
}



function LoadLiveRadioThumbs(obj, pl_id, us_id, g_id) {
    $.ajax({
        type: "POST",
        url: "/playlist/getPlaylistDetailsPreviewRadio?PL_ID=" + pl_id + "&US_ID=" + us_id,
        async: true,
        success: function (data) {
            $("#RdGenre_" + g_id).html(data);
        }
    });
}
/* ------------------------------------------------------------------ */


