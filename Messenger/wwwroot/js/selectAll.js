$(function () {
    $('#select_all').click(function () {
        var c = this.checked;
        $('input[name="selectUser"]').prop('checked', c);
    });
});