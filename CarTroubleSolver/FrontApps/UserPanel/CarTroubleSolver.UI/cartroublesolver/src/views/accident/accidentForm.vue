<template>
  <div class="formMessageContainer">
    <button type="button" @click="checkWorkshops" class="btn btn-outline-dark button-top-right">Check Workshops</button>
    <form novalidate @submit.prevent="sendMessage" class="formMessage">
      <div class="form-floating">
        <select ref="workshopSelect" class="form-select" id="floatingSelect" aria-label="Floating label select example" @change="FetchWorkshopServices">
          <option value="" disabled selected>Select Workshop</option>
          <option v-for="(item, index) in worskhops" :key="index" :value="item.id">
            {{ index + 1 }}. {{ item.name }} - {{ item.city }}
          </option>
        </select>
        <label for="floatingSelect">Workshop</label>
        <div v-if="errors.Worskhop" class="text-danger">{{ errors.Worskhop }}</div>
      </div>
      <div class="form-floating">
        <select ref="serviceSelect" class="form-select" id="floatingSelect" aria-label="Floating label select example">
          <option value="" disabled selected>Select Service Type You want</option>
          <option v-for="(item, index) in sevices" :key="index" :value="item.id">{{ item.serviceType }}</option>
        </select>
        <label for="floatingSelect">Service</label>
        <div v-if="errors.ServiceType" class="text-danger">{{ errors.ServiceType }}</div>
      </div>
      <div class="form-floating">
        <select ref="carSelect" class="form-select" id="floatingSelect" aria-label="Floating label select example">
          <option value="" disabled selected>Select Car</option>
          <option v-for="(item, index) in Cars" :key="index" :value="item.id">
            {{ index + 1 }}. {{ item.brand }} - {{ item.model }}
          </option>
          <option v-if="Cars.length < 1" value="" disabled>
            Add car to be able to add event
          </option>
        </select>
        <label for="floatingSelect">Car</label>
        <div v-if="errors.Car" class="text-danger">{{ errors.Car }}</div>
      </div>
      <div class="form-floating">
        <textarea ref="descriptionTextarea" class="form-control" placeholder="Describe your problem" id="floatingTextarea" style="width: 100%; height: 200%"></textarea>
        <label for="floatingTextarea">Describe problem</label>
        <div v-if="errors.Description" class="text-danger">{{ errors.Description }}</div>
      </div>
      <button type="submit" class="btn btn-primary" style="margin-top: 4vh; margin-left: 16vw;">Submit</button>
    </form>
  </div>
</template>

<script>
import router from '@/router';
import { fetchTypes } from '@/services/AccidentCommunication';
import { worskhops, sendMessage } from '@/services/UserApiCommunication';
import { fetchUserCars } from '@/services/CarApiCommunication';

export default {
  name: 'AccidentForm',
  data() {
    return {
      worskhops: [],
      sevices: [],
      Cars: [],
      errors: {
        Worskhop: '',
        ServiceType: '',
        Car: '',
        Description: ''
      },
    };
  },
  mounted() {
    this.getWorkshops();
    this.getCars();
  },
  methods: {
    checkWorkshops() {
      router.push("/WorkshopList");
    },
    async getWorkshops() {
      this.worskhops = await worskhops();
    },
    async getCars() {
      this.Cars = await fetchUserCars();
      console.log(this.Cars)
    },
    async FetchWorkshopServices() {
      const selectedWorkshopId = this.$refs.workshopSelect.value; 
      if (selectedWorkshopId) {
        this.sevices = await fetchTypes(selectedWorkshopId);
      } else {
        this.sevices = [];
      }
    },
    async sendMessage() {
      this.errors = {
        Worskhop: '',
        ServiceType: '',
        Car: '',
        Description: ''
      };

      let isValid = true;

      const selectedWorkshop = this.$refs.workshopSelect.value;
      const selectedService = this.$refs.serviceSelect.value;
      const selectedCar = this.$refs.carSelect.value;
      const description = this.$refs.descriptionTextarea.value;

      if (!selectedWorkshop) {
        this.errors.Worskhop = 'Workshop is required.';
        isValid = false;
      }
      if (!selectedService) {
        this.errors.ServiceType = 'Service type is required.';
        isValid = false;
      }
      if (!selectedCar) {
        this.errors.Car = 'Car is required.';
        isValid = false;
      }
      if (!description) {
        this.errors.Description = 'Description is required.';
        isValid = false;
      }

      if (!isValid) return; 

      const message = { 
        workshopId: selectedWorkshop,
        service: selectedService,
        carId: selectedCar,
        description: description
      }
      await sendMessage(message)
    }
  }
}
</script>

<style>
.formMessageContainer {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 50vh;
  padding-top: 2.5vw;
  position: relative;
}

form > .formMessage {
  width: fit-content;
  padding: 4vw;
  border: 1px solid #ddd;
  border-radius: 8px;
  background-color: #fff;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.button-top-right {
  position: absolute;
  top: 20px;
  right: 20px;
}

.text-danger {
  color: red;
}
</style>
