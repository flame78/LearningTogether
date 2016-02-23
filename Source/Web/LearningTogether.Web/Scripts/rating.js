$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

$(".star").mouseover(function () {
    var span = $(this).parent("span").parent("span");
    var newRating = $(this).attr("value");
    setRating(span, newRating);
});

$(".star").mouseout(function () {
    var span = $(this).parent("span").parent("span");
    var rating = span.attr("data-rating");
    setRating(span, rating);
});

function setRating(span, rating) {
    span.find('img').each(function () {
        var value = parseFloat($(this).attr("value"));
        var imgSrc = $(this).attr("src");
        if (value <= rating) {
            $(this).attr("src", imgSrc.replace("-off.png", "-on.png"));
        } else {
            $(this).attr("src", imgSrc.replace("-on.png", "-off.png"));
        }
    });
}

$(".star").click(function () {
    var span = $(this).parent("span").parent("span");
    var newRating = $(this).attr("value");
    var itemId = span.attr("data-item-id");
    var text = span.children("span");
    $.post("/Home/SetRating", { id: itemId, vote: newRating }, function (obj) {
        if (obj.Success) {
            text.attr("data-original-title", "Currently rated " + obj.Result.Rating + " by " + obj.Result.Raters + " people");
            span.attr("data-rating", obj.Result.Rating);
            setRating(span, obj.Result.Rating);
            alert("Thank you, your vote was casted successfully.");
        } else {
            alert(obj.Message);
        }
    });
});