<template>
  <form>

    <select v-model="selected" class="form-select" aria-label="Default select example">
        <option v-for="(brand, index) in brands" :key="index" :value="brand">
            {{ brand }}
        </option>
    </select>

  </form>

    
</template>
    
    
<script>
    
export default {
    name: "CarForm",
      data() {
        return {
            brands:null,

        }
      },
      methods: {
        
    },
      
}

</script>
    
    
    
    
    
<style scoped>
.form-select{
    width: 10vw;
    margin-left: 10vw;
    margin-top: 10vh;

}
    
</style>
    