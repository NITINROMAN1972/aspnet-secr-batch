



$(document).ready(function () {

    // search - batch no
    $('#ddScBatchNo').select2({
        theme: 'classic',
        placeholder: 'Select here.....',
        allowClear: false,
    });

    // search - unit or office
    $('#ddScUnit').select2({
        theme: 'classic',
        placeholder: 'Select here.....',
        allowClear: false,
    });
    





    // unit or office
    $('#ddUnitOffice').select2({
        theme: 'classic',
        placeholder: 'Select here.....',
        allowClear: false,
    });
    $('#ddUnitOffice').on('select2:select', function (e) {
        __doPostBack('ddUnitOffice', '');
    });








    // sumo select
    $('#ddBillNo').on('sumo:opening', function () {
        $('.select-all').css('height', '40px');
    });

    // sumo select elements
    $('#ddBillNo').SumoSelect({
        search: true,
        searchText: "search here....",
        multiSelect: true,
        okCancelInMulti: true,
        prefix: "",
        up: false, // list at top side
        selectAll: true, // not working properly
        clearAll: true, // not working at all
        renderOption: function (data, escape) {
            // Check if the option is the "Select Values" item
            if (data.value === "0") {
                // Add a class to "Select Values" item to handle differently
                return '<div class="title special-item">' + escape(data.text) + '</div>';
            } else {
                return '<div><label><input type="checkbox" />' + escape(data.text) + '</label></div>';
            }
        }
    });

    // Add an event listener to handle the specific item
    $('#ddBillNo').on('sumo:opened', function () {
        // Check if the "Select Values" item is present
        if ($('#ddBillNo .special-item').length > 0) {
            // Disable multiselect for the "Select Values" item
            $('#ddBillNo .special-item input').prop('disabled', true);
        }
    });





    // Reinitialize Select2 after UpdatePanel partial postback
    var prm = Sys.WebForms.PageRequestManager.getInstance();

    // Reinitialize Select2 for all dropdowns
    prm.add_endRequest(function () {

        setTimeout(function () {

            // search - batch no
            $('#ddScBatchNo').select2({
                theme: 'classic',
                placeholder: 'Select here.....',
                allowClear: false,
            });

            // search - unit or office
            $('#ddScUnit').select2({
                theme: 'classic',
                placeholder: 'Select here.....',
                allowClear: false,
            });





            // unit or office
            $('#ddUnitOffice').select2({
                theme: 'classic',
                placeholder: 'Select here.....',
                allowClear: false,
            });
            $('#ddUnitOffice').on('select2:select', function (e) {
                __doPostBack('ddUnitOffice', '');
            });




            // sumo select
            $('#ddBillNo').on('sumo:opening', function () {
                $('.select-all').css('height', '40px');
            });

            // sumo select elements
            $('#ddBillNo').SumoSelect({
                search: true,
                searchText: "search here....",
                multiSelect: true,
                okCancelInMulti: true,
                prefix: "",
                up: false, // list at top side
                selectAll: true, // not working properly
                clearAll: true, // not working at all
                renderOption: function (data, escape) {
                    // Check if the option is the "Select Values" item
                    if (data.value === "0") {
                        // Add a class to "Select Values" item to handle differently
                        return '<div class="title special-item">' + escape(data.text) + '</div>';
                    } else {
                        return '<div><label><input type="checkbox" />' + escape(data.text) + '</label></div>';
                    }
                }
            });

            // Add an event listener to handle the specific item
            $('#ddBillNo').on('sumo:opened', function () {
                // Check if the "Select Values" item is present
                if ($('#ddBillNo .special-item').length > 0) {
                    // Disable multiselect for the "Select Values" item
                    $('#ddBillNo .special-item input').prop('disabled', true);
                }
            });





        }, 0);
    });







});