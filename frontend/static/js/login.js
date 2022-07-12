

//TODO: Call the endpoint with the validated information
function authenticateLogin(){

    const email=document.getElementById("email").value;
    const password=document.getElementById("password").value;

    completedSignIn.completedSignIn(email,password);

};
