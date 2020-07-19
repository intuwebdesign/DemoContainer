$(function () {
    $(".multi-column").on("mouseover",
        function () {
            $(this).closest(".nav-item").find(".nav-link").css("color", "black");
        });
    $(".multi-column").on("mouseout",
        function () {
            $(this).closest(".nav-item").find(".nav-link").css("color", "black");
        });

    $(".nav-link").on("mouseover",
        function () {
            var leftAlign = $("#main-div").offset();
            var i;

            for (i = 1; i < 7; i++) {
                $(`.sub-container-${i}`).css("position", "fixed");
                $(`.sub-container-${i}`).css("left", + Math.floor(leftAlign.left) + "px");
            }
        });
});