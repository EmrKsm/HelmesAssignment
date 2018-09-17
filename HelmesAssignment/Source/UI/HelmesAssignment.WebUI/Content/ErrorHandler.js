function createErrorWindow(header, message) {
    $('#error').append("<h4>" + header + "</h4><span> " + message + "</span>");
    $('#error').show();
};

function errorWindow(xhr, status, error) {

    var message;
    if (status === "timeout") {
        message = "Timeout";
    }
    else {
        if (xhr.responseJSON !== null) {
            if ($.isEmptyObject(xhr.responseJSON.Message) === false) message = xhr.responseJSON.Message;
        }
        else {
            message = "Unknown error.";
        }
    }
    createErrorWindow("Error", message);
};