$(document).ready(function(){
  $(".cart-qty-plus").click(function(){
   
	$(".qty").val(Number($(".qty").val())+1 );
	
  });
  $(".cart-qty-minus").click(function(){
   
	var amount = Number($(".qty").val());
	if (amount > 0) {
		$(".qty").val(amount-1);
	}
	
  });
});