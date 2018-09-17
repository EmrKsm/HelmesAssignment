/* Load sectors from database , if there is cookie set before , input values is changing due to cookie */
function loadSectorList() {
    var sectorBox = $('#sectors');
    var sectorJson = [];
    var optionDepth = 0;
    $('#error').hide();
    callService();

    function callService() {
        $.ajax({
            type: 'GET',
            url: "http://localhost:9999/api/Sectors/GetSectorList",
            dataType: "json",
            xhrFields: {
                withCredentials: false
            },
            beforeSend: function () {
            },
            success: function (data, textStatus, xhr) {
                sectorJson = data;
                buildListFromJsonArray(sectorJson, optionDepth);

                var cookie = document.cookie.split('-');
                $('#userName').val(getCookie("username"));
                $('#termsCheck')[0].checked = getCookie("terms");
                var sectors = getCookie("sectors");
                if (sectors !== null) {
                    $.each(sectors.split("-"), function (index, item) {
                        $.each($('#sectors')[0].options, function (index2, item2) {
                            if (item2.value === item) {
                                item2.selected = true;
                            }
                        });
                    });
                }

            },
            error: function (xhr, status, error) {
                errorWindow(xhr, status, error);
            },
            complete: function () {
            }
        });
    };

    function buildListFromJsonArray(data, depth) {
        if (typeof (data) !== 'object') return;

        $.each(data, function (index, item) {
            var option = document.createElement("option");
            option.innerHTML = "&nbsp;".repeat(depth) + item.SectorName.trim();
            option.value = item.SectorId;
            sectorBox.append(option);

            if (item.ChildSectors && item.ChildSectors.length > 0) {
                buildListFromJsonArray(item.ChildSectors, depth + 4);
            }
        });
    };
};

function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) === 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}