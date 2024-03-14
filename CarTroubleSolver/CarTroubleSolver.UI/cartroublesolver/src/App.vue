<template>
<nav class="navbar navbar-expand-lg navbar-light bg-light">
  <a class="navbar-brand" href="#">Navbar</a>

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


  <div class="main">
      
    <router-view class="router-view" v-slot="{Component}">
      <transition name="slide-fade" mode='out-in'>
        <Component :is="Component"/>
      </transition>
    </router-view>

  </div>



  <div class="footer">

  </div>


</template>

<script>



export default {
  name: 'App',
  components:{
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
</style>./components/LoginPage.vue./Views/LoginPage.vue./Views/RegisterPage.vue
