<template>
  <div class="mainRegisterForm">
    <form id="registerForm" class="needs-validation my-form-validation" novalidate @submit.prevent="Register">
      <Toast v-if="showToast" :msg="toastMsg" :color="toastColor" />
      
      <div class="container">
        <CustomInput v-model="Email" label="Email" type="email" :error="Errors.Email" :width='15'/>
        <div class="row">
          <div class="col-4">
        <CustomInput v-model="Name" label="Workshop Name" type="text" :error="Errors.Name" :width='15'/>
      </div>
      <div class="col-4">
        <CustomInput v-model="NIP" label="NIP" type="number" :error="Errors.NIP" :width='15'/>
      </div></div>
        <div class="row">
          <div class="col-4">
            <CustomInput v-model="Password" label="Password" type="password" :error="Errors.Password" :width='15'/>
          </div>
          <div class="col-4">
            <CustomInput v-model="ConfirmedPassword" label="Confirm Password" type="password" :error="Errors.ConfirmedPassword" :width='15'/>
          </div>
        </div> 

        <CustomInput v-model="Phone" label="Phone Number" type="number" :error="Errors.PhoneNumber" :width='15'/>

        <button type="button" style="margin-bottom: 3vh" class="btn btn-secondary" @click="fillFormWithLocation">Automatically fill in the location</button>

        <div class="row">
          <div class="col-4">
            <CustomInput v-model="StreetName" label="Street Name" type="text" :error="Errors.StreetName" :width='15'/>
          </div>
          <div class="col-4">
            <CustomInput v-model="StreetNumber" label="Street Number" type="text" :error="Errors.StreetNumber" :width='15'/>
          </div>
        </div>

        <div class="row">
          <div class="col-4">
            <CustomInput v-model="PostalCode" label="Postal Code" type="text" :error="Errors.PostalCode" :width='15'/>
          </div>
          <div class="col-4">
            <SelectInput v-model="Province" label="Province" type="text" :dataArray="ProvinceData" :width='15'/>
          </div>
        </div>

        <div class="row">
          <div class="col-4">
            <CustomInput v-model="City" label="City" type="text" :error="Errors.City" :width='15'/>
          </div>
          <div class="col-4">
            <CustomInput v-model="Country" label="Country" type="text" :error="Errors.Country" :width='15'/>
          </div>
        </div>

        <button type="submit" class="btn btn-primary">Register</button>
      </div>
    </form>
  </div>
</template>

<script>
import Toast from '@/components/Toast.vue';
import CustomInput from '../../components/Form/CustomInput.vue';
import SelectInput from '../../components/Form/SelectInput.vue';
import { Workshop, Street } from '@/Models/AuthModels/Register';
import { getLocation, reverseGeocode } from '../../Services/GeoLocalizationService';
import { GetProvinces } from '../../ApiCommunication/Province/Province';
import { Register } from '../../ApiCommunication/Auth';

export default {
  name: 'Register',
  components: { CustomInput, SelectInput, Toast },
  data() {
    return {
      Email: "",
      Name: "",
      Password: "",
      ConfirmedPassword: "",
      Phone: null,
      NIP: null,
      StreetName: "",
      StreetNumber: "",
      PostalCode: "",
      Province: "",
      City: "",
      Country: "",
      Errors: [],
      ProvinceData: [],
      showToast: false,
      toastMsg: '',
      toastColor: ''
    };
  },
  mounted(){
    this.fetchProvinces();
  },
  methods: {
    async Register() {
  try {
    if (!this.Name) {
      this.Errors.Name = ['Name is required'];
      return;
    }
    if (!this.Email) {
      this.Errors.Email = ['Email is required'];
      return;
    }
    if (!this.Password) {
      this.Errors.Password = ['Password is required'];
      return;
    }
    if (this.Password !== this.ConfirmedPassword) {
      this.Errors.ConfirmedPassword = ['Passwords do not match'];
      return;
    }

    const street = new Street(
      this.StreetName, 
      this.StreetNumber, 
      this.PostalCode, 
      this.City, 
      this.Country, 
      this.Province
    );

      const workshop = new Workshop(
      this.Name, 
      this.Email, 
      this.Password, 
      this.ConfirmedPassword, 
      parseInt(this.Phone), 
      parseInt(this.NIP), 
      street
    );
    await Register(workshop);
  } catch (ex) {
    this.Errors= ex;
    console.log(this.Errors)
    return
  }
},

    async fetchProvinces(){
      this.ProvinceData = await GetProvinces()
    },
    async fillFormWithLocation() {
      try {
        const position = await getLocation();
        const latitude = position.coords.latitude;
        const longitude = position.coords.longitude;
        const address = await reverseGeocode(latitude, longitude);

        this.StreetName = address.streetName;
        this.StreetNumber = address.streetNumber;
        this.City = address.city;
        this.PostalCode = address.postalCode;
        this.Country = address.country;
        this.changeApiStateResponse(address.province);

        this.showToastMessage('Update successfully completed', 'primary');
        setTimeout(() => {
          this.showToastMessage('Please verify the accuracy of the data', 'warning');
      }, 3000);

      } catch (error) {
        this.showToastMessage('Nie można ustalić lokalizacji.');
      }
    },
    changeApiStateResponse(provinceName) {
      const cleanedProvinceName = provinceName.replace('województwo ', '');
      const matchingProvince = this.ProvinceData.find(
        (province) => province.toLowerCase() === cleanedProvinceName.toLowerCase()
      );

      if (matchingProvince) {
        this.Province = matchingProvince;
      }
    },

    showToastMessage(message, color) {
      this.toastMsg = message;
      this.showToast = true;
      this.toastColor = color;
      setTimeout(() => {
        this.showToast = false;
      }, 3000);
    },
  },
};
</script>

<style scoped>
@import './Register.css';
</style>