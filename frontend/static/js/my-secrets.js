import mySecrets from './mock_data/secrets-mock.js';

const getMySecrets = (userID) => {
  
  // axios({
  //   method: "GET",
  //   headers: {},
  //   url: `http://ec2-3-82-51-192.compute-1.amazonaws.com:3002/api/my-secrets/${id}`
  // }).then((data) => {
  //   console.log(data);
  //   sessionStorage.setItem("--my-secrets", data.data);
  //   return data.data;
  // });
  return mySecrets().data;
};

const createSecretsTable = (userID) => {

  const secrets = sessionStorage.getItem('--my-secrets') || getMySecrets(userID);

  console.log('secrets', secrets);

  for (let i = 0; i < secrets.length; i++) {

    console.table('secret', secrets[i]);

    const id = secrets[i].id;
    const text = secrets[i].secret;
    const privacy = secrets[i].privacy;

    createSecretRow(id, text, privacy);
  }
};

const createSecretRow = (id, text, privacy) => {

  const parent = document.getElementById("secrets-body");
  const row = document.createElement("tr");

  const secretColumn = document.createElement("td");
  const secret = document.createTextNode(text);
  secretColumn.appendChild(secret);

  const privacyColumn = document.createElement("td");

  const privateLabel = document.createElement("label");
  privateLabel.setAttribute("for", "private");
  const privateText = document.createTextNode("private");
  privateLabel.appendChild(privateText);

  const privateRadioButton = document.createElement("input");
  privateRadioButton.setAttribute("type", "radio");
  privateRadioButton.setAttribute("id", "private");
  privateRadioButton.setAttribute("name", `${id}`);
  privateRadioButton.setAttribute("value", "private");


  const publicLabel = document.createElement("label");
  publicLabel.setAttribute("for", "public");
  const publicText = document.createTextNode("public");
  publicLabel.appendChild(publicText);
  
  const publicRadioButton = document.createElement("input");
  publicRadioButton.setAttribute("type", "radio");
  publicRadioButton.setAttribute("id", "public");
  publicRadioButton.setAttribute("name", `${id}`);
  publicRadioButton.setAttribute("value", "public");

  privacyColumn.appendChild(privateLabel);
  privacyColumn.appendChild(privateRadioButton);
  privacyColumn.appendChild(publicLabel);
  privacyColumn.appendChild(publicRadioButton);

  privacy === "private" ? privateRadioButton.checked = true : publicRadioButton.checked = true;

  row.appendChild(secretColumn);
  row.appendChild(privacyColumn);

  parent.appendChild(row);
};

document.getElementById("secrets-body").onload=createSecretsTable('userId');

// modules.exports = {
//   createSecretsTable
// }