let input1 = document.getElementById('password-input');
let input2 = document.getElementById('password-input1');
let popup = document.getElementById('password-error-message');
let login = document.getElementById('login');
const passwordInput = document.getElementById("password-input");
const passwordErrorMessage = document.getElementById("password-error-message");
const submitButton = document.getElementById("submit-button");
const errorMessage = document.getElementById("error-message");
const usernameErrorMessage = document.getElementById("username-error-message");


input1.addEventListener('focus', function() {
    popup.style.display = 'block';
});

input2.addEventListener('focus', function() {
    popup.style.display = 'none';
});
login.addEventListener('focus', function() {
    popup.style.display = 'none';
});


passwordInput.addEventListener("input", () => {
    const password = passwordInput.value;
    const hasNumber = /\d/.test(password);
    const hasLetter = /[a-zA-Z]/.test(password);
    const hasSymbol = /[\W_]/.test(password);
    const hasMinLength = password.length >= 8;

    if (!hasNumber || !hasLetter || !hasSymbol || !hasMinLength) {
    let errorMessage = "Пароль должен содержать: ";
    if (!hasNumber) errorMessage += "хотя бы 1 цифру, ";
    if (!hasLetter) errorMessage += "хотя бы 1 латинскую букву, ";
    if (!hasSymbol) errorMessage += "хотя бы 1 спецсимвол, ";
    if (!hasMinLength) errorMessage += "минимум 8 знаков, ";
    errorMessage = errorMessage.slice(0, -2) + ".";
    passwordErrorMessage.textContent = errorMessage;
    submitButton.disabled = true;
    } else {
    passwordErrorMessage.textContent = "";
    submitButton.disabled = false;
    }
});

input1.addEventListener("input", checkInputs);
input2.addEventListener("input", checkInputs);

function checkInputs() {
    if (input1.value !== input2.value) {
    errorMessage.textContent = "Пароли должны совпадать";
    submitButton.disabled = true;
    } else {
    errorMessage.textContent = "";
    submitButton.disabled = false;
    }
}

login.addEventListener("input", () => {
    const username = login.value;
    const hasMinLength = username.length >= 4;
  
    if (!hasMinLength) {
      usernameErrorMessage.textContent = "Логин должен содержать не менее 4 символов.";
      submitButton.disabled = true;
    } else {
      usernameErrorMessage.textContent = "";
      submitButton.disabled = false;
    }
    if (username === "") {
        usernameErrorMessage.textContent = "";
        submitButton.disabled = true;
      }
  });