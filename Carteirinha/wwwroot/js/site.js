// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function readImage() {
    if (this.files && this.files[0]) {
        var file = new FileReader();
        file.onload = function (e) {
            refazerbox()
            document.getElementById("preview").src = e.target.result;
        };
        file.readAsDataURL(this.files[0]);
    }
}
function refazerbox() {
    var box = document.getElementById("icon")
    box.classList.add("hide")
}
