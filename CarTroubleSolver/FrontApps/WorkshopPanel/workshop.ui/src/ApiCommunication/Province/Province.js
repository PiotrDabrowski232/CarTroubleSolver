import axios from 'axios';

//const base_url = "http://localhost:7287"

const GetProvinces = async () => {
  try {
    const response = await axios.get(`https://localhost:7287/Provinces`);

    for (let x = 0; x < response.data.province.length; x++) {
      response.data.province[x] = response.data.province[x].replace(/_/g, " ");
    }
    return response.data.province;
    
  } catch (ex) {
    console.log(ex);
  }
}



export {GetProvinces};
