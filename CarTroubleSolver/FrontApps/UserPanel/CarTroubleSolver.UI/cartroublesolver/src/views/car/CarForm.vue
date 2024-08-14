<template>
  <div>
    <Toast position="top-center" />
    <form id="myForm" class="needs-validation" novalidate @submit.prevent="tryCreate">

      <div class="row">
        <div class="col">
          <select id="brandSelect" v-model="selectedBrand" class="form-select" @change="fetchModels" :class="{'is-invalid': errors.selectedBrand}">
            <option :value="null" disabled>Select Brand</option>
            <option v-for="(brand, index) in brands" :key="index" :value="brand">
              {{ brand }}
            </option>
          </select>
          <div v-if="errors.selectedBrand" class="invalid-feedback">
            {{ errors.selectedBrand }}
          </div>
        </div>

        <div class="col">
          <select v-model="selectedModel" class="form-select" aria-label="Default select example" :disabled="!selectedBrand" :class="{'is-invalid': errors.selectedModel}">
            <option :value="null" disabled>Select Model</option>
            <option v-for="(model, index) in models" :key="index" :value="model">
              {{ model }}
            </option>
          </select>
          <div v-if="errors.selectedModel" class="invalid-feedback">
            {{ errors.selectedModel }}
          </div>
        </div>
      </div>

          <select v-model="selectedType" class="form-select" aria-label="Default select example" :class="{'is-invalid': errors.selectedType}">
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
                <input v-model="Car.VIN" type="number" class="form-control" id="VIN" placeholder="VIN" :class="{'is-invalid': errors.VIN}">
                <label for="VIN">VIN</label>
                <div v-if="errors.VIN" class="invalid-feedback">
                  {{ errors.VIN }}
                </div>
              </div>
            </div>


              <div class="col">
                <div class="form-floating">
                  <input v-model="Car.Mileage" type="number" class="form-control" id="Mileage" placeholder="Mileage" :class="{'is-invalid': errors.Mileage}">
                  <label for="Mileage">Mileage</label>
                  <div v-if="errors.Mileage" class="invalid-feedback">
                    {{ errors.Mileage }}
                  </div>
                </div>
              </div>

              <div class="col">
                <div class="form-floating">
                  <input v-model="Car.Engine" type="text" class="form-control" id="Engine" placeholder="Engine" :class="{'is-invalid': errors.Engine}">
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
                  <input v-model="Car.DoorCount" type="number" class="form-control" id="DoorCount" placeholder="Door Count" :class="{'is-invalid': errors.DoorCount}">
                  <label for="DoorCount">Door Count</label>
                  <div v-if="errors.DoorCount" class="invalid-feedback">
                    {{ errors.DoorCount }}
                  </div>
                </div>
              </div>

            <div class="col">
              <div class="form-floating">
                <input v-model="Car.DateOfProduction" type="date" class="form-control" id="DateOfProduction" placeholder="Date Of Production" :class="{'is-invalid': errors.DateOfProduction}">
                <label for="DateOfProduction">Date Of Production</label>
                <div v-if="errors.DateOfProduction" class="invalid-feedback">
                  {{ errors.DateOfProduction }}
                </div>
              </div>
            </div>
          </div>

      <div class="different-pickers">
        <div class="mb-2">
          <label>Select Color:</label>
          <ColorPicker v-model="selectedColor" inputId="cp-rgb" format="rgb" class="mb-3" :class="{'is-invalid': errors.selectedColor}" />
          <div v-if="errors.selectedColor" class="invalid-feedback">
            {{ errors.selectedColor }}
          </div>
        </div>

        <div class="mb-2">
          <label>Select Photo:</label>
          <input @change="onFileSelected" class="form-control file-control" type="file" id="formFile" accept=".jpg, .jpeg, .png, .gif" :class="{'is-invalid': errors.CarPhoto}">
          <div v-if="errors.CarPhoto" class="invalid-feedback">
            {{ errors.CarPhoto }}
          </div>
        </div>
      </div>

      <button type="submit" class="btn btn-primary">Create Car</button>
    </form>
  </div>
</template>

  
<script>
import { fetchCarBrand, fetchCarModels, CreateCar } from "../../services/CarApiCommunication";
import router from '@/router';

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
          Red: null,
          Green: null,
          Blue: null,
        },
        DoorCount: null,
        DateOfProduction: null,
        Type: null,
        Mileage: null,
        Engine: null
      },
      errors: {
        VIN: null,
        selectedBrand: null,
        selectedModel: null,
        selectedType: null,
        DoorCount: null,
        DateOfProduction: null,
        selectedColor: null,
        CarPhoto: null,
        Mileage: null,
        Engine: null
      },
      CarPhoto: {
        Vin: null,
        Photo: null,
      }
    };
  },
  mounted() {
    this.fetchData();
  },
  methods: {
    onFileSelected(event) {
      const file = event.target.files[0];
      if (file) {
        this.CarPhoto.Photo = file;
      }
    },
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
    validateFields() {
      let valid = true;
      
      this.errors.VIN = this.Car.VIN ? null : "VIN is required.";
      this.errors.selectedBrand = this.selectedBrand ? null : "Brand is required.";
      this.errors.selectedModel = this.selectedModel ? null : "Model is required.";
      this.errors.selectedType = this.selectedType ? null : "Type is required.";
      this.errors.DoorCount = this.Car.DoorCount ? null : "Door Count is required.";
      this.errors.DateOfProduction = this.Car.DateOfProduction ? null : "Date of Production is required.";
      this.errors.Mileage = this.Car.Mileage ? null : "Mileage is required.";
      this.errors.Engine = this.Car.Engine ? null : "Engine is required.";

      for (let key in this.errors) {
        if (this.errors[key]) valid = false;
      }
      
      return valid;
    },
    async tryCreate() {
      if (!this.validateFields()) {
        this.$toast.add({ severity: 'error', summary: 'Please fill all required fields correctly.', life: 3000 });
        return;
      }
      this.Car.Brand = this.selectedBrand;
      this.Car.Model = this.selectedModel;
      this.Car.Type = this.selectedType;
      this.Car.Color.Red = this.selectedColor.r;
      this.Car.Color.Green = this.selectedColor.g;
      this.Car.Color.Blue = this.selectedColor.b;
      this.CarPhoto.Vin = this.Car.VIN;
      try {
        await CreateCar(this.Car, this.CarPhoto);

        localStorage.clear()
        
        this.$toast.add({ severity: 'success', summary: 'Car added successfully', life: 3000 });

        setTimeout(() => {
          router.push("/UserInfo")
        }, 3000);
        
      } catch (error) {
        this.$toast.add({
            severity: 'error',
            summary: 'An error occurred while creating the car.',
            life: 3000
        });
        console.error('Error creating car:', error);

        const errorData = error.response && error.response.data && error.response.data.errors;
        if (errorData) {
            this.errors.VIN = errorData.VIN ? errorData.VIN[0] : null;
            this.errors.selectedBrand = errorData.Brand ? errorData.Brand[0] : null;
            this.errors.selectedModel = errorData.Model ? errorData.Model[0] : null;
            this.errors.selectedType = errorData.Type ? errorData.Type[0] : null;
            this.errors.DoorCount = errorData.DoorCount ? errorData.DoorCount[0] : null;
            this.errors.DateOfProduction = errorData.DateOfProduction ? errorData.DateOfProduction[0] : null;
            this.errors.Mileage = errorData.Mileage ? errorData.Mileage[0] : null;
            this.errors.Engine = errorData.Engine ? errorData.Engine[0] : null;
        }
    }
    },
  },
};
</script>

  
<style scoped>

  .form-select {
    margin-top: 4.5vh;
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
  