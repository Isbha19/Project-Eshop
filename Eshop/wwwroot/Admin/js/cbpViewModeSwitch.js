document.addEventListener('DOMContentLoaded', function () {
    // Your existing cbpviewmodeswitch.js code here
    var container = document.getElementById('cbp-vm'),
        optionSwitch = Array.prototype.slice.call(container.querySelectorAll('div.cbp-vm-options > a'));

    function init() {
        optionSwitch.forEach(function (el, i) {
            el.addEventListener('click', function (ev) {
                ev.preventDefault();
                _switch(this);
            }, false);
        });
    }

    function _switch(opt) {
        optionSwitch.forEach(function (el) {
            classie.remove(container, el.getAttribute('data-view'));
            classie.remove(el, 'cbp-vm-selected');
        });
        classie.add(container, opt.getAttribute('data-view'));
        classie.add(opt, 'cbp-vm-selected');
    }

    init();
});
