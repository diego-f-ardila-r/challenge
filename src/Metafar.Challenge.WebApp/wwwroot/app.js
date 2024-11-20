window.localStorageHelper = {
    saveToken: function (token) {
        localStorage.setItem('token', token);
    },
    getToken: function () {
        return localStorage.getItem('token');
    }
};