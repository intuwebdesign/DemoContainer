$(function () {
    //If there are no items in sessionStorage, hide the cart button
    var basketCounter = 0;
    $.each(sessionStorage, function (key, value) {
        if (key.startsWith("Product")) {
            basketCounter++;
        }
    });
    if (basketCounter > 0) {
        $("#btnModalCart").show();
    } else {
        $("#btnModalCart").hide();
    }

    if (sessionStorage.getItem("country") === null) {
        const country = $("#currencyFormat").val();
        sessionStorage.setItem("country", country);
    };
});

$(function () {
    $(".productId").on("click", function () {
        //Add items to sessionStorage
        $("#btnModalCart").show();
        const productId     = $(this).data("id");
        const productTitle  = $(this).parent("div.productInfo").find("p.productHeading").html();
        const productPrice  = $(this).parent("div.productInfo").find("p.productPrice").data("price");
        const productImage  = $(this).parent("div.productInfo").find("img").attr("src");

        const cartItem          = JSON.stringify(`${productId}#${productTitle}#${productPrice}#${productImage}`);
        const productSessionId  = `Product${productId}`;

        sessionStorage.setItem(productSessionId, cartItem);
        console.log(JSON.stringify(productTitle + "#" + productId + "#" + productPrice + "#" + productImage));
    });
});

$(function () {
    $("#btnModalCart").on("click", function () {
        //Display items in the cart
        $("#modelulcart").html("");
        var basketCounter   = 0;
        var totalCount      = 0.00;
        $.each(sessionStorage, function (key, value) {
            if (key.startsWith("Product")) {
                const products = value.split("#");
                const [, displayProductTitle, displayProductPrice, displayProductImage] = products;

                totalCount += parseFloat(displayProductPrice);
                basketCounter++;

                $("#modalCart").html(currencyFormatter(totalCount));
                $("#modal-body ul").append(`<li class="cart-item"><span class="modalLiProducts"><p><strong>${displayProductTitle}</strong></p><img src="${displayProductImage}" height="200"/></span><span class="modalLiProducts"><br/> Price: ${currencyFormatter(displayProductPrice)} <br/> <a class="delete-ticket" data-delete="${key}" href="#">Delete</a></span></li>`);
            }
        });

        updateBasketCount(totalCount, basketCounter);
    });
});

$(function () {
    //Displays the cart items on the checkout page
    var basketCounter = 0;
    var totalCount = 0.00;
    $.each(sessionStorage, function (key, value) {
        if (key.startsWith("Product")) {
            const products = value.split("#");
            const [, displayProductTitle, displayProductPrice, displayProductImage] = products;

            totalCount += parseFloat(displayProductPrice);
            basketCounter++;

            $("#checkout-header").html(currencyFormatter(totalCount));
            $("#modal-body ul").append(`<li class="cart-item"><span class="modalLiProducts"><p><strong>${displayProductTitle}</strong></p><img src="${displayProductImage}" height="200"/></span><span class="modalLiProducts"><br/> Price: ${currencyFormatter(displayProductPrice)} <br/> <a class="delete-ticket" data-delete="${key}" href="#">Delete</a></span></li>`);
        }
    });

    updateBasketCount(totalCount, basketCounter);
});

$(function () {
    $("#modal-body").on("click", "ul li a.delete-ticket", function (e) {
        //Delete items from the cart
        e.preventDefault();
        const targetId = $(this).attr("data-delete");
        $(this).closest("li").fadeOut();
        sessionStorage.removeItem(targetId);

        var basketCounter   = 0;
        var totalCount      = 0.00;
        $.each(sessionStorage, function (key, value) {
            if (key.startsWith("Product")) {
                const products = value.split("#");
                const displayProductPrice = products[2];
                totalCount += parseFloat(displayProductPrice);
                basketCounter++;
            }
        });
        updateBasketCount(totalCount, basketCounter);
    });
});

function updateBasketCount(totalCount, basketCounter) {
    //Count number of items in the cart and total price
    $("#modalCart").html(currencyFormatter(totalCount));

    if (basketCounter === 1) {
        $("#modalCount").html(basketCounter + " item in basket");
    } else if (basketCounter > 1) {
        $("#modalCount").html(basketCounter + " items in basket");
    } else {
        $("#modalCount").html(basketCounter + " items in basket");
        $("#btnModalCart").hide();
        $("#myModal").modal("hide");
    }
}

function currencyFormatter(value) {
    //Format price to currency based on country
    var currencyFormat;
    var displayCurrencyFormat;
    var country;

    if (sessionStorage.getItem("country") !== null) {
        country = sessionStorage.getItem("country");

        switch (country) {
        case "France":
            currencyFormat = new Intl.NumberFormat("fr-FR", { style: "currency", currency: "EUR" });
            displayCurrencyFormat = currencyFormat.format(value);
            break;
        case "United States":
            currencyFormat = new Intl.NumberFormat("en-US", { style: "currency", currency: "USD" });
            displayCurrencyFormat = currencyFormat.format(value);
            break;
        default:
            currencyFormat = new Intl.NumberFormat("en-GB", { style: "currency", currency: "GBP" });
            displayCurrencyFormat = currencyFormat.format(value);
            break;
        }
        return displayCurrencyFormat;
    }
    currencyFormat = new Intl.NumberFormat("en-GB", { style: "currency", currency: "GBP" });
    displayCurrencyFormat = currencyFormat.format(value);
    return displayCurrencyFormat;
}

$(function () {
    var form = $("#demoShoppingCart");
    //Submit form for payment
    $(form).submit(function (event) {
        event.preventDefault();
        var isValid = $("#demoShoppingCart").valid();
        if (isValid) {
            var productIds = [];
            $.each(sessionStorage, function (key, value) {
                if (key.startsWith("Product")) {
                    const item = {};
                    item["ID"] = key;
                    productIds.push(item);
                }
            });

            $("#ProductIds").val(JSON.stringify(productIds));

            $("#ajaxMessage").hide();
            $("#ajaxMessage").html("");
            jQuery.ajax({
                type: "Post",
                url: $(form).attr("action"),
                data: $(form).serialize(),
                cache: false,
                dataType: "json",
                success: function (data) {
                    if (data.Confirm === true) {
                        $("#ajaxMessage").append(`<p class="text-success">${data.message}</p>`).show();
                        sessionStorage.clear();
                    } else {
                        $("#ajaxMessage").append(data.message).show();
                    }
                }
            });
        }
    });
});