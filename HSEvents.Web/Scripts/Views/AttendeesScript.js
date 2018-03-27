function changeType() {

    var type = document.querySelector('#Type').value;
    if (type == 1)
        document.querySelector('#pupilItems').style.visibility = "visible";
    else
        document.querySelector('#pupilItems').style.visibility = "hidden";
}

function openAttendee(index) {
    location.href = "/Attendees/Edit?id=" + index;
}

function deleteAttendee(index) {
    location.href = "/Attendees/Delete?id=" + index;
}