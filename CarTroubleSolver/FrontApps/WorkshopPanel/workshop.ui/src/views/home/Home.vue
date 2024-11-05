<template>

  <div>
    <RouterLink to="/ReceiveMessage">
        <h3>Home</h3>
    </RouterLink>
  </div>

</template>





<script>

export default {
  name: 'Home',
  props: {
    msg: String
  }
}
</script>




<style scoped>
h4 {
  margin: 0;
}
</style>