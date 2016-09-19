//-------------------FOODS-----------------------------------------------------------------------------------//

//Retrives template for adding or removing units
$("#addUnit").on("click", function () {
			
	var unitsArray = []; //array of units that user has added - for  display in table

	$("#userFoodUnit option").each(function () {
		var optionName =  $(this).val();
		   
		if (optionName !== "gr" && optionName !== "oz") {

			var optionId = $(this).attr('id');
			var unitId = optionId.substring(6, 20);
			var optionGramWeight = $(this).data("gramweight");

			var unit = {
				Id: unitId,
				Name: optionName,
				GramWeight: optionGramWeight
			};
			unitsArray.push(unit);
		}              
	});

	$("#addFoodUnit").remove();
	var addFoodUnitTemplate = $('#addFoodUnitTemplate').html();
	$("#addUnitContainer").html(Mustache.render(addFoodUnitTemplate, {unitsArray:unitsArray}));

});


function addFoodUnitsToArray() {

	var foodUnitArray = [];

	$("#userFoodUnit option").each(function () {

		var gramWeight = $(this).attr("data-gramweight");
		var name = $(this).val();

		var foodUnit = {
			Name: name,
			GramWeight: gramWeight
		}
		foodUnitArray.push(foodUnit);
	});
	$("#foodUnits").val(JSON.stringify(foodUnitArray));
};


//continue to step 2 of form
$("#continueBtn").on("click", function (e) {        
	e.preventDefault();
	var foodName = $("#userFoodName").val();

	if (foodName !== null && foodName !== "") {
		var amount = $("#userFoodAmount").val();
		var gramWeight = $("#userFoodUnit").find(":selected").data('gramweight');
		var gramsTotal = gramWeight * amount;
		$("#gramsTotal").val(gramsTotal);

		addFoodUnitsToArray();

		var selectedFoodUnit = $("#userFoodUnit option:selected").val();

		$("#createFoodStepNum").text("2");
		$("#createFoodPerAmount").html("<strong>" + amount + " x " + selectedFoodUnit + " or " + gramsTotal + " grams.</strong>");

		$("#createFoodStep1").hide();
		$("#createFoodStep2").show();
	} else {
		$(".validationMsg").remove();
		$("#userFoodName").before("<p class='validationMsg'>* Food name cannot be empty</p>");
	}


});


//cancel form submit and return to default foods/index state
$(".userFoodForm").on("click", "#cancelBtnCreateFood", function (e) {
	e.preventDefault();
	location.reload();
});
		

//Add new unit
$("#addUnitContainer").on("click", "#addUnitBtn", function () {


	var unitId = Math.random().toString(36).substr(2, 10);
	var gramWeight = $("#weightInGramsAddUnit").val();
	var name = $("#nameAddUnit").val();

	$("#addUnitTable").append("<tr><td><input type='checkbox' name='checkboxUnit' id='" + unitId + "'></td><td id='nameUnit'>" + name + "</td><td id='weightInGramsUnit'>" + gramWeight + " g</td></tr>");
	$("#userFoodUnit").append('<option value="' + name + '" id="option'+unitId+'" data-gramweight="' + gramWeight + '" selected>' + name + '</option>');
	$("#userFoodAmount").val(1);

});

//close the addunit div
$("#addUnitContainer").on("click", "#closeBtnAddUnit", function () {
	$("#addUnitContainer").empty();
});

//delete selected units
$("#addUnitContainer").on("click", "#deleteUnitBtn", function () {
	$('[name="checkboxUnit"]:checked').each(function () {
			
		var checkBoxId = $(this).attr('id');

		$("#" + checkBoxId).parent().parent().remove();
		$("#option" + checkBoxId).remove();
	});

});





