$(function() {
    const resizeableImage = function (imageTarget) {
        imageTarget = $(imageTarget).get(0);
        var $container,
            origSrc         = new Image(),
            eventState      = {},
            constrain       = false,
            minWidth        = 100,
            minHeight       = 100,
            maxWidth        = 1000,
            maxHeight       = 1000,
            initHeight      = 500,
            resizeCanvas    = document.createElement("canvas");

        var imageData   = null;
        var crop        = null;
        var loadData    = null;
        var startResize = null;
        var startMoving = null;

        const init = function () {

            $(".uploadFile").change(function (evt) {
                var files = evt.target.files; 
                var reader = new FileReader();

                reader.onload = function (e) {
                    imageData = reader.result;
                    loadData();
                }
                reader.readAsDataURL(files[0]);
            });

            $(".btnReset").click(function () {
                if (imageData)
                    loadData();
            });

            origSrc.src = imageTarget.src;

            $(imageTarget).height(initHeight)
                .wrap('<div class="canvasResizeContainer"></div>')
                .before('<span class="resizeDragHandleBottomRight"></span>')
                .before('<span class="resizeDragHandleBottomLeft"></span>')
                .after('<span class="resizeDragHandleTopLeft"></span>')
                .after('<span class="resizeDragHandleTopRight"></span>');

            $container = $(".canvasResizeContainer");

            $container.prepend('<div class="canvasResizeContainerTop"></div>');

            $container.on("mousedown touchstart", startResize);
            $container.on("mousedown touchstart", ".canvasResizeContainerTop", startMoving);
            $(".btnCrop").on("click", crop);
        };

        var resizeImageCanvas = 0;
        loadData = function () {

            imageTarget.src = imageData;
            origSrc.src = imageTarget.src;
            $(imageTarget).css({
                width: "auto",
                height: initHeight
            });
            
            $(origSrc).bind("load", function () {
                resizeImageCanvas($(imageTarget).width(), $(imageTarget).height());
            });
        };

        var endResize = null;
        var saveEventState = null;
        var resizing = null;
        startResize = function (e) {
            e.preventDefault();
            e.stopPropagation();
            saveEventState(e);
            $(document).on("mousemove touchmove", resizing);
            $(document).on("mouseup touchend", endResize);
        };

        endResize = function (e) {
            resizeImageCanvas($(imageTarget).width(), $(imageTarget).height());
            e.preventDefault();
            $(document).off("mouseup touchend", endResize);
            $(document).off("mousemove touchmove", resizing);
        };

        saveEventState = function (e) {
            eventState.containerWidth = $container.width();
            eventState.containerHeight = $container.height();
            eventState.containerLeft = $container.offset().left;
            eventState.containerTop = $container.offset().top;
            eventState.mouse_x = (e.clientX || e.pageX || e.originalEvent.touches[0].clientX) + $(window).scrollLeft();
            eventState.mouse_y = (e.clientY || e.pageY || e.originalEvent.touches[0].clientY) + $(window).scrollTop();

            if (typeof e.originalEvent.touches !== "undefined") {
                eventState.touches = [];
                $.each(e.originalEvent.touches, function (i, ob) {
                    eventState.touches[i] = {};
                    eventState.touches[i].clientX = 0 + ob.clientX;
                    eventState.touches[i].clientY = 0 + ob.clientY;
                });
            }
            eventState.evnt = e;
        };

        var resizeImage = null;
        resizing = function (e) {
            var mouse = {}, width, height, left, top, offset = $container.offset();
            mouse.x = (e.clientX || e.pageX || e.originalEvent.touches[0].clientX) + $(window).scrollLeft();
            mouse.y = (e.clientY || e.pageY || e.originalEvent.touches[0].clientY) + $(window).scrollTop();

            if ($(eventState.evnt.target).hasClass("resizeDragHandleTopLeft")) {
                width = mouse.x - eventState.containerLeft;
                height = mouse.y - eventState.containerTop;
                left = eventState.containerLeft;
                top = eventState.containerTop;
            } else if ($(eventState.evnt.target).hasClass("resizeDragHandleTopRight")) {
                width = eventState.containerWidth - (mouse.x - eventState.containerLeft);
                height = mouse.y - eventState.containerTop;
                left = mouse.x;
                top = eventState.containerTop;
            } else if ($(eventState.evnt.target).hasClass("resizeDragHandleBottomRight")) {
                width = eventState.containerWidth - (mouse.x - eventState.containerLeft);
                height = eventState.containerHeight - (mouse.y - eventState.containerTop);
                left = mouse.x;
                top = mouse.y;
                if (constrain || e.shiftKey) {
                    top = mouse.y - ((width / origSrc.width * origSrc.height) - height);
                }
            } else if ($(eventState.evnt.target).hasClass("resizeDragHandleBottomLeft")) {
                width = mouse.x - eventState.containerLeft;
                height = eventState.containerHeight - (mouse.y - eventState.containerTop);
                left = eventState.containerLeft;
                top = mouse.y;
                if (constrain || e.shiftKey) {
                    top = mouse.y - ((width / origSrc.width * origSrc.height) - height);
                }
            }

            if (constrain || e.shiftKey) {
                height = width / origSrc.width * origSrc.height;
            }

            if (width > minWidth && height > minHeight && width < maxWidth && height < maxHeight) {
                resizeImage(width, height);
                $container.offset({ 'left': left, 'top': top });
            }
        }

        resizeImage = function (width, height) {
            $(imageTarget).width(width).height(height);
        };

        resizeImageCanvas = function (width, height) {
            resizeCanvas.width = width;
            resizeCanvas.height = height;
            resizeCanvas.getContext("2d").drawImage(origSrc, 0, 0, width, height);
            $(imageTarget).attr("src", resizeCanvas.toDataURL("placeholder/png"));
        };

        var endMoving = null;
        var moving = null;
        startMoving = function (e) {
            e.preventDefault();
            e.stopPropagation();
            saveEventState(e);
            $(document).on("mousemove touchmove", moving);
            $(document).on("mouseup touchend", endMoving);
        };

        endMoving = function (e) {
            e.preventDefault();
            $(document).off("mouseup touchend", endMoving);
            $(document).off("mousemove touchmove", moving);
        };

        moving = function (e) {
            const mouse = {};
            e.preventDefault();
            e.stopPropagation();

            const touches = e.originalEvent.touches;

            mouse.x = (e.clientX || e.pageX || touches[0].clientX) + $(window).scrollLeft();
            mouse.y = (e.clientY || e.pageY || touches[0].clientY) + $(window).scrollTop();
            $container.offset({ 'left': mouse.x - (eventState.mouse_x - eventState.containerLeft), 'top': mouse.y - (eventState.mouse_y - eventState.containerTop) });
            if (eventState.touches && eventState.touches.length > 1 && touches.length > 1) {
                let width = eventState.containerWidth;
                let height = eventState.containerHeight;
                let a = eventState.touches[0].clientX - eventState.touches[1].clientX;
                a = a * a;
                let b = eventState.touches[0].clientY - eventState.touches[1].clientY;
                b = b * b;
                const dist1 = Math.sqrt(a + b);

                a = e.originalEvent.touches[0].clientX - touches[1].clientX;
                a = a * a;
                b = e.originalEvent.touches[0].clientY - touches[1].clientY;
                b = b * b;
                const dist2 = Math.sqrt(a + b);

                const ratio = dist2 / dist1;

                width = width * ratio;
                height = height * ratio;
                resizeImage(width, height);
            }
        };

        crop = function () {
            const left = $(".cropImageOverlay").offset().left - $container.offset().left;
            const top = $(".cropImageOverlay").offset().top - $container.offset().top;
            var width = $(".cropImageOverlay").width(), height = $(".cropImageOverlay").height();

            const cropCanvas = document.createElement("canvas");

            cropCanvas.width = width;
            cropCanvas.height = height;

            cropCanvas.getContext("2d").drawImage(imageTarget, left, top, width, height, 0, 0, width, height);
            const dataUrl = cropCanvas.toDataURL("image/png");
            imageTarget.src = dataUrl;
            origSrc.src = imageTarget.src;
            $("#Base64Image").val(dataUrl);


            $(imageTarget).bind("load", function () {
                $(this).css({
                    width: width,
                    height: height
                }).unbind("load").parent().css({
                    top: $(".cropImageOverlay").offset().top - $(".canvasCropWrapper").offset().top,
                    left: $(".cropImageOverlay").offset().left - $(".canvasCropWrapper").offset().left
                });
            });
        }

        init();
    };
    resizeableImage($(".resize-image"));
});