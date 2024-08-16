import AuthService from './AuthService';
import axios from 'axios';

const BASE_URL = 'http://localhost:5113';


const fetchCarBrand = async () => {
  try {
    const response = await axios.get(`${BASE_URL}/Brands`, {
      headers: {
        'Authorization': 'Bearer ' + AuthService.getToken(),
      }
    });
    return response.data.result;
  } catch (error) {
    console.error('Error fetching car data:', error);
    throw error;
  }
}

const fetchCarModels = async (selectedBrand) => {
  try {
    const response = await axios.get(`${BASE_URL}/Models?Brand=${selectedBrand}`,
      {
      headers: {
        'Authorization': 'Bearer ' + AuthService.getToken(),
      }
    });
    console.log(response)
    return response.data.result;
  } catch (error) {
    console.error('Error fetching car data:', error);
    throw error;
  }
}

const CreateCar = async (Car, carPhoto) => {
  try {
    const response = await axios.post(`${BASE_URL}/AddCar`, Car, {
      headers: {
        'Authorization': `Bearer ${AuthService.getToken()}`,
      },
    });    

    if(carPhoto.Photo !== null && carPhoto.Photo !== undefined){
      const formData = new FormData();

      formData.append('Vin', carPhoto.Vin);  
      formData.append('Photo', carPhoto.Photo);
  
      console.log(carPhoto.Photo)
  
      await axios.post(`${BASE_URL}/AddFile`, formData);
    }
    return response.data.result;
  } catch (error) {
    console.error('Error creating car:', error);
    throw error;
  }
};

const tryDelete = async (vin) => {
  try {
     await axios.post(`${BASE_URL}/Delete?VIN=${vin}`, null,{
      headers: {
        'Authorization': `Bearer ${AuthService.getToken()}`,
      },
    });
  } catch (error) {
    console.error('Error creating car:', error);
    throw error;
  }
};

const UpdateData = async (Car, lastVin) => {
  try { 
    let car = {};
    car.Brand = Car.brand;
    car.Model = Car.model;
    car.Type = Car.type; 
    car.Color = {
      red: Car.color.r,
      green: Car.color.g,
      blue: Car.color.b,
    };
    car.VIN = Car.vin;
    car.Engine = Car.engine;
    car.Mileage = Car.mileage;
    car.DoorCount = Car.doorCount;
    car.DateOfProduction = Car.dateOfProduction;
    car.OldVin = lastVin;

    const response = await axios.put(`${BASE_URL}/UpdateData`, car, {
      headers: {
        'Authorization': `Bearer ${AuthService.getToken()}`,
      },
    });
    return response.data.result;

  } catch (error) {
    console.error('Error creating car:', error);
    console.log(error)
    throw error;
  }
};

const CarImage = async (vin) => {
  try {

      const response = await axios.get(`${BASE_URL}/DownloadFile?vin=${vin}`, {
          responseType: 'blob' 
      });

      const imageUrl = URL.createObjectURL(new Blob([response.data], { type: response.headers['content-type'] }));
console.log(response)
      return imageUrl;
  } catch (error) {
      console.error("Error downloading image:", error);
  }
};
export { CarImage };

export {  fetchCarBrand,  fetchCarModels, CreateCar, tryDelete, UpdateData };
