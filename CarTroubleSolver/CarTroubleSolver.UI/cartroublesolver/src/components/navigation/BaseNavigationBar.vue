<template>
    <AuthenticatedBar @refresh="refreshParent" v-if="this.isAuthenticated"/>
    <UnAuthenticatedBar v-if="!this.isAuthenticated"/>
</template>
    
<script>
import AuthService from '../../services/AuthService';
import AuthenticatedBar from './AuthenticatedBar.vue';
import UnAuthenticatedBar from './UnAuthenticatedBar.vue';

export default {
  name: 'BaseNavigationBar',
  components:{
    AuthenticatedBar, UnAuthenticatedBar
  },
  data() {
    return {
      isAuthenticated: AuthService.getToken() 
    };
  },
  computed: {
    userComponent(){
      console.log(this.isAuthenticated)
      return this.isAuthenticated ;
    }
  },
  watch: {
    '$route'() {
      this.isAuthenticated = AuthService.getToken();
    }
  },
  methods:{
    refreshParent() {
      this.isAuthenticated = AuthService.getToken();
    }
  }
}
</script>
    
    <style>
    
    body{
      margin:auto;
      background-color: white;
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
    