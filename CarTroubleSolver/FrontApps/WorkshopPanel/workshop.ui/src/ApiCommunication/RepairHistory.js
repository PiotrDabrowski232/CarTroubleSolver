import axios from 'axios';

const base_url = "https://localhost:7287";


const getRepairHistory = async (id) => {
    try {
      const response = await axios.get(`${base_url}/GetRepairHistory?accidentId=${id}`);
      return response.data;
    } catch (error) {
      console.error('There is no history:', error);
    }
  };

  const updateHistory = async (id, repairs) => {
    try {
      const response = await axios.put(`${base_url}/UpdateRepairs?accidentId=${id}`, repairs);
      return response.data;
    } catch (error) {
      console.error('There is no history:', error);
    }
  };


export { getRepairHistory, updateHistory };
