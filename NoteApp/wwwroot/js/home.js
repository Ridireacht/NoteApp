function saveImage(id) {
  const img = document.getElementById(id);
  const imgData = getBase64Image(img);
  localStorage.setItem(id, imgData);
}


function deleteImage(id) {
  localStorage.removeItem(id);
  const img = document.getElementById(id);
  img.src = "https://media.komus.ru/medias/sys_master/root/h10/h94/10568726741022/204993-1-800Wx800H.jpg";
}

function loadImage(id) {
  const img = document.getElementById(id);
  const imgData = localStorage.getItem(id);
  if (imgData) {
    const canvas = document.createElement("canvas");
    const image = new Image();
    image.onload = function() {
      canvas.width = img.width;
      canvas.height = img.height;
      const ctx = canvas.getContext("2d");
      ctx.drawImage(image, 0, 0, canvas.width, canvas.height);
      const dataURL = canvas.toDataURL("image/png");
      img.src = dataURL;
    }
    image.src = imgData;
  }
}

function getBase64Image(img) {
  const canvas = document.createElement("canvas");
  canvas.width = img.width;
  canvas.height = img.height;
  const ctx = canvas.getContext("2d");
  ctx.drawImage(img, 0, 0);
  const dataURL = canvas.toDataURL("image/png");
  return dataURL;
}

function getTotalImageSize() {
  let totalSize = 0;
  for (let i = 0; i < localStorage.length; i++) {
    const key = localStorage.key(i);
    const item = localStorage.getItem(key);
    if (key.startsWith("preview") && item.startsWith("data:image")) {
      totalSize += item.length * 2 / 1024; // размер в килобайтах
    }
  }
  return totalSize;
}

function previewFile(id, inputId) {
  const preview = document.getElementById(id);
  const file = document.getElementById(inputId).files[0];
  const reader = new FileReader();
  reader.addEventListener("load", function () {
    const image = new Image();
    image.onload = function() {
      const resizedImage = resizeImage(image, 375, 375);
      preview.src = resizedImage;
      saveImage(id);
    }
    image.src = reader.result;
  }, false);
  if (file) {
    reader.readAsDataURL(file);
  }
}
// хз понадобится или нет
function resizeImage(image, newWidth, newHeight) {
  // Создаем новый canvas
  const canvas = document.createElement('canvas');
  canvas.width = newWidth;
  canvas.height = newHeight;

  // Получаем контекст рисования
  const ctx = canvas.getContext('2d');

  // Рисуем изображение в контексте с измененным размером
  ctx.drawImage(image, 0, 0, newWidth, newHeight);

  // Возвращаем измененное изображение в формате base64
  return canvas.toDataURL('image/jpeg');
}

function deleteFile(id, inputId) {
  const preview = document.getElementById(id);
  preview.src = "https://media.komus.ru/medias/sys_master/root/h10/h94/10568726741022/204993-1-800Wx800H.jpg";
  deleteImage(id);
}

window.addEventListener("beforeunload", function() {
  saveImage("preview");
  saveImage("preview1");
  saveImage("preview2");
  saveImage("preview3");
  saveImage("preview4");
  saveImage("preview5");
  saveImage("preview6");
  saveImage("preview7");
  saveImage("preview8");
});

window.addEventListener("load", function () {
  loadImage("preview");
  loadImage("preview1");
  loadImage("preview2");
  loadImage("preview3");
  loadImage("preview4");
  loadImage("preview5");
  loadImage("preview6");
  loadImage("preview7");
  loadImage("preview8");
});



  const inputs = document.querySelectorAll('input[type="text"]');
  const saveButtons = document.querySelectorAll('.buttoni');
  // Добавляем обработчик клика на каждую кнопку сохранения
  saveButtons.forEach((button, index) => {
    button.addEventListener('click', () => {
      // Сохраняем значение поля ввода в localStorage с ключом, основанным на индексе
      localStorage.setItem(`text${index}`, inputs[index].value);
    });
  });
  
  // При загрузке страницы загружаем сохраненные значения текстовых полей
  window.addEventListener('load', () => {
    inputs.forEach((input, index) => {
      input.value = localStorage.getItem(`text${index}`) || '';
    });
  });