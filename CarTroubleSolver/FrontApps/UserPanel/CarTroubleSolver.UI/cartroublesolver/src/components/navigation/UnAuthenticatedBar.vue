<template>

    <nav class="navbar navbar-expand-lg navbar-light bg-light">
      <router-link class="navbar-brand router-link" to="/"  exact-path>Home</router-link>
      <div class="collapse navbar-collapse justify-content-end" id="navbarNav">
        <ul class="navbar-nav">
          <li class="nav-item">
            <router-link class="router-link" to="/Login"  exact-path>Login</router-link>
          </li>
          <li class="nav-item">
            <router-link class="router-link" to="/Register" exact-path>Register</router-link>
          </li>
        </ul>
      </div>
    </nav>

  </template>
      
  <script>
import AuthService from '../../services/AuthService'
  
  export default {
    name: 'AuthenticatedBar',
    data() {
      return {
      };
    },
    methods: {
      Logout() {
        AuthService.removeToken();
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
      </style>
      