import AuthService from './AuthService';
import axios from 'axios';

const BASE_URL = 'http://localhost:5113';


const fetchCarBrand = async () => {
  try {
    const response = await axios.get(`${BASE_URL}/Brands`, {
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

const fetchCarModels = async (selectedBrand) => {
  try {
    const response = await axios.get(`${BASE_URL}/Models?Brand=${selectedBrand}`,
      {
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


export {  fetchCarBrand,  fetchCarModels };
