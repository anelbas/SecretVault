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

function validateLogin(){
    const email=document.getElementById("email").value;
    const password=document.getElementById("password").value;
    
    const validEmailRegEx=/^[a-zA-Z0-9_+&*-]+(?:\.[a-zA-Z0-9_+&*-]+)*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,7}$/;

    const isEmailValid=validEmailRegEx.test(email);
	const isPasswordValid=password.length>0;

    if(!isEmailValid){
		const xCoordRef=document.querySelector('#email').getBoundingClientRect().left // X
		const yCoordRef=document.querySelector('#email').getBoundingClientRect().top // Y
		const xCoordOffset=330;
		const yCoordOffset=22;

		const alertText="Please enter a valid email address";
	
		makeToast(alertText,xCoordRef-xCoordOffset,yCoordRef-yCoordOffset);

		return false;
	}

    if(!isPasswordValid){
        const xCoordRef=document.querySelector('#password').getBoundingClientRect().left // X
		const yCoordRef=document.querySelector('#password').getBoundingClientRect().top // Y
		const xCoordOffset=260;
		const yCoordOffset=22;

		const alertText="Please enter a password";
	
		makeToast(alertText,xCoordRef-xCoordOffset,yCoordRef-yCoordOffset);

        return false;
    }

    return true;
}

function authenticateLogin(){

    const email=document.getElementById("email").value;
    const password=document.getElementById("password").value;

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
