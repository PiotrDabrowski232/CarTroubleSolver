<template>
  <div>
    <Toast position="top-center"/>
    <div class="MainContent">
      <button class="home-button" @click="this.WorkshopApoitment()">
        <p>Workshop appointment</p>
        <img :src="require('@/assets/workshop.jpg')" >
      </button>

      <button class="home-button" @click="this.BreakdownEventForm()">
        <p>Add Car Breakdown Event</p>
        <img :src="require('@/assets/workshop.jpg')" >
      </button>

      <button class="home-button" @click="this.MyEvents()">
        <p>My Car Breakdown Events</p>
        <img :src="require('@/assets/mybreakdown.jpg')" >
      </button>
    </div>
  </div>
</template>

<script>
import AuthService from '../services/AuthService';
import router from '@/router';

export default {
  name: 'Home',
  data() {
    return {
      LoginUser: false,
    }
  },
  mounted() {
    this.isLogged()
  },
  methods:{
    isLogged(){
      if(AuthService.getToken() !== null && AuthService.getToken() !== undefined){
        return true;
      }
      else{
        return false;
      }
    },
    WorkshopApoitment(){
      if(this.isLogged()){
        router.push("/WorkshopApoitment")
      }
      else{
        this.WarningToast();
      }
    },
    BreakdownEventForm(){
      if(this.isLogged()){
        router.push("/BreakdownForm")
      }
      else{
        this.WarningToast();
      }
    },
    MyEvents(){
      if(this.isLogged()){
        router.push("/MyEvents")
      }
      else{
        this.WarningToast();
      }
    },
    WarningToast(){
      return this.$toast.add({severity: 'info' , summary: 'Log in to gain access' , life: 3000 });
    }
  }
}
</script>

<style>
.MainContent {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
  align-items: center;
  height: 85vh;
  padding-top: 5vh;
}

.home-button {
  position: relative;
  flex: 1 1 30%;
  height: 90%;
  margin: 1%;
  border: none;
  border-radius: 100px;
  overflow: hidden;
  text-align: center;
  font-size: 2em;
  font-weight: 700;
  letter-spacing: 0.125em;
  color: black;
  background-color: transparent;
  cursor: pointer;
}

.home-button p {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  z-index: 2;
  margin: 0;
  transition: opacity 0.5s ease; 
  color: white;
}

.home-button img {
  width: 100%;
  height: 100%;
  object-fit: cover;
  filter: brightness(50%);
  transition: filter 0.5s ease; 
}

.home-button:hover img {
  filter: brightness(100%);
}

.home-button:hover p {
  opacity: 0;
}
</style>
