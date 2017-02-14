(function () {
    var scriptUrl = "{{@domain}}/js/addUrl.js";
    s = document.createElement('script');
    s.type = 'text/javascript';
    s.src = scriptUrl;
    document.body.appendChild(s);
})();