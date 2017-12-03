
function hideshow(item) {
    item.style.display = 'block';
    item.style.display = 'none';
}

function execute() {
    $(".image-updater")
        .each(function(index, item) {
            var url = $(item).data("url");
            if (url && url.length > 0) {
                $(item)
                    .load(url,
                        { photoId: $(this).attr("value") },
                        function(data) {
                            $(item).replaceWith(data);
                        });
            }
        });
}

$(document)
    .ready(function (e) {
        $(".image-updater").each(function (index, item) {
            var url = $(item).data("url");
            if (url && url.length > 0) {
                $(item).load(url, { photoId: $(this).attr("value")}, function(data) {
                    $(item).replaceWith(data);
                });            
            }
        });
    });

