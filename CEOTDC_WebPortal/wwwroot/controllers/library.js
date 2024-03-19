var $categoryIdEl = $('#input_category_id_library'),
    $pageEl = $('#input_page_library'),
    $recordEl = $('#input_record_library');

const dataParms = function () {
    return {
        c: $categoryIdEl.val(),
        page: parseInt($pageEl.val()),
        record: parseInt($recordEl.val())
    }
}

$(document).ready(function () {
    LoadListMainData();

    //List document image in detail page
    $('.list_document_image').slick({
        dots: false,
        infinite: true,
        speed: 300,
        slidesToShow: 3,
        slidesToScroll: 3,
        autoplay: true,
        autoplaySpeed: 3000,
        prevArrow: false,
        nextArrow: false,
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 3,
                    infinite: true,
                    prevArrow: false,
                    nextArrow: false,
                    dots: false

                }
            },
            {
                breakpoint: 600,
                settings: {
                    slidesToShow: 2,
                    prevArrow: false,
                    nextArrow: false,
                    slidesToScroll: 2
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1,
                    prevArrow: false,
                    nextArrow: false,
                    slidesToScroll: 1
                }
            }
        ]
    });
});

//Load list main data News
function LoadListMainData(id) {
    var data = {
        id: id
    };
    try {
        ShowOverlay("#div_main_data_library");
        var data = dataParms();
        $.ajax({
            type: 'GET',
            url: '/Library/GetList',
            data: data,
            dataType: "json",
            success: function (response) {
                HideOverlay("#div_main_data_library");
                if (response.result !== 1) {
                    document.getElementById("div_main_data_library").innerHTML =
                        `<div class="text-center p-2">
                            <h4>Kết nối không ổn định</h4>
                            <button type="button" class="btn btn-primary" 
                                style="width:200px;height:45px;border-radius:4px;" 
                                onclick="LoadListMainData();$(this).parent().remove();">Tải lại
                            </button>
                        </div>`;
                    return false;
                }

                var listData = response.data;
                if (listData == null || listData.length === 0) {
                    document.getElementById("div_main_data_library").innerHTML =
                        `<div class="d-flex align-items-center flex-column" >
                            <img class="w-auto d-flex m-auto" src="./assets/images/page-404-image.png" style="width:200px; height:200px;"/>
                            <span class="text-muted">Không có dữ liệu!</span>
                        </div>`;
                    return false;
                }

                //Load list data
                var html = '';
                let link = '';
                listData.forEach(function (item) {
                    link = `/thu-vien/${item.nameSlug}-${item.id}`;
                    html +=
                        `<div class="col-12 col-md-4 col-lg-4 grid-item">
                            <div class="blog-item-02 aos-init aos-animate library_card">
                                <div class="blog-item-02__image">
                                    <a href="${link}"><img class="img_custom_respon" src="${item.imageObj?.mediumUrl ?? '/assets/images/error/avatar.png'}" alt="${item.title ?? ''}" width="370" height="201"></a>
                                </div>
                                <div class="blog-item-02__content">
                                    <span class="course-info__badge-text badge-all mb-1">${item.documentSubjectObj?.name ?? ''}</span>
                                    <h3 class="blog-item-02__title custom_title"><a  title="${item.name ?? ''}" href="${link}">${item.name ?? ''}</a></h3>
                                  <blockquote class="custom_description blockquote mw-100 fs-6 m-0 mb-2" style="color:grey; padding-left:7px; line-height:1.3">
                                   <p class="mt-0" style="font-size: 13px;">${item.summaryDetail ?? ''}</p>
                                 </blockquote>
                                    <a class="blog-item-02__more btn btn-light btn-hover-white mt-1" href="${link}">Xem thêm <i class="fal fa-long-arrow-right"></i></a>
                                </div>
                            </div>
                        </div>`;
                });
                $('#div_main_data_library').html(html);

                //Pagination
                var totalRecord = parseInt(response.data2nd);
                var currentPage = parseInt(data.page);
                var pageSize = parseInt(data.record);
                var pagination = LoadPagination(totalRecord, pageSize, currentPage);
                $('#ul_main_pagination_library').html(pagination);
            },
            error: function (error) {
                HideOverlay("#div_main_data_library");
                document.getElementById("div_main_data_library").innerHTML =
                    `<div class="text-center p-2">
                        <i type="button" class="fa fa-refresh" 
                            style="border-radius:4px;font-size:24px;" 
                            onclick="LoadListMainData();$(this).parent().remove();">
                        </i>
                    </div>`;
            }
        });
    } catch (e) {
        HideOverlay("#div_main_data_library");
        document.getElementById("div_main_data_library").innerHTML = `
                    <div class="text-center p-2">
                        <i type="button" class="fa fa-refresh" 
                            style="border-radius:4px;font-size:24px;" 
                            onclick="LoadListMainData();$(this).parent().remove();">
                        </i>
                    </div>`;
    }
}

//When click pagination 
function ChangePageNews(page, e, elm) {
    e.preventDefault();
    ScrollToTop('#div_main_data_library', 70, 500);
    $pageEl.val(page);

    //Change Link
    var newHref = $(elm).attr("href");
    ChangeURLWithOut("", newHref);

    //Get List
    LoadListMainData();
}

//Load Pagination Page
function LoadPagination(totalRecords, pageSize = 10, currentPage = 1) {
    var pageDisplay = 5;
    var totalPage = Math.ceil(totalRecords / pageSize);
    if (currentPage > totalPage) //Check currentPage error
        currentPage = totalPage;
    if (currentPage < 1)
        currentPage = 1;
    var startPage = parseInt(Math.max(1, Math.ceil(currentPage - pageDisplay / 2)));
    var endPage = parseInt(Math.min(totalPage, currentPage + pageDisplay / 2));

    if (totalPage >= 1) {
        var html = '';
        let link = GetLink();
        if (currentPage > 1)
            html += `<li><a href="${link}record=${pageSize}&page=${currentPage - 1}" title="Trang trước" onclick="ChangePageNews(${currentPage - 1},event,this)"><i class="far fa-angle-double-left"></i></a></li>`;
        for (var i = startPage; i <= endPage; i++) {
            if (currentPage == i)
                html += `<li><a class="active" href="javascript:void(0);">${currentPage}</a></li>`;
            else
                html += `<li><a href="${link}record=${pageSize}&page=${i}" onclick="ChangePageNews(${i},event,this)" title="Trang ${i}">${i}</a></li>`;
        }
        if (currentPage < totalPage)
            html += `<li><a href="${link}record=${pageSize}&page=${currentPage + 1}" title="Trang kế tiếp" onclick="ChangePageNews(${currentPage + 1},event,this)"><i class="far fa-angle-double-right"></i></a></li>`;
        return html;
    }
    else //NoData
        return "";
}

//Get Url Link
function GetLink() {
    var str = window.location.search.substring(1);
    var res = str.split("&");
    var test = res.splice(-2, 2);
    var link = "";
    if (test[0].indexOf("record") > -1 && test[1].indexOf("page") > -1) {
        res.forEach(function (item, index) {
            link += item + "&";
        });
    }
    else {
        if (str == "") {
            link = str;
        }
        else {
            link = str + "&";
        }
    }
    return window.location.pathname + "?" + link;
}

