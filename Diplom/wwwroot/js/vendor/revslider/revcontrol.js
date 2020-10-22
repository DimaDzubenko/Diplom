(function($) {
    "use strict";

    $(document).ready(function(){
        var slide = $('.r_slider'),
            rwidth =slide.attr('data-width'),
            rheight =slide.attr('data-height'),
            rscreen =slide.attr('data-screen'),
            rfullwidth=slide.attr('data-fullwidth');
        if(slide.length){
            var api = slide.revolution({
                delay:10000,
                startwidth:rwidth,
                startheight:rheight,
                autoHeight:"off",
                fullScreenAlignForce:"off",

                onHoverStop:"on",

                thumbWidth:100,
                thumbHeight:50,
                thumbAmount:3,

                hideThumbsOnMobile:"off",
                hideBulletsOnMobile:"off",
                hideArrowsOnMobile:"off",
                hideThumbsUnderResoluition:0,

                hideTimerBar:"on",
                hideThumbs:0,

                parallax:"mouse",
                parallaxBgFreeze:"on",
                parallaxLevels:[7,6,2,5,8,4,14,11,9,0],

                navigationType:"bullet",
                navigationArrows:"solo",
                navigationStyle: 'preview3',

                navigationHAlign:"center",
                navigationVAlign:"bottom",
                navigationHOffset:0,
                navigationVOffset:15,

                soloArrowLeftHalign:"left",
                soloArrowLeftValign:"center",
                soloArrowLeftHOffset:40,
                soloArrowLeftVOffset:0,

                soloArrowRightHalign:"right",
                soloArrowRightValign:"center",
                soloArrowRightHOffset:40,
                soloArrowRightVOffset:0,


                touchenabled:"on",

                stopAtSlide:-1,
                stopAfterLoops:-1,
                hideCaptionAtLimit:0,
                hideAllCaptionAtLilmit:0,
                hideSliderAtLimit:0,

                swipe_velocity: 0.7,
                swipe_min_touches: 1,
                swipe_max_touches: 1,
                drag_block_vertical: false,

                dottedOverlay:"none",

                fullWidth:rfullwidth,
                forceFullWidth:"off",
                fullScreen:rscreen,
                fullScreenOffsetContainer:"#topheader-to-offset",

                shadow:0

            });
        }
        $('.engo-banner').show().revolution(
            {
                delay:16000,
                startwidth:1860,
                startheight:945,
                hideThumbs:200,
                hideTimerBar:"on",

                thumbWidth:100,
                thumbHeight:50,
                thumbAmount:5,

                swipe_velocity: 0.7,
                swipe_min_touches: 1,
                swipe_max_touches: 1,
                drag_block_vertical: false,

                parallax:"mouse",
                parallaxBgFreeze:"on",
                parallaxLevels:[7,4,3,2,5,4,3,2,1,0],

                keyboardNavigation:"off",

                navigationHAlign:"center",
                navigationVAlign:"bottom",
                navigationHOffset:0,
                navigationVOffset:20,

                soloArrowLeftHalign:"left",
                soloArrowLeftValign:"center",
                soloArrowLeftHOffset:20,
                soloArrowLeftVOffset:0,

                soloArrowRightHalign:"right",
                soloArrowRightValign:"center",
                soloArrowRightHOffset:20,
                soloArrowRightVOffset:0,

                shadow:0,
                fullWidth:"off",
                fullScreen:"on",

                navigationType:"both",
                navigationArrows:"",
                navigationStyle: 'preview3',
                touchenabled:"on",
                onHoverStop:"on",

                spinner:"",

                stopLoop:"off",
                stopAfterLoops:-1,
                stopAtSlide:-1,

                shuffle:"off",

                autoHeight:"off",
                forceFullWidth:"off",

                dottedOverlay:"none",

                hideThumbsOnMobile:"off",
                hideNavDelayOnMobile:1500,
                hideBulletsOnMobile:"off",
                hideArrowsOnMobile:"off",
                hideThumbsUnderResolution:0,

                hideSliderAtLimit:0,
                hideCaptionAtLimit:0,
                hideAllCaptionAtLilmit:0,
                startWithSlide:0,
                fullScreenOffsetContainer: ".site-header"
            });
        $('.engo-slide').show().revolution(
            {
                delay:16000,
                startwidth:1920,
                startheight:1065,
                hideThumbs:200,
                hideTimerBar:"on",

                thumbWidth:100,
                thumbHeight:50,
                thumbAmount:5,

                navigationType:"both",
                navigationArrows:"",
                navigationStyle: 'preview3',
                touchenabled:"on",
                onHoverStop:"on",


                swipe_velocity: 0.7,
                swipe_min_touches: 1,
                swipe_max_touches: 1,
                drag_block_vertical: false,

                parallax:"mouse",
                parallaxBgFreeze:"on",
                parallaxLevels:[7,6,3,5,8,4,10,11,9,0],

                keyboardNavigation:"off",

                navigationHAlign:"center",
                navigationVAlign:"bottom",
                navigationHOffset:0,
                navigationVOffset:20,

                soloArrowLeftHalign:"left",
                soloArrowLeftValign:"center",
                soloArrowLeftHOffset:20,
                soloArrowLeftVOffset:0,

                soloArrowRightHalign:"right",
                soloArrowRightValign:"center",
                soloArrowRightHOffset:20,
                soloArrowRightVOffset:0,

                shadow:0,
                fullWidth:"off",
                fullScreen:"on",

                spinner:"",

                stopLoop:"off",
                stopAfterLoops:-1,
                stopAtSlide:-1,

                shuffle:"off",

                autoHeight:"off",
                forceFullWidth:"off",

                //dottedOverlay:"none",

                hideThumbsOnMobile:"off",
                hideNavDelayOnMobile:1500,
                hideBulletsOnMobile:"off",
                hideArrowsOnMobile:"off",
                hideThumbsUnderResolution:0,

                hideSliderAtLimit:0,
                hideCaptionAtLimit:0,
                hideAllCaptionAtLilmit:0,
                startWithSlide:0
            })
    });
})(jQuery);