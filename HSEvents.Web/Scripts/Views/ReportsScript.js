$('html').ready(onLoad());

function onLoad() {
    $.ajax({
            url: "/api/AttendeeNH/GetAll",
            dataType: 'text'
        })
        .done(function (data) { draw(data) });
}

function draw(data) {

    var list = JSON.parse(data);
    this.data = list;

    if (list.length == 0) {
        document.querySelector('#report').style.visibility = "hidden";
        document.querySelector('#notFound').style.visibility = "visible";
        document.querySelector('#reportBody').innerHTML = "";
        return;
    }

    document.querySelector('#report').style.visibility = "visible";
    document.querySelector('#notFound').style.visibility = "hidden";


    var body = "";
    console.log(list);

    for (var i = 0; i < list.length; i++) {
        var element = list[i];
        body += '<tr>';
        body += '<td>' + element.ContactInfo.FullName + '</td>';
        body += '<td>' + element.ContactInfo.PhoneNumber + '</td>';
        body += '<td>' + element.ContactInfo.Email + '</td>';
        body += '<td>' + getType(element.Type) + '</td>';

        body += '</tr>';

    }

    console.log(body);
    document.querySelector('#reportBody').innerHTML = body;
       
}

function getType(type) {
    switch (type) {
        case 1:
            return "Абитуриент";
        case 2:
            return "Родитель";
        case 3:
            return "Учитель";
        default:
            return "";
    }
}

function checkedChanged() {

    var attendeed = document.querySelector('#attendeed');
    var registered = document.querySelector('#registered');
    var currentYearGraduate = document.querySelector('#currentYearGraduate');
    var entered = document.querySelector('#entered');
    var participated = document.querySelector('#participated');

        if (!attendeed.value && !registered.value && !currentYearGraduate.value && !entered.value && !participated.value) {
            $.ajax({
                url: "/api/AttendeeNH/GetAll",
                    dataType: 'text'
                })
                .done(function (data) { draw(data) });

        }
    else {
            $.ajax({
                url: "/api/AttendeeNH/GetAllByParams?attendeed=" + attendeed.checked + "&registered=" + registered.checked
                    + "&currentYearGraduate=" + currentYearGraduate.checked + "&entered=" + entered.checked + "&participated=" + participated.checked,
                    dataType: 'text'
                })
                .done(function(data) { draw(data) });
        }
    
}

var data = [];

function exportClick() {
    console.log(data);

    $.ajax({
            type: 'POST',
            url: "/Reports/Index",
            data: { 'data': data }

        })
        .done(function (isCorrect) { if (isCorrect) location.href = "/Reports/Create" });
}