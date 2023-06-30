// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var sidbare = true;
function openNav() {
    if (sidbare == false) {
        document.getElementById("layoutSidenav_nav").style.width = "250px";
        document.getElementById("layoutSidenav_content").style.marginLeft = "2%";
        sidbare = true;
    }
    else {
        document.getElementById("layoutSidenav_nav").style.width = "0%";
        document.getElementById("layoutSidenav_content").style.marginLeft = "-17%";
        sidbare = false;
    }
}


function openPopup(rowcount) {

    var url = '/Invoice/Details?rowcount=' + encodeURIComponent(rowcount);
    var newWindow = window.open(url, '_blank');
    newWindow.focus();
}
function generatePDF() {
    // Define options for PDF generation
    var today = new Date();
    var date = today.toISOString().slice(0, 10);
    var time = today.toTimeString().slice(0, 8).replace(/:/g, '');
    var filename = 'Invoice_' + date + '_' + time + '.pdf';
    var options = {
        margin: 10,
        filename: filename,
        image: { type: 'jpeg', quality: 0.98 },
        html2canvas: { scale: 2 },
        jsPDF: { unit: 'pt', format: 'letter', orientation: 'portrait' }
    };

    // Select the HTML element that you want to convert to PDF
    var element = document.documentElement;

    // Exclude the header and footer elements from PDF conversion
    var headerElements = document.getElementsByClassName('fixed-header');
    var footerElements = document.getElementsByClassName('fixed-footer');

    // Hide the PDF generation button during conversion
    var pdfButton = document.getElementById('pdfgenerate');
    pdfButton.style.display = 'none';

    for (var i = 0; i < headerElements.length; i++) {
        headerElements[i].style.display = 'none';
    }

    for (var j = 0; j < footerElements.length; j++) {
        footerElements[j].style.display = 'none';
    }

    // Call the html2pdf library to generate the PDF asynchronously
    html2pdf().set(options).from(element).save().then(function () {
        // Show the PDF generation button and restore header and footer display after conversion
        pdfButton.style.display = 'block';

        for (var i = 0; i < headerElements.length; i++) {
            headerElements[i].style.display = 'block';
        }

        for (var j = 0; j < footerElements.length; j++) {
            footerElements[j].style.display = 'block';
        }
    });
}

