<template>
  <div class="accident-info">
    <h2>Repair Details</h2>

    <div class="user-info" v-if="Accident.userName">
      <h3>User Information</h3>
      <p><strong>User:</strong> {{ Accident.userName }}</p>
      <p><strong>Contact:</strong> {{ Accident.userContact }}</p>
    </div>

    <div class="car-info" v-if="Accident.car">
      <h3>Car Information</h3>
      <p><strong>Brand:</strong> {{ Accident.car.brand }}</p>
      <p><strong>Model:</strong> {{ Accident.car.model }}</p>
      <p><strong>Mileage:</strong> {{ Accident.car.mileage }} km</p>
      <p><strong>Engine:</strong> {{ Accident.car.engine }}</p>
    </div>

    <div class="service-info">
      <h3>Service Information</h3>
      <p><strong>Service Type:</strong> {{ formatService(Accident.service) }}</p>
      <p><strong>Status:</strong> {{ formatStatus(Accident.status) }}</p>
      <p><strong>Scheduled Date:</strong> {{ formatDate(Accident.date) }}</p>
      <p v-if="Accident.userMessage"><strong>Message:</strong> {{ Accident.userMessage }}</p>
    </div>

    <button style="margin-bottom: 1vh; width: 100%;" v-if="this.Accident.status === 'ReadyToReceive'" @click="UpdateHistory" class="btn btn-warning">Upadate History</button>
    <button style="margin-bottom: 1vh; width: 100%;" v-if="this.Accident.status === 'Retrieved'" @click="HistoryDetails" class="btn btn-info">Show History</button>
    <button v-if="this.Accident.status !== 'Retrieved'" @click="changeStatus" class="status-button">{{ getNextStatus() }}</button>
    
  </div>
</template>

<script>
import { GetAccidentFullInfo, changeAccidentStatus } from '@/ApiCommunication/Accident';

export default {
  name: 'AccidentFullInfo',
  props: {
    id: String
  },
  data() {
    return {
      Accident: {}
    };
  },
  mounted() {
    this.getAccident();
  },
  methods: {
    async getAccident() {
      this.Accident = await GetAccidentFullInfo(this.id);
    },
    formatService(service) {
      return service === 'MechanicalService' ? 'Mechanical Service' : service;
    },
    formatStatus(status) {
      const statuses = {
        WaitingForCar: 'Waiting for Car',
        Progress: 'In Progress',
        ReadyToReceive: 'Ready to Receive',
        Retrieved: 'Retrieved'
      };
      return statuses[status] || status;
    },
    formatDate(date) {
      const options = { day: '2-digit', month: '2-digit', year: 'numeric', hour: '2-digit', minute: '2-digit' };
      return new Date(date).toLocaleDateString(undefined, options);
    },
    async changeStatus() {
      if (this.Accident.status === "Progress")
        this.$router.push({ name: 'AddRepairHistory', params: { id: this.id, action: "AddHistory" } });
      else {
        var result = await changeAccidentStatus(this.id);
        if (result) {
          this.Accident = await GetAccidentFullInfo(this.id);
        }
      }
    },
    UpdateHistory() {
        this.$router.push({ name: 'AddRepairHistory', params: { id: this.id, action: "UpdateHistory" } });
    },
    HistoryDetails() {
        this.$router.push({ name: 'RepairHistoryDetails', params: { id: this.id} });
    },
    getNextStatus() {
      if (this.Accident.status == "WaitingForCar") {
        return "Workshop takes care of the car"
      }
      if (this.Accident.status == "Progress") {
        return "Workshop has finished the service work"
      }
      if (this.Accident.status == "ReadyToReceive") {
        return "The car has been received"
      }
    }
  }
};
</script>

<style scoped>
.accident-info {
  max-width: 600px;
  margin: 0 auto;
  padding: 20px;
  border: 1px solid #ddd;
  border-radius: 8px;
  background-color: #f9f9f9;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
}

h2,
h3 {
  color: #333;
  text-align: center;
  margin-bottom: 10px;
}

.car-info,
.service-info {
  margin-bottom: 20px;
}

p {
  margin: 5px 0;
  color: #555;
}

.status-button {
  display: block;
  width: 100%;
  padding: 10px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 5px;
  font-size: 1em;
  cursor: pointer;
  transition: background-color 0.3s;
}

.status-button:hover {
  background-color: #0056b3;
}
</style>