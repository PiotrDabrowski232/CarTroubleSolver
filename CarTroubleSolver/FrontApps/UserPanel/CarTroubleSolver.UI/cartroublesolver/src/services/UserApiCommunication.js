import AuthService from '../services/AuthService';
import axios from 'axios';
import {saveToLocalStorage, getFromLocalStorage} from '@/LocalStorage/useLocalStorage';

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
    if (response.data.result.cars) {
        saveToLocalStorage(`userCars`, response.data.result.cars);
    }
    return response.data.result;
  } catch (error) {
    console.error('Error fetching user data:', error);
    throw error;
  }
};

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

const worskhops = async () => {
  try {
    var geolocation = getFromLocalStorage('geoLocation');
    var response;

    if (geolocation === null) {
      response = await axios.get(`${BASE_URL}/Workshops`);
    } else {
      response = await axios.get(`${BASE_URL}/Workshops`, {
        params: { 
          Latitude: geolocation.Latitude,
          Longitude: geolocation.Longitude
        }
      });
    }

    return response.data;
  } catch (error) {
    console.error('Error fetching workshops:', error);
    throw error;
  }
};

const sendMessage = async (message) => {
  try {
    const response = await axios.post(`${BASE_URL}/SendMessage`, message, {
      headers: {
        'Authorization': 'Bearer ' + AuthService.getToken(),
      },
    });
    return response.data;
  } catch (error) {
    console.error('Error fetching posts:', error);
    throw error;
  }
};

const receiveMessage = async () => {
  try {
    const response = await axios.get(`${BASE_URL}/ReceiveMessage`, {
      headers: {
        'Authorization': 'Bearer ' + AuthService.getToken(),
      },
    });
    return response.data;
  } catch (error) {
    console.error('Error fetching posts:', error);
    throw error;
  }
};

const setMessageRead = async (id) => {
  try {
    const response = await axios.post(`${BASE_URL}/SetMessageRead?id=${id}`);
    return response.data;
  } catch (error) {
    console.error('Error fetching posts:', error);
    throw error;
  }
};

const GetMessageFullInfo = async (id) => {
  try {
    const response = await axios.get(`${BASE_URL}/ReceiveMessageDetails?id=${id}`);
    return response.data;
  } catch (error) {
    console.error('Error fetching posts:', error);
    throw error;
  }
};

export { getUserInfo, LoginUser, fetchUserData, ResetPassword, worskhops, sendMessage, receiveMessage,setMessageRead, GetMessageFullInfo };
