var index = 0;
var data = [];

$('html').ready(onLoad());

function add() {
    index = 0;
    var view = window.location.hash.substring(1);

    $.ajax({
        type: 'GET',
        url: 'Directory/Partail?viewName=' + view,
        success: function (data) {
            document.querySelector('#dialogContent').innerHTML = data;
            $('#modDialog').modal('show');
            onModalShow(view);
        }
    });
}

function edit(index) {
    this.index = index;

    var view = window.location.hash.substring(1);

    $.ajax({
        type: 'GET',
        url: 'Directory/Partail?viewName=' + view,
        success: function (data) {
            document.querySelector('#dialogContent').innerHTML = data;
            $('#modDialog').modal('show');
            onModalShow(view);
        }
    });
}

function change(type) {
    document.querySelector('#title').innerHTML = type;
    document.querySelector('#list').innerHTML = "";

    setTimeout(function () {
        var hash = window.location.hash.substring(1);
        getAll(hash);
    }, 1);
    
    console.log(type);
}

function getAll(type) {

    $.ajax({
        url: "/api/" + type + "NH/GetSimple",
            dataType: 'text'
        })
        .done(function (data) { draw(data)});
}

function draw(data) {
    var list = JSON.parse(data);
    this.data = list;
    var body = "";
    console.log(list);

    for (var i = 0; i < list.length; i++) {
        body += ' <div class="element" onclick="edit(' + list[i].Id + ')">' + list[i].View + '</div>';
        body += '<br />';
    }
    
    document.querySelector('#list').innerHTML = body;
       
}


function onLoad() {
    if (window.location.hash != null && window.location.hash !== "") {
        var type = window.location.hash.substring(1);
        getAll(type);
    } else {
        window.location.hash = "Attendees";
        change("Участники");
    }
}

function saveSchool() {
    var name = $('#schoolName')[0].value;
    var address = $('#address')[0].value;

    if (index === 0) {
        $.ajax({
            type: 'POST',
            url: '/api/SchoolNH/Add?name=' + name + "&address=" + address,
            success: function () {
                getAll("School");
                $('#modDialog').modal('hide');
            }
        });

    } else {
        $.ajax({
            type: 'POST',
            url: '/api/SchoolNH/Save?id=' + index + "&name=" + name + "&address=" + address,
            success: function () {
                getAll("School");
                $('#modDialog').modal('hide');
            }
        });

    }
}

function saveSchoolType() {
    var name = $('#name')[0].value;

    if (index === 0) {
        $.ajax({
            type: 'POST',
            url: '/api/SchoolTypeNH/Add?name=' + name,
            success: function () {
                getAll("SchoolType");
                $('#modDialog').modal('hide');
            }
        });

    } else {
        $.ajax({
            type: 'POST',
            url: '/api/SchoolTypeNH/Save?id=' + index + "&name=" + name,
            success: function () {
                getAll("SchoolType");
                $('#modDialog').modal('hide');
            }
        });

    }
}

function remove() {
    var hash = window.location.hash.substring(1);

    $.ajax({
        type: 'POST',
        url: '/api/' + hash + 'NH/Delete?id=' + index,
        success: function () {
            getAll(hash);
            $('#modDialog').modal('hide');
        }
    });

}


function save(view) {
    switch (view) {
    case "School":
        saveSchool();
    case "SchoolType":
        saveSchoolType();
    default:
    }
}

function onModalShow(view) {
    switch (view) {
    case "School":
        schoolsOnLoad(); 
    case "SchoolType":
        schoolTypesOnLoad();
    default:
    }
}

function schoolsOnLoad() {
    var element;
    var isLoad;

    if (index != 0) {
        $.ajax({
                url: "/api/SchoolNH/Get?index=" + index

        })
            .done(function(data) {
                element = data;
                $("#schoolName")[0].value = element.Name;
                if (isLoad == false) {
                    $("#address")[0].value = element.Addresses[0].Id;

                }

            });

    }


    $.ajax({
        url: "/api/DirectoryNH/Addresses?index=" + index,
            dataType: 'text'
        })
        .done(function(json) {
            var data = JSON.parse(json);

            for (i = 0; i < data.length; i++) {
                var o = new Option(data[i].FullAddress, data[i].Id);
                $(o).html(data[i].FullAddress);
                $("#address").append(o);
                
                }
            isLoad = false;

            if (element!=undefined) {
                isLoad = true;
                $("#address")[0].value = element.Addresses[0].Id;
            }
        });
}


function schoolTypesOnLoad() {
    if (index != 0) {
        $.ajax({
                url: "/api/SchoolTypeNH/Get?index=" + index

            })
            .done(function(element) {
                $("#name")[0].value = element.Name;
            });

    }
}