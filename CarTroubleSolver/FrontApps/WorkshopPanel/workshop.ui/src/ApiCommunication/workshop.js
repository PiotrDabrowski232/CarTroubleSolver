import { saveToLocalStorage, getFromLocalStorage } from '@/Services/LocalStorageService';
import axios from 'axios';


const base_url = "https://localhost:7287";



const updateHoursInLocalStorage = (newHours) => {
  const workshopDetails = getFromLocalStorage('Workshop');
  
  if (workshopDetails) {
    workshopDetails.hours = newHours;
    
    saveToLocalStorage('Workshop', workshopDetails);
  } else {
    console.error('There is no Hour object in localStorage');
  }
};

const UpdateHours = async (Hours) => {
  try {
    const response = await axios.post(`${base_url}/HoursConfiguration`, Hours, {
      headers: {
        'Content-Type': 'application/json'
      }
    });

    if (response.status === 200) {
      updateHoursInLocalStorage(response.data)
    }
  } catch (error) {
    console.error('Błąd podczas aktualizacji godzin:', error);
  }
};

export { UpdateHours };
