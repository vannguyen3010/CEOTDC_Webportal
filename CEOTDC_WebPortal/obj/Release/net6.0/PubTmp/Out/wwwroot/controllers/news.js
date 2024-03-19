var $categoryIdEl = $('#input_category_id'),
    $pageEl = $('#input_page'),
    $recordEl = $('#input_record');

const dataParms = function () {
    return {
        c: $categoryIdEl.val(),
        page: parseInt($pageEl.val()),
        record: parseInt($recordEl.val())
    }
}

$(document).ready(function () {
    LoadListMainData();

    //Slider news related
    $('.list_news_related').slick({
        dots: true,
        infinite: false,
        speed: 300,
        slidesToShow: 4,
        autoplay: true,
        autoplaySpeed: 2000,
        prevArrow: false,
        nextArrow: false,
        dots: false,
        slidesToScroll: 4,
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 3,
                    infinite: true,
                    dots: true
                }
            },
            {
                breakpoint: 600,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 2
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }
        ]
    });

});

//Load list main data News
function LoadListMainData() {
    try {
        ShowOverlay("#div_main_data");
        var data = dataParms();
        $.ajax({
            type: 'GET',
            url: '/News/GetList',
            data: data,
            dataType: "json",
            success: function (response) {
                HideOverlay("#div_main_data");
                if (response.result !== 1) {
                    document.getElementById("div_main_data").innerHTML =
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
                    document.getElementById("div_main_data").innerHTML =
                        `<div class="d-flex align-items-center flex-column" >
                            <img class="w-auto d-flex m-auto" src="./assets/images/page-404-image.png" style="width:200px; height:200px;"/>
                            <span class="text-muted">Không có dữ liệu!</span>
                        </div >`;
                    return false;
                }

                //Load list data
                var html = '';
                let link = '';
                listData.forEach(function (item) {
                    link = `/tin-tuc/${item.titleSlug}-${item.id}`;
                    html +=
                        `<div class="blog-list-item-02 news_card" data-aos="fade-up" data-aos-duration="1000">
                            <div class="blog-list-item-02__image">
                                <a href="${link}"><img src="${item.imageObj?.mediumUrl ?? '/assets/images/error/avatar.png'}" alt="${item.title ?? ''}" width="270" height="170"></a>
                                <div class="blog-list-item__categories">
                                    <a href="/tin-tuc?c=${item.categoryId}">${item.categoryObj?.name ?? ''}</a>
                                </div>
                            </div>
                            <div class="blosg-list-item-02__content" style="padding-left:10px;  ">
                                <h3 class="blog-list-item-02__title"><a href="${link}">${item.title ?? ''}</a></h3>
                                <div class="blog-list-item-02__meta">
                                    <span class="meta-action"><i class="far fa-calendar"></i> ${IsNullOrEmty(item.publishedAt) ? '' : moment(item.publishedAt).format('DD-MM-YYYY')}</span>
                                </div>
                                <p class="blog-list-item-02__title" style="font-size: 14px;">${item.description ?? ''}</p>
                                <a class="blog-list-item-02__more" href="${link}">Xem <i class="fal fa-long-arrow-right"></i></a>
                            </div>
                        </div>`;
                });
                $('#div_main_data').html(html);

                //Pagination
                var totalRecord = parseInt(response.data2nd);
                var currentPage = parseInt(data.page);
                var pageSize = parseInt(data.record);
                var pagination = LoadPagination(totalRecord, pageSize, currentPage);
                $('#ul_main_pagination').html(pagination);
            },
            error: function (error) {
                HideOverlay("#div_main_data");
                document.getElementById("div_main_data").innerHTML =
                    `<div class="text-center p-2">
                        <i type="button" class="fa fa-refresh" 
                            style="border-radius:4px;font-size:24px;" 
                            onclick="LoadListMainData();$(this).parent().remove();">
                        </i>
                    </div>`;
            }
        });
    } catch (e) {
        HideOverlay("#div_main_data");
        document.getElementById("div_main_data").innerHTML = `
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
    ScrollToTop('#div_main_data', 70, 500);
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

