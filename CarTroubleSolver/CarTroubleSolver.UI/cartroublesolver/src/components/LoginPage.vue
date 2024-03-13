<template>

      <div class="main">
        <h1>Login Vue</h1>
      </div>
    
</template>
    
    <script>

    export default {
  name: 'LoginPage',
  data(){
    return {
      component:''
    }
  }
}

    </script>
    
    <style>
    
    body{
      margin:auto;
    }
    
    #app {
      font-family: Avenir, Helvetica, Arial, sans-serif;
      -webkit-font-smoothing: antialiased;
      -moz-osx-font-smoothing: grayscale;
      text-align: center;
      color: #2c3e50;
    }
    nav{
      width: 100%;
      height: 8vh;
      border-bottom: 1px solid black;
    }
    
    .navbar-brand{
      padding-left: 2.8vw;
      margin-top: 1vw;
    }
    
    #navbarNav{
      margin-right: 4vw;
    }
    
    .navbar-nav{
      margin-top: 1.2vh;
      padding-right: 2vw;
    }
    
    .main{
      width: 100%;
      height: 87vh;
    }
    
    .footer{
      background-color: rgb(0, 0, 0);
      width: 100%;
      height: 5vh;
    }
    </style>
    