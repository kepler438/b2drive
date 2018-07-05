$(function () {
	
	
	$('.subnavbar').find ('li').each (function (i) {
	
		var mod = i % 3;
		
		if (mod === 2) {
			$(this).addClass ('subnavbar-open-right');
		}
		
	});
});

$(document).ready(function () {

    if ($('.openInnerTable').length > 0) {
        $('.openInnerTable').on('click', function (e) {
            e.stopPropagation();
            $(this).find('innerTablebody').stop().slideToggle();
            $(this).stop().toggleClass('selected');
        });
    }

});