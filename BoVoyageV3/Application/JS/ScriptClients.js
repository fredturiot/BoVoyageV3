//scripts affichage

function getTaches() {
    var divListeTaches = $('#listeTaches');
    setLoader(divListeTaches);
    $.ajax({
        url: '/api/taches',
        success: function (taches) {
            divListeTaches.empty();
            if (taches.length > 0) {
                for (tache of taches) {
                    var divTache = $('<div class="Tache alert"></div>');
                    divTache.attr("data-id", tache.ID);
                    if (tache.Statut) {
                        divTache.addClass('alert-success');
                    }
                    var inputEtat = $('<input type="checkbox">');
                    inputEtat.attr('checked', tache.Statut);
                    inputEtat.on('click', function () {
                        var div = $(this).closest(".Tache");
                        var idTache = div.attr("data-id");
                        ModifierStatutTache(idTache, this);
                    });
                    var buttonDelete = $('<button class="btn btn-danger btn-sm"><i class="fas fa-trash"></i></button>');
                    buttonDelete.on('click', function () {
                        // Affichage d'une confirmation
                        $.bsAlert.confirmTitle = 'Suppression';
                        $.bsAlert.closeDisplay = 'Non';
                        $.bsAlert.sureDisplay = 'Oui';
                        $.bsAlert.confirm("Es-tu sûr?", () => {
                            var div = $(this).closest(".Tache");
                            var idTache = div.attr("data-id");
                            SupprimerTache(idTache, div);
                        });
                    });
                    divTache.append(inputEtat);
                    divTache.append($('<label></label>').text(tache.Nom));
                    divTache.append(buttonDelete);
                    divListeTaches.append(divTache);
                }
            } else {
                divListeTaches.append($('<p class="lead">Aucune tache pour le moment...</p>'));
            }
        },
        error: Erreur
    });
}
function ModifierStatutTache(id, input) {
    $.ajax({
        type: 'PUT',
        url: '/api/tachestatut/' + id + '?statut=' + input.checked,
        success: function () {
            var divTache = $(input).closest(".Tache");
            if (input.checked) {
                divTache.addClass('alert-success');
            } else {
                divTache.removeClass('alert-success');
            }
        },
        error: Erreur
    });
}
function SupprimerTache(id, div) {
    $.ajax({
        type: 'DELETE',
        url: '/api/taches/' + id,
        success: function () {
            getTaches();
        },
        error: Erreur
    });
}
function Erreur() {
    alert("Erreur !");
}
$(document).ready(getTaches);

// script modif
function Valider() {
    var form = $('form');
    let estValide = form[0].checkValidity();
    form.addClass("was-validated");
    return estValide;
}
function Enregistrer() {
    if (!Valider()) {
        return false;
    }
    let tache = {
        Nom: $('#Nom').val(),
        Description: $('#Description').val(),
        CategorieID: $('#Categorie').val(),
        DateFin: $('#DateFin').val(),
        Priorite: $('[name=Priorite]:checked').val()
    };
    $.ajax({
        type: 'POST',
        url: '/api/taches',
        data: tache,
        success: function () {
            $('form')[0].reset();
            $('form').removeClass('was-validated');
            //window.location.href = '/index.html';
        },
        error: function () {
            alert('Aïe aïe aïe carambla !!');
        }
    });
    return false;
}
function LoadCategories() {
    $.ajax({
        url: '/api/categories',
        success: function (categories) {
            var select = $("#Categorie");
            select.empty();
            select.append($('<option></option>')); // élément vide
            for (categorie of categories) {
                var option = $('<option></option>');
                option.val(categorie.ID);
                option.text(categorie.Nom);
                select.append(option);
            }
        }
    });
}
$(document).ready(function () {
    LoadCategories();
    $('#Nom').focus(); // Sans jquery: document.getElementById('Nom').focus();
});