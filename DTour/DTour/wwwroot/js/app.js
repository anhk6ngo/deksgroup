(function ($) {
    "use strict";
    function runSlider(){
        $('.th-slider').each(function () {
            var thSlider = $(this);
            var settings = $(this).data('slider-options');

            // Store references to the navigation Slider
            var prevArrow = thSlider.find('.slider-prev');
            var nextArrow = thSlider.find('.slider-next');
            var paginationElN = thSlider.find('.slider-pagination.pagi-number');
            var paginationExternel = thSlider.siblings('.slider-controller').find('.slider-pagination');

            var paginationEl = paginationExternel.length ? paginationExternel.get(0) : thSlider.find('.slider-pagination').get(0);

            var paginationType = settings['paginationType'] ? settings['paginationType'] : 'bullets';
            var autoplayconditon = settings['autoplay'];

            var sliderDefault = {
                slidesPerView: 1,
                spaceBetween: settings['spaceBetween'] || 24,
                loop: settings['loop'] !== false,
                speed: settings['speed'] || 1000,
                autoplay: autoplayconditon || {
                    delay: 6000,
                    disableOnInteraction: false
                },
                navigation: {
                    nextEl: nextArrow.get(0),
                    prevEl: prevArrow.get(0),
                },
                pagination: {
                    el: paginationEl,
                    type: paginationType,
                    clickable: true,
                    renderBullet: function (index, className) {
                        var number = index + 1;
                        var formattedNumber = number < 10 ? '0' + number : number;
                        if (paginationElN.length) {
                            return '<span class="' + className + ' number">' + formattedNumber + '</span>';
                        } else {
                            return '<span class="' + className + '" aria-label="Go to Slide ' + formattedNumber + '"></span>';
                        }
                    },
                    formatFractionCurrent: function (number) {
                        return number < 10 ? '0' + number : number;
                    },
                    formatFractionTotal: function (number) {
                        return number < 10 ? '0' + number : number;
                    }
                },
                on: {
                    slideChange: function () {
                        setTimeout(function () {
                            swiper.params.mousewheel.releaseOnEdges = false;
                        }, 500);
                    },
                    reachEnd: function () {
                        setTimeout(function () {
                            swiper.params.mousewheel.releaseOnEdges = true;
                        }, 750);
                    }
                }
            };

            var options = JSON.parse(thSlider.attr('data-slider-options'));
            options = $.extend({}, sliderDefault, options);
            var swiper = new Swiper(thSlider.get(0), options); // Assign the swiper variable

            if ($('.slider-area').length > 0) {
                $('.slider-area').closest(".container").parent().addClass("arrow-wrap");
            }

            // Category slider specific wheel effect
            if (thSlider.hasClass('categorySlider')) {
                const multiplier = {
                    translate: 0.1,
                    rotate: 0.01
                };

                function calculateWheel() {
                    const slides = document.querySelectorAll('.single');
                    slides.forEach((slide) => {
                        const rect = slide.getBoundingClientRect();
                        const r = window.innerWidth * 0.5 - (rect.x + rect.width * 0.5);
                        let ty = Math.abs(r) * multiplier.translate - rect.width * multiplier.translate;

                        if (ty < 0) {
                            ty = 0;
                        }
                        const transformOrigin = r < 0 ? 'left top' : 'right top';
                        slide.style.transform = `translate(0, ${ty}px) rotate(${-r * multiplier.rotate}deg)`;
                        slide.style.transformOrigin = transformOrigin;
                    });
                }

                function raf() {
                    requestAnimationFrame(raf);
                    calculateWheel();
                }

                raf();
            }
        });
    }
    if ($('.scroll-top').length > 0) {

        var scrollTopbtn = document.querySelector('.scroll-top');
        var progressPath = document.querySelector('.scroll-top path');
        var pathLength = progressPath.getTotalLength();
        progressPath.style.transition = progressPath.style.WebkitTransition = 'none';
        progressPath.style.strokeDasharray = pathLength + ' ' + pathLength;
        progressPath.style.strokeDashoffset = pathLength;
        progressPath.getBoundingClientRect();
        progressPath.style.transition = progressPath.style.WebkitTransition = 'stroke-dashoffset 10ms linear';
        var updateProgress = function () {
            var scroll = $(window).scrollTop();
            var height = $(document).height() - $(window).height();
            var progress = pathLength - (scroll * pathLength / height);
            progressPath.style.strokeDashoffset = progress;
        }
        updateProgress();
        $(window).scroll(updateProgress);
        var offset = 50;
        var duration = 750;
        jQuery(window).on('scroll', function () {
            if (jQuery(this).scrollTop() > offset) {
                jQuery(scrollTopbtn).addClass('show');
            } else {
                jQuery(scrollTopbtn).removeClass('show');
            }
        });
        jQuery(scrollTopbtn).on('click', function (event) {
            event.preventDefault();
            jQuery('html, body').animate({
                scrollTop: 0
            }, duration);
            return false;
        })
    }
    window.initTheme = () => {
        KTApp.init();
        if ($("[data-bg-src]").length > 0) {
            $("[data-bg-src]").each(function () {
                var src = $(this).attr("data-bg-src");
                $(this).css("background-image", "url(" + src + ")");
                $(this).removeAttr("data-bg-src").addClass("background-image");
            });
        }
        if ($('[data-mask-src]').length > 0) {
            $('[data-mask-src]').each(function () {
                var mask = $(this).attr('data-mask-src');
                $(this).css({
                    'mask-image': 'url(' + mask + ')',
                    '-webkit-mask-image': 'url(' + mask + ')'
                });
                $(this).addClass('bg-mask');
                $(this).removeAttr('data-mask-src');
            });
        }
        runSlider();
        // Function to add animation classes
        function animationProperties() {
            $('[data-ani]').each(function () {
                var animationName = $(this).data('ani');
                $(this).addClass(animationName);
            });

            $('[data-ani-delay]').each(function () {
                var delayTime = $(this).data('ani-delay');
                $(this).css('animation-delay', delayTime);
            });
        }
        animationProperties();

        // Add click event handlers for external slider arrows based on data attributes
        $('[data-slider-prev], [data-slider-next]').on('click', function () {
            var sliderSelectors = ($(this).data('slider-prev') || $(this).data('slider-next')).split(', ');

            sliderSelectors.forEach(function (sliderSelector) {
                var targetSlider = $(sliderSelector);

                if (targetSlider.length) {
                    var swiper = targetSlider[0].swiper;

                    if (swiper) {
                        if ($(this).data('slider-prev')) {
                            swiper.slidePrev();
                        } else {
                            swiper.slideNext();
                        }
                    }
                }
            });
        });

        document.addEventListener('mouseenter', event => {
            const el = event.target;
            if (el && el.matches && el.matches('.swiper-container')) {
                // console.log('mouseenter');
                // console.log('autoplay running', swiper.autoplay.running);
                el.swiper.autoplay.stop();
                el.classList.add('swiper-paused');

                const activeNavItem = el.querySelector('.swiper-pagination-bullet-active');
                activeNavItem.style.animationPlayState = "paused";
            }
        }, true);

        document.addEventListener('mouseleave', event => {
            // console.log('mouseleave', swiper.activeIndex, swiper.slides[swiper.activeIndex].progress);
            // console.log('autoplay running', swiper.autoplay.running);
            const el = event.target;
            if (el && el.matches && el.matches('.swiper-container')) {
                el.swiper.autoplay.start();
                el.classList.remove('swiper-paused');

                const activeNavItem = el.querySelector('.swiper-pagination-bullet-active');

                activeNavItem.classList.remove('swiper-pagination-bullet-active');
                // activeNavItem.style.animation = 'none';

                setTimeout(() => {
                    activeNavItem.classList.add('swiper-pagination-bullet-active');
                    // activeNavItem.style.animation = '';
                }, 10);
            }
        }, true);

        $('.destination-list-wrap').on('click', function () {
            $(this).addClass('active').siblings().removeClass('active');
        });

        function showNextdestination() {
            var $activedestination = $('.destination-list-area .destination-list-wrap.active');
            if ($activedestination.next().length > 0) {
                $activedestination.removeClass('active');
                $activedestination.next().addClass('active');
            } else {
                $activedestination.removeClass('active');
                $('.destination-list-area .destination-list-wrap:first').addClass('active');
            }
        }

        function showPreviousdestination() {
            var $activedestination = $('.destination-list-area .destination-list-wrap.active');
            if ($activedestination.prev().length > 0) {
                $activedestination.removeClass('active');
                $activedestination.prev().addClass('active');
            } else {
                $activedestination.removeClass('active');
                $('.destination-list-area .destination-list-wrap:last').addClass('active');
            }
        }

        $('.destination-prev').on('click', function () {
            showPreviousdestination();
        });
        $('.destination-next').on('click', function () {
            showNextdestination();
        });


        // Show the first tab and hide the rest
        $('.accordion-item-wrapp li:first-child').addClass('active');
        $('.according-img-tab').hide();
        $('.according-img-tab:first').show();

        // Click function
        $('.accordion-item-wrapp .accordion-item-content').mouseenter(function () {
            $('.accordion-item-wrapp .accordion-item-content').removeClass('active');
            // $(this).addClass('active');
            $('.according-img-tab').hide();

            var activeTab = $(this).find('.accordion-tab-item').attr('data-bs-target');
            $(activeTab).fadeIn();
            return false;
        });

        $(document).on('mouseover', '.hover-item', function () {
            $(this).addClass('item-active');
            $('.hover-item').removeClass('item-active');
            $(this).addClass('item-active');
        });
        $(".popup-image").magnificPopup({
            type: "image",
            mainClass: 'mfp-zoom-in',
            removalDelay: 260,
            gallery: {
                enabled: true,
            },
        });
        $.fn.shapeMockup = function () {
            var $shape = $(this);
            $shape.each(function () {
                var $currentShape = $(this),
                    shapeTop = $currentShape.data("top"),
                    shapeRight = $currentShape.data("right"),
                    shapeBottom = $currentShape.data("bottom"),
                    shapeLeft = $currentShape.data("left");
                $currentShape
                    .css({
                        top: shapeTop,
                        right: shapeRight,
                        bottom: shapeBottom,
                        left: shapeLeft,
                    })
                    .removeAttr("data-top")
                    .removeAttr("data-right")
                    .removeAttr("data-bottom")
                    .removeAttr("data-left")
                    .parent()
                    .addClass("shape-mockup-wrap");
            });
        };
        if ($(".shape-mockup")) {
            $(".shape-mockup").shapeMockup();
        }
        "undefined" != typeof countUp && [].slice.call(document.querySelectorAll('[data-kt-countup="true"]:not(.counted)')).map((function(e) {
                if (KTUtil.isInViewport(e) && KTUtil.visible(e)) {
                    if ("1" === e.getAttribute("data-kt-initialized"))
                        return;
                    var t = {}
                        , n = e.getAttribute("data-kt-countup-value");
                    n = parseFloat(n.replace(/,/g, "")),
                    e.hasAttribute("data-kt-countup-start-val") && (t.startVal = parseFloat(e.getAttribute("data-kt-countup-start-val"))),
                    e.hasAttribute("data-kt-countup-duration") && (t.duration = parseInt(e.getAttribute("data-kt-countup-duration"))),
                    e.hasAttribute("data-kt-countup-decimal-places") && (t.decimalPlaces = parseInt(e.getAttribute("data-kt-countup-decimal-places"))),
                    e.hasAttribute("data-kt-countup-prefix") && (t.prefix = e.getAttribute("data-kt-countup-prefix")),
                    e.hasAttribute("data-kt-countup-separator") && (t.separator = e.getAttribute("data-kt-countup-separator")),
                    e.hasAttribute("data-kt-countup-suffix") && (t.suffix = e.getAttribute("data-kt-countup-suffix")),
                        new countUp.CountUp(e,n,t).start(),
                        e.classList.add("counted"),
                        e.setAttribute("data-kt-initialized", "1")
                }
            }
        ));
        window.addEventListener('contextmenu', function (e) {
            e.preventDefault();
        }, false);
        document.onkeydown = function (e) {
            if (e.key === 'F12') {
                return false;
            }
            if (e.ctrlKey && e.shiftKey && e.key === 'I') {
                return false;
            }
            if (e.ctrlKey && e.shiftKey && e.key === 'C') {
                return false;
            }
            if (e.ctrlKey && e.shiftKey && e.key === 'J') {
                return false;
            }
            if (e.ctrlKey && e.key === 'U') {
                return false;
            }
        };
    };
    window.initSlider = function () {
        runSlider()
    }
})(jQuery);



