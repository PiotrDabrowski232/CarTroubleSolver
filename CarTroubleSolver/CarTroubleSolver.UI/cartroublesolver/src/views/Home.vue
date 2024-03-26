<template>
    
    <div class="MainContent">
        <h1 v-if="token == null">
            Home
        </h1>
        <p  v-if="token != null">welcome logged user: {{ token }}</p>
        <button :on-click="Logout()">Logout</button>
    </div>
  </template>
    
  <script>

    export default {
  name: 'Home',
  data(){
    return {
      token:  localStorage.getItem('token')
    }
  },
  methods:{
    Logout: function(){
      localStorage.clear()
    }
  }
  }
  </script>
  
  
    
  <style>
  .MainContent{
    padding-top: 5vh;
  }
  </style>