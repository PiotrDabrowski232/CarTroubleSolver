<template>
  <div class="row">
    <div class="col-md-8">
      <div class="account-details-container">
        <div class="account-info">
          <h2>User Account</h2>
          <p><strong>Name:</strong> {{ Workshop.name }}</p>
          <p><strong>Email:</strong> {{ Workshop.email }}</p>
          <p><strong>Phone Number:</strong> {{ Workshop.phoneNumber }}</p>
          <p><strong>NIP:</strong> {{ Workshop.nip }}</p>
        </div>
      
        <div class="address-info">
          <h2>Address</h2>
          <p><strong>Street:</strong> {{ Workshop.adress.streetName }} {{ Workshop.adress.streetNumber }}</p>
          <p><strong>Postal Code:</strong> {{ Workshop.adress.postalCode }}</p>
          <p><strong>City:</strong> {{ Workshop.adress.city }}</p>
          <p><strong>Province:</strong> {{ Workshop.adress.province }}</p>
          <p><strong>Country:</strong> {{ Workshop.adress.country }}</p>
        </div>

        <div class="button-group mt-2">
          <button class="btn btn-primary me-2">Change Password</button>
          <button class="btn btn-secondary me-2">Some Actions</button>
          <button class="btn btn-secondary">Some Actions</button>
        </div>
      </div>
    </div>

    <div class="col-md-4 d-flex justify-content-center align-items-center">
      <div class="opening-hours">
        <button class="btn btn-warning btn-block mb-3 w-100" v-on:click="changeSite()">Change Opening Hours</button>
        <ul class="list-group">
          <li class="list-group-item">Monday <p>{{this.DisplayHours("Monday")}}</p></li>
          <li class="list-group-item">Tuesday <p>{{this.DisplayHours("Tuesday")}}</p></li>
          <li class="list-group-item">Wednesday <p>{{this.DisplayHours("Wednesday")}}</p></li>
          <li class="list-group-item">Thursday <p>{{this.DisplayHours("Thursday")}}</p></li>
          <li class="list-group-item">Friday <p>{{this.DisplayHours("Friday")}}</p></li>
        </ul>
      </div>
    </div>
  </div>
</template>


<script>
import { getFromLocalStorage } from '@/Services/LocalStorageService';
import router from '@/router';

export default {
  name: 'AccountDetails',
  data() {
    return {
      Workshop: getFromLocalStorage("Workshop") || {}
    };
  },
  methods:{
    changeSite(){
      router.push("/Hours")
    },
    DisplayHours(dayName){
      const hoursForDay = this.Workshop.hours.find(x => x.dayOfWeek == dayName)
        if (hoursForDay) {
          return `${hoursForDay.from} - ${hoursForDay.to}`;
      } else {
          return "--";
      }
    }
  }
}
</script>

<style scoped>
@import './AccountDetails.css';
</style>
