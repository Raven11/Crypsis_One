$(document).ready(function() {
	
	$('html').niceScroll();
	
	$("nav").height($(document).height());
	
	// Add class to last list item of submenu	
	$("ul.submenu li:last-child").addClass("last");	
		
	// Navigation accordion menu
	$(window).bind("load", function(){								   
		$("nav ul li:has(ul)").hover(function(){
			$(this).find("ul.submenu").stop("true", "true").slideDown(500);
		}, function(){
			$(this).find("ul.submenu").stop("true", "true").delay(100).slideUp(500);
		});
	});
	
	// Mobile navigation
	$(".ico-font").toggle(function(){
		$("nav").animate({"left" : 0}, 20);
		$("section.content").animate({ width: $(document).width-200, marginLeft: 200, marginRight: 0}, 20);
	}, function(){
		$("nav").animate({"left" : "-200px"});
		$("section.content").animate({ width: $(document).width+200, marginLeft: 0, marginRight: 0}, 400);
		return false;
	}); 
	
	$(window).bind("load", function(){
		var w=($(document).width());
		if(w >= 768 && $("nav").css('left')=='-200px') {
			$("nav").show();
			$("section.content").css('width' , $(document).width-200);
		}
	});
});