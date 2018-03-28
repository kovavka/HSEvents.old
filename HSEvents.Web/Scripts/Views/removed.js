function remove(hash, index) {

    $.ajax({
        type: 'POST',
        url: '/api/' + hash + 'NH/Delete?id=' + index,
        success: function () {
            location.href = '/Directory';
        },
        error: function () {
            alert(
                "Не удалось удалить сущность, т.к. она связана с другими, попробуйте сначала удалить связанные сущности");
        }
    });

}