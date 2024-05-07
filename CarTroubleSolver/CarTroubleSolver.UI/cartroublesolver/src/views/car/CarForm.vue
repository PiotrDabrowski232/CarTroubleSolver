<template>
  <form>

    <select v-model="selectedBrand" class="form-select" aria-label="Default select example">
        <option v-for="(brand, index) in brands" :key="index" :value="brand">
            {{ brand }}
        </option>
    </select>

    <select v-model="selectedModel" class="form-select" aria-label="Default select example">
        <option v-for="(model, index) in models" :key="index" :value="models">
            {{ model }}
        </option>
    </select>

  </form>

    
</template>
    
    
<script>
    import {fetchCarBrand} from "../../services/CarApiCommunication"
    
export default {
    name: "CarForm",
      data() {
        return {
            brands:null,
            models:null,

        }
      },

      mounted(){
      this.fetchData()
  },
      methods: {
        async fetchData(){
            this.brands = await fetchCarBrand()
        },
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
    