import axios from 'axios';
import AuthService from './AuthService';


const WORKSHOP_URL = 'https://localhost:7287';
const BASE_URL = 'http://localhost:5113';


const fetchTypes = async (id) => {
  try {
    const response = await axios.get(`${WORKSHOP_URL}/GetServices?id=${id}`, {
      headers: {
        'Authorization': 'Bearer ' + AuthService.getToken(),
      }
    });
    return response.data;
  } catch (error) {
    console.error('Error fetching car data:', error);
    throw error;
  }
}

const AddAccident = async (id, isAccepted) => {
  try {
    const response = await axios.post(`${BASE_URL}/AddAccident?id=${id}`, isAccepted, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
    return response.data;
  } catch (error) {
    console.error('Error fetching car data:', error);
    throw error;
  }
};

const sendRate = async (id, rate) => {
  try {
    const response = await axios.post(`${BASE_URL}/SendRate?id=${id}`, rate, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
    return response.data;
  } catch (error) {
    console.error('Error fetching car data:', error);
    throw error;
  }
};


export {  fetchTypes, AddAccident, sendRate};
