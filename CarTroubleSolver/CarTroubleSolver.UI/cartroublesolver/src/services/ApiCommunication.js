import AuthService from '../services/AuthService';
import axios from 'axios';

const BASE_URL = 'http://localhost:5113';

const getUserInfo = async () => {
  try {
    const response = await axios.get(`${BASE_URL}/User`);
    return response.data;
  } catch (error) {
    console.error('Error fetching posts:', error);
    throw error;
  }
};

const LoginUser = async (user) => {
  await axios.post(`${BASE_URL}/Login`, {
    Email: user.Email,
    Password: user.Password
      }).then(response => {
        AuthService.setToken(response.data)
    })
}

const fetchUserData = async () => {
  try {
    const response = await axios.get(`${BASE_URL}/User`, {
      headers: {
        'Authorization': 'Bearer ' + AuthService.getToken(),
      }
    });
    return response.data;
  } catch (error) {
    console.error('Error fetching user data:', error);
    throw error;
  }
}
export { getUserInfo, LoginUser, fetchUserData };
