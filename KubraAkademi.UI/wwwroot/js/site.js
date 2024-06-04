
var token = localStorage.getItem("token")
var userRoles = [];

$(".logged-in-admin").hide();

if (token == null) {

    $(".not-login").show();
    $(".logged-in").hide();

} else {
    $(".not-login").hide();
    $(".logged-in").show();
    var payload = parseJwt(token);
    var username = payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
    userRoles = payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

    $("#UserName").text(username);

    if (userRoles.includes("Admin")) {
        $(".logged-in-admin").show();
    }

    console.log(userRoles);
}



function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);

}
$("#logout").click(function () {
    localStorage.removeItem("token");
    location.href = "/";
});