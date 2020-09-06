var id = 0;
function EditProfile(profileId) {
    id = profileId;
    $.ajax({
        url: LeonniUrl("/ControlPanel/AjaxGetProfileById"),
        type: "POST",
        data: "id=" + profileId,
        dataType: "json",
        success: function (response) {
            if (response.status == "success") {
                var disciplineDropdown = $('.Disciplines').first();
                var disciplineList = response.lstDiscipline;
                if (disciplineList.length > 0) {
                    disciplineDropdown.empty();
                    for (var i = 0; i < disciplineList.length; i++) {
                        disciplineDropdown.append(new Option(disciplineList[i].DisciplineName, disciplineList[i].Id));
                    }
                }

                var provinceList = response.lstProvince;
                var provinceDropdown = $('.Provinces').first();
                if (provinceList.length > 0) {
                    provinceDropdown.empty();
                    for (var i = 0; i < provinceList.length; i++) {
                        provinceDropdown.append(new Option(provinceList[i].Name, provinceList[i].Id));
                    }
                }
                
                $(".update-btn").show();
                $("#FirstName").val(response.profile.FirstName);
                $("#SurName").val(response.profile.SurName);
                var date = new Date(response.profile.Date);
                $("#BirthDate").val((date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear());
                $("#Id").val(response.profile.Id);
                $("#DisciplineId").val(response.profile.DisciplineId);
                $("#CategoryId").val(response.profile.CategoryId);
                $("#Summary").val(response.profile.Summary);
                $("#CountryName").val(response.profile.CountryName);
                $("#ProvinceId").val(response.profile.ProvinceId);
                $("#Address").val(response.profile.Address);
                $("#Occupation").val(response.profile.Occupation);
                $("#Company").val(response.profile.Company);
                $("#Phone").val(response.profile.Phone);
                $("#Locality").val(response.profile.Locality);
                $("#WebLink").val(response.profile.WebLink);
                $("#ZipCode").val(response.profile.ZipCode);
                $("#Description").val(response.profile.Description);
                $("#Sex").val(response.profile.Sex);
                $("#IsNew").val('false');
            }
            else if (response.status == "error") {
                Leonni.ShowErrorMessage(response.errors);
            }
        }

    });

};

function ViewProfile() {
    window.location.href = LeonniUrl("ControlPanel/ViewProfile/" + id);
}