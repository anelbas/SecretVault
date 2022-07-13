const token = getCookie.getCookie();
const username = getUsername.getUsername();

const privacyObject = {
  on: "Private",
  off: "Public",
  true: "Private",
  false: "Public"
}

const getMySecrets = async (userID) => {

  return await axios({
    method: "GET",
    url: `https://localhost:63153/v1/Posts/user?userId=${userID}`,
    headers: {
      "Accept": "application/json",
      "Content-Type": "application/json",
      "Authorization": `Bearer ${token}`
  }
  }).then((res) => {
    console.log(res.data);
    return res.data
  }).catch((err) => {
    console.log("Unable to get your secrets", err);
    return [];
  });
};

const createSecretsTable = async (userID) => {

  const secrets = await getMySecrets(userID);

  console.log('secrets', secrets);

  if(!Array.isArray(secrets)) {
    const errorCard = document.getElementById('secrets-error');
    const error = document.createElement("h1");
    const message = document.createTextNode("Don't bottle everything up, Open up and tell us your secrets. We promise not to tell...");
    error.appendChild(message);
    errorCard.appendChild(error);
    errorCard.style.display = 'block';
    return;
  }

  for (let i = 0; i < secrets.length; i++) {

    console.table('secret', secrets[i]);

    const id = secrets[i].postId;
    const text = secrets[i].content;
    const privacy = secrets[i].privacyStatus;

    createSecretCard(id, text, privacy, userID);
  }
};

const createSecretCard = (id, text, privacy, userID) => {

  const parent = document.getElementById("my-secrets");
  const card = document.createElement("article");
  card.className = "secrets grid-item"

  const content = document.createElement("p");
  const secret = document.createTextNode(text);
  content.appendChild(secret);

  const privacyStatus = document.createElement("section");
  privacyStatus.className = "privacy-toggle"

  const privacyToggle = document.createElement("label");
  privacyToggle.className = "switch";

  const privacyCheckbox = document.createElement("input");
  privacyCheckbox.type = "checkbox";
  privacyCheckbox.checked = privacy === "Private" ? true : false;

  const privacySlider = document.createElement("span");
  privacySlider.className = "slider round";
  privacySlider.id = "privacy-toggle";
  privacySlider.onclick = () => {
    
    axios({
      method: "PUT",
      url: `https://localhost:63153/v1/Posts?id=${id}`,
      data: {
        title: text,
        content: text,
        timestamp: "2022-07-12T01:22:12.8576588+02:00",
        privacyStatus: privacyObject[privacy === "Private" ? 'off' : 'on'],
        userId: userID
      },
      headers: {
        "Accept": "application/json",
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`
      }
    }).then((res) => {
      console.log(res.data);
      return res.data
    }).catch((err) => {
      console.log("Unable to get your secrets", err);
      return [];
    });
  }

  privacyToggle.appendChild(privacyCheckbox);
  privacyToggle.appendChild(privacySlider);
  
  const privacyLabel = document.createElement("label");
  privacyLabel.className = "std-margin";
  const privacyText = document.createTextNode("Private");
  privacyLabel.appendChild(privacyText);

  privacyStatus.appendChild(privacyToggle);
  privacyStatus.appendChild(privacyLabel);

  card.appendChild(content);
  card.appendChild(privacyStatus);

  parent.appendChild(card);
};

function createNewSecret(userID) {

  const section = document.getElementById('new-secrets-section');
  const secret = document.getElementById('new-secret-content');
  const privacyStatus = document.getElementById('new-secret-privacy');

  if (section?.offsetParent === null || secret?.value === '' || secret?.value === null) {
    console.log("abort");
    return;
  }

  console.log("secret", secret.value);
  console.log("privacy", privacyStatus.value);
  console.log(new Date().toUTCString());

  axios({
    method: "POST",
    url: `https://localhost:63153/v1/Posts`,
    data: {
      title: secret.value,
      content: secret.value,
      timestamp: "2022-07-12T01:22:12.8576588+02:00",
      privacyStatus: privacyObject[privacyStatus.value],
      userId: userID
    },
    headers: {
      "Accept": "application/json",
      "Content-Type": "application/json",
      "Authorization": `Bearer ${token}`
    }
  }).then((res) => {
    const posts = res.data
    window.location.reload();
  }).catch((err) => {
    console.log("Server error", err);
  });

  section.style.display = "none";
  secret.value = null;
  privacyStatus.value = "on";
}

window.onload = (event) => {

  document.getElementById("submit").addEventListener('click', () => {
    createNewSecret(1);
  });
  
  document.getElementById("cancel").addEventListener('click', () => {
    const section = document.getElementById('new-secrets-section');
    const secret = document.getElementById('new-secret-content');
    const privacyStatus = document.getElementById('new-secret-privacy');
    section.style.display = "none";
    secret.value = null;
    privacyStatus.value = "on";
  });

  document.getElementById("add").addEventListener('click', () => {
    const section = document.getElementById('new-secrets-section');
    section.style.display = "block";
  });
};

document.getElementById("my-secrets").onload = createSecretsTable(username);