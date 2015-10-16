﻿var isNP = '';

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











function onError(event) {
    Reps('error');

    if (event.data == 5)
        return;
    
    PlayNextVideo();
}



var done = false;
function onPlayerStateChange(event) {

    //console.log("StateChange --" + event.data);
    

    if (event.data == 5) {
        player.playVideo();
        return;
    }

    if (event.data == YT.PlayerState.ENDED) {
        Reps('ended');
        //rLoad();
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
        pers = (player.getCurrentTime() / player.getDuration()) * 100;
    }
    catch (ex) {
        //console.log("Exc | currTime +++++" + player.getDuration() + " pers " + pers);
        //player.addEventListener("onStateChange", "onPlayerStateChange");
    }
    //console.log(" currDurr ++" + player.getDuration() + " currTime" + player.getCurrentTime() + " pers " + pers);

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

function changePlaybackStatus_Duration(e) {
    
    var x = e;
    
    //x -= PSC1.offsetLeft;
    
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

    Reps('next');
    rLoad();

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


function PlayVideo() {

    player.playVideo();

    PLAY_STATE = "1";
    $("#PlPs_btn").css("background-image", "url('/imgs/player/pause_icn.jpg')");
}

function PauseVideo() {

    player.pauseVideo();

    PLAY_STATE = "0";
    $("#PlPs_btn").css("background-image", "url('/imgs/player/play_icn.jpg')");
}








function Higlight_Item(activeNum) {
    $(".playlistItemC").css("background-color", "#292929");
    $("#PlItem_" + activeNum).css("background-color", "#CC9933");


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

    rLoad();
    
    load_related_videos(); // need to check if skip
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


    //if (isNP == 'Y') {
    //    document.title = $("#songC_" + CURR_VIDEO).html() + " | music playlist | hypster.com";
    //}


    var ca_title = $("#songC_" + CURR_VIDEO).html().replace("&", "-").replace("feat.", "-").replace("feat", "-").split('-');

    var song_artist_str = "";
    if (ca_title.length > 0) {
        var search_term_str = ca_title[0].replace(/(\r\n|\n|\r)/gm, "").replace("&", "").replace("?", "").replace("%", "");

        var div = document.createElement("div");
        div.innerHTML = search_term_str;
        var text = div.textContent || div.innerText || "";

        song_artist_str = toTitleCase(text);
    }


    

    if($("#si_" + items_arr[CURR_VIDEO]).val() != 0)
    {
        $.ajax({
            type: "POST",
            url: "/compatibilityCheck/getCompatiblePlaylistsMPL?Playlist_ID=" + curr_playlist_id + "&SongID=" + $("#si_" + items_arr[CURR_VIDEO]).val() + "|&song_artist=" + encodeURIComponent(song_artist_str.replace(/<(?:.|\n)*?>/gm, '')),
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

function rLoad() {
}









function SharesNum() {
    
    var shares_num = $('.button-type-post-share .ra1-pw-button-counter-value').html();
    $.ajax({
        type: "POST",
        url: "/playlist/chSSres?PL_ID=" + curr_playlist_id + "&US_ID=" + curr_user_id + "&SN=" + shares_num,
        async: true,
        success: function (data) {
        }
    });
}


function HistRec(page_title) {

    var h_title = $('<div/>').text(page_title).html();
    var h_url = $('<div/>').text(document.URL).html();

    $.ajax({
        type: "POST",
        url: "/playlists/ListenHistory?title=" + h_title + "&url=" + h_url,
        async: true,
        success: function (data) {
        }
    });
}




function ShuffleSongsI(us_id) {
    var object = document.getElementById("dr_PlaylistList");
    $("#PlayerPlaylistHolder1").html("");

    rLoad();

    if (object != null) {
        $.ajax({
            type: "POST",
            url: "/playlist/getPlaylistDetailsPlayer?PL_ID=" + object[object.selectedIndex].value + "&US_ID=" + us_id + "&Sort=Rand",
            async: true,
            success: function (data) {
                $('#PlayerPlaylistHolder1').html(data);
            }
        });
    }
    else {
        $.ajax({
            type: "POST",
            url: "/playlist/getPlaylistDetailsPlayer?PL_ID=" + curr_playlist_id + "&US_ID=" + us_id + "&Sort=Rand",
            async: true,
            success: function (data) {
                $('#PlayerPlaylistHolder1').html(data);
            }
        });
    }
}




function LoadMoreSongsPlaylist() {
    curr_playlist_segment = curr_playlist_segment + 1;
    $('.LoadMoreSongs_btn').css('display', 'none');


    $.ajax({
        type: "POST",
        url: "/playlist/getPlaylistSegmentPlayer?PL_ID=" + curr_playlist_id + "&US_ID=" + curr_user_id + "&Seg=" + curr_playlist_segment,
        async: true,
        success: function (data) {
            $('#PlayerPlaylistHolder1').append(data);
        }
    });
}


function srchTagS(p_ss) {
    $.ajax({
        type: "POST",
        url: "/compatibilityCheck/getCompatibleTagsPlaylists/" + p_ss.replace(/^\s\s*/, '').replace(/\s\s*$/, ''),
        async: true,
        success: function (data) {
            $('#PopPlstsCH1').html(data);
        }
    });
}



function openCHCont() {
    var ss_cont = document.createElement('script');
    ss_cont.src = "http://i.po.st/static/v3/post-widget.js#publisherKey=caq8eudavn7j6erou9n3";
    var ss_cont_obj = document.getElementById('ShrContH1innt');
    ss_cont_obj.parentNode.insertBefore(ss_cont, ss_cont_obj);
}