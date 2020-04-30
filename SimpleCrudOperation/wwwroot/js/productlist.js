var dataTable;
$(document).ready(function () {
    dataTable = $('#DT_Load').DataTable({
        "ajax": {
            "url": "/Product/GetAllDataApiJson",
             "type": "GET",
            "datatype":"json"
        },
        "columns": [
            {
                "data": "productName",
                "width":"20%"
            },
            {
                "data": "unitPrice",
                "width":"20%"
            },
            {
                "data": "totalQuantity",
                "width": "20%"
            },
            {
                "data": "id",
                "width": "40%",
                "render": function (data) {
                    return `<div class='text-center'>
                    <a class='btn btn-success' href="/Product/Edit?id=${data}">Edit</a>
                        &nbsp;
                    <a onclick=Delete('/Product/DeleteByDataApiJson?id='+${data}) class='btn btn-danger text-white' style='cursor:pointer'>Delete</a>
                       
                       </div>`
                   
                }
            }

        ],
        "language": {
            "emptyTable":"Not Found Data"
        },
        "width":"100%"

    });
});


function Delete(url) {

    swal({
        title: "Are you want to delete?",
        text: "It will be pramanently delete!",
        icon: "warning",
        buttons: true,
        dangermode: true

    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })


}