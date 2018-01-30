
class Event {
    constructor(name, day, id) {
        this.name = name;
        this.day = day;
        this.id = id;
    }

    getDate() {
        return this.day;
    }
}

function eventFilter(event) {
    var day = event.day;
    return currentDay === day;
}


var calendarDate = new Date();
var currentDay = 1;

var events = [];
var g = new Event('g', calendarDate.getDate(), 1);
var gt = new Event('gt', 23, 1);
events.push(g);
events.push(gt);

function getEvents() {
    $.ajax({
        url: "http://localhost:58724/api/EventNH/GetForMonth",
        data: { month: 1},
        dataType: []
    }).done(function (data) {
        events = JSON.parse(data);
        console.log(data);
        console.log(events);
    });
}
getEvents();

function calendarBig(year, month) {
    
    var body = '';

    var date = new Date(year, month, 1);

    var first = date.getDay();
    if (first !== 1) {

        var last = new Date(year, month, 0).getDate();


        if (first !== 0) {
            last -= first - 2;
            for (var i = 1; i < first; i++, last++)
                body += '<td class="last">' + last + '</td>';
        }
        else {
            last -= 5;
            for (var i = 0; i < 6; i++, last++)
                body += '<td class="last">' + last + '</td>';
        }

    }

    while (date.getMonth() === month) {

        currentDay = date.getDate();
        var filtered = events.filter(eventFilter);
        if (filtered.length > 0) {
            body += '<td style="background-color:pink">' + currentDay + '</td>';
            console.log(filtered);
        } else
            body += '<td>' + currentDay + '</td>';

        var dayOfWeek = date.getDay();

        if (dayOfWeek === 0)
            body += '</tr><tr>';
        
        date.setDate(date.getDate() + 1);
    }

    var next = date.getDay();

    if (next !== 1) {

        if (next !== 0) {
            for (var i = next, day = 1; i <= 7; i++, day++)
                body += '<td class="next">' + day + '</td>';
        } else
            body += '<td class="next">1</td>';

    }

    body += '</tr>';
    
    document.querySelector('#calendarBody').innerHTML = body;

    document.querySelector('#calendarDate').innerHTML =
        getCurrentMonth(calendarDate.getMonth()) + ' ' + calendarDate.getFullYear();

}
calendarBig(calendarDate.getFullYear(), calendarDate.getMonth());

function getCurrentMonth(month) {
    switch (month) {
    case 0:
        return "Январь";
    case 1:
        return "Февраль";
    case 2:
        return "Март";
    case 3:
        return "Апрель";
    case 4:
        return "Май";
    case 5:
        return "Июнь";
    case 6:
        return "Июль";
    case 7:
        return "Август";
    case 8:
        return "Сентябрь";
    case 9:
        return "Октябрь";
    case 10:
        return "Ноябрь";
    case 11:
        return "Декабрь";
        
        default:
            return "";
    }
    
}

function nextMonth() {
    calendarDate.setMonth(calendarDate.getMonth() + 1);

    calendarBig(calendarDate.getFullYear(), calendarDate.getMonth());
}


function lastMonth() {
    calendarDate.setMonth(calendarDate.getMonth() - 1);

    calendarBig(calendarDate.getFullYear(), calendarDate.getMonth());
}

$('html').keydown(function (eventObject) {
    if (eventObject.keyCode === 37)
        lastMonth();
    if (eventObject.keyCode === 39)
        nextMonth();
});

