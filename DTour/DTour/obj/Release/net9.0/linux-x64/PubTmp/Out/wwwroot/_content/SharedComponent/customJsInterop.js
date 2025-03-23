function Effective1($, e, t) {
    let a = document.getElementById($);
    a.style.background = "#000";
    let n = 2 * Math.PI, r, i,
        l = ((r = document.createElement("div")).style.textAlign = "center", a.append(r), (i = document.createElement("canvas")).width = i.height = 2 * e, r.append(i), i.getContext("2d"));

    function o() {
        this.RK1 = 80 + 80 * Math.random(), this.GK1 = 80 + 80 * Math.random(), this.BK1 = 80 + 80 * Math.random(), this.RK2 = n * Math.random(), this.GK2 = n * Math.random(), this.BK2 = n * Math.random(), this.set = () => {
            let $ = Math.round(159 + 96 * Math.cos(this.RK2 + _ / this.RK1)),
                e = Math.round(159 + 96 * Math.cos(this.GK2 + _ / this.GK1)),
                t = Math.round(159 + 96 * Math.cos(this.BK2 + _ / this.BK1));
            this.v = "rgb(" + $ + "," + e + "," + t + ")"
        }, this.set()
    }

    l.setTransform(1, 0, 0, 1, e, e), l.globalAlpha = .7, onresize = () => {
        let $ = Math.min(window.innerWidth, window.innerHeight) - 40;
        l.canvas.style.width = l.canvas.style.height = ($ < t ? $ : t) + "px"
    };
    var s = 1e3, h = $ => {
        d || ($ < s ? requestAnimationFrame(h) : requestAnimationFrame(c))
    }, _ = 0, d = !0, c = $ => {
        if (!d) {
            _++, u();
            for (let e = 0; e < y.length; e++) y[e].set();
            K(), _ % 400 == 0 ? (_ % 1600 == 0 && x(), s = performance.now() + 3200, requestAnimationFrame(h)) : requestAnimationFrame(c)
        }
    }, v = () => {
        d ? (d = !1, requestAnimationFrame(c)) : d = !0
    };
    l.canvas.addEventListener("click", v, !1);
    let y = [, , , , ,];
    for (let f = 0; f < 5; f++) y[f] = new o;
    onresize();
    var m = [, , , , ,], x = () => {
        let $ = 0;
        for (let e = 0; e < m.length; e++) .5 > Math.random() ? m[e] = 800 : m[e] = 1600, $ += m[e];
        (8e3 === $ || 4e3 === $) && (m = [1600, 800, 1600, 800, 1600])
    };
    x();
    var g = [, , , , ,];
    let u = () => {
        let $ = 0;
        for (let e = 0; e < 5; e++) g[e] = (1 + Math.cos(n * _ / m[e])) / 2, $ += g[e];
        for (let t = 0; t < 5; t++) g[t] = g[t] / $
    };
    u();
    var b = [, , , , ,], p = ($, t, a, r, i) => {
        if ($ > 4) return;
        b[$].moveTo(t, a);
        let l = g[$] * e, o = t + l * Math.cos(r), s = a + l * Math.sin(r);
        i ? b[$].arc(o, s, l, r - n / 2, r + n / 4, i) : b[$].arc(o, s, l, r + n / 2, r + 3 * n / 4, i);
        let h = o + l * Math.cos(r + 3 * n / 4), _ = s + l * Math.sin(r + 3 * n / 4), d = o + l * Math.cos(r + n / 4),
            c = s + l * Math.sin(r + n / 4);
        i ? (p($ + 1, d, c, r + n / 4, !1), p($ + 1, d, c, r - n / 4, !0)) : (p($ + 1, h, _, r + n / 4, !1), p($ + 1, h, _, r - n / 4, !0))
    }, K = () => {
        for (let $ = 0; $ < 5; $++) b[$] = new Path2D;
        for (let e = 0; e < 4; e++) p(0, 0, 0, e * n / 4, e % 2), p(0, 0, 0, e * n / 4, !(e % 2));
        for (let t = b.length - 1; t >= 0; t--) l.lineWidth = 6, l.strokeStyle = "#00000020", l.stroke(b[t]), l.lineWidth = 4, l.strokeStyle = y[t].v, l.stroke(b[t])
    };
    v()
}
function Effective2($, e, t) {
    let a = document.getElementById($);
    a.style.background = "#000";
    let n = 2 * Math.PI, r, i,
        l = ((r = document.createElement("div")).style.textAlign = "center", a.append(r), (i = document.createElement("canvas")).width = i.height = 2 * e, r.append(i), i.getContext("2d"));
    l.translate(e, e), onresize = () => {
        let $ = Math.min(window.innerWidth, window.innerHeight) - 40;
        l.canvas.style.width = l.canvas.style.height = ($ < t ? $ : t) + "px"
    };
    let o = ($, e, t) => t ? Math.floor(Math.random() * Math.random() * (e - $)) + $ : Math.floor(Math.random() * (e - $)) + $;
    var s = [], h = Math.random() / 200, _ = Math.random() / 200, d = Math.random() / 200, c = () => {
        s = [];
        for (let $ = 0; $ < 4; $++) {
            let e = .001;
            e = 2 == $ ? s[0] : Math.random() / 250, s.push(e), s.push(-e)
        }
        h = Math.random() / 200, _ = Math.random() / 200, d = Math.random() / 200
    };
    c();
    let v = {
        a1: n * Math.random(), a2: n * Math.random(), a3: n * Math.random(), r1: 0, r2: 0, r3: 0, move() {
            v.a1 += h, v.a1 > n && (v.a1 = v.a1 - n), v.r1 = 80 + (e - 140) * (1 + Math.sin(v.a1)) / 2, v.a2 += _, v.a2 > n && (v.a2 = v.a2 - n), v.r2 = 60 + (v.r1 - 120) * (1 + Math.sin(v.a2)) / 2, v.a3 += d, v.a3 > n && (v.a3 = v.a3 - n), v.r3 = 40 + (v.r2 - 100) * (1 + Math.sin(v.a3)) / 2
        }
    }, y = function ($) {
        this.a = n * $ / b + 3 * n / 64, this.move = () => {
            this.a += s[$ % 8], this.a > n && (this.a = this.a - n), this.a < 0 && (this.a = this.a + n);
            let t = Math.cos(this.a), a = Math.sin(this.a);
            this.x0 = e * t, this.y0 = e * a, this.x1 = v.r1 * t, this.y1 = v.r1 * a, this.x2 = v.r2 * t, this.y2 = v.r2 * a, this.x3 = v.r3 * t, this.y3 = v.r3 * a
        }
    };
    var f = ($, e, t) => {
        l.beginPath(), l.arc($, e, 3, 0, n), l.closePath(), t ? l.fillStyle = t : l.fillStyle = "red", l.fill()
    };

    function m() {
        x ? (requestAnimationFrame(u), x = !1) : x = !0
    }

    l.canvas.addEventListener("click", m, !1);
    var x = !0, g = 0;

    function u($) {
        x || (++g % 100 == 0 && (T = ++T % 360, E = (E + 2) % 360, k = (k + 3) % 360), g % 1e3 == 0 && c(), C(), w(), requestAnimationFrame(u))
    }

    onresize();
    let b = 32;
    var p = [];
    for (let K = 0; K < b; K++) p.push(new y(K));
    var T = o(0, 360), E = o(0, 360), k = o(0, 360), w = () => {
        l.clearRect(-e, -e, 2 * e, 2 * e);
        for (let $ = 0; $ < p.length; $++) {
            let t = ($ + 1) % p.length, a = p[t].a - p[$].a;
            if (a < 0 && (a += n), a < .0023) continue;
            l.beginPath(), l.moveTo(p[$].x1, p[$].y1), l.bezierCurveTo(p[$].x0, p[$].y0, p[t].x0, p[t].y0, p[t].x1, p[t].y1), l.bezierCurveTo(p[t].x2, p[t].y2, p[$].x2, p[$].y2, p[$].x1, p[$].y1);
            let r = Math.round(360 * a);
            l.fillStyle = "hsla(" + (r + T) % 360 + ",100%,50%,0.67)", l.fill(), l.beginPath(), l.moveTo(p[$].x2, p[$].y2), l.bezierCurveTo(p[$].x1, p[$].y1, p[t].x1, p[t].y1, p[t].x2, p[t].y2), l.bezierCurveTo(p[t].x3, p[t].y3, p[$].x3, p[$].y3, p[$].x2, p[$].y2), l.fillStyle = "hsla(" + (r + E) % 360 + ",100%,50%,0.67)", l.fill(), l.beginPath(), l.moveTo(p[$].x3, p[$].y3), l.bezierCurveTo(p[$].x2, p[$].y2, p[t].x2, p[t].y2, p[t].x3, p[t].y3), l.bezierCurveTo(0, 0, 0, 0, p[$].x3, p[$].y3), l.fillStyle = "hsla(" + (r + k) % 360 + ",100%,50%,0.67)", l.fill()
        }
        f(0, 0, "silver")
    }, C = () => {
        v.move();
        for (let $ = 0; $ < p.length; $++) p[$].move();
        p.sort(($, e) => $.a - e.a)
    };
    C(), m()
}
!function (e) {
    Number.prototype.format = function (e, t, a, n) {
        var o = "\\d(?=(\\d{" + (t || 3) + "})+" + (e > 0 ? "\\D" : "$") + ")",
            i = this.toFixed(Math.max(0, ~~e));
        return (n ? i.replace(".", n) : i).replace(new RegExp(o, "g"), "$&" + (a || "."))
    },
        e.fn.castSelect2 = function () {
            const e = this.val();
            return null != e ? e.toString() : e
        },
        e.fn.setValueSelect2 = function (e) {
            this.val(e),
                this.trigger("change")
        },
        e.fn.select2a = function (e) {
            this.select2({
                placeholder: sselectoption,
                allowClear: !0,
                data: e
            })
        },
        e.fn.select2an = function (e) {
            this.empty(),
                this.select2({
                    placeholder: sselectoption,
                    data: e
                })
        }
}(jQuery);
window.initAutocompleteLimit = (c, o, i) => {
    let autocomplete;
    let address1Field;
    fillInAddressLimit = (autocomplete) => {

        const place = autocomplete.getPlace();
        let address1 = "";
        let dataInfo = {
            'address': '',
            'postcode': '',
            'locality': '',
            'subLocality': '',
            'state': '',
            'country': '',
            'countryCode': ''
        }
        if (place) {
            for (const component of place.address_components) {
                const componentType = component.types[0];
                switch (componentType) {
                    case "street_number": {
                        dataInfo.address = `${component.long_name} ${address1}`;
                        break;
                    }
                    case "route": {
                        dataInfo.address += component.long_name;
                        break;
                    }
                    case "postal_code": {
                        dataInfo.postcode = `${component.long_name}${dataInfo.postcode}`;
                        break;
                    }
                    case "postal_code_suffix": {
                        dataInfo.postcode = `${dataInfo.postcode}-${component.long_name}`;
                        break;
                    }
                    case "locality":
                        dataInfo.locality = component.long_name;
                        break;
                    case "sublocality_level_1":
                        dataInfo.subLocality = component.long_name;
                        break;
                    case "administrative_area_level_1": {
                        dataInfo.state = component.short_name;
                        break;
                    }
                    case "country":
                        dataInfo.country = component.long_name;
                        dataInfo.countryCode = component.short_name;
                        break;
                }
            }
            o.invokeMethodAsync("ApiPlaceInfo", dataInfo, i);
        }
    }
    address1Field = document.querySelector("#" + c);
    autocomplete = new google.maps.places.Autocomplete(address1Field, {
        componentRestrictions: {country: "us"},
        fields: ["address_components", "geometry"],
        types: ["address"],
    });
    autocomplete.addListener("place_changed", function () {
        fillInAddressLimit(autocomplete)
    });
};
window.initAutocomplete = (c, o, i) => {
    let autocomplete;
    let address1Field;
    fillInAddress = (autocomplete) => {

        const place = autocomplete.getPlace();
        let address1 = "";
        let dataInfo = {
            'address': '',
            'postcode': '',
            'locality': '',
            'subLocality': '',
            'state': '',
            'country': '',
            'countryCode': ''
        }
        if (place) {
            for (const component of place.address_components) {
                const componentType = component.types[0];
                switch (componentType) {
                    case "street_number": {
                        dataInfo.address = `${component.long_name} ${address1}`;
                        break;
                    }
                    case "route": {
                        dataInfo.address += component.long_name;
                        break;
                    }
                    case "postal_code": {
                        dataInfo.postcode = `${component.long_name}${dataInfo.postcode}`;
                        break;
                    }
                    case "postal_code_suffix": {
                        dataInfo.postcode = `${dataInfo.postcode}-${component.long_name}`;
                        break;
                    }
                    case "locality":
                        dataInfo.locality = component.long_name;
                        break;
                    case "sublocality_level_1":
                        dataInfo.subLocality = component.long_name;
                        break;
                    case "administrative_area_level_1": {
                        dataInfo.state = component.short_name;
                        break;
                    }
                    case "country":
                        dataInfo.country = component.long_name;
                        dataInfo.countryCode = component.short_name;
                        break;
                }
            }
            o.invokeMethodAsync("ApiPlaceInfo", dataInfo, i);
        }
    }
    address1Field = document.querySelector("#" + c);
    autocomplete = new google.maps.places.Autocomplete(address1Field, {
        fields: ["address_components", "geometry"],
        types: ["address"],
    });
    autocomplete.addListener("place_changed", function () {
        fillInAddress(autocomplete)
    });
};
window.setNumInputMask = () => {
    Inputmask("numeric", {
        radixPoint: ".",
        groupSeparator: ",",
        autoGroup: !0,
        digits: 3,
        allowPlus: !0,
        allowMinus: !0,
        clearMaskOnLostFocus: !1,
        removeMaskOnSubmit: !0,
        autoUnmask: !0,
        onUnMask: function (e, t) {
            return e.replace(/\,/g, "");
        }
    }).mask(".numberinputmask"),
        Inputmask({
            placeholder: "HH:MM",
            alias: "datetime",
            inputFormat: "HH:MM"
        }).mask(".timeinputmask")
};
window.ShowToastr = ((t, o = 0, e = "Portal System") => {
    0 === o ? toastr.success(t, e) : toastr.error(t, e)
});
window.ShowSweetAlert = ((t, w = 400, o = "Ok", e = "info") => {
    Swal.fire({
        html: t,
        icon: e,
        width: w,
        showClass: {popup: "animate__animated animate__fadeInDown"},
        hideClass: {popup: "animate__animated animate__fadeOutUp"},
        buttonsStyling: !1,
        confirmButtonText: o,
        customClass: {confirmButton: "btn btn-primary"}
    })
});
window.ShowConfirmSweetAlert = (async (t, w = 400, o = "Ok", e = "warning") => {
    return await Swal.fire({
        title: t,
        icon: e,
        showClass: {popup: "animate__animated animate__fadeInUp animate__faster"},
        hideClass: {popup: "animate__animated animate__fadeOutDown animate__faster"},
        showCancelButton: true,
        customClass: {confirmButton: "btn btn-primary", cancelButton: "btn btn-secondary"}
    }).then((result) => {
        return !!result.isConfirmed;
    });
});
window.setMaxLength = (() => {
    $(".maxlength").maxlength({
        warningClass: "badge badge-primary",
        limitReachedClass: "badge badge-success",
        appendToParent: !0
    });

});
window.setSelect2 = (dotnet) => {
    var e = !1;
    "undefined" != typeof jQuery && void 0 !== $.fn.select2 && ([].slice.call(document.querySelectorAll('[data-control="select2"], [data-kt-select2="true"]')).map(function (e) {
        var t = {dir: document.body.getAttribute("direction")};
        "true" === e.getAttribute("data-hide-search") && (t.minimumResultsForSearch = 1 / 0), $(e).select2(t).on("change", function () {
            const ctrId = $(this).attr('data-id');
            if (ctrId !== undefined) {
                dotnet.invokeMethodAsync('UpdateSelect2', $(this).val().toString(), ctrId);
            }
        })
    }), !1 === e && (e = !0, $(document).on("select2:open", function (e) {
        const t = document.querySelectorAll(".select2-container--open .select2-search__field");
        t.length > 0 && t[t.length - 1].focus()
    })))
};
window.setSelect2Value = ((e, t) => {
    const a = t.split(",");
    $("#" + e).val(a).trigger("change")
});
window.setSelect2aIndex = ((e, t, a, n, o = !1) => {
    0 === a.length ? $(t).select2({
        placeholder: "Select item...",
        allowClear: o
    }).on("select2:unselecting", function () {
        $(this).data("unselecting", !0)
    }).on("select2:opening", function (e) {
        $(this).data("unselecting") && ($(this).removeData("unselecting"), e.preventDefault())
    }).on("change.select2", function (t) {
        var a = $(this).castSelect2(),
            o = $(this).attr("data-id");
        e.invokeMethodAsync("Select2ACallbackIndex", a, o, n)
    }) : $(t).select2({
        placeholder: "Select item...",
        allowClear: o,
        data: a
    }).on("select2:unselecting", function () {
        $(this).data("unselecting", !0)
    }).on("select2:opening", function (e) {
        $(this).data("unselecting") && ($(this).removeData("unselecting"), e.preventDefault())
    }).on("change.select2", function (t) {
        const a = $(this).castSelect2(),
            o = $(this).attr("data-id");
        e.invokeMethodAsync("Select2ACallbackIndex", a, o, n)
    })
});
window.setSelect2Values = (e => {
    for (let a = 0; a < e.length; a++) {
        const t = e[a].text.split(",");
        $("#" + e[a].id).setValueSelect2(t);
    }
});
window.ToggleModal = (t => {
    $(t).modal("toggle");
});
window.ModalIsShow = (t => {
    return $(t).hasClass("show");
});
window.airDatePicker = () => {
    "undefined" != typeof jQuery && void 0 !== $.fn.select2 && ([].slice.call(document.querySelectorAll('[data-control="air-date-picker"]')).map(function (e) {
        var t = {dir: document.body.getAttribute("direction")};
        $(e).flatpickr({
            altInput: !0,
            dateFormat: "dM",
            altFormat: "dM"
        });
    }))
};
window.setRangeDatepicker = ((e, t, g = "RangeDatePickerCall") => {
    $(t).daterangepicker({
        autoApply: !0
    }).on("change", function (t) {
        const a = $(this).val();
        e.invokeMethodAsync(g, a)
    })
});
window.callClick = (e => {
    $(e).click();
});
window.initChart = ((e, a, c) => {
    $(e).empty();
    var element = document.querySelector(e);
    var height = parseInt(KTUtil.css(element, 'height'));
    var labelColor = KTUtil.getCssVariableValue('--kt-gray-900');
    var borderColor = KTUtil.getCssVariableValue('--kt-border-dashed-color');
    var options = {
        series: [{
            name: 'Labels',
            data: a
        }],
        chart: {
            fontFamily: 'inherit',
            type: 'bar',
            height: height,
            toolbar: {
                show: false
            }
        },
        plotOptions: {
            bar: {
                horizontal: false,
                columnWidth: ['22%'],
                borderRadius: 5,
                dataLabels: {
                    position: "top" // top, center, bottom
                },
                startingShape: 'flat'
            },
        },
        legend: {
            show: false
        },
        dataLabels: {
            enabled: true,
            offsetY: -28,
            style: {
                fontSize: '13px',
                colors: [labelColor]
            }
        },
        stroke: {
            show: true,
            width: 2,
            colors: ['transparent']
        },
        xaxis: {
            categories: c,
            axisBorder: {
                show: false,
            },
            axisTicks: {
                show: false
            },
            labels: {
                style: {
                    colors: KTUtil.getCssVariableValue('--kt-gray-500'),
                    fontSize: '13px'
                }
            },
            crosshairs: {
                fill: {
                    gradient: {
                        opacityFrom: 0,
                        opacityTo: 0
                    }
                }
            }
        },
        yaxis: {
            labels: {
                style: {
                    colors: KTUtil.getCssVariableValue('--kt-gray-500'),
                    fontSize: '13px'
                }
            }
        },
        fill: {
            opacity: 1
        },
        states: {
            normal: {
                filter: {
                    type: 'none',
                    value: 0
                }
            },
            hover: {
                filter: {
                    type: 'none',
                    value: 0
                }
            },
            active: {
                allowMultipleDataPointsSelection: false,
                filter: {
                    type: 'none',
                    value: 0
                }
            }
        },
        tooltip: {
            style: {
                fontSize: '12px'
            }
        },
        colors: [KTUtil.getCssVariableValue('--kt-primary'), KTUtil.getCssVariableValue('--kt-primary-light')],
        grid: {
            borderColor: borderColor,
            strokeDashArray: 4,
            yaxis: {
                lines: {
                    show: true
                }
            }
        }
    };
    var chart = new ApexCharts(element, options);
    chart.render();
});
window.destroyChart = (e => {
    $(e).empty();
});
window.datepicker = () => {
    "undefined" != typeof jQuery && ([].slice.call(document.querySelectorAll('[data-control="date-picker"]')).map(function (e) {
        var t = {dir: document.body.getAttribute("direction")};
        $(e).flatpickr({
            altInput: !0,
            altFormat: "d-M-Y",
            weekNumbers: !0
        });
    }))
};
window.mindatepicker = () => {
    "undefined" != typeof jQuery && ([].slice.call(document.querySelectorAll('[data-control="date-picker"]')).map(function (e) {
        const t = {dir: document.body.getAttribute("direction")};
        $(e).flatpickr({
            altInput: !0,
            altFormat: "d-M-Y",
            weekNumbers: !0,
            minDate: "today"
        });
    }))
};
window.datePicker2M = (m='single', format = "d-M-y") => {
    "undefined" != typeof jQuery && ([].slice.call(document.querySelectorAll('[data-control="date-picker"]')).map(function (e) {
        let t = {dir: document.body.getAttribute("direction")};
        $(e).flatpickr({
            altInput: !0,
            mode: m,
            dateFormat: "Y-m-d",
            altFormat: format,
            showMonths: 2,
        });
    }));
};

window.initNiceSelect = function (d, s='UpdateSelect2') {
    "undefined" != typeof jQuery && ([].slice.call(document.querySelectorAll('.nice-select')).map(function (e) {
        $(e).niceSelect();
        let t = $(e).attr('data-id');
        if (t !== undefined) {
            $(e).change(function (v) {
                d.invokeMethodAsync(s, v.target.value, t);
            });
        }
    }));
}
window.BlazorDownloadFile = ((e, t, a) => {
    o = new File([a], e, {
        type: t
    }),
        i = URL.createObjectURL(o),
        s = document.createElement("a"),
        document.body.appendChild(s),
        s.href = i,
        s.download = e,
        s.target = "_self",
        s.click(),
        URL.revokeObjectURL(i)
});

window.clearDataTable = ((t) => {
    if ($.fn.dataTable.isDataTable('#' + t)) {
        $("#" + t).DataTable().destroy();
        $("#" + t).empty();
        $("#" + t).off('click', 'td');
    }
});
window.dataTableBasic = ((t) => {
    $("#" + t).DataTable({
        language: {
            lengthMenu: "Show _MENU_"
        },
        "displayLength": 50,
        dom: "<'row'<'col-sm-6 d-flex align-items-center justify-conten-start'l><'col-sm-6 d-flex align-items-center justify-content-end'f>><'table-responsive'tr><'row'<'col-sm-12 col-md-5 d-flex align-items-center justify-content-center justify-content-md-start'i><'col-sm-12 col-md-7 d-flex align-items-center justify-content-center justify-content-md-end'p>>"
    })
});
window.dataTableBasicNoOrder = ((t) => {
    $("#" + t).DataTable({
        language: {
            lengthMenu: "Show _MENU_"
        },
        "ordering": false,
        "displayLength": 50,
        dom: "<'row'<'col-sm-6 d-flex align-items-center justify-conten-start'l><'col-sm-6 d-flex align-items-center justify-content-end'f>><'table-responsive'tr><'row'<'col-sm-12 col-md-5 d-flex align-items-center justify-content-center justify-content-md-start'i><'col-sm-12 col-md-7 d-flex align-items-center justify-content-center justify-content-md-end'p>>"
    })
});
window.dataTableComplex = ((t) => {
    $("#" + t).DataTable({
        language: {
            lengthMenu: "Show _MENU_"
        },
        "ordering": false,
        "columnDefs": [{
            "visible": false,
            "targets": -1
        }],
        "displayLength": 50,
        dom: "<'row'<'col-sm-6 d-flex align-items-center justify-conten-start'l><'col-sm-6 d-flex align-items-center justify-content-end'f>><'table-responsive'tr><'row'<'col-sm-12 col-md-5 d-flex align-items-center justify-content-center justify-content-md-start'i><'col-sm-12 col-md-7 d-flex align-items-center justify-content-center justify-content-md-end'p>>"
    })
});
window.dataTableFixHeader = ((t) => {
    $("#" + t).DataTable({
        language: {
            lengthMenu: "Show _MENU_"
        },
        "fixedHeader": {
            "header": true,
            "headerOffset": 5
        },
        "displayLength": 50,
        dom: "<'row'<'col-sm-6 d-flex align-items-center justify-conten-start'l><'col-sm-6 d-flex align-items-center justify-content-end'f>><'table-responsive'tr><'row'<'col-sm-12 col-md-5 d-flex align-items-center justify-content-center justify-content-md-start'i><'col-sm-12 col-md-7 d-flex align-items-center justify-content-center justify-content-md-end'p>>"
    })
});
window.dataTable = ((t, cl = 2) => {
    $("#" + t).DataTable({
        "scrollY": 500,
        "scrollX": true,
        "scrollCollapse": true,
        "paging": false,
        "bDestroy": true,
        fixedColumns: {
            left: cl
        },
        "dom":
            "<'row'" +
            "<'col-sm-12 col-md-5 d-flex align-items-center justify-content-center justify-content-md-start'i>" +
            "<'col-sm-12 col-md-7 d-flex align-items-center justify-content-center justify-content-md-end'f>" +
            "><'table-responsive'tr>",
    });
});
window.dataTableNoScroll = ((t, cl = 2) => {
    $("#" + t).DataTable({
        "paging": false,
        fixedColumns: {
            left: cl
        },
        "bDestroy": true,
        "dom":
            "<'row'" +
            "<'col-sm-12 col-md-5 d-flex align-items-center justify-content-center justify-content-md-start'i>" +
            "<'col-sm-12 col-md-7 d-flex align-items-center justify-content-center justify-content-md-end'f>" +
            "><'table-responsive'tr>",
    });
});

window.initPasswordMeter = ((id) => {
    t = document.querySelector("#" + id);
    o = KTPasswordMeter.getInstance(t.querySelector('[data-kt-password-meter="true"]'));
});

window.dateMask = (lang = "vi") => {
    Inputmask({
        alias: "datetime",
        inputFormat: lang === "vi" ? "dd/mm/yyyy" : "mm/dd/yyyy"
    }).mask('.date-mask')
};
window.dateMaskF = () => {
    Inputmask({
        alias: "datetime",
        inputFormat: "mm-dd-yyyy"
    }).mask('.date-maskF')
};
window.tagIfy = (control, data) => {
    var input = document.querySelector(control);
    new Tagify(input, {
        whitelist: data,
        maxTags: 10,
        dropdown: {
            maxItems: 20,
            classname: "tagify__inline__suggestions",
            enabled: 0,
            closeOnSelect: false
        }
    });
};
window.untitled = (id, CSIZE = 400, D = 500) => {
    const rnd = Math.floor(Math.random() * 10);
    if (rnd < 5) {
        Effective1(id, CSIZE, D);
    } else {
        Effective2(id, CSIZE, D);
    }
};
window.initEditor = (i, d, c = "EditorCall") => {
    const toolbarOptions = [
        ['bold', 'italic', 'underline', 'strike'],        // toggled buttons
        ['blockquote', 'code-block'],
        ['link', 'image', 'video', 'formula'],

        [{'list': 'ordered'}, {'list': 'bullet'}, {'list': 'check'}],
        [{'script': 'sub'}, {'script': 'super'}],      // superscript/subscript
        [{'indent': '-1'}, {'indent': '+1'}],          // outdent/indent
        [{'direction': 'rtl'}],                         // text direction

        [{'size': ['small', false, 'large', 'huge']}],  // custom dropdown
        [{'header': [1, 2, 3, 4, 5, 6, false]}],

        [{'color': []}, {'background': []}],          // dropdown with defaults from theme
        [{'font': []}],
        [{'align': []}],

        ['clean']                                         // remove formatting button
    ];
    var quill = new Quill('#' + i, {
        modules: {
            toolbar: toolbarOptions,
        },
        placeholder: 'Type your text here...',
        theme: 'snow' // or 'bubble'
    });
    quill.on('editor-change', (eventName, ...args) => {
        const a = quill.getSemanticHTML();
        d.invokeMethodAsync(c, i, a)
    });
}

window.selectCountry = (e, i = "country") => {
    const optionFormat = function (item) {
        if (!item.id) {
            return item.text;
        }

        const span = document.createElement('span');
        const imgUrl = item.element.getAttribute('data-select2-country');
        let template = '';

        template += '<img src="' + imgUrl + '" class="rounded-circle h-20px me-2" alt="image"/>';
        template += item.text;

        span.innerHTML = template;

        return $(span);
    };
    $('#' + i).select2({
        templateSelection: optionFormat,
        templateResult: optionFormat
    }).on("select2:unselecting", function () {
        $(this).data("unselecting", !0)
    }).on("select2:opening", function (e) {
        $(this).data("unselecting") && ($(this).removeData("unselecting"), e.preventDefault())
    }).on("change.select2", function (t) {
        let a = $(this).castSelect2(),
            o = $(this).attr("data-id");
        e.invokeMethodAsync("UpdateSelect2", a, o)
    });
}

window.MoveTop = () => {
    $("#kt_scrolltop").click();
};
window.FocusElement = (id) => {
    const element = document.getElementById(id);
    element.focus();
}
window.ToggleDropMenu = (e)=>{
    let menuElement = document.querySelector(e);
    let menu = KTMenu.getInstance(menuElement);
    menu.toggle();
};
window.initCursor = ()=>{
    (function ($) {
        function lerp(a, b, n) {
            return (1 - n) * a + n * b
        }

        class Cursor {
            constructor() {
                this.bind()
                this.cursor = document.querySelector('.js-cursor')

                this.mouseCurrent = {
                    x: 0,
                    y: 0
                }

                this.mouseLast = {
                    x: this.mouseCurrent.x,
                    y: this.mouseCurrent.y
                }

                this.rAF = undefined
            }

            bind() {
                ['getMousePosition', 'run'].forEach((fn) => this[fn] = this[fn].bind(this))
            }

            getMousePosition(e) {
                this.mouseCurrent = {
                    x: e.clientX,
                    y: e.clientY
                }
            }

            run() {
                this.mouseLast.x = lerp(this.mouseLast.x, this.mouseCurrent.x, 0.2)
                this.mouseLast.y = lerp(this.mouseLast.y, this.mouseCurrent.y, 0.2)

                this.mouseLast.x = Math.floor(this.mouseLast.x * 100) / 100
                this.mouseLast.y = Math.floor(this.mouseLast.y * 100) / 100

                this.cursor.style.transform = `translate3d(${this.mouseLast.x}px, ${this.mouseLast.y}px, 0)`

                this.rAF = requestAnimationFrame(this.run)
            }

            requestAnimationFrame() {
                this.rAF = requestAnimationFrame(this.run)
            }

            addEvents() {
                window.addEventListener('mousemove', this.getMousePosition, false)
            }

            on() {
                this.addEvents()

                this.requestAnimationFrame()
            }

            init() {
                this.on()
            }
        }

        if ($('.js-cursor').length > 0) {
            const cursor = new Cursor()
            cursor.init();
            $('a, button').hover(function () {
                $('.cursor').toggleClass('light');
            });
        }
    })(jQuery);
};
