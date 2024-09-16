<template>
    <div class="panel">
      <RouterLink class="homeLink" to="/"><h3>Home</h3></RouterLink>
      <nav>
        <RouterLink to="/Login">Login</RouterLink>
        <RouterLink to="/Register">Register</RouterLink>
      </nav>
    </div>
  </template>
  
  <script>
  export default {
    name: 'UnauthorizedNavBar',
  }
  </script>
  
  <style scoped>
    @import './navsStyle.css';
  </style>
  