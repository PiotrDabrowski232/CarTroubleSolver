<template>

    <nav class="navbar navbar-expand-lg navbar-light bg-light">
      <router-link class="navbar-brand router-link" to="/"  exact-path>Home</router-link>
      <div class="collapse navbar-collapse justify-content-end" id="navbarNav">

        <div class="dropdown">
          <button class="btn  dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
            <i class="pi pi-user" style="font-size: 1.5rem; color:darkgreen; "></i>
          </button>
          <ul class="dropdown-menu">
            <li class="dropdown-item">
              <router-link v-on:click="Logout()" class="router-link" to="/" exact-path>Logout</router-link>
            </li>
            <li class="dropdown-item">
              <router-link class="router-link" to="/UserInfo" exact-path>User Info</router-link>
            </li>
          </ul>
        </div>

        <button type="button" class="btn position-relative" data-bs-toggle="offcanvas" data-bs-target="#staticBackdrop" aria-controls="staticBackdrop">
          <i class="pi pi-envelope" style="font-size: 1.5rem; color:darkgreen; "></i>
          <span class="position-absolute top-0 start-100 translate-middle p-2 bg-danger border border-light rounded-circle">
            <span class="visually-hidden">New alerts</span>
          </span>
        </button>
      </div>
    </nav>

    <div class="offcanvas offcanvas-start" data-bs-backdrop="static" tabindex="-1" id="staticBackdrop" aria-labelledby="staticBackdropLabel">
        <div class="offcanvas-header">
          <h5 class="offcanvas-title" id="staticBackdropLabel">Offcanvas</h5>
          <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
        </div>
        <div class="offcanvas-body">
          <div>
            I will not close if you click outside of me.
          </div>
        </div>
    </div>

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
        localStorage.clear()
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
      
      #navbarNav{
        margin-right: 4vw;
      }
      
      .dropdown{
        margin-right: vw;
      }

      .navbar-nav{
        margin-top: 1.2vh;
        padding-right: 2vw;
      }
      .dropdown-item .router-link{
        color:black;
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
      