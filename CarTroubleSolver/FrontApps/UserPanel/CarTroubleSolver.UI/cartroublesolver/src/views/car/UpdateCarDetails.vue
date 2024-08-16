<template>
  <div>
    <Toast position="top-center" />
    <form id="myForm" class="needs-validation" novalidate @submit.prevent="tryUpdate">

      <div class="row">
        <div class="col">
          <select id="brandSelect" v-model="this.Car.brand" class="form-select" @change="fetchModels()" :class="{'is-invalid': errors.selectedBrand}">
            <option v-for="(brand, index) in brands" :key="index" :value="brand">
              {{ brand }}
            </option>
          </select>
          <div v-if="errors.selectedBrand" class="invalid-feedback">
            {{ errors.selectedBrand }}
          </div>
        </div>

        <div class="col">
          <select v-model="this.Car.model" class="form-select" aria-label="Default select example" :class="{'is-invalid': errors.selectedModel}">
            <option v-for="(model, index) in models" :key="index" :value="model">
              {{ model }}
            </option>
          </select>
          <div v-if="errors.selectedModel" class="invalid-feedback">
            {{ errors.selectedModel }}
          </div>
        </div>
      </div>

      <select v-model="this.Car.type" class="form-select" aria-label="Default select example" :class="{'is-invalid': errors.selectedType}">
        <option :value="null" disabled>Select Type</option>
        <option v-for="(type, index) in types" :key="index" :value="type">
          {{ type }}
        </option>
      </select>
      <div v-if="errors.selectedType" class="invalid-feedback">
        {{ errors.selectedType }}
      </div>
      
      <div class="row">
            <div class="col">
              <div class="form-floating">
                <input v-model="Car.vin" type="number" class="form-control" id="vin" placeholder="vin" :class="{'is-invalid': errors.VIN}">
                <label for="vin">VIN</label>
                <div v-if="errors.VIN" class="invalid-feedback">
                  {{ errors.VIN }}
                </div>
              </div>
            </div>


              <div class="col">
                <div class="form-floating">
                  <input v-model="Car.mileage" type="number" class="form-control" id="Mileage" placeholder="Mileage" :class="{'is-invalid': errors.Mileage}">
                  <label for="Mileage">Mileage</label>
                  <div v-if="errors.Mileage" class="invalid-feedback">
                    {{ errors.Mileage }}
                  </div>
                </div>
              </div>

              <div class="col">
                <div class="form-floating">
                  <input v-model="Car.engine" type="text" class="form-control" id="Engine" placeholder="Engine" :class="{'is-invalid': errors.Engine}">
                  <label for="Engine">Engine</label>
                  <div v-if="errors.Engine" class="invalid-feedback">
                    {{ errors.Engine }}
                  </div>
                </div>
              </div>
            </div>

      <div class="row">
        <div class="col">
          <div class="form-floating">
            <input v-model="this.Car.doorCount" type="number" class="form-control" id="DoorCount" placeholder="Door Count" :class="{'is-invalid': errors.DoorCount}">
            <label for="DoorCount">Door Count</label>
            <div v-if="errors.DoorCount" class="invalid-feedback">
              {{ errors.DoorCount }}
            </div>
          </div>
        </div>

        <div class="col">
          <div class="form-floating">
            <input v-model="this.Car.dateOfProduction" type="date" class="form-control" id="DateOfProduction" placeholder="Date Of Production" :class="{'is-invalid': errors.DateOfProduction}">
            <label for="DateOfProduction">Date Of Production</label>
            <div v-if="errors.DateOfProduction" class="invalid-feedback">
              {{ errors.DateOfProduction }}
            </div>
          </div>
        </div>
      </div>

      <button type="submit" class="btn btn-primary">Update Data</button>
    </form>
  </div>
</template>

  
<script>
import { fetchCarBrand, fetchCarModels, UpdateData } from "../../services/CarApiCommunication";
import router from '@/router';

export default {
  name: "UpdateCarDetails",
  data() {
    return {
      brands: null,
      models: null,
      types: null,
      Car: {
        vin: null,
        brand: null,
        model: null,
        doorCount: null,
        dateOfProduction: null,
        type: null,
        mileage: null,
        engine: null
      },
      errors: {
        VIN: null,
        selectedBrand: null,
        selectedModel: null,
        selectedType: null,
        DoorCount: null,
        DateOfProduction: null,
        Engine:null,
        Mileage: null,
      },
    };
  },
  mounted() {
    this.fetchCarByVIN();
    this.fetchData();
  },
  methods: {
    fetchCarByVIN() {
      const userCars = JSON.parse(localStorage.getItem("userCars")) || [];
      const car = userCars.find(car => car.vin == this.$route.params.vin);
      if (car) {
        this.Car = { ...car, dateOfProduction: this.formatDate(new Date(car.dateOfProduction)) };
        this.fetchModels();
      } else {
        console.warn("No car found with the specified VIN");
      }
    },
    formatDate(date) {
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0'); 
        const day = String(date.getDate()).padStart(2, '0');
        return `${year}-${month}-${day}`;
    },
    async fetchData() {
      const data = await fetchCarBrand();
      this.brands = data.brands;
      this.types = data.types;
    },
    async fetchModels() {
      if (this.Car.brand) {
        this.models = await fetchCarModels(this.Car.brand);
      }
    },
    async tryUpdate(){
      try{
          if (!this.validateFields()) {
          this.$toast.add({ severity: 'error', summary: 'Please fill all required fields correctly.', life: 3000 });
          return;
        }

        await UpdateData(this.Car, this.$route.params.vin)

        localStorage.clear()
        
        this.$toast.add({ severity: 'success', summary: 'Car updated successfully', life: 3000 });

        setTimeout(() => {
          router.push("/UserInfo")
        }, 3000);

      }catch(error){

        const errorData = error.response && error.response.data && error.response.data.errors;

        if (errorData) {
            this.errors.VIN = errorData.VIN ? errorData.VIN[0] : null;
            this.errors.DoorCount = errorData.DoorCount ? errorData.DoorCount[0] : null;
            this.errors.DateOfProduction = errorData.DateOfProduction ? errorData.DateOfProduction[0] : null;
            this.errors.Mileage = errorData.Mileage ? errorData.Mileage[0] : null;
            this.errors.Engine = errorData.Engine ? errorData.Engine[0] : null;
        }
      }
    },
    validateFields() {

      let valid = true;

      console.log(this.Car)
      this.errors.VIN = this.Car.vin ? null || "" : "VIN is required.";
      this.errors.selectedBrand = this.Car.brand ? null || "" : "Brand is required.";
      this.errors.selectedModel = this.Car.model ? null || "" : "Model is required.";
      this.errors.selectedType = this.Car.type ? null || "" : "Type is required.";
      this.errors.DoorCount = this.Car.doorCount ? null || "" : "Door Count is required.";
      this.errors.DateOfProduction = this.Car.dateOfProduction ? null || "" : "Date of Production is required.";
      this.errors.Mileage = this.Car.mileage ? null || "" : "Mileage is required.";
      this.errors.Engine = this.Car.engine ? null || "" : "Engine is required.";

      for (let key in this.errors) {
        if (this.errors[key]) valid = false;
      }
      
      return valid;
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

.invalid-feedback{
  font-size: 65%;
  text-align: left;
}

  </style>