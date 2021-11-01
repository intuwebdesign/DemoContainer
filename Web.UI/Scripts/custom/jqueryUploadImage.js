$(function() {
    $("#ajaxRegister").hide();
    $(function() {
        var form = $("#frmAjaxForm");
        $(form).submit(function(event) {
            event.preventDefault();
            var isValid = $("#frmAjaxForm").valid();
            if (isValid) {
                $("#ajaxRegister").hide();
                $("#ajaxRegister").html("");

                var formData = new FormData($(this)[0]);

                jQuery.ajax({
                    type: "Post",
                    enctype:"multipart/form-data",
                    url: $(form).attr("action"),
                    data: formData,
                    processData: false,
                    contentType: false,
                    cache: false,
                    dataType: "json",
                    success: function(data) {
                        if (data.Confirm === true) {
                            $("#ajaxRegister").append(`<p class=\"text-success\">${data.message}</p>`).show();
                        } else {
                            $("#ajaxRegister").append(`<p class=\"text-danger\">${data.message}</p>`).show();
                        }
                    }
                });
            }
        });
    });
});