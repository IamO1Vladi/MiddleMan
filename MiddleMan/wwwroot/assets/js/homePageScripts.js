var swiper; // Declare the Swiper instance globally

$(document).ready(function () {
    // Load the partial view asynchronously
    $("#informationPostsPlaceholder").load('/Information/InformationThumbnailPartial', function () {
        // After the partial view loads, update the Swiper instance
        if (swiper) {
            swiper.update(); // Update the existing Swiper instance
        } else {
            // Initialize Swiper for the first time if it's not initialized yet
            swiper = new Swiper('.swiper-container', {
                slidesPerView: 3,
                spaceBetween: 30,
                loop: true,
                navigation: {
                    nextEl: '.swiper-button-next',
                    prevEl: '.swiper-button-prev',
                },
                pagination: {
                    el: '.swiper-pagination',
                    clickable: true,
                },
                autoplay: {
                    delay: 3000,
                },
                breakpoints: {
                    320: {
                        slidesPerView: 1,
                        spaceBetween: 10,
                    },
                    576: {
                        slidesPerView: 2,
                        spaceBetween: 15,
                    },
                    768: {
                        slidesPerView: 2,
                        spaceBetween: 20,
                    },
                    1024: {
                        slidesPerView: 3,
                        spaceBetween: 30,
                    }
                }
            });
        }
    });
});