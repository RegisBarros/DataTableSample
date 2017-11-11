$(document).ready(function () {
    $('#myTable').DataTable({
        "processing": true,
        "serverSide": true,
        // "ajax": "employees",
        ajax: function (data, callback, settings) {
            console.log(data);
            console.log(settings);


            var filter = {
                draw: data.draw,
                columns: data.columns
            }

            $.ajax({
                url: 'employees',
                method: 'GET',
                data: filter
            }).done(callback);
        },
        "columns": [
            { "data": "name" },
            { "data": "position" },
            { "data": "office" },
            { "data": "age" },
            { "data": "startDate" },
            { "data": "salary" },
        ]
    });
});