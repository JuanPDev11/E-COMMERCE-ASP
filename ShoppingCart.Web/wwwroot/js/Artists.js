var dtable;
$(document).ready(function () {
    dtable = $('#myTableA').DataTable({
        "ajax": { "url": "/Admin/Artist/AllArtists" },
        "columns": [
            {
                "data": "imageUrl",
                "render": function (data) {

                    return `<img src="${data}" width="30" height="30" />`
                }
            },
            { "data": "name"},
            { "data": "description"},
            { "data": "age"},
            { "data": "volume"},
            { "data": "followers"},
            { "data": "totalSold" },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Admin/Artist/CreateUpdate?id=${data}"><i class="bi bi-pencil-square"></i></a> 
                            <a onclick=RemoveArtist("/Admin/Artist/Delete/${data}")><i class="bi bi-trash-fill"></i></a>`
                }
            }
        ]
    });
});

function RemoveArtist(url) {
    Swal.fire({
        title: 'Estas Seguro?',
        text: 'Esta accion es irreversible',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#8b0000',
        cancelButtonColor: '#a9a9a9',
        confirmButtomText: 'Si, Borrar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dtable.ajax.reload();
                        toastr.success(data.message)
                    } else {
                        toastr.error(data.message)
                    }
                }
            });
        }
    })
}