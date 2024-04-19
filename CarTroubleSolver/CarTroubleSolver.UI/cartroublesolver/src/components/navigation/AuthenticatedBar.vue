<template>

    <nav class="navbar navbar-expand-lg navbar-light bg-light">
      <router-link class="navbar-brand router-link" to="/"  exact-path>Home</router-link>
      <div class="collapse navbar-collapse justify-content-end" id="navbarNav">
        <ul class="navbar-nav">
          <li class="nav-item">
            <button v-on:click="Logout()">Logout</button>
          </li>
          <li class="nav-item">
            <router-link class="router-link" to="/UserInfo" exact-path>User Info</router-link>
          </li>
        </ul>
      </div>
    </nav>

</template>
      
  <script>
  import AuthService from '../../services/AuthService'
  
  export default {
    name: 'UnAuthenticatedBar',
    data() {
      return {
      };
    },
    methods: {
      Logout() {
        AuthService.removeToken()
        this.$router.push("/")
        this.$emit('refresh');
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
      .nav-item .router-link{
        color:black;
        padding-left: 2vw;
        text-decoration: none;
      }
      .main{
        width: 100%;
        min-height: 87vh;
      }
      .router-view{
        height: 100%;
        width: 100%;
      }
      .footer{
        background-color: rgb(0, 0, 0);
        width: 100%;
        height: 5vh;
      }
      button{
        background-color: white;
        border: 0px;
      }
      </style>
      