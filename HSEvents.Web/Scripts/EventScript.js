
var calendarDate = new Date();

function calendarBig(year, month) {
    var body = '';

    var date = new Date(year, month, 1);

    var first = date.getDay();
    if (first !== 1)
        if (first!==0)
            for (var i = 1; i < first; i++)
                body += '<td style="border: black"></td>';
        else
            for (var i = 0; i < 6; i++)
                body += '<td style="border: black"></td>';

    while (date.getMonth() === month) {
        var dayOfWeek = date.getDay();
        body += '<td>' + date.getDate() + '</td>';

        if (dayOfWeek === 0)
            body += '</tr><tr>';

        date.setDate(date.getDate() + 1);
    }
    body += '</tr>';
    
    document.querySelector('#calendarBody').innerHTML = body;
}
calendarBig(calendarDate.getFullYear(), calendarDate.getMonth());

function Next() {
    calendarDate.setMonth(calendarDate.getMonth() + 1);

    calendarBig(calendarDate.getFullYear(), calendarDate.getMonth());
}


function Back() {
    calendarDate.setMonth(calendarDate.getMonth() - 1);

    calendarBig(calendarDate.getFullYear(), calendarDate.getMonth());
}
