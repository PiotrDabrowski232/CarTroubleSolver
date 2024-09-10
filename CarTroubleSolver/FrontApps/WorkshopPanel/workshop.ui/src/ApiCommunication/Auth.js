import AuthService from '../Services/AuthService';
import axios from 'axios';
import { WorkshopError, StreetError } from '@/Models/AuthModels/RegisterError';

const base_url = "https://localhost:7287";

const Login = async (user) => {
  await axios.post(`${base_url}/Login`, user)
  .then(response => {
        AuthService.setToken(response.data);
    })
}

const Register = async (Workshop) => {
  try {
     await axios.post(`${base_url}/Register`, Workshop, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
  } catch (ex) {
    const errors = ex.response?.data?.errors || {};

    // Bezpieczne przypisanie właściwości błędów
    var workshopErrors = new WorkshopError(
      errors.Name || [],
      errors.Email || [],
      errors.Password || [],
      errors.ConfirmedPassword || [],
      errors.PhoneNumber || [],
      errors.NIP || [],
      new StreetError(
        errors["Street.StreetName"] || [],
        errors["Street.StreetNumber"] || [],
        errors["Street.PostalCode"] || [], 
        errors["Street.City"] || [], 
        errors["Street.Country"] || [], 
        errors["Street.Province"] || []
      )
    );
    
    console.log(workshopErrors);
    return workshopErrors;
  }
}

export {Login, Register};