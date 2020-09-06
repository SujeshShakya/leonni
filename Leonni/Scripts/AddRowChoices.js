
var addContainer = $(".TextBoxDiv:last").find(".btnAdd");

addContainer.live("click", function () {
    if ($("#TextBoxesGroup").children().length == 1) {
        $(".TextBoxDiv:first").find(".btnAdd").after('<input type="button"  class="btnRemove" />')
    }

    $(".TextBoxDiv:last").find(".btnAdd").remove();
    var newTextBoxDiv = $(document.createElement('div')).addClass("TextBoxDiv");

    newTextBoxDiv.html('<div> <input type="text" class="choice" name="QuestionChoice"/> </div> <div class="add-remove"><input type="button" style="margin-left:13px;"  class="btnAdd" /><input type="button"  class="btnRemove" />&nbsp;&nbsp;<span class="errDuration" class="errmsg"></span></div><div class="clear"></div>');

    newTextBoxDiv.appendTo("#TextBoxesGroup");

});

var removeContainer = $(".btnRemove");

removeContainer.live("click", function () {

    if ($("#TextBoxesGroup").children().length != 2) {

        $(this).parent().parent().remove();
        addRemoveContainer = $(".TextBoxDiv:last").find(".add-remove");
        if (addRemoveContainer.find(".btnAdd").length == 0) {
            addRemoveContainer.prepend('<input type="button" style="margin-left:13px;"  class="btnAdd" />');
        }
        else {
            addRemoveContainer.find(".btnAdd").show();
        }
    }
    else {

        $(".TextBoxDiv:first").find(".btnRemove").after('<input type="button" style="margin-left:13px;"  class="btnAdd" />');
        $(this).parent().parent().remove();
        $(".TextBoxDiv:first").find(".btnRemove").remove();
    }

});

