# SecretVault
A vault for all your secrets :)

## SETTING UP THE BACK-END:
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

## RUNDOWN OF API ENDPOINTS:
- GET /v1/Posts
	- Input: nothing
	- Application: Get all public posts
	- Output: PostDTO* list

- POST /v1/Posts
	- Input: PostDTO*
	- Application: Create a new post
	- Output: OK status
	
- GET /v1/Posts/user/{userID}
	- Input: User ID
	- Application: Get all posts for a specific user
	- Output: PostDTO* List
	
- GET /v1/Posts/user/{userId}/{title}
	- Input: User ID, string
	- Application: Search for a post by title
	- Output: PostDTO* List
	
- GET /v1/Posts/{id}
	- Input: Post ID
	- Application: Returns details of a specific post
	- Output: PostDTO
	
- PUT /v1/Posts/{id}
	- Input: Post ID
	- Application: Edit a specific post
	- Output: OK status
	
- PATCH /v1/Posts/{id}
	- Input: Post ID
	- Application: Edit a spesific post
	- Output: OK status
	
-DELETE /v1/Posts/{id}
	- Input: Post ID
	- Application: Delete a spesific post
	- Output: OK status

*PostDTO: See the structure in \SecretVault\SecretVaultAPI\DTOs\PostDTO.cs

### EXTRA NEED TO KNOWS:
- The content of a post is encrypted before it is inserted into the database and decrypted before it is sent to the front-   end to ensure that no one can read the content of a post in the database.
- Most of the security is handled by cognito so it will be less secure when ran locally.


## SETTING UP THE FRONT-END:
