
if (typeof jQuery == 'undefined') {
    alert("GregHijCalendar Control Requires to reference jQuery Script Library in the Page or set RegisterJQuery Property as true");
}

$(document).ready(function () {

    $(document).ready(function () {
        $(".calTbl").each(function (i) {
            configCal($(this), $(this).find("option:selected").val());
        });

    })

    function configCal(element, t) {
        $(element).find(".dayBox").unbind();
        $(element).find(".dayBox").keyup(function (e) {

            if (!validate(e, this, 0, 31)) return false;

            if (!handleCustomKeys(this, e.keyCode)) return false;

            if ($(this).val().length > 1) {
                if ($(element).find(".calBox").css("display") != "none") {
                    if (t == "H") {
                        if ($(this).val() == 31) {
                            $(element).find(".calBox").val("M");
                            configCal(element, "M");
                        }
                        if ($(this).val() > 31) {
                            $(this).val(31);
                        }
                    }
                }

                $(element).find(".monthBox").focus();
            }
        });

        $(element).find(".monthBox").keyup(function (e) {

            if (!validate(e, this, 0, 12)) return false;

            if ($(this).val().length > 1) {

                $(element).find(".yearBox").focus();
            }
        });

        $(element).find(".yearBox").keyup(function (e) {

            if (!validate(e, this, 1, 3000)) return false;

            if ($(this).val().length >= 4) {
                if ($(element).find(".calBox").css("display") != "none") {

                    if (t == "H") {
                        if ($(this).val() >= 1700) {
                            $(element).find(".calBox").val("M");
                            configCal(element, "M");
                        }
                    } else {
                        if ($(this).val() <= 1700) {
                            $(element).find(".calBox").val("H");
                            configCal(element, "H");
                        }
                    }
                }
                $(element).find(".calBox").focus();

            }
        });

        $(element).find(".dayBox").focusout(function (e) {
            validateDate(element);
        });
        $(element).find(".monthBox").focusout(function (e) {
            validateDate(element);
        });
        $(element).find(".yearBox").focusout(function (e) {
            validateDate(element);
        });

        $(element).find(".calBox").change(function (e) {
            getDate(element);
            return false;
        });

        $(element).find(".todayBtn").click(function () {
            var day = -1;
            var month = -1;
            var year = -1;
            var calType = $.trim($(element).find(".calBox").val());
            var elementID = $(element).attr("id");
            var data = day + "/" + month + "/" + year + "/" + calType + "/" + elementID;
            $(element).find(".imgLoad").show();
            CallServer("T|" + data);
            return false;
        });




    }

    function handleCustomKeys(element, keyCode) {

        if (keyCode != 39 || keyCode != 40)
            return true;

        if (keyCode == 39)
            $(this).val($(this).val() + 1);
        else if (keyCode == 40)
            $(this).val($(this).val() - 1);

        return false;
    }


    function getDate(element) {
        var day = $.trim($(element).find(".dayBox").val());
        var month = $.trim($(element).find(".monthBox").val());
        var year = $.trim($(element).find(".yearBox").val());
        var calType = $.trim($(element).find(".calBox").val());
        var elementID = $(element).attr("id");

        $("#" + elementID).find(".imgError").hide();
        if (day == '')
            $(element).find(".daybox").focus();
        else if (month == '')
            $(element).find(".monthbox").focus();
        else if (year == '')
            $(element).find(".yearbox").focus();
        else {
            var date = day + "/" + month + "/" + year + "/" + calType + "/" + elementID;
            $(element).find(".imgLoad").show();
            CallServer("C|" + date);
        }

    }

    function validate(e, obj, min, max) {

        if (e.keyCode == 39 && e.keyCode == 40)
            return true; // Up/Down Arrow

        if (e.keyCode >= 0 && e.keyCode <= 47)
            return false; //ignore

        if (e.keyCode >= 58 && e.keyCode <= 95) {
            obj.value = '';
            return false; //ignore
        }

        if (e.keyCode > 105) {
            obj.value = '';
            return false; //ignore
        }

        var result = parseInt(obj.value);
        if (!(result >= min && result <= max)) {
            obj.value = '';
            return false;
        }
        return true;
    }

    function validateDate(element) {
        $(element).find(".imgError").hide();
        var day = $.trim($(element).find(".dayBox").val());
        var month = $.trim($(element).find(".monthBox").val());
        var year = $.trim($(element).find(".yearBox").val());
        var calType = $.trim($(element).find(".calBox").val());
        var elementID = $(element).attr("id");
        if (day == '')
            return false;
        else if (month == '')
            return false;
        else if (year == '')
            return false;
        else {
            var date = day + "/" + month + "/" + year + "/" + calType + "/" + elementID;
            $(element).find(".imgLoad").show();
            CallServer("V|" + date);
        }
    }
})

function ReceiveServerData(arg, context) {
    var op = arg.split('|'); // get operation

    if (op[0] == "C" || op[0] == "T") {

        var dateParts = op[1].split('/');
        var day = dateParts[0];
        var month = dateParts[1];
        var year = dateParts[2];
        var calType = dateParts[3];
        var elementID = dateParts[4];

        $("#" + elementID).find(".imgLoad").hide();

        $("#" + elementID).find(".dayBox").val(day);
        $("#" + elementID).find(".monthBox").val(month);
        $("#" + elementID).find(".yearBox").val(year);
    }
    else if (op[0] == "V") {
        var output = op[1].split('/');
        var elementID = output[1];
        $("#" + elementID).find(".imgLoad").hide();
        if (output[0] != "OK") {
            $("#" + elementID).find(".imgError").show();
        }
    }


}