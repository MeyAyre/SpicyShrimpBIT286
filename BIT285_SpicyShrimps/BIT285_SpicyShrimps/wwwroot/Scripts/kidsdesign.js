//Gem moving code was adopted from https://codepen.io/jonathan/pen/wixFp by Jonathan Marzullo
//Using GSAP

$(document).ready(function () {

    //Gem moving code
    var totalElements = 0;
    var elementAnimationDelay = 0;
    var elementCountUp = 0;
    //Easter Egg
    var magicsound = "Sounds/fairy-wand.wav";

    function elementMoveDown(elementItem) {
        if (elementCountUp != totalElements) {
            elementCountUp++;
        } else {
            elementAnimationDelay = 0; //no delay after initial run
        }

        TweenMax.to(elementItem, 1.3, {
            delay: elementAnimationDelay,
            y: "+=20px",
            yoyo: true,
            repeat: -1,
            ease: Power2.easeOut//,
            //onCompleteParams:[elementItem],
            //onComplete: elementMoveUp
        });
    }
    function initPhoneHovering() {
        $(".gems > div[id]").each(function () {
            totalElements++;
            elementAnimationDelay += 0.4;

            var targetElement = $("#element-" + totalElements);

            //init animation
            elementMoveDown(targetElement);
        });
    }

    initPhoneHovering();

    //Easter Eggs
    $('.gems').hover(function () {
        console.log("PLEASE");
        $('#audio').html('<audio autoplay><source src= ' + magicsound + '></audio>');
    });
});