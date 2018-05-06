var loadingElement = '<!-- Loading (remove the following to stop the loading)-->'+
    '<div class="overlay" id="loading-element">'+
    '<i class="fa fa-refresh fa-spin"></i>'+
    '</div>'+
    '<!-- end loading -->';
function onBegineForm(formContainer) {
    $("[id='"+formContainer+"']").append(loadingElement);
}
function onCompleteForm(formContainer) {

    //$("#" + formContainer).activateBox();
    //$.AdminLTE.boxWidget.activate();
    $("#" + formContainer).find("[type='password']").val("");
    $("[id='" + formContainer + "']").find("[id='loading-element']").remove();
}
function userNameChanged() {

    $.ajax(
        {
            url: "/Accounts/GetUsername",
            method: "POST",
            success: function (data, textStatus, jqXHR) {
                $("[name ='user-full-name-display']").html(data)
            }
            /*,
            error: function (jqXHR, textStatus, errorThrown) {
                alert(textStatus+"\n"+errorThrown);
            },
            complete: function (jqXHR, textStatus) {
                alert(textStatus);
            },
            statusCode: {
                404: function (content) { alert('cannot find resource'); },
                500: function (content) { alert('internal server error'); }
            }
            */
        }
        );
}
