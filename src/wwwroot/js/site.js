$(document).ready(function () {
    $('#myTable').DataTable({
        "processing": true,
        "serverSide": true,
        // "ajax": "employees",
        ajax: function (data, callback, settings) {
            console.log(data);

            // some alternatives here
            // var filter = {
            //     draw: data.draw,
            //     columns: data.columns
            // }

            $.ajax({
                url: 'employees',
                method: 'POST',
                data: data
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