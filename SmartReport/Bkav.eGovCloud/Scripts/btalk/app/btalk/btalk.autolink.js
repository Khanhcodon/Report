// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

(function () {
    'use strict';

    if (btalk.autolink) {
        return btalk.autolink;
    }

    btalk.autolink = {
        process: function (str) {
            if (!str) {
                return;
            }
            var link = str.autoLink();
            if (link) return link;
        }
    }
})();