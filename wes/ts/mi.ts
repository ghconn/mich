function greeter(user: string) {
    return "welcome, " + user;
}
let user = "conn";

document.body.innerHTML = greeter(user);