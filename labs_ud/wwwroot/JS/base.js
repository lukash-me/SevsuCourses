LogOut = () => {
    document.cookie = `id=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
    document.cookie = `role=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;

    window.location.href = "login.html";
}

menu = () => {
    $logoutButton = $(".logout").on("click", () => {
        LogOut();
    });
}



menu();