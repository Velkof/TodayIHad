$(document).ready(function () {



    $("#tabs").tabs();
    $("#tabs").css("display", "block");


    $('#datepicker').datepicker({
        onSelect: function (dateText, inst) {

            var dateText = moment(dateText).format("YYYY-MM-DD HH:mm:ss");

            getLoggedFoodsForDate(dateText);
       
    
        }
    });


    function getLoggedFoodsForDate(date) {
        $.ajax({
            type: "POST",
            url: "/Dashboard/GetLoggedFoodsForDate",
            data: { dateText: date },
            success: function (data) {

                $(".loggedFoodCompact").remove();
                $("#editLoggedFood").remove();
                $("#logFood").hide();


                for (var i in data) {
                    for (var j in data[i]) {

                        var dateCreated = moment(data[i][j].DateCreated).format("YYYY-MM-DD HH:mm:ss");

                        $("#loggedFoodsContainer").prepend("<div class='loggedFoodCompact'>" +
                                                           "<span class='hiddenFoodIdCompact' style='display:none;'>" + data[i][j].FoodId + "</span>" +
                                                           "<span class='hiddenLoggedFoodIdCompact' style='display:none;'>" + data[i][j].Id + "</span>" +
                                                           "<span class='hiddenDateCreatedCompact' style='display:none;'>" + dateCreated + "</span>" +
                                                           "<span id='hiddenRowNumberCompact' style='display:none;'></span>" +
                                                           "<div class='loggedFoodNameCompact'>" + data[i][j].Name + "</div>" +
                                                           "<div class='loggedFoodInfoCompact'>" +
                                                           "Calories: <span class='loggedFoodCaloriesCompact'>" + data[i][j].Calories + "</span>" +
                                                           " | Quantity: <span class='loggedFoodQuantityCompact'>" + data[i][j].Amount + "</span>" +
                                                           " | Unit: <span class='loggedFoodUnitCompact'>" + data[i][j].Unit + "</span></div></div>");

                    }
                }
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

    //Get clicked food from search list in the logfood div
    $("#foodSearchUL").on("click", "li", function () {
        var selectedFoodName = $(this).html();

        $.ajax({
            type: "POST",
            url: "/Dashboard/GetSelectedFood",
            data: { foodName: selectedFoodName },
            success: function (data) {

                $("#datepicker").datepicker("setDate", "08/24/2016");
                var dateNow = moment(new Date()).format("YYYY-MM-DD HH:mm:ss");


                alert(dateNow);
                //getLoggedFoodsForDate(dateNow);
                $("#btnContainerLogFood").empty();
                $("#foodSearchUL").empty();
                $("#unitsLogFood").empty();

                //$("#quantityLogFood").empty();                
                //$("#editLoggedFood #unitsLogFood").empty();
                //$("#editLoggedFood #quantityLogFood").empty();

                $("#logFood").show();

                $("#btnContainerLogFood").append("<button class='btn btn-success saveBtn'>Save</button>");
                $("#btnContainerLogFood").append("<button class='btn btn-default cancelBtn'>Cancel</button>");

                for (var i in data) {
                    $("#hiddenIdLogFood").text(data[i].food.Id);
                    $("#nameLogFood").text(data[i].food.Name);

                    $("#fatAmountLogFood").text(data[i].food.FatGr);
                    $("#fatAmountLogFood").data("fatAmountLogFood", data[i].food.FatGr);
                    $("#satFatAmountLogFood").text(data[i].food.FatSatGr);
                    $("#satFatAmountLogFood").data("satFatAmountLogFood", data[i].food.FatSatGr);
                    $("#monoFatAmountLogFood").text(data[i].food.FatMonoGr);
                    $("#monoFatAmountLogFood").data("monoFatAmountLogFood", data[i].food.FatMonoGr);
                    $("#polyFatAmountLogFood").text(data[i].food.FatPolyGr);
                    $("#polyFatAmountLogFood").data("polyFatAmountLogFood", data[i].food.FatPolyGr);
                    $("#carbsAmountLogFood").text(data[i].food.CarbsGr);
                    $("#carbsAmountLogFood").data("carbsAmountLogFood", data[i].food.CarbsGr);
                    $("#fiberAmountLogFood").text(data[i].food.FiberGr);
                    $("#fiberAmountLogFood").data("fiberAmountLogFood", data[i].food.FiberGr);
                    $("#sugarAmountLogFood").text(data[i].food.SugarGr);
                    $("#sugarAmountLogFood").data("sugarAmountLogFood", data[i].food.SugarGr);
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




    //when units or quantity changes, update all nutrient and calorie values
    
    $("#loggedFoodsContainer").on("change", "#unitsLogFood, #quantityLogFood", function () {

        var divId = "#" + $(this).parent().parent().attr("id");

        var caloriesAmount = $(divId + " #calsAmountLogFood").data("calsAmountLogFood");
        var fatAmount = $(divId + " #fatAmountLogFood").data("fatAmountLogFood");
        var satFatAmount = $(divId + " #satFatAmountLogFood").data("satFatAmountLogFood");
        var monoFatAmount = $(divId + " #monoFatAmountLogFood").data("monoFatAmountLogFood");
        var polyFatAmount = $(divId + " #polyFatAmountLogFood").data("polyFatAmountLogFood");
        var carbsAmount = $(divId + " #carbsAmountLogFood").data("carbsAmountLogFood");
        var fiberAmount = $(divId + " #fiberAmountLogFood").data("fiberAmountLogFood");
        var sugarAmount = $(divId + " #sugarAmountLogFood").data("sugarAmountLogFood");
        var sodiumAmount = $(divId + " #sodiumAmountLogFood").data("sodiumAmountLogFood");
        var cholesAmount = $(divId + " #cholesAmountLogFood").data("cholesAmountLogFood");
        var protAmount = $(divId + " #protAmountLogFood").data("protAmountLogFood");

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
        }
        
        var dateNow = moment(new Date()).format("YYYY-MM-DD HH:mm:ss");

        if (thisLogFoodDivId == "#logFood") {
            var dateCreated = dateNow;
            var url = "/Dashboard/LogFood";
            LoggedFood["DateCreated"] = dateNow;
            LoggedFood["DateUpdated"] = dateNow;
        } else {
            var dateCreated = $("#editLoggedFood #hiddenDateCreatedLoggedFood").text();
            var loggedFoodId = $("#editLoggedFood .hiddenLoggedFoodIdFull").text();                               
            var url = "/Dashboard/UpdateLoggedFood";
            LoggedFood["DateCreated"] = dateCreated;
            LoggedFood["DateUpdated"] = dateNow;
            LoggedFood["Id"] = loggedFoodId;
        }
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json",
            traditional: true,
            data: JSON.stringify(LoggedFood),
            success: function (data) {
                //$(".loggedFoodCompact").show();
                $(thisLogFoodDiv).hide();
                $(thisLogFoodDiv).prev(".newLoggedFoodCompact").remove();
                //$(".newLoggedFoodCompact").remove();
               
                $("#btnContainerLogFood").empty();

                var newLoggedFoodCompact = "<div class='loggedFoodCompact newLoggedFoodCompact'>" +
												   "<span class='hiddenFoodIdCompact' style='display:none;'>" + foodId + "</span>" +
												   "<span class='hiddenLoggedFoodIdCompact' style='display:none;'>" + loggedFoodId + "</span>" +
												   "<span class='hiddenDateCreatedCompact' style='display:none;'>" + dateCreated + "</span>" +
                                                   "<span id='hiddenRowNumberCompact' style='display:none;'></span>" +
												   "<div class='loggedFoodNameCompact newLoggedFoodNameCompact'>" + name + "</div>" +
												   "<div class='loggedFoodInfoCompact'>" +
												   "Calories: <span class='loggedFoodCaloriesCompact'>" + calories + "</span>" +
												   " | Quantity: <span class='loggedFoodQuantityCompact'>" + amount + "</span>" +
												   " | Unit: <span class='loggedFoodUnitCompact'>" + unit + "</span></div></div>";

                $(thisLogFoodDiv).after(newLoggedFoodCompact);


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
        $(thisLogFoodDiv).prev().children(".loggedFoodNameCompact").addClass("newLoggedFoodNameCompact");
        $(thisLogFoodDiv).prev().addClass("newLoggedFoodCompact").show();            
    });

    //open editLoggedFood and retrieve logged food
    $("#loggedFoodsContainer").on("click", ".loggedFoodCompact", function () {

        var loggedFoodFoodId = $(this).find(".hiddenFoodIdCompact").html();
        var loggedFoodDateCreated = $(this).find(".hiddenDateCreatedCompact").html();
        var loggedFoodName = $(this).find(".loggedFoodNameCompact").html();
        var rowNumber = $(this).index();
        var clickedLoggedFoodCompact = this;


        $.ajax({
            type: "POST",
            url: "/Dashboard/GetLoggedFood",
            data: {
                loggedFoodFoodId: loggedFoodFoodId,
                dateCreated: loggedFoodDateCreated
            },
            success: function (data) {
                $(clickedLoggedFoodCompact).hide();
                $("#editLoggedFood").remove();
                var logFoodDiv = $("#logFood").html();

                $(clickedLoggedFoodCompact).after("<div id='editLoggedFood'>" + logFoodDiv + "</div>");

                $("hiddenRowNumberCompact").text(rowNumber);
                $("#editLoggedFood #unitsLogFood").empty();
                $("#editLoggedFood #btnContainerLogFood").empty();

                $("#editLoggedFood #btnContainerLogFood").append("<span id='hiddenDateCreatedLoggedFood' style='display:none'>" + loggedFoodDateCreated + "</span>");
                $("#editLoggedFood #btnContainerLogFood").append("<button class='btn btn-success updateBtn'>Update</button>");
                $("#editLoggedFood #btnContainerLogFood").append("<button class='btn btn-default cancelBtn'>Cancel</button>");
                $("#editLoggedFood #btnContainerLogFood").append("<button class='btn btn-danger deleteBtn'>Delete</button>");
                $("#editLoggedFood #btnContainerLogFood").append("<span class='hiddenLoggedFoodIdFull' style='display:none'></span>");

                for (var i in data) {

                    $("#editLoggedFood #hiddenIdLogFood").text(data[i].loggedFood.FoodId);
                    $("#editLoggedFood .hiddenLoggedFoodIdFull").text(data[i].loggedFood.Id);
                    
                    $("#editLoggedFood #nameLogFood").text(data[i].loggedFood.Name);


                    $("#editLoggedFood #fatAmountLogFood").text(data[i].loggedFood.FatGr);
                    $("#editLoggedFood #fatAmountLogFood").data("fatAmountLogFood", data[i].food.FatGr);
                    $("#editLoggedFood #satFatAmountLogFood").text(data[i].loggedFood.FatSatGr);
                    $("#editLoggedFood #satFatAmountLogFood").data("satFatAmountLogFood", data[i].food.FatSatGr);
                    $("#editLoggedFood #monoFatAmountLogFood").text(data[i].loggedFood.FatMonoGr);
                    $("#editLoggedFood #monoFatAmountLogFood").data("monoFatAmountLogFood", data[i].food.FatMonoGr);
                    $("#editLoggedFood #polyFatAmountLogFood").text(data[i].loggedFood.FatPolyGr);
                    $("#editLoggedFood #polyFatAmountLogFood").data("polyFatAmountLogFood", data[i].food.FatPolyGr);
                    $("#editLoggedFood #carbsAmountLogFood").text(data[i].loggedFood.CarbsGr);
                    $("#editLoggedFood #carbsAmountLogFood").data("carbsAmountLogFood", data[i].food.CarbsGr);
                    $("#editLoggedFood #fiberAmountLogFood").text(data[i].loggedFood.FiberGr);
                    $("#editLoggedFood #fiberAmountLogFood").data("fiberAmountLogFood", data[i].food.FiberGr);
                    $("#editLoggedFood #sugarAmountLogFood").text(data[i].loggedFood.SugarGr);
                    $("#editLoggedFood #sugarAmountLogFood").data("sugarAmountLogFood", data[i].food.SugarGr);
                    $("#editLoggedFood #sodiumAmountLogFood").text(data[i].loggedFood.SodiumMg);
                    $("#editLoggedFood #sodiumAmountLogFood").data("sodiumAmountLogFood", data[i].food.SodiumMg);
                    $("#editLoggedFood #cholesAmountLogFood").text(data[i].loggedFood.CholesterolMg);
                    $("#editLoggedFood #cholesAmountLogFood").data("cholesAmountLogFood", data[i].food.CholesterolMg);
                    $("#editLoggedFood #protAmountLogFood").text(data[i].loggedFood.ProteinGr);
                    $("#editLoggedFood #protAmountLogFood").data("protAmountLogFood", data[i].food.ProteinGr);

                    $("#editLoggedFood #quantityLogFood").val(data[i].loggedFood.Amount);
                    $("#editLoggedFood #calsAmountLogFood").text(data[i].loggedFood.Calories);
                    $("#editLoggedFood #calsAmountLogFood").data("calsAmountLogFood", data[i].food.CaloriesKcal);


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

            },
            error: function () {
                alert("error deleting row");
            }
        });

    });


});