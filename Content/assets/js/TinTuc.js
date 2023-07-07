$(document).ready(function () {
   
    loadDSDM();
    loadDataTinTuc();
    $('body').on('click', '#btbSearch', function (e) {
        var txtSearch = $('#txtSearchBox').val();
        $('#show_data').html("");
        loadDataTinTuc(txtSearch);

    })
   
    $('body').on('click', '#btnLuu', function () {

        var tieude = $('#txtTitle').val();
        var mota = $('#txtDescription').val();
        var category = $('#dropDownlist').val();
        var chitiet = CKEDITOR.instances['txtDetail'].getData();
        var anh = $('#txtImage').val();
        var trangthai = $('#gender_male_checkbox').prop('checked');
        var seotitle = $('#txtSeoTitle').val();
        var seodescription = $('#txtSeoDescription').val();
        var seokeywordstle = $('#txtSeoKeywords').val();
        var models = {
            
            "Title": tieude,
            "Description": mota,
            "Categoryid": category,
            "Detail": chitiet,
            "Image": anh,
            "IsActive": trangthai,
            "SeoTitle": seotitle,
            "SeoDescription": seodescription,
            "SeoKeywordstle": seokeywordstle,
        };
        
        if (Validate()) {
      
            $.ajax({
                url: '/Admin/News/AddNews',
                type: 'POST', 
                data: models,
                success: function (rs) {
                    if (rs.success) {  
                        ResetInput();
                        loadDataTinTuc();
                      /*  location.reload();*/
                     
                    }
                }
            })
        }

    })

    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var id = $(this).attr("data-id");
        var conf = confirm('Bạn có muốn xóa bản ghi này không?')
        if (conf === true) {
            $.ajax({
                url: '/admin/News/Delete',
                type: 'POST',
                data: { id: id },
                success: function (rs) {
                    if (rs.success) {
                        $('#trow_' + id).fadeTo('fast', 0.5, function () {
                            $(this).slideUp('fast', function () {
                                $(this).remove();
                            })

                        })
                        location.reload();
                    }
                }
            })
        }
    })


    $('body').on('click', '.btnActive', function (e) {
        e.preventDefault();
        var btn = $(this);
        var id = btn.attr("data-id");
        $.ajax({
            url: '/admin/News/IsActive',
            type: 'POST',
            data: { id: id },
            success: function (rs) {

                if (rs.success) {
                    if (rs.isActive) {
                        //btn.html("<i class='fa fa-check text-success'></i>")
                        btn.find("i").removeClass("fas fa-times text-danger");
                        btn.find("i").addClass("fa fa-check text-success");

                    }
                    else {
                        //btn.html("<i class='fas fa-times text-danger'></i>")
                        btn.find("i").removeClass("fa fa-check text-success");
                        btn.find("i").addClass("fas fa-times text-danger");

                    }
                }

            }
        })
    })
    $('body').on('change', '#SelectAll', function (e) {
        e.preventDefault();
        var checkStatus = this.checked;
        var checkbox = $(this).parents('.card-body').find('tr td input');
        checkbox.each(function () {
            this.checked = checkStatus;
            if (this.checked) {

                checkbox.attr('selected', 'checked');
            }
            else {
                checkbox.attr('selected', '');
            }
        })
    })
    $('body').on('click', '#btnDeleteAll', function (e) {
        e.preventDefault();
        var str = "";
        var checkbox = $(this).parents('.card').find('tr td input');
        var i = 0;
        checkbox.each(function () {

            if (this.checked) {
                var id = $(this).val();
                checkbox.attr('selected', 'checked');
                if (i === 0) {
                    str += id;
                }
                else {
                    str += "," + id;
                }
                i++;
            }
            else {
                checkbox.attr('selected', '');
            }
        })
        if (str.length > 0) {
            var conf = confirm('Bạn có muốn xóa các bản ghi này không?');
            if (conf === true) {
                $.ajax({
                    url: '/admin/News/DeleteAll',
                    type: 'POST',
                    data: { str: str },
                    success: function (rs) {
                        if (rs.success) {
                            location.reload();

                        }
                    }
                })
            }
        }
    })
    $('body').on('click', '.btbEdit', function (e) {
        var id = $(this).attr("data-id");
   
       GetDataById(id);

    })
})

function Validate() {
   
    var check = false;
    var tieude = $('#txtTitle').val();
    if (tieude == '') {
        $('#txtTitle').next().html('Bạn chưa nhập tên tin');
        check = false;
    }
    else {
        $('#txtTitle').next().html('');
        check = true;
    }
    var category = $('#dropDownlist').val();
    if (category == '') {
        $('#dropDownlist').next().html('Bạn chưa nhập tên tin');
        check = false;
    }
    else {
        $('#dropDownlist').next().html('');
        check = true;
    }
    return check;
}

function GetDataById(id) {
    $.ajax({
        url: '/Admin/News/EditNews?id='+ id,
        type: 'GET',
        data: {id:id},
        success: function (res) {
            debugger

            if (res.data != null) {
             
                $('#exampleId').val(res.data.id);
                $('#txtTitle').val(res.data.Title);
                $('#txtDescription').val(res.data.Description);
                $('#dropDownlist').val(res.data.Categoryid);
                CKEDITOR.instances['txtDetail'].getData(res.data.Detail);
                $('#txtImage').val(res.data.Image);
                $('#gender_male_checkbox').prop('checked');
                $('#txtSeoTitle').val(res.data.SeoTitle);
                $('#txtSeoDescription').val(res.data.SeoDescription);
                $('#txtSeoKeywords').val(res.data.SeoKeywords);
            }
        }
    })
}
function loadDSDM() {
    $.ajax({
        url: '/Admin/Category/GetCategory',
        type: 'GET',

        success: function (rs) {


            if (rs.TotalItems > 0) {
                var items = rs.data;
                var str = "";

                for (var i = 0; i < rs.data.length; i++) {
                    str += '<option value="' + items[i].Id + '">' + items[i].Title + '</option>'

                }
                $('#dropDownlist').html(str);
            }
        }
    })
}

function formatJsonDate(date) {
   
    var dateString = date.substr(6);
    var currentTime = new Date(parseInt(dateString));
    var month = currentTime.getMonth() + 1;
    var day = currentTime.getDate();
    var year = currentTime.getFullYear();
    if (month < 10) {
        month = '0' + month
    }
    if (day < 10) {
        day = '0' + day
    }
    return day + "/" + month + "/" + year;
}
function ResetInput() {
   
    $('#txtTitle').val('');
    $('#txtDescription').val('');
    $('#selectDanhMuc').empty('');
    CKEDITOR.instances['txtDetail'].setData('');
    $('#txtImage').val('');
    $('#gender_male_checkbox').prop('checked','');
    $('#txtSeoTitle').val('');
    $('#txtSeoDescription').val('');
    $('#txtSeoKeywords').val('');
}
function Pagination(CurrentPage, NumberPage, pageSize) {

    if (NumberPage > 0) {
        var str = "";
        str += '<nav aria-label="page navigation example">';
        str += '<ul class="pagination">';
        if (CurrentPage != 1) {
            str += '<li class="page-item"><a class="page-link" onclick="NextPage(' + (CurrentPage - 1) + ',' + pageSize + ')" >Previous</a></li>';
        }
        for (i = 1; i <= NumberPage; i++) {
            if (CurrentPage === i) {
                str += '<li class="page-item active"><a class="page-link">' + i + '</a></li>';
            }
            else {
                str += '<li class="page-item"><a class="page-link" onclick="NextPage(' + i + ',' + pageSize + ')" >' + i + '</a></li>';
            }


        }
        if (CurrentPage != NumberPage) {
            str += '<li class="page-item"><a class="page-link" onclick="NextPage(' + (CurrentPage + 1) + ',' + pageSize + ')">Next</a></li>';
        }
       

        str += '</nav>';
        str += '</ul>';
        $('#pagination').html(str);

    }
}
function NextPage(page, pageSize) {
    var txtSearch = $('#txtSearchBox').val();

    loadDataTinTuc(txtSearch, page, pageSize);
}
function loadDataTinTuc(_searchName, page, pageSize) {

    $.ajax({
        url: '/Admin/News/GetNews',
        type: 'GET',
        data: { searchName: _searchName, page: page, pageSize: pageSize },
        success: function (rs) {
            
            if (rs.TotalItems > 0) {
                var items = rs.data;
                var html = "";
                var ngaylayanh = "";
                var strCheck = "";
                for (var k = 0; k < rs.data.length; k++) {

                    strCheck = items[k].IsActive ? "<i class='fa fa-check text-success'></i>" : "<i class='fas fa-times text-danger'></i>";
                    if (items[k].CreatedDate != null) {
                        ngaylayanh = formatJsonDate(items[k].CreatedDate);
                    }
                    html += '<tr id="trow_' + items[k].Id + '">';
                    html += '<td><input type="checkbox" class="cbkItem" value="' + items[k].Id + '" /></td>';
                    html += '<td>' + (k + 1) + '</td>';
                    html += '<td><img src="' + items[k].Image + '" style="width:50px;" /></td>';
                    html += '<td>' + items[k].TenTinTuc + '</td>';
                    html += '<td>' + items[k].TenDanhMuc + '</td>';
                    html += '<td>' + ngaylayanh + '</td>';
                    html += '<td class="text-center"><a href = "#" data-id="' + items[k].Id + '" class="btnActive" >' + strCheck + '</a ></td>';
                    html += '<td><a href="/admin/news/edit/' + items[k].Id + '" data-id="' + items[k].Id + '" class="btn btn-sm btn-primary btbEdit">Sửa</a> <a href="#" data-id="' + items[k].Id + '" class="btn btn-sm btn-danger btnDelete">Xóa</a></td>';
                    html += '</tr>';
                }

            }
            else {
                html += '<tr>';
                html += '<td colspan="4">Không có bản ghi nào</td>';
                html += '</tr>';
            }
            $('#show_data').html(html);
            Pagination(rs.CurrentPage, rs.NumberPage)

        }
    })
}