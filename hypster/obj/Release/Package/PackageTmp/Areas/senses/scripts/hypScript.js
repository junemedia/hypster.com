
var myWidth = 0;
var myHeight = 0;
var active_menu = "";



var home_vis = false;


var att_speech_timer = null;



String.prototype.replaceAll = function (target, replacement) {
    return this.split(target).join(replacement);
};



$(document).ready(function () {
});

$(window).resize(function () {
});



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




function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) != -1) return c.substring(name.length, c.length);
    }
    return "";
}

//****************************************************************************************











/*   SPEECH METHODS    */
//****************************************************************************************

function cancelSpeech() {
    if (window.speechSynthesis != null) {
        window.speechSynthesis.cancel();
    }
}


function startSpeech(curr_msg_str) {
    if (is_speech_sound == 1) {
        if (window.speechSynthesis != null) {
            try {
                var msg = new SpeechSynthesisUtterance(curr_msg_str);
                window.speechSynthesis.speak(msg);
            }
            catch (e) {

                clearTimeout(att_speech_timer);

                att_speech_timer = setTimeout(function () {
                    document.getElementById("speechContainer").src = "http://" + speechDomain + "/sSpeech/getSpeech/" + currUser + "?qq=" + encodeURI(curr_msg_str);    
                }, 600);
            }
        }
        else {
            clearTimeout(att_speech_timer);

            att_speech_timer = setTimeout(function () {
                document.getElementById("speechContainer").src = "http://" + speechDomain + "/sSpeech/getSpeech/" + currUser + "?qq=" + encodeURI(curr_msg_str);
            }, 600);
        }
    }
}

function startSpeechOn(curr_msg_str) {
    if (window.speechSynthesis != null) {
        var msg = new SpeechSynthesisUtterance(curr_msg_str);
        window.speechSynthesis.speak(msg);
    }
    else {
        document.getElementById("speechContainer").src = "http://" + speechDomain + "/sSpeech/getSpeech/" + currUser + "?qq=" + encodeURI(curr_msg_str);
    }
}


function PlaySpeechSound(curr_sound) {

    try {
        var audio = new Audio('/Areas/senses/sound/' + curr_sound + '.mp3');
        audio.play();
    } catch (ex) { }

}



function pauseSpeech() {
    if (window.speechSynthesis != null) {
        window.speechSynthesis.pause();
    }
}

function resumeSpeech() {
    if (window.speechSynthesis != null) {
        window.speechSynthesis.resume();
    }
}


function isSpeechSpeaking() {
    if (window.speechSynthesis != null) {

        if (window.speechSynthesis.speaking == true) {
            return true;
        }

    }

    return false;
}

//****************************************************************************************










//*   VOICE COMMANDS   */
//****************************************************************************************


//contexts to implement
// myAccount
// home
// search
// radio
// myPlaylists
// MPL
// 

function StartVoiceCommand(context) {

    DisplayMicIcon();




    if (!('webkitSpeechRecognition' in window)) {

        //PlaySpeechSound('beep_01');
        
        setTimeout(function () {
                //handle error stuff here...
                slCtl.Content.SL2JS.StartVoiceCommand();

                setTimeout(function () {
                    slCtl.Content.SL2JS.StopVoiceCommand();

                    // proccess results
                    //
                    checkVoiceCommandFunction(context);

                    startSpeech('wait');

                }, 3300);
        }, 200);


    } else {

        cancelSpeech();
        PlaySpeechSound('beep_01');
        //startSpeechOn('say');
        //startSpeech('beep_01');
        
        setTimeout(function () {
            try {
                var recognition = new webkitSpeechRecognition();
                //recognition.continuous = true;
                //recognition.interimResults = true;
                recognition.lang = "en-US";
                recognition.onresult = function (event) {

                    // proccess results
                    //
                    var curr_speech = event.results[0][0]; 
                    console.log(event.results[0][0].transcript)
                    ProccessVoiceCommand(event.results[0][0].transcript);

                }
                recognition.start();
            } catch (ex) { }
        }, 200);


    }



}



function checkVoiceCommandFunction(context) {

    //process tgrough generic voice recognition
    //
    if (context == "search" || context == "myPlaylists" ) {

        setTimeout(function () {
            $.ajax({
                dataType: "jsonp",
                url: "http://" + speechDomain + "/sSpeechToText/getText?qq=" + currUser + "&context=" + context,
                async: true,
                success: function (data) {
                }
            });

        }, 3000);

        return;
    }
    else //process tgrough custom voice recognition
    {
        setTimeout(function () {
            $.ajax({
                dataType: "jsonp",
                url: "http://" + speechDomain + "/sSpeechToTextCustom/getText?qq=" + currUser + "&context=" + context,
                async: true,
                success: function (data) {
                }
            });

        }, 3000);

        return;
    }

}


//callback from voice.hypster recognizer
//
function sSpeechRes(data) {
    console.log(data.result);
    ProccessVoiceCommand(data.result);
}



function beepSoundCommandFunction(context) {
    PlaySpeechSound('beep_01');
}



function noMicCommandFunction() {
    $("#micContentHolder").append("<div id='micActiveMsg' style='width:300px; height:23px; line-height:23px; overflow:hidden; font-size:16px; background-color:#BCBCBC; margin:-70px 0 0 55px; padding:0 0 0 5px;'>Microphone not found</div>");
}


//****************************************************************************************





// voice commands proccessing
//****************************************************************************************



// check at the end
//
function handle_generic_commands(comm) {

    var isHandled = false;

    switch (comm.toLowerCase()) {
        case "my playlists":
        case "my playlist":
        case "playlists":
        case "playlist":
            $("#msgBox1").html("loading your playlists.....");


            cancelSpeech();
            startSpeech("Navigating to your playlists");


            setTimeout(function () {

                window.location = "/senses/sPlaylists/myPlaylists";

            }, 3000);

            isHandled = true;
            break;
        case "search":
        case "search music":
        case "music":
            $("#msgBox1").html("loading search.....");

            cancelSpeech();
            startSpeech("Navigating to search music");


            setTimeout(function () {

                window.location = "/senses/sSearch/index";

            }, 3000);

            isHandled = true;
            break;
        case "charts":
        case "chart":
        case "listen to charts":
        case "listen charts":
            $("#msgBox1").html("loading charts.....");

            cancelSpeech();
            startSpeech("Navigating to charts");


            setTimeout(function () {

                window.location = "/senses/sCharts/index";

            }, 3000);

            isHandled = true;
            break;
        case "radio":
        case "listen to radio":
        case "listen radio":
            $("#msgBox1").html("loading radio.....");

            cancelSpeech();
            startSpeech("Navigating to radio");


            setTimeout(function () {

                window.location = "/senses/sRadio/index";

            }, 3000);

            isHandled = true;
            break;
        case "my account":
        case "account":
        case "access my account":
            $("#msgBox1").html("loading account info.....");

            cancelSpeech();
            startSpeech("Navigating to my account");


            setTimeout(function () {

                window.location = "/senses/sAccount/ManageAccount";

            }, 3000);

            isHandled = true;
            break;


        case "home":
            hvc_NavigateHome();

            isHandled = true;
            break;


        case "sign out":
        case "log out":
        case "sign off":
            $("#msgBox1").html("logging you out.....");

            startSpeech('logging you out.....');

            setTimeout(function () {
                $.ajax({
                    type: "POST",
                    url: "/senses/sAccount/logOut",
                    async: true,
                    success: function (data) {
                        $("#bContHolder1").html(data);
                    }
                });
            }, 3000);

            isHandled = true;
            break;





        default:

            //cancelSpeech();
            //startSpeech("Please say again...");

            break;
    }

    return isHandled;
}




function handle_generic_commands_confirm(comm) {

    var isHandled = false;

    switch (comm.toLowerCase()) {
        case "my playlists":
        case "my playlist":
        case "playlists":
        case "playlist":
            $("#MyPlaylist_Actions_CH1").html("Press Space to navigate to your playlists");

            cancelSpeech();
            startSpeech("Press Space to navigate to your playlists");
            isConfirmation = true;

            setTimeout(function () {
                $("#MyPlaylist_Actions_CH1").html("");

                if (isConfirmed == true) {
                    window.location = "/senses/sPlaylists/myPlaylists";
                }
                isConfirmation = false;
                isConfirmed = false;

            }, 5500);

            isHandled = true;
            break;
        case "search":
        case "search music":
        case "music":
            $("#MyPlaylist_Actions_CH1").html("Press Space to navigate to search music");

            cancelSpeech();
            startSpeech("Press Space to navigate to search music");
            isConfirmation = true;

            setTimeout(function () {
                $("#MyPlaylist_Actions_CH1").html("");

                if (isConfirmed == true) {
                    window.location = "/senses/sSearch/index";
                }
                isConfirmation = false;
                isConfirmed = false;

            }, 5500);

            isHandled = true;
            break;
        case "charts":
        case "chart":
        case "listen to charts":
        case "listen charts":
            $("#MyPlaylist_Actions_CH1").html("Press Space to navigate to charts");

            cancelSpeech();
            startSpeech("Press Space to navigate to charts");
            isConfirmation = true;

            setTimeout(function () {
                $("#MyPlaylist_Actions_CH1").html("");

                if (isConfirmed == true) {
                    window.location = "/senses/sCharts/index";
                }
                isConfirmation = false;
                isConfirmed = false;

            }, 5500);

            isHandled = true;
            break;
        case "radio":
        case "listen to radio":
        case "listen radio":
            $("#MyPlaylist_Actions_CH1").html("Press Space to navigate to radio");

            cancelSpeech();
            startSpeech("Press Space to navigate to radio");
            isConfirmation = true;

            setTimeout(function () {
                $("#MyPlaylist_Actions_CH1").html("");

                if (isConfirmed == true) {
                    window.location = "/senses/sRadio/index";
                }
                isConfirmation = false;
                isConfirmed = false;

            }, 5500);

            isHandled = true;
            break;
        case "my account":
        case "account":
        case "access my account":
            $("#MyPlaylist_Actions_CH1").html("Press Space to navigate to my account");

            cancelSpeech();
            startSpeech("Press Space to navigate to my account");
            isConfirmation = true;

            setTimeout(function () {
                $("#MyPlaylist_Actions_CH1").html("");

                if (isConfirmed == true) {
                    window.location = "/senses/sAccount/ManageAccount";
                }
                isConfirmation = false;
                isConfirmed = false;

            }, 5500);

            isHandled = true;
            break;


        case "home":
            $("#MyPlaylist_Actions_CH1").html("Press Space to navigate to home");

            cancelSpeech();
            startSpeech("Press Space to navigate to home");
            isConfirmation = true;

            setTimeout(function () {
                $("#MyPlaylist_Actions_CH1").html("");

                if (isConfirmed == true) {
                    hvc_NavigateHome();
                }
                isConfirmation = false;
                isConfirmed = false;

            }, 5500);


            isHandled = true;
            break;



        default:
             break;
    }

    return isHandled;
}


//****************************************************************************************









//****************************************************************************************

function DisplayMicIcon() {

    //mic icon
    $(".cir_mic").css('left', ($(window).width() / 2 - 145) + 'px');
    $(".cir_mic").css('top', ($(window).height() / 2 - 145) + 'px');
    $(".cir_mic").css('display', 'block');

    setTimeout(function () {
        $(".cir_mic").css('left', '-300px');
        $(".cir_mic").css('top', '-300px');
        $(".cir_mic").css('display', 'none');
    }, 4000);

}






// HVC_FUNC
//****************************************************************************************

function hvc_NavigateHome() {
    $("#msgBox1").html("loading step 2...");
    window.location = "/senses/sStart/home";
}

//****************************************************************************************







//****************************************************************************************

function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";

    document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function eraseCookie(name) {
    createCookie(name, "", -1);
}

//****************************************************************************************





