<template>
  <div class="accidents-grid">
    <h2>Accidents</h2>
    
    <!-- Status Filter -->
    <div class="filter-container">
      <label for="status-filter">Filter by Status:</label>
      <select v-model="selectedStatus" id="status-filter" @change="filterAccidents">
        <option value="">All</option>
        <option value="WaitingForCar">Waiting for Car</option>
        <option value="Progress">In Progress</option>
        <option value="ReadyToReceive">Ready to Receive</option>
        <option value="Retrieved">Retrieved</option>
      </select>
    </div>

    <div v-if="filteredAccidents.length > 0" class="grid">
      <div v-for="accident in filteredAccidents" :key="accident.id" class="accident-card"
        :class="getStatusClass(accident.status)">
        <div class="card-header">
          <h4>{{ accident.brand }} {{ accident.model }}</h4>
          <span class="status">{{ accident.status }}</span>
        </div>
        <div class="card-body">
          <p><strong>VIN:</strong> {{ accident.vin }}</p>
          <p><strong>Engine:</strong> {{ accident.engine }}</p>
          <p><strong>Service:</strong> {{ accident.service }}</p>
        </div>
        <div class="card-footer">
          <p><strong>Date:</strong> {{ new Date(accident.date).toLocaleDateString() }}</p>
          <button class="details-button" v-on:click="AccidentDetalis(accident.id)">Details</button>
        </div>
      </div>
    </div>

    <p v-else class="no-accidents-message">No accidents found.</p>
  </div>
</template>

<script>
import { GetAccidents } from '@/ApiCommunication/Accident';

export default {
  name: 'Accidents',
  data() {
    return {
      accidents: [],
      selectedStatus: '',  // Store selected status for filtering
      filteredAccidents: []  // Store filtered accidents
    }
  },
  mounted() {
    this.getAccidents();
  },
  methods: {
    async getAccidents() {
      this.accidents = await GetAccidents();
      this.filteredAccidents = this.accidents;  // Initially display all accidents
    },
    
    filterAccidents() {
      if (this.selectedStatus) {
        this.filteredAccidents = this.accidents.filter(accident => accident.status === this.selectedStatus);
      } else {
        this.filteredAccidents = this.accidents;  // Show all accidents if no filter is selected
      }
    },

    getStatusClass(status) {
      switch (status) {
        case 'WaitingForCar':
          return 'waiting-for-car';
        case 'Progress':
          return 'in-progress';
        case 'ReadyToReceive':
          return 'ready-to-receive';
        case 'Retrieved':
          return 'retrieved';
        default:
          return '';
      }
    },
    
    AccidentDetalis(id) {
      this.$router.push({ name: 'AccidentFullInfo', params: { id: id } });
    }
  }
}
</script>

<style scoped>
.accidents-grid {
  max-width: 80vw;
  height: fit-content;
  margin: 0 auto;
  padding: 2vh;
}

h2 {
  text-align: center;
  margin-bottom: 1vh;
  color: #333;
}

.filter-container {
  margin-bottom: 1.5vh;
  display: flex;
  align-items: center;
  justify-content: center;
}

.filter-container label {
  margin-right: 10px;
  font-size: 1em;
}

.filter-container select {
  padding: 8px;
  font-size: 1em;
  border-radius: 5px;
  border: 1px solid #ccc;
}

.grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
  column-gap: 4vw;
}

.accident-card {
  position: relative;
  background: #f9f9f9;
  border-radius: 8px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  padding: 15px;
  transition: transform 0.2s;
  height: auto;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  margin-bottom: 5vh;
}

.accident-card:hover {
  transform: scale(1.02);
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.status {
  background-color: #e0e0e0;
  border-radius: 12px;
  padding: 5px 10px;
  font-size: 0.75em;
  color: #333;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.card-body {
  width: auto;
  overflow-wrap: break-word;
}

.card-footer {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.details-button {
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 5px;
  padding: 8px 12px;
  cursor: pointer;
  transition: background-color 0.3s;
  font-size: 0.9em;
}

.details-button:hover {
  background-color: #0056b3;
}

.accident-card::before {
  content: '';
  position: absolute;
  left: 0;
  top: 0;
  bottom: 0;
  width: 5px;
  background-color: transparent;
  z-index: 0;
}

.waiting-for-car::before {
  background-color: #f9c74f;
}

.in-progress::before {
  background-color: #f94144;
}

.ready-to-receive::before {
  background-color: #43aa8b;
}

.retrieved::before {
  background-color: #577590;
}

.no-accidents-message {
  text-align: center;
  color: #666;
  font-size: 1.2em;
  margin-top: 20px;
}
</style>
