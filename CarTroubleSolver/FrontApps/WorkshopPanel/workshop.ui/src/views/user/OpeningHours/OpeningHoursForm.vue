<template>
  <div class="form-container">
    <h2>Opening Hours</h2>
    <form @submit.prevent="submitForm" class="form-content">
      <Toast v-if="showToast" :msg="toastMsg" :color="toastColor" />
      <div class="grid-container">
        <div class="column">
          <div v-for="(day, index) in daysOfWeek.slice(0, 3)" :key="index" class="day-section">
            <h3>{{ day }}</h3>
            <SelectInput
              label="From"
              v-model="openingTimes[day].from"
              :dataArray="hours"
              :width="10"
            />
            <SelectInput
              label="To"
              v-model="openingTimes[day].to"
              :dataArray="hours"
              :width="10"
            />
            <p v-if="errorMessages[day]" class="error-message">{{ errorMessages[day] }}</p>
          </div>
        </div>
        <div class="column">
          <div v-for="(day, index) in daysOfWeek.slice(3)" :key="index + 3" class="day-section">
            <h3>{{ day }}</h3>
            <SelectInput
              label="From"
              v-model="openingTimes[day].from"
              :dataArray="hours"
              :width="10"
            />
            <SelectInput
              label="To"
              v-model="openingTimes[day].to"
              :dataArray="hours"
              :width="10"
            />
            <p v-if="errorMessages[day]" class="error-message">{{ errorMessages[day] }}</p>
          </div>
          <button type="submit">Zapisz godziny</button>
        </div>
      </div>
    </form>
  </div>
</template>

<script>
import SelectInput from '../../../components/Form/SelectInput.vue';
import { UpdateHours } from '@/ApiCommunication/workshop';
import { getFromLocalStorage } from '@/Services/LocalStorageService';
import Toast from '@/components/Toast.vue';
import router from '@/router';

export default {
  name: 'OpeningHoursForm',
  components: {
    SelectInput, Toast
  },
  data() {
    const days = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];
    return {
      showToast: false,
      toastMsg: '',
      toastColor: '',
      daysOfWeek: days,
      openingTimes: days.reduce((acc, day) => {
        acc[day] = { from: '', to: '' }; 
        return acc;
      }, {}),
      errorMessages: days.reduce((acc, day) => {
        acc[day] = ''; 
        return acc;
      }, {}),
      hours: [
        '06:00', '07:00', '08:00', '09:00', '10:00', '11:00',
        '12:00', '13:00', '14:00', '15:00', '16:00', '17:00',
        '18:00', '19:00', '20:00', '21:00', '22:00'
      ]
    };
  },
  methods: {
    async submitForm() {
    let hasErrors = false;
    const newErrorMessages = { ...this.errorMessages };

    this.daysOfWeek.forEach(day => {
      const { from, to } = this.openingTimes[day];

      if (!from || !to) {
        newErrorMessages[day] = 'Both "From" and "To" times are required';
        hasErrors = true;
      } else if (from >= to) {
        newErrorMessages[day] = 'The time "To" must be later than "From"';
        hasErrors = true;
      } else {
        newErrorMessages[day] = '';
      }
    });

    this.errorMessages = newErrorMessages;

    if (!hasErrors) {
      const workingHoursList = this.daysOfWeek.map(day => ({
        DayOfWeek: day,
        From: this.openingTimes[day].from,
        To: this.openingTimes[day].to,
        WorkshopId: getFromLocalStorage("Workshop").id
      }));

      try {
        await UpdateHours(workingHoursList);
        this.showToastMessage('Hours updated successfully', 'success');
        setTimeout(() => {
          router.push('/Account')
        }, 2000);
      } catch (error) {
        console.error('Error while updating hours:', error);
        this.showToastMessage('Some errors occured', 'warning');
      }
    }
  },
  showToastMessage(message, color) {
      this.toastMsg = message;
      this.showToast = true;
      this.toastColor = color;
      setTimeout(() => {
        this.showToast = false;
      }, 3000);
    },
  }
};
</script>
  
  <style scoped>
  .form-container {
    max-width: 600px; 
    margin: 0 auto;
    padding: 15px;
    border: 1px solid #ccc;
    border-radius: 5px;
    background-color: #f9f9f9;
  }
  
  .form-content {
    display: flex;
    justify-content: space-between;
    align-items: flex-start;
  }
  
  .grid-container {
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: 20px;
    flex: 1;
  }
  
  .column {
    display: flex;
    flex-direction: column;
  }
  
  .day-section {
    margin-bottom: 15px;
  }
  
  h3 {
    margin: 10px 0;
    font-size: 16px; 
  }
  
  .error-message {
    color: red;
    font-size: 13px; 
    margin-bottom: 8px; 
  }

  button {
    padding: 8px 12px;
    background-color: #007BFF;
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    font-size: 14px; 
    margin-top: 16vh;
  }
  
  button:hover {
    background-color: #0056b3;
  }
  </style>
  