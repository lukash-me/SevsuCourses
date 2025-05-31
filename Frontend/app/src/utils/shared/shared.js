import Cookies from "js-cookie";

export function logResultIfFailure(result) {
    if (typeof result === "object" && "errors" in result) {
        console.log("Have Errors", result.errors);
        return true;
    }
    else {
        return false;
    }
}

export function getId() {
    return Cookies.get("id");
}

export function getRole() {
    return Cookies.get("role");
}