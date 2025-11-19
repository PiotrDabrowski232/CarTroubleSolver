import axios from 'axios';

const BASE_URL = 'http://localhost:5113';


const WorkshopDetails = async (id) => {
  try {
    const response = await axios.get(`${BASE_URL}/GetWorkshop?id=${id}`);
    return response.data;
  } catch (error) {
    console.error('Error fetching car data:', error);
    throw error;
  }
}



export {  WorkshopDetails };
