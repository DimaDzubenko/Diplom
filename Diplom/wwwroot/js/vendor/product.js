(function($) {
	"use strict";
	$(document).ready(function(){
		var product = $('.product'),
			make3d = $(".make3D");

		$( window ).load(function() {
			var win =$(window).width(),
				full = product.parent();

			if (win > 479) {
				productchange();
			}
			else {
				$('.largeGrid').removeClass('active');
				full.removeClass('full-width');
				product.removeClass('engo-product-list');
				product.addClass('engo-product-grid')
				$('div.product').removeClass('large');
				make3d.removeClass('animate');
				make3d.removeClass('flip180');
				product.removeClass('large');
			}
			$(window).on('resize', function(){
				var win = $(this).width();
				if (win > 479) {
					productchange();
				}
				else {
					$('.largeGrid').removeClass('active');
					full.removeClass('full-width');
					product.removeClass('engo-product-list');
					product.addClass('engo-product-grid');
					$('div.product').removeClass('large');
					make3d.removeClass('animate');
					make3d.removeClass('flip180');
					make3d.removeClass('large');
				}
			});
		});
		function productchange(){
			var product = $('.product'),
			 	full = product.parent();
			$(".largeGrid").on('click',function(e){
				if (!($(this).hasClass('active'))) {
					$(this).addClass('active');
					$('.smallGrid').removeClass('active');
					full.addClass('full-width');
					product.removeClass('engo-product-grid')
					product.addClass('engo-product-list');
					$('.product').addClass('large').each(function(){
					});
					setTimeout(function(){
						$('.info-large').show();
					}, 200);
					setTimeout(function(){

						$('.view_gallery').trigger("click");
					}, 400);

					return false;
				}
				e.preventDefault();
			});

			$(".smallGrid").on('click',function(e){
				if (!($(this).hasClass('active'))) {
					$(this).addClass('active');
					$('.largeGrid').removeClass('active');
					full.removeClass('full-width');
					product.removeClass('engo-product-list');
					product.addClass('engo-product-grid')
					$('div.product').removeClass('large');
					$(".make3D").removeClass('animate');
					$('.info-large').fadeOut("fast");
					setTimeout(function(){
						$('div.flip-back').trigger("click");
					}, 400);
					$('.product').removeClass('large');
					return false;
				}
				e.preventDefault();
			});
		}
		//$(".smallGrid").click(function(){
		//
		//	return false;
		//});
		$('.colors-large a').on('click',function(){return false;});


		product.each(function(i, el){
			var win = $(window).width();
			if (win >= 992 ) {
				$(el).find('.make3D').hover(function(){
					$(this).parent().css('z-index', "20");
					$(this).addClass('animate');
					$(this).find('div.arrowNext, div.arrowPrev').addClass('visible');
				}, function(){
					$(this).removeClass('animate');
					$(this).parent().css('z-index', "1");
					$(this).find('div.arrowNext, div.arrowPrev').removeClass('visible');
				});
			}
			// Lift card and show stats on Mouseover

			// Flip card to the back side
			$(el).find('.view_gallery').on('click',function(e){

				$(el).find('div.arrowNext, div.arrowPrev').removeClass('visible');
				$(el).find('.make3D').addClass('flip-10');
				setTimeout(function(){
					$(el).find('.make3D').removeClass('flip-10').addClass('flip90').find('div.shadow').show().fadeTo( 80 , 1, function(){
						$(el).find('.product-front, .product-front div.shadow').hide();
					});
				}, 50);

				setTimeout(function(){
					$(el).find('.make3D').removeClass('flip90').addClass('flip190');
					$(el).find('.product-back').show().find('div.shadow').show().fadeTo( 90 , 0);
					setTimeout(function(){
						$(el).find('.make3D').removeClass('flip190').addClass('flip180').find('div.shadow').hide();
						setTimeout(function(){
							$(el).find('.make3D').css('transition', '100ms ease-out');
							$(el).find('.cx, .cy').addClass('s1');
							setTimeout(function(){$(el).find('.cx, .cy').addClass('s2');}, 100);
							setTimeout(function(){$(el).find('.cx, .cy').addClass('s3');}, 200);
							$(el).find('div.arrowNext, div.arrowPrev').addClass('visible');
						}, 100);
					}, 100);
				}, 150);
				e.preventDefault();
			});

			// Flip card back to the front side
			$(el).find('.flip-back').on('click',function(e){

				$(el).find('.make3D').removeClass('flip180').addClass('flip190');
				setTimeout(function(){
					$(el).find('.make3D').removeClass('flip190').addClass('flip90');

					$(el).find('.product-back div.shadow').css('opacity', 0).fadeTo( 100 , 1, function(){
						$(el).find('.product-back, .product-back div.shadow').hide();
						$(el).find('.product-front, .product-front div.shadow').show();
					});
				}, 50);

				setTimeout(function(){
					$(el).find('.make3D').removeClass('flip90').addClass('flip-10');
					$(el).find('.product-front div.shadow').show().fadeTo( 100 , 0);
					setTimeout(function(){
						$(el).find('.product-front div.shadow').hide();
						$(el).find('.make3D').removeClass('flip-10').css('transition', '100ms ease-out');
						$(el).find('.cx, .cy').removeClass('s1 s2 s3');
					}, 100);
				}, 150);
				e.preventDefault();

			});

			makeCarousel(el);
		});
		/*
		$('.add-cart-large').each(function(i, el){
			$(el).on('click',function(){
				var carousel = $(this).parent().parent().find(".carousel-container");
				var img = carousel.find('img').eq(carousel.attr("rel"))[0];
				var position = $(img).offset();

				var productName = $(this).parent().find('h4').get(0).innerHTML;

				$("body").append('<div class="floating-cart"></div>');
				var cart = $('div.floating-cart');
				$("<img src='"+img.src+"' class='floating-image-large' />").appendTo(cart);

				$(cart).css({'top' : position.top + 'px', "left" : position.left + 'px'}).fadeIn("slow").addClass('moveToCart');
				setTimeout(function(){$("body").addClass("MakeFloatingCart");}, 800);

				setTimeout(function(){
					$('div.floating-cart').remove();
					$("body").removeClass("MakeFloatingCart");


					var cartItem = "<div class='cart-item'><div class='img-wrap'><img src='"+img.src+"' alt='' /></div><span>"+productName+"</span><strong>$39</strong><div class='cart-item-border'></div><div class='delete-item'></div></div>";

					$("#cart .empty").hide();
					$("#cart").append(cartItem);
					$("#checkout").fadeIn(500);

					$("#cart .cart-item").last()
							.addClass("flash")
							.find(".delete-item").click(function(){
						$(this).parent().fadeOut(300, function(){
							$(this).remove();
							if($("#cart .cart-item").size() == 0){
								$("#cart .empty").fadeIn(500);
								$("#checkout").fadeOut(500);
							}
						})
					});
					setTimeout(function(){
						$("#cart .cart-item").last().removeClass("flash");
					}, 10 );

				}, 1000);


			});
		})
		*/
		/* ----  Image Gallery Carousel   ---- */
		function makeCarousel(el){


			var carousel = $(el).find('.carousel ul');
			var carouselSlideWidth = carousel.children("li").outerWidth();
			var carouselWidth = 0;
			var isAnimating = false;
			var currSlide = 0;
			$(carousel).attr('rel', currSlide);

			// building the width of the casousel
			$(carousel).find('li').each(function(){
				carouselWidth += carouselSlideWidth;
			});
			$(carousel).css('width', carouselWidth);

			// Load Next Image
			$(el).find('div.arrowNext').on('click', function(){
				var currentLeft = Math.abs(parseInt($(carousel).css("left")));
				var newLeft = currentLeft + carouselSlideWidth;
				if(newLeft == carouselWidth || isAnimating === true){return;}
				$(carousel).css({'left': "-" + newLeft + "px",
					"transition": "300ms ease-out"
				});
				isAnimating = true;
				currSlide++;
				$(carousel).attr('rel', currSlide);
				setTimeout(function(){isAnimating = false;}, 300);
			});

			// Load Previous Image
			$(el).find('div.arrowPrev').on('click', function(){
				var currentLeft = Math.abs(parseInt($(carousel).css("left")));
				var newLeft = currentLeft - carouselSlideWidth;
				if(newLeft < 0  || isAnimating === true){return;}
				$(carousel).css({'left': "-" + newLeft + "px",
					"transition": "300ms ease-out"
				});
				isAnimating = true;
				currSlide--;
				$(carousel).attr('rel', currSlide);
				setTimeout(function(){isAnimating = false;}, 300);
			});
		}

		$('.sizes a span, .categories a span').each(function(i, el){
			$(el).append('<span class="x"></span><span class="y"></span>');

			$(el).parent().on('click', function(){
				if($(this).hasClass('checked')){
					$(el).find('.y').removeClass('animate');
					setTimeout(function(){
						$(el).find('.x').removeClass('animate');
					}, 50);
					$(this).removeClass('checked');
					return false;
				}

				$(el).find('.x').addClass('animate');
				setTimeout(function(){
					$(el).find('.y').addClass('animate');
				}, 100);
				$(this).addClass('checked');
				return false;
			});
		});
		/**
		$('.add_to_cart').click(function(){
			var productCard = $(this).parent();
			var position = productCard.offset();
			var productImage = $(productCard).find('img').get(0).src;
			var productName = $(productCard).find('.product_name').get(0).innerHTML;

			$("body").append('<div class="floating-cart"></div>');
			var cart = $('div.floating-cart');
			productCard.clone().appendTo(cart);
			$(cart).css({'top' : position.top + 'px', "left" : position.left + 'px'}).fadeIn("slow").addClass('moveToCart');
			setTimeout(function(){$("body").addClass("MakeFloatingCart");}, 800);
			setTimeout(function(){
				$('div.floating-cart').remove();
				$("body").removeClass("MakeFloatingCart");


				var cartItem = "<div class='cart-item'><div class='img-wrap'><img src='"+productImage+"' alt='' /></div><span>"+productName+"</span><strong>$39</strong><div class='cart-item-border'></div><div class='delete-item'></div></div>";

				$("#cart .empty").hide();
				$("#cart").append(cartItem);
				$("#checkout").fadeIn(500);

				$("#cart .cart-item").last()
						.addClass("flash")
						.find(".delete-item").click(function(){
					$(this).parent().fadeOut(300, function(){
						$(this).remove();
						if($("#cart .cart-item").size() == 0){
							$("#cart .empty").fadeIn(500);
							$("#checkout").fadeOut(500);
						}
					})
				});
				setTimeout(function(){
					$("#cart .cart-item").last().removeClass("flash");
				}, 10 );

			}, 1000);
		});
		*/
	});
})(jQuery);