<template>
    <div>
      <h2>Status History</h2>
      <ul class="timeline">
        <li v-for="status in formattedStatuses" :key="status.id">
          <div class="timeline-content">
            <h3>{{ status.date }}</h3>
            <p>Status: <strong>{{ formatStatusName(status.status) }}</strong></p>
            <p>Explanation: {{ getStatusExplanation(status.status) }}</p>
          </div>
        </li>
      </ul>
    </div>
  </template>
  
  <script>
  import { getStatusHistory } from '@/services/StatusCommunication';
  
  export default {
    name: 'StatusHistory',
    props: {
      id: String,
    },
    data() {
      return {
        statuses: [],
      };
    },
    computed: {
      formattedStatuses() {
        return this.statuses.map((status) => {
          const date = new Date(status.date);
          const options = { day: '2-digit', month: 'long', year: 'numeric', hour: '2-digit', minute: '2-digit' };
          const formattedDate = date.toLocaleDateString('en-GB', options);
          return {
            ...status,
            date: formattedDate,
          };
        });
      },
    },
    mounted() {
      this.getStatus();
    },
    methods: {
      async getStatus() {
        this.statuses = await getStatusHistory(this.id);
      },
      formatStatusName(status) {
        return status.replace(/([a-z])([A-Z])/g, '$1 $2');
      },
      getStatusExplanation(status) {
        const explanations = {
          Retrieved: "The car was received.",
          ReadyToReceive: "The car is ready for pickup.",
          Progress: "It is performed ordered service on the car.",
          WaitingForCar: "The workshop is waiting for the car to be delivered.",
        };
        return explanations[status] || "No explanation available for this status.";
      },
    },
  };
  </script>
  
  <style>
  .timeline {
    list-style: none;
    padding: 0;
  }
  .timeline li {
    position: relative;
    padding: 10px 0;
  }
  .timeline-content {
    padding: 10px;
    background: #f4f4f4;
    border-radius: 5px;
  }
  .timeline-content h3 {
    margin: 0 0 5px;
  }
  .timeline-content p {
    margin: 5px 0;
  }
  </style>
  