

//TODO: Call the endpoint with the validated information

//Options for password strength checker
const options = {
    translations: zxcvbnts['language-en'].translations,
    graphs: zxcvbnts['language-common'].adjacencyGraphs,
    dictionary: {
      ...zxcvbnts['language-common'].dictionary,
      ...zxcvbnts['language-en'].dictionary,
    },
}
  
zxcvbnts.core.zxcvbnOptions.setOptions(options);

const meter = document.getElementById('password-strength-meter');
const passwordStrengthText = document.getElementById('password-strength-text');

  
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

function isPasswordStrong(){
	passwordValue = password.value;
	result = zxcvbnts.core.zxcvbn(passwordValue);//use zxcvbn to obtain info on password strength

	return result.score==4 ? true : false; 	
}
  
function validateSignup(){

	const email=document.getElementById("email").value;
	const username=document.getElementById("username").value; 
	const password = document.getElementById('password').value;

	const validEmailRegEx=/^[a-zA-Z0-9_+&*-]+(?:\.[a-zA-Z0-9_+&*-]+)*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,7}$/;

	const validUsernameRegEx=/^[^'";:/\\ ]*$/;

	const isEmailValid=validEmailRegEx.test(email);
	const isUsernameValid=validUsernameRegEx.test(username)&&username.length>=5;
	const isPasswordValid=isPasswordStrong();

	if(!isEmailValid){
		const xCoordRef=document.querySelector('#email').getBoundingClientRect().left // X
		const yCoordRef=document.querySelector('#email').getBoundingClientRect().top // Y
		const xCoordOffset=330;
		const yCoordOffset=22;

		const alertText="Please enter a valid email address";
	
		makeToast(alertText,xCoordRef-xCoordOffset,yCoordRef-yCoordOffset);

		return;
	}

	// if(!isUsernameValid){
	// 	const xCoordRef=document.querySelector('#username').getBoundingClientRect().left; // X
	// 	const yCoordRef=document.querySelector('#username ').getBoundingClientRect().top; // Y
	// 	const xCoordOffset=350;
	// 	const yCoordOffset=19;

	// 	const alertText="Please enter a valid username\n\u2022 No spaces or these characters: ' ; : /\\ \"\ \n\u2022 At least 5 characters";

	// 	makeToast(alertText,xCoordRef-xCoordOffset,yCoordRef-yCoordOffset);

	// 	return;
		
	// }

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

	console.log(email, password)
	completedSignUp.completedSignUp(email, password);

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

