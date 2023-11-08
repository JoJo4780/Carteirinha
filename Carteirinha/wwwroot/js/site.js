document.getElementById('User_image').addEventListener('change', function (event) {
    const imagePreview = document.getElementById('preview_Image');
    var input_image = document.getElementById('Url_image');
    var preview = document.getElementById('preview');
    const file = event.target.files[0];
    var image_hide = document.getElementById("mensagem_image")
    var icone = document.getElementById("Icone")
    image_hide.style.display = "none"
    icone.style.display = "none"
    if (file) {
        // Leitura do arquivo e exibição da imagem
        const reader = new FileReader();
        reader.onload = function (e) {
            imagePreview.src = e.target.result;
            input_image.value = e.target.result;
            console.log(e.target.result)
            imagePreview.style.display = 'block';
            preview.style.display = 'block';
            
        };
        reader.readAsDataURL(file);
    } else {
        imagePreview.style.display = 'none';
        imagePreview.src = '#';
    }
});
