(function($){

	$.fn.Engo_CountDown = function( options ) {
		"use strict";
	 	return this.each(function() {
			// get instance of the Engo_CountDown.
			new  $.Engo_CountDown( this, options );
		});
 	 }
	$.Engo_CountDown = function( obj, options ){
		"use strict";
		var ddiff = '',
			gsecs = '';
		this.options = $.extend({
				autoStart			: true,
				LeadingZero:true,
				DisplayFormat:"<div><span>%%D%%</span> Days</div><div><span>%%H%%</span> Hours</div><div><span>%%M%%</span> Mins</div><div><span>%%S%%</span> Secs</div>",
				FinishMessage:"Expired",
				CountActive:true,
				TargetDate:null
		}, options || {} );
		if( this.options.TargetDate == null || this.options.TargetDate == '' ){
			return ;
		}
		this.timer  = null;
		this.element = obj;
		this.CountStepper = -1;
		this.CountStepper = Math.ceil(this.CountStepper);
		this.SetTimeOutPeriod = (Math.abs(this.CountStepper)-1)*1000 + 990;
		var dthen = new Date(this.options.TargetDate);
		var dnow = new Date();
		if( this.CountStepper > 0 ) {
			ddiff = new Date(dnow-dthen);
		}
		else {
			 ddiff = new Date(dthen-dnow);
		}
		gsecs = Math.floor(ddiff.valueOf()/1000);
		this.CountBack(gsecs, this);

	};
	 $.Engo_CountDown.fn =  $.Engo_CountDown.prototype;
     $.Engo_CountDown.fn.extend =  $.Engo_CountDown.extend = $.extend;
	 $.Engo_CountDown.fn.extend({
		calculateDate:function( secs, num1, num2 ){
			"use strict";
			  var s = ((Math.floor(secs/num1))%num2).toString();
			  if ( this.options.LeadingZero && s.length < 2) {
					s = "0" + s;
			  }
			  return "<b>" + s + "</b>";
		},
		CountBack:function( secs, self ){
			"use strict";
			var DisplayStr = '';
			 if (secs < 0) {
				self.element.innerHTML = '<div class="lof-labelexpired"> '+self.options.FinishMessage+"</div>";
				return;
			  }
			  clearInterval(self.timer);
			  DisplayStr = self.options.DisplayFormat.replace(/%%D%%/g, self.calculateDate( secs,86400,100000) );
			  DisplayStr = DisplayStr.replace(/%%H%%/g, self.calculateDate(secs,3600,24));
			  DisplayStr = DisplayStr.replace(/%%M%%/g, self.calculateDate(secs,60,60));
			  DisplayStr = DisplayStr.replace(/%%S%%/g, self.calculateDate(secs,1,60));
			  self.element.innerHTML = DisplayStr;
			  if (self.options.CountActive) {
				   self.timer = null;
				 self.timer =  setTimeout( function(){
					self.CountBack((secs+self.CountStepper),self);
				},( self.SetTimeOutPeriod ) );
			 }
		}

	});


	$(document).ready(function(){
		"use strict";
		$('[data-countdown="countdown"]').each(function(index, el) {
            var $this = $(this);
            var $date = $this.data('date').split("-");
            $this.Engo_CountDown({
                TargetDate:$date[0]+"/"+$date[1]+"/"+$date[2]+" "+$date[3]+":"+$date[4]+":"+$date[5],
                DisplayFormat:"<div class=\"countdown-times\"><div class=\"day count\"><span class=\"number\">%%D%%</span> <span class=\"text\">Days</span></div> <div class=\"hours count\"><span class=\"number\">%%H%%</span> <span class=\"text\">Hours</span>  </div> <div class=\"minutes count\"><span class=\"number\">%%M%%</span> <span class=\"text\">Mins</span> </div> <div class=\"seconds count\"><span class=\"number\">%%S%%</span> <span class=\"text\">Secs</span> </div> </div>",
                FinishMessage: "Expired"
            });
        });
	});

	////


})(jQuery)
//
