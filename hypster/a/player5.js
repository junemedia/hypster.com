
//**************************************************************************************************************************
var imgs_f = "imgs5";
var PL_CREAETED = false;
var hypHostName = "hypster.com";
var hypMainBackColor = "#171717";
var hypUserID = 0;
var hypPlaylistID = 0;
var hypPlayerControls = 0;
var hypPlayerSort = "";
var hypButtonsBack = "#353535";
var hypAutostart = 0;
var hypVisibility = "visible";
var hypSaveState = 1;

var hypVolumeVal = 80;



//otp
var playlistLoaded = false;


var PL_W = 280;
var PL_H = 220;


if (typeof jQuery == 'undefined') {
    var tag_jquery = document.createElement('script');
    tag_jquery.src = "//code.jquery.com/jquery-1.8.3.min.js";
    var firstScriptTag_jquery = document.getElementsByTagName('script')[0];
    firstScriptTag_jquery.parentNode.insertBefore(tag_jquery, firstScriptTag_jquery);
}


var tag = document.createElement('script');
tag.src = "//www.youtube.com/iframe_api";
var firstScriptTag = document.getElementsByTagName('script')[0];
firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
//**************************************************************************************************************************










//**************************************************************************************************************************

var player;
function onYouTubeIframeAPIReady() {
    InitPlayer();        
}



function InitPlayer() {
    var PlayerIDVar = GeneratePlayerControls();
    

    var controls_display = '0';

    try {
        var device = navigator.userAgent.match(/iPhone|iPad|iPod/i);
        if (device == 'iPhone' | device == 'iPad' | device == 'iPod') {
            controls_display = '1';
        }
    }
    finally {
    }
    


    player = new YT.Player(PlayerIDVar, {
        width: PL_W,
        height: PL_H,
        videoId: '',
        playerVars: { 'autoplay': 0, 'controls': controls_display },
        events: {
            'onReady': onPlayerReady
        }
    });

    try {
        $("#" + PlayerIDVar).attr("frameborder", "0");
    }
    finally {
    }

}



var player_height = PL_H + 27;
function GeneratePlayerControls() {

    //-------------------------------------------------------------------------------
    $('#hypPlayerCont').remove();
    $('.hypPlayerAddCons').html("");
    //-------------------------------------------------------------------------------

    

    //get varibles
    //-------------------------------------------------------------------------------
    hypHostName = $("#HypPlayerScript").attr("hypHostName");
    hypUserID = $("#HypPlayerScript").attr("hypUserID");
    hypPlaylistID = $("#HypPlayerScript").attr("hypPlaylistID");
    hypMainBackColor = $("#HypPlayerScript").attr("hypMainBackColor");
    hypPlayerControls = $("#HypPlayerScript").attr("hypPlayerControls");
    hypPlayerSort = $("#HypPlayerScript").attr("hypPlayerSort");
    hypButtonsBack = $("#HypPlayerScript").attr("hypButtonsBack");
    hypAutostart = $("#HypPlayerScript").attr("hypAutostart");
    hypVisibility = $("#HypPlayerScript").attr("hypVisibility");
    hypSaveState = $("#HypPlayerScript").attr("hypSaveState");

    try {
        if ($("#HypPlayerScript").attr("hypVolumeVal") != undefined) {
            hypVolumeVal = $("#HypPlayerScript").attr("hypVolumeVal");
        }
    }
    finally {
    }
    //-------------------------------------------------------------------------------




    //check settings
    //-------------------------------------------------------------------------------
    if (hypPlayerControls == 0) {
        player_height = PL_H;
    }
    else {
        player_height = PL_H + 27;
    }
    //-------------------------------------------------------------------------------




    //create base elements
    //-------------------------------------------------------------------------------
    var tag_player_cont = document.createElement('div');
    $(tag_player_cont).attr("id", "hypPlayerCont");
    $(tag_player_cont).css("clear", "both");
    $(tag_player_cont).css("width", PL_W + "px");
    $(tag_player_cont).css("height", player_height + "px");
    $(tag_player_cont).css("background-color", hypMainBackColor); //SET COLOR HERE
    $(tag_player_cont).css("visibility", "visible"); //hypVisibility
    $(tag_player_cont).css("overflow", "hidden");

    if (hypVisibility == "hidden") {
        $(tag_player_cont).css("width", "2px");
        $(tag_player_cont).css("height", "2px");
        $(tag_player_cont).css("filter", "progid:DXImageTransform.Microsoft.Alpha(opacity=10);");
        $(tag_player_cont).css("opacity", "0.10;");
        $(tag_player_cont).css("-moz-opacity", "0.10;");
    }

    var insert_to = document.getElementById('HypPlayerScript');
    insert_to.parentNode.insertBefore(tag_player_cont, insert_to);


    var PlayerIDVar = "hypPlayer" + Math.floor(Math.random() * 110000);
    $("#hypPlayerCont").append("<div id='" + PlayerIDVar + "' style='float:left; width:" + PL_W + "px; height:" + PL_H + "px;' ></div>");
    //-------------------------------------------------------------------------------




    //create controls bar for player
    //-------------------------------------------------------------------------------
    if (hypPlayerControls == 1) {
        GenerateControlsBar();
    }
    //-------------------------------------------------------------------------------




    //check if aditional player controls are defined
    //-------------------------------------------------------------------------------
    if ($('.hypPlayerAddCons').length > 0) {
        GenerateControlsBar_Additional();
    };
    //-------------------------------------------------------------------------------


    return PlayerIDVar;
}




function GenerateControlsBar() {

    $("#hypPlayerCont").append("<div id='hypPlayerMenu' style='float:left; width:" + PL_W + "px; height:27px; background:" + hypMainBackColor + " url(http://" + hypHostName + "/a/" + imgs_f + "/player_back.png) repeat-x; ' />");


    var btn_style = "float:left; width:24px; height:23px; line-height:23px; margin:2px 0 0 2px; border-radius:3px; text-align:center; font-size:16px; color:#FFFFFF; cursor:pointer; font-weight:bold;";
    $("#hypPlayerMenu").append("<a class=\"hypPlayer_Prev_btn\" href=\"javascript:PlayPrevVideo();\" style=\" " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/prev_icn.png') no-repeat center;\" ></a>");
    $("#hypPlayerMenu").append("<a class=\"hypPlayer_PlPs_btn\" href=\"javascript:PlayPauseVideo();\" style=\" " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/play_icn.png') no-repeat center;\" ></a>");
    $("#hypPlayerMenu").append("<a class=\"hypPlayer_Next_btn\" href=\"javascript:PlayNextVideo();\" style=\" " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/next_icn.png') no-repeat center;\" ></a>");
    $("#hypPlayerMenu").append("<a class=\"hypPlayer_Shuffle_btn\" href=\"javascript:ShuffleSongs();\" style=\" " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/shuffle_icn.png') no-repeat center;\" ></a>");
    $("#hypPlayerMenu").append("<a class=\"hypPlayer_Mute_btn\" href=\"javascript:MuteSound();\" style=\" " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/mute_icn.png') no-repeat center;\" ></a>");
    $("#hypPlayerMenu").append("<a class=\"hypPlayer_Hypster_btn\" href=\"http://hypster.com/?rtg=p11\" title=\"Create Free Playlist Online\" target=\"_blank\" style=\" display:block; " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/hypster_icn.png') no-repeat center;\" ></a>");

}


function GenerateControlsBar_Additional() {

    $(".hypPlayerAddCons").css("clear", "both"); 
    $(".hypPlayerAddCons").css("width", "158px");
    $(".hypPlayerAddCons").css("height", "27px");
    $(".hypPlayerAddCons").css("visibility", "visible");
    $(".hypPlayerAddCons").css("background", hypMainBackColor + " url(http://" + hypHostName + "/a/" + imgs_f + "/player_back.png) repeat-x"); //hypMainBackColor); //SET COLOR HERE
    $(".hypPlayerAddCons").css("border-radius", "2px"); 


    var btn_style = "float:left; width:24px; height:23px; line-height:23px; margin:2px 0 0 2px; border-radius:3px; text-align:center; font-size:16px; color:#FFFFFF; cursor:pointer; font-weight:bold;";
    $(".hypPlayerAddCons").append("<a class=\"hypPlayer_Prev_btn\" href=\"javascript:PlayPrevVideo();\" style=\" " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/prev_icn.png') no-repeat center;\" ></a>");
    $(".hypPlayerAddCons").append("<a class=\"hypPlayer_PlPs_btn\" href=\"javascript:PlayPauseVideo();\" style=\" " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/play_icn.png') no-repeat center;\" ></a>");
    $(".hypPlayerAddCons").append("<a class=\"hypPlayer_Next_btn\" href=\"javascript:PlayNextVideo();\" style=\" " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/next_icn.png') no-repeat center;\" ></a>");
    $(".hypPlayerAddCons").append("<a class=\"hypPlayer_Shuffle_btn\" href=\"javascript:ShuffleSongs();\" style=\" " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/shuffle_icn.png') no-repeat center;\" ></a>");
    $(".hypPlayerAddCons").append("<a class=\"hypPlayer_Mute_btn\" href=\"javascript:MuteSound();\" style=\" " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/mute_icn.png') no-repeat center;\" ></a>");
    $(".hypPlayerAddCons").append("<a class=\"hypPlayer_Hypster_btn\" href=\"http://hypster.com/?rtg=p11\" title=\"Create Free Playlist Online\" target=\"_blank\" style=\" display:block; " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/hypster_icn.png') no-repeat center;\" ></a>");

}
//**************************************************************************************************************************


































/* PLAYER EVENTS */
//**************************************************************************************************************************
function onPlayerReady(event) {

    player.addEventListener("onStateChange", "onPlayerStateChange");
    player.addEventListener("onError", "onError");


    try {
        player.setVolume(hypVolumeVal);
    }
    finally {
    }


    if (playlistLoaded == false) {
        LoadPlaylist();

        playlistLoaded = true;
    }
}

function onError(event) {
    PlayNextVideo();
}

var done = false;
function onPlayerStateChange(event) {
    if (event.data == YT.PlayerState.ENDED) {
        PlayNextVideo();
        return;
    }


    if (event.data == YT.PlayerState.PAUSED) {
        PLAY_STATE = "0";
        $(".hypPlayer_PlPs_btn").css("background-image", "url('http://" + hypHostName + "/a/" + imgs_f + "/play_icn.png')");
    }
    if (event.data == YT.PlayerState.PLAYING) {
        PLAY_STATE = "1";
        $(".hypPlayer_PlPs_btn").css("background-image", "url('http://" + hypHostName + "/a/" + imgs_f + "/pause_icn.png')");
    }
}
//**************************************************************************************************************************








//**************************************************************************************************************************
var items_arr = new Array();
var PLAY_STATE = "0";
var CURR_VIDEO = 0;
var SOUND_STATE = 1;




function localJsonpCallback(json) {
    
    var data_str = json.songs;
    if (data_str[data_str.length - 1]) {
        data_str = json.songs.substring(0, data_str.length - 1);
    }
    items_arr = data_str.split(',');


    CURR_VIDEO = 0;

    

    //overwrite default settings if user
    //--------------------------------------------------------------
    var cookie_playerState = getCookie("PlayPause");
    if (typeof cookie_playerState != "undefined" && cookie_playerState != "" && hypSaveState == 1) {

        if (cookie_playerState == 1) {
            StartPlayVideo();
        }
        return;
    }
    //--------------------------------------------------------------

    
    //--------------------------------------------------------------
    if (hypAutostart == 1) {
        StartPlayVideo();
    }
    //--------------------------------------------------------------
}



function LoadPlaylist() {

    $.ajax({
        url: "http://" + hypHostName + "/playlist/exPl_Playlist_Npl_1?PL_ID=" + hypPlaylistID + "&US_ID=" + hypUserID + "&Sort=" + hypPlayerSort,
        type: "GET",
        dataType: "jsonp",
        jsonpCallback: "localJsonpCallback"
    });

}







function PlayPrevVideo() {

    if (CURR_VIDEO > 0) {
        CURR_VIDEO = CURR_VIDEO - 1;
    }


    player.stopVideo();
    player.loadVideoById(items_arr[CURR_VIDEO], 0, 'default');


    PLAY_STATE = "1";
    $(".hypPlayer_PlPs_btn").css("background-image", "url('http://" + hypHostName + "/a/" + imgs_f + "/pause_icn.png')");

}



// initial start
function StartPlayVideo() {
    var start_time = 0;


    var saved_song = getCookie("CurrSong");
    var curr_time = getCookie("CurrTime");


    if (typeof curr_time != "undefined" && curr_time != "" && hypSaveState == 1) {
        start_time = curr_time;
    }


    if (typeof saved_song != "undefined" && saved_song != "" && hypSaveState == 1) {
        for (var i = 0; i < items_arr.length; i++) {
            if (items_arr[i] == saved_song) {
                CURR_VIDEO = i;
            }
        }
    }
    else {
        CURR_VIDEO = 0;
        start_time = 0;
    }


    player.stopVideo();
    player.loadVideoById(items_arr[CURR_VIDEO], start_time, 'default');


    PLAY_STATE = "1";
    $(".hypPlayer_PlPs_btn").css("background-image", "url('http://" + hypHostName + "/a/" + imgs_f + "/pause_icn.png')");


    if (CURR_VIDEO == items_arr.length - 1) {
        CURR_VIDEO = -1;
    }

}






function PlayNextVideo() {

    if (CURR_VIDEO < items_arr.length) {
        CURR_VIDEO = CURR_VIDEO + 1;
    }


    player.stopVideo();
    player.loadVideoById(items_arr[CURR_VIDEO], 0, 'default');


    PLAY_STATE = "1";
    $(".hypPlayer_PlPs_btn").css("background-image", "url('http://" + hypHostName + "/a/" + imgs_f + "/pause_icn.png')");


    if (CURR_VIDEO == items_arr.length - 1) {
        CURR_VIDEO = -1;
    }
}





function PlayPauseVideo() {

    if (PLAY_STATE == "0") { //IF NOT STARTED START
        player.playVideo();

        PLAY_STATE = "1";
        $(".hypPlayer_PlPs_btn").css("background-image", "url('http://" + hypHostName + "/a/" + imgs_f + "/pause_icn.png')");
    }
    else { //IF STARTED NEED TO STOP
        player.pauseVideo();

        PLAY_STATE = "0";
        $(".hypPlayer_PlPs_btn").css("background-image", "url('http://" + hypHostName + "/a/" + imgs_f + "/play_icn.png')");
    }
}




function ShuffleSongs() {

    hypPlayerSort = 'rand';
    setCookie("PlayPause", "", 1);
    setCookie("CurrTime", "", 1);
    setCookie("CurrSong", "", 1);
    hypAutostart = 1;

    
    //-----------------------------------------------
    player.pauseVideo();
    PLAY_STATE = "0";
    $(".hypPlayer_PlPs_btn").css("background-image", "url('http://" + hypHostName + "/a/" + imgs_f + "/play_icn.png')");
    player.stopVideo();
    //-----------------------------------------------

    LoadPlaylist();
}


function MuteSound() {
    if (SOUND_STATE == 1) {
        player.mute();
        SOUND_STATE = 0;

        $(".hypPlayer_Mute_btn").css("background-image", "url('http://" + hypHostName + "/a/" + imgs_f + "/unmute_icn.png')");
    }
    else {
        player.unMute();
        SOUND_STATE = 1;

        $(".hypPlayer_Mute_btn").css("background-image", "url('http://" + hypHostName + "/a/" + imgs_f + "/mute_icn.png')");
    }

}
//**************************************************************************************************************************









//**************************************************************************************************************************
window.onbeforeunload = function () {

    setCookie("PlayPause", PLAY_STATE, 7);
    if (typeof items_arr[CURR_VIDEO] != "undefined") {
        setCookie("CurrSong", items_arr[CURR_VIDEO], 7);
    }
    setCookie("CurrTime", player.getCurrentTime(), 7);
}
//**************************************************************************************************************************














/* SYS N/M */
//**************************************************************************************************************************
function setCookie(c_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
    document.cookie = c_name + "=" + c_value;
}

function getCookie(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
}
//**************************************************************************************************************************





/* SYS N/M */
//**************************************************************************************************************************
(function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
(i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
})(window,document,'script','//www.google-analytics.com/analytics.js','ga');

ga('create', 'UA-28695705-6', 'auto', {'legacyCookieDomain': 'hypster.com'});
ga('send', 'pageview');

//**************************************************************************************************************************
