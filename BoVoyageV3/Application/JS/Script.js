function setLoader(element) {
    element.append($('<div class="loader"></div>'));
}

function GetURLParameter(sParam)
{
     var sPageURL = window.location.search.substring(1);
	var sURLVariables = sPageURL.split('&');
	for (var i = 0; i < sURLVariables.length; i++)
	{
	    var sParameterName = sURLVariables[i].split('=');
	    if (sParameterName[0] == sParam)
	    {
	        return sParameterName[1];
	    }
	 }
    }

/* function LoadAjaxData() {
    var searchString = $('#searchString').val();
    $.ajax(
        {
            method: "GET",
            url: "" + searchString + 
        })
        .done(function (data) {
            var ul = $('<ul>');
            data[''].forEach(function (item) {
                ul.append($('<li>' + item.name + '</li>'));
            });
            $('#result').append(ul);
        }
        )
}
 */






