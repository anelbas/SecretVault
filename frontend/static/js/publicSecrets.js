


let posts = [];
let postsTitles = [];
let currentTab;
let mySecrets = [];

let userID = getUsername.getUsername();
let token = getCookie.getCookie();

function allPrivatePosts(currentPosts)
{
    if (currentPosts.length < 1)
    {
        let noPosts = document.createElement("h1");
        noPosts.innerText = "No posts to show!"
        currentTab.appendChild(noPosts);
    }
    else {
        currentPosts.forEach(currentPost => {
            if (currentPost.privacyStatus === "Public")
             currentTab = document.getElementById("tab-content-public")
            else
             currentTab = document.getElementById("tab-content-private");

            let secretsDiv = document.createElement("div");
            secretsDiv.className = "secrets";
            currentTab.appendChild(secretsDiv);
            let timestamp = document.createElement("p");
            timestamp.className = "timestamp";
            let formattedTimeStamp = formatTimeStamp(currentPost.timestamp)
            timestamp.innerText = formattedTimeStamp;
            secretsDiv.appendChild(timestamp);
            let titlePost = document.createElement("h4");
            titlePost.className = "titlePost";
            titlePost.innerHTML = `${currentPost.title}`;
            secretsDiv.appendChild(titlePost);
            let post = document.createElement("p");
            post.className = "post";
            post.innerText = `${currentPost.content}`;
            secretsDiv.appendChild(post);
        });
    }
}


function allPosts(currentPosts){
    
    if (currentPosts.length < 1)
    {
        let noPosts = document.createElement("h1");
        noPosts.innerText = "No posts to show!"
        currentTab.appendChild(noPosts);
    }
    else {
        currentPosts.forEach(currentPost => {
            if (currentPost.privacyStatus === "Public")
             currentTab = document.getElementById("tab-content-public")
            else
             currentTab = document.getElementById("tab-content-private");

            let secretsDiv = document.createElement("div");
            secretsDiv.className = "secrets";
            currentTab.appendChild(secretsDiv);
            let timestamp = document.createElement("p");
            timestamp.className = "timestamp";
            let formattedTimeStamp = formatTimeStamp(currentPost.timestamp)
            timestamp.innerText = formattedTimeStamp;
            secretsDiv.appendChild(timestamp);
            let titlePost = document.createElement("h4");
            titlePost.className = "titlePost";
            titlePost.innerHTML = `${currentPost.title}`;
            secretsDiv.appendChild(titlePost);
            let post = document.createElement("p");
            post.className = "post";
            post.innerText = `${currentPost.content}`;
            secretsDiv.appendChild(post);
        });
    }
}

function manageSecrets(){
    window.location.href = '/mySecrets';
}

function formatTimeStamp(timeStamp) {

    let datePosted = new Date(Date.parse(timeStamp));
    return datePosted.toUTCString();
}

function getAllPosts() {
    axios({
        method: "GET",
        url: "https://localhost:44391/v1/Posts",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        }
    }).then((res) => {
        posts = res.data
        allPosts(posts)
    }).catch((err) => {
        console.log("help", err);
    })
}

function myPrivateSecrets(userID){
    axios({
        method: "GET",
        url: `https://localhost:44391/v1/Posts/user?userId=${userID}`,
        headers: {
          "Accept": "application/json",
          "Content-Type": "application/json",
          "Authorization": `Bearer ${token}`,
          "Access-Control-Allow-Origin": "*"
      }
      }).then((res) => {
        mySecrets = res.data;
        allPrivatePosts(mySecrets);
      }).catch((err) => {
        console.log("Unable to get your secrets", err);
        return [];
      });
}

function searchPostsPrivate(){
    var searchBar = document.getElementById("searchPostsIDPrivate").value;

    if (searchBar === '')
    {
        clearTab();
        getAllPosts();
        return;
    }
}

function searchPostsPublic() {
    var searchBar = document.getElementById("searchPostIDPublic").value;

    if (searchBar === '')
    {
        clearTab();
        getAllPosts();
        return;
    }

    postsTitles = posts.filter(post => post.title.indexOf(searchBar) > -1);

    if (postsTitles.length < 1)
    {
        clearTab();
        let noPosts = document.createElement("h1");
        noPosts.innerText = "No posts to show!"
        currentTab.appendChild(noPosts);
    }
    else
    {
        clearTab();
        allPosts(postsTitles);
    }
}

function searchPostsPrivate() {
    var searchBar = document.getElementById("searchPostsIDPrivate").value;

    if (searchBar = '')
    {
        clearTab();
        getAllPosts();
        return;
    }
}

function clearTab(){
    while (currentTab.lastChild.id !== 'searchPostIDPublicDiv') {
        currentTab.removeChild(currentTab.lastChild);
    }
}

window.onload = (event) => {
    getAllPosts();
    myPrivateSecrets(userID)
  };