let posts = [];
let postsTitles = [];
let currentTab;


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

function formatTimeStamp(timeStamp) {

    let datePosted = new Date(Date.parse(timeStamp));
    return datePosted.toUTCString();
}

function getAllPosts() {
    axios({
        method: "GET",
        url: "https://localhost:5001/v1/Posts"
    }).then((res) => {
        posts = res.data
        allPosts(posts)
    }).catch((err) => {
        console.log("help", err);
    })
}

function searchPostsPrivate(){
    var searchBar = document.getElementById("searchPostsIDPrivate").value;

    if (searchBar === '')
    {
        clearTab();
        getAllPosts();
        return;
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

function clearTab(){
    while (currentTab.lastChild.id !== 'searchPostIDPublicDiv') {
        currentTab.removeChild(currentTab.lastChild);
    }
}

window.onload = (event) => {
    getAllPosts();
  };