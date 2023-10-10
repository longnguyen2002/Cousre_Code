
$(document).ready(function () {
    ShowCount();
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = 1;
        var tQuantity = $('#quantity_value').text();
        if (tQuantity != '') {
            quantity = parseInt(tQuantity);
        }
       
        $.ajax({
            url: '/ShoppingCart/AddToCart',
            type: 'POST',
            data: { id: id, quantity: quantity },
            success: function (rs) {
                if (rs.success) {
                    $('#checkout_item').html(rs.Counts);  
                }
                alert(rs.tbm);
                $('#checkout_item').html(rs.Counts); 
                
            }
        });
        
    });

    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conc = confirm("Bạn có chắc muốn xóa sản phẩm này khỏi giỏ hàng không!!!");
        if (conc == true) {
            $.ajax({
            url: '/ShoppingCart/Delete',
            type: 'POST',
            data: { id: id},
            success: function (rs) {
                if (rs.success) {
                    $('#checkout_item').html(rs.Counts);
                    $('#trow_' + id).remove();
                }
               
                $('#checkout_item').html(rs.Counts);
                $('#trow_' + id).remove();
                LoadCart();
               

            }
        });
        }
        

    });

    $('body').on('click', '.btnDeleteAll', function (e) {
        e.preventDefault();
        var conc = confirm("Bạn có chắc muốn xóa hết sản phẩm trong giỏ hàng không!!!");
        if (conc == true) {
            DeleteAll();
        }
    });

    $('body').on('click', '.btnUpdate', function (e) {
        e.preventDefault();
        var id = $(this).data("id");
        var quantity = $('#Quantity_' + id).val();
        Update(id, quantity);
    });
});

function ShowCount() {
    $.ajax({
        url: '/ShoppingCart/ShowCount',
        type: 'GET',
        success: function (rs) {          
            $('#checkout_item').html(rs.Counts);
            
        }
    });
}

function DeleteAll() {
    $.ajax({
        url: '/ShoppingCart/DeleteAll',
        type: 'POST',
        success: function (rs) {
            if (rs.success) {
                LoadCart();
            }

        }
    });
}

function Update(id,quantity) {
    $.ajax({
        url: '/ShoppingCart/Update',
        type: 'POST',
        data: { id: id, quantity: quantity },  
        success: function (rs) {
            if (rs.success) { 
                LoadCart();
            }

        }
    });
}

function LoadCart() {
    $.ajax({
        url: '/ShoppingCart/Partial_Item_Cart',
        type: 'GET',
        success: function (rs) {
            $('#load_data').html(rs); 

        }
    });
}