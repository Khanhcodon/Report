(function () {

    $("#optionSelect").on("change", function () {
        var valueStr = this.value;
        if (valueStr == 1) {
            //nam 
            document.getElementById("numberYearKey").style.display = "block";
            document.getElementById("numberQuater").style.display = "none";
            document.getElementById("numberMonth").style.display = "none";
        } else if (valueStr == 2) {
            // quy
            document.getElementById("numberYearKey").style.display = "block";
            document.getElementById("numberQuater").style.display = "block";
            document.getElementById("numberMonth").style.display = "none";
        } else if(valueStr == 3){
            // thang
            document.getElementById("numberYearKey").style.display = "block";         
            document.getElementById("numberMonth").style.display = "block";
            document.getElementById("numberQuater").style.display = "none";
        }
    });

    $('#periodModel').on('change', function () {
        var valueStr = this.value;
        if (valueStr == "KTC") {
            var numberYear = parseInt($('#DocType_ActionLevel').val());
            if (numberYear == 1) {
                document.getElementById("optionSelect").style.display = "block";
                document.getElementById("numberYearKey").style.display = "block";
            } else {
                document.getElementById("numberYearKey").style.display = "none";
                document.getElementById("numberQuater").style.display = "none";
                document.getElementById("numberMonth").style.display = "none";
                document.getElementById("optionSelect").style.display = "none";
            }
        } else {
            document.getElementById("numberYearKey").style.display = "none";
            document.getElementById("numberQuater").style.display = "none";
            document.getElementById("numberMonth").style.display = "none";
            document.getElementById("optionSelect").style.display = "none";
        }
    });
})();