$(document).ready(function () {


	var selectedDates = [];
	getUserScoreInfo();

	$("#tabs").tabs();
	$("#tabs").css("display", "block");

	var currentDate = moment(new Date()).format("DD/MM/YYYY");
	var currentMonth = new Date().getMonth() + 1;
	var currentYear = new Date().getFullYear();

	$("#dateValue").text(currentDate);


	var currentDateMMMMDYYYY = moment(new Date()).format("MMMM D, YYYY");
	$(".dateFullDailyReport").text(currentDateMMMMDYYYY);

	var dataDailyTotals = {
		labels: [' '],
		series: [1]
	};

	var optionsDailyTotals = {
		labelInterpolationFnc: function (value) {
			return value.slice(0, 7);
		},
	};


	var responsiveOptionsDailyTotals = [
	  ['screen and (min-width: 1px)', {
		  labelDirection: 'explode',
		  labelInterpolationFnc: function (value) {
			  return value;
		  }
	  }],
	  ['screen and (min-width: 300px)', {
		  labelOffset: 0,
		  chartPadding: 20
	  }],
	  ['screen and (min-width: 476px)', {
		  labelOffset: 35,
		  chartPadding: 20
	  }],
	  ['screen and (min-width: 640px)', {
		  chartPadding: 25,
		  labelOffset: 75,

	  }],
	  ['screen and (min-width: 768px)', {
		  labelOffset: 45,
		  chartPadding: 20
	  }],
	  ['screen and (min-width: 1024px)', {
		  labelOffset: 70,
		  chartPadding: 25
	  }]
	];


	new Chartist.Pie('#dailyTotalsChart', dataDailyTotals, optionsDailyTotals, responsiveOptionsDailyTotals);
	
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

			var dateMMMMDYYYY = moment(dateText).format("MMMM D, YYYY");
			$(".dateFullDailyReport").text(dateMMMMDYYYY);

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

	function getSelectedDateInSQLFormat() {
		var date = $("#datepicker").datepicker("getDate");
		$("#datepicker").datepicker("setDate", date);

		var dateUserFriendlyFormat = moment(date).format("DD/MM/YYYY");
		$("#dateValue").text(dateUserFriendlyFormat);

		var dateMMMMDYYYY = moment(date).format("MMMM D, YYYY");
		$(".dateFullDailyReport").text(dateMMMMDYYYY);

		var dateSQLFormat = moment(date).format("YYYY-MM-DD HH:mm:ss");
		return dateSQLFormat;
	}

	var dateSQLFormat = getSelectedDateInSQLFormat();
	getLoggedFoodsForDate(dateSQLFormat);

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

		var dateMMMMDYYYY = moment(date).format("MMMM D, YYYY");
		$(".dateFullDailyReport").text(dateMMMMDYYYY);

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

		var dateMMMMDYYYY = moment(date).format("MMMM D, YYYY");
		$(".dateFullDailyReport").text(dateMMMMDYYYY);

		var dateSQLFormat = moment(date).format("YYYY-MM-DD HH:mm:ss");
		getLoggedFoodsForDate(dateSQLFormat);

	});


	function updateDailyTotals(dailyTotals) {

		$("#caloriesDailyTotals").text(Math.ceil(dailyTotals.Calories));

		if (dailyTotals.Carbs == 0 && dailyTotals.Protein == 0 && dailyTotals.Fat == 0) {
			var dataDailyTotals = {
				labels: [' '],
				series: [1]
			};
		} else {

			var dataDailyTotals = {
				labels: ['Protein', 'Fat', 'Carbs'],
				series: [dailyTotals.Protein, dailyTotals.Fat, dailyTotals.Carbs]
			};

		}
		var dailyTotalsChart = $("#dailyTotalsChart");
		dailyTotalsChart.get(0).__chartist__.update(dataDailyTotals);


	};

	$("#showDailyReport").on("click", function () {
		
		$("#dailyTotalsTable").show();
		$("#showDailyReport").hide();
	   
	});
	$("#hideDailyReport").on("click", function () {
		$("#dailyTotalsTable").hide();
		$("#showDailyReport").show();
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

				var dailyTotals = {
					Calories: 0,
					Fat: 0,
					FatSat: 0,
					FatMono: 0,
					FatPoly: 0,
					Protein: 0,
					Carbs: 0,
					Sugar: 0,
					Fiber: 0,
					Cholesterol: 0,
					Sodium: 0
				};

				$.each(data.data, function (i, item) {

					item.DateCreated = moment(item.DateCreated).format("YYYY-MM-DD HH:mm:ss");
					$("#loggedFoodsContainer").prepend(Mustache.render(loggedFoodTemplate, item));

					dailyTotals.Calories = dailyTotals.Calories + item.Calories;
					dailyTotals.Fat = dailyTotals.Fat + item.FatGr;
					dailyTotals.FatSat = dailyTotals.FatSat + item.FatSatGr;
					dailyTotals.FatMono = dailyTotals.FatMono + item.FatMonoGr;
					dailyTotals.FatPoly = dailyTotals.FatPoly + item.FatPolyGr;
					dailyTotals.Protein = dailyTotals.Protein + item.ProteinGr;
					dailyTotals.Carbs = dailyTotals.Carbs + item.CarbsGr;
					dailyTotals.Sugar = dailyTotals.Sugar + item.SugarGr;
					dailyTotals.FIber = dailyTotals.Fiber + item.FiberGr;
					dailyTotals.Cholesterol = dailyTotals.Cholesterol + item.CholesterolMg;
					dailyTotals.Sodium = dailyTotals.Sodium + item.SodiumMg;
				});

				updateDailyTotals(dailyTotals);



				
				for (var i in dailyTotals) {
					dailyTotals[i] = Math.round(dailyTotals[i]);
				};

				$("#fullDailyReportTable").remove();
				$("#macroRatiosTableDailyReport").remove();

				var fullDailyReportTemplate = $("#fullDailyReportTemplate").html();

				$("#dailyTotalsTable").prepend(Mustache.render(fullDailyReportTemplate, dailyTotals));


				//var percentages = calculatePercentages(dailyTotals.Carbs, dailyTotals.Protein, dailyTotals.Fat);

				//$("#carbsRatioDailyReport").text("C: " + percentages.Carbs + "%");
				//$("#proteinRatioDailyReport").text("P: " + percentages.Protein + "%");
				//$("#fatRatioDailyReport").text("F: " + percentages.Fat + "%");

				hideNothingHereMessageIfLoggedFoods();
			},
			error: function () {
				
			},
		});
	}

	function calculatePercentages(carbs, protein, fat) {


		var totalCalories = carbs * 4 + fat * 8.8 + protein * 4;

		var carbsPercentage = Math.round(carbs * 4 / totalCalories * 100 * 10) / 10;
		var proteinPercentage = Math.round(protein * 4 / totalCalories * 100 * 10) / 10;
		var fatPercentage = Math.round(fat * 8.8 / totalCalories * 100 * 10) / 10;

		var percentages = {
			Carbs: carbsPercentage,
			Protein: proteinPercentage,
			Fat: fatPercentage
		}
		return percentages;
	};

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


				$('html,body').animate({
					scrollTop: $("#loggedFoodsContainer").offset().top - 65
				},'slow');

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
				var dateSQLFormat = getSelectedDateInSQLFormat();
				getLoggedFoodsForDate(dateSQLFormat);
				hideNothingHereMessageIfLoggedFoods();
				getYearMonthAndCallHighlightFunc();

				updateUserScoreInfo();

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

				var dateSQLFormat = getSelectedDateInSQLFormat();
				getLoggedFoodsForDate(dateSQLFormat);
				
				hideNothingHereMessageIfLoggedFoods();
				getYearMonthAndCallHighlightFunc();

			},
			error: function () {
				alert("error deleting row");
			}
		});

	});


	function getUserScoreInfo() {

		$.ajax({
			type: "POST",
			url: "Dashboard/GetUserScoreInfo",
			success: function (data) {
				var scoreCollapsedTemplate = $("#scoreCollapsedTemplate").html();
				var scoreFullTemplate = $("#scoreFullTemplate").html();

				$("#scoreInfoCollapsed").html(Mustache.render(scoreCollapsedTemplate, data.data));
				$("#scoreInfoFull").html(Mustache.render(scoreFullTemplate, data.data));

			},
			error: function () {
			    alert("didn't get user score");
			}
		});

	};

	function updateUserScoreInfo() {

		$.ajax({
			type: "POST",
			url: "Dashboard/UpdateUserScoreInfo",
			success: function (data) {

				$(".scoreValue").text(data.data.Score);
				$(".streakValue").text(data.data.Streak);
				$(".activeValue").text(data.data.ActiveDays);
				$(".levelValue").text(data.data.Level);

				$("#levelProgressBar").html("<span>" + data.data.ScoreToNextLevel + " pts to Level " + data.data.NextLevel + "</span>");
				$("#levelProgressBar").attr('aria-valuenow', data.data.PercentOfLevel).css('width', data.data.PercentOfLevel + "%");

			},
			error: function () {
				alert("didn't update userscore info");

			}
		});
	};





	//-----------------STATS-TAB----------------------------------------------------------------------------------//


	$("#statsTab").on("click", function(e){

		e.preventDefault();
		$(".newLoggedFoodCompact").removeClass("newLoggedFoodCompact");


	});

	//-----------------Scores-TAB--------------------------------------------------------------------------------//

	
	$("#scoresTab").on("click", function (e) {

		e.preventDefault();
		$(".newLoggedFoodCompact").removeClass("newLoggedFoodCompact");
		$("#rankTableContainer tbody").empty();
		getFollowed();



		$("#btnFollowUser").on("click", function () {
			var followedUserEmail = $("#followUserInput").val();
			
			$.ajax({
				type: "POST",
				url: "/DashBoard/AddFollowed",
				data: { followedUserEmail: followedUserEmail },
				success: function (data) {

					getFollowed();


				},
				error: function () {
					$("#NoSuchUserWarning").remove();
					$("#followedUsersList").before("<p id='NoSuchUserWarning'>There are no users with this email address.</p>");
				}
			});

		});


	});


	function getFollowed() {
		$.ajax({
			type: "POST",
			url: "/DashBoard/GetFollowed",
			success: function (data) {
				$.each(data.data, function (i, item) {
					$("#rankTableContainer tbody").append("<tr><td>" + item.Score +
						"</td><td>" + item.UserEmail + "</td><td><span class='glyphicon glyphicon-trash'></span></td></tr>");
				});
			},
		});
	}

	//-----------------PROFILE-TAB-------------------------------------------------------------------------------//


	$("#profileTab").on("click", function (e) {

		e.preventDefault();
		$(".newLoggedFoodCompact").removeClass("newLoggedFoodCompact");

	});



});


