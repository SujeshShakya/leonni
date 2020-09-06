
var addContainer = $(".TextBoxDiv:last").find(".btnAdd");

addContainer.live("click", function () {    
    if ($("#TextBoxesGroup").children().length == 1) {
        $(".TextBoxDivHS:first").find(".btnAdd").after('<input type="button"  class="btnRemove btnRemoveHS" />')
    }
    $(".TextBoxDivHS:last").find(".btnAdd").remove();
    var newTextBoxDiv = $(document.createElement('div')).addClass("TextBoxDivHS");

    newTextBoxDiv.html('<div id="TextBoxDiv1" class="TextBoxDiv"><div class="field-1"><div class="field" style="margin-bottom: 20px"><input type="text" value="video youtube" name="txtVideo" style="width: 65%"></div></div><div class="add-remove"><input type="button" style="margin-left:13px;"  class="btnAdd btnAddHS" /><input type="button"  class="btnRemove btnRemoveHS" />&nbsp;&nbsp;<span class="errDuration" class="errmsg"></span></div><div class="clear"></div></div>');

    newTextBoxDiv.appendTo("#TextBoxesGroup");

});

var removeContainer = $(".btnRemoveHS");

removeContainer.live("click", function () {

    if ($("#TextBoxesGroup").children().length != 2) {

        $(this).parent().parent().remove();
        addRemoveContainer = $(".TextBoxDiv:last").find(".add-remove");
        if (addRemoveContainer.find(".btnAdd").length == 0) {
            addRemoveContainer.prepend('<input type="button" style="margin-left: 13px;"  class="btnAdd btnAddHS" />');
        }
        else {
            addRemoveContainer.find(".btnAdd").show();
        }
    }
    else {

        $(".TextBoxDivHS:first").find(".btnRemove").after('<input type="button" style="margin-left: 13px;"  class="btnAdd btnAddHS" />');
        $(this).parent().parent().remove();
        //removeContainer.parents().find(".TextBoxDivHS:first").remove();
        $(".TextBoxDiv:first").find(".btnRemove").remove();
    }

});