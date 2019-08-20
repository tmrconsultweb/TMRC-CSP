$(document).ready(function () {
    var trigger = $('.hamburger'),
        overlay = $('.overlay'),
        isClosed = true;

    trigger.click(function () {
        hamburger_cross();
    });

    function hamburger_cross() {

        if (isClosed == true) {
            overlay.hide();
            trigger.removeClass('is-open');
            trigger.addClass('is-closed');
            isClosed = false;
        } else {
            overlay.show();
            trigger.removeClass('is-closed');
            trigger.addClass('is-open');
            isClosed = true;
        }
    }

    $('[data-toggle="offcanvas"]').click(function () {
        $('#wrapper').toggleClass('toggled');
    });

    //function inputFilter(inputFilter) {

    //    return this.on("input keydown keyup mousedown mouseup select contextmenu drop", function () {
    //        if (inputFilter(this.value)) {

    //            this.oldValue = this.value;
    //            this.oldSelectionStart = this.selectionStart;
    //            this.oldSelectionEnd = this.selectionEnd;
    //        } else if (this.hasOwnProperty("oldValue")) {
    //            console.log(this.type)
    //            // console.log(this.oldSelectionEnd)
    //            //try {
    //                if (this.type == "number") {
    //                    this.value = this.oldValue;
    //                    this.setSelectionRange(0, 0);
    //                }
    //                else {
    //                    this.value = this.oldValue;
    //                    this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
    //                }

    //            //}
    //           // catch{}
    //        }
    //    });
    //};
}); 





