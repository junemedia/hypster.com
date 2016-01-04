var isNP = '';

var PlbkBar_W = 0;
var volContBar_W = 0;

var curr_playlist_id = '';
var curr_user_id = '';
var iua = '';
var desc_length = '';


var player;
var plTimer;
var PSC1;
var volCont1;


var curr_playlist_segment = 0;
//-------------------------------------------------------------------------------------------------------





//-------------------------------------------------------------------------------------------------------
if (isMobile.any()) {
    $(window).unbind('beforeunload');
}


$(document).ready(function () {

    //var margin_top = 2;
    //if ($(window).height() >= 720) {
    //    margin_top = ($(window).height() - 720) / 2;
    //}
    //$(".srclArea1").css("margin-top", margin_top + "px");


});

$(window).resize(function () {

    var margin_top = 2;
    if ($(window).height() >= 720) {
        margin_top = ($(window).height() - 720) / 2;
    }
    $(".srclArea1").css("margin-top", margin_top + "px");

});
//-------------------------------------------------------------------------------------------------------









//-------------------------------------------------------------------------------------------------------

var curr_scrl_pos = "L";
var curr_scrl_pos2 = "L"
/*
function ScrollContF(sender) {
    if (curr_scrl_pos == "L") {
        $(sender).html("<");
        curr_scrl_pos = "R";
        //$(document).scrollLeft(1050);
        $('html, body').animate({ scrollLeft: 1050 }, 600);
    }
    else {
        $(sender).html(">");
        curr_scrl_pos = "L";
        $("#conRMMenu1").css("display", "block");
        //$(document).scrollLeft(0);
        $('html, body').animate({ scrollLeft: 0 }, 600);
    }
}
*/
function ScrollContF(sender) {
    if (curr_scrl_pos == "L") {
        $(sender).html("<");
        curr_scrl_pos = "R";
        $('.srclArea1').animate({ scrollLeft: 640 }, 600);
    }
    else {
        $(sender).html(">");
        curr_scrl_pos = "L";
        curr_scrl_pos2 = "L";

        $("#conRMMenu1").css("display", "block");
        $('.srclArea1').animate({ scrollLeft: 0 }, 600);
    }
}

function ScrollContF2(sender) {
    if (curr_scrl_pos2 == "L") {
        $(sender).html("<");
        curr_scrl_pos2 = "R";
        $('.srclArea1').animate({ scrollLeft: 860 }, 600);
    }
    else {
        $(sender).html(">");
        curr_scrl_pos2 = "L";
        $("#conRMMenu1").css("display", "block");
        $('.srclArea1').animate({ scrollLeft: 640 }, 600);
    }
}





function ScrollContLeft(sender) {

    curr_scrl_pos = "L";
    $("#conRMMenu1").css("display", "block");

    $('.srclArea1').animate({ scrollLeft: 0 }, 600);
}



function ScrollContRight(sender) {

    curr_scrl_pos = "R";
    $("#conRMMenu1").css("display", "none");

    $('.srclArea1').animate({ scrollLeft: 640 }, 600);
}







var skip_items_arr = new Array();

function actAclick(object, index) {
    $("#songC_" + index).css("width", "255px");
    $(object).css("width", "40px");
    $(object).html("skip");
}

function actRclick(object, index) {
    $("#songC_" + index).css("width", "285px");
    $(object).css("width", "12px");
    $(object).html(">");
}


//-------------------------------------------------------------------------------------------------------











//-------------------------------------------------------------------------------------------------------

function onError(event) {

    Reps('error');

    if (event.data == 5)
        return;

    
    PlayNextVideo();
}



var done = false;
function onPlayerStateChange(event) {
    
    
    if (event.data == 5) {
        player.playVideo();
        return;
    }


    if (event.data == YT.PlayerState.ENDED) {
        Reps('ended');
        PlayNextVideo();
        return;
    }

    if (event.data == YT.PlayerState.PAUSED) {
        PLAY_STATE = "0";
        $("#PlPs_btn").css("background-image", "url('/imgs/player/play_icn.jpg')");
    }
    if (event.data == YT.PlayerState.PLAYING) {
        PLAY_STATE = "1";
        $("#PlPs_btn").css("background-image", "url('/imgs/player/pause_icn.jpg')");
    }
}



function currentPlaybackState() {

    var pers = 0;

    try
    {
        var pers = (player.getCurrentTime() / player.getDuration()) * 100;
    }
    catch(ex){}

    if (pers > 0) {
        $("#PlaybackStatus1").css("width", pers + "%");
    }
    else {
        $("#PlaybackStatus1").css("width", "0%");
    }
}

function changePlaybackStatus_Click(e) {
    var x;
    var y;
    if (e.pageX || e.pageY) {
        x = e.pageX;
        y = e.pageY;
    }
    else {
        x = e.clientX + document.body.scrollLeft + document.documentElement.scrollLeft;
        y = e.clientY + document.body.scrollTop + document.documentElement.scrollTop;
    }
    x -= PSC1.offsetLeft;
    y -= PSC1.offsetTop;



    var pers = (x / PlbkBar_W) * 100;

    var sec_pl = (pers * player.getDuration()) / 100;

    player.seekTo(sec_pl);
}


function PSC_over() {
    $("#PlaybackStatusInnrCont1").css("margin-top", "-2px");
    $("#PlaybackStatusInnrCont1").css("height", "6px");
    $("#PlaybackStatus1").css("height", "6px");
}

function PSC_out() {
    $("#PlaybackStatusInnrCont1").css("margin-top", "0px");
    $("#PlaybackStatusInnrCont1").css("height", "3px");
    $("#PlaybackStatus1").css("height", "3px");
}








function plVClick(video_id, i) {
    
    if (isReady == false) {
        i = i - 1;
    }

    CURR_VIDEO = i;
    Higlight_Item(CURR_VIDEO);

    player.loadVideoById(video_id, 0, 'default');

    PLAY_STATE = "1";
    $("#PlPs_btn").css("background-image", "url('/imgs/player/pause_icn.jpg')");
    
}





function PlayPrevVideo() {

    if (CURR_VIDEO > 0) {
        CURR_VIDEO = CURR_VIDEO - 1;
    }


    for (var j = 0; j < skip_items_arr.length; j++) {
        if (skip_items_arr[j] == CURR_VIDEO) {
            PlayPrevVideo();
        }
    }

    Reps('prev');

    player.stopVideo();
    player.loadVideoById(items_arr[CURR_VIDEO], 0, 'default');
    Higlight_Item(CURR_VIDEO);


    PLAY_STATE = "1";
    $("#PlPs_btn").css("background-image", "url('/imgs/player/pause_icn.jpg')");

}




function PlayNextVideo() {

    if (CURR_VIDEO < items_arr.length) {
        CURR_VIDEO = CURR_VIDEO + 1;
    }
    else {
        return;
    }


    for (var j = 0; j < skip_items_arr.length; j++) {
        if (skip_items_arr[j] == CURR_VIDEO) {
            PlayNextVideo();
        }
    }


    Reps('MB');


    player.stopVideo();
    player.loadVideoById(items_arr[CURR_VIDEO], 0, 'default');
    Higlight_Item(CURR_VIDEO);


    PLAY_STATE = "1";
    $("#PlPs_btn").css("background-image", "url('/imgs/player/pause_icn.jpg')");

}



var PLAY_STATE = "0";
var CURR_VIDEO = -1;
var SOUND_STATE = 1;


function PlayPauseVideo() {

    if (PLAY_STATE == "0") { //IF NOT STARTED START

        player.playVideo();


        PLAY_STATE = "1";
        $("#PlPs_btn").css("background-image", "url('/imgs/player/pause_icn.jpg')");
    }
    else { //IF STARTED NEED TO STOP
        player.pauseVideo();

        PLAY_STATE = "0";
        $("#PlPs_btn").css("background-image", "url('/imgs/player/play_icn.jpg')");
    }
}




function Higlight_Item(activeNum) {
    $(".playlistItemC").css("background-color", "#292929");
    $("#PlItem_" + activeNum).css("background-color", "#d88700");


    var offset_hlt = 0;
    for (var j = 0; j < skip_items_arr.length; j++) {
        if (skip_items_arr[j] <= CURR_VIDEO) {
            offset_hlt = offset_hlt + 1;
        }
    }

    var vertical_position = 0;
    vertical_position = ((CURR_VIDEO - offset_hlt) * 46);


    vertical_position = vertical_position - (46 * 2);



    $("#PlayerPlaylistHolder1").scrollTop(vertical_position);


    
    load_related_videos(); // need to check if skip
    //$("#currPlS1").html("&nbsp;Currently playing: " + $("#songC_" + CURR_VIDEO).html());
}





function MuteSound() {
    if (SOUND_STATE == 1) {
        player.mute();
        SOUND_STATE = 0;

        $("#Mute_btn").css("background-image", "url('/imgs/player/unmute_icn.jpg')");
    }
    else {
        player.unMute();
        SOUND_STATE = 1;

        $("#Mute_btn").css("background-image", "url('/imgs/player/mute_icn.jpg')");
    }

}



var song_title = "";
var song_guid = "";

function AddSong_CLICK() {
    song_guid = getQuerystring_str("v", player.getVideoUrl());
    ShowAddToMyPlaylistPopup("?song_guid=" + song_guid + "&song_title=" + song_title);
}


function AddNewPlaylistLikeComplete() {
    $("#PlstLikeBtn").css("background", "#353535 url('/imgs/player/like_playlist_icn_selected.jpg') no-repeat center");
}








function SoundMouseOver() {
    $("#Mute_btn_cont").css("width", "80px");
}

function SoundMouseOut() {
    $("#Mute_btn_cont").css("width", "33px");
}

function VolumeControl_Click(e) {
    var x;
    var y;
    if (e.pageX || e.pageY) {
        x = e.pageX;
        y = e.pageY;
    }
    else {
        x = e.clientX + document.body.scrollLeft + document.documentElement.scrollLeft;
        y = e.clientY + document.body.scrollTop + document.documentElement.scrollTop;
    }
    x -= volCont1.offsetLeft;
    y -= volCont1.offsetTop;



    var pers = (x / volContBar_W) * 100;
    player.setVolume(pers);
    //inmute if muted
    player.unMute();
    SOUND_STATE = 1;
    $("#Mute_btn").css("background-image", "url('/imgs/player/mute_icn.jpg')");

    $("#VolumeControlVal").css("width", pers + "%");
}





function load_related_videos() {
    $('#PopPlstsCH1').html("<span style='font-size:18px;'>&nbsp;&nbsp;Loading...</span>");


    if (isNP == 'Y') {
        document.title = $("#songC_" + CURR_VIDEO).html() + " | music playlist | hypster.com";
    }


    var ca_title = $("#songC_" + CURR_VIDEO).html().replace("&", "-").replace("feat.", "-").replace("feat", "-").split('-');

    var song_artist_str = "";
    if (ca_title.length > 0) {
        var search_term_str = ca_title[0].replace(/(\r\n|\n|\r)/gm, "").replace("&","").replace("?","").replace("%","");
        song_artist_str = toTitleCase(search_term_str);
    }


    

    if($("#si_" + items_arr[CURR_VIDEO]).val() != 0)
    {
        $.ajax({
            type: "POST",
            url: "/compatibilityCheck/getCompatiblePlaylistsMPL?Playlist_ID=" + curr_playlist_id + "&SongID=" + $("#si_" + items_arr[CURR_VIDEO]).val() + "|&song_artist=" + encodeURIComponent(song_artist_str),
            async: true,
            success: function (data) {
                $('#PopPlstsCH1').html(data);
            }
        });
    }
    else
    {
        $.ajax({
            type: "POST",
            url: "/compatibilityCheck/getCompatiblePlaylistsMPLSong?Song_GUID=" + items_arr[CURR_VIDEO],
            async: true,
            success: function (data) {
                $('#PopPlstsCH1').html(data);
            }
        });
    }



    
    if ( desc_length < 100 && iua == 'True' && curr_playlist_id != '' && curr_user_id != '') {
        setTimeout(function () { check_art_title(song_artist_str, curr_playlist_id); }, 5000);
    }
}




function check_art_title(title_str, curr_playlist_id) {
    
    var occ_num = 0;
    $(".songTtl").each(function (index) {

        if ($(this).html().indexOf(title_str) != -1) {
            occ_num += 1;
        }
    });

    if (occ_num > 1) {
        $.ajax({
            type: "POST",
            url: "/compatibilityCheck/check_art_title?Playlist_ID=" + curr_playlist_id + "&curr_user_id=" + curr_user_id + "&song_artist=" + title_str,
            async: true,
            success: function (data) {
            }
        });

    }
}








function toTitleCase(str) {
    return str.replace(/\w\S*/g, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase(); });
}

function trim1(str) {
    return str.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
}

function replaceAll(find, replace, str) {
    return str.replace(new RegExp(find, 'g'), replace);
}

//-------------------------------------------------------------------------------------------------------








//-------------------------------------------------------------------------------------------------------
function skipSong(index) {
    $("#PlItem_" + index).css("display", "none");
    skip_items_arr.push(index);

    if (index == CURR_VIDEO) {
        PlayNextVideo();
    }


    
    //trkskip
    $.ajax({
        type: "POST",
        url: "/home/trkskip/" + items_arr[index] + "?SID=" + $("#si_" + items_arr[index]).val(),
        async: true,
        success: function (data) {
        }
    });
}



function Reps(a) {
    $.ajax({
        type: "POST",
        url: "/home/reps/" + items_arr[CURR_VIDEO] + "?a=" + a + "&sid=" + $("#si_" + items_arr[CURR_VIDEO]).val(),
        async: true,
        success: function (data) {
        }
    });
}

//-------------------------------------------------------------------------------------------------------