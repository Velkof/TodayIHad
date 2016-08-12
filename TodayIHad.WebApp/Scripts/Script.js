$(document).ready(function () {


     $("#foodLogDialog").dialog({
        //closeText: "hide",
        my: "center",
        at: "center",
        of: window,
        autoOpen: false,
    });


    $("#tabs").tabs();
    $("#tabs").css("display", "block");



    $("#foodSearchBox").on("input",function () {

                var food = $('#foodSearchBox').val();


                $.ajax({
                    type: 'POST',
                    url: '/Dashboard/SearchFood',
                    data: { searchFoodString: food },
                    success: function (data) {

                        $("#foodSearchUL").empty();

                        for (var i in data) {
                            for (var j in data[i]) {
                                $("#foodSearchUL")
                                    .append('<li class="foodSearchResultsLI list-group-item">' +
                                        data[i][j].Name + '</li>');
                            }
                        }

                    },
                    error: function () {
                        alert("Failed");
                    }

                });
    });


        $("#foodSearchUL")
               .on("click",
                "li",
                function () {
                    var selectedFoodName = $(this).html();

                    $.ajax({
                        type: 'POST',
                        url: '/Dashboard/GetSelectedFood',
                        data: { foodName: selectedFoodName },
                        success: function (data) {
                            $("#foodSearchUL").empty();
                            $("#foodLogDialog").dialog("open");

                            for (var i in data) {
                                $("#foodLogDialog").dialog('option', 'title', data[i].food.Name);

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

    $("#logFoodSaveBtn").on("click", function() {
        var amount = $("#foodLogDialogAmountInput").val();
        var unit = $("#foodLogDialogUnitSelect").val();
        var foodId = $("#hiddenFieldfoodLogDialog").val();
        var name = $("#foodLogDialog").dialog("option", "title");
        var calories = $("#foodLogDialogCalories").html(); 


        $("#foodLogDialog").prepend("<p>"+ amount + " " + unit + foodId + name+calories+"</p>")

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
                calories: calories
            },
            success: function(data) {
                alert("success");
            },
            error: function() {
                alert("error1");
            }
        });

    });
});

