/* After save button clicked entered user input send to API and after API returns response , cookie become available with entered data. */
function UserInputAction() {
    $('#error').hide();
    var sectorList = $('#sectors').val();
    var userName = $('#userName').val();
    var termsCheck = $('#termsCheck');
    var sectors = "";

    if (validateForm()) {
        $.each(sectorList, function (index, item) {
            if (index === sectorList.length - 1)
                sectors += item;
            else
                sectors += item + "-";
        });
        var input = {
            "UserName": userName,
            "SectorList": sectors,
            "TermsAgreed": termsCheck[0].checked
        };
        $.ajax({
            type: 'POST',
            url: "http://localhost:9999/api/UserInput/AddUserInput",
            data: input,
            dataType: "json",
            beforeSend: function () {
            },
            success: function (data, textStatus, xhr) {
                setCookie("username", userName, 1);
                setCookie("sectors", sectors, 1);
                setCookie("terms", termsCheck[0].checked, 1);
                if (data === true)
                    alert("User input saved.");
            },
            error: function (xhr, status, error) {
                errorWindow(xhr, status, error);
            },
            complete: function () {
            }
        });
    }

    function validateForm() {
        var name = userName;
        if (name === "") {
            alert("Name must be filled out");
            return false;
        }
        else if (sectorList.length === 0) {
            alert("Sector must be selected");
            return false;
        }
        else if (termsCheck[0].checked === false) {
            alert("Terms must be checked");
            return false;
        }
        else {
            return true;
        }
    }
}
function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}

