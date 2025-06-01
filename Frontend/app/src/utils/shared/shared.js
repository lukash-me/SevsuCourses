import Cookies from "js-cookie";

export function logIfFailure(controller) {
    if (controller.errors.length != 0) {
        console.log("Have Errors", controller.errors);
        return true;
    }
    else {
        return false;
    }
}

export function isFailure(result) {
    if (typeof result === "object" && "errors" in result) {
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