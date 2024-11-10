import axios from 'axios';
import AuthService from '@/Services/AuthService';

const base_url = "https://localhost:7287";

const GetRatings = async () => {
  try {
    const response = await axios.get(
      `${base_url}/Ratings`,
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


export { GetRatings  };
