
var currentPageNumber = 1;
var takeNumber = 3;

var List = function (pageNumber, itemsPerPage, section) {
    var url = "";
    var data = {};
    if (section == "groups") {
        url = LeonniUrl("ControlPanel/groupList");
        data = { groupName: $(".groupTitle").val(), createdBy: $("#createdBy").val(), "pageNumber": pageNumber, "itemsPerPage": itemsPerPage }
    }
    else if (section == "users") {
        url = LeonniUrl("ControlPanel/userList");
        data = { name: $('.name').val(), countryName: $(".CountrySearch").val(), provinceId: $(".ProvinceSearch").val(), disciplineId: $(".DisciplineSearch").val(), sex: $(".SexSearch").val(), categoryId: $(".CategorySearch").val(), sortOrder: $("#SortBy").val(), "pageNumber": pageNumber, "itemsPerPage": itemsPerPage }
    }
    else if (section == "projects") {
        url = LeonniUrl("ControlPanel/projectList");
        data = { name: $('.name').val(), countryName: $(".CountrySearch").val(), provinceId: $(".ProvinceSearch").val(), disciplineId: $(".DisciplineSearch").val(), sex: $(".SexSearch").val(), categoryId: $(".CategorySearch").val(), sortOrder: $("#SortBy").val(), "pageNumber": pageNumber, "itemsPerPage": itemsPerPage }
    }
    else if (section == "visitorsGroup") {
        url = LeonniUrl("Group/groupList");
        data = { name: $('.name').val(), countryName: $(".CountrySearch").val(), provinceId: $(".ProvinceSearch").val(), disciplineId: $(".DisciplineSearch").val(), sex: $(".SexSearch").val(), categoryId: $(".CategorySearch").val(), sortOrder: $("#SortBy").val(), "pageNumber": pageNumber, "itemsPerPage": itemsPerPage }
    }
    else if (section == "viewGroupContents") {
        url = LeonniUrl("Group/groupContentList");
         data = { id : 40 , "pageNumber": pageNumber, "itemsPerPage": itemsPerPage}
     }
     else if (section == "publications") {
         url = LeonniUrl("ControlPanel/publicationList");
         data = { name: $('.name').val(), countryName: $(".CountrySearch").val(), provinceId: $(".ProvinceSearch").val(), disciplineId: $(".DisciplineSearch").val(), sex: $(".SexSearch").val(), categoryId: $(".CategorySearch").val(), sortOrder: $("#SortBy").val(), "pageNumber": pageNumber, "itemsPerPage": itemsPerPage }
     }
     

    $.ajax({
        url: url,
        dataType: "html",
        data: data,
        type: "POST"
    }).done(function (result) {
        $("#user-list-partial").html(result);
        var pageitems = 0;
        var i = 0;
        var pagination = "<a href='javascript:void(0);' class = 'previous-user-list'>Previous </a>";
        var pages = "";
        while (pageitems < result.listcount) {
            pageitems += 3;
            i++;
            pages = pages + "<a href='javascript:void(0);' id='page-number-user-list-" + i + "' class='page-number-user-list'>'" + i + "'</a>";

        }
        pagination = pagination + pages + "<a href='javascript:void(0);' class ='next-user-list'>Next</a>";

        $("#pagination").html(pagination);
    });
}


$(".previous-user-list").live('click', function () {
    if (currentPageNumber > 1) {
        currentPageNumber -= 1;
    }
    groupList(currentPageNumber, takeNumber);
});

$(".next-user-list").live('click', function () {
    currentPageNumber += 1;
    groupList(currentPageNumber, takeNumber);
});

$(".page-number-user-list").live('click', function (e) {
    currentPageNumber = parseInt($(this).html());
    groupList(currentPageNumber, takeNumber);
});

$(".groupSearch").bind('click', function () {
    groupList(currentPageNumber, takeNumber);
});

function EditGroup(groupId) {

    $.ajax({
        url: LeonniUrl("/ControlPanel/AjaxGetGroupById"),
        type: "POST",
        data: "id=" + groupId,
        dataType: "json",
        success: function (response) {
            if (response.status == "success") {

                var provinceList = response.lstProvince;
                var provinceDropdown = $('.Provinces').first();
                if (provinceList.length > 0) {
                    provinceDropdown.empty();
                    for (var i = 0; i < provinceList.length; i++) {
                        provinceDropdown.append(new Option(provinceList[i].Name, provinceList[i].Id));
                    }
                }

                $("#submit_link").val("Update");
                $("#viewGroup").remove();
                $("#editMode").append("<div class='blue-btn update-btn' id='viewGroup'><a href='ViewGroup/" + groupId + "'>see the group </a></div>")

                $("#Id").val(response.group.Id);
                $("#CreatedBy").val(response.group.CreatedBy);
                $("#Title").val(response.group.Title);
                $("#DisciplineId").val(response.group.DisciplineId);
                $("#Summary").val(response.group.Summary);
                $("#CountryName").val(response.group.CountryName);
                $("#ProvinceId").val(response.group.ProvinceId);
                if (response.group.IsVisible) {
                    $("#IsVisible").attr('checked', 'checked');
                }
                else {
                    $("#IsVisible").removeAttr('checked');
                }

                if (response.group.IsCloseIncome) {
                    $("#IsCloseIncome").attr('checked', 'checked');
                }
                else {
                    $("#IsCloseIncome").removeAttr('checked');
                }
                if (response.group.AllowContents) {
                    $("#AllowContents").attr('checked', 'checked');
                }
                else {
                    $("#AllowContents").removeAttr('checked');
                }
                $("#IsNew").val('false');
            }
            else if (response.status == "error") {
                Leonni.ShowErrorMessage(response.errors);
            }
        }

    });

};

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
                    groupList(currentPageNumber, takeNumber);
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
                if (type != 'search') {
                    var disciplineDropdown = $(element).parents().find('.Disciplines').first();
                }
                else {
                    var disciplineDropdown = $(element).parents().find('.Disciplines').eq(1);
                }
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
                    groupList(currentPageNumber, takeNumber);
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
            url: LeonniUrl("ControlPanel/SearchFilters"),
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

//$("#searchhover").hover(function () {
//    alert(1);
//});

$(".").hover(
  function () {
      $(this).append($("<span> ***</span>"));
  },
  function () {
      $(this).find("span:last").remove();
  }
);

