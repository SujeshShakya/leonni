//var id = 0;
//$(".search-result").hover(
//    function () {
//    alert(1);
//        id = $(this).attr('id');
//        $(this).append("<span  class='options'><img src='../../Images/sp_remove-btn.jpg' alt='down'/></span>")
//    },
//    function () {
//        $(this).find("span:last").remove();
//        $(this).find("ul:last").remove();
//    }
//    );

//    $(".options").live('click', function () {
//        $("<div><ul><li onclick='MakePublic(" + id + ")'>Make Public</li><li onclick='DeleteContent("+ id +")'>Delete</li></ul></div>").insertAfter(this);
//        $(this).die('click');
//    });

//function MakePublic(id) {
//    $.ajax({
//        url: LeonniUrl("/ControlPanel/MakePublic"),
//        type: "POST",
//        data: { id: id, sectionId: 1 },
//        dataType: "json",
//        success: function (response) {
//            if (response.status == "success") {

//            }
//            else if (response.status == "error") {
//                Leonni.ShowErrorMessage(response.errors);
//            }
//        }

//    });
//}

//function DeleteContent(id) {

//    if(confirm("Are you sure to Delete?")) { 
//         $.ajax({
//        url: LeonniUrl("/ControlPanel/DeleteContent"),
//        type: "POST",
//        data: { id: id, sectionId: 1 },
//        dataType: "json",
//        success: function (response) {
//            if (response.status == "success") {

//            }
//            else if (response.status == "error") {
//                Leonni.ShowErrorMessage(response.errors);
//            }
//        }

//    });
//}
//    }
//}

$('.search-result').live('hover', function (event) {
    if (event.type == 'mouseenter') {
        // first function here
        id = $(this).attr('id');
        $(this).find(".options").show();
    } else {
        // second function here
        $(this).parent().find('.adminOptions').hide();
        $(this).find(".options").hide();
    }
});

$(".options").live('click', function () {
    var container = $(this).parent().find('.adminOptions');
    //alert(container.attr('style'));
    if (container.attr('style') == 'display: none;' || container.attr('style') == 'undefined') {
        $(this).parent().find('.adminOptions').show();
    }
    else if (container.attr('style') == 'display: block;') {
        $(this).parent().find('.adminOptions').hide();
    }
    else {
        $(this).parent().find('.adminOptions').show();
    }
});
function AddRemovePublic(id, sectionId) {
    $.ajax({
        url: LeonniUrl("/ControlPanel/AddRemovePublic"),
        type: "POST",
        data: { id: id, sectionId: sectionId },
        dataType: "json",
        success: function (response) {
            if (response.status == "success") {
                if (response.message == "added") {
                    $("#Public" + id).html("Remove Public");
                }
                else {
                    $("#Public" + id).html("Make Public");
                }
            }
            else if (response.status == "error") {
                Leonni.ShowErrorMessage(response.errors);
            }
        }

    });
}

function DeleteContent(id, e, sectionId) {
    var url = "";
    if (sectionId == 1) ///users
    {
        url = LeonniUrl("/ControlPanel/RemoveUser");
    }
    else if (sectionId == 2) {
        url = LeonniUrl("/ControlPanel/RemoveGroup")
    }
    if (confirm("Are you sure to Delete?")) {
        $.ajax({
            url: url,
            type: "POST",
            data: { id: id },
            dataType: "json",
            success: function (response) {
                if (response.status == "success") {
                    $(e).parents().find('#' + id).remove();
                }
                else if (response.status == "error") {
                    Leonni.ShowErrorMessage(response.errors);
                }
            }

        });
    }

}

function DisableContent(id, e) {

    if (confirm("Are you sure to Disable?")) {
        $.ajax({
            url: LeonniUrl("/ControlPanel/DisableContent"),
            type: "POST",
            data: { id: id, sectionId: 1 },
            dataType: "json",
            success: function (response) {
                if (response.status == "success") {
                    $(e).parents().find('#' + id).remove();
                }
                else if (response.status == "error") {
                    Leonni.ShowErrorMessage(response.errors);
                }
            }

        });
    }
}

//function ApproveDisApprove(id, sectionId) {
//    if (confirm("Are you sure?")) {
//        $.ajax({
//            url: LeonniUrl("/ControlPanel/ApproveDisApprove"),
//            type: "POST",
//            data: { id: id},
//            dataType: "json",
//            success: function (response) {
//                if (response.status == "success") {
//                    if (response.message == "Approved") {
//                        $("#Approved" + id).html("DisApprove");
//                    }
//                    else {
//                        $("#Approved" + id).html("Approve");
//                    }
//                }
//                else if (response.status == "error") {
//                    Leonni.ShowErrorMessage(response.errors);
//                }
//            }

//        });
//    }
//}

function View(id, sectionId) {
    window.location.href = LeonniUrl("ControlPanel/ViewProfile/" + id);
}

