window.cookieConfiguration = {
    getCookie: function (cookieName) {
        var cookie = Cookies.get(cookieName);
        return cookie;
    }
};