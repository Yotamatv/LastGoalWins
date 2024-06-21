import axios from "axios";
const apiUrl = "https://localhost:7223/Users";
export const login = async (user) => {
  try {
    const { data } = await axios.post(`${apiUrl}/login`, user);
    console.log(data);
    return data;
  } catch (error) {
    console.log(error);
    return Promise.reject(error.message);
  }
};
export const signup = async (user) => {
  try {
    console.log(user);
    const { data } = await axios.post(
      `https://localhost:7223/Users/signup`,
      user
    );
    return data;
  } catch (error) {
    console.log(error);
    return Promise.reject(error.message);
  }
};
export const getUsersData = async () => {
  try {
    const { data } = await axios.get(`${apiUrl}`);
    return data;
  } catch (error) {
    return Promise.reject(error.message);
  }
};
export const deleteUser = async (id) => {
  try {
    const { data } = await axios.delete(`${apiUrl}/${id}`);
    return data;
  } catch (error) {
    return Promise.reject(error.message);
  }
};
export const makeUserAdmin = async (id) => {
  try {
    const { data } = await axios.put(`${apiUrl}/MakeAdmin/${id}`);
    return data;
  } catch (error) {
    return Promise.reject(error.message);
  }
};
