<template>

    <BaseNav />

  <div class="mainContent">
    <RouterView />
  </div>

  <div class="footer">
    
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
body{
  margin: 0;
}

.mainContent{
  height: 87.9vh;
  width: 100%;
}
.footer{
  background-color: black;
  width: 100%;
  height: 5.5vh;
}

</style>
