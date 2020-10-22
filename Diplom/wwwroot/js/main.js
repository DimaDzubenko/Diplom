/**
 * Created by cuongact on 1/11/16.
 */
window.jQuery = window.$ = jQuery;
var Engo_Custom = {
    start: function() {
        "use strict";
        this.wishlist();
        this.tooltip();
        this.pageloader();
        this.searchv2();
        this.engoMobiledetect();
        this.sticky_header();
        this.engo_horizontal_megamenu();
        //this.engo_vertical_megamenu();
        this.engo_vertical_menu();
        this.engo_menu_sidebar();
        this.engo_full_height_slide();
        this.engo_menu_full();
        this.back_to_top();
        this.tab();
        this.sub_menu_header_v1();
        this.popup_v2();
        this.quantity();
        this.blog_main_v1();
        this.responsive_menu();
    },
    wishlist: function() {
        /* wishlist*/
        "use strict";
        var wishlist = $('.btn-wishlist');
        wishlist.click(function (e) {
            var seft = $(this);
            if(seft.hasClass('added')){
                seft.removeClass('added');
            }
            else {
                seft.addClass('added');
            }
            e.preventDefault();
        })
    },
    tooltip: function() {
        /* tooltip*/
        "use strict";
        $('[data-toggle="tooltip"]').tooltip({container: 'body'});
    },
    pageloader: function() {
        /* Pageloader */
        "use strict";
        $("#page-loader .page-loader-inner").delay(500).fadeIn(10, function () {
            $(this).fadeOut(500, function () {
                $("#page-loader").fadeOut(500);
            })
        })
    },
    searchv2: function(){
        /*  Search v2 */
        "use strict";
        var searchv2 = $('.search-box-v2 .search-btn'),
            textsearch = $('.search-box-v2 .text-search');

        searchv2.on( 'click', function(e) {
            if (textsearch.hasClass('active')) {
                textsearch.removeClass('active');
            }
            else  {
                textsearch.addClass('active');
            }
            e.preventDefault();
        });
        var searchv1 = $('.search-container .icon-search'),
            searchboxv1 = $('.search-container .search-box-v1'),
            closesearch = $('.search-container .search-box-v1 .close-search');

        searchv1.on( 'click', function(e){
            var widthvindown = $(window).width(),
                widsboxv1 = searchboxv1.width();
            searchboxv1.addClass('active');
            searchboxv1.css("left", (widthvindown - widsboxv1)/2);
            e.preventDefault();
        })
        closesearch.on('click', function(e) {
            searchboxv1.removeClass('active');
            e.preventDefault();
        } )
    },
    engo_horizontal_megamenu: function() {
        /*  Engo horizontal megamenu: */
        "use strict";
        var win = $(window).width();
        if (win > 1200) {
            /* horizontal */
            $(".engo-horizontal-megamenu #primary-menu > li.dropdown").each(function () {
                var dropdown        = $(this).children(".dropdown-menu"),
	                megamenu       = $("nav.megamenu"),
					dropdown_begin_mega = $(this).offset().left - megamenu.offset().left,
	                widthElement = $(this).children("a").outerWidth(),
                    elementpadding = 56,
                    arrowElement = $(dropdown).find(".arrow").outerWidth(),
                	marginElement = 0;

                if($(this).hasClass('has-mega')) {
                    if($(this).hasClass('aligned-fullwindown')) {
                        var dropdown_begin = $(this).offset().left;
                        $(dropdown).css({'left':-dropdown_begin,'width':$(window).width()});
                        $(dropdown).find(".arrow").css("left", dropdown_begin+ (widthElement - elementpadding - arrowElement)/2);
                    } else if($(this).hasClass('aligned-fullwidth')) {
                        $(dropdown).css({'left':-dropdown_begin_mega,'width':$(megamenu).width()});
                    } else if($(this).hasClass('aligned-right')) {
                        var dropdown_end_right = ($(megamenu).offset().left + $(megamenu).outerWidth()) - ($(this).offset().left + $(this).outerWidth()) ;
                        console.log(dropdown_end_right);
                        $(dropdown).css({'right':-dropdown_end_right});
                    } else if($(this).hasClass('aligned-left')) {
                        $(dropdown).css({'left':-dropdown_begin_mega});
                        marginElement = dropdown_begin_mega;
                        $(dropdown).find(".arrow").css("left", marginElement+ (widthElement - elementpadding - arrowElement)/2);
                    }
                    //else {
                    //    var end_right     = ($(window).width() - (dropdown.offset().left + dropdown.outerWidth())),
                    //    	end_right2    = ($(window).width() - (megamenu.offset().left + megamenu.outerWidth()));
                    //    if(end_right2 > end_right) {
                    //    }
                    //}
                } else {
                    var singleleft =80;
                    $(dropdown).find(".arrow").css("left", singleleft+ (widthElement - elementpadding - arrowElement)/2);
                }
                if(win > 991) {
                    $(this).hover(function(){
                        $("#primary-menu > li.dropdown").removeClass("at");
                        $(this).addClass("at");
                        $(dropdown).css("right", "auto");
                    },function () {
                        $(this).removeClass("at");
                    })
                }
            });

            /* vertical */
            $(".engomainmenu-vertical #primary-menu > li.dropdown").each(function () {
                var menu =$('.engomainmenu-vertical'),
                    megamenu= $('.dropdown-menu',$(this)),
                    dataw = megamenu.attr('data-width');
                console.log(dataw);
                if($(this).hasClass('has-mega')) {
                    var container = $(this).find('.container').width();
                    if($(this).hasClass('aligned-fullwidth') || $(this).hasClass('aligned-fullwindown')) {
                        megamenu.attr("style","width:" + ($(window).width() - menu.outerWidth())+"px !important;");
                    }
                    if($(this).hasClass('aligned-left')) {
                        megamenu.attr("style","width:" + dataw +"px !important;");
                    }
                    if (win < 1550) {
                        megamenu.find('.container').css({
                            'width': '100%',
                            'padding-left': '30px'
                        })
                    }
                }
                $(this).hover(function(){
                    $(this).addClass("at");
                },function () {
                    $(this).removeClass("at");
                });
            })
        }
    },
    engo_vertical_megamenu: function() {
        /*  Engo vertical megamenu: */
        "use strict";
        $(".engo-vertical-megamenu ul.megamenu > li.dropdown").each(function () {
            var dropdown        = $(this).children(".dropdown-menu"),
                megamenu       = $("ul.megamenu"),
                this_toggle = $(this).parent().parent(),
                wmegamenu = dropdown.attr('data-width'),
                marginElement = 0;

            console.log(wmegamenu);
            if($(this).hasClass('has-mega')) {
                if($(this).hasClass('aligned-fullwidth') || $(this).hasClass('aligned-fullwindown')) {
                    $(dropdown).attr("style","width:" +wmegamenu +"px !important; right:"+ - dropdown.outerWidth(true));
                } else if($(this).hasClass('aligned-right') || $(this).hasClass('aligned-left')) {
                    $(dropdown).css({
                        'right': - dropdown.outerWidth(true),
                        'width' : wmegamenu
                    });
                } else {
                    $(dropdown).css({'right': - dropdown.outerWidth(true)});
                }
            }else {
                $(dropdown).css({'right': - dropdown.outerWidth(true)});
            }

            var dropdownHeight = $(dropdown).outerHeight(),
                heightElement = $(this).children("a").outerHeight()/ 2,
                arrowElement = $(dropdown).find(".arrow").outerHeight();


            $(dropdown).css("top", -dropdownHeight/2 + heightElement);

            var end_bottom     = $(window).height() - (dropdown.outerHeight() + dropdown.offset().top);
            if(end_bottom <= 0) {
                if($(window).height() > dropdown.outerHeight()) {
                    $(dropdown).css("top", - ($(this).offset().top - ($(window).height() - dropdown.outerHeight())/2) );
                    marginElement = ($(window).height() - dropdown.outerHeight())/2;
                } else {
                    $(dropdown).css("top",- $(this).offset().top);
                }
            }

            $(dropdown).find(".arrow").css("top", marginElement + dropdownHeight/2 - arrowElement/2);

            $(this).hover(function(){
                $("ul.megamenu > li").removeClass("active");
                $(this).addClass("active");
            },function () {
                $(this).removeClass("active");
            });
        });
    },
    engo_vertical_menu: function(){
        /* Vertical menu */
        "use strict";
        var iconmenu = $('.icon-menu'),
            overlay = $('.overlayfixed'),
            vmenu    = $('.engomainmenu-vertical');

        var win = $(window).width();
        iconmenu.on('click', function(e){
            if ($(this).hasClass('active')){
                iconmenu.removeClass('active');
                vmenu.removeClass('active');
                overlay.removeClass('active');
                $('.bg-menu').removeClass('active');
                if (win < 767) {
                    $('.header-v3 .main-header').removeClass('active');
                }
            }
            else {
                iconmenu.addClass('active');
                vmenu.addClass('active');
                overlay.addClass('active');
                $('.bg-menu').addClass('active');
                if (win < 767) {
                    $('.header-v3 .main-header').addClass('active');
                }
            }
            e.preventDefault();
        });
        overlay.on('click',function(e){
            iconmenu.removeClass('active');
            vmenu.removeClass('active');
            overlay.removeClass('active');
            $('.bg-menu').removeClass('active');
        })
    },
    engo_menu_sidebar:function(){
        /* Menu sidebar */
        "use strict";
        if($('#sidebar').length) {
            var menusidebar = $('#sidebar .product-cat-widget .lever-01 >a');
            menusidebar.on('click',function(e){
                var seft = $(this),
                    menuchildsidebar = seft.parent().find('.sub-menu');
                if (seft.hasClass('active')) {
                    seft.removeClass('active');
                    menuchildsidebar.slideUp(400);
                }
                else {
                    seft.addClass('active');
                    menuchildsidebar.slideDown(400);
                }
                e.preventDefault();
            })
        }
    },
    engo_full_height_slide: function() {
        /* Full height slide */
        "use strict";
        $( window ).load(function() {
            var win =$(window).height();
            $(".bg-slider").css({"height": win });
            if (win < 720) {
                $(".engomainmenu-vertical").addClass('fixheight');
            }
            $(".engomainmenu-vertical").css({"min-height": win });
            $(window).on('resize', function(){
                var win = $(this).height();
                $(".bg-slider").css({"height": win});
                $(".engomainmenu-vertical").css({"min-height": win });
            });
        });
    },
    engo_menu_full : function() {
        /*  Menu full */
        "use strict";
        var main_nav = $(".main-nav"),
        	nav_inner = $('.nav-inner');
        $('.hamburger').on('click',function(e) {
            var seft =$(this);
            if (seft.hasClass('clicked')){
                seft.removeClass('clicked');
                main_nav.delay(100).slideUp(700,'easeInOutExpo',function() {
                    nav_inner.fadeIn(200);
                    main_nav.removeClass("nav-visible");
                });
            }
            else {
                seft.addClass('clicked');
                main_nav.slideDown(700,'easeInOutExpo',function(){
                    main_nav.addClass("nav-visible");
                    nav_inner.animate({ opacity: 1}, 700, 'easeInOutQuart');
                });
            }
            e.preventDefault();
        });
    },
    sticky_header: function() {
        "use strict";
        var self = this;
        if (($(window).width() >= 1200 ) && (self.engoMobiledetect() == false)) {
            var stickyelement = $('.engoj-sticky-header');
            if (stickyelement.hasClass('no-sticky')) return false;
            else {
                var engostickyonTop = stickyelement.offset().top;
                $(window).scroll(function () {
                    if ($(window).scrollTop() > engostickyonTop) {
                        stickyelement.addClass("stick");
                    } else {
                        stickyelement.removeClass("stick");
                    }
                });
            }
        }
    },
    engoMobiledetect: function() {
        var engomobile;
        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
            engomobile = true;
            $("html").addClass("mobile");
        }
        else {
            engomobile = false;
            $("html").addClass("no-mobile");
        }
        return engomobile;
    },
    back_to_top : function() {
        /* Back to top */
        "use strict";
        var offset = 300,
            offset_opacity = 1200,
            scroll_top_duration = 700,
            hwrap = $('body').height(),
            hfooter = $('footer#engo-footer').height(),
            hwindown = $(window).height(),
            $back_to_top = $('.totoplink a'),
            padingtop = 100;
        var  fix_footer = hwrap - hfooter - padingtop - hwindown;
        //console.log(hwrap);
        //console.log(hfooter);
        //console.log(fix_footer);
        $(window).scroll(function(){
            //console.log($(this).scrollTop());
            ( $(this).scrollTop() > offset ) ? $back_to_top.addClass('visible') : $back_to_top.removeClass('visible cd-fade-out');
            ( $(this).scrollTop() > fix_footer ) ? $back_to_top.addClass('btop-fix-footer') : $back_to_top.removeClass('btop-fix-footer');
            if( $(this).scrollTop() > offset_opacity ) {
                $back_to_top.addClass('cd-fade-out');
            }
        });

        $back_to_top.on('click', function(event){
            event.preventDefault();
            var seft = $(this);
            seft.addClass('clicked');
            setTimeout(function(){
                seft.removeClass('clicked');
            }, 1200);
            $('body,html').animate({
                    scrollTop: 0 ,
                }, scroll_top_duration
            );
        });
    },
    tab: function(){
        /*  [ Tabs ]
         - - - - - - - - - - - - - - - - - - - - */
        "use strict";
        var tabs_container = $( '.tabs-container' );
        tabs_container.each(function() {
            $( this ).find( '.tabs li:first-child a' ).addClass('active');
            $( this ).find( '.tab-content:first' ).show();

            $( this ).find('.tabs li a').on('click',function(e){
                e.preventDefault();
                var selector = $(this).attr('href');
                $(this).parent().parent().find('a').not(this).removeClass('active');
                $(this).addClass('active');
                $(this).parents(tabs_container).find('.tab-content').not(selector).slideUp(300);
                $(this).parents(tabs_container).find(selector).slideDown(300);
            });
        });

        $( '.toggle' ).each(function() {
            $( this ).find( '.toggle-controls li:first-child a' ).addClass('active');
            $( this ).find( '.toggle-content:first' ).show();

            $( this ).find('.toggle-controls li a').on('click',function(e){
                e.preventDefault();
                var selector = $(this).attr('href');
                $(this).parent().parent().find('a').not(this).removeClass('active');
                $(this).addClass('active');
                $(this).parents('.toggle').find('.toggle-content').not(selector).slideUp(300);
                $(this).parents('.toggle').find(selector).slideDown(300);
            });
        });
    },
    sub_menu_header_v1:function (){
        /*  Sub menu header-v1 */
        "use strict";
        var header_v1 = $('.header-v1'),
        	menu_h_v1 = $('.menu-header-v1');
        header_v1.each(function(){
            var submenu = $('.sub-menu', menu_h_v1),
                menuv1 = menu_h_v1.width(),
                fullhead = header_v1.width();
            submenu.css({
                'width':fullhead,
                'left':((menuv1 - fullhead) / 2)
            });
            $( window ).load(function() {
                var submenu = $('.sub-menu',menu_h_v1),
                    menuv1 = menu_h_v1.width(),
                    fullhead = header_v1.width();
                submenu.css({
                    'width':fullhead,
                    'left':((menuv1 - fullhead) / 2)
                });
            })
        })
    },
    popup_v2: function (){
        /*  Popup */
        "use strict";
        var popup_v2 = $('.popup-v2'),
        	modal_full = $('.modal-full');
        popup_v2.each(function(){
            //var win = $(window).height(),
            //    popuph = $('.modal-full .modal-content').height();

            //$('.modal-full').css({
            //    'top' : (win - popuph)
            //})
            popup_v2.on('shown.bs.modal', function () {
                modal_full.addClass('showpopup');
            })
            popup_v2.on('hidden.bs.modal', function () {
                modal_full.removeClass('showpopup');
            })
        })
    },
    quantity:function(){
        /* Quantity */
        "use strict";
        if( $( '.quantity' ).length > 0 ) {
            var minus =  $( '.minus'),
                plus  =  $( '.plus');

            minus.on( 'click', function(){
                var qty = $( this ).parent().find( '.qty' );
                if ( parseInt(qty.val(),10) <= 0 ) {
                    qty.val( 0 );
                } else {
                    qty.val( ( parseInt( qty.val(),10 ) - 1 ) );
                }
            });
            plus.on( 'click', function(){
                var qty = $( this ).parent().find( '.qty' );
                qty.val( ( parseInt( qty.val(),10 ) + 1 ) );
            });
        }
    },
    blog_main_v1: function(){
        "use strict";
        var wwin= $(window).width(),
            hblogv1 = $('.blog-main-v1').height(),
            whblov1 = $('.blog-main-v1').width();
        function imgRealSize(img) {
            var $img = $(img);
            if ($img.prop('naturalWidth') == undefined) {
                var $tmpImg = $('<img/>').attr('src', $img.attr('src'));
                $img.prop('naturalWidth', $tmpImg[0].width);
                $img.prop('naturalHeight', $tmpImg[0].height);
            }
            return { 'width': $img.prop('naturalWidth'), 'height': $img.prop('naturalHeight') }
        }

        if (1200 < wwin ){
            var screenImage = $(".blog-main-v1 .images-bg img"),
                img = imgRealSize( screenImage),
                imageWidth = img.width,
                imageHeight = img.height,
                a = whblov1 / hblogv1,
                b = imageWidth /imageHeight;

            if (a > b) {
                screenImage.css({
                    'width': whblov1,
                    'height' : ((whblov1 * imageHeight) / imageWidth)
                });
            }
            else  {
                screenImage.css({
                    'height': hblogv1,
                    'width' : ((hblogv1 * imageWidth) / imageHeight)
                })
            }
        }
    },
    responsive_menu : function () {
        /* Responsive menu */
        "use strict";
        var win = $(window).width();
        /* menu v4 */
        if ($('.header-v4').length) {
            var arrow_h4 = $('.engo-mainmenu ul.lever-01 .dropdown-menu .arrow'),
                lever2_h4 =$('.engo-mainmenu ul.lever-01 .dropdown-menu .lever-02'),
                title_h4 = $('.lever-02 .menu-menu h3.title'),
                menu_menu_h4 =$('.dropdown-menu .menu-menu ul'),
                menu_h4 = $('#engo-mainmenu'),
                iconmenu_h4 = $('a.menu');
            $('.header-v4').append("<div class='overlayfixed'></div>");

            iconmenu_h4.on('click',function(e){
                if ($(this).hasClass('active')){
                    $(this).removeClass('active');
                    menu_h4.removeClass('active');
                    $('body').removeClass('hfixed');
                }
                else {
                    $(this).addClass('active');
                    menu_h4.addClass('active');
                    $('body').addClass('hfixed');
                }
            })
            if (win < 1200) {
                arrow_h4.on('click',function(e){
                    var seft = $(this),
                        parent = seft.parent();

                    if (!(seft.hasClass('at'))) {
                        arrow_h4.removeClass('at');
                        seft.addClass('at');
                        lever2_h4.slideUp(400);
                        parent.find('.lever-02').slideDown(400);
                    }
                    else {
                        seft.removeClass('at');
                        parent.find('.lever-02').slideUp(400);
                    }
                    e.preventDefault();
                })
                title_h4.on('click',function(e){
                    var seft = $(this),
                        parent = seft.parent();
                    if(!(seft.hasClass('at'))) {
                        title_h4.removeClass('at');
                        menu_menu_h4.slideUp(400);
                        seft.addClass('at');
                        parent.find('ul').slideDown(400);
                    }
                    else {
                        seft.removeClass('at');
                        parent.find('ul').slideUp(400);
                    }
                    e.preventDefault();
                })
            }
        }
        /* menu v1 */
        if ($('.header-v1').length) {
            var menu_v1 = $('.header-v1 .main-menu .lever-01 > a'),
                submenu01 = $('.header-v1 .main-menu .lever-01 .sub-menu'),
                menu_h1 = $('.header-v1 .main-menu'),
                iconmenu_h1 = $('a.menu'),
                wrapper = $("#wrapper");
            $( window ).load(function() {
                var win = $(window).width();
                if (win < 992) {
                    wrapper.css({
                        'padding-top': '0'
                    });
                    $('.fix #wrapper').css({
                        'padding-top': '70px'
                    })
                }
                else  {
                    wrapper.css({
                        'padding-top': '0'
                    })
                }
                $(window).on('resize', function(){
                    var win = $(this).width();
                    if (win < 992) {
                        wrapper.css({
                            'padding-top': '0'
                        });
                        $('.fix #wrapper').css({
                            'padding-top': '70px'
                        })
                    }
                    else  {
                        wrapper.css({
                            'padding-top': '0'
                        })
                    }
                });
            })
            iconmenu_h1.on('click',function(e){
                if ($(this).hasClass('active')){
                    $(this).removeClass('active');
                    menu_h1.removeClass('active');
                    menu_v1.removeClass('at');
                    submenu01.slideUp(400);
                    $('body').removeClass('fix');
                }
                else {
                    $(this).addClass('active');
                    menu_h1.addClass('active');
                    $('body').addClass('fix');
                }
            })
            // menu_v1.on('click',function(e){
            //     var seft = $(this),
            //         parent = seft.parent();
            //     if(!(seft.hasClass('at'))) {
            //         menu_v1.removeClass('at');
            //         submenu01.slideUp(400);
            //         seft.addClass('at');
            //         parent.find('.sub-menu').slideDown(400);
            //     }
            //     else {
            //         seft.removeClass('at');
            //         parent.find('.sub-menu').slideUp(400);
            //     }
            //     e.preventDefault();
            // })
        }
        /* menu v3 */
        if ($('.header-v3').length) {
            var arrow_h3 = $('.engo-mainmenu ul.lever-01 .dropdown-menu .arrow'),
                lever2_h3 =$('.engo-mainmenu ul.lever-01 .dropdown-menu .lever-02'),
                title_h3 = $('.lever-02 .menu-menu h3.title'),
                menu_menu_h3 =$('.dropdown-menu .menu-menu ul');

                arrow_h3.on('click',function(e){
                var seft = $(this),
                    parent = seft.parent();

                if (!(seft.hasClass('at'))) {
                    arrow_h3.removeClass('at');
                    seft.addClass('at');
                    lever2_h3.slideUp(400);
                    parent.find('.lever-02').slideDown(400);
                }
                else {
                    seft.removeClass('at');
                    parent.find('.lever-02').slideUp(400);
                }
                e.preventDefault();
            })

            title_h3.on('click',function(e){
                var seft = $(this),
                    parent = seft.parent();
                if(!(seft.hasClass('at'))) {
                    title_h3.removeClass('at');
                    menu_menu_h3.slideUp(400);
                    seft.addClass('at');
                    parent.find('ul').slideDown(400);
                }
                else {
                    seft.removeClass('at');
                    parent.find('ul').slideUp(400);
                }
                e.preventDefault();
            })
        }
    }
}

jQuery(document).ready(function($){
    Engo_Custom.start();
});