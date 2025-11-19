import axios from 'axios';
import AuthService from '@/Services/AuthService';

const base_url = "https://localhost:7287";


const GetAccidents = async () => {
  try {
    const response = await axios.get(`${base_url}/Accidents`, {
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${AuthService.getToken()}`
      }
    });
    return response.data;
  } catch (error) {
    console.error('There is no services:', error);
  }
};

const GetAccidentFullInfo = async (id) => {
  try {
    const response = await axios.get(`${base_url}/AccidentFullInfo?accidentId=${id}`);
    return response.data;
  } catch (error) {
    console.error('There is no services:', error);
  }
};

const changeAccidentStatus = async (id, repairs) => {
  try {
    console.log(repairs)
    const response = await axios.put(`${base_url}/ChangeAccidentStatus?accidentId=${id}`, repairs);
    return response.data;
  } catch (error) {
    console.error('There is no services:', error);
  }
};

const getServicePriceDetails = async (id) => {
  try {
    const response = await axios.get(`${base_url}/AccidentServiceDetails?accidentId=${id}`);
    return response.data;
  } catch (error) {
    console.error('There is no services:', error);
  }
};

export { GetAccidents, GetAccidentFullInfo, changeAccidentStatus, getServicePriceDetails };
