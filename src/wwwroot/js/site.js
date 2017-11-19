function updateDataTableSelectAllCtrl(table) {
    var $table = table.table().node();
    var $chkbox_all = $('tbody input[type="checkbox"]', $table);
    var $chkbox_checked = $('tbody input[type="checkbox"]:checked', $table);
    var chkbox_select_all = $('thead input[name="select_all"]', $table).get(0);

    // If none of the checkboxes are checked
    if ($chkbox_checked.length === 0) {
        chkbox_select_all.checked = false;
        if ('indeterminate' in chkbox_select_all) {
            chkbox_select_all.indeterminate = false;
        }

        // If all of the checkboxes are checked
    } else if ($chkbox_checked.length === $chkbox_all.length) {
        chkbox_select_all.checked = true;
        if ('indeterminate' in chkbox_select_all) {
            chkbox_select_all.indeterminate = false;
        }

        // If some of the checkboxes are checked
    } else {
        chkbox_select_all.checked = true;
        if ('indeterminate' in chkbox_select_all) {
            chkbox_select_all.indeterminate = true;
        }
    }
}

$(document).ready(function () {
    var rows_selected = [];
    var data_row = [];

    var table = $('#myTable').DataTable({
        'columnDefs': [{
            'targets': 0,
            'searchable': false,
            'orderable': false,
            'width': '1%',
            'className': 'dt-body-center',
            'render': function (data, type, full, meta) {
                return '<input type="checkbox">';
            }
        },
        {
            'targets': 6,
            'className': 'salary'
        }],
        'rowCallback': function (row, data, dataIndex) {
            // Get row ID
            var rowId = data.id;

            // If row ID is in the list of selected row IDs
            if ($.inArray(rowId, rows_selected) !== -1) {
                $(row).find('input[type="checkbox"]').prop('checked', true);
                $(row).addClass('selected');
            }

            // var hasDataRow = data_row.some(function(d){
            //     return d.id === rowId;
            // });

            var oldRow = data_row.filter(function (d) {
                return d.id === rowId;
            });

            if (oldRow.length && oldRow[0].salary !== data.salary) {
                // console.log('id: ' + rowId + ' oldId: ' + oldRow[0].id + ' old: ' + oldRow[0].salary + ' changed: ' + data.salary);

                if (oldRow[0].salary > data.salary) {
                    $(row).find('.salary').addClass('increase-salary');

                    setInterval(function () {
                        $(row).find('.salary').removeClass('increase-salary');
                    }, 2000);
                }
                else {
                    $(row).find('.salary').addClass('decrease-salary');

                    setInterval(function () {
                        $(row).find('.salary').removeClass('decrease-salary');
                    }, 2000);
                }
            }
        },
        'order': [1, 'asc'],
        "processing": false,
        "serverSide": true,
        // "ajax": "employees",
        ajax: function (data, callback, settings) {
            // some alternatives here
            // var filter = {
            //     draw: data.draw,
            //     columns: data.columns
            // }

            $.ajax({
                url: 'employees',
                method: 'POST',
                data: data
            })
                .done(callback)
                .complete(function (response) {
                    data_row = response.responseJSON.data;
                });
        },
        "columns": [
            { "data": "id" },
            { "data": "name" },
            { "data": "position" },
            { "data": "office" },
            { "data": "age" },
            { "data": "startDate" },
            { "data": "salary" },
        ]
    });


    // Handle click on checkbox
    $('#myTable tbody').on('click', 'input[type="checkbox"]', function (e) {
        var $row = $(this).closest('tr');

        // Get row data
        var data = table.row($row).data();

        // Get row ID
        var rowId = data.id;;

        // Determine whether row ID is in the list of selected row IDs 
        var index = $.inArray(rowId, rows_selected);

        // If checkbox is checked and row ID is not in list of selected row IDs
        if (this.checked && index === -1) {
            rows_selected.push(rowId);

            // Otherwise, if checkbox is not checked and row ID is in list of selected row IDs
        } else if (!this.checked && index !== -1) {
            rows_selected.splice(index, 1);
        }

        if (this.checked) {
            $row.addClass('selected');
        } else {
            $row.removeClass('selected');
        }

        // Update state of "Select all" control
        updateDataTableSelectAllCtrl(table);

        // Prevent click event from propagating to parent
        e.stopPropagation();
    });


    // Handle click on table cells with checkboxes
    $('#myTable').on('click', 'tbody td, thead th:first-child', function (e) {
        $(this).parent().find('input[type="checkbox"]').trigger('click');
    });

    // Handle click on "Select all" control
    $('thead input[name="select_all"]', table.table().container()).on('click', function (e) {
        if (this.checked) {
            $('#myTable tbody input[type="checkbox"]:not(:checked)').trigger('click');
        } else {
            $('#myTable tbody input[type="checkbox"]:checked').trigger('click');
        }

        // Prevent click event from propagating to parent
        e.stopPropagation();
    });

    // Handle table draw event
    table.on('draw', function () {
        // Update state of "Select all" control
        updateDataTableSelectAllCtrl(table);
    });

    $('#send').click(function () {
        rows_selected = null;
        table.ajax.reload();
    });

    // https://datatables.net/reference/api/ajax.reload()
    setInterval(function () {
        table.ajax.reload(null, false);
    }, 5000);
});