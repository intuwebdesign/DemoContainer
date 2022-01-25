$(function() {

    $("#eyePassword").on("click",function () {

        $(this).toggleClass("fa-eye-slash fa-eye");
        var input = $("#Password");
        if (input.attr("type") === "password") {
            input.attr("type", "text");
        } else {
            input.attr("type", "password");
        }
    });

    $("#eyePasswordCompare").on("click", function () {

        $(this).toggleClass("fa-eye-slash fa-eye");
        var input = $("#ComparePassword");
        if (input.attr("type") === "password") {
            input.attr("type", "text");
        } else {
            input.attr("type", "password");
        }
    });
});

$(function () {
    $.validator.addMethod("passwordStrength", function (value, element) {
        const password = value;
        if (/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$])[0-9a-zA-Z!@#$]{6,10}$/.test(password)) {
            return true;
        }
        return false;
    }, "Password must be between 6 and 10 characters and contain uppercase and lowercase letters, numbers and one of the following !@#$");

    //jquery validate form before submit example

    $("#demoValidation").on("submit", function (e) { e.preventDefault(); }).validate({
            rules: {
                FirstName: "required",
                LastName: "required",
                Email: {
                    required: true,
                    email: true
                },
                Password: {
                    passwordStrength: true
                },
                ComparePassword: {
                    required: true,
                    equalTo: "#Password"
                }
            },
            messages: {
                FirstName: "Please enter your first name",
                LastName: "Please enter your last name",
                Email: "Please enter a valid email",
                ComparePassword: {
                    required: "Please enter compare password",
                    equalTo: "Passwords do not match"
                }
            },
            errorElement: "div",
            errorLabelContainer: ".errorTxt",

            submitHandler: function (form) {
                $.ajax({
                    type: "Post",
                    url: $(form).attr("action"),
                    data: $(form).serialize(),
                    cache: false,
                    dataType: "json",
                    success: function (data) {
                        if (data.Confirm === true) {
                            $("#ajaxMessage").append(`<div class="alert alert-success">${data.message}</div>`).show();
                        } else {
                            $("#ajaxMessage").append(`<div class="alert alert-danger">${data.message}</div>`).show();
                        }
                    }
                });
            }
     });
})