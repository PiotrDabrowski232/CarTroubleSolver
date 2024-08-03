<template>
    <form id="myForm" class="needs-validation" novalidate @submit.prevent="tryCreate">
      <select v-model="selectedBrand" class="form-select" aria-label="Default select example" @change="fetchModels">
        <option v-for="(brand, index) in brands" :key="index" :value="brand">
          {{ brand }}
        </option>
      </select>
  
      <select v-model="selectedModel" class="form-select" aria-label="Default select example" :disabled="!selectedBrand">
        <option v-for="(model, index) in models" :key="index" :value="model">
          {{ model }}
        </option>
      </select>
  
      <select v-model="selectedType" class="form-select" aria-label="Default select example">
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
  
      <div class="form-floating">
        <input v-model="Car.DoorCount" type="number" class="form-control" id="DoorCount" placeholder="Door Count">
        <label for="DoorCount">Door Count</label>
        <div v-if="errorFromApi.DoorCount" class="invalid-feedback">
          <div v-for="(item, index) in errorFromApi.DoorCount" :key="index">
            - {{ item }}
          </div>
        </div>
      </div>
  
      <ColorPicker v-model="selectedColor" inputId="cp-rgb" format="rgb" class="mb-3" />
  
      <div class="form-floating">
        <input v-model="Car.DateOfProduction" type="date" class="form-control" id="DateOfProduction" placeholder="Date Of Production">
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
        },
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
    width: 10vw;
    margin-left: 10vw;
    margin-top: 10vh;
  }
  </style>
  