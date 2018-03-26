function setAccess(index) {
    $.ajax({
            type: 'PUT',
            url: "/api/UsersNH/SetAccess?id="+index

        })
        .done(function () { changeElement('access' + index, true) });
}

function changeRole(index) {
    $.ajax({
            type: 'PUT',
            url: "/api/UsersNH/ChangeRole?id=" + index

        })
        .done(function(data) { changeElement('role' + index, data) });
}

function changeElement(id, value) {
    document.querySelector("#"+id).innerHTML = value;
}
