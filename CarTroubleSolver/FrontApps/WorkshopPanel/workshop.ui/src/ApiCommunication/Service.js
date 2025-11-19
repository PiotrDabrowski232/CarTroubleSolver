import axios from 'axios';
import AuthService from '@/Services/AuthService';

const base_url = "https://localhost:7287";


const GetServiceTypes = async () => {
  try {
    const response = await axios.get(`${base_url}/ServiceType`, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
    return response.data;
  } catch (error) {
    console.error('There is no services:', error);
  }
};

const AddServices = async (id, services) => {
  try {
    const response = await axios.post(
      `${base_url}/AddServices?id=${id}`, 
      services, 
      {
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${AuthService.getToken()}`
        }
      }
    );
    return response.data;
  } catch (error) {
    console.error('Error adding services:', error);
    throw error;
  }
};

const GetService = async (id) => {
  try {
    const response = await axios.get(`${base_url}/GetServices?id=${id}`, {
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

export { GetServiceTypes, AddServices,GetService  };
