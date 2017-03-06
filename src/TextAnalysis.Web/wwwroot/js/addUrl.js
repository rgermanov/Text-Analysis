(function () {
    var pageUrl = document.location.href;
    var http = new XMLHttpRequest();
    var postUrl = "http://text-analysis.azurewebsites.net/api/articles";
    var data = { url: pageUrl };

    http.open("POST", postUrl, true);
    http.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
    http.send(JSON.stringify(data));

    console.log(data);
})();