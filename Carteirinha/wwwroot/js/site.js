document.getElementById('User_image').addEventListener('change', function (event) {
    const imagePreview = document.getElementById('preview_Image');
    var input_image = document.getElementById('Url_image');
    const file = event.target.files[0];
    var image_hide = document.querySelectorAll(".image")
    console.log(image_hide)
    image_hide.forEach(function (e) {
        e.style.display = "none"
    })
    if (file) {
        // Leitura do arquivo e exibição da imagem
        const reader = new FileReader();
        reader.onload = function (e) {
            imagePreview.src = e.target.result;
            input_image.value = e.target.result;
            imagePreview.style.display = 'block';
        };
        reader.readAsDataURL(file);
    } else {
        imagePreview.style.display = 'none';
        imagePreview.src = '#';
    }
});
import { Tabulator } from 'https://unpkg.com/tabulator-tables@5.5.2/dist/js/tabulator_esm.min.js';
var table = new Tabulator("#example-table", {
    height: "311px",
    columns: [
        { title: "Name", field: "name" },
        { title: "Progress", field: "progress", sorter: "number" },
        { title: "Gender", field: "gender" },
        { title: "Rating", field: "rating" },
        { title: "Favourite Color", field: "col" },
        { title: "Date Of Birth", field: "dob", hozAlign: "center" },
    ],
});