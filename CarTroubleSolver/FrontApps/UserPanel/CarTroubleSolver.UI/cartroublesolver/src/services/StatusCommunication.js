import axios from 'axios';

const BASE_URL = 'http://localhost:5113';
const WORKSHOP_URL = 'https://localhost:7287';


const getStatusHistory = async (id) => {
  try {
    const response = await axios.get(`${BASE_URL}/StatusHistory?accidentId=${id}`);
    return response.data;
  } catch (error) {
    console.error('Error fetching car data:', error);
    throw error;
  }
}


const getRepairHistory = async (id) => {
  try {
    const response = await axios.get(`${WORKSHOP_URL}/GetRepairHistory?accidentId=${id}`);
    return response.data;
  } catch (error) {
    console.error('There is no history:', error);
  }
};

export { getStatusHistory, getRepairHistory};

