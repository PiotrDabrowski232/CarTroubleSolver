import axios from 'axios';
import AuthService from '@/Services/AuthService';

const base_url = "https://localhost:7287";

const getMessages = async () => {
    try {
      const response = await axios.get(`${base_url}/ReceiveMessage`, {
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

  const MessageFullInfo = async (id) => {
    try {
      const response = await axios.get(`${base_url}/FullMessage?id=${id}`);
      return response.data;
    } catch (error) {
      console.error('There is no services:', error);
    }
  };
  const setRead = async (id) => {
    try {
      const response = await axios.post(`${base_url}/SetRead?id=${id}`);
      return response.data;
    } catch (error) {
      console.error('There is no services:', error);
    }
  };

  const sendMessage = async (id, message) => {
    try {
      const response = await axios.post(`${base_url}/SendMessage?id=${id}`, message);
      return response.data;
    } catch (error) {
      console.error('There is no services:', error);
    }
  };

export { getMessages,MessageFullInfo, setRead ,sendMessage};
