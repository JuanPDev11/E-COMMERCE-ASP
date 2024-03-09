var dtable;
$(document).ready(function(){
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
            { "data": "orderStatus"},
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