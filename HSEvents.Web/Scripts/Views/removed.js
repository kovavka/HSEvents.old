function remove(index) {
    var hash = window.location.hash.substring(1);

    $.ajax({
        type: 'POST',
        url: '/api/' + hash + 'NH/Delete?id=' + index,
        success: function () {
            getAll(hash);
        },
        error: function () {
            alert(
                "Не удалось удалить сущность, т.к. она связана с другими, попробуйте сначала удалить связанные сущности");
        }
    });

}