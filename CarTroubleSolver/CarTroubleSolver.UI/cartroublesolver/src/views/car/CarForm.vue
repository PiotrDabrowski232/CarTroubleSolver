<template>

    <form id="myForm" class="needs-validation" novalidate @submit.prevent="tryCreate">
      
        <select v-model="this.selectedBrand" class="form-select" aria-label="Default select example" @change="fetchModels">
            <option v-for="(brand, index) in brands" :key="index" :value="brand" placeholder="Brand">
                {{ brand }}
            </option>
        </select>
        

        <select v-model="this.selectedModel" class="form-select" aria-label="Default select example" :disabled="!this.selectedBrand">
            <option v-for="(model, index) in models" :key="index" :value="model" >
                {{ model }}
            </option>
        </select>

        <select v-model="this.selectedType" class="form-select" aria-label="Default select example">
            <option v-for="(type, index) in types" :key="index" :value="type" > 
                {{ type }}
            </option>
        </select>

        <div class="form-floating">
        <input v-model="Car.VIN"  type="number" :minlength="9" :maxlength="9" class="form-control" id="VIN" placeholder="VIN" >
        <label for="PhoneNumber">VIN</label>
          <div v-if="errorFromApi.VIN" class="invalid-feedback">
            <div v-for="(item, index) in errorFromApi.VIN" :key="index">
              - {{ item }}
            </div>
          </div>
      </div>

      <div class="form-floating">
        <input v-model="Car.DoorCount"  type="number" :minlength="9" :maxlength="9" class="form-control" id="DoorCount" placeholder="DoorCount" >
        <label for="DoorCount">Door Count</label>
          <div v-if="errorFromApi.DoorCount" class="invalid-feedback">
            <div v-for="(item, index) in errorFromApi.DoorCount" :key="index">
              - {{ item }}
            </div>
          </div>
      </div>

      <div class="form-floating">
        <input v-model="Car.Color"  type="text"  class="form-control" id="Color" placeholder="Color" >
        <label for="Color">Color</label>
          <div v-if="errorFromApi.Color" class="invalid-feedback">
            <div v-for="(item, index) in errorFromApi.Color" :key="index">
              - {{ item }}
            </div>
          </div>
      </div>

      <div class="form-floating">
          <input v-model="Car.DateOfProduction" type="date" class="form-control" id="DateOfProduction" placeholder="DateOfProduction" >
          <label for="DateOfProduction">Date Of Production</label>
          <div v-if="errorFromApi.DateOfProduction" class="invalid-feedback">
            <div v-for="(item, index) in errorFromApi.DateOfProduction" :key="index">
              - {{ item }}
            </div>
          </div>
        </div>

        <button type="submit" class="btn btn-primary">Create Car</button>

    </form>
</template>

<script>
    import { fetchCarBrand, fetchCarModels, CreateCar } from "../../services/CarApiCommunication"

    export default {
        name: "CarForm",
        data() {
            return {
                brands: null,
                models: null,
                selectedBrand: null,
                selectedModel: null,
                types: null,
                selectedType: null,
                Car: {
                    VIN: null,
                    Brand: null,
                    Model: null,
                    Color: null,
                    DoorCount: null,
                    DateOfProduction: null,
                    Type: null,
                },

                errorFromApi:{
                    VIN: null,
                    Brand: null,
                    Model: null,
                    Color: null,
                    DoorCount: null,
                    DateOfProduction: null,
                    Type: null,
                }
            }
        },
        mounted() {
            this.fetchData()
        },
        methods: {
            async fetchData() {
                var data = await fetchCarBrand()
                this.brands = data.brands
                this.types = data.types
            },
            async fetchModels() {
                console.log(this.models)
                if (this.selectedBrand) {
                    this.models = await fetchCarModels(this.selectedBrand)
                    console.log(this.models)
                }
            },
            async tryCreate(){
              this.Car.Brand = this.selectedBrand
              this.Car.Model = this.selectedModel
              this.Car.Type = this.selectedType
              var response = await CreateCar(this.Car)
              console.log(response)
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
    