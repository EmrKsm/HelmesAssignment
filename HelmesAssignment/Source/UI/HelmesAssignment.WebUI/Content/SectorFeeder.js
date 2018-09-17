
/* Feeds sector table from sector list with creating parent and child relations from given index.html*/
var sectorsListToForward = [{}];
function feedSectorDB() {
    var sectorList = $('#sectors')[0];
    var sectorListLength = sectorList.length;

    sectorsListToForward[0] = {
        "SectorId": sectorList.options[0].value,
        "SectorName": sectorList.options[0].text,
        "SectorParent": 0
    };
    var parentId = sectorsListToForward[0].SectorId;
    var parentIndex = 0;
    var parentSpaceCount = sectorsListToForward[0].SectorName.search(/\S/);

    for (i = 1; i < sectorListLength; i++) {
        if (sectorList.options[i - 1] === null || sectorList.options[i].text.search(/\S/) === 0) {
            parentId = sectorList.options[i].value;
            parentIndex = i;
            sectorsListToForward[i] = {
                "SectorId": sectorList.options[i].value,
                "SectorName": sectorList.options[i].text,
                "SectorParent": 0
            };
            continue;
        }
        else {
            if (sectorList.options[i - 1].text.search(/\S/) < sectorList.options[i].text.search(/\S/)) {
                parentId = sectorList.options[i - 1].value;
                parentIndex = i - 1;
                sectorsListToForward[i] = {
                    "SectorId": sectorList.options[i].value,
                    "SectorName": sectorList.options[i].text,
                    "SectorParent": parentId
                };
                continue;
            }
            else if (sectorList.options[i - 1].text.search(/\S/) > sectorList.options[i].text.search(/\S/)) {
                getParentIndex(parentIndex, i);
                parentId = sectorList.options[parentIndex].value;
                sectorsListToForward[i] = {
                    "SectorId": sectorList.options[i].value,
                    "SectorName": sectorList.options[i].text,
                    "SectorParent": parentId
                };
            }
            else {
                sectorsListToForward[i] = {
                    "SectorId": sectorList.options[i].value,
                    "SectorName": sectorList.options[i].text,
                    "SectorParent": parentId
                };
            }
        }
    }

    function getParentIndex(previousParentIndex, i) {
        if (sectorList.options[previousParentIndex].text.search(/\S/) < sectorList.options[i].text.search(/\S/)) {
            parentIndex = previousParentIndex;
        }
        else {
            getParentIndex(previousParentIndex - 1, i);
        }
    };

    callService();

    function callService() {
        $.ajax({
            type: 'POST',
            url: "http://localhost:9999/api/Sectors/InsertSectors",
            data: { "SectorList": sectorsListToForward },
            dataType: 'json',
            beforeSend: function () {
            },
            success: function (data, textStatus, xhr) {
                var item = data;
            },
            error: function (xhr, status, error) {
            },
            complete: function () {
            }
        });
    };
};

