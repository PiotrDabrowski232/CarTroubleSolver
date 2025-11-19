<template>
  <BaseNav />
  <div class="mainContent">
    <RouterView />
  </div>
  <div class="footer">
    <p style="color: white; text-align: center; margin: 0;">© 2024 My Application</p>
  </div>
</template>

<script>
import BaseNav from './views/navs/BaseNav.vue'

export default {
  name: 'App',
  components: {
    BaseNav
  },
}
</script>

<style>
body {
  margin: 0;
  position: relative; 
  min-height: 100vh; 
}

.mainContent {
  min-height: calc(100vh - 5.5vh); 
  width: 100%;
}

.footer {
  background-color: black;
  width: 100%;
  height: 4vh;
  position: absolute; 
  bottom: 0; 
}
</style>
