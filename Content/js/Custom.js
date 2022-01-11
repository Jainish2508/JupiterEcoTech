var product;
$(document).ready(function () {
    "use strict";
    initHomeSlider();
    function initHomeSlider() {
        if ($('.home_slider').length) {
            var homeSlider = $('.home_slider'); homeSlider.owlCarousel({ items: 1, autoplay: true, autoplayTimeout: 10000, loop: true, nav: false, smartSpeed: 1200, dotsSpeed: 1200, fluidSpeed: 1200 }); if ($('.home_slider_custom_dot').length) {
                $('.home_slider_custom_dot').on('click', function () { $('.home_slider_custom_dot').removeClass('active'); $(this).addClass('active'); homeSlider.trigger('to.owl.carousel', [$(this).index(), 1200]); });
            }
            homeSlider.on('changed.owl.carousel', function (event) { $('.home_slider_custom_dot').removeClass('active'); $('.home_slider_custom_dots li').eq(event.page.index).addClass('active'); }); function setAnimation(_elem, _InOut) {
                var animationEndEvent = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend'; _elem.each(function () {
                    var $elem = $(this); var $animationType = 'animated ' + $elem.data('animation-' + _InOut); $elem.addClass($animationType).one(animationEndEvent, function () { $elem.removeClass($animationType); });
                });
            }
            homeSlider.on('change.owl.carousel', function (event) { var $currentItem = $('.home_slider_item', homeSlider).eq(event.item.index); var $elemsToanim = $currentItem.find("[data-animation-out]"); setAnimation($elemsToanim, 'out'); }); homeSlider.on('changed.owl.carousel', function (event) { var $currentItem = $('.home_slider_item', homeSlider).eq(event.item.index); var $elemsToanim = $currentItem.find("[data-animation-in]"); setAnimation($elemsToanim, 'in'); })
        }
    }

    $(".productlist").change(function () {
        if (this.value == "custom") {
            $(".productlist").hide();
            $(".customproduct").show().focusin();
        }
        else {
            product = $("#selectedproduct").val();
        }
    });

    $(".customproduct").blur(function (event) {
        if (this.value == null || this.value == "") {
            $(".productlist").show();
            $(".customproduct").hide();
        }
        product = event.target.value;
    });
});

function recaptchaCallback() {
    $('#submitBtn').removeAttr('disabled');
};

const testimonials1 = [
    {
        customer: "Pradeep Verma",

        text:
            "I was looking for a Laptop with a Graphic card and i7 processor but the new Laptop was not in my budget, my sir suggested me to purchase Refurbished Laptop from Jupiter EcoTech. I contacted Jupiter Eco-Tech and took a visit. I was surprised by seeing thecollection of systems, it was beyond my expectations. I selected one Laptop that included 2GB Graphic Card, 1 TB SSD Hard Disk, i7 Processor, the surprising part was Laptop Quality and most affordable price.I thank to Jupiter EcoTech to provide me such agreat quality product.I am forwarding this Testimony after 3 years from purchased date.Generally, I don’t give testimony of any product but Jupiter EcoTech product has won my heart.",
        star: 5
    },
    {
        customer: "Jignesh Savaliya",

        text:
            "Selecting a Laptop for my student career was most challenging work for me because I was zero in computer knowledge when I enter BCA First year; however, Jupiter EcoTech Team helped me to recommend the best Laptop that suits my need. It has been 6 months didn’t face any problem yet not even in Software.",
        star: 3
    },
    {
        customer: "Anil Harkhani",

        text:
            "I started a business for data entry but was struggling to get systems for my office at my budget. I shared this with my Uncle he suggested me to take a visit at Jupiter EcoTech. First, I denied his suggestion, I know it was rude but I don’t trust anyone soon specially in business matters; however, later when I didn’t have any option I took a visit at Jupiter EcoTech, they helped me a lot to select the office systems at my budget also assured the service guarantee. I thanks to my uncle for his suggestion. Today, our office is in profit because of Jupiter EcoTech.",
        star: 4
    }
];

(function (arr) {
    const target = document.getElementById("testimonials1");
    target.classList.add("text-center");

    const fragment = document.createDocumentFragment();

    // Get all the templates
    arr.forEach((item, index, arr) => {
        createTestimonialTemplate(item);
    });

    // Templates
    function createTestimonialTemplate({ star, text, customer }) {
        const div = document.createElement("div");
        div.classList.add("testimonial");
        div.classList.add("d-none");

        // Customer template
        const c = document.createElement("h4");
        c.classList.add("mb-1");
        c.textContent = customer;
        div.appendChild(c);

        // Stars template
        const starsWrapper = document.createElement("div");
        starsWrapper.classList.add("stars-wrapper");
        while (star--) {
            const starWrapper = document.createElement("div");
            starWrapper.classList.add("star-wrapper");

            const starIcon = document.createElement("i");
            starIcon.classList.add("fa", "fa-star", "star");
            starWrapper.appendChild(starIcon);

            const insideStar = document.createElement("i");
            insideStar.classList.add("fa", "fa-star", "inside-star");
            starWrapper.appendChild(insideStar);

            starsWrapper.appendChild(starWrapper);
        }
        div.appendChild(starsWrapper);

        // Text template
        const p = document.createElement("p");
        p.classList.add("mt-4");
        p.textContent = text;
        div.appendChild(p);

        // Append template
        fragment.appendChild(div);
    }

    target.appendChild(fragment);

    // Animation
    function animation() {
        const timeTestimonialAppear = 10000;
        [...target.children].forEach((item, index, arr) => {

            // Appear
            const timerAppearing = setTimeout(function () {

                // Appearing
                item.classList.remove("d-none");

                // Animation
                const stars = item.querySelectorAll(".star-wrapper");
                let starsLength = stars.length;


                // Star appearing
                setTimeout(function () {
                    for (let i = 0; i < starsLength; i++) {
                        setTimeout(function () {
                            stars[i].classList.add("active");
                        }, 250 * i)
                    }
                }, 250)

                // Star disappearing
                setTimeout(function () {
                    for (let i = 0; i < starsLength; i++) {
                        setTimeout(function () {
                            stars[i].classList.remove("active");
                        }, 250 * i)
                    }
                }, timeTestimonialAppear - 250 * (starsLength + 1));

                //Customer Name appearing
                const cust = item.querySelector("h4");
                setTimeout(function () {
                    cust.classList.add("active");
                }, 250)

                setTimeout(function () {
                    cust.classList.remove("active");
                }, timeTestimonialAppear - 250 * (starsLength + 1));


                // Paragraph
                const par = item.querySelector("p");
                setTimeout(function () {
                    par.classList.add("active");
                }, 250)

                setTimeout(function () {
                    par.classList.remove("active");
                }, timeTestimonialAppear - 250 * (starsLength + 1));

                // Disappearing testimonial
                // If the first item or not the first item
                if (index === 0) {
                    arr[arr.length - 1].classList.add("d-none");
                } else {
                    arr[index - 1].classList.add("d-none");
                }

                // If the last item
                if (index === arr.length - 1) {
                    clearTimeout(timerAppearing);
                    // Infinit loop
                    setTimeout(animation, timeTestimonialAppear);
                }

            }, timeTestimonialAppear * index);
        })
    }

    setTimeout(animation, 2000);

})(testimonials1);

(function ($) {
    "use strict";
    var spinner = $('#process-loader');
    var formid = $('form').attr('id');
    $(document).ajaxStart(function () {
        spinner.show();
    });
    $(document).ajaxStop(function () {
        spinner.hide();
    });
    $("#" + formid).on("submit", function (event) {
        if (event.isDefaultPrevented()) {
            // handle the invalid form...
            formError(formid);
            submitMSG(false, "Did you fill in the form properly?");
        } else {
            // everything looks good!
            event.preventDefault();
            submitForm(formid);
        }
    });

    function submitForm(formid) {
        let valdata = {};
        var form = $("#" + formid);
        var antiForgeryToken = $("input[name=__RequestVerificationToken]", form).val();
        if (formid == "enquiry") {
            valdata = {
                __RequestVerificationToken: antiForgeryToken,
                name: $("#name").val(),
                email: $("#email").val(),
                contact: $("#phone").val(),
                product_name: product,
                query: $("#message").val(),
                recaptcha: grecaptcha.getResponse()
            };

            $.ajax({
                async: true,
                type: 'POST',
                url: "enquiry/send_message",
                data: valdata,
                contentType: "application/x-www-form-urlencoded",
                success: function (text) {
                    if (text) {
                        formSuccess(formid);
                        $('#submitbtn').prop("disabled", true);
                        $('#submitbtn').addClass("btn-disabled");
                        grecaptcha.reset();
                    }
                    else {
                        formError(formid);
                        grecaptcha.reset();
                        submitMSG(false, "Please try again!");
                    }
                }
                //error: function (XHR, textStatus, errorThrown) {
                //    alert(errorThrown);
                //}
            });
        }
        else {
            valdata = {
                __RequestVerificationToken: antiForgeryToken,
                name: $("#name").val(),
                email: $("#email").val(),
                phone: $("#phone").val(),
                subject: $("#subject").val(),
                query: $("#message").val(),
                recaptcha: grecaptcha.getResponse()
            };

            $.ajax({
                async: true,
                type: 'POST',
                url: "contact/send_message",
                data: valdata,
                contentType: "application/x-www-form-urlencoded",
                success: function (text) {
                    if (text) {
                        formSuccess(formid);
                        $('#submitbtn').prop("disabled", true);
                        $('#submitbtn').addClass("btn-disabled");
                        grecaptcha.reset();
                        location.reload();
                    }
                    else {
                        formError(formid);
                        grecaptcha.reset();
                        submitMSG(false, "Please try again!");
                    }
                }
                //error: function (XHR, textStatus, errorThrown) {
                //    alert(errorThrown);
                //}
            });
        }
    }

    function formSuccess(formid) {
        $("#" + formid)[0].reset();
        submitMSG(true, "Message Submitted!")
    }

    function formError(formid) {
        $("#" + formid).removeClass().addClass('shake animated').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
            $(this).removeClass();
        });
    }

    function submitMSG(valid, msg) {
        if (valid) {
            var msgClasses = "h4 tada animated text-success center pt-30";
        } else {
            var msgClasses = "h4 text-danger center pt-30";
        }
        $("#msgSubmit").removeClass().addClass(msgClasses).text(msg);
    }
}(jQuery));

