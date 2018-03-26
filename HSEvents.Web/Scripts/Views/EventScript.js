function addExecution() {

    $.ajax({
        type: 'GET',
        url: '/Events/AddExecution',
        success: function (data) {
            document.querySelector('#dialogContent').innerHTML = data;
            $('#modDialog').modal('show');
        }
    });
}