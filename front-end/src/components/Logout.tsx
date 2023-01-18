export function logCurrentUserOut() {
    sessionStorage.removeItem("myId");
    sessionStorage.removeItem("myCart");
    return("");
}