function changeType() {

    var type = document.querySelector('#Type').value;
    if (type == 1)
        document.querySelector('#pupilItems').style.visibility = "visible";
    else
        document.querySelector('#pupilItems').style.visibility = "hidden";
}