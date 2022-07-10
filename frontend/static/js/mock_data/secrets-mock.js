export default function mySecrets() {
  return {
    status: 200,
    data: [
      {
        id: "1",
        secret: "secret stuff. do not try to decipher",
        privacy: "private"
      },{
        id: "2",
        secret: "super loooooooooooooooooooooooooooooooooooooong secret. it will take foreeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeevvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvveeeeeeeeeeeeeeeeeeeeeeeeeeeeerrrrrrrrrrrr to decipher",
        privacy: "public"
      },{
        id: "3",
        secret: "secret stuff. do not try to decipher",
        privacy: "public"
      },{
        id: "4",
        secret: "secret stuff. do not try to decipher",
        privacy: "private"
      },
    ]
  }
};