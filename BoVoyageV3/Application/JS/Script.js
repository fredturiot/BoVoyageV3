function setLoader(element) {
    element.append($('<div class="loader"></div>'));
}

function GetURLParameter(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return sParameterName[1];
        }
    }
}

function Load(controller, id = 0) {
    if (id === 0) {
        return Promise.resolve($.ajax({
            url: '/api/' + controller,
        }));
    } else {
        return Promise.resolve($.ajax({
            url: '/api/' + controller + '/' + id.toString(),
        }));
    }
}

function Erreur(data) {
    var modelState = data.responseJSON.ModelState;
    var message = modelState[Object.keys(modelState)[0]][0];
    alert(message);
}