document.addEventListener('DOMContentLoaded', function () {
    document.querySelector("#cookieConsentdiv img[data-cookie-string]").addEventListener('click', function () {
        document.cookie = this.getAttribute("data-cookie-string");
        document.querySelector("#cookieConsentdiv").style.display = 'none';
    });
});