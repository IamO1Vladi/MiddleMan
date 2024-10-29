/*----------------------------------------------
*
* [Main Scripts]
*
* Theme    : NEXGEN
* Version  : 1.0
* Author   : Codings
* Support  : codings.dev
* 
----------------------------------------------*/

/*----------------------------------------------

[ALL CONTENTS]

1. Preloader

----------------------------------------------*/

/*----------------------------------------------
1. Preloader
----------------------------------------------*/




jQuery(function ($) {
    $(document).ready(function () {

        $('#nexgen-subscribe').submit(function (event) {
            event.preventDefault(); // Prevent the default form submission

            const formData = JSON.stringify(Object.fromEntries(new FormData(this)));

            $.ajax({
                url: $(this).attr('action'),
                type: 'POST',
                data: formData,
                contentType: 'application/json',
                traditional: true, // Add this line
                success: function (response) {
                    if (response.success) {
                        $('#nexgen-subscribe')[0].reset();
                        showMessage('success', response.message);
                    } else {
                        showMessage('danger', response.message);
                    }
                },
                error: function () {
                    showMessage('danger', 'An unexpected error occurred. Please try again later.');
                }
            });

        });

        function showMessage(type, message) {
            const messageBar = $('#response-message');
            messageBar.removeClass('alert-success alert-danger');
            messageBar.addClass('alert-' + type);
            $('#response-text').text(message);
            messageBar.show();

            // Hide the message after a few seconds
            setTimeout(function () {
                messageBar.fadeOut();
            }, 5000); // 5 seconds
        }
    });
})

