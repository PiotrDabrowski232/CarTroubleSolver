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

const ResetPassword = async (ResetModel) => {
  try {
    var response = await axios.put(`${BASE_URL}/ChangePassword`, {
      OldPassword: ResetModel.Password,
      NewPassword: ResetModel.NewPassword,
      ConfirmedNewPassword: ResetModel.NewConfirmedPassword,
      Id:""
      },
      {
        headers: {
          'Authorization': 'Bearer ' + AuthService.getToken(),
        },
      }
    )
    return response;
    }catch (error) {
    console.error('Error Reset Password Action');
    throw error
  }
}

export { getUserInfo, LoginUser, fetchUserData, ResetPassword };
