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

var data = [];

var obj;

function addDate() {
    var text = document.querySelector('#Date').value;
    var date = new Date(text);
    if (date =='Invalid Date') {
        alert("Неверный формат даты");
        return;
    }

    text = document.querySelector('#Start').value;
    var start = new Date("12.12.2012 " + text);
    if (start == 'Invalid Date') {
        alert("Неверный формат даты");
        return;
    }

    text = document.querySelector('#End').value;
    var end = new Date("12.12.2012 " + text);
    if (end == 'Invalid Date') {
        alert("Неверный формат даты");
        return;
    }
    var startTime = start.getTime();
    var endTime = end.getTime();

    obj = new Object();

    obj.date = date.getTime();
    obj.startTime = startTime;
    obj.endTime = endTime;

    console.log(obj);
    data.push(obj);

    console.log(data);

    document.querySelector('#datesBody').innerHTML +=
        "<tr>" + "<td>" + date + "</td>"
        + "<td>" + start.getHours() + ":" + start.getMinutes() + "</td>"
        + "<td>" + end.getHours() + ":" + end.getMinutes() + "</td>" + "</tr>";

}

var executions = [];

function saveExecution() {
    if (data.length == 0) {
        alert("Выберите хотя бы одну дату");
        return;
    }

    var address = document.querySelector('#Address').value;


    $.ajax({
        type: 'POST',
        url: '/Events/GetExecution',
        data: {
            'address':address,
             'data': data
        },
        success: function (data) {
            console.log(data);

            console.log(data);
            executions.push(data);
            console.log(executions);

            $('#modDialog').modal('hide');


            document.querySelector('#executionBody').innerHTML +=
             "<tr>"+"<td>" + data.Dates[0].Date + "</td>"
            + "<td>" + data.Address.Id + "</td>" + "</tr>";

        }
    });
    data = [];
}

function addEvent() {
    $.ajax({
        type: 'POST',
        url: '/Events/PutExecutions',
        data: {
            'executions': executions
        }
    });
}