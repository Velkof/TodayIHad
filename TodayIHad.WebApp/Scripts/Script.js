$(document).ready(function () {

    var selectedDates = [];


	$("#tabs").tabs();
	$("#tabs").css("display", "block");

	var currentDate = moment(new Date()).format("DD/MM/YYYY");
	var currentDateSQLFormat = moment(new Date()).format("YYYY-MM-DD HH:mm:ss");
	var currentMonth = new Date().getMonth() + 1;
	var currentYear = new Date().getFullYear();

	$("#dateValue").text(currentDate);
	highlightDatesWhenFoodLoggedForDisplayedMonth(currentYear, currentMonth);


	function hideNothingHereMessageIfLoggedFoods() {

		if ($("#loggedFoodsContainer").has(".loggedFoodCompact").is(":visible")) {
			$("#nothingHere").hide();
		} else {
			$("#nothingHere").show();
		}

	}

	function getYearMonthAndCallHighlightFunc() {
	    var selectedDate = $("#datepicker").datepicker('getDate');
	    var selectedMonth = selectedDate.getMonth() + 1;
	    var selectedYear = selectedDate.getFullYear();

	    highlightDatesWhenFoodLoggedForDisplayedMonth(selectedYear, selectedMonth);
	}

	hideNothingHereMessageIfLoggedFoods();


	$('#datepicker').datepicker({
	    onSelect: function (dateText, inst) {


			var dateText = moment(dateText).format("YYYY-MM-DD HH:mm:ss");
			getLoggedFoodsForDate(dateText);

			var dateValue = moment(dateText).format("DD/MM/YYYY");
			$("#dateValue").text(dateValue);

		},
		onChangeMonthYear: function(year, month) {

			highlightDatesWhenFoodLoggedForDisplayedMonth(year, month);

		},
		beforeShowDay: function (date) {

            
			var highlight = selectedDates[date];
			if (highlight) {
				return [true, "highlightedDates", ""];
			}
			else {
				return [true, "", ""];
			}
		},
		maxDate: new Date()

	});




	//Toggle datepicker
	$("#date").on("click", function () {
		$("#datepicker").toggle();
	});

	$("#prevDate").on("click", function () {

		var date = $("#datepicker").datepicker("getDate");
		date.setDate(date.getDate() - 1);
		$("#datepicker").datepicker("setDate", date);


		var dateUserFriendlyFormat = moment(date).format("DD/MM/YYYY");
		$("#dateValue").text(dateUserFriendlyFormat);
		var dateSQLFormat = moment(date).format("YYYY-MM-DD HH:mm:ss");
		getLoggedFoodsForDate(dateSQLFormat);
	});


	$("#nextDate").on("click", function () {

		var date = $("#datepicker").datepicker("getDate");
		date.setDate(date.getDate() + 1);
		$("#datepicker").datepicker("setDate", date);


		var date = $("#datepicker").datepicker("getDate"); //gets date a second time so it doesn't go to next date if field disabled in calendar

		var dateUserFriendlyFormat = moment(date).format("DD/MM/YYYY");
		$("#dateValue").text(dateUserFriendlyFormat);
		var dateSQLFormat = moment(date).format("YYYY-MM-DD HH:mm:ss");
		getLoggedFoodsForDate(dateSQLFormat);

	});


	//Highlight dates function
	function highlightDatesWhenFoodLoggedForDisplayedMonth(year, month) {
		$.ajax({
			type: "POST",
			url: "/Dashboard/GetDatesWhenFoodLoggedForDisplayedMonth",
			dataType:"json", 
			data: {
				year: year,
				month: month
			},
			success: function (data) {
			    selectedDates = [];

				$.each(data.data, function(i, item){
					
					var highlightDate = moment(item.DateCreated).format("MM/DD/YYYY");
					selectedDates[new Date(highlightDate)] = new Date(highlightDate);
				});

				$("#datepicker").datepicker("refresh");
			},
			error: function () {
				//alert("highlighting failed");
			}
		});
	}


	//Get Logged Foods For date function
	function getLoggedFoodsForDate(date) {
		$.ajax({
			type: "POST",
			url: "/Dashboard/GetLoggedFoodsForDate",
			dataType: "json",
			data: { dateText: date },
			success: function (data) {

				$(".loggedFoodCompact").remove();
				$("#editLoggedFood").remove();
				$("#logFood").hide();

				var loggedFoodTemplate = $("#loggedFoodTemplate").html();

				$.each(data.data, function (i, item) {

					item.DateCreated = moment(item.DateCreated).format("YYYY-MM-DD HH:mm:ss");
					$("#loggedFoodsContainer").prepend(Mustache.render(loggedFoodTemplate, item));              
				});

				hideNothingHereMessageIfLoggedFoods();				
			},
			error: function () {
				alert("Couldn't get logged foods for selected date.");
			},
		});
	}

	//Get list of foods based on user search
	$("#foodSearchBox").on("input", function () {

		var food = $("#foodSearchBox").val();

		$.ajax({
			type: "POST",
			url: "/Dashboard/SearchFood",
			data: { searchFoodString: food },
			success: function (data) {

				$("#foodSearchUL").empty();

				$.each(data.data, function (i, item) {

					$("#foodSearchUL").append('<li class="foodSearchResultsLI list-group-item">' + item.Name + "</li>");
				});
			},
			error: function () {
				alert("Failed");
			}

		});
	});

	//Get clicked food from search list in the logfood div
	$("#foodSearchUL").on("click", "li", function () {
		var selectedFoodName = $(this).html();
		var selectedFood = this;
		$.ajax({
			type: "POST",
			url: "/Dashboard/GetSelectedFood",
			data: { foodName: selectedFoodName },
			success: function (data) {
			    $("#nothingHere").hide();

				$("#logFood").remove();
				$("#editLoggedFood").remove();

				$(".newLoggedFoodCompact").removeClass("newLoggedFoodCompact");
				$(selectedFood).remove();

				$("#btnContainerLogFood").empty();
				$("#foodSearchUL").empty();
				$("#unitsLogFood").empty();




				$("#btnContainerLogFood").append("<button class='btn btn-success saveBtn'>Save</button>");
				$("#btnContainerLogFood").append("<button class='btn btn-default cancelBtn'>Cancel</button>");

				for (var i in data) {

					var logFoodTemplate = $("#logFoodTemplate").html();
					$("#loggedFoodsContainer").prepend(Mustache.render(logFoodTemplate, data[i].food));

					for (var j in data[i].foodUnits) {
						$("#logFood #quantityLogFood").val(100);

						if (data[i].foodUnits[j].Name == "gr") {
							$("#logFood #unitsLogFood").append("<option value=\'" +data[i].foodUnits[j].GramWeight +
															   "\'  selected='selected'>" + data[i].foodUnits[j].Name + "</option>");
						} else {
							$("#logFood #unitsLogFood").append("<option value=\'" + data[i].foodUnits[j].GramWeight + "\'>" +
															   data[i].foodUnits[j].Name + "</option>");
						}
					}
				}

				var calories = $("#unitsLogFood option:selected").val()
							   * $("#quantityLogFood").val() * data[i].food.CaloriesKcal / 100;

			},
			error: function () {
				alert("fail");
			}
		});
	});


	//when units or quantity changes, update all nutrient and calorie values   
	$("#loggedFoodsContainer").on("change", "#unitsLogFood, #quantityLogFood", function () {

		var divId = "#" + $(this).parent().parent().attr("id");

		var caloriesAmount = $(divId + " #calsAmountLogFood").data("calsamountlogfood");
		var fatAmount = $(divId + " #fatAmountLogFood").data("fatamountlogfood");
		var satFatAmount = $(divId + " #satFatAmountLogFood").data("satfatamountlogfood");
		var monoFatAmount = $(divId + " #monoFatAmountLogFood").data("monofatamountlogfood");
		var polyFatAmount = $(divId + " #polyFatAmountLogFood").data("polyfatamountlogfood");
		var carbsAmount = $(divId + " #carbsAmountLogFood").data("carbsamountlogfood");
		var fiberAmount = $(divId + " #fiberAmountLogFood").data("fiberamountlogfood");
		var sugarAmount = $(divId + " #sugarAmountLogFood").data("sugaramountlogfood");
		var sodiumAmount = $(divId + " #sodiumAmountLogFood").data("sodiumamountlogfood");
		var cholesAmount = $(divId + " #cholesAmountLogFood").data("cholesamountlogfood");
		var protAmount = $(divId + " #protAmountLogFood").data("protamountlogfood");

		var grams = $(divId + " #unitsLogFood option:selected").val()
								  * $(divId + " #quantityLogFood").val() / 100;


		$(divId + " #calsAmountLogFood").text(Math.round(grams * caloriesAmount));
		$(divId + " #fatAmountLogFood").text((grams * fatAmount).toFixed(1));
		$(divId + " #satFatAmountLogFood").text((grams * satFatAmount).toFixed(1));
		$(divId + " #monoFatAmountLogFood").text((grams * monoFatAmount).toFixed(1));
		$(divId + " #polyFatAmountLogFood").text((grams * polyFatAmount).toFixed(1));
		$(divId + " #carbsAmountLogFood").text((grams * carbsAmount).toFixed(1));
		$(divId + " #fiberAmountLogFood").text((grams * fiberAmount).toFixed(1));
		$(divId + " #sugarAmountLogFood").text((grams * sugarAmount).toFixed(1));
		$(divId + " #sodiumAmountLogFood").text((grams * sodiumAmount).toFixed(1));
		$(divId + " #cholesAmountLogFood").text((grams * cholesAmount).toFixed(1));
		$(divId + " #protAmountLogFood").text((grams * protAmount).toFixed(1));

	});




	//open editLoggedFood and retrieve logged food
	$("#loggedFoodsContainer").on("click", ".loggedFoodCompact", function () {

		var loggedFoodFoodId = $(this).find(".hiddenFoodIdCompact").html();  
		var loggedFoodDateCreated = $(this).find(".hiddenDateCreatedCompact").html();
	
		var loggedFoodName = $(this).find(".loggedFoodNameCompact").text();
		var rowNumber = $(this).index();
		var clickedLoggedFoodCompact = this;
		$(".newLoggedFoodCompact").removeClass("newLoggedFoodCompact");
		$(".loggedFoodCompact").show();


		$.ajax({
			type: "POST",
			url: "/Dashboard/GetLoggedFood",
			data: {
				loggedFoodFoodId: loggedFoodFoodId,
				dateCreated: loggedFoodDateCreated,
			},
			success: function (data) {



				$("hiddenRowNumberCompact").text(rowNumber);


				$(clickedLoggedFoodCompact).hide();
				$("#logFood").remove();
				$("#editLoggedFood").remove();

				for (var i in data) {
					var editLoggedFoodTemplate = $("#editLoggedFoodTemplate").html();
					$(clickedLoggedFoodCompact).after(Mustache.render(editLoggedFoodTemplate, data[i]));

					$("#editLoggedFood #hiddenDateCreatedLoggedFood").text(loggedFoodDateCreated);

					for (var j in data[i].foodUnits) {

						if (data[i].foodUnits[j].Name == data[i].loggedFood.Unit) {
							$("#editLoggedFood #unitsLogFood")
								.append("<option value=\'" +
									data[i].foodUnits[j].GramWeight +
									"\'  selected='selected'>" +
									data[i].foodUnits[j].Name +
									"</option>");
						} else {
							$("#editLoggedFood #unitsLogFood")
								.append("<option value=\'" +
									data[i].foodUnits[j].GramWeight + "\'>" +
									data[i].foodUnits[j].Name + "</option>");
						}
					}
				}


			},
			error: function () {
				alert("couldn't retrieve and/or display logged food");
			}
		});
	});

	//Log Food or update logged food
	$("#loggedFoodsContainer").on("click", ".saveBtn, .updateBtn", function () {
		var thisLogFoodDiv = $(this).parent().parent().parent();
		var thisLogFoodDivId = "#" + thisLogFoodDiv.attr("id");

		var amount = $(thisLogFoodDivId + " #quantityLogFood").val();
		var unit = $(thisLogFoodDivId + " #unitsLogFood option:selected").text();
		var foodId = $(thisLogFoodDivId + " #hiddenIdLogFood").text();
		var name = $(thisLogFoodDivId + " #nameLogFood").text();
		var calories = $(thisLogFoodDivId + " #calsAmountLogFood").text();
		var fat = $(thisLogFoodDivId + " #fatAmountLogFood").text();
		var satFat = $(thisLogFoodDivId + " #satFatAmountLogFood").text();
		var monoFat = $(thisLogFoodDivId + " #monoFatAmountLogFood").text();
		var polyFat = $(thisLogFoodDivId + " #polyFatAmountLogFood").text();
		var carbs = $(thisLogFoodDivId + " #carbsAmountLogFood").text();
		var fiber = $(thisLogFoodDivId + " #fiberAmountLogFood").text();
		var sugar = $(thisLogFoodDivId + " #sugarAmountLogFood").text();
		var sodium = $(thisLogFoodDivId + " #sodiumAmountLogFood").text();
		var choles = $(thisLogFoodDivId + " #cholesAmountLogFood").text();
		var prot = $(thisLogFoodDivId + " #protAmountLogFood").text();

		var LoggedFood = {
			Name: name,
			FoodId: foodId,
			Amount: amount,
			Unit: unit,
			Calories: calories,
			FatGr: fat,
			FatSatGr: satFat,
			FatMonoGr: monoFat,
			FatPolyGr: polyFat,
			CarbsGr: carbs,
			FiberGr: fiber,
			SugarGr: sugar,
			SodiumMg: sodium,
			CholesterolMg: choles,
			ProteinGr: prot,
			DateUpdated: currentDateSQLFormat
		}


		if (thisLogFoodDivId == "#logFood") {
			//var dateCreated = currentDateSQLFormat;
			//LoggedFood["DateCreated"] = currentDateSQLFormat;
			//var url = "/Dashboard/LogFood";
			var date = $("#datepicker").datepicker("getDate");
			var dateSQLFormat = moment(date).format("YYYY-MM-DD");  //date for which you want to enter food (the one currently selected on datepicker)
			var timeSQLFormat = moment(currentDateSQLFormat).format("HH:mm:ss"); //current hh:mm:ss so all dates would be unique, even if it would be incorrect for dates other than current
			var dateCreated = dateSQLFormat + " " + timeSQLFormat;

			LoggedFood["DateCreated"] = dateCreated;
			var url = "/Dashboard/LogFood";

		} else {
			var dateCreated = $("#editLoggedFood #hiddenDateCreatedLoggedFood").text();
			LoggedFood["DateCreated"] = dateCreated;

			var loggedFoodId = $("#editLoggedFood .hiddenLoggedFoodIdFull").text();
			LoggedFood["Id"] = loggedFoodId;

			var url = "/Dashboard/UpdateLoggedFood";

		}
		$.ajax({
			type: "POST",
			url: url,
			contentType: "application/json",
			traditional: true,
			data: JSON.stringify(LoggedFood),
			success: function (data) {


				$(".newLoggedFoodCompact").removeClass("newLoggedFoodCompact");


				var newLoggedFoodTemplate = $("#newLoggedFoodTemplate").html();

				$(thisLogFoodDiv).after(Mustache.render(newLoggedFoodTemplate, LoggedFood));

				$(thisLogFoodDiv).prev(".loggedFoodCompact").remove();

				$("#btnContainerLogFood").empty();

				$(thisLogFoodDiv).hide();

				hideNothingHereMessageIfLoggedFoods();
				getYearMonthAndCallHighlightFunc();

			},
			error: function () {
				alert("error1");
			}
		});

	});


	//close current open logFood or editLoggedFood div
	$("#loggedFoodsContainer").on("click", ".cancelBtn", function () {

		var thisLogFoodDiv = $(this).parent().parent().parent();
		$(thisLogFoodDiv).hide();
		$(thisLogFoodDiv).prev().show();

		hideNothingHereMessageIfLoggedFoods();

		//$(thisLogFoodDiv).prev().children(".loggedFoodNameCompact").addClass("newLoggedFoodNameCompact");
		//$(thisLogFoodDiv).prev().addClass("newLoggedFoodCompact").show();
	});


	//delete logged food
	$("#loggedFoodsContainer").on("click", ".deleteBtn", function () {

		var editFoodDiv = $(this).parent().parent().parent();
		var loggedFoodId = $("#editLoggedFood .hiddenLoggedFoodIdFull").text();

		$.ajax({
			type: "POST",
			url: "/Dashboard/DeleteLoggedFood",
			data: { loggedFoodId: loggedFoodId },
			success: function (data) {
				$(editFoodDiv).prev(".loggedFoodCompact").remove();
				$(editFoodDiv).remove();


				hideNothingHereMessageIfLoggedFoods();
				getYearMonthAndCallHighlightFunc();

			},
			error: function () {
				alert("error deleting row");
			}
		});

	});

//-----------------STATS-TAB----------------------------------------------------------------------------------//


$("#statsTab").on("click", function(e){

	e.preventDefault();
	$(".newLoggedFoodCompact").removeClass("newLoggedFoodCompact");

});

//-----------------FRIENDS-TAB--------------------------------------------------------------------------------//

	
$("#friendsTab").on("click", function (e) {

	e.preventDefault();
	$(".newLoggedFoodCompact").removeClass("newLoggedFoodCompact");

});

//-----------------PROFILE-TAB-------------------------------------------------------------------------------//


$("#profileTab").on("click", function (e) {

	e.preventDefault();
	$(".newLoggedFoodCompact").removeClass("newLoggedFoodCompact");

});



//-------------------FOODS-----------------------------------------------------------------------------------//

	//Retrives template for adding or removing units
	$("#addUnit").on("click", function () {

		$("#addFoodUnit").remove();
		var addFoodUnitTemplate = $('#addFoodUnitTemplate').html();
		$("#addUnitContainer").html(Mustache.render(addFoodUnitTemplate));

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

		var amount = $("#userFoodAmount").val();
		var gramWeight = $("#userFoodUnit").find(":selected").data('gramweight');
		var gramsTotal = gramWeight * amount;
		$("#gramsTotal").val(gramsTotal);


		addFoodUnitsToArray();

		var selectedFoodUnit = $("#userFoodUnit option:selected").val();

		$("#createFoodStepNum").text("2");
		$("#createFoodPerAmount").html("<strong>per " + amount + " " + selectedFoodUnit + ".</strong>");

		$("#createFoodStep1").hide();
		$("#createFoodStep2").show();

	});


	//cancel form submit and return to default foods/index state
	$(".userFoodForm").on("click", "#cancelBtnCreateFood", function (e) {
		e.preventDefault();
		location.reload();
	});
		

	//Add new unit
	$("#addUnitContainer").on("click", "#addUnitBtn", function () {


		var gramWeight = $("#weightInGramsAddUnit").val();
		var name = $("#nameAddUnit").val();

		$("#addUnitTable").append("<tr><td id='nameUnit'>" + name + "</td><td id='weightInGramsUnit'>" + gramWeight  + "</td><td id='removeUnit' class='glyphicon glyphicon-remove'></td></tr>");		
		$("#userFoodUnit").append('<option value="' + name + '" data-gramweight="' + gramWeight + '" selected>' + name + '</option>');
		$("#userFoodAmount").val(1);
	});

	$("#addUnitContainer").on("click", "#closeBtnAddUnit", function () {

		$("#addUnitContainer").empty();
	});

}); 




