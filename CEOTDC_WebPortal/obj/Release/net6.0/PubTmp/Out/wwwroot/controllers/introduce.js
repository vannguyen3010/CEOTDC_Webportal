var $categoryIdEl = $('#input_category_id_introduce'),
    $pageEl = $('#input_page_introduce'),
    $recordEl = $('#input_record_introduce');

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

    //Button click tab organizational composition
    $('')
});

//Load list main data News
function LoadListMainData() {
    try {
        ShowOverlay("#div_main_data_introduce");
        var data = dataParms();
        $.ajax({
            type: 'GET',
            url: '/Introduce/GetList',
            data: data,
            dataType: "json",
            success: function (response) {
                HideOverlay("#div_main_data_introduce");
                if (response.result !== 1) {
                    document.getElementById("div_main_data_introduce").innerHTML =
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
                    document.getElementById("div_main_data_introduce").innerHTML =
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
                    link = `/gioi-thieu/${item.titleSlug}-${item.id}`;
                    html +=
                        `<div class="blog-list-item-02 news_card" data-aos="fade-up" data-aos-duration="1000">
                            <div class="blog-list-item-02__image">
                                <a href="${link}"><img src="${item.imageObj?.mediumUrl ?? '/assets/images/error/avatar.png'}" alt="${item.title ?? ''}" width="270" height="170"></a>
                                <div class="blog-list-item__categories">
                                    <a href="/gioi-thieu?c=${item.categoryId}">${item.categoryObj?.name ?? ''}</a>
                                </div>
                            </div>
                            <div class="blosg-list-item-02__content" style="padding-left:10px;  ">
                                <h3 class="blog-list-item-02__title"><a title="${item.title ?? ""}" href="${link}">${item.title ?? ''}</a></h3>
                                <p class="blog-list-item-02__title" style="font-size: 14px;">${item.description ?? ''}</p>
                                <a class="blog-list-item-02__more" href="${link}">Xem <i class="fal fa-long-arrow-right"></i></a>
                            </div>
                        </div>`;
                });
                $('#div_main_data_introduce').html(html);

                //Pagination
                var totalRecord = parseInt(response.data2nd);
                var currentPage = parseInt(data.page);
                var pageSize = parseInt(data.record);
                var pagination = LoadPagination(totalRecord, pageSize, currentPage);
                $('#ul_main_pagination_introduce').html(pagination);
            },
            error: function (error) {
                HideOverlay("#div_main_data_introduce");
                document.getElementById("div_main_data_introduce").innerHTML =
                    `<div class="text-center p-2">
                        <i type="button" class="fa fa-refresh" 
                            style="border-radius:4px;font-size:24px;" 
                            onclick="LoadListMainData();$(this).parent().remove();">
                        </i>
                    </div>`;
            }
        });
    } catch (e) {
        HideOverlay("#div_main_data_introduce");
        document.getElementById("div_main_data_introduce").innerHTML = `
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
    ScrollToTop('#div_main_data_introduce', 70, 500);
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

//Get List Member
function GetListMember(id) {
    var dataIdListMember = {
        id: id,
    };
    try {
        $.ajax({
            type: 'GET',
            data: dataIdListMember,
            url: '/Library/OrganizationalChart',
            dataType: "json",
            success: function (response) {
                //Check Error code
                if (response.result !== 1) {
                    document.getElementById("").innerHTML = ` 
                    <div class="text-center p-2">
                        <h4>Kết nối không ổn định</h4>
                        <button type="button" class="btn btn-primary" 
                            style="width:200px;height:45px;border-radius:4px;" 
                            onclick="LoadListMainData();$(this).parent().remove();">Tải lại
                        </button>
                    </div>`;
                    return;
                }

                var listData = response.data;
                var listMemberHtml = ''; let link = '';
                if (listData != null && listData.length > 0) {
                    $.each(listData, function (key, value) {
                        link = `/hoi-vien/${value.supplierObj.nameSlug}-${value.id}`;
                        listMemberHtml += `
                         <div class="col-xl-3 col-lg-4 col-sm-6">
                            <div class="instructor-item aos-init aos-animate" data-aos-duration="1000" data-aos="fade-up">
                                <div class="instructor-item__link">
                                    <div class="instructor-item__header">
                                        <div class="instructor-item__image">
                                        ${!IsNullOrEmty(value.supplierObj?.imageObj) ? `<img src="${value.supplierObj?.imageObj.smallUrl}" alt="Instructor" width="140" height="140">` : `<img src="/assets/images/error/avatar.png" alt="Instructor" width="140" height="140">`}
                                          
                                        </div>
                                        <div class="instructor-item__content" >
                                            <h4 /*href="${link}"*/ style="cursor:pointer" onclick="ToggleModalDetail(${value.id})" class="instructor-item__name fs-6 name_company">${value.supplierObj?.name ?? ''}</h4>

                                            ${!IsNullOrEmty(value.supplierObj?.description) ? `<p class="instructor-item__designation m-0 descripton_company">${value.supplierObj?.description}</p>` : ''}
                                            <div class="instructor-item__rating d-flex justify-content-evenly">
                                                ${!IsNullOrEmty(value.supplierObj?.faceBook) ? `<a href="${value.supplierObj?.faceBook}" target="_blank" rel="noreferrer"><i class="fab fa-facebook-f"></i></a>` : ''}
                                                ${!IsNullOrEmty(value.supplierObj?.twitter) ? `<a href="${value.supplierObj?.twitter}" target="_blank" rel="noreferrer"><i class="fab fa-twitter"></i></a>` : ''}
                                                ${!IsNullOrEmty(value.supplierObj?.instagram) ? `<a href="${value.supplierObj?.instagram}" target="_blank" rel="noreferrer"><i class="fab fa-instagram"></i></a>` : ''}
                                                ${!IsNullOrEmty(value.supplierObj?.zalo) ? `<a href="${value.supplierObj?.zalo}" target="_blank" rel="noreferrer" style="width:17px; top:-2px;"><img src="/assets/images/icon_zalo.png" style="object-fit:cover; margin-top: -3px;" />` : ''}
                                                ${!IsNullOrEmty(value.supplierObj?.youtube) ? `<a href="${value.supplierObj?.youtube}" target="_blank" rel="noreferrer"><i class="fab fa-youtube"></i></a>` : ''}
                                                ${!IsNullOrEmty(value.supplierObj?.tiktokUrl) ? `<a href="${value.supplierObj?.tiktokUrl}" target="_blank" rel="noreferrer"><i class="fab fa-tiktok"></i></a>` : ''}
                                                ${!IsNullOrEmty(value.supplierObj?.hotlineNumber) ? `<a href="tel:${value.supplierObj?.hotlineNumber}" target="_blank" rel="noreferrer"><i class="fas fa-phone"></i></a>` : ''}
                                                ${!IsNullOrEmty(value.supplierObj?.email) ? `<a href="mailto:${value.supplierObj?.email}" target="_blank" rel="noreferrer"><i class="fas fa-envelope"></i></a>` : ''}
                                            </div>
                                        </div>
                                    </div>
                                    <div class="instructor-item__footer" style="background-color:#031f42; border-bottom-left-radius:8px;border-bottom-right-radius:8px" data-toggle="tooltip" data-placement="top" title="Tooltip on top">
                                        <a class="instructor-item__meta mx-auto text-white" data-placement="top" data-toggle="tooltip" title="Ngành nghề: ${value.majorObj?.name}"><i class="fas fa-chalkboard-teacher"></i><span class="value">${value.majorObj?.name}</span></a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        `;
                    });
                    document.getElementById("list_member").innerHTML = listMemberHtml;
                    document.getElementById("count_member").innerHTML = listData.length;
                }

                //Pagination
                var totalRecord = parseInt(response.data2nd);
                var currentPage = parseInt(data.page);
                var pageSize = parseInt(data.record);

                var pagination = LoadPagination(totalRecord, pageSize, currentPage);
                $('#ul_main_pagination_member').html(pagination);

            },
            error: function (error) {
                HideOverlay("#ul_main_pagination_member");
                 document.getElementById("").innerHTML = ` 
                     <div class="text-center p-2">
                         <h4>Kết nối không ổn định!</h4>
                         <button type="button" class="btn btn-primary" 
                             style="width:200px;height:45px;border-radius:4px;" 
                             onclick="GetListMember(id);$(this).parent().remove();">Tải lại
                         </button>
                     </div>`;
                 console.log("Error when load product!");
            }
        });
    } catch (e) {
        document.getElementById("ul_main_pagination_member").innerHTML = ` 
                    <div class="text-center p-2">
                        <h4>Kết nối không ổn định!</h4>
                        <button type="button" class="btn btn-primary" 
                            style="width:200px;height:45px;border-radius:4px;" 
                            onclick="GetListMember(id);$(this).parent().remove();">Tải lại
                        </button>
                    </div>`;
        console.log("Error when load product!");
    }
}

function ToggleModalDetail(id) {
    $.ajax({
        type: 'GET',
        data: {
            id: id
        },
        url: '/Introduce/DetailPerson',
        dataType: "json",
        success: function (response) {
            //Check Error code
            if (response.result !== 1) {
                document.getElementById("").innerHTML = ` 
                    <div class="text-center p-2">
                        <h4>Kết nối không ổn định</h4>
                        <button type="button" class="btn btn-primary" 
                            style="width:200px;height:45px;border-radius:4px;" 
                            onclick="LoadListMainData();$(this).parent().remove();">Tải lại
                        </button>
                    </div>`;
                return;
            }

            var MemberData = response.data;
            var memberHtml = '';
            var personHtml = '';
            console.log(MemberData);

            if (MemberData != null) {
            personHtml = `
             <div class="col-12 col-md-6 col-lg-6 col-xl-4 mb-3">
                    <div class="instructor-item aos-init aos-animate" data-aos="fade-up" data-aos-duration="1000">
                        <div class="instructor-item__link" href="#">
                            <div class="instructor-item__header">
                                <div class="instructor-item__image">
                                    <img src="${MemberData.imageObj?.mediumUrl ?? '/assets/images/error/avatar.png'}" alt="${MemberData.imageObj?.name}" width="140" height="140">
                                </div>
                                <div class="instructor-item__content">
                                    <h4 class="instructor-item__name">${MemberData.lastName} ${MemberData.firstName}</h4>
                                    <p class="instructor-item__designation">${!IsNullOrEmty(MemberData.positionObj?.name) ? `Chức vụ: ${MemberData.positionObj?.name}` : ``}</p>
                                </div>
                            </div>
                         ${!IsNullOrEmty(MemberData.email) ? ` <div class="instructor-item__footer">
                                <div class="instructor-item__meta mx-auto">
                                  <i class="far fa-envelope"></i><a href="mailto:${MemberData.email}" target="_blank">${MemberData.email}</a>
                                </div>
                            </div>`: ``}
                        </div>
                    </div>
                </div>`
            }

            if (MemberData != null) {
                memberHtml = `
            <div class="modal-header">
                <h5 class="modal-title mb-0" id="exampleModalLabel">Thông tin thành viên</h5>
            </div>
            <div class="instructor-item aos-init aos-animate" data-aos="fade-up" data-aos-duration="1000">
            </div>
            <div class="row pt-5 mb-5 bg-color-02" style="border-top: 1px solid #dee2e6;">
                <h3 class="mt-1"></h3>
                <div class="course-list-item">
                    <div class="course-list-header">
                        <div class="course-list-header__thumbnail">
                            <a href="javacript:void(0)"><img src="${MemberData.supplierObj?.imageObj?.mediumUrl ?? '/assets/images/error/avatar.png'}" alt="courses" width="270" height="181"></a>
                        </div>
                    </div>
                    <div class="course-list-info course_list_info_custom" style="padding-left:50px">
                        <h3 class="course-list-info__title"><a href="javacript:void(0)">${MemberData.supplierObj?.name}</a></h3>
                        <div class="course-list-info__description">
                            <p>
                              ${!IsNullOrEmty(MemberData.supplierObj?.description) ? MemberData.supplierObj?.description : ''}
                            </p>
                        </div>
                       <div class="instructor-item__rating d-flex justify-content-start">
                                                ${!IsNullOrEmty(MemberData.supplierObj?.faceBook) ? `<a href="${MemberData.supplierObj?.faceBook}" target="_blank" rel="noreferrer"><i class="fab fa-facebook-f p-2"></i></a>` : ''}
                                                ${!IsNullOrEmty(MemberData.supplierObj?.twitter) ? `<a href="${MemberData.supplierObj?.twitter}" target="_blank" rel="noreferrer"><i class="fab fa-twitter p-2"></i></a>` : ''}
                                                ${!IsNullOrEmty(MemberData.supplierObj?.instagram) ? `<a href="${MemberData.supplierObj?.instagram}" target="_blank" rel="noreferrer"><i class="fab fa-instagram p-2"></i></a>` : ''}
                                                ${!IsNullOrEmty(MemberData.supplierObj?.zalo) ? `<a href="${MemberData.supplierObj?.zalo}" target="_blank" rel="noreferrer" style="width:17px; top:-2px;"><img src="/assets/images/icon_zalo.png" style="object-fit:cover; margin-top: -3px;" class="p-2" />` : ''}
                                                ${!IsNullOrEmty(MemberData.supplierObj?.youtube) ? `<a href="${MemberData.supplierObj?.youtube}" target="_blank" rel="noreferrer"><i class="fab fa-youtube p-2"></i></a>` : ''}
                                                ${!IsNullOrEmty(MemberData.supplierObj?.tiktokUrl) ? `<a href="${MemberData.supplierObj?.tiktokUrl}" target="_blank" rel="noreferrer"><i class="fab fa-tiktok p-2"></i></a>` : ''}
                                                ${!IsNullOrEmty(MemberData.supplierObj?.hotlineNumber) ? `<a href="tel:${MemberData.supplierObj?.hotlineNumber}" target="_blank" rel="noreferrer"><i class="fas fa-phone p-2"></i></a>` : ''}
                                                ${!IsNullOrEmty(MemberData.supplierObj?.email) ? `<a href="mailto:${MemberData.supplierObj?.email}" target="_blank" rel="noreferrer"><i class="fas fa-envelope p-2"></i></a>` : ''}
                       </div>
                        <div class="course-list-info__action mt-0">
                            ${!IsNullOrEmty(MemberData.supplierObj?.hotlineNumber) ? `<a href="tel:${MemberData.supplierObj?.hotlineNumber}" class="btn btn-primary btn-hover-secondary">Liên hệ ngay</a>` : ``}
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
            ${personHtml}
            </div>
            <div class="modal-footer">
                <button onclick="CloseModalDetailPerson()" type="button" class="btn btn-secondary" data-dismiss="modal">Đóng</button>
            </div>`;
            }
            document.getElementById("detail_person").innerHTML = memberHtml;
            $('#modal_person_detail').modal('show')
        }
    });
}

function CloseModalDetailPerson() {
    $('#modal_person_detail').modal("hide");
}