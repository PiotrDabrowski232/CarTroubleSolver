import AuthService from './AuthService';
import axios from 'axios';

const BASE_URL = 'http://localhost:5113';


const fetchCarData = async () => {
  try {
    const response = await axios.get(`${BASE_URL}/GetCarData`, {
      headers: {
        'Authorization': 'Bearer ' + AuthService.getToken(),
      }
    });
    console.log(response)
    return response.data.result;
  } catch (error) {
    console.error('Error fetching car data:', error);
    throw error;
  }
}


export {  fetchCarData };
