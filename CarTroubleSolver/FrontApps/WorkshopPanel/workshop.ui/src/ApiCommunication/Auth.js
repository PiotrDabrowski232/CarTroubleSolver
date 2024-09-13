import AuthService from '../Services/AuthService';
import axios from 'axios';
import { WorkshopError, StreetError } from '@/Models/AuthModels/RegisterError';

const base_url = "https://localhost:7287";

const Login = async (loginData) => {
  await axios.post(`${base_url}/Login`, loginData, {
    headers: {
      'Content-Type': 'application/json'
    }
  })
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
    throw workshopErrors;
  }
}

export {Login, Register};