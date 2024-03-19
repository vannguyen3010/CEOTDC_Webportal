var laddaSubmitForm;

function ResetForm(formElm) {
    $(formElm).trigger('reset');
    RemoveClassValidate(formElm);
}
function showAlert(elm) {
    $(elm).addClass("show");
    setTimeout(function () {
        $(elm).removeClass("show");
        $(elm).fadeOut(2000);
        ResetForm("#form_data")
    }, 3000)
}

//Submit form
$('#form_data').on('submit', function (e) {
    laddaSubmitForm = Ladda.create(document.querySelector('#btn_submit_form'));
    laddaSubmitForm.start();
    e.preventDefault();
    let $formElm = $('#form_data');
    let isvalidate = CheckValidationUnobtrusive($formElm);
    if (!isvalidate) { laddaSubmitForm.stop(); return false; }
    let formData = new FormData($formElm[0]);
    $.ajax({
        url: '/Contact/SubmitContact',
        type: $formElm.attr('method'),
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (!CheckResponseIsSuccess(response)) return false;
            ResetForm("#form_data")
            laddaSubmitForm.stop();
            showAlert("#alert_submit");
        }, error: function (err) {
            laddaSubmitForm.stop();
            CheckResponseIsSuccess({ result: -1, error: { code: err.status } });
        }
    });
});

//Init Map
function InitMap() {
    var marker, i, infowindow;
    let latitude = parseFloat($("#input_latitude").val());
    let longitude = parseFloat($("#input_longitude").val());
    var locations = [
        ['', latitude, longitude, 1],
    ];
    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 14,
        center: new google.maps.LatLng(latitude, longitude),
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });
    for (i = 0; i < locations.length; i++) {
        marker = new google.maps.Marker({
            position: new google.maps.LatLng(locations[i][1], locations[i][2]),
            animation: google.maps.Animation.BOUNCE,
            //icon: '~/images/icon/map-marker2.png',
            map: map
        });

        if (!IsNullOrEmty(locations[i][0])) {
            google.maps.event.addListener(marker, 'onload', (function (marker, i) {
                infowindow = new google.maps.InfoWindow();
                infowindow.setContent(locations[i][0]);
                infowindow.open(map, marker);
            })(marker, i));
        }
    }
}