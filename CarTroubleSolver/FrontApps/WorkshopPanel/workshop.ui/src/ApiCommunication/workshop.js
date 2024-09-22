import axios from 'axios';
import { saveToLocalStorage } from '@/Services/LocalStorageService';

const base_url = "https://localhost:7287";

const UpdateHours = async (Hours) => {
    console.log(Hours)
  await axios.post(`${base_url}/HoursConfiguration`, Hours, {
    headers: {
      'Content-Type': 'application/json'
    }
  })
  .then(response => {
    localStorage.clear();
    
    })
}


export {UpdateHours};