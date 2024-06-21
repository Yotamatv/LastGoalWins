const normalizeUser = (user) => ({
  newUser: {},
  Email: user.email,
  Password: user.password,
  FirstName: user.firstName,
  LastName: user.lastName,
  IsAdmin: false,
  Balance: 0,
  ProfilePictureUrl: user.profileImage,
});
export default normalizeUser;
