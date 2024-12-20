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

    'use strict';

    $('#services-dropdown').on('change', function () {
        var selectedValue = $(this).val();
        if (selectedValue === 'Organise a Factory Tour' || selectedValue === 'Find a Supplier') {
            $('#industry-box').show(); // Show the industry box
        } else {
            $('#industry-box').hide(); // Hide the industry box
        }
    });
   
})


jQuery(function ($) {
    // Ensure intl-tel-input is fully loaded before initializing the iti variable
    var input = document.querySelector("#phone");

    // Initialize intl-tel-input on the phone input field
    var iti = window.intlTelInput(input, {
        initialCountry: "auto", // Automatically detect the user's country
        geoIpLookup: function (success, failure) {
            $.get("https://ipinfo.io", function () { }, "jsonp").always(function (resp) {
                var countryCode = (resp && resp.country) ? resp.country : "us";
                success(countryCode);
            });
        },
        utilsScript: "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/17.0.8/js/utils.js" // For formatting/validation
    });

    function updateHiddenPhoneNumber() {
        var fullPhoneNumber = iti.getNumber(); // Get the full phone number (with country code)
        $('#fullPhoneNumber').val(fullPhoneNumber); // Update the hidden field
    }

    // Listen for the `countrychange` event to detect when the user changes the country code
    input.addEventListener('countrychange', function () {
        updateHiddenPhoneNumber(); // Update the hidden field when country changes
    });

    // Optionally, update the hidden field on input change as well
    input.addEventListener('input', function () {
        updateHiddenPhoneNumber(); // Update on phone input change
    });

    // Initially populate the hidden field with the default phone number
    updateHiddenPhoneNumber();
})

jQuery(function ($) {
    $(document).ready(function () {

        $('#nexgen-simple-form').submit(function (event) {
            event.preventDefault(); // Prevent the default form submission

            // Create a FormData object to handle file uploads
            var formData = new FormData(this);

            $.ajax({
                url: $(this).attr('action'),
                type: 'POST',
                data: formData,
                processData: false,  // Important: prevent jQuery from automatically processing the data
                contentType: false,
                success: function (response) {
                    if (response.success) {
                        // Clear the form if successful
                        $('#nexgen-simple-form')[0].reset();
                        $('#file-upload-status').hide();
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

jQuery(function ($) {
    $(document).ready(function () {
        const fileInput = $('#file-upload');
        const dragArea = $('#file-drag-area');
        const status = $('#file-upload-status');

        // Drag and drop functionality
        dragArea.on('dragover', function (event) {
            event.preventDefault();
            dragArea.addClass('dragging');
        });

        dragArea.on('dragleave', function () {
            dragArea.removeClass('dragging');
        });

        dragArea.on('drop', function (event) {
            event.preventDefault();
            dragArea.removeClass('dragging');

            const files = event.originalEvent.dataTransfer.files;

            // Manually assign the dropped file(s) to the file input element
            fileInput[0].files = files;  // This sets the file input to the dropped files

            // Trigger the change event to ensure form submission works as expected
            fileInput.trigger('change');

            // Update the display with the file names
            displayUploadedFiles(files);
        });

        // File input change event (for clicking to upload or when drag-and-drop triggers it)
        fileInput.on('change', function (event) {
            const files = event.target.files;
            displayUploadedFiles(files);
        });

        // Function to display uploaded files
        function displayUploadedFiles(files) {
            let fileNames = [];
            for (let i = 0; i < files.length; i++) {
                fileNames.push(files[i].name);
            }
            status.text(`Uploaded: ${fileNames.join(', ')}`);
        }
    });
})

//jQuery(function ($) {
//    $(document).ready(function () {
//        $('#nexgen-simple-form').on('submit', function (event) {
//            let isValid = true;

//            // Validate Name
//            const name = $('.field-name').val().trim();
//            if (name.length < 2 || name.length > 50) {
//                alert('Name is required and must be between 2 and 50 characters.');
//                isValid = false;
//            }

//            // Validate Email
//            const email = $('.field-email').val().trim();
//            const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
//            if (!email || !emailPattern.test(email)) {
//                alert('Please enter a valid email address.');
//                isValid = false;
//            }

//            // Validate Phone Number
//            const phone = $('.field-phone').val().trim();
//            if (!phone) {
//                alert('Phone number is required.');
//                isValid = false;
//            }

//            // Validate Service Type
//            const serviceType = $('#services-dropdown').val();
//            if (!serviceType) {
//                alert('Please select a service type.');
//                isValid = false;
//            }

//            // Validate Industry if visible
//            if ($('#industry-box').is(':visible')) {
//                const industry = $('.field-industry').val().trim();
//                if (!industry) {
//                    alert('Industry is required if visible.');
//                    isValid = false;
//                }
//            }

//            // Validate Initial Message
//            const message = $('.field-message').val().trim();
//            if (message.length < 10 || message.length > 5000) {
//                alert('Message is required and must be between 10 and 5000 characters.');
//                isValid = false;
//            }

//            // Validate File Attachment
//            const fileInput = $('.file-input')[0].files[0];
//            const maxFileSize = 90 * 1024 * 1024; // 90MB
//            if (fileInput && fileInput.size > maxFileSize) {
//                alert('File size must not exceed 90MB.');
//                isValid = false;
//            }

//            // Prevent form submission if validation fails
//            if (!isValid) {
//                event.preventDefault();
//            }
//        });
//    });




//})