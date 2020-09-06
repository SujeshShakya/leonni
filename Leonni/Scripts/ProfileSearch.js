

var currentPageNumber = 1;
var takeNumber = 100;

var userList = function (pageNumber, itemsPerPage) {
    $.ajax({
        url: LeonniUrl("ControlPanel/UserList"),
        dataType: "html",
        data: { name: $('.name').val(), countryName: $(".CountrySearch").val(), provinceId: $(".ProvinceSearch").val(), disciplineId: $(".DisciplineSearch").val(), sex: $(".SexSearch").val(), categoryId: $(".CategorySearch").val(), sortOrder: $("#SortBy").val(), "pageNumber": pageNumber, "itemsPerPage": itemsPerPage },
        type: "POST"
    }).done(function (result) {
        $("#user-list-partial").html(result);
        var pageitems = 0;
        var i = 0;
        var pagination = "<a href='javascript:void(0);' class = 'previous-user-list'>Previous </a>";
        var pages = "";
        while (pageitems < result.listcount) {
            pageitems += 100;
            i++;
            pages = pages + "<a href='javascript:void(0);' id='page-number-user-list-" + i + "' class='page-number-user-list'>'" + i + "'</a>";

        }
        pagination = pagination + pages + "<a href='javascript:void(0);' class ='next-user-list'>Next</a>";

        $("#pagination").html(pagination);
    });
}

if ($("#user-list-partial").length > 0) {
    userList(currentPageNumber, takeNumber);
}

$(".previous-user-list").live('click', function () {
    if (currentPageNumber > 1) {
        currentPageNumber -= 1;
    }
    userList(currentPageNumber, takeNumber);
});

$(".next-user-list").live('click', function () {
    currentPageNumber += 1;
    userList(currentPageNumber, takeNumber);
});

$(".page-number-user-list").live('click', function (e) {
    currentPageNumber = parseInt($(this).html());
    userList(currentPageNumber, takeNumber);
});

$("#SortBy").bind('change', function () {
    userList(currentPageNumber, takeNumber);
});

//    $(".CategorySearch").bind('change', function () {

//        userList(currentPageNumber, takeNumber);
//    });

$(".SexSearch").bind('change', function () {
    userList(currentPageNumber, takeNumber);
});

$(".DisciplineSearch").bind('change', function () {
    userList(currentPageNumber, takeNumber);
});


$(".ProvinceSearch").bind('change', function () {
    userList(currentPageNumber, takeNumber);
});

$(".NameSearch").bind('click', function () {
    userList(currentPageNumber, takeNumber);
});


function GetProvinces(element, type) {
    countryname = element.value;
    $.ajax({
        url: LeonniUrl("/Front/AjaxGetProvinces"),
        type: "POST",
        data: "country=" + countryname,
        dataType: "json",
        success: function (response) {
            if (response.status == "success") {
                var provinceList = response.provinceList;
                if (type != 'search') {
                    var provinceDropdown = $(element).parents().find('.Provinces').first();
                }
                else {
                    var provinceDropdown = $(element).parents().find('.Provinces').first();
                }
                if (provinceList.length > 0) {
                    provinceDropdown.empty();
                    for (var i = 0; i < provinceList.length; i++) {
                        provinceDropdown.append(new Option(provinceList[i].Name, provinceList[i].Id));
                    }
                }
                else {
                    provinceDropdown.empty();
                    provinceDropdown.append(new Option("State", "0"));
                }
                if (type == 'Search') {
                    userList(currentPageNumber, takeNumber);
                }
            }
            else if (response.status == "error") {
                Leonni.ShowErrorMessage(response.errors);
            }
        }

    });
}

function GetDisciplines(element, type) {

    Id = element.value;
    $.ajax({
        url: LeonniUrl("/Front/AjaxGetDisciplines"),
        type: "POST",
        data: "id=" + Id,
        dataType: "json",
        success: function (response) {
            if (response.status == "success") {
                var disciplineList = response.disciplineList;

                var disciplineDropdown = $('.Disciplines');

                if (disciplineList.length > 0) {
                    disciplineDropdown.empty();
                    for (var i = 0; i < disciplineList.length; i++) {
                        disciplineDropdown.append(new Option(disciplineList[i].DisciplineName, disciplineList[i].Id));
                    }
                }
                else {
                    disciplineDropdown.empty();
                    disciplineDropdown.append(new Option("Discipline", "0"));
                }
                if (type == 'search') {
                    userList(currentPageNumber, takeNumber);
                }
            }
            else if (response.status == "error") {
                Leonni.ShowErrorMessage(response.errors);
            }
        }
    });
}


$("#showFilters").live("click", function () {
    var str = $("#showFilters").html();
    if (str.replace(/^\s+|\s+$/g, '') == "Apply Filters") {
        $.ajax({
            url: LeonniUrl("User/SearchFilters"),
            dataType: "html",
            //data: { name: $('.name').val(), countryName: $(".CountrySearch").val(), provinceId: $(".ProvinceSearch").val(), disciplineId: $(".DisciplineSearch").val(), sex: $(".SexSearch").val(), categoryId: $(".CategorySearch").val(), sortOrder: $("#SortBy").val(), "pageNumber": pageNumber, "itemsPerPage": itemsPerPage },
            type: "POST"
        }).done(function (result) {
            $("#searchfilters-partial").html(result);
            $("#showFilters").html('Hide Filters');
        });
    }
    else {
        $("#searchfilters-partial").html('');
        $("#showFilters").html('Apply Filters');
    }
});