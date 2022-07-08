let posts = [{
    username: "amy chicken licken",
    timestamp: "Tuesday, 13:00",
    titlePost: "Wowzers",
    post: "heya, maaaaaaybe we should not do this",
    privacyStatus: "partial"
},
{
    username: "amy licken",
    timestamp: "Tuesday, 13:00",
    titlePost: "Wowzers",
    post: "city boooyyyyyyyy, city booooooooooooy-",
    privacyStatus: "public"
},
{
    username: "amy licken",
    timestamp: "Tuesday, 13:00",
    titlePost: "Wowzers",
    post: "city boooyyyyyyyy, city booooooooooooy-",
    privacyStatus: "public"
},{
    username: "amy licken",
    timestamp: "Tuesday, 13:00",
    titlePost: "Wowzers",
    post: "city boooyyyyyyyy, city booooooooooooy-",
    privacyStatus: "public"
},{
    username: "amy licken",
    timestamp: "Tuesday, 13:00",
    titlePost: "Wowzers",
    post: "city boooyyyyyyyy, city booooooooooooy-",
    privacyStatus: "public"
},{
    username: "amy licken",
    timestamp: "Tuesday, 13:00",
    titlePost: "Wowzers",
    post: "city boooyyyyyyyy, city booooooooooooy-",
    privacyStatus: "public"
},{
    username: "amy licken",
    timestamp: "Tuesday, 13:00",
    titlePost: "Wowzers",
    post: "city boooyyyyyyyy, city booooooooooooy-",
    privacyStatus: "public"
},{
    username: "amy licken",
    timestamp: "Tuesday, 13:00",
    titlePost: "Wowzers",
    post: "city boooyyyyyyyy, city booooooooooooy-",
    privacyStatus: "public"
},{
    username: "amy licken",
    timestamp: "Tuesday, 13:00",
    titlePost: "Wowzers",
    post: "city boooyyyyyyyy, city booooooooooooy-",
    privacyStatus: "public"
},{
    username: "amy licken",
    timestamp: "Tuesday, 13:00",
    titlePost: "Wowzers",
    post: "city boooyyyyyyyy, city booooooooooooy-",
    privacyStatus: "public"
}];

function allPosts(){
    if (posts.length < 1)
    {
        let noPosts = document.createElement("h1");
        noPosts.innerText = "No friends bethuna"
        // publicTab.appendChild(noPosts);
    }
    else {
        posts.forEach(currentPost => {
            let currentTab;
            if (currentPost.privacyStatus === "public")
             currentTab = document.getElementById("tab-content-public")
            else
             currentTab = document.getElementById("tab-content-private");

            let secretsDiv = document.createElement("div");
            secretsDiv.className = "secrets";
            currentTab.appendChild(secretsDiv);
            let username = document.createElement("p");
            username.className = "username";
            username.innerText = `${currentPost.username}`;
            secretsDiv.appendChild(username);
            let timestamp = document.createElement("p");
            timestamp.className = "timestamp";
            timestamp.innerText = `${currentPost.timestamp}`;
            secretsDiv.appendChild(timestamp);
            let titlePost = document.createElement("h4");
            titlePost.className = "titlePost";
            titlePost.innerHTML = `${currentPost.titlePost}`;
            secretsDiv.appendChild(titlePost);
            let post = document.createElement("p");
            post.className = "post";
            post.innerText = `${currentPost.post}`;
            secretsDiv.appendChild(post);
        });
    }
}

// function getAllPosts() {
//     axios({
//         method: "GET",
//         url: "whatever",
//         params: "whatever here"
//     }).then((res) => {
//         posts = res.data
//         allPosts()
//     }).catch((err) => {
//         console.log("help", err);
//     })
// }

window.onload = (event) => {
    allPosts();
  };