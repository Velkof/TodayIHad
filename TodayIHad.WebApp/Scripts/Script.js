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


	//Get clicked food from search list in the logfood dialog
	$("#foodSearchUL").on("click", "li", function () {
					var selectedFoodName = $(this).html();

					$.ajax({
						type: "POST",
						url: "/Dashboard/GetSelectedFood",
						data: { foodName: selectedFoodName },
						success: function (data) {
							$("#foodSearchUL").empty();
							$("#foodLogDialog").dialog("open");

							$("#foodLogDialogUnitSelect").empty();


							for (var i in data) {
								$("#foodLogDialog").dialog("option", "title", data[i].food.Name);
								$("#foodLogDialogCalories").text(
									data[i].food.CaloriesKcal);
								$("#hiddenFieldfoodLogDialog").val(data[i].food.Id);

								for (var j in data[i].foodUnits) {
									$("#foodLogDialogAmountInput").val(100);

									if (data[i].foodUnits[j].Name == "gr") {
										$("#foodLogDialogUnitSelect")
											.append("<option value=\'" +
												data[i].foodUnits[j].Name +
												"\'  selected='selected'>" +
												data[i].foodUnits[j].Name +
												"</option>");
									} else {
										$("#foodLogDialogUnitSelect")
											.append("<option value=\'" +
												data[i].foodUnits[j].Name + "\'>" +
												data[i].foodUnits[j].Name + "</option>");
									}
								}
							}
						},
						error: function () {
							alert("fail");
						}
					});
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

				$("#loggedFoodTable").prepend("<tr><td style='display:none;'>" + foodId + "</td>" +
											   "<td style='display:none;'>"+ dateCreated +"</td> <td>" + name +
											  "</td><td>" + calories + "</td><td>" + amount + " " +
											  unit + "</td><td class='editLoggedFoodTd'>" +
											  "<span class='glyphicon glyphicon-pencil'></td></tr>");

			},
			error: function() {
				alert("error1");
			}
		});

	});

	//open edit dialog and retrieve logged food
	$("#loggedFoodTable").on("click", "td.editLoggedFoodTd", function () {
		var loggedFoodFoodId = $(this).siblings(':first-child').html();
		var loggedFoodDateCreated = $(this).siblings(':first-child').next().html();
		var loggedFoodName = $(this).siblings(':first-child').next().next().html();
		var rowNumber = $(this).parent().parent().children().index($(this).parent());

		$.ajax({
			type: "POST",
			url: "/Dashboard/GetLoggedFood",
			data: {
				loggedFoodFoodId: loggedFoodFoodId,
				dateCreated: loggedFoodDateCreated
			},
			success: function (data) {
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