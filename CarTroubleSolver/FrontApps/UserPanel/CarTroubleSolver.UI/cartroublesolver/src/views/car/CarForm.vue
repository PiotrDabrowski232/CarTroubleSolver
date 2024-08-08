<template>
    <form id="myForm" class="needs-validation" novalidate @submit.prevent="tryCreate">

      <div class="row">

        <div class="col">
          <select id="brandSelect" v-model="selectedBrand" class="form-select" @change="fetchModels">
          <option :value="null" disabled>Select Brand</option>
          <option v-for="(brand, index) in brands" :key="index" :value="brand">
            {{ brand }}
          </option>
        </select>
        </div>

        <div class="col">
          <select v-model="selectedModel" class="form-select" aria-label="Default select example" :disabled="!selectedBrand">
            <option :value="null" disabled>Select Model</option>
            <option v-for="(model, index) in models" :key="index" :value="model">
              {{ model }}
            </option>
          </select>
        </div>
        
      </div>

      <select v-model="selectedType" class="form-select" aria-label="Default select example">
        <option :value="null" disabled>Select Type</option>
        <option v-for="(type, index) in types" :key="index" :value="type">
          {{ type }}
        </option>
      </select>
  

      
          <div class="form-floating">
            <input v-model="Car.VIN" type="number" class="form-control" id="VIN" placeholder="VIN">
            <label for="VIN">VIN</label>
            <div v-if="errorFromApi.VIN" class="invalid-feedback">
              <div v-for="(item, index) in errorFromApi.VIN" :key="index">
                - {{ item }}
              </div>
            </div>
          </div>

    <div class="row">
      <div class="col">
        <div class="form-floating">
          <input v-model="Car.DoorCount" type="number" class="form-control" id="DoorCount" placeholder="Door Count">
          <label for="DoorCount">Door Count</label>
          <div v-if="errorFromApi.DoorCount" class="invalid-feedback">
            <div v-for="(item, index) in errorFromApi.DoorCount" :key="index">
              - {{ item }}
            </div>
          </div>
        </div>
      </div>

      <div class="col">
        <div class="form-floating">
          <input v-model="Car.DateOfProduction" type="date" class="form-control" id="DateOfProduction" placeholder="Date Of Production">
          <label for="DateOfProduction">Date Of Production</label>
          <div v-if="errorFromApi.DateOfProduction" class="invalid-feedback">
            <div v-for="(item, index) in errorFromApi.DateOfProduction" :key="index">
              - {{ item }}
            </div>
          </div>
        </div>
      </div>

    </div>

    <div class="different-pickers">
      <div class="mb-2">
        <label>Select Color:</label>
        <ColorPicker v-model="selectedColor" inputId="cp-rgb" format="rgb" class="mb-3" />
      </div>

      <div class="mb-2">
        <label>Select Photo:</label>
        <input class="form-control file-control" type="file" id="formFile">
      </div>
    </div>

      <button type="submit" class="btn btn-primary">Create Car</button>
    </form>

  </template>
  
  <script>
  import { fetchCarBrand, fetchCarModels, CreateCar } from "../../services/CarApiCommunication";
  
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
        selectedColor: { red: 0, green: 0, blue: 0 },
        Car: {
          VIN: null,
          Brand: null,
          Model: null,
          Color: {
            Red:null,
            Green: null,
            Blue:null,
          },
          DoorCount: null,
          DateOfProduction: null,
          Type: null,
        },
        errorFromApi: {
          VIN: null,
          Brand: null,
          Model: null,
          Color: null,
          DoorCount: null,
          DateOfProduction: null,
          Type: null,
        }
      };
    },
    mounted() {
      this.fetchData();
    },
    methods: {
      async fetchData() {
        const data = await fetchCarBrand();
        this.brands = data.brands;
        this.types = data.types;
      },
      async fetchModels() {
        if (this.selectedBrand) {
          this.models = await fetchCarModels(this.selectedBrand);
        }
      },
      async tryCreate() {
        this.Car.Brand = this.selectedBrand;
        this.Car.Model = this.selectedModel;
        this.Car.Type = this.selectedType;
        this.Car.Color.Red = this.selectedColor.r;
        this.Car.Color.Green = this.selectedColor.g;
        this.Car.Color.Blue = this.selectedColor.b;
        console.log(this.Car)
        try {
          const result = await CreateCar(this.Car);
          console.log('Car created successfully:', result);
        } catch (error) {
          console.error('Error creating car:', error);
        }
      },
    },
  };
  </script>
  
  <style scoped>

  .form-select {
    margin-top: 8vh;
    width: 10vw;
  }
  .form-floating > .form-control, .form-floating > .form-control-plaintext {
    width: 12vw;
}

.needs-validation{
  width: 60vw;
  margin: auto;
  padding-left: 12vw;
}

.file-control{
  width: 25vw;
}

.different-pickers{
  margin-top: 5vh;
  text-align: left;
}

.different-pickers label{
  margin-right: 1vw;
}

button{
  margin-left: 50%;
  margin-top: 4vh;
}

.different-pickers .mb-2 label{
  margin-bottom: 0.5vh;
}
  </style>
  