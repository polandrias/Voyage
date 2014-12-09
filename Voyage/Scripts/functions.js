var screenW;
var screenH;

$(document).on('ready', function () {
    screenH = $(window).height();
    screenW = $(window).width();

    videoPlayer.init();
    fancybox.init();
    data.Store();
    calcTotalPrice();
    readyFunctions();
});


$(window).on('load', function () {
    screenH = $(window).height();
    screenW = $(window).width();

    centerAlign();
    responsive.init(screenW);
});


$(window).on('resize', function () {
    screenH = $(window).height();
    screenW = $(window).width();

    centerAlign();
    responsive.init(screenW);
});


function fullHeight() {

    $('.fullHeight').each(function () {
        $(this).css('min-height', screenH + 'px');
    });

}


function centerAlign() {

    $('.centerAlign').each(function () {
        var parentHeight = $(this).closest('.parentAlign').height();
        var thisHeight = $(this).height();
        var topmargin = ((parentHeight - thisHeight) / 3.5);

        $(this).css('margin-top', topmargin);
    });

}


function centerScreen() {

    $('.centerScreen').each(function () {
        var thisHeight = $(this).height();
        var topmargin = ((screenH - thisHeight) / 2);

        $(this).css('margin-top', topmargin);
    });

}


var responsive = new function () {
    var self = this;
    var responsiveMode;


    this.init = function (screenW) {

        if (screenW < 768) {
            responsiveMode = 'media-xs';
        }
        if (screenW >= 768) {
            responsiveMode = 'media-sm';
        }
        if (screenW >= 992) {
            responsiveMode = 'media-md';
        }
        if (screenW >= 1200) {
            responsiveMode = 'media-lg';
        }

        $('body').removeClass('media-lg media-md media-sm media-xs')
        $('body').addClass(responsiveMode);

    }

}


var videoPlayer = new function () {
    var self = this;

    this.init = function () {

        $(document).on('click', '.youtube-play', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            self.play(id);
            return false;
        });

    };

    this.play = function (id) {

        var player = '<iframe width="560" height="315" src="//www.youtube.com/embed/' + id + '?rel=0&autoplay=1" frameborder="0" allowfullscreen></iframe>';

        var content = '<div class="video-container">' + player + '</div>';

        $.fancybox({
            content: content,
            fitToView: false,
            maxWidth: 1200,
            width: '100%',
            height: 'auto',
            autoSize: false,
            closeClick: false,
            openEffect: 'none',
            closeEffect: 'none',
            overflow: 'hidden',
            padding: 0
        });
    };

};


var fancybox = new function () {
    var self = this;

    this.init = function () {

        $(document).on('click', '.fancybox', function (e) {
            e.preventDefault();
            $.fancybox({
                maxWidth: 800,
                width: '100%',
                fitToView: false,
                autoSize: false,
                closeClick: false,
                openEffect: 'none',
                closeEffect: 'none',
                overflow: 'hidden',
                padding: 0
            });
        });

    };

};


var Store = new Object();

var data = new function () {
    var self = this;

    this.Store = function () {

        $(document).on('click change keyup', '*[data-store]', function () {
            var key = $(this).data('store');
            var value;

            if ($(this).data('value') != undefined) {
                value = $(this).data('value');
            } else {
                value = $(this).val();
            }

            Store[key] = value;

            console.log(Store);
        });

    };

    this.Save = function () {

        // Final save function - sends data to controller -> database

    };

};


function selectShow(ID) {

    // store selected data
    //Store.MovieID = ID;

    $.fancybox.close();
    $.fancybox.showLoading();

    obj = new Object();
    obj['ID'] = ID;

    $.ajax({
        type: 'POST',
        url: '/Booking/StepSelectShow/',
        cache: false,
        data: obj,
        dataType: 'html',
        success: function (data) {
            $.fancybox.hideLoading();
            popupAuto('overlay', 'md', 'overlay');
            $('#overlay').html(data);
        }

    }).done(function () {
        updateFancybox('#overlay');
    });

}


function selectSeats(ID) {

    $.fancybox.showLoading();

    obj = new Object();
    obj['ID'] = ID;

    $.ajax({
        type: 'POST',
        url: '/Booking/StepSeats/',
        cache: false,
        data: obj,
        dataType: 'html',
        success: function (data) {
            $.fancybox.hideLoading();
            $('#overlay').html(data);

        }

    }).done(function () {
        updateFancybox('#overlay');
    });

}


function findCustomer(seats) {

    $.fancybox.showLoading();

    obj = new Object();
    obj['seats'] = seats;

    $.ajax({
        type: 'POST',
        url: '/Booking/StepFindCustomer/',
        cache: false,
        data: obj,
        dataType: 'html',
        success: function (data) {
            $.fancybox.hideLoading();
            $('#overlay').html(data);

        }

    }).done(function () {
        updateFancybox('#overlay');
    });

}


function checkCustomer() {

    var phone = $('input[name="phone-check"]').val();

    $.fancybox.showLoading();

    obj = new Object();
    obj['phone'] = phone;

    $.ajax({
        type: 'POST',
        url: '/Booking/customerCheck/',
        cache: false,
        data: obj,
        dataType: 'html',
        success: function (data) {
            $.fancybox.hideLoading();
            $('#overlay').html(data);

            //if (data != "") {
            //    $('#customer-form').html(data);
            //} else {
            //    $('#customer-form').html("Dit telefonnummer er ikke registreret. Udfyld formularen nedenfor.");
            //}

        }

    }).done(function () {
        updateFancybox('#overlay');
    });

}


function saveCustomer() {

    $.fancybox.showLoading();

    obj = new Object();
    var customer = $('#customerForm');

    $.ajax({
        type: 'POST',
        url: '/Booking/ajaxSaveCustomer/',
        cache: false,
        data: customer.serialize(),
        dataType: 'html',
        success: function (data) {
            $.fancybox.hideLoading();
            //alert(data);
            //$('#overlay').html(data);
            stepConfirm();

        }

    }).done(function () {
        //updateFancybox('#overlay');
    });

}


function stepConfirm() {

    $.fancybox.showLoading();

    obj = new Object();

    $.ajax({
        type: 'POST',
        url: '/Booking/StepConfirm/',
        cache: false,
        data: obj,
        dataType: 'html',
        success: function (data) {
            $.fancybox.hideLoading();
            $('#overlay').html(data);
        }

    }).done(function () {
        updateFancybox('#overlay');
    });

}


function placeOrder(statusID) {

    $.fancybox.showLoading();

    obj = new Object();
    obj['statusID'] = statusID;

    $.ajax({
        type: 'POST',
        url: '/Booking/StepComplete/',
        cache: false,
        data: obj,
        dataType: 'html',
        success: function (data) {
            $.fancybox.hideLoading();
            $('#overlay').html(data);
        }

    }).done(function () {
        updateFancybox('#overlay');
    });

}



/* ---------- Universal popup holder for ajax --------- */
function popupAuto(uniqueID, size, uniqueClass, extraClass) {

    if (uniqueID != "") {
        var popupContent = '<div class="popup ' + uniqueClass + '" id="' + uniqueID + '"></div>';
    } else {
        var popupContent = '<div class="popup"></div>';
    }

    var boxMaxWidth;

    if (size == "lg") {
        boxMaxWidth = '900';
    } else if (size == "md") {
        boxMaxWidth = '780';
    } else {
        boxMaxWidth = '400';
    }

    if (extraClass == undefined) {
        var extraClass = "";
    }

    $.fancybox({
        content: popupContent,
        fitToView: false,
        maxWidth: boxMaxWidth,
        width: '95%',
        height: 'auto',
        autoSize: false,
        closeClick: false,
        openEffect: 'none',
        closeEffect: 'none',
        padding: 0,
        beforeShow: function () {
            $('.fancybox-wrap').addClass(extraClass);
        }
    });

    if (uniqueID != "") {
        $('#' + uniqueID).show();
    } else {
        $('.popup').show();
    }

}


function updateFancybox(id) {

    var elm = $(id);
    var img = $(id + ' img');

    if (elm.find("img").length > 0) {
        img.on('load', function () {
            $.fancybox.update();
        });
    } else {
        $.fancybox.update();
    }

}


function calcTotalPrice() {


    var seats;
    var totalPrice;

    $(document).on('change keyup', 'input[name="seats"]', function () {
        seats = $(this).val();

        if (seats == 0 || seats == undefined) {
            seats = 0;
        }

        totalPrice = seats * unitPrice.replace(",", ".");

        $('.total-price').html(totalPrice);
    });

}



function readyFunctions() {

    $(document).on('click', '.edit-user', function () {
        $('#customerForm').show();
        $('.customer-info').hide();

        updateFancybox('#overlay');

        return false;
    });

}