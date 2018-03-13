function edit() {
    var view = window.location.hash.substring(1);

    $.ajax({
        type: 'GET',
        url: 'Directory/Partail?viewName=' + view,
        success: function (data) {
            document.querySelector('#dialogContent').innerHTML = data;
            $('#modDialog').modal('show');
        }
    });
}

function change(type) {
    document.querySelector('#title').innerHTML = type;

    setTimeout(function () {
        var hash = window.location.hash.substring(1);
        getAll(hash);
    }, 1);
    
    console.log(type);
}

function getAll(type) {

    $.ajax({
        url: "/api/DirectoryNH/" + type,
            dataType: []
        })
        .done(draw);
}

function draw(data) {
    var list = JSON.parse(data);

    var body = "";
    console.log(list);

    for (var i = 0; i < list.length; i++) {
        body += ' <div>' + list[i] + '</div>';
        body +=
            ' <input type="button" class="btn btn-default" style="width: 73%" value="Редактировать" onclick="edit()"/>';

        body += '<input type="button" class="btn btn-default" style="width: 73%" value="Удалить" />';
        body += '<br />';
    }
    
    document.querySelector('#list').innerHTML = body;
       
}


function onLoad() {
    window.location.hash = "Attendees";
    change("Участники");
}

function save() {

}

function remove()
{

}