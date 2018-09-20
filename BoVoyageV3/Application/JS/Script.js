function LoadAjaxData() {
    var searchString = $('#searchString').val();
    $.ajax(
        {
            method: "GET",
            url: "https://secure.geonames.org/searchJSON?q=" + searchString + "&maxRows=60&username=nblaudez"
        })
        .done(function (data) {
            var ul = $('<ul>');
            data['geonames'].forEach(function (item) {
                ul.append($('<li>' + item.name + '</li>'));
            });
            $('#result').append(ul);
        }
        )
}







