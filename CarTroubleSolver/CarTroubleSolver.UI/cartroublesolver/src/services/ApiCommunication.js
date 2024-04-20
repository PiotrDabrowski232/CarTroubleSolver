// api.js

import axios from 'axios';

const BASE_URL = 'http://localhost:5113';

// Definicja metod do wykonywania zapytań
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
      try {
        const response = await axios.post(`${BASE_URL}/Login`, {
            Email: user.Email,
            Password: user.Password
          })
          return response;
      } catch (error) {
        console.error('Error fetching posts:', error);
        throw error;
      }
}



export { getUserInfo, LoginUser };
