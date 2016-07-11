

// -----SLIDESHOW-----
//****************************************************************************************
var slideshow_stst = 0;
function StartSlideshow() {
    $('.mainSlideshowInnr').cycle({
        fx: 'scrollRight',
        speed: 500,
        timeout: 4000
    });

    slideshow_stst = 1;
}


function StopSlideshow() {
    $('.mainSlideshowInnr').cycle('pause');
    slideshow_stst = 0;
}


function StartStopSlideshow() {
    if (slideshow_stst == 1) {
        $('.mainSlideshowInnr').cycle('pause');
        slideshow_stst = 0;
    }
    else {
        $('.mainSlideshowInnr').cycle('resume');
        slideshow_stst = 1;
    }
}


function opnSlide(id) {
    $('.mainSlideshowInnr').cycle('pause');
    $('.HomeSldShow').css('display', 'none');
    $('#HomeSlide' + id).css('visibility', 'visible');
    $('#HomeSlide' + id).css('display', 'block');
    $('#HomeSlide' + id).css('left', '0px');
}







function ResizeSlides() {
    /*var wwidth = $(window).width() - 10;
    if ($(window).width() < 500) {
        return;
    }
    if ($(window).width() > 1024) {
        wwidth = ($(window).width() / 2) - 10;
    }
    //var pf = 250 / 706;
    var pf = 360 / 706;
    var new_height = wwidth * pf;
    $(".topLeft").css('height', new_height + 'px');
    */
    /*
    if ($(window).width() > 1024) {
        $("#listenSlide1_textBoxes").css('height', (new_height + 15) + 'px');
        $(".middleC1").css('height', new_height + 'px');
        $(".middleC2").css('height', new_height + 'px');
    }
    else {
        $("#listenSlide1_textBoxes").css('height', 175 + 'px');
        $(".middleC1").css('height', 160 + 'px');
        $(".middleC2").css('height', 160 + 'px');
    }*/
}

//****************************************************************************************