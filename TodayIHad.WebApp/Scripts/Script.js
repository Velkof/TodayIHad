$(document).ready(function () {


	$("#foodLogDialog").dialog({
		my: "center",
		at: "center",
		of: window,
		autoOpen: false,
		width: $(document).width() - 100,
	 });

	$("#loggedFoodEditDialog").dialog({
		my: "center",
		at: "center",
		of: window,
		autoOpen: false
	 });
	 


	$("#tabs").tabs();
	$("#tabs").css("display", "block");

	$("#datepicker").datepicker();


	//Get list of foods based on user search
	$("#foodSearchBox").on("input",function () { 

				var food = $("#foodSearchBox").val();

				$.ajax({
					type: "POST",
					url: "/Dashboard/SearchFood",
					data: { searchFoodString: food },
					success: function (data) {

						$("#foodSearchUL").empty();

						for (var i in data) {
							for (var j in data[i]) {
								$("#foodSearchUL")
									.append('<li class="foodSearchResultsLI list-group-item">' +
										data[i][j].Name + "</li>");
							}
						}

					},
					error: function () {
						alert("Failed");
					}

				});
	});


	////Get clicked food from search list in the logfood dialog
	//$("#foodSearchUL").on("click", "li", function () {
	//				var selectedFoodName = $(this).html();
	//				$.ajax({
	//					type: "POST",
	//					url: "/Dashboard/GetSelectedFood",
	//					data: { foodName: selectedFoodName },
	//					success: function (data) {
	//						$("#foodSearchUL").empty();
	//						$("#foodLogDialog").dialog("open");
	//						$("#foodLogDialogUnitSelect").empty();
	//						for (var i in data) {
	//							$("#foodLogDialog").dialog("option", "title", data[i].food.Name);
	//							$("#foodLogDialogCalories").text(
	//								data[i].food.CaloriesKcal);
	//							$("#hiddenFieldfoodLogDialog").val(data[i].food.Id);
	//							for (var j in data[i].foodUnits) {
	//								$("#foodLogDialogAmountInput").val(100);
	//								if (data[i].foodUnits[j].Name == "gr") {
	//									$("#foodLogDialogUnitSelect")
	//										.append("<option value=\'" +
	//											data[i].foodUnits[j].Name +
	//											"\'  selected='selected'>" +
	//											data[i].foodUnits[j].Name +
	//											"</option>");
	//								} else {
	//									$("#foodLogDialogUnitSelect")
	//										.append("<option value=\'" +
	//											data[i].foodUnits[j].Name + "\'>" +
	//											data[i].foodUnits[j].Name + "</option>");
	//								}
	//							}
	//						}
	//					},
	//					error: function () {
	//						alert("fail");
	//					}
	//				});
	//			});


	//Get clicked food from search list in the logfood dialog
	$("#foodSearchUL").on("click", "li", function () {
					var selectedFoodName = $(this).html();

					$.ajax({
						type: "POST",
						url: "/Dashboard/GetSelectedFood",
						data: { foodName: selectedFoodName },
						success: function (data) {
							$("#foodSearchUL").empty();
							$("#logFood").show();

							for (var i in data) {
							    $("#hiddenIdLogFood").text(data[i].food.Id);
								$("#nameLogFood").text(data[i].food.Name);

								$("#fatAmountLogFood").text(data[i].food.FatGr);
								$("#fatAmountLogFood").data("fatAmountLogFood", data[i].food.FatGr);
								$("#satFatAmountLogFood").text(data[i].food.FatSatGr);
								$("#satFatAmountLogFood").data("satFatAmountLogFood", data[i].food.FatSatGr);
								$("#monoFatAmountLogFood").text(data[i].food.FatMonoGr);
								$("#monoFatAmountLogFood").data("monoFatAmountLogFood" , data[i].food.FatMonoGr);
								$("#polyFatAmountLogFood").text(data[i].food.FatPolyGr);
								$("#polyFatAmountLogFood").data("polyFatAmountLogFood", data[i].food.FatPolyGr);
								$("#carbsAmountLogFood").text(data[i].food.CarbsGr);
								$("#carbsAmountLogFood").data("carbsAmountLogFood", data[i].food.CarbsGr);
								$("#fiberAmountLogFood").text(data[i].food.FiberGr);
								$("#fiberAmountLogFood").data("fiberAmountLogFood" , data[i].food.FiberGr);
								$("#sugarAmountLogFood").text(data[i].food.SugarGr);
								$("#sugarAmountLogFood").data("sugarAmountLogFood" , data[i].food.SugarGr);
								$("#sodiumAmountLogFood").text(data[i].food.SodiumMg);
								$("#sodiumAmountLogFood").data("sodiumAmountLogFood", data[i].food.SodiumMg);
								$("#cholesAmountLogFood").text(data[i].food.CholesterolMg);
								$("#cholesAmountLogFood").data("cholesAmountLogFood", data[i].food.CholesterolMg);
								$("#protAmountLogFood").text(data[i].food.ProteinGr);
								$("#protAmountLogFood").data("protAmountLogFood", data[i].food.ProteinGr);

								for (var j in data[i].foodUnits) {
								    $("#quantityLogFood").val(100);

									if (data[i].foodUnits[j].Name == "gr") {
									    $("#unitsLogFood")
											.append("<option value=\'" +
												data[i].foodUnits[j].GramWeight +
												"\'  selected='selected'>" +
												data[i].foodUnits[j].Name +
												"</option>");
									} else {
									    $("#unitsLogFood")
											.append("<option value=\'" +
												data[i].foodUnits[j].GramWeight + "\'>" +
												data[i].foodUnits[j].Name + "</option>");
									}
								}

							}

							var calories = $("#unitsLogFood option:selected").val()
								  * $("#quantityLogFood").val() * data[i].food.CaloriesKcal / 100;

							$("#calsAmountLogFood").text(calories);
							$("#calsAmountLogFood").data("calsAmountLogFood", calories);

						},
						error: function () {
							alert("fail");
						}
					});
				});


	$("#unitsLogFood, #quantityLogFood").on("change", function () {

	    var caloriesAmount = $("#calsAmountLogFood").data("calsAmountLogFood");
	    var fatAmount = $("#fatAmountLogFood").data("fatAmountLogFood");
	    var satFatAmount = $("#satFatAmountLogFood").data("satFatAmountLogFood");
	    var monoFatAmount = $("#monoFatAmountLogFood").data("monoFatAmountLogFood");
	    var polyFatAmount = $("#polyFatAmountLogFood").data("polyFatAmountLogFood");
	    var carbsAmount = $("#carbsAmountLogFood").data("carbsAmountLogFood");
	    var fiberAmount = $("#fiberAmountLogFood").data("fiberAmountLogFood");
	    var sugarAmount = $("#sugarAmountLogFood").data("sugarAmountLogFood");
	    var sodiumAmount = $("#sodiumAmountLogFood").data("sodiumAmountLogFood");
	    var cholesAmount = $("#cholesAmountLogFood").data("cholesAmountLogFood");
	    var protAmount = $("#protAmountLogFood").data("protAmountLogFood");

	    var grams = $("#unitsLogFood option:selected").val()
								  * $("#quantityLogFood").val() / 100;

	    //$("#actionContainerLogFood").append("<button class='btn btn-danger deleteBtn'>Delete</button>");

	    $("#calsAmountLogFood").text(Math.round(grams * caloriesAmount));
	    $("#fatAmountLogFood").text((grams * fatAmount).toFixed(1));
	    $("#satFatAmountLogFood").text((grams * satFatAmount).toFixed(1));
	    $("#monoFatAmountLogFood").text((grams * monoFatAmount).toFixed(1));
	    $("#polyFatAmountLogFood").text((grams * polyFatAmount).toFixed(1));
	    $("#carbsAmountLogFood").text((grams * carbsAmount).toFixed(1));
	    $("#fiberAmountLogFood").text((grams * fiberAmount).toFixed(1));
	    $("#sugarAmountLogFood").text((grams * sugarAmount).toFixed(1));
	    $("#sodiumAmountLogFood").text((grams * sodiumAmount).toFixed(1));
	    $("#cholesAmountLogFood").text((grams * cholesAmount).toFixed(1));
	    $("#protAmountLogFood").text((grams * protAmount).toFixed(1));




	});


	//Log Food
	$("#logFoodSaveBtn").on("click", function() {             
		var amount = $("#foodLogDialogAmountInput").val();
		var unit = $("#foodLogDialogUnitSelect").val();
		var foodId = $("#hiddenFieldfoodLogDialog").val();
		var name = $("#foodLogDialog").dialog("option", "title");
		var calories = $("#foodLogDialogCalories").html();
		//var dateCreated = new Date().toLocaleString();
		var dateCreated = moment(new Date()).format("YYYY-MM-DD HH:mm:ss");


		$.ajax({
			type: "POST",
			url: "/Dashboard/LogFood",
			dataType: "json",
			traditional: true,
			data: {
				amount: amount,
				unit: unit,
				foodId: foodId,
				name: name,
				calories: calories,
				dateCreated: dateCreated
			},
			success: function(data) {
				$("#foodLogDialog").dialog("close");


				$("#loggedFoodsContainer").prepend("<div class='loggedFoodCompact'>" +
												   "<span class='hiddenFoodIdCompact' style='display:none;'>" + foodId + "</span>" + 
												   "<span class='hiddenDateCreatedCompact' style='display:none;'>" + dateCreated + "</span>" + 
												   "<div class='loggedFoodNameCompact'>" + name + "</div>" +
												   "<div class='loggedFoodInfoCompact'>" +
												   "Calories: <span class='loggedFoodCaloriesCompact'>" + calories + "</span>" +
												   " | Quantity: <span class='loggedFoodQuantityCompact'>"+amount+"</span>" +
												   " | Unit: <span class='loggedFoodUnitCompact'>"+ unit + "</span></div></div>"
												   );

			},
			error: function() {
				alert("error1");
			}
		});

	});

		//open edit dialog and retrieve logged food

	$("#loggedFoodsContainer").on("click", ".loggedFoodCompact", function () {
		//var loggedFoodFoodId = $(this).siblings(':first-child').html();
		//var loggedFoodDateCreated = $(this).siblings(':first-child').next().html();
		//var loggedFoodName = $(this).siblings(':first-child').next().next().html();
		//var rowNumber = $(this).parent().parent().children().index($(this).parent());

		var loggedFoodFoodId = $(this).find(".hiddenFoodIdCompact").html();
		var loggedFoodDateCreated = $(this).find(".hiddenDateCreatedCompact").html();
		var loggedFoodName = $(this).find(".loggedFoodNameCompact").html();
		var rowNumber = $(this).parent().children().index($(this).parent());

		var clickedLoggedFoodCompact = this;

		$.ajax({
			type: "POST",
			url: "/Dashboard/GetLoggedFood",
			data: {
				loggedFoodFoodId: loggedFoodFoodId,
				dateCreated: loggedFoodDateCreated
			},
			success: function (data) {

				$(clickedLoggedFoodCompact).append("<p>test</p>");


				$("#loggedFoodEditDialogUnitSelect").empty();
				for (var i in data) {
					$("#hiddenFieldloggedFoodEditDialog").val(data[i].loggedFood.FoodId);
					$("#hiddenLoggedFoodIdEditDialog").val(data[i].loggedFood.Id);
					$("#hiddenTableRowNumber").val(rowNumber);
					$("#loggedFoodEditDialog").dialog("option", "title", data[i].loggedFood.Name);
					$("#loggedFoodEditDialogCalories").text("Calories: " + data[i].loggedFood.Calories);
					for (var j in data[i].foodUnits) {
						$("#loggedFoodEditDialogAmountInput").val(data[i].loggedFood.Amount);
						if (data[i].foodUnits[j].Name == data[i].loggedFood.Unit) {
							$("#loggedFoodEditDialogUnitSelect").append("<option value=\'" +
									data[i].foodUnits[j].Name + "\'  selected='selected'>" +
									data[i].foodUnits[j].Name + "</option>");
						} else {
							$("#loggedFoodEditDialogUnitSelect").append("<option value=\'" +
									data[i].foodUnits[j].Name + "\'>" +
									data[i].foodUnits[j].Name + "</option>");
						}
					}
				}
				$("#loggedFoodEditDialog").dialog("open");
			},
			error: function () {
				alert("failed");
			}
		});
	});


	//open edit dialog and retrieve logged food

	//$("#loggedFoodTable").on("click", "td.editLoggedFoodTd", function () {
	//	var loggedFoodFoodId = $(this).siblings(':first-child').html();
	//	var loggedFoodDateCreated = $(this).siblings(':first-child').next().html();
	//	var loggedFoodName = $(this).siblings(':first-child').next().next().html();
	//	var rowNumber = $(this).parent().parent().children().index($(this).parent());
	//	$.ajax({
	//		type: "POST",
	//		url: "/Dashboard/GetLoggedFood",
	//		data: {
	//			loggedFoodFoodId: loggedFoodFoodId,
	//			dateCreated: loggedFoodDateCreated
	//		},
	//		success: function (data) {
	//			$("#loggedFoodEditDialogUnitSelect").empty();
	//			for (var i in data) {
	//				$("#hiddenFieldloggedFoodEditDialog").val(data[i].loggedFood.FoodId);
	//				$("#hiddenLoggedFoodIdEditDialog").val(data[i].loggedFood.Id);
	//				$("#hiddenTableRowNumber").val(rowNumber);
	//				$("#loggedFoodEditDialog").dialog("option", "title", data[i].loggedFood.Name);
	//				$("#loggedFoodEditDialogCalories").text("Calories: " + data[i].loggedFood.Calories);
	//				for (var j in data[i].foodUnits) {
	//					$("#loggedFoodEditDialogAmountInput").val(data[i].loggedFood.Amount);
	//					if (data[i].foodUnits[j].Name == data[i].loggedFood.Unit) {
	//						$("#loggedFoodEditDialogUnitSelect").append("<option value=\'" +
	//								data[i].foodUnits[j].Name + "\'  selected='selected'>" +
	//								data[i].foodUnits[j].Name + "</option>");
	//					} else {
	//						$("#loggedFoodEditDialogUnitSelect").append("<option value=\'" +
	//								data[i].foodUnits[j].Name + "\'>" +
	//								data[i].foodUnits[j].Name + "</option>");
	//					}
	//				}
	//			}
	//			$("#loggedFoodEditDialog").dialog("open");
	//		},
	//		error: function () {
	//			alert("failed");
	//		}
	//	});
	//});



	//update logged food
	$("#loggedFoodEditDialogUpdateBtn").on("click", function() {
		var loggedFoodId = $("#hiddenLoggedFoodIdEditDialog").val();
		var amount = $("#loggedFoodEditDialogAmountInput").val();
		var unit = $("#loggedFoodEditDialogUnitSelect").val();

		$.ajax({
			type: 'POST',
			url: '/Dashboard/UpdateLoggedFood',
			data: {
				loggedFoodId: loggedFoodId,
				amount: amount,
				unit: unit
			},
			success: function(data) {
				$("#loggedFoodEditDialog").dialog("close");

				var rowNumber = $("#hiddenTableRowNumber").val();

				$("#loggedFoodTable tbody tr").eq(rowNumber).children().eq(4).text(amount + " " + unit);


			},
			error: function() {
				alert("food has not been updated");
			}
		});
	});

	//delete logged food
	$("#loggedFoodEditDialogDeleteBtn").on("click", function () {

		var loggedFoodId = $("#hiddenLoggedFoodIdEditDialog").val();

		$.ajax({
			type: "POST",
			url: "/Dashboard/DeleteLoggedFood",
			data:{loggedFoodId: loggedFoodId},
			success: function(data) {

				$("#loggedFoodEditDialog").dialog("close");

				var rowNumber = $("#hiddenTableRowNumber").val();

				$("#loggedFoodTable tbody tr").eq(rowNumber).remove();

			},
			error: function() {
				alert("error deleting row");
			}
		});

	});	


});