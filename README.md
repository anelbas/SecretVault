# SecretVault
## PROJECT SCOPE
Create a platform where a user can create, edit and delete secrets that they want to keep track of. The users are able to make their secrets public so that other users can see them (anonymously).

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
		- At the moment you won't be able to run the endpoints from swagger because you need to use a token. You 		   will need to use postman to test endpoints.

## RUNDOWN OF API ENDPOINTS:
- GET /v1/Posts : Get all public posts
- POST /v1/Posts : Create a new post	
- PUT /v1/Posts : Edit a specific post
- PATCH /v1/Posts : Edit a spesific post
- DELETE /v1/Posts : Delete a spesific post
- GET /v1/Post/user : Get all posts of a spesific user
- GET /v1/Post/user/search : Get all user posts containing a certain string in the title
- GET /v1/Post/details : Get the details of a certain post

## EXTRA NEED-TO-KNOWS:
- Most of the security is handled by cognito so it will be less secure when ran locally.
- Please ask for special accistance if you want to run tests through postman becuase you will need to provide an id token     for starters


## SETTING UP THE FRONT-END:
