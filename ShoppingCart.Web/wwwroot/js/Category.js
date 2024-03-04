var dtable;
$(document).ready(function () {
    dtable = $('#myTableC').DataTable({
        "ajax": { "url": "/Admin/Category/AllCategories" },
        "columns": [
            { "data": "name" },
            { "data": "displayOrder" },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Admin/Category/CreateUpdate?id=${data}">Editar</a>
                            <a onclick=RemoveCategory("/Admin/Category/Delete/${data}") style="cursor:pointer;">Eliminar</a>`
                }
            }
        ]
    });
});

function RemoveCategory(url) {
    Swal.fire({
        title: 'Estas seguro?',
        text: 'Esta accion es irreversible',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#8b0000',
        cancelButtonColor: '#808080',
        confirmButtonText: 'Si, Estoy Seguro'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dtable.ajax.reload();
                        toastr.success(data.success)
                    } else {
                        toastr.error(data.success)
                    }
                }
            })
        }
    })
}