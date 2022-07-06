let overlay = document.getElementById("overlay");
let overlay2 = document.getElementById("overlay2");
let popup = {
    login: document.getElementById("loginPopup"),
    create: document.getElementById("createPopup"),
}
let formData = {};

function initalizeForm() {
    formData = {
        login: {
            email: {
                value: null,
                isValid: false,
                isTouched: false,
                placeholder: "Eg. john@gmail.com",
                display: "Email address",
                error: "Please enter a valid email address",
                validation: /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
            },
            password: {
                value: null,
                isValid: false,
                isTouched: false,
                placeholder: "Enter your password",
                display: "Password",
                error: "Please enter a valid password",
                type: "password",
                validation: /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/
            }
        },
        create: {
            firstName: {
				value: null,
				isValid: false,
				isTouched: false,
                placeholder: "Eg. John",
                display: "First name",
                error: "Please enter a valid first name",
				validation: /^[a-zA-Z ]{2,50}$/
			},
			lastName: {
				value: null,
				isValid: false,
				isTouched: false,
                placeholder: "Eg. Smith",
                display: "Last name",
                error: "Please enter a valid last name",
				validation: /^[a-zA-Z ]{2,50}$/
			},
            email: {
                value: null,
                isValid: false,
                isTouched: false,
                placeholder: "Eg. john@gmail.com",
                display: "Email address",
                error: "Please enter a valid email address",
                validation: /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
            },
            password: {
                value: null,
                isValid: false,
                isTouched: false,
                placeholder: "Enter your password",
                display: "Password",
                error: "Please enter a valid password",
                type: "password",
                validation: /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/
            },
            userAge: {
                value: null,
                isValid: false,
                isTouched: false,
                placeholder: "Enter your age",
                display: "Age",
                error: "Please enter a valid Age",
                validation: /^[1-9]\d*$/,
                otherValidation: "age",
                hint: "Passwords should be 8 characters or more and must contain at least one digit."
            }
        }
    };

    for (let type in formData) {
        let inputsSection = document.getElementById(`${type}PopupInputs`);
        inputsSection.textContent = '';
        for (let key in formData[type]) {
            let article = document.createElement('article');
            article.className = "popup-input";
            let p = document.createElement('p');
            p.className = "popup-input-label";
            p.innerText = formData[type][key].display;
            let input = document.createElement('input');
            input.className = "popup-input-box";
            input.placeholder = formData[type][key].placeholder;
            input.onkeyup = () => setValue(input.value, key, type);
            input.type = formData[type][key].type ? formData[type][key].type : "text" ;
            let section = document.createElement('section');
            section.id = `${type}${key}`;
            section.className = "input-box-invalid-wrapper";
            let p2 = document.createElement('p');
            p2.className = "input-box-invalid-msg";
            p2.innerText = formData[type][key].error;
            section.appendChild(p2);
            let section2 = document.createElement('section');
            section2.id = `${type}${key}`;
            section2.className = "input-box-hint-wrapper";
            if (formData[type][key].hint) {
              let p3 = document.createElement('p');
              p3.className = "input-box-hint";
              p3.innerText = formData[type][key].hint;
              section2.appendChild(p3);
            }
            article.appendChild(p);
            article.appendChild(input);
            article.appendChild(section);
            article.appendChild(section2);
            inputsSection.appendChild(article);
            document.getElementById(`${type}${key}`).style.display = "none";
        }
        document.getElementById(`${type}Submit`).disabled = true;
    }
}

function setValue(value, key, type) {
    formData[type][key].value = value;
    formData[type][key].isTouched = true;
    checkValidity(type);
}

function checkValidity(type) {
    let isFormValid = true;

    for (let key in formData[type]) {
        if (formData[type][key].value) {
            formData[type][key].isValid = formData[type][key].validation ? formData[type][key].validation.test(formData[type][key].value) : true;
            if (formData[type][key].otherValidation) {
                if (formData[type][key].otherValidation === "age")
                    formData[type][key].isValid = parseInt(formData[type][key].value) > 12 && parseInt(formData[type][key].value) < 100 ? true : false;
            }
        }
        else {
            formData[type][key].isValid = false;
        }
        if (!formData[type][key].isValid) {
            document.getElementById(`${type}${key}`).style.display = formData[type][key].isTouched ? "flex" : "none";
            isFormValid = false;
        }
        else
            document.getElementById(`${type}${key}`).style.display = "none";
    }
    document.getElementById(`${type}Submit`).disabled = !isFormValid;
}

function openPopup(type) {
    initalizeForm();
    // overlay.style.display = 'block';
    // popup[type].style.display = 'block';
}

function closePopup (type) {
    // overlay.style.display = 'none';
    // popup[type].style.display = 'none';
}

function submit(type) {
    let payload = {};
    for (let key in formData[type]) {
        payload[key] = formData[type][key].value;
    }
    overlay2.style.display = 'block';
    if (type == 'login') {
        axios({
            method: "GET",
            url: `http://ec2-3-82-51-192.compute-1.amazonaws.com:3002/api/login`,
            params: payload
        }).then((data) => {
            console.log(data)
            sessionStorage.setItem("userDetails", JSON.stringify(data.data))
            window.location.href = "/home";
        }).catch((err) => {
            overlay2.style.display = 'none';
            console.log("help", err);
        })
    }
    else {
        axios({
            method: "POST",
            url: `http://ec2-3-82-51-192.compute-1.amazonaws.com:3002/api/signUp`,
            headers: {
                'Content-Type': 'application/json',
            },
            data: payload
        }).then((data) => {
            console.log(data)
            sessionStorage.setItem("userDetails", JSON.stringify(data.data.userDetails))
            window.location.href = "/profileSetup";
        }).catch((err) => {
            overlay2.style.display = 'none';
            console.log("help", err);
        })
    };
}