<template>
  <div>
    <Toast position="top-center" />
    <Dialog v-model:visible="visible" modal header="Are you sure you want to remove the car?"
      :style="{ width: '20rem' }" :breakpoints="{ '1199px': '75vw', '575px': '90vw' }">
      <button type="button" class="btn btn-outline-info" @click="visible = false">Cancel</button>
      <button type="button" class="btn btn-outline-danger" @click="deleteCar()">Delete</button>
    </Dialog>

    <div class="CarDetais">
      <p><strong :style="[getCarColor(this.car.color)]">VIN: </strong> {{ this.car.vin }}</p>
      <p><strong :style="[getCarColor(this.car.color)]">Brand: </strong> {{ this.car.brand }}</p>
      <p><strong :style="[getCarColor(this.car.color)]">Model: </strong> {{ this.car.model }}</p>
      <p><strong :style="[getCarColor(this.car.color)]">Type: </strong> {{ this.car.type }}</p>
      <p><strong :style="[getCarColor(this.car.color)]">Mileage: </strong> {{ this.car.mileage }}</p>
      <p><strong :style="[getCarColor(this.car.color)]">Engine: </strong> {{ this.car.engine }}</p>
      <p><strong :style="[getCarColor(this.car.color)]">Production Year: </strong> {{
        this.convertDate(this.car.dateOfProduction) }}</p>
      <p><strong :style="[getCarColor(this.car.color)]">Door Count: </strong> {{ this.car.doorCount }}</p>
    </div>

    <div class="details-functional-buttons">
      <button type="button" class="btn btn-outline-info" @click="CarRepairsHistory">Repairs History</button>
      <button type="button" class="btn btn-outline-warning" @click="this.UpdateDetails()">Update Car Details</button>
      <button type="button" class="btn btn-outline-danger" @click="visible = true">Delete Car</button>
    </div>

    <div class="visit-section" v-if="paginatedAccidents.length > 0">
      <h4><strong>Mechanical Visits...</strong></h4>
      <div class="visit-cardSection">
        <div v-for="(accident, index) in paginatedAccidents" :key="index" class="card">
          <div class="card-body" :style="visitStatus(accident.status)">
            <h5 class="card-title"><strong>Type of Visit: {{ formatTypeOfVisit(accident.service) }}</strong></h5>
            <p class="card-text">Date: {{ formatDate(accident.date) }}</p>
            <p class="card-text">Workshop: {{ accident.workshopName }}</p>
          </div>
          <div class="btn-group no-rounded-top" role="group" aria-label="Basic example">
            <button type="button" class="btn btn-warning" v-on:click="CheckStatusHistory(accident.id)">Status History</button>
            <button type="button" class="btn btn-warning" v-on:click="RepairHistory(accident.id)"
              v-if="accident.status === 'ReadyToReceive' || accident.status === 'Retrieved'">Repair History</button>
          </div>
        </div>
      </div>
      <Paginator v-model:first="first" :rows="rowsPerPage" :totalRecords="accidents.length" :pageLinkSize="5"
        class="paginator" />
    </div>
  </div>
</template>

<script>
import { tryDelete } from "../../services/CarApiCommunication";
import router from '@/router';
import { getCarAccidents } from "../../services/CarApiCommunication";

export default {
  name: 'CarDetails',
  data() {
    return {
      car: null,
      visible: false,
      accidents: [],
      first: 0,
      rowsPerPage: 5
    };
  },
  computed: {
    paginatedAccidents() {
      const start = this.first;
      const end = this.first + this.rowsPerPage;
      return this.accidents.slice(start, end);
    }
  },
  created() {
    this.fetchCarByVIN();
  },
  mounted() {
    this.getAccidents();
  },
  methods: {
    UpdateDetails() {
      router.push(`/ChangeCarDetails/${this.$route.params.vin}`);
    },
    fetchCarByVIN() {
      const userCars = JSON.parse(localStorage.getItem("userCars")) || [];
      const car = userCars.find(car => car.vin == this.$route.params.vin);
      if (car) {
        this.car = car;
      } else {
        console.warn("No car found with the specified VIN");
      }
    },
    getCarColor(color) {
      return `color: rgb(${color.red}, ${color.green}, ${color.blue})`;
    },
    convertDate(date) {
      return new Date(date).getFullYear().toString();
    },
    visitStatus(visit) {
      if (visit === "WaitingForCar") {
        return `background-color: rgb(173, 216, 230);`;
      } else if (visit === "Progress") {
        return `background-color: rgb(255, 255, 0);`;
      } else if (visit === "ReadyToReceive") {
        return `background-color: rgb(144, 238, 144);`;
      } else if (visit === "Retrieved") {
        return `background-color: rgb(0, 128, 0);`;
      } else {
        return `background-color: rgb(220, 220, 220);`;
      }
    },
    deleteCar() {
      tryDelete(this.car.vin);
      localStorage.clear();
      this.$toast.add({ severity: 'success', summary: 'Car removed successfully', life: 3000 });
      setTimeout(() => {
        router.push("/UserInfo");
      }, 3000);
    },
    async getAccidents() {
      console.log(this.car.vin);
      this.accidents = await getCarAccidents(this.car.vin);
    },
    formatTypeOfVisit(service) {
      const types = {
        "OilChange": "Oil Change",
        "CarInspection": "Car Inspection",
        "MechanicalService": "Mechanical Service",
        "FilterReplacement": "Filter Replacement",
        "FluidReplacement": "Fluid Replacement"
      };
      return types[service] || service;
    },
    formatDate(date) {
      const options = { year: 'numeric', month: 'long', day: 'numeric' };
      return new Date(date).toLocaleDateString('pl-PL', options);
    },
    CheckStatusHistory(id) {
      this.$router.push({ name: 'StatusHistory', params: { id: id } });
    },
    RepairHistory(id) {
      this.$router.push({ name: 'RepairHistory', params: { id: id } });
    },
    CarRepairsHistory(){
      this.$router.push({ name: 'RepairsHistory', params: { id: this.car.vin } });
    }
  }
};
</script>

<style scoped>
.CarDetais {
  text-align: left;
  width: fit-content;
  height: fit-content;
  padding-top: 4vh;
}

.CarDetais p {
  font-size: 110%;
  margin-top: 0.1rem;
  margin-left: 4vw;
}

.visit-cardSection {
  display: flex;
  flex-wrap: wrap;
  gap: 2vw;
  justify-content: center;
  padding: 2vw 4vw;
  max-width: 100%;
}

.visit-cardSection .card {
  width: 18vw;
  border: 2px solid black;
  border-radius: 8px;
  box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
}

.paginator {
  margin-top: 3vh;
  display: flex;
  justify-content: center;
  border: 0px;
}

.visit-section h4 {
  text-align: left;
  padding-left: 4vw;
}

.details-functional-buttons {
  top: 12vh;
  right: 10vw;
  position: absolute;
}

.details-functional-buttons .btn {
  width: 20vw;
  font-size: 1.3vw;
  letter-spacing: 0.125vw;
  display: block;
  margin-top: 2vh;
}

.p-dialog-content .btn-outline-danger {
  margin-left: 6vw;
}

.no-rounded-top .btn:first-child {
  border-top-left-radius: 0;
}

.no-rounded-top .btn:last-child {
  border-top-right-radius: 0;
}

.no-rounded-top .btn {
  border-radius: 0;
}
</style>
