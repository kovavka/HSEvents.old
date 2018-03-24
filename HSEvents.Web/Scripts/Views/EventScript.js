var calendarDate = new Date();
var currentDay = 1;

function eventFilter(event) {
    var day = event.Day;
    return currentDay === day;
}

function creteCalendar() {
    var year = calendarDate.getFullYear();
    var month = calendarDate.getMonth();

    document.querySelector('#preloaderbg').style = "display: block";
    

    $.ajax({
        url: "/api/EventNH/GetForMonth",
        data: { month: month+1 },
        dataType: 'text'
    })
        .done(function (data) {
        console.log('done');
        var events = JSON.parse(data);
        console.log(events);
        fillCalendar(year, month, events);

        document.querySelector('#preloaderbg').style = "";
    });
}

function fillCalendar(year, month, events) {
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

        body += '<td><div><h4>' + currentDay + '</h4>';

        for (var i = 0; i < filtered.length; i++) {

            var color = 'blue';
            if (filtered[i].Colors!=null && filtered[i].Colors.length == 1)
                color = filtered[i].Colors[0];

            body += '<div class="popup" style="background-color:' + color + '" onclick="openEvent()" ondblclick="editEvent()">' + filtered[i].Name
                + '<span class="popuptext" id="myPopup' + currentDay+i + '">Popup text...</span>'
                + '</div>';

        }
        body += '</div></td>';

        

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

document.addEventListener('click', printMousePos, true);
var X = 0;
var Y = 0;

function printMousePos(e) {
    console.log(e.pageX);
    X = e.pageX;
    console.log(e.pageY);
    Y = e.pageY;
}

function openEvent() {
    var popup = document.getElementById("tdd");


    popup.style = "position: absolute;     left:" + X + "px; top: " + Y + "px;";


    //var popup = document.getElementById("myPopup" +day+ index);
    //popup.classList.toggle("show");
}

function editEvent() {
    window.open('/Event/Add');
}

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
    creteCalendar();
}


function lastMonth() {
    calendarDate.setMonth(calendarDate.getMonth() - 1);
    creteCalendar();
}

creteCalendar();

$('html').keydown(function (eventObject) {
    if (eventObject.keyCode === 37)
        lastMonth();
    if (eventObject.keyCode === 39)
        nextMonth();
});



// When the user clicks on <div>, open the popup
    function myFunction() {
        var popup = document.getElementById("myPopup");
        popup.classList.toggle("show");
    }
 