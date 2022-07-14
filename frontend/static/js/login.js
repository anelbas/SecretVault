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
 * Performs input validation on email and password inputs
 * @function validateLogin
 * @returns {boolean} Based on if the input is valid or not
 */
function validateLogin(){
    const email=document.getElementById("email").value;
    const password=document.getElementById("password").value;
    
    const validEmailRegEx=/^[a-zA-Z0-9_+&*-]+(?:\.[a-zA-Z0-9_+&*-]+)*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,7}$/;

    const isEmailValid=validEmailRegEx.test(email); // Make sure email address is somewhat ok
	const isPasswordValid=password.length>0; // Make sure password isn't empty

    if(!isEmailValid){
		const xCoordRef=document.querySelector('#email').getBoundingClientRect().left // X
		const yCoordRef=document.querySelector('#email').getBoundingClientRect().top // Y
		const xCoordOffset=330; // Hardcoding styling :)
		const yCoordOffset=22;

		const alertText="Please enter a valid email address";
	
		makeToast(alertText,xCoordRef-xCoordOffset,yCoordRef-yCoordOffset);

		return false;
	}

    if(!isPasswordValid){
        const xCoordRef=document.querySelector('#password').getBoundingClientRect().left // X
		const yCoordRef=document.querySelector('#password').getBoundingClientRect().top // Y
		const xCoordOffset=260; // Hardcoding styling :)
		const yCoordOffset=22;

		const alertText="Please enter a password";
	
		makeToast(alertText,xCoordRef-xCoordOffset,yCoordRef-yCoordOffset);

        return false;
    }

    return true;
}

/**
 * Performs authentication on email and password inputs
 * @function authenticateLogin
 */
function authenticateLogin(){

    const email=document.getElementById("email").value;
    const password=document.getElementById("password").value;

    //Check for valid inputs
    if(!validateLogin()){
        return;
    }

    completedSignIn.completedSignIn(email,password).then((res) => {
        try {
			if (res.status == 200)
			{
				swal({title: "Nice!",
				text: "Hello fellow... secret person!",
				icon: "success",
				button: true}).then((value) => {
					window.location.href = "/mySecrets"
				  });
			}
			else
			{
				swal("Oh no :(!", res.message, "error")
			}
		} catch (error) {
			console.log("error");
		}
    });

};

function goBack(){
    window.location.href = "/";
}
