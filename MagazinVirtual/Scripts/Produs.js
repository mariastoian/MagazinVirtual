
$('.sidebar-nav li a').on('click', function () {
    $('li a.current').removeClass('current');
    $(this).addClass('current');
});
$(document).ready(function () {
    $('#showmenu').click(function () {
        $('.menu').slideToggle("fast");
    });
});

//date-time picker 1 Bootstrap

function loadAllDatetimePicker1() {
    $('#datetimepicker1').datetimepicker({
        format: 'DD/MM/YYYY',
        icons: {
            time: "fa fa-clock-o",
            date: "fa fa-calendar",
            up: "fa fa-arrow-up",
            down: "fa fa-arrow-down"
        }
    });
}
function loadAllDatetimePicker2() {
    $('#datetimepicker2').datetimepicker({
        format: 'DD/MM/YYYY',
        icons: {
            time: "fa fa-clock-o",
            date: "fa fa-calendar",
            up: "fa fa-arrow-up",
            down: "fa fa-arrow-down"
        }
    });
}


$(document).ready(function () {
    $('[data-toggle="popover"]').popover({
        html: true,
        trigger: "hover",
        placement: 'right'
    })
});
function loadAllPopOver() {
    $(document).ready(function () {
        $('[data-toggle="popover"]').popover({
            html: true,
            trigger: "hover",
            placement: 'right'
        });
    });
}

//select2  confirmation   tooltip  script

$(document).ready(function () {
    $(".select2").select2({
        placeholder: "Select"
    });
});
/*
$(document).ready(function () {

    $('[data-toggle="confirmation"]').confirmation({
        title: 'Sunteți sigur?',
        btnOkLabel: 'DA',
        btnCancelLabel: 'NU'
    });
});
*/
$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});


/*
function loadAllTooltip() {
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
    });
}*/
function loadSelectedItemList() {
    $('.sidebar-nav li a').on('click', function () {
        $('li a.current').removeClass('current');
        $(this).addClass('current');
    });
}

function loadAllConfirmation() {
    $(document).ready(function () {

        $('[data-toggle="confirmation"]').confirmation({
            title: 'Sunteți sigur?',
            btnOkLabel: 'DA',
            btnCancelLabel: 'NU'
        });
    });
}

function loadAllSelect2() {
    $(document).ready(function () {
        $(".select2").select2({
            placeholder: "Select",
            allowClear: true
        });
    });
}

$('.sidebar-nav li a').on('click', function () {
    $('li a.current').removeClass('current');
    $(this).addClass('current');
});
$(document).ready(function () {
    $('#showmenu').click(function () {
        $('.menu').slideToggle("fast");
    });
});

//date-time picker 1 Bootstrap
/*
function loadAllDatetimePicker1() {
    $('#datetimepicker1').datetimepicker({
        format: 'DD/MM/YYYY',
        icons: {
            time: "fa fa-clock-o",
            date: "fa fa-calendar",
            up: "fa fa-arrow-up",
            down: "fa fa-arrow-down"
        }
    });
}
function loadAllDatetimePicker2() {
    $('#datetimepicker2').datetimepicker({
        format: 'DD/MM/YYYY',
        icons: {
            time: "fa fa-clock-o",
            date: "fa fa-calendar",
            up: "fa fa-arrow-up",
            down: "fa fa-arrow-down"
        }
    });
}
*/

$(document).ready(function () {
    $('[data-toggle="popover"]').popover({
        html: true,
        trigger: "hover",
        placement: 'right'
    })
});
function loadAllPopOver() {
    $(document).ready(function () {
        $('[data-toggle="popover"]').popover({
            html: true,
            trigger: "hover",
            placement: 'right'
        });
    });
}

//select2  confirmation   tooltip  script

$(document).ready(function () {
    $(".select2").select2({
        placeholder: "Select"
    });
});

/*$(document).ready(function () {

    $('[data-toggle="confirmation"]').confirmation({
        title: 'Sunteți sigur?',
        btnOkLabel: 'DA',
        btnCancelLabel: 'NU'
    });
});*/


$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});



function loadAllTooltip() {
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
    });
}
function loadSelectedItemList() {
    $('.sidebar-nav li a').on('click', function () {
        $('li a.current').removeClass('current');
        $(this).addClass('current');
    });
}

function loadAllConfirmation() {
    $(document).ready(function () {

        $('[data-toggle="confirmation"]').confirmation({
            title: 'Sunteți sigur?',
            btnOkLabel: 'DA',
            btnCancelLabel: 'NU'
        });
    });
}


function loadAllSelect2() {
    $(document).ready(function () {
        $(".select2").select2({
            placeholder: "Selectati un element",
            allowClear: true
        });
    });
}

//Categorii pentru produs (creare get si post)
$(document).on('click', "#butonCreareCategorieProdus", function (e) {
    var url = $(this).data('url');

    $.get(url, function (data) {
        $('#ProdusContainer').html(data), loadAllSelect2(); loadAllTooltip(); loadAllPopOver();

        $('#ProdusModal').modal('show');
    });
});
$(document).on('click', "#SubmitCategorieProdus", function (e) {
    var partialurl = $(this).data('url');
    var url = partialurl + '?numeCategorie=' + $('#IdInputCategorie').val() + '&idParinteCategorie=' + $('#IdSelectParinteCategorie').val();

    if ($('#IdInputCategorie').val() === "") { $('#CampuriObligatorii').hide(); $('#CampuriObligatorii').show(); }
    else {

        $.post(url, function (data) {
            $.notify("Categoria a fost creata cu succes", "success");
            $('#ProdusContainer').html(data), loadAllSelect2(); loadAllTooltip(); loadAllPopOver();

            $('#ProdusModal').modal('show');
        });
    }
});

//Produs creare (get si post)

$(document).on('click', "#butonCreareProdus", function (e) {
    var url = $(this).data('url');

    $.get(url, function (data) {
        $('#ProdusContainer').html(data), loadAllSelect2(); loadAllTooltip(); loadAllPopOver();

        $('#ProdusModal').modal('show');
    });
});

$(document).on('click', "#SubmitProdus", function (e) {
    var bntSave = $(this);
    bntSave.attr("disabled", true);
    var partialurl = $(this).data('url');
    var url = partialurl + '?DenumireProdus=' + $('#IdInputDenumire').val() +
        '&IdCategorie=' + $('#SelectIdCategorie').val() +
        '&Price=' + $('#IdInputPrice').val() +
        '&Mentiuni=' + $('#IdInputMentiuni').val();


    if ($('#SelectIdCategorie').val() === "" ||
        $('#IdInputDenumire').val() === "" ||
        $('#IdInputMentiuni').val() === "" ||
        isNaN($('#IdInputPrice').val())) { $("#SubmitProdus").prop("disabled", false); $('#CampuriObligatorii').hide(); $('#CampuriObligatorii').show(); }
    else {

        $.ajax({
            url: url,
            type: 'POST',
            dataType: "Json",
            success: function (e) {
            },
            error: function (e) {
                if (e.responseText === "Salvat") {
                    $('.modal').modal('hide');
                    $.notify("Produsul a fost adăugat în baza de date Depositum", "success");
                    $('#tabelaProdus').load('Produse/ViewProduseDinCateg?page=1' + '&IdCategorie=' + $('#SelectIdCategorie').val() + '&denumire=', null, function () { loadAllSelect2(); loadAllTooltip(); loadAllPopOver() });;
                } else {
                    $.notify(e.responseText, "error");
                    bntSave.attr("disabled", false);
                }
            }

        });

    }
});

var timer;
//index cautare 

$(document).on("click", "#CautaProduse ", function () {
    $('#loader').modal('show');

    var partialUrl = $('#IdFIltruProdus').data('url');
    var idCateg = $('#idCategorieModel').val();
    var elemFiltrate = $('#IdFIltruProdus').val();
    var searchURL = partialUrl + '?denumire=' + elemFiltrate;
    $('#ulListaCategoriCuProduse').load(searchURL, null, function () { loadAllSelect2(), loadAllTooltip(), loadAllPopOver(), $('#loader').modal('hide'); });
    var url = $(".linkCategorieCuProduse").data('url') + '&denumire=' + $('#IdFIltruProdus').val();
    $('#tabelaProdus').load(url, null, function () { loadAllSelect2(); loadAllTooltip(); loadAllPopOver() });




});


//refresh mwniu stanga
$(document).on('click', "#IdRefreshLista", function (e) {
    $('#loader').modal('show');
    var partialUrl = $('#IdFIltruProdus').data('url');
    var elemFiltrate = $('#IdFIltruProdus').val();
    var url = (partialUrl + '?id=0&denumire=' + elemFiltrate).replace(" ", "%20");
    clearTimeout(timer);
    timer = setTimeout(function () {
        $('#ulListaCategoriCuProduse').load(url, null, function () { });
        $('#loader').modal('hide');
    }, 500);
});
//index afisare item selectat categorie  sau produs

//categorie
$(document).on('click', ".linkCategorieCuProduse", function (e) {
    var url = $(this).data('url') + '&denumire=' + $('#IdFIltruProdus').val();
    $('#tabelaProdus').load(url, null, function () { loadAllSelect2(); loadAllTooltip(); loadAllPopOver() });
});

//produs
$(document).on('click', ".linckProdus", function (e) {
    var url = $(this).data('url');
    $('#tabelaProdus').load(url, null, function () { loadAllSelect2(); loadAllTooltip(); loadAllPopOver() });
});

//actiuni stergere /editare categorii /produse

//get modal edit produs
$(document).on('click', ".EditareProdus", function (e) {
    var url = $(this).data('url');

    $.get(url, function (data) {
        $('#ProdusContainer').html(data), loadAllSelect2(); loadAllTooltip(); loadAllPopOver();

        $('#ProdusModal').modal('show');
    });
});
//post submit produs editat
$(document).on('click', "#SubmitProdusEditat", function (e) {
    $(this).attr("disabled", true);
    var partialurl = $(this).data('url');
    var pagina = $('#SelectImageArticol').data("pagina");
    var url = partialurl + '?DenumireProdus=' + $('#IdInputDenumire').val() +
        '&IdProdus=' + $('#idInputProdusId').val() +
        '&IdCategorie=' + $('#SelectIdCategorie').val() +
        '&Mentiuni=' + $('#IdInputMentiuni').val() +
        '&Price=' + $('#IdInputPrice').val();
    if (
        $('#IdInputDenumire').val() === "" ||
        $('#IdInputMentiuni').val() === "" ||
        isNaN($('#IdInputPrice').val())) { $("#SubmitProdusEditat").prop("disabled", false); $('#CampuriObligatorii').hide(); $('#CampuriObligatorii').show(); }
    else {

        $.ajax({
            type: "POST",
            url: url,
            contentType: "json",
            success: function (e) {
                $.notify("Produsul a fost editat cu succes", "success");
                $('.modal').modal('hide');
                $("#tabelaProdus").load('Produse/ViewProduseDinCateg?page=' + pagina + '&IdCategorie=' + $('#SelectIdCategorie').val() + '&denumire=' + $("#IdFIltruProdus").val(), null, function () { loadAllSelect2(); loadAllTooltip(); loadAllPopOver() });
            }, error: function (e) {
                $.notify(e.responseText, "error");
            }
        })
    }
});

//sterge produs
$(document).on('click', ".StergeProdus", function (e) {
    $('#loader').modal('show');
    var url = $(this).data('url');
    var p = $(this).parent().parent();


    $.ajax({
        url: url,
        type: 'POST',
        dataType: "json",
        success: function (e) { },
        error: function (e) {
            if (e.responseText === "Sters") {
                $.notify("Produsul a fost șters cu succes", "success");
                p.remove();
            } else { $.notify(e.responseText, "error"); }
            $('#loader').modal('hide');
        }

    });

});
//sterge categorie produs
$(document).on('click', ".StergeCategorieProdus", function (e) {
    $('#loader').modal('show');
    var url = $(this).data('url');
    var nr = $(this).data('nriteme');
    var p = $(this).parent().parent();

    $.ajax({
        url: url,
        type: 'POST',
        dataType: "Json",
        success: function (e) { },
        error: function (e) {
            if (e.responseText === "Sters") {
                $.notify("Categoria a fost stersa cu succes", "success");
                p.remove();
            } else { $.notify("Categoria contine produse", "error"); }
            $('#loader').modal('hide');
        }

    });

});

//pagination produse
$(document).on("click", "#paginareCategorieCuProduse a", function () {
    if (typeof $(this).attr("href") !== "undefined") {
        $('#loader').modal('show');
        var searchURL = $(this).attr("href") + '&denumire=' + $("#IdFIltruProdus").val();
        $.ajax({
            url: searchURL,
            type: 'GET',
            cache: false,
            success: function (result) {
                $('#tabelaProdus').html(result), loadAllSelect2(); loadAllTooltip(); loadAllPopOver();
                $('#loader').modal('hide');
            }
        });
        return false;
    }
});

$(document).on('change', "#SelectImageArticol", function (e) {
    var url = $(this).data('url');
    var file = $(this).get(0).files;
    var idArticol = $(this).data("id");
    var pagina = $(this).data("pagina");
    var IdCatArt = $(this).data("idcatart");
    var data = new FormData;
    data.append("ImageFile", file[0]);
    data.append("Id", idArticol);
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        contentType: false,
        processData: false,
        success: function (e) {
            $("#tabelaProdus").load('Produse/ViewProduseDinCateg?page='
                + pagina + '&IdCategorie=' + IdCatArt + '&denumire=' + $("#IdFIltruProdus").val(), null, function () { loadAllSelect2(); loadAllTooltip(); loadAllPopOver() });
        }
    })
});



$(document).on('click', ".StareProdus", function (e) {
    $('#loader').modal('show');
    var partialUrl = $(this).data('url');
    //var Stare = ($(this).is(':checked'))
    var url = partialUrl;// + "&Stare=" + Stare
    $.ajax({
        url: url,
        type: 'POST',
        dataType: "text",
        success: function (e) {
            $.notify(e, "success");
            $('#loader').modal('hide');
        },

        error: function (e) {
            $.notify(e, "error");
            $('#loader').modal('hide');
        }

    });

});