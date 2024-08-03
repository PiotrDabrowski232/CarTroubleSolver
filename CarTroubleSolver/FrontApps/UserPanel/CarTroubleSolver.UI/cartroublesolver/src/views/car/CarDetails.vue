<template>
    
    <div>
      <h5>VIN: {{ vin }}</h5>
    </div>

  </template>
  
  <script>
  export default {
    name: 'CarDetails',
    computed: {
      vin() {
        return this.$route.params.vin
      }
    }
  }
  </script>
  