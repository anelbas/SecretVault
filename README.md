# SecretVault
## Project Scope
Create a platform where a user can create, edit and delete secrets that they want to keep track of. The users are able to make their secrets public so that other users can see them (anonymously).

# SETTING UP THE FRONT-END (locally):
- Clone the github repo
- Open the 'frontend' folder in your IDE
- Run the application (in VS Code I selected the Run tab and then Run Without Debugging"
- In the terminal you will get a message saying on which port it is running
- Navigate to that port to see the front-end locally (localhost:portNumber)

- To see the hosted front-end go to: http://ec2-13-246-58-156.af-south-1.compute.amazonaws.com:3003/
- PLEASE NOTE: At the moment the front-end is connected to the local api because it is more secure so the above link will not work. Please test it while running everything locally. Don't try to test the hosted stuff, thank you!
- Extra documentation for the front-end can be found in github

# SETTING UP THE BACK-END:
- The database is running on a CloudFormation instance so you do not have do set up anything for that. See the ERD for the layout of the database.
- The link to the hosted API (not much to see there but here it is): https://bwjbt1ijm4.execute-api.eu-west-1.amazonaws.com/Prod
- The API is hosted on API Gateway but can be run locally by following these steps:
	- Clone the main branch on this repo to an appropriate folder
	- Ask one of the team members to send you the appsettings.json file
	- Add the appsettings.json file to the SecretVaultAPI folder
	- Open up SecretVaultAPI in an IDE (I used VS Code)
	- Run the API (if using VS code:
		- Open a new terminal
		- Make sure the path is pointing to the SecretVaultAPI folder
		- Type "dotnet run"
		- Go to "https://localhost:portNumberSpecifiedInTerminal/swagger/index.html" to view the API)
		- At the moment you won't be able to run the endpoints from swagger because you need to use a token. You will need to use postman to test endpoints because swagger does not account for authentication.

## Rundown of API endpoints:
- GET /v1/Posts : Get all public posts
- POST /v1/Posts : Create a new post	
- PUT /v1/Posts : Edit a specific post
- PATCH /v1/Posts : Edit a spesific post
- DELETE /v1/Posts : Delete a spesific post
- GET /v1/Post/user : Get all posts of a spesific user
- GET /v1/Post/user/search : Get all user posts containing a certain string in the title
- GET /v1/Post/details : Get the details of a certain post

## AWS Technologies uses:
- Database hosted on CloudFormation instance
- API hosted on API Gateway instance
- Front-end hosted on EC2 instance
- Using cognito for authentication
- User pools in union with cognito
- Trying to use KSM for sensitive information instead of storing it in the code but we shall see what the time limit does to us

## Extra need-to-knows:
- Please ask for special accistance if you want to run tests through postman because you will need to set the header properly and add a token (Aaron & Mika are the go to people for that I would say).
- You do not have to run the back- & front-end in a specific order, just make sure both are running while you are testing.
- Please be kind.
