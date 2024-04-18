<template>
  
    <div class="MainContent">
        <h1>
            Home
        </h1>
        <p v-if="isLoggedIn()">welcome logged user: {{   }}</p>
        <button v-if="isLoggedIn()" v-on:click="Logout()">Logout</button>
    </div>
  </template>
    
  <script>
import AuthService from '../services/AuthService';

    export default {
  name: 'Home',
  data(){
    return {
    }
  },
  methods:{
    Logout(){
      AuthService.removeToken()
      
    },
    isLoggedIn() {
      if(!AuthService.getToken()){
        return false;
      }else{
        return true;
      }
}

  }
  }
  </script>
  
  
    
  <style>
  .MainContent{
    padding-top: 5vh;
  }
  </style>