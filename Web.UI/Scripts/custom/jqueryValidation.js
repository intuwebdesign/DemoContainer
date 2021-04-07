$(function() {
    $("form[name='demoValidation']").validate({
        rules: {
            FirstName: "required",
            LastName: "required",
            Email: "required",
            Password: "required",
            ComparePassword: {
                required: true,
                equalTo: "#Password"
            }
        },
        messages: {
            FirstName: "Please enter your first name",
            LastName: "Please enter your last name",
            Email: "Please enter your email",
            Password: "Please enter a password",
            ComparePassword: {
                required: "Please enter compare password",
                equalTo: "Passwords do not match"
            }
        },
        errorElement: "div",
        errorLabelContainer: ".errorTxt",
        submitHandler: function (form) {
            form.submit();
        }
    });
})