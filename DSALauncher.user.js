// ==UserScript==
// @name         DSALauncher
// @namespace    https://lukeiam.github.com/
// @version      0.1
// @description  try to take over the world!
// @author       lukeIam
// @match        https://de.wiki-aventurica.de/wiki/*
// @grant        none
// @require      https://code.jquery.com/jquery-3.3.1.slim.min.js
// ==/UserScript==

(function() {
    'use strict';
    var port = 7964;
    $.map($("#Publikationen").parent().nextUntil("h2").find('li'), function( e, i ) {
        var name = $(e).children('a').first().text();
        e.innerHTML = e.innerHTML.replace(/(?<=[^.] )[0-9]+/g,
            function(match) {
                return '<a href="http://127.0.0.1:' + port + '/?book=' + name + '&page=' + match + '" target="_blank" >' + match + '</a>';
            });
    });
})();