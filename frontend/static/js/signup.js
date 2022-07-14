/**
 * Settings for password strength checker
 */
const options = {
    translations: zxcvbnts['language-en'].translations,
    graphs: zxcvbnts['language-common'].adjacencyGraphs,
    dictionary: {
      ...zxcvbnts['language-common'].dictionary,
      ...zxcvbnts['language-en'].dictionary,
    },
}

/**
 * Apply settings for password strength checker
 */
zxcvbnts.core.zxcvbnOptions.setOptions(options);

const meter = document.getElementById('password-strength-meter');
const passwordStrengthText = document.getElementById('password-strength-text');

/**
 * Displays a toast alert on the webpage
 * @function makeToast
 * @param {string} alertText - The text that goes inside the alert
 * @param {number} alertX - The x-value for the alert
 * @param {number} alertY - The y-value for the alert
 */  
function makeToast(alertText,alertX,alertY){
	Toastify({
		text: alertText,
		className:"alert",
		duration: 2000,
		close: true,
		offset: {
			x: alertX, // horizontal axis - can be a number or a string indicating unity. eg: '2em'
			y: alertY// vertical axis - can be a number or a string indicating unity. eg: '2em'
		},
		stopOnFocus: true, // Prevents dismissing of toast on hover
		style: {
		background: "#f78783",
		},
		onClick: function(){} // Callback after click
	}).showToast();
}

/**
 * Checks to see if password is strong using zxcvbn
 * @function isPasswordStrong
 */
function isPasswordStrong(){
	passwordValue = password.value;
	result = zxcvbnts.core.zxcvbn(passwordValue);//use zxcvbn to obtain info on password strength

	return result.score==4 ? true : false; 	
}

/**
 * Performs input validation on email and password inputs
 * @function validateSignup
 */
function validateSignup(){

	const email=document.getElementById("email").value; 
	const password = document.getElementById('password').value;

	const validEmailRegEx=/^[a-zA-Z0-9_+&*-]+(?:\.[a-zA-Z0-9_+&*-]+)*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,7}$/;


	const isEmailValid=validEmailRegEx.test(email);// Make sure email address is somewhat ok
	const isPasswordValid=isPasswordStrong();// Make sure password isn't empty

	if(!isEmailValid){
		const xCoordRef=document.querySelector('#email').getBoundingClientRect().left // X
		const yCoordRef=document.querySelector('#email').getBoundingClientRect().top // Y
		const xCoordOffset=330;
		const yCoordOffset=22;

		const alertText="Please enter a valid email address";
	
		makeToast(alertText,xCoordRef-xCoordOffset,yCoordRef-yCoordOffset);

		return;
	}

	if(!isPasswordValid){	
		const xCoordRef=document.querySelector('#password').getBoundingClientRect().left; // X
		const yCoordRef=document.querySelector('#password ').getBoundingClientRect().top; // Y
		const xCoordOffset=320;
		const yCoordOffset=22;

		const alertText="Please enter a stronger password";

		makeToast(alertText,xCoordRef-xCoordOffset,yCoordRef-yCoordOffset);

		return;
	}

	//Hide the meter and strength test if successful
	meter.style.visibility="hidden";
	passwordStrengthText.style.visibility="hidden";

	var signUpSuccessful;

	//Initiate signup with AWS
	completedSignUp.completedSignUp(email, password).then((res) => {
		try {
			if (res[0].signUp == 'passed')
			{
				swal({title: "Great!",
				text: "Look out for a verification email - you can't log in unless you've verified :)!",
				icon: "success",
				button: true}).then((value) => {
					window.location.href = "/login"
				  });
			}
			else
			{
				swal("Oh no :(!", res[0].message, "error")
			}
		} catch (error) {
			console.log("error");
		}
	
	});
	
};


//Password strength ratings from zxcvbn
const strength = {
	0: "Worst",
	1: "Bad",
	2: "Weak",
	3: "Good",
	4: "Strong"
}

let passwordValue="";
let result="";

//Checks password strength when typing a password
password.addEventListener('input', function() {
	meter.style.visibility="visible";
	passwordStrengthText.style.visibility="visible";
	
	passwordValue = password.value;
	result = zxcvbnts.core.zxcvbn(passwordValue);//use zxcvbn to obtain info on password strength

	// Update the password strength meter
	meter.value = result.score;

	// Update the text indicator
	if (passwordValue !== "") {
		passwordStrengthText.innerHTML = "Password Strength: "+strength[result.score]+"</br>"+result.feedback.warning+"</br>"+result.feedback.suggestions; 
	} else {
		passwordStrengthText.innerHTML = "";
	}
});

function goBack(){
    window.location.href = "/";
}

