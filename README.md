# SecretVault
## PROJECT SCOPE
Create a platform where a user can create, edit and delete secrets that they want to keep track of. The users are able to make their secrets public so that other users can see them (anonymously).

# SETTING UP THE FRONT-END (locally):
- Clone the github repo
- Open the 'frontend' folder in your IDE
- Run the application (in VS Code I selected the Run tab and then Run Without Debugging"
- In the terminal you will get a message saying on which port it is running
- Navigate to that port to see the front-end locally (localhost:portNumber)

- To see the hosted front-end go to: //insert 
- Extra documentation for the front-end can be found in the github

# SETTING UP THE BACK-END:
- The database is running on a CloudFormation instance so you do not have do set up anything for that. See the ERD for the   layout of the database.
- The API is hosted on API Gateway but can be run locally by following these steps:
	- Clone the main branch on this repo to an appropriate folder
	- Ask one of the team members to send you the appsettings.json file
	- Add the appsettings.json file to the SecretVaultAPI folder
	- Open up SecretVaultAPI in an IDE (I used VS Code)
	- Run the API (if using VS code:
		- Open a new terminal
		- Make sure the path is pointing to the SecretVaultAPI folder
		- Type "dotnet run"
		- Go to "https://localhost:5001/swagger/index.html" to view the API)
		- At the moment you won't be able to run the endpoints from swagger because you need to use a token. You will need to use postman to test endpoints because swagger does not account for authentication.

## RUNDOWN OF API ENDPOINTS:
- GET /v1/Posts : Get all public posts
- POST /v1/Posts : Create a new post	
- PUT /v1/Posts : Edit a specific post
- PATCH /v1/Posts : Edit a spesific post
- DELETE /v1/Posts : Delete a spesific post
- GET /v1/Post/user : Get all posts of a spesific user
- GET /v1/Post/user/search : Get all user posts containing a certain string in the title
- GET /v1/Post/details : Get the details of a certain post

# EXTRA NEED-TO-KNOWS:
- Please ask for special accistance if you want to run tests through postman becuase you will need to set the header properly and add a token.
- You do not have to run the back- & front-end in a specific order, just make sure both are running while you are testing.
- Please be kind.



