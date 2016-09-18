﻿$(document).ready(function () {

	var selectedDates = [];


	$("#tabs").tabs();
	$("#tabs").css("display", "block");

	var currentDate = moment(new Date()).format("DD/MM/YYYY");
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
				$(".loggedFoodCompact").show();
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

		var foodNutrients = {
			calsAmount: $(divId + " #calsAmountLogFood").data("calsamountlogfood"),
			fatAmount: $(divId + " #fatAmountLogFood").data("fatamountlogfood"),
			satFatAmount: $(divId + " #satFatAmountLogFood").data("satfatamountlogfood"),
			monoFatAmount: $(divId + " #monoFatAmountLogFood").data("monofatamountlogfood"),
			polyFatAmount: $(divId + " #polyFatAmountLogFood").data("polyfatamountlogfood"),
			carbsAmount: $(divId + " #carbsAmountLogFood").data("carbsamountlogfood"),
			fiberAmount: $(divId + " #fiberAmountLogFood").data("fiberamountlogfood"),
			sugarAmount: $(divId + " #sugarAmountLogFood").data("sugaramountlogfood"),
			sodiumAmount: $(divId + " #sodiumAmountLogFood").data("sodiumamountlogfood"),
			cholesAmount: $(divId + " #cholesAmountLogFood").data("cholesamountlogfood"),
			protAmount: $(divId + " #protAmountLogFood").data("protamountlogfood")
		}

		var grams = $(divId + " #unitsLogFood option:selected").val()
								  * $(divId + " #quantityLogFood").val() / 100;

		for (var i in foodNutrients) {
			if (foodNutrients[i] != "") {
				$(divId + " #" + i + "LogFood").text((grams * foodNutrients[i]).toFixed(1));
			}
		}
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
		var currentDateSQLFormat = moment(new Date()).format("YYYY-MM-DD HH:mm:ss");



		var thisLogFoodDiv = $(this).parent().parent().parent();
		var thisLogFoodDivId = "#" + thisLogFoodDiv.attr("id");

		var LoggedFood = {
			Name: $(thisLogFoodDivId + " #nameLogFood").text(),
			FoodId: $(thisLogFoodDivId + " #hiddenIdLogFood").text(),
			Amount: $(thisLogFoodDivId + " #quantityLogFood").val(),
			Unit: $(thisLogFoodDivId + " #unitsLogFood option:selected").text(),
			Calories: $(thisLogFoodDivId + " #calsAmountLogFood").text(),
			FatGr: $(thisLogFoodDivId + " #fatAmountLogFood").text(),
			FatSatGr: $(thisLogFoodDivId + " #satFatAmountLogFood").text(),
			FatMonoGr: $(thisLogFoodDivId + " #monoFatAmountLogFood").text(),
			FatPolyGr: $(thisLogFoodDivId + " #polyFatAmountLogFood").text(),
			CarbsGr: $(thisLogFoodDivId + " #carbsAmountLogFood").text(),
			FiberGr: $(thisLogFoodDivId + " #fiberAmountLogFood").text(),
			SugarGr: $(thisLogFoodDivId + " #sugarAmountLogFood").text(),
			SodiumMg: $(thisLogFoodDivId + " #sodiumAmountLogFood").text(),
			CholesterolMg: $(thisLogFoodDivId + " #cholesAmountLogFood").text(),
			ProteinGr: $(thisLogFoodDivId + " #protAmountLogFood").text(),
			DateUpdated: currentDateSQLFormat
		}


		if (thisLogFoodDivId == "#logFood") {

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
				alert("food not logged or updated");
			}
		});
	});


	//close current open logFood or editLoggedFood div
	$("#loggedFoodsContainer").on("click", ".cancelBtn", function () {

		var thisLogFoodDiv = $(this).parent().parent().parent();
		$(thisLogFoodDiv).hide();
		$(thisLogFoodDiv).prev().show();

		hideNothingHereMessageIfLoggedFoods();
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





}); 




