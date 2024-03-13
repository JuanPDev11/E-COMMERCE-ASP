var dtable;
$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("pending")) {
        OrderTable("pending");
    }
    else {
        if (url.includes("approved")) {
            OrderTable("approved");
        }
        else {
            if (url.includes("shipped")) {
                OrderTable("shipped");
            }
            else {
                if (url.includes("underprocess")) {
                    OrderTable("underprocess");
                }
                else {
                    OrderTable("all");
                }
            }
        }
    }
});

function OrderTable(status) {
    dtable = $('#myTableO').DataTable({
        "ajax": { "url": "/Admin/Orders/AllOrders?status=" + status },
        "columns": [
            { "data": "name" },
            { "data": "phone" },
            { "data": "orderStatus" },
            { "data": "orderTotal" },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Admin/Orders/OrderDetails?id=${data}"><i class="bi bi-pencil-square"></i></a>`
                }
            }
        ]
    });
}

///////////////////////////

var dtable;
$(document).ready(function () {
    dtable = $('#myTable').DataTable({
        "ajax": { "url": "/Admin/Product/AllProducts" },
        "columns": [
            { "data": "name" },
            { "data": "description" },
            { "data": "price" },
            { "data": "artistData.name" },
            { "data": "category.name" },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Admin/Product/CreateUpdate?id=${data}"><i class="bi bi-pencil-square"></i></a>
                        <a onclick=RemoveProduct("/Admin/Product/Delete/${data}")><i class="bi bi-trash"></i></a>`
                }
            }
        ]
    });
});

function RemoveProduct(url) {
    Swal.fire({
        title: '¿AreYou Sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dtable.ajax.reload();
                        toastr.success(data.message)
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    })
}