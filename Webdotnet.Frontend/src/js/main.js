const App = () => {
    let cookiesContainer = $(".js-cookies-container");
    let cookiesButton = cookiesContainer.find(".js-cookies-button");
    let cookieName = "webdotnet-cookie"
    let cookieValue = $.cookie(cookieName);
    if(cookieValue === undefined || cookieValue != "agreed"){
        cookiesContainer.show();
    }
    cookiesButton.click( () => {
        $.cookie(cookieName, "agreed", { expires: 365 })
        cookiesContainer.hide();
    });
};
App();
