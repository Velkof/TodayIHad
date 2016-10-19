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
	var foodCalories = $("#userFoodCalories").val();
	var foodCarbs = $("#userFoodCarbs").val();
	var foodProtein = $("#userFoodProtein").val();
	var foodFat = $("#userFoodFat").val();


	if (foodName == "" || foodCalories == "" || foodCarbs == "" || foodProtein == "" || foodFat == "") {
		$(".validationMsg").remove();
		$("#continueBtn").before("<p class='validationMsg'>All fields are required.</p>");
	} else if (NameNotInProperFormat(foodName) || NumNotInRange(foodCalories) || NumNotInRange(foodCarbs) || NumNotInRange(foodProtein) || NumNotInRange(foodFat)) {
	} else {

		addFoodUnitsToArray();

		$("#createFoodStepNum").text("2");

		$("#createFoodStep1").hide();
		$("#createFoodStep2").show();

	}
});

$("#userFoodAmount, #userFoodUnit").on("change", function(){

	var amount = $("#userFoodAmount").val();
	var gramWeight = $("#userFoodUnit").find(":selected").data('gramweight');
	var gramsTotal = gramWeight * amount;
	$("#gramsTotal").val(gramsTotal);
	var selectedFoodUnit = $("#userFoodUnit option:selected").val();
	$("#createFoodPerAmount").html("<strong>" + amount + " x " + selectedFoodUnit + " or " + gramsTotal.toFixed(0) + " grams.</strong>");


});

function NumNotInRange (num){
	if (num >= 0 && num <= 99999)
		return false;
	else
		return true;
}

function NameNotInProperFormat (name) {
	if (name.length >= 2 && name.length <= 200 && /^[a-zA-Z0-9-_(),.%/]*$/.test(name))
		return false;
	else
		return true;
}


//cancel form submit and return to default foods/index state
$(".userFoodForm").on("click", "#cancelBtnCreateFood", function (e) {
	e.preventDefault();
	location.reload();
});

//go back to Step 1
$(".userFoodForm").on("click", "#backBtnCreateFood", function (e) {
	e.preventDefault();
	$("#createFoodStep1").show();
	$("#createFoodStep2").hide();

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





