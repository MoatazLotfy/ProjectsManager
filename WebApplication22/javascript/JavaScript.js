/* index ------------------------------------------------------------
--------------------------------------------------*/
$(document).ready(function () {

  
    $('.x').click(function () {

        $('.PopUpBG').fadeOut('slow');
        
    });
  
});
$('.Rsubmit').click(function () {
    alert($('.profilephoto').val());
});
$(function () {
    'use stricrt';
    $('.HomeBG').height($(window).height());

    $(window).resize(function () {
        $('.HomeBG').height($(window).height());
    });
});
$(function () {
    'use stricrt';
    $('.header').height($(window).height());

    $(window).resize(function () {
        $('.header').height($(window).height());
    });
});



$(function () {
    'use stricrt';

    $('.options .right li a').click(function () {
        $('.options .right li').addClass('NaviActive').siblings().removeClass('NaviActive');
    });
});





//-- end index ------------------------------
//-----------------------------------------

// LOGIN ---------------------------------------------------------------
//---------------------------------------


$(function () {


    $('.email').click(function () {
        $(this).addClass('actived').siblings().removeClass('actived');
        $('.submit').removeClass("faild").val('Login').prop("disabled", false);
    });

    $('.password').click(function () {
        $(this).addClass('actived').siblings().removeClass('actived');
        $('.submit').removeClass("faild").val('Login').prop("disabled", false);
    });
});



$(function () {
    $('.submit').click(function () {
        var email = "a";
        var password = "1";
       
       

        $(this).prop("disabled", true).addClass('disabled').val('logging in');
        $('.email').removeClass('actived');
        $('.password').removeClass('actived');
        $('.dots li').css({ opacity: 1 });
        for (i = 0; i < 4; i++) {
            $('.dots li').eq(i).animate({
                top: "-15px"

            }, (i * 100) + 300);
        }
        for (i = 0; i < 4; i++) {
            $('.dots li').eq(i).animate({
                top: "0px"

            }, (i * 100) + 300);
        }
        for (i = 0; i < 4; i++) {
            $('.dots li').eq(i).animate({
                top: "-15px"

            }, (i * 100) + 300);
        }
        for (i = 0; i < 3; i++) {
            $('.dots li').eq(i).animate({
                top: "0px"

            }, (i * 100) + 300);

        }
        $('.dots li').eq(i).animate({
            top: "0px"
        }, 600, function () {
            Email = $('.email').val();
            Pass = $('.password').val();
            $.ajax({
                url: '/Welcome/LoginView',
                type: 'POST',
                data: { email: Email, password: Pass },
                success: function (result) {
                   
                    
                    if (result.sum == "true")
                       
                        window.location.href = '../Home/Index';
                    else

                        $('.submit').addClass("faild").removeClass("disabled").val("Login Failed");
                }
            });
            });
        


    });
});

//LOGIN E-------------------------------------------------
//---------------------------------------------


$(function () {


    $('.Remail').click(function () {
        $(this).addClass('actived').siblings().removeClass('actived').removeClass('R');
    });

    $('.Rpassword').click(function () {
        $(this).addClass('actived').siblings().removeClass('actived');
        $('.RRpassword').addClass('R');
    });

    $('.RFname').click(function () {
        $(this).addClass('actived').siblings().removeClass('actived');
        $('.RLname').addClass('R');
    });
    $('.RLname').click(function () {
        $(this).addClass('actived').siblings().removeClass('actived').removeClass('R');
    });

    $('.RRpassword').click(function () {
        $(this).addClass('actived').siblings().removeClass('actived').removeClass('R');
    });
    $('.Rphone').click(function () {
        $(this).addClass('actived').siblings().removeClass('actived').removeClass('R');
    });
});
$(function () {
    $('.TypeSelect').click(function () {

        $(this).addClass('actived').siblings().removeClass('actived');
        $('.Ftype').prop('disabled', true);
    });

});


$(function () {
    var x = $('.Rcheckbox').val();
    if (x == '0') {
        $('.Rsubmit').addClass('disabled').prop("disabled", true);
    }
    $('.Rcheckbox').click(function () {
        var x = $('.Rcheckbox').val();

        if (x == '0') {
            $('.Rcheckbox').val('1');
            $('.Rsubmit').removeClass('disabled').prop("disabled", false);
        }
        else {
            $('.Rcheckbox').val('0');
            $('.Rsubmit').addClass('disabled').prop("disabled", true);
        }

    });

});
// pop up change ----------------------------------------------------------------------------------------------------------------------------------------------------------------
$(function () {
    $('.ToRegister').click(function () {
        $('.LoginPopUp').fadeOut(0);
        $('.PopUpBG').fadeOut('slow');
            $('.RegisterPopUp').fadeIn('slow');
        
     
       
       
        
        
       
    });
  
   
});

/* customer Layout -------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

$(function () {
    $('.NotDeliveredProjects').click(function () {
        $(this).addClass('active');
        $('.DeliveredProjects').removeClass('active');
        $('.DeliveredTable').fadeOut(0);
        $('.NotDeliveredTable').fadeIn('slow');
        
    });
    $('.DeliveredProjects').click(function () {
        $(this).addClass('active');
        $('.NotDeliveredProjects').removeClass('active');
        $('.NotDeliveredTable').fadeOut(0);
        $('.DeliveredTable').fadeIn('slow');

    });
});
/* pie chart */
$(function () {
    $('.EnableChart').click(function () {
        $('.Java').removeClass('disabled').prop("disabled", false);
        $('.Python').removeClass('disabled').prop("disabled", false);
        $('.Php').removeClass('disabled').prop("disabled", false);
        $('.Asp').removeClass('disabled').prop("disabled", false);
    });
    $('.ShowChart').click(function () {
        $('.Java').addClass('disabled').prop("disabled", true);
        $('.Python').addClass('disabled').prop("disabled", true);
        $('.Php').addClass('disabled').prop("disabled", true);
        $('.Asp').addClass('disabled').prop("disabled", true);
        var Java = $('.Java').val();
        var Python = $('.Python').val();
        var Php = $('.Php').val();
        var Asp = $('.Asp').val();
        
        var canvas = document.getElementById("MTPieChart");
        var ctx = canvas.getContext('2d');
     
        // Global Options:
        Chart.defaults.global.defaultFontColor = 'black';
        Chart.defaults.global.defaultFontSize = 16;

        var data = {
            labels: ["java ", "php", "python", "asp"],
            datasets: [
              {
                  fill: true,
                  backgroundColor: [
                      '#188c90',
                      'black',
                       'lightblue',
                  'yellow '],
                  
                  data: [Java, Php,Python, Asp],
                  // Notice the borderColor 
                  borderColor: ['white',
                      'white',
                       'white',
                    'white'],
                  borderWidth: [2, 2,2,2]
              }
            ]
        };

        // Notice the rotation from the documentation.

        var options = {
            title: {
                display: true,
                text: 'Qualifications',
                position: 'top',
            },
            rotation: -0.7 * Math.PI
        };


        // Chart declaration:
        var myBarChart = new Chart(ctx, {
            type: 'pie',
            data: data,
            options: options
        });

    });
    
   

});
/* pie chart */
$(function () {
    $('.MDEnableChart').click(function () {
        $('.MDJava').removeClass('disabled').prop("disabled", false);
        $('.MDPython').removeClass('disabled').prop("disabled", false);
        $('.MDPhp').removeClass('disabled').prop("disabled", false);
        $('.MDAsp').removeClass('disabled').prop("disabled", false);
    });
    $('.MDShowChart').click(function () {
        $('.MDJava').addClass('disabled').prop("disabled", true);
        $('.MDPython').addClass('disabled').prop("disabled", true);
        $('.MDPhp').addClass('disabled').prop("disabled", true);
        $('.MDAsp').addClass('disabled').prop("disabled", true);
        var Java = $('.MDJava').val();
        var Python = $('.MDPython').val();
        var Php = $('.MDPhp').val();
        var Asp = $('.MDAsp').val();

        var canvas = document.getElementById("MDPieChart");
        var ctx = canvas.getContext('2d');

        // Global Options:
        Chart.defaults.global.defaultFontColor = 'black';
        Chart.defaults.global.defaultFontSize = 16;

        var data = {
            labels: ["java ", "php", "python", "asp"],
            datasets: [
              {
                  fill: true,
                  backgroundColor: [
                      '#188c90',
                      'black',
                       'lightblue',
                  'yellow '],

                  data: [Java, Php, Python, Asp],
                  // Notice the borderColor 
                  borderColor: ['white',
                      'white',
                       'white',
                    'white'],
                  borderWidth: [2, 2, 2, 2]
              }
            ]
        };

        // Notice the rotation from the documentation.

        var options = {
            title: {
                display: true,
                text: 'Qualifications',
                position: 'top',
            },
            rotation: -0.7 * Math.PI
        };


        // Chart declaration:
        var myBarChart = new Chart(ctx, {
            type: 'pie',
            data: data,
            options: options
        });

    });



});






















