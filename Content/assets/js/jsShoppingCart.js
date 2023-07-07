$(document).ready(function () {

    ShowCount();
    loadDataCart();
    loadDataCheckOut();
   
    $("body").on('click', '.btnAddToCart', function (e) {
     
        e.preventDefault();
        var id = $(this).attr("data-id");
        var quantity = 1;
        var tQuantity = $('#quantity_value').text();
        if (tQuantity != '') {
            var quantity = parseInt(tQuantity);
        }
        $.ajax({
            url: '/ShoppingCart/AddToCart',
            type: 'POST',
            data: { id: id ,quantity:quantity },
            success: function (rs) {
                if (rs.success) {
                    $('#checkout_items').html(rs.TotalItems);
                   
                }
            }
        })
    })
   
    $('body').on('click', '.btnUpdate', function () {

        var id = $(this).attr("data-id");
        var quantity = $('#Quantity_' + id).val();
        Update(id, quantity);
    })
    $('body').on('click', '.btnDelete', function () {

        var id = $(this).attr("data-id");
        var conf = confirm('Bạn có muốn xóa sản phẩm này khỏi giỏ hàng ?')
        if (conf === true) {
            $.ajax({
                url: '/ShoppingCart/Delete',
                type: 'POST',
                data: { id: id },
                success: function (rs) {
                    if (rs.success) {
                        $('#checkout_items').html(rs.TotalItems);
                        $('#trow_' + id).fadeTo('fast', 0.5, function () {
                            $(this).slideUp('fast', function () {
                                loadDataCart();
                                $(this).remove();
                            })

                        })
                    }
                }
            })
        }
    })
    $('body').on('click', '.btnDeleteAll', function (e) {

        e.preventDefault();
        var conf = confirm('Bạn có muốn xóa hết khỏi giỏ hàng ?')
        if (conf === true) {
            $.ajax({
                url: '/ShoppingCart/DeleteAll',
                type: 'POST',
                data: { },
                success: function (rs) {
                    if (rs.success) {   
                        $('#checkout_items').html(rs.TotalItems);
                        loadDataCart();

                    }
                }
            })
        }
    })
});
function ShowCount() {
   
    $.ajax({
        url: '/ShoppingCart/ShowCount',
        type: 'GET',
        success: function (rs) {     
            if (rs.success) {
                $('#checkout_items').html(rs.TotalItems);

            }    
        }
    })
}

function Update(id, quantity) {

    $.ajax({
        url: '/ShoppingCart/Update',
        type: 'POST',
        data: { id: id, quantity:quantity },
        success: function (rs) {

            if (rs.success) {

                loadDataCart();
                $('#checkout_items').html(rs.TotalItems);

            }
        }
    })
}
function addCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

function loadDataCart() {

    $.ajax({
        url: '/ShoppingCart/GetDataJSON',
        type: 'GET',
        data: { },
        success: function (rs) {
           
            if (rs.TotalItems > 0) {
                var items = rs.data;
                var html = "";
                var giasp = "";
                var thanhtien = "";
                var tongtien = 0;
                $('#showdata_cart').html('');
                for (var k = 0; k < items.length; k++) {

                    giasp = addCommas(items[k].Price);
                    giasale = addCommas(items[k].PriceSale);
                    thanhtien = addCommas(items[k].Quantity * items[k].Price);
                    tongtien = tongtien + items[k].TotalPrice;
                    
                    html += '<tr class="text-center" id="trow_' + items[k].ProductId + '">';
                    html += '<td>' + (k + 1) + '</td>';
                    html += '<td><img src="' + items[k].ProductImg + '" data-id="' + items[k].ProductId + '" class="imgproduct" style="width:50px;" /></td>';
                    html += '<td><a href="/chi-tiet/' + items[k].AliasName + '-' + items[k].ProductId +'">' + items[k].ProductName + '</a></td>';
                    html += '<td>' + items[k].CategoryName + '</td>';
                    html += '<td><input type="number" class="form-control" id="Quantity_' + items[k].ProductId + '"value="' + items[k].Quantity + '"/></td>';   
                    if (items[k].PriceSale > 0) {
                        html += '<td class="product_price"><a>đ</a>' + giasale + '<span><a class="product_price">đ</a>' + giasp + '</span></td>';
                    }
                    else {
                        html += '<td class="product_price"><a class="product_price">đ</a>' + giasp + '</td>';
                    }
                    html += '<td class="total-acount"><a class="total-acount">đ</a>' + thanhtien + '</td>';  
                    html += '<td><a href="#" data-id="' + items[k].ProductId + '" class="btn btn-sm btn-danger btnDelete">Xóa</a> <a href="#" data-id="' + items[k].ProductId + '" class="btn btn-sm btn-success btnUpdate">Cập nhật</a> </td>';
                    html += '</tr>';
                }
                html += '<tr>';
                html += '<td colspan="6" class="text-right" >Tổng tiền:</td>';
                html += '<td class="text-center">' + addCommas(tongtien) + '</td>';
                html += '<td></td>';
                html += '</tr>';
                html += '<tr>';
                html += '<td colspan="6" class="text-right"></td>';
                html += '<td></td>';
                html += '<td class="text-center"><a href="#" class="btn btn-danger btnDeleteAll">Xóa</a> <a href="/thanh-toan" class="btn btn-success">Thanh toán</a><td>';
             
                html += '</tr>';

            }
            else {
                html += '<tr>';
                html += '<td colspan="4">Không có bản ghi nào</td>';
                html += '</tr>';
            }
           
            $('#showdata_cart').html(html);
           

        }
    })
}
   




function loadDataCheckOut() {

    $.ajax({

        url: '/ShoppingCart/Partial_Item_CheckOut',
        type: 'GET',
        data: {},
        success: function (rs) {
          
            if (rs.TotalItems > 0) {
                var items = rs.data;
                var html = "";
                var giasp = "";
                var thanhtien = "";
                var tongtien = 0;
                
               
              
                for (var k = 0; k < rs.data.length; k++) {


                    giasp = addCommas(items[k].Price);
                    giasale = addCommas(items[k].PriceSale);
                    thanhtien = addCommas(items[k].Quantity * items[k].Price);
                    tongtien = tongtien + items[k].TotalPrice;
                 
                    html += '<tr class="text-left" id="trow_' + items[k].ProductId + '">';
                 
                   
                    html += '<td class="text-left">' + items[k].ProductName + '</td>';

              
                    if (items[k].PriceSale > 0) {
                        html += '<td >đ' + giasale + '<span>đ' + giasp + '</span></td>';
                    }
                    else {
                        html += '<td >đ' + giasp + '</td>';
                    }
              
                    html += '</tr>';
                }
                html += '<tr>';
                html += '<td class="text-left">Tổng tiền:</td>';
                html += '<td class="text-center">' + addCommas(tongtien) + '</td>';
            
                html += '</tr>';
                

                 

            }
            
            $('#showdata_checkout').html(html);


        }
    })
}