<template>
<component v-bind:is="component"></component>
<nav class="navbar navbar-expand-lg navbar-light bg-light">
  <a class="navbar-brand" href="#">Navbar</a>

  <div class="collapse navbar-collapse justify-content-end" id="navbarNav">
    <ul class="navbar-nav">
      <li class="nav-item">
        <a class="nav-link" v-on:click="component='LoginPage'" href="#">Login</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" v-on:click="component='RegisterPage'" href="#">Register</a>
      </li>
    </ul>
  </div>
</nav>

<nav>
                    <router-link to="/Register"  exact-path>Contact</router-link>
                    <router-link to="/Login"  exact-path>Contact</router-link>
</nav>  
<div class="rout-content">
                    <router-view v-slot="{Component}">
                        <transition name="slide-fade" mode='out-in'>
                            <Component :is="Component"/>
                        </transition>
                    </router-view>
                </div>
  <div class="main">

  </div>



  <div class="footer">

  </div>


</template>

<script>
import LoginPage from './components/LoginPage.vue'
import RegisterPage from './components/RegisterPage.vue'



export default {
  name: 'App',
  components:{
    'LoginPage': LoginPage,
    'RegisterPage': RegisterPage,
  },
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
</style>./components/LoginPage.vue
