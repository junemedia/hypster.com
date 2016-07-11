var myWidth = 0;
var myHeight = 0;
var active_menu = "";
var home_vis = false;
var refresh_delay = 40; // Refresh the ads every 'refresh_delay' seconds.
var refresh_max = 50; // Refresh the current ads spot for 'refresh_max' times.



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


    window.location = "/black/srhome/step_2";


    /*
    $.ajax({
        type: "POST",
        url: "/black/bhome/step_2",
        async: true,
        success: function (data) {
            $("#bContHolder1").html(data);
        }
    });
    */

}






//****************************************************************************************






